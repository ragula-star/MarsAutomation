using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages;
using NUnit.Framework;
using MarsAutomation.Utilities;

namespace MarsAutomation.Tests
{
    [TestFixture]
    public class AboutMeTest : TestsHooks
    {
        private AboutMePages aboutMePage;

        [SetUp]
        public void Setup()
        {
            aboutMePage = new AboutMePages(driver, wait);
            aboutMePage.GoToProfile();
        }

       
       
        [Test]
        [Category("AboutMe")]
        public void AboutMeDropdownTest()
        {
            var data = JsonReader.ReadJson<AboutMe_Model>("TestData/AboutMe.json");

            foreach (var field in data.AboutMeFields)
            {
                aboutMePage.AboutMe.SetDropdownField(field.FieldName, field.Value);
                string actualValue = aboutMePage.AboutMe.GetDropdownValue(field.FieldName);

                Assert.AreEqual(field.Value, actualValue, $"Field '{field.FieldName}' was not updated correctly.");
            }
        }

    }

}
