using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CSRPulse.ExportImport.Interfaces
{
    public interface IExport
    {
        bool ExportToExcel(DataSet dsSource, string sFullPath, string fileName);
    }
}
