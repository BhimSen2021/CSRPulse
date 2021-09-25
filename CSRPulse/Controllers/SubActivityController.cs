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
    public class SubActivityController : BaseController<SubActivityController>
    {
        private readonly ISubActivityService _subActivityService;
        private readonly IDropdownBindService _ddlService;
        public SubActivityController(ISubActivityService subActivityService, IDropdownBindService dropdownBindService)
        {
            _subActivityService = subActivityService;
            _ddlService = dropdownBindService;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("SubActivityController/Index");
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
        public async Task<PartialViewResult> GetSubActivityList(SubActivity subActivity)
        {
            _logger.LogInformation("SubActivityController/GetSubActivityList");
            try
            {
                var result = await _subActivityService.GetSubActivityAsync(subActivity);
                return PartialView("_SubActivityList", result);
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
            return View(new SubActivity());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SubActivity subActivity)
        {
            try
            {
                _logger.LogInformation("SubActivityController/Create");
                if (ModelState.IsValid)
                {
                    subActivity.CreatedBy = userDetail.UserID;
                    subActivity.CreatedRid = userDetail.RoleId;
                    subActivity.CreatedRname = userDetail.RoleName;
                    subActivity.CreatedOn = DateTime.Now;
                    var result = await _subActivityService.CreateSubActivityAsync(subActivity);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "SubActivity name already exists.");
                    }
                    if (result.ThemeId > 0)
                    {
                        TempData["Message"] = "SubActivity created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                BindDropdowns();
                return View(subActivity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public IActionResult Edit(int subActivityId)
        {
            try
            {              
                var aDetail = _subActivityService.GetSubActivityId(subActivityId);
                return View(aDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubActivity subActivity)
        {
            try
            {
                _logger.LogInformation("SubActivityController/Edit");
                if (ModelState.IsValid)
                {
                    subActivity.UpdatedBy = userDetail.UserID;
                    subActivity.UpdatedRid = userDetail.RoleId;
                    subActivity.UpdatedRname = userDetail.RoleName;
                    subActivity.UpdatedOn = DateTime.Now;
                    var result = await _subActivityService.UpdateSubActivityAsync(subActivity);
                    if (subActivity.IsExist)
                    {
                        ModelState.AddModelError("", "SubActivity name already exists");
                    }
                    else if (result)
                    {
                        TempData["Message"] = "SubActivity Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "SubActivity Updation Failed.";
                    }
                }
                return View(subActivity);
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
            var themeList = _ddlService.GetTheme(null);
            ViewBag.ddlTheme = new SelectList(themeList, "id", "value");
        }

        [HttpGet]
        public JsonResult BindActivityDropdown(int themeId)
        {
            _logger.LogInformation("SubActivityController/ActiveDeActive");
            var selectListModels = _ddlService.GetActivity(themeId, null).ToList();
            return Json(new SelectList(selectListModels, "id", "value"));

        }

        [HttpPost]
        public JsonResult ActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("SubActivityController/ActiveDeActive");
            var result = _subActivityService.ActiveDeActive(id, isChecked);
            return Json(result);

        }
    }
}
