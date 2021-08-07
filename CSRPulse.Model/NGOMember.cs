using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class NGOMember : BaseModel
    {       
        [Key]
        public int NgomemberId { get; set; }
        public int PartnerId { get; set; }
        public byte MemberType { get; set; }
        [Required, StringLength(150),Display(Name = "Name of the Member")]
        public string Name { get; set; }
        public byte Gender { get; set; }
        [Required, StringLength(200)]       
        public string Designation { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Joining Date")]
        public DateTime? JoiningDate { get; set; }
        [StringLength(256)]
        public string Qualification { get; set; }
        [StringLength(4000)]
        public string Experience { get; set; }
        [StringLength(256)]
        public string Address { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [Display(Name = "PIN Code")]
        public int? Pincode { get; set; }

        [DataType(DataType.Date), Display(Name = "Date Of Birth")]        
        public DateTime? DateOfBirth { get; set; }        
        public byte? Age { get; set; }
       
        [StringLength(10), Display(Name ="PAN No")]
        [RegularExpression(@"^[A-Za-z]{5}\d{4}[A-Za-z]{1}$", ErrorMessage = "Invalid PAN Format")]
        public string Panno { get; set; }
        [StringLength(10), Display(Name = "Passport No")]
        public string PassportNo { get; set; }
        [StringLength(10), Display(Name = "Voter Id")]
        public string VoterId { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(500),Display(Name = "Details of formal affiliations with other organization")]
        public string AffiliatedOrganisation { get; set; }
        [Display(Name = "Is the Member drawing remuneration(Y/N)")]
        public bool IsDrawingRemuneration { get; set; }
        [Display(Name = "Has the member been convicted of any crime?(If yes,please specify)")]
        public bool IsCrimeDescription { get; set; }

        [StringLength(500)]
        public string CrimeDescription { get; set; }
        [Display(Name = "Are the Board Members / Trustees related to the employees of ABF")]
        public bool IsMemberRelatedtoAbf { get; set; }       
        [StringLength(500)]
        public string MemberRelatedtoAbfdetail { get; set; }
        [Display(Name = "Has the member been declared as a willful defaulter?(If yes,please specify)")]
        public bool IsMemberWillfulDefaulter { get; set; }
        [StringLength(500), Display(Name = "Has the member been declared as a willful defaulter?(If yes,please specify)")]
        public string MemberWillfulDefaulterDetail { get; set; }
    }
}
