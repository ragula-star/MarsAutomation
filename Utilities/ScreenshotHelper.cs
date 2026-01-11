using OpenQA.Selenium;
using System;
using System.IO;

namespace Utilities
{
    public static class ScreenshotHelper
    {
        public static string CaptureScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                
                string filePath = Path.Combine(folderPath, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                File.WriteAllBytes(filePath, screenshot.AsByteArray);

                return filePath;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error capturing screenshot: " + e.Message);
                return null;
            }
        }
    }
}

