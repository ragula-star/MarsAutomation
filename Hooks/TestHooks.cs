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
        protected WaitHelpers wait;
        protected Loginpage loginpage;
        protected ProfilePage profilePage;

        private ExtentReports _extent;
        private ExtentTest _test;

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            string reportsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            Directory.CreateDirectory(reportsDir);

            var reporter = new ExtentSparkReporter(Path.Combine(reportsDir, "AutomationReport.html"));
            reporter.Config.DocumentTitle = "Mars Automation Report";
            reporter.Config.ReportName = "Mars Automation Results";

            _extent = new ExtentReports();
            _extent.AttachReporter(reporter);
        }

        [SetUp]
        public void BeforeEachTest()
        {
            driver = CommonDriver.InitializeDriver("Chrome");
            wait = new WaitHelpers(driver);

            driver.Navigate().GoToUrl(ConfigReader.Get("BaseUrl"));

            loginpage = new Loginpage(driver, wait);
            loginpage.Clicklogin();
            loginpage.Emaillogin(ConfigReader.Username, ConfigReader.Password);

            profilePage = new ProfilePage(driver, wait);


            var categories = TestContext.CurrentContext.Test.Properties["Category"];

            profilePage.GoToProfile();

            if (categories.Contains("Skill"))
            {
                TryCleanup(() => profilePage.Skills.ClearAllSkills());
            }

            if (categories.Contains("Education"))
            {
                TryCleanup(() => profilePage.Educations.ClearAllEducations());
            }

            if (categories.Contains("Certification"))
            {
                TryCleanup(() => profilePage.Certifications.ClearAllCertifications());
            }

            if (categories.Contains("Language"))
            {
                TryCleanup(() => profilePage.Languages.ClearAllLanguages());
            }

            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterEachTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var testName = TestContext.CurrentContext.Test.Name;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string screenshot = ScreenshotHelper.CaptureScreenshot(driver, testName);
                _test.Fail("Test Failed",
                    MediaEntityBuilder.CreateScreenCaptureFromPath(screenshot).Build());
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                _test.Pass("Test Passed");
            }
            else
            {
                _test.Skip("Test Skipped");
            }
            driver.Dispose();
            driver?.Quit();
            driver = null;
        }
        private void TryCleanup(Action cleanupAction)
        {
            try
            {
                cleanupAction.Invoke();
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Cleanup skipped safely: {ex.Message}");
            }
        }

       
        [OneTimeTearDown]
        public void AfterAllTests()
        {
            _extent.Flush();
        }

    }
}
