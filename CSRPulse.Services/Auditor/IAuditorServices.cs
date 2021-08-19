using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IAuditorServices
    {
        bool ActiveDeActive(int id, bool IsActive);
        Task<Auditor> CreateAuditorAsync(Auditor auditor);       
        Auditor GetAuditorById(int auditorId);
        Task<List<Auditor>> GetAuditorAsync(Auditor auditor);
        Task<bool> UpdateAuditorAsync(Auditor auditor);
    }
}