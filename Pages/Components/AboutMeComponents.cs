using MarsAutomation.Models;  // Add this at the top of your test/component file
using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;



namespace MarsAutomation.Pages.Components
{
    public class AboutMeComponent
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        public AboutMeComponent(IWebDriver driver, WaitHelpers wait)
        {
            this.driver = driver;
            this._wait = wait;
        }

        private string GetDropdownName(string fieldName) => fieldName switch
        {
            "Availability" => "availabiltyType",
            "Hours" => "availabiltyHour",
            "Earn Target" => "availabiltyTarget",
            _ => throw new ArgumentException($"Unknown field: {fieldName}")
        };

        private By EditIconXpath(string fieldName) =>
            By.XPath($"//div[@class='item'][.//strong[text()='{fieldName}']]//i[contains(@class,'write icon')]");

        private By FieldRowXpath(string fieldName) =>
            By.XPath($"//div[@class='item'][.//strong[text()='{fieldName}']]");

        private By DropdownXpath(string fieldName) =>
            By.Name(GetDropdownName(fieldName));

        private By ValueXpath(string fieldName, string value) =>
            By.XPath($"//div[@class='item'][.//strong[text()='{fieldName}']]//span[contains(text(),'{value}')]");

        public void SetDropdownField(string fieldName, string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            
            var fieldRow = _wait.WaitForElementVisible(FieldRowXpath(fieldName));
            js.ExecuteScript("arguments[0].scrollIntoView({block:'center'});", fieldRow);

           new Actions(driver).MoveToElement(fieldRow).Perform();

            var editIcon = _wait.WaitForElementClickable(EditIconXpath(fieldName));
            editIcon.Click();

            var dropdown = _wait.WaitForElementVisible(DropdownXpath(fieldName));
            new SelectElement(dropdown).SelectByText(value);
            _wait.WaitForElementVisible(ValueXpath(fieldName, value));
        }

        public string GetDropdownValue(string fieldName)
        {
            var valueSpan = _wait.WaitForElementVisible(By.XPath(
                $"//div[@class='item'][.//strong[text()='{fieldName}']]//div[@class='right floated content']/span"
            ));
            return valueSpan.Text.Trim(); 
        }
     }

}
