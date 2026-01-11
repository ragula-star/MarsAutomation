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
        public string Type { get; set; }

        
        public string OriginalName { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedLevel { get; set; }
    }
}
