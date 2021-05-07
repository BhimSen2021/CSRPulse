using System;
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
            District = new HashSet<District>();
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

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.StateCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.StateUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty("State")]
        public virtual ICollection<District> District { get; set; }
    }
}
