using AutomationFramework.Utilities.Enum;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AutomationFramework.Utilities
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

        public static DriverType GetDriverType()
        {
            Environment.SetEnvironmentVariable("DriverType", "");
            var driverType = Environment.GetEnvironmentVariable("DriverType"); //GetConfiguration()[$"DriverType:Driver"];
            System.Enum.TryParse(driverType, out DriverType expectedDriverType);
            return expectedDriverType;
        }

        private static IConfiguration GetConfiguration()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ConfigurationBuilder().AddJsonFile(Path.Combine(directoryName, "AppSettings.json")).Build();
        }
    }
}