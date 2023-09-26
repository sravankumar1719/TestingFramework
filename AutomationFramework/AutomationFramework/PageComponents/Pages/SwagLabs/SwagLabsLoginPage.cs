using OpenQA.Selenium;

namespace AutomationFramework.PageComponents.Pages.SwagLabs
{
    public class SwagLabsLoginPage : BasePage
    {
        private static readonly By LoginTextBoxLocator = By.Id("user-name");
        private static readonly By PasswordTextBoxLocator = By.Id("password");
        private static readonly By LoginButtonLocator = By.Id("login-button");
        private static readonly By AcceptedUserNamesLocator = By.XPath("//div[@id = 'login_credentials']");
        private static readonly By PasswordForUsersLocator = By.XPath("//div[@class = 'login_password']");

        public void LoginToApplication()
        {
            this.Log.Info("Login to the application.");
            DriverExtensions.GetWebDriver().GetElement(LoginTextBoxLocator).SendKeys(this.GetUserName());
            DriverExtensions.GetWebDriver().GetElement(PasswordTextBoxLocator).SendKeys(this.GetPasswordText());
            DriverExtensions.GetWebDriver().GetElement(LoginButtonLocator).Click();
        }

        public string GetUserName()
        {
            var userNamesList = DriverExtensions.GetWebDriver().GetElement(AcceptedUserNamesLocator).Text.Split("\r\n").Skip(1);
            string userName = userNamesList.ElementAt(0);
            this.Log.Info($"The username to login : {userName}");
            return userName;
        }

        public string GetPasswordText()
        {
            string passwordText = DriverExtensions.GetWebDriver().GetElement(PasswordForUsersLocator).Text.Split("\r\n").Skip(1).First();
            this.Log.Info($"The password to login : {passwordText}");
            return passwordText;
        }
    }
}
