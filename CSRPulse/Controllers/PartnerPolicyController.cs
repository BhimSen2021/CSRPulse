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
    [Route("[Controller]/[action]")]
    public class PartnerPolicyController : CSRPulse.Controllers.BaseController<PartnerPolicyController>
    {
        private readonly IPartnerPolicyService _partnerPolicyServices;

        public PartnerPolicyController(IPartnerPolicyService partnerPolicyServices)
        {
            _partnerPolicyServices = partnerPolicyServices;

        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("PartnerPolicyController/Index");
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
        public async Task<PartialViewResult> GetPartnerPolicyList(PartnerPolicy partnerPolicy)
        {
            _logger.LogInformation("PartnerPolicyController/GetPartnerPolicyList");
            try
            {
                var result = await _partnerPolicyServices.GetPartnerPolicyAsync(partnerPolicy);
                return PartialView("_PartnerPolicyList", result);
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
            return View(new PartnerPolicy());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(PartnerPolicy partnerPolicy)
        {
            try
            {
                _logger.LogInformation("PartnerPolicyController/Create");
                ModelState.Remove("IsActive");
                if (ModelState.IsValid)
                {

                    partnerPolicy.CreatedBy = userDetail == null ? 1 : userDetail.UserID;
                    partnerPolicy.IsActive = true;
                    partnerPolicy.IsFormallyApprovedByBoard = true;
                    partnerPolicy.IsImplementedSince = true;
                    partnerPolicy.IsLastUpdatedOn = true;
                    if (await _partnerPolicyServices.RecordExist(partnerPolicy))
                    {
                        ModelState.AddModelError("", "Policy already exists");
                    }
                    else
                    {
                        var result = await _partnerPolicyServices.CreatePartnerPolicy(partnerPolicy);
                        if (partnerPolicy.RecordExist)
                        {
                            ModelState.AddModelError("", "Policy already exists");
                        }
                        if (result)
                        {
                            TempData["Message"] = "Policy Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(partnerPolicy);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }


        public async Task<IActionResult> Edit(int policyId)
        {
            try
            {
                var uDetail = await _partnerPolicyServices.GetPartnerPolicyById(policyId);
                return View(uDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(PartnerPolicy partnerPolicy)
        {
            try
            {
                _logger.LogInformation("PartnerPolicyController/Edit");
                ModelState.Remove("IsActive");
                ModelState.Remove("IsFormallyApprovedByBoard");
                ModelState.Remove("IsImplementedSince");
                ModelState.Remove("IsLastUpdatedOn");

                if (ModelState.IsValid)
                {
                    partnerPolicy.UpdatedBy = userDetail == null ? 1 : userDetail.UserID;
                    partnerPolicy.UpdatedOn = DateTime.Now;
                    var result = await _partnerPolicyServices.UpdatePartnerPolicy(partnerPolicy);
                    if (partnerPolicy.RecordExist)
                    {
                        ModelState.AddModelError("", "Policy already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "Policy Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(partnerPolicy);
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
                _logger.LogInformation("PartnerPolicyController/ActiveDeActive");
                var result = _partnerPolicyServices.ActiveDeActive(id, isChecked);
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
                _logger.LogInformation("PartnerPolicyController/ActiveDeActiveIsFormallyApprovedByBoard");
                var result = _partnerPolicyServices.ActiveDeActiveIsFormallyApprovedByBoard(id, isChecked);
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
                _logger.LogInformation("PartnerPolicyController/ActiveDeActiveIsImplementedSince");
                var result = _partnerPolicyServices.ActiveDeActiveIsImplementedSince(id, isChecked);
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
                _logger.LogInformation("PartnerPolicyController/ActiveDeActiveIsLastUpdatedOn");
                var result = _partnerPolicyServices.ActiveDeActiveIsLastUpdatedOn(id, isChecked);
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
