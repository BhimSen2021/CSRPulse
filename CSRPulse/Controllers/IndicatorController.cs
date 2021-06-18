using CSRPulse.Model;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace CSRPulse.Controllers
{
    public class IndicatorController : BaseController<IndicatorController>
    {
        private readonly IIndicatorService _indicatorService;
        private readonly IDropdownBindService _ddlService;
        public IndicatorController(IIndicatorService indicatorService, IDropdownBindService dropdownBindService)
        {
            _indicatorService = indicatorService;
            _ddlService = dropdownBindService;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("IndicatorController/Index");
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
        public async Task<PartialViewResult> GetIndicatorList(Indicator indicator)
        {
            _logger.LogInformation("IndicatorController/GetIndicatorList");
            try
            {
                var result = await _indicatorService.GetIndicatorAsync(indicator.ThemeId, indicator.ActivityId);
                return PartialView("_IndicatorList", result);
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
            return View(new Indicator());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Indicator indicator)
        {
            try
            {
                _logger.LogInformation("IndicatorController/Create");
                if (ModelState.IsValid)
                {
                    indicator.CreatedBy = userDetail.CreatedBy;
                    var result = await _indicatorService.CreateIndicatorAsync(indicator);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "Indicator name already exists.");
                    }
                    if (result.ThemeId > 0)
                    {
                        TempData["Message"] = "Indicator created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                BindDropdowns();
                return View(indicator);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int indicatorId)
        {
            try
            {
                BindDropdowns();
                var aDetail = await _indicatorService.GetIndicatorIdAsync(indicatorId);

                BindNestedDropdowns(aDetail.ThemeId, aDetail.ActivityId);
                return View(aDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Indicator indicator)
        {
            try
            {
                _logger.LogInformation("IndicatorController/Edit");
                if (ModelState.IsValid)
                {
                    indicator.UpdatedBy = userDetail.CreatedBy;
                    indicator.UpdatedOn = DateTime.Now;
                    var result = await _indicatorService.UpdateIndicatorAsync(indicator);
                    if (indicator.IsExist)
                    {
                        ModelState.AddModelError("", "Indicator name already exists");
                    }
                    else if (result)
                    {
                        TempData["Message"] = "Indicator Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Indicator Updation Failed.";
                    }
                }
                return View(indicator);
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

            var uomList = _ddlService.GetUoM(null);
            ViewBag.ddlUom = new SelectList(uomList, "id", "value");

            var indResponseType = _ddlService.GetIndicatorResponseType();
            ViewBag.ddlIndResponceType = new SelectList(indResponseType, "id", "value");

            var indicatorType = _ddlService.GetIndicatorType();
            ViewBag.ddlIndicatorType = new SelectList(indicatorType, "id", "value");
        }

        [NonAction]
        void BindNestedDropdowns(int themId, int activityId)
        {
            var subTheme = _ddlService.GetSubTheme(themId, null);
            ViewBag.ddlsubTheme = new SelectList(subTheme, "id", "value");

            var activity = _ddlService.GetActivity(themId, null);
            ViewBag.ddlActivity = new SelectList(activity, "id", "value");

            var subActivity = _ddlService.GetSubActivity(themId, activityId);
            ViewBag.ddlSubActivity = new SelectList(subActivity, "id", "value");

        }

        [HttpGet]
        public JsonResult BindSubThemeDropdown(int themeId)
        {
            _logger.LogInformation("IndicatorController/BindActivityDropdown");
            var selectListModels = _ddlService.GetSubTheme(themeId, null).ToList();
            return Json(new SelectList(selectListModels, "id", "value"));
        }

        [HttpGet]
        public JsonResult BindActivityDropdown(int themeId)
        {
            _logger.LogInformation("IndicatorController/BindActivityDropdown");
            var selectListModels = _ddlService.GetActivity(themeId, null).ToList();
            return Json(new SelectList(selectListModels, "id", "value"));

        }

        [HttpGet]
        public JsonResult BindSubActivityDropdown(int themeId, int activityId)
        {
            _logger.LogInformation("IndicatorController/BindActivityDropdown");
            var selectListModels = _ddlService.GetSubActivity(themeId, activityId).ToList();
            return Json(new SelectList(selectListModels, "id", "value"));

        }

        [HttpPost]
        public JsonResult ActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("IndicatorController/ActiveDeActive");
            var result = _indicatorService.ActiveDeActive(id, isChecked);
            return Json(result);

        }
    }
}
