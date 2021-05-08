using CSRPulse.Controllers;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Model;

namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class AccountController : BaseController<AccountController>
    {
        private readonly IAccountService _accountService;
        private readonly IMenuService _menuService;
        public AccountController(IAccountService accountService, IMenuService menuService)
        {
            _accountService = accountService;
            _menuService = menuService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(SingIn singIn, string returnUrl)
        {
            _logger.LogInformation("Amin/AccountController/Login");
            try
            {
                if (ModelState.IsValid)
                {
                    bool isAuthenticated = false;
                    UserDetail uDetail = new UserDetail();
                    isAuthenticated = _accountService.AuthenticateUser(singIn, out uDetail);
                    if (isAuthenticated)
                    {
                        HttpContext.Session.SetComplexData("User", uDetail);

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else
                        ModelState.AddModelError("", "Invalid credentials");

                }
                return View(singIn);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        [HttpGet]
        public IActionResult Logout()
        {
            _logger.LogInformation("Amin/AccountController/Logout");
            UserDetail userDetail = HttpContext.Session.GetComplexData<UserDetail>("User");
            if (userDetail != null)
            {
                HttpContext.Session.Clear();                
            }
            
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

    }
}
