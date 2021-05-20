﻿using CSRPulse.Data.Models;
using Microsoft.Extensions.Configuration;
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


        public Task<bool> CreateBD(Customer dtoCustomer, string dbPath, string password, out string res)
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
                    //------------------------------------------

                    // changed connection from master to customer to run database structure script in created database
                    dbSqlconnection.ConnectionString = customConnection.Replace("CSRPulse", dtoCustomer.DataBaseName);
                    dbSqlconnection.Open();
                    SqlCommand cmdScript = new SqlCommand(File.ReadAllText(dbPath));
                    cmdScript.Connection = dbSqlconnection;
                    cmdScript.ExecuteNonQueryAsync();
                    //------------------------------------------

                    // Insert Admin credential in [User] table in customer database
                    var insScript = $"insert into [User](UserTypeId,UserName,FullName,[Password],IsActive,IsDeleted,CreatedBy,RoleId,EmailID)values(2,'{dtoCustomer.CustomerCode}','{dtoCustomer.CustomerName}','{password}',1,0,1,99,'{dtoCustomer.Email}')";
                    cmdScript.CommandText = insScript;
                    cmdScript.ExecuteNonQuery();
                    //-------------------------------------------
                    dbSqlconnection.Close();
                    res = "success";
                }

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
