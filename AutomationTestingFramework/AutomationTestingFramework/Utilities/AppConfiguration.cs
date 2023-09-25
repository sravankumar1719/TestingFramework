using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AutomationTestingFramework.Utilities
{
    public static class AppConfiguration
    {
        private static IConfiguration _configuration;

        public static string GetChromeDriverPath()
        {
            _configuration = GetConfiguration();
            return _configuration["DriverLocation:ChromeBrowser"];
        }

        public static string GetApplicationUrl(string applicationName)
        {
            return GetConfiguration()[$"ApplicationUrl:{applicationName}"];
        }

        public static string GetSeleniumGridServer()
        {
            return GetConfiguration()[$"SeleniumGridServer:Server"];
        }

        private static IConfiguration GetConfiguration()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ConfigurationBuilder().AddJsonFile(Path.Combine(directoryName, "AppSettings.json")).Build();
        }
    }
}
