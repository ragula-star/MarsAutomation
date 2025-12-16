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
        public void AddSkillsFromJson()
        {
            var data = JsonReader.ReadJson<Skill_Model>("TestData/Skills.json");

            foreach (var skill in data.Skills)
            {
                _skillComponent.AddSkill(skill.Name, skill.Level);
            }
        }
    }
}

