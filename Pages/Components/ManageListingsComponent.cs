using OpenQA.Selenium;
using MarsAutomation.Utilities;
using System.Linq;

namespace MarsAutomation.Pages.Components
{
    public class ManageListingsComponent
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        private By ListingRows => By.XPath("//table[@class='ui striped table']/tbody/tr");

        public ManageListingsComponent(IWebDriver driver, WaitHelpers wait)
        {
            this.driver = driver;
            _wait = wait;
        }

        
        public void EditListing(string currentTitle, string updatedTitle, string updatedDescription)
        {
            
            
            var rows = driver.FindElements(By.XPath("//table[contains(@class,'ui striped table')]//tbody/tr"));

           
            foreach (var row in rows)
            {
                
                var titleCell = row.FindElement(By.XPath(".//td[3]"));
                if (titleCell.Text.Equals(currentTitle, StringComparison.OrdinalIgnoreCase))
                {
                   
                    var editButton = row.FindElement(By.XPath(".//td[last()]//button[.//i[contains(@class,'outline write icon')]]"));

                    editButton.Click();

                   
                    _wait.WaitForElementVisible(By.Name("title"));
                    _wait.WaitForElementVisible(By.Name("description"));

                    var titleInput = driver.FindElement(By.Name("title"));
                    titleInput.Clear();
                    titleInput.SendKeys(updatedTitle);

                    var descInput = driver.FindElement(By.Name("description"));
                    descInput.Clear();
                    descInput.SendKeys(updatedDescription);

                    var saveButton = driver.FindElement(By.XPath("//input[@value='Save']"));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", saveButton);
                    saveButton.Click();

                    
                    //_wait.WaitForElementVisible(By.XPath("//table[contains(@class,'ui striped table')]//tbody/tr"));
                    _wait.WaitForElementVisible(By.XPath("//table[contains(@class,'ui striped table')]//tbody/tr"));

                     return;
                }
            }

            throw new Exception($"Listing with title '{currentTitle}' not found");

        }

        public void DeleteListing(string titleToDelete)
        {
            
            var rows = _wait.WaitForElementsVisible(By.XPath("//table[contains(@class,'ui striped table')]//tbody/tr"));

            foreach (var row in rows)
            {
                var titleCell = row.FindElement(By.XPath(".//td[3]"));
                if (titleCell.Text.Equals(titleToDelete, StringComparison.OrdinalIgnoreCase))
                {
                    var deleteButton = row.FindElement(By.XPath(".//td[last()]//button[.//i[contains(@class,'remove icon')]]"));
                    deleteButton.Click();

                   _wait.WaitForElementVisible(By.XPath("//button[text()='Yes']"))?.Click();
                  _wait.WaitForElementVisible(By.XPath("//table[contains(@class,'ui striped table')]//tbody/tr"));
                    return;
                }
            }

            throw new Exception($"Listing with title '{titleToDelete}' not found");
        }

    }
}
