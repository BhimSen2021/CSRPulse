using CSRPulse.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<CustomerLicenseActivation>> GetCustomerCurrentPlan();
    }
}
