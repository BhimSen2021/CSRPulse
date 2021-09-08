using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class NGOChartDocument : BaseModel
    {
        [Key]
        public int NgochartDocumentId { get; set; }
        public int PartnerId { get; set; }
        [StringLength(250),Display(Name = "Document Name")]       
        public string DocumentName { get; set; }
        [StringLength(256)]        
        public string ServerDocumentName { get; set; }
        [StringLength(2000)]
        public string Remarks { get; set; }
        public IFormFile DocumentFile { get; set; }
        public bool Mandatory { get; set; }
    }
}
