﻿using CSRPulse.Model;
using CSRPulse.Services;
using DNTCaptcha.Core;
using DNTCaptcha.Core.Providers;
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
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly IMaintenanceService _maintenanceService;
        public AccountController(IAccountService accountService, IMenuService menuService, IDNTCaptchaValidatorService validatorService, IMaintenanceService maintenanceService)
        {
            _accountService = accountService;
            _menuService = menuService;
            _validatorService = validatorService;
            _maintenanceService = maintenanceService;
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

        public IActionResult CustomerLogin(CustomerSignIn signIn, string returnUrl, string ButtonName)
        {
            _logger.LogInformation("AccountController/CustomerLogin");
            try
            {
                /// First check site is under maintenance
                if (IsMaintenance())
                {
                    return Json(new { maintenance = true, htmlData = ConvertViewToString("_Authenticate", signIn, true) });
                }

                /// First check the customer ID is exists in our database
                if (ButtonName == "verify")
                {
                    ModelState.Remove("UserName");
                    ModelState.Remove("Password");

                    if (!ModelState.IsValid)
                    {
                        return Json(new { htmlData = ConvertViewToString("_Authenticate", signIn, true) });
                    }
                    string returnOutPut = string.Empty;
                    int? customerID = null;
                    bool isCustExists = false;
                    string companyName;
                    isCustExists = _accountService.AuthenticateCustomer(signIn, out returnOutPut, out customerID, out companyName);
                    if (isCustExists)
                    {
                        TempData["companyName"] = companyName;
                        TempData.Keep("companyName");
                        ModelState.AddModelError("", UserDefineMessage(returnOutPut));
                        return Json(new { htmlData = ConvertViewToString("_CustomerLogin", signIn, true) });
                    }
                    else
                    {
                        if (returnOutPut == "nopayment")
                        {
                            // ModelState.AddModelError("", UserDefineMessage(returnOutPut));
                            //Customer customer = new Customer
                            //{
                            //    CustomerId = (int)customerID
                            //};   
                            TempData.Remove("companyName");
                            var userMsg = UserDefineMessage(returnOutPut);
                            TempData["customerCode"] = signIn.CompanyID;
                            return Json(new { payment = true, cid = (int)customerID, msg = userMsg });
                        }
                        else
                        {
                            TempData.Remove("companyName");
                            ModelState.AddModelError("", UserDefineMessage(returnOutPut));
                            return Json(new { htmlData = ConvertViewToString("_Authenticate", signIn, true) });
                        }
                    }
                }

                // if customer ID is exists in our database, then check user credential in customer database
                if (ModelState.IsValid)
                {


                    TempData.Keep("companyName");
                    if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                    {

                        this.ModelState.AddModelError(DNTCaptchaTagHelper.CaptchaInputName, "Please Enter Valid Captcha.");
                        return View(signIn);
                    }
                    bool isAuthenticated = false;
                    UserDetail userDetail = new UserDetail();
                    SingIn sign = new SingIn
                    {
                        UserName = signIn.UserName,
                        Password = signIn.Password,
                        RememberMe = signIn.RememberMe
                    };

                    isAuthenticated = _accountService.AuthenticateUser(sign, out userDetail);
                    if (isAuthenticated)
                    {
                        HttpContext.Session.SetComplexData("User", userDetail);
                        TempData.Remove("companyName");
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Dashboard", new { Area = "Customer" });
                    }
                    else
                    {
                        if (userDetail.ErrorMessage == "notexists")
                        {
                            ModelState.AddModelError("", "Please enter correct user name.");
                        }
                        else if (userDetail.WrongAttemp.HasValue)
                        {
                            if (userDetail.WrongAttemp.Value == 0)
                                ModelState.AddModelError("WrongAttemp", "Your account is temporary locked, please contact your administrator.");
                            else
                            {
                                ModelState.AddModelError("WrongAttemp", $"Invalid credential, {userDetail.WrongAttemp.Value} attemp(s) left.");
                                ModelState.AddModelError("", "Invalid credentials");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("WrongAttemp", $"Your account is temperory locked, please try after {userDetail.ErrorMessage}");
                        }
                    }
                }
                return View(signIn);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        /// <summary>
        /// This function will return user define message, based on output return from  _accountService.AuthenticateCustomer() service 
        /// </summary>
        /// <param name="returnOutPut"></param>
        /// <returns></returns>
        [NonAction]
        private string UserDefineMessage(string returnOutPut)
        {
            try
            {
                string message = string.Empty;
                switch (returnOutPut)
                {
                    case "3daysexp":
                        message = "Dear Customer, Your Licence will be expire in 3 days.";
                        break;
                    case "2daysexp":
                        message = "Dear Customer, Your Licence will be expire in 2 days.";
                        break;
                    case "1dayexp":
                        message = "Dear Customer, Your Licence will be expire in 1 day.";
                        break;
                    case "0exp":
                        message = "Dear Customer, Your Licence will be expire today.";
                        break;
                    case "expired":
                        message = "Dear Customer, Your Licence is expired. Please contact to your vendor.";
                        break;
                    case "lincexpired":
                        message = "Dear Customer, Your Licence is expired. Please contact to your vendor.";
                        break;
                    case "nopayment":
                        message = "Dear Customer, We did'nt received payement for the selected plan, Please do payment to proceed.";
                        break;
                    case "notexists":
                        message = "Dear Customer, We did'nt find you company detail in our database, please first register and then try to login.";
                        break;
                    default:
                        break;
                }
                return message;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public ViewResult ForgotPassword(string cid)
        {
            _logger.LogInformation($"Account/ForgotPassword/cid={cid}");
            try
            {
                ForgotPassword forgotPassword = new ForgotPassword
                {

                    CompanyID = cid
                };
                TempData.Keep("companyName");
                return View(forgotPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
            }
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassword forgotPassword, string ButtonType)
        {
            _logger.LogInformation("Account/ForgotPassword");
            try
            {
                if (ButtonType == "OTP" || ButtonType == "resendotp")
                {
                    TempData.Keep("companyName");
                    ModelState.Remove("OTP");
                    if (ModelState.IsValid)
                    {

                        var userExists = _accountService.UserExists(username: null, emailId: forgotPassword.EmailId, password: null);
                        if (userExists)
                        {
                            var OTP = GenerateOTP();
                            HttpContext.Session.SetComplexData("OTP", OTP);
                            forgotPassword.OTP = OTP;
                            ViewBag.OTPSent = _accountService.SendOTP(forgotPassword, Common.MailProcess.OneTimePassword);
                            ViewBag.OTPSent = true;
                            ViewBag.IsVerified = false;
                            ViewBag.IsOTPSection = false;
                            forgotPassword.OTP = string.Empty;
                        }
                        else
                        {
                            ModelState.AddModelError("UserName", "Please enter correct user name.");
                            return View(forgotPassword);
                        }
                    }
                }
                else if (ButtonType == "verifyotp")
                {
                    TempData.Keep("companyName");
                    if (ModelState.IsValid)
                    {

                        if (!string.IsNullOrEmpty(HttpContext.Session.GetComplexData<string>("OTP")))
                        {
                            var otpval = Convert.ToString(HttpContext.Session.GetComplexData<string>("OTP"));
                            if (!string.IsNullOrEmpty(forgotPassword.OTP) && otpval == forgotPassword.OTP)
                            {
                                ViewBag.VerifyOTP = true;
                                ViewBag.IsVerified = true;
                            }
                            else if (!string.IsNullOrEmpty(forgotPassword.OTP) && otpval != forgotPassword.OTP)
                            {
                                ModelState.AddModelError("OTP", "Incorrct OTP, Please enter correct OTP");
                                ViewBag.IsVerified = false;
                                ViewBag.IsOTPSection = false;
                            }
                        }
                    }
                }
                else if (ButtonType == "Reset")
                {
                    TempData.Remove("companyName");
                    _accountService.UpdatePassword(forgotPassword.EmailId, "Password");
                    TempData["Message"] = "New password has been sent on your registered email.";
                    return RedirectToAction("CustomerLogin");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
            }
            return View(forgotPassword);
        }

        [NonAction]
        private bool IsMaintenance()
        {
            var mData = _maintenanceService.GetMaintenanceDetails();
            if (mData != null)
            {
                var cDateTime = DateTime.Now;
                if (mData.EndDateTime == null)
                {
                    if (cDateTime > mData.StartDateTime && mData.IsMaintenance)
                    { return true; }

                    else { return false; }
                }
                else
                {
                    if (cDateTime > mData.StartDateTime && cDateTime < mData.EndDateTime)
                    { return true; }
                    else { return false; }
                }
            }
            return false;
        }
    }
}
