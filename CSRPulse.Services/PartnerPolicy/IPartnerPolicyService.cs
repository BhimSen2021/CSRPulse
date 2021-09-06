using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;

namespace CSRPulse.Services
{
    public interface IPartnerPolicyService
    {
        Task<List<Model.PartnerPolicy>> GetPartnerPolicyList();
        Task<bool> CreatePartnerPolicy(Model.PartnerPolicy partnnerpolicy);
        Task<List<PartnerPolicy>> GetPartnerPolicyAsync(PartnerPolicy partnnerpolicy);
        Task<bool> UpdatePartnerPolicy(Model.PartnerPolicy partnnerpolicy);
        Task<Model.PartnerPolicy> GetPartnerPolicyById(int PolicyId);
        Task<bool> RecordExist(Model.PartnerPolicy partnnerpolicy);
        bool ActiveDeActive(int id, bool IsActive);

        bool ActiveDeActiveIsFormallyApprovedByBoard(int id, bool IsFormallyApprovedByBoard);

        bool ActiveDeActiveIsImplementedSince(int id, bool IsImplementedSince);

        bool ActiveDeActiveIsLastUpdatedOn(int id, bool IsLastUpdatedOn);

    }
}
