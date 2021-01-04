using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerCompany.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var env = "dev";
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) => 
                    {
                        var directory = Directory.GetCurrentDirectory();
                        var folder = Path.Combine(directory, "gatewayConfigs");
                        //config.AddJsonFile($"ocelot.{env}.json");
                        config.AddOcelot(folder, hostingContext.HostingEnvironment);
                    });
                })
                .ConfigureLogging(logging => logging.AddConsole());
    }
}
