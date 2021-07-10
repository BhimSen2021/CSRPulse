using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class DesignationHistory
    {
        [Key]
        public int DesignationHistoryId { get; set; }
        public int DesignationId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Formdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Todate { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(DesignationId))]
        [InverseProperty("DesignationHistory")]
        public virtual Designation Designation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("DesignationHistory")]
        public virtual User User { get; set; }
    }
}
