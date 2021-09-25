using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Menu
    {
        public Menu()
        {
            UserRights = new HashSet<UserRights>();
        }

        [Key]
        public int MenuId { get; set; }
        [Required]
        [StringLength(100)]
        public string MenuName { get; set; }
        public int? ParentMenuId { get; set; }
        public int? SequenceNo { get; set; }
        [StringLength(100)]
        public string Area { get; set; }
        [StringLength(100)]
        public string Url { get; set; }
        [StringLength(100)]
        public string IconClass { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool ActiveOnMobile { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        [Column(TypeName = "text")]
        public string Help { get; set; }
        public bool IsDeleted { get; set; }
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
        [InverseProperty(nameof(User.MenuCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.MenuCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.MenuUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.MenuUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("Menu")]
        public virtual ICollection<UserRights> UserRights { get; set; }
    }
}
