using SellerApiOzon.DAL;
using SellerApiOzon.Models;
using System;
using Newtonsoft.Json;
using SellerApiOzon.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SellerApiOzon
{
    class Program
    {             

        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
           .ConfigureServices(ConfigureServices)
           .ConfigureServices(services => services.AddSingleton<Executor>())
           .Build()
           .Services
           .GetService<Executor>()
           .Execute();

            Console.WriteLine("Starting a matrix..");  
            
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<IApiConnector, ApiConnector>();
            services.AddSingleton<IDbContext, DbContext>();
        }        
    }
}


    public class Executor
    {
        public IApiConnector _apiConnector;

        public Executor(IApiConnector apiConnector)
        {
            _apiConnector = apiConnector;
        }

        public void Execute()
        {
             _apiConnector.GetAttributes();
           // _apiConnector.GetSkuFromOzon();
        }
    }

