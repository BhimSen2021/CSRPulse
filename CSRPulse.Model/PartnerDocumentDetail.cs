using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class PartnerDocumentDetail
    {
        public int PartnetId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }

        [Display(Name = "Uploaded")]
        public bool IsUploaded { get; set; }
    }
    
}
