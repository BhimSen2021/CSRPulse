using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProjectCommunication
    {
        [Key]
        public int ProjectCommunicationId { get; set; }
        public int ProjectId { get; set; }
        [Required]
        [StringLength(256)]
        public string Subject { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CommunicationDate { get; set; }
        [Required]
        [StringLength(2000)]
        public string Communication { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectCommunication")]
        public virtual Project Project { get; set; }
    }
}
