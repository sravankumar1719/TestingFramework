using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationTestingFramework.Utilities
{
    public static class DriverExtensions
    {
        private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        public static IWebDriver GetWebDriver()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            //driver.Value = driver.Value ?? new ChromeDriver(AppConfiguration.GetChromeDriverPath());
            driver.Value = driver.Value ?? new RemoteWebDriver(new Uri(AppConfiguration.GetSeleniumGridServer()), chromeOptions.ToCapabilities());
            return driver.Value;
        }

        public static void CloseBrowser(this IWebDriver webDriver)
        {
            webDriver.Quit();
            driver.Value = null;
        }

        public static void NavigateToApplicationUrl(this IWebDriver driver, string applicationName)
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(AppConfiguration.GetApplicationUrl(applicationName));
            driver.WaitForPageLoad();
        }

        public static IWebElement GetElement(this IWebDriver driver, By locator)
        {
            var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            return webDriverWait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static IReadOnlyCollection<IWebElement> GetElements(this IWebDriver driver, By locator)
        {
            var elements = new List<IWebElement>();
            try
            {
                var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                elements = webDriverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator)).ToList();
            }

            catch (Exception ex)
            {
            }

            return elements;
        }

        public static bool IsDisplayed(this IWebDriver driver, By locator, int timeOutInSeconds = 0)
        {
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds)).Until(webDriver => webDriver.FindElement(locator).Displayed);
            }

            catch
            {
                return false;
            }
        }

        public static void WaitForPageLoad(this IWebDriver driver)
        {
            var javascriptExecutor = (IJavaScriptExecutor)driver;
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(
                webDriver => javascriptExecutor.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public static T CreatePageInstance<T>(this IWebDriver driver)
        {
            return Activator.CreateInstance<T>();
        }
    }
}
