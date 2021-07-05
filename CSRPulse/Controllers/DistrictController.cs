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
    public class DistrictController : BaseController<DistrictController>
    {
        private readonly IDistrictServices _districtServices;
        private readonly IStateServices _stateServices;
        private readonly IDropdownBindService _ddlService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExport _export;
        public DistrictController(IDistrictServices districtServices, IDropdownBindService ddlService, IWebHostEnvironment webHostEnvironment, IStateServices stateServices, IExport export)
        {
            _districtServices = districtServices;
            _ddlService = ddlService;
            _webHostEnvironment = webHostEnvironment;
            _stateServices = stateServices;
            _export = export;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("DistrictController/Index");
            try
            {
                BindDropdowns();
                //  var result = await _districtServices.GetDistrictList();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public async Task<PartialViewResult> GetDistrictList(District district)
        {
            _logger.LogInformation("DistrictController/GetDistrictList");
            try
            {
                var result = await _districtServices.GetDistrictList(district);
                return PartialView("_DistrictList", result);
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
            return View(new District());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(District district)
        {
            try
            {
                _logger.LogInformation("DistrictController/Create");
                ModelState.Remove("IsActive");
                BindDropdowns();
                if (ModelState.IsValid)
                {

                    district.CreatedBy = userDetail == null ? 1 : userDetail.UserID;
                    district.IsActive = true;
                    if (await _districtServices.RecordExist(district))
                    {
                        ModelState.AddModelError("", "District or District Code already exists");
                    }
                    else
                    {
                        var result = await _districtServices.CreateDistrict(district);
                        if (district.RecordExist)
                        {
                            ModelState.AddModelError("", "District already exists");
                        }
                        if (result)
                        {
                            TempData["Message"] = "District Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(district);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int rid)
        {
            try
            {
                BindDropdowns();
                var uDetail = await _districtServices.GetDistrictById(rid);
                return View(uDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(District district)
        {
            try
            {
                _logger.LogInformation("DistrictController/Edit");
                ModelState.Remove("IsActive");
                BindDropdowns();
                if (ModelState.IsValid)
                {
                    district.UpdatedBy = userDetail == null ? 1 : userDetail.UserID;
                    district.UpdatedOn = DateTime.Now;
                    var result = await _districtServices.UpdateDistrict(district);
                    if (district.RecordExist)
                    {
                        ModelState.AddModelError("", "District already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "District Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(district);
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
            _logger.LogInformation("UoMController/ActiveDeActive");
            var result = _districtServices.ActiveDeActive(id, isChecked);
            return Json(result);
        }

        public async Task<IActionResult> ExportTemplate()
        {
            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, @"Templates\Location\DistrictTemplate.xlsx");

            if (!System.IO.File.Exists(filepath))
                return Content($"Template not found.");

            return await DownloadFile(filepath);
        }

        public async Task<IActionResult> ExportRefTemplate()
        {
            var statelist = await _stateServices.GetStateList();
            List<string> takeColumns = new List<string> { "StateId", "StateName" };
            var statedt = Common.ExtensionMethods.ToDataTable<State>(statelist);


            var dt = Common.ExtensionMethods.PrepairExportTable(statedt, takeColumns);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);


            var fname = string.Format(@"{0}.xlsx", DateTime.Now.Ticks);

            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, @"TempFiles\" + fname);

            if (_export.ExportToExcel(ds, filepath, "RefrenceCode_District"))
            {
                if (!System.IO.File.Exists(filepath))
                    return Content($"Tempfile path not found.");

                var res = await DownloadFile(filepath);

                Common.ExtensionMethods.DeleteFile(filepath);
                return res;

            }
            else
            {
                return Content($"District refrence code file downloding filed.");
            }
        }

        public ViewResult Import()
        {
            return View();
        }
        
    }
}
