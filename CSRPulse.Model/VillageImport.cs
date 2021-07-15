using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
   public class VillageImport : ImportBaseModel
    {
        public string StateId { get; set; }
        public string DistrictId { get; set; }
        public string BlockId { get; set; }
        public string VillageId { get; set; }
        public string VillageCode { get; set; }
        public string LocationType { get; set; }
        public string VillageName { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
        public string Village { get; set; }
    }

    public class VillageColValues
    {
        public List<string> State { set; get; }
        public List<string> District { set; get; }
        public List<string> Block { set; get; }
        public List<string> Village { set; get; }
    }
}
