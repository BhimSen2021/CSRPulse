using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Designation : BaseModel
    {
        [Key]
        public int DesignationId { get; set; }
        [Required]
        [StringLength(256), MinLength(2, ErrorMessage = "Department should be contain atleast 2 characters.")]
        [Display(Name = "Designation")]
        public string DesignationName { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
    }
}
