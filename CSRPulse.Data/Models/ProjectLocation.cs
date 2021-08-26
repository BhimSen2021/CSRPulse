using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProjectLocation
    {
        [Key]
        public int LocationId { get; set; }
        public int ProjectId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }

        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("ProjectLocation")]
        public virtual District District { get; set; }
        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectLocation")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("ProjectLocation")]
        public virtual State State { get; set; }
    }
}
