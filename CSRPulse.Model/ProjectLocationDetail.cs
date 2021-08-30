using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Total Village")]
        public int TotalVillage { get; set; }
        [Display(Name = "State Name")]
        public string StateName { get; set; }
        [Display(Name = "District Name")]
        public string DistrictName { get; set; }
        [Display(Name = "Block Name")]
        public string BlockName { get; set; }
        [Display(Name = "Village Name")]
        public string VillageName { get; set; }
        [Display(Name = "Location Type")]
        public string LocationType { get; set; }
        public bool IsActive { get; set; }
    }
}
