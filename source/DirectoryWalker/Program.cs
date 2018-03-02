using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DirectoryWalker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .ConfigureAppConfiguration(LoadConfiguration)
                .UseStartup<Startup>()
                .Build();

        private static void LoadConfiguration(WebHostBuilderContext webHostBuilderContext, IConfigurationBuilder configurationBuilder)
        {
            var operatingSystem = System.Environment.GetEnvironmentVariable("OPERATING_SYSTEM");
            var configurationFilesLocation = "ConfiguarionFiles";

            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(configurationFilesLocation, "appsettings.json"), false, true)
                .AddJsonFile(Path.Combine(configurationFilesLocation, $"appsettings.{operatingSystem}.json"), true)
                .AddEnvironmentVariables();
        }
    }
}