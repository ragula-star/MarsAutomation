using MarsAutomation.Pages.Components;
using MarsAutomation.Utilities;
using OpenQA.Selenium;

namespace MarsAutomation.Pages
{
    public class ProfilePage
    {
        public LanguageComponent Languages { get; }
        public SkillComponent Skills { get; }
        public EducationComponent Educations { get; }
        public CertificationComponent Certifications { get; }

        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        public ProfilePage(IWebDriver driver, WaitHelpers wait)
        {
            this.driver = driver;
            this._wait = wait;


            Languages = new LanguageComponent(driver, wait);
            Skills = new SkillComponent(driver, wait);
            Educations = new EducationComponent(driver, wait);
            Certifications = new CertificationComponent(driver, wait);
        }

        private By profileMenu => By.XPath("//a[text()='Profile']");
        private By languageTab => By.XPath("//a[text()='Languages']");
        private By skillsTab => By.XPath("//a[text()='Skills']");
        private By educationTab => By.XPath("//a[text()='Education']");
        private By certificationTab => By.XPath("//a[text()='Certifications']");

        public void GoToProfile() => _wait.WaitForElementClickable(profileMenu).Click();
        public void GoToLanguageTab() => _wait.WaitForElementClickable(languageTab).Click();
        public void GoToSkillsTab() => _wait.WaitForElementClickable(skillsTab).Click();
        public void GoToEducationTab() => _wait.WaitForElementClickable(educationTab).Click();
        public void GoToCertificationTab() => _wait.WaitForElementClickable(certificationTab).Click();
    }

}
