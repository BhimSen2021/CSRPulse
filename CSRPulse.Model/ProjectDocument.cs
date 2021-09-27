using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectDocument:BaseModel
    {  
        public int ProjectDocumentId { get; set; }
        public int ProjectId { get; set; }
        public int DocumentId { get; set; }
        [StringLength(500), Display(Name = "Document Name")]
        public string DocumentName { get; set; }
        [StringLength(500), Display(Name = "File Name")]
        public string UploadFileName { get; set; }
        [StringLength(256)]
        public string ServerFileName { get; set; }
        public int DocumentMaxSize { get; set; }
        public string DocumentType { get; set; }       
        public bool Mandatory { get; set; }
        public string Remark { get; set; }
        public IFormFile DocumentFile { get; set; }

    }
}
