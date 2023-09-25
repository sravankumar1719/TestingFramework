using AutomationTestingFramework.PageComponents.Enum;
using AutomationTestingFramework.PageComponents.Pages;
using AutomationTestingFramework.PageComponents.Pages.SwagLabsPage;
using AutomationTestingFramework.Utilities;
using OpenQA.Selenium;

namespace AutomationTestingFramework.PageComponents.Components.SwagLab
{
    public class SwagLabsHeaderComponent : BaseComponent
    {
        private static readonly By AppLogoLocator = By.XPath("//div[@class = 'app_logo']");
        private static readonly By ShoppingCartLocator = By.Id("shopping_cart_container");
        private static readonly By OpenMenuButtonLocator = By.Id("react-burger-menu-btn");
        private static readonly By MenuItemListLocator = By.XPath("//nav[@class = 'bm-item-list']//a");
        private static readonly By ShoppingCartItemsNumberLocator = By.XPath("//span[@class = 'shopping_cart_badge']");

        public SwagLabsHeaderComponent(By locator) : base(locator)
        {
        }

        /// <summary>
        /// Returns the App logo text.
        /// </summary>
        /// <returns> Returns the App logo text. </returns>
        public string GetAppLogoText()
        {
            this.Log.Info("Returns the App logo text.");
            return DriverExtensions.GetWebDriver().GetElement(AppLogoLocator).Text;
        }

        /// <summary>
        /// Checks if shopping cart logo is displayed.
        /// </summary>
        /// <returns> True, if Shopping Cart logo is displayed, else false. </returns>
        public bool IsShoppingCartLogoDisplayed()
        {
            this.Log.Info("Checks if shopping cart logo is displayed.");
            return DriverExtensions.GetWebDriver().IsDisplayed(ShoppingCartLocator);
        }

        /// <summary>
        /// Checks if Open menu button is displayed.
        /// </summary>
        /// <returns> True, if Open Menu button is displayed, else false. </returns>
        public bool IsOpenMenuButtonDisplayed()
        {
            this.Log.Info("Checks if Open menu button is displayed.");
            return DriverExtensions.GetWebDriver().IsDisplayed(OpenMenuButtonLocator);
        }

        /// <summary>
        /// Clicks on Open Menu button.
        /// </summary>
        public void ClickOnOpenMenuButton()
        {
            this.Log.Info("Clicks on Open Menu button");
            DriverExtensions.GetWebDriver().GetElement(OpenMenuButtonLocator).Click();
            DriverExtensions.GetWebDriver().WaitForPageLoad();
        }

        /// <summary>
        /// Returns the menu items list.
        /// </summary>
        /// <returns> Returns the menu items list. </returns>
        public List<string> GetMenuItemsList()
        {
            var menuItemsList = DriverExtensions.GetWebDriver().GetElements(MenuItemListLocator).Select(element => element.Text).ToList();
            this.Log.Info("Returns the menu items list : " + string.Join(", ", menuItemsList));
            return menuItemsList;
        }

        /// <summary>
        /// Clicks on specified menu item.
        /// </summary>
        /// <typeparam name="T"> The page instance. </typeparam>
        /// <param name="menuItem"> The menu item. </param>
        /// <returns> The page instance. </returns>
        public T ClickOnMenuItem<T>(MenuItem menuItem) where T : BasePage
        {
            this.Log.Info($"Clicks on '{menuItem.GetEnumValue()}' Menu item");
            DriverExtensions.GetWebDriver().GetElements(MenuItemListLocator).First(element => element.Text.Equals(menuItem.GetEnumValue())).Click();
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Clicks on Add to cart icon.
        /// </summary>
        /// <returns> The page instance of Add to cart page. </returns>
        public SwagLabsAddToCartPage ClickOnAddToCartIcon()
        {
            this.Log.Info("Clicks on Add To Cart Icon");
            DriverExtensions.GetWebDriver().GetElement(ShoppingCartLocator).Click();
            return Activator.CreateInstance<SwagLabsAddToCartPage>();
        }

        /// <summary>
        /// Returns the count of Added items to the cart
        /// </summary>
        /// <returns> Returns the count of Added items to the cart. </returns>
        public int GetCountOfAddedItemsToCart()
        {
            int count = int.Parse(DriverExtensions.GetWebDriver().GetElement(ShoppingCartItemsNumberLocator).Text);
            this.Log.Info("Returns the count of Added items to the cart : " + count);
            return count;
        }

        public bool IsCountOfItemsToCartDisplayed()
        {
            this.Log.Info("Checks if count of items to cart is displayed or not.");
            return DriverExtensions.GetWebDriver().IsDisplayed(ShoppingCartItemsNumberLocator);
        }
    }
}
