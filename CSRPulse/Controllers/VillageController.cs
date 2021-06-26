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
using CSRPulse.ExportImport.Interfaces;
using System.Data;

namespace CSRPulse.Controllers
{
    [Route("[Controller]/[action]")]
    public class VillageController : BaseController<VillageController>
    {
        private readonly IBlockServices _blockServices;
        private readonly IVillageServices _villageServices;
        private readonly IDropdownBindService _ddlService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExport _export;
        public VillageController(IBlockServices blockServices,IVillageServices villageServices, IDropdownBindService ddlService, IWebHostEnvironment webHostEnvironment, IExport export)
        {
            _blockServices = blockServices;
            _villageServices = villageServices;
            _ddlService = ddlService;
            _webHostEnvironment = webHostEnvironment;
            _export = export;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("VillageController/Index");
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
        public async Task<PartialViewResult> GetVillageList(Village village)
        {
            _logger.LogInformation("VillageController/GetVillageList");
            try
            {
                var result = await _villageServices.GetVillageList(village);
                return PartialView("_VillageList",result);
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
            return View(new Village());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Village village)
        {
            try
            {
                _logger.LogInformation("VillageController/Create");
                ModelState.Remove("IsActive");
                BindDropdowns();
                if (ModelState.IsValid)
                {

                    village.CreatedBy = userDetail == null ? 1 : userDetail.UserID;
                    village.IsActive = true;
                    if (await _villageServices.RecordExist(village))
                    {
                        ModelState.AddModelError("", "Village or Village Code already exists");
                    }
                    else
                    {
                        var result = await _villageServices.CreateVillage(village);
                        if (village.RecordExist)
                        {
                            ModelState.AddModelError("", "Village already exists");
                        }
                        if (result)
                        {
                            TempData["Message"] = "Village Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(village);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
       
        public async Task<IActionResult> Edit(int rid)
        {
            _logger.LogInformation("VillageController/Edit");
            try
            {
                BindDropdowns();
                var uDetail = await _villageServices.GetVillageById(rid);
                ViewBag.ddlDistrict = GetDistrict(uDetail.StateId, null);
                ViewBag.ddlBlock = GetBlock(uDetail.StateId, uDetail.DistrictId, null);
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
        public async Task<IActionResult> Edit(Village village)
        {
            try
            {
                _logger.LogInformation("VillageController/Edit");
                ModelState.Remove("IsActive");
                BindDropdowns();
                ViewBag.ddlDistrict = GetDistrict(village.StateId, null);
                ViewBag.ddlBlock = GetBlock(village.StateId, village.DistrictId, null);
                if (ModelState.IsValid)
                {
                    village.UpdatedBy = userDetail == null ? 1 : userDetail.UserID;
                    village.UpdatedOn = DateTime.Now;
                    var result = await _villageServices.UpdateVillage(village);
                    if (village.RecordExist)
                    {
                        ModelState.AddModelError("", "Village already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "Village Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(village);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }


        public JsonResult BindDistrict(int stateId)
        {
            _logger.LogInformation($"VillageController/BindDistrict/stateId={stateId}");
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
        public JsonResult BindBlock(int stateId, int districtId)
        {
            _logger.LogInformation($"VillageController/BindBlock/stateId={stateId}/districtId={districtId}");
            try
            {
                return Json(GetBlock(stateId, districtId, null));
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
            _logger.LogInformation($"VillageController/GetDistrict/stateId={stateId}");
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

        public SelectList GetBlock(int? stateId, int? districtId, int? blockId)
        {
            _logger.LogInformation($"VillageController/GetBlock/stateId={stateId}/districtId={districtId}");
            try
            {
                var blockList = _ddlService.GetBlock(stateId, districtId, null);
                var ddlBlock = new SelectList(blockList, "id", "value");
                return ddlBlock;
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
            _logger.LogInformation("VillageController/ActiveDeActive");
            var result = _villageServices.ActiveDeActive(id, isChecked);
            return Json(result);
        }

        public async Task<IActionResult> ExportTemplate()
        {
            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, @"Templates\Location\VillageTemplate.xlsx");
            if (!System.IO.File.Exists(filepath))
                return Content($"Template not found.");

            return await DownloadFile(filepath);
        }

        public async Task<IActionResult> ExportRefTemplate()
        {
            var model = new Block();
            model.IsActive = true;
            var blocklist = await _blockServices.GetBlockList(model);

            List<string> takeColumns = new List<string> { "StateId", "StateName", "DistrictId", "DistrictName", "BlockId", "BlockName" };
            var blockdt = Common.ExtensionMethods.ToDataTable<Block>(blocklist);
            var dt = Common.ExtensionMethods.PrepairExportTable(blockdt, takeColumns);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            var fname = string.Format(@"{0}.xlsx", DateTime.Now.Ticks);

            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, @"TempFiles\" + fname);

            if (_export.ExportToExcel(ds, filepath, "RefrenceCode_Village"))
            {
                if (!System.IO.File.Exists(filepath))
                    return Content($"Tempfile path not found.");

                var res = await DownloadFile(filepath);

                Common.ExtensionMethods.DeleteFile(filepath);
                return res;
            }
            else
            {
                return Content($"Village refrence code file downloding filed.");
            }
        
        }
    }
}
