using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data
{
    public class DataContext : DbContext
    {
        public static string DatabaseName=string.Empty;
        //public DataContext()
        //{
        //    string baseConnectionString =
        //    "Data Source=" +
        //        // ".\\sqlexpress"
        //    ConfigurationManager.AppSettings["DataSource"]
        //    + ";" +
        //    "Database=" + DatabaseName + ";" +
        //    "User ID=" + ConfigurationManager.AppSettings["UserID"] + ";" +
        //    "Password=" + ConfigurationManager.AppSettings["Password"] + ";" +
        //    "MultipleActiveResultSets=True";
        //    Database.Connection.ConnectionString = baseConnectionString;
        //    Database.DefaultConnectionFactory = new SqlConnectionFactory(baseConnectionString);
        //    //this.Configuration.LazyLoadingEnabled = false;
        //}
        public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<AccountSubGroup> AccountSubGroups { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<BillingTerm> BillingTerms { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<Godown> Godowns { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<Narration> Narrations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductSubGroup> ProductSubGroups { get; set; }
        public DbSet<ProductUnitConversion> ProductUnitConversions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SubLedger> SubLedgers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CashBankMaster> CashBankMasters { get; set; }
        public DbSet<CashBankDetail> CashBankDetails { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        public DbSet<JournalVoucher> JournalVouchers { get; set; }
        public DbSet<JournalVoucherDetail> JournalVoucherDetails { get; set; }
        public DbSet<CreditNoteMaster> CreditNoteMasters { get; set; }
        public DbSet<CreditNoteDetail> CreditNoteDetails { get; set; }
        public DbSet<DebitNoteMaster> DebitNoteMasters { get; set; }
        public DbSet<DebitNoteDetail> DebitNoteDetails { get; set; }
        public DbSet<DocumentNumberingScheme> DocumentNumberingSchemes { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<SystemControl> SystemControls { get; set; }
        public DbSet<PurchaseImpTransDoc> PurchaseImpTransDocs { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<PurchaseReturnMaster> PurchaseReturnMasters { get; set; }
        public DbSet<PurchaseReturnDetail> PurchaseReturnDetails { get; set; }
        public DbSet<PurchaseReturnImpTransDoc> PurchaseReturnImpTransDocs { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }
        public DbSet<SalesInvoiceOtherDetail> SalesInvoiceOtherDetails { get; set; }
        public DbSet<SalesReturnMaster> SalesReturnMasters { get; set; }
        public DbSet<SalesReturnDetail> SalesReturnDetails { get; set; }
        public DbSet<SalesReturnOtherDetail> SalesReturnImpTransDocs { get; set; }
        public DbSet<UDFEntry> UdfEntries { get; set; }
        public DbSet<UDFEntryDetail> UDFEntryDetails { get; set; }
        public DbSet<UDFData> UDFDatas { get; set; }
        public DbSet<ModuleList> ModuleLists { get; set; }
        public DbSet<SecurityRight> SecurityRights { get; set; }
        public DbSet<DayBookModule> DayBookModules { get; set; }
        public DbSet<OpeningLedger> OpeningLedgers { get; set; }
        public DbSet<EntryControlPL> EntryControlPls { get; set; }
        public DbSet<EntryControlPurchase> EntryControlPurchases { get; set; }
        public DbSet<EntryControlSales> EntryControlSaleses { get; set; }
        public DbSet<EntryControlInventory> EntryControlInventories { get; set; }
        public DbSet<BillingTermDetail> BillingTermDetails { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<ProductOpening> ProductOpening { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<MaterialIssue> MaterialIssue { get; set; }
        public DbSet<MaterialIssueDetail> MaterialIssueDetail { get; set; }
        public DbSet<MaterialReturn> MaterialReturn { get; set; }
        public DbSet<MaterialReturnDetail> MaterialReturnDetail { get; set; }
        public DbSet<FinishedGoodReceive> FinishedGoodReceive { get; set; }
        public DbSet<FinishedGoodReceiveDetail> FinishedGoodReceiveDetail { get; set; }
        public DbSet<FinishedGoodReturn> FinishedGoodReturn { get; set; }
        public DbSet<FinishedGoodReturnDetail> FinishedGoodReturnDetail { get; set; }
        public DbSet<BillOfMaterial> BillOfMaterials { get; set; }
        public DbSet<BillOfMaterialDetail> BillOfMaterialDetails { get; set; }
        public DbSet<StockTransferMaster> StockTransferMasters { get; set; }
        public DbSet<StockTransferDetail> StockTransferDetails { get; set; }
        public DbSet<ShortNameDetail> ShortNameDetails { get; set; }
        public DbSet<PurchaseOrderMaster> PurchaseOrderMasters { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<PurchaseOrderImpTransDoc> PurchaseOrderImpTransDocs { get; set; }
        public DbSet<StockAdjustmentMaster> StockAdjustment { get; set; }
        public DbSet<StockAdjustmentDetail> StockAdjustmentDetails { get; set; }
        public DbSet<ExpiryBreakageDetail> ExpiryBreakageDetails { get; set; }
        public DbSet<ExpiryBreakageMaster> ExpiryBreakageMasters { get; set; }

        public DbSet<PurchaseChallanMaster> PurchaseChallanMasters { get; set; }
        public DbSet<PurchaseChallanDetail> PurchaseChallanDetails { get; set; }
        public DbSet<PurchaseChallanImpTransDoc> PurchaseChallanImpTransDocs { get; set; }
        public DbSet<SalesOrderMaster> SalesOrderMasters { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<SalesOrderOtherDetail> SalesOrderOtherDetails { get; set; }
        public DbSet<SalesOrderAddress> SalesOrderAddresses { get; set; }

        public DbSet<SalesChallanMaster> SalesChallanMasters { get; set; }
        public DbSet<SalesChallanImpTransDoc> SalesChallanImpTransDocs { get; set; }
        public DbSet<SalesChallanDetail> SalesChallanDetails { get; set; }
        public DbSet<SalesChallanBillingAddress> SalesChallanBillingAddresses { get; set; }
        public DbSet<Scheme> Scheme { get; set; }
        public DbSet<SchemeFreeProduct> SchemeFreeProduct { get; set; }
        public DbSet<SchemeProduct> SchemeProduct { get; set; }
        public DbSet<PurchaseProductBatch> PurchaseProductBatches { get; set; }
        public DbSet<AccountWatchList> AccountWatchLists { get; set; } 
        //  School
        public DbSet<AcademicYear> AcademicYears { get; set; } 
        public DbSet<ScEmployeeInfo> ScStaffMaster { get; set; }
        public DbSet<ScEmployeeCategory> ScStaffGroup { get; set; }
        public DbSet<SchBuilding> SchBuildings { get; set; }
        public DbSet<SchClass> SchClass { get; set; }
        public DbSet<ScStudentinfo> ScStudentinfos { get; set; }
        public DbSet<ScNationality> ScNationality { get; set; }
        public DbSet<ScReligion> ScReligion { get; set; }
        public DbSet<ScStudentCategory> ScStudentCategories { get; set; }
        public DbSet<ScSection> ScSection { get; set; }
        public DbSet<ScShift> ScShift { get; set; }
        public DbSet<ScBus> ScBus { get; set; }
        public DbSet<ScSubject> Subjects { get; set; }
        public DbSet<ScClassSubjectMapping> ClassSubjectMappings { get; set; }
        public DbSet<ScBusStop> ScBusStops { get; set; }
        public DbSet<ScBusRouteDetails> ScBusRouteDetailses { get; set; }
        public DbSet<ScTransportMapping> ScTransportMappings { get; set; }
        public DbSet<ScStudentSubjectMapping> StudentSubjectMappings { get; set; }
        public DbSet<ScDivision> ScDivisions { get; set; }
        public DbSet<ScExtraActivity> ScExtraActivity { get; set; }
        public DbSet<ScGrade> ScGrade { get; set; }
        public DbSet<ScExam> ScExam { get; set; }
        public DbSet<ScExamRoutine> ScExamRoutine { get; set; }
        public DbSet<ScExamMarkSetup> ScExamMarkSetup { get; set; }
        public DbSet<ScFeeItem> ScFeeItems { get; set; }
        public DbSet<ScFeeTerm> ScFeeTerm { get; set; }
        public DbSet<ScBoader> ScBoader { get; set; }
        public DbSet<ScBoaderMapping> ScBoaderMappings { get; set; }
        public DbSet<ScClassFeeRate> ScClassFeeRate { get; set; }
        public DbSet<ScStudentFeeRateMaster> ScStudentFeeRateMasters { get; set; }
        public DbSet<ScStudentFeeRateDetail> ScStudentFeeRateDetail { get; set; }
        public DbSet<ScStudentFeeTerm> ScStudentFeeTerm { get; set; }
        public DbSet<ScHouseGroup> ScHouseGroup { get; set; }
        public DbSet<ScHouseMapping> ScHouseMapping { get; set; }
        public DbSet<ScFineScheme> FineSchemes { get; set; }
        public DbSet<ScFineSchemeDetails> SchemeDetails { get; set; }
        public DbSet<ScPrePaidScheme> PrePaidSchemes { get; set; }
        public DbSet<ScPrePaidSchemeDetails> PrePaidSchemeDetails { get; set; }
        public DbSet<ScStudentRegistration> ScStudentRegistrations { get; set; }
        public DbSet<ScStudentRegistrationDetail> ScStudentRegistrationDetails { get; set; }
        public DbSet<ScStudentExtraActivity> ScStudentExtraActivities { get; set; }
        public DbSet<ScExamAttendanceMaster> ScExamAttendanceMaster { get; set; }
        public DbSet<ScExamAttendanceDetail> ScExamAttendanceDetail { get; set; }
        public DbSet<ScStudentExtraActivityDetails> ScStudentExtraActivityDetailses { get; set; }
        public DbSet<ScExamMarksEntry> ScExamMarksEntry { get; set; }
        public DbSet<ScRoom> ScRoom { get; set; }
        public DbSet<SchBuildingRoomMapping> SchBuildingRoomMapping { get; set; }
        public DbSet<ScClassRoomMapping> ScClassRoomMapping { get; set; }
        public DbSet<ScAbsentApplication> ScAbsentApplication { get; set; }
        public DbSet<ScClassSchedule> ScClassSchedule { get; set; }
        public DbSet<ScSalaryHead> ScSalaryHead { get; set; }
        public DbSet<ScStaffSubjectMapping> ScStaffSubjectMapping { get; set; }
        public DbSet<ScConsolidatedMarksSetup> ScConsolidatedMarksSetup { get; set; }
        public DbSet<ScProgramMaster> ScProgramMaster { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<ScClassScheduleDetail> ClassScheduleDetail { get; set; }
        public DbSet<ScTeacherSchedule> ScTeacherSchedule { get; set; }
        public DbSet<ScStaffAttendance> ScStaffAttendance { get; set; }
        public DbSet<ScRollNumberingScheme> RollNumberingSchemes { get; set; }
        public DbSet<StudentSlcInfo> StudentSlcInfos { get; set; }
        public DbSet<ScClassTeacherMapping> ClassTeacherMappings { get; set; }
        public DbSet<ScAssignment> Assignments { get; set; }
        public DbSet<ScStudentRegistrationNumbering> RegistrationNumberings { get; set; }
        public DbSet<Design> Designs { get; set; }
        //Transaction 
        public DbSet<ScMonthlyBill> MonthlyBills { get; set; }
        public DbSet<ScMonthlyBillStudent> MonthlyBillDetails { get; set; }
        public DbSet<ScBillTransaction> ScBillTransaction { get; set; }
        public DbSet<ScFeeReceipt> ScFeeReceipt { get; set; }
        //Library
        public DbSet<ScBook> ScBook { get; set; }
        public DbSet<ScBookReceived> ScBookReceived { get; set; }
        public DbSet<ScBookReceivedDetails> ScBookReceivedDetails { get; set; }
        public DbSet<ScLibraryMemberRegistration> ScLibrarymemberRegistration { get; set; }
        public DbSet<ScBookIssued> ScBookIssued { get; set; }
        public DbSet<ScBookIssueReturn> ScBookIssueReturn { get; set; }
        public DbSet<ScMaterialIssueMaster> MaterialIssue_Master { get; set; }
        public DbSet<ScMaterialIssueDetails> MaterialIssue_Details { get; set; }
        public DbSet<ScMaterialReturnMaster> ScMaterialReturnMaster { get; set; }
        public DbSet<ScMaterialReturnDetails> ScMaterialReturnDetails { get; set; }
        public DbSet<ScStudentAttendance> ScStudentAttendance { get; set; }
        public DbSet<ScEmployeeDepartment> ScEmployeeDepartment { get; set; }
        public DbSet<ScEmployeePost> ScEmployeePost { get; set; }
        public DbSet<ScLibraryLateFine> ScLibraryLateFine { get; set; }
        public DbSet<ScLibraryCard> ScLibraryCards { get; set; }
       
        //SMS
        public DbSet<SmsGroup> SmsGroup { get; set; }
        public DbSet<SmsGroupDetail> SmsGroupDetail { get; set; }
        public DbSet<SmsLog> SmsLog { get; set; }
        public DbSet<SmsTemplates> SmsTemplates { get; set; }
        public DbSet<SMSSettings> SMSSettings { get; set; }
        


        //payroll 
        public DbSet<PyAllowanceMaster> PyAllowanceMaster { get; set; }
        public DbSet<PyCorporateSalaryMaster> PyCorporateSalaryMaster { get; set; }
        public DbSet<PyCorporateAllowanceMapping> PyCorporateAllowanceMappings { get; set; } 
        public DbSet<PyDeductionMaster> PyDeductionMaster { get; set; }
        public DbSet<PyEmployeeDeductionMaster> PyEmployeeDeductionMaster { get; set; }
        public DbSet<PyEmployeeDeductionMapping>  PyEmployeeDeductionMappings { get; set; }
        public DbSet<PyEmployeeSalaryAllowanceMapping> EmployeeSalaryAllowanceMappings { get; set; }
        public DbSet<PyEmployeeSalaryMaster> PyEmployeeSalaryMaster { get; set; }
        public DbSet<PyPFEmployeeMaster> PyPFEmployeeMaster { get; set; }
        public DbSet<PyPFEmployeeMapping> PfEmployeeMappings { get; set; }
        public DbSet<PyTaxDeductionPattern> PyTaxDeductionPattern { get; set; }
        public DbSet<PyTaxDeductionEmployeeMapping> PyTaxDeductionEmployeeMappings { get; set; }
        public DbSet<PyPayrollGeneration> PyPayrollGeneration { get; set; }
        public DbSet<PyPayrollGenerationDetail> PyPayrollGenerationDetail { get; set; }
        public DbSet<PyPaymentSlip> PyPaymentSlip { get; set; }
        public DbSet<OpeningStudent> OpeningStudent { get; set; }
        public DbSet<Consolidate> Consolidates { get; set; }

        public DbSet<EmployeeTransaction> EmployeeTransactions { get; set; }
        public DbSet<ConsolidateDetail> ConsolidateDetails { get; set; } 

        public DbSet<ScLibrarySetting> ScLibrarySettings { get; set; }
        public DbSet<StudentDocument> StudentDocuments { get; set; }
        public DbSet<ScMessage> ScMessages { get; set; }
        public DbSet<ScCalendarEvent> ScCalendarEvents { get; set; }

        public DbSet<AttendanceLog> AttendanceLogs { get; set; }
        public DbSet<BoardExamMaster> BoardExamMasters { get; set; }
        public DbSet<BoardExamDetail>BoardExamDetails  { get; set; }
        public DbSet<AcademicBackground> AcademicBackgrounds { get; set; }
        //public DbSet<ClassIdPercentage> ClassIdPercentage { get; set; }
       
       // public DbSet<ScStudentRegistrationNumbering> ScStudentRegistrationNumberings { get; set; }

        //public DbSet<ScClassTeacherMapping> ScClassTeacherMappings { get; set; }


        public virtual void Commit()
        {
            this.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                         {
                             m.ToTable("RoleMapping");
                             m.MapLeftKey("UserId");
                             m.MapRightKey("Id");
                         });

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }

    //public sealed class DataContextSingleton
    //{
    //    private static readonly DataContext instance = new DataContext();

    //    static DataContextSingleton() { }

    //    private DataContextSingleton() { }

    //    public static DataContext Instance
    //    {
    //        get
    //        {
    //            return instance;
    //        }
    //    }
    //}
}