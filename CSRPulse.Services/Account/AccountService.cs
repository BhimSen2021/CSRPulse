﻿using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CSRPulse.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IEmailService _emailService;
        public AccountService(IMapper mapper, IGenericRepository genericRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _emailService = emailService;
        }

        public bool AuthenticateUser(SingIn singIn, out UserDetail userDetail)
        {
            userDetail = new UserDetail();
            //SetConnectionString(null); // to reset connection.

            var uData = _genericRepository.GetIQueryable<DTOModel.User>(u => u.IsDeleted == false && u.IsActive == true && u.UserName.ToLower() == singIn.UserName && u.Password.ToLower() == singIn.Password).Include(r => r.Role).FirstOrDefault();

            if (uData == null)
            {
                var uInnerData = _genericRepository.GetIQueryable<DTOModel.User>(u => u.IsDeleted == false && u.IsActive == true && u.UserName.ToLower() == singIn.UserName).FirstOrDefault();
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
                else
                {
                    userDetail.ErrorMessage = "notexists";
                }
                return false;
            }
            if (uData != null)
            {
                if (uData.WrongAttemp == 0 && uData.LockDate.HasValue)
                {
                    userDetail.ErrorMessage = uData.LockDate.Value.AddDays(1).ToString("dd-MM-yyyy hh:mm tt");
                    return false;
                }
                else if (uData.WrongAttemp == 0)
                {
                    return false;
                }
                userDetail = _mapper.Map<UserDetail>(uData);

                userDetail.RoleId = uData.Role.RoleId;
                userDetail.RoleName = uData.Role.RoleName;

                var uRights = _genericRepository.GetIQueryable<DTOModel.UserRights>(u => u.UserId == uData.UserId).Include(r => r.Menu).ToList();

                if (uRights != null && uRights.Count > 0)
                {
                    userDetail.userMenuRights = uRights.Where(r => r.ShowMenu == true).Select(uRigth => new UserRight()
                    {
                        UserId = uRigth.UserId,
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

        public bool UserExists(string username, string password)
        {
            return _genericRepository.Exists<DTOModel.User>(x => x.UserName == username && (!string.IsNullOrEmpty(password) ? x.Password == password : (1 > 0)));
        }
        /// <summary>
        /// To send mail regarding password forgot
        /// </summary>
        /// <param name="forgotPassword">hold mail related data</param>
        /// <param name="type"> 1 is for otp, 2 is for new password </param>
        /// <returns></returns>
        public bool SendOTP(ForgotPassword forgotPassword, int type)
        {
            bool flag = false;
            try
            {
                var custEmail = _genericRepository.GetIQueryable<DTOModel.User>(x => x.UserName == forgotPassword.UserName && x.IsActive == true).FirstOrDefault();
                if (custEmail == null)
                {
                    return false;
                }
                StringBuilder emailBody = new StringBuilder("");
                Common.EmailMessage message = new Common.EmailMessage();
                message.To = custEmail.EmailId;
                var mailSubj = _genericRepository.Get<DTOModel.MailSubject>(x => x.MailProcessId == 2).FirstOrDefault();
                if (mailSubj != null)
                {
                    message.Subject = mailSubj.Subject;
                    message.SubjectId = mailSubj.SubjectId;
                }
                else
                    message.Subject = "CSRPulse Mail";


                message.TemplateName = type == 1 ? "ForgotOTP" : "RecoverPassword";

                message.PlaceHolders = new List<KeyValuePair<string, string>>();

                if (type == 1)
                {
                    message.PlaceHolders.Add(new KeyValuePair<string, string>("{$otp}", forgotPassword.OTP));
                    message.PlaceHolders.Add(new KeyValuePair<string, string>("{$custName}", custEmail.FullName));
                }
                else if (type == 2)
                {
                    message.PlaceHolders.Add(new KeyValuePair<string, string>("{$password}", forgotPassword.Password));
                    message.PlaceHolders.Add(new KeyValuePair<string, string>("{$custName}", custEmail.FullName));
                    message.PlaceHolders.Add(new KeyValuePair<string, string>("{$user}", custEmail.UserName));
                }
                _emailService.CustomerRelatedMails(message);
                flag = true;
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
                var result = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.User>().Include(d => d.Department));
               
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

        public async Task<bool> UpdatePassword(string custCode, string password)
        {
            bool flag;
            try
            {
                var custData = _genericRepository.GetIQueryable<DTOModel.User>(x => x.UserName == custCode).FirstOrDefault();
                if (custData != null)
                {
                    custData.Password = password;
                    _genericRepository.Update(custData);
                }
                ForgotPassword forgotPassword = new ForgotPassword
                {
                    Password = password,
                    UserName = custCode
                };
                // 2 is for Password changed confirmation mail, it will used in send function to identity mail type.
                flag = await Task.FromResult(SendOTP(forgotPassword, 2));

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

    }
}
