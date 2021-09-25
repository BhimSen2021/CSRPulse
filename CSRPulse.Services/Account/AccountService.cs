using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CSRPulse.Common;

namespace CSRPulse.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IEmailService _emailService;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IMapper mapper, IGenericRepository genericRepository, IEmailService emailService, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _emailService = emailService;
            _accountRepository = accountRepository;
        }

        public bool AuthenticateUser(SingIn singIn, out UserDetail userDetail)
        {
            string dbPassword = string.Empty;
            bool userExits = false;
            userDetail = new UserDetail();
            userExits = _accountRepository.VerifyUser(singIn.UserName, out dbPassword);
            if (!userExits)
            {
                userDetail.ErrorMessage = "notexists";
                return false;
            }
            bool isMatch = Password.VerifyPassword(singIn.Password, dbPassword, Password.Password_Salt);

            if (isMatch)
            {
                var uData = _genericRepository.GetIQueryable<DTOModel.User>(u => u.IsDeleted == false && u.IsActive == true && u.UserName == singIn.UserName).Include(r => r.Role).FirstOrDefault();
                if (uData != null)
                {
                    var LockDate = uData.LockDate ?? DateTime.Now;                    
                    if (uData.WrongAttemp == 0 && LockDate.AddHours(24) > DateTime.Now)
                    {
                        userDetail.ErrorMessage = uData.LockDate.Value.AddDays(1).ToString("dd-MM-yyyy hh:mm tt");
                        return false;
                    }
                    
                    userDetail = _mapper.Map<UserDetail>(uData);

                    userDetail.RoleId = uData.Role.RoleId;
                    userDetail.RoleName = uData.Role.RoleName;

                    var uRights = _genericRepository.GetIQueryable<DTOModel.UserRights>(u => u.RoleId == uData.RoleId).Include(r => r.Menu).ToList();

                    if (uRights != null && uRights.Count > 0)
                    {
                        userDetail.userMenuRights = uRights.Where(r => r.ShowMenu == true).Select(uRigth => new UserRight()
                        {
                            RoleId = uRigth.RoleId,
                            MenuId = uRigth.MenuId,
                            ShowMenu = uRigth.ShowMenu,
                            CreateRight = uRigth.CreateRight,
                            EditRight = uRigth.EditRight,
                            ViewRight = uRigth.ViewRight,
                            DeleteRight = uRigth.DeleteRight,

                            menu = new Menu()
                            {
                                ParentMenuId = uRigth.Menu.ParentMenuId,
                                MenuId = uRigth.Menu.MenuId,
                                Area = uRigth.Menu.Area,
                                MenuName = uRigth.Menu.MenuName,
                                Url = uRigth.Menu.Url == null ? uRigth.Menu.Url : uRigth.Menu.Url.ToLower(),
                                SequenceNo = uRigth.Menu.SequenceNo,
                            }

                        }).ToList();
                    }
                    uData.WrongAttemp = null;
                    _genericRepository.Update(uData);
                }
            }
            else
            {
                var uInnerData = _genericRepository.GetIQueryable<DTOModel.User>(u => u.IsDeleted == false && u.IsActive == true && u.UserName == singIn.UserName).FirstOrDefault();
                if (uInnerData != null)
                {
                    if (uInnerData.WrongAttemp == 0 && uInnerData.LockDate.HasValue)
                    {
                        userDetail.ErrorMessage = uInnerData.LockDate.Value.AddDays(1).ToString("dd-MM-yyyy hh:mm tt");
                        return false;
                    }
                    switch (uInnerData.WrongAttemp)
                    {
                        case 0:
                            userDetail.WrongAttemp = 0;
                            break;
                        case 2:
                            userDetail.WrongAttemp = 1;
                            break;
                        case 1:
                            userDetail.WrongAttemp = 0;
                            break;
                        default:
                            userDetail.WrongAttemp = 2;
                            break;
                    }
                    if (userDetail.WrongAttemp.HasValue)
                    {
                        uInnerData.WrongAttemp = userDetail.WrongAttemp;
                        uInnerData.LockDate = uInnerData.RoleId == (int)Common.Roles.Admin && userDetail.WrongAttemp == 0 ? DateTime.Now : uInnerData.LockDate;
                        _genericRepository.Update(uInnerData);
                    }
                }
                else { userDetail.ErrorMessage = "notexists"; }
                return false;
            }

            if (userDetail.UserID > 0) { return true; }
            else { return false; }

        }

        public bool AuthenticateCustomer(CustomerSignIn singIn, out string outPutValue, out int? customerID, out string companyName)
        {
            try
            {
                SetConnectionString(null);
                outPutValue = string.Empty;
                customerID = null;
                bool flag = false;
                companyName = string.Empty;

                var validateCustomer = _genericRepository.GetIQueryable<DTOModel.Customer>(c => c.CustomerUniqueId == singIn.CompanyID).Include(p => p.CustomerPayment).Include(y => y.CustomerLicenseActivation);
                if (validateCustomer.FirstOrDefault() != null)
                {
                    customerID = validateCustomer.FirstOrDefault().CustomerId;
                    companyName = validateCustomer.FirstOrDefault().CustomerName;
                    if (validateCustomer.FirstOrDefault().CustomerPayment != null && validateCustomer.FirstOrDefault().CustomerPayment.Count > 0)
                    {
                        var custPayment = validateCustomer.FirstOrDefault().CustomerPayment.OrderByDescending(o => o.PaymentId).FirstOrDefault();
                        if (custPayment != null)
                        {
                            if (custPayment.IsSuccess)
                            {
                                var custAct = validateCustomer.FirstOrDefault().CustomerLicenseActivation.Where(x => x.PaymentId == custPayment.PaymentId).FirstOrDefault();
                                if (custAct != null)
                                {
                                    if (DateTime.Now.Date.AddDays(3) == custAct.LastActivationDate.Date)
                                    {
                                        outPutValue = "3daysexp";
                                        flag = true;
                                    }
                                    else if (DateTime.Now.Date.AddDays(2) == custAct.LastActivationDate.Date)
                                    {
                                        outPutValue = "2daysexp";
                                        flag = true;
                                    }
                                    else if (DateTime.Now.Date.AddDays(1) == custAct.LastActivationDate.Date)
                                    {
                                        outPutValue = "1dayexp";
                                        flag = true;
                                    }
                                    else if (DateTime.Now.Date == custAct.LastActivationDate.Date)
                                    {
                                        outPutValue = "0exp";
                                        flag = true;
                                    }
                                    else if (DateTime.Now.Date > custAct.LastActivationDate.Date)
                                    {
                                        outPutValue = "expired";
                                        flag = false;
                                    }
                                    else
                                        flag = true;
                                }
                                else
                                {
                                    outPutValue = "lincexpired";
                                    flag = false;
                                }
                            }
                            else
                            {
                                outPutValue = "nopayment";
                                flag = false;
                            }
                        }
                    }
                    else
                    {
                        outPutValue = "nopayment";
                        flag = false;
                    }
                }
                else
                {
                    outPutValue = "notexists";
                    flag = false;
                }

                if (flag)
                {
                    var database = validateCustomer.FirstOrDefault().DataBaseName;
                    SetConnectionString(database);
                }
                return flag;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UserExists(string username, string emailId, string password)
        {
            return _genericRepository.Exists<DTOModel.User>(x => (!string.IsNullOrEmpty(username) ? x.UserName == username : (1 > 0)) && (!string.IsNullOrEmpty(emailId) ? x.EmailId == emailId : (1 > 0)) && (!string.IsNullOrEmpty(password) ? x.Password == password : (1 > 0)));
        }

        /// <summary>
        /// To send mail regarding password forgot
        /// </summary>
        /// <param name="forgotPassword">hold mail related data</param>
        /// <param name="type"> 1 is for otp, 2 is for new password </param>
        /// <returns></returns>
        public async Task<bool> SendOTP(ForgotPassword forgotPassword, MailProcess mailProcess)
        {
            bool flag = false;
            try
            {
                var udata = _genericRepository.GetIQueryable<DTOModel.User>(x => x.EmailId == forgotPassword.EmailId && x.IsActive == true).FirstOrDefault();
                if (udata == null)
                {
                    return false;
                }

                var mailDetail = new MailDetail()
                {
                    To = udata.EmailId,
                    OTP = forgotPassword.OTP,
                    FullName = udata.FullName,
                    EmailId = udata.EmailId,
                    Password = forgotPassword.Password,
                    UserName = udata.UserName
                };

                flag = await _emailService.SendMail(mailDetail, mailProcess);
            }
            catch (Exception)
            {
                throw;
            }

            return flag;
        }


        public async Task<List<User>> GetUserAsync()
        {
            try
            {
                var result = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.User>().Include(d => d.Department).Include(r => r.UserRole));
                

                return _mapper.Map<List<User>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var result = await _genericRepository.GetByIDAsync<DTOModel.User>(userId);
            try
            {
                return _mapper.Map<User>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePassword(string emailId, string password)
        {
            bool flag;
            try
            {
                var uData = _genericRepository.GetIQueryable<DTOModel.User>(x => x.EmailId == emailId).FirstOrDefault();
                if (uData != null)
                {
                    uData.Password = Password.CreatePasswordHash(password.Trim(), Password.CreateSalt(Password.Password_Salt));
                    _genericRepository.Update(uData);
                }
                ForgotPassword forgotPassword = new ForgotPassword
                {
                    Password = password,
                    EmailId = emailId
                };
                // 2 is for Password changed confirmation mail, it will used in send function to identity mail type.
                flag = await SendOTP(forgotPassword, MailProcess.ResetPassword);
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public List<UserDetail> GetUserProfileAsync()
        {
            var result = _genericRepository.GetIQueryable<DTOModel.User>().Include(r => r.Role);
            try
            {
                return _mapper.Map<List<UserDetail>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserDetail GetUserProfileByIdAsync(int userId)
        {
            var result = _genericRepository.GetIQueryable<DTOModel.User>(x => x.UserId == userId).Include(r => r.Role).FirstOrDefault();
            try
            {
                return _mapper.Map<UserDetail>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CreateUser(Model.User user)
        {
            try
            {
                var dtoUser = _mapper.Map<DTOModel.User>(user);
                _genericRepository.Insert(dtoUser);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        public bool UpdateUser(Model.User user)
        {
            try
            {
                var userDtl = _genericRepository.GetByID<DTOModel.User>(user.UserID);
                if (userDtl != null)
                {
                    userDtl.RoleId = user.RoleId;
                    userDtl.FullName = user.FullName;
                    userDtl.MobileNo = user.MobileNo;
                    userDtl.EmailId = user.EmailID;
                    userDtl.ImageName = user.ImageName;
                    userDtl.Password = user.Password == userDtl.Password ? userDtl.Password : user.Password;
                    _genericRepository.Update(userDtl);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }



        public bool ValidatePassword(ChangePassword changePassword, out string errorMessage)
        {
            string dbPassword = string.Empty;
            errorMessage = string.Empty;

            if (!_accountRepository.VerifyUser(changePassword.UserName, out dbPassword))
            {
                errorMessage = "User details not found.";
                return false;
            }
            if (Password.VerifyPassword(changePassword.OldPassword, dbPassword, Password.Password_Salt)) { return true; }

            else
            {
                errorMessage = "Invalid old password, Please enter correct old password.";
                return false;
            }
        }
        public async Task<bool> ChangePassword(ChangePassword changePassword)
        {
            bool flag = false;
            try
            {
                var uData = _genericRepository.GetIQueryable<DTOModel.User>(u => u.IsDeleted == false && u.IsActive == true && u.UserName == changePassword.UserName).FirstOrDefault();
                if (uData != null)
                {
                    uData.Password = Password.CreatePasswordHash(changePassword.Password.Trim(), Password.CreateSalt(Password.Password_Salt));
                    uData.UpdatedBy = changePassword.UserId;                    
                    uData.UpdatedOn = DateTime.UtcNow;
                    await _genericRepository.UpdateAsync(uData);
                    flag = true;
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
    }
}
