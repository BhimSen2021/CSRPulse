using System.Data;

namespace CSRPulse.Data.Repositories
{
    public interface IVillageImportRepository
    {
        DataSet GetImportFieldValues(DataTable dtState, DataTable dtDistrict, DataTable dtBlock, DataTable dtVillage);
        bool ImportVillageData(DataTable dtVillage);
    }
}