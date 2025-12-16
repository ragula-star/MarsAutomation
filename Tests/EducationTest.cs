using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages.Components;
using MarsAutomation.Utilities;
using NUnit.Framework;

namespace MarsAutomation.Tests
{
    public class EducationTest : TestsHooks
    {
        private EducationComponent _educationComponent;

        [SetUp]
        public void Setup() => _educationComponent = new EducationComponent(driver, wait);

        [Test]
        public void AddEducationsFromJson()
        {
            var data = JsonReader.ReadJson<Education_Model>("TestData/Education.json");

            foreach (var edu in data.Educations)
            {
                _educationComponent.AddEducation(edu.University, edu.Country, edu.Title, edu.Degree, edu.GraduationYear);
            }
        }
    }
}
