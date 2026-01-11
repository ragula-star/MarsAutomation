using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Pages.Components
{
    public class ShareSkillComponents
    {
        private IWebDriver driver;
        private WaitHelpers _wait;
        

        public ShareSkillComponents(IWebDriver driver, WaitHelpers _wait)
        {
            this.driver = driver;
            this._wait = _wait;
             
        }
        
        private By FillShareSkillTitle => By.XPath("//input[@placeholder='Write a title to describe the service you provide.']");
        private By FillShareSkillDiscription => By.XPath("//textarea[@placeholder='Please tell us about any hobbies, additional expertise, or anything else you’d like to add.']");
        private By FillShareSkillCategory => By.XPath("//select[@name='categoryId']");
        private By SelectSubCategory => By.XPath("//select[@name='subcategoryId']");
        private By FillShareSkillTags => By.XPath("//input[@placeholder='Add new tag']");
        private By FillShareSkillServiceType => By.XPath("//label[text()='Hourly basis service']");
        private By FillShareSkillLocationType => By.XPath("//label[text()='On-site']");
        private By FillShareSkillTrade => By.XPath("//label[text()='Credit']");
        private By FillShareSkillSkillExchange => By.CssSelector("input.ReactTags__tagInputField");
        private By FillShareSkillWorkSample => By.CssSelector("label.worksamples-file");
        private By FillShareSkillActive => By.XPath("$//div[@class='ui radio checkbox']/label[text()='{status}']");
        private By Clicksave => By.XPath("//input[@type='button' and @value='Save']");
        private By ShareSkillErrorMessage => By.XPath("//div[contains(text(),'Please complete the form correctly')]");

        public void FillShareSkill(string title, string description)
        {
            _wait.WaitForElementVisible(FillShareSkillTitle).SendKeys(title);
            _wait.WaitForElementVisible(FillShareSkillDiscription).SendKeys(description);
        }
        public void ShareskillCategory() 
        {

            SelectElement select = new SelectElement(driver.FindElement(FillShareSkillCategory));
            select.SelectByText("Machine Learning");


            SelectElement sub = new SelectElement(driver.FindElement(SelectSubCategory));
            sub.SelectByValue("2");

        }
        public void ShareSkillTag(string tags)
        {

            var tagInput = _wait.WaitForElementVisible(FillShareSkillTags);

            
            var tagsArray = tags.Split(',');

            foreach (var tag in tagsArray)
            {
                tagInput.SendKeys(tag.Trim());
                tagInput.SendKeys(Keys.Enter); 
                Thread.Sleep(3000);
            }
        }
        public void ShareSkillServiceType() 

        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});",
                  driver.FindElement(FillShareSkillServiceType));

           
            _wait.WaitForElementClickable(FillShareSkillServiceType).Click();


        }
        public void ShareSkillLocationType()
        {
            _wait.WaitForElementClickable(FillShareSkillLocationType).Click();
        }
        public void ShareSkillTRade()
        {
            IWebElement tradeElement = driver.FindElement(FillShareSkillTrade);

            
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", tradeElement);

            
            _wait.WaitForElementClickable(FillShareSkillTrade).Click();
        }
        public void ShareSkillExchange(string skillexchange)
        {
             var skill =_wait.WaitForElementVisible(FillShareSkillSkillExchange);
             skill.SendKeys(skillexchange);
             skill.SendKeys(Keys.Enter);
        }
        public void ShareSkillWorkSample()
        {
            IWebElement label = driver.FindElement(FillShareSkillWorkSample);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", label);
            label.Click(); 

            
            IWebElement fileInput = driver.FindElement(By.Id("selectFile"));
            fileInput.SendKeys(@"C:\Users\muthu\OneDrive\Pictures\developer-8829735_1280.jpg");

        }
        public void ShareSkillActive()
        {
            _wait.WaitForElementClickable(FillShareSkillActive).Click();
        }
        public void ShareSkillSave()
        {
            _wait.WaitForElementClickable(Clicksave).Click();
        }
    }

}
