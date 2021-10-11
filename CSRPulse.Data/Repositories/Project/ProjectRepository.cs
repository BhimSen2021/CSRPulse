using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSRPulse.Data.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {

        public IQueryable<ProjectLocationDetail> GetTvLocationDetails(int projectId, int? locationLavel = 2)
        {
            if (locationLavel == 4)
            {
                var level4 = from l in _dbContext.ProjectLocation.Where(x => x.ProjectId == projectId)
                             join s in _dbContext.State on l.StateId equals s.StateId
                             join d in _dbContext.District on l.DistrictId equals d.DistrictId
                             join b in _dbContext.Block on l.DistrictId equals b.DistrictId into block
                             from b1 in block.DefaultIfEmpty()
                             join v in _dbContext.Village on b1.BlockId equals v.BlockId into village
                             from v1 in village.DefaultIfEmpty()
                             where b1.BlockName != null && v1.VillageName != null
                             select new ProjectLocationDetail
                             {
                                 StateId = l.StateId,
                                 StateName = s.StateName,
                                 DistrictId = l.DistrictId,
                                 DistrictName = d.DistrictName,
                                 BlockId = b1.BlockId,
                                 BlockName = b1.BlockName,
                                 VillageId = v1.VillageId,
                                 VillageName = v1.VillageName
                             };
                return level4;
            }
            else if (locationLavel == 3)
            {
                var level3 = from l in _dbContext.ProjectLocation.Where(x => x.ProjectId == projectId)
                             join s in _dbContext.State on l.StateId equals s.StateId
                             join d in _dbContext.District on l.DistrictId equals d.DistrictId
                             join b in _dbContext.Block on l.DistrictId equals b.DistrictId into block
                             from b1 in block.DefaultIfEmpty()
                             where b1.BlockName != null
                             select new ProjectLocationDetail
                             {
                                 StateId = l.StateId,
                                 StateName = s.StateName,
                                 DistrictId = l.DistrictId,
                                 DistrictName = d.DistrictName,
                                 BlockId = b1.BlockId,
                                 BlockName = b1.BlockName
                             };
                return level3;
            }
            else
            {
                var level2 = from l in _dbContext.ProjectLocation.Where(x => x.ProjectId == projectId)
                             join s in _dbContext.State on l.StateId equals s.StateId
                             join d in _dbContext.District on l.DistrictId equals d.DistrictId
                             select new ProjectLocationDetail
                             {
                                 StateId = l.StateId,
                                 StateName = s.StateName,
                                 DistrictId = l.DistrictId,
                                 DistrictName = d.DistrictName
                             };
                return level2;
            }
        }
        public IQueryable<ProjectDocument> GetDocuments(int projectId, int processId)
        {
            return from m in _dbContext.ProcessDocument.Where(x => x.ProcessId == processId && x.IsActive == true)
                   join d in _dbContext.ProjectDocument.Where(d => d.ProjectId == projectId)
                   on m.DocumentId equals d.DocumentId into doc
                   from d1 in doc.DefaultIfEmpty()
                   select new ProjectDocument
                   {
                       ProjectDocumentId = d1.ProjectDocumentId,
                       ProjectId = d1.ProjectId,
                       DocumentId = m.DocumentId,
                       DocumentName = d1.DocumentName,
                       MDocumentName = m.DocumentName,
                       DocumentMaxSize = m.DocumentMaxSize ?? 10,
                       DocumentType = m.DocumentType,
                       ServerDocumentName = d1.ServerFileName,
                       Mandatory = m.Mandatory ?? false,
                       CreatedBy = m.CreatedBy,
                       CreatedOn = m.CreatedOn,
                       CreatedRid = m.CreatedRid,
                       CreatedRname = m.CreatedRname
                   };
        }
        public IQueryable<ProjectTeam> Getteammemberslist(int projectId, int roleId)
        {
            return from tm in _dbContext.ProjectTeamDetail.Where(x => x.ProjectId == projectId)
                   join r in _dbContext.Role on tm.RoleId equals r.RoleId
                   join u in _dbContext.User on tm.RoleId equals u.RoleId
                   select new ProjectTeam
                   {
                       ProjectTeamDetailId = tm.ProjectTeamDetailId,
                       ProjectId = tm.ProjectId,
                       UserId = tm.UserId,
                       RoleId = tm.RoleId,
                       RoleName = r.RoleName,
                       FullName = u.FullName,
                       EmailID = u.EmailId,
                       MobileNo = u.MobileNo,
                       FromDate = tm.FromDate,
                       ToDate = tm.ToDate

                   };
        }
        public IQueryable<ProjectTeam> GetTeamMemeber(int roleId)
        {
            return from UR in _dbContext.User.Where(x => x.RoleId != roleId)
                   join RL in _dbContext.Role on UR.RoleId equals RL.RoleId
                   select new ProjectTeam
                   {
                       UserId = UR.UserId,
                       RoleId = UR.RoleId,
                       RoleName = RL.RoleName,
                       FullName = UR.FullName,
                       EmailID = UR.EmailId,
                       MobileNo = UR.MobileNo,
                   };
        }
        public IQueryable<ProjectOverviewModule> GetOverview(int projectId)
        {
            return (from Q in _dbContext.ProjectNarrativeQuestion.Where(x => x.ProjectId == projectId)
                    join A in _dbContext.ProjectNarrativeAnswer.Where(u => u.ProjectId == projectId)
                   on Q.ProjectQuestionId equals A.ProjectQuestionId into assigned
                    from au in assigned.DefaultIfEmpty()
                    select new ProjectOverviewModule
                    {
                        ProjectQuestionId = Q.ProjectQuestionId,
                        ProjectId = Q.ProjectId,
                        QuestionId = Q.QuestionId,
                        Question = Q.Question,
                        ProcessId = Q.ProcessId,
                        CommentRequire = Q.CommentRequire,
                        QuestionType = Q.QuestionType,
                        ContentLimit = Q.ContentLimit,
                        OrderNo = Q.OrderNo,
                        Projectanswer = au.Answer,
                        ProjectAnswerId = au.ProjectAnswerId
                    });

        }
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

        }
        public class ProjectDocument
        {
            public int ProjectDocumentId { get; set; }
            public int ProjectId { get; set; }
            public int DocumentId { get; set; }
            public string DocumentName { get; set; }
            public string MDocumentName { get; set; }
            public string ServerDocumentName { get; set; }
            public string DocumentType { get; set; }
            public int DocumentMaxSize { get; set; }
            public bool Mandatory { get; set; }
            public int CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public int CreatedRid { get; set; }
            public string CreatedRname { get; set; }
        }
        public class ProjectTeam
        {
            public int ProjectTeamDetailId { get; set; }
            public int ProjectId { get; set; }
            public int UserId { get; set; }
            public int OldUserId { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public string FullName { get; set; }
            public string MobileNo { get; set; }
            public string EmailID { get; set; }
            public int RoleId { get; set; }
            public string RoleName { get; set; }
            public string DepartmentName { get; set; }
            public int? DepartmentId { get; set; }
            public int? DesignationId { get; set; }
            public bool AssigneTeam { get; set; }
            public int CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public int CreatedRid { get; set; }
            public string CreatedRname { get; set; }

        }
        public class ProjectOverviewModule 
        {
            public int ProjectQuestionId { get; set; }
            public int ProjectId { get; set; }
            public int QuestionId { get; set; }
            public string Question { get; set; }
            public int ProcessId { get; set; }
            public bool? CommentRequire { get; set; }
            public string QuestionType { get; set; }
            public int? ContentLimit { get; set; }
            public int? OrderNo { get; set; }
            public string Projectanswer { get; set; }
            public int ProjectAnswerId { get; set; }

        }
    }
}
