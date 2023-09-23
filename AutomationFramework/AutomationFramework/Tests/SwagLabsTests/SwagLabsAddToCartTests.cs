using AutomationFramework.PageComponents.Pages.SwagLabs;
using Xunit;
using Xunit.Abstractions;

namespace AutomationFramework.Tests.SwagLabsTests
{
    //[Collection("Serial")]
    public class SwagLabsAddToCartTests : BaseTests
    {
        private static readonly string applicationName = "SwagLabsUrl";

        public SwagLabsAddToCartTests(ITestOutputHelper _outputHelper) : base(applicationName, _outputHelper)
        {
            SwagLabsLoginPage swagLabsLoginPage = this.GetHomePage<SwagLabsLoginPage>();
            swagLabsLoginPage.LoginToApplication();
        }

        [Fact]
        [Trait("Category", "AddToCart")]
        public void AddToCartFunctionalityTest()
        {
            _outputHelper.WriteLine("Test case : AddToCartFunctionalityTest()");

            List<string> InventoryNames = new List<string>() { "Sauce Labs Backpack", "Sauce Labs Bolt T-Shirt", "Sauce Labs Fleece Jacket" };
            var swagLabHomePage = this.GetHomePage<SwagLabsHomePage>();

            InventoryNames.ForEach(inventoryName => swagLabHomePage.InventoryComponent.ClickOnAddtoCart(inventoryName));

            Assert.Equal(InventoryNames.Count, swagLabHomePage.HeaderComponent.GetCountOfAddedItemsToCart());
            var addToCartPage = swagLabHomePage.HeaderComponent.ClickOnAddToCartIcon();
            var actualInventoryNames = addToCartPage.GetListOfItemsAddedToCart();

            Assert.Equal(InventoryNames.Count, actualInventoryNames.Count);
            foreach (var inventoryName in InventoryNames)
            {
                Assert.True(actualInventoryNames.Any(name => name.Equals(inventoryName)), $"Expected {inventoryName} is not displayed in Add To Cart page.");
            }

            InventoryNames.ForEach(inventoryName => addToCartPage.ClickOnRemoveItemFromCart(inventoryName));
            Assert.False(addToCartPage.IsItemsDisplayedInCart(), "Still items are getting displayed in the Cart.");
        }

        [Fact]
        [Trait("Category", "AddToCart")]
        public void HomePageRemoveButtonFunctionalityTest()
        {
            _outputHelper.WriteLine("Test case : HomePageRemoveButtonFunctionalityTest()");

            List<string> InventoryNames = new List<string>() { "Sauce Labs Backpack", "Sauce Labs Bolt T-Shirt", "Sauce Labs Fleece Jacket" };
            var swagLabHomePage = this.GetHomePage<SwagLabsHomePage>();

            InventoryNames.ForEach(inventoryName => swagLabHomePage.InventoryComponent.ClickOnAddtoCart(inventoryName));
            Assert.Equal(InventoryNames.Count, swagLabHomePage.HeaderComponent.GetCountOfAddedItemsToCart());

            InventoryNames.ForEach(inventoryName => swagLabHomePage.InventoryComponent.CLickOnRemoveItemButton(inventoryName));
            Assert.False(swagLabHomePage.HeaderComponent.IsCountOfItemsToCartDisplayed(), "Still items count is getting displayed in the Cart.");
        }
    }
}
