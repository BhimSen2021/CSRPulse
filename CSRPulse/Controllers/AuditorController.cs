using CSRPulse.Common;
using CSRPulse.IServices;
using CSRPulse.Model;
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
using System.Threading.Tasks;

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
                    auditor.CreatedBy = userDetail.CreatedBy;
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
                var pDocument = await _processDocument.GetProcessDocuments(4);
                var auditor = _auditorServices.GetAuditorById(auditorId);

                if (pDocument != null)
                {
                    if (auditor.AuditorDocument == null)
                        auditor.AuditorDocument = new List<AuditorDocument>();

                    
                    foreach (var d in pDocument)
                    {
                        if (auditor.AuditorDocument.Any(x => x.DocumentId == d.DocumentId))
                        {
                            auditor.AuditorDocument.Where(m => m.DocumentId == d.DocumentId).FirstOrDefault().DocumentName = d.DocumentName;
                        }
                        else
                        {
                            auditor.AuditorDocument.Add(new AuditorDocument
                            {
                                AuditorId = auditorId,
                                DocumentId = d.DocumentId,
                                DocumentName = d.DocumentName,
                                DocumentType = ExtensionMethods.GetUploadDocumentType(d.DocumentType),
                                DocumentMaxSize = d.DocumentMaxSize ?? 20,
                            });
                        }
                    }
                }
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
        public async Task<IActionResult> Edit(Auditor auditor)
        {
            try
            {
                _logger.LogInformation("AuditorController/Edit");
                if (ModelState.IsValid)
                {
                    auditor.UpdatedBy = userDetail.CreatedBy;
                    auditor.UpdatedOn = DateTime.Now;

                    // Upload Documents
                    if (auditor.AuditorDocument != null)
                    {
                        auditor.AuditorDocument = auditor.AuditorDocument.Where(x => x.DocumentFile != null || x.Sdname != null).ToList();

                        for (int i = 0; i < auditor.AuditorDocument.Count; i++)
                        {
                            auditor.AuditorDocument[i].CreatedBy = userDetail.CreatedBy;
                            auditor.AuditorDocument[i].CreatedOn = auditor.UpdatedOn ?? DateTime.Now;
                            auditor.AuditorDocument[i].AuditorId = auditor.AuditorId;

                            if (auditor.AuditorDocument[i].DocumentFile != null)
                            {
                                string folder = "images/Auditor/Document/";
                                auditor.AuditorDocument[i].Udname = auditor.AuditorDocument[i].DocumentFile.FileName;
                                auditor.AuditorDocument[i].Sdname = await UploadImage(folder, auditor.AuditorDocument[i].DocumentFile);
                            }
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

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, folderPath)))
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, folderPath));


            var fileName = DateTime.Now.ToString("ddMMyyhhssmmff") + "_" + file.FileName;
            folderPath += fileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return fileName;
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

    }
}
