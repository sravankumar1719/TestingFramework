using OpenQA.Selenium.Support.Extensions;
using System.IO;
using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomationTestingFramework.Utilities
{
    public class ScreenShotTaker
    {
        public TestContext TestContext { get; set; }

        public ScreenShotTaker(TestContext testContext)
        {
            this.TestContext = testContext;
        }

        public void TakeScreenShot()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = $"{TestContext.TestName}_{DateTime.Now:yyMMddHHmmss}.png";
            string filePath = Path.Combine(directoryName, fileName);
            //string filePath = Path.Combine(directoryName, $"Screenshots\\", fileName);
            //Directory.CreateDirectory(filePath);
            var screenshot = DriverExtensions.GetWebDriver().TakeScreenshot();
            screenshot.SaveAsFile(filePath, OpenQA.Selenium.ScreenshotImageFormat.Png);
        }

        public void TakeScreenShotForFailedAsserts(string assertMessage)
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = $"{assertMessage}.png";
            string filePath = Path.Combine(directoryName, fileName);
            //string filePath = Path.Combine(directoryName, $"\\Screenshots\\{TestContext.TestName}", fileName);
            //Directory.CreateDirectory(filePath);
            var screenshot = DriverExtensions.GetWebDriver().TakeScreenshot();
            screenshot.SaveAsFile(filePath, OpenQA.Selenium.ScreenshotImageFormat.Png);
        }
    }
}