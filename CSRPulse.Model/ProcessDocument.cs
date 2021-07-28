using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ProcessDocument:BaseModel
    {
        public int DocumentId { get; set; }
        [Display(Name = "Process"),Required(ErrorMessage="Please select Process")]        
        public int? ProcessId { get; set; }
        [Display(Name ="Document Name")]
        [Required(ErrorMessage ="Please enter document name")]
        [StringLength(500)]
        public string DocumentName { get; set; }     
        public decimal SeqNo { get; set; }

        [Display(Name = "Parent Document")] 
        public int? ParentDocumentId { get; set; }
        [Display(Name ="Document Max Size (MB)"),Required(ErrorMessage ="Please enter max size"),Range(1,20,ErrorMessage ="Document Size should be between 1 to 20 MB")]
        public int? DocumentMaxSize { get; set; }
        [StringLength(50)]
        public string DocumentType { get; set; }
        public bool Mandatory { get; set; }
       
      
        [Display(Name = "Use as only Document Group")]
        public bool DocumentUpload { get; set; }
        [Display(Name = "Remarks")]
        [StringLength(500)]
        public string Remark { get; set; }

        public List<DocumentType> DocumentTypes { get; set; }

        public string ParentDocument { get; set; }
    }

    public class DocumentType
    {
        public bool DocumentCheck { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }

    }
}
