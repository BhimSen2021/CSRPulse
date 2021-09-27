using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }      
        public List<Document> documents { get; set; }
    }
}
