using AutomationTestingFramework.PageComponents.Pages;
using AutomationTestingFramework.Utilities;
using AutomationTestingFramework.Utilities.Enum;
using System.Text;

namespace AutomationTestingFramework.Tests
{
    public class BaseTests
    {
        public TestContext TestContext { get; set; }

        public string ApplicationName;

        private SoftAssertions _softAssertions;

        private ScreenShotTaker _screenShotTaker;

        public ScreenShotTaker ScreenShotTaker
        {
            get
            {
                this._screenShotTaker = this._screenShotTaker ?? new ScreenShotTaker(TestContext);
                return this._screenShotTaker;
            }

            set
            {
                this._screenShotTaker = value;
            }
        }

        public SoftAssertions SoftAssertions
        {
            get
            {
                this._softAssertions = this._softAssertions ?? new SoftAssertions(TestContext);
                return this._softAssertions;
            }

            set
            {
                this._softAssertions = value;
            }
        }

        [TestInitialize]
        public virtual void InitSetUp()
        {
            DriverExtensions.GetWebDriver().NavigateToApplicationUrl(ApplicationName);
            DriverExtensions.GetWebDriver().WaitForPageLoad();
        }

        public T GetHomePage<T>() where T : BasePage
        {
            return DriverExtensions.GetWebDriver().CreatePageInstance<T>();
        }

        [TestCleanup]
        public virtual void TestCaseCleanUp()
        {
            try
            {
                if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
                {
                    ScreenShotTaker.TakeScreenShot();
                }

                if (SoftAssertions.Asserts.Any(x => x.AssertOutcome.Equals(AssertOutcome.Failed) || x.AssertOutcome.Equals(AssertOutcome.Inconclusive)))
                {
                    var finalMessage = new StringBuilder();
                    var failedAssertions = SoftAssertions.Asserts.FindAll(x => !x.AssertOutcome.Equals(AssertOutcome.Passed));
                    foreach (var failedAssert in failedAssertions)
                    {
                        finalMessage.AppendLine(failedAssert.ToString());
                    }

                    if (failedAssertions.Any(x => x.AssertOutcome.Equals(AssertOutcome.Failed)))
                    {
                        Assert.Fail(finalMessage.ToString());
                    }
                    Assert.Inconclusive(finalMessage.ToString());
                }
            }
            finally
            {
                DriverExtensions.GetWebDriver().CloseBrowser();
            }
        }
    }
}
