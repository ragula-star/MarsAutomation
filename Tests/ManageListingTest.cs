using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages;
using MarsAutomation.Pages.Components;
using MarsAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Tests
{
    public class ManageListingTest: TestsHooks
    {
        
        private ManageListingPage _manageListingPage;
        private ManageListingModel manageListingModel;

        [SetUp]
        public void setup()
        {
            
            _manageListingPage = new ManageListingPage(driver, wait);
            manageListingModel = new ManageListingModel(driver, wait);

        }

        [Test]
        public void VerifyNoListingMessage()
        {
           
            _manageListingPage.GoToManageListing();

            string jsonpath = "TestData/ManageListing.json";
            var data = JsonReader.ReadJson<ManageListingRoot>(jsonpath);
            var testData = data.ManageListingsTests[0];

            string ManageListText = manageListingModel.GetManageListMessage();
            Assert.That(ManageListText, Is.EqualTo(testData.ExpectedMessage),
      $"Test '{testData.TestCase}' failed. Expected: '{testData.ExpectedMessage}', but got '{ManageListText}'");
        }
    }
}
