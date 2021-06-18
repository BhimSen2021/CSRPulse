using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class IndicatorType
    {
        [Key]
        public int IndicatorTypeId { get; set; }
        [Required]
        [Column("IndicatorType")]
        [StringLength(50)]
        public string IndicatorType1 { get; set; }
        [Required]
        public bool? IsActive { get; set; }
    }
}
