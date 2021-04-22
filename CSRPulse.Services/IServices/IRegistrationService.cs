using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services.IServices
{
    public interface IRegistrationService
    {
        Task<string> CustomerRegistrationAsync(Model.Customer customer);
    }
}
