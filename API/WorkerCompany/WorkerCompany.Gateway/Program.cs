using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using System.IO;

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
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var directory = Directory.GetCurrentDirectory();
                        var folder = Path.Combine(directory, "gatewayConfigs");
                        config.AddOcelot(folder, hostingContext.HostingEnvironment);
                    });
                })
                .ConfigureLogging(logging => logging.AddConsole());
    }
}
