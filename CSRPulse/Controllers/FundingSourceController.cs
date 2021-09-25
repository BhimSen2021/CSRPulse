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
    public class FundingSourceController : BaseController<FundingSourceController>
    {
        private readonly IFundingSourceService _fundingSourceService;
        private readonly IDropdownBindService _ddlService;

        public FundingSourceController(IFundingSourceService fundingSourceService, IDropdownBindService dropdownBindService)
        {
            _fundingSourceService = fundingSourceService;
            _ddlService = dropdownBindService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("FundingSourceController/Index");
            try
            {
                BindSourcetype();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [NonAction]
        void BindSourcetype()
        {
            var fundingsourceList = _ddlService.GetFundingSourceAsync(null);
            ViewBag.ddlsourcetype = new SelectList(fundingsourceList, "id", "value");
        }
        [HttpGet]
        public async Task<PartialViewResult> GetFundingSourceList(FundingSource FundingSource)
        {
            _logger.LogInformation("FundingSourceController/GetFundingSourceList");
            try
            {
                var result = await _fundingSourceService.GetFundingSourcesAsync(FundingSource);
                return PartialView("_FundingSourceList", result);
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
            BindSourcetype();
            return View(new FundingSource());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(FundingSource FundingSource)
        {
            try
            {
                _logger.LogInformation("FundingSourceController/Create");
                BindSourcetype();
                if (ModelState.IsValid)
                {
                    FundingSource.CreatedBy = userDetail.UserID;
                    FundingSource.CreatedRid = userDetail.RoleId;
                    FundingSource.CreatedRname = userDetail.RoleName;
                    var result = await _fundingSourceService.CreateFundingSourceAsync(FundingSource);
                    if (result.IsExist)
                    {
                        ModelState.AddModelError("", "Funding Source name already exists.");
                    }
                    if (result.SourceId > 0)
                    {
                        TempData["Message"] = "Funding Source created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(FundingSource);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int SourceId)
        {
            try
            {
                BindSourcetype();
                var uDetail = await _fundingSourceService.GetFundingSourceByIdAsync(SourceId);
                return View(uDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(FundingSource FundingSource)
        {
            try
            {
                _logger.LogInformation("FundingSourceController/Edit");
                BindSourcetype();
                if (ModelState.IsValid)
                {
                    FundingSource.UpdatedBy = userDetail.UserID;
                    FundingSource.UpdatedRid = userDetail.RoleId;
                    FundingSource.UpdatedRname = userDetail.RoleName;
                    FundingSource.UpdatedOn = DateTime.Now;
                    var result = await _fundingSourceService.UpdateFundingSourceAsync(FundingSource);
                    if (FundingSource.IsExist)
                    {
                        ModelState.AddModelError("", "Funding Source name already exists");
                    }
                    else if (result)
                    {
                        TempData["Message"] = "Funding Source Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Funding Source Updation Failed.";
                    }
                }
                return View(FundingSource);
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
            _logger.LogInformation("FundingSourceController/ActiveDeActive");
            var result = _fundingSourceService.ActiveDeActive(id, isChecked);
            return Json(result);

        }
    }
}
