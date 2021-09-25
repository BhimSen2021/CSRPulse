using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    [Table("NGOMember")]
    public partial class Ngomember
    {
        [Key]
        [Column("NGOMemberId")]
        public int NgomemberId { get; set; }
        public int PartnerId { get; set; }
        public byte MemberType { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public byte Gender { get; set; }
        [Required]
        [StringLength(200)]
        public string Designation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JoiningDate { get; set; }
        [StringLength(256)]
        public string Qualification { get; set; }
        [StringLength(4000)]
        public string Experience { get; set; }
        [StringLength(256)]
        public string Address { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        public int? Pincode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        public byte? Age { get; set; }
        [Column("PANNo")]
        [StringLength(10)]
        public string Panno { get; set; }
        [StringLength(10)]
        public string PassportNo { get; set; }
        [StringLength(10)]
        public string VoterId { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(500)]
        public string AffiliatedOrganisation { get; set; }
        public bool? IsDrawingRemuneration { get; set; }
        public bool? IsCrimeDescription { get; set; }
        [StringLength(500)]
        public string CrimeDescription { get; set; }
        [Column("IsMemberRelatedtoABF")]
        public bool? IsMemberRelatedtoAbf { get; set; }
        [Column("MemberRelatedtoABFDetail")]
        [StringLength(500)]
        public string MemberRelatedtoAbfdetail { get; set; }
        public bool? IsMemberWillfulDefaulter { get; set; }
        [StringLength(500)]
        public string MemberWillfulDefaulterDetail { get; set; }
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
        [InverseProperty(nameof(User.NgomemberCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.NgomemberCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("Ngomember")]
        public virtual Partner Partner { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.NgomemberUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.NgomemberUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
