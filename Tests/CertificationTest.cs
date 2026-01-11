using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages;
using MarsAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace MarsAutomation.Tests
{
    [TestFixture]
    public class CertificationTests : TestsHooks
    {
        private ProfilePage profilePage;

        [SetUp]
        public void Setup()
        {
            profilePage = new ProfilePage(driver, wait);
            profilePage.GoToProfile();

            
            profilePage.Certifications.ClearAllCertifications();
        }

        [Test]
        [Category("Certification-Add")]
        public void AddCertificationsFromJson()
        {
            profilePage.Certifications.ClearAllCertifications();

            var data = JsonReader.ReadJson<Certification_Model>("TestData/Certifications.json");

            foreach (var cert in data.Certifications.Where(c => c.TestCase.StartsWith("Add Certification")))
            {
                profilePage.Certifications.AddCertification(cert.Name, cert.From, cert.Year);
            }

            var actualCerts = profilePage.Certifications.GetAllCertifications();

            Assert.Multiple(() =>
            {
                foreach (var cert in data.Certifications.Where(c => c.TestCase.StartsWith("Add Certification") && c.Type == "Positive"))
                {
                    string expected = $"{cert.Name} | {cert.From} | {cert.Year}";
                    Assert.That(actualCerts, Does.Contain(expected), $"Expected certification not found: {expected}");
                }
            });
        }

        [Test]
        [Category("Certification-Edit")]
        public void EditCertificationFromJson()
        {
            profilePage.Certifications.ClearAllCertifications();

            
            profilePage.Certifications.AddCertification("ISTQB Foundation", "ISTQB", "2023");

            var data = JsonReader.ReadJson<Certification_Model>("TestData/Certifications.json");
            var editCert = data.Certifications.First(c => c.TestCase == "Edit Certification");

            profilePage.Certifications.EditCertification(
                editCert.OriginalName,
                editCert.UpdatedName,
                editCert.UpdatedFrom,
                editCert.UpdatedYear
            );

            var certs = profilePage.Certifications.GetAllCertifications();
            string expected = $"{editCert.UpdatedName} | {editCert.UpdatedFrom} | {editCert.UpdatedYear}";

            Assert.That(certs, Does.Contain(expected), "Edited certification not found");
        }

        [Test]
        [Category("Certification-Delete")]
        public void DeleteCertificationFromJson()
        {
            profilePage.Certifications.ClearAllCertifications();

            
            profilePage.Certifications.AddCertification("ISTQB Advanced", "ISTQB", "2024");

            var data = JsonReader.ReadJson<Certification_Model>("TestData/Certifications.json");
            var deleteCert = data.Certifications.First(c => c.TestCase == "Delete");

            profilePage.Certifications.DeleteCertification(deleteCert.Name);

            var certs = profilePage.Certifications.GetAllCertifications();

            Assert.That(
                certs.Any(c => c.Contains(deleteCert.Name)),
                Is.False,
                $"Certification '{deleteCert.Name}' was not deleted"
            );
        }




    }
}

