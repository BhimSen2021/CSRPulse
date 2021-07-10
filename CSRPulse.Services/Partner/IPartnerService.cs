using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IPartnerService
    {
        bool ActiveDeActive(int id, bool IsActive);
        Task<bool> CreatePartner(Partner partner);
        Task<List<Partner>> GetPartnerAsync(Partner partner);
        Task<Partner> GetPartnerById(int PartnerId);
        Task<bool> RecordExist(Partner partner);
        Task<bool> UpdatePartner(Partner partner);
    }
}