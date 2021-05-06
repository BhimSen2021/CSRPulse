using CSRPulse.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Customer/[Controller]/[action]")]
    public class DashboardController : BaseController<DashboardController>
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
