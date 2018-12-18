using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Service;
using KRBAccounting.Service.Services;
using KRBAccounting.Web.Services;


[assembly: WebActivator.PreApplicationStartMethod(typeof(KRBAccounting.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(KRBAccounting.Web.App_Start.NinjectWebCommon), "Stop")]

namespace KRBAccounting.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);

        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDesignRepository>().To<DesignRepository>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IAccountGroupRepository>().To<AccountGroupRepository>();
            kernel.Bind<IAccountSubGroupRepository>().To<AccountSubGroupRepository>();
            kernel.Bind<IAgentRepository>().To<AgentRepository>();
            kernel.Bind<IAreaRepository>().To<AreaRepository>();
            kernel.Bind<IBillingTermRepository>().To<BillingTermRepository>();
            kernel.Bind<ICompanyInfoRepository>().To<CompanyInfoRepository>();
            kernel.Bind<ICostCenterRepository>().To<CostCenterRepository>();
            kernel.Bind<ICurrencyRepository>().To<CurrencyRepository>();
            kernel.Bind<IFiscalYearRepository>().To<FiscalYearRepository>();
            kernel.Bind<IGodownRepository>().To<GodownRepository>();
            kernel.Bind<ILedgerRepository>().To<LedgerRepository>();
            kernel.Bind<INarrationRepository>().To<NarrationRepository>();
            kernel.Bind<IProductGroupRepository>().To<ProductGroupRepository>();
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<IProductSubGroupRepository>().To<ProductSubGroupRepository>();
            kernel.Bind<ISecurityService>().To<SecurityService>();
            kernel.Bind<ISubLedgerRepository>().To<SubLedgerRepository>();
            kernel.Bind<IUnitRepository>().To<UnitRepository>();
            kernel.Bind<ICashBankMasterRepository>().To<CashBankMasterRepository>();
            kernel.Bind<ICashBankDetailRepository>().To<CashBankDetailRepository>();
            kernel.Bind<IAccountTransactionRepository>().To<AccountTransactionRepository>();
            kernel.Bind<IJournalVoucherRepository>().To<JournalVoucherRepository>();
            kernel.Bind<IJournalVoucherDetailRepository>().To<JournalVoucherDetailRepository>();
            kernel.Bind<IDebitNoteMasterRepository>().To<DebitNoteMasterRepository>();
            kernel.Bind<IDebitNoteDetailRepository>().To<DebitNoteDetailRepository>();
            kernel.Bind<ICreditNoteMasterRepository>().To<CreditNoteMasterRepository>();
            kernel.Bind<ICreditNoteDetailRepository>().To<CreditNoteDetailRepository>();
            kernel.Bind<IDocumentNumeringSchemeRepository>().To<DocumentNumberingSchemeRepository>();
            kernel.Bind<IPurchaseInvoiceReository>().To<PurchaseInvoiceReository>();
            kernel.Bind<IPurchaseDetailRepository>().To<PurchaseDetailRepository>();
            kernel.Bind<ISystemControlRepository>().To<SystemControlRepository>();
            kernel.Bind<IPurchaseImpTransDocRepository>().To<PurchaseImpTransDocRepository>();
            kernel.Bind<IStockTransactionRepository>().To<StockTransactionRepository>();
            kernel.Bind<IPurchaseReturnMasterRepository>().To<PurchaseReturnMasterRepository>();
            kernel.Bind<IPurchaseReturnDetailRepository>().To<PurchaseReturnDetailRepository>();
            kernel.Bind<IPurchaseReturnImpTransDocRepository>().To<PurchaseReturnImpTransDocRepository>();
            kernel.Bind<ISalesInvoiceRepository>().To<SalesInvoiceRepository>();
            kernel.Bind<ISalesDetailRepository>().To<SalesDetailRepository>();
            kernel.Bind<ISalesInvoiceOtherDetailRepository>().To<SalesInvoiceOtherDetailRepository>();
            kernel.Bind<ISalesReturnMasterRepository>().To<SalesReturnMasterRepository>();
            kernel.Bind<ISalesReturnDetailRepository>().To<SalesReturnDetailRepository>();
            kernel.Bind<ISalesReturnOtherDetailRepository>().To<SalesReturnOtherDetailRepository>();
            kernel.Bind<IUdfEntryRepository>().To<UdfEntryRepository>();
            kernel.Bind<IUdfEntryDetailRepository>().To<UdfEntryDetailRepository>();
            kernel.Bind<IUDFDataRepository>().To<UDFDataRepository>();
            kernel.Bind<IModuleListRepository>().To<ModuleListRepository>();
            kernel.Bind<ISecurityRightRepository>().To<SecurityRightRepository>();
            kernel.Bind<IOpeningLedgerRepository>().To<OpeningLedgerRepository>();
            kernel.Bind<IEntryControlPLRepository>().To<EntryControlPLRepository>();
            kernel.Bind<IEntryControlPurchaseRepository>().To<EntryControlPurchaseRepository>();
            kernel.Bind<IEntryControlSalesRepository>().To<EntryControlSalesRepository>();
            kernel.Bind<IEntryControlInventoryRepository>().To<EntryControlInventoryRepository>();
            kernel.Bind<IBillingTermDetailRepository>().To<BillingTermDetailRepository>();
            kernel.Bind<IFunctionService>().To<FunctionService>();
            kernel.Bind<IMenuItemRepository>().To<MenuItemRepository>();
            kernel.Bind<IScLibraryCardRepository>().To<ScLibraryCardRepository>();
            kernel.Bind<IProductUnitConversionRepository>().To<ProductUnitConversionRepository>();
            kernel.Bind<IProductImagesRepository>().To<ProductImagesRepository>();
            kernel.Bind<IShortNameDetailRepository>().To<ShortNameDetailRepository>();
            kernel.Bind<ISchemeRepository>().To<SchemeRepository>();
            kernel.Bind<ISchemeFreeProductRepository>().To<SchemeFreeProductRepository>();
            kernel.Bind<ISchemeProductRepository>().To<SchemeProductRepository>();
            kernel.Bind<IBillOfMaterialRepository>().To<BillOfMaterialRepository>();
            kernel.Bind<IBillOfMaterialDetailRepository>().To<BillOfMaterialDetailRepository>();
            kernel.Bind<IMaterialIssueRepository>().To<MaterialIssueRepository>();
            kernel.Bind<IMaterialIssueDetailRepository>().To<MaterialIssueDetailRepository>();
            kernel.Bind<IMaterialReturnDetailRepository>().To<MaterialReturnDetailRepository>();
            kernel.Bind<IMaterialReturnRepository>().To<MaterialReturnRepository>();
            kernel.Bind<IStockTransferMasterRepository>().To<StockTransferMasterRepository>();
            kernel.Bind<IStockTransferDetailRepository>().To<StockTransferDetailRepository>();
            kernel.Bind<IPurchaseOrderMasterRepository>().To<PurchaseOrderMasterRepository>();
            kernel.Bind<IPurchaseOrderDetailRepository>().To<PurchaseOrderDetailRepository>();
            kernel.Bind<IPurchaseOrderImpTransDocRepository>().To<PurchaseOrderImpTransDocRepository>();
            kernel.Bind<IPurchaseChallanMasterRepository>().To<PurchaseChallanMasterRepository>();
            kernel.Bind<IPurchaseChallanDetailRepository>().To<PurchaseChallanDetailRepository>();
            kernel.Bind<IStockAdjustmentMasterRepository>().To<StockAdjustmentMasterRepository>();
            kernel.Bind<IStockAdjustmentDetailRepository>().To<StockAdjustmentDetailRepository>();
            kernel.Bind<IExpiryBreakageDetailRepository>().To<ExpiryBreakageDetailRepository>();
            kernel.Bind<IExpiryBreakageMasterRepository>().To<ExpiryBreakageMasterRepository>();
            kernel.Bind<IPurchaseChallanImpTransDocRepository>().To<PurchaseChallanImpTransDocRepository>();
            kernel.Bind<ISalesService>().To<SalesService>();
            kernel.Bind<ISalesOrderMasterRepository>().To<SalesOrderMasterRepository>();
            kernel.Bind<ISalesOrderAddressRepository>().To<SalesOrderAddressRepository>();
            kernel.Bind<ISalesOrderDetailRepository>().To<SalesOrderDetailRepository>();
            kernel.Bind<ISalesOrderOtherDetailRepository>().To<SalesOrderOtherDetailRepository>();
            kernel.Bind<ISalesChallanMasterRepository>().To<SalesChallanMasterRepository>();
            kernel.Bind<ISalesChallanDetailRepository>().To<SalesChallanDetailRepository>();
            kernel.Bind<ISalesChallanImpTransDocRepository>().To<SalesChallanImpTransDocRepository>();
            kernel.Bind<IPurchaseProductBatchRepository>().To<PurchaseProductBatchRepository>();
            kernel.Bind<IPurchaseService>().To<PurchaseService>();
            kernel.Bind<IAccountWatchListRepository>().To<AccountWatchListRepository>();
            //  School
            kernel.Bind<IScEmployeeDepartmentRepository>().To<ScEmployeeDepartmentRepository>();
            kernel.Bind<IScEmployeePostRepository>().To<ScEmployeePostRepository>();
            kernel.Bind<ISchBuildingRepository>().To<SchBuildingRepository>();
            kernel.Bind<IScClassRepository>().To<ScClassRepository>();
            kernel.Bind<IScSubjectRepository>().To<ScSubjectRepository>();
            kernel.Bind<IScNationalityRepository>().To<ScNationalityRepository>();
            kernel.Bind<IScReligionRepository>().To<ScReligionRepository>();
            kernel.Bind<IScStudentCategoryRepository>().To<ScStudentCategoryRepository>();
            kernel.Bind<IScStudentinfoRepository>().To<ScStudentinfoRepository>();
            kernel.Bind<IScSectionRepository>().To<ScSectionRepository>();
            kernel.Bind<IScShiftRepository>().To<ScShiftRepository>();
            kernel.Bind<IScBusRepository>().To<ScBusRepository>();
            kernel.Bind<IScStudentSubjectMappingRepository>().To<ScStudentSubjectMappingRepository>();
            kernel.Bind<IScClassSubjectMappingRepository>().To<ScClassSubjectMappingRepository>();
            kernel.Bind<IScExamRepository>().To<ScExamRepository>();
            kernel.Bind<IScBusStopRepository>().To<ScBusStopRepository>();
            kernel.Bind<IScBusRouteDetailsRepository>().To<ScBusRouteDetailsRepository>();
            kernel.Bind<IScTransportMappingRepository>().To<ScTransportMappingRepository>();
            kernel.Bind<IScGradeRepository>().To<ScGradeRepository>();
            kernel.Bind<IScDivisionRepository>().To<ScDivisionRepository>();
            kernel.Bind<IScExtraActivityRepository>().To<ScExtraActivityRepository>();
            kernel.Bind<IScExamRoutineRepository>().To<ScExamRoutineRepository>();
            kernel.Bind<IScExamMarkSetupRepository>().To<ScExamMarkSetupRepository>();
            kernel.Bind<IScExamAttendanceMasterRepository>().To<ScExamAttendanceMasterRepository>();
            kernel.Bind<IScFeeItemRepository>().To<ScFeeItemRepository>();
            kernel.Bind<IScFeeTermRepository>().To<ScFeeTermRepository>();
            kernel.Bind<IScBoaderRepository>().To<ScBoaderRepository>();
            kernel.Bind<IScClassFeeRateRepository>().To<ScClassFeeRateRepository>();
            kernel.Bind<IScStudentFeeRateMasterRepository>().To<ScStudentFeeRateMasterRepository>();
            kernel.Bind<IScStudentFeeRateDetailRepository>().To<ScStudentFeeRateDetailRepository>();
            kernel.Bind<IScStudentFeeTermRepository>().To<ScStudentFeeTermRepository>();
            kernel.Bind<IScEmployeeCategoryRepository>().To<ScEmployeeCategoryRepository>();
            kernel.Bind<IScEmployeeInfoRepository>().To<ScEmployeeInfoRepository>();
            kernel.Bind<IScHouseGroupRepository>().To<ScHouseGroupRepository>();
            kernel.Bind<IScHouseMappingRepository>().To<ScHouseMappingRepository>();
            kernel.Bind<IScFineSchemeRepository>().To<ScFineSchemeRepository>();
            kernel.Bind<IScFineSchemeDetailsRepository>().To<ScFineSchemeDetailsRepository>();
            kernel.Bind<IScExamAttendanceDetailRepository>().To<ScExamAttendanceDetailRepository>();
            kernel.Bind<IScPrePaidSchemeDetailsRepository>().To<ScPrePaidSchemeDetailsRepository>();
            kernel.Bind<IScPrePaidSchemeRepository>().To<ScPrePaidSchemeRepository>();
            kernel.Bind<IScBoaderMappingRepository>().To<ScBoaderMappingRepository>();
            kernel.Bind<IScStudentRegistrationDetailRepository>().To<ScStudentRegistrationDetailRepository>();
            kernel.Bind<IScStudentRegistrationRepository>().To<ScStudentRegistrationRepository>();
            kernel.Bind<IStudentExtraActivityRepository>().To<StudentExtraActivityRepository>();
            kernel.Bind<IStudentExtraActivityDetailRepository>().To<StudentExtraActivityDetailRepository>();
            kernel.Bind<IScExamMarksEntryRepository>().To<ScExamMarksEntryRepository>();
            kernel.Bind<IScRoomRepository>().To<ScRoomRepository>();
            kernel.Bind<ISchBuildingRoomMappingRepository>().To<SchBuildingRoomMappingRepository>();
            kernel.Bind<IScClassRoomMappingRepository>().To<ScClassRoomMappingRepository>();
            kernel.Bind<IScAbsentApplicationRepository>().To<ScAbsentApplicationRepository>();
            kernel.Bind<IScClassScheduleRepository>().To<ScClassScheduleRepository>();
            kernel.Bind<IScSalaryHeadRepository>().To<ScSalaryHeadRepository>();
            kernel.Bind<IScConsolidatedMarksSetupRepository>().To<ScConsolidatedMarksSetupRepository>();
            kernel.Bind<IScProgramMasterRepository>().To<ScProgramMasterRepository>();
            kernel.Bind<IScStaffAttendanceRepository>().To<ScStaffAttendanceRepository>();
            kernel.Bind<IStudentRegistrationNumberingRepository>().To<StudentRegistrationNumberingRepository>();
            //Transaction
            kernel.Bind<IScMonthlyBillRepository>().To<ScMonthlyBillRepository>();
            kernel.Bind<IScMonthlyBillStudentRepository>().To<ScMonthlyBillStudentRepository>();
            kernel.Bind<IScMaterialReturnMasterRepository>().To<ScMaterialReturnMasterRepository>();
            kernel.Bind<IScMaterialReturnDetailsRepository>().To<ScMaterialReturnDetailsRepository>();
            kernel.Bind<IScBillTransactionRepository>().To<ScBillTransactionRepository>();
            kernel.Bind<IScFeeReceiptRepository>().To<ScFeeReceiptRepository>();

            //Library
            kernel.Bind<IScBookRepository>().To<ScBookRepository>();
            kernel.Bind<IScBookReceivedRepository>().To<ScBookReceivedRepository>();
            kernel.Bind<IScBookReceivedDetailsRepository>().To<ScBookReceivedDetailsRepository>();
            kernel.Bind<IScLibraryMemberRegistrationRepository>().To<ScLibraryMemberRegistrationRepository>();
            kernel.Bind<IScBookIssuedRepository>().To<ScBookIssuedRepository>();
            kernel.Bind<IScBookIssueReturnRepository>().To<ScBookIssueReturnRepository>();
            kernel.Bind<IMaterialIssue_MasterRepository>().To<MaterialIssue_MasterRepository>();
            kernel.Bind<IMaterialIssue_DetailsRepository>().To<MaterialIssue_DetailsRepository>();
            kernel.Bind<IScStaffSubjectMappingRepository>().To<ScStaffSubjectMappingRepository>();
            kernel.Bind<IAcademicYearRepository>().To<AcademicYearRepository>();
            kernel.Bind<ITemplateRepository>().To<TemplateRepository>();
            kernel.Bind<IClassScheduleDetailRepository>().To<ClassScheduleDetailRepository>();
            kernel.Bind<IScStudentAttendanceRepository>().To<ScStudentAttendanceRepository>();
            kernel.Bind<IScTeacherScheduleRepository>().To<ScTeacherScheduleRepository>();

            kernel.Bind<IScLibrarySettingRepository>().To<ScLibrarySettingRepository>();
            kernel.Bind<IScLibraryLateFineRepository>().To<ScLibraryLateFineRepository>();
            kernel.Bind<IStudentSlcInfoRepository>().To<StudentSlcInfoRepository>();
            kernel.Bind<IStudentDocumentRepository>().To<StudentDocumentRepository>();

            
            

            //sms
            kernel.Bind<ISmsGroupRepository>().To<SmsGroupRepository>();
            kernel.Bind<ISmsGroupDetailRepository>().To<SmsGroupDetailRepository>();
            kernel.Bind<ISmsLogRepository>().To<SmsLogRepository>();
            kernel.Bind<ISmsTemplatesRepository>().To<SmsTemplatesRepository>();
            kernel.Bind<ISMSSettingsRepository>().To<SMSSettingsRepository>();
           //Payroll
            kernel.Bind<IPyAllowanceMasterRepository>().To<PyAllowanceMasterRepository>();
            kernel.Bind<IPyCorporateSalaryMasterRepository>().To<PyCorporateSalaryMasterRepository>();
            kernel.Bind<IPyCorporateAllowanceMappingRepository>().To<PyCorporateAllowanceMappingRepository>();
            kernel.Bind<IPyDeductionMasterRepository>().To<PyDeductionMasterRepository>();
            kernel.Bind<IPyEmployeeDeductionMasterRepository>().To<PyEmployeeDeductionMasterRepository>();
            kernel.Bind<IPyEmployeeSalaryMasterRepository>().To<PyEmployeeSalaryMasterRepository>();
            kernel.Bind<IPyPFEmployeeMasterRepository>().To<PyPFEmployeeMasterRepository>();
            kernel.Bind<IPyTaxDeductionPatternRepository>().To<PyTaxDeductionPatternRepository>();
            kernel.Bind<IPyEmployeeDeductionMappingRepository>().To<PyEmployeeDeductionMappingRepository>();
            kernel.Bind<IPyPFEmployeeMappingRepository>().To<PyPFEmployeeMappingRepository>();
            kernel.Bind<IPyEmployeeSalaryAllowanceMappingRepository>().To<PyEmployeeSalaryAllowanceMappingRepository>();
            kernel.Bind<IPyTaxDeductionEmployeeMappingRepository>().To<PyTaxDeductionEmployeeMappingRepository>();
            kernel.Bind<IPyPayrollGenerationRepository>().To<PyPayrollGenerationRepository>();
            kernel.Bind<IPyPayrollGenerationDetailRepository>().To<PyPayrollGenerationDetailRepository>();
            kernel.Bind<IPyPaymentSlipRepository>().To<PyPaymentSlipRepository>();
            kernel.Bind<IProductOpeningRepository>().To<ProductOpeningRepository>();
            kernel.Bind<IOpeningStudentRepository>().To<OpeningStudentRepository>();
            kernel.Bind<IScRollNumberingSchemeRepository>().To<ScRollNumberingSchemeRepository>();
            kernel.Bind<IConsolidateDetailRepository>().To<ConsolidateDetailRepository>();
            kernel.Bind<IConsolidateRepository>().To<ConsolidateRepository>();
            kernel.Bind<IEmployeeTransactionRepository>().To<EmployeeTransactionRepository>();
            kernel.Bind<IScClassTeacherMappingRepository>().To<ScClassTeacherMappingRepository>();
            kernel.Bind<IScMessageRepository>().To<ScMessageRepository>();
            kernel.Bind<IScCalendarEventRepository>().To<ScCalendarEventRepository>();

            kernel.Bind<IAttendanceLogRepository>().To<AttendanceLogRepository>();
            kernel.Bind<IBoardExamMasterRepository>().To<BoardExamMasterRepository>();
            kernel.Bind<IBoardExamDetailRepository>().To<BoardExamDetailRepository>();
            kernel.Bind<IAcademicBackgroundRepository>().To<AcademicBackgroundRepository>();
            //kernel.Bind<IClassIdPercentageRepository>().To<ClassIdPercentageRepository>();

        }
    }
}
