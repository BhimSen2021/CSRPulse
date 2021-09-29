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
    [AutoValidateAntiforgeryToken]
    public class PartnerPolicyModuleController : CSRPulse.Controllers.BaseController<PartnerPolicyModuleController>
    {
        private readonly IPartnerPolicyModuleService _partnerPolicyModuleServices;
        private readonly IPartnerPolicyService _partnerPolicyServices;
        private readonly IDropdownBindService _ddlService;

        public PartnerPolicyModuleController(IPartnerPolicyModuleService partnerPolicyModuleServices, IDropdownBindService ddlService, IPartnerPolicyService partnerPolicyServices)
        {
            _partnerPolicyModuleServices = partnerPolicyModuleServices;
            _ddlService = ddlService;
            _partnerPolicyServices = partnerPolicyServices;
        }

        [NonAction]
        void BindDropdowns()
        {
            var partnerPolicyList = _ddlService.GetPartnerPolicy();
            ViewBag.ddlPartnerPolicy = new SelectList(partnerPolicyList, "id", "value");
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("PartnerPolicyModuleController/Index");
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
        public async Task<PartialViewResult> GetPartnerPolicyModuleList(PartnerPolicyModule PartnerPolicyModule)
        {
            _logger.LogInformation("PartnerPolicyModuleController/GetPartnerPolicyModuleList");
            try
            {
               
                var result = await _partnerPolicyModuleServices.GetPartnerPolicyModuleAsync(PartnerPolicyModule);
                return PartialView("_PartnerPolicyModuleList", result);
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
            return View(new PartnerPolicyModule());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(PartnerPolicyModule PartnerPolicyModule)
        {
            try
            {
                _logger.LogInformation("PartnerPolicyModuleController/Create");

                if (ModelState.IsValid)
                {
                    PartnerPolicyModule.CreatedBy = userDetail.CreatedBy;
                    PartnerPolicyModule.CreatedRid = userDetail.RoleId;
                    PartnerPolicyModule.CreatedRname = userDetail.RoleName;
                    PartnerPolicyModule.IsActive = true;                    
                    var result = await _partnerPolicyModuleServices.CreatePartnerPolicyModuleAsync(PartnerPolicyModule);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "Policy Module name already exists.");
                    }
                    if (result.PolicyId > 0)
                    {
                        TempData["Message"] = "Policy Module created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                BindDropdowns();
                return View(PartnerPolicyModule);

            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }


        public async Task<IActionResult> Edit(int policyModuleId)
        {
            try
            {
                BindDropdowns();
                var uDetail = await _partnerPolicyModuleServices.GetPartnerPolicyModuleByIdAsync(policyModuleId);
                return View(uDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(PartnerPolicyModule PartnerPolicyModule)
        {
            try
            {
                _logger.LogInformation("PartnerPolicyModuleController/Edit");
               
                if (ModelState.IsValid)
                {
                    PartnerPolicyModule.UpdatedBy = userDetail.CreatedBy;
                    PartnerPolicyModule.UpdatedRid = userDetail.RoleId;
                    PartnerPolicyModule.UpdatedRname = userDetail.RoleName;
                    PartnerPolicyModule.UpdatedOn = DateTime.Now;

                    var result = await _partnerPolicyModuleServices.UpdatePartnerPolicyModuleAsync(PartnerPolicyModule);
                    if (PartnerPolicyModule.IsExist)
                    {
                        ModelState.AddModelError("", "Policy Module name already exists");
                    }
                    else if (result)
                    {
                        TempData["Message"] = "Policy Module Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Policy Module Updation Failed.";
                    }
                }
                return View(PartnerPolicyModule);
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
            try
            {
                _logger.LogInformation("PartnerPolicyModuleController/ActiveDeActive");
                var result = _partnerPolicyModuleServices.ActiveDeActive(id, isChecked);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        public JsonResult ActiveDeActiveIsFormallyApprovedByBoard(int id, bool isChecked)
        {
            try
            {
                _logger.LogInformation("PartnerPolicyModuleController/ActiveDeActiveIsFormallyApprovedByBoard");
                var result = _partnerPolicyModuleServices.ActiveDeActiveIsFormallyApprovedByBoard(id, isChecked);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        public JsonResult ActiveDeActiveIsImplementedSince(int id, bool isChecked)
        {
            try
            {
                _logger.LogInformation("PartnerPolicyModuleController/ActiveDeActiveIsImplementedSince");
                var result = _partnerPolicyModuleServices.ActiveDeActiveIsImplementedSince(id, isChecked);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        public JsonResult ActiveDeActiveIsLastUpdatedOn(int id, bool isChecked)
        {
            try
            {
                _logger.LogInformation("PartnerPolicyModuleController/ActiveDeActiveIsLastUpdatedOn");
                var result = _partnerPolicyModuleServices.ActiveDeActiveIsLastUpdatedOn(id, isChecked);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

    }
}
