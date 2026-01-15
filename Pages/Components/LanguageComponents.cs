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
        private By ValidationMessage => By.XPath("//div[contains(@class,'ns-box') and contains(text(),'Please enter')]");
        private By SuccessMessage => By.XPath("//div[contains(@class,'ns-box') and contains(text(),'has been added')]");
        private By LanguageRows => By.XPath("//table[@class='ui fixed table']//tbody/tr");
        private By DeleteButtons => By.XPath("//table[@class='ui fixed table']//tbody/tr/td[3]/span/i[@class='remove icon']");

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

        public string GetValidationMessage()
        {
            return _wait.WaitForElementVisible(ValidationMessage).Text;
        }

        public string GetSuccessMessage() => _wait.WaitForElementVisible(SuccessMessage).Text;

        public List<string> GetAllLanguages()
        {
            var list = new List<string>();
            var rows = driver.FindElements(LanguageRows);
            foreach (var row in rows)
            {
                string name = row.FindElement(By.XPath("./td[1]")).Text;
                string level = row.FindElement(By.XPath("./td[2]")).Text;
                list.Add($"{name} | {level}");
            }
            return list;
        }

        public void ClearAllLanguages()
        {
            _wait.WaitForElementClickable(LanguagesTab).Click();

            var buttons = driver.FindElements(DeleteButtons);
            foreach (var btn in buttons)
            {
                btn.Click();
               
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
