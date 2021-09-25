using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Auditor
    {
        public Auditor()
        {
            AuditorDocument = new HashSet<AuditorDocument>();
        }

        [Key]
        public int AuditorId { get; set; }
        [Required]
        [StringLength(256)]
        public string AuditOrganization { get; set; }
        [StringLength(10)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Website { get; set; }
        [StringLength(256)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        public int? StateId { get; set; }
        public int? PinCode { get; set; }
        [Column("GSTNo")]
        [StringLength(20)]
        public string Gstno { get; set; }
        [Column("PAN")]
        [StringLength(20)]
        public string Pan { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
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
        [InverseProperty(nameof(User.AuditorCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.AuditorCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("Auditor")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.AuditorUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.AuditorUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("Auditor")]
        public virtual ICollection<AuditorDocument> AuditorDocument { get; set; }
    }
}
