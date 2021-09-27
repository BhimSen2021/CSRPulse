using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public int DocumentMaxSize { get; set; }
        public string DocumentType { get; set; }
        public bool Mandatory { get; set; }
        public string Remark { get; set; }
        public bool AssigneDocument { get; set; }

    }
}
