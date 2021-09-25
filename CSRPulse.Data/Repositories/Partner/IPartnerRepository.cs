using System.Linq;

namespace CSRPulse.Data.Repositories
{
    public interface IPartnerRepository
    {
        IQueryable<Document> GetDocuments(int partnerId);
        IQueryable<Document> GetPartnerDocuments(int partnerId, int processId);
        //IQueryable<PolicyDetails> GetPartnerPolicyDetails(int partnerId);

    }
}