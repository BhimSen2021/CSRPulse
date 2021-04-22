using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class PlanController : Controller
    {
        [Route("Plan")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
