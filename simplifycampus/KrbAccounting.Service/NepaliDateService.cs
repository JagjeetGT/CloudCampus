using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using KRBAccounting.Data;


namespace KRBAccounting.Service
{
    public static class NepaliDateService
    {
        public static DataContext _dataContext = new DataContext();

        public static NepaliDate NepaliDate(DateTime MyDate)
        {
            DateTime StartDate;
            //var date = MyDate.ToString("dd/MM/yyyy");
            //MyDate = Convert.ToDateTime(date);
            int _NepaliMonth; int _NepaliDay; int _NepaliYear;

            SqlConnection con;
            string sql;
            DataTable dtCm = new DataTable();
            DataTable dtCy = new DataTable();

            int year = Convert.ToInt32(MyDate.Year);

            ArrayList Years = new ArrayList();
            ArrayList Months = new ArrayList();
            int temp = 0, nY = 0;
            int NMonth, NDate, TotalDay, TotalDayNep = 0;
            con = ConnectionString.GetConnectionString();
            sql = "Select * from CalendarMonthInfo";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dtCm);

            for (int i = 0; i < dtCm.Rows.Count; i++)
            {
                Months.Add(Convert.ToInt32(dtCm.Rows[i]["nNoOfDays"].ToString()));
            }

            sql = "Select * from CalendarYearInfo";
            SqlDataAdapter da1 = new SqlDataAdapter(sql, con);
            da1.Fill(dtCy);
            nY = 2000 + nY - 1943;
            for (int i = 0; i < dtCy.Rows.Count; i++)
            {
                Years.Add(dtCy.Rows[i]["YearStart"].ToString().ToDate());
            }
            nY = 2000 + year - 1943;
            if (MyDate < Years[year - 1943].ToString().ToDate())
            {
                nY = nY - 1;
                StartDate = Years[year - 1943 - 1].ToString().ToDate();
            }
            else
            {
                StartDate = Years[year - 1943].ToString().ToDate();
            }
            NDate = 1;
            NMonth = 1;
            TimeSpan diff = MyDate.Subtract(StartDate);
            TotalDay = diff.Days + 1;
            for (int i = 1; i <= 12; i++)
            {
                if (TotalDay <= TotalDayNep)
                {
                    break;
                }
                else
                {
                    TotalDayNep = TotalDayNep + Months[((nY - 2000) * 12) + (i - 1)].ToString().ToInt32();
                    temp = i;
                }
            }
            TotalDayNep = TotalDay - (TotalDayNep - Months[(((nY - 2000) * 12) + (temp - 1))].ToString().ToInt32());
            _NepaliMonth = temp;
            _NepaliYear = nY;
            _NepaliDay = TotalDayNep;

            //ToString().PadLeft(6, '0')
            var nepDate = string.Format("{0}/{1}/{2}", _NepaliYear, _NepaliMonth.ToString().PadLeft(2, '0'), _NepaliDay.ToString().PadLeft(2, '0'));
            return NepaliDateFormat(nepDate);
        }

        public static string NepaliLongDate(DateTime MyDate)
        {
            SqlConnection con;
            string sql;

            ArrayList Months = new ArrayList();
            ArrayList Years = new ArrayList();

            DateTime StartDate;

            DataTable dtCm = new DataTable();
            DataTable dtCy = new DataTable();

            int year = Convert.ToInt32(MyDate.Year);

            int temp = 0, nY = 0;
            int NMonth, NDate, TotalDay, TotalDayNep = 0;
            con = ConnectionString.GetConnectionString();
            sql = "Select * from CalendarMonthInfo";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dtCm);

            for (int i = 0; i < dtCm.Rows.Count; i++)
            {
                Months.Add(dtCm.Rows[i]["nNoOfDays"].ToString().ToInt32());
            }

            sql = "Select * from CalendarYearInfo";
            SqlDataAdapter da1 = new SqlDataAdapter(sql, con);
            da1.Fill(dtCy);
            nY = 2000 + nY - 1943;
            for (int i = 0; i < dtCy.Rows.Count; i++)
            {
                Years.Add(dtCy.Rows[i]["YearStart"].ToString().ToInt32());

            }
            nY = 2000 + year - 1943;
            if (MyDate < Years[year - 1943].ToString().ToDate())
            {
                nY = nY - 1;
                StartDate = Years[year - 1943 - 1].ToString().ToDate();
            }
            else
            {
                StartDate = Years[year - 1943].ToString().ToDate();
            }

            NDate = 1;
            NMonth = 1;
            TimeSpan diff = MyDate.Subtract(StartDate);
            TotalDay = diff.Days + 1;
            for (int i = 1; i <= 12; i++)
            {
                if (TotalDay <= TotalDayNep)
                { break; }
                else
                {
                    TotalDayNep = TotalDayNep + Months[((nY - 2000) * 12) + (i - 1)].ToString().ToInt32();
                    temp = i;
                }
            }
            TotalDayNep = TotalDay - (TotalDayNep - Months[(((nY - 2000) * 12) + (temp - 1))].ToString().ToInt32());
            NDate = TotalDayNep;
            NMonth = temp;
            string dtstring = nY + " " + LongMonth(NMonth) + " " + NDate;

            return dtstring;
        }

        public static string LongMonth(int m)
        {
            if (m == 1) return "Baisakh";
            else if (m == 2) return "Jestha";
            else if (m == 3) return "Ashadh";
            else if (m == 4) return "Shrawan";
            else if (m == 5) return "Bhadra";
            else if (m == 6) return "Ashwin";
            else if (m == 7) return "Kartik";
            else if (m == 8) return "Mangsir";
            else if (m == 9) return "Poush";
            else if (m == 10) return "Magh";
            else if (m == 11) return "Falgun";
            else if (m == 12) return "Chaitra";
            else return "Error";
        }

        public static DateTime NepalitoEnglishDate(string MyDate)
        {
            var Years = new ArrayList();
            var Months = new ArrayList();
            var sep = new char[2];
            sep[0] = '/';
            sep[1] = '-';
            string[] NepDate = MyDate.Split(sep);
            int year = NepDate[0].ToString().ToInt32();
            int month = NepDate[1].ToString().ToInt32();
            int day = NepDate[2].ToString().ToInt32();
            int TotalDay = 0;
            var dtCm = new DataTable();
            var dtCy = new DataTable();
            SqlConnection con = ConnectionString.GetConnectionString();
            string sql = "Select * from CalendarMonthInfo";
            var da = new SqlDataAdapter(sql, con);
            da.Fill(dtCm);
            for (int i = 0; i < dtCm.Rows.Count; i++)
            {
                Months.Add(dtCm.Rows[i]["nNoOfDays"].ToString().ToInt32());
            }
            sql = "Select * from CalendarYearInfo";
            var da1 = new SqlDataAdapter(sql, con);
            da1.Fill(dtCy);
            for (int i = 0; i < dtCy.Rows.Count; i++)
            {
                Years.Add(dtCy.Rows[i]["YearStart"].ToString().ToDate());

            }
            DateTime StartDate = Years[year - 2000].ToString().ToDate();
            for (int i = 1; i <= month - 1; i++)
            {
                TotalDay = TotalDay + Months[(((year - 2000) * 12) + (i - 1))].ToString().ToInt32();
            }
            TotalDay = TotalDay + day;
            StartDate = StartDate.AddDays(TotalDay - 1);
            return StartDate;
        }

        public static DateTime NepalitoEnglishDate(int year, int month, int day)
        {
            SqlConnection con;
            string sql;
            ArrayList Years = new ArrayList();
            ArrayList Months = new ArrayList();
            int TotalDay = 0;
            DataTable dtCm = new DataTable();
            DataTable dtCy = new DataTable(); ;
            con = ConnectionString.GetConnectionString();
            sql = "Select * from CalendarMonthInfo";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dtCm);
            for (int i = 0; i < dtCm.Rows.Count; i++)
            {
                Months.Add(dtCm.Rows[i]["nNoOfDays"].ToString().ToInt32());
            }
            sql = "Select * from CalendarYearInfo";
            SqlDataAdapter da1 = new SqlDataAdapter(sql, con);
            da1.Fill(dtCy);
            for (int i = 0; i < dtCy.Rows.Count; i++)
            {
                Years.Add(dtCy.Rows[i]["YearStart"].ToString().ToDate());

            }
            DateTime EnglishDate = Years[year - 2000].ToString().ToDate();
            for (int i = 1; i <= month - 1; i++)
            {
                TotalDay = TotalDay + Months[(((year - 2000) * 12) + (i - 1))].ToString().ToInt32();
            }
            TotalDay = TotalDay + day;
            EnglishDate = EnglishDate.AddDays(TotalDay - 1);
            return EnglishDate;
        }


        public static NepaliDate NepaliAddMonth(NepaliDate MyDate, int addMonth)
        {
            var month = MyDate.Month;
            var year = MyDate.Year;
            month += addMonth;
            CheckMonth(ref month, ref year);
            var newDate = NepaliDateFormat(year, month, MyDate.Day);
            return newDate;
        }

        public static NepaliDate NepaliDateFormat(int year, int month, int day)
        {
            var nepaliDate = new NepaliDate();
            nepaliDate.Year = year;
            nepaliDate.Month = month;
            nepaliDate.Day = day;
            return nepaliDate;
        }

        public static NepaliDate NepaliDateFormat(string date)
        {
            char[] sep = new char[2];
            sep[0] = '/';
            sep[1] = '-';
            string[] NepDate = date.Split(sep);
            var nepaliDate = NepaliDateFormat(NepDate[0].ToString().ToInt32(),
                                              NepDate[1].ToString().ToInt32(),
                                              NepDate[2].ToString().ToInt32());
            return nepaliDate;
        }

        public static void CheckMonth(ref int month, ref int year)
        {
            if (month > 12)
            {
                month = month - 12;
                year = year + 1;
                CheckMonth(ref month, ref year);
            }
        }
    }

    public class NepaliDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Date
        {
            get { return string.Format("{0}/{1}/{2}", Year.ToString().PadLeft(2, '0'), Month.ToString().PadLeft(2, '0'), Day.ToString().PadLeft(2, '0')); }
        }
    }
}
