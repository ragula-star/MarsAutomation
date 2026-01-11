using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages;
using MarsAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace MarsAutomation.Tests
{
    [TestFixture]
    public class SearchSkillTests : TestsHooks
    {
        private SearchSkillPage searchSkillPage;

        [SetUp]
        public void TestSetup()
        {
            
            searchSkillPage = new SearchSkillPage(driver, wait);
        }

        [Test]
        [Category("SearchSkill")]
        public void ValidateSkillsFromJson()
        {
            
            var skillsFromJson = JsonReader
                .ReadJson<SearchSkill_Model>("TestData/searchSkill.json")
                .Skills;

            foreach (var skillData in skillsFromJson)
            {
                string skill = skillData.Skill;

                TestContext.WriteLine($"Searching for skill: {skill}");

                
                searchSkillPage.SearchSkillComponent.SearchSkill(skill);

                
                if (skillData.Type.ToLower() == "positive")
                {
                    try
                    {
                        searchSkillPage.SearchSkillComponent.ClickSkillCard(skill);
                        TestContext.WriteLine($"Skill card clicked: {skill}");
                    }
                    catch (Exception ex)
                    {
                        TestContext.WriteLine($"Skill card not found: {skill} - {ex.Message}");
                    }
                }
                else
                {
                    
                    var message = searchSkillPage.SearchSkillComponent.GetEmptyMessage();
                    TestContext.WriteLine($"Message shown for invalid skill '{skill}': {message}");
                    Assert.That(message.ToLower().Contains("no skills found"), $"Unexpected message for skill {skill}");
                }
            }
        }



    }
}

