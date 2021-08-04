using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    [Table("NGOChartDocument")]
    public partial class NgochartDocument
    {
        [Key]
        [Column("NGOChartDocumentId")]
        public int NgochartDocumentId { get; set; }
        public int PartnerId { get; set; }
        [Required]
        [StringLength(250)]
        public string DocumentName { get; set; }
        [Required]
        [StringLength(256)]
        public string ServerDocumentName { get; set; }
        [StringLength(2000)]
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("NgochartDocument")]
        public virtual Partner Partner { get; set; }
    }
}
