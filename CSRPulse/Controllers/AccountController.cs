using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        [Route("User-Signup")]
        public IActionResult UserSignup()
        {
            return View();
        }


        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }


    }
}
