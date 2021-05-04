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


        [HttpGet]

        public IActionResult CustomerLogin()
        {
            return View();
        }


        /// <summary>
        /// In this function, we'll check the customer in our database and then verify its credentail in customer database
        /// /// </summary>
        /// <param name="singIn"> contain user input fields</param>
        /// <param name="returnUrl">to return previous page</param>
        /// <param name="ButtonName">based on button, will perfom action</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CustomerLogin(SingIn singIn, string returnUrl, string ButtonName)
        {
            _logger.LogInformation("AccountController/CustomerLogin");
            try
            {
                /// First check the customer ID is exists in our database
                if (ButtonName == "verify")
                {
                    string returnOutPut = string.Empty;
                    bool isCustExists = false;
                    isCustExists = _accountService.AuthenticateCustomer(singIn, out returnOutPut);
                    if (isCustExists)
                    {
                        ModelState.AddModelError("", "Your Registration will be expire within 2 day(s),Please contact you administrator.");
                        return View(singIn);

                    }
                }

                // if customer ID is exists in our database, then check user credential in customer database
                if (ModelState.IsValid)
                {
                    bool isAuthenticated = false;
                    UserDetail userDetail = new UserDetail();
                    isAuthenticated = _accountService.AuthenticateUser(singIn, out userDetail);
                    if (isAuthenticated)
                    {
                        HttpContext.Session.SetComplexData("User", userDetail);

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        return RedirectToAction("CustomerLogin", "Account");
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
    }
}
