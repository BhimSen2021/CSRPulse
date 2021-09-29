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
    [AutoValidateAntiforgeryToken]
    public class NarrativeController : BaseController<SubThemeController>
    {
        private readonly IDropdownBindService _ddlService;
        private readonly INarrativeService _narrativeService;

        public NarrativeController(INarrativeService narrativeService, IDropdownBindService ddlService)
        {
            _narrativeService = narrativeService;
            _ddlService = ddlService;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("NarrativeController/Index");
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
        public async Task<PartialViewResult> GetQuestionList(NarrativeQuestion narrative)
        {
            _logger.LogInformation("NarrativeController/GetQuestionList");
            try
            {
                var result = await _narrativeService.GetQuestionAsync(narrative);
                return PartialView("_NarrativeList", result);
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
            return View(new NarrativeQuestion());
        }

        [HttpPost]      
        public async Task<IActionResult> Create(NarrativeQuestion narrative)
        {
            try
            {
                _logger.LogInformation("NarrativeController/Create");
                if (ModelState.IsValid)
                {
                    narrative.CreatedBy = userDetail.UserID;
                    narrative.CreatedRid = userDetail.RoleId;
                    narrative.CreatedRname = userDetail.RoleName;
                    var result = await _narrativeService.CreateAsync(narrative);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "Question name already exists.");
                    }
                    if (result.QuestionId > 0)
                    {
                        TempData["Message"] = "narrative created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                BindDropdowns();
                return View(narrative);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int questionId)
        {
            try
            {
                BindDropdowns();
                var question = await _narrativeService.GetQuestionByIdAsync(questionId);
                return View(question);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NarrativeQuestion narrative)
        {
            try
            {
                _logger.LogInformation("NarrativeController/Edit");
                if (ModelState.IsValid)
                {
                    narrative.UpdatedBy = userDetail.CreatedBy;
                    narrative.UpdatedRid = userDetail.RoleId;
                    narrative.UpdatedRname = userDetail.RoleName;
                    narrative.UpdatedOn = DateTime.Now;
                    var result = await _narrativeService.UpdateQuestionAsync(narrative);
                    if (narrative.IsExist)
                    {
                        ModelState.AddModelError("", "Question name already exists");
                    }
                    else if (result)
                    {
                        TempData["Message"] = "Question Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Question Updation Failed.";
                    }
                }
                return View(narrative);
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
            var processList = _ddlService.GetProcessByFinalStatus(99);
            ViewBag.ddlProcess = new SelectList(processList, "id", "value");
        }

        [HttpPost]
        public JsonResult ActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("NarrativeController/ActiveDeActive");
            var result = _narrativeService.ActiveDeActive(id, isChecked);
            return Json(result);
        }

        [HttpPost]
        public JsonResult CommentRequired(int id, bool isChecked)
        {
            _logger.LogInformation("NarrativeController/CommentRequired");
            var result = _narrativeService.CommentRequired(id, isChecked);
            return Json(result);
        }
    }
}
