using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CSRPulse.Model
{
    public class BaseModel
    {
        public bool IsDeleted { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public string ErrorMessage { get; set; }
        public bool IsExist { get; set; }
    }
}
