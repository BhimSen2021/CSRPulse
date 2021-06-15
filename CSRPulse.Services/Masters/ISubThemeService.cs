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
        Task<bool> UpdateSubThemeAsync(SubTheme theme);
    }
}