using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface ISubThemeService
    {
        Task<SubTheme> CreateSubThemeAsync(SubTheme theme);
        Task<SubTheme> GetSubThemeByIdAsync(int SubThemeId);
        Task<List<SubTheme>> GetSubThemesAsync();
        Task<List<SubTheme>> GetSubThemesAsync(SubTheme theme);
        Task<bool> UpdateSubThemeAsync(SubTheme theme);
        bool ActiveDeActive(int id, bool IsActive);
    }
}