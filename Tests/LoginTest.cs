using MarsAutomation.Base;
using MarsAutomation.Hooks;
using MarsAutomation.Models;
using MarsAutomation.Pages;
using MarsAutomation.Utilities;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Tests
{
    public class LoginTest : TestsHooks
    {
        Loginpage loginpage;
        WaitHelpers wait;

        [SetUp]
        public void setup()
        {
            wait = new WaitHelpers(driver);
            loginpage = new Loginpage(driver, wait);

        }

        [Test]
        public void TestLogin()
        {
            string jsonpath = "TestData/login.json";
            LoginModel AllTest = JsonReader.ReadJson<LoginModel>(jsonpath);
            var TestData = AllTest.PositiveTests[0];
            loginpage.Clicklogin();
            loginpage.Emaillogin(TestData.Email, TestData.Password);

            bool isLoginSuccess = loginpage.verifylogo(); 
            bool isLoginSuccess = loginpage.verifylogo();
            Assert.That(isLoginSuccess, Is.EqualTo(TestData.ExpectedResult == "LoginSuccess"));

        }
        [Test]
        public void ValidLoginwithLeading()
        {
            string Jsonpath = "TestData/login.json";
            LoginModel Test = JsonReader.ReadJson<LoginModel>(Jsonpath);
            var Data = Test.ValidLoginWithLeadingTrailingSpaces[0];
            loginpage.Emaillogin(Data.Email, Data.Password);
            string message = loginpage.invalidEmailText();
            Assert.That(message, Does
                .Contain("Please enter a valid email address"));


        }
        [Test]
        public void InvalidTest()
        {
            string jsonpath = "TestData/login.json";
            LoginModel model = JsonReader.ReadJson<LoginModel>(jsonpath);

            var failures = new List<string>();

            foreach (var testData in model.NegativeTests)
            {

                loginpage.ClearFields();
                loginpage.Emaillogin(testData.Email, testData.Password);

                string emailMessage = loginpage.invalidEmailText();
                string passwordMessage = loginpage.InvalidpasswordText();

               
                
                switch (testData.ExpectedResult)
                {
                    case "InvalidCredentials":
                        if (!emailMessage.Contains("Please enter a valid email address"))
                            failures.Add($"Test '{testData.TestCase}' - Email: Expected error message missing.");
                        if (!passwordMessage.Contains("Password must be at least 6 characters"))
                            failures.Add($"Test '{testData.TestCase}' - Password: Expected error message missing.");
                        break;

                    case "EmailRequired":
                        if (!emailMessage.Contains("Please enter a valid email address"))
                            failures.Add($"Test '{testData.TestCase}' - Email: Expected error message missing.");
                        break;

                    case "PasswordRequired":
                        if (!passwordMessage.Contains("Password must be at least 6 characters"))
                            failures.Add($"Test '{testData.TestCase}' - Password: Expected error message missing.");
                        break;

                    case "FieldsRequired":
                        if (!emailMessage.Contains("Please enter a valid email address"))
                            failures.Add($"Test '{testData.TestCase}' - Email: Expected error message missing.");
                        if (!passwordMessage.Contains("Password must be at least 6 characters"))
                            failures.Add($"Test '{testData.TestCase}' - Password: Expected error message missing.");
                        break;

                    case "InvalidEmailFormat":
                        if (!emailMessage.Contains("Please enter a valid email address"))
                            failures.Add($"Test '{testData.TestCase}' - Email: Expected error message missing.");
                        break;
                }
            }

           
            
            if (failures.Count > 0)
            {
                Assert.Fail(string.Join(Environment.NewLine, failures));
            }
        }




        [Test]
        public void SQLInjectionTests()
        {
            string jsonpath = "TestData/login.json";
            LoginModel model = JsonReader.ReadJson<LoginModel>(jsonpath);

            var failures = new List<string>();

            foreach (var testData in model.SQLInjectionTests)
            {
                loginpage.ClearFields();
                loginpage.Emaillogin(testData.Email, testData.Password);

                string emailMessage = loginpage.invalidEmailText();
                string passwordMessage = loginpage.InvalidpasswordText();

                
               
                if (testData.ExpectedResult == "InvalidCredentials")
                {
                    if (!emailMessage.Contains("Please enter a valid email address"))
                        failures.Add($"Test '{testData.TestCase}' - Email: Expected error message missing.");
                    if (!passwordMessage.Contains("Password must be at least 6 characters"))
                        failures.Add($"Test '{testData.TestCase}' - Password: Expected error message missing.");
                }
            }

            if (failures.Count > 0)
            {
                Assert.Fail(string.Join(Environment.NewLine, failures));
            }
        }
        [Test]
        public void DestructiveTests()
        {
            string jsonpath = "TestData/login.json";
            LoginModel model = JsonReader.ReadJson<LoginModel>(jsonpath);

            var failures = new List<string>();

            foreach (var testData in model.DestructiveTests)
            {
                loginpage.ClearFields();
                loginpage.Emaillogin(testData.Email, testData.Password);

                string emailMessage = loginpage.invalidEmailText();
                string passwordMessage = loginpage.InvalidpasswordText();

                switch (testData.ExpectedResult)
                {
                    case "InvalidCredentials":
                        if (!emailMessage.Contains("Please enter a valid email address"))
                            failures.Add($"Test '{testData.TestCase}' - Email: Expected error message missing.");
                        if (!passwordMessage.Contains("Password must be at least 6 characters"))
                            failures.Add($"Test '{testData.TestCase}' - Password: Expected error message missing.");
                        break;

                    case "InputTooLong":
                        if (!emailMessage.Contains("Input is too long") && !passwordMessage.Contains("Input is too long"))
                            failures.Add($"Test '{testData.TestCase}' - InputTooLong: Expected error message missing.");
                        break;
                }
            }

            if (failures.Count > 0)
            {
                Assert.Fail(string.Join(Environment.NewLine, failures));
            } }



        } }



