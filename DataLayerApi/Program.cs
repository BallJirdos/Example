using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DataLayerApi.Services.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataLayerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDatabase().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var asseblyLocation = new FileInfo(Assembly.GetEntryAssembly().Location);
            var rootPath = asseblyLocation.Directory.FullName;
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
            .SetBasePath(rootPath)
            .AddJsonFile($"appsettings.{environment}.json")
            .Build();


            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                     webBuilder.UseIISIntegration();
                     webBuilder.UseContentRoot(rootPath);
                     webBuilder.UseStartup<Startup>();
                 })
                //.ConfigureLogging((hostingContext, logging) =>
                //{
                //    // The ILoggingBuilder minimum level determines the
                //    // the lowest possible level for logging. The log4net
                //    // level then sets the level that we actually log at.
                //    logging.AddLog4Net();
                //    logging.SetMinimumLevel(LogLevel.Debug);
                //    //log4net.GlobalContext.Properties["LogFileName"] = @"E:\\file1"; //log file path
                //    //var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                //    //XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
                //})
                ;
        }
    }
}
