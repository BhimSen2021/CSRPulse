using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class HandlerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult UnAuthorizeAccess()
        {
            return View("UnAuthorizeAccess");
        }
    }
}
