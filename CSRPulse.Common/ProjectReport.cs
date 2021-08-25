using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CSRPulse.Common
{
    public static class ProjectReport
    {
        public static DataTable MakeProjectReport(int projectId, DateTime startDate, DateTime endDate, int reportType)
        {
            try
            {
                if (reportType == (int)ReportType.Quarterly)
                    return MakeQuarters(projectId, startDate, endDate);

                else if (reportType == (int)ReportType.Monthly)
                    return MakeMonths(projectId, startDate, endDate);

                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Q u a r t e r l y
        private static DataTable MakeQuarters(int projectId, DateTime startDate, DateTime endDate)
        {
            int NoOfQuarters = CountQuarters(startDate, endDate);
            DataTable dt = new DataTable();
            dt.Columns.Add("ProjectId");
            dt.Columns.Add("ReportNo");
            dt.Columns.Add("DueDate");
            dt.Columns.Add("ReportName");
            dt.Columns.Add("ProjectYear");
            dt.Columns.Add("StartDate");
            dt.Columns.Add("EndDate");
            dt.Columns.Add("Status");

            DataRow dataRow;
            DateTime dtstart = new DateTime();
            DateTime dtend = new DateTime();
            for (int i = 1; i <= NoOfQuarters; i++)
            {
                dataRow = dt.NewRow();
                dataRow["ProjectId"] = projectId;
                dataRow["ReportNo"] = i;
                dataRow["Status"] = 1;

                if (i == 1)
                {
                    dtend = CheckFirstQuarterEndDate(startDate);
                    dataRow["StartDate"] = startDate.ToString("dd/MM/yyyy");
                    int Endmo = dtend.Month;
                    int EndY = dtend.Year;
                    if (Endmo > 12)
                    {
                        Endmo = Endmo % 12; EndY = EndY + 1;
                    }
                    dataRow["DueDate"] = dtend.AddDays(7).ToString("dd/MM/yyyy");
                    dataRow["EndDate"] = dtend.ToString("dd/MM/yyyy");
                }
                else
                {
                    if (i != NoOfQuarters)
                    {
                        dtstart = dtend.AddDays(1);
                        dtend = dtstart.AddMonths(3).AddDays(-1);
                        dataRow["StartDate"] = dtstart.ToString("dd/MM/yyyy");
                        dataRow["EndDate"] = dtend.ToString("dd/MM/yyyy");
                        dataRow["DueDate"] = dtend.AddDays(7).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        dtstart = dtend.AddDays(1);
                        dtend = dtstart.AddMonths(3).AddDays(-1);
                        dataRow["StartDate"] = dtstart.ToString("dd/MM/yyyy");
                        dataRow["EndDate"] = endDate.ToString("dd/MM/yyyy");
                        dataRow["DueDate"] = endDate.AddDays(7).ToString("dd/MM/yyyy");
                    }
                }
                dt.Rows.Add(dataRow);
            }
            dt.AcceptChanges();

            int year = 0, qtrNo = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 4 == 0)
                {
                    year = year + 1;
                    qtrNo = 1;
                }
                else
                    qtrNo = qtrNo + 1;
                dt.Rows[i]["ReportName"] = "Year" + year.ToString() + "/Q" + qtrNo.ToString();
                dt.Rows[i]["ProjectYear"] = "Year " + year.ToString();
            }
            return dt;
        }       
        public static int CountQuarters(DateTime startDate, DateTime endDate)
        {
            startDate = CheckFirstQuarterEndDate(startDate).AddMonths(-3).AddDays(1);
            endDate = CheckLastQuarterEndDate(endDate);
            int MonthsforQuarter = CalMonths(startDate, endDate);
            int NoOfQuarters = MonthsforQuarter / 3;
            if (MonthsforQuarter % 3 != 0)
                NoOfQuarters = NoOfQuarters + 1;

            return NoOfQuarters;
        }
        public static DateTime CheckFirstQuarterEndDate(DateTime startDate)
        {
            DateTime Q1EndDate;
            int MonthNo = startDate.Month;
            int FinQtrNo = 0;
            if (MonthNo >= 1 && MonthNo <= 3)
                FinQtrNo = 4;
            else if (MonthNo >= 4 && MonthNo <= 6)
                FinQtrNo = 1;
            else if (MonthNo >= 7 && MonthNo <= 9)
                FinQtrNo = 2;
            else if (MonthNo >= 10 && MonthNo <= 12)
                FinQtrNo = 3;
            int qno = FinQtrNo;
            string q1end = "";
            if (qno == 1)
                q1end = "30/06/" + startDate.Year.ToString();
            else if (qno == 2)
                q1end = "30/09/" + startDate.Year.ToString();
            else if (qno == 3)
                q1end = "31/12/" + startDate.Year.ToString();
            else if (qno == 4)
            {
                if ((startDate.AddMonths(3)).Year - startDate.Year == 0)
                    q1end = "31/03/" + (startDate.Year).ToString();
                else
                    q1end = "31/03/" + (startDate.Year + 1).ToString();
            }

            Q1EndDate = Convert.ToDateTime(q1end);


            int Numberofdays = Convert.ToInt32((Q1EndDate - startDate).TotalDays) + 1;
            if (Numberofdays > 45) { }
            else
            {
                startDate = startDate.AddMonths(3);
                MonthNo = startDate.Month;
                if (MonthNo >= 1 && MonthNo <= 3)
                    FinQtrNo = 4;
                else if (MonthNo >= 4 && MonthNo <= 6)
                    FinQtrNo = 1;
                else if (MonthNo >= 7 && MonthNo <= 9)
                    FinQtrNo = 2;
                else if (MonthNo >= 10 && MonthNo <= 12)
                    FinQtrNo = 3;
                qno = FinQtrNo;
                if (qno == 1)
                    q1end = "30/06/" + startDate.Year.ToString();
                else if (qno == 2)
                    q1end = "30/09/" + startDate.Year.ToString();
                else if (qno == 3)
                    q1end = "31/12/" + startDate.Year.ToString();
                else if (qno == 4)
                    q1end = "31/03/" + (startDate.Year).ToString();

                Q1EndDate = Convert.ToDateTime(q1end);
            }
            return Q1EndDate;
        }
        public static DateTime CheckLastQuarterEndDate(DateTime endDate)
        {
            string qnstart = "";
            int mnth = endDate.Month;
            if (mnth >= 1 && mnth <= 3)
            {
                qnstart = "01/01/" + endDate.Year.ToString();
            }
            else if (mnth >= 4 && mnth <= 6)
            {
                qnstart = "01/04/" + endDate.Year.ToString();
            }
            else if (mnth >= 7 && mnth <= 9)
            {
                qnstart = "01/07/" + endDate.Year.ToString();
            }
            else if (mnth >= 10 && mnth <= 12)
            {
                qnstart = "01/10/" + endDate.Year.ToString();
            }
            DateTime QnStartDate = Convert.ToDateTime(qnstart);
            string qnend = "";
            DateTime QnEndDate;
            int Numberofdays = Convert.ToInt32((endDate - QnStartDate).TotalDays) + 1;
            if (Numberofdays > 45)
            {
                if (mnth >= 1 && mnth <= 3)
                {
                    qnend = "31/03/" + endDate.Year.ToString();
                }
                else if (mnth >= 4 && mnth <= 6)
                {
                    qnend = "30/06/" + endDate.Year.ToString();
                }
                else if (mnth >= 7 && mnth <= 9)
                {
                    qnend = "30/09/" + (endDate.Year).ToString();
                }
                else if (mnth >= 10 && mnth <= 12)
                {
                    qnstart = "31/12/" + endDate.Year.ToString();
                }
                QnEndDate = Convert.ToDateTime(qnstart);
            }
            else
            {
                QnEndDate = QnStartDate.AddDays(-1);
            }
            return QnEndDate;
        }
      
        #endregion

        #region M o n t h l y
        private static DataTable MakeMonths(int projectId, DateTime startDate, DateTime endDate)
        {
            int PeriodEndOn = 31;
            int DueOnDate = 5;
            DataTable dt = new DataTable();
            dt.Columns.Add("ProjectId");
            dt.Columns.Add("ReportNo");
            dt.Columns.Add("DueDate");
            dt.Columns.Add("ReportName");
            dt.Columns.Add("ProjectYear");
            dt.Columns.Add("StartDate");
            dt.Columns.Add("EndDate");
            dt.Columns.Add("Status");
            DataRow drow;
            int numofmonth = 0;
            numofmonth = CalMonths(startDate, endDate);
            int counter = 1;
            DateTime nextMonthStartDate = DateTime.Now;
            DateTime MonthEndDate = DateTime.Now;
            int Year = 1, Month = 1; string variable = ""; string dayvar30 = "";
            for (int mon = 0; mon < numofmonth; mon++)
            {
                drow = dt.NewRow();
                drow["ProjectId"] = projectId;
                drow["ReportNo"] = counter;
                drow["Status"] = 1;              
                drow["ReportName"] = "Year" + Year.ToString() + "/Month" + Month.ToString();
                drow["ProjectYear"] = "Year " + Year.ToString();
                if (mon == 0)
                {
                    drow["StartDate"] = startDate.ToString("dd/MM/yyyy");
                    int PeriodEnd = 0;
                    if (PeriodEndOn == 31)
                    {
                        if (Convert.ToInt32(DateTime.DaysInMonth(startDate.Year, startDate.Month)) == 30)
                            PeriodEnd = PeriodEndOn - 1;
                        else
                            PeriodEnd = PeriodEndOn;
                    }
                    else
                        PeriodEnd = PeriodEndOn;
                    MonthEndDate = new DateTime(startDate.Year, startDate.Month, PeriodEnd);
                    drow["EndDate"] = (new DateTime(startDate.Year, startDate.Month, PeriodEnd)).ToString("dd/MM/yyyy");
                    drow["DueDate"] = (new DateTime(startDate.Year, startDate.Month, PeriodEnd).AddDays(DueOnDate)).ToString("dd/MM/yyyy");
                    nextMonthStartDate = new DateTime(startDate.Year, startDate.Month, PeriodEnd).AddDays(1);
                    if (nextMonthStartDate.Month == MonthEndDate.Month)
                        dayvar30 = "next";
                    else
                        dayvar30 = "";
                }
                else if (mon > 0)
                {
                    if (mon == numofmonth - 1)
                    {
                        MonthEndDate = endDate;
                        drow["DueDate"] = endDate.AddDays(DueOnDate).ToString("dd/MM/yyyy");
                        drow["StartDate"] = nextMonthStartDate.ToString("dd/MM/yyyy");
                        drow["EndDate"] = endDate.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        drow["StartDate"] = nextMonthStartDate.ToString("dd/MM/yyyy");

                        if (nextMonthStartDate.AddMonths(1).Month == 2 && PeriodEndOn > 27 && PeriodEndOn != 31)
                        {
                            variable = "Feb";
                            MonthEndDate = nextMonthStartDate.AddMonths(1);
                            drow["EndDate"] = (nextMonthStartDate.AddMonths(1)).ToString("dd/MM/yyyy");
                            drow["DueDate"] = nextMonthStartDate.AddMonths(1).AddDays(DueOnDate).ToString("dd/MM/yyyy");
                            nextMonthStartDate = (nextMonthStartDate.AddMonths(1).AddDays(1));
                        }
                        else if (((nextMonthStartDate.AddMonths(-1).Month == 2 && PeriodEndOn > 27) & variable == "Feb") || variable == "Feb")
                        {
                            int PeriodEnd = 0;
                            if (PeriodEndOn == 31)
                            {
                                if (Convert.ToInt32(DateTime.DaysInMonth(nextMonthStartDate.Year, nextMonthStartDate.Month)) == 30)
                                    PeriodEnd = PeriodEndOn - 1;
                                else
                                    PeriodEnd = PeriodEndOn;

                                if (nextMonthStartDate.Month == 2)
                                    PeriodEnd = Convert.ToInt32(DateTime.DaysInMonth(nextMonthStartDate.Year, nextMonthStartDate.Month));

                                MonthEndDate = new DateTime(nextMonthStartDate.Year, nextMonthStartDate.Month, PeriodEnd);
                                drow["EndDate"] = new DateTime(nextMonthStartDate.Year, nextMonthStartDate.Month, PeriodEnd).ToString("dd/MM/yyyy");
                               
                                drow["DueDate"] = new DateTime(nextMonthStartDate.Year, nextMonthStartDate.Month, PeriodEnd).AddDays(DueOnDate).ToString("dd/MM/yyyy");
                                nextMonthStartDate = new DateTime(nextMonthStartDate.Year, nextMonthStartDate.Month, PeriodEnd).AddDays(1);
                            }
                            else
                            {
                                MonthEndDate = new DateTime(nextMonthStartDate.Year, nextMonthStartDate.Month, PeriodEndOn);
                                drow["EndDate"] = new DateTime(nextMonthStartDate.Year, nextMonthStartDate.Month, PeriodEndOn).ToString("dd/MM/yyyy");
                                drow["DueDate"] = new DateTime(nextMonthStartDate.Year, nextMonthStartDate.Month, PeriodEndOn).AddDays(DueOnDate).ToString("dd/MM/yyyy");
                                nextMonthStartDate = new DateTime(nextMonthStartDate.Year, nextMonthStartDate.Month, PeriodEndOn).AddDays(1);
                            }
                            variable = "";
                        }
                        else
                        {
                            int yearname = nextMonthStartDate.Year;
                            if ((nextMonthStartDate.Month) % 12 == 0)
                                yearname = yearname + 1;

                            int PeriodEnd = 0;
                            if (PeriodEndOn == 31)
                            {
                                if (Convert.ToInt32(DateTime.DaysInMonth(nextMonthStartDate.Year, nextMonthStartDate.Month)) == 30)
                                    PeriodEnd = PeriodEndOn - 1;
                                else
                                    PeriodEnd = PeriodEndOn;
                                int chkdaylast;
                                chkdaylast = DateTime.DaysInMonth(yearname, nextMonthStartDate.Month);
                                if (PeriodEnd > chkdaylast)
                                {
                                    PeriodEnd = chkdaylast;
                                }
                                MonthEndDate = new DateTime(yearname, nextMonthStartDate.Month, PeriodEnd);
                                drow["EndDate"] = MonthEndDate.ToString("dd/MM/yyyy");
                                drow["DueDate"] = new DateTime(yearname, nextMonthStartDate.Month, PeriodEnd).AddDays(DueOnDate).ToString("dd/MM/yyyy");
                                nextMonthStartDate = new DateTime(yearname, nextMonthStartDate.Month, PeriodEnd).AddDays(1);
                            }
                            else
                            {
                                if (dayvar30 == "next")
                                {
                                    MonthEndDate = new DateTime(yearname, nextMonthStartDate.AddMonths(1).Month, PeriodEndOn);
                                    drow["EndDate"] = MonthEndDate.ToString("dd/MM/yyyy");
                                    drow["DueDate"] = new DateTime(yearname, nextMonthStartDate.AddMonths(1).Month, PeriodEndOn).AddDays(DueOnDate).ToString("dd/MM/yyyy");
                                    nextMonthStartDate = new DateTime(yearname, nextMonthStartDate.AddMonths(1).Month, PeriodEndOn).AddDays(1);
                                }
                                else
                                {
                                    MonthEndDate = new DateTime(yearname, nextMonthStartDate.Month, PeriodEndOn);
                                    drow["EndDate"] = MonthEndDate.ToString("dd/MM/yyyy");
                                    drow["DueDate"] = new DateTime(yearname, nextMonthStartDate.Month, PeriodEndOn).AddDays(DueOnDate).ToString("dd/MM/yyyy");
                                    nextMonthStartDate = new DateTime(yearname, nextMonthStartDate.Month, PeriodEndOn).AddDays(1);

                                }
                            }

                            if ((nextMonthStartDate.Month != MonthEndDate.Month))
                                variable = "Feb";

                        }
                    }
                    if ((nextMonthStartDate.Month == MonthEndDate.Month))
                        dayvar30 = "next";
                    else
                        dayvar30 = "";
                }
                if (Month > 11)
                {
                    Month = 1;
                    Year = Year + 1;
                }
                else
                    Month = Month + 1;
                counter += 1;
                dt.Rows.Add(drow);
            }
            dt.AcceptChanges();
            return dt;
        }
        #endregion

        private static int CalMonths(DateTime startDate, DateTime endDate)
        {
            int MonthVal = 0;
            try
            {
                int FDay, TDay, FMonth, TMonth, FYear, TYear;
                FDay = startDate.Day;
                FMonth = startDate.Month;
                FYear = startDate.Year;
                TDay = endDate.Day;
                TMonth = endDate.Month;
                TYear = endDate.Year;

                if (FYear == TYear)
                {
                    if (FMonth == TMonth)
                        MonthVal = 1;
                    else if (FMonth < TMonth)
                        MonthVal = FDay <= TDay ? TMonth - FMonth + 1 : TMonth - FMonth;
                }
                else if (TYear > FYear)
                {
                    if (TYear - FYear == 1)
                    {
                        MonthVal = FDay <= TDay ? ((12 - FMonth + 1) + TMonth) : ((12 - FMonth) + TMonth);
                    }
                    else
                    {
                        MonthVal = (12 - FMonth) + TMonth + (12 * (TYear - FYear - 1));
                        MonthVal = FDay <= TDay ? MonthVal + 1 : MonthVal;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MonthVal;
        }
    }
}
