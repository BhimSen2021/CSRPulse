using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Common;

namespace CSRPulse.Services
{
    public interface IAccountService
    {        
        bool AuthenticateUser(SingIn singIn, out UserDetail userDetail);
        bool AuthenticateCustomer(CustomerSignIn singIn, out string outPutValue, out int? customerID, out string companyName);
        bool UserExists(string username, string emailId, string password);
        Task<bool> SendOTP(ForgotPassword forgotPassword, MailProcess mailProcess);
        Task<List<User>> GetUserAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> UpdatePassword(string custCode, string password);
        bool ValidatePassword(ChangePassword changePassword, out string errorMessage);
        Task<bool> ChangePassword(ChangePassword changePassword);
        UserDetail GetUserProfileByIdAsync(int userId);
        List<UserDetail> GetUserProfileAsync();
        bool CreateUser(Model.User user);
        bool UpdateUser(User user);
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         