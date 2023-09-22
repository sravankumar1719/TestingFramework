using AutomationFramework.PageComponents.Enum;
using AutomationFramework.PageComponents.Pages;
using AutomationFramework.PageComponents.Pages.SwagLabs;
using Xunit;
using Xunit.Abstractions;

namespace AutomationFramework.Tests.SwagLabsTests
{
    //[Collection("Serial")]
    public class SwagLabHomePageTests : BaseTests
    {
        private static readonly string applicationName = "SwagLabsUrl";
        private SwagLabsLoginPage swagLabsLoginPage;

        public SwagLabHomePageTests(ITestOutputHelper _outputHelper) : base(applicationName, _outputHelper)
        {
            swagLabsLoginPage = this.GetHomePage<SwagLabsLoginPage>();
            swagLabsLoginPage.LoginToApplication();
        }

        [Fact]
        [Trait("Category", "SwagLabHomePage")]
        public void VerifyHeaderComponentsTest()
        {
            const string AppLogoText = "Swag Labs";

            _outputHelper.WriteLine("Test case : VerifyHeaderComponentsTest()");
            var swagLabHomePage = this.GetHomePage<SwagLabsHomePage>();

            Assert.Equal(AppLogoText, swagLabHomePage.HeaderComponent.GetAppLogoText());
            Assert.True(swagLabHomePage.HeaderComponent.IsShoppingCartLogoDisplayed(), "Shopping cart logo is not geting displayed");
            Assert.True(swagLabHomePage.InventoryComponent.IsDisplayed(), "Inventory component is not geting displayed");
            Assert.True(swagLabHomePage.FooterComponent.IsDisplayed(), "Footer component is not geting displayed");
        }

        [Fact]
        [Trait("Category", "SwagLabHomePage")]
        public void VerifyFooterComponentTest()
        {
            const string FooterText = "© 2023 Sauce Labs. All Rights Reserved. Terms of Service | Privacy Policy";
            var expectedSocialNetworkLinks = new Dictionary<SocialNetworks, string>()
            { 
                {SocialNetworks.Twitter, "https://twitter.com/saucelabs" },
                {SocialNetworks.Facebook, "https://www.facebook.com/saucelabs" },
                {SocialNetworks.LinkedIn, "https://www.linkedin.com/" } 
            };

            _outputHelper.WriteLine("Test case : VerifyFooterComponentTest()");
            var swagLabHomePage = this.GetHomePage<SwagLabsHomePage>();
            Assert.Equal(FooterText, swagLabHomePage.FooterComponent.GetFooterText());

            var actualSocialNetWorkLinks = swagLabHomePage.FooterComponent.GetSocialNetworkLinkText();
            foreach (SocialNetworks socialNetwork in expectedSocialNetworkLinks.Keys)
            {
                Assert.True(actualSocialNetWorkLinks.Any(linkText => linkText.Equals(socialNetwork.GetEnumValue())), 
                    $"'{socialNetwork}' link text is not displayed.");
            }

            foreach (SocialNetworks socialNetwork in expectedSocialNetworkLinks.Keys)
            {
                var basePage = swagLabHomePage.FooterComponent.ClickOnSocialNetworkLinkText<BasePage>(socialNetwork);
                Assert.True(basePage.GetCurrentUrl().Contains(expectedSocialNetworkLinks[socialNetwork]),
                    $"'{socialNetwork}' is not navigating to expected page.");
                basePage.SwitchToDefaultWindow();
            }
        }

        [Fact]
        [Trait("Category", "SwagLabHomePage")]
        public void VerifyMenuItemsTest()
        {
            const string ExpectedUrl = "https://saucelabs.com/";

            var expectedMenuItems = new List<MenuItem> { MenuItem.AllItems, MenuItem.About, MenuItem.Logout, MenuItem.ResetAppState };

            _outputHelper.WriteLine("Test case : VerifyMenuItemsTest()");
            var swagLabHomePage = this.GetHomePage<SwagLabsHomePage>();
            Assert.True(swagLabHomePage.HeaderComponent.IsOpenMenuButtonDisplayed(), "Open menu button is not geting displayed");
            swagLabHomePage.HeaderComponent.ClickOnOpenMenuButton();

            var actualMenuItemList = swagLabHomePage.HeaderComponent.GetMenuItemsList();

            foreach(string menuItem in actualMenuItemList)
            {
                Assert.True(expectedMenuItems.Any(menuItems => menuItems.GetEnumValue().Equals(menuItem)),
                    $"{menuItem}, menu item is not in expected list");
            }

            var basePage = swagLabHomePage.HeaderComponent.ClickOnMenuItem<BasePage>(MenuItem.About);
            Assert.True(basePage.GetCurrentUrl().Contains(ExpectedUrl),
                    $"'{MenuItem.About}' is not navigating to expected page.");
        }
    }
}
