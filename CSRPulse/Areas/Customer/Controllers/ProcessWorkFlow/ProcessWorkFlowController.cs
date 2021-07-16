using CSRPulse.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Areas.Customer.Controllers.ProcessWorkFlow
{
    public class ProcessWorkFlowController : BaseController<ProcessWorkFlowController>
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
