using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Model;
namespace CSRPulse.Models
{
    public class DistrictImportModel
    {
        public List<DistrictImport> DistrictData { set; get; }
        public Dictionary<string, string> ErrorMsgCollection { get; set; }
        public int NoOfErrors { get; set; }
        public string Message { get; set; }
        public DataTable DistrictInput { get; set; }
    }
}
