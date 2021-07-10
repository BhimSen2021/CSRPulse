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
            BlockCreatedByNavigation = new HashSet<Block>();
            BlockUpdatedByNavigation = new HashSet<Block>();
            DesignationHistory = new HashSet<DesignationHistory>();
            DistrictCreatedByNavigation = new HashSet<District>();
            DistrictUpdatedByNavigation = new HashSet<District>();
            EmailConfigurationCreatedByNavigation = new HashSet<EmailConfiguration>();
            EmailConfigurationUpdatedByNavigation = new HashSet<EmailConfiguration>();
            ProcessWorkFlowReceiver = new HashSet<ProcessWorkFlow>();
            ProcessWorkFlowSender = new HashSet<ProcessWorkFlow>();
            StateCreatedByNavigation = new HashSet<State>();
            StateUpdatedByNavigation = new HashSet<State>();
            UserRights = new HashSet<UserRights>();
            VillageCreatedByNavigation = new HashSet<Village>();
            VillageUpdatedByNavigation = new HashSet<Village>();
        }

        [Key]
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int RoleId { get; set; }
        public int? PartnerId { get; set; }
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
        public byte? WrongAttemp { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LockDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLogin { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("User")]
        public virtual Department Department { get; set; }
        [ForeignKey(nameof(DesignationId))]
        [InverseProperty("User")]
        public virtual Designation Designation { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("User")]
        public virtual Role Role { get; set; }
        [ForeignKey(nameof(UserTypeId))]
        [InverseProperty("User")]
        public virtual UserType UserType { get; set; }
        [InverseProperty(nameof(Block.CreatedByNavigation))]
        public virtual ICollection<Block> BlockCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Block.UpdatedByNavigation))]
        public virtual ICollection<Block> BlockUpdatedByNavigation { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<DesignationHistory> DesignationHistory { get; set; }
        [InverseProperty(nameof(District.CreatedByNavigation))]
        public virtual ICollection<District> DistrictCreatedByNavigation { get; set; }
        [InverseProperty(nameof(District.UpdatedByNavigation))]
        public virtual ICollection<District> DistrictUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(EmailConfiguration.CreatedByNavigation))]
        public virtual ICollection<EmailConfiguration> EmailConfigurationCreatedByNavigation { get; set; }
        [InverseProperty(nameof(EmailConfiguration.UpdatedByNavigation))]
        public virtual ICollection<EmailConfiguration> EmailConfigurationUpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProcessWorkFlow.Receiver))]
        public virtual ICollection<ProcessWorkFlow> ProcessWorkFlowReceiver { get; set; }
        [InverseProperty(nameof(ProcessWorkFlow.Sender))]
        public virtual ICollection<ProcessWorkFlow> ProcessWorkFlowSender { get; set; }
        [InverseProperty(nameof(State.CreatedByNavigation))]
        public virtual ICollection<State> StateCreatedByNavigation { get; set; }
        [InverseProperty(nameof(State.UpdatedByNavigation))]
        public virtual ICollection<State> StateUpdatedByNavigation { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRights> UserRights { get; set; }
        [InverseProperty(nameof(Village.CreatedByNavigation))]
        public virtual ICollection<Village> VillageCreatedByNavigation { get; set; }
        [InverseProperty(nameof(Village.UpdatedByNavigation))]
        public virtual ICollection<Village> VillageUpdatedByNavigation { get; set; }
    }
}
