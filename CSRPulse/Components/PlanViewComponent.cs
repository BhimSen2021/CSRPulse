using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Services.Admin;

namespace CSRPulse.Components
{
    
    public class PlanViewComponent: ViewComponent
    {
        private readonly IPlanService planService;
        public PlanViewComponent(IPlanService planService)
        {
            this.planService = planService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int planId)
        {          
           
            var plan = await planService.GetPlanByIdAsync(planId);


            return View("Plan",plan);
        }
    }
}
