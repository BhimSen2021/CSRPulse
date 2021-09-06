using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectDocument
    {        
        public int ProjectDocumentId { get; set; }
        public int ProjectId { get; set; }
        public int DocumentId { get; set; }
        [StringLength(256), Display(Name = "Document Name")]
        public string DocumentName { get; set; }
        [StringLength(256)]
        public string ServerDocumentName { get; set; }
        public string MDocumentName { get; set; }
        public string DocumentType { get; set; }
        public int DocumentMaxSize { get; set; }
        public bool Mandatory { get; set; }
        public IFormFile DocumentFile { get; set; }

    }
}
