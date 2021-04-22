using CSRPulse.Model;
using CSRPulse.Model.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services.Admin.Plan
{
    public interface IPlanService
    {
        Task<int> AddNewPlanAsync(PlanModel planModel);
        Task<List<PlanModel>> GetAllPlanAsync();
        Task<PlanModel> GetPlanByIdAsync();
        Task<int> AddNewUserTypeAsync(UserTypeModel planModel); 
    }
}
