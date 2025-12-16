using System.Collections.Generic;

namespace MarsAutomation.Models
{
    public class Skill_Model
    {
        public List<SkillData> Skills { get; set; }
    }

    public class SkillData
    {
        public string Name { get; set; }
        public string Level { get; set; }
    }
}
