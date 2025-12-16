using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAutomation.Pages.Components
{
    public class EducationComponent
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        private By EducationTab => By.XPath("//a[text()='Education']");
        private By AddNewEducationBtn => By.XPath("//div[contains(@class,'ui teal button') and text()='Add New']");
        private By UniversityInput => By.XPath("//input[@placeholder='College/University Name']");
        private By CountryDropdown => By.Name("country");
        private By TitleDropdown => By.Name("title");
        private By DegreeInput => By.XPath("//input[@placeholder='Degree']");
        private By YearDropdown => By.Name("yearOfGraduation");
        private By AddEducationBtn => By.XPath("//input[@value='Add']");

        public EducationComponent(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
        }

        public void AddEducation(string university, string country, string title, string degree, string year)
        {
            _wait.WaitForElementClickable(EducationTab).Click();
            _wait.WaitForElementClickable(AddNewEducationBtn).Click();

            _wait.WaitForElementVisible(UniversityInput).SendKeys(university);
            new SelectElement(driver.FindElement(CountryDropdown)).SelectByText(country);
            new SelectElement(driver.FindElement(TitleDropdown)).SelectByText(title);
            _wait.WaitForElementVisible(DegreeInput).SendKeys(degree);
            new SelectElement(driver.FindElement(YearDropdown)).SelectByText(year);

            _wait.WaitForElementClickable(AddEducationBtn).Click();
        }
    }
}
