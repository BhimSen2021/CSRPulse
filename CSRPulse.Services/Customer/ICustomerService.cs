using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomerAsync();
        Task<Customer> GetCustomerDetailsAsync(int customerId);
        Task<Customer> GetCustomerByIdAsync();
    }
}
