using System.Collections.Generic;

namespace MarsAutomation.Models
{
    public class SearchSkill_Model
    {
        public List<SearchSkillData> Skills { get; set; } = new();
    }


    public class SearchSkillData
    {
        public string TestCase { get; set; }
        public string Skill { get; set; }
        public string Type { get; set; }
    }
}
