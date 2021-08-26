using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProjectLocationDetail
    {
        [Key]
        public int LocationId { get; set; }
        public int ProjectId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int? BlockId { get; set; }
        public int? VillageId { get; set; }
        [Required]
        public bool? IsActive { get; set; }

        [ForeignKey(nameof(BlockId))]
        [InverseProperty("ProjectLocationDetail")]
        public virtual Block Block { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("ProjectLocationDetail")]
        public virtual District District { get; set; }
        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectLocationDetail")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("ProjectLocationDetail")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(VillageId))]
        [InverseProperty("ProjectLocationDetail")]
        public virtual Village Village { get; set; }
    }
}
