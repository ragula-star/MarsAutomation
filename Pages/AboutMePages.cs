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
    internal class AboutMePages
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        public AboutMeComponent AboutMe { get; }

        public AboutMePages(IWebDriver driver, WaitHelpers wait)
        {
            this.driver = driver;
            this._wait = wait;

            AboutMe = new AboutMeComponent(driver, wait);
        }

        public void GoToProfile()
        {
            
               driver.FindElement(By.XPath("//a[@href='/Account/Profile']"));
           
        }
    }
}
