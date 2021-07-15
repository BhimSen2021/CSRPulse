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
using static CSRPulse.Common.FileHelper;
using CSRPulse.Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using CSRPulse.Models;

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



        [HttpPost]
        public ActionResult _ImportDistrictData(IFormFile file)
        {
            _logger.LogInformation("DistrictController/_ImportDistrictData");
            try
            {
                string fileName = string.Empty; string filePath = string.Empty;
                int error = 0, warning = 0, duplicateEntries = 0;
                List<DistrictImport> importDistrictSave = new List<DistrictImport>();
                List<string> columnName = new List<string>();
                List<string> missingHeaders;
                string msg = string.Empty;
                DistrictImportModel districtImpModel = new DistrictImportModel();
                List<DistrictImport> DistrictForm = new List<DistrictImport>();
                if (file.Length > 0)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    fileName = Path.GetFileName(file.FileName);

                    var contentType = Path.GetExtension(file.FileName);
                    var dicValue = GetDictionaryValueByKeyName(".xlsx");
                    if ((fileExtension == ".xlsx" || fileExtension == ".xlx"))
                    {
                        #region Upload File At temp location===
                        fileName = ExtensionMethods.SetUniqueFileName(Path.GetFileNameWithoutExtension(file.FileName),
                               Path.GetExtension(file.FileName));

                        filePath = @"Templates\Location\";
                        var uploadedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
                        string sPhysicalPath = Path.Combine(uploadedFilePath, fileName);

                        using (FileStream stream = new FileStream(Path.Combine(uploadedFilePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        #endregion
                        var objDistrict = _districtServices.ReadDistrictExcelData(sPhysicalPath, false, out msg, out error, out warning, out importDistrictSave, out missingHeaders, out columnName);
                        if (error > 0 && msg == "Rows")
                        {
                            //TempData["error"] = -3;
                            districtImpModel.NoOfErrors = 1;
                            districtImpModel.Message = "No record found, Please check the sheet and reupload.";
                            return Json(new { status = "noRecordFound", htmlData = ConvertViewToString("_DistrictImportGridView", districtImpModel, true) });
                        }
                        else if (error > 0 && msg == "MAXROW")
                        {
                            districtImpModel.NoOfErrors = 1;
                            districtImpModel.Message = "Found more than 10,000 records, You can not validate more than 10,000 districts a single sheet.";
                            return Json(new { status = "noRecordFound", htmlData = ConvertViewToString("_BlockImportGridView", districtImpModel, true) });
                        }

                        else
                        {
                            duplicateEntries = objDistrict.GroupBy(x => new { x.DistrictCode }).Sum(g => g.Count() - 1);
                            var getDuplicateRows = ExtensionMethods.FindDuplicates(objDistrict.ToList(), x => new { x.DistrictCode });
                            objDistrict.Join(getDuplicateRows, (x) => new { x.DistrictCode }, (y) => new { y.DistrictCode }, (x, y) =>
                            {
                                x.isDuplicatedRow = ((x.DistrictCode == y.DistrictCode) ? true : false);
                                return x;
                            }).ToList();

                            if (error == 0 && duplicateEntries > 0)
                                error = duplicateEntries;
                            else
                                error += duplicateEntries;

                            var json = JsonConvert.SerializeObject(objDistrict);
                            DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                            if (msg != "Headers")
                            {
                                dataTable.Columns["StateId"].SetOrdinal(0);
                                dataTable.Columns["DistrictCode"].SetOrdinal(1);
                                dataTable.Columns["DistrictName"].SetOrdinal(2);
                                dataTable.Columns["DistrictShort"].SetOrdinal(3);
                                dataTable.Columns["State"].SetOrdinal(4);
                                dataTable.Columns["District"].SetOrdinal(5);
                                dataTable.Columns["error"].SetOrdinal(6);
                                dataTable.Columns["warning"].SetOrdinal(7);
                                dataTable.Columns["isDuplicatedRow"].SetOrdinal(8);
                            }

                            districtImpModel.ErrorMsgCollection = GetErrorMessage();
                            if (dataTable != null)
                            {
                                districtImpModel.DistrictInput = dataTable;
                            }
                            else
                                districtImpModel.DistrictData = DistrictForm;

                            var errors = error;
                            var msgs = msg;

                            districtImpModel.NoOfErrors = errors != 0 ? (int)errors : 0;
                            districtImpModel.Message = msgs != null ? Convert.ToString(msgs) : "";
                            if (districtImpModel.Message != "Headers")
                            {
                                HttpContext.Session.SetComplexData("DistrictSave", importDistrictSave);
                                if (districtImpModel.Message == "noRecord")
                                {
                                    districtImpModel.Message = "No record found, Please check the sheet and reupload.";
                                }
                                else
                                {
                                    if (districtImpModel.NoOfErrors > 0)
                                        districtImpModel.Message = string.Format("{0} error(s) found ,check and correct the sheet data and re-validate the sheet.", districtImpModel.NoOfErrors.ToString());
                                    else
                                        districtImpModel.Message = "Sheet validated successfully, Click on Upload button to upload your data.";
                                }
                            }
                            else
                            {
                                var list = missingHeaders;
                                var missingheader = String.Join(",", list);
                                if (list.Count > 1)
                                    districtImpModel.Message = string.Format("{0} header(s) are missing", missingheader);
                                else
                                    districtImpModel.Message = string.Format("{0} header(s) is missing", missingheader);
                            }
                            return Json(new { status = "success", htmlData = ConvertViewToString("_DistrictImportGridView", districtImpModel, true) });
                        }
                    }
                    else
                    {
                        districtImpModel.NoOfErrors = 1;
                        districtImpModel.Message = "Invalid file format.";
                        return Json(new { status = "success", htmlData = ConvertViewToString("_DistrictImportGridView", districtImpModel, true) });
                    }

                }
                return Json("success");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message + " ,StackTrace: " + ex.StackTrace + " ,DateTimeStamp :" + DateTime.Now);
                throw ex;
            }
        }

        public JsonResult Save()
        {
            _logger.LogInformation("DistrictImportController/SubmitFileData");
            try
            {
                var importDataList = HttpContext.Session.GetComplexData<List<DistrictImport>>("DistrictSave");
                var result = _districtServices.ImportDistrictData(importDataList);
                if (result)
                {
                    HttpContext.Session.Remove("DistrictData");
                    HttpContext.Session.Remove("DistrictSave");
                    HttpContext.Session.Remove("error");
                    HttpContext.Session.Remove("msg");
                    HttpContext.Session.Remove("missingHeaders");
                }
                return Json(new { errorCode = 0, msg = "District data has been imported sucessfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message + " ,StackTrace: " + ex.StackTrace + " ,DateTimeStamp :" + DateTime.Now);
                return Json(new { errorCode = -1, error = ex.Message });

            }

        }
    }
}
