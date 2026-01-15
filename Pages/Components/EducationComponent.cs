using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MarsAutomation.Pages.Components
{
    public class EducationComponent
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        private By EducationTab => By.XPath("//a[@data-tab='third' and text()='Education']");
        private By AddNewEducationBtn => By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/thead/tr/th[6]/div");
        private By UniversityInput => By.XPath("//input[@placeholder='College/University Name']");
        private By CountryDropdown => By.Name("country");
        private By TitleDropdown => By.Name("title");
        private By DegreeInput => By.XPath("//input[@placeholder='Degree']");
        private By YearDropdown => By.Name("yearOfGraduation");
        private By AddEducationBtn => By.XPath("//input[@value='Add']");
        private By DeleteButtons => By.XPath("//table[@class='ui fixed table']//tbody/tr/td[6]/span/i[@class='remove icon']");
        private By EducationRows => By.XPath("//table[@class='ui fixed table']//tbody/tr");

        private By SuccessMessage => By.XPath("//div[contains(@class,'ns-box') and contains(text(),'Education has been added')]");
        private By ValidationMessage => By.XPath("//div[contains(@class,'ns-box') and contains(text(),'Please enter all the fields')]");
        public EducationComponent(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
        }

        public void AddEducation(string university, string country, string title, string degree, string year)
        {
            _wait.WaitForElementClickable(EducationTab).Click();
            _wait.WaitForElementClickable(AddNewEducationBtn).Click();

            
            if (!string.IsNullOrEmpty(university))
                _wait.WaitForElementVisible(UniversityInput).SendKeys(university);

            if (!string.IsNullOrEmpty(degree))
                _wait.WaitForElementVisible(DegreeInput).SendKeys(degree);

            
            if (!string.IsNullOrEmpty(country))
            {
                var countryDropdown = new SelectElement(driver.FindElement(CountryDropdown));
                var option = countryDropdown.Options.FirstOrDefault(o => o.Text.Trim() == country.Trim());
                if (option != null)
                    countryDropdown.SelectByText(option.Text);
                else
                    Console.WriteLine($"Country '{country}' not found, skipping selection.");
            }

            if (!string.IsNullOrEmpty(title))
            {
                var titleDropdown = new SelectElement(driver.FindElement(TitleDropdown));
                var option = titleDropdown.Options.FirstOrDefault(o => o.Text.Trim() == title.Trim());
                if (option != null)
                    titleDropdown.SelectByText(option.Text);
                else
                    Console.WriteLine($"Title '{title}' not found, skipping selection.");
            }

            if (!string.IsNullOrEmpty(year))
            {
                var yearDropdown = new SelectElement(driver.FindElement(YearDropdown));
                var option = yearDropdown.Options.FirstOrDefault(o => o.Text.Trim() == year.Trim());
                if (option != null)
                    yearDropdown.SelectByText(option.Text);
                else
                    Console.WriteLine($"Year '{year}' not found, skipping selection.");
            }

            _wait.WaitForElementClickable(AddEducationBtn).Click();
        }


        public void ClearAllEducations()
        {
            _wait.WaitForElementClickable(EducationTab).Click();

            var deleteButtons = driver.FindElements(DeleteButtons);

            while (deleteButtons.Count > 0)
            {
                try
                {
                    deleteButtons[0].Click();
                    System.Threading.Thread.Sleep(500); 
                    deleteButtons = driver.FindElements(DeleteButtons); 
                }
                catch (StaleElementReferenceException)
                {
                    deleteButtons = driver.FindElements(DeleteButtons); 
                }
                catch (ElementNotInteractableException)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", deleteButtons[0]);
                    deleteButtons[0].Click();
                    System.Threading.Thread.Sleep(500);
                    deleteButtons = driver.FindElements(DeleteButtons);
                }
            }
        }


        public List<string> GetAllEducations()
        {
            var list = new List<string>();
            var rows = driver.FindElements(EducationRows);
            foreach (var row in rows)
            {
                string uni = row.FindElement(By.XPath("./td[1]")).Text;
                string country = row.FindElement(By.XPath("./td[2]")).Text;
                string title = row.FindElement(By.XPath("./td[3]")).Text;
                string degree = row.FindElement(By.XPath("./td[4]")).Text;
                string year = row.FindElement(By.XPath("./td[5]")).Text;
                list.Add($"{uni} | {country} | {title} | {degree} | {year}");
            }
            return list;
        }

        public string GetSuccessMessage() => _wait.WaitForElementVisible(SuccessMessage).Text;
        public string GetValidationMessage() => _wait.WaitForElementVisible(ValidationMessage).Text;
    }
}
