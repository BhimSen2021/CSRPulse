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
        bool UserExists(string username, string password);
        bool SendOTP(ForgotPassword forgotPassword, int type);
        Task<List<User>> GetUserAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> UpdatePassword(string custCode, string password);
        UserDetail GetUserProfileByIdAsync(int userId);
        List<UserDetail> GetUserProfileAsync();
        bool CreateUser(Model.User user);
        bool UpdateUser(User user);
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         