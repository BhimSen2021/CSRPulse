using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class PartnerDocument
    {
        public int PartnetId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public bool IsUploaded { get; set; }
    }
}
