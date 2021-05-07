using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class User
    {
        public User()
        {
            DistrictCreatedByNavigation = new HashSet<District>();
            DistrictUpdatedByNavigation = new HashSet<District>();
            StateCreatedByNavigation = new HashSet<State>();
            StateUpdatedByNavigation = new HashSet<State>();
            UserRights = new HashSet<UserRights>();
        }

        [Key]
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(200)]
        public string Password { get; set; }
        [StringLength(10)]
        public string MobileNo { get; set; }
        [Required]
        [Column("EmailID")]
        [StringLength(50)]
        public string EmailId { get; set; }
        [StringLength(50)]
        public string ImageName { get; set; }
        public int? DefaultMenuId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(UserTypeId))]
        [InverseProperty("User")]
        public virtual UserType UserType { get; set; }
        [InverseProperty(nameof(District.CreatedByNavigation))]
        public virtual ICollection<District> DistrictCreatedByNavigation { get; set; }
        [InverseProperty(nameof(District.UpdatedByNavigation))]
        public virtual ICollection<District> DistrictUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(State.CreatedByNavigation))]
        public virtual ICollection<State> StateCreatedByNavigation { get; set; }
        [InverseProperty(nameof(State.UpdatedByNavigation))]
        public virtual ICollection<State> StateUpdatedByNavigation { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRights> UserRights { get; set; }
    }
}
