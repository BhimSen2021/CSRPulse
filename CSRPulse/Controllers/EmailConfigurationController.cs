﻿using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    [Route("[Controller]/[action]")]
    public class EmailConfigurationController : BaseController<EmailConfigurationController>
    {
        private readonly IEmailConfigurationServices _emailconfigServices;

        public EmailConfigurationController(IEmailConfigurationServices emailconfigServices)
        {
            _emailconfigServices = emailconfigServices;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _emailconfigServices.GetEmailConfigAsync();
            return View(result);

        }
        public async Task<IActionResult> Edit()
        {
            try
            {
                var uDetail = await _emailconfigServices.GetEmailConfigAsync();
                return View(uDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(EmailConfiguration email)
        {
            try
            {
                _logger.LogInformation("EmailConfigurationController/Edit");
                if (ModelState.IsValid)
                {
                    email.UpdatedBy = userDetail.UserID;
                    email.UpdatedRid = userDetail.RoleId;
                    email.UpdatedRname = userDetail.RoleName;
                    email.UpdatedOn = DateTime.Now;
                    var result = await _emailconfigServices.UpdateEmailConfig(email);
                    if (result)
                    {
                        TempData["Message"] = "Email Configuration Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Email Configuration Updation Failed.";
                    }
                }
                return View(email);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }


    }
}
