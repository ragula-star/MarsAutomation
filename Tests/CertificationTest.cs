using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages.Components;
using MarsAutomation.Utilities;
using NUnit.Framework;

namespace MarsAutomation.Tests
{
    public class CertificationTest : TestsHooks
    {
        private CertificationComponent _certificationComponent;

        [SetUp]
        public void Setup() => _certificationComponent = new CertificationComponent(driver, wait);

        [Test]
        public void AddCertificationsFromJson()
        {
            var data = JsonReader.ReadJson<Certification_Model>("TestData/Certifications.json");

            foreach (var cert in data.Certifications)
            {
                _certificationComponent.AddCertification(cert.Name, cert.From, cert.Year);
            }
        }
    }
}
