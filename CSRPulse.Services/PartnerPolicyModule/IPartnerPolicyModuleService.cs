using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;

namespace CSRPulse.Services
{
    public interface IPartnerPolicyModuleService
    {
        Task<PartnerPolicyModule> CreatePartnerPolicyModuleAsync(PartnerPolicyModule partnnerpolicymodule);

        Task<PartnerPolicyModule> GetPartnerPolicyModuleByIdAsync(int PartnerPolicyModuleId);

        Task<List<PartnerPolicyModule>> GetPartnerPolicyModuleAsync(PartnerPolicyModule partnnerpolicymodule);

        Task<bool> UpdatePartnerPolicyModuleAsync(PartnerPolicyModule PartnerPolicyModule);

        Task<bool> RecordExist(Model.PartnerPolicyModule partnerpolicymodule);
        bool ActiveDeActive(int id, bool IsActive);

        bool ActiveDeActiveIsFormallyApprovedByBoard(int id, bool IsFormallyApprovedByBoard);

        bool ActiveDeActiveIsImplementedSince(int id, bool IsImplementedSince);

        bool ActiveDeActiveIsLastUpdatedOn(int id, bool IsLastUpdatedOn);
    }
}
