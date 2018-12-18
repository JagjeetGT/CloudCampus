using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.ViewModels.Install;

namespace KRBAccounting.Web.Helpers
{
    public class InstallHelper
    {
        public static string AddDataBase(string connString, string prefix)
        {
            SqlConnection _sqlConnection = new SqlConnection();
            SqlConnection _sqlConnection1 = new SqlConnection();

            _sqlConnection.ConnectionString = connString;
            _sqlConnection.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = _sqlConnection;

            //cmd.CommandText = "select Count([Name]) as TotalDb from sysdatabases Where name Like 'Academy" + prefix +
            //                  "%'";
            //SqlDataReader reader = cmd.ExecuteReader();
            //int count;
            //count = 1;
            //if (reader.HasRows)
            //{
            //    reader.Read();
            //    count = Convert.ToInt32(reader[0].ToString());
            //    count++;
            //}

            ////  Create Database
            //_sqlConnection1.ConnectionString = connString;
            //_sqlConnection1.Open();

            string DatabaseName = CheckDbExistance("Academy", prefix, _sqlConnection);

            SqlCommand cmd1 = new SqlCommand();
            _sqlConnection1.ConnectionString = connString;
            _sqlConnection1.Open();
            cmd1.Connection = _sqlConnection1;
            cmd1.CommandText = "create database " + DatabaseName;
            SqlDataReader reader1 = cmd1.ExecuteReader();
            reader1.Close();
            //reader.Close();
            _sqlConnection.Close();
            _sqlConnection1.Close();
            return DatabaseName;
        }

        private static string CheckDbExistance(string database, string prefix, SqlConnection sqlConnection, int Index = 1)
        {



            bool dbExists = true;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            string dbName = database + prefix + Index.ToString();

            while (dbExists == true)
            {
                cmd.CommandText = "select Count([Name]) as TotalDb from sysdatabases Where name Like '" + dbName +
                                  "%'";
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (Convert.ToInt32(reader[0].ToString()) != 0)
                {
                    Index++;
                    dbName = database + prefix + Index.ToString();
                    reader.Close();
                    dbExists = true;

                }
                else
                {
                    dbName = database + prefix + Index.ToString();
                    dbExists = false;
                    break;
                }
            }

            return dbName;
        }

        public static void AddCompanyInfo(ref string str, InstallerViewModel model)
        {
            str +=
                    "INSERT [dbo].[CompanyInfo] (" +

                    "[Initial], " +
                    "[Name]," +
                    " [Country], " +
                    "[State], " +
                    "[City], " +
                    "[Address], " +
                    "[PhoneNo], " +
                    "[Fax], " +
                    "[Email], " +
                    "[PanNo], " +
                    "[TaxInvoice], " +
                    "[StartDate], " +
                    "[EndDate], " +
                    "[CreatedDate]," +
                    " [UpdatedDate]," +
                    " [ParentId]," +
                    " [StartMiti], " +
                    "[EndMiti], " +
                    "[LogoGuid]," +
                    " [LogoExt]) " +
                    "VALUES (" +

                    "'" + model.CompanyInitial + "', " +//Initial
                    "'" + model.CompanyName + "', " +//Name
                    "'" + model.CompanyCountry + "', " +//Country
                    "'" + model.CompanyState + "', " +//State
                    "'" + model.CompanyCity + "', " +//City
                    "'" + model.CompanyAddress + "'," +//Address
                    "'" + model.CompanyPhoneNo + "', " +//PhoneNo
                    "'" + model.CompanyFax + "', " +//Fax
                    "'" + model.CompanyEmail + "'," +//Email
                    " '" + model.CompanyPanNo + "', " +//PanNo
                    "'" + model.CompanyTaxInvoice + "', " +//TaxInvoice
                //model.StartDate.Date.ToString("yyyy-MM-dd");
                    "'" + model.CompanyStartDate.ToString("yyyy-MM-dd") + "', " +//StartDate
                    "'" + model.CompanyEndDate.ToString("yyyy-MM-dd") + "', " +//EndDate
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +//CreatedDate
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +//UpdatedDate
                    "0, " +//ParentId
                    "'" + model.CompanyStartMiti + "', " +//StartMiti
                    "'" + model.CompanyEndMiti + "', " +//EndMiti
                    "'', " +//LogoGUId
                    "'')";//logoExt
        }

        public static void AddUserInfo(ref string str, AdminInfo model)
        {
            str +=
                    "INSERT [User] ( " +
                    "[Username], " +
                    "[FullName], " +
                    "[EmailAddress], " +
                    "[Password], " +
                    "[CreatedDate]," +
                    " [UpdatedDate]," +
                    " [LastLoginDate], " +
                    "[LastLoginIp]," +
                    " [IsActive]," +
                    " [IsDeleted], " +
                    "[CompanyId], " +
                    "[BranchId], " +
                    "[AllBranch], " +
                    "[LastAccessedBranch], " +
                    "[ImageGuid]," +
                    " [Ext]) VALUES (" +


                    " '" + model.AdminUserName + "'," +//UserName
                    "'" + model.AdminUserName + "'," +//FullName
                    "'admin@admin.com', " +//EmailAddress
                    "'" + PasswordHelper.HashPassword(model.AdminUserPassword) + "', " +//Password
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +//CreatedDate
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +//UpdatedDate
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +//LastLoginDate
                    "N'', " +//LastLoginIp
                    "1, " +//IsActive
                    "0," +//IsDeleted
                    " 1, " +//CompanyId
                    "1, " +//BranchId
                    "1, " +//AllBranch
                    "1," +//LastAccessedBranch
                    " '', " +//ImageGuid
                    "'')";//Ext

        }

        public static void AddRole(ref string str)
        {
            str += "INSERT [dbo].[Role] ([RoleName]) VALUES (N'admin')";
        }

        public static void AddRoleMapping(ref string str)
        {
            str += "INSERT [dbo].[RoleMapping] ([UserId], [Id]) VALUES (1, 1)";
        }

        public static void AddSystemControl(ref string str)
        {
            str += "INSERT [dbo].[SystemControl] ([CompanyId], [DateType], [AuditTrial], [UDF], [CurrCode], [CurrDesc], [CurrUnit], [IsAutoPopup], [IsCurrDate], [IsConfirmSaving], [IsConfirmCancel], [ProfitLossAc], [CashBook], [SalesAc], [SalesReturnAc], [Vat], [PurchaseAc], [PurchaseReturnAc], [OpeningStockPl], [ClosingStockPl], [ClosingingStock], [PageSize], [EnableBranch], [StudentFeeAc], [EducationTaxAc], [DepositAc], [NoOfFeeReceiptPrint], [PrintDataOnly], [NoOfBillPrint],[LibraryLateFine],[ExpiredProduct]) VALUES (1, 1, N'1', N'1', N'Rs.', N'NRS', N'Paisa', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 0, 1, 1, 1, 2, 0, 2,0,0)";
            //INSERT [dbo].[SystemControl] ([CompanyId], [DateType], [AuditTrial], [UDF], [CurrCode], [CurrDesc], [CurrUnit], [IsAutoPopup], [IsCurrDate], [IsConfirmSaving], [IsConfirmCancel], [ProfitLossAc], [CashBook], [SalesAc], [SalesReturnAc], [Vat], [PurchaseAc], [PurchaseReturnAc], [OpeningStockPl], [ClosingStockPl], [ClosingingStock], [PageSize], [EnableBranch], [StudentFeeAc], [EducationTaxAc], [DepositAc], [NoOfFeeReceiptPrint], [PrintDataOnly], [NoOfBillPrint]) VALUES (13, 1, N'1', N'1', N'Rs.', N'NRS', N'Paisa', 1, 1, 1, 1, 1, 2, 3, 6, 15, 4, 5, 12, 10, 9, 11, 0, 8, 32, 0, 2, 0, 2)
        }

        public static void AddFiscalYear(ref string str, string startDateNep, string endDateNep, DateTime startDate, DateTime endDate)
        {
            str +=
                    "INSERT [dbo].[FiscalYear] (" +
                    "[StartDateNep], " +
                    "[StartDate], " +
                    "[EndDateNep], " +
                    "[EndDate]," +
                    " [CompanyId], " +
                    "[IsDefalut]," +
                    " [CreatedById], " +
                    "[UpdatedById]) " +

                    "VALUES (" +

                   "'" + startDateNep + "', " +//StartDateNep
                     "'" + startDate.ToString("yyyy-MM-dd") + "', " +//StartDate
                    "'" + endDateNep + "', " +//EndDateNep
                     "'" + endDate.ToString("yyyy-MM-dd") + "', " +//EndDate
                    "1," +//CompanyId
                    "1, " +//IsDefault
                    "1, " +//CreatedById
                    "1)";//UpdatedById
        }

        public static void AddAcademicYear(ref string str, string startAcademicDateNep, string endAcademicDateNep, DateTime startAcademicDate, DateTime endAcademicDate)
        {
            str +=
                "INSERT [dbo].[AcademicYear] (" +
                "[StartDateNep], " +
                "[StartDate], " +
                "[EndDateNep], " +
                "[EndDate]," +
                " [CompanyId], " +
                "[IsDefalut]," +
                " [CreatedById], " +
                "[ModifiedById]," +
                "[FiscalYearId])" +

                "VALUES (" +

                "'" + startAcademicDateNep + "', " + //StartDateNep
                "'" + startAcademicDate.ToString("yyyy-MM-dd") + "', " + //StartDate
                "'" + endAcademicDateNep + "', " + //EndDateNep
                "'" + endAcademicDate.ToString("yyyy-MM-dd") + "', " + //EndDate
                "1," + //CompanyId
                "1, " + //IsDefault
                "1, " + //CreatedById
                "1," + //UpdatedById
                "1)";//FiscalYearId
        }
    }
}