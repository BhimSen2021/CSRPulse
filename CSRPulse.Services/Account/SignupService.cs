using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services.Account
{
    public class SignupService : BaseService, ISignupService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public SignupService(IGenericRepository genericRepository, IMapper mapper) : base(mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<int> CreateCustomerAsync(Customer user)
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

        public Task<int> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
