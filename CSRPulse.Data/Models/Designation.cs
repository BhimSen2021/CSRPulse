using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Designation
    {
        public Designation()
        {
            DesignationHistory = new HashSet<DesignationHistory>();
            User = new HashSet<User>();
        }

        [Key]
        public int DesignationId { get; set; }
        [Required]
        [StringLength(256)]
        public string DesignationName { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [InverseProperty("Designation")]
        public virtual ICollection<DesignationHistory> DesignationHistory { get; set; }
        [InverseProperty("Designation")]
        public virtual ICollection<User> User { get; set; }
    }
}
