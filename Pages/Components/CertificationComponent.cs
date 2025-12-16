using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAutomation.Pages.Components
{
    public class CertificationComponent
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        private By CertificationsTab => By.XPath("//a[@data-tab='fourth']");
        private By AddNewCertificationBtn => By.XPath("//div[contains(@class,'ui teal button') and text()='Add New']");
        private By CertificationName => By.Name("certificationName");
        private By CertificationFrom => By.Name("certificationFrom");
        private By CertificationYear => By.Name("certificationYear");
        private By AddCertificationBtn => By.XPath("//input[@value='Add']");

        public CertificationComponent(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
        }

        public void AddCertification(string name, string from, string year)
        {
            _wait.WaitForElementClickable(CertificationsTab).Click();
            _wait.WaitForElementClickable(AddNewCertificationBtn).Click();
            _wait.WaitForElementVisible(CertificationName).SendKeys(name);
            _wait.WaitForElementVisible(CertificationFrom).SendKeys(from);
            new SelectElement(driver.FindElement(CertificationYear)).SelectByText(year);
            _wait.WaitForElementClickable(AddCertificationBtn).Click();
        }
    }
}
