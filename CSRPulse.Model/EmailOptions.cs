using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class EmailOptions
    {
        public List<string> ToEmails { get; set; }
        public List<string> CcEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<KeyValuePair<string, string>> PlaceHolders { get; set; }
    }
}
