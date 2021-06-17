using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface ISubActivityService
    {
        bool ActiveDeActive(int id, bool isActive);
        Task<SubActivity> CreateSubActivityAsync(SubActivity subActivity);
        Task<List<SubActivity>> GetSubActivityAsync(int themeId, int activityId);
        Task<List<SubActivity>> GetSubActivityAsync();
        Task<SubActivity> GetSubActivityIdAsync(int subActivityId);
        Task<bool> UpdateSubActivityAsync(SubActivity activity);
       
    }
}