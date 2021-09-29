using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CSRPulse.Model
{
    public class PartnerDocument : BaseModel
    {
        public int PartnerDocumentId { get; set; }
        public int PartnerId { get; set; }
        public int DocumentId { get; set; }
        [StringLength(500), Display(Name = "Document Name")]
        public string DocumentName { get; set; }
        [StringLength(500), Display(Name = "File Name")]
        public string UploadFileName { get; set; }
        [StringLength(256)]
        public string ServerFileName { get; set; }
        [StringLength(256)]
        public string Remark { get; set; }
        public IFormFile DocumentFile { get; set; }
        public bool? Mandatory { get; set; }
        [Display(Name = "Uploaded")]
        public bool IsUploaded { get; set; }
        public string DocumentType { get; set; }
        public int DocumentMaxSize { get; set; }
    }
}
