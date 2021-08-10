using CSRPulse.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    [Route("[Controller]/[action]")]
    public class ProcessDocumentController : BaseController<ProcessDocumentController>
    {
        private readonly IProcessDocumentService _documentService;

        public ProcessDocumentController(IProcessDocumentService documentService)
        {
            _documentService = documentService;
        }
        public IActionResult Index()
        {
            var processList = _documentService.GetProcess();
            ViewBag.ddlProcess = new SelectList(processList, "id", "value");
            return View();
        }

        public async Task<IActionResult> Search(Model.ProcessDocumentFilter filter)
        {
            _logger.LogInformation("ProcessDocumentController/Search");
            try
            {
                if (ModelState.IsValid)
                {
                    var getDocuments = await _documentService.GetProcessDocuments(filter.ProcessId);
                    return PartialView("_ProcessDocument", getDocuments);
                }
                else
                {
                    return new EmptyResult();
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                throw;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {

            _logger.LogInformation("ProcessDocumentController/Search");
            try
            {
                BindDropDown();
                Model.ProcessDocument processDocument = new Model.ProcessDocument();
                processDocument.DocumentTypes = new List<Model.DocumentType>();
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 1,
                    DocumentTypeName = "Word"
                });
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 2,
                    DocumentTypeName = "Excel"
                });
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 3,
                    DocumentTypeName = "PDF"
                });
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 4,
                    DocumentTypeName = "Image(jpeg,png)"
                });
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 5,
                    DocumentTypeName = "Text"
                });
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 6,
                    DocumentTypeName = "Vedio(MP4)"
                });
                return View(processDocument);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                throw;
            }
        }
        [NonAction]
        private void BindDropDown()
        {
            var processList = _documentService.GetProcess();
            ViewBag.ddlProcess = new SelectList(processList, "id", "value");
            var documentList = _documentService.GetParentDocument();
            ViewBag.ddlParentDocument = new SelectList(documentList, "id", "value");
        }
        [HttpPost]
        public IActionResult Create(Model.ProcessDocument processDocument)
        {
            _logger.LogInformation("ProcessDocumentController/Create");
            try
            {

                var docList = processDocument.DocumentTypes.Where(x => x.DocumentCheck == true).ToList();
                if (docList == null || docList.Count == 0)
                    ModelState.AddModelError("DocumentType", "Please select Document Type");

                if (ModelState.IsValid)
                {
                    var docType = string.Empty;
                    foreach (var item in docList)
                    {
                        docType = docType + item.DocumentTypeId + ',';
                    }
                    docType = docType.TrimEnd(',');
                    processDocument.DocumentType = docType;
                    processDocument.CreatedBy = userDetail.UserID;
                    var res = _documentService.CreateProcessDocument(processDocument);
                    TempData["Message"] = "Process Document Added Successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    processDocument.DocumentTypes = new List<Model.DocumentType>();
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 1,
                        DocumentTypeName = "Word"
                    });
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 2,
                        DocumentTypeName = "Excel"
                    });
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 3,
                        DocumentTypeName = "PDF"
                    });
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 4,
                        DocumentTypeName = "Image(jpeg,png)"
                    });
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 5,
                        DocumentTypeName = "Text"
                    });
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 6,
                        DocumentTypeName = "Vedio(MP4)"
                    });
                    BindDropDown();
                    return View(processDocument);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int dId)
        {

            _logger.LogInformation($"ProcessDocumentController/Edit/dId={dId}");
            try
            {
                BindDropDown();
                var getProcessDocument = await _documentService.GetProcessDocumentById(dId);
                Model.ProcessDocument processDocument = new Model.ProcessDocument();
                processDocument = getProcessDocument;

                processDocument.DocumentTypes = new List<Model.DocumentType>();
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 1,
                    DocumentTypeName = "Word"
                });
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 2,
                    DocumentTypeName = "Excel"
                });
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 3,
                    DocumentTypeName = "PDF"
                });
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 4,
                    DocumentTypeName = "Image(jpeg,png)"
                });
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 5,
                    DocumentTypeName = "Text"
                });
                processDocument.DocumentTypes.Add(new Model.DocumentType
                {
                    DocumentTypeId = 6,
                    DocumentTypeName = "Vedio(MP4)"
                });

                if (getProcessDocument != null)
                {
                    var splitDocType = Array.ConvertAll(getProcessDocument.DocumentType.Split(','), int.Parse).ToList();
                    for (int i = 0; i < processDocument.DocumentTypes.Count; i++)
                    {
                        if (splitDocType.Any(x => x == processDocument.DocumentTypes[i].DocumentTypeId))
                        {
                            processDocument.DocumentTypes[i].DocumentCheck = true;
                        }
                    }
                }
                return View(processDocument);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(Model.ProcessDocument processDocument)
        {
            _logger.LogInformation("ProcessDocumentController/Edit");
            try
            {

                var docList = processDocument.DocumentTypes.Where(x => x.DocumentCheck == true).ToList();
                if (docList == null || docList.Count == 0)
                    ModelState.AddModelError("DocumentType", "Please select Document Type");

                if (ModelState.IsValid)
                {
                    var docType = string.Empty;
                    foreach (var item in docList)
                    {
                        docType = docType + item.DocumentTypeId + ',';
                    }
                    docType = docType.TrimEnd(',');
                    processDocument.DocumentType = docType;
                    processDocument.UpdatedBy = userDetail.UserID;
                    processDocument.UpdatedOn = DateTime.Now;
                    var res = _documentService.UpdateProcessDocument(processDocument);
                    TempData["Message"] = "Process Document Updated Successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    processDocument.DocumentTypes = new List<Model.DocumentType>();
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 1,
                        DocumentTypeName = "Word"
                    });
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 2,
                        DocumentTypeName = "Excel"
                    });
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 3,
                        DocumentTypeName = "PDF"
                    });
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 4,
                        DocumentTypeName = "Image(jpeg,png)"
                    });
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 5,
                        DocumentTypeName = "Text"
                    });
                    processDocument.DocumentTypes.Add(new Model.DocumentType
                    {
                        DocumentTypeId = 6,
                        DocumentTypeName = "Vedio(MP4)"
                    });
                    BindDropDown();
                    return View(processDocument);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                throw;
            }
        }

    }
}
