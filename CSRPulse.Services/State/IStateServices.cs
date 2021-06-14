using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;
namespace CSRPulse.Services
{
  public interface IStateServices
    {
        Task<List<Model.State>> GetStateList();
        Task<bool> CreateState(Model.State state);
        Task<bool> UpdateState(Model.State state);
        Task<Model.State> GetStateById(int stateId);
        Task<bool> RecordExist(Model.State state);
    }
}
