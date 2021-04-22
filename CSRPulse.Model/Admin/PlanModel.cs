using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CSRPulse.Model
{
    public class PlanModel : BaseModel
    {
        [Key]
        [Column("PlanID")]
        public int PlanId { get; set; }
        [Required]
        [StringLength(100), Display(Name = "Plan Name")]
        public string PlanName { get; set; }
        [StringLength(500), Display(Name = "Plan Detail")]
        public string PlanDetail { get; set; }
    }
}
