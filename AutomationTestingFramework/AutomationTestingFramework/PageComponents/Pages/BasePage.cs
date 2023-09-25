using AutomationTestingFramework.Utilities;
using log4net;
using System.Reflection;

namespace AutomationTestingFramework.PageComponents.Pages
{
    public class BasePage
    {
        public readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BasePage()
        {
            DriverExtensions.GetWebDriver().WaitForPageLoad();
        }

        public string GetCurrentUrl()
        {
            this.Log.Info("Reutrns the current url.");
            return DriverExtensions.GetWebDriver().Url;
        }

        public void SwitchToDefaultWindow()
        {
            var firstWindow = DriverExtensions.GetWebDriver().WindowHandles.First();
            DriverExtensions.GetWebDriver().SwitchTo().Window(firstWindow);
        }

        public void SwitchToNewlyOpenedTab()
        {
            var recentWindow = DriverExtensions.GetWebDriver().WindowHandles.Last();
            DriverExtensions.GetWebDriver().SwitchTo().Window(recentWindow);
        }
    }
}
