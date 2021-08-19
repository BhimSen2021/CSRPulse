using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class AuditorDocument : BaseModel
    {
        [Key]
        public int AuditorDocumentId { get; set; }
        public int AuditorId { get; set; }
        public int DocumentId { get; set; }
        [Display(Name = "Document Name")]
        public string DocumentName { get; set; }
        [Display(Name = "Uploaded Document Name")]
        public string Udname { get; set; }
        public string Sdname { get; set; }
        [StringLength(256)]
        public string Remark { get; set; }
        public string DocumentType { get; set; }
        public int DocumentMaxSize { get; set; }
        public IFormFile DocumentFile { get; set; }
    }
}
