using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSRPulse.Data.Repositories
{
    public class PartnerRepository : BaseRepository, IPartnerRepository
    {
        public IQueryable<Document> GetDocuments(int partnerId)
        {
            var result = from d in _dbContext.ProcessDocument
                         join p in _dbContext.PartnerDocument.Where(x => x.PartnerId == partnerId) 
                         on d.DocumentId equals p.DocumentId into er1
                         from x1 in er1.DefaultIfEmpty()
                         where d.ProcessId == 2
                         select new Document
                         {
                             DocumentId = d.DocumentId,
                             PartnerId = x1.PartnerId,
                             DocumentName = d.DocumentName,
                             UploadedDocumentName=x1.UploadedDocumentName,
                             ServerDocumentName=x1.ServerDocumentName,
                             PartnerDocumentId=x1.PartnerDocumentId,
                             Remark =x1.Remark,
                             Mandatory = d.Mandatory??false,
                             DocumentType=d.DocumentType,
                             DocumentMaxSize=d.DocumentMaxSize??20,
                             IsUploaded = (x1 == null ? false : true)

                         };
            return result;
        }

        public IQueryable<Document> GetPartnerDocuments(int partnerId, int processId)
        {
            return from m in _dbContext.ProcessDocument.Where(x => x.ProcessId == processId && x.IsActive == true)
                   join d in _dbContext.PartnerDocument.Where(d => d.PartnerId == partnerId)
                   on m.DocumentId equals d.DocumentId into doc
                   from d1 in doc.DefaultIfEmpty()
                   select new Document
                   {
                       PartnerDocumentId = d1.PartnerDocumentId,
                       PartnerId = d1.PartnerId,
                       DocumentId = m.DocumentId,
                       UploadedDocumentName = d1.UploadedDocumentName,
                       DocumentName = m.DocumentName,
                       DocumentMaxSize = m.DocumentMaxSize ?? 10,
                       DocumentType = m.DocumentType,
                       ServerDocumentName = d1.ServerDocumentName,
                       Remark=d1.Remark,
                       Mandatory = m.Mandatory ?? false,
                       IsUploaded = (d1 == null ? false : true)

                   };
        }
    }
    public class Document
    {
        public int PartnerDocumentId { get; set; }
        public int PartnerId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string UploadedDocumentName { get; set; }
        public bool IsUploaded { get; set; }
        public string MDocumentName { get; set; }
        public string ServerDocumentName { get; set; }
        public string Remark { get; set; }
        public string DocumentType { get; set; }
        public int DocumentMaxSize { get; set; }
        public bool Mandatory { get; set; }

    }
}
