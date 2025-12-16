using MarsAutomation.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Pages.Components
{
    public class ManageListingModel
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        public ManageListingModel(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;

           
    }
        private By ManageListMessage = By.XPath("//div[@class='ui container']/h3[text()='You do not have any service listings!']");

        public string GetManageListMessage()
        {
            return _wait.WaitForElementVisible(ManageListMessage).Text;
        }
    }
}
