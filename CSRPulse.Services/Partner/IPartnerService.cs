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

        Task<List<NgoawardDetail>> GetUpdateNGOAwardDetails(Partner partner);
        Task<List<NgosaturatoryAuditorDetail>> GetUpdateSaturatoryAuditorDetails(Partner partner);
        Task<List<NGOKeyProjects>> GetUpdateNGOKeyProjects(Partner partner);
        Task<List<NGOCorpusGrantFund>> GetUpdateNGOCorpusGrantFund(Partner partner, int fundType);
        Task<List<NGORegistrationDetail>> GetUpdateNGORegistration(Partner partner);
        Task<List<NGOChartDocument>> GetUpdateNGOChartDocument(Partner partner);
        Task<List<NGOMember>> GetInsertNGOMember(NGOMember member);
        Task<List<NGOMember>> GetDeleteNGOMember(int id, int partnerId, int memberType);
        Task<List<NGOFundingPartner>> GetInsertNGOFundingPartner(NGOFundingPartner fundingPartner);
        List<NGOFundingPartner> GetDeleteNGOFundingPartner(int id, int partnerId, int agencyType);
        Task<NGOFundingPartner> GetNGOFundingPartner(int id);
        Task<List<PartnerDocumentDetail>> GetPartnerDocumentAsync(int partnerId);
    }
}