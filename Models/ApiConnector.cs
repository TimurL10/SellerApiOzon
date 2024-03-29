﻿using Newtonsoft.Json;
using SellerApiOzon.DAL;
using SellerApiOzon.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace SellerApiOzon.Models
{
    class ApiConnector : IApiConnector
    {

        public static IDbContext _dbContext;

        public ApiConnector(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string urlWorkEnv = "https://api-seller.ozon.ru";
        string jsonData = "{'category_id': '85894960','language':'DEFAULT'}".Replace("'","\"");
        string jsonData2 = "{'attribute_type': 'ALL','category_id': ['71197035'],'language': 'DEFAULT'}".Replace("'", "\"");
        string  jsonData3  = "{'attribute_id': '28732849','category_id': '17028654','language': 'DEFAULT','last_value_id': '0','limit':'5'}".Replace("'", "\"");
        

        public void GetAttributes()
        {
            try
            {

                using (var client = new HttpClient())
                {
                    
                    client.DefaultRequestHeaders.Add("Client-Id", "344925");
                    client.DefaultRequestHeaders.Add("Api-Key", "44247e72-c8ce-4164-b67f-9e03a0319eb2");

                    //var response = client.GetAsync(urlWorkEnv + "/v1/categories/tree").Result;
                    //var result = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(result);


                    //var response = client.PostAsync(urlWorkEnv + "/v2/category/tree", new StringContent(jsonData, Encoding.UTF8)).Result;
                    //var result = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(result);

                    //// присылает атрибуты по категории
                    var response = client.PostAsync(urlWorkEnv + "/v3/category/attribute", new StringContent(jsonData2, Encoding.UTF8)).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);

                    //присылает полный спр.атрибуты по категории
                     response = client.PostAsync(urlWorkEnv + "/v2/category/attribute/values", new StringContent(jsonData3, Encoding.UTF8)).Result;
                     result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                }


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetSkuFromOzon()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Client-Id", "344925");
                client.DefaultRequestHeaders.Add("Api-Key", "44247e72-c8ce-4164-b67f-9e03a0319eb2");

                var VendorCodes = _dbContext.Dal_GetSkuFromOzon();

                //string json = JsonConvert.SerializeObject(VendorCodes, Formatting.Indented);
                var response = client.PostAsync(urlWorkEnv + "/v2/product/list", new StringContent(VendorCodes, Encoding.UTF8, "application/json")).Result;                
                var result = response.Content.ReadAsStringAsync().Result;
                _dbContext.Dal_SaveSkuFromOzon((string)result);

            }

        }        

        public void UpdateStockForOzon()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Client-Id", "344925");
                client.DefaultRequestHeaders.Add("Api-Key", "44247e72-c8ce-4164-b67f-9e03a0319eb2");

                var Stock = _dbContext.Dal_UpdateStockForOzon();

                //string json = JsonConvert.SerializeObject(VendorCodes, Formatting.Indented);
                var response = client.PostAsync(urlWorkEnv + "/v1/product/import/stocks", new StringContent(Stock, Encoding.UTF8, "application/json")).Result;
                var result = response.Content.ReadAsStringAsync().Result;                

            }
        }

        public void UpdatePricesForOzon()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Client-Id", "344925");
                client.DefaultRequestHeaders.Add("Api-Key", "44247e72-c8ce-4164-b67f-9e03a0319eb2");

                var Stock = _dbContext.Dal_UpdatePricesForOzon();   
                //string json = JsonConvert.SerializeObject(VendorCodes, Formatting.Indented);
                var response = client.PostAsync(urlWorkEnv + "/v1/product/import/prices", new StringContent(Stock, Encoding.UTF8, "application/json")).Result;
                var result = response.Content.ReadAsStringAsync().Result;

            }
        }

        public void GetItemForOzon()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Client-Id", "344925");
                client.DefaultRequestHeaders.Add("Api-Key", "44247e72-c8ce-4164-b67f-9e03a0319eb2");

                var Items = _dbContext.Dal_GetItemsForOzon();


                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(Items);
                int count = 0;
                

                while (myDeserializedClass.items.Count > 0)
                {
                    if (myDeserializedClass.items.Count < 99)
                        count = myDeserializedClass.items.Count;
                    else
                        count = 99;
                    var items100 = myDeserializedClass.items.GetRange(0, count);
                    myDeserializedClass.items.RemoveRange(0, count);

                    var items100Json = JsonConvert.SerializeObject(items100, Formatting.Indented);
                    items100Json = string.Format("{{ \"{0}\": {1} }}", "items", items100Json);

                    var response = client.PostAsync(urlWorkEnv + "/v2/product/import", new StringContent(items100Json, Encoding.UTF8, "application/json")).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(result);

                    Thread.Sleep(1000);

                }

            }
        }

    }
}
