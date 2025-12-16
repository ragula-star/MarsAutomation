using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Models
{
    public class ManageListing
    {
        public string TestCase { get; set; }
        public string ExpectedMessage { get; set; }
    }
    public class ManageListingRoot()
    {
        public List<ManageListing> ManageListingsTests { get; set; }
    }
}
