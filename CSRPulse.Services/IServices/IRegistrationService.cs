using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTOModel= CSRPulse.Data.Models;
namespace CSRPulse.Services.IServices
{
    public interface IRegistrationService
    {
        Task<int> CustomerRegistrationAsync(Model.Customer customer);

        Task<bool> CustomerPaymentAsync(Model.Customer customer);
    }
}
