using MarsAutomation.Pages.Components;
using MarsAutomation.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Pages
{
    public class NotificationPage
    {
        private IWebDriver driver;
        private WaitHelpers _wait;
        public NotificationComponents notificationComponents { get; }

        public NotificationPage(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
            notificationComponents = new NotificationComponents(driver, _wait);
        }

        private By ClickNotificationTab = By.XPath("//div[contains(@class,'dropdown') and contains(.,'Notification')]");

        public void GoToNotification()
        {
            _wait.WaitForElementClickable(ClickNotificationTab).Click();
        }
        

    }

}
