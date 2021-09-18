using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class EmailConfiguration
    {
        [Key]
        [Column("EmailConfigurationID")]
        public int EmailConfigurationId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Server { get; set; }
        public int Port { get; set; }
        [Column("SSLStatus")]
        public bool Sslstatus { get; set; }
        [StringLength(50)]
        public string FriendlyName { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.EmailConfigurationCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.EmailConfigurationUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}
