using AutomationFramework.PageComponents.Pages;
using OpenQA.Selenium;

namespace AutomationFramework.Tests.SwagLabsTests
{
    public class SwagLabsAddToCartPage : BasePage
    {
        private static readonly By AddToCartListLocator = By.XPath("//div[@class = 'cart_list']//div[@class = 'cart_item_label']//div[@class = 'inventory_item_name']");

        private static readonly string RemoveItemFromCartLocatorMask = "//div[text() = '{0}']/ancestor::div[@class = 'cart_item_label']//button[contains(@id, 'remove')]";

        /// <summary>
        /// Returns the list of items added to the cart
        /// </summary>
        /// <returns> Returns the list of items added to the cart. </returns>
        public List<string> GetListOfItemsAddedToCart()
        {
            List<string> inventoryItems = DriverExtensions.GetWebDriver().GetElements(AddToCartListLocator).Select(element => element.Text).ToList();
            this.Log.Info("Returns the list of items added to the cart : " + string.Join(", ", inventoryItems));
            return inventoryItems;
        }

        /// <summary>
        /// Clicks on Remove button for the inventory option
        /// </summary>
        /// <param name="inventoryName"> The inventory name. </param>
        public void ClickOnRemoveItemFromCart(string inventoryName)
        {
            this.Log.Info($"Clicks on Remove button for the inventory option '{inventoryName}'");
            DriverExtensions.GetWebDriver().GetElement(By.XPath(string.Format(RemoveItemFromCartLocatorMask, inventoryName))).Click();
        }

        /// <summary>
        /// Checks if Itemas are displayed in the Cart or not
        /// </summary>
        /// <returns> True, if Items are displayed in Cart, else false. </returns>
        public bool IsItemsDisplayedInCart()
        {
            this.Log.Info("Checks if Itemas are displayed in the Cart or not.");
            return DriverExtensions.GetWebDriver().IsDisplayed(AddToCartListLocator);
        }
    }
}
