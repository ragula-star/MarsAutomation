using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages;
using MarsAutomation.Pages.Components;
using MarsAutomation.Utilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAutomation.Tests
{
    [TestFixture]
    public class ManageListingTest : TestsHooks
    {
        private ManageListingPage manageListingPage;
        private ManageListingsComponent manageListingsComponent;

        [SetUp]
        public void Setup()
        {
            manageListingPage = new ManageListingPage(driver, wait);
            manageListingsComponent = new ManageListingsComponent(driver, wait);
        }

        [Test]
        public void EditAndDeleteListingFromJson()
        {
            manageListingPage.GoToManageListing();

            string jsonPath = "TestData/ManageListing.json";
            var data = JsonReader.ReadJson<ManageListingRoot>(jsonPath);

            foreach (var test in data.ManageListingsTests)
            {
                if (test.TestCase == "EditListing")
                {
                    
                    manageListingsComponent.EditListing(test.TitleToEdit, test.UpdatedTitle, test.UpdatedDescription);

                    
                    wait.WaitForElementVisible(By.XPath("//table[contains(@class,'ui striped table')]"));
                }
                else if (test.TestCase == "DeleteListing")
                {
                    
                    manageListingsComponent.DeleteListing(test.TitleToDelete);
                }
            }
        }


    }
}
