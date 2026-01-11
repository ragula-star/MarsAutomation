using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsAutomation.Utilities
{
    public class WaitHelpers
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public WaitHelpers(IWebDriver driver, int seconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        }

        public IWebElement WaitForElementVisible(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement WaitForElementClickable(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public bool WaitForText(By locator, string text)
        {
            return _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }

       
        public IList<IWebElement> WaitForElementsVisible(By locator, int timeoutInSeconds = 10)
        {
            WebDriverWait localWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));

            return localWait.Until(d =>
            {
                var elements = d.FindElements(locator);
                return elements.Count > 0 ? elements : null;
            });
        }
        public IWebElement WaitForElementVisible(IWebElement element)
        {
            return _wait.Until(driver =>
                element.Displayed ? element : null
            );
        }

    }
}

