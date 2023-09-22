using AutomationFramework.PageComponents.Components.SwagLabs;
using OpenQA.Selenium;

namespace AutomationFramework.PageComponents.Pages.SwagLabs
{
    public class SwagLabsHomePage : BasePage
    {
        private static readonly By HeaderComponentLocator = By.ClassName("header_label");
        private static readonly By InventoryContainerLocator = By.Id("inventory_container");
        private static readonly By FooterComponentLocator = By.XPath("//footer[@class = 'footer']");

        public SwagLabsHeaderComponent HeaderComponent = new SwagLabsHeaderComponent(HeaderComponentLocator);

        public SwagLabsInventoryComponent InventoryComponent = new SwagLabsInventoryComponent(InventoryContainerLocator);

        public SwagLabsFooterComponent FooterComponent = new SwagLabsFooterComponent(FooterComponentLocator);
    }
}
