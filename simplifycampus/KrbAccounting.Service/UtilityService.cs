
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Enums;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Service.Models.StudentProfile;


namespace KRBAccounting.Service
{
    public static class UtilityService
    {
        public static DataContext _context = new DataContext();

        public static List<SchClass> GetClassList()
        {


            var list = _context.SchClass.ToList();

                     list.Insert(0, new SchClass { Id = 0, Description = "All" });
            return list;
        }

        public static List<ScProgramMaster> GetProgramList()
        {
            var list = _context.ScProgramMaster.ToList();

            return list;
        }

        public static string GetSeName(string name)
        {
            if (String.IsNullOrEmpty(name))
                return string.Empty;
            const string okChars = "abcdefghijklmnopqrstuvwxyz1234567890 _-";
            name = name.Trim().ToLowerInvariant();


            var sb = new StringBuilder();
            foreach (string c2 in name.ToCharArray().Select(c => c.ToString()).Where(c2 => okChars.Contains(c2)))
            {
                sb.Append(c2);
            }
            string name2 = sb.ToString();
            name2 = name2.Replace(" ", "-");
            while (name2.Contains("--"))
                name2 = name2.Replace("--", "-");
            while (name2.Contains("__"))
                name2 = name2.Replace("__", "_");
            return (name2);
        }


        public static string GetResultSystem(int enumId)
        {
            return Enum.GetName(typeof (ResultSystem), enumId);
        }

        public static string GetClassType(int enumId)
        {
            return Enum.GetName(typeof (ClassType), enumId);
        }

       
        public static string GetEvaluateForPass(int enumId)
        {
            return Enum.GetName(typeof (EvaluateForPass), enumId);
        }

        public static string GetSubjectType(int enumId)
        {
            return Enum.GetName(typeof (SubjectType), enumId);
        }

        public static string GetActive(bool active)
        {
            string notification = string.Empty;
            notification = active == true ? "Yes" : "No";
            return notification;
        }

        public static String GetAgentNameById(int id)
        {
            var Name = string.Empty;
            if (id == null || id == 0)
            {
                return Name;
            }
            var agent = _context.Agents.Where(x => x.Id == id).FirstOrDefault();
            if (agent != null)
                Name = agent.Name;
            return Name;
        }

        public static List<SP_TrialBalanceLedgerWiseAsOnDate> GetTrialBalanceReportNormal(string AsOn, int FYId,
            int Subledger, int branchid)
        {
            DataContext _context = new DataContext();
            var report =
                _context.Database.SqlQuery<SP_TrialBalanceLedgerWiseAsOnDate>(
                    "EXEC SP_TrialBalanceLedgerWiseAsOnDate @AsOnDate='" + AsOn + "',@FYId=" + FYId + ",@SubLedger=" +
                    Subledger + " ,@BranchId=" + branchid).ToList();
            return report;
        }

        public static List<SP_TrialBalanceAcGroupWiseAsOnDate> GetTrialBalanceReportGroupWise(string AsOn, int FYId,
            int AcSubGroup, int Ledger, int SubLedger, int branchid)
        {
            DataContext _context = new DataContext();
            var str = "SP_TrialBalanceAcGroupWiseAsOnDate @AsOnDate='" + AsOn + "',@FYId=" + FYId + ",@AcSubGroup=" +
                      AcSubGroup + ",@Ledger=" + Ledger + ",@SubLedger=" + SubLedger + " ,@BranchId=" + branchid;
            var report =
                _context.Database.SqlQuery<SP_TrialBalanceAcGroupWiseAsOnDate>(
                    "EXEC SP_TrialBalanceAcGroupWiseAsOnDate @AsOnDate='" + AsOn + "',@FYId=" + FYId + ",@AcSubGroup=" +
                    AcSubGroup + ",@Ledger=" + Ledger + ",@SubLedger=" + SubLedger + ", @BranchId = " + branchid)
                    .ToList();
            return report;
        }

        public static List<SP_TrialBalancePeriodic> GetTrialBalanceReportPeriodic(string startDate, string endDate,
            int FYId, int ACGroup, int AcSubGroup, int Ledger, int SubLedger, int branchid)
        {
            DataContext _context = new DataContext();
            var str = "SP_TrialBalancePeriodic @StartDate='" + startDate + "', @EndDate='" + endDate + "',@FYId=" + FYId +
                      ",@AcGroup=" + ACGroup + ",@AcSubGroup=" + AcSubGroup + ",@Ledger=" + Ledger + ",@SubLedger=" +
                      SubLedger + ",@BranchId=" + branchid;
            var report =
                _context.Database.SqlQuery<SP_TrialBalancePeriodic>("EXEC SP_TrialBalancePeriodic @StartDate='" +
                                                                    startDate + "', @EndDate='" + endDate + "',@FYId=" +
                                                                    FYId + ",@AcGroup=" + ACGroup + ",@AcSubGroup=" +
                                                                    AcSubGroup + ",@Ledger=" + Ledger + ",@SubLedger=" +
                                                                    SubLedger + ", @BranchId = " + branchid).ToList();
            return report;
        }

        public static List<SP_BSLedgerWise> GetBalanceLedgerWise(string asOnDate, int FYId, string type, int SubLedger,
            int branchid)
        {
            DataContext _context = new DataContext();
            var str = "EXEC SP_BSLedgerWise @AsOnDate='" + asOnDate + "',@FYId=" + FYId + ",@Type='" + type +
                      "',@SubLedger=" + SubLedger + ",@BranchId=" + branchid;
            var report =
                _context.Database.SqlQuery<SP_BSLedgerWise>("EXEC SP_BSLedgerWise @AsOnDate='" + asOnDate + "',@FYId=" +
                                                            FYId + ",@Type='" + type + "',@SubLedger=" + SubLedger +
                                                            ",@BranchId=" + branchid + "").ToList();
            return report;
        }

        public static List<SP_BSAccountGroupWise> GetBalanceAccountGroupWise(string asOnDate, int FYId, string type,
            int ACGroup, int AcSubGroup, int Ledger, int SubLedger, int branchid)
        {
            DataContext _context = new DataContext();
            var sql = "EXEC SP_BSAccountGroupWise @AsOnDate='" + asOnDate + "',@FYId=" + FYId + ",@Type='" + type +
                      "',@AcGroup=" + ACGroup + ",@AcSubGroup=" + AcSubGroup + ",@Ledger=" + Ledger + ",@SubLedger=" +
                      SubLedger + ",@BranchId=" + branchid + "";
            var report =
                _context.Database.SqlQuery<SP_BSAccountGroupWise>("EXEC SP_BSAccountGroupWise @AsOnDate='" + asOnDate +
                                                                  "',@FYId=" + FYId + ",@Type='" + type + "',@AcGroup=" +
                                                                  ACGroup + ",@AcSubGroup=" + AcSubGroup + ",@Ledger=" +
                                                                  Ledger + ",@SubLedger=" + SubLedger + ",@BranchId=" +
                                                                  branchid + "").ToList();
            return report;
        }

        public static List<SP_BSPeriodic> GetBalancePeriodic(string startDate, string endDate, int FYId, string type,
            int ACGroup, int AcSubGroup, int Ledger, int SubLedger, int branchid)
        {
            DataContext _context = new DataContext();
            var sql = "EXEC SP_BSPeriodic @StartDate='" + startDate + "',@EndDate='" + endDate + "',@FYId=" + FYId +
                      ",@Type='" + type + "',@AcGroup=" + ACGroup + ",@AcSubGroup=" + AcSubGroup + ",@Ledger=" + Ledger +
                      ",@SubLedger=" + SubLedger + ",@BranchId=" + branchid + "";
            var report =
                _context.Database.SqlQuery<SP_BSPeriodic>("EXEC SP_BSPeriodic @StartDate='" + startDate + "',@EndDate='" +
                                                          endDate + "',@FYId=" + FYId + ",@Type='" + type +
                                                          "',@AcGroup=" + ACGroup + ",@AcSubGroup=" + AcSubGroup +
                                                          ",@Ledger=" + Ledger + ",@SubLedger=" + SubLedger +
                                                          ",@BranchId=" + branchid + "").ToList();
            return report;
        }

        public static List<SP_PLAcGroupHeader> GetProfitAndLossHeaders(string startDate, string endDate,
            string accountType, int FYId, int branchid)
        {
            var str = "EXEC SP_PLAcGroupHeader @StartDate='" + startDate + "', @EndDate='" + endDate +
                      "',@GroupAccountType='" + accountType + "',@FYId=" + FYId + " ,@BranchId=" + branchid + "";
            DataContext _context = new DataContext();
            var report =
                _context.Database.SqlQuery<SP_PLAcGroupHeader>("EXEC SP_PLAcGroupHeader @StartDate='" + startDate +
                                                               "', @EndDate='" + endDate + "',@GroupAccountType='" +
                                                               accountType + "',@FYId=" + FYId + " ,@BranchId=" +
                                                               branchid + "").ToList();
            return report;
        }

        public static List<SP_PLLedgerTotals> GetProfitAndLossLedgerTotals(string startDate, string accountType,
            int accountGroupId, int accountsubGroupId, int ledgerId, int FYId, int AccSubGrp, int SubLed, int branchid)
        {
            DataContext _context = new DataContext();
            if (AccSubGrp == null)
                AccSubGrp = 0;
            if (SubLed == null)
                SubLed = 0;

            var str = "EXEC SP_PLLedgerTotals @StartDate='" + startDate + "',@GroupAccountType='" + accountType +
                      "', @AccountGroupId='" + accountGroupId + "',@AccountSubGroupId='" + accountsubGroupId +
                      "',@LedgerId='" + ledgerId + "',@FYId=" + FYId + ",@AccSubGrp=" + AccSubGrp + ",@SubLed=" + SubLed +
                      " ,@BranchId=" + branchid + "";
            var report =
                _context.Database.SqlQuery<SP_PLLedgerTotals>("EXEC SP_PLLedgerTotals @StartDate='" + startDate +
                                                              "',@GroupAccountType='" + accountType +
                                                              "', @AccountGroupId='" + accountGroupId +
                                                              "',@AccountSubGroupId='" + accountsubGroupId +
                                                              "',@LedgerId='" + ledgerId + "',@FYId=" + FYId +
                                                              ",@AccSubGrp=" + AccSubGrp + ",@SubLed=" + SubLed +
                                                              " ,@BranchId=" + branchid + "").ToList();
            return report;
        }

        public static List<SP_PLPeriodicLedgers> GetProfitAndLossPeriodicLedger(string startDate, string endDate,
            string accountType, int accountGroupId, int accountsubGroupId, int ledgerId, int FYId, int AccSubGrp,
            int SubLed, int branchid)
        {
            DataContext _context = new DataContext();
            var str = "EXEC SP_PLPeriodicLedgers @StartDate='" + startDate + "', @EndDate='" + endDate +
                      "',@GroupAccountType='" + accountType + "', @AccountGroupId='" + accountGroupId +
                      "',@AccountSubGroupId='" + accountsubGroupId + "',@LedgerId='" + ledgerId + "',@FYId=" + FYId +
                      ",@AccSubGrp=" + AccSubGrp + ",@SubLed=" + SubLed + ",@BranchId=" + branchid + "";
            var report =
                _context.Database.SqlQuery<SP_PLPeriodicLedgers>("EXEC SP_PLPeriodicLedgers @StartDate='" + startDate +
                                                                 "', @EndDate='" + endDate + "',@GroupAccountType='" +
                                                                 accountType + "', @AccountGroupId='" + accountGroupId +
                                                                 "',@AccountSubGroupId='" + accountsubGroupId +
                                                                 "',@LedgerId='" + ledgerId + "',@FYId=" + FYId +
                                                                 ",@AccSubGrp=" + AccSubGrp + ",@SubLed=" + SubLed +
                                                                 " ,@BranchId=" + branchid + "").ToList();
            return report;
        }

        public static List<SP_PLTotalByGroupSide> GetProfitAndLossTotalByGroupSide(string startDate, string endDate,
            int FYId, int branchid)
        {
            DataContext _context = new DataContext();
            var report =
                _context.Database.SqlQuery<SP_PLTotalByGroupSide>("EXEC SP_PLTotalByGroupSide @StartDate='" + startDate +
                                                                  "', @EndDate='" + endDate + "', @FYId='" + FYId +
                                                                  "',@BranchId=" + branchid + "").ToList();
            return report;
        }



        public static List<SP_StudentLedgerSummary> GetStudentLedgerSumary(string startDate, string endDate,
            int classId, int sectionId, string studentId, int AcademicYearId)
        {
            var sql = "EXEC SP_StudentLedgerSummary @StartDate='" + startDate +
                      "', @EndDate='" + endDate + "', @ClassId=" + classId + ",@SectionId=" + sectionId +
                      ",@StudentId='" + studentId + "',@AYId=" + AcademicYearId;
            var report =
                _context.Database.SqlQuery<SP_StudentLedgerSummary>(sql)
                    .ToList();
            return report;
        }

        public static List<SP_StudentWiseHouse> GetStudentHouseMapping(string startDate, string endDate,
            int classId, int houseId, string studentId)
        {
            var sql = "EXEC SP_StudentWiseHouse @StartDate='" + startDate +
                      "', @EndDate='" + endDate + "', @ClassId=" + classId + ",@HouseId=" + houseId +
                      ",@StudentId='" + studentId + "'";
            var report =
                _context.Database.SqlQuery<SP_StudentWiseHouse>(sql)
                    .ToList();
            return report;
        }

        public static List<ScMonthlyBillStudent> GetBillDetailByStudentId(DateTime StartDate, DateTime EndDate,
            int StudentId)
        {
            //var predicate = PredicateBuilder.True<ScBillTransaction>();
            //if (StartDate.ToString("MM/dd/yyyy") != "01/01/0001")
            //{
            //    predicate = predicate.And(x =>x.Date >= StartDate);
            //}
            //if (EndDate.ToString("MM/dd/yyyy") != "01/01/0001")
            //{
            //    predicate = predicate.And(x => x.Date <= EndDate);
            //}
            //predicate = predicate.And(x => x.StudentId == StudentId);
            var context = new DataContext();

            var monthlyBill = context.MonthlyBillDetails.Where(
                x => x.Date >= StartDate && x.Date <= EndDate && x.StudentId == StudentId).ToList();
            return monthlyBill;
        }

        public static List<ScFeeReceipt> GetFeeReceiptList( int StudentId, int monthid , int year)
        {
            var context = new DataContext();
          
            var receipt = context.ScFeeReceipt.Where(x => x.StudentId == StudentId && x.PaidUpMonth == monthid && x.PaidUpYear == year).ToList();
            return receipt;
        }
        public static List<Sp_SubjectWiseMarksDetail> GetSubjectWiseMarksDetail(int examId, int classId, int subjectId,
            int subjecttype)
        {
            var sql = "EXEC Sp_SubjectWiseMarksDetail @examId=" + examId +
                      ",@classId=" + classId + ",@subjectId=" +
                      subjectId + ",@SubjectType=" + subjecttype;
            var data =
                _context.Database.SqlQuery<Sp_SubjectWiseMarksDetail>(sql).ToList();
            return data;
        }

        public static List<SP_MonthlyBillGenerate> GetMonthlyBillGenerate(int classId, int boaderId, int shiftId,
            string studentId, string feeitem, int AYId)
        {
            var sql = "EXEC SP_MonthlyBillGenerate @ClassId=" + classId + ",@BoaderId=" + boaderId + ",@ShiftId=" +
                      shiftId + ",@StdId='" + studentId + "',@FeeItemId='" + feeitem + "',@AYId=" + AYId;
            var data =
                _context.Database.SqlQuery<SP_MonthlyBillGenerate>(sql).ToList();
            return data;

        }

        public static List<SP_MonthlyBillFeeWise> GetMonthlyBillGenerateFeeWise(int classId, int boaderId, int shiftId,
            string studentId, string feeitem)
        {
            var sql = "EXEC SP_MonthlyBillFeeWise @ClassId=" + classId + ",@BoaderId=" + boaderId + ",@ShiftId=" +
                      shiftId + ",@StdId='" + studentId + "',@feeitem_Id='" + feeitem + "'";
            var data =
                _context.Database.SqlQuery<SP_MonthlyBillFeeWise>(sql).ToList();
            return data;
        }

        public static List<ScBookIssued> GetBookIssued(string type, string cardtype, int issuedto, int bookid)
        {
            var sql = "EXEC SP_BookIssued @type='" + type + "',@issuedTo=" + issuedto + ",@cardtype='" + cardtype +
                      "',@bookId=" + bookid;
            var data =
                _context.Database.SqlQuery<ScBookIssued>(sql).ToList();
            return data;

        }

        public static List<Sp_SubjectWiseMarksDetail> GetSubjectWiseMarksDetailEdit(int classId, int examId,
            int subjectId)
        {
            var data =
                _context.Database.SqlQuery<Sp_SubjectWiseMarksDetail>("EXEC Sp_SubjectWiseMarksDetailEdit @examId=" +
                                                                      examId + ",@classId=" + classId + ",@subjectId=" +
                                                                      subjectId).ToList();
            return data;
        }

        public static List<SP_StudentWiseMarksDetail> GetStudentWiseMarksDetail(int examId, int classId, int studentId)
        {
            var data =
                _context.Database.SqlQuery<SP_StudentWiseMarksDetail>("EXEC SP_StudentWiseMarksDetail @examId=" + examId +
                                                                      ",@classId=" + classId + ",@studentId=" +
                                                                      studentId).ToList();
            return data;
        }

        public static List<SP_StudentWiseMarksDetail> GetStudentWiseMarksDetailEdit(int examId, int classId,
            int studentId)
        {
            var sql = "EXEC SP_StudentWiseMarksDetailEdit @examId=" +
                      examId + ",@classId=" + classId + ",@studentId=" +
                      studentId;
            var data =
                _context.Database.SqlQuery<SP_StudentWiseMarksDetail>(sql).ToList();
            return data;
        }

        public static List<SP_SubLedgers> GetSubLedgers(int ledgerId, string startDate, string endDate, int FYId,
            string subLedgers, int branchId)
        {
            DataContext _context = new DataContext();
            //var str = "EXEC SP_SubLedgers @StartDate='" + startDate + "', @EndDate='" + endDate + "', @LedgerId=" + ledgerId + ",@FYId=" + FYId + ",@SubLedgerId='" + subLedgers + "'";
            var report =
                _context.Database.SqlQuery<SP_SubLedgers>("EXEC SP_SubLedgers @StartDate='" + startDate +
                                                          "', @EndDate='" + endDate + "', @LedgerId=" + ledgerId +
                                                          ",@FYId=" + FYId + ",@SubLedgerId='" + subLedgers +
                                                          "',@BranchId=" + branchId).ToList();
            return report;
        }

        public static List<SP_PartyLedgers> GetLedgerParties(int ledgerId, string startDate, string endDate, int FYId,
            int ProductDetails, int TermDetails, int DspsubLedger, int branchId, int subLedgerId = 0)
        {
            DataContext _context = new DataContext();
            var str = "EXEC SP_PartyLedgers @StartDate='" + startDate + "', @EndDate='" + endDate + "', @LedgerId=" +
                      ledgerId + ",@FYId=" + FYId + ",@SubLedger=" + DspsubLedger + ",@SubLedgerId=" + subLedgerId +
                      ",@ProductDetails=" + ProductDetails + ",@TermDetails=" + TermDetails + ",@BranchId=" + branchId;
            var report =
                _context.Database.SqlQuery<SP_PartyLedgers>("EXEC SP_PartyLedgers @StartDate='" + startDate +
                                                            "', @EndDate='" + endDate + "', @LedgerId=" + ledgerId +
                                                            ",@FYId=" + FYId + ",@SubLedger=" + DspsubLedger +
                                                            ",@SubLedgerId=" + subLedgerId + ",@ProductDetails=" +
                                                            ProductDetails + ",@TermDetails=" + TermDetails +
                                                            ",@BranchId=" + branchId).ToList();
            return report;
        }

        public static List<SP_PartyLedgersSummary> GetLedgerPartiesSummary(int ledgerId, string startDate,
            string endDate, int groupById, int groupBy, bool? docAgent, int DspsubLedger, int branchId,
            int subLedgerId = 0)
        {
            DataContext _context = new DataContext();
            var str = "EXEC SP_PartyLedgerSummary @StartDate='" + startDate + "', @EndDate='" + endDate +
                      "', @LedgerId=" + ledgerId + ",@AcGrpId=" + groupById + ",@GroupBy=" + groupBy + ",@DocAgent=" +
                      docAgent + ",@SubLedger=" + DspsubLedger + ",@SubLedgerId=" + subLedgerId;
            var report =
                _context.Database.SqlQuery<SP_PartyLedgersSummary>("EXEC SP_PartyLedgerSummary @StartDate='" + startDate +
                                                                   "', @EndDate='" + endDate + "', @LedgerId=" +
                                                                   ledgerId + ",@AcGrpId=" + groupById + ",@GroupBy=" +
                                                                   groupBy + ",@DocAgent=" + docAgent + ",@SubLedger=" +
                                                                   DspsubLedger + ",@SubLedgerId=" + subLedgerId +
                                                                   ",@BranchId=" + branchId).ToList();
            return report;
        }

        public static List<SP_Ledgers> GetLedgers(string startDate, string endDate, string catList, string Grouplist,
            string ledgerlist, int FYId, int groupBy, bool? docAgent, int branchId)
        {
            if (Grouplist == null)
                Grouplist = "";
            DataContext _context = new DataContext();
            var str = "EXEC SP_Ledgers @StartDate='" + startDate + "', @EndDate='" + endDate + "', @LedgerCategory='" +
                      catList + "', @Grouplist='" + Grouplist + "', @LedgerList='" + ledgerlist + "',@FYId='" + FYId +
                      "',@GroupBy=" + groupBy + ",@DocAgent=" + docAgent;
            var report =
                _context.Database.SqlQuery<SP_Ledgers>("EXEC SP_Ledgers @StartDate='" + startDate + "', @EndDate='" +
                                                       endDate + "', @LedgerCategory='" + catList + "',@Grouplist='" +
                                                       Grouplist + "', @LedgerList='" + ledgerlist + "',@FYId='" + FYId +
                                                       "',@GroupBy=" + groupBy + ",@DocAgent=" + docAgent +
                                                       ",@BranchId=" + branchId
                    ).ToList();
            return report;
        }

        public static decimal GetLedgerOpeningBalance(int ledgerId, int SubledgerId, string startDate, string endDate,
            int FYId, int branchId)
        {
            DataContext _context = new DataContext();
            var str = "EXEC SP_CalculateOpening @outputType=1, @ledgerId=" + ledgerId + ",@StartDate='" + startDate +
                      "',@EndDate='" + endDate + "',@FYId=" + FYId + ",@Subledger=0,@SubledgerId=" + SubledgerId +
                      ",@BranchId=" + branchId;
            var balance =
                _context.Database.SqlQuery<string>("SP_CalculateOpening @outputType=1, @ledgerId=" + ledgerId +
                                                   ",@StartDate='" + startDate + "',@EndDate='" + endDate + "',@FYId=" +
                                                   FYId + ",@Subledger=0,@SubledgerId=" + SubledgerId + ",@BranchId=" +
                                                   branchId).FirstOrDefault();
            return Convert.ToDecimal(balance);
        }

        public static decimal GetSubLedgerOpeningBalance(int ledgerId, int SubledgerId, string startDate, string endDate,
            int FYId, int branchId)
        {
            DataContext _context = new DataContext();
            var str = "EXEC SP_CalculateOpening @outputType=1, @ledgerId=" + ledgerId + ",@StartDate='" + startDate +
                      "',@EndDate='" + endDate + "',@FYId=" + FYId + ",@Subledger=1,@SubledgerId=" + SubledgerId +
                      ",@BranchId=" + branchId;
            var balance =
                _context.Database.SqlQuery<string>("SP_CalculateOpening @outputType=1, @ledgerId=" + ledgerId +
                                                   ",@StartDate='" + startDate + "',@EndDate='" + endDate + "',@FYId=" +
                                                   FYId + ",@Subledger=1,@SubledgerId=" + SubledgerId + ",@BranchId=" +
                                                   branchId).FirstOrDefault();
            return Convert.ToDecimal(balance);
        }

        public static List<SP_OutStanding> GetOutstandingCustomerLedgers(string AsOnDate, int ledgerId)
        {
            var report =
                _context.Database.SqlQuery<SP_OutStanding>("EXEC SP_OutstandingCustomer @AsOnDate='" + AsOnDate +
                                                           "',@Ledger_Id=" + ledgerId).ToList();
            return report;
        }

        public static List<SP_Ledgers> GetVenderLedgers(string AsOnDate, string category, string ledgerlist, int FYId)
        {
            var report =
                _context.Database.SqlQuery<SP_Ledgers>("EXEC SP_OutstandingSupplierLedgers @AsOnDate='" + AsOnDate +
                                                       "',@LedgerCategory='" + category + "', @LedgerList='" +
                                                       ledgerlist + "',@FYId=" + FYId).ToList();
            return report;
        }

        public static List<SP_Ledgers> GetCustomerLedgers(string AsOnDate, string category, string ledgerlist, int FYId)
        {
            var report =
                _context.Database.SqlQuery<SP_Ledgers>("EXEC SP_OutstandingCustomerLedgers @AsOnDate='" + AsOnDate +
                                                       "',@LedgerCategory='" + category + "', @LedgerList='" +
                                                       ledgerlist + "',@FYId=" + FYId).ToList();
            return report;
        }

        public static List<SP_OutStanding> GetOutstandingSupplierLedgers(string AsOnDate, int ledgerId)
        {
            var report =
                _context.Database.SqlQuery<SP_OutStanding>("EXEC SP_OutstandingSupplier @AsOnDate='" + AsOnDate +
                                                           "',@Ledger_Id=" + ledgerId).ToList();
            return report;
        }

        public static decimal GetOpeningBalance(string source, int ledgerId, int FYId)
        {
            var report =
                _context.Database.SqlQuery<decimal>("EXEC SP_OpeningBalance @Source='" + source + "',@ledgerId=" +
                                                    ledgerId + ",@FYid=" + FYId).FirstOrDefault();
            return report;
        }

        public static List<SP_CashBankBook> GetCashFlowSummary(string startDate, string endDate, int LedgerId, int FYId,
            int branchId)
        {
            DataContext _context = new DataContext();
            var str = "EXEC SP_CashFlowSummary @StartDate='" + startDate + "', @EndDate='" + endDate + "', @LedgerId='" +
                      LedgerId + "' ,@FYId=" + FYId + " , @BranchId=" + branchId + "";
            var report =
                _context.Database.SqlQuery<SP_CashBankBook>("EXEC SP_CashFlowSummary @StartDate='" + startDate +
                                                            "', @EndDate='" + endDate + "', @LedgerId='" + LedgerId +
                                                            "' ,@FYId=" + FYId + " , @BranchId=" + branchId + "")
                    .ToList();
            return report;
        }

        #region documentNumbering

        public static string GetDocumentNumbering(int id)
        {
            DataContext _datacontext = new DataContext();
            var documentNumbering = _datacontext.DocumentNumberingSchemes.FirstOrDefault(x => x.Id == id);
            var number = string.Empty;
            if (!string.IsNullOrEmpty(documentNumbering.DocPrefix))
                number = documentNumbering.DocPrefix;
            var currentNo = documentNumbering.DocCurrNo.ToString();
            if (documentNumbering.DocNumFill)
            {
                if (documentNumbering.DocCharFill != null)
                {
                    currentNo = currentNo.PadLeft(documentNumbering.DocBodyLen,
                        Convert.ToChar(documentNumbering.DocCharFill));
                }
            }
            number += currentNo;
            if (!string.IsNullOrEmpty(documentNumbering.DocSuffix))
                number += documentNumbering.DocSuffix;
            return number;
        }

        public static string GetStudentRegistrationNumbering()
        {
            DataContext _datacontext = new DataContext();
            var documentNumbering = _datacontext.RegistrationNumberings.FirstOrDefault();
            var number = string.Empty;
            if (!string.IsNullOrEmpty(documentNumbering.DocPrefix))
                number = documentNumbering.DocPrefix;
            var currentNo = documentNumbering.DocCurrNo.ToString();
            if (documentNumbering.DocNumFill)
            {
                if (documentNumbering.DocCharFill != null)
                {
                    currentNo = currentNo.PadLeft(documentNumbering.DocBodyLen,
                        Convert.ToChar(documentNumbering.DocCharFill));
                }
            }
            number += currentNo;
            if (!string.IsNullOrEmpty(documentNumbering.DocSuffix))
                number += documentNumbering.DocSuffix;
        
          
            return number;
        }

        public static string GetDocumentNumberingByModuleName(string name)
        {
            DataContext _datacontext = new DataContext();
            var documentNumbering = _datacontext.DocumentNumberingSchemes.FirstOrDefault(x => x.ModuleName == name);
            var number = string.Empty;
            if (documentNumbering != null)
            {
                if (!string.IsNullOrEmpty(documentNumbering.DocPrefix))
                    number = documentNumbering.DocPrefix;
                var currentNo = documentNumbering.DocCurrNo.ToString();
                if (documentNumbering.DocNumFill)
                {
                    if (documentNumbering.DocCharFill != null)
                    {
                        currentNo = currentNo.PadLeft(documentNumbering.DocBodyLen,
                            Convert.ToChar(documentNumbering.DocCharFill));
                    }
                }
                number += currentNo;
                if (!string.IsNullOrEmpty(documentNumbering.DocSuffix))
                    number += documentNumbering.DocSuffix;
            }
            return number;
        }

        public static DocumentNumberingScheme GetDocDetailByModuleNameBYType(string name, string type)
        {
            DataContext _datacontext = new DataContext();
            var documentNumbering =
                _datacontext.DocumentNumberingSchemes.FirstOrDefault(x => x.ModuleName == name);
            if (type != null)
            {
                documentNumbering =
                    _datacontext.DocumentNumberingSchemes.FirstOrDefault(x => x.ModuleName == name && x.DocType == type);
            }
            return documentNumbering;
        }

        public static DocumentNumberingScheme GetDocDetail(int id)
        {
            var documentNumbering = _context.DocumentNumberingSchemes.FirstOrDefault(x => x.Id == id);
            return documentNumbering;
        }

        #endregion

        #region Roll Numbering

        public static string GetRollNumber(int classId, int sectionId, int branchId, int AYId)
        {
            var _datacontext = new DataContext();
            var rollNumbering =
                _datacontext.RollNumberingSchemes.FirstOrDefault(
                    x =>
                        x.ClassId == classId && x.SectionId == sectionId && x.BranchId == branchId &&
                        x.AcademyYearId == AYId);
            var number = string.Empty;
            if (rollNumbering != null)
            {
                if (!string.IsNullOrEmpty(rollNumbering.Prefix))
                    number = rollNumbering.Prefix;
                var currentNo = rollNumbering.CurrNo.ToString();
                if (rollNumbering.NumFill)
                {
                    if (rollNumbering.CharFill != null)
                    {
                        currentNo = currentNo.PadLeft(rollNumbering.BodyLen,
                            Convert.ToChar(rollNumbering.CharFill));
                    }
                }
                number += currentNo;
                if (!string.IsNullOrEmpty(rollNumbering.Suffix))
                    number += rollNumbering.Suffix;

                rollNumbering.CurrNo = rollNumbering.CurrNo + 1;
                _datacontext.Entry(rollNumbering).State = EntityState.Modified;
                _datacontext.SaveChanges();
            }
            return number;
        }

        #endregion

        #region CashBankBook

        public static List<DateTime> GetDateList(string startDate, string endDate, int CashBookId, int FYId,
            int branchId)
        {
            DataContext _context = new DataContext();
            var test = "EXEC SP_CBTransactionDateForCashBook @StartDate='" + startDate + "', @EndDate='" + endDate +
                       "', @CashBookId='" + CashBookId + "', @FYId=" + FYId + ", @BranchId=" + branchId;
            var report =
                _context.Database.SqlQuery<DateTime>("EXEC SP_CBTransactionDateForCashBook @StartDate='" + startDate +
                                                     "', @EndDate='" + endDate + "', @CashBookId='" + CashBookId +
                                                     "', @FYId=" + FYId + ", @BranchId=" + branchId).ToList();
            return report;
        }

        public static SP_CashBankOpening GetCashBankOpeningBalance(string startDate, int cashbankId, int FYId,
            int branchId)
        {
            DataContext _context = new DataContext();
            var report =
                _context.Database.SqlQuery<SP_CashBankOpening>("EXEC SP_CBOpeningByCashBook @StartDate='" + startDate +
                                                               "', @CashBookId='" + cashbankId + "', @FYId=" + FYId +
                                                               ", @BranchId=" + branchId).FirstOrDefault();
            return report;
        }

        public static List<SP_CashBankBook> GetCashBankReportList(string AsOnDate, int LedgerId, int FYId, int branchId)
        {
            DataContext _context = new DataContext();
            //  var str = "EXEC SP_CBTransactionDetails @AsOnDate='" + AsOnDate + "',@CashBookId='" + LedgerId + "' ,@FYId=" + FYId + ", @BranchId=" + branchId;
            var report =
                _context.Database.SqlQuery<SP_CashBankBook>("EXEC SP_CBTransactionDetails @AsOnDate='" + AsOnDate +
                                                            "',@CashBookId='" + LedgerId + "' ,@FYId=" + FYId +
                                                            ", @BranchId=" + branchId).ToList();
            return report;
        }

        #endregion


        #region CashBankSummary

        public static List<SP_CashBankBookSummary> GetCashBankSummaries(int glCode, string startDate, string endDate,
            int FYId, int branchId)
        {
            DataContext _context = new DataContext();
            var report =
                _context.Database.SqlQuery<SP_CashBankBookSummary>("EXEC CashBankBookSummary @StartDate='" + startDate +
                                                                   "', @EndDate='" + endDate + "', @CashBookId=" +
                                                                   glCode + ",@FYId=" + FYId + ", @BranchId=" + branchId)
                    .ToList();
            return report;
        }

        #endregion

        public static ScStudentRegistrationDetail GetStudentRegistrationDetail(int studentId)
        {
            return _context.ScStudentRegistrationDetails.FirstOrDefault(x => x.StudentId == studentId);

        }

        public static List<DateTime> GetJournalVoucherDates(string startDate, string endDate, int FYId, int branchId)
        {
            DataContext _context = new DataContext();
            var jvDates =
                _context.Database.SqlQuery<DateTime>("SP_JVDatesBet @StartDate='" + startDate + "',@EndDate='" + endDate +
                                                     "',@FYId=" + FYId + ",@BranchId=" + branchId).ToList();
            return jvDates;
        }

        public static List<SP_JournalVourcherHeader> JournalVouchersByDate(string AsOnDate, int FYId, int details = 0)
        {
            DataContext _context = new DataContext();
            var report =
                _context.Database.SqlQuery<SP_JournalVourcherHeader>("EXEC SP_JournalVouchersByDate @AsOnDate='" +
                                                                     AsOnDate + "',@FYId=" + FYId + ",@Details=" +
                                                                     details).ToList();
            return report;
        }

        public static List<SP_JournalVoucherDetail> JournalVoucherDetailsById(string VNo, string Source)
        {
            DataContext _context = new DataContext();
            var report =
                _context.Database.SqlQuery<SP_JournalVoucherDetail>("EXEC SP_JournalVoucherDetail @VoucherNo='" + VNo +
                                                                    "',@Source='" + Source + "'").ToList();
            return report;
        }


        public static List<CheckBoxListInfo> ModuleCheckboxList()
        {
            var list = (from s in _context.DayBookModules
                select
                    new CheckBoxListInfo {DisplayText = s.Name, IsChecked = true, Value = s.ShortName}).ToList();
            return list;
        }

        public static List<CheckBoxListInfo> ModuleCheckboxListForCooperative()
        {
            var list = (from s in _context.DayBookModules.Where(a => a.Type == 2)
                select
                    new CheckBoxListInfo {DisplayText = s.Name, IsChecked = true, Value = s.ShortName}).ToList();
            return list;
        }

        public static SP_DayBookOpening GetDayBookOpeningBalance(string startDate, int FYId, int branchId)
        {
            DataContext _context = new DataContext();
            var report =
                _context.Database.SqlQuery<SP_DayBookOpening>("EXEC SP_DayBookOpening @StartDate='" + startDate +
                                                              "',@FYId=" + FYId + ",@BranchId=" + branchId)
                    .FirstOrDefault();
            return report;
        }

        public static List<DateTime> GetDayBookDates(string startDate, string endDate, int FYId, string sourceList,
            int branchId)
        {
            DataContext _context = new DataContext();
            var dates =
                _context.Database.SqlQuery<DateTime>("SP_DayBookDates @StartDate='" + startDate + "',@EndDate='" +
                                                     endDate + "',@FYId=" + FYId + ",@SourceList='" + sourceList +
                                                     "',@BranchId=" + branchId).ToList();
            return dates;
        }

        public static List<SP_DayBookHeader> GetDayBookHeaders(string asOnDate, int FYId, string sourceList,
            int branchId)
        {
            DataContext _context = new DataContext();
            // sourceList = string.Empty;
            var query = "EXEC SP_DayBookHeader @AsOnDate='" + asOnDate + "',@FYId=" + FYId + ",@SourceList='" +
                        sourceList + "'";
            var report =
                _context.Database.SqlQuery<SP_DayBookHeader>("SP_DayBookHeader @AsOnDate='" + asOnDate + "',@FYId=" +
                                                             FYId + ",@SourceList='" + sourceList + "',@BranchId=" +
                                                             branchId).ToList();
            return report;
        }

        public static List<SP_DayBookDetail> GetDayBookDetail(string voucherNo, string source, int branchId)
        {
            DataContext _context = new DataContext();
            var report =
                _context.Database.SqlQuery<SP_DayBookDetail>("EXEC SP_DayBookDetail @VoucherNo='" + voucherNo +
                                                             "',@Source='" + source + "',@BranchId=" + branchId)
                    .ToList();
            return report;
        }



        public static string GetDivision(int? id)
        {
            var division = string.Empty;
            if (id == 0)
            {
                return division;
            }
            var divisionid = _context.ScDivisions.FirstOrDefault(x => x.Id == id);
            if (divisionid != null)
            {
                division = divisionid.Description;
            }
            return division;
        }


        //Cooperative
        public static string GetAccountGroupById(int? id)
        {
            var account = string.Empty;
            if (id == 0)
            {
                return account;
            }
            var firstOrDefault = _context.AccountGroups.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault != null)
                account = firstOrDefault.Description;
            return account;
        }

        public static string GetCurrency(string id)
        {
            var currency = string.Empty;
            if (id == null)
                return string.Empty;
            DataContext _context = new DataContext();
            var intId = Convert.ToInt32(id);
            var firstOrDefault = _context.Currencies.FirstOrDefault(x => x.Id == intId);
            if (firstOrDefault != null)
            {
                currency = firstOrDefault.Code;

            }
            return currency;
        }

        public static string GetAccountSubGroupById(int? id)
        {
            var account = string.Empty;
            if (id == null || id == 0)
            {
                return account;
            }
            var accountSubGroup = _context.AccountSubGroups.FirstOrDefault(x => x.Id == id);
            if (accountSubGroup != null)
            {
                account = accountSubGroup.Description;
            }

            return account;
        }


        public static String GetLedgerNameById(int id)
        {
            var accountname = string.Empty;
            if (id == null || id == 0)
            {
                return accountname;
            }
            var ledger = _context.Ledgers.FirstOrDefault(x => x.Id == id);
            if (ledger != null)
            {
                accountname = ledger.AccountName;
            }
            return accountname;
        }

        public static String GetLedgerNameById(int? id)
        {
            var accountname = string.Empty;
            if (id == null || id == 0)
            {
                return accountname;
            }
            var ledger = _context.Ledgers.FirstOrDefault(x => x.Id == id);
            if (ledger != null)
            {
                accountname = ledger.AccountName;
            }
            return accountname;
        }

        public static String GetSubLedgerNameById(int? id)
        {
            var Description = string.Empty;
            if (id == null || id == 0)
            {
                return Description;
            }
            var firstOrDefault = _context.SubLedgers.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault != null)
                Description = firstOrDefault.Description;
            return Description;
        }

        public static String GetAgentNameById(int? id)
        {
            var Name = string.Empty;
            if (id == null || id == 0)
            {
                return Name;
            }
            var firstOrDefault = _context.Agents.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault != null)
                Name = firstOrDefault.Name;
            return Name;
        }

        public static String GetProductNameById(int id)
        {
            var Name = string.Empty;
            if (id == null || id == 0)
            {
                return Name;
            }
            var firstOrDefault = _context.Products.FirstOrDefault(x => x.Id == id);
            if (firstOrDefault != null)
                Name = firstOrDefault.Name;
            return Name;
        }

        public static string GetAreaById(int id)
        {
            var area = string.Empty;
            if (id == 0)
            {
                return area;
            }
            var firstOrDefault = _context.Areas.FirstOrDefault(a => a.Id == id);
            if (firstOrDefault != null)
                area = firstOrDefault.AreaName;
            return area;
        }

        public static string GetProductGroupById(int? id)
        {
            var productGroupName = string.Empty;
            if (id == null)
                return productGroupName;
            var product = _context.ProductGroups.Find(id);
            if (product != null)
                productGroupName = product.Description;
            return productGroupName;
        }

        public static string GetProductSubGroupById(int? id)
        {
            var productSubGroupName = string.Empty;
            if (id == null)
                return productSubGroupName;
            var product = _context.ProductSubGroups.Find(id);
            if (product != null)
                productSubGroupName = product.Description;
            return productSubGroupName;

        }

        public static string GetAreaById(int? id)
        {
            var Area = string.Empty;
            if (id == null)
                return Area;
            var Ar = _context.Areas.Find(id);
            return Ar != null ? Ar.AreaName : null;

        }

        public static SelectList UdfList(int id)
        {
            var list = _context.UDFEntryDetails.Where(x => x.UdfId == id).ToList();

            var list1 = new SelectList(list, "Id", "Value");
            return list1;
        }

        public static string GetUdfValueById(int id, int? referenceId)
        {
            if (referenceId == null)
            {
                referenceId = 0;
            }
            var value = string.Empty;
            var data = _context.UDFDatas.FirstOrDefault(x => x.UdfId == id && x.ReferenceId == referenceId);
            if (data != null)
            {
                value = data.Value;
            }
            return value;
        }

        public static string GetUserNameById(int? id)
        {
            if (id == null)
            {
                id = 0;
            }
            var value = string.Empty;
            var data = _context.Users.FirstOrDefault(x => x.Id == id);
            if (data != null)
            {
                value = data.Username;
            }
            return value;
        }






        public static DateTime GetStartEndDate(DateTime baseDate)
        {
            var today = baseDate;
            var yesterday = baseDate.AddDays(-1);
            var thisWeekStart = baseDate.AddDays(-(int) baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
            var lastWeekStart = thisWeekStart.AddDays(-7);
            var lastWeekEnd = thisWeekStart.AddSeconds(-1);
            var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
            var lastMonthStart = thisMonthStart.AddMonths(-1);
            var lastMonthEnd = thisMonthStart.AddSeconds(-1);
            return today;
        }

        public static string GetUserByUserName(string userName)
        {

            var con = new DataContext();
            var data = con.Users.FirstOrDefault(x => x.Username == userName);
            return data != null ? data.FullName : string.Empty;
        }

        public static User GetUser(string userName)
        {
            var con = new DataContext();
            var data = con.Users.FirstOrDefault(x => x.Username == userName);
            return data ?? new User();
        }

        public static ScStudentinfo GetStudentbyUserId(int userid)
        {
            var con = new DataContext();
            var data = con.ScStudentRegistrationDetails.FirstOrDefault(x => x.UserId == userid);
            if (data != null)
            {
                return data.Studentinfo ?? new ScStudentinfo();
            }
            return new ScStudentinfo();
        }

        public static ScEmployeeInfo GetTeacherbyUserId(int userid)
        {
            var con = new DataContext();
            var data = con.ScStaffMaster.FirstOrDefault(x => x.userId == userid);
            if (data != null)
            {
                return data ?? new ScEmployeeInfo();
            }
            return new ScEmployeeInfo();
        }

        public static string GetBranchNameByUserName(string username)
        {
            var data = new DataContext();
            var firstOrDefault = data.Users.FirstOrDefault(x => x.Username == username);
            if (firstOrDefault != null)
            {
                var branchId = firstOrDefault.LastAccessedBranch;
                var company = data.CompanyInfos.FirstOrDefault(x => x.Id == branchId);
                return company != null ? company.Name : null;
            }
            return null;
        }

        public static int GetLedgerByUserName(string username)
        {
            var _contextDB = new DataContext();
            var firstOrDefault = _contextDB.SystemControls.FirstOrDefault();
            if (firstOrDefault != null)
            {
                int ledgerId = firstOrDefault.CashBook;
                return ledgerId;
            }
            else
            {
                return 0;
            }
        }

        public static string GetThumbnailImageByUserName(string username)
        {
            DataContext _contextDB = new DataContext();
            var user = _contextDB.Users.FirstOrDefault(x => x.Username == username);
            var imageUrl = string.Empty;
            if (string.IsNullOrEmpty(user.ImageGuid))
            {
                imageUrl =
                    VirtualPathUtility.ToAbsolute(
                        "~/Content/social/assets/img/plugins/bootstrap-fileupload/no-image.png");
                return imageUrl;
            }
            var imageName = user.ImageGuid + "_th" + user.Ext;
            var imageFile = AppDomain.CurrentDomain.BaseDirectory + "Content\\UserImages\\" + imageName;
            imageUrl = VirtualPathUtility.ToAbsolute("~/Content/UserImages/" + imageName);
            FileInfo objFileInfoLogo = new FileInfo(imageFile);

            if (!objFileInfoLogo.Exists)
            {
                imageUrl = VirtualPathUtility.ToAbsolute("~/Content/UserImages/default.jpg");
            }
            return imageUrl;
        }

        public static string GetThumbnailImageByUserId(int userId)
        {
            DataContext _contextDB = new DataContext();
            var user = _contextDB.Users.FirstOrDefault(x => x.Id == userId);
            var imageUrl = string.Empty;
            if (string.IsNullOrEmpty(user.ImageGuid))
            {
                imageUrl =
                    VirtualPathUtility.ToAbsolute(
                        "~/Content/social/assets/img/plugins/bootstrap-fileupload/no-image.png");
                return imageUrl;
            }
            var imageName = user.ImageGuid + "_th" + user.Ext;
            var imageFile = AppDomain.CurrentDomain.BaseDirectory + "Content\\UserImages\\" + imageName;
            imageUrl = VirtualPathUtility.ToAbsolute("~/Content/UserImages/" + imageName);
            FileInfo objFileInfoLogo = new FileInfo(imageFile);

            if (!objFileInfoLogo.Exists)
            {
                imageUrl = VirtualPathUtility.ToAbsolute("~/Content/UserImages/default.jpg");
            }
            return imageUrl;
        }

        public static string GetFullImageByUserName(string username)
        {
            DataContext _contextDB = new DataContext();
            var user = _contextDB.Users.Where(x => x.Username == username).FirstOrDefault();

            var imageUrl = string.Empty;
            if (string.IsNullOrEmpty(user.ImageGuid))
            {
                imageUrl = VirtualPathUtility.ToAbsolute("~/Content/UserImages/default.jpg");
                return imageUrl;
            }
            var imageName = user.ImageGuid + user.Ext;
            var imageFile = AppDomain.CurrentDomain.BaseDirectory + "Content\\UserImages\\" + imageName;
            imageUrl = VirtualPathUtility.ToAbsolute("~/Content/UserImages/" + imageName);
            FileInfo objFileInfoLogo = new FileInfo(imageFile);

            if (!objFileInfoLogo.Exists)
            {
                imageUrl = VirtualPathUtility.ToAbsolute("~/Content/UserImages/default.jpg");
            }
            return imageUrl;
        }

        public static string IsLoanMatured(DateTime lastInstallmentDate)
        {
            if (DateTime.Now > lastInstallmentDate)
            {
                return "Yes";
            }
            return "No";
        }



        public static string GetBusById(int id)
        {
            var bus = _context.ScBus.Find(id);
            if (bus != null)
                return bus.Description;
            return null;
        }

        public static string GetRouteNameById(int id)
        {
            var scBusRouteDetails = _context.ScBusRouteDetailses.FirstOrDefault(x => x.Id == id);
            if (scBusRouteDetails != null)
            {
                var route = scBusRouteDetails.BusRouteDescription;
                return route;
            }
            return null;
        }

        public static string GetFiscalYearDetail(int id)
        {
            var scBusRouteDetails = _context.FiscalYears.FirstOrDefault(x => x.Id == id);
            if (scBusRouteDetails != null)
            {
                var route = string.Empty;
                route = Convert.ToString(scBusRouteDetails.StartDate.ToShortDateString()) + " - " +
                        Convert.ToString(scBusRouteDetails.EndDate.ToShortDateString());
                return route;
            }
            return null;
        }

        public static string GetRouteNameByStringId(string Id)
        {
            var intId = Convert.ToInt32(Id);
            var scBusRouteDetails = _context.ScBusRouteDetailses.Where(x => x.Id == intId).FirstOrDefault();
            if (scBusRouteDetails != null)
            {
                var route = scBusRouteDetails.BusRouteDescription;
                return route;
            }
            return null;
        }

        public static string GetRegistrationNumberByStudentId(int Id)
        {
            var regNo = _context.ScStudentinfos.Where(x => x.StudentID == Id).FirstOrDefault().Regno;
            return regNo;
        }



        public static string GetBusStops(string RouteDescription)
        {
            string busStop = string.Empty;
            var busstops = _context.ScBusRouteDetailses.Where(x => x.BusRouteDescription == RouteDescription);
            int i = 0;

            foreach (var scBusRouteDetailse in busstops)
            {

                if (i > 0)
                {
                    busStop += ", ";
                }
                busStop += _context.ScBusStops.Find(scBusRouteDetailse.BusStopId).Description;
                i++;
            }
            return busStop;
        }


        public static string GetStaffSubjects(int staffId)
        {
            string subjects = string.Empty;
            var subjectlist = _context.ScStaffSubjectMapping.Where(x => x.StaffId == staffId);
            int i = 0;

            foreach (var item in subjectlist)
            {

                if (i > 0)
                {
                    subjects += ", ";
                }
                subjects += _context.Subjects.Find(item.SubjectId).Description;
                i++;
            }
            return subjects;
        }

        public static string GetTemporaryFullAddress(ScStudentinfo model)
        {
            var tempAddress = string.Empty;
            if (model.TmpAdd != null)
            {
                tempAddress += model.TmpAdd;
            }
            if (model.TmpWardNo != null)
            {
                tempAddress += " - " + model.TmpWardNo;
            }
            if (model.TmpCity != null)
            {
                tempAddress += " , " + model.TmpCity;
            }
            if (model.TmpDistrict != null)
            {
                tempAddress += " , " + model.TmpDistrict;
            }
            if (model.TmpCountry != null)
            {
                tempAddress += " , " + model.TmpCountry;
            }
            return tempAddress;
        }


        public static string GetPermanentFullAddress(ScStudentinfo model)
        {
            var permanentAdd = string.Empty;
            if (model.PerAdd != null)
            {
                permanentAdd += model.PerAdd;
            }
            if (model.PerWardNo != null)
            {
                permanentAdd += " - " + model.PerWardNo;
            }
            if (model.PerCity != null)
            {
                permanentAdd += " , " + model.PerCity;
            }
            if (model.PerDistrict != null)
            {
                permanentAdd += " , " + model.PerDistrict;
            }
            if (model.PerCountry != null)
            {
                permanentAdd += " , " + model.PerCountry;
            }
            return permanentAdd;
        }

        public static bool IsMappedInDb(int staffid, int subjectid)
        {
            var data = _context.ScStaffSubjectMapping.Where(m => m.StaffId == staffid && m.SubjectId == subjectid);
            if (data.Count() != 0)
            {
                return true;
            }
            return false;
        }

        public static ScStudentinfo GetStudentInfoById(int id)
        {
            return _context.ScStudentinfos.FirstOrDefault(x => x.StudentID == id);

        }

        public static ScEmployeeInfo GetStaffMasterById(int id)
        {
            return _context.ScStaffMaster.FirstOrDefault(x => x.Id == id);

        }

        //public static int? GetBookQunaity(int id)
        //{
        //    var count = _context.ScBook.Find(id).CurrentQty;
        //    return count;
        //}
        public static List<ScStudentFeeTerm> GetStudentFeeTerm(int studentId, int FeeitemId, int AYId)
        {
            return _context.ScStudentFeeTerm.Where(x =>
                x.StudentFeeRateDetail.FeeRateMaster.StudentId == studentId &&
                x.StudentFeeRateDetail.FeeItemId == FeeitemId && x.StudentFeeRateMaster.AcademicYearId == AYId).ToList();

        }

        public static string GetParentName(int? id)
        {
            var data = _context.MenuItems.FirstOrDefault(x => x.Id == id);
            return data != null ? data.LinkText : null;
        }

        public static string GetImage(string filename, string extenstion)
        {

            var imageUrl = VirtualPathUtility.ToAbsolute("~/Content/academicsimage/" + filename + extenstion);
            return imageUrl;
        }

        public static string GetLogo(string filename, string extenstion)
        {

            var imageUrl = VirtualPathUtility.ToAbsolute("~/Content/Logo/" + filename + extenstion);
            return imageUrl;
        }

        public static ScExamMarkSetup GetExamMarkSetup(int examId, int classId, int subjectId)
        {
            return _context.ScExamMarkSetup.FirstOrDefault(
                x => x.ExamId == examId && x.ClassId == classId && x.SubjectId == subjectId);

        }

        public static ScStudentRegistrationDetail GetStudentDetial(int studentId, int AYID)
        {
            return
                _context.ScStudentRegistrationDetails.FirstOrDefault(
                    x => x.StudentId == studentId && x.StudentRegistration.AcademicYearId == AYID);

        }

        //public static ScStudentAttendance GetStudentAttendance(int classId, int sectionId, int studentId, DateTime date,
        //    int AYID)
        //{
        //    return
        //        _context.ScStudentAttendance.FirstOrDefault(
        //            x =>
        //                x.ClassId == classId && x.SectionId == sectionId && x.AcademicYearId == AYID &&
        //                x.StudentId == studentId &&
        //                x.Date == date);
        //}

        public static AttendanceLog GetStudentAttendance(int classId, int sectionId, int studentId, DateTime date,
            int AYID)
        {
            //return
            //    _context.ScStudentAttendance.FirstOrDefault(
            //        x =>
            //            x.ClassId == classId && x.SectionId == sectionId && x.AcademicYearId == AYID &&
            //            x.StudentId == studentId &&
            //            x.Date == date);
          //  var date1 = date.Date;
            var list = (from c in _context.AttendanceLogs

                        join sr in _context.ScStudentRegistrationDetails on c.EnrollId equals sr.Studentinfo.DeviceUserId
                        where
                          EntityFunctions.TruncateTime(c.TDate) == date.Date && sr.StudentRegistration.ClassId == classId && sr.SectionId == sectionId &&
                            sr.StudentId == studentId
                        select c).FirstOrDefault();
            return (from c in _context.AttendanceLogs
             
                join sr in _context.ScStudentRegistrationDetails on c.EnrollId equals sr.Studentinfo.DeviceUserId
                where
                  EntityFunctions.TruncateTime(c.TDate) == date.Date && sr.StudentRegistration.ClassId == classId && sr.SectionId == sectionId &&
                    sr.StudentId == studentId
                select c).FirstOrDefault();

        }
        public static ScStaffAttendance GetStaffAttendance(int staffId, DateTime date, int AYID)
        {
            return
                _context.ScStaffAttendance.FirstOrDefault(
                    x =>
                        x.StaffId == staffId && x.AcademicYearId == AYID &&
                        x.Date == date);
        }

        public static ScTeacherSchedule GetStaffIdClassScheduleDetailId(int classscheduleDetailId)
        {
            var staffId =
                _context.ScTeacherSchedule.First(x => x.ClassScheduleDetailId == classscheduleDetailId);
            return staffId;
        }

        public static string GetMarkSheetLedger(int classId, int examId, int AYID)
        {
            return
                _context.Database.SqlQuery<string>("EXEC SP_MarkLedger @ExamId=" + examId + ",@ClassId=" + classId +
                                                   ",@AcademicYearId=" + AYID).FirstOrDefault();
        }

        public static int GetClassByStudentId(int studentId, int AYID)
        {
            var data =
                _context.ScStudentRegistrationDetails.FirstOrDefault(
                    x => x.StudentId == studentId && x.StudentRegistration.AcademicYearId == AYID);
            return data != null ? data.StudentRegistration.ClassId : 0;
        }

        public static string GetClassNameByStudent(int studentId, int AYID)
        {
            var data =
                _context.ScStudentRegistrationDetails.FirstOrDefault(
                    x => x.StudentId == studentId && x.StudentRegistration.AcademicYearId == AYID);
            if (data != null)
                if (data.StudentRegistration != null)
                    if (data.StudentRegistration.Class != null)
                        return data.StudentRegistration.Class.Description;

            return null;
        }

        public static string FirstLetterToUpper(string text)
        {
            text = text.ToLower();

            string[] arr = text.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (arr.Length == 1)
            {
                text = text.Substring(0, 1).ToUpper() + text.Substring(1);
            }
            else if (arr.Length > 1)
            {
                StringBuilder objStringBuilder = new StringBuilder();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i == 0)
                    {
                        string temp = arr[i];
                        temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1);
                        objStringBuilder.Append(temp);
                        objStringBuilder.Append(" ");
                    }
                    else if (i == arr.Length - 1)
                    {
                        string temp = arr[i];
                        temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1);
                        objStringBuilder.Append(temp);
                    }
                    else
                    {
                        objStringBuilder.Append(arr[i]);
                        objStringBuilder.Append(" ");
                    }
                }
                text = objStringBuilder.ToString();
            }
            return text;
        }

        public static string GetNameByReferenceIdForSMSStaff(int id)
        {
            var name =
                _context.ScStaffMaster.First(x => x.Id == id).Description;
            return name;
        }

        public static string GetNameByReferenceIdForSMSStudent(int id)
        {
            var name =
                _context.ScStudentinfos.First(x => x.StudentID == id).StuDesc;
            return name;
        }

        public static bool CheckCorporateAllowance(int corporatid, int allowanceid)
        {
            var data =
                _context.PyCorporateAllowanceMappings.FirstOrDefault(
                    x => x.CorporateId == corporatid && x.AllowanceId == allowanceid);
            return data != null;
        }

        public static bool CheckEmployeeDeduction(int deductionId, int employeedeductionId)
        {
            var data =
                _context.PyEmployeeDeductionMappings.FirstOrDefault(
                    x => x.DeductionId == deductionId && x.EmployeeDeductionId == employeedeductionId);
            return data != null;
        }

        public static bool CheckEmployeeAllowance(int allowanceId, int employeedeductionId)
        {
            var data =
                _context.EmployeeSalaryAllowanceMappings.FirstOrDefault(
                    x => x.AllowanceId == allowanceId && x.EmployeeSalaryId == employeedeductionId);
            return data != null;
        }

        public static bool CheckPFEmployee(int emoployeeId, int pfemployeeId)
        {
            var data =
                _context.PfEmployeeMappings.FirstOrDefault(
                    x => x.EmployeeId == emoployeeId && x.PFEmployeeId == pfemployeeId);
            return data != null;
        }

        public static bool CheckTaxDeductionEmployee(int emoployeeId, int taxdeductionId)
        {
            var data =
                _context.PyTaxDeductionEmployeeMappings.FirstOrDefault(
                    x => x.EmployeeId == emoployeeId && x.TaxDeductionId == taxdeductionId);
            return data != null;
        }

        public static string ErrorMsg(Exception ex)
        {
            return ex.InnerException.InnerException.Message.Split(',')[0];
        }

        public static ScEmployeeInfo GetEmployeeInfo(int id)
        {
            return _context.ScStaffMaster.FirstOrDefault(x => x.Id == id);
        }

        public static List<PyPayrollGenerationDetail> GetExistPayrollBySalaryHeadAndYear(int headId, int year,
            int branchId, int datetype)
        {
            var list = new List<PyPayrollGenerationDetail>();
            var data = new List<PyPayrollGeneration>();
            if (datetype == 1)
            {
                data = _context.PyPayrollGeneration.Where(
                    x => x.SalaryHeadId == headId && x.Year == year && x.BranchId == branchId).ToList();
            }
            else
            {
                data = _context.PyPayrollGeneration.Where(
                    x => x.SalaryHeadId == headId && x.YearMiti == year && x.BranchId == branchId).ToList();
            }

            return data.Aggregate(list,
                (current, item) => current.Union(item.PyPayrollGenerationDetails.ToList()).ToList());
        }

        public static string GetStudentRollNoByStudentId(int studentId)
        {
            var data = _context.ScStudentRegistrationDetails.FirstOrDefault(x => x.StudentId == studentId);
            return data != null ? data.RollNo : "";
        }


        public static int GetUnitIdbyCode(string code)
        {
            var id = _context.Units.Where(x => x.Code == code).First().Id;
            return id;

        }

        public static decimal UnitConversion(int productId, decimal quantity, string currentunit, string convertToUnit)
        {
            var directConversiondata =
                _context.ProductUnitConversions.Where(
                    x =>
                        x.Unit.ToLower() == currentunit.ToLower() && x.LowestUnit.ToLower() == convertToUnit.ToLower() &&
                        x.ProductId == productId);
            decimal multiplier = 0;
            if (directConversiondata.Any())
            {
                multiplier = directConversiondata.First().LowestQuantity;
            }

            if (multiplier != 0)
            {
                multiplier = multiplier*quantity;
            }
            else
            {
                var datasConversion =
                    _context.ProductUnitConversions.Where(
                        x => x.Unit.ToLower() == currentunit.ToLower() && x.ProductId == productId);
                if (datasConversion.Any())
                {
                    var baseunit = datasConversion.First().LowestUnit;
                    var baseunitmatch = _context.ProductUnitConversions.Where(
                        x =>
                            x.LowestUnit.ToLower() == baseunit && x.Unit.ToLower() == convertToUnit.ToLower() &&
                            x.ProductId == productId);
                    if (baseunitmatch.Any())
                    {
                        multiplier = datasConversion.First().LowestQuantity;
                        multiplier = multiplier*quantity;

                        var divisor = baseunitmatch.First().LowestQuantity;

                        multiplier = multiplier/divisor;
                    }

                }
            }
            if (multiplier == 0)
            {
                return Math.Round(quantity, 2);
            }
            return Math.Round(multiplier, 2);

        }


        public static bool CheckEducationTax(int feeitemId)
        {
            var data = _context.ScFeeItems.FirstOrDefault(x => x.Id == feeitemId);
            return data != null && data.EducationTax;
        }

        public static void GenerateRank(int Examid, int ClassId, int AYID)
        {
            var _context = new DataContext();

            _context.Database.ExecuteSqlCommand("Sp_UpateRank @ExamId=" + Examid + ",@ClassId=" + ClassId +
                                                ",@AcademicYearId=" + AYID);

        }

        public static string GetProductUnitCode(int productid, string productUnit, string selectedUnit)
        {
            var data = UnitConversion(productid, 1, productUnit, selectedUnit);
            if (data != 1)
            {
                return selectedUnit;
            }
            return productUnit;
        }

        public static List<SP_EmployeeSalaryCreditSlipDetails> GeneratingEmployeeSalaryDetail(int EmployeeId,
            int MonthId, int Year, int AYear)
        {
            var _context = new DataContext();

            var list =
                _context.Database.SqlQuery<SP_EmployeeSalaryCreditSlipDetails>(
                    "SP_EmployeeSalaryCreditSlipDetails @EmployeeList='" + EmployeeId + "',@ACYID=" +
                    AYear +
                    ",@Month=" + MonthId + ",@Year=" + Year).ToList();
            return list;
        }

        public static List<string> GetSubjectByStudentId(string username)
        {
            var con = new DataContext();
            var user = GetUser(username);
            var studentinfo = GetStudentbyUserId(user.Id);
            return (from s in
                (from c in
                    (


                        from csm in con.ClassSubjectMappings.Where(x => x.SubjectType == 1)


                        where csm.ClassId == (
                            (
                                from n in
                                    (from rd in
                                        con.ScStudentRegistrationDetails.Where(
                                            x => x.StudentId == studentinfo.StudentID)


                                        select new {Class = rd.StudentRegistration.ClassId})
                                group n by new {i = n.Class}
                                into g
                                select g.Key.i).FirstOrDefault())
                        select new {Subjectid = csm.SubjectId}
                        ).Union
                        (

                            from ssm in con.StudentSubjectMappings.Where(x => x.StudentId == studentinfo.StudentID)

                            select new {Subjectid = ssm.SubjectId}
                        )

                    select new {subId = c.Subjectid})
                join sub in con.Subjects on s.subId equals sub.Id
                select sub.Description).ToList();
        }


        public static List<SelectModel> GetTeacherSubject(string username)
        {
            var con = new DataContext();
            var user = GetUser(username);
            var teacher = GetTeacherbyUserId(user.Id);
            //var list = (from n in
            //                (from ts in con.ScTeacherSchedule.Where(x => x.StaffId == teacher.Id)
            //                 join csd in con.ClassScheduleDetail on ts.ClassScheduleDetailId equals csd.Id
            //                 join cs in con.ScClassSchedule on csd.ClassScheduleId equals cs.Id
            //                 join s in con.Subjects on cs.SubjectId equals s.Id
            //                 select new { sid = cs.SubjectId, sname = s.Description })
            //            group n by new { SubjectId = n.sid, Name = n.sname }
            //                into g
            //                select new { Id = g.Key.SubjectId, Desc = g.Key.Name }).Select(x => new SelectModel
            //                                                                                       {
            //                                                                                           Description = x.Desc,
            //                                                                                           Id = x.Id
            //                                                                                       }).ToList();

            //return list;
            var list = con.ScStaffSubjectMapping.Where(x => x.StaffId == teacher.Id).Select(x => new SelectModel
            {
                Description = x.Subject.Description,
                Id = x.SubjectId
            }).ToList();
            return list;
        }

        public class SelectModel
        {
            public int Id
            {
                get;
                set;
            }

            public string Description { get; set; }
        }

        public class Classes
        {
            public int ClassId { get; set; }
            public string ClassName { get; set; }
            public int SectionId { get; set; }
            public string SectionName { get; set; }
        }

        public static List<ScClassTeacherMapping> GetTeacherClasses(string username)
        {
            var con = new DataContext();
            var user = GetUser(username);
            var teacher = GetTeacherbyUserId(user.Id);


            //var list = (from n in
            //                (from ts in con.ScTeacherSchedule.Where(x => x.StaffId == teacher.Id)
            //                 join csd in con.ClassScheduleDetail on ts.ClassScheduleDetailId equals csd.Id
            //                 join cs in con.ScClassSchedule on csd.ClassScheduleId equals cs.Id
            //                 join c in con.SchClass on cs.ClassId equals c.Id
            //                 join s in con.ScSection on cs.SectionId equals s.Id into tt
            //                 from dd in tt.DefaultIfEmpty()
            //                 select new { cid = cs.ClassId, cname = c.Description, sid = cs.SectionId, sname = dd.Description })
            //            group n by new { ClassId = n.cid, Name = n.cname, SectionId = n.sid, secName = n.sname }
            //                into g
            //                select new { Id = g.Key.ClassId, Desc = g.Key.Name, SecId = g.Key.SectionId, SecName = g.Key.secName }).Select(x => new Classes
            //                   {
            //                       ClassName = x.Desc,
            //                       ClassId = x.Id,
            //                       SectionId = x.SecId,
            //                       SectionName = x.SecName
            //                   }).ToList();

            //     var list=       (from n in(from ts in con.ScTeacherSchedule.Where(x=>x.StaffId ==6)
            //join csd in  con.ClassScheduleDetail on ts.ClassScheduleDetailId equals csd.Id
            //join cs in con.ScClassSchedule on csd.ClassScheduleId equals cs.Id
            //join c in con.SchClass on cs.ClassId equals c.Id
            //join s in con.ScSection on cs.SectionId equals s.Id into tt
            //from dd in tt.DefaultIfEmpty()
            //select new {cid = cs.ClassId, cname= c.Description ,sid=cs.SectionId, sname = dd.Description})
            //group n by new {ClassId = n.cid, Name = n.cname,SectionId =n.sid, secName = n.sname}
            //into g
            //select new {Id = g.Key.ClassId, Desc= g.Key.Name,SecId=g.Key.SectionId, SecName = g.Key.secName}).Select(x => new Classes
            //                               {
            //                                   ClassName = x.Desc,
            //                                   ClassId = x.Id,
            //                                   SectionId = x.SecId,
            //                                   SectionName = x.SecName
            //                               }).ToList();
            //            return list;

            //var liss- from n in con.ScClassTeacherMappings.Where(x=>x.TeacherId==teacher.Id) 

            //var list = (from c in con.ScTeacherSchedule.Where(x => x.StaffId == teacher.Id).ToList()

            //           group c by new
            //                          {
            //                              c.ClassScheduleDetail.ClassSchedule.ClassId,
            //                              c.ClassScheduleDetail.ClassSchedule.SectionId
            //                          }
            //           into g
            //           select g.ToList()).Select(x => new Classes
            //                                                                          {
            //                                                                              ClassId =
            //                                                                                  x.FirstOrDefault().ClassScheduleDetail.
            //                                                                                  ClassSchedule.ClassId,
            //                                                                              ClassName =
            //                                                                                  x.FirstOrDefault().ClassScheduleDetail.
            //                                                                                  ClassSchedule.Class.
            //                                                                                  Description,
            //                                                                              SectionId =
            //                                                                                  x.FirstOrDefault().ClassScheduleDetail.
            //                                                                                  ClassSchedule.SectionId,
            //                                                                              SectionName =
            //                                                                                  (x.FirstOrDefault().ClassScheduleDetail.
            //                                                                                       ClassSchedule.
            //                                                                                       SectionId != 0)
            //                                                                                      ? x.FirstOrDefault().
            //                                                                                            ClassScheduleDetail
            //                                                                                            .ClassSchedule.
            //                                                                                            Section.
            //                                                                                            Description
            //                                                                                      : null
            //                                                                          }).ToList();
            var list = (from n in con.ClassTeacherMappings.Where(x => x.TeacherId == teacher.Id)
                select n).ToList();
            return list;
        }

        public static List<SelectListItem> GetExamist()
        {
            var context = new DataContext();

            var list =
                context.ScExam.OrderBy(x => x.Schedule)
                    .Select(x => new {x.Id, x.Description})
                    .AsEnumerable()
                    .Select(x => new SelectListItem()
                    {
                        Text = x.Description,
                        Value = x.Id.ToString()
                    }).ToList();
            if (context.ScExam != null)

                return list;
            return new List<SelectListItem>();
        }

        public static int GetStudentId(string username)
        {
            var con = new DataContext();
            var user = GetUser(username);
            return GetStudentbyUserId(user.Id).StudentID;

        }

        public static decimal GetLibraryFine(DateTime dueDate)
        {
            var con = new DataContext();
            var currentday = DateTime.Now;

            decimal fineamount = 0;

            ScLibraryLateFine fine;
            var day = currentday - dueDate;
            var diffday = day.TotalDays;
            var max = _context.ScLibraryLateFine.Max(x => x.DayStart);
            if (diffday >= max)
            {
                fine = (ScLibraryLateFine) _context.ScLibraryLateFine.FirstOrDefault(x => x.DayStart == max);

            }
            else
            {
                fine =
                    (ScLibraryLateFine)
                        _context.ScLibraryLateFine.FirstOrDefault(x => diffday >= x.DayStart && diffday <= x.DayEnd);
            }


            if (fine != null)
            {
                fineamount = fine.Amount;
            }
            return fineamount;
        }

        public static string GetMonthName(int month, bool abbrev)
        {
            DateTime date = new DateTime(1900, month, 1);
            if (abbrev) return date.ToString("MMM");
            return date.ToString("MMMM");
        }

        public static int GetClassId(string username)
        {
            var classId = 0;
            var student = _context.ScStudentRegistrationDetails.Where(x => x.User.Username == username).ToList();
            var scStudentRegistrationDetail = student.FirstOrDefault();
            if (scStudentRegistrationDetail != null)
            {
                classId = scStudentRegistrationDetail.Studentinfo.ClassId;
            }
            return classId;
        }

        public static int GetBookCount(string bookname, int BookId)
        {
            var book = _context.ScBookReceivedDetails.Count(x => x.Status == 1 && x.BookReceived.BookId == BookId);
            return book;
        }

        public static List<SP_LedgerListByCategory> GetLedgerListByCategory(string LedgerCategory, string LedgerList,
            int GroupBy, int FYId)
        {
            DataContext _context = new DataContext();
            var str = "EXEC SP_LedgerListByCategory @LedgerCategory='" + LedgerCategory + "', @LedgerList='" +
                      LedgerList + "', @GroupBy='" + GroupBy + "',@FYId=" + FYId;
            var report =
                _context.Database.SqlQuery<SP_LedgerListByCategory>("EXEC SP_LedgerListByCategory @LedgerCategory='" +
                                                                    LedgerCategory + "', @LedgerList='" + LedgerList +
                                                                    "', @GroupBy='" + GroupBy + "',@FYId=" + FYId)
                    .ToList();
            return report;
        }

        public static string GetPorductUnitCode(int productid, string productUnit, string selectedUnit)
        {
            var data = UnitConversion(productid, 1, productUnit, selectedUnit);
            if (data != 1)
            {
                return selectedUnit;
            }
            return productUnit;
        }

        public static IEnumerable<SelectListItem> Months
        {
            get
            {
                return DateTimeFormatInfo
                    .InvariantInfo
                    .MonthNames
                    .Select((monthName, index) => new SelectListItem
                    {
                        Value = (index + 1).ToString(),
                        Text = monthName
                    });
            }
        }

        public static List<NotificationDashboard> GetMessageCount(string username)
        {
            var count = 0;
            var context = new DataContext();
            var user = GetUser(username);
            //UtilityService.GetStudentbyUserId(x.SenderId).StuDesc + " (S)" == " (S)"
            //        ? UtilityService.GetTeacherbyUserId(x.SenderId).Description + "(T)"
            //        : UtilityService.GetStudentbyUserId(x.SenderId).StuDesc + "(S)"
            var list = (from n in context.ScMessages
                where n.ReceiverId == user.Id && !n.IsRead

                select n).ToList()

                .Select(x => new NotificationDashboard
                {
                    Id = x.Id,
                    IsRead = x.IsRead,
                    MsgBody = x.MsgBody,
                    MsgDate = x.MsgDate,
                    MsgSubject = x.MsgSubject,
                    SenderId = x.SenderId,
                    MsgTime = x.MsgTime,

                    ReceiverId = x.ReceiverId,
                    StudentName = UtilityService.GetUserNameByUserId(x.SenderId)

                }).OrderBy(x => x.MsgDate).ToList();

            //var final = (from n in list
            //             let studentName = UtilityService.GetStudentbyUserId(n.SenderId).StuDesc + " (S)"
            //             select new NotificationDashboard
            //                        {
            //                            Id = n.Id,
            //                            IsRead = n.IsRead,
            //                            MsgBody = n.MsgSubject,
            //                            MsgDate = n.MsgDate,
            //                            MsgSubject = n.MsgSubject,
            //                            SenderId = n.SenderId,
            //                            MsgTime = n.MsgTime,

            //                            ReceiverId = n.ReceiverId,
            //                            StudentName =
            //               studentName == " (S)" ?
            //                    studentName
            //                     : UtilityService.GetTeacherbyUserId(n.SenderId).Description + " (T)"

            //                        }).OrderBy(x => x.MsgDate).ToList();


            return list;

        }

        public static string GetUserNameByUserId(int userId)
        {
            var context = new DataContext();
            var list = (from t in context.ScStaffMaster
                select new
                {
                    UserId = t.userId,
                    UserName = t.Description + " (T)"
                }).Concat(from s in context.ScStudentRegistrationDetails
                    select new
                    {
                        UserId = s.UserId,
                        UserName = s.Studentinfo.StuDesc + " (S)"
                    }).Where(x => x.UserId == userId).Select(x => x.UserName).FirstOrDefault();
            return list;

        }

        public static string GetFeeItemName(int feeItemId)
        {
            var context = new DataContext();
            var str = "";
            var data = context.ScFeeItems.Where(x => x.Id == feeItemId);
            if (data.Any())
            {
                str = data.FirstOrDefault().Description;

            }
            return str;

        }

        //public static List<SP_Ledgers> GetLedgers(string startDate, string endDate, string catList, string ledgerlist, int FYId, int groupBy, bool? docAgent)
        //{
        //    DataContext _context = new DataContext();
        //    var str = "EXEC SP_Ledgers @StartDate='" + startDate + "', @EndDate='" + endDate + "', @LedgerCategory='" + catList + "', @LedgerList='" + ledgerlist + "',@FYId='" + FYId + "',@GroupBy=" + groupBy + ",@DocAgent=" + docAgent;
        //    var report = _context.Database.SqlQuery<SP_Ledgers>("EXEC SP_Ledgers @StartDate='" + startDate + "', @EndDate='" + endDate + "', @LedgerCategory='" + catList + "', @LedgerList='" + ledgerlist + "',@FYId='" + FYId + "',@GroupBy=" + groupBy + ",@DocAgent=" + docAgent).ToList();
        //    return report;
        //}
        //public static List<string> GetPrinter()
        //{

        //    //System.Management.ObjectQuery oquery =
        //    //    new System.Management.ObjectQuery("SELECT * FROM Win32_Printer");

        //    //System.Management.ManagementObjectSearcher mosearcher =
        //    //    new System.Management.ManagementObjectSearcher(oquery);

        //    //System.Management.ManagementObjectCollection moc = mosearcher.Get();

        //    //foreach (ManagementObject mo in moc)
        //    //{
        //    //    System.Management.PropertyDataCollection pdc = mo.Properties;
        //    //    foreach (System.Management.PropertyData pd in pdc)
        //    //    {
        //    //        if ((bool)mo["Network"])
        //    //        {
        //    //          var a = mo[pd.Name];
        //    //        }
        //    //    }
        //    //}

        //   // return System.Drawing.Printing.PrinterSettings.InstalledPrinters.Cast<string>().ToList();
        // }
        public static SelectList DropdownlistByProductId(int? productId, int unitid)
        {
            DataContext _context = new DataContext();
            int productid = Convert.ToInt32(productId);
            var unitlist = _context.ProductUnitConversions.Where(x => x.ProductId == productid).Select(x => x.Unit);
            if (unitlist.Any())
            {
                var list = new SelectList(_context.Units.Where(x => unitlist.Contains(x.Code)), "Id", "Code", unitid);
                return list;
            }
            var newlist = new SelectList(_context.Units.Where(x => x.Id == unitid), "Id", "Code", unitid);
            return newlist;
        }

        public static SelectList DropdownlistByProductId(int? productId, string unitcode)
        {
            DataContext _context = new DataContext();
            int productid = Convert.ToInt32(productId);
            var unitlist = _context.ProductUnitConversions.Where(x => x.ProductId == productid).Select(x => x.Unit);
            if (unitlist.Any())
            {
                var list = new SelectList(_context.Units.Where(x => unitlist.Contains(x.Code)), "Code", "Code", unitcode);
                return list;
            }
            var newlist = new SelectList(_context.Units.Where(x => x.Code == unitcode), "Code", "Code", unitcode);
            return newlist;
        }

        public static string GetGodownName(int? id)
        {
            if (id == null)
                return string.Empty;
            DataContext _context = new DataContext();
            var godown = _context.Godowns.Where(x => x.Id == (int) id).FirstOrDefault();
            if (godown != null)
                return godown.Name;
            return string.Empty;
        }

       
    }

}

