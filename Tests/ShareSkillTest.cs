using MarsAutomation.Hooks;
using MarsAutomation.Utilities;
using MarsAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsAutomation.Models;

namespace MarsAutomation.Tests
{
    public class ShareSkillTest:TestsHooks
    {
        public ShareSkillPage _shareskillpage;
        WaitHelpers _wait;

        [SetUp]
        public void Initialize()
        {
            _wait = new WaitHelpers(driver);

            _shareskillpage = new ShareSkillPage(driver, _wait);
        }

        [Test]
        public void ShareSkillFormTest()
        {
            var testdata = "TestData/ShareSkill.json";
            ShareSkill_Model data = JsonReader.ReadJson<ShareSkill_Model>(testdata);

            var feature = new List<string>();
            

            foreach (var test in data.ShareSkill)
            {

                _shareskillpage.GoToShareSkill();

                _shareskillpage.shareSkillComponents.FillShareSkill(test.Title, test.Description);
                _shareskillpage.shareSkillComponents.ShareskillCategory();
                _shareskillpage.shareSkillComponents.ShareSkillTag(test.Tags);
                _shareskillpage.shareSkillComponents.ShareSkillServiceType();
                _shareskillpage.shareSkillComponents.ShareSkillLocationType();
                _shareskillpage.shareSkillComponents.ShareSkillTRade();
                _shareskillpage.shareSkillComponents.ShareSkillExchange(test.SkillExchange);
                _shareskillpage.shareSkillComponents.ShareSkillWorkSample();
                _shareskillpage.shareSkillComponents.ShareSkillActive();
                _shareskillpage.shareSkillComponents.ShareSkillSave();

                string msg = _shareskillpage.VerifySuccessMessage();
                Assert.That(msg, Does.Contain("Service Listing Added successfully"));
               
            }
        }

    }
}
