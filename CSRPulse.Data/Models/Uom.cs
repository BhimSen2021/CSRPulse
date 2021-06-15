using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    [Table("UOM")]
    public partial class Uom
    {
        public Uom()
        {
            Indicator = new HashSet<Indicator>();
        }

        [Key]
        [Column("UOMId")]
        public int Uomid { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitName { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [InverseProperty("Uom")]
        public virtual ICollection<Indicator> Indicator { get; set; }
    }
}
