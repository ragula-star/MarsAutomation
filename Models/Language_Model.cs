using System.Collections.Generic;

namespace MarsAutomation.Models
{
    public class Language_Model
    {
        public List<LanguageData> Languages { get; set; }
    }

    public class LanguageData
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }
    }
}

