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
        [Category("Language")]
        public void AddLanguagesFromJson()
        {
            var data = JsonReader.ReadJson<Language_Model>("TestData/Languages.json");

            foreach (var lang in data.Languages)
            {
                profilePage.GoToProfile();

                
                profilePage.Languages.AddLanguage(lang.Name, lang.Level);

               
                if (lang.Type == "Positive")
                {
                    string msg = profilePage.Languages.GetSuccessMessage();
                    Assert.That(msg, Does.Contain($"{lang.Name} has been added to your languages"),
                                $"Positive test failed for {lang.Name}");
                }
               
                else if (lang.Type == "Negative")
                {
                    string msg = profilePage.Languages.GetValidationMessage();
                    Assert.That(msg, Does.Contain("Please enter language and level"),
                                $"Negative test failed for empty input");
                }
                
                else if (lang.Type == "SQLInjection" || lang.Type == "XSS")
                {
                    var allLanguages = profilePage.Languages.GetAllLanguages();
                    Assert.That(allLanguages, Does.Not.Contain($"{lang.Name} | {lang.Level}"),
                                $"{lang.Type} input should not be added to table");
                }
            }
        }

    }
}
