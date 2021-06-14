using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Maintenance
    {
        [Key]
        public int MaintenanceId { get; set; }
        public bool IsMaintenance { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDateTime { get; set; }
        [Required]
        [StringLength(2000)]
        public string Message { get; set; }
    }
}
