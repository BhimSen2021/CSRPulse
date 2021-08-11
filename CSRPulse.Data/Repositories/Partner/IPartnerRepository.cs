using System.Linq;

namespace CSRPulse.Data.Repositories
{
    public interface IPartnerRepository
    {
        IQueryable<Document> GetDocuments(int partnerId);
    }
}