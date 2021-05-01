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
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public AccountService(IGenericRepository genericRepository, IMapper mapper) : base(mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
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
                userDetail.UserID = uDetail.UserID;
                userDetail.UserName = uDetail.UserName;
                userDetail.EmailID = uDetail.EmailID;
                userDetail.CreatedOn = uDetail.CreatedOn;
                userDetail.CreatedBy = uDetail.CreatedBy;
                userDetail.IsDeleted = false;
            }
           
            if (uDetail == null)
            {
                return false;
            }

            if (userDetail.UserID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
