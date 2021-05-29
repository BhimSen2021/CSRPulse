using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IAccountService
    {        
        bool AuthenticateUser(SingIn singIn, out UserDetail userDetail);
        bool AuthenticateCustomer(CustomerSignIn singIn, out string outPutValue, out int? customerID, out string companyName);

        Task<List<User>> GetUserAsync();
        Task<User> GetUserByIdAsync(int userId);


    }
}
