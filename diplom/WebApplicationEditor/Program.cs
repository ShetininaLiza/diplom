using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BisnessLogic.Models;
using System.Data;
using System.Data.SQLite;
using System.Configuration;

namespace ApplicationEditor
{
    public static class Program
    {
        public static User editor;
        public static IDbConnection database;

        public static void Main(string[] args)
        {
            database = new SQLiteConnection(LoadConnectionString());
            database.Open();

            CreateHostBuilder(args).Build().Run();
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
