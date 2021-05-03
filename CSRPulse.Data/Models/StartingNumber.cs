using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class StartingNumber
    {
        public int StartNumberId { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string Prefix { get; set; }
        public int Number { get; set; }
        public byte NumberWidth { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
