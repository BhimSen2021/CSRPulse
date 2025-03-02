﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
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

        public static List<T> GetDistinctCellValues<T>(DataTable dtSource, string sourceColName)
        {
            try
            {
                return (from row in dtSource.AsEnumerable()
                        where (!CheckNullOrEmpty<T>(row.Field<T>(sourceColName)))
                        select row.Field<T>(sourceColName)).Distinct().ToList<T>();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static bool CheckNullOrEmpty<T>(T value)
        {
            if (typeof(T) == typeof(string))
                return (string.IsNullOrEmpty(value as string) || ((value as string).Trim().Length == 0));

            return value == null || value.Equals(default(T));
        }

        public static string SetUniqueFileName(this string fileName, string fileExtension)
        {
            string renamedFileName = fileName + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
            return renamedFileName + fileExtension;
        }

        public static string SetUniqueFileName(this string fileExtension)
        {
            string renamedFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
            return renamedFileName + fileExtension;
        }

        public static List<U> FindDuplicates<T, U>(this List<T> list, Func<T, U> keySelector)
        {
            return list.GroupBy(keySelector)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key).ToList();
        }



        public static DataTable LinqQueryToDataTable(this IEnumerable<dynamic> v)
        {
            //We really want to know if there is any data at all
            var firstRecord = v.FirstOrDefault();
            if (firstRecord == null)
                return null;

            /*Okay, we have some data. Time to work.*/

            //So dear record, what do you have?
            PropertyInfo[] infos = firstRecord.GetType().GetProperties();

            //Our table should have the columns to support the properties
            DataTable table = new DataTable();

            //Add, add, add the columns
            foreach (var info in infos)
            {

                Type propType = info.PropertyType;

                if (propType.IsGenericType
                    && propType.GetGenericTypeDefinition() == typeof(Nullable<>)) //Nullable types should be handled too
                {
                    table.Columns.Add(info.Name, Nullable.GetUnderlyingType(propType));
                }
                else
                {
                    table.Columns.Add(info.Name, info.PropertyType);
                }
            }

            //Hmm... we are done with the columns. Let's begin with rows now.
            DataRow row;

            foreach (var record in v)
            {
                row = table.NewRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row[i] = infos[i].GetValue(record) != null ? infos[i].GetValue(record) : DBNull.Value;
                }

                table.Rows.Add(row);
            }

            //Table is ready to serve.
            table.AcceptChanges();

            return table;
        }

        public static string AddSpacesToSentence(string str)
        {
            if (!string.IsNullOrEmpty(str))
                return string.Concat(str.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
            else
                return str;
        }

        public static string StripHTML(string str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, @"<(?!br[\x20/>])(?!p[\x20/>])[^<>]+>", string.Empty).Replace("<p>", " ").Replace("<br>", " ");
        }

        public static string GetUploadDocumentType(string str)
        {
            if (str.Contains(','))
            {
                List<int> typeIds = str.Split(',').Select(s => int.Parse(s)).ToList();

                var docType = string.Empty;
                foreach (var item in typeIds)
                {
                    switch (item)
                    {
                        case (int)ProcessDocumentType.Word:
                            docType = docType + "docx" + ',' + "doc" + ',';
                            break;
                        case (int)ProcessDocumentType.Excel:
                            docType = docType + "xlsx" + ',' + "xls" + ',';
                            break;
                        case (int)ProcessDocumentType.PDF:
                            docType = docType + "pdf" + ',';
                            break;
                        case (int)ProcessDocumentType.Text:
                            docType = docType + "txt" + ',';
                            break;
                        case (int)ProcessDocumentType.Image:
                            docType = docType + "png" + ',' + "jpg" + ',' + "jpeg" + ',' + "gif" + ',' + "bmp" + ',';
                            break;
                        case (int)ProcessDocumentType.Vedio_MP4:
                            docType = docType + "mp4" + ',';
                            break;
                        default:
                            break;
                    }
                }
                docType = docType.TrimEnd(',');

                return docType;
            }
            return str;
        }


        public static string GetUploadDocumentTypeIds(string str)
        {
            if (str.Contains(','))
            {
                List<string> typeIds = str.Split(',').Select(s => s.ToLower()).ToList();

                var docType = string.Empty;
                foreach (var item in typeIds)
                {
                    switch (item)
                    {
                        case "doc":
                        case "docx":
                            docType = docType + "1" + ',';
                            break;
                        case "xlsx":
                        case "xls":
                            docType = docType + "2" + ',';
                            break;
                        case "pdf":
                            docType = docType + "3" + ',';
                            break;
                        case "png":
                        case "jpg":
                        case "jpeg":
                        case "gif":
                        case "bmp":
                            docType = docType + "4" + ',';
                            break;
                        case "txt":
                            docType = docType + "5" + ',';
                            break;
                        case "mp4":
                            docType = docType + "6" + ',';
                            break;
                        default:
                            break;
                    }
                }
                docType = docType.TrimEnd(',');

                return docType;
            }
            return str;
        }
        public static string GenerateOTP()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            characters += alphabets + numbers + small_alphabets;

            int length = 6;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        public static string GeneratePassword(int passwordLength)
        {
            string _allowedChars = "abcdefghijklmnopqrstuvwxyz@#$&ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random randNum = new Random();
            char[] chars = new char[passwordLength];
            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public static string ReportStatus(int status, DateTime startDate)
        {
            switch (status)
            {
                case 1:
                    {
                        if (startDate.Date > DateTime.UtcNow.Date)
                            return "Not Due";
                        else
                            return "Pending";
                    }
                case 2:
                    return "Submitted";

                default:
                    return "";
            }
        }

        public static string MakeDisplayEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return emailAddress;
            else
                return emailAddress.Replace(".", "[dot]").Replace("@", "[at]");
        }


    }
}
