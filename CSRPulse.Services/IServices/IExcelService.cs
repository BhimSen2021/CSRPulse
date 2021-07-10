using CSRPulse.ExportImport.Model;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Text;

namespace CSRPulse.Services.IServices
{
    public interface IExcelService
    {
        DataTable ReadExcel(string sFileFullPath, bool IsHeader, int iSheetIndex = 0, string sSheetName = "");
        DataTable ReadExcelWithNoHeader(string sFileFullPath, int iSheetIndex = 0, string sSheetName = "");
        StringBuilder ReadExcelInSB(string sFileFullPath, int iSheetIndex = 0, string sSheetName = "");

        List<ExpandoObject> ReadExcelDynamic(string sFileFullPath, int iSheetIndex = 0, string sSheetName = "");

        DataTable GetDataWithHeader(DataTable dt);
        List<ExcelSheetList> GetWorksheetName(string fileFullPath);
        DataTable ReadAndValidateHeader(string filePath, List<string> headerList, bool isHeader, out bool isValid, out List<string> lstMissingHeader);
    }
}
