namespace MarsAutomation.Models
{
    public class AboutMeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FieldName { get; set; }   // "Location", "Availability", "Hours", "EarnTarget"
        public string Value { get; set; }       // Value to set
        public string Type { get; set; }
    }
    public class AboutMe_Model
    {
        public List<AboutMeData> AboutMeFields { get; set; }
    }
    public class AboutMeData
    {
        public string FieldName { get; set; }   
        public string Value { get; set; }       
        public string Type { get; set; } 
     }
}

