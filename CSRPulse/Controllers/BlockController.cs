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
using static CSRPulse.Common.FileHelper;
using CSRPulse.Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using CSRPulse.Models;

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
            var sPhysicalPath = Path.Combine(_webHostEnvironment.WebRootPath, 
                DocumentUploadFilePath.TemplatesLocationFilePath + "BlockTemplate.xlsx");

            if (!System.IO.File.Exists(sPhysicalPath))
                return Content($"Template not found.");

            return await DownloadFile(sPhysicalPath);
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

            var uploadedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, DocumentUploadFilePath.TempFilePath);
            if (!Directory.Exists(uploadedFilePath))
                Directory.CreateDirectory(uploadedFilePath);

            var sPhysicalPath = Path.Combine(uploadedFilePath, ExtensionMethods.SetUniqueFileName(".xlsx"));

            if (_export.ExportToExcel(ds, sPhysicalPath, "RefrenceCode_Block"))
            {
                if (!System.IO.File.Exists(sPhysicalPath))
                    return Content($"Tempfile path not found.");

                var res = await DownloadFile(sPhysicalPath);

                Common.ExtensionMethods.DeleteFile(sPhysicalPath);
                return res;
            }
            else
            {
                return Content($"Block refrence code file downloding filed.");
            }
        }

        public ViewResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _ImportBlockData(IFormFile file)
        {
            _logger.LogInformation("BlockController/_ImportBlockData");
            try
            {
                string fileName = string.Empty; string filePath = string.Empty;
                int error = 0, warning = 0, duplicateEntries = 0;
                List<BlockImport> importBlockSave = new List<BlockImport>();
                List<string> columnName = new List<string>();
                List<string> missingHeaders;
                string msg = string.Empty;
                BlockImportModel blockImpModel = new BlockImportModel();
                List<BlockImport> BlockForm = new List<BlockImport>();
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
                            blockImpModel.NoOfErrors = 1;
                            blockImpModel.Message = "File contains formula or arithmetic operators that is not allowed in system.";
                            return Json(new { status = "success", htmlData = ConvertViewToString("_BlockImportGridView", blockImpModel, true) });
                        }
                        #endregion

                        #endregion
                        var objBlock = _blockServices.ReadBlockExcelData(sPhysicalPath, false, out msg, out error, out warning, out importBlockSave, out missingHeaders, out columnName);
                        if (error > 0 && msg == "Rows")
                        {
                            //TempData["error"] = -3;
                            blockImpModel.NoOfErrors = 1;
                            blockImpModel.Message = "No record found, Please check the sheet and reupload.";
                            return Json(new { status = "noRecordFound", htmlData = ConvertViewToString("_BlockImportGridView", blockImpModel, true) });
                        }
                        else if (error > 0 && msg == "MAXROW")
                        {
                            blockImpModel.NoOfErrors = 1;
                            blockImpModel.Message = "Found more than 10,000 records, You can not validate more than 10,000 blocks a single sheet.";
                            return Json(new { status = "noRecordFound", htmlData = ConvertViewToString("_BlockImportGridView", blockImpModel, true) });
                        }
                        else
                        {
                            duplicateEntries = objBlock.GroupBy(x => new { x.BlockCode }).Sum(g => g.Count() - 1);

                            var getDuplicateRows = ExtensionMethods.FindDuplicates(objBlock.ToList(), x => new { x.BlockCode });

                            objBlock.Join(getDuplicateRows, (x) => new { x.BlockCode }, (y) => new { y.BlockCode }, (x, y) =>
                            {
                                x.isDuplicatedRow = ((x.BlockCode == y.BlockCode) ? true : false);
                                return x;
                            }).ToList();

                            if (error == 0 && duplicateEntries > 0)
                                error = duplicateEntries;
                            else
                                error += duplicateEntries;

                            var json = JsonConvert.SerializeObject(objBlock);
                            DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                            if (msg != "Headers")
                            {
                                dataTable.Columns["StateId"].SetOrdinal(0);
                                dataTable.Columns["DistrictId"].SetOrdinal(1);
                                dataTable.Columns["BlockCode"].SetOrdinal(2);
                                dataTable.Columns["BlockName"].SetOrdinal(3);
                                dataTable.Columns["State"].SetOrdinal(4);
                                dataTable.Columns["District"].SetOrdinal(5);
                                dataTable.Columns["error"].SetOrdinal(6);
                                dataTable.Columns["warning"].SetOrdinal(7);
                                dataTable.Columns["isDuplicatedRow"].SetOrdinal(8);
                            }

                            blockImpModel.ErrorMsgCollection = GetErrorMessage();
                            if (dataTable != null)
                            {
                                blockImpModel.BlockInput = dataTable;
                            }
                            else
                                blockImpModel.BlockData = BlockForm;

                            var errors = error;
                            var msgs = msg;

                            blockImpModel.NoOfErrors = errors != 0 ? (int)errors : 0;
                            blockImpModel.Message = msgs != null ? Convert.ToString(msgs) : "";
                            if (blockImpModel.Message != "Headers")
                            {
                                HttpContext.Session.SetComplexData("BlockSave", importBlockSave);
                                if (blockImpModel.Message == "noRecord")
                                {
                                    blockImpModel.Message = "No record found, Please check the sheet and reupload.";
                                }
                                else
                                {
                                    if (blockImpModel.NoOfErrors > 0)
                                        blockImpModel.Message = string.Format("{0} error(s) found ,Please check and correct the sheet data and re-validate the sheet.", blockImpModel.NoOfErrors.ToString());
                                    else
                                        blockImpModel.Message = "Sheet validated successfully, Click on Upload button to upload your data.";
                                }
                            }
                            else
                            {
                                var list = missingHeaders;
                                var missingheader = String.Join(",", list);
                                if (list.Count > 1)
                                    blockImpModel.Message = string.Format("{0} header(s) are missing", missingheader);
                                else
                                    blockImpModel.Message = string.Format("{0} header(s) is missing", missingheader);
                            }
                            return Json(new { status = "success", htmlData = ConvertViewToString("_BlockImportGridView", blockImpModel, true) });
                        }
                    }
                    else
                    {
                        blockImpModel.NoOfErrors = 1;
                        blockImpModel.Message = "Invalid file format.";
                        return Json(new { status = "success", htmlData = ConvertViewToString("_BlockImportGridView", blockImpModel, true) });
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
            _logger.LogInformation("BlockImportController/SubmitFileData");
            try
            {
                var importDataList = HttpContext.Session.GetComplexData<List<BlockImport>>("BlockSave");
                var result = _blockServices.ImportBlockData(importDataList);
                if (result)
                {
                    HttpContext.Session.Remove("BlockData");
                    HttpContext.Session.Remove("BlockSave");
                    HttpContext.Session.Remove("error");
                    HttpContext.Session.Remove("msg");
                    HttpContext.Session.Remove("missingHeaders");
                }
                return Json(new { errorCode = 0, msg = "Block data has been imported sucessfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message + " ,StackTrace: " + ex.StackTrace + " ,DateTimeStamp :" + DateTime.Now);
                return Json(new { errorCode = -1, error = ex.Message });

            }

        }
    }
}
