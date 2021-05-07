using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class District
    {
        [Key]
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        [Required]
        [StringLength(200)]
        public string DistrictName { get; set; }
        [Required]
        [StringLength(4)]
        public string DistrictCode { get; set; }
        [StringLength(4)]
        public string DistrictShort { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.DistrictCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("District")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.DistrictUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}
