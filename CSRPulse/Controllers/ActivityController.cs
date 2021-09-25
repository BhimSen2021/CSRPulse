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
    public class ActivityController : BaseController<ActivityController>
    {
        private readonly IActivityService _activityService;
        private readonly IDropdownBindService _ddlService;
        public ActivityController(IActivityService activityService, IDropdownBindService dropdownBindService)
        {
            _activityService = activityService;
            _ddlService = dropdownBindService;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("ActivityController/Index");
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
        public async Task<PartialViewResult> GetActivityList(Activity activity)
        {
            _logger.LogInformation("ActivityController/GetActivityList");
            try
            {
                var result = await _activityService.GetActivityAsync(activity);
                return PartialView("_ActivityList", result);
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
            return View(new Activity());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Activity activity)
        {
            try
            {
                _logger.LogInformation("ActivityController/Create");
                if (ModelState.IsValid)
                {
                    activity.CreatedBy = userDetail.UserID;
                    activity.CreatedRid = userDetail.RoleId;
                    activity.CreatedRname = userDetail.RoleName;

                    var result = await _activityService.CreateActivityAsync(activity);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "Activity name already exists.");
                    }
                    if (result.ThemeId > 0)
                    {
                        TempData["Message"] = "Activity created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                BindDropdowns();
                return View(activity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int activityId)
        {
            try
            {
                BindDropdowns();
                var uDetail = await _activityService.GetActivityByIdAsync(activityId);
                return View(uDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Activity activity)
        {
            try
            {
                _logger.LogInformation("activityController/Edit");
                if (ModelState.IsValid)
                {
                    activity.UpdatedBy = userDetail.UserID;
                    activity.UpdatedOn = DateTime.Now;
                    activity.UpdatedRid = userDetail.RoleId;
                    activity.UpdatedRname = userDetail.RoleName;
                    var result = await _activityService.UpdateActivityAsync(activity);
                    if (activity.IsExist)
                    {
                        ModelState.AddModelError("", "Activity name already exists");
                    }
                    else if (result)
                    {
                        TempData["Message"] = "Activity Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Activity Updation Failed.";
                    }
                }
                return View(activity);
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

        [HttpPost]
        public JsonResult ActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("UoMController/ActiveDeActive");
            var result = _activityService.ActiveDeActive(id, isChecked);
            return Json(result);

        }
    }
}
