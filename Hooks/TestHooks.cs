using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using MarsAutomation.Base;
using MarsAutomation.Pages;
using MarsAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Utilities;

namespace MarsAutomation.Hooks
{
    [TestFixture]
    public class TestsHooks
    {
        protected IWebDriver driver;
        private ExtentReports _extent;
        private ExtentTest _test;

        protected Loginpage loginpage;
        protected WaitHelpers wait;

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            // Create Reports folder
            string reportsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            Directory.CreateDirectory(reportsDir);

            // Initialize ExtentReports with SparkReporter
            string reportPath = Path.Combine(reportsDir, "AutomationReport.html");
            var sparkReporter = new ExtentSparkReporter(reportPath);
            sparkReporter.Config.DocumentTitle = "Mars Automation Report";
            sparkReporter.Config.ReportName = "Mars Project Test Results";

            _extent = new ExtentReports();
            _extent.AttachReporter(sparkReporter);
        }

        [SetUp]
        public void BeforeEachTest()
        {
            // Initialize WebDriver
            driver = CommonDriver.InitializeDriver("Chrome");
            wait = new WaitHelpers(driver);
            loginpage = new Loginpage(driver, wait);
            driver.Navigate().GoToUrl(ConfigReader.Get("BaseUrl"));

            // Common pre-step: open login page
            loginpage.Clicklogin();
            loginpage.Emaillogin(ConfigReader.Username, ConfigReader.Password);

            // Create test in ExtentReports
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterEachTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var testName = TestContext.CurrentContext.Test.Name;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, testName);
                _test.Fail("Test Failed", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                _test.Pass("Test Passed");
            }
            else
            {
                _test.Skip("Test Skipped");
            }

            // Dispose driver
            driver.Dispose();
            driver?.Quit();
            driver = null;

        }


        [OneTimeTearDown]
        public void AfterAllTests()
        {
            // Flush ExtentReports
            _extent.Flush();
            
        }
       

    }
}

