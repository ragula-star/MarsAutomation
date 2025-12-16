using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace MarsAutomation.Reports
{
    public static class ExtentManager
    {
        private static ExtentReports? _extent;
        private static ExtentTest? _test;

        // Get or create the ExtentReports instance
        public static ExtentReports GetReporter()
        {
            if (_extent == null)
            {
                string reportsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
                Directory.CreateDirectory(reportsDir);

                string reportPath = Path.Combine(reportsDir, "AutomationReport.html");
                var sparkReporter = new ExtentSparkReporter(reportPath);

                _extent = new ExtentReports();
                _extent.AttachReporter(sparkReporter);
            }
            return _extent;
        }

        // Create a test in the report
        public static ExtentTest CreateTest(string testName)
        {
            _test = GetReporter().CreateTest(testName);
            return _test;
        }

        // Log test pass
        public static void LogPass(string message)
        {
            _test?.Pass(message);
        }

        // Log test fail with optional screenshot
        public static void LogFail(string message, string? screenshotPath = null)
        {
            if (!string.IsNullOrEmpty(screenshotPath))
            {
                _test?.Fail(message).AddScreenCaptureFromPath(screenshotPath);
            }
            else
            {
                _test?.Fail(message);
            }
        }

        // Flush reports to HTML
        public static void Flush()
        {
            _extent?.Flush();
        }
    }
}

