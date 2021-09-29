using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CSRPulse.Controllers
{
    public class FinancialAuditReportController : BaseController<FinancialAuditReportController>
    {
        private readonly IFinancialAuditReportService _finanacServices;
        private readonly IDropdownBindService _ddlService;
        public FinancialAuditReportController(IFinancialAuditReportService financeServices, IDropdownBindService dropdownBindService)
        {
            _finanacServices = financeServices;
            _ddlService = dropdownBindService;

        }
        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            _logger.LogInformation("FinancialAuditReportController/Index");
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
           // return View();
        }
        [HttpGet]
        public async Task<PartialViewResult> GetfinancialList(FinancialAuditReport financial)
        {
            _logger.LogInformation("FinancialAuditReportController/GetfinancialList");
            try
            {
                var result = await _finanacServices.GetFinancialAsync(financial);

                return PartialView("_FinancialList", result);
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
            return View(new FinancialAuditReport());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(FinancialAuditReport financial)
        {
            try
            {
                _logger.LogInformation("FinancialAuditReportController/Create");
                BindDropdowns();
                if (ModelState.IsValid)
                {
                    financial.CreatedBy = userDetail.UserID;
                    financial.CreatedRid = userDetail.RoleId;
                    financial.CreatedRname = userDetail.RoleName;
                    var result = await _finanacServices.CreateFinancialAsync(financial);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "Financial Audit Report name already exists.");
                    }
                    if (result.FareportId > 0)
                    {
                        TempData["Message"] = "Financial Audit Report created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                
                return View(financial);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int fareportId)
        {
            try
            {
                BindDropdowns();
                var uDetail = await _finanacServices.GetFinancialByIdAsync(fareportId);
                return View(uDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(FinancialAuditReport financial)
        {
            try
            {
                _logger.LogInformation("FinancialAuditReportController/Edit");
                ModelState.Remove("IsActive");
                BindDropdowns();
                if (ModelState.IsValid)
                {
                    financial.UpdatedBy = userDetail.UserID;
                    financial.UpdatedRid = userDetail.RoleId;
                    financial.UpdatedRname = userDetail.RoleName;
                    financial.UpdatedOn = DateTime.Now;
                    var result = await _finanacServices.UpdateFinancialAsync(financial);
                    if (financial.IsExist)
                    {
                        ModelState.AddModelError("", "Financial Audit Report already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "Financial Audit Report Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(financial);
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
            _logger.LogInformation("FinancialAuditReportController/ActiveDeActive");
            var result = _finanacServices.ActiveDeActive(id, isChecked);
            return Json(result);

        }
        [NonAction]              
        void BindDropdowns()
        {
            var ngoList = _ddlService.GetPartners(null);
            ViewBag.ddlngo = new SelectList(ngoList, "id", "value");
            var managerList = _ddlService.GetUsers((int)Common.UserRole.ProgramManager);
            ViewBag.ddlManager = new SelectList(managerList, "id", "value");
            var auditorList = _ddlService.GetAuditors(null);
            ViewBag.ddlAuditor = new SelectList(auditorList, "id", "value");
            var auditorcheckerList = _ddlService.GetAuditorChecker(null);
            ViewBag.ddlAuditorchecker = new SelectList(auditorcheckerList, "id", "value");
            var auditormakerList = _ddlService.GetAuditorMaker(null);
            ViewBag.ddlAuditormaker = new SelectList(auditormakerList, "id", "value");
            var projectList = _ddlService.GetProjects(null);
            ViewBag.ddlGrant = new SelectList(projectList, "id", "value");
            var ACList = _ddlService.GetUsers((int)Common.UserRole.AuditorChecker);
            ViewBag.ddlAC = new SelectList(ACList, "id", "value");
            var AMList = _ddlService.GetUsers((int)Common.UserRole.AuditorMaker);
            ViewBag.ddlAM = new SelectList(AMList, "id", "value");
        }

    }
}
