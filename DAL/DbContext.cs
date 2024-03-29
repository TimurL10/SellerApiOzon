﻿using SellerApiOzon.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SellerApiOzon.DAL
{
    class DbContext : IDbContext
    {
        public int minRemains = 0;
        public int minPrice = 5000;
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

        // формирует json со списком товаров
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
                scCommand.Parameters.AddWithValue("@minPrice", minPrice);
                scCommand.Parameters.AddWithValue("@minRemain", minRemains);
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

        // Забирает из Озон номера товаров Озон и номера товаров 7electro
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
                scCommand.Parameters.AddWithValue("@minPrice", minPrice);
                scCommand.Parameters.AddWithValue("@minRemain", minRemains);
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

        // Сохраняет номера товаров Озон и номера товаров 7electro
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
        // Обновить остатки
        public string Dal_UpdateStockForOzon()
        {
            string idsList = "";
            using (IDbConnection connection = dbConnection)
            {
                IDataReader reader = null;

                SqlCommand scCommand = new SqlCommand("UpdateStockForOzon", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.CommandTimeout = 400;
                SqlParameter parameter = new SqlParameter();
                scCommand.Parameters.AddWithValue("@minPrice", minPrice);
                scCommand.Parameters.AddWithValue("@minRemain", minRemains);
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

        // Обновить список товаров на Озон
        public string Dal_GetItemsForOzon()
        {
            string itemsList = "";
            using (IDbConnection connection = dbConnection)
            {
                IDataReader reader = null;

                SqlCommand scCommand = new SqlCommand("GetItemsForOzon", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.CommandTimeout = 400;
                SqlParameter parameter = new SqlParameter();
                scCommand.Parameters.AddWithValue("@minPrice", minPrice);
                scCommand.Parameters.AddWithValue("@minRemain", minRemains);
                connection.Open();
                reader = scCommand.ExecuteReader();
                while (reader.Read())
                {
                    itemsList = (string)reader[0];
                }
                connection.Close();

            }
            return itemsList;
        }

        // Обновить список товаров на Озон
        public string Dal_UpdatePricesForOzon()
        {
            string pricesList = "";
            using (IDbConnection connection = dbConnection)
            {
                IDataReader reader = null;

                SqlCommand scCommand = new SqlCommand("UpdatePricesForOzon", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.CommandTimeout = 400;
                SqlParameter parameter = new SqlParameter();
                scCommand.Parameters.AddWithValue("@minPrice", minPrice);
                scCommand.Parameters.AddWithValue("@minRemain", minRemains);
                connection.Open();
                reader = scCommand.ExecuteReader();
                while (reader.Read())
                {
                    pricesList = (string)reader[0];
                }
                connection.Close();

            }
            return pricesList;
        }
    }
}
