
using NPOI.XSSF.Streaming;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.ExportImport
{
    /// <summary>
    ///  All common method of excel exports
    /// </summary>
    abstract public class BaseExcel : Base
    {
        public static XSSFWorkbook CreateWookBook()
        {
            XSSFWorkbook wb = new XSSFWorkbook();
            return wb;
        }
        public static XSSFSheet CreateSheet(string sSheetName, XSSFWorkbook wb)
        {
            return (XSSFSheet)wb.CreateSheet(sSheetName);
        }
        public static XSSFRow CreateRow(XSSFSheet sheet, int iRowNumber)
        {
            return (XSSFRow)sheet.CreateRow(iRowNumber);
        }
        public static SXSSFRow CreateRow(SXSSFSheet sheet, int iRowNumber)
        {
            return (SXSSFRow)sheet.CreateRow(iRowNumber);
        }

        public static XSSFFont CreateFont(XSSFWorkbook wb)
        {
            return (XSSFFont)wb.CreateFont();
        }
    }
}
