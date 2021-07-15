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
    public class ProcessController : BaseController<ProcessController>
    {
        private readonly IProcessServices _processServices;

        public ProcessController(IProcessServices processServices)
        {
            _processServices = processServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("ProcessController/Index");
            try
            {
                var result = await _processServices.GetProcessAsync(new Process());
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public async Task<PartialViewResult> GetProcessList(Process process)
        {
            _logger.LogInformation("ProcessController/GetProcessList");
            try
            {
                var result = await _processServices.GetProcessAsync(process);
                return PartialView("_ProcessList", result);
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
            return View(new Process());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Process process)
        {
            try
            {
                _logger.LogInformation("ProcessController/Create");
                if (ModelState.IsValid)
                {
                    if (await _processServices.RecordExist(process))
                    {
                        ModelState.AddModelError("", "Process already exists");
                    }
                    else
                    {
                        var result = await _processServices.CreateProcess(process);

                        if (result)
                        {
                            TempData["Message"] = "Process Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(process);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        public async Task<IActionResult> Edit(int processId)
        {
            try
            {
                var dDetail = await _processServices.GetProcessById(processId);
                return View(dDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Process process)
        {
            try
            {
                _logger.LogInformation("ProcessController/Edit");
                if (ModelState.IsValid)
                {
                    if (await _processServices.RecordExist(process))
                    {
                        ModelState.AddModelError("", "Process already exists");
                    }
                    else
                    {
                        var result = await _processServices.UpdateProcess(process);
                        if (result)
                        {
                            TempData["Message"] = "Process Updated Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }

                }
                return View(process);
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
            _logger.LogInformation("Process/ActiveDeActive");
            var result = _processServices.ActiveDeActive(id, isChecked);
            return Json(result);

        }
    }
}
