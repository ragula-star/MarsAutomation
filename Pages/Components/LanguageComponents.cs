using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAutomation.Pages.Components
{
    public class LanguageComponent
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        private By LanguagesTab => By.XPath("//a[text()='Languages']");
        private By AddNewLanguageBtn => By.XPath("//div[contains(@class,'ui teal button') and text()='Add New']");
        private By LanguageInput => By.Name("name");
        private By LevelDropdown => By.Name("level");
        private By AddButton => By.XPath("//input[@value='Add']");

        public LanguageComponent(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
        }

        public void AddLanguage(string name, string level)
        {
            _wait.WaitForElementClickable(LanguagesTab).Click();
            _wait.WaitForElementClickable(AddNewLanguageBtn).Click();
            _wait.WaitForElementVisible(LanguageInput).SendKeys(name);

            new SelectElement(driver.FindElement(LevelDropdown)).SelectByText(level);
            _wait.WaitForElementClickable(AddButton).Click();
        }
    }
}
