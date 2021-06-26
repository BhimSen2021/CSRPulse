using CSRPulse.ExportImport.Interfaces;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace CSRPulse.ExportImport
{
    public class Export : BaseExcel, IExport
    {

        [Obsolete]
        public bool ExportToExcel(DataSet dsSource, string sFullPath, string fileName)
        {
            try
            {
                XSSFWorkbook workbook = CreateWookBook();
                using (var file = new FileStream(sFullPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    foreach (DataTable table in dsSource.Tables)
                    {
                        XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet(fileName);
                        XSSFRow dataRow;
                        XSSFRow headerRow = CreateRow(sheet, 0);
                        XSSFFont hFont = CreateFont(workbook);
                        hFont.FontHeightInPoints = 11;
                        hFont.FontName = "Calibri";
                        hFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                        foreach (DataColumn column in table.Columns)
                        {
                            int rowIndex = 1;
                            headerRow.Height = 500;
                            XSSFCell cell = (XSSFCell)headerRow.CreateCell(column.Ordinal);
                            cell.SetCellValue(column.ColumnName);
                            cell.CellStyle = workbook.CreateCellStyle();                           
                            cell.CellStyle.Alignment = HorizontalAlignment.Center;
                            cell.CellStyle.VerticalAlignment = VerticalAlignment.Center;
                            cell.CellStyle.SetFont(hFont);
                            foreach (DataRow row in table.Rows)
                            {
                                if (column.Ordinal == 0)
                                    dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                                else
                                    dataRow = (XSSFRow)sheet.GetRow(rowIndex);

                                if (!row[column].ToString().Trim().Equals(""))
                                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                                rowIndex++;
                            }
                            sheet.AutoSizeColumn(column.Ordinal);
                        }
                    }
                    workbook.Write(file);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
