using System.Collections.Generic;

namespace MarsAutomation.Models
{
    public class Certification_Model
    {
        public List<CertificationData> Certifications { get; set; }
    }

    public class CertificationData
    {
        public string Name { get; set; }
        public string From { get; set; }
        public string Year { get; set; }
    }
}
