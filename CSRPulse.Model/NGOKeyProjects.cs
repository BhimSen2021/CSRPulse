using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
   public class NGOKeyProjects:BaseModel
    {
        [Key]
        public int NgokeyProjectId { get; set; }
        public int PartnerId { get; set; }
        [StringLength(100), Display(Name = "Name of the Donor Agency")]
        public string DonorAgency { get; set; }
        [StringLength(500), Display(Name = "Project Objective")]
        public string ProjectObjective { get; set; }
        [StringLength(500), Display(Name = "Project Location (District, State)")]
        public string ProjectLocation { get; set; } 
        [StringLength(10), Display(Name = "Start(mm/yyyy)")]       
        public string ProjectStart { get; set; }
        [StringLength(10), Display(Name = "End(mm/yyyy)")]
        //[RegularExpression(@"^(((0)[1-9])|((1)[0-2]))(\/)\d{4}$", ErrorMessage = "Please enter a valid end date {mm/yyyy}.")]
        public string ProjectEnd { get; set; }
        [Display(Name = "Grant Amount(₹)")]
        public decimal? GrantAmount { get; set; }
        [StringLength(500)]
        [Display(Name = "Additional Information, if any")]
        public string AdditionalInformation { get; set; }
    }
}
