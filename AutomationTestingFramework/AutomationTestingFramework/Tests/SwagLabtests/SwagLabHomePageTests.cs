using AutomationTestingFramework.PageComponents.Enum;
using AutomationTestingFramework.PageComponents.Pages.SwagLabsPage;
using AutomationTestingFramework.PageComponents.Pages;
using AutomationTestingFramework.Utilities;

namespace AutomationTestingFramework.Tests.SwagLabtests
{
    [TestClass]
    public class SwagLabHomePageTests : BaseTests
    {
        private SwagLabsLoginPage swagLabsLoginPage;

        public SwagLabHomePageTests()
        {
            ApplicationName = "SwagLabsUrl";
        }

        [TestInitialize]
        public override void InitSetUp()
        {
            base.InitSetUp();
            swagLabsLoginPage = this.GetHomePage<SwagLabsLoginPage>();
            swagLabsLoginPage.LoginToApplication();
        }

        [TestMethod]
        [TestCategory("SwagLabHomePage")]
        public void VerifyHeaderComponentsTest()
        {
            var applicationLogoTextCheck = new AssertCheck("Verify Application Logo text.");
            var shoppingCartLogoDisplayCheck = new AssertCheck("Verify Shopping Cart Logo is displayed.");
            var inventoryComponentDisplayCheck = new AssertCheck("Verify Inventory Component is displayed.");
            var footerComponentDisplayCheck = new AssertCheck("Verify Footer Component is displayed.");
            this.SoftAssertions.AddAssertions(applicationLogoTextCheck, shoppingCartLogoDisplayCheck, inventoryComponentDisplayCheck, footerComponentDisplayCheck);

            const string AppLogoText = "Swag Labs";
            var swagLabHomePage = this.GetHomePage<SwagLabsHomePage>();

            this.SoftAssertions.AreEqual(applicationLogoTextCheck, AppLogoText, swagLabHomePage.HeaderComponent.GetAppLogoText(), "Application logo text is not matching.");
            this.SoftAssertions.IsTrue(shoppingCartLogoDisplayCheck, swagLabHomePage.HeaderComponent.IsShoppingCartLogoDisplayed(), "Shopping cart logo is not geting displayed");
            this.SoftAssertions.IsTrue(inventoryComponentDisplayCheck, swagLabHomePage.InventoryComponent.IsDisplayed(), "Inventory component is not geting displayed");
            this.SoftAssertions.IsTrue(footerComponentDisplayCheck, swagLabHomePage.FooterComponent.IsDisplayed(), "Footer component is not geting displayed");
        }

        [TestMethod]
        [TestCategory("SwagLabHomePage")]
        public void VerifyFooterComponentTest()
        {
            var footerTextCheck = new AssertCheck("Verify Footer text is displayed.");
            this.SoftAssertions.AddAssertions(footerTextCheck);

            const string FooterText = "© 2023 Sauce Labs. All Rights Reserved. Terms of Service | Privacy Policy";
            var expectedSocialNetworkLinks = new Dictionary<SocialNetworks, string>()
            {
                {SocialNetworks.Twitter, "https://twitter.com/saucelabs" },
                {SocialNetworks.Facebook, "https://www.facebook.com/saucelabs" },
                {SocialNetworks.LinkedIn, "https://www.linkedin.com/" }
            };

            var swagLabHomePage = this.GetHomePage<SwagLabsHomePage>();
            this.SoftAssertions.AreEqual(footerTextCheck, FooterText, swagLabHomePage.FooterComponent.GetFooterText(), "Footer text is not matching.");

            var actualSocialNetWorkLinks = swagLabHomePage.FooterComponent.GetSocialNetworkLinkText();
            foreach (SocialNetworks socialNetwork in expectedSocialNetworkLinks.Keys)
            {
                var socialNetworkLinkDisplayCheck = new AssertCheck($"Verify {socialNetwork} link text is displayed.");
                this.SoftAssertions.AddAssertions(socialNetworkLinkDisplayCheck);

                this.SoftAssertions.IsTrue(socialNetworkLinkDisplayCheck, actualSocialNetWorkLinks.Any(linkText => linkText.Equals(socialNetwork.GetEnumValue())),
                    $"'{socialNetwork}' link text is not displayed.");
            }

            foreach (SocialNetworks socialNetwork in expectedSocialNetworkLinks.Keys)
            {
                var socialNetworkNavigationCheck = new AssertCheck($"Verify navigating page for '{socialNetwork}' link.");
                this.SoftAssertions.AddAssertions(socialNetworkNavigationCheck);

                var basePage = swagLabHomePage.FooterComponent.ClickOnSocialNetworkLinkText<BasePage>(socialNetwork);
                this.SoftAssertions.IsTrue(socialNetworkNavigationCheck, basePage.GetCurrentUrl().Contains(expectedSocialNetworkLinks[socialNetwork]),
                    $"'{socialNetwork}' is not navigating to expected page.");
                basePage.SwitchToDefaultWindow();
            }
        }

        [TestMethod]
        [TestCategory("SwagLabHomePage")]
        public void VerifyMenuItemsTest()
        {
            var openMenuButtonDisplayCheck = new AssertCheck("Verify Open Menu button is displayed.");
            var aboutMenuItemCheck = new AssertCheck("Verify clicking on About item navigating to expected page.");
            this.SoftAssertions.AddAssertions(openMenuButtonDisplayCheck, aboutMenuItemCheck);

            const string ExpectedUrl = "https://saucelabs.com/";

            var expectedMenuItems = new List<MenuItem> { MenuItem.AllItems, MenuItem.About, MenuItem.Logout, MenuItem.ResetAppState };

            var swagLabHomePage = this.GetHomePage<SwagLabsHomePage>();
            this.SoftAssertions.IsTrue(openMenuButtonDisplayCheck, swagLabHomePage.HeaderComponent.IsOpenMenuButtonDisplayed(), 
                "Open menu button is not geting displayed");
            swagLabHomePage.HeaderComponent.ClickOnOpenMenuButton();

            var actualMenuItemList = swagLabHomePage.HeaderComponent.GetMenuItemsList();

            foreach (string menuItem in actualMenuItemList)
            {
                var menuItemCheck = new AssertCheck($"Verify '{menuItem}' menu item is displayed or not.");
                this.SoftAssertions.AddAssertions(menuItemCheck);
                this.SoftAssertions.IsTrue(menuItemCheck, expectedMenuItems.Any(menuItems => menuItems.GetEnumValue().Equals(menuItem)),
                    $"{menuItem}, menu item is not in expected list");
            }

            var basePage = swagLabHomePage.HeaderComponent.ClickOnMenuItem<BasePage>(MenuItem.About);
            this.SoftAssertions.IsTrue(aboutMenuItemCheck, basePage.GetCurrentUrl().Contains(ExpectedUrl),
                    $"'{MenuItem.About}' is not navigating to expected page.");
        }
    }
}
