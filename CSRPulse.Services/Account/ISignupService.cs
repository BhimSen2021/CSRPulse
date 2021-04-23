using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface ISignupService
    {
        Task<int> CreateUserAsync(User user);
        Task<int> CreateCustomerAsync(Customer user);
    }
}
