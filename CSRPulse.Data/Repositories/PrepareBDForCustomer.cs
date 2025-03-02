﻿using CSRPulse.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace CSRPulse.Data.Repositories
{
    public class PrepareBDForCustomer : IPrepareDBForCustomer
    {
        private readonly IConfiguration config;
        public PrepareBDForCustomer(IConfiguration configuration)
        {
            config = configuration;
        }

        public Task<bool> CreateBD(Customer dtoCustomer, string _dbPath, string password, out string res)
        {
            SqlConnection dbSqlconnection;
            dbSqlconnection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
            var customConnection = dbSqlconnection.ConnectionString;

            try
            {
                using (dbSqlconnection)
                {
                    var dbCreateScript = $"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{dtoCustomer.DataBaseName}') create database {dtoCustomer.DataBaseName}";
                    // Create empty Database for customer
                    SqlCommand dbCommand = new SqlCommand(dbCreateScript);
                    dbSqlconnection.Open();
                    dbCommand.Connection = dbSqlconnection;
                    dbCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();
                    //--------------------------------------------

                }

                // changed connection from master to customer to run database structure script in created database
                dbSqlconnection.ConnectionString = customConnection.Replace("CSRPulse", dtoCustomer.DataBaseName);


                FileInfo file = new FileInfo(_dbPath);
                string script = file.OpenText().ReadToEnd();
                Microsoft.Data.SqlClient.SqlConnection dbSqlconnection1 = new Microsoft.Data.SqlClient.SqlConnection(dbSqlconnection.ConnectionString);
                Server server = new Server(new ServerConnection(dbSqlconnection1));
                server.ConnectionContext.ExecuteNonQuery(script);

                //------------------------------------------

                // Insert Admin default credential in [User],userrole,userrights tables in customer database                  
                SqlCommand cmdScript = new SqlCommand();
                cmdScript.Connection = dbSqlconnection;
                cmdScript.CommandType = System.Data.CommandType.StoredProcedure;
                cmdScript.CommandText = "SPCreateUser";
                cmdScript.Parameters.Add("@CustomerCode", SqlDbType.VarChar).Value = dtoCustomer.CustomerCode;
                cmdScript.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = dtoCustomer.CustomerName;
                cmdScript.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                cmdScript.Parameters.Add("@email", SqlDbType.NVarChar).Value = dtoCustomer.Email;

                cmdScript.Connection = dbSqlconnection;
                cmdScript.Connection.Open();
                cmdScript.ExecuteNonQuery();
                //-------------------------------------------
                dbSqlconnection.Close();
                res = "success";


                return Task.FromResult(true);

            }
            catch (SqlException)
            {
                dbSqlconnection.ConnectionString = customConnection;
                //Script to kill existing connection so database can be dropped.
                var killConScrpit = $"alter database {dtoCustomer.DataBaseName} set single_user with rollback immediate";
                var dbDropScript = $"IF EXISTS(SELECT * FROM sys.databases WHERE name = '{dtoCustomer.DataBaseName}') Drop database {dtoCustomer.DataBaseName}";
                SqlCommand cmd = new SqlCommand(killConScrpit);
                cmd.Connection = dbSqlconnection;
                dbSqlconnection.Open();
                cmd.ExecuteNonQuery();
                //-----------------------------------------------

                // Drop cutomer database Admin credential in [User] table in customer database
                cmd.CommandText = dbDropScript;
                cmd.ExecuteNonQuery();
                dbSqlconnection.Close();
                res = "nodb";
                return Task.FromResult(false);

            }
        }


    }
}
