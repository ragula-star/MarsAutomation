using MarsAutomation.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Pages.Components
{
    public class NotificationComponents
    {
        private IWebDriver driver;
        private WaitHelpers _wait;

        public NotificationComponents(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
        }

        private By NotificationEmptyMessage = By.XPath("//div[@class='item' and contains(text(),'You have no notifications')]");

        public string GetEmptyNotificationMessage()
        {
            return _wait.WaitForElementVisible(NotificationEmptyMessage).Text;
        }
    }
}
