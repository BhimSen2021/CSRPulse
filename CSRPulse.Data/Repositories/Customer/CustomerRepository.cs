using CSRPulse.Data.Data;
using CSRPulse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Data.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {

        public Task<List<CustomerLicenseActivation>> GetCustomerCurrentPlan()
        {

            var max = _dbContext.CustomerLicenseActivation.AsEnumerable().GroupBy(x => new { x.CustomerId, x.PlanId, x.IsTrail }).Select(g => new
            {
                CustomerId = g.Key.CustomerId,
                PlanId = g.Key.PlanId,
                IsTrail = g.Key.IsTrail,
            });


            var query = _dbContext.CustomerLicenseActivation.GroupBy(x => new { x.CustomerId, x.PlanId, x.IsTrail },
                        (key, group) => new
                        {
                            CustomerId = group.First().CustomerId,
                            PlanId = group.First().PlanId,
                            IsTrail = group.First().IsTrail,
                            LicenceActId = group.Select(m => m.LicenceActId).Max()
                        })
                    .ToList();
            
            return null;
        }
    }
}
