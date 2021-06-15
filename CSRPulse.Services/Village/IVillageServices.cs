using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;
namespace CSRPulse.Services
{
  public interface IVillageServices
    {
        Task<List<Model.Village>> GetVillageList();
        Task<bool> CreateVillage(Model.Village village);
        Task<bool> UpdateVillage(Model.Village village);
        Task<Model.Village> GetVillageById(int villageId);
        Task<bool> RecordExist(Model.Village village);
    }
}
