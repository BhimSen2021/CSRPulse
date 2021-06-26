using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using System.Data;
using CSRPulse.ExportImport.Interfaces;

namespace CSRPulse.Controllers
{
    [Route("[Controller]/[action]")]
    public class BlockController : BaseController<BlockController>
    {
        private readonly IDistrictServices _districtServices;
        private readonly IBlockServices _blockServices;
        private readonly IDropdownBindService _ddlService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExport _export;
        public BlockController(IDistrictServices districtServices, IBlockServices blockServices, IDropdownBindService ddlService, IWebHostEnvironment webHostEnvironment, IExport export)
        {
            _districtServices = districtServices;
            _blockServices = blockServices;
            _ddlService = ddlService;
            _webHostEnvironment = webHostEnvironment;
            _export = export;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("BlockController/Index");
            try
            {
                BindDropdowns();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBlockList(Block block)
        {
            _logger.LogInformation("BlockController/Index");
            try
            {
                var result = await _blockServices.GetBlockList(block);
                return PartialView("_BlockList", result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            BindDropdowns();
            return View(new Block());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Block block)
        {
            try
            {
                _logger.LogInformation("BlockController/Create");
                ModelState.Remove("IsActive");
                BindDropdowns();
                if (ModelState.IsValid)
                {

                    block.CreatedBy = userDetail == null ? 1 : userDetail.UserID;
                    block.IsActive = true;
                    if (await _blockServices.RecordExist(block))
                    {
                        ModelState.AddModelError("", "Block or Block Code already exists");
                    }
                    else
                    {
                        var result = await _blockServices.CreateBlock(block);
                        if (block.RecordExist)
                        {
                            ModelState.AddModelError("", "Block already exists");
                        }
                        if (result)
                        {
                            TempData["Message"] = "Block Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(block);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }


        public async Task<IActionResult> Edit(int rid)
        {
            _logger.LogInformation("BlockController/Edit");
            try
            {
                BindDropdowns();
                var uDetail = await _blockServices.GetBlockById(rid);
                ViewBag.ddlDistrict = GetDistrict(uDetail.StateId, null);
                return View(uDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Block block)
        {
            try
            {
                _logger.LogInformation("BlockController/Edit");
                ModelState.Remove("IsActive");
                BindDropdowns();
                ViewBag.ddlDistrict = GetDistrict(block.StateId, null);
                if (ModelState.IsValid)
                {
                    block.UpdatedBy = userDetail == null ? 1 : userDetail.UserID;
                    block.UpdatedOn = DateTime.Now;
                    var result = await _blockServices.UpdateBlock(block);
                    if (block.RecordExist)
                    {
                        ModelState.AddModelError("", "Block already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "Block Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(block);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }


        public JsonResult BindDistrict(int stateId)
        {
            _logger.LogInformation($"BlockController/BindDistrict/stateId={stateId}");
            try
            {
                return Json(GetDistrict(stateId, null));
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        [NonAction]
        public SelectList GetDistrict(int? stateId, int? districtId)
        {
            _logger.LogInformation($"BlockController/GetDistrict/stateId={stateId}");
            try
            {
                var districtList = _ddlService.GetDistrict(stateId, null);
                var ddlDistrict = new SelectList(districtList, "id", "value");
                return ddlDistrict;
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [NonAction]
        void BindDropdowns()
        {
            var stateList = _ddlService.GetStateAsync(null, null);
            ViewBag.ddlState = new SelectList(stateList, "id", "value");
        }

        [HttpPost]
        public JsonResult ActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("BlockController/ActiveDeActive");
            var result = _blockServices.ActiveDeActive(id, isChecked);
            return Json(result);

        }

        public async Task<IActionResult> ExportTemplate()
        {
            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, @"Templates\Location\BlockTemplate.xlsx");
            if (!System.IO.File.Exists(filepath))
                return Content($"Template not found.");

            return await DownloadFile(filepath);
        }

        public async Task<IActionResult> ExportRefTemplate()
        {
            var model = new District();
            model.IsActive = true;
            var districtlist = await _districtServices.GetDistrictList(model);

            List<string> takeColumns = new List<string> { "StateId", "StateName", "DistrictId", "DistrictName" };
            var districtdt = Common.ExtensionMethods.ToDataTable<District>(districtlist);
            var dt = Common.ExtensionMethods.PrepairExportTable(districtdt, takeColumns);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);


            var fname = string.Format(@"{0}.xlsx", DateTime.Now.Ticks);

            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, @"TempFiles\" + fname);

            if (_export.ExportToExcel(ds, filepath, "RefrenceCode_Block"))
            {
                if (!System.IO.File.Exists(filepath))
                    return Content($"Tempfile path not found.");

                var res = await DownloadFile(filepath);

                Common.ExtensionMethods.DeleteFile(filepath);
                return res;

            }
            else
            {
                return Content($"Block refrence code file downloding filed.");
            }

        }
    }
}
