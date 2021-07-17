using CSRPulse.Model;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IProcessSetupServices
    {
        Task<bool> CreateProcessSetup(ProcessSetup process);
        Task<ProcessSetup> GetProcessSetupById(int setupId);
        Task<bool> UpdateProcessSetup(ProcessSetup processSetup);
        Task<bool> InsertProcessSetupHistory(ProcessSetupHistory process);
        Task<ProcessSetupHistory> GetProcessSetupHistoryBySetupId(int setupId);
    }
}