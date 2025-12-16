using MarsAutomation.Utilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Pages
{
    public class Loginpage
    {
        IWebDriver driver;
        WaitHelpers waitHelpers;

        public Loginpage(IWebDriver driver, WaitHelpers waitHelpers)
        {
            this.driver = driver;
            this.waitHelpers = waitHelpers;
        }
        private By Clicksign => By.XPath("//a[text()='Sign In']");
        private By EmailInput => By.XPath("//input[@placeholder='Email address']");
        private By PasswordInput => By.XPath("//input[@placeholder='Password']");
        private By LoginButton => By.XPath("//button[text()='Login']");
        private By Logocheck => By.XPath("//a[@href='/' and contains(text(),'Mars Logo')]");
        private By InvalidEmailAlert => By.CssSelector("div.ui.basic.red.pointing.prompt.label.transition.visible");
        private By InvalidPasswordAlert => By.XPath("//div[text()='Password must be at least 6 characters']");

        public void Clicklogin()
        {
            waitHelpers.WaitForElementClickable(Clicksign).Click();

        }
        public void Emaillogin(string Email, string Password)
        {
            var emailField = waitHelpers.WaitForElementClickable(EmailInput);
            emailField.Clear();
            emailField.SendKeys(Email);

            var passwordField = waitHelpers.WaitForElementClickable(PasswordInput);
            passwordField.Clear();
            passwordField.SendKeys(Password);

            var loginButton = waitHelpers.WaitForElementClickable(LoginButton);
            loginButton.Click();

        }
        public bool verifylogo()
        {
            return waitHelpers.WaitForElementVisible(Logocheck).Displayed;
        }
        public string invalidEmailText()
        {


            try
            {
                var elements = driver.FindElements(InvalidEmailAlert);
                if (elements.Count > 0)
                    return elements[0].Text;
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }

        }
        public string InvalidpasswordText()
        {

            try
            {
                var elements = driver.FindElements(InvalidPasswordAlert);
                if (elements.Count > 0)
                    return elements[0].Text;
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }

        }

        public void ClearFields()
        {
            waitHelpers.WaitForElementVisible(EmailInput).Clear();
            waitHelpers.WaitForElementVisible(PasswordInput).Clear();
        }

    }
    
}
