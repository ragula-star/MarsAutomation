using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAutomation.Pages.Components
{
    public class SkillComponent
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        private By SkillsTab => By.XPath("//a[@data-tab='second' and text()='Skills']");
        private By AddNewSkillBtn => By.XPath("//div[@class='ui teal button' and text()='Add New']");
        private By SkillInput => By.XPath("//input[@name='name']");
        private By SkillLevelDropdown => By.XPath("//select[@name='level']");
        private By AddSkillBtn => By.XPath("//input[@value='Add']");
        private By SkillRows => By.XPath("//table[@class='ui fixed table']//tbody/tr");
        private By DeleteButtons => By.XPath("//table[@class='ui fixed table']//tbody/tr/td[3]/span/i[@class='remove icon']");
        private By SuccessMessage => By.XPath("//div[contains(@class,'ns-box') and contains(text(),'has been added')]");
        private By ValidationMessage => By.XPath("//div[contains(@class,'ns-box') and contains(text(),'Please enter skill')]");
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

        public void EditSkill(string originalName, string updatedName, string updatedLevel)
        {
            _wait.WaitForElementClickable(SkillsTab).Click();
            var rows = driver.FindElements(SkillRows);

            foreach (var row in rows)
            {
                var skillName = row.FindElement(By.XPath("./td[1]")).Text.Trim();
                if (skillName.Equals(originalName, StringComparison.OrdinalIgnoreCase))
                {
                    
                    row.FindElement(By.XPath(".//i[contains(@class,'outline write icon')]/..")).Click();

                    
                    var input = driver.FindElement(SkillInput);
                    input.Clear();
                    input.SendKeys(updatedName);

                    new SelectElement(driver.FindElement(SkillLevelDropdown)).SelectByText(updatedLevel);

                    
                    _wait.WaitForElementClickable(AddSkillBtn).Click();
                    Thread.Sleep(500); 
                    return;
                }
            }

            throw new Exception($"Skill '{originalName}' not found to edit.");
        }

        public void DeleteSkill(string name)
        {
            _wait.WaitForElementClickable(SkillsTab).Click();
            var rows = driver.FindElements(SkillRows);

            foreach (var row in rows)
            {
                var skillName = row.FindElement(By.XPath("./td[1]")).Text.Trim();
                if (skillName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    row.FindElement(By.XPath("./td[3]/span/i[@class='remove icon']")).Click();
                    Thread.Sleep(500); 
                    return;
                }
            }

            throw new Exception($"Skill '{name}' not found to delete.");
        }

        public List<string> GetAllSkills()
        {
            var list = new List<string>();
            var rows = driver.FindElements(SkillRows);
            foreach (var row in rows)
            {
                string name = row.FindElement(By.XPath("./td[1]")).Text;
                string level = row.FindElement(By.XPath("./td[2]")).Text;
                list.Add($"{name} | {level}");
            }
            return list;
        }

        public void ClearAllSkills()
        {
            _wait.WaitForElementClickable(SkillsTab).Click();

            var deleteButtons = driver.FindElements(DeleteButtons);

            if (deleteButtons.Count == 0)
            {
               
                return;
            }

            foreach (var btn in deleteButtons)
            {
                try
                {
                    By Addbtn = By.XPath("//input[@value='Add']");
                    _wait.WaitForElementClickable(Addbtn).Click();

                     Thread.Sleep(300);
                }
                catch (StaleElementReferenceException)
                {
                    // DOM refreshed → re-fetch buttons
                    ClearAllSkills();
                    return;
                }
                catch (ElementNotInteractableException)
                {
                    // Skip non-interactable elements
                    continue;
                }
            }
        }


        public string GetSuccessMessage() => _wait.WaitForElementVisible(SuccessMessage).Text;
        public string GetValidationMessage() => _wait.WaitForElementVisible(ValidationMessage).Text;
    }
}
