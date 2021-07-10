using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Role : BaseModel
    {
        public int RoleId { get; set; }
        [Required]
        [Display(Name = "Role Name"), StringLength(256)]
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "Role Short Name"), StringLength(10)]
        public string RoleShortName { get; set; }
        [StringLength(500)]
        public string Details { get; set; }
        public int? Seniorty { get; set; }
       
        public int? ReportTo { get; set; }

        public bool RecordExist { get; set; }
    }
}
