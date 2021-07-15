using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;
namespace CSRPulse.Services
{
  public interface IVillageServices
    {
        Task<List<Model.Village>> GetVillageList(Model.Village village);
        Task<bool> CreateVillage(Model.Village village);
        Task<bool> UpdateVillage(Model.Village village);
        Task<Model.Village> GetVillageById(int villageId);
        Task<bool> RecordExist(Model.Village village);
        bool ActiveDeActive(int id, bool IsActive);

        IEnumerable<dynamic> ReadVillageExcelData(string fileFullPath, bool isHeader, out string message, out int error, out int warning, out List<Model.VillageImport> importVillageSave, out List<string> missingHeaders, out List<string> columnName);
        bool ImportVillageData(List<Model.VillageImport> blockData);
    }
}
