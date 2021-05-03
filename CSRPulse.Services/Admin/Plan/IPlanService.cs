using CSRPulse.Model;
using CSRPulse.Model.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services.Admin
{
    public interface IPlanService
    {
        Task<int> AddNewPlanAsync(Plan plan);
        Task<List<Plan>> GetAllPlanAsync();
        Task<Plan> GetPlanByIdAsync();
    }
}
