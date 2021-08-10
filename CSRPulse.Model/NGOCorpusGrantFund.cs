using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class NGOCorpusGrantFund : BaseModel
    {
        [Key]
        public int NgocorpusGrantFundId { get; set; }
        public int PartnerId { get; set; }
        public byte FundType { get; set; }
        public int YearId { get; set; }
        [Display(Name = "Amount(₹)")]
        public decimal? FundsAmount { get; set; }
    }
}
