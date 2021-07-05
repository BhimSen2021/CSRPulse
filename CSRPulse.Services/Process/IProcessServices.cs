using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IProcessServices
    {
        bool ActiveDeActive(int id, bool IsActive);
        Task<bool> CreateProcess(Process process);
        Task<List<Process>> GetProcessAsync(Process process);
        Task<Process> GetProcessById(int ProcessId);
        Task<bool> RecordExist(Process process);
        Task<bool> UpdateProcess(Process process);
    }
}