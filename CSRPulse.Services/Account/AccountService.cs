using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
              userDetail =  _mapper.Map<UserDetail>(uDetail);

                var uRight = _genericRepository.GetFirstOrDefault<DTOModel.UserRights>(r => r.UserId == uDetail.UserId);

                if (uRight != null)
                {
                   //userDetail.userMenuRights =  _mapper.Map<List<UserRight>>(uRight);
                }
            }

            if (userDetail.UserID > 0) { return true; }

            else { return false; }
        }
    }
}
