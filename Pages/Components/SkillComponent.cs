using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAutomation.Pages.Components
{
    public class SkillComponent
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        private By SkillsTab => By.XPath("//a[text()='Skills']");
        private By AddNewSkillBtn => By.XPath("//div[@class='ui teal button' and text()='Add New']");
        private By SkillInput => By.XPath("//input[@name='name']");
        private By SkillLevelDropdown => By.XPath("//select[@name='level']");
        private By AddSkillBtn => By.XPath("//input[@value='Add']");

        public SkillComponent(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
        }

        public void AddSkill(string name, string level)
        {
            _wait.WaitForElementClickable(SkillsTab).Click();
            _wait.WaitForElementClickable(AddNewSkillBtn).Click();
            _wait.WaitForElementVisible(SkillInput).SendKeys(name);
            new SelectElement(driver.FindElement(SkillLevelDropdown)).SelectByText(level);
            _wait.WaitForElementClickable(AddSkillBtn).Click();
        }
    }
}
