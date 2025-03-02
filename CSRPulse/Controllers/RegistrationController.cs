﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CSRPulse.Controllers
{

    public class RegistrationController : BaseController<RegistrationController>
    {

        private readonly IRegistrationService registrationService;
        private readonly IDropdownBindService ddlService;
        private IWebHostEnvironment _Environment;
        public RegistrationController(IRegistrationService registrationService, IDropdownBindService ddlService, IWebHostEnvironment Environment)
        {
            this.registrationService = registrationService;
            this.ddlService = ddlService;
            _Environment = Environment;
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
                customer.CustomerCode = (string)TempData["customerCode"];
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
                    string msgType = string.Empty;
                    string msg = string.Empty;
                    BindDropdowns();
                    if (ButtonType == "verifydtl" || ButtonType == "Resend OTP")
                    {
                        bool res = await registrationService.CustomerExists(customer);
                        if (!res)
                        {
                            return Json(new { recordExist = true });
                        }

                        var OTP = GenerateOTP();
                        HttpContext.Session.SetComplexData("OTP", OTP);

                        ViewBag.OTPSent = registrationService.SendOTP(customer.Email, OTP, customer.CustomerName);
                        if (ViewBag.OTPSent)
                            msg = "OTP has ben sent on your Email.Please Enter OTP to verify your details.";

                        msgType = "success";
                        ViewBag.IsVerified = false;
                        ViewBag.IsOTPSection = false;
                        return Json(new { rType = 1, msgType = msgType, msg = msg, htmlData = ConvertViewToString("_Registration", customer, true) });
                    }
                    if (ButtonType == "verifyotp")
                    {
                        if (!string.IsNullOrEmpty(HttpContext.Session.GetComplexData<string>("OTP")))
                        {
                            var otpval = Convert.ToString(HttpContext.Session.GetComplexData<string>("OTP"));
                            if (!string.IsNullOrEmpty(customer.OTP) && otpval == customer.OTP)
                            {
                                msg = "OTP has been Validated.";
                                ViewBag.VerifyOTP = true;
                                ViewBag.IsVerified = true;
                                msgType = "success";
                            }
                            else if (!string.IsNullOrEmpty(customer.OTP) && otpval != customer.OTP)
                            {
                                msg = "Incorrct OTP, Please enter correct OTP";
                                ViewBag.IsVerified = false;
                                msgType = "error";
                                ViewBag.IsOTPSection = false;
                            }
                            else
                            {
                                msg = "Please enter OTP.";
                                ViewBag.IsVerified = false;
                                msgType = "warning";
                                ViewBag.IsOTPSection = false;
                            }
                            return Json(new { rType = 2, msgType = msgType, msg = msg, htmlData = ConvertViewToString("_Registration", customer, true) });
                        }
                    }

                    if (ButtonType == "submit")
                    {
                        customer.CreatedBy = userDetail.UserID ;
                        customer.CreatedRid = userDetail.RoleId ;
                        customer.CreatedRname = userDetail.RoleName ;
                        string custCode;
                        int customerID = await registrationService.CustomerRegistrationAsync(customer, out custCode);
                        if (customerID > 0)
                        {
                            customer.CustomerId = customerID;
                            customer.CustomerCode = custCode;
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
                        CreatedBy = userDetail.UserID,
                        CreatedRid = userDetail.RoleId,
                        CreatedRname = userDetail.RoleName

                    };

                    customer.CustomerLicense = new Model.CustomerLicenseActivation
                    {

                        ActivationCount = 1,
                        ActivationDate = DateTime.Now,
                        LastActivationDate = DateTime.Now.AddMonths(1),
                        CustomerID = customer.CustomerId,
                        PlanID = 2,
                        CreatedBy = userDetail.UserID,
                        CreatedRid = userDetail.RoleId,
                        CreatedRname = userDetail.RoleName
                    };
                    string dbPath = Path.Combine(_Environment.WebRootPath, "DB/DefaultDbScript.sql");
                    var res = await registrationService.CustomerPaymentAsync(customer, dbPath);

                    return Json(new { success = res, bType = ButtonType });
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
