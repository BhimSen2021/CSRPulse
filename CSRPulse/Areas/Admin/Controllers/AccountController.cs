using CSRPulse.Model;
using CSRPulse.Services;
using static CSRPulse.Common.ExtensionMethods;
using DNTCaptcha.Core;
using DNTCaptcha.Core.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using CSRPulse.Common;
using CSRPulse.Attributes;

namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class AccountController : Controller
    {
        private Guid userSessionId;
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly ILogger<HomeController> _logger;
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly IMemoryCache memoryCache;
        public AccountController(IAccountService accountService, IEmailService emailService,IDNTCaptchaValidatorService validatorService, IMemoryCache memoryCache, ILogger<HomeController> logger)
        {
            _accountService = accountService;
            _emailService = emailService;
            _validatorService = validatorService;
            this.memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(SingIn singIn, string returnUrl)
        {

            _logger.LogInformation("Admin/AccountController/Login");
            try
            {
                if (string.IsNullOrEmpty(singIn.hdUserName))
                    ModelState.AddModelError("UserName", "Please enter user name.");
                if (string.IsNullOrEmpty(singIn.hdPassword))
                    ModelState.AddModelError("Password", "Please enter password.");

                if (ModelState.IsValid)
                {
                    if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                    {

                        this.ModelState.AddModelError(DNTCaptchaTagHelper.CaptchaInputName, "Please Enter Valid Captcha.");
                        return View(singIn);
                    }

                    singIn.UserName = Password.DecryptStringAES(singIn.hdUserName);
                    singIn.Password = Password.DecryptStringAES(singIn.hdPassword);


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

                        bool isExist = memoryCache.TryGetValue(uDetail.UserID, out userSessionId);
                        if (!isExist)
                        {
                            userSessionId = GetUserSessionId();
                            memoryCache.Set(uDetail.UserID, userSessionId);
                        }
                        else
                        {
                            TempData["logoutAlert"] = "The previous session was not logged out. If you do wish to login now , please press the logout button to close the previous session, else press cancel.";

                            ModelState.AddModelError("", "The previous session was not logged out.");
                            return View(singIn);
                        }


                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else
                    {
                        if (uDetail.ErrorMessage == "notexists")
                        {
                            ModelState.AddModelError("", "Please enter correct user name.");
                        }
                        else if (uDetail.WrongAttemp.HasValue)
                        {
                            if (uDetail.WrongAttemp.Value == 0)
                                ModelState.AddModelError("WrongAttemp", "Your account is temporary locked, please contact your administrator.");
                            else
                            {
                                ModelState.AddModelError("WrongAttemp", $"{uDetail.WrongAttemp.Value} attemp(s) left.");
                                ModelState.AddModelError("", "Invalid credentials");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("WrongAttemp", $"Your account is temporary locked, please try after {uDetail.ErrorMessage}");
                        }
                    }

                }
                return View(singIn);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }


        public IActionResult ForgotPassword()
        {
           
            return View(new ForgotPassword());
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassword forgotPassword, string ButtonType)
        {
            _logger.LogInformation("Account/ForgotPassword");
            try
            {
                if (ButtonType == "OTP" || ButtonType == "resendotp")
                {
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
                            ModelState.AddModelError("", "If we have an account for the username you provided, Further instructions have been sent to that, Please check your email.");
                            return View(forgotPassword);
                        }
                    }
                }
                else if (ButtonType == "verifyotp")
                {
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
                                ModelState.AddModelError("OTP", "Invalid OTP entred");
                                ViewBag.IsVerified = false;
                                ViewBag.IsOTPSection = false;
                            }
                        }
                    }
                }
                else if (ButtonType == "Reset")
                {
                    _accountService.UpdatePassword(forgotPassword.EmailId, GeneratePassword(8));
                    TempData["Message"] = "Password Reset Successfully, New password has been sent on your registered email.";
                    HttpContext.Session.Remove("OTP");
                    return View(new ForgotPassword());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
            }
            return View(forgotPassword);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _logger.LogInformation("Amin/AccountController/Logout");
            UserDetail userDetail = HttpContext.Session.GetComplexData<UserDetail>("User");
            if (userDetail != null)
            {
                HttpContext.Session.Clear();
                bool isExist = memoryCache.TryGetValue(userDetail.UserID, out userSessionId);
                if (isExist)
                {
                    memoryCache.Remove(userDetail.UserID);
                }
            }
           
            return View();
        }

        private Guid GetUserSessionId()
        {
            return Guid.NewGuid();
        }


        [SessionTimeout]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        
        [HttpPost]
        [SessionTimeout]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            _logger.LogInformation("ChangePasswordController/ChangePassword");

            if (string.IsNullOrEmpty(changePassword.hdOldPassword))
                ModelState.AddModelError("OldPassword", "Please enter old password.");

            if (string.IsNullOrEmpty(changePassword.hdConfirmPassword))
                ModelState.AddModelError("ConfirmPassword", "Please enter confirm password.");
            if (string.IsNullOrEmpty(changePassword.hdPassword))
                ModelState.AddModelError("Password", "Please enter password.");

            changePassword.OldPassword = Password.DecryptStringAES(changePassword.hdOldPassword);
            changePassword.Password = Password.DecryptStringAES(changePassword.hdPassword);
            changePassword.ConfirmPassword = Password.DecryptStringAES(changePassword.hdConfirmPassword);
            string ErrorMessage = string.Empty;
            UserDetail userDetail = HttpContext.Session.GetComplexData<UserDetail>("User");
            changePassword.UserId = userDetail.UserID;
            changePassword.UserName = userDetail.UserName;

            if (!Password.ValidatePassword(changePassword.Password, out ErrorMessage))
            {
                ModelState.AddModelError("", ErrorMessage);
            }
            else if (!_accountService.ValidatePassword(changePassword, out ErrorMessage))
            {
                ModelState.AddModelError("", ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                bool res = await _accountService.ChangePassword(changePassword);
                if (res)
                {
                   
                    var mailDetail = new MailDetail()
                    {
                        To = userDetail.EmailID,
                        FullName = userDetail.FullName,
                        Password = changePassword.Password,
                    };

                    bool IsSend = await _emailService.SendMail(mailDetail, MailProcess.ResetPassword);
                    if (IsSend)
                        TempData["Message"] = "Password has been changed successfully.";
                    else
                        TempData["Message"] = "Password has been changed successfully, but email sending failed.";
                }
                else
                {
                    TempData["Error"] = "Password change failed!";
                    return View();
                }
                return RedirectToAction("Logout", "Account", new { area = "Admin" });
            }
            return View();
        }
    }
}
