using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CSRPulse.Controllers
{
    public class RegistrationController : BaseController<RegistrationController>
    {
        private readonly IRegistrationService registrationService;
        private readonly IDropdownBindService ddlService;

        public RegistrationController(IRegistrationService registrationService, IDropdownBindService ddlService)
        {
            this.registrationService = registrationService;
            this.ddlService = ddlService;
        }

        [NonAction]
        void BindDropdowns()
        {
            var stateList = ddlService.GetStateAsync(null, null);
            ViewBag.ddlState = new SelectList(stateList, "id", "value");
        }
        public IActionResult Index(int? cid, int? pId = null)
        {
            _logger.LogInformation("RegistrationController/Index");

            Model.Customer customer = new Model.Customer
            {
                PlainId = pId
            };

            if (cid.HasValue)
            {
                customer.CustomerId = (int)cid;
            }
            BindDropdowns();
            return View(customer);

        }
        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            return await Task.FromResult((IActionResult)PartialView("_Registration", new Model.Customer()));
        }


        /// <summary>
        /// To Register New Cutomer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Registration(Model.Customer customer, string ButtonType)
        {
            _logger.LogInformation("RegistrationController/Index");
            try
            {
                if (!customer.IsAgree)
                    ModelState.AddModelError("IsAgree", "Please select terms & condition");

                if (ModelState.IsValid)
                {
                    BindDropdowns();
                    if (ButtonType == "verifydtl" || ButtonType == "Resend OTP")
                    {
                        bool res = await registrationService.CustomerExists(customer);
                        if (!res)
                        {
                            return Json(new { recordExist = true });
                        }

                        int OTP = registrationService.GenerateOTP();
                        HttpContext.Session.SetComplexData("OTP", OTP);

                        ViewBag.OTPSent = registrationService.SendOTP(customer.Email, OTP);
                        if (ViewBag.OTPSent)
                            ViewBag.Message = "OTP has ben sent on your Email.Please Enter OTP to verify your details.";
                        ViewBag.IsVerified = false;
                        return Json(new { htmlData = ConvertViewToString("_Registration", customer, true) });
                    }
                    if (ButtonType == "verifyotp")
                    {

                        if (HttpContext.Session.GetComplexData<int>("OTP") != 0)
                        {
                            var otpval = Convert.ToString(HttpContext.Session.GetComplexData<int>("OTP"));
                            if (customer.OTP != "0" && otpval == customer.OTP)
                            {
                                ViewBag.Message = "OTP has been verified.";
                                ViewBag.VerifyOTP = true;
                                ViewBag.IsVerified = true;
                            }
                            else if (customer.OTP != "0" && otpval != customer.OTP)
                            {
                                ViewBag.Message = "Incorrct OTP.";
                                ViewBag.IsVerified = false;
                            }
                            else
                            {
                                ViewBag.Message = "Please enter OTP.";
                                ViewBag.IsVerified = false;
                            }
                            return Json(new { htmlData = ConvertViewToString("_Registration", customer, true) });
                        }
                    }

                    if (ButtonType == "submit")
                    {
                        customer.CreatedBy = 1;
                        int customerID = await registrationService.CustomerRegistrationAsync(customer);
                        if (customerID > 0)
                        {
                            customer.CustomerId = customerID;
                            return Json(new { success = true, htmlData = ConvertViewToString("_PaymentOption", customer, true) });
                        }
                        else
                        {
                            if (customer.RecordExist)
                            {

                                return Json(new { recordExist = true });
                            }
                        }
                    }
                }
                BindDropdowns();
                return Json(new { htmlData = ConvertViewToString("_Registration", customer, true) });

            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> PaymentOption(Model.Customer customer, string ButtonType)
        {
            try
            {

                if (ButtonType == "free")
                {
                    customer.CustomerPayment = new Model.CustomerPayment
                    {
                        CustomerID = customer.CustomerId,
                        AmountPaid = 0,
                        IsSuccess = true,
                        PaymentDate = DateTime.Now,
                        PaymentMode = 1,
                        CreatedBy = 1

                    };

                    customer.CustomerLicense = new Model.CustomerLicenseActivation
                    {

                        ActivationCount = 1,
                        ActivationDate = DateTime.Now,
                        LastActivationDate = DateTime.Now.AddMonths(1),
                        CustomerID = customer.CustomerId,
                        PlanID = 2,
                        CreatedBy = 1
                    };

                    var res = await registrationService.CustomerPaymentAsync(customer);

                    return Json(new { success = res });
                }
                return new ContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

    }
}
