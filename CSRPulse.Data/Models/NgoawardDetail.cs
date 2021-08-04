using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    [Table("NGOAwardDetail")]
    public partial class NgoawardDetail
    {
        [Key]
        [Column("NGOAwardDetailId")]
        public int NgoawardDetailId { get; set; }
        public int PartnerId { get; set; }
        [StringLength(500)]
        public string Award { get; set; }
        public int? YearOfReceiving { get; set; }
        [StringLength(500)]
        public string AwardConferrer { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("NgoawardDetail")]
        public virtual Partner Partner { get; set; }
    }
}
