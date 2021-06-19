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
    public class SubThemeController : BaseController<SubThemeController>
    {
        private readonly ISubThemeService _subThemeService;
        private readonly IDropdownBindService _ddlService;
        public SubThemeController(ISubThemeService subThemeService, IDropdownBindService dropdownBindService)
        {
            _subThemeService = subThemeService;
            _ddlService = dropdownBindService;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("SubThemeController/Index");
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
        public async Task<PartialViewResult> GetSubThemeList(SubTheme subTheme)
        {
            _logger.LogInformation("SubThemeController/GetSubThemeList");
            try
            {
                var result = await _subThemeService.GetSubThemesAsync(subTheme);
                return PartialView("_SubThemeList", result);
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
            return View(new SubTheme());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SubTheme subTheme)
        {
            try
            {
                _logger.LogInformation("SubThemeController/Create");
                if (ModelState.IsValid)
                {
                    subTheme.CreatedBy = userDetail.CreatedBy;
                    var result = await _subThemeService.CreateSubThemeAsync(subTheme);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "SubTheme name already exists.");
                    }
                    if (result.ThemeId > 0)
                    {
                        TempData["Message"] = "SubTheme created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                BindDropdowns();
                return View(subTheme);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int SubThemeId)
        {
            try
            {
                BindDropdowns();
                var uDetail = await _subThemeService.GetSubThemeByIdAsync(SubThemeId);
                return View(uDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(SubTheme subTheme)
        {
            try
            {
                _logger.LogInformation("SubThemeController/Edit");
                if (ModelState.IsValid)
                {
                    subTheme.UpdatedBy = userDetail.CreatedBy;
                    subTheme.UpdatedOn = DateTime.Now;
                    var result = await _subThemeService.UpdateSubThemeAsync(subTheme);
                    if (subTheme.IsExist)
                    {
                        ModelState.AddModelError("", "SubTheme name already exists");
                    }
                    else if (result)
                    {
                        TempData["Message"] = "SubTheme Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "SubTheme Updation Failed.";
                    }
                }
                return View(subTheme);
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
            var result = _subThemeService.ActiveDeActive(id, isChecked);
            return Json(result);

        }
    }
}
