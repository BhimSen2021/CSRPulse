using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IThemeService
    {
        Task<Theme> CreateThemeAsync(Theme theme);
        Task<Theme> GetThemeByIdAsync(int ThemeId);
        Task<List<Theme>> GetThemesAsync();
        Task<bool> UpdateThemeAsync(Theme theme);
    }
}