using CSRPulse.Controllers;
using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class PlanController : BaseController<PlanController>
    {
        private readonly IPlanService _planService;
        public PlanController(IPlanService planService)
        {            
            _planService = planService;
        }
        public async Task<IActionResult> Index()
        {            
            _logger.LogInformation("Index");
            try
            {
                var data = await _planService.GetAllPlanAsync();
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public IActionResult Create() => View(new Plan());

        [HttpPost]
        public async Task<IActionResult> Create(Plan planModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    planModel.CreatedBy = 1;
                    planModel.CreatedOn = DateTime.UtcNow;
                    planModel.IsDeleted = false;
                    var planId = await _planService.AddNewPlanAsync(planModel);

                    if (planId > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(planModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
    }
}
