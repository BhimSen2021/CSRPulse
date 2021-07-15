using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            ProcessSetupPrimaryRole = new HashSet<ProcessSetup>();
            ProcessSetupQuaternaryRole = new HashSet<ProcessSetup>();
            ProcessSetupSecondoryRole = new HashSet<ProcessSetup>();
            ProcessSetupTertiaryRole = new HashSet<ProcessSetup>();
            User = new HashSet<User>();
        }

        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }
        [Required]
        [StringLength(10)]
        public string RoleShortName { get; set; }
        public int? Seniorty { get; set; }
        [StringLength(500)]
        public string Details { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public int? ReportTo { get; set; }

        [InverseProperty(nameof(ProcessSetup.PrimaryRole))]
        public virtual ICollection<ProcessSetup> ProcessSetupPrimaryRole { get; set; }
        [InverseProperty(nameof(ProcessSetup.QuaternaryRole))]
        public virtual ICollection<ProcessSetup> ProcessSetupQuaternaryRole { get; set; }
        [InverseProperty(nameof(ProcessSetup.SecondoryRole))]
        public virtual ICollection<ProcessSetup> ProcessSetupSecondoryRole { get; set; }
        [InverseProperty(nameof(ProcessSetup.TertiaryRole))]
        public virtual ICollection<ProcessSetup> ProcessSetupTertiaryRole { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<User> User { get; set; }
    }
}
