using AutomationFramework.PageComponents.Pages;
using AutomationFramework.Utilities;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AutomationFramework.Tests
{
    public class BaseTests : IDisposable
    {
        //public ITestResultMessage TestContext { get; set; }
        public ITestOutputHelper _outputHelper;

        public BaseTests(string applicationName, ITestOutputHelper outputHelper)
        {
            DriverExtensions.GetWebDriver().NavigateToApplicationUrl(applicationName);
            _outputHelper = outputHelper;
        }

        public T GetHomePage<T>() where T: BasePage
        {
            return Activator.CreateInstance<T>();
        }

        public void Dispose()
        {
            DriverExtensions.GetWebDriver().CloseBrowser();
            
            //try
            //{
            //    if (TestContext.Output) == UnitTestOutcome.Failed)
            //    {
            //        ScreenShotTaker.TakeScreenShot();
            //    }

            //    if (SoftAssertions.Assertions.Any(x => x.Outcome.Equals(Outcome.Failed) || x.Outcome.Equals(Outcome.Inconclusive)))
            //    {
            //        var finalMessage = new StringBuilder();
            //        var failedAssertions = SoftAssertions.Assertions.FindAll(x => !x.Outcome.Equals(Outcome.Passed));
            //        foreach (var failedAssert in failedAssertions)
            //        {
            //            finalMessage.AppendLine(failedAssert.ToString());
            //        }

            //        if (failedAssertions.Any(x => x.Outcome.Equals(Outcome.Failed)))
            //        {
            //            Assert.Fail(finalMessage.ToString());
            //        }
            //        Assert.Inconclusive(finalMessage.ToString());
            //    }
            //}
            //finally
            //{
            //    DriverExtensions.CloseBrowser();
            //}
        }
    }
}