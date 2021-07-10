using CSRPulse.Model;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class PartnerController : BaseController<PartnerController>
    {
        private readonly IPartnerService _partnerService;
        private readonly IDropdownBindService _ddlService;
        public PartnerController(IPartnerService partnerService, IDropdownBindService dropdownBindService)
        {
            _partnerService = partnerService;
            _ddlService = dropdownBindService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("PartnerController/Index");
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
        public async Task<PartialViewResult> GetPartnerList(Partner partner)
        {
            _logger.LogInformation("PartnerController/GetPartnerList");
            try
            {
                var result = await _partnerService.GetPartnerAsync(partner);
                return PartialView("_PartnerList", result);
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
            return View(new Partner());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Partner partner)
        {
            try
            {
                _logger.LogInformation("PartnerController/Create");
                if (ModelState.IsValid)
                {
                    if (await _partnerService.RecordExist(partner))
                    {
                        ModelState.AddModelError("", "Partner already exists");
                    }
                    else
                    {
                        partner.IsActive = true;
                        partner.CreatedBy = userDetail.UserID;
                        var result = await _partnerService.CreatePartner(partner);

                        if (result)
                        {
                            TempData["Message"] = "Partner Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                BindDropdowns();
                return View(partner);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int partnerId)
        {
            try
            {
                BindDropdowns();
                   var dDetail = await _partnerService.GetPartnerById(partnerId);
                return View(dDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Partner partner)
        {
            try
            {
                _logger.LogInformation("PartnerController/Edit");
                if (ModelState.IsValid)
                {
                    var result = await _partnerService.UpdatePartner(partner);
                    if (result)
                    {
                        TempData["Message"] = "Partner Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Partner Updation Failed.";
                    }

                }
                return View(partner);
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
            _logger.LogInformation("Partner/ActiveDeActive");
            var result = _partnerService.ActiveDeActive(id, isChecked);
            return Json(result);
        }

        [NonAction]
        void BindDropdowns()
        {
            var stateList = _ddlService.GetStateAsync(null, null);
            ViewBag.ddlState = new SelectList(stateList, "id", "value");
        }


    }
}
