using CSRPulse.Model;
using CSRPulse.Services.IServices;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class RegistrationController : CSRPulse.Controllers.BaseController<RegistrationController>
    {
        private readonly IRegistrationService _registrationService;
        private readonly IDesignationHistoryService _designationHistory;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDropdownBindService _ddlService;

        public RegistrationController(IRegistrationService registrationService, IAccountService accountService, IWebHostEnvironment webHostEnvironment, IDropdownBindService dropdownBindService, IDesignationHistoryService designationHistory) : base()
        {
            _registrationService = registrationService;
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
            _ddlService = dropdownBindService;
            _designationHistory = designationHistory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Admin/RegistrationController/Index");
            try
            {
                var result = await _accountService.GetUserAsync();
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            BindDropdowns();
            return View(new SignUp());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SignUp singUp)
        {
            try
            {
                _logger.LogInformation("Admin/RegistrationController/Create");

                if (ModelState.IsValid)
                {
                    if (singUp.ImagePhoto != null)
                    {
                        string folder = "assets/images/users/";
                        singUp.ImageName = await UploadImage(folder, singUp.ImagePhoto);
                    }
                    else
                        singUp.ImageName = "sample-profile.png";

                    singUp.CreatedBy = userDetail.UserID;
                    singUp.CreatedOn = DateTime.Now;
                    singUp.IsActive = true;
                    var result = await _registrationService.RegistrationAsync(singUp);
                    if (singUp.RecordExist)
                    {
                        ModelState.AddModelError("", "User already exists");
                    }
                    if (result > 0)
                    {
                        var uHistory = new DesignationHistory()
                        {
                            UserId = result,
                            DesignationId = singUp.DesignationId,
                            Formdate = DateTime.Now,
                            Todate = null,
                            CreatedBy = userDetail.UserID
                        };
                        await _designationHistory.CreateDesignation(uHistory);

                        TempData["Message"] = "User Registred Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                BindDropdowns();
                return View(singUp);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int userId)
        {
            try
            {
                var hData = await _designationHistory.GetDesignationHistoryByUserId(userId);
                ViewData["history"] = hData;

                BindDropdowns();
                var uDetail = await _registrationService.GetUserById(userId);
                return View(uDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(SignUp signUp)
        {
            try
            {
                _logger.LogInformation("Admin/RegistrationController/Edit");
                ModelState.Remove("UserName");

                if (ModelState.IsValid)
                {
                    if (signUp.ImagePhoto != null)
                    {
                        string folder = "assets/images/users/";
                        signUp.ImageName = await UploadImage(folder, signUp.ImagePhoto);
                    }

                    signUp.UpdatedBy = userDetail.UserID;
                    signUp.UpdatedOn = DateTime.Now;
                    var result = await _registrationService.UpdateUser(signUp);
                    if (result)
                    {
                        if (signUp.OldDesignationId != signUp.DesignationId)
                        {

                            var uHistory = new DesignationHistory()
                            {
                                UserId = signUp.UserID,
                                DesignationId = signUp.DesignationId,
                                Formdate = DateTime.Now,
                                Todate = null,
                                CreatedBy = userDetail.UserID
                            };
                            await _designationHistory.UpdateTodatePrevious(uHistory);

                            await _designationHistory.CreateDesignation(uHistory);
                        }


                        TempData["Message"] = "User Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                BindDropdowns();
                return View(signUp);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        //[NonAction]
        public async Task<ActionResult> RevertDesignation(int hId, int uId)
        {
            var res = await _designationHistory.RevertDesignation(hId, uId);
            if (res)
                TempData["Message"] = "Designation Reverted Successfully.";
            else
                TempData["Error"] = "Designation Reverting failed.";

            return RedirectToAction("Edit", new { userId = uId });
        }

        [HttpPost]
        public JsonResult UserActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("Admin/RegistrationController/UserActiveDeActive");
            var result = _registrationService.UserActiveDeActive(id, isChecked);
            return Json(result);
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, folderPath)))
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, folderPath));


            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            folderPath += fileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return fileName;
        }

        [NonAction]
        void BindDropdowns()
        {
            var listDepartment = _ddlService.GetDepartments();
            ViewBag.ddlDepartment = new SelectList(listDepartment, "id", "value");

            var listRole = _ddlService.GetRole(null);
            ViewBag.ddlRole = new SelectList(listRole, "id", "value");

            var Designations = _ddlService.GetDesignations(null);
            ViewBag.ddlDesignation = new SelectList(Designations, "id", "value");

            var Partners = _ddlService.GetPartners(null);
            ViewBag.ddlPartner = new SelectList(Partners, "id", "value");
        }



    }
}
