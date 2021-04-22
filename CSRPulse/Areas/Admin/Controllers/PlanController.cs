﻿using CSRPulse.Controllers;
using CSRPulse.Model;
using CSRPulse.Services.Admin.Plan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Model.Admin;
namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class PlanController : BaseController<PlanController>
    {
        private readonly IPlanService _planService;     
        public PlanController(IPlanService planService):base()
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
        public IActionResult Create() => View(new PlanModel());

        [HttpPost]

        public async Task<IActionResult> Create(PlanModel planModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    planModel.CreatedBy = 1;
                    planModel.CreatedOn = DateTime.UtcNow;
                    planModel.IsDeleted = false;
                    //  var taskId = await _planService.AddNewPlanAsync(planModel);
                    UserTypeModel obj = new UserTypeModel
                    {
                        UserTypeName = "Internal",
                        CreatedBy = 1,
                        CreatedOn = DateTime.UtcNow,
                        IsDeleted = false
                    };
                    var taskId = await _planService.AddNewUserTypeAsync(obj);
                    if (taskId > 0)
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
