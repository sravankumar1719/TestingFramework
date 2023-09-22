using AutomationFramework.PageComponents.Pages;
using OpenQA.Selenium;

namespace AutomationFramework.PageComponents.Components
{
    public class BaseComponent : BasePage
    {
        public readonly By ComponentLocator;

        public BaseComponent(By locator)
        {
            this.ComponentLocator = locator;
        }

        public virtual bool IsDisplayed()
        {
            this.Log.Info("Checks if component is displayed or not.");
            return DriverExtensions.GetWebDriver().IsDisplayed(ComponentLocator);
        }
    }
}
