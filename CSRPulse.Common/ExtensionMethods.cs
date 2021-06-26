using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace CSRPulse.Common
{
    public static class ExtensionMethods
    {
        public static string BoolToYesNo(bool bValue)
        {
            if (bValue)
                return "Yes";
            else
                return "No";
        }
        public static void DeleteFile(string sFileFullPath)
        {
            if (System.IO.File.Exists(sFileFullPath))
            {
                System.IO.File.Delete(sFileFullPath);
            }
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, (dr[column.ColumnName].ToString().Trim().Equals(string.Empty)) ? string.Empty : dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = (prop.GetValue(item) == null || prop.GetValue(item).ToString().Trim().Equals(string.Empty)) ? DBNull.Value : prop.GetValue(item);
                table.Rows.Add(row);
            }
            return table;
        }

        public static string GetFinancialYear(this DateTime date)
        {
            int CurrentYear = date.Year;
            int PreviousYear = (date.Year - 1);
            int NextYear = (date.Year + 1);
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = string.Empty;
            if (date.Month > 3)
            {
                FinYear = CurYear + " - " + NexYear;
            }
            else
            {
                FinYear = PreYear + " - " + CurYear;
            }
            return FinYear.Trim();
        }

        public static DataTable PrepairExportTable(DataTable dataTable, List<string> takeColumns)
        {
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                if (!takeColumns.Contains(dataTable.Columns[i].ColumnName))
                {
                    dataTable.Columns.Remove(dataTable.Columns[i].ColumnName);
                    dataTable.AcceptChanges();
                    i--;
                }
            }
            return dataTable;
        }
    }
}
