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
        string GenerateOTP();
        bool SendOTP(string email, string OTP,string companyName);
    }
}
