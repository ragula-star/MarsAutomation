using MarsAutomation.Pages.Components;
using MarsAutomation.Utilities;
using OpenQA.Selenium;

namespace MarsAutomation.Pages
{
    public class SearchSkillPage
    {
        private IWebDriver driver;
        private WaitHelpers _wait;

        public SearchSkillComponent SearchSkillComponent { get; }

        private By SearchBox = By.XPath("private By SearchInput = By.XPath(//div[contains(@class,'search-box')]/input[@placeholder='Search skills']");

        public SearchSkillPage(IWebDriver driver, WaitHelpers wait)
        {
            this.driver = driver;
            _wait = wait;
            SearchSkillComponent = new SearchSkillComponent(driver, wait);
        }

       
        public void OpenSearchSkill()
        {
            _wait.WaitForElementClickable(SearchBox).Click();
        }
    }
}

