using CSRPulse.Model;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IPlanService
    {
        Task<int> AddNewPlanAsync(Plan plan);
        Task<List<Plan>> GetAllPlanAsync();
        Task<Model.Plan> GetPlanByIdAsync(int planID);
    }
}
