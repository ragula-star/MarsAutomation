using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages.Components;
using MarsAutomation.Utilities;
using NUnit.Framework;

namespace MarsAutomation.Tests
{
    public class SkillTest : TestsHooks
    {
        private SkillComponent _skillComponent;

        [SetUp]
        public void Setup() => _skillComponent = new SkillComponent(driver, wait);

        [Test]
        [Category("Skill")]
        public void SkillsJsonTest()
        {
            var data = JsonReader.ReadJson<Skill_Model>("TestData/Skills.json");

            foreach (var skill in data.Skills)
            {
                if (skill.Type == "Positive" || skill.Type == "SQLInjection" || skill.Type == "XSS")
                {
                    _skillComponent.AddSkill(skill.Name, skill.Level);
                }
                else if (skill.Type == "Edit")
                {
                    _skillComponent.EditSkill(skill.OriginalName, skill.UpdatedName, skill.UpdatedLevel);
                }
                else if (skill.Type == "Delete")
                {
                    _skillComponent.DeleteSkill(skill.Name);
                }
            }
            
            var currentSkills = _skillComponent.GetAllSkills();
            foreach (var skill in data.Skills.Where(s => s.Type == "Positive"))
            {
                Assert.IsTrue(currentSkills.Any(s => s.Contains(skill.Name)), $"Skill '{skill.Name}' not found in table.");
            }
        }
    }
}

