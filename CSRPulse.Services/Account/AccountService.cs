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

namespace CSRPulse.Services
{
    public class AccountService : BaseService, IAccountService
    {


        public AccountService(IMapper mapper, IGenericRepository genericRepository) : base(mapper, genericRepository)
        {
        }

        public async Task<int> CreateUserAsync(User user)
        {
            try
            {
                var userDTO = _mapper.Map<DTOModel.User>(user);
                return await _genericRepository.InsertAsync(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> CreateCustomerAsync(Customer user)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(SingIn singIn, out UserDetail userDetail)
        {
            userDetail = new UserDetail();

            var uDetail = _genericRepository.GetFirstOrDefault<DTOModel.User>(u => u.IsDeleted == false && u.IsActive == true && u.UserName.ToLower() == singIn.UserName && u.Password.ToLower() == singIn.Password);

            if (uDetail == null)
            {
                return false;
            }
            else
            {
                userDetail = _mapper.Map<UserDetail>(uDetail);

                var uRight = _genericRepository.GetFirstOrDefault<DTOModel.UserRights>(r => r.UserId == uDetail.UserId);

                if (uRight != null)
                {
                    userDetail.userMenuRights = _mapper.Map<List<UserRight>>(uRight);
                }
            }

            if (userDetail.UserID > 0) { return true; }

            else { return false; }
        }

        public bool AuthenticateCustomer(CustomerSignIn singIn, out string outPutValue, out int ? customerID)
        {
            try
            {
                outPutValue = string.Empty;
                customerID = null;
                bool flag = false;
                var validateCustomer = _genericRepository.GetIQueryable<DTOModel.Customer>(c => c.CustomerCode == singIn.CompanyID).Include(p => p.CustomerPayment).Include(y => y.CustomerLicenseActivation);
                if (validateCustomer.FirstOrDefault() != null)
                {
                    customerID = validateCustomer.FirstOrDefault().CustomerId;
                    if (validateCustomer.FirstOrDefault().CustomerPayment != null && validateCustomer.FirstOrDefault().CustomerPayment.Count>0)
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
                    flag= false;
                }

                if (flag)
                {
                    //var database = validateCustomer.FirstOrDefault().DataBaseName;
                   // SetConnectionString(ref database);
                }
                return flag;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

 
    }
}
