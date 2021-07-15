using System.Data;

namespace CSRPulse.Data.Repositories
{
    public interface IBlockImportRepository
    {
        DataSet GetImportFieldValues(DataTable dtState, DataTable dtDistrict, DataTable dtBlock);
        bool ImportBlockData(DataTable dtBlock);
    }
}