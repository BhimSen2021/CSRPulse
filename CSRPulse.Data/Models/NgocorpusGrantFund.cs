using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    [Table("NGOCorpusGrantFund")]
    public partial class NgocorpusGrantFund
    {
        [Key]
        [Column("NGOCorpusGrantFundId")]
        public int NgocorpusGrantFundId { get; set; }
        public int PartnerId { get; set; }
        public byte FundType { get; set; }
        public int YearId { get; set; }
        [Column(TypeName = "money")]
        public decimal? FundsAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("NgocorpusGrantFund")]
        public virtual Partner Partner { get; set; }
    }
}
