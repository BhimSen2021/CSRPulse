using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IDesignationHistoryService
    {
        Task<bool> CreateDesignation(DesignationHistory designationHistory);
        Task<DesignationHistory> GetDesignationHistoryById(int designationHistoryId);
        Task<List<DesignationHistory>> GetDesignationHistoryByUserId(int userId);
        Task<bool> UpdateTodatePrevious(DesignationHistory designationHistory);

        Task<bool> RevertDesignation(int hId, int uId);
    }
}