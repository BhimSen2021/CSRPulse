using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CSRPulse.Data.Repositories
{
    public class VillageImportRepository : IVillageImportRepository
    {
        private readonly IConfiguration _configuration;
        public VillageImportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataSet GetImportFieldValues(DataTable dtState, DataTable dtDistrict, DataTable dtBlock, DataTable dtVillage)
        {
            DataSet dbSqlDataTable = new DataSet();
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;

                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand();
                    dbSqlCommand.Connection = dbSqlconnection;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "GetImportVillageFieldDtls";

                    SqlParameter paramState = dbSqlCommand.Parameters.AddWithValue("@tblStateId", dtState);
                    paramState.SqlDbType = SqlDbType.Structured;
                    paramState.TypeName = "udtStateId";

                    SqlParameter paramDistrict = dbSqlCommand.Parameters.AddWithValue("@tblDistrictId", dtDistrict);
                    paramDistrict.SqlDbType = SqlDbType.Structured;
                    paramDistrict.TypeName = "udtDistrictId";

                    SqlParameter paramBlock = dbSqlCommand.Parameters.AddWithValue("@tblBlockId", dtBlock);
                    paramBlock.SqlDbType = SqlDbType.Structured;
                    paramBlock.TypeName = "udtBlockId";

                    SqlParameter paramVillageCode = dbSqlCommand.Parameters.AddWithValue("@tblVillageCode", dtVillage);
                    paramVillageCode.SqlDbType = SqlDbType.Structured;
                    paramVillageCode.TypeName = "udtVillageCode";


                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlAdapter.Fill(dbSqlDataTable);
                    dbSqlconnection.Close();

                    return dbSqlDataTable;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ImportVillageData(DataTable dtVillage)
        {
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand();
                    dbSqlCommand.Connection = dbSqlconnection;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "ImportVillage";

                    SqlParameter paramAttendanceDT = dbSqlCommand.Parameters.AddWithValue("@dtVillage", dtVillage);
                    paramAttendanceDT.SqlDbType = SqlDbType.Structured;
                    paramAttendanceDT.TypeName = "udtVillage";

                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
