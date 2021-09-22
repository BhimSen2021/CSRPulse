using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CSRPulse.Model
{
    public class PartnerDocumentDetail
    {
        public int PartnerDocumentId { get; set; }
        public int PartnerId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string Remark { get; set; }
        public string ServerDocumentName { get; set; }
        public IFormFile DocumentFile { get; set; }
        public string UploadedDocumentName { get; set; }

        [Display(Name = "Uploaded")]
        public bool IsUploaded { get; set; }
    }
    
}
