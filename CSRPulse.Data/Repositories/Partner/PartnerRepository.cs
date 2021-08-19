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
                         join p in _dbContext.PartnerDocument.Where(x => x.PartnerId == partnerId) on d.DocumentId equals p.DocumentId into er1
                         from x1 in er1.DefaultIfEmpty()
                         where d.ProcessId == 2
                         select new Document
                         {
                             DocumentId = d.DocumentId,
                             PartnetId = x1.PartnerId,
                             DocumentName = d.DocumentName,
                             IsUploaded = (x1 == null ? false : true)

                         };
            return result;
        }
    }

    public class Document
    {
        public int PartnetId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public bool IsUploaded { get; set; }

    }
}
