using AutomationTestingFramework.PageComponents.Pages.SwagLabsPage;
using AutomationTestingFramework.Utilities;

namespace AutomationTestingFramework.Tests.SwagLabtests
{
    [TestClass]
    public class SwagLabsAddToCartTests : BaseTests
    {
        public SwagLabsAddToCartTests()
        {
            ApplicationName = "SwagLabsUrl";
        }

        [TestInitialize]
        public override void InitSetUp()
        {
            base.InitSetUp();
            SwagLabsLoginPage swagLabsLoginPage = this.GetHomePage<SwagLabsLoginPage>();
            swagLabsLoginPage.LoginToApplication();
        }

        [TestMethod]
        [TestCategory("AddToCart")]
        public void AddToCartFunctionalityTest()
        {
            var addToCartNumberCheck = new AssertCheck("Verify same number of added items are getting displayed in the Cart icon.");
            var numberOfItemsInCartCheck = new AssertCheck("Verify same number of added items are getting displayed in the Cart page.");
            var emptyCartCheck = new AssertCheck("Verify no items are getting displayed in the Cart.");
            this.SoftAssertions.AddAssertions(addToCartNumberCheck, numberOfItemsInCartCheck, emptyCartCheck);

            List<string> InventoryNames = new List<string>() { "Sauce Labs Backpack", "Sauce Labs Bolt T-Shirt", "Sauce Labs Fleece Jacket" };
            var swagLabHomePage = this.GetHomePage<SwagLabsHomePage>();

            InventoryNames.ForEach(inventoryName => swagLabHomePage.InventoryComponent.ClickOnAddtoCart(inventoryName));

            this.SoftAssertions.AreEqual(addToCartNumberCheck, InventoryNames.Count, swagLabHomePage.HeaderComponent.GetCountOfAddedItemsToCart(), 
                "Same number of items are not displayed in Add To Cart icon");
            var addToCartPage = swagLabHomePage.HeaderComponent.ClickOnAddToCartIcon();
            var actualInventoryNames = addToCartPage.GetListOfItemsAddedToCart();

            this.SoftAssertions.AreEqual(numberOfItemsInCartCheck, InventoryNames.Count, actualInventoryNames.Count(), 
                "Same number of items are not displayed in Add To Cart");
            foreach (var inventoryName in InventoryNames)
            {
                var itemInCartPageCheck = new AssertCheck($"Verify added item - {inventoryName} is displayed in the cart page");
                this.SoftAssertions.AddAssertions(itemInCartPageCheck);
                this.SoftAssertions.IsTrue(itemInCartPageCheck, actualInventoryNames.Any(name => name.Equals(inventoryName)), 
                    $"Expected {inventoryName} is not displayed in Add To Cart page.");
            }

            InventoryNames.ForEach(inventoryName => addToCartPage.ClickOnRemoveItemFromCart(inventoryName));
            this.SoftAssertions.IsFalse(emptyCartCheck, addToCartPage.IsItemsDisplayedInCart(), "Still items are getting displayed in the Cart.");
        }

        [TestMethod]
        [TestCategory("AddToCart")]
        public void HomePageRemoveButtonFunctionalityTest()
        {
            var addToCartNumberCheck = new AssertCheck("Verify same number of added items are getting displayed in the Cart icon.");
            var emptyCartCountCheck = new AssertCheck("Verify items count in Add To Cart icon is not getting displayed.");
            this.SoftAssertions.AddAssertions(addToCartNumberCheck, emptyCartCountCheck);

            List<string> InventoryNames = new List<string>() { "Sauce Labs Backpack", "Sauce Labs Bolt T-Shirt", "Sauce Labs Fleece Jacket" };
            var swagLabHomePage = this.GetHomePage<SwagLabsHomePage>();

            InventoryNames.ForEach(inventoryName => swagLabHomePage.InventoryComponent.ClickOnAddtoCart(inventoryName));
            this.SoftAssertions.AreEqual(addToCartNumberCheck, InventoryNames.Count, swagLabHomePage.HeaderComponent.GetCountOfAddedItemsToCart(),
                "Same number of items are not displayed in Add To Cart icon");

            InventoryNames.ForEach(inventoryName => swagLabHomePage.InventoryComponent.ClickOnRemoveItemButton(inventoryName));
            this.SoftAssertions.IsFalse(emptyCartCountCheck, swagLabHomePage.HeaderComponent.IsCountOfItemsToCartDisplayed(), 
                "Still items count is getting displayed in the Cart.");
        }
    }
}
