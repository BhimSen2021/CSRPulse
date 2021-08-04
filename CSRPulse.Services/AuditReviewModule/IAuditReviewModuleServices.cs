using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IAuditReviewModuleServices
    {
        bool ActiveDeActive(int id, bool IsActive);
        Task<bool> CreateModuleAsync(AuditReviewModule module);
        Task<List<AuditReviewModule>> GetModuleAsync(AuditReviewModule module);
        Task<AuditReviewModule> GetModuleByIdAsync(int moduleId);
        Task<bool> RecordExistAsync(AuditReviewModule module);
        Task<bool> UpdateModuleAsync(AuditReviewModule module);
        Task<int> Count();
    }
}