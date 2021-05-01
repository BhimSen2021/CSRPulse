using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet, Route("ContactMe")]
        public IActionResult ContactMe()
        {
            return View();
        }
    }
}
