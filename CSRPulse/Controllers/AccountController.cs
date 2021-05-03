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
        private readonly IAccountService _accountService;
        private readonly IMenuService _menuService;

        public AccountController(IAccountService accountService, IMenuService menuService)
        {
            _accountService = accountService;
            _menuService = menuService;
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
                    await _accountService.CreateUserAsync(user);
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

        public ActionResult _MenuAsync()
        {
            List<Menu> userMenu = new List<Menu>();
            UserDetail uDetail = new UserDetail();

            if (HttpContext.Session.GetComplexData<UserDetail>("User") != null)
            {
                uDetail = HttpContext.Session.GetComplexData<UserDetail>("User");
            }
            userMenu = _menuService.GetMenuByUserAsync(uDetail.UserID);
            return PartialView(userMenu);
        }

    }
}
