using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class PartnerDocument : BaseModel
    {
        public int PartnerDocumentId { get; set; }
        public int PartnerId { get; set; }
        public int DocumentId { get; set; }
        [Display(Name = "Document Name")]
        public string UploadedDocumentName { get; set; }

        public string ServerDocumentName { get; set; }
        [StringLength(256)]
        public string Remark { get; set; }
    }
}
