using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class NgoawardDetail : BaseModel
    {
        [Key]
        public int NgoawardDetailId { get; set; }
        public int PartnerId { get; set; }
        [StringLength(500)]
        [Display(Name = "Name of the Award/Recognition")]
        public string Award { get; set; }

        [Display(Name = "Year of Receiving")]
        public int YearOfReceiving { get; set; }

        [StringLength(500)]
        [Display(Name = "Name of the Body/ organization who conferred the Award/ Recognition")]
        public string AwardConferrer { get; set; }
    }
}
