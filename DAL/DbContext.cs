using SellerApiOzon.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SellerApiOzon.DAL
{
    class DbContext : IDbContext
    {
        private string _configuration;
        public DbContext()
        {
            _configuration = "Server = LAPTOP-94EIKF8P\\SQLEXPRESS; Integrated Security = SSPI; Database = 7gostore_db;Connection Timeout=380;";
        }

        internal IDbConnection dbConnection
        {
            get
            {
                return new SqlConnection(_configuration);
            }

        }

        public string Dal_GetRemainsForPrices()
        {

            string idsList = "";
            using (IDbConnection connection = dbConnection)
            {
                IDataReader reader = null;
                
                SqlCommand scCommand = new SqlCommand("GetItemsForOzon", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.CommandTimeout = 400;
                SqlParameter parameter = new SqlParameter();
                scCommand.Parameters.AddWithValue("@minPrice", 10000);
                scCommand.Parameters.AddWithValue("@minRemain", 1);
                connection.Open();
                reader = scCommand.ExecuteReader();
                while (reader.Read())
                {
                    idsList = (string)reader[0];
                }
                connection.Close();

            }
            return idsList;
        }

        public string Dal_GetSkuFromOzon()
        {

            string idsList = "";
            using (IDbConnection connection = dbConnection)
            {
                IDataReader reader = null;

                SqlCommand scCommand = new SqlCommand("GetSkuFromOzon", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.CommandTimeout = 400;
                SqlParameter parameter = new SqlParameter();
                scCommand.Parameters.AddWithValue("@minPrice", 10000);
                scCommand.Parameters.AddWithValue("@minRemain", 1);
                connection.Open();
                reader = scCommand.ExecuteReader();
                while (reader.Read())
                {
                    idsList = (string)reader[0];
                }
                connection.Close();

            }
            return idsList;
        }

        public void Dal_SaveSkuFromOzon(string skuList)
        {
            using (IDbConnection connection = dbConnection)
            {

                SqlCommand scCommand = new SqlCommand("SaveSkuFromOzon", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.CommandTimeout = 200;
                SqlParameter parameter = new SqlParameter();
                scCommand.Parameters.AddWithValue("@skuList", skuList);
                connection.Open();
                scCommand.ExecuteNonQuery();
                connection.Close();

            }
        }
    }
}
