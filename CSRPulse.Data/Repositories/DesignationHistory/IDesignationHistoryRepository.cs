using CSRPulse.Data.Models;
using System.Threading.Tasks;

namespace CSRPulse.Data.Repositories
{
    public interface IDesignationHistoryRepository
    {
        Task<DesignationHistory> GetLastDesignationHistory(int userId);
    }
}