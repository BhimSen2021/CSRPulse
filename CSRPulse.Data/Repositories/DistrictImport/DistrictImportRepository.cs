using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CSRPulse.Data.Repositories
{
    public class DistrictImportRepository : IDistrictImportRepository
    {
        private readonly IConfiguration config;
        public DistrictImportRepository(IConfiguration configuration)
        {
            config = configuration;
        }
        public DataSet GetImportFieldValues(DataTable dtState,DataTable dtDistrict)
        {
            DataSet dbSqlDataTable = new DataSet();
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;

                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(config.GetConnectionString("DefaultConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand();
                    dbSqlCommand.Connection = dbSqlconnection;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "GetImportDistrictFieldDtls";

                    SqlParameter paramState = dbSqlCommand.Parameters.AddWithValue("@tblStateId", dtState);
                    paramState.SqlDbType = SqlDbType.Structured;
                    paramState.TypeName = "udtStateId";

                    SqlParameter paramDistrictCode = dbSqlCommand.Parameters.AddWithValue("@tblDistrictCode", dtDistrict);
                    paramDistrictCode.SqlDbType = SqlDbType.Structured;
                    paramDistrictCode.TypeName = "udtDistrictCode";

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

        public bool ImportDistrictData(DataTable dtDistrict)
        {
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(config.GetConnectionString("DefaultConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand();
                    dbSqlCommand.Connection = dbSqlconnection;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "ImportDistrict";

                    SqlParameter paramAttendanceDT = dbSqlCommand.Parameters.AddWithValue("@dtDistrict", dtDistrict);
                    paramAttendanceDT.SqlDbType = SqlDbType.Structured;
                    paramAttendanceDT.TypeName = "udtDistrict";                  

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
