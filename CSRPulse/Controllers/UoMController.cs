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
    public class UoMController : BaseController<UoMController>
    {
        private readonly IUOMService _uOMService;
        public UoMController(IUOMService uOMService)
        {
            _uOMService = uOMService;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("UoMController/Index");
            try
            {
                var result = await _uOMService.GetUOMsAsync();
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
            return View(new Uom());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Uom uom)
        {
            try
            {
                _logger.LogInformation("UoMController/Create");
                if (ModelState.IsValid)
                {
                    uom.CreatedBy = userDetail.CreatedBy;
                    var result = await _uOMService.CreateUOMAsync(uom);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "Unit name already exists.");
                    }
                    if (result.UOMId > 0)
                    {
                        TempData["Message"] = "Unit of measurement created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(uom);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int uomId)
        {
            try
            {
                var uDetail = await _uOMService.GetUOMByIdAsync(uomId);
                return View(uDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Uom uom)
        {
            try
            {
                _logger.LogInformation("UoMController/Edit");
                if (ModelState.IsValid)
                {
                    uom.UpdatedBy = userDetail.CreatedBy;
                    uom.UpdatedOn = DateTime.Now;
                    var result = await _uOMService.UpdateUOMAsync(uom);
                    if (uom.IsExist)
                    {
                        ModelState.AddModelError("", "Unit name already exists");
                    }
                    else if (result)
                    {
                        TempData["Message"] = "UoM Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "UoM Updation Failed.";
                    }
                }
                return View(uom);
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
            var result = _uOMService.ActiveDeActive(id, isChecked);
            return Json(result);

        }

    }
}
