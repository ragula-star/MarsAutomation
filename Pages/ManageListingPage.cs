using MarsAutomation.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Pages
{
    public class ManageListingPage
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        public ManageListingPage(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
        }

        private By clickManagelisting = By.XPath("//a[text()='Manage Listings']");
        

        public void GoToManageListing()
        {
            _wait.WaitForElementClickable(clickManagelisting).Click();
        }
       
    }

}
