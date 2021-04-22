using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
   public class StartingNumber:BaseModel
    {
        public int StartNumberID { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string Prefix { get; set; }
        public int Number { get; set; }
        public byte NumberWidth { get; set; }
    }
}
