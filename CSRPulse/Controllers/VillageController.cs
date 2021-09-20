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
using Microsoft.AspNetCore.Http;
using static CSRPulse.Common.FileHelper;
using CSRPulse.Common;
using Newtonsoft.Json;
using CSRPulse.Models;

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
        public VillageController(IBlockServices blockServices, IVillageServices villageServices, IDropdownBindService ddlService, IWebHostEnvironment webHostEnvironment, IExport export)
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
                return PartialView("_VillageList", result);
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
            var sPhysicalPath = Path.Combine(_webHostEnvironment.WebRootPath, DocumentUploadFilePath.TemplatesLocationFilePath + "VillageTemplate.xlsx");

            if (!System.IO.File.Exists(sPhysicalPath))
                return Content($"Template not found.");

            return await DownloadFile(sPhysicalPath);
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

            var uploadedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, DocumentUploadFilePath.TempFilePath);
            if (!Directory.Exists(uploadedFilePath))
                Directory.CreateDirectory(uploadedFilePath);

            var sPhysicalPath = Path.Combine(uploadedFilePath, ExtensionMethods.SetUniqueFileName(".xlsx"));

            if (_export.ExportToExcel(ds, sPhysicalPath, "RefrenceCode_Village"))
            {
                if (!System.IO.File.Exists(sPhysicalPath))
                    return Content($"Tempfile path not found.");

                var res = await DownloadFile(sPhysicalPath);

                Common.ExtensionMethods.DeleteFile(sPhysicalPath);
                return res;
            }
            else
            {
                return Content($"Village refrence code file downloding filed.");
            }

        }

        public ViewResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _ImportVillageData(IFormFile file)
        {
            _logger.LogInformation("VillageController/_ImportVillageData");
            try
            {
                string fileName = string.Empty; string filePath = string.Empty;
                int error = 0, warning = 0, duplicateEntries = 0;
                List<VillageImport> importVillageSave = new List<VillageImport>();
                List<string> columnName = new List<string>();
                List<string> missingHeaders;
                string msg = string.Empty;
                VillageImportModel villageImpModel = new VillageImportModel();
                List<VillageImport> VillageForm = new List<VillageImport>();
                if (file.Length > 0)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    if (ValidateFileMimeType(file) && fileExtension == ".xlsx")
                    {
                        #region Upload File At temp location===
                        fileName = ExtensionMethods.SetUniqueFileName(Path.GetFileNameWithoutExtension(file.FileName),
                               Path.GetExtension(file.FileName));

                        filePath = DocumentUploadFilePath.TempFilePath;
                        var uploadedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
                        string sPhysicalPath = Path.Combine(uploadedFilePath, fileName);

                        using (FileStream stream = new FileStream(Path.Combine(uploadedFilePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        #region C H E C K  F O R M U L A S
                        var isFormulas = CheckFormulaInExcelFile(sPhysicalPath);
                        if (isFormulas)
                        {
                            DeleteFile(sPhysicalPath);
                            villageImpModel.NoOfErrors = 1;
                            villageImpModel.Message = "File contains formula or arithmetic operators that is not allowed in system.";
                            return Json(new { status = "success", htmlData = ConvertViewToString("_VillageImportGridView", villageImpModel, true) });
                        }
                        #endregion

                        #endregion
                        var objVillage = _villageServices.ReadVillageExcelData(sPhysicalPath, false, out msg, out error, out warning, out importVillageSave, out missingHeaders, out columnName);
                        if (error > 0 && msg == "Rows")
                        {
                            //TempData["error"] = -3;
                            villageImpModel.NoOfErrors = 1;
                            villageImpModel.Message = "No record found, Please check the sheet and reupload.";
                            return Json(new { status = "noRecordFound", htmlData = ConvertViewToString("_VillageImportGridView", villageImpModel, true) });
                        }
                        else if (error > 0 && msg == "MAXROW")
                        {
                            villageImpModel.NoOfErrors = 1;
                            villageImpModel.Message = "Found more than 10,000 records, You can not validate more than 10,000 villages a single sheet.";
                            return Json(new { status = "noRecordFound", htmlData = ConvertViewToString("_VillageImportGridView", villageImpModel, true) });
                        }
                        else
                        {
                            duplicateEntries = objVillage.GroupBy(x => new { x.VillageCode }).Sum(g => g.Count() - 1);

                            var getDuplicateRows = ExtensionMethods.FindDuplicates(objVillage.ToList(), x => new { x.VillageCode });

                            objVillage.Join(getDuplicateRows, (x) => new { x.VillageCode }, (y) => new { y.VillageCode }, (x, y) =>
                            {
                                x.isDuplicatedRow = ((x.VillageCode == y.VillageCode) ? true : false);
                                return x;
                            }).ToList();

                            if (error == 0 && duplicateEntries > 0)
                                error = duplicateEntries;
                            else
                                error += duplicateEntries;

                            var json = JsonConvert.SerializeObject(objVillage);
                            DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                            if (msg != "Headers")
                            {
                                dataTable.Columns["StateId"].SetOrdinal(0);
                                dataTable.Columns["DistrictId"].SetOrdinal(1);
                                dataTable.Columns["BlockId"].SetOrdinal(2);
                                dataTable.Columns["VillageCode"].SetOrdinal(3);
                                dataTable.Columns["VillageName"].SetOrdinal(4);
                                dataTable.Columns["State"].SetOrdinal(5);
                                dataTable.Columns["District"].SetOrdinal(6);
                                dataTable.Columns["Block"].SetOrdinal(7);
                                dataTable.Columns["Village"].SetOrdinal(8);
                                dataTable.Columns["error"].SetOrdinal(9);
                                dataTable.Columns["warning"].SetOrdinal(10);
                                dataTable.Columns["isDuplicatedRow"].SetOrdinal(11);
                            }

                            villageImpModel.ErrorMsgCollection = GetErrorMessage();
                            if (dataTable != null)
                            {
                                villageImpModel.VillageInput = dataTable;
                            }
                            else
                                villageImpModel.VillageData = VillageForm;

                            var errors = error;
                            var msgs = msg;

                            villageImpModel.NoOfErrors = errors != 0 ? (int)errors : 0;
                            villageImpModel.Message = msgs != null ? Convert.ToString(msgs) : "";
                            if (villageImpModel.Message != "Headers")
                            {
                                HttpContext.Session.SetComplexData("VillageSave", importVillageSave);
                                if (villageImpModel.Message == "noRecord")
                                {
                                    villageImpModel.Message = "No record found, Please check the sheet and reupload.";
                                }
                                else
                                {
                                    if (villageImpModel.NoOfErrors > 0)
                                        villageImpModel.Message = string.Format("{0} error(s) found ,Please check and correct the sheet data and re-validate the sheet.", villageImpModel.NoOfErrors.ToString());
                                    else
                                        villageImpModel.Message = "Sheet validated successfully, Click on Upload button to upload your data.";
                                }
                            }
                            else
                            {
                                var list = missingHeaders;
                                var missingheader = String.Join(",", list);
                                if (list.Count > 1)
                                    villageImpModel.Message = string.Format("{0} header(s) are missing", missingheader);
                                else
                                    villageImpModel.Message = string.Format("{0} header(s) is missing", missingheader);
                            }
                            return Json(new { status = "success", htmlData = ConvertViewToString("_VillageImportGridView", villageImpModel, true) });
                        }
                    }
                    else
                    {
                        villageImpModel.NoOfErrors = 1;
                        villageImpModel.Message = "Invalid file format.";
                        return Json(new { status = "success", htmlData = ConvertViewToString("_VillageImportGridView", villageImpModel, true) });
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
            _logger.LogInformation("VillageImportController/SubmitFileData");
            try
            {
                var importDataList = HttpContext.Session.GetComplexData<List<VillageImport>>("VillageSave");
                var result = _villageServices.ImportVillageData(importDataList);
                if (result)
                {
                    HttpContext.Session.Remove("VillageData");
                    HttpContext.Session.Remove("VillageSave");
                    HttpContext.Session.Remove("error");
                    HttpContext.Session.Remove("msg");
                    HttpContext.Session.Remove("missingHeaders");
                }
                return Json(new { errorCode = 0, msg = "Village data has been imported sucessfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message + " ,StackTrace: " + ex.StackTrace + " ,DateTimeStamp :" + DateTime.Now);
                return Json(new { errorCode = -1, error = ex.Message });

            }

        }
    }
}
