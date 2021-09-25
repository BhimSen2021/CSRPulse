using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class AuditReviewModuleController : BaseController<AuditReviewModuleController>
    {
        private readonly IAuditReviewModuleServices _auditReviewModuleServices;

        public AuditReviewModuleController(IAuditReviewModuleServices auditReviewModuleServices)
        {
            _auditReviewModuleServices = auditReviewModuleServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("AuditReviewModuleController/Index");
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
        public async Task<PartialViewResult> GetModuleList(AuditReviewModule auditReviewModule)
        {
            _logger.LogInformation("AuditReviewModuleController/GetModuleList");
            try
            {
                var result = await _auditReviewModuleServices.GetModuleAsync(auditReviewModule);
                return PartialView("_ModuleList", result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var rCount = await _auditReviewModuleServices.Count();
            var model = new AuditReviewModule();
            model.Sequence = rCount + 1;

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(AuditReviewModule auditReviewModule)
        {
            try
            {
                _logger.LogInformation("AuditReviewModuleController/Create");

                if (ModelState.IsValid)
                {
                    auditReviewModule.CreatedBy = userDetail.UserID;
                    auditReviewModule.CreatedRid = userDetail.RoleId;
                    auditReviewModule.CreatedRname = userDetail.RoleName;

                    if (await _auditReviewModuleServices.RecordExistAsync(auditReviewModule))
                    {
                        ModelState.AddModelError("", "Audit Review Module already exists");
                    }
                    else
                    {
                        var result = await _auditReviewModuleServices.CreateModuleAsync(auditReviewModule);

                        if (result)
                        {
                            TempData["Message"] = "Audit Review Module Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                            TempData["Message"] = "Audit Review Module Creation Failed!";
                    }
                }
                return View(auditReviewModule);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int moduleId)
        {
            try
            {
                var module = await _auditReviewModuleServices.GetModuleByIdAsync(moduleId);
                return View(module);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(AuditReviewModule auditReviewModule)
        {
            try
            {
                _logger.LogInformation("AuditReviewModuleController/Edit");

                if (ModelState.IsValid)
                {
                    auditReviewModule.UpdatedBy = userDetail.UserID;
                    auditReviewModule.UpdatedRid = userDetail.RoleId;
                    auditReviewModule.UpdatedRname = userDetail.RoleName;
                    auditReviewModule.UpdatedOn = DateTime.Now;
                    var result = await _auditReviewModuleServices.UpdateModuleAsync(auditReviewModule);

                    if (result)
                    {
                        TempData["Message"] = "Audit Review Module Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        TempData["Error"] = "Audit Review Module Updation Failed!";
                }
                return View(auditReviewModule);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        public JsonResult ActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("AuditReviewModuleController/ActiveDeActive");
            var result = _auditReviewModuleServices.ActiveDeActive(id, isChecked);
            return Json(result);
        }

    }
}
