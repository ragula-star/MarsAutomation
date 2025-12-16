using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages.Components;
using MarsAutomation.Utilities;
using NUnit.Framework;

namespace MarsAutomation.Tests
{
    public class LanguageTest : TestsHooks
    {
        private LanguageComponent _languageComponent;

        [SetUp]
        public void Setup() => _languageComponent = new LanguageComponent(driver, wait);

        [Test]
        public void AddLanguagesFromJson()
        {
            var data = JsonReader.ReadJson<Language_Model>("TestData/Languages.json");

            foreach (var lang in data.Languages)
                _languageComponent.AddLanguage(lang.Name, lang.Level);
        }
    }
}
