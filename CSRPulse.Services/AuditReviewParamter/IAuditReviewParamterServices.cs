using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IAuditReviewParamterServices
    {
        bool ActiveDeActive(int id, bool IsActive);
        Task<int> Count();
        Task<bool> CreateParamterAsync(AuditReviewParamter paramter);
        Task<List<AuditReviewParamter>> GetParamterAsync(AuditReviewParamter paramter);
        Task<AuditReviewParamter> GetParamterByIdAsync(int paramterId);
        Task<bool> RecordExistAsync(AuditReviewParamter paramter);
        Task<bool> UpdateParamterAsync(AuditReviewParamter paramter);
    }
}