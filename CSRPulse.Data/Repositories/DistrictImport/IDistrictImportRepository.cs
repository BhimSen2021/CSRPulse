using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Data.Repositories
{
   public interface IDistrictImportRepository
    {
        DataSet GetImportFieldValues(DataTable dtState, DataTable dtDistrict);
        bool ImportDistrictData(DataTable dtDistrict);
    }
}
