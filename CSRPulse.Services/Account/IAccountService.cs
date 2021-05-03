using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IAccountService
    {
        Task<int> CreateUserAsync(User user);
        Task<int> CreateCustomerAsync(Customer user);
        bool AuthenticateUser(SingIn singIn, out UserDetail userDetail);
      
    }
}
