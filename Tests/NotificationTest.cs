using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages;
using MarsAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Tests
{
    public class NotificationTest: TestsHooks
    {
        public NotificationPage notificationPage;
        public Notification_Model notification;

        [SetUp]
        public void setup()
        {
            notificationPage = new NotificationPage(driver, wait);
            notification = new Notification_Model();
        }
        [Test]
        public void Notificationtest()
        {
            // Act – open notification tab
            notificationPage.GoToNotification();

            // Read JSON test data
            string jsonPath = "TestData/notification.json";

            var jsonData = JsonReader.ReadJson
                <Dictionary<string, List<Notification_Model>>>(jsonPath);

            string expectedMessage =
                jsonData["Notification"][0].ExpectedMessage;

            // Get actual message from UI
            string actualMessage = notificationPage.notificationComponents.GetEmptyNotificationMessage();


            // Assert
            Assert.AreEqual(expectedMessage, actualMessage,
                "Notification empty message does not match expected value");
        }

    }
}
