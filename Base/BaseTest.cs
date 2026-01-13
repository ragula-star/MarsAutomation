using NUnit.Framework;
using OpenQA.Selenium;
using MarsAutomation.Utilities;

namespace MarsAutomation.Base
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            
            {
                driver = CommonDriver.InitializeDriver("Chrome"); 
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(ConfigReader.Get("BaseUrl")); 
            }

        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
            driver?.Quit();
            
        }
    }
}

