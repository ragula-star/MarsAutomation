using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MarsAutomation.Models
{
    public class ShareSkill_Model
    {
        public List<ShareSkill> ShareSkill { get; set; }
    }

    public class ShareSkill
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategoryValue { get; set; }
        public string Tags { get; set; }
        public string ServiceType { get; set; }
        public string LocationType { get; set; }
        public string SkillTrade { get; set; }
        public string SkillExchange { get; set; }
        public string Active { get; set; }
        public string WorkSamplePath { get; set; }
    }
}
