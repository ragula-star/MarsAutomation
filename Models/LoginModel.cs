using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Models
{
    public class LoginModel
    {
        public class LoginTestCase
        {
            public string TestCase { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ExpectedResult { get; set; }
        }
        public List<LoginTestCase> PositiveTests { get; set; }
        public List<LoginTestCase> ValidLoginWithLeadingTrailingSpaces { get; set; }

        public List<LoginTestCase> NegativeTests { get; set; }
        public List<LoginTestCase> SQLInjectionTests { get; set; }
        public List<LoginTestCase> DestructiveTests { get; set; }
        }
}
