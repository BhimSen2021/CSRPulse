using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ThemeController : BaseController<ThemeController>
    {
        private readonly IThemeService _themeService;
        public ThemeController(IThemeService themeService)
        {
            _themeService = themeService;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("ThemeController/Index");
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
        public async Task<PartialViewResult> GetThemeList(Theme theme)
        {
            _logger.LogInformation("ThemeController/GetThemeList");
            try
            {
                var result = await _themeService.GetThemesAsync(theme);
                return PartialView("_ThemeList", result);
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
            return View(new Theme());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Theme theme)
        {
            try
            {
                _logger.LogInformation("ThemeController/Create");
                if (ModelState.IsValid)
                {
                    theme.CreatedBy = userDetail.UserID;
                    theme.CreatedRid = userDetail.RoleId;
                    theme.CreatedRname = userDetail.RoleName;
                    var result = await _themeService.CreateThemeAsync(theme);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "Theme name already exists.");
                    }
                    if (result.ThemeId > 0)
                    {
                        TempData["Message"] = "Theme created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(theme);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int ThemeId)
        {
            try
            {
                var uDetail = await _themeService.GetThemeByIdAsync(ThemeId);
                return View(uDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Theme theme)
        {
            try
            {
                _logger.LogInformation("ThemeController/Edit");
                if (ModelState.IsValid)
                {
                    theme.UpdatedBy = userDetail.UserID;
                    theme.UpdatedRid = userDetail.RoleId;
                    theme.UpdatedRname = userDetail.RoleName;
                    theme.UpdatedOn = DateTime.Now;
                    var result = await _themeService.UpdateThemeAsync(theme);
                    if (theme.IsExist)
                    {
                        ModelState.AddModelError("", "Theme name already exists");
                    }
                    else if (result)
                    {
                        TempData["Message"] = "Theme Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "UoM Updation Failed.";
                    }
                }
                return View(theme);
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
            _logger.LogInformation("UoMController/ActiveDeActive");
            var result = _themeService.ActiveDeActive(id, isChecked);
            return Json(result);

        }

    }
}
