using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IActivityService
    {
        Task<Activity> CreateActivityAsync(Activity activity);
        Task<List<Activity>> GetActivityAsync();
        Task<List<Activity>> GetActivityAsync(int themeId);
        Task<Activity> GetActivityByIdAsync(int activityId);
        Task<bool> UpdateActivityAsync(Activity activity);
        bool ActiveDeActive(int id, bool isActive);
    }
}