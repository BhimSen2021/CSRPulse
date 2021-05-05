using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class StartingNumber
    {
        [Key]
        public int StartNumberId { get; set; }
        [Required]
        [StringLength(50)]
        public string TableName { get; set; }
        [Required]
        [StringLength(100)]
        public string ColumnName { get; set; }
        [Required]
        [StringLength(7)]
        public string Prefix { get; set; }
        public int Number { get; set; }
        public byte NumberWidth { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
