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
    public class DesignationController : BaseController<DesignationController>
    {
        private readonly IDesignationServices _designationServices;

        public DesignationController(IDesignationServices designationServices)
        {
            _designationServices = designationServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("DesignationController/Index");
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
        public async Task<PartialViewResult> GetDesignationList(Designation designation)
        {
            _logger.LogInformation("DesignationController/GetDesignationList");
            try
            {
                var result = await _designationServices.GetDesignationsAsync(designation);
                return PartialView("_DesignationList", result);
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
            return View(new Designation());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Designation designation)
        {
            try
            {
                _logger.LogInformation("DesignationController/Create");
                if (ModelState.IsValid)
                {
                    designation.CreatedBy = userDetail.UserID;                    
                    if (await _designationServices.RecordExist(designation))
                    {
                        ModelState.AddModelError("", "Designation already exists");
                    }
                    else
                    {
                        var result = await _designationServices.CreateDesignation(designation);
                        if (designation.IsExist)
                        {
                            ModelState.AddModelError("", "Designation already exists");
                        }
                        if (result)
                        {
                            TempData["Message"] = "Designation Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(designation);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int designationId)
        {
            try
            {
                var dDetail = await _designationServices.GetDesignationById(designationId);
                return View(dDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Designation designation)
        {
            try
            {
                _logger.LogInformation("DesignationController/Edit");
                if (ModelState.IsValid)
                {
                    designation.UpdatedBy = userDetail.UserID;
                    designation.UpdatedOn = DateTime.Now;
                    var result = await _designationServices.UpdateDesignation(designation);
                    if (designation.IsExist)
                    {
                        ModelState.AddModelError("", "Designation already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "Designation Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(designation);
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
            _logger.LogInformation("DesignationController/ActiveDeActive");
            var result = _designationServices.ActiveDeActive(id, isChecked);
            return Json(result);

        }

    }
}
