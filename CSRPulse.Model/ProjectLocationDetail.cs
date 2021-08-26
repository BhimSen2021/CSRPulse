using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectLocationDetail
    {
        public int LocationId { get; set; }
        public int ProjectId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int? BlockId { get; set; }
        public int? VillageId { get; set; }
        public int TotalVillage { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string VillageName { get; set; }
        public string LocationType { get; set; }
        public bool IsActive { get; set; }
    }
}
