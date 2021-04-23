using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        private readonly ISignupService _signupService;

        public AccountController(ISignupService signupService)
        {
            _signupService = signupService;
        }
        [HttpGet, Route("Signup")]
        public IActionResult Signup()
        {
            _logger.LogInformation("AccountController/UserSignup");
            return View();
        }


        [HttpPost, Route("Signup")]
        public async Task<IActionResult> Signup(User user)
        {
            _logger.LogInformation("AccountController/Signup");
            try
            {
                if (ModelState.IsValid)
                {
                    await _signupService.CreateUserAsync(user);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }

        [HttpGet, Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, Route("login")]
        public IActionResult Login(SingIn singIn, string returnUrl)
        {
            _logger.LogInformation("AccountController/Login");
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");

                }
                return View(singIn);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }

    }
}
