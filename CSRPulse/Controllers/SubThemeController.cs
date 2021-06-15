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
    public class SubThemeController :  BaseController<UoMController>
    {
        private readonly ISubThemeService _subThemeService;
        public SubThemeController(ISubThemeService subThemeService)
        {
            _subThemeService = subThemeService;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("SubThemeController/Index");
            try
            {
                var result = await _subThemeService.GetSubThemesAsync();
                return View(result);
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

    }
}
