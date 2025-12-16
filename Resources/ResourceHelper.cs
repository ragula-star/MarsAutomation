using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Resources
{
    public class ResourceHelper
    {
        public static string ReportPath => @"Reports\ExtentReport.html";
        public static string ScreenshotPath => @"Reports\Screenshots\";
        public static string TestDataPath => @"TestData\";
        public static string ConfigFilePath => @"Utilities\settings.json";
    }
}
