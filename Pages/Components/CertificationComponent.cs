using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static System.Collections.Specialized.BitVector32;

namespace MarsAutomation.Pages.Components
{
    public class CertificationComponent
    {
        private readonly IWebDriver driver;
        private readonly WaitHelpers _wait;

        private By CertificationsTab => By.XPath("//a[@data-tab='fourth']");
        private By AddNewCertificationBtn => By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/thead/tr/th[4]/div");
        private By CertificationName => By.Name("certificationName");
        private By CertificationFrom => By.Name("certificationFrom");
        private By CertificationYear => By.Name("certificationYear");
        private By AddCertificationBtn => By.XPath("//input[@value='Add']");
        private By CertificationRows => By.XPath("//table[@class='ui fixed table']//tbody/tr");
        private By DeleteButtons => By.XPath("//table[@class='ui fixed table']//tbody/tr/td[4]/span/i[@class='remove icon']");

        public CertificationComponent(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
        }

        public void AddCertification(string Name, string From, string Year, bool expectSuccess = true)
        {
            _wait.WaitForElementClickable(CertificationsTab).Click();
            _wait.WaitForElementClickable(AddNewCertificationBtn).Click();
            _wait.WaitForElementVisible(CertificationName);

            if (!string.IsNullOrEmpty(Name))
                driver.FindElement(CertificationName).SendKeys(Name);

            if (!string.IsNullOrEmpty(From))
                driver.FindElement(CertificationFrom).SendKeys(From);

            if (!string.IsNullOrEmpty(Year))
            {
                var yearDropdown = new SelectElement(driver.FindElement(CertificationYear));
                var optionExists = yearDropdown.Options.Any(o => o.Text == Year);
                if (optionExists)
                    yearDropdown.SelectByText(Year);
                else
                    Console.WriteLine($"Year '{Year}' not found in dropdown. Skipping selection.");
            }

            _wait.WaitForElementClickable(AddCertificationBtn).Click();

           
            //if (expectSuccess)
            //{
            //    _wait.WaitForElementVisible(By.XPath("//table[contains(@class,'ui fixed table')]//tbody/tr[last()]"));
            //}
        }


        public List<string> GetAllCertifications()
        {
            var certList = new List<string>();
            var rows = driver.FindElements(CertificationRows);

            foreach (var row in rows)
            {
                string certName = row.FindElement(By.XPath("./td[1]")).Text.Trim();
                string certFrom = row.FindElement(By.XPath("./td[2]")).Text.Trim();
                string certYear = row.FindElement(By.XPath("./td[3]")).Text.Trim();

               
                if (!string.IsNullOrEmpty(certName) || !string.IsNullOrEmpty(certFrom) || !string.IsNullOrEmpty(certYear))
                {
                    certList.Add($"{certName} | {certFrom} | {certYear}");
                }
            }

            return certList;
        }

        public void DeleteCertification(string name)
        {
            _wait.WaitForElementClickable(CertificationsTab).Click();

            var rows = driver.FindElements(CertificationRows);

            foreach (var row in rows)
            {
                var certName = row.FindElement(By.XPath("./td[1]")).Text.Trim();
                if (certName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    var deleteBtn = row.FindElement(By.XPath("./td[4]/span/i[@class='remove icon']"));
                    deleteBtn.Click();

                    
                    _wait.WaitForElementVisible(CertificationRows);
                    return;
                }
            }

            throw new Exception($"Certification '{name}' not found to delete");
        }


        public void EditCertification(string originalName, string updatedName, string updatedFrom, string updatedYear)
        {
            _wait.WaitForElementClickable(CertificationsTab).Click();

            var rows = driver.FindElements(By.XPath("//table[@class='ui fixed table']//tbody/tr"));

            foreach (var row in rows)
            {
                var nameCell = row.FindElement(By.XPath("./td[1]"));

                if (nameCell.Text.Trim().Equals(originalName, StringComparison.OrdinalIgnoreCase))
                {
                    
                    row.FindElement(By.XPath("./td[4]//i[contains(@class,'write icon')]")).Click();

                    
                    var nameInput = _wait.WaitForElementVisible(By.Name("certificationName"));
                    nameInput.Clear();
                    nameInput.SendKeys(updatedName);

                    var fromInput = driver.FindElement(By.Name("certificationFrom"));
                    fromInput.Clear();
                    fromInput.SendKeys(updatedFrom);

                    var yearDropdown = new SelectElement(driver.FindElement(By.Name("certificationYear")));
                    yearDropdown.SelectByText(updatedYear);

                   
                    driver.FindElement(By.XPath("//input[@value='Update']")).Click();

                   
                    _wait.WaitForElementVisible(By.XPath("//table[@class='ui fixed table']//tbody/tr"));

                    return;
                }
            }

            throw new Exception($"Certification '{originalName}' not found for edit");
        }



        public void ClearAllCertifications()
        {
            _wait.WaitForElementClickable(CertificationsTab).Click();

            while (true)
            {
                var buttons = driver.FindElements(DeleteButtons);
                if (buttons.Count == 0)
                    break;

                foreach (var btn in buttons)
                {
                    try
                    {
                        
                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                        wait.Until(d => btn.Displayed && btn.Enabled);

                        btn.Click();

                        System.Threading.Thread.Sleep(500);
                        break;
                    }
                    catch (ElementNotInteractableException)
                    {
                        continue; 
                    }
                    catch (StaleElementReferenceException)
                    {
                        break; 
                    }
                }
            }
        }



    }
}
