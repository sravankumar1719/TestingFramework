using AutomationFramework.Utilities;
using OpenQA.Selenium;

namespace AutomationFramework.PageComponents.Components.SwagLabs
{
    public class SwagLabsInventoryComponent : BaseComponent
    {
        private static readonly string AddToCartLocatorMask = "//div[text() = '{0}']/ancestor::div[@class = 'inventory_item_description']//button[contains(@id, 'add-to-cart')]";
        private static readonly string RemoveItemLocatorMask = "//div[text() = '{0}']/ancestor::div[@class = 'inventory_item_description']//button[contains(@id, 'remove')]";

        public SwagLabsInventoryComponent(By locator) : base(locator)
        {
        }

        /// <summary>
        /// Clicks on Add to cart button for the specified inventory option
        /// </summary>
        /// <param name="inventoryName"> The inventory name. </param>
        public void ClickOnAddtoCart(string inventoryName)
        {
            this.Log.Info($"Clicks on Add to cart button for the inventory option '{inventoryName}'");
            DriverExtensions.GetWebDriver().GetElement(By.XPath(string.Format(AddToCartLocatorMask, inventoryName))).Click();
        }

        /// <summary>
        /// Clicks on Remove Item button for the specified inventory option
        /// </summary>
        /// <param name="inventoryName"> The inventory name. </param>
        public void CLickOnRemoveItemButton(string inventoryName)
        {
            this.Log.Info($"Clicks on Remove item button for the inventory option '{inventoryName}'");
            DriverExtensions.GetWebDriver().GetElement(By.XPath(string.Format(RemoveItemLocatorMask, inventoryName))).Click();
        }
    }
}
