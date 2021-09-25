using CSRPulse.Common;
using CSRPulse.Controllers;
using CSRPulse.Model;
using CSRPulse.Models;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class RegistrationController : BaseController<RegistrationController>
    {
        private readonly IRegistrationService _registrationService;
        private readonly IDesignationHistoryService _designationHistory;
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDropdownBindService _ddlService;

        public RegistrationController(IRegistrationService registrationService, IAccountService accountService, IWebHostEnvironment webHostEnvironment, IDropdownBindService dropdownBindService, IDesignationHistoryService designationHistory, IEmailService emailService) : base()
        {
            _registrationService = registrationService;
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
            _ddlService = dropdownBindService;
            _designationHistory = designationHistory;
            _emailService = emailService;
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


                if (string.IsNullOrEmpty(singUp.hdConfirmPassword))
                    ModelState.AddModelError("ConfirmPassword", "Please enter confirm password.");
                if (string.IsNullOrEmpty(singUp.hdPassword))
                    ModelState.AddModelError("Password", "Please enter password.");

                singUp.Password = Password.DecryptStringAES(singUp.hdPassword);
                singUp.ConfirmPassword = Password.DecryptStringAES(singUp.hdConfirmPassword);
                string ErrorMessage = string.Empty;


                if (!Password.ValidatePassword(singUp.Password, out ErrorMessage))
                {
                    ModelState.AddModelError("Password", ErrorMessage);
                }

                if (ModelState.IsValid)
                {
                    if (singUp.ImagePhoto != null)
                    {
                        string imagePath = DocumentUploadFilePath.UserProfileImagePath;
                        singUp.ImageName = await UploadImage(imagePath, singUp.ImagePhoto);
                    }
                    else
                        singUp.ImageName = "sample-profile.png";

                    singUp.CreatedBy = userDetail.UserID;
                    singUp.CreatedOn = DateTime.Now;
                    singUp.CreatedRid = userDetail.RoleId;
                    singUp.CreatedRname = userDetail.RoleName;

                    singUp.IsActive = true;
                    string decryptPassword = string.Empty;
                    decryptPassword = singUp.Password.Trim();

                    singUp.Password = Password.CreatePasswordHash(singUp.Password.Trim(), Password.CreateSalt(Password.Password_Salt));

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
                            CreatedBy = userDetail.UserID,
                            CreatedRid = userDetail.RoleId,
                            CreatedRname = userDetail.RoleName
                        };
                        await _designationHistory.CreateDesignation(uHistory);

                        bool IsSend = await _emailService.SendMail(new MailDetail() { To = singUp.EmailId, FullName = singUp.FullName, UserName = singUp.UserName, Password = decryptPassword }, MailProcess.Registration);
                        if (IsSend)
                            TempData["Message"] = "User Registred Successfully.";
                        else
                            TempData["Message"] = "User Registred Successfully, But email sending failed.";

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
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
                if (ModelState.IsValid)
                {
                    if (signUp.ImagePhoto != null)
                    {
                        string imagePath = DocumentUploadFilePath.UserProfileImagePath;
                        if (ValidateFileMimeType(signUp.ImagePhoto))
                        {
                            signUp.ImageName = await UploadImage(imagePath, signUp.ImagePhoto);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid user image format");
                            BindDropdowns();
                            return View(signUp);
                        }
                    }

                    signUp.UpdatedBy = userDetail.UserID;
                    signUp.UpdatedOn = DateTime.Now;
                    signUp.UpdatedRid = userDetail.RoleId;
                    signUp.UpdatedRname = userDetail.RoleName;
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
                                CreatedBy = userDetail.UserID,
                                CreatedRid = userDetail.RoleId,
                                CreatedRname = userDetail.RoleName,
                                UpdatedRid = userDetail.RoleId,
                                UpdatedRname = userDetail.RoleName,
                                UpdatedBy = userDetail.UserID,
                                UpdatedOn = DateTime.Now
                            };
                            await _designationHistory.UpdateTodatePrevious(uHistory);

                            uHistory.UpdatedRid = null;
                            uHistory.UpdatedRname = null;
                            uHistory.UpdatedBy = null;
                            uHistory.UpdatedOn = null;

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

            var uploadedImagePath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            return await UploadFile(uploadedImagePath, file);
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

        public async Task<IActionResult> AssignRole(int userId)
        {
            try
            {
                UserRoleModel userRoleModel = new UserRoleModel();
                userRoleModel.userRoles = new List<Model.UserRole>();

                var userRoles = await _registrationService.GetAssignedRoles(userId);
                userRoleModel.SelectedRole = 0;
                userRoleModel.UserId = userId;
                userRoleModel.userRoles = userRoles;

                return Json(new { htmlData = ConvertViewToString("_AssignRole", userRoleModel, true) });
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(UserRoleModel userRoleModel)
        {
            try
            {
                bool flag = false;
                var roles = userRoleModel.userRoles.Where(x => x.AssigneRole == true).ToList();
                var baseModel = new BaseModel();
                baseModel.CreatedBy = userDetail.UserID;
                baseModel.CreatedRid = userDetail.RoleId;
                baseModel.CreatedRname = userDetail.RoleName;

                flag = await _registrationService.AssignedRoles(roles, userRoleModel.UserId, baseModel);
                return Json(new { flag = flag });
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> SwitchAccount()
        {
            try
            {
                UserRoleModel userRoleModel = new UserRoleModel();
                userRoleModel.userRoles = new List<Model.UserRole>();

                var userRoles = await _registrationService.GetUserRoles(userDetail.UserID);
                if (userRoles.Exists(x => x.RoleId == userDetail.RoleId))
                {
                    userRoles.Where(r => r.RoleId == userDetail.RoleId).ToList().ForEach(i => i.AssigneRole = true);
                }
                else
                {
                    userRoles.Add(new Model.UserRole()
                    { RoleId = userDetail.RoleId, RoleName = userDetail.RoleName, AssigneRole = true });
                }
                userRoleModel.SelectedRole = 0;
                userRoleModel.UserId = userDetail.UserID;
                userRoleModel.userRoles = userRoles.OrderByDescending(x => x.AssigneRole).ToList();

                return Json(new { htmlData = ConvertViewToString("_SwitchAccount", userRoleModel, true) });
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        public IActionResult SwitchAccount(UserRoleModel userRoleModel)
        {
            try
            {
                var uDetail = new UserDetail();

                if (HttpContext.Session.GetComplexData<UserDetail>("User") != null)
                {
                    var RoleName = userRoleModel.userRoles.Where(r => r.RoleId == userRoleModel.SelectedRole).Select(x => x.RoleName).FirstOrDefault();

                    uDetail = HttpContext.Session.GetComplexData<UserDetail>("User");
                    uDetail.RoleId = userRoleModel.SelectedRole;
                    uDetail.RoleName = RoleName;
                    HttpContext.Session.SetComplexData("User", uDetail);
                    TempData["message"] = "Account switched to " + RoleName + " successfully.";
                    return Json(new { flag = 1 });
                }
                else
                    return Json(new { flag = 2 });
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                throw;
            }
        }

        public async Task<IActionResult> LockUnlockUser(int userId, bool ulock)
        {
            bool flag = false;
            try
            {
                flag = await _registrationService.LockUnlockUser(userId, ulock);
                if (flag)
                    TempData["message"] = "User " + (ulock == true ? "locked successfully." : "unlocked successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
            return Json(new { flag = flag });

        }
    }
}
