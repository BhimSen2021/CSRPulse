﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class State
    {
        public State()
        {
            Auditor = new HashSet<Auditor>();
            Block = new HashSet<Block>();
            District = new HashSet<District>();
            ProjectLocation = new HashSet<ProjectLocation>();
            ProjectLocationDetail = new HashSet<ProjectLocationDetail>();
            Village = new HashSet<Village>();
        }

        [Key]
        public int StateId { get; set; }
        [Required]
        [StringLength(200)]
        public string StateName { get; set; }
        [Required]
        [StringLength(2)]
        public string StateCode { get; set; }
        [StringLength(4)]
        public string StateShort { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? Longitude { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? Latitude { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column("CreatedRId")]
        public int CreatedRid { get; set; }
        [Required]
        [Column("CreatedRName")]
        [StringLength(256)]
        public string CreatedRname { get; set; }
        [Column("UpdatedRId")]
        public int? UpdatedRid { get; set; }
        [Column("UpdatedRName")]
        [StringLength(256)]
        public string UpdatedRname { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.StateCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.StateCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.StateUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.StateUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("State")]
        public virtual ICollection<Auditor> Auditor { get; set; }
        [InverseProperty("State")]
        public virtual ICollection<Block> Block { get; set; }
        [InverseProperty("State")]
        public virtual ICollection<District> District { get; set; }
        [InverseProperty("State")]
        public virtual ICollection<ProjectLocation> ProjectLocation { get; set; }
        [InverseProperty("State")]
        public virtual ICollection<ProjectLocationDetail> ProjectLocationDetail { get; set; }
        [InverseProperty("State")]
        public virtual ICollection<Village> Village { get; set; }
    }
}
