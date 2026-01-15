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
        [Category("Education")]
        public void AddEducationsFromJson()
        {
            
            _educationComponent.ClearAllEducations();

            var data = JsonReader.ReadJson<Education_Model>("TestData/Education.json");

            foreach (var edu in data.Educations)
            {
                _educationComponent.AddEducation(edu.University, edu.Country, edu.Title, edu.Degree, edu.GraduationYear);

                if (edu.Type == "Positive")
                {
                    string msg = _educationComponent.GetSuccessMessage();
                    Assert.IsTrue(msg.Contains("Education has been added"));
                }
                else if (edu.Type == "Negative")
                {
                    string msg = _educationComponent.GetValidationMessage();
                    Assert.IsTrue(msg.Contains("Please enter all the fields"));
                }
                else if (edu.Type == "SQLInjection")
                {
                    TestContext.WriteLine($"Tested SQL input: {edu.University}");
                }
            }
        }
    }
}
