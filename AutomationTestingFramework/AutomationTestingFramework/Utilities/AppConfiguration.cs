using AutomationTestingFramework.Utilities.Enum;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace AutomationTestingFramework.Utilities
{
    public static class AppConfiguration
    {
        public static string GetChromeDriverPath()
        {
            return GetConfiguration()["DriverLocation:ChromeBrowser"];
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
