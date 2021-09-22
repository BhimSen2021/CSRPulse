using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
namespace CSRPulse.Services.IServices
{
    public interface IRegistrationService
    {
        Task<int> CustomerRegistrationAsync(Model.Customer customer, out string customerCode);

        Task<bool> CustomerPaymentAsync(Model.Customer customer, string dbPath);
        Task<bool> CustomerExists(Model.Customer customer);     
        bool SendOTP(string email, string OTP, string companyName);

        Task<int> RegistrationAsync(Model.SignUp signUp);
        Task<SignUp> GetUserById(int userId);
        Task<bool> UpdateUser(SignUp signUp);
        bool UserActiveDeActive(int userId, bool IsActive);
        Task<List<Model.UserRole>> GetAssignedRoles(int userId);
        Task<List<Model.UserRole>> GetUserRoles(int userId);
        Task<bool> AssignedRoles(List<Model.UserRole> userRoles, int userId);
        Task<bool> LockUnlockUser(int userId, bool ulock);

    }
}
