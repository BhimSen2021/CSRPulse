using CSRPulse.Common;
using CSRPulse.IServices;
using CSRPulse.Model;
using CSRPulse.Models;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSRPulse.Common.FileHelper;


namespace CSRPulse.Controllers
{
    public class AuditorController : BaseController<AuditorController>
    {
        private readonly IAuditorServices _auditorServices;
        private readonly IProcessDocumentService _processDocument;
        private readonly IDropdownBindService _ddlService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuditorController(IAuditorServices auditorServices, IProcessDocumentService processDocument, IWebHostEnvironment webHostEnvironment, IDropdownBindService ddlService)
        {
            _auditorServices = auditorServices;
            _processDocument = processDocument;
            _webHostEnvironment = webHostEnvironment;
            _ddlService = ddlService;
        }
        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            _logger.LogInformation("AuditorController/Index");
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public async Task<PartialViewResult> GetAuditorList(Auditor auditor)
        {
            _logger.LogInformation("AuditorController/GetAuditorList");
            try
            {
                var result = await _auditorServices.GetAuditorAsync(auditor);
                return PartialView("_AuditorList", result);
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
            return View(new Auditor());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Auditor auditor)
        {
            try
            {
                _logger.LogInformation("AuditorController/Create");
                if (ModelState.IsValid)
                {
                    auditor.CreatedBy = userDetail.UserID;
                    auditor.CreatedRid = userDetail.RoleId;
                    auditor.CreatedRname = userDetail.RoleName;
                    var result = await _auditorServices.CreateAuditorAsync(auditor);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "Financial Audit/Impact Assessent already exists.");
                    }
                    if (result.AuditorId > 0)
                    {
                        TempData["Message"] = "Financial Audit/Impact Assessent created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                BindDropdowns();
                return View(auditor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int auditorId)
        {
            try
            {
                BindDropdowns();
                var pDocument = await _processDocument.GetProcessDocuments((int)Common.ProcessDocument.AuditAgencyOnboardingDocument);
                var auditor = _auditorServices.GetAuditorById(auditorId);

                //if (pDocument != null)
                //{
                //    if (auditor.AuditorDocument == null)
                //        auditor.AuditorDocument = new List<AuditorDocument>();


                //    //foreach (var d in pDocument)
                //    //{
                //    //    if (auditor.AuditorDocument.Any(x => x.DocumentId == d.DocumentId))
                //    //    {
                //    //        auditor.AuditorDocument.Where(m => m.DocumentId == d.DocumentId).FirstOrDefault().DocumentName = d.DocumentName;
                //    //        auditor.AuditorDocument.Where(m => m.DocumentId == d.DocumentId).FirstOrDefault().Mandatory = d.Mandatory;
                //    //        auditor.AuditorDocument.Where(m => m.DocumentId == d.DocumentId).FirstOrDefault().DocumentType = d.DocumentType;
                //    //        auditor.AuditorDocument.Where(m => m.DocumentId == d.DocumentId).FirstOrDefault().DocumentMaxSize = d.DocumentMaxSize ?? 20;
                //    //    }
                //    //    else
                //    //    {
                //    //        auditor.AuditorDocument.Add(new AuditorDocument
                //    //        {
                //    //            AuditorId = auditorId,
                //    //            DocumentId = d.DocumentId,
                //    //            DocumentName = d.DocumentName,
                //    //            DocumentType = ExtensionMethods.GetUploadDocumentType(d.DocumentType),
                //    //            DocumentMaxSize = d.DocumentMaxSize ?? 20,
                //    //            Mandatory = d.Mandatory
                //    //        });
                //    //    }
                //    //}
                //}
                return View(auditor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Auditor auditor,string ButtonType)
        {
            try
            {
                _logger.LogInformation("AuditorController/Edit");
                if (ModelState.IsValid)
                {
                    auditor.UpdatedBy = userDetail.CreatedBy;
                    auditor.UpdatedOn = DateTime.Now;
                    string fileName = string.Empty;
                    StringBuilder stringBuilder = new StringBuilder();
                    int flag = 0;
                    if (ButtonType == "SaveAuditorDocument")
                    {
                        if (auditor.AuditorDocument.Where(x => (x.ServerFileName == null && x.DocumentFile == null) && x.Mandatory == true).Any())
                            return Json(new { flag = 2, type = 1, msg = "select all mandatory documents", htmlData = ConvertViewToString("_AuditorDocument", auditor, true) });

                        var listAD = auditor.AuditorDocument.Where(s => s.ServerFileName != null || s.DocumentFile != null).ToList();

                        #region Check Mime Type

                        for (int i = 0; i < auditor.AuditorDocument.Count; i++)
                        {
                            if (auditor.AuditorDocument[i].DocumentFile != null)
                            {
                                if (!ValidateFileMimeType(auditor.AuditorDocument[i].DocumentFile))
                                {
                                    stringBuilder.Append($"# {i + 1} The file format of the {auditor.AuditorDocument[i].DocumentFile.FileName} file is incorrect");
                                    stringBuilder.Append("<br>");
                                    flag = 3;
                                }
                            }
                        }

                        if (stringBuilder.ToString() != "")
                        {
                            return Json(new { flag = 3, msg = stringBuilder.ToString(), htmlData = ConvertViewToString("_AuditorDocument", auditor, true) });
                        }
                        #endregion

                        //RevoveModelState
                        if (TryValidateModel(auditor.AuditorDocument))
                        {
                            var CreatedOn = DateTime.Now;
                            string SDocumentName = string.Empty;
                            for (int i = 0; i < listAD.Count; i++)
                            {
                                listAD[i].AuditorId = auditor.AuditorId;
                                listAD[i].AuditorDocumentId = 0;
                                listAD[i].CreatedOn = DateTime.UtcNow;
                                listAD[i].CreatedBy = userDetail.UserID;
                                listAD[i].CreatedRid = userDetail.RoleId;
                                listAD[i].CreatedRname = userDetail.RoleName;


                                if (listAD[i].DocumentFile != null)
                                {
                                    var filePath = DocumentUploadFilePath.AuditorFilePath;
                                    listAD[i].UploadFileName = auditor.AuditorDocument[i].DocumentFile.FileName;
                                    listAD[i].DocumentId = auditor.AuditorDocument[i].DocumentId;

                                    SDocumentName = await UploadAuditorDocument(filePath, auditor.AuditorDocument[i].DocumentFile);

                                    if (SDocumentName == "FormulaFound")
                                    {
                                        stringBuilder.Append($"# {i + 1} {auditor.AuditorDocument[i].DocumentFile.FileName} contains formula or arithmetic operators that is not allowed in system");
                                        stringBuilder.Append("<br>");
                                        flag = 4;
                                    }
                                    else
                                    {
                                        listAD[i].ServerFileName = SDocumentName;
                                    }
                                }
                            }
                            if (stringBuilder.ToString() != "")
                            {
                                return Json(new { flag = flag, msg = stringBuilder.ToString(), htmlData = ConvertViewToString("_AuditorDocument", auditor, true) });

                            }

                            auditor.AuditorDocument = listAD;
                            var Details = await _auditorServices.GetUpdateAuditorDocument(auditor);
                            var documents = await _auditorServices.GetAuditorDocumentList(auditor.AuditorId, (int)Common.ProcessDocument.AuditAgencyOnboardingDocument);
                            auditor.AuditorDocument = documents;

                            flag = 1;
                            return Json(new { flag = flag, msg = "Document Upload Succesfully", htmlData = ConvertViewToString("_AuditorDocument", auditor, true) });

                        }
                    }

                    var result = await _auditorServices.UpdateAuditorAsync(auditor);
                    if (auditor.IsExist)
                    {
                        ModelState.AddModelError("", "Financial Audit/Impact Assessent already exists");
                    }
                    else if (result)
                    {
                        TempData["Message"] = "Financial Audit/Impact Assessent Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Financial Audit/Impact Assessent Updation Failed.";
                    }
                }
                BindDropdowns();
                return View(auditor);
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

        private async Task<string> UploadDocument(string folderPath, IFormFile file)
        {
            if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, folderPath)))
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, folderPath));

            var uploadedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            return await UploadFile(uploadedFilePath, file);
        }

        [HttpPost]
        public JsonResult ActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("AuditorController/ActiveDeActive");
            var result = _auditorServices.ActiveDeActive(id, isChecked);
            return Json(result);

        }

        public async Task<IActionResult> DownloadDocument(string fileName)
        {
            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\Auditor\Document\" + fileName);
            if (!System.IO.File.Exists(filepath))
                return Content($"file not found.");

            return await DownloadFile(filepath);
        }

        [HttpGet]
        public async Task<IActionResult> AddDocument(int auditorId)
        {
            try
            {
                DocumentModel documentModel = new DocumentModel();
                documentModel.documents = new List<Document>();
                documentModel.Id = auditorId;
                var documents = await _auditorServices.GetAuditorDocument((int)Common.ProcessDocument.AuditAgencyOnboardingDocument);
                documentModel.documents = documents;

                return Json(new { htmlData = ConvertViewToString("_AddDocument", documentModel, true) });
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDocument(DocumentModel model)
        {
            try
            {
                int flag = 0;
                string msg = "Documents added successfully";
                Auditor auditor = new Auditor();
                var documents = model.documents.Where(x => x.AssigneDocument == true).ToList();

                foreach (var item in documents)
                {
                    var auditorDocument = new AuditorDocument();
                    auditorDocument.AuditorId = model.Id;
                    auditorDocument.DocumentId = item.DocumentId;
                    auditorDocument.DocumentName = item.DocumentName;
                    auditorDocument.DocumentMaxSize = item.DocumentMaxSize;
                    auditorDocument.DocumentType = item.DocumentType;
                    auditorDocument.Mandatory = item.Mandatory;
                    auditorDocument.Remark = item.Remark;
                    auditorDocument.CreatedBy = userDetail.UserID;
                    auditorDocument.CreatedRid = userDetail.RoleId;
                    auditorDocument.CreatedRname = userDetail.RoleName;

                    flag = await _auditorServices.AddDocument(auditorDocument);
                    if (flag == 2)
                        msg = "Some documents will not added due to already exits in the auditor documents.";
                }


                auditor.AuditorId = model.Id;
                auditor.AuditorDocument = new List<AuditorDocument>();
                auditor.AuditorDocument = await _auditorServices.GetAuditorDocumentList(auditor.AuditorId, (int)Common.ProcessDocument.AuditAgencyOnboardingDocument);

                return Json(new { flag = flag, msg = msg, htmlData = ConvertViewToString("_AuditorDocument", auditor, true) });
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        public IActionResult DownloadAuditorDocument(string fileName)
        {
            var filePath = DocumentUploadFilePath.AuditorFilePath;
            var sPhysicalPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath + fileName);
            if (!System.IO.File.Exists(sPhysicalPath))
                return Content($"file not found.");
            return DownloadAnyFile(fileName, sPhysicalPath, null);
        }
        private async Task<string> UploadAuditorDocument(string filePath, IFormFile file)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                var auditor = new Auditor();
                string fileName = string.Empty;
                if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, filePath)))
                    Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, filePath));
                var uploadedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
                var fileupload = await UploadFile(uploadedFilePath, file);
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                {
                    fileName = Path.GetExtension(file.FileName);
                    string sPhysicalPath = Path.Combine(uploadedFilePath, fileName);
                    #region C H E C K  F O R M U L A S
                    var isFormulas = CheckFormulaInExcelFile(sPhysicalPath);
                    if (isFormulas)
                    {
                        DeleteFile(sPhysicalPath);
                        fileupload = "FormulaFound";
                    }
                    #endregion
                }
                return fileupload;
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        public async Task<PartialViewResult> RemoveAuditorDocument(int adId, int aId, string fName)
        {
            try
            {
                var auditor = new Auditor();
                auditor.AuditorId = aId;
                var isDeleted = _auditorServices.DeleteAuditorDocument(adId);
                if (isDeleted)
                {
                    var filePath = DocumentUploadFilePath.AuditorFilePath;
                    var sPhysicalPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath + fName);
                    if (System.IO.File.Exists(sPhysicalPath))
                    {
                        FileInfo myfile = new FileInfo(sPhysicalPath);
                        myfile.Delete();
                    }
                }
                auditor.AuditorDocument = await _auditorServices.GetAuditorDocumentList(auditor.AuditorId, (int)Common.ProcessDocument.AuditAgencyOnboardingDocument);
                return PartialView("_AuditorDocument", auditor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }

    }
}
