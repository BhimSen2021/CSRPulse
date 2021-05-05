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

        [InverseProperty("Menu")]
        public virtual ICollection<UserRights> UserRights { get; set; }
    }
}
