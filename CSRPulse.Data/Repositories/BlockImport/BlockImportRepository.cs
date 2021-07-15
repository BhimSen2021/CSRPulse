using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CSRPulse.Data.Repositories
{
    public class BlockImportRepository : IBlockImportRepository
    {
        private readonly IConfiguration _configuration;
        public BlockImportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataSet GetImportFieldValues(DataTable dtState, DataTable dtDistrict, DataTable dtBlock)
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
                    dbSqlCommand.CommandText = "GetImportBlockFieldDtls";

                    SqlParameter paramState = dbSqlCommand.Parameters.AddWithValue("@tblStateId", dtState);
                    paramState.SqlDbType = SqlDbType.Structured;
                    paramState.TypeName = "udtStateId";

                    SqlParameter paramDistrict = dbSqlCommand.Parameters.AddWithValue("@tblDistrictId", dtDistrict);
                    paramDistrict.SqlDbType = SqlDbType.Structured;
                    paramDistrict.TypeName = "udtDistrictId";

                    SqlParameter paramBlockCode = dbSqlCommand.Parameters.AddWithValue("@tblBlockCode", dtBlock);
                    paramBlockCode.SqlDbType = SqlDbType.Structured;
                    paramBlockCode.TypeName = "udtBlockCode";

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

        public bool ImportBlockData(DataTable dtBlock)
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
                    dbSqlCommand.CommandText = "ImportBlock";

                    SqlParameter paramAttendanceDT = dbSqlCommand.Parameters.AddWithValue("@dtBlock", dtBlock);
                    paramAttendanceDT.SqlDbType = SqlDbType.Structured;
                    paramAttendanceDT.TypeName = "udtBlock";

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
