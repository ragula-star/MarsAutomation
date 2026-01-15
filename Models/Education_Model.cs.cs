using System.Collections.Generic;

namespace MarsAutomation.Models
{
    public class Education_Model
    {
        public List<EducationData> Educations { get; set; }
    }

    public class EducationData
    {
        public string University { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public string Degree { get; set; }
        public string GraduationYear { get; set; }
        public string Type { get; set; }
    }
}
