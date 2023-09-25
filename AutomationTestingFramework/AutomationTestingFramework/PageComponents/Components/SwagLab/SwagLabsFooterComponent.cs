using AutomationTestingFramework.PageComponents.Enum;
using AutomationTestingFramework.PageComponents.Pages;
using AutomationTestingFramework.Utilities;
using OpenQA.Selenium;

namespace AutomationTestingFramework.PageComponents.Components.SwagLab
{
    public class SwagLabsFooterComponent : BaseComponent
    {
        private static readonly By FooterTextLocator = By.XPath("//div[@class = 'footer_copy']");
        private static readonly By SocialNetworkLinkLocator = By.XPath("//ul[@class = 'social']//li");

        public SwagLabsFooterComponent(By locator) : base(locator)
        {
        }

        public string GetFooterText()
        {
            this.Log.Info("Returns the footer text of he application.");
            return DriverExtensions.GetWebDriver().GetElement(FooterTextLocator).Text.Replace("\r\n", " ");
        }

        public List<string> GetSocialNetworkLinkText()
        {
            var socialNetworkLinks = DriverExtensions.GetWebDriver().GetElements(SocialNetworkLinkLocator).Select(element => element.Text).ToList();
            this.Log.Info("Returns the list of social network links displayed : " + string.Concat(socialNetworkLinks, ", "));
            return socialNetworkLinks;
        }

        public T ClickOnSocialNetworkLinkText<T>(SocialNetworks socialNetwork) where T : BasePage
        {
            this.Log.Info($"Clicks on '{socialNetwork}' link displayed in footer component.");
            DriverExtensions.GetWebDriver().GetElements(SocialNetworkLinkLocator).First(element => element.Text.Equals(socialNetwork.ToString())).Click();
            this.SwitchToNewlyOpenedTab();
            return Activator.CreateInstance<T>();
        }
    }
}
