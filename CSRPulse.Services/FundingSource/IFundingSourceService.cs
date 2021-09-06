using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IFundingSourceService
    {
        Task<FundingSource> CreateFundingSourceAsync(FundingSource FundingSource);
        Task<FundingSource> GetFundingSourceByIdAsync(int FundingSourceId);
        Task<List<FundingSource>> GetFundingSourcesAsync();
        Task<List<FundingSource>> GetFundingSourcesAsync(FundingSource FundingSource);
        Task<bool> UpdateFundingSourceAsync(FundingSource FundingSource);
        bool ActiveDeActive(int id, bool IsActive);
    }
}
