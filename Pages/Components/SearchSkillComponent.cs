using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsAutomation.Pages.Components
{
    public class SearchSkillComponent
    {
        private IWebDriver driver;
        private WaitHelpers _wait;

        
        private By SearchInput = By.XPath("//div[@class='ui small icon input search-box']/input");
        private By SkillCards = By.XPath("//div[@class='ui card']");
        private By EmptyMessageLocator = By.XPath("//div[contains(text(),'No skills found')]");

        public SearchSkillComponent(IWebDriver driver, WaitHelpers wait)
        {
            this.driver = driver;
            _wait = wait;
        }

        public void SearchSkill(string skill)
        {
            var input = _wait.WaitForElementVisible(SearchInput);
            input.Clear();
            input.SendKeys(skill + Keys.Enter);
        }

        public void ClickSkillCard(string skill)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            
            wait.Until(d => d.FindElements(SkillCards).Count > 0);

            var cards = driver.FindElements(SkillCards);

            foreach (var card in cards)
            {
                var skillText = card.FindElement(By.XPath(".//div[@class='extra content']//em")).Text;
                var skills = skillText.Replace("Skills:", "").Split(',')
                                      .Select(s => s.Trim().ToLower())
                                      .ToList();

                if (skills.Contains(skill.ToLower()))
                {
                    card.FindElement(By.XPath(".//a[@class='service-info']")).Click();
                    Console.WriteLine($"Skill card clicked: {skill}");
                    return;
                }
            }

            Console.WriteLine($"Skill card not found: {skill}");
        }
        public List<string> GetAllSkillsFromCards()
        {
            var skillsList = new List<string>();
            var cards = driver.FindElements(SkillCards);

            foreach (var card in cards)
            {
                var skillText = card.FindElement(By.XPath(".//div[@class='extra content']//em")).Text;
                if (skillText.Contains("Skills:"))
                {
                    var skills = skillText.Replace("Skills:", "").Split(',').Select(s => s.Trim().ToLower());
                    skillsList.AddRange(skills);
                }
            }

            return skillsList;
        }

        
        public string GetEmptyMessage()
        {
            return _wait.WaitForElementVisible(EmptyMessageLocator).Text;
        }
    }
}

