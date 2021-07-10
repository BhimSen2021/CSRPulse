using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class DistrictImport:ImportBaseModel
    {
        public string StateName { get; set; }
        public string DistrictName { get; set; }     
        public string DistrictCode { get; set; }      
        public string DistrictShort { get; set; }
        public string StateId { get; set; }
        public string DistrictId { get; set; }

        public string  State { get; set; }
        public string District { get; set; }
    }

    public class DistrictColValues
    {
        public List<string> State { set; get; }
        public List<string> District { set; get; }
    }
}
