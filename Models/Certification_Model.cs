using System.Collections.Generic;

namespace MarsAutomation.Models
{
    public class Certification_Model
    {
        public List<CertificationData> Certifications { get; set; }
    }

    public class CertificationData
    {
       
            public string TestCase { get; set; }
            public string Name { get; set; }          
            public string From { get; set; }          
            public string Year { get; set; }          
            public string Type { get; set; }          

            
            public string OriginalName { get; set; }
            public string UpdatedName { get; set; }
            public string UpdatedFrom { get; set; }
            public string UpdatedYear { get; set; }
        

    }
}
