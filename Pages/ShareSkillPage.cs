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
    public class ShareSkillPage
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;
        public ShareSkillComponents shareSkillComponents { get; }

        public ShareSkillPage(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
            shareSkillComponents = new ShareSkillComponents(driver, _wait);
        }

        private By ClickShareSkill => By.XPath("//a[text()='Share Skill']");
        private By SuccessMessage => By.XPath("*//[Contains(text(), 'Service Listing Added successfully')]");
       
        public void GoToShareSkill()
        {
            _wait.WaitForElementClickable(ClickShareSkill).Click();
        }

        public string VerifySuccessMessage()
        {
            return _wait.WaitForElementVisible(SuccessMessage).Text;

        }
    }
}
