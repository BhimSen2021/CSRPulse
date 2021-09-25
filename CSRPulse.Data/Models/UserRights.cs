using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class UserRights
    {
        [Key]
        public int UserRightId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool ShowMenu { get; set; }
        public bool CreateRight { get; set; }
        public bool ViewRight { get; set; }
        public bool EditRight { get; set; }
        public bool DeleteRight { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
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
        [InverseProperty(nameof(User.UserRightsCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty("UserRightsCreatedR")]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(MenuId))]
        [InverseProperty("UserRights")]
        public virtual Menu Menu { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("UserRightsRole")]
        public virtual Role Role { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.UserRightsUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty("UserRightsUpdatedR")]
        public virtual Role UpdatedR { get; set; }
    }
}
