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
                       ServerDocumentName = d1.ServerDocumentName,
                       Mandatory = m.Mandatory ?? false
                   };
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
        }
    }
}
