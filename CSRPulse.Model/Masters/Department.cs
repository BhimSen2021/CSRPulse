using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
   public class Department:BaseModel
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [StringLength(200), MinLength(2, ErrorMessage = "Department should be contain atleast 2 characters.")]
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
        public bool RecordExist { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
    }
}
