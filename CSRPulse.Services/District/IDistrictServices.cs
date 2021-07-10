using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;
namespace CSRPulse.Services
{
  public interface IDistrictServices
    {
        Task<List<Model.District>> GetDistrictList(Model.District district);
        Task<bool> CreateDistrict(Model.District district);
        Task<bool> UpdateDistrict(Model.District district);
        Task<Model.District> GetDistrictById(int districtId);
        Task<bool> RecordExist(Model.District district);
        bool ActiveDeActive(int id, bool IsActive);


        IEnumerable<dynamic> ReadDistrictExcelData(string fileFullPath, bool isHeader, out string message, out int error, out int warning, out List<Model.DistrictImport> importDistrictSave, out List<string> missingHeaders, out List<string> columnName);
        bool ImportDistrictData(List<Model.DistrictImport> districtData);
    }
}
