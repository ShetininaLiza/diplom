using BisnessLogic.Models;
using Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class Program
    {
        public static User user;
        public static IDbConnection database;
        public static void Main(string[] args)
        {
            //await Task.Run(() => Start(args));
            database = new SQLiteConnection(LoadConnectionString());
            database.Open();
            
            user = new User();
            CreateHostBuilder(args).Build().Run();
        }
        static void Start(string[] args) 
        {
            database = new SQLiteConnection(LoadConnectionString());
            database.Open();

            CreateHostBuilder(args).Build().Start();
                //.Run();
            
        }
        private static string LoadConnectionString(string id = "DiplomDB") 
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
         
    }
}
