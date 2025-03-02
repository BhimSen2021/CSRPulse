﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectTeamDetail:BaseModel
    {
        public int ProjectTeamDetailId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int? OldUserID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public int? DesignationId { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
        public bool AssigneTeam { get; set; }
    }
}
