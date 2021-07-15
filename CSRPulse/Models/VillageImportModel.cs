using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Model;
namespace CSRPulse.Models
{
    public class VillageImportModel
    {
        public List<VillageImport> VillageData { set; get; }
        public Dictionary<string, string> ErrorMsgCollection { get; set; }
        public int NoOfErrors { get; set; }
        public string Message { get; set; }
        public DataTable VillageInput { get; set; }
    }
}
