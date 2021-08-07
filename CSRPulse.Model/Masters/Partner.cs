using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Partner : BaseModel
    {
        [Key]
        public int PartnerId { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "Partner name should be contain atleast 2 characters.")]
        [Display(Name = "Partner Name")]
        public string PartnerName { get; set; }

        [Required]
        [Display(Name = "Partner Type")]
        public int PartnerType { get; set; }

        [Display(Name = "Work Email"), Required(ErrorMessage = "Please Enter Work Email ID")]
        [StringLength(50, ErrorMessage = "{0} not be exceed 50 char"), DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email.")]
        public string Email { get; set; }
        [StringLength(50)]

        [Display(Name = "Web Site")]
        public string Website { get; set; }
        [Display(Name = "Date of Established"), DataType(DataType.Date), Required(ErrorMessage = "Please Enter date of established")]
        public DateTime DateOfEst { get; set; }
        [Display(Name = "Dealing with us since"), DataType(DataType.Date)]
        public DateTime? DealingWithUsSince { get; set; }
        [Display(Name = "Breif History of the Partner")]
        public string PartnerDetails { get; set; }
        [Display(Name = "Mission and Vision")]
        public string MissionVision { get; set; }

        [StringLength(256)]
        [Display(Name = "Address"), Required(ErrorMessage = "Please Enter Address")]
        public string RegAddress { get; set; }

        [StringLength(50)]
        [Display(Name = "City"), Required(ErrorMessage = "Please Enter City")]
        public string RegCity { get; set; }

        [Display(Name = "PIN Code"), Required(ErrorMessage = "Please Enter PIN Code")]
        public int RegPin { get; set; }

        [Display(Name = "State"), Required(ErrorMessage = "Please Select State")]
        public int RegState { get; set; }
        [StringLength(12, MinimumLength = 10, ErrorMessage = "Phone No must be contain 10 digits number")]
        [Display(Name = "Phone No")]
        public string RegPhone { get; set; }

        [StringLength(12, MinimumLength = 10, ErrorMessage = "Mobile No must be contain 10 digits number")]
        [Display(Name = "Mobile No")]
        public string RegMobile { get; set; }

        [StringLength(256)]
        [Display(Name = "Address"), Required(ErrorMessage = "Please Enter Address")]
        public string CommAddress { get; set; }
        [StringLength(50)]
        [Display(Name = "City"), Required(ErrorMessage = "Please Enter City")]
        public string ComCity { get; set; }
        [Display(Name = "PIN Code"), Required(ErrorMessage = "Please Enter PIN Code")]
        public int? ComPin { get; set; }
        [Display(Name = "State"), Required(ErrorMessage = "Please Select State")]
        public int? ComState { get; set; }
        [StringLength(12, MinimumLength = 10, ErrorMessage = "Phone No must be contain 10 digits number")]
        [Display(Name = "Phone No")]
        public string CommPhone { get; set; }

        [StringLength(12, MinimumLength = 10, ErrorMessage = "Mobile No must be contain 10 digits number")]
        [Display(Name = "Mobile No")]
        public string CommMobile { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }

        [Display(Name = "Program Type")]
        public int? ProgramType { get; set; }

        [Display(Name = "Does the Organization have any religious or political objectives?")]
        public bool? IsReligiousPoliticalObjectives { get; set; }
        [StringLength(500, ErrorMessage = "Length does not exceed 500 characters.")]
        public string ReligiousPoliticalObjectives { get; set; }
        [Display(Name = "Whether the entire grant proposal falls within the constitutional document?")]
        public bool? IsConstitutionalDocument { get; set; }

        [Display(Name = "List of Awards/Recognitions given to the Organization")]
        public List<NgoawardDetail> NgoawardDetail { get; set; }
        [Display(Name = "Details of the Statutory Auditor")]
        public List<NgosaturatoryAuditorDetail> NgosaturatoryAuditorDetail { get; set; }
        [Display(Name = "Details of current key projects of the Organization")]
        public List<NGOKeyProjects> NGOKeyProjects { get; set; }

        [Display(Name = "Corpus fund and Grant Inflow")]
        public List<NGOCorpusGrantFund> NgocorpusGrantFund { get; set; }

        [Display(Name = "NGO Registration Details")]
        public List<NGORegistrationDetail> NgoregistrationDetail { get; set; }

        [Display(Name = "Organization Chart specifying supervisory, admin, project related, advisors, consultants")]
        public List<NGOChartDocument> NgochartDocument { get; set; }

        public List<NGOMember> Ngomember { get; set; }

    }


}
