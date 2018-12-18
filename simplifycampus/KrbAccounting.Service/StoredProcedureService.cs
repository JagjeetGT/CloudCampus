using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using KRBAccounting.Data;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Service
{
    public static class StoredProcedureService
    {
        public static DataContext _context = new DataContext();

        #region Dashboard

        public static List<SP_CashFlow> GetTotalCashFlow(string StartDate, string EndDate, int FYId)
        {
            var _context = new DataContext();
            var str = "SP_CashFlow @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@FYId=" + FYId;
            var cashFlow = _context.Database.SqlQuery<SP_CashFlow>("SP_CashFlow @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@FYId=" + FYId).ToList();
            return cashFlow;
        }

        #endregion

        #region Sales

        public static DataTable GetSalesSummary(string StartDate, string EndDate, int groupby)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@GroupBy", groupby);
            var summary = ConnectionString.GetDataTable("SP_SalesSummary", param);
            return summary;
        }
        public static DataTable GetSalesReturnSummary(string StartDate, string EndDate, int groupby)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@GroupBy", groupby);
            var summary = ConnectionString.GetDataTable("SP_SalesReturnSummary", param);
            return summary;
        }

        public static List<SP_SalesDetail> GetSalesDetails(string StartDate, string EndDate, int godownId = 0)
        {
            var _context = new DataContext();
            var salesDetails = _context.Database.SqlQuery<SP_SalesDetail>("SP_SalesDetail @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GodownId=" + godownId).ToList();
            return salesDetails;
        }

        public static List<SP_SalesGodownHeader> GetSalesGodownHeaders(string StartDate, string EndDate, string godownIds)
        {
            var _context = new DataContext();
            var salesDetails = _context.Database.SqlQuery<SP_SalesGodownHeader>("SP_SalesGodownHeader @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GodownId='" + godownIds + "'").ToList();
            return salesDetails;
        }

        public static List<SP_SalesDetailProducts> GetSalesDetailProducts(int SalesInvoiceId)
        {
            var _context = new DataContext();
            var salesDetailProducts = _context.Database.SqlQuery<SP_SalesDetailProducts>("SP_SalesDetailProducts @SalesInvoiceId=" + SalesInvoiceId + ",@GodownId=0").ToList();
            return salesDetailProducts;
        }


        public static List<SP_SalesReturnDetail> GetSalesReturnDetails(string StartDate, string EndDate, int godownId = 0)
        {
            var _context = new DataContext();
            var salesDetails = _context.Database.SqlQuery<SP_SalesReturnDetail>("SP_SalesReturnDetail @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GodownId=" + godownId).ToList();
            return salesDetails;
        }

        public static List<SP_SalesReturnGodownHeader> GetSalesReturnGodownHeaders(string StartDate, string EndDate, string godownIds)
        {
            var _context = new DataContext();
            var salesDetails = _context.Database.SqlQuery<SP_SalesReturnGodownHeader>("SP_SalesReturnGodownHeader @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GodownId='" + godownIds + "'").ToList();
            return salesDetails;
        }

        public static List<SP_SalesReturnDetailProducts> GetSalesReturnDetailProducts(int SalesReturnInvoiceId, int godownId = 0)
        {
            var _context = new DataContext();
            var salesDetailProducts = _context.Database.SqlQuery<SP_SalesReturnDetailProducts>("SP_SalesReturnDetailProducts @SalesReturnInvoiceId=" + SalesReturnInvoiceId + ",@GodownId=" + godownId).ToList();
            return salesDetailProducts;
        }

        public static List<SP_SalesProductTerms> GetSalesProductWiseTerms(int SalesInvoiceDetailId)
        {
            var _context = new DataContext();
            var salesDetailProducts = _context.Database.SqlQuery<SP_SalesProductTerms>("SP_SalesProductTerms @SalesInvoiceDetailId=" + SalesInvoiceDetailId).ToList();
            return salesDetailProducts;
        }

        public static List<Sp_SalesBillTerms> GetSalesBillWiseTerms(int SalesInvoiceId)
        {
            var _context = new DataContext();
            var salesDetailProducts = _context.Database.SqlQuery<Sp_SalesBillTerms>("Sp_SalesBillTerms @SalesInvoiceId=" + SalesInvoiceId).ToList();
            return salesDetailProducts;
        }

        public static List<SP_SalesChallanDetails> GetSalesChallanDetails(string StartDate, string EndDate, int groupBy = 0)
        {
            var _context = new DataContext();
            var salesChallan = _context.Database.SqlQuery<SP_SalesChallanDetails>("SP_SalesChallanDetails @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GroupBy=" + groupBy).ToList();
            return salesChallan;
        }
        public static List<SP_SalesOrderDetails> GetSalesOrderDetails(string StartDate, string EndDate, int GroupBy)
        {
            var _context = new DataContext();
            var salesDetails = _context.Database.SqlQuery<SP_SalesOrderDetails>("SP_SalesOrderDetails @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GroupBy=" + GroupBy).ToList();
            return salesDetails;
        }

        public static DataTable GetSalesChallanSummaryTable(string StartDate, string EndDate, int groupby)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@GroupBy", groupby);
            var summary = ConnectionString.GetDataTable("SP_SalesChallanSummary", param);
            return summary;
        }

        public static DataTable GetSalesOrder(string StartDate, string EndDate, int GroupBy)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@GroupBy", GroupBy);
            var summary = ConnectionString.GetDataTable("SP_SalesOrderSummary", param);
            return summary;
        }


        #endregion

        #region Purchase

        public static DataTable GetPurchaseSummary(string StartDate, string EndDate, int GroupBy)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@GroupBy", GroupBy);
            var summary = ConnectionString.GetDataTable("SP_PurchaseSummary", param);
            return summary;
        }
        public static DataTable GetPurchaseReturnSummary(string StartDate, string EndDate, int groupby)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@GroupBy", groupby);
            var summary = ConnectionString.GetDataTable("SP_PurchaseReturnSummary", param);
            return summary;
        }
        public static DataTable GetPurchaseOrder(string StartDate, string EndDate, int GroupBy)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@GroupBy", GroupBy);
            var summary = ConnectionString.GetDataTable("SP_PurchaseOrderSummary", param);
            return summary;
        }
        public static DataTable GetPurchaseChallan(string StartDate, string EndDate, int GroupBy)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@GroupBy", GroupBy);
            var summary = ConnectionString.GetDataTable("SP_PurchaseChallanSummary", param);
            return summary;
        }
        public static List<SP_PurchaseGodownHeader> GetPurchaseGodownHeaders(string StartDate, string EndDate, string godownIds)
        {
            var _context = new DataContext();
            var purchaseDetails = _context.Database.SqlQuery<SP_PurchaseGodownHeader>("SP_PurchaseGodownHeader @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GodownId='" + godownIds + "'").ToList();
            return purchaseDetails;
        }

        public static List<SP_PurchaseDetail> GetPurchaseDetails(string StartDate, string EndDate, int godownId = 0)
        {
            var _context = new DataContext();
            var purchaseDetails = _context.Database.SqlQuery<SP_PurchaseDetail>("SP_PurchaseDetail @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GodownId=" + godownId).ToList();
            return purchaseDetails;
        }
        public static List<SP_PurchaseOrderDetails> GetPurchaseOrderDetails(string StartDate, string EndDate, int GroupBy)
        {
            var _context = new DataContext();
            var purchaseDetails = _context.Database.SqlQuery<SP_PurchaseOrderDetails>("SP_PurchaseOrderDetails @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GroupBy=" + GroupBy).ToList();
            return purchaseDetails;
        }
        public static List<SP_PurchaseChallanDetails> GetPurchaseChallanDetails(string StartDate, string EndDate, int GroupBy)
        {
            var _context = new DataContext();
            var purchaseDetails = _context.Database.SqlQuery<SP_PurchaseChallanDetails>("SP_PurchaseChallanDetails @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GroupBy=" + GroupBy).ToList();
            return purchaseDetails;
        }

        public static List<SP_PurchaseDetailProducts> GetPurchaseDetailProducts(int PurchaseInvoiceId, int godownId = 0)
        {
            var _context = new DataContext();
            var purchaseDetailProducts = _context.Database.SqlQuery<SP_PurchaseDetailProducts>("SP_PurchaseDetailProducts @PurchaseInvoiceId=" + PurchaseInvoiceId + ",@GodownId=" + godownId).ToList();
            return purchaseDetailProducts;
        }


        public static List<SP_PurchaseReturnGodownHeader> GetPurchaseReturnGodownHeaders(string StartDate, string EndDate, string godownIds)
        {
            var _context = new DataContext();
            var purchaseDetails = _context.Database.SqlQuery<SP_PurchaseReturnGodownHeader>("SP_PurchaseReturnGodownHeader @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GodownId='" + godownIds + "'").ToList();
            return purchaseDetails;
        }

        public static List<SP_PurchaseReturnDetail> GetPurchaseReturnDetails(string StartDate, string EndDate, int godownId = 0)
        {
            var _context = new DataContext();
            var purchaseDetails = _context.Database.SqlQuery<SP_PurchaseReturnDetail>("SP_PurchaseReturnDetail @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@GodownId=" + godownId).ToList();
            return purchaseDetails;
        }

        public static List<SP_PurchaseReturnDetailProducts> GetPurchaseReturnDetailProducts(int PurchaseReturnInvoiceId, int godownId = 0)
        {
            var _context = new DataContext();
            var purchaseDetailProducts = _context.Database.SqlQuery<SP_PurchaseReturnDetailProducts>("SP_PurchaseReturnDetailProducts @PurchaseReturnInvoiceId=" + PurchaseReturnInvoiceId + ",@GodownId=" + godownId).ToList();
            return purchaseDetailProducts;
        }

        public static List<SP_PurchaseProductTerms> GetPurchaseProductWiseTerms(int PurchaseInvoiceDetailId)
        {
            var _context = new DataContext();
            var purchaseDetailProducts = _context.Database.SqlQuery<SP_PurchaseProductTerms>("SP_PurchaseProductTerms @PurchaseInvoiceDetailId=" + PurchaseInvoiceDetailId).ToList();
            return purchaseDetailProducts;
        }

        public static List<Sp_PurchaseBillTerms> GetPurchaseBillWiseTerms(int PurchaseInvoiceId)
        {
            var _context = new DataContext();
            var purchaseDetailProducts = _context.Database.SqlQuery<Sp_PurchaseBillTerms>("Sp_PurchaseBillTerms @PurchaseInvoiceId=" + PurchaseInvoiceId).ToList();
            return purchaseDetailProducts;
        }

        public static List<SP_PurchaseFooterTermSummary> GetPurchaseFooterTerms(string StartDate, string EndDate)
        {
            var _context = new DataContext();
            var footerTerms = _context.Database.SqlQuery<SP_PurchaseFooterTermSummary>("SP_PurchaseFooterTermSummary @StartDate='" + StartDate + "',@EndDate='" + EndDate + "'").ToList();
            return footerTerms;
        }

        public static List<SP_SalesFooterTermSummary> GetSalesFooterTerms(string StartDate, string EndDate)
        {
            var _context = new DataContext();
            var footerTerms = _context.Database.SqlQuery<SP_SalesFooterTermSummary>("SP_SalesFooterTermSummary @StartDate='" + StartDate + "',@EndDate='" + EndDate + "'").ToList();
            return footerTerms;
        }
        #endregion

        #region Inventory

        public static List<SP_StockLedgerProducts> GetStockLedgerProduct(string StartDate, string EndDate, string ProductId, int Batch, string GodownId)
        {
            var _context = new DataContext();
            var str = "SP_StockLedgerProducts @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@ProductId='" +
                      ProductId + "',@GodownId='" + GodownId + "',@BatchSerialNo=" + Batch;
            var productOpenings = _context.Database.SqlQuery<SP_StockLedgerProducts>("SP_StockLedgerProducts @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@ProductId='" + ProductId + "',@GodownId='" + GodownId + "',@BatchSerialNo=" + Batch).ToList();
            return productOpenings;
        }

        public static List<SP_StocLedgerGodowns> GetStockLedgerGodowns(string StartDate, string EndDate, string ProductId, string GodownId)
        {
            var _context = new DataContext();
            GodownId = GodownId == "null" ? "" : GodownId;
            var str = "SP_StocLedgerGodowns @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@ProductId='" +
                      ProductId + "',@GodownId='" + GodownId + "'";
            var godowns = _context.Database.SqlQuery<SP_StocLedgerGodowns>("SP_StocLedgerGodowns @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@ProductId='" + ProductId + "',@GodownId='" + GodownId + "'").ToList();
            return godowns;
        }

        public static List<SP_StockProductOpening> GetStockProductOpening(string StartDate, string ProductId, int FYId, int Batch)
        {
            var _context = new DataContext();
            var str = "SP_StockProductOpening @StartDate='" + StartDate + "',@ProductId='" + ProductId + "',@FYId=" + FYId +
            ",@BatchSerialNo=0";
            var productOpenings = _context.Database.SqlQuery<SP_StockProductOpening>("SP_StockProductOpening @StartDate='" + StartDate + "',@ProductId='" + ProductId + "',@FYId=" + FYId + ",@BatchSerialNo=" + Batch).ToList();
            return productOpenings;
        }

        public static List<SP_StockLedgerProductWise> GetStockProductLedger(string StartDate, string EndDate, string ProductId, int FYId, int Batch, int WithValue)
        {
            var _context = new DataContext();
            var str = "SP_StockLedgerProductWise @StartDate='" + StartDate + "', @EndDate='" + EndDate +
                      "', @ProductId='" + ProductId + "',@FYId=" + FYId + ",@GodownId=0" + ",@BatchSerialNo=" + Batch + ",@WithValue=" + WithValue;
            var productLedgers = _context.Database.SqlQuery<SP_StockLedgerProductWise>("SP_StockLedgerProductWise @StartDate='" + StartDate + "', @EndDate='" + EndDate + "', @ProductId='" + ProductId + "',@FYId=" + FYId + ",@GodownId=0" + ",@BatchSerialNo=" + Batch + ",@WithValue=" + WithValue).ToList();
            return productLedgers;
        }

        public static List<SP_StockLedgerSummary> GetStockLedgerSummary(string StartDate, string EndDate, string ProductId, int FYId, int WithValue)
        {
            var _context = new DataContext();
            var productLedgers = _context.Database.SqlQuery<SP_StockLedgerSummary>("SP_StockLedgerSummary @StartDate='" + StartDate + "', @EndDate='" + EndDate + "', @ProductId='" + ProductId + "',@FYId=" + FYId + ",@WithValue=" + WithValue).ToList();
            return productLedgers;
        }

        public static List<SP_PriceList> GetPriceListReport(string AsOnDate, string ProductId, int FYId)
        {
            var _context = new DataContext();
            var productLedgers = _context.Database.SqlQuery<SP_PriceList>("SP_PriceList @AsOnDate='" + AsOnDate + "', @ProductId='" + ProductId + "',@FYId=" + FYId).ToList();
            _context.Dispose();
            return productLedgers;
        }

        public static List<SP_ReOrderStatus> GetReOrderStatusReport(string AsOnDate, string ProductId, int FYId)
        {
            var _context = new DataContext();
            var reOrderStatus = _context.Database.SqlQuery<SP_ReOrderStatus>("SP_ReOrderStatus @AsOnDate='" + AsOnDate + "', @ProductId='" + ProductId + "',@FYId=" + FYId).ToList();
            _context.Dispose();
            return reOrderStatus;
        }

        public static List<SP_SalesVatRegister> GetSalesVatRegisterReport(string StartDate, string EndDate, int VatTerm)
        {
            var _context = new DataContext();
            var salesRegister = _context.Database.SqlQuery<SP_SalesVatRegister>("SP_SalesVatRegister @StartDate='" + StartDate + "', @EndDate='" + EndDate + "',@BillingTermId=" + VatTerm).ToList();
            _context.Dispose();
            return salesRegister;
        }

        public static List<SP_PurchaseVatRegister> GetPurchaseVatRegisterReport(string StartDate, string EndDate, int VatTerm)
        {
            var _context = new DataContext();
            var purchaseRegister = _context.Database.SqlQuery<SP_PurchaseVatRegister>("SP_PurchaseVatRegister @StartDate='" + StartDate + "', @EndDate='" + EndDate + "',@BillingTermId=" + VatTerm).ToList();
            _context.Dispose();
            return purchaseRegister;
        }


        public static List<SP_ProductBatchSummary> GetProductBatchSummaryReport(string ProductId, int BranchId, string ExpiredGoods,
         int RemainingDaysToExpire, string MStartDate, string MEndDate, string EStartDate, string EEndDate)
        {
            var _context = new DataContext();
            var sql = "SP_ProductBatchSummary @ProductId ='" + ProductId + "', @BranchId =" + BranchId +
                      ",@ExpiredGoods =" + ExpiredGoods + ",@RemainingDaysToExpire =" + RemainingDaysToExpire +
                      ", @MfgStartDate ='" + MStartDate + "',@MfgEndDate ='" + MEndDate + "',@ExpStartDate ='" +
                      EStartDate + "',@ExpEndDate ='" + EEndDate + "'";
            var list = _context.Database.SqlQuery<SP_ProductBatchSummary>(sql).ToList();
            _context.Dispose();
            return list;
        }

        #endregion

        #region Dashboard

        public static List<SP_AnalysisMonthly> GetAnalysisMonthly(int Year, int Month, int Divisor)
        {
            var _context = new DataContext();
            var str = "SP_AnalysisMonthly @Year=" + Year + ",@Month=" + Month + ",@Divisor=" + Divisor;
            var analysis = _context.Database.SqlQuery<SP_AnalysisMonthly>("SP_AnalysisMonthly @Year=" + Year + ",@Month=" + Month + ",@Divisor=" + Divisor).ToList();
            return analysis;
        }

        public static List<SP_AnalysisYearly> GetAnalysisYearly(int Year, int Divisor)
        {
            var _context = new DataContext();
            var analysis = _context.Database.SqlQuery<SP_AnalysisYearly>("SP_AnalysisYearly @Year=" + Year + ",@Divisor=" + Divisor).ToList();
            return analysis;
        }
        public static List<SP_CashFlowMonthly> GetCashFlowMonthly(string StartDate, string EndDate, int FYId)
        {
            var _context = new DataContext();
            var str = "SP_CashFlowMonthly @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@FYId=" + FYId;
            var cashFlow =
                _context.Database.SqlQuery<SP_CashFlowMonthly>("SP_CashFlowMonthly @StartDate='" + StartDate +
                                                               "',@EndDate='" + EndDate + "',@FYId=" + FYId).ToList();
            return cashFlow;
        }
        public static List<SP_BankFlowMonthly> GetBankFlowMonthly(string StartDate, string EndDate, int FYId)
        {
            var _context = new DataContext();
            var str = "SP_BankFlowMonthly @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@FYId=" + FYId;
            var cashFlow =
                _context.Database.SqlQuery<SP_BankFlowMonthly>("SP_BankFlowMonthly @StartDate='" + StartDate +
                                                               "',@EndDate='" + EndDate + "',@FYId=" + FYId).ToList();
            return cashFlow;
        }
        public static List<SP_PLTotalForDashBoard> GetPLTotal(string StartDate, string EndDate, int FYId)
        {
            var _context = new DataContext();
            var plTotal =
                _context.Database.SqlQuery<SP_PLTotalForDashBoard>("SP_PLTotalForDashBoard @StartDate='" + StartDate +
                                                                   "',@EndDate='" + EndDate + "',@FYId=" + FYId).ToList();
            return plTotal;
        }

        public static List<SP_AccountWatchList> GetAccountWatchList(string StartDate, string EndDate, int FYId, string LedgerCategory, string LedgerList, int TopLedger)
        {
            var _context = new DataContext();
            var str = "SP_AccountWatchList @StartDate='" + StartDate + "',@EndDate='" + EndDate + "',@FYId=" + FYId +
                      ",@LedgerCategory=" + LedgerCategory + ",@LedgerList=" + LedgerList + ",@TopLedger=" + TopLedger;
            var accountWatch =
                _context.Database.SqlQuery<SP_AccountWatchList>("SP_AccountWatchList @StartDate='" + StartDate +
                                                                "',@EndDate='" + EndDate + "',@FYId=" + FYId +
                                                                ",@LedgerCategory=" + LedgerCategory + ",@LedgerList='" +
                                                                LedgerList + "',@TopLedger=" + TopLedger).ToList();
            return accountWatch;
        }

        public static List<SP_TopItems> GetTopItemses(string StartDate, string EndDate, int FYId, int TopItems, int branchid)
        {
            var _context = new DataContext();
            var topItems = _context.Database.SqlQuery<SP_TopItems>("SP_TopItems @StartDate='" + StartDate +
                                                                   "',@EndDate='" + EndDate + "',@FYId=" + FYId +
                                                                   ",@TopItems=" + TopItems + " ,@BranchId=" + branchid).ToList();
            return topItems;
        }
        #endregion

#region Attendance 
        public static List<Sp_DailyAttendance> GetdailyAttendance(string employees, string FromDate, string ToDate, string departments)
        {
            var sql = "EXEC sp_DailyAttendaceReport @empid='" + employees + "',@startdate='" + FromDate + "',@enddate='" + ToDate + "',@departmentid='" + departments + "'";
            var report = _context.Database.SqlQuery<Sp_DailyAttendance>(sql).ToList();
            return report;
        }
        public static List<SP_StudentDeviceAttendance> GetStudentAttendance(int  classId, string FromDate, string ToDate, int sectionId,int AYID,int WorkingDay,int studentid)
        {
            var sql = "EXEC SP_StudentDeviceAttendance @Class=" + classId + ",@FromDate='" + FromDate + "',@ToDate='" + ToDate + "',@Section=" + sectionId + ",@AcemedicYear=" + AYID + ",@NoOfWorkingDays=" + WorkingDay+",@StudentId="+studentid;
            var report = _context.Database.SqlQuery<SP_StudentDeviceAttendance>(sql).ToList();
            return report;
        }
#endregion


    }
}
