using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Block
    {
        public Block()
        {
            Village = new HashSet<Village>();
        }

        [Key]
        public int BlockId { get; set; }
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        [Required]
        [StringLength(50)]
        public string BlockName { get; set; }
        [Required]
        [StringLength(5)]
        public string BlockCode { get; set; }
        public int BlockType { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.BlockCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("Block")]
        public virtual District District { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("Block")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.BlockUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty("Block")]
        public virtual ICollection<Village> Village { get; set; }
    }
}
