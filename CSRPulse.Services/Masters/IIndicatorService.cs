using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IIndicatorService
    {
        bool ActiveDeActive(int id, bool isActive);
        Task<Indicator> CreateIndicatorAsync(Indicator indicator);
        Task<List<Indicator>> GetIndicatorAsync();
        Task<List<Indicator>> GetIndicatorAsync(Indicator indicator);
        Task<Indicator> GetIndicatorIdAsync(int indicatorId);
        Task<bool> UpdateIndicatorAsync(Indicator activity);
    }
}