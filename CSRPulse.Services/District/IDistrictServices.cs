using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;
namespace CSRPulse.Services
{
  public interface IDistrictServices
    {
        Task<List<Model.District>> GetDistrictList();
        Task<bool> CreateDistrict(Model.District district);
        Task<bool> UpdateDistrict(Model.District district);
        Task<Model.District> GetDistrictById(int districtId);
        Task<bool> RecordExist(Model.District district);
    }
}
