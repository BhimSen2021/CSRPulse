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

namespace CSRPulse.Controllers
{
    public class AuditReviewParamterController : BaseController<AuditReviewParamterController>
    {
        private readonly IAuditReviewParamterServices _reviewParamterServices;
        private readonly IDropdownBindService _ddlService;
        public AuditReviewParamterController(IAuditReviewParamterServices reviewParamterServices, IDropdownBindService ddlService)
        {
            _reviewParamterServices = reviewParamterServices;
            _ddlService = ddlService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("AuditReviewParamterController/Index");
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
        public async Task<PartialViewResult> GetParamterList(AuditReviewParamter paramter)
        {
            _logger.LogInformation("AuditReviewParamterController/GetParamterList");
            try
            {
                var result = await _reviewParamterServices.GetParamterAsync(paramter);
                return PartialView("_ParamterList", result);
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
            BindDropdowns();
            var rCount = await _reviewParamterServices.Count();
            var paramter = new AuditReviewParamter();
            paramter.ReferenceNo = rCount + 1;

            return View(paramter);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(AuditReviewParamter paramter)
        {
            try
            {
                _logger.LogInformation("AuditReviewParamterController/Create");

                if (ModelState.IsValid)
                {
                    paramter.CreatedBy = userDetail.UserID;
                    paramter.CreatedRid = userDetail.RoleId;
                    paramter.CreatedRname = userDetail.RoleName;

                    if (await _reviewParamterServices.RecordExistAsync(paramter))
                    {
                        ModelState.AddModelError("", "Audit Review Paramter already exists");
                    }
                    else
                    {
                        var result = await _reviewParamterServices.CreateParamterAsync(paramter);

                        if (result)
                        {
                            TempData["Message"] = "Audit Review Paramter Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                            TempData["Message"] = "Audit Review Paramter Creation Failed!";
                    }
                }
                return View(paramter);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int paramterId)
        {
            try
            {
                BindDropdowns();
                var paramter = await _reviewParamterServices.GetParamterByIdAsync(paramterId);
                return View(paramter);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(AuditReviewParamter paramter)
        {
            try
            {
                _logger.LogInformation("AuditReviewParamterController/Edit");

                if (ModelState.IsValid)
                {
                    paramter.UpdatedBy = userDetail == null ? 1 : userDetail.UserID;
                    paramter.UpdatedOn = DateTime.Now;
                    paramter.UpdatedRid = userDetail.RoleId;
                    paramter.UpdatedRname = userDetail.RoleName;
                    var result = await _reviewParamterServices.UpdateParamterAsync(paramter);

                    if (result)
                    {
                        TempData["Message"] = "Audit Review Paramter Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        TempData["Error"] = "Audit Review Paramter Updation Failed!";
                }
                return View(paramter);
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
            _logger.LogInformation("AuditReviewParamterController/ActiveDeActive");
            var result = _reviewParamterServices.ActiveDeActive(id, isChecked);
            return Json(result);
        }

        [NonAction]
        void BindDropdowns()
        {
            var listModels = _ddlService.GetModule(null);
            ViewBag.ddlModule = new SelectList(listModels, "id", "value");
        }
    }
}
