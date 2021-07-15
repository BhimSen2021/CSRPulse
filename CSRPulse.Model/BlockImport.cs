using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
   public class BlockImport: ImportBaseModel
    {
        public string StateId { get; set; }
        public string DistrictId { get; set; }
        public string BlockId { get; set; }
        public string BlockCode { get; set; }
        public string BlockType { get; set; }       
        public string BlockName { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Block { get; set; }

    }

    public class BlockColValues
    {
        public List<string> State { set; get; }
        public List<string> District { set; get; }
        public List<string> Block { set; get; }
    }
}
