using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Service.Models.BillingTerm;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;

using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.Entry;

using KRBAccounting.Service.Services;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;

using KRBAccounting.Service.Models.Sales;
using KRBAccounting.Service.Models.Purchase;
using KRBAccounting.Web.ViewModels.Home;
using HtmlAgilityPack;

namespace KRBAccounting.Web.Controllers
{
    [CheckFirstInstallation]
    [Authorize]
    public class EntryController : BaseController
    {
        #region Variables

        private readonly ICashBankMasterRepository _cashBankMaster;
        private readonly ICashBankDetailRepository _cashBankDetail;
        private readonly IFormsAuthenticationService _authentication;
        private readonly IAccountTransactionRepository _accountTransaction;
        private readonly DataContext _context = new DataContext();
        private readonly ILedgerRepository _ledger;
        private readonly IAgentRepository _agent;
        private readonly IJournalVoucherRepository _journalVoucher;
        private readonly IJournalVoucherDetailRepository _journalVoucherDetail;
        private readonly IDebitNoteMasterRepository _debitNoteMaster;
        private readonly IDebitNoteDetailRepository _debitNoteDetail;
        private readonly ICreditNoteMasterRepository _creditNoteMaster;
        private readonly ICreditNoteDetailRepository _creditNoteDetail;
        private readonly IPurchaseInvoiceReository _purchaseInvoice;
        private readonly IPurchaseDetailRepository _purchaseDetail;
        private readonly IProductRepository _product;
        private readonly IPurchaseImpTransDocRepository _purchaseImpTransDoc;
        private readonly IStockTransactionRepository _stockTransaction;
        private readonly IPurchaseReturnMasterRepository _purchaseReturnMaster;
        private readonly IPurchaseReturnDetailRepository _purchaseReturnDetail;
        private readonly IPurchaseReturnImpTransDocRepository _purchaseReturnImpTransDoc;
        private readonly ISalesInvoiceRepository _salesInvoice;
        private readonly ISalesDetailRepository _salesDetail;
        private readonly ISalesInvoiceOtherDetailRepository _salesInvoiceOtherDetailRepository;
        private readonly ISalesReturnMasterRepository _salesReturnMaster;
        private readonly ISalesReturnDetailRepository _salesReturnDetail;
        private readonly ISalesReturnOtherDetailRepository _salesReturnOtherDetailRepository;
        private readonly ISystemControlRepository _systemControl;
        private readonly IUdfEntryRepository _udfEntry;
        private readonly IUDFDataRepository _udfData;
        private readonly IDocumentNumeringSchemeRepository _docNumering;
        private readonly IEntryControlPLRepository _entryControlPL;
        private readonly IEntryControlSalesRepository _entryControlSales;
        private readonly IEntryControlPurchaseRepository _entryControlPurchase;
        private readonly IEntryControlInventoryRepository _entryControlInventory;
        private readonly IBillingTermRepository _billingTerm;
        private readonly IBillingTermDetailRepository _billingTermDetail;
        private readonly IAreaRepository _area;
        private readonly IAccountGroupRepository _accountGroup;
        private readonly IAccountSubGroupRepository _accountSubGroup;
        private readonly ICurrencyRepository _currency;
        private readonly IProductGroupRepository _productGroup;
        private readonly IUnitRepository _unitRepository;
        private readonly IProductUnitConversionRepository _productUnitConversionRepository;
        private readonly IBillOfMaterialRepository _billOfMaterial;
        private readonly IBillOfMaterialDetailRepository _billOfMaterialDetail;
        private readonly IMaterialIssueRepository _materialIssue;
        private readonly IMaterialIssueDetailRepository _materialIssueDetail;
        private readonly ICostCenterRepository _costCenter;
        private readonly IGodownRepository _godown;
        private readonly IMaterialReturnRepository _materialReturn;
        private readonly IMaterialReturnDetailRepository _materialReturnDetail;
        private readonly IStockTransferMasterRepository _stockTransfer;
        private readonly IStockTransferDetailRepository _stockTransferDetail;
        private readonly IPurchaseOrderMasterRepository _purchaseOrderMaster;
        private readonly IPurchaseOrderDetailRepository _purchaseOrderDetail;
        private readonly IPurchaseOrderImpTransDocRepository _purchaseOrderImpTransDoc;
        private readonly IPurchaseChallanMasterRepository _purchaseChallanMaster;
        private readonly IStockAdjustmentMasterRepository _stockAdjustment;
        private readonly IStockAdjustmentDetailRepository _stockAdjustmentDetail;
        private readonly IExpiryBreakageDetailRepository _expiryBreakageDetail;
        private readonly IExpiryBreakageMasterRepository _expiryBreakageMaster;
        private readonly ISalesOrderMasterRepository _salesOrderMaster;
        private readonly IPurchaseChallanDetailRepository _purchaseChallanDetail;
        private readonly IPurchaseChallanImpTransDocRepository _purchaseChallanImpTransDoc;
        private readonly IPurchaseService _purchaseService;
        private readonly ISalesService _salesService;
        private readonly IPurchaseProductBatchRepository _purchaseProductBatch;
        private readonly IFiscalYearRepository _fiscalYear;
        private readonly IAccountWatchListRepository _accountWatchList;
        public int CurrentBranchId;
        public bool EnableBranch;
        private readonly DataContext db = new DataContext();
 
        #endregion

        #region Constructor
        public EntryController(ICashBankMasterRepository cashBankMasterRepository, IUnitRepository unitRepository,
            ICashBankDetailRepository cashBankDetailRepository,
            IAccountTransactionRepository accountTransactionRepository,
            ILedgerRepository ledgerRepository,
            IJournalVoucherRepository journalVoucherRepository,
            IJournalVoucherDetailRepository journalVoucherDetailRepository,
            IDebitNoteMasterRepository debitNoteMasterRepository,
            IDebitNoteDetailRepository debitNoteDetailRepository,
            ICreditNoteMasterRepository creditNoteMasterRepository,
            ICreditNoteDetailRepository creditNoteDetailRepository,
            IPurchaseInvoiceReository purchaseInvoiceReository,
            IPurchaseDetailRepository purchaseDetailRepository,
            IProductRepository productRepository,
            IPurchaseImpTransDocRepository purchaseImpTransDocRepository,
            IStockTransactionRepository stockTransactionRepository,
            IPurchaseReturnMasterRepository purchaseReturnMasterRepository,
            IPurchaseReturnDetailRepository purchaseReturnDetailRepository,
            IPurchaseReturnImpTransDocRepository purchaseReturnImpTransDocRepository,
            ISalesInvoiceRepository salesInvoiceRepository,
            ISalesDetailRepository salesDetailRepository,
            ISalesInvoiceOtherDetailRepository salesInvoiceOtherDetailRepository,
            ISalesReturnMasterRepository salesReturnMasterRepository,
            ISalesReturnDetailRepository salesReturnDetailRepository,
            ISalesReturnOtherDetailRepository salesReturnOtherDetailRepository,
            IAgentRepository agentRepository,
            ISystemControlRepository systemControlRepository,
            IUdfEntryRepository udfEntryRepository,
            IUDFDataRepository udfDataRepository,
            IDocumentNumeringSchemeRepository documentNumeringSchemeRepository,
            IEntryControlPLRepository entryControlRepositoryPL,
            IEntryControlSalesRepository entryControlSalesRepository,
            IEntryControlPurchaseRepository entryControlPurchaseRepository,
            IEntryControlInventoryRepository entryControlInventoryRepository,
            IBillingTermRepository billingTermRepository,
            IBillingTermDetailRepository billingTermDetailRepository,
            IAccountGroupRepository accountGroupRepository,
            IAreaRepository areaRepository,
            IAccountSubGroupRepository accountSubGroupRepository,
            ICurrencyRepository currencyRepository,
            IProductGroupRepository productGroupRepository,
            IProductUnitConversionRepository productUnitConversionRepository,
            IBillOfMaterialRepository billOfMaterialRepository,
            IBillOfMaterialDetailRepository billOfMaterialDetailRepository,
            IMaterialIssueRepository materialIssueRepository,
            IMaterialIssueDetailRepository materialIssueDetailRepository,
            ICostCenterRepository costCenterRepository,
            IGodownRepository godownRepository,
            IMaterialReturnDetailRepository materialReturnDetail,
            IMaterialReturnRepository materialReturn,
            IStockTransferMasterRepository stockTransferMasterRepository,
            IStockTransferDetailRepository stockTransferDetailRepository,
            IPurchaseOrderMasterRepository purchaseOrderMasterRepository,
            IPurchaseOrderDetailRepository purchaseOrderDetailRepository,
            IPurchaseOrderImpTransDocRepository purchaseOrderImpTransDocRepository,
            IPurchaseChallanMasterRepository purchaseChallanMaster,
            IStockAdjustmentMasterRepository stockAdjustmentMaster,
            IStockAdjustmentDetailRepository stockAdjustmentDetail,
            IExpiryBreakageDetailRepository expiryBreakageDetail,
            IExpiryBreakageMasterRepository expiryBreakageMaster,
           IPurchaseChallanDetailRepository purchaseChallanDetailRepository,
            IPurchaseChallanImpTransDocRepository purchaseChallanImpTransDocRepository,
            ISalesService salesService,
            ISalesOrderMasterRepository salesOrderMasterRepository,
            IPurchaseService purchaseService,
            IPurchaseProductBatchRepository purchaseProductBatchRepository,
            IFiscalYearRepository fiscalYearRepository,
            IAccountWatchListRepository accountWatchList
            
            )
        {
            _cashBankMaster = cashBankMasterRepository;
            _cashBankDetail = cashBankDetailRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Accounting);
            _accountTransaction = accountTransactionRepository;
            _ledger = ledgerRepository;
            _journalVoucher = journalVoucherRepository;
            _journalVoucherDetail = journalVoucherDetailRepository;
            _debitNoteMaster = debitNoteMasterRepository;
            _debitNoteDetail = debitNoteDetailRepository;
            _creditNoteMaster = creditNoteMasterRepository;
            _creditNoteDetail = creditNoteDetailRepository;
            _purchaseInvoice = purchaseInvoiceReository;
            _purchaseDetail = purchaseDetailRepository;
            _product = productRepository;
            _purchaseImpTransDoc = purchaseImpTransDocRepository;
            _stockTransaction = stockTransactionRepository;
            _purchaseReturnMaster = purchaseReturnMasterRepository;
            _purchaseReturnDetail = purchaseReturnDetailRepository;
            _purchaseReturnImpTransDoc = purchaseReturnImpTransDocRepository;
            _salesInvoice = salesInvoiceRepository;
            _salesDetail = salesDetailRepository;
            _salesInvoiceOtherDetailRepository = salesInvoiceOtherDetailRepository;
            _salesReturnMaster = salesReturnMasterRepository;
            _salesReturnDetail = salesReturnDetailRepository;
            _salesReturnOtherDetailRepository = salesReturnOtherDetailRepository;
            _agent = agentRepository;
            _systemControl = systemControlRepository;
            _udfEntry = udfEntryRepository;
            _udfData = udfDataRepository;
            _docNumering = documentNumeringSchemeRepository;
            _entryControlPL = entryControlRepositoryPL;
            _entryControlInventory = entryControlInventoryRepository;
            _entryControlPurchase = entryControlPurchaseRepository;
            _entryControlSales = entryControlSalesRepository;
            _billingTerm = billingTermRepository;
            _billingTermDetail = billingTermDetailRepository;
            _accountGroup = accountGroupRepository;
            _area = areaRepository;
            _accountSubGroup = accountSubGroupRepository;
            _currency = currencyRepository;
            _productGroup = productGroupRepository;
            CurrentBranchId = CurrentBranch.Id;
            EnableBranch = base.SystemControl.EnableBranch;
            _unitRepository = unitRepository;
            _productUnitConversionRepository = productUnitConversionRepository;
            _billOfMaterial = billOfMaterialRepository;
            _billOfMaterialDetail = billOfMaterialDetailRepository;
            _materialIssue = materialIssueRepository;
            _materialIssueDetail = materialIssueDetailRepository;
            _costCenter = costCenterRepository;
            _godown = godownRepository;
            _materialReturn = materialReturn;
            _materialReturnDetail = materialReturnDetail;
            _stockTransfer = stockTransferMasterRepository;
            _stockTransferDetail = stockTransferDetailRepository;
            _purchaseOrderMaster = purchaseOrderMasterRepository;
            _purchaseOrderDetail = purchaseOrderDetailRepository;
            _purchaseOrderImpTransDoc = purchaseOrderImpTransDocRepository;
            _purchaseChallanMaster = purchaseChallanMaster;

            _stockAdjustment = stockAdjustmentMaster;
            _stockAdjustmentDetail = stockAdjustmentDetail;
            _expiryBreakageMaster = expiryBreakageMaster;
            _expiryBreakageDetail = expiryBreakageDetail;
            _salesOrderMaster = salesOrderMasterRepository;
            _purchaseChallanDetail = purchaseChallanDetailRepository;
            _purchaseChallanImpTransDoc = purchaseChallanImpTransDocRepository;
            _salesService = salesService;
            _purchaseService = purchaseService;
            _purchaseProductBatch = purchaseProductBatchRepository;
            _fiscalYear = fiscalYearRepository;
            _accountWatchList = accountWatchList;


        }

        #endregion
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DashBoard()
        {

            var viewModel = base.CreateViewModel<HomeViewModel>();
            var branchId = _authentication.GetAuthenticatedUser().LastAccessedBranch;
            var currentDate = DateTime.UtcNow;
            var thisMonthStart = currentDate.AddDays(1 - currentDate.Day);
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
            var startDate = thisMonthStart.ToString("yyyy-MM-dd");
            var endDate = thisMonthEnd.ToString("yyyy-MM-dd");
            var thisYearStart = FiscalYear.StartDate.ToString("yyyy-MM-dd");
            var thisYearEnd = FiscalYear.EndDate.ToString("yyyy-MM-dd");
            var cashFlowList = StoredProcedureService.GetTotalCashFlow(startDate, endDate, base.FiscalYear.Id);
            var cashFlowIn = (from cIn in cashFlowList
                              select new
                              {
                                  Date = cIn.vdate,
                                  Total = cIn.InTotal
                              }).ToDictionary(o => o.Date, o => o.Total);
            var cashFlowOut = (from cOut in cashFlowList
                               select new
                               {
                                   Date = cOut.vdate,
                                   Total = cOut.OutTotal
                               }).ToDictionary(o => o.Date, o => o.Total);
            var categories = CreateGroupedList(thisMonthStart, thisMonthEnd);
            viewModel.CashFlowInChart = GetTotalInChart(categories, cashFlowIn, thisMonthStart, thisMonthEnd);
            viewModel.CashFlowOutChart = GetTotalOutChart(categories, cashFlowOut, thisMonthStart, thisMonthEnd);
            viewModel.CurrentDate = currentDate;

            var monthlyAnalysis = StoredProcedureService.GetAnalysisMonthly(currentDate.Year, currentDate.Month, 1000);
            var yearlyAnalysis = StoredProcedureService.GetAnalysisYearly(currentDate.Year, 1000);
            viewModel.MonthlyAnalysisChart = GetMonthlyAnalysisChart(monthlyAnalysis);
            viewModel.YearlyAnalysisChart = GetYearlyAnalysisChart(yearlyAnalysis);
            var cashAnalysis = StoredProcedureService.GetCashFlowMonthly(thisYearStart, thisYearEnd, FiscalYear.Id).Where(x => x.NoOfMonth != 0).ToList();
            var bankAnalysis = StoredProcedureService.GetBankFlowMonthly(thisYearStart, thisYearEnd, FiscalYear.Id).Where(x => x.NoOfMonth != 0).ToList();
            viewModel.CashAnalysisChart = GetCashAnalysis(cashAnalysis, bankAnalysis);
            string customer = "CU";
            string ledgerList = "0";
            string all = "A";
            var data = _accountWatchList.GetAll().Select(x => x.LedgerId).ToArray();
            string idString = String.Join(",", data);

            var topItems = StoredProcedureService.GetTopItemses(thisYearStart, thisYearEnd, FiscalYear.Id, 5, CurrentBranch.Id).Select(x => new PieViewModel()
            {
                Text = x.Name,
                Value = x.Amount
            }).ToList();
            viewModel.TopItem = topItems;
            var topCustomer =
                StoredProcedureService.GetAccountWatchList(thisYearStart, thisYearEnd, FiscalYear.Id, customer, ledgerList, 5).Select(x => new PieViewModel()
                {
                    Text = x.AccountName,
                    Value = x.Total
                }).ToList();
            viewModel.TopCustomer = topCustomer;
            var PLTotal = StoredProcedureService.GetPLTotal(thisYearStart, thisYearEnd, FiscalYear.Id).ToList();
            viewModel.PLTotalList = PLTotal;
            var accountWatchList =
                StoredProcedureService.GetAccountWatchList(thisYearStart, thisYearEnd, FiscalYear.Id, all, idString, 10)
                    .ToList();
            viewModel.AccountWatchLists = accountWatchList;
            return View(viewModel);
        }
        public ActionResult PreviousChart(DateTime currentDate)
        {
            var viewModel = new HomeViewModel();
            var prevDate = currentDate.AddMonths(-1);
            var thisMonthStart = prevDate.AddDays(1 - prevDate.Day);
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
            var startDate = thisMonthStart.ToString("yyyy-MM-dd");
            var endDate = thisMonthEnd.ToString("yyyy-MM-dd");
            var cashFlowList = StoredProcedureService.GetTotalCashFlow(startDate, endDate, base.FiscalYear.Id);
            var cashFlowIn = (from cIn in cashFlowList
                              select new
                              {
                                  Date = cIn.vdate,
                                  Total = cIn.InTotal
                              }).ToDictionary(o => o.Date, o => o.Total);
            var cashFlowOut = (from cOut in cashFlowList
                               select new
                               {
                                   Date = cOut.vdate,
                                   Total = cOut.OutTotal
                               }).ToDictionary(o => o.Date, o => o.Total);
            var categories = CreateGroupedList(thisMonthStart, thisMonthEnd);
            viewModel.CashFlowInChart = GetTotalInChart(categories, cashFlowIn, thisMonthStart, thisMonthEnd);
            viewModel.CashFlowOutChart = GetTotalOutChart(categories, cashFlowOut, thisMonthStart, thisMonthEnd);
            viewModel.CurrentDate = prevDate;
            return PartialView("_PartialCashFlowChart", viewModel);
        }

        public ActionResult NextChart(DateTime currentDate)
        {
            var viewModel = new HomeViewModel();
            var nextDate = currentDate.AddMonths(1);
            var thisMonthStart = nextDate.AddDays(1 - nextDate.Day);
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
            var startDate = thisMonthStart.ToString("yyyy-MM-dd");
            var endDate = thisMonthEnd.ToString("yyyy-MM-dd");
            var cashFlowList = StoredProcedureService.GetTotalCashFlow(startDate, endDate, base.FiscalYear.Id);
            var cashFlowIn = (from cIn in cashFlowList
                              select new
                              {
                                  Date = cIn.vdate,
                                  Total = cIn.InTotal
                              }).ToDictionary(o => o.Date, o => o.Total);
            var cashFlowOut = (from cOut in cashFlowList
                               select new
                               {
                                   Date = cOut.vdate,
                                   Total = cOut.OutTotal
                               }).ToDictionary(o => o.Date, o => o.Total);
            var categories = CreateGroupedList(thisMonthStart, thisMonthEnd);
            viewModel.CashFlowInChart = GetTotalInChart(categories, cashFlowIn, thisMonthStart, thisMonthEnd);
            viewModel.CashFlowOutChart = GetTotalOutChart(categories, cashFlowOut, thisMonthStart, thisMonthEnd);
            viewModel.CurrentDate = nextDate;
            return PartialView("_PartialCashFlowChart", viewModel);
        }

        public ActionResult PreviousMonthlyChart(DateTime currentDate)
        {
            var prevDate = currentDate.AddMonths(-1);
            var monthlyAnalysis = StoredProcedureService.GetAnalysisMonthly(prevDate.Year, prevDate.Month, 1000);
            var monthlyAnalysisChart = GetMonthlyAnalysisChart(monthlyAnalysis);
            monthlyAnalysisChart.CurrentDate = prevDate;
            return PartialView("_partialMonthlyChart", monthlyAnalysisChart);
        }

        public ActionResult NextMonthlyChart(DateTime currentDate)
        {
            var nextDate = currentDate.AddMonths(1);
            var monthlyAnalysis = StoredProcedureService.GetAnalysisMonthly(nextDate.Year, nextDate.Month, 1000);
            var monthlyAnalysisChart = GetMonthlyAnalysisChart(monthlyAnalysis);
            monthlyAnalysisChart.CurrentDate = nextDate;
            return PartialView("_partialMonthlyChart", monthlyAnalysisChart);
        }

        public ActionResult PreviousYearlyChart(DateTime currentDate)
        {
            var prevDate = currentDate.AddYears(-1);
            var yearlyAnalysis = StoredProcedureService.GetAnalysisYearly(prevDate.Year, 1000);
            var yearlyAnalysisChart = GetYearlyAnalysisChart(yearlyAnalysis);
            yearlyAnalysisChart.CurrentDate = prevDate;
            return PartialView("_partialYearlyChart", yearlyAnalysisChart);
        }

        public ActionResult NextYearlyChart(DateTime currentDate)
        {
            var nextDate = currentDate.AddYears(1);
            var yearlyAnalysis = StoredProcedureService.GetAnalysisYearly(nextDate.Year, 1000);
            var yearlyAnalysisChart = GetYearlyAnalysisChart(yearlyAnalysis);
            yearlyAnalysisChart.CurrentDate = nextDate;
            return PartialView("_partialYearlyChart", yearlyAnalysisChart);
        }
        private CashFlowChart GetTotalOutChart(List<DateTime> categories,
                                                                   Dictionary<DateTime, decimal> outFlow,
                                                                   DateTime startDate, DateTime endDate)
        {
            var chart = new CashFlowChart()
            {
                Categories = new List<string>(),
                BeginDate = startDate,
                EndDate = endDate,
                Total = new List<TotalChartDataItem>()
            };

            categories.ForEach(d =>
            {
                // set category
                chart.Categories.Add(d.ToString("dd"));

                decimal totalOutFlow = 0;
                totalOutFlow = outFlow.Where(o => o.Key.Date == d.Date).FirstOrDefault().Value;

                var ins = new TotalChartDataItem() { y = totalOutFlow };

                chart.Total.Add(ins);
            });
            return chart;
        }

        private AnalysisChart GetMonthlyAnalysisChart(List<SP_AnalysisMonthly> totals)
        {
            var analysisChart = new AnalysisChart();
            //Purchase
            var purchaseChart = new DataChart()
            {
                Categories = new List<string>(),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Total = new List<TotalChartDataItem>()
            };
            totals.ForEach(d =>
            {
                // set category
                purchaseChart.Categories.Add(d.Id.ToString());
                var ins = new TotalChartDataItem() { y = d.Purchase };
                purchaseChart.Total.Add(ins);
            });
            //Purchase Return
            var purchaseReturnChart = new DataChart()
            {
                Categories = new List<string>(),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Total = new List<TotalChartDataItem>()
            };
            totals.ForEach(d =>
            {
                // set category
                purchaseReturnChart.Categories.Add(d.Id.ToString());
                var ins = new TotalChartDataItem() { y = d.PurchaseReturn };
                purchaseReturnChart.Total.Add(ins);
            });
            //Sales
            var salesChart = new DataChart()
            {
                Categories = new List<string>(),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Total = new List<TotalChartDataItem>()
            };
            totals.ForEach(d =>
            {
                // set category
                salesChart.Categories.Add(d.Id.ToString());
                var ins = new TotalChartDataItem() { y = d.Sales };
                salesChart.Total.Add(ins);
            });
            //Sales Return
            var salesReturnChart = new DataChart()
            {
                Categories = new List<string>(),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Total = new List<TotalChartDataItem>()
            };
            totals.ForEach(d =>
            {
                // set category
                salesReturnChart.Categories.Add(d.Id.ToString());
                var ins = new TotalChartDataItem() { y = d.SalesReturn };
                salesReturnChart.Total.Add(ins);
            });
            analysisChart.PurchaseChart = purchaseChart;
            analysisChart.PurchaseReturnChart = purchaseReturnChart;
            analysisChart.SalesChart = salesChart;
            analysisChart.SalesReturnChart = salesReturnChart;
            return analysisChart;
        }

        private AnalysisChart GetYearlyAnalysisChart(List<SP_AnalysisYearly> totals)
        {
            var analysisChart = new AnalysisChart();
            //Purchase
            var purchaseChart = new DataChart()
            {
                Categories = new List<string>(),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Total = new List<TotalChartDataItem>()
            };
            totals.ForEach(d =>
            {
                // set category
                if (SystemControl.DateType == 1)
                    purchaseChart.Categories.Add(UtilityService.GetMonthName(d.Month, true));
                else
                    purchaseChart.Categories.Add(Enum.GetName(typeof(Enums.NepaliMonth), d.Month));

                var ins = new TotalChartDataItem() { y = d.Purchase };
                purchaseChart.Total.Add(ins);
            });
            //Purchase Return
            var purchaseReturnChart = new DataChart()
            {
                Categories = new List<string>(),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Total = new List<TotalChartDataItem>()
            };
            totals.ForEach(d =>
            {
                // set category
                if (SystemControl.DateType == 1)
                    purchaseReturnChart.Categories.Add(d.MonthName);
                else
                    purchaseReturnChart.Categories.Add(Enum.GetName(typeof(Enums.NepaliMonth), d.Month));
                var ins = new TotalChartDataItem() { y = d.PurchaseReturn };
                purchaseReturnChart.Total.Add(ins);
            });
            //Sales
            var salesChart = new DataChart()
            {
                Categories = new List<string>(),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Total = new List<TotalChartDataItem>()
            };
            totals.ForEach(d =>
            {
                // set category
                if (SystemControl.DateType == 1)
                    salesChart.Categories.Add(d.MonthName);
                else
                    salesChart.Categories.Add(Enum.GetName(typeof(Enums.NepaliMonth), d.Month));
                var ins = new TotalChartDataItem() { y = d.Sales };
                salesChart.Total.Add(ins);
            });
            //Sales Return
            var salesReturnChart = new DataChart()
            {
                Categories = new List<string>(),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Total = new List<TotalChartDataItem>()
            };
            totals.ForEach(d =>
            {
                // set category
                if (SystemControl.DateType == 1)
                    salesReturnChart.Categories.Add(d.MonthName);
                else
                    salesReturnChart.Categories.Add(Enum.GetName(typeof(Enums.NepaliMonth), d.Month));
                var ins = new TotalChartDataItem() { y = d.SalesReturn };
                salesReturnChart.Total.Add(ins);
            });
            analysisChart.PurchaseChart = purchaseChart;
            analysisChart.PurchaseReturnChart = purchaseReturnChart;
            analysisChart.SalesChart = salesChart;
            analysisChart.SalesReturnChart = salesReturnChart;
            return analysisChart;
        }

        private CashFlowChart GetTotalInChart(List<DateTime> categories,
                                                                    Dictionary<DateTime, decimal> inflow,
                                                                    DateTime startDate, DateTime endDate)
        {
            var chart = new CashFlowChart()
            {
                Categories = new List<string>(),
                BeginDate = startDate,
                EndDate = endDate,
                Total = new List<TotalChartDataItem>()
            };

            categories.ForEach(d =>
            {
                // set category
                chart.Categories.Add(d.ToString("dd"));

                decimal totalInflow = 0;
                totalInflow = inflow.Where(o => o.Key.Date == d.Date).FirstOrDefault().Value;

                var profileViews = new TotalChartDataItem() { y = totalInflow };

                chart.Total.Add(profileViews);
            });
            return chart;
        }

        public List<DateTime> CreateGroupedList(DateTime startDate, DateTime endDate)
        {
            var dateList = new List<DateTime>();

            var difference = endDate.Subtract(startDate);

            for (var i = 0; i <= difference.TotalDays; i++)
            {
                dateList.Add(startDate.AddDays(i));
            }
            //var monthYearList = dateList.Select(d => new { d.Year, d.Month }).GroupBy(t => new { t.Year, t.Month }).ToList();
            //dateList.Clear();
            //monthYearList.ForEach(t => dateList.Add(new DateTime(t.Key.Year, t.Key.Month, 1)));
            return dateList;
        }
        public ActionResult Currency()
        {
            HtmlWeb hw = new HtmlWeb();
            List<Currency> Curr = new List<Currency>();

            HtmlDocument html = hw.Load("http://www.nrb.org.np/fxmexchangerate1.php?YY=&&MM=&&DD=");
            HtmlNodeCollection link = html.DocumentNode.SelectNodes("//table[@width='386']//tr");
            int rowNum = 0;
            int colNum = 0;
            foreach (HtmlNode hl in link)
            {
                rowNum++;
                if (rowNum >= 3)
                {
                    colNum = 0;
                    Currency c = new Currency();
                    foreach (HtmlNode n in hl.SelectNodes("td"))
                    {
                        //if (colNum == 0) c.currencyName = n.InnerText.Replace("\n", "").Trim();
                        //if (colNum == 1) c.Unit = n.InnerText.Replace("\n", "").Trim();
                        //if (colNum == 2) c.BuyRate = n.InnerText.Replace("\n", "").Trim();
                        //if (colNum == 3) c.SaleRate = n.InnerText.Replace("\n", "").Trim();
                        colNum++;
                    }
                    Curr.Add(c);
                }
            }
            return View();
        }

        private CashAnalysisChart GetCashAnalysis(List<SP_CashFlowMonthly> CashTotals, List<SP_BankFlowMonthly> BankTotals)
        {
            var cashAnalysisChart = new CashAnalysisChart();
            var cashChart = new DataChart()
            {
                Categories = new List<string>(),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Total = new List<TotalChartDataItem>()
            };
            CashTotals.ForEach(d =>
            {
                cashChart.Categories.Add(UtilityService.GetMonthName(d.NoOfMonth, true));
                var ins = new TotalChartDataItem() { y = d.Total };
                cashChart.Total.Add(ins);
            });

            var bankChart = new DataChart()
            {
                Categories = new List<string>(),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                Total = new List<TotalChartDataItem>()
            };
            BankTotals.ForEach(d =>
            {
                bankChart.Categories.Add(d.NoOfMonth.ToString());
                var ins = new TotalChartDataItem() { y = d.Total };
                bankChart.Total.Add(ins);
            });
            cashAnalysisChart.CashInHand = cashChart;
            cashAnalysisChart.CashInBank = bankChart;
            return cashAnalysisChart;
        }

        public ActionResult GetDocDetail(int id)
        {
            var detail = UtilityService.GetDocDetail(id);
            return Json(detail, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDocNumber(int id)
        {
            var number = UtilityService.GetDocumentNumbering(id);

            return Json(number, JsonRequestBehavior.AllowGet);
        }
        #region Cash/Bank Voucher

        public JsonResult CheckVoucherNoInEntry(string VoucherNo, int? Id)
        {

            var feeterm = new CashBankMaster();
            var fiscalyear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _cashBankMaster.GetById(x => x.Id == Id);

                if (data.VoucherNo.ToLower().Trim() != VoucherNo.ToLower().Trim())
                {
                    feeterm =
                        _cashBankMaster.GetById(x => x.VoucherNo.ToLower().Trim() == VoucherNo.Trim().ToLower() && !x.IsDeleted && x.FYid == fiscalyear.Id);

                }
            }
            else
            {
                feeterm = _cashBankMaster.GetById(x => x.VoucherNo.ToLower().Trim() == VoucherNo.Trim().ToLower() && !x.IsDeleted && x.FYid == fiscalyear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new CashBankMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        [CheckPermission(Permissions = "Navigate", Module = "CB")]
        public ActionResult CashBankVoucher()
        {
            ViewBag.UserRight = base.UserRight("CB");
            return View();
        }

        [CheckPermission(Permissions = "Navigate", Module = "CB")]
        public ActionResult CashBankVoucherList()
        {
            ViewBag.UserRight = base.UserRight("CB");
            var viewModel = new CashBankVoucherViewModel();
            var fiscalyear = _fiscalYear.GetById(x => x.IsDefalut);
            //var listPending = _cashBankMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYid == fiscalyear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //var listAccepted = _cashBankMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //viewModel.AcceptedList = listAccepted;
            var context = new DataContext();
            //var listPending =
            //    context.CashBankMasters.Where(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYid == fiscalyear.Id)
            //        .AsQueryable()
            //        .OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            var listPending = _cashBankMaster.GetPagedList(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYid == fiscalyear.Id,
                x => x.Id, 1, SystemControl.PageSize);
                //.ToPagedList(1, base.SystemControl.PageSize);
            viewModel.PendingList = listPending;
            return PartialView(viewModel);
        }
        [HttpPost]
        public ActionResult CashBankVoucherDelete(int id)
        {
            var model = _cashBankMaster.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _cashBankMaster.Update(model);
            _accountTransaction.Delete(x => x.Source == "CB" && x.ReferenceId == id);
            return Json(true);
        }
        public ActionResult CashBankVoucherPendingList(int pageNo)
        {
            var fiscalyear = _fiscalYear.GetById(x => x.IsDefalut);
            var list = _cashBankMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYid == fiscalyear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            return PartialView(list);
        }

        //public ActionResult CashBankVoucherAcceptedList(int pageNo)
        //{
        //    var list = _cashBankMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
        //    return PartialView(list);
        //}
        public JsonResult GetCurrency()
        {
            var data = _currency.GetMany(x => !x.IsDeleted).Select(x => new
                                                                            {
                                                                                Id = x.Id,
                                                                                Description = x.Code
                                                                            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckFiscalyearDateinCashBank(string VoucherDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(VoucherDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(VoucherDate);
                    }

                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }

        [CheckPermission(Permissions = "Create", Module = "CB")]
        public ActionResult CashBankVoucherAdd()
        {
            var viewmodel = base.CreateViewModel<CashBankVoucherAddViewModel>();
            viewmodel.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                viewmodel.EntryControl = entryControl;
            }
            if (base.SystemControl.IsCurrDate)
            {
                viewmodel.VoucherDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _cashBankMaster.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewmodel.VoucherDate = base.SystemControl.DateType == 1 ? data.VoucherDate.ToShortDateString() : data.VoucherMiti;
                }
                else
                {
                    viewmodel.VoucherDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }
            }
            viewmodel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.CashBankMaster).ToList();
            viewmodel.CashBankMaster = new CashBankMaster();
            return PartialView(viewmodel);
        }

        public ActionResult GetCashBankVoucherPartial()
        {
            var model = new CashBankDetailEntryViewModel();
            model.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                model.EntryControl = entryControl;
            }
            return PartialView("_PartialCashBankDetailEntry", model);
        }

        [HttpPost]
        public ActionResult CashBankVoucherAdd(IEnumerable<CashBankDetailEntryViewModel> cashBankDetailEntry, CashBankVoucherAddViewModel model, FormCollection formCollection)
        {
            //  decimal rate = 0;

            if (cashBankDetailEntry != null && cashBankDetailEntry.Count() != 0)
            {
                var vNo = model.CashBankMaster.VoucherNo;
                if (model.CashBankMaster.DocId != null)
                {
                    var docId = Convert.ToInt32(model.CashBankMaster.DocId);
                    vNo = UtilityService.GetDocumentNumbering(docId);
                }


                var cashbank = _cashBankMaster.GetById(x => x.VoucherNo == vNo && !x.IsDeleted);
                if (cashbank == null)
                {
                    var userId = _authentication.GetAuthenticatedUser().Id;
                    //Temporary
                    model.CashBankMaster.ChequeDate = DateTime.Now;
                    model.CashBankMaster.CreatedDate = DateTime.UtcNow;
                    model.CashBankMaster.UpdatedDate = DateTime.UtcNow;
                    model.CashBankMaster.CreatedById = userId;
                    model.CashBankMaster.UpdatedById = userId;
                    // model.CashBankMaster.VoucherDate = model.VoucherDate;
                    if (base.SystemControl.DateType == 1)
                    {
                        model.CashBankMaster.VoucherDate = model.VoucherDate.ToDate();
                        model.CashBankMaster.VoucherMiti = NepaliDateService.NepaliDate(model.CashBankMaster.VoucherDate).Date;

                    }
                    else
                    {
                        model.CashBankMaster.VoucherMiti = model.VoucherDate;
                        model.CashBankMaster.VoucherDate = NepaliDateService.NepalitoEnglishDate(model.VoucherDate);

                    }
                    if (!string.IsNullOrEmpty(model.ChequeDate))
                    {
                        if (base.SystemControl.DateType == 1)
                        {
                            model.CashBankMaster.ChequeDate = model.ChequeDate.ToDate();
                            model.CashBankMaster.ChequeMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.CashBankMaster.ChequeDate)).Date;
                        }
                        else
                        {
                            model.CashBankMaster.ChequeMiti = model.ChequeDate;
                            model.CashBankMaster.ChequeDate = NepaliDateService.NepalitoEnglishDate(model.ChequeDate);
                        }
                    }

                    model.CashBankMaster.VoucherNo = vNo;
                    model.CashBankMaster.FYid = this.FiscalYear.Id;
                    model.CashBankMaster.BranchId = CurrentBranchId;

                    _cashBankMaster.Add(model.CashBankMaster);
                    if (model.CashBankMaster.DocId != null)
                    {
                        var docId = Convert.ToInt32(model.CashBankMaster.DocId);
                        var documentnumber = _docNumering.GetById(docId);
                        documentnumber.DocCurrNo += 1;
                        _docNumering.Update(documentnumber);
                    }
                    int sno = 1;
                    foreach (var item in cashBankDetailEntry)
                    {
                        if (item.LedgerId != 0 && (item.RecAmount != null || item.PayAmount != null))
                        {


                            var cashBankDetail = new CashBankDetail();
                            var accountTransactionMaster = new AccountTransaction();
                            var accountTransactionDetail = new AccountTransaction();
                            cashBankDetail.LedgerId = item.LedgerId;
                            if (item.RecAmount != null)
                            {
                                cashBankDetail.Amount = item.RecAmount.ToDecimal();
                                cashBankDetail.AmountType = Convert.ToInt32(KRBAccounting.Enums.AmountTypeEnum.Receipt);
                                accountTransactionMaster.DrAmt = item.RecAmount.ToDecimal();
                                accountTransactionDetail.CrAmt = item.RecAmount.ToDecimal();
                                if (model.CashBankMaster.CurrencyId != null && model.CashBankMaster.CurrencyId != 0)
                                {
                                    accountTransactionMaster.LocalDrAmt = item.RecAmount.ToDecimal() * Convert.ToDecimal(model.CashBankMaster.Rate);
                                    accountTransactionDetail.LocalCrAmt = item.RecAmount.ToDecimal() * Convert.ToDecimal(model.CashBankMaster.Rate);
                                }
                                else
                                {
                                    accountTransactionMaster.LocalDrAmt = item.RecAmount.ToDecimal();
                                    accountTransactionDetail.LocalCrAmt = item.RecAmount.ToDecimal();

                                }


                                //Need to add enum for amounttype
                            }
                            else
                            {
                                cashBankDetail.Amount = Convert.ToDecimal(item.PayAmount);
                                cashBankDetail.AmountType = Convert.ToInt32(KRBAccounting.Enums.AmountTypeEnum.Payment);
                                accountTransactionMaster.CrAmt = Convert.ToDecimal(item.PayAmount);
                                accountTransactionDetail.DrAmt = Convert.ToDecimal(item.PayAmount);
                                if (model.CashBankMaster.CurrencyId != null && model.CashBankMaster.CurrencyId != 0)
                                {
                                    accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(item.PayAmount) * Convert.ToDecimal(model.CashBankMaster.Rate);
                                    accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.PayAmount) * Convert.ToDecimal(model.CashBankMaster.Rate);
                                }
                                else
                                {
                                    accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(item.PayAmount);
                                    accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.PayAmount);
                                }

                            }

                            cashBankDetail.SlCode = item.SubLedgerId;
                            cashBankDetail.Narration = item.Narration;
                            cashBankDetail.MasterId = model.CashBankMaster.Id;
                            cashBankDetail.CreatedDate = DateTime.UtcNow;
                            cashBankDetail.UpdatedDate = DateTime.UtcNow;
                            cashBankDetail.CreatedById = userId;
                            cashBankDetail.UpdatedById = userId;

                            _cashBankDetail.Add(cashBankDetail);

                            accountTransactionMaster.GlCode = model.CashBankMaster.LedgerId;
                            accountTransactionDetail.GlCode = cashBankDetail.LedgerId;

                            accountTransactionDetail.SlCode = item.SubLedgerId;

                            accountTransactionMaster.ReferenceId = model.CashBankMaster.Id;
                            accountTransactionDetail.ReferenceId = model.CashBankMaster.Id;

                            accountTransactionMaster.VNo = model.CashBankMaster.VoucherNo;
                            accountTransactionDetail.VNo = model.CashBankMaster.VoucherNo;

                            accountTransactionMaster.VDate = model.CashBankMaster.VoucherDate;
                            accountTransactionDetail.VDate = model.CashBankMaster.VoucherDate;

                            accountTransactionMaster.VMiti = model.CashBankMaster.VoucherMiti;
                            accountTransactionDetail.VMiti = model.CashBankMaster.VoucherMiti;


                            accountTransactionMaster.CrRate = Convert.ToDecimal(model.CashBankMaster.Rate);
                            accountTransactionDetail.CrRate = Convert.ToDecimal(model.CashBankMaster.Rate);

                            accountTransactionMaster.CrCode = Convert.ToString(model.CashBankMaster.CurrencyId);
                            accountTransactionDetail.CrCode = Convert.ToString(model.CashBankMaster.CurrencyId);

                            accountTransactionMaster.Narration = cashBankDetail.Narration;
                            accountTransactionDetail.Narration = cashBankDetail.Narration;

                            accountTransactionMaster.Remarks = model.CashBankMaster.Remarks;
                            accountTransactionDetail.Remarks = model.CashBankMaster.Remarks;

                            accountTransactionMaster.SNo = sno;
                            accountTransactionDetail.SNo = sno;

                            var isCashBank = _ledger.GetById(model.CashBankMaster.LedgerId).IsCashBank;
                            if (isCashBank)
                                accountTransactionMaster.CbCode = model.CashBankMaster.LedgerId;
                            var isCashBankDetail = _ledger.GetById(model.CashBankMaster.LedgerId).IsCashBank;
                            if (isCashBankDetail)
                                accountTransactionDetail.CbCode = model.CashBankMaster.LedgerId;

                            accountTransactionMaster.Source =
                                StringEnum.Parse(typeof(KRBAccounting.Enums.ModuleEnum), "Cash/Bank Book").ToString();
                            accountTransactionDetail.Source =
                                StringEnum.Parse(typeof(KRBAccounting.Enums.ModuleEnum), "Cash/Bank Book").ToString();

                            accountTransactionMaster.CreatedById = userId;
                            accountTransactionDetail.CreatedById = userId;
                            accountTransactionMaster.FYId = this.FiscalYear.Id;
                            accountTransactionDetail.FYId = this.FiscalYear.Id;

                            accountTransactionMaster.BranchId = CurrentBranchId;
                            accountTransactionDetail.BranchId = CurrentBranchId;

                            _accountTransaction.Add(accountTransactionMaster);
                            _accountTransaction.Add(accountTransactionDetail);

                            sno++;
                        }
                    }
                    var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.CashBankMaster);
                    if (data != null)
                    {
                        foreach (var udfEntry in data)
                        {
                            var value = formCollection[udfEntry.FieldName];
                            var i = new UDFData()
                            {
                                ReferenceId = model.CashBankMaster.Id,
                                UdfId = udfEntry.Id,
                                Value = value,
                                SourceId = (int)EntryModuleEnum.CashBankMaster
                            };

                            _udfData.Add(i);
                        }
                    }
                    return Json(new { success = true, isEdit = false });
                }
                return Json(new { success = false, msg = "Cash/Bank Voucher No. Cannot be Duplicate" });
            }
            return Json(new { success = false, msg = "Please fill all the required values" });
        }

        [CheckPermission(Permissions = "Edit", Module = "CB")]
        public ActionResult CashBankVoucherEdit(int id)
        {
            var data = _cashBankMaster.GetById(id);
            var viewmodel = base.CreateViewModel<CashBankVoucherAddViewModel>();
            viewmodel.CashBankMaster = data;
            viewmodel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.CashBankMaster).ToList();
            ViewBag.Id = data.Id;
            viewmodel.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                ViewBag.EntryControl = entryControl;
            }
            if (base.SystemControl.DateType == 1)
            {
                viewmodel.VoucherDate = data.VoucherDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewmodel.VoucherDate = data.VoucherMiti;

            }
            if (data.ChequeDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewmodel.ChequeDate = Convert.ToDateTime(data.ChequeDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewmodel.ChequeDate = data.ChequeMiti;
                }
            }
            viewmodel.CashBankMaster.CurrencyName = "";
            if (data.CurrencyId != null)
            {
                var CurrId = Convert.ToInt32(data.CurrencyId);
                viewmodel.CashBankMaster.CurrencyName = _currency.GetById(x => x.Id == CurrId).Code;
                viewmodel.CashBankMaster.CurrencyId = _currency.GetById(x => x.Id == CurrId).Id;
                // viewmodel.CashBankMaster.CurrencyName = _currency.GetById(x => x.Id == data.CurrencyId).Code;
            }
            viewmodel.RecAmt = data.CashBankDetails.Where(x => x.AmountType == 1).Sum(x => x.Amount);
            viewmodel.PayAmt = data.CashBankDetails.Where(x => x.AmountType == 2).Sum(x => x.Amount);
            viewmodel.NetAmt = viewmodel.RecAmt - viewmodel.PayAmt;
            viewmodel.NetAmtRs = viewmodel.NetAmt;
            return PartialView(viewmodel);
        }

        [HttpPost]
        public ActionResult CashBankVoucherEdit(IEnumerable<CashBankDetail> cashBankDetailEntry, CashBankVoucherAddViewModel model, FormCollection formCollection)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.CashBankMaster.UpdatedDate = DateTime.UtcNow;
            model.CashBankMaster.CreatedById = userId;
            model.CashBankMaster.UpdatedById = userId;
            model.CashBankMaster.FYid = this.FiscalYear.Id;
            if (base.SystemControl.DateType == 1)
            {
                model.CashBankMaster.VoucherDate = model.VoucherDate.ToDate();
                model.CashBankMaster.VoucherMiti = NepaliDateService.NepaliDate(model.CashBankMaster.VoucherDate).Date;

            }
            else
            {
                model.CashBankMaster.VoucherMiti = model.VoucherDate;
                model.CashBankMaster.VoucherDate = NepaliDateService.NepalitoEnglishDate(model.VoucherDate);

            }
            if (!string.IsNullOrEmpty(model.ChequeDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.CashBankMaster.ChequeDate = model.ChequeDate.ToDate();
                    model.CashBankMaster.ChequeMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.CashBankMaster.ChequeDate)).Date;
                }
                else
                {
                    model.CashBankMaster.ChequeMiti = model.ChequeDate;
                    model.CashBankMaster.ChequeDate = NepaliDateService.NepalitoEnglishDate(model.ChequeDate);
                }
            }
            _cashBankMaster.Update(model.CashBankMaster);
            var source = StringEnum.Parse(typeof(ModuleEnum), "Cash/Bank Book").ToString();
            _accountTransaction.Delete(x => x.ReferenceId == model.CashBankMaster.Id && x.Source == source);
            _cashBankDetail.Delete(x => x.MasterId == model.CashBankMaster.Id);
            int sno = 1;
            foreach (var item in cashBankDetailEntry)
            {
                if (item.LedgerId != 0 && item.AmountType != 0 && item.Amount != 0)
                {
                    var cashBankDetail = new CashBankDetail();
                    var accountTransactionMaster = new AccountTransaction();
                    var accountTransactionDetail = new AccountTransaction();
                    cashBankDetail.LedgerId = item.LedgerId;
                    if (item.AmountType == Convert.ToInt32(KRBAccounting.Enums.AmountTypeEnum.Receipt))
                    {

                        cashBankDetail.Amount = Convert.ToDecimal(item.Amount);
                        cashBankDetail.AmountType = Convert.ToInt32(KRBAccounting.Enums.AmountTypeEnum.Receipt);
                        accountTransactionMaster.DrAmt = Convert.ToDecimal(item.Amount);
                        accountTransactionDetail.CrAmt = Convert.ToDecimal(item.Amount);
                        if (model.CashBankMaster.CurrencyId != null && model.CashBankMaster.CurrencyId != 0)
                        {
                            accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.CashBankMaster.Rate);
                            accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.CashBankMaster.Rate);
                        }
                        else
                        {
                            accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(item.Amount);
                            accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.Amount);

                        }


                        //Need to add enum for amounttype
                    }
                    else
                    {
                        cashBankDetail.Amount = Convert.ToDecimal(item.Amount);
                        cashBankDetail.AmountType = Convert.ToInt32(KRBAccounting.Enums.AmountTypeEnum.Payment);
                        accountTransactionMaster.CrAmt = Convert.ToDecimal(item.Amount);
                        accountTransactionDetail.DrAmt = Convert.ToDecimal(item.Amount);
                        if (model.CashBankMaster.CurrencyId != null && model.CashBankMaster.CurrencyId != 0)
                        {
                            accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.CashBankMaster.Rate);
                            accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.CashBankMaster.Rate);

                        }
                        accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(item.Amount);
                        accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.Amount);
                    }

                    //cashBankDetail.SlCode = 0;
                    cashBankDetail.SlCode = item.SlCode;
                    cashBankDetail.Narration = item.Narration;
                    cashBankDetail.MasterId = model.CashBankMaster.Id;
                    cashBankDetail.CreatedDate = DateTime.UtcNow;
                    cashBankDetail.UpdatedDate = DateTime.UtcNow;
                    cashBankDetail.CreatedById = userId;
                    cashBankDetail.UpdatedById = userId;

                    _cashBankDetail.Add(cashBankDetail);

                    accountTransactionMaster.GlCode = model.CashBankMaster.LedgerId;
                    accountTransactionDetail.GlCode = cashBankDetail.LedgerId;

                    accountTransactionDetail.SlCode = cashBankDetail.SlCode;
                    accountTransactionMaster.ReferenceId = model.CashBankMaster.Id;
                    accountTransactionDetail.ReferenceId = model.CashBankMaster.Id;

                    accountTransactionMaster.VNo = model.CashBankMaster.VoucherNo;
                    accountTransactionDetail.VNo = model.CashBankMaster.VoucherNo;

                    accountTransactionMaster.VDate = model.CashBankMaster.VoucherDate;
                    accountTransactionDetail.VDate = model.CashBankMaster.VoucherDate;

                    accountTransactionMaster.VMiti = model.CashBankMaster.VoucherMiti;
                    accountTransactionDetail.VMiti = model.CashBankMaster.VoucherMiti;

                    accountTransactionMaster.CrRate = Convert.ToDecimal(model.CashBankMaster.Rate);
                    accountTransactionDetail.CrRate = Convert.ToDecimal(model.CashBankMaster.Rate);

                    accountTransactionMaster.CrCode = Convert.ToString(model.CashBankMaster.CurrencyId);
                    accountTransactionDetail.CrCode = Convert.ToString(model.CashBankMaster.CurrencyId);

                    accountTransactionMaster.Narration = cashBankDetail.Narration;
                    accountTransactionDetail.Narration = cashBankDetail.Narration;

                    accountTransactionMaster.Remarks = model.CashBankMaster.Remarks;
                    accountTransactionDetail.Remarks = model.CashBankMaster.Remarks;

                    accountTransactionMaster.SNo = sno;
                    accountTransactionDetail.SNo = sno;

                    var isCashBank = _ledger.GetById(model.CashBankMaster.LedgerId).IsCashBank;
                    if (isCashBank)
                        accountTransactionMaster.CbCode = model.CashBankMaster.LedgerId;
                    var isCashBankDetail = _ledger.GetById(model.CashBankMaster.LedgerId).IsCashBank;
                    if (isCashBankDetail)
                        accountTransactionDetail.CbCode = model.CashBankMaster.LedgerId;

                    accountTransactionMaster.Source =
                        StringEnum.Parse(typeof(KRBAccounting.Enums.ModuleEnum), "Cash/Bank Book").ToString();
                    accountTransactionDetail.Source =
                        StringEnum.Parse(typeof(KRBAccounting.Enums.ModuleEnum), "Cash/Bank Book").ToString();

                    accountTransactionMaster.CreatedById = userId;
                    accountTransactionDetail.CreatedById = userId;
                    accountTransactionMaster.FYId = this.FiscalYear.Id;
                    accountTransactionDetail.FYId = this.FiscalYear.Id;

                    accountTransactionMaster.BranchId = CurrentBranchId;
                    accountTransactionDetail.BranchId = CurrentBranchId;

                    _accountTransaction.Add(accountTransactionMaster);
                    _accountTransaction.Add(accountTransactionDetail);

                    sno++;
                }
            }
            _udfData.Delete(x => x.ReferenceId == model.CashBankMaster.Id && x.SourceId == (int)EntryModuleEnum.CashBankMaster);
            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.CashBankMaster);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = model.CashBankMaster.Id,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.CashBankMaster
                    };

                    _udfData.Add(i);
                }
            }
            return Json(new { success = true, isEdit = true });
        }

        //public ActionResult GetDocByCashBank()
        //{
        //    var data = _docNumering.Filter(x => x.ModuleName == "CB").Select(x => new
        //    {
        //        Id = x.Id,
        //        Description = x.DocDesc,

        //    });
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        [CheckPermission(Permissions = "Approve", Module = "CB")]
        [HttpPost]
        public ActionResult CashBankVourcherApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _cashBankMaster.GetById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _cashBankMaster.Update(data);
            return null;
        }

        [CheckPermission(Permissions = "Create", Module = "CB")]
        public ActionResult GetCashBankDetailEntry()
        {
            var viewmodel = new CashBankDetailEntryViewModel();
            viewmodel.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                viewmodel.EntryControl = entryControl;
            }
            return PartialView("_PartialCashBankDetailEntry", viewmodel);
        }

        public ActionResult CheckCashBankVno(string Vno)
        {
            var number = false;

            var data = _cashBankMaster.GetById(x => x.VoucherNo == Vno);
            if (data == null)
                number = true;
            return Json(number, JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Edit", Module = "CB")]
        public ActionResult GetCashBankDetailEdit()
        {
            var data = new CashBankDetail();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                ViewBag.EntryControl = entryControl;
            }
            return PartialView("_PartialCashBankDetailEdit", data);
        }

        public JsonResult GetCurrentBalance(int glCode)
        {
            try
            {
                var balance = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=0, @glCode=" + glCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
                return Json(balance, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetCashBank()
        {
            var data = _ledger.Filter(x => x.IsCashBank && !x.IsDeleted).Select(x => new
            {
                Id = x.Id,
                Description = x.AccountName
            }).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGLCode(int id)
        {
            var data = _ledger.GetById(x => x.Id == id).ShortName;
            return Json(data ?? null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGLDetailCurrBalance()
        {
            var data = _ledger.Filter(x => x.IsCashBank && !x.IsDeleted).Select(x => new
            {
                Id = x.Id,
                Description = x.AccountName
            }).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DisableCheque(int glcode)
        {
            var data = _ledger.GetById(x => x.Id == glcode).IsCashBook;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Journal Voucher
        //public ActionResult GetDocByJournalVoucher()
        //{

        //    var data = _docNumering.Filter(x => x.ModuleName == "JV").Select(x => new
        //    {
        //        Id = x.Id,
        //        Description = x.DocDesc,

        //    });
        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}

        public JsonResult CheckJVNumberInJournalVoucher(string JVNumber, int? Id)
        {
            var feeterm = new JournalVoucher();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _journalVoucher.GetById(x => x.Id == Id);
                if (data.JVNumber.ToLower().Trim() != JVNumber.ToLower().Trim())
                {
                    feeterm =
                        _journalVoucher.GetById(x => x.JVNumber.ToLower().Trim() == JVNumber.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _journalVoucher.GetById(x => x.JVNumber.ToLower().Trim() == JVNumber.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new JournalVoucher();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Navigate", Module = "JV")]
        public ActionResult JournalVoucher()
        {
            ViewBag.UserRight = base.UserRight("JV");
            return View();
        }

        [CheckPermission(Permissions = "Navigate", Module = "JV")]
        public ActionResult JournalVoucherList()
        {
            ViewBag.UserRight = base.UserRight("JV");
            var viewModel = new JournalVoucherViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var listPending = _journalVoucher.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //var listAccepted = _journalVoucher.Filter(x => x.IsApproved == true && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            // viewModel.AcceptedList = listAccepted;
            viewModel.PendingList = listPending;
            return PartialView(viewModel);
        }

        public ActionResult JournalVoucherPendingList(int pageNo)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var list = _journalVoucher.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            return PartialView(list);
        }

        //public ActionResult JournalVoucherAcceptedList(int pageNo)
        //{
        //    var list = _journalVoucher.Filter(x => x.IsApproved == true && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
        //    return PartialView(list);
        //}

        public ActionResult JournalVoucherDelete(int id)
        {
            var model = _journalVoucher.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _journalVoucher.Update(model);
            _accountTransaction.Delete(x => x.Source == "JV" && x.ReferenceId == id);
            return Json(true);
        }

        public ActionResult CheckFiscalyearDateinjournalVoucher(string Date)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(Date).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(Date);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }

                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                // return Json(false, JsonRequestBehavior.AllowGet);
                return Content("False");
            }

        }


        [CheckPermission(Permissions = "Create", Module = "JV")]
        public ActionResult JournalVoucherAdd()
        {
            var viewmodel = base.CreateViewModel<JournalVoucherAddViewModel>();
            viewmodel.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                viewmodel.EntryControl = entryControl;
            }

            viewmodel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.JournalVoucherMaster).ToList();
            viewmodel.JournalVoucher = new JournalVoucher();

            if (base.SystemControl.IsCurrDate)
            {
                viewmodel.Date = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _journalVoucher.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewmodel.Date = base.SystemControl.DateType == 1 ? data.JVDate.ToShortDateString() : data.JVMiti;
                }
                else
                {
                    viewmodel.Date = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            return PartialView(viewmodel);
        }

        [HttpPost]
        public ActionResult JournalVoucherAdd(IEnumerable<JournalVoucherDetailEntryViewModel> journalVoucherDetailEntry, JournalVoucherAddViewModel model, FormCollection formCollection)
        {
            //var vNo = UtilityService.GetDocumentNumbering(model.JournalVoucher.DocId);
            decimal rate = 0;

            if (model.JournalVoucher.CurrencyId != 0)
            {
                var Code = _currency.GetById(model.JournalVoucher.CurrencyId);
                model.JournalVoucher.CurCode = Convert.ToString(Code.Id);
                // model.JournalVoucher.CurCode = _currency.GetById(model.JournalVoucher.CurrencyId).Code;
            }
            if (journalVoucherDetailEntry != null && journalVoucherDetailEntry.Count() != 0)
            {
                var vNo = model.JournalVoucher.JVNumber;
                if (model.JournalVoucher.DocId != null)
                {
                    var docId = Convert.ToInt32(model.JournalVoucher.DocId);
                    vNo = UtilityService.GetDocumentNumbering(docId);
                }
                var journalVoucher = _journalVoucher.GetById(x => x.JVNumber == vNo && !x.IsDeleted);
                if (base.SystemControl.DateType == 1)
                {
                    model.JournalVoucher.JVDate = Convert.ToDateTime(model.Date);
                    model.JournalVoucher.JVMiti = NepaliDateService.NepaliDate(model.JournalVoucher.JVDate).Date;
                }
                else
                {
                    model.JournalVoucher.JVMiti = model.Date;
                    model.JournalVoucher.JVDate = NepaliDateService.NepalitoEnglishDate(model.Date);
                }

                if (journalVoucher == null)
                {
                    var userId = _authentication.GetAuthenticatedUser().Id;
                    //Temporary
                    model.JournalVoucher.CreatedDate = DateTime.UtcNow;
                    model.JournalVoucher.CreatedById = userId;
                    model.JournalVoucher.FYId = this.FiscalYear.Id;
                    model.JournalVoucher.JVNumber = vNo;
                    model.JournalVoucher.BranchId = CurrentBranchId;
                    model.JournalVoucher.IsApproved = false;
                    _journalVoucher.Add(model.JournalVoucher);

                    if (model.JournalVoucher.DocId != null)
                    {
                        var documentnumber = _docNumering.GetById(Convert.ToInt32(model.JournalVoucher.DocId));
                        documentnumber.DocCurrNo += 1;
                        _docNumering.Update(documentnumber);
                    }
                    int sno = 1;
                    var source = StringEnum.Parse(typeof(ModuleEnum), "Journal Voucher").ToString();

                    foreach (var item in journalVoucherDetailEntry)
                    {
                        if (item.LedgerId != 0 && (item.DrAmount != null || item.CrAmount != null))
                        {
                            var journalVoucherDetail = new JournalVoucherDetail();
                            var accountTransaction = new AccountTransaction();

                            decimal drAmt = 0;
                            decimal drLocalAmt = 0;
                            decimal crAmt = 0;
                            decimal crLocalAmt = 0;

                            if (item.DrAmount != null)
                            {
                                journalVoucherDetail.Amount = Convert.ToDecimal(item.DrAmount);
                                journalVoucherDetail.JVAmountType = Convert.ToInt32(KRBAccounting.Enums.JVAmountTypeEnum.D);

                                accountTransaction.DrAmt = journalVoucherDetail.Amount;
                                drAmt = journalVoucherDetail.Amount;
                                if (model.JournalVoucher.CurrencyId != null && model.JournalVoucher.CurrencyId != 0)
                                {
                                    accountTransaction.LocalDrAmt = journalVoucherDetail.Amount * (decimal)Convert.ToDouble(model.JournalVoucher.CurRate);
                                    drLocalAmt = journalVoucherDetail.Amount * (decimal)Convert.ToDouble(model.JournalVoucher.CurRate);
                                }
                                else
                                {
                                    drLocalAmt = journalVoucherDetail.Amount;
                                    accountTransaction.LocalDrAmt = journalVoucherDetail.Amount;

                                }


                                // posttoac()

                            }
                            else
                            {
                                journalVoucherDetail.Amount = Convert.ToDecimal(item.CrAmount);
                                journalVoucherDetail.JVAmountType = Convert.ToInt32(KRBAccounting.Enums.JVAmountTypeEnum.C);
                                accountTransaction.CrAmt = journalVoucherDetail.Amount;
                                crAmt = journalVoucherDetail.Amount;
                                if (model.JournalVoucher.CurRate != null && model.JournalVoucher.CurRate != 0)
                                {
                                    accountTransaction.LocalCrAmt = journalVoucherDetail.Amount * (decimal)Convert.ToDouble(model.JournalVoucher.CurRate);
                                    crLocalAmt = journalVoucherDetail.Amount * (decimal)Convert.ToDouble(model.JournalVoucher.CurRate);
                                }
                                else
                                {
                                    accountTransaction.LocalCrAmt = journalVoucherDetail.Amount;
                                    crLocalAmt = journalVoucherDetail.Amount;

                                }




                            }
                            journalVoucherDetail.SlCode = item.SubLedgerId;
                            journalVoucherDetail.Narration = item.Narration;
                            journalVoucherDetail.JVMasterId = model.JournalVoucher.Id;
                            journalVoucherDetail.GlCode = item.LedgerId;

                            _journalVoucherDetail.Add(journalVoucherDetail);

                            //Add Account Transaction
                            accountTransaction.VNo = model.JournalVoucher.JVNumber;
                            accountTransaction.VDate = Convert.ToDateTime(model.JournalVoucher.JVDate);
                            accountTransaction.VMiti = NepaliDateService.NepaliDate(model.JournalVoucher.JVDate).Date;
                            accountTransaction.ReferenceId = model.JournalVoucher.Id;
                            accountTransaction.GlCode = item.LedgerId;
                            accountTransaction.Narration = item.Narration;
                            accountTransaction.Remarks = model.JournalVoucher.Remarks;
                            accountTransaction.Source = StringEnum.Parse(typeof(ModuleEnum), "Journal Voucher").ToString();
                            accountTransaction.SlCode = journalVoucherDetail.SlCode;
                            accountTransaction.SNo = sno;
                            accountTransaction.CreatedById = userId;
                            accountTransaction.FYId = FiscalYear.Id;
                            accountTransaction.SlCode = item.SubLedgerId;
                            accountTransaction.BranchId = CurrentBranchId;
                            accountTransaction.CrRate = Convert.ToDecimal(model.JournalVoucher.CurRate);
                            accountTransaction.CrCode = model.JournalVoucher.CurCode;
                            var isCashBank = _ledger.GetById(item.LedgerId).IsCashBank;
                            if (isCashBank)
                                accountTransaction.CbCode = item.LedgerId;
                            _accountTransaction.Add(accountTransaction);

                            sno++;
                        }
                    }
                    var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.JournalVoucherMaster);
                    if (data != null)
                    {
                        foreach (var udfEntry in data)
                        {
                            var value = formCollection[udfEntry.FieldName];
                            var i = new UDFData()
                            {
                                ReferenceId = model.JournalVoucher.Id,
                                UdfId = udfEntry.Id,
                                Value = value,
                                SourceId = (int)EntryModuleEnum.JournalVoucherMaster
                            };

                            _udfData.Add(i);
                        }
                    }
                    return Json(new { success = true, isEdit = false });
                }

                return Json(new { success = false, msg = "The Voucher No. should be unique." });
            }
            return Json(new { success = false, msg = "Please Fill all the required Fields." });
        }

        [CheckPermission(Permissions = "Edit", Module = "JV")]
        public ActionResult JournalVoucherEdit(int id)
        {

            var data = _journalVoucher.GetById(id);
            var viewmodel = base.CreateViewModel<JournalVoucherAddViewModel>();
            viewmodel.JournalVoucher = data;
            if (base.SystemControl.DateType == 1)
            {
                viewmodel.Date = data.JVDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewmodel.Date = data.JVMiti;
            }

            if (!string.IsNullOrEmpty(data.CurCode))
            {
                var CurrId = Convert.ToInt32(data.CurCode);
                viewmodel.JournalVoucher.CurCode = _currency.GetById(x => x.Id == CurrId).Code;
                viewmodel.JournalVoucher.CurrencyId = _currency.GetById(x => x.Id == CurrId).Id;
                // viewmodel.JournalVoucher.CurCode = data.CurCode;
                //  viewmodel.JournalVoucher.CurrencyId = _currency.GetById(x => x.Code == data.CurCode).Id;
            }
            // viewmodel.Date = data.JVMiti.ToString();
            viewmodel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.JournalVoucherMaster).ToList();
            ViewBag.Id = data.Id;
            ViewBag.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                ViewBag.EntryControl = entryControl;
            }
            return PartialView(viewmodel);
        }
        [HttpPost]
        public ActionResult JournalVoucherEdit(IEnumerable<JournalVoucherDetail> journalVoucherDetailEntry, JournalVoucherAddViewModel model, FormCollection formCollection)
        {
            if (!ModelState.IsValid)
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    if (modelState.Errors.Any())
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                        }
                    }
                }
            }
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.JournalVoucher.FYId = FiscalYear.Id;
            if (base.SystemControl.DateType == 1)
            {
                model.JournalVoucher.JVDate = Convert.ToDateTime(model.Date);
                model.JournalVoucher.JVMiti = NepaliDateService.NepaliDate(model.JournalVoucher.JVDate).Date;
            }
            else
            {
                model.JournalVoucher.JVMiti = model.Date;
                model.JournalVoucher.JVDate = NepaliDateService.NepalitoEnglishDate(model.Date);
            }
            if (model.JournalVoucher.CurrencyId != 0)
            {
                var Code = _currency.GetById(model.JournalVoucher.CurrencyId);
                model.JournalVoucher.CurCode = Convert.ToString(Code.Id);
                // model.JournalVoucher.CurCode = _currency.GetById(model.JournalVoucher.CurrencyId).Code;
            }
            _journalVoucher.Update(model.JournalVoucher);
            var source = StringEnum.Parse(typeof(ModuleEnum), "Journal Voucher").ToString();
            _accountTransaction.Delete(x => x.ReferenceId == model.JournalVoucher.Id && x.Source == source);
            _journalVoucherDetail.Delete(x => x.JVMasterId == model.JournalVoucher.Id);

            int sno = 1;
            foreach (var item in journalVoucherDetailEntry)
            {
                if (item.GlCode != 0 && item.JVAmountType != 0 || item.Amount != 0)
                {
                    var journalVoucherDetail = new JournalVoucherDetail();
                    var accountTransaction = new AccountTransaction();


                    journalVoucherDetail.Amount = Convert.ToDecimal(item.Amount);
                    journalVoucherDetail.JVAmountType = item.JVAmountType;
                    journalVoucherDetail.Narration = item.Narration;
                    journalVoucherDetail.JVMasterId = model.JournalVoucher.Id;
                    journalVoucherDetail.SlCode = item.SlCode;
                    journalVoucherDetail.GlCode = item.GlCode;
                    _journalVoucherDetail.Add(journalVoucherDetail);

                    //Add Account Transaction
                    if (item.JVAmountType == Convert.ToInt32(JVAmountTypeEnum.D))
                    {
                        accountTransaction.DrAmt = journalVoucherDetail.Amount;
                        accountTransaction.LocalDrAmt = journalVoucherDetail.Amount;
                        if (model.JournalVoucher.CurrencyId != null && model.JournalVoucher.CurrencyId != 0)
                        {
                            accountTransaction.LocalDrAmt = journalVoucherDetail.Amount * Convert.ToDecimal(model.JournalVoucher.CurRate);

                        }
                    }
                    else
                    {
                        accountTransaction.CrAmt = journalVoucherDetail.Amount;
                        accountTransaction.LocalCrAmt = journalVoucherDetail.Amount;
                        if (model.JournalVoucher.CurrencyId != null && model.JournalVoucher.CurrencyId != 0)
                        {
                            accountTransaction.LocalCrAmt = journalVoucherDetail.Amount * Convert.ToDecimal(model.JournalVoucher.CurRate);
                        }

                    }
                    accountTransaction.VNo = model.JournalVoucher.JVNumber;
                    accountTransaction.VDate = Convert.ToDateTime(model.JournalVoucher.JVDate);
                    accountTransaction.VMiti = NepaliDateService.NepaliDate(model.JournalVoucher.JVDate).Date;
                    accountTransaction.GlCode = item.GlCode;
                    accountTransaction.Narration = item.Narration;
                    accountTransaction.Remarks = model.JournalVoucher.Remarks;
                    accountTransaction.Source = source;
                    accountTransaction.ReferenceId = model.JournalVoucher.Id;
                    accountTransaction.SlCode = journalVoucherDetail.SlCode;
                    accountTransaction.SNo = sno;
                    accountTransaction.CreatedById = userId;
                    accountTransaction.FYId = FiscalYear.Id;
                    accountTransaction.BranchId = CurrentBranchId;
                    accountTransaction.CrRate = Convert.ToDecimal(model.JournalVoucher.CurRate);
                    accountTransaction.CrCode = model.JournalVoucher.CurCode;
                    var isCashBank = _ledger.GetById(item.GlCode).IsCashBank;
                    if (isCashBank)
                        accountTransaction.CbCode = item.GlCode;
                    _accountTransaction.Add(accountTransaction);

                    sno++;

                }
            }
            _udfData.Delete(x => x.ReferenceId == model.JournalVoucher.Id && x.SourceId == (int)EntryModuleEnum.JournalVoucherMaster);
            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.JournalVoucherMaster);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = model.JournalVoucher.Id,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.JournalVoucherMaster
                    };

                    _udfData.Add(i);
                }
            }
            return Json(new { success = true, isEdit = true });
        }

        [HttpPost]
        [CheckPermission(Permissions = "Approve", Module = "JV")]
        public ActionResult JournalVoucherApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _journalVoucher.GetById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _journalVoucher.Update(data);
            return null;
        }

        [CheckPermission(Permissions = "Create", Module = "JV")]
        public ActionResult GetJournalVoucherDetailEntry()
        {
            var viewModel = new JournalVoucherDetailEntryViewModel();
            viewModel.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
                viewModel.EntryControl = entryControl;
            return PartialView("_PartialJournalVoucherDetailEntry", viewModel);
        }
        [CheckPermission(Permissions = "Edit", Module = "JV")]
        public ActionResult GetJournalVoucherDetailEdit()
        {
            var data = new JournalVoucherDetail();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                ViewBag.EntryControl = entryControl;
            }
            return PartialView("_PartialJournalVoucherDetailEdit", data);
        }
        #endregion

        #region Debit Note
        public JsonResult CheckNumberInDebitNote(string Number, int? Id)
        {
            var feeterm = new DebitNoteMaster();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _debitNoteMaster.GetById(x => x.Id == Id);
                if (data.Number.ToLower().Trim() != Number.ToLower().Trim())
                {
                    feeterm =
                        _debitNoteMaster.GetById(x => x.Number.ToLower().Trim() == Number.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _debitNoteMaster.GetById(x => x.Number.ToLower().Trim() == Number.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new DebitNoteMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Navigate", Module = "DN")]
        public ActionResult DebitNote()
        {
          
            return View();
        }

        [CheckPermission(Permissions = "Navigate", Module = "DN")]
        public ActionResult DebitNoteList()
        {
            ViewBag.UserRight = base.UserRight("DN");
            var viewModel = new DebitNoteViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var listPending = _debitNoteMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //var listAccepted = _debitNoteMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //viewModel.AcceptedList = listAccepted;
            viewModel.PendingList = listPending;
            return PartialView(viewModel);
        }

        public ActionResult DebitNotePendingList(int pageNo)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var list = _debitNoteMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView(list);
        }

        //public ActionResult DebitNoteAcceptedList(int pageNo)
        //{
        //    var list = _debitNoteMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
        //    return PartialView(list);
        //}

        [HttpPost]
        public ActionResult DebitNoteDelete(int id)
        {
            var model = _debitNoteMaster.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _debitNoteMaster.Update(model);
            _accountTransaction.Delete(x => x.Source == "DN" && x.ReferenceId == id);
            _stockTransaction.Delete(x => x.Source == "DN" && x.ReferenceId == id);
            return Json(true);
        }

        public ActionResult CheckFiscalyearDateinDebitNote(string Date)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(Date).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(Date);
                   }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {

                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }
        [CheckPermission(Permissions = "Create", Module = "DN")]
        public ActionResult DebitNoteAdd()
        {
            //var data = _debitNoteMaster.GetAll().LastOrDefault();
            var viewmodel = base.CreateViewModel<DebitNoteAddViewModel>();
            viewmodel.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                viewmodel.EntryControl = entryControl;
            }
            //viewmodel.Date = data != null ? data.Date : DateTime.UtcNow;
            viewmodel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.DebitNoteMaster).ToList();
            viewmodel.DebitNoteMaster = new DebitNoteMaster();
            if (base.SystemControl.IsCurrDate)
            {
                viewmodel.Date = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _debitNoteMaster.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewmodel.Date = base.SystemControl.DateType == 1 ? data.Date.ToShortDateString() : data.Miti;
                }
                else
                {
                    viewmodel.Date = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }


            return PartialView(viewmodel);
        }

        [HttpPost]
        public ActionResult DebitNoteAdd(IEnumerable<DebitNoteDetailEntryViewModel> debitNoteDetailEntry, DebitNoteAddViewModel model, FormCollection formCollection)
        {
            var vNo = model.DebitNoteMaster.Number;
            if (model.DebitNoteMaster.DocId != null)
            {
                var docId = Convert.ToInt32(model.DebitNoteMaster.DocId);
                vNo = UtilityService.GetDocumentNumbering(docId);
            }
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var debitNote = _debitNoteMaster.GetById(x => x.Number == vNo && !x.IsDeleted && x.FYId == fiscalYear.Id);

            if (base.SystemControl.DateType == 1)
            {
                model.DebitNoteMaster.Date = Convert.ToDateTime(model.Date);
                model.DebitNoteMaster.Miti = NepaliDateService.NepaliDate(model.DebitNoteMaster.Date).Date;
            }
            else
            {
                model.DebitNoteMaster.Miti = model.Date;
                model.DebitNoteMaster.Date = NepaliDateService.NepalitoEnglishDate(model.Date);
            }
            if (base.SystemControl.DateType == 1)
            {
                model.DebitNoteMaster.RefBillDate = Convert.ToDateTime(model.RefDate);
                model.DebitNoteMaster.RefBillMiti = NepaliDateService.NepaliDate(model.RefDate.ToDate()).Date;
            }
            else
            {
                model.DebitNoteMaster.RefBillMiti = model.RefDate;
                model.DebitNoteMaster.RefBillDate = NepaliDateService.NepalitoEnglishDate(model.RefDate);
            }
            if (model.DebitNoteMaster.CurrencyId != 0)
            {
                var Code = _currency.GetById(model.DebitNoteMaster.CurrencyId);
                model.DebitNoteMaster.CurCode = Convert.ToString(Code.Id);
                //  model.DebitNoteMaster.CurCode = _currency.GetById(model.DebitNoteMaster.CurrencyId).Code;
            }
            if (debitNote == null)
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                model.DebitNoteMaster.CreatedDate = DateTime.UtcNow;
                model.DebitNoteMaster.CreatedById = userId;
                // model.DebitNoteMaster.Date = model.Date;
                model.DebitNoteMaster.FYId = FiscalYear.Id;
                model.DebitNoteMaster.Number = vNo;
                model.DebitNoteMaster.BranchId = CurrentBranchId;
                model.DebitNoteMaster.IsApproved = false;
                _debitNoteMaster.Add(model.DebitNoteMaster);
                if (model.DebitNoteMaster.DocId != null)
                {
                    var documentnumber = _docNumering.GetById(Convert.ToInt32(model.DebitNoteMaster.DocId));
                    documentnumber.DocCurrNo += 1;
                    _docNumering.Update(documentnumber);
                }
                var Source = StringEnum.Parse(typeof(ModuleEnum), "Debit Note").ToString();
                int sno = 1;
                foreach (var item in debitNoteDetailEntry)
                {
                    if (item.GlCode != 0 && item.Amount != null)
                    {
                        var debitNoteDetail = new DebitNoteDetail();
                        var accountTransactionMaster = new AccountTransaction();
                        var accountTransactionDetail = new AccountTransaction();

                        debitNoteDetail.Amount = Convert.ToDecimal(item.Amount);
                        debitNoteDetail.Narration = item.Narration;
                        debitNoteDetail.DebitNoteMasterId = model.DebitNoteMaster.Id;
                        debitNoteDetail.GlCode = item.GlCode;
                        debitNoteDetail.SlCode = item.SubLedgerId;
                        _debitNoteDetail.Add(debitNoteDetail);

                        accountTransactionMaster.VNo = model.DebitNoteMaster.Number;
                        accountTransactionDetail.VNo = model.DebitNoteMaster.Number;

                        accountTransactionMaster.VDate = model.DebitNoteMaster.Date;
                        accountTransactionDetail.VDate = model.DebitNoteMaster.Date;

                        accountTransactionMaster.VMiti = model.DebitNoteMaster.Miti;
                        accountTransactionDetail.VMiti = model.DebitNoteMaster.Miti;

                        accountTransactionMaster.GlCode = model.DebitNoteMaster.GlCode;
                        accountTransactionDetail.GlCode = item.GlCode;

                        accountTransactionDetail.SlCode = item.SubLedgerId;

                        accountTransactionMaster.ReferenceId = model.DebitNoteMaster.Id;
                        accountTransactionDetail.ReferenceId = model.DebitNoteMaster.Id;

                        accountTransactionMaster.CrRate = Convert.ToDecimal(model.DebitNoteMaster.CurRate);
                        accountTransactionDetail.CrRate = Convert.ToDecimal(model.DebitNoteMaster.CurRate);

                        accountTransactionMaster.CrCode = model.DebitNoteMaster.CurCode;
                        accountTransactionDetail.CrCode = model.DebitNoteMaster.CurCode;

                        accountTransactionMaster.DrAmt = Convert.ToDecimal(item.Amount);
                        accountTransactionDetail.CrAmt = Convert.ToDecimal(item.Amount);

                        accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(item.Amount);
                        accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.Amount);

                        if (model.DebitNoteMaster.CurrencyId != null && model.DebitNoteMaster.CurrencyId != 0)
                        {
                            accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.DebitNoteMaster.CurRate);
                            accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.DebitNoteMaster.CurRate);
                        }

                        accountTransactionMaster.Narration = item.Narration;
                        accountTransactionDetail.Narration = item.Narration;

                        accountTransactionMaster.Remarks = model.DebitNoteMaster.Remarks;
                        accountTransactionDetail.Remarks = model.DebitNoteMaster.Remarks;

                        accountTransactionMaster.Source = Source;
                        accountTransactionDetail.Source = Source;

                        accountTransactionMaster.SNo = sno;
                        accountTransactionDetail.SNo = sno;

                        accountTransactionMaster.CreatedById = userId;
                        accountTransactionDetail.CreatedById = userId;

                        accountTransactionMaster.FYId = FiscalYear.Id;
                        accountTransactionDetail.FYId = FiscalYear.Id;

                        accountTransactionMaster.BranchId = CurrentBranchId;
                        accountTransactionDetail.BranchId = CurrentBranchId;

                        var isCashBank = _ledger.GetById(model.DebitNoteMaster.GlCode).IsCashBank;
                        if (isCashBank)
                            accountTransactionMaster.CbCode = model.DebitNoteMaster.GlCode;
                        var isCashBankDetail = _ledger.GetById(item.GlCode).IsCashBank;
                        if (isCashBankDetail)
                            accountTransactionMaster.CbCode = item.GlCode;
                        _accountTransaction.Add(accountTransactionMaster);
                        _accountTransaction.Add(accountTransactionDetail);
                        sno++;
                    }
                }
                var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.DebitNoteMaster);
                if (data != null)
                {
                    foreach (var udfEntry in data)
                    {
                        var value = formCollection[udfEntry.FieldName];
                        var i = new UDFData()
                                    {
                                        ReferenceId = model.DebitNoteMaster.Id,
                                        UdfId = udfEntry.Id,
                                        Value = value,
                                        SourceId = (int)EntryModuleEnum.DebitNoteMaster
                                    };

                        _udfData.Add(i);
                    }
                }
                return Json(new { success = true, isEdit = false });
            }
            return Json(new { success = false, msg = "Debit Note No. must be unique" });
        }

        [CheckPermission(Permissions = "Edit", Module = "DN")]
        public ActionResult DebitNoteEdit(int id)
        {
            var data = _debitNoteMaster.GetById(id);
            var viewmodel = base.CreateViewModel<DebitNoteAddViewModel>();
            if (base.SystemControl.DateType == 1)
            {
                viewmodel.Date = data.Date.ToString("MM/dd/yyyy");
            }
            else
            {
                viewmodel.Date = data.Miti;
            }
            if (base.SystemControl.DateType == 1)
            {
                viewmodel.RefDate = Convert.ToDateTime(data.RefBillDate).ToString("MM/dd/yyyy");
            }
            else
            {
                viewmodel.RefDate = data.RefBillMiti;
            }


            viewmodel.DebitNoteMaster = data;
            viewmodel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.DebitNoteMaster).ToList();
            ViewBag.Id = data.Id;
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                ViewBag.EntryControl = entryControl;
            }
            if (!string.IsNullOrEmpty(data.CurCode))
            {
                var CurrId = Convert.ToInt32(data.CurCode);
                viewmodel.DebitNoteMaster.CurCode = _currency.GetById(x => x.Id == CurrId).Code;
                viewmodel.DebitNoteMaster.CurrencyId = _currency.GetById(x => x.Id == CurrId).Id;

                //viewmodel.DebitNoteMaster.CurCode = data.CurCode;
                // viewmodel.DebitNoteMaster.CurrencyId = _currency.GetById(x => x.Code == data.CurCode).Id;
            }
            viewmodel.TotalAmt = data.DebitNoteDetails.Sum(x => x.Amount).ToString();
            viewmodel.TotalAmtRs = data.DebitNoteDetails.Sum(x => x.Amount).ToString();
            return PartialView(viewmodel);
        }

        [HttpPost]
        public ActionResult DebitNoteEdit(IEnumerable<DebitNoteDetail> debitNoteDetailEntry, DebitNoteAddViewModel model, FormCollection formCollection)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            //Temporary
            model.DebitNoteMaster.FYId = FiscalYear.Id;
            if (base.SystemControl.DateType == 1)
            {
                model.DebitNoteMaster.Date = Convert.ToDateTime(model.Date);
                model.DebitNoteMaster.Miti = NepaliDateService.NepaliDate(model.DebitNoteMaster.Date).Date;
            }
            else
            {
                model.DebitNoteMaster.Miti = model.Date;
                model.DebitNoteMaster.Date = NepaliDateService.NepalitoEnglishDate(model.Date);
            }

            if (base.SystemControl.DateType == 1)
            {
                model.DebitNoteMaster.RefBillDate = model.RefDate.ToDate();
                model.DebitNoteMaster.RefBillMiti = NepaliDateService.NepaliDate(model.RefDate.ToDate()).Date;
            }
            else
            {
                model.DebitNoteMaster.RefBillMiti = model.RefDate;
                model.DebitNoteMaster.RefBillDate = NepaliDateService.NepalitoEnglishDate(model.RefDate);
            }
            if (model.DebitNoteMaster.CurrencyId != 0)
            {
                var Code = _currency.GetById(model.DebitNoteMaster.CurrencyId);
                model.DebitNoteMaster.CurCode = Convert.ToString(Code.Id);
            }

            _debitNoteMaster.Update(model.DebitNoteMaster);
            var source = StringEnum.Parse(typeof(ModuleEnum), "Debit Note").ToString();
            _accountTransaction.Delete(x => x.ReferenceId == model.DebitNoteMaster.Id && x.Source == source);
            _debitNoteDetail.Delete(x => x.DebitNoteMasterId == model.DebitNoteMaster.Id);

            int sno = 1;
            foreach (var item in debitNoteDetailEntry)
            {
                if (item.GlCode != 0 && item.Amount != 0)
                {
                    var debitNoteDetail = new DebitNoteDetail();
                    var accountTransactionMaster = new AccountTransaction();
                    var accountTransactionDetail = new AccountTransaction();

                    debitNoteDetail.Amount = Convert.ToDecimal(item.Amount);
                    debitNoteDetail.Narration = item.Narration;
                    debitNoteDetail.DebitNoteMasterId = model.DebitNoteMaster.Id;
                    debitNoteDetail.SlCode = item.SlCode;
                    debitNoteDetail.GlCode = item.GlCode;
                    _debitNoteDetail.Add(debitNoteDetail);

                    accountTransactionMaster.VNo = model.DebitNoteMaster.Number;
                    accountTransactionDetail.VNo = model.DebitNoteMaster.Number;

                    accountTransactionMaster.VDate = model.DebitNoteMaster.Date;
                    accountTransactionDetail.VDate = model.DebitNoteMaster.Date;

                    accountTransactionMaster.VMiti = model.DebitNoteMaster.Miti;
                    accountTransactionDetail.VMiti = model.DebitNoteMaster.Miti;

                    accountTransactionMaster.GlCode = model.DebitNoteMaster.GlCode;
                    accountTransactionDetail.GlCode = item.GlCode;

                    accountTransactionDetail.SlCode = debitNoteDetail.SlCode;

                    accountTransactionMaster.CrRate = Convert.ToDecimal(model.DebitNoteMaster.CurRate);
                    accountTransactionDetail.CrRate = Convert.ToDecimal(model.DebitNoteMaster.CurRate);

                    accountTransactionMaster.CrCode = model.DebitNoteMaster.CurCode;
                    accountTransactionDetail.CrCode = model.DebitNoteMaster.CurCode;

                    accountTransactionMaster.ReferenceId = model.DebitNoteMaster.Id;
                    accountTransactionDetail.ReferenceId = model.DebitNoteMaster.Id;

                    accountTransactionMaster.DrAmt = Convert.ToDecimal(item.Amount);
                    accountTransactionDetail.CrAmt = Convert.ToDecimal(item.Amount);

                    accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(item.Amount);
                    accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.Amount);

                    if (model.DebitNoteMaster.CurrencyId != null && model.DebitNoteMaster.CurrencyId != 0)
                    {
                        accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.DebitNoteMaster.CurRate);
                        accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.DebitNoteMaster.CurRate);
                    }

                    accountTransactionMaster.Narration = item.Narration;
                    accountTransactionDetail.Narration = item.Narration;

                    accountTransactionMaster.Remarks = model.DebitNoteMaster.Remarks;
                    accountTransactionDetail.Remarks = model.DebitNoteMaster.Remarks;
                    accountTransactionMaster.Source = source;
                    accountTransactionDetail.Source = source;

                    accountTransactionMaster.SNo = sno;
                    accountTransactionDetail.SNo = sno;

                    accountTransactionMaster.CreatedById = userId;
                    accountTransactionDetail.CreatedById = userId;

                    accountTransactionMaster.FYId = FiscalYear.Id;
                    accountTransactionDetail.FYId = userId;

                    accountTransactionMaster.BranchId = CurrentBranchId;
                    accountTransactionDetail.BranchId = CurrentBranchId;

                    var isCashBank = _ledger.GetById(model.DebitNoteMaster.GlCode).IsCashBank;
                    if (isCashBank)
                        accountTransactionMaster.CbCode = model.DebitNoteMaster.GlCode;
                    var isCashBankDetail = _ledger.GetById(item.GlCode).IsCashBank;
                    if (isCashBankDetail)
                        accountTransactionDetail.CbCode = model.DebitNoteMaster.GlCode;
                    _accountTransaction.Add(accountTransactionMaster);
                    _accountTransaction.Add(accountTransactionDetail);
                    sno++;
                }
            }
            _udfData.Delete(x => x.ReferenceId == model.DebitNoteMaster.Id && x.SourceId == (int)EntryModuleEnum.DebitNoteMaster);
            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.DebitNoteMaster);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = model.DebitNoteMaster.Id,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.DebitNoteMaster
                    };

                    _udfData.Add(i);
                }
            }
            return Json(new { success = true, isEdit = true });
        }

        [CheckPermission(Permissions = "Approve", Module = "DN")]
        [HttpPost]
        public ActionResult DebitNoteApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _debitNoteMaster.GetById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _debitNoteMaster.Update(data);
            return null;
        }

        [CheckPermission(Permissions = "Create", Module = "DN")]
        public ActionResult GetDebitNoteDetailEntry()
        {
            var viewModel = new DebitNoteDetailEntryViewModel();
            viewModel.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
                viewModel.EntryControl = entryControl;
            return PartialView("_PartialDebitNoteDetailEntry", viewModel);
        }

        [CheckPermission(Permissions = "Edit", Module = "DN")]
        public ActionResult GetDebitNoteDetailEdit()
        {
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                ViewBag.EntryControl = entryControl;
            }
            var data = new DebitNoteDetail();
            return PartialView("_PartialDebitNoteDetailEdit", data);
        }

        #endregion

        #region Credit Note

        public JsonResult CheckNumberInCreditNote(string Number, int? Id)
        {
            var feeterm = new CreditNoteMaster();

            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _creditNoteMaster.GetById(x => x.Id == Id);
                if (data.Number.ToLower().Trim() != Number.ToLower().Trim())
                {
                    feeterm = _creditNoteMaster.GetById(x => x.Number.ToLower().Trim() == Number.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
                }
            }
            else
            {
                feeterm = _creditNoteMaster.GetById(x => x.Number.ToLower().Trim() == Number.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new CreditNoteMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Navigate", Module = "CN")]
        public ActionResult CreditNote()
        {
            return View();
        }

        [CheckPermission(Permissions = "Navigate", Module = "CN")]
        public ActionResult CreditNoteList()
        {
            ViewBag.UserRight = base.UserRight("CN");
            var viewModel = new CreditNoteViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var listPending = _creditNoteMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            // var listAccepted = _creditNoteMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //viewModel.AcceptedList = listAccepted;
            viewModel.PendingList = listPending;
            return PartialView(viewModel);
        }

        public ActionResult CreditNotePendingList(int pageNo)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var list = _creditNoteMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView(list);
        }

        //public ActionResult CreditNoteAcceptedList(int pageNo)
        //{
        //    var list = _creditNoteMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
        //    return PartialView(list);
        //}
        [HttpPost]
        public ActionResult CreditNoteDelete(int id)
        {
            var model = _creditNoteMaster.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _creditNoteMaster.Update(model);
            _accountTransaction.Delete(x => x.Source == "CN" && x.ReferenceId == id);
            _stockTransaction.Delete(x => x.Source == "CN" && x.ReferenceId == id);
            return Json(true);
        }

        public ActionResult CheckFiscalyearDateinCreditNote(string Date)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(Date).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(Date);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {

                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }

        [CheckPermission(Permissions = "Create", Module = "CN")]
        public ActionResult CreditNoteAdd()
        {
            //var data = _creditNoteMaster.GetMany(x => !x.IsDeleted).OrderBy(x => x.Id).LastOrDefault();
            var viewmodel = base.CreateViewModel<CreditNoteAddViewModel>();
            viewmodel.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                viewmodel.EntryControl = entryControl;
            }
            // viewmodel.Date = data != null ? data.Date : DateTime.UtcNow;
            viewmodel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.CreditNoteMaster).ToList();
            viewmodel.CreditNoteMaster = new CreditNoteMaster();
            if (base.SystemControl.IsCurrDate)
            {
                viewmodel.Date = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _creditNoteMaster.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewmodel.Date = base.SystemControl.DateType == 1 ? data.Date.ToShortDateString() : data.Miti;
                }
                else
                {
                    viewmodel.Date = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            return PartialView(viewmodel);
        }

        [HttpPost]
        public ActionResult CreditNoteAdd(IEnumerable<CreditNoteDetailEntryViewModel> CreditNoteDetailEntry, CreditNoteAddViewModel model, FormCollection formCollection)
        {
            var vNo = model.CreditNoteMaster.Number;
            if (model.CreditNoteMaster.DocId != null)
            {
                var docId = Convert.ToInt32(model.CreditNoteMaster.DocId);
                vNo = UtilityService.GetDocumentNumbering(docId);
            }

            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var creditNote = _creditNoteMaster.GetById(x => x.Number == vNo && !x.IsDeleted && x.FYId == fiscalYear.Id);

            if (base.SystemControl.DateType == 1)
            {
                model.CreditNoteMaster.Date = Convert.ToDateTime(model.Date);
                model.CreditNoteMaster.Miti = NepaliDateService.NepaliDate(model.CreditNoteMaster.Date).Date;
                model.CreditNoteMaster.RefBillDate = Convert.ToDateTime(model.RefDate);
                model.CreditNoteMaster.RefBillMiti = NepaliDateService.NepaliDate(model.RefDate.ToDate()).Date;
            }
            else
            {
                model.CreditNoteMaster.Miti = model.Date;
                model.CreditNoteMaster.Date = NepaliDateService.NepalitoEnglishDate(model.Date);
                model.CreditNoteMaster.RefBillMiti = model.RefDate;
                model.CreditNoteMaster.RefBillDate = NepaliDateService.NepalitoEnglishDate(model.RefDate);
            }
            if (model.CreditNoteMaster.CurrencyId != null && model.CreditNoteMaster.CurrencyId != 0)
            {
                var Code = _currency.GetById(model.CreditNoteMaster.CurrencyId);
                model.CreditNoteMaster.CurCode = Convert.ToString(Code.Id);
            }
            if (creditNote == null)
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                //var vNo = UtilityService.GetDocumentNumbering(model.CreditNoteMaster.DocId);
                model.CreditNoteMaster.CreatedDate = DateTime.UtcNow;
                model.CreditNoteMaster.CreatedById = userId;
                //model.CreditNoteMaster.Date = model.Date;
                model.CreditNoteMaster.FYId = FiscalYear.Id;
                model.CreditNoteMaster.BranchId = CurrentBranchId;
                _creditNoteMaster.Add(model.CreditNoteMaster);
                if (model.CreditNoteMaster.DocId != null)
                {
                    var documentnumber = _docNumering.GetById(Convert.ToInt32(model.CreditNoteMaster.DocId));
                    documentnumber.DocCurrNo += 1;
                    _docNumering.Update(documentnumber);
                }
                var source = StringEnum.Parse(typeof(ModuleEnum), "Credit Note").ToString();

                int sno = 1;
                foreach (var item in CreditNoteDetailEntry)
                {
                    if (item.GlCode != 0 && item.Amount != null)
                    {
                        var creditNoteDetail = new CreditNoteDetail();
                        var accountTransactionMaster = new AccountTransaction();
                        var accountTransactionDetail = new AccountTransaction();

                        creditNoteDetail.Amount = Convert.ToDecimal(item.Amount);
                        creditNoteDetail.Narration = item.Narration;
                        creditNoteDetail.CreditNoteMasterId = model.CreditNoteMaster.Id;
                        creditNoteDetail.GlCode = item.GlCode;
                        creditNoteDetail.SlCode = item.SubLedgerId;
                        _creditNoteDetail.Add(creditNoteDetail);

                        accountTransactionMaster.VNo = model.CreditNoteMaster.Number;
                        accountTransactionDetail.VNo = model.CreditNoteMaster.Number;

                        accountTransactionMaster.VDate = model.CreditNoteMaster.Date;
                        accountTransactionDetail.VDate = model.CreditNoteMaster.Date;

                        accountTransactionMaster.VMiti = model.CreditNoteMaster.Miti;
                        accountTransactionDetail.VMiti = model.CreditNoteMaster.Miti;

                        accountTransactionMaster.GlCode = model.CreditNoteMaster.GlCode;
                        accountTransactionDetail.GlCode = item.GlCode;

                        accountTransactionDetail.SlCode = item.SubLedgerId;

                        accountTransactionMaster.CrRate = Convert.ToDecimal(model.CreditNoteMaster.CurRate);
                        accountTransactionDetail.CrRate = Convert.ToDecimal(model.CreditNoteMaster.CurRate);

                        accountTransactionMaster.CrCode = model.CreditNoteMaster.CurCode;
                        accountTransactionDetail.CrCode = model.CreditNoteMaster.CurCode;

                        accountTransactionMaster.ReferenceId = model.CreditNoteMaster.Id;
                        accountTransactionDetail.ReferenceId = model.CreditNoteMaster.Id;

                        accountTransactionMaster.DrAmt = Convert.ToDecimal(item.Amount);
                        accountTransactionDetail.CrAmt = Convert.ToDecimal(item.Amount);

                        if (model.CreditNoteMaster.CurrencyId != null && model.CreditNoteMaster.CurrencyId != 0)
                        {
                            accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.CreditNoteMaster.CurRate);
                            accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.CreditNoteMaster.CurRate);

                        }
                        else
                        {
                            accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(item.Amount);
                            accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.Amount);
                        }

                        accountTransactionMaster.Narration = item.Narration;
                        accountTransactionDetail.Narration = item.Narration;

                        accountTransactionMaster.Remarks = model.CreditNoteMaster.Remarks;
                        accountTransactionDetail.Remarks = model.CreditNoteMaster.Remarks;

                        accountTransactionMaster.Source = source;
                        accountTransactionDetail.Source = source;

                        accountTransactionMaster.SNo = sno;
                        accountTransactionDetail.SNo = sno;

                        accountTransactionMaster.CreatedById = userId;
                        accountTransactionDetail.CreatedById = userId;

                        accountTransactionMaster.FYId = FiscalYear.Id;
                        accountTransactionDetail.FYId = FiscalYear.Id;

                        accountTransactionMaster.BranchId = CurrentBranchId;
                        accountTransactionDetail.BranchId = CurrentBranchId;

                        var isCashBank = _ledger.GetById(model.CreditNoteMaster.GlCode).IsCashBank;
                        if (isCashBank)
                            accountTransactionMaster.CbCode = model.CreditNoteMaster.GlCode;
                        var isCashBankDetail = _ledger.GetById(item.GlCode).IsCashBank;
                        if (isCashBankDetail)
                            accountTransactionMaster.CbCode = item.GlCode;

                        _accountTransaction.Add(accountTransactionMaster);
                        _accountTransaction.Add(accountTransactionDetail);
                        sno++;
                    }
                }
                var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.CreditNoteMaster);
                if (data != null)
                {
                    foreach (var udfEntry in data)
                    {
                        var value = formCollection[udfEntry.FieldName];
                        var i = new UDFData()
                                    {
                                        ReferenceId = model.CreditNoteMaster.Id,
                                        UdfId = udfEntry.Id,
                                        Value = value,
                                        SourceId = (int)EntryModuleEnum.CreditNoteMaster
                                    };

                        _udfData.Add(i);
                    }
                }
                return Json(new { success = true, isEdit = false });
            }
            return Json(new { success = false, msg = "Debit Note No. must be unique" });
        }

        [CheckPermission(Permissions = "Edit", Module = "CN")]
        public ActionResult CreditNoteEdit(int id)
        {
            var data = _creditNoteMaster.GetById(id);
            var viewmodel = base.CreateViewModel<CreditNoteAddViewModel>();
            if (base.SystemControl.DateType == 1)
            {
                viewmodel.Date = data.Date.ToString("MM/dd/yyyy");
            }
            else
            {
                viewmodel.Date = data.Miti;
            }
            if (base.SystemControl.DateType == 1)
            {
                viewmodel.RefDate = data.RefBillDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewmodel.RefDate = data.RefBillMiti;
            }
            viewmodel.CreditNoteMaster = data;
            viewmodel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.CreditNoteMaster).ToList();
            ViewBag.Id = data.Id;
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                ViewBag.EntryControl = entryControl;
            }
            if (!string.IsNullOrEmpty(data.CurCode))
            {
                var CurrId = Convert.ToInt32(data.CurCode);
                viewmodel.CreditNoteMaster.CurCode = _currency.GetById(x => x.Id == CurrId).Code;
                viewmodel.CreditNoteMaster.CurrencyId = _currency.GetById(x => x.Id == CurrId).Id;
            }
            viewmodel.TotalAmt = data.CreditNoteDetails.Sum(x => x.Amount).ToString();
            viewmodel.TotalAmtRs = data.CreditNoteDetails.Sum(x => x.Amount).ToString();
            return PartialView(viewmodel);
        }

        [HttpPost]
        public ActionResult CreditNoteEdit(IEnumerable<CreditNoteDetail> CreditNoteDetailEntry, CreditNoteAddViewModel model, FormCollection formCollection)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.CreditNoteMaster.FYId = FiscalYear.Id;
            //Temporary
            if (base.SystemControl.DateType == 1)
            {
                model.CreditNoteMaster.Date = Convert.ToDateTime(model.Date);
                model.CreditNoteMaster.Miti = NepaliDateService.NepaliDate(model.CreditNoteMaster.Date).Date;
            }
            else
            {
                model.CreditNoteMaster.Miti = model.Date;
                model.CreditNoteMaster.Date = NepaliDateService.NepalitoEnglishDate(model.Date);
            }
            if (base.SystemControl.DateType == 1)
            {
                model.CreditNoteMaster.RefBillDate = model.RefDate.ToDate();
                model.CreditNoteMaster.RefBillMiti = NepaliDateService.NepaliDate(model.RefDate.ToDate()).Date;
            }
            else
            {
                model.CreditNoteMaster.RefBillMiti = model.RefDate;
                model.CreditNoteMaster.RefBillDate = NepaliDateService.NepalitoEnglishDate(model.RefDate);
            }
            if (model.CreditNoteMaster.CurrencyId != 0)
            {
                var Code = _currency.GetById(model.CreditNoteMaster.CurrencyId);
                model.CreditNoteMaster.CurCode = Convert.ToString(Code.Id);
            }
            _creditNoteMaster.Update(model.CreditNoteMaster);
            var source = StringEnum.Parse(typeof(ModuleEnum), "Credit Note").ToString();
            _accountTransaction.Delete(x => x.ReferenceId == model.CreditNoteMaster.Id && x.Source == source);
            _creditNoteDetail.Delete(x => x.CreditNoteMasterId == model.CreditNoteMaster.Id);
            int sno = 1;
            foreach (var item in CreditNoteDetailEntry)
            {
                if (item.GlCode != 0 && item.Amount != 0)
                {
                    var creditNoteDetail = new CreditNoteDetail();
                    var accountTransactionMaster = new AccountTransaction();
                    var accountTransactionDetail = new AccountTransaction();

                    creditNoteDetail.Amount = Convert.ToDecimal(item.Amount);
                    creditNoteDetail.Narration = item.Narration;
                    creditNoteDetail.CreditNoteMasterId = model.CreditNoteMaster.Id;
                    creditNoteDetail.SlCode = item.SlCode;
                    creditNoteDetail.GlCode = item.GlCode;
                    _creditNoteDetail.Add(creditNoteDetail);

                    accountTransactionMaster.VNo = model.CreditNoteMaster.Number;
                    accountTransactionDetail.VNo = model.CreditNoteMaster.Number;

                    accountTransactionMaster.VDate = model.CreditNoteMaster.Date;
                    accountTransactionDetail.VDate = model.CreditNoteMaster.Date;

                    accountTransactionMaster.VMiti = model.CreditNoteMaster.Miti;
                    accountTransactionDetail.VMiti = model.CreditNoteMaster.Miti;

                    accountTransactionMaster.GlCode = model.CreditNoteMaster.GlCode;
                    accountTransactionDetail.GlCode = item.GlCode;

                    accountTransactionDetail.SlCode = creditNoteDetail.SlCode;
                    accountTransactionMaster.CrRate = Convert.ToDecimal(model.CreditNoteMaster.CurRate);
                    accountTransactionDetail.CrRate = Convert.ToDecimal(model.CreditNoteMaster.CurRate);

                    accountTransactionMaster.CrCode = model.CreditNoteMaster.CurCode;
                    accountTransactionDetail.CrCode = model.CreditNoteMaster.CurCode;



                    accountTransactionMaster.ReferenceId = model.CreditNoteMaster.Id;
                    accountTransactionDetail.ReferenceId = model.CreditNoteMaster.Id;

                    accountTransactionMaster.DrAmt = Convert.ToDecimal(item.Amount);
                    accountTransactionDetail.CrAmt = Convert.ToDecimal(item.Amount);


                    if (model.CreditNoteMaster.CurrencyId != null && model.CreditNoteMaster.CurrencyId != 0)
                    {
                        accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.CreditNoteMaster.CurRate);
                        accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(model.CreditNoteMaster.CurRate);
                    }
                    else
                    {
                        accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(item.Amount);
                        accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.Amount);

                    }

                    accountTransactionMaster.Narration = item.Narration;
                    accountTransactionDetail.Narration = item.Narration;

                    accountTransactionMaster.Remarks = model.CreditNoteMaster.Remarks;
                    accountTransactionDetail.Remarks = model.CreditNoteMaster.Remarks;

                    accountTransactionMaster.Source = source;
                    accountTransactionDetail.Source = source;

                    accountTransactionMaster.SNo = sno;
                    accountTransactionDetail.SNo = sno;

                    accountTransactionMaster.CreatedById = userId;
                    accountTransactionDetail.CreatedById = userId;

                    accountTransactionMaster.FYId = FiscalYear.Id;
                    accountTransactionDetail.FYId = FiscalYear.Id;

                    accountTransactionMaster.BranchId = CurrentBranchId;
                    accountTransactionDetail.BranchId = CurrentBranchId;
                    var isCashBank = _ledger.GetById(model.CreditNoteMaster.GlCode).IsCashBank;
                    if (isCashBank)
                        accountTransactionMaster.CbCode = model.CreditNoteMaster.GlCode;
                    var isCashBankDetail = _ledger.GetById(item.GlCode).IsCashBank;
                    if (isCashBankDetail)
                        accountTransactionDetail.CbCode = model.CreditNoteMaster.GlCode;

                    _accountTransaction.Add(accountTransactionMaster);
                    _accountTransaction.Add(accountTransactionDetail);
                    sno++;
                }
            }
            _udfData.Delete(x => x.ReferenceId == model.CreditNoteMaster.Id && x.SourceId == (int)EntryModuleEnum.CreditNoteMaster);
            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.CreditNoteMaster);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = model.CreditNoteMaster.Id,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.CreditNoteMaster
                    };

                    _udfData.Add(i);
                }
            }
            return Json(new { success = true, isEdit = true });
        }

        [CheckPermission(Permissions = "Approve", Module = "CN")]
        [HttpPost]
        public ActionResult CreditNoteApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _creditNoteMaster.GetById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _creditNoteMaster.Update(data);
            return null;
        }

        [CheckPermission(Permissions = "Create", Module = "CN")]
        public ActionResult GetCreditNoteDetailEntry()
        {
            var viewModel = new CreditNoteDetailEntryViewModel();
            viewModel.EntryControl = new EntryControlPL();
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
                viewModel.EntryControl = entryControl;
            return PartialView("_PartialCreditNoteDetailEntry", viewModel);
        }

        [CheckPermission(Permissions = "Edit", Module = "CN")]
        public ActionResult GetCreditNoteDetailEdit()
        {
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl != null)
            {
                ViewBag.EntryControl = entryControl;
            }
            var data = new CreditNoteDetail();
            return PartialView("_PartialCreditNoteDetailEdit", data);
        }
        #endregion

        #region Purchase
        public ActionResult Purchase()
        {
            return View();
        }

        public JsonResult GetAgent()
        {
            var data = _agent.GetMany(x => !x.IsDeleted).Select(x => new
                                                                         {
                                                                             Id = x.Id,
                                                                             Description = x.Name
                                                                         }).OrderBy(x => x.Description);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVendorList()
        {
            var vendor = StringEnum.Parse(typeof(LedgerCategoryEnum), "Vendor").ToString();
            var both = StringEnum.Parse(typeof(LedgerCategoryEnum), "Both").ToString();
            //var test=(int) Enum.Parse(typeof (LedgerCategoryEnum), Enum.GetName(typeof (LedgerCategoryEnum), "test"));
            var data = _ledger.Filter(x => (x.Category == vendor || x.Category == both || x.IsCashBank) && !x.IsDeleted).Select(x => new
            {
                Id = x.Id,
                Description = x.AccountName
            }).OrderBy(x => x.Description);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerList()
        {
            var vendor = StringEnum.Parse(typeof(LedgerCategoryEnum), "Customer").ToString();
            var both = StringEnum.Parse(typeof(LedgerCategoryEnum), "Both").ToString();
            //var test=(int) Enum.Parse(typeof (LedgerCategoryEnum), Enum.GetName(typeof (LedgerCategoryEnum), "test"));
            var data = _ledger.Filter(x => (x.Category == vendor || x.Category == both || x.IsCashBank) && !x.IsDeleted).Select(x => new
            {
                Id = x.Id,
                Description = x.AccountName
            }).OrderBy(x => x.Description);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVendorDetails(int glCode)
        {
            var balance = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=0, @glCode=" + glCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            var signedBalance = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=1, @glCode=" + glCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            var bal = Convert.ToDecimal(signedBalance);
            var ledger = _ledger.GetById(x => x.Id == glCode);
            decimal creditLimit = 0;
            if (ledger != null)
            {
                if (ledger.CreditLimit != null)
                {
                    creditLimit = Convert.ToDecimal(ledger.CreditLimit);
                }
            }
            var outstandingChallan = 0;
            var totalOutstanding = bal - outstandingChallan;
            return Json(new { creditLimit = creditLimit, balance = balance, outstandingChallan = outstandingChallan, totalOutstanding = totalOutstanding }, JsonRequestBehavior.AllowGet);
        }

        #region Purchase Invoice
        public JsonResult CheckInvoiceNoInPurchaseInvoice(string InvoiceNo, int? Id)
        {
            var feeterm = new PurchaseInvoice();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _purchaseInvoice.GetById(x => x.Id == Id);
                if (data.InvoiceNo.ToLower().Trim() != InvoiceNo.ToLower().Trim())
                {
                    feeterm =
                        _purchaseInvoice.GetById(x => x.InvoiceNo.ToLower().Trim() == InvoiceNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _purchaseInvoice.GetById(x => x.InvoiceNo.ToLower().Trim() == InvoiceNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new PurchaseInvoice();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulatePurchaseChallanList(int id)
        {
            var challan = _purchaseChallanMaster.GetById(x => x.Id == id, "PurchaseChallanDetails");
            var vendor = _ledger.GetById(x => x.Id == challan.GlCode);
            var agent = string.Empty;
            if (challan.AgentCode != null)
            {
                var agentId = Convert.ToInt32(challan.AgentCode);
                var agentData = _agent.GetById(agentId);
                if (agentData != null)
                    agent = agentData.Name;
            }
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var challanDetailView = string.Empty;
            var billWiseBillingTerm = string.Empty;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);

            foreach (var item in challan.PurchaseChallanDetails)
            {
                var singleChallanDetail = new PurchaseDetailEntryViewModel();
                var productId = Convert.ToInt32(item.ProductCode);
                var product = _product.GetById(productId);
                singleChallanDetail.ProductName = product.Name;
                singleChallanDetail.ProductId = Convert.ToInt32(item.ProductCode);

                if (!string.IsNullOrEmpty(item.Unit))
                {
                    var unit = _unitRepository.GetById(x => x.Code == item.Unit);
                    singleChallanDetail.Unit = unit.Id;
                }
                singleChallanDetail.Qty = item.Qty;
                singleChallanDetail.UnitList = unitSelectList;
                singleChallanDetail.Rate = item.Rate;
                singleChallanDetail.BasicAmt = item.BasicAmt;
                singleChallanDetail.TermAmt = item.TermAmt;
                singleChallanDetail.NetAmt = item.NetAmt;
                if (billingTerm.Count() != 0)
                    singleChallanDetail.AllowProductWiseBillTerm = true;
                challanDetailView += base.RenderPartialViewToString("_PartialPurchaseDetailEntry", singleChallanDetail);
            }

            var billingTermDetail = _billingTermDetail.GetMany(x => x.Source == "PC" && x.ReferenceId == challan.Id);

            foreach (var item in billingTermDetail)
            {
                var billing = _billingTerm.GetById(x => x.Id == item.BillingTermId);
                var viewModel = new BillingTermDetailViewModel();
                viewModel.Amount = item.TermAmount;
                viewModel.Description = billing.Description;
                viewModel.Id = item.Id;
                viewModel.Rate = item.Rate;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                viewModel.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), billing.Sign));
                viewModel.BillingTermIndex = item.Index;
                viewModel.IsProductWise = item.IsProductWise;

                billWiseBillingTerm += base.RenderPartialViewToString("_PartialHdnBillingTerm", viewModel);
            }
            var CurrentBalance = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=0, @glCode=" + challan.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            var TotalOutstanding = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=1, @glCode=" + challan.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            return Json(new
            {
                VendorName = vendor.AccountName,
                VendorId = challan.GlCode,
                AgentName = agent,
                AgentId = challan.AgentCode,
                challanDetailView = challanDetailView,
                billWiseBillingTerm = billWiseBillingTerm,
                BasicAmt = challan.BasicAmt,
                NetAmt = challan.NetAmt,
                TermAmt = challan.TermAmt,
                TotalAmtRs = challan.BasicAmt + challan.TermAmt,
                CurrentBalance = CurrentBalance,
                TotalOutstanding = TotalOutstanding
            });
        }
        public ActionResult PopulatePurchaseOrderList(int id)
        {
            var order = _purchaseOrderMaster.GetById(x => x.Id == id, "PurchaseOrderDetails");
            var vendor = _ledger.GetById(x => x.Id == order.GlCode);
            var agent = string.Empty;
            if (order.AgentCode != null)
            {
                var agentId = Convert.ToInt32(order.AgentCode);
                var agentData = _agent.GetById(agentId);
                if (agentData != null)
                    agent = agentData.Name;
            }
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var challanDetailView = string.Empty;
            var billWiseBillingTerm = string.Empty;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);

            foreach (var item in order.PurchaseOrderDetails)
            {
                var singleChallanDetail = new PurchaseDetailEntryViewModel();
                var productId = Convert.ToInt32(item.ProductCode);
                var product = _product.GetById(productId);
                singleChallanDetail.ProductName = product.Name;
                singleChallanDetail.ProductId = Convert.ToInt32(item.ProductCode);
                singleChallanDetail.Unit = item.UnitId;
                singleChallanDetail.Qty = item.Qty;
                singleChallanDetail.UnitList = unitSelectList;
                singleChallanDetail.Rate = item.Rate;
                singleChallanDetail.BasicAmt = item.BasicAmt;
                singleChallanDetail.TermAmt = item.TermAmt;
                singleChallanDetail.NetAmt = item.NetAmt;
                if (billingTerm.Count() != 0)
                    singleChallanDetail.AllowProductWiseBillTerm = true;
                challanDetailView += base.RenderPartialViewToString("_PartialPurchaseDetailEntry", singleChallanDetail);
            }

            var billingTermDetail = _billingTermDetail.GetMany(x => x.Source == "PO" && x.ReferenceId == order.Id);

            foreach (var item in billingTermDetail)
            {
                var billing = _billingTerm.GetById(x => x.Id == item.BillingTermId);
                var viewModel = new BillingTermDetailViewModel();
                viewModel.Amount = item.TermAmount;
                viewModel.Description = billing.Description;
                viewModel.Id = item.BillingTermId;
                viewModel.Rate = item.Rate;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                viewModel.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), billing.Sign));
                viewModel.BillingTermIndex = item.Index;
                viewModel.IsProductWise = item.IsProductWise;

                billWiseBillingTerm += base.RenderPartialViewToString("_PartialHdnBillingTerm", viewModel);
            }
            var CurrentBalance = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=0, @glCode=" + order.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            var TotalOutstanding = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=1, @glCode=" + order.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            return Json(new
            {
                VendorName = vendor.AccountName,
                VendorId = order.GlCode,
                AgentName = agent,
                AgentId = order.AgentCode,
                materialIssueDetailView = challanDetailView,
                billWiseBillingTerm = billWiseBillingTerm,
                BasicAmt = order.BasicAmt,
                NetAmt = order.NetAmt,
                TermAmt = order.TermAmt,
                TotalAmtRs = order.BasicAmt + order.TermAmt,
                CurrentBalance = CurrentBalance,
                TotalOutstanding = TotalOutstanding
            });
        }

        public ActionResult PopulateLedgerList(int id, string date)
        {
            var vendor = _ledger.GetById(x => x.Id == id);
            //   var purchaseInvoice = new PurchaseInvoice();
            var agent = string.Empty;
            var currency = string.Empty;
            var dateDue = string.Empty;
            // var miti = string.Empty;
            if (vendor.CreditDays != null)
            {
                var dueDays = Convert.ToInt32(vendor.CreditDays);
                if (base.SystemControl.DateType == 1)
                {
                    var dueDate = Convert.ToDateTime(date);
                    dateDue = dueDate.AddDays(+dueDays).ToString("MM/dd/yyyy");
                }
                else
                {
                    var dueMiti = NepaliDateService.NepalitoEnglishDate(date).Date;
                    var data = dueMiti.AddDays(+dueDays);
                    dateDue = NepaliDateService.NepaliDate(data).Date;
                }
                // purchaseInvoice.DueDay = dueDays;
            }

            if (vendor.AgentId != null)
            {
                var agentId = Convert.ToInt32(vendor.AgentId);
                var agentData = _agent.GetById(agentId);
                if (agentData != null)
                    agent = agentData.Name;
            }
            if (vendor.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(vendor.CurrencyId);
                var currCode = _currency.GetById(currencyId);
                if (currCode != null)
                    currency = currCode.Name;
            }

            return Json(new
                            {
                                VendorName = vendor.AccountName,
                                CreditDays = vendor.CreditDays,
                                AgentName = agent,
                                AgentId = vendor.AgentId,
                                CurrencyId = vendor.CurrencyId,
                                Currency = currency,
                                DueDate = dateDue,
                                DateType = base.SystemControl.DateType
                                //DueMiti=miti
                            });
        }
        [CheckPermission(Permissions = "Navigate", Module = "PB")]
        public ActionResult PurchaseInvoice()
        {
            ViewBag.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            var viewModel = new PurchaseInvoiceViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.Category == cat && !x.IsDeleted);
            var billingTermProductWise = billingTerm.Where(x => x.IsProductWise).OrderBy(x => x.DispalyOrder);
            var billingTermBillWise = billingTerm.Where(x => !x.IsProductWise).OrderBy(x => x.DispalyOrder);
            viewModel.ProductWiseBillTerms = new List<BillingTermSelectViewModel>();
            viewModel.BillWiseBillTerms = new List<BillingTermSelectViewModel>();
            billingTermProductWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.ProductWiseBillTerms.Add(billingTermSelectList);
            });
            billingTermBillWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.BillWiseBillTerms.Add(billingTermSelectList);
            });
            return View(viewModel);
        }

        [CheckPermission(Permissions = "Navigate", Module = "PB")]
        public ActionResult PurchaseInvoiceList()
        {
            ViewBag.UserRight = base.UserRight("PB");
            var viewModel = new PurchaseInvoiceViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var listPending = _purchaseInvoice.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //var listAccepted = _purchaseInvoice.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            // viewModel.AcceptedList = listAccepted;
            viewModel.PendingList = listPending;
            return PartialView(viewModel);
        }

        public ActionResult PurchaseInvoicePendingList(int pageNo)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var list = _purchaseInvoice.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView(list);
        }

        //public ActionResult PurchaseInvoiceAcceptedList(int pageNo)
        //{
        //    var list = _purchaseInvoice.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
        //    return PartialView(list);
        //}

        public ActionResult PurchaseInvoiceDelete(int id)
        {
            var model = _purchaseInvoice.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _purchaseInvoice.Update(model);
            _accountTransaction.Delete(x => x.Source == "PB" && x.ReferenceId == id);
            _stockTransaction.Delete(x => x.Source == "PB" && x.ReferenceId == id);
            return Json(true);
        }

        public ActionResult GetProductUnitAndAltUnit(int id)
        {
            var product = _product.GetById(id);
            return Json(new { unit = product.Unit, altunit = product.AltUnit }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckFiscalyearDateinPurchaseInvoice(string InvoiceDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(InvoiceDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(InvoiceDate);
                    }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {

                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }
        [CheckPermission(Permissions = "Create", Module = "PB")]
        public ActionResult PurchaseInvoiceAdd()
        {
            var viewModel = base.CreateModel<PurchaseInvoiceAddViewModel>();
            viewModel.AllowProductWiseBillTerm = false;
            viewModel.InvoiceTypeList = new SelectList(
                Enum.GetValues(typeof(PurchaseInvoiceType)).Cast
                    <PurchaseInvoiceType>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");
            viewModel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.PurchaseInvoice).ToList();

            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.PurchaseInvoice = new PurchaseInvoice();
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _purchaseInvoice.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? data.InvoiceDate.ToShortDateString() : data.InvoiceMiti;
                }
                else
                {
                    viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            viewModel.EntryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            var billingTermProductWise = billingTerm.Where(x => x.Category == cat && !x.IsProductWise && !x.IsDeleted).OrderBy(x => x.DispalyOrder);
            var billingTermBillWise = billingTerm.Where(x => x.Category == cat && x.IsProductWise && !x.IsDeleted).OrderBy(x => x.DispalyOrder);
            viewModel.ProductWiseBillTerms = new List<BillingTermSelectViewModel>();
            viewModel.BillWiseBillTerms = new List<BillingTermSelectViewModel>();
            billingTermProductWise.ToList().ForEach(item =>
                                             {

                                                 var billingTermSelectList = new BillingTermSelectViewModel();
                                                 billingTermSelectList.Id = item.Id;
                                                 billingTermSelectList.Basis =
                                                     Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                                                 billingTermSelectList.Description = item.Description;
                                                 StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                                                 billingTermSelectList.Sign =
                                                     stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                                                 billingTermSelectList.Rate = item.Rate;
                                                 viewModel.ProductWiseBillTerms.Add(billingTermSelectList);
                                             });
            billingTermBillWise.ToList().ForEach(item =>
            {

                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                viewModel.BillWiseBillTerms.Add(billingTermSelectList);
            });
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseInvoiceAdd(IEnumerable<PurchaseDetailEntryViewModel> purchaseDetailEntry, PurchaseInvoiceAddViewModel model, IEnumerable<BillingTermDetailViewModel> billTermList, IEnumerable<PurchaseProductBatchViewModel> ProductBatchList)
        {
            var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
            if (model.PurchaseInvoice.GlCode != 0)
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                if (model.PurchaseInvoice.CurrencyId != 0 && model.PurchaseInvoice.CurrencyId != null)
                {
                    var currencyId = Convert.ToInt32(model.PurchaseInvoice.CurrencyId);
                    //var Code = _currency.GetById(model.PurchaseInvoice.CurrencyId);
                    var Code = _currency.GetById(currencyId);
                    //  model.PurchaseInvoice.CurrCode = _currency.GetById(model.PurchaseInvoice.CurrencyId).Code;
                    model.PurchaseInvoice.CurrCode = Convert.ToString(Code.Id);
                }
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseInvoice.InvoiceDate = Convert.ToDateTime(model.InvoiceDate);
                    model.PurchaseInvoice.InvoiceMiti = NepaliDateService.NepaliDate(model.PurchaseInvoice.InvoiceDate).Date;
                }
                else
                {
                    model.PurchaseInvoice.InvoiceMiti = model.InvoiceDate;
                    model.PurchaseInvoice.InvoiceDate = NepaliDateService.NepalitoEnglishDate(model.InvoiceDate);
                }
                if (!string.IsNullOrEmpty(model.PartyDate))
                {
                    if (base.SystemControl.DateType == 1)
                    {
                        model.PurchaseInvoice.PartyInvoiceDate = Convert.ToDateTime(model.PartyDate);
                        model.PurchaseInvoice.PartyInvoiceMiti = NepaliDateService.NepaliDate(model.PartyDate.ToDate()).Date;
                    }
                    else
                    {
                        model.PurchaseInvoice.PartyInvoiceMiti = model.PartyDate;
                        model.PurchaseInvoice.PartyInvoiceDate = NepaliDateService.NepalitoEnglishDate(model.PartyDate);
                    }
                }

                if (!string.IsNullOrEmpty(model.DueDate) && !string.IsNullOrEmpty(model.DueDate.Trim()))
                {
                    if (base.SystemControl.DateType == 1)
                    {
                        model.PurchaseInvoice.DueDate = Convert.ToDateTime(model.DueDate);
                        model.PurchaseInvoice.DueMiti = NepaliDateService.NepaliDate(model.DueDate.ToDate()).Date;
                    }
                    else
                    {
                        model.PurchaseInvoice.DueMiti = model.DueDate;
                        model.PurchaseInvoice.DueDate = NepaliDateService.NepalitoEnglishDate(model.DueDate);
                    }
                }

                //_purchaseInvoice.Add(model.PurchaseInvoice);
                _salesService.SavePurchaseInvoice(model, userId, CurrentBranchId, FiscalYear.Id);

                if (model.PurchaseInvoice.DocId != null)
                {
                    var docId = Convert.ToInt32(model.PurchaseInvoice.DocId);
                    var documentnumber = _docNumering.GetById(docId);
                    documentnumber.DocCurrNo += 1;
                    _docNumering.Update(documentnumber);

                }

                if (!string.IsNullOrEmpty(model.PPDDate))
                {
                    if (base.SystemControl.DateType == 1)
                    {
                        model.PurchaseImpTransDoc.PPDDate = Convert.ToDateTime(model.PPDDate);
                        model.PurchaseImpTransDoc.PPDMiti = NepaliDateService.NepaliDate(model.PPDDate.ToDate()).Date;
                    }
                    else
                    {
                        model.PurchaseImpTransDoc.PPDMiti = model.PPDDate;
                        model.PurchaseImpTransDoc.PPDDate = NepaliDateService.NepalitoEnglishDate(model.PPDDate);
                    }
                }
                if (!string.IsNullOrEmpty(model.CnDate))
                {
                    if (base.SystemControl.DateType == 1)
                    {
                        model.PurchaseImpTransDoc.CnDate = Convert.ToDateTime(model.CnDate);
                        model.PurchaseImpTransDoc.CnMiti = NepaliDateService.NepaliDate(model.CnDate.ToDate()).Date;
                    }
                    else
                    {
                        model.PurchaseImpTransDoc.CnMiti = model.CnDate;
                        model.PurchaseImpTransDoc.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                    }
                }
                var tempPurchaseDetailList = _salesService.SavePurchaseDetail(purchaseDetailEntry, model.PurchaseInvoice,
                                                                     base.SystemControl.PurchaseAc, userId,
                                                                     base.FiscalYear.Id, CurrentBranchId, ProductBatchList, billTermList);
                //if (billTermList != null)
                //{
                //    _salesService.SavePurchaseBillingTerm(billTermList.Where(x => x.Amount != 0).ToList(), tempPurchaseDetailList, model.PurchaseInvoice, userId, FiscalYear.Id, CurrentBranchId);
                //}
                model.PurchaseImpTransDoc.PurchaseInvoiceId = model.PurchaseInvoice.Id;
                _purchaseImpTransDoc.Add(model.PurchaseImpTransDoc);
                return Json(new { success = true, isEdit = false });
            }
            return Json(new { success = false, isEdit = false });
        }

        [CheckPermission(Permissions = "Edit", Module = "PB")]
        public ActionResult PurchaseInvoiceEdit(int id)
        {
            var data = _purchaseInvoice.GetById(id);
            var model = _purchaseImpTransDoc.GetById(x => x.PurchaseInvoiceId == id);
            var viewModel = base.CreateModel<PurchaseInvoiceAddViewModel>();
            viewModel.PurchaseOrderId = data.OrderId;
            if (base.SystemControl.DateType == 1)
            {
                viewModel.InvoiceDate = data.InvoiceDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.InvoiceDate = data.InvoiceMiti;
            }
            if (data.PartyInvoiceDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.PartyDate = Convert.ToDateTime(data.PartyInvoiceDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.PartyDate = data.PartyInvoiceMiti;
                }
            }

            if (data.DueDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.DueDate = Convert.ToDateTime(data.DueDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.DueDate = data.DueMiti;
                }
            }
            if (model != null)
            {
                if (model.PPDDate != null)
                {
                    if (base.SystemControl.DateType == 1)
                    {
                        viewModel.PPDDate = Convert.ToDateTime(model.PPDDate).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        viewModel.PPDDate = model.PPDMiti;
                    }
                }


                if (model.CnDate != null)
                {
                    if (base.SystemControl.DateType == 1)
                    {
                        viewModel.CnDate = Convert.ToDateTime(model.CnDate).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        viewModel.CnDate = model.CnMiti;
                    }
                }
            }

            viewModel.InvoiceTypeList = new SelectList(
                Enum.GetValues(typeof(PurchaseInvoiceType)).Cast
                    <PurchaseInvoiceType>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text", data.PurchaseInvoiceType);
            viewModel.PurchaseImpTransDoc = _purchaseImpTransDoc.GetById(x => x.PurchaseInvoiceId == data.Id);
            if (data.OrderId != null)
            {
                viewModel.OrderNo = _purchaseOrderMaster.GetById(data.OrderId.Value).OrderNo;
            }
            viewModel.PurchaseInvoice = data;
            if (!string.IsNullOrEmpty(data.CurrCode))
            {
                //  viewModel.PurchaseInvoice.CurrCode = data.CurrCode;
                var CurrId = Convert.ToInt32(data.CurrCode);
                // viewModel.PurchaseInvoice.CurrencyId = _currency.GetById(x => x.Id == CurrCode).Id;
                viewModel.PurchaseInvoice.CurrCode = _currency.GetById(x => x.Id == CurrId).Code;
                viewModel.PurchaseInvoice.CurrencyId = _currency.GetById(x => x.Id == CurrId).Id;
            }
            if (data.ChallanId != null)
            {
                viewModel.ChallanNo = _purchaseChallanMaster.GetById(data.ChallanId.Value).ChallanNo;
            }
            viewModel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.PurchaseInvoice).ToList();
            ViewBag.Id = data.Id;
            ViewBag.AllowProductWiseBillTerm = 0;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
            {
                ViewBag.AllowProductWiseBillTerm = 1;
                viewModel.AllowProductWiseBillTerm = true;
            }
            foreach (var term in viewModel.PurchaseInvoice.PurchaseDetails)
            {
                term.UnitId = _unitRepository.GetById(x => x.Code.ToLower() == term.Unit.ToLower()).Id;
            }

            /* var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;*/
            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id && x.Source == "PB").ToList();

            //var defaultIndex = billTerm.Max(x => x.Index);
            var defaultIndex = 1; // data.PurchaseDetails.Max(x => x.Index);

            ViewBag.Index = defaultIndex == null ? 1 : defaultIndex + 1;
            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }

            ViewBag.BillingTermList = billTerm;
            viewModel.TotalAmtRs = viewModel.PurchaseInvoice.NetAmt.ToString();
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");

            var context = new DataContext();
            var PurchaseProductBatchViewModels = (from pd in context.PurchaseDetails.Where(x => x.PurchaseInvoiceId == id)
                                                  join pb in context.PurchaseProductBatches.Where(x => x.Source == "PB") on pd.Id equals pb.DetailId
                                                  join p in context.Products on pb.ProductId equals p.Id
                                                  join gd in context.Godowns on pb.Godown equals gd.Id into g
                                                  from gd in g.DefaultIfEmpty()
                                                  select new PurchaseProductBatchViewModel()
                                                  {
                                                      BatchSerialNo = pb.SerialNo,
                                                      BuyRate = pb.BuyRate,
                                                      ExpDate = pb.EXPDate,
                                                      Godown = gd == null ? string.Empty : gd.Name,
                                                      GodownId = gd == null ? 0 : gd.Id,
                                                      IsExpDate = p.ExpDate,
                                                      IsMfgDate = p.MfgDate,
                                                      MfgDate = pb.MFGDate,
                                                      ProductId = pb.ProductId,
                                                      Qty = pb.Qty,
                                                      SalesRate = pb.SalesRate,
                                                      UnitId = pb.Unit,
                                                      DetailId = pd.Id
                                                  }).ToList();
            ViewBag.PurchaseProductBatchViewModels = PurchaseProductBatchViewModels;
            viewModel.EntryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseInvoiceEdit(IEnumerable<PurchaseDetail> purchaseDetailEntry, PurchaseInvoiceAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList, IEnumerable<PurchaseProductBatchViewModel> ProductBatchList)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.PurchaseInvoice.FYId = FiscalYear.Id;
            model.PurchaseInvoice.BasicAmt = Convert.ToDecimal(model.PurchaseInvoice.BasicAmt);
            model.PurchaseInvoice.NetAmt = Convert.ToDecimal(model.PurchaseInvoice.NetAmt);
            model.PurchaseInvoice.TermAmt = Convert.ToDecimal(model.PurchaseInvoice.TermAmt);
            model.PurchaseInvoice.OrderId = model.PurchaseOrderId;

            if (base.SystemControl.DateType == 1)
            {
                model.PurchaseInvoice.InvoiceDate = Convert.ToDateTime(model.InvoiceDate);
                model.PurchaseInvoice.InvoiceMiti = NepaliDateService.NepaliDate(model.PurchaseInvoice.InvoiceDate).Date;
            }
            else
            {
                model.PurchaseInvoice.InvoiceMiti = model.InvoiceDate;
                model.PurchaseInvoice.InvoiceDate = NepaliDateService.NepalitoEnglishDate(model.InvoiceDate);
            }
            if (!string.IsNullOrEmpty(model.PartyDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseInvoice.PartyInvoiceDate = Convert.ToDateTime(model.PartyDate);
                    model.PurchaseInvoice.PartyInvoiceMiti = NepaliDateService.NepaliDate(model.PurchaseInvoice.PartyInvoiceDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseInvoice.PartyInvoiceMiti = model.PartyDate;
                    model.PurchaseInvoice.PartyInvoiceDate = NepaliDateService.NepalitoEnglishDate(model.PartyDate);
                }
            }
            if (!string.IsNullOrEmpty(model.DueDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseInvoice.DueDate = Convert.ToDateTime(model.DueDate);
                    model.PurchaseInvoice.DueMiti = NepaliDateService.NepaliDate(model.PurchaseInvoice.DueDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseInvoice.DueMiti = model.DueDate;
                    model.PurchaseInvoice.DueDate = NepaliDateService.NepalitoEnglishDate(model.DueDate);
                }

            }
            if (model.PurchaseInvoice.CurrencyId != 0 && model.PurchaseInvoice.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(model.PurchaseInvoice.CurrencyId);
                //var Code = _currency.GetById(model.PurchaseInvoice.CurrencyId);
                var Code = _currency.GetById(currencyId);
                model.PurchaseInvoice.CurrCode = Convert.ToString(Code.Id);

            }
            _purchaseInvoice.Update(model.PurchaseInvoice);
            if (!string.IsNullOrEmpty(model.PPDDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseImpTransDoc.PPDDate = Convert.ToDateTime(model.PPDDate);
                    model.PurchaseImpTransDoc.PPDMiti = NepaliDateService.NepaliDate(model.PurchaseImpTransDoc.PPDDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseImpTransDoc.PPDMiti = model.PPDDate;
                    model.PurchaseImpTransDoc.PPDDate = NepaliDateService.NepalitoEnglishDate(model.PPDDate);
                }
            }
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseImpTransDoc.CnDate = Convert.ToDateTime(model.CnDate);
                    model.PurchaseImpTransDoc.CnMiti = NepaliDateService.NepaliDate(model.PurchaseImpTransDoc.CnDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseImpTransDoc.CnMiti = model.CnDate;
                    model.PurchaseImpTransDoc.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }

            _purchaseImpTransDoc.Update(model.PurchaseImpTransDoc);
            var source = StringEnum.Parse(typeof(ModuleEnum), "Purchase").ToString();
            _accountTransaction.Delete(x => x.ReferenceId == model.PurchaseInvoice.Id && x.Source == source);
            int[] detailId = purchaseDetailEntry.Where(x => x.Id != 0).Select(x => new[] { x.Id }).SelectMany(x => x).ToArray();
            _purchaseProductBatch.Delete(x => detailId.Contains(x.DetailId));
            _purchaseDetail.Delete(x => x.PurchaseInvoiceId == model.PurchaseInvoice.Id);
            _stockTransaction.Delete(x => x.ReferenceId == model.PurchaseInvoice.Id && x.Source == source);
            _billingTermDetail.Delete(x => x.ReferenceId == model.PurchaseInvoice.Id && x.Source == source);
            //int sno = 1;
            //decimal basicAmt = 0;


            //var source = StringEnum.Parse(typeof(ModuleEnum), "Purchase").ToString();
            int sno = 1;
            decimal basicAmt = 0;
            var tempPurchaseDetailList = new List<PurchaseDetail>();
            foreach (var item in purchaseDetailEntry)
            {
                if (item.PCode != null && item.PCode != 0 && item.BasicAmt != null && item.BasicAmt != 0 &&
                    item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    var batchSerialNo = string.Empty;
                    var unitcode = _unitRepository.GetById(item.UnitId).Code;
                    basicAmt += Convert.ToDecimal(item.BasicAmt);
                    var purchaseDetail = new PurchaseDetail();
                    purchaseDetail.SNo = sno;
                    purchaseDetail.PCode = item.PCode;
                    purchaseDetail.AltQty = item.AltQty;
                    purchaseDetail.Qty = Convert.ToDecimal(item.Qty);
                    purchaseDetail.AltStockQty = item.AltQty;
                    purchaseDetail.StockQty = Convert.ToDecimal(item.Qty);
                    purchaseDetail.Rate = Convert.ToDecimal(item.Rate);
                    purchaseDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    purchaseDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    purchaseDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    purchaseDetail.Remarks = model.PurchaseInvoice.Remarks;
                    purchaseDetail.PurchaseInvoiceId = model.PurchaseInvoice.Id;
                    purchaseDetail.Index = item.Index;
                    purchaseDetail.Unit = unitcode;
                    purchaseDetail.AltUnit = item.AltUnit;
                    purchaseDetail.Godown = item.Godown;
                    _purchaseDetail.Add(purchaseDetail);
                    tempPurchaseDetailList.Add(purchaseDetail);

                    if (ProductBatchList != null)
                    {
                        var batch = ProductBatchList.Where(x => x.ParentGuid == item.DetailGuid).FirstOrDefault();
                        if (batch != null)
                        {
                            batchSerialNo = batch.BatchSerialNo;
                            var productBatch = new PurchaseProductBatch();
                            productBatch.BranchId = base.CurrentBranch.Id;
                            productBatch.Godown = item.Godown;
                            productBatch.ProductId = item.PCode;
                            productBatch.DetailId = purchaseDetail.Id;
                            productBatch.Qty = Convert.ToDecimal(item.Qty);
                            productBatch.SerialNo = batch.BatchSerialNo;
                            if (batch.IsMfgDate)
                                productBatch.MFGDate = batch.MfgDate;
                            if (batch.IsExpDate)
                                productBatch.EXPDate = batch.ExpDate;
                            productBatch.BuyRate = batch.BuyRate;
                            productBatch.SalesRate = batch.SalesRate;
                            productBatch.Unit = item.UnitId;
                            productBatch.StockQuantity = Convert.ToDecimal(item.Qty);
                            _purchaseProductBatch.Add(productBatch);
                        }
                    }
                    int purchaseAc;
                    var product = _product.GetById(item.PCode);
                    purchaseAc = product.PurchaseId != null ? Convert.ToInt32(product.PurchaseReturnId) : base.SystemControl.PurchaseAc;

                    var stockTransaction = new StockTransaction();
                    stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.PurchaseInvoice.AgentCode);
                    stockTransaction.AltQty = purchaseDetail.AltQty;
                    stockTransaction.AltStockQty = purchaseDetail.AltStockQty;
                    stockTransaction.AltUnit = purchaseDetail.AltUnit;
                    stockTransaction.CurrCode = model.PurchaseInvoice.CurrCode;
                    stockTransaction.CurrRate = model.PurchaseInvoice.CurrRate;
                    stockTransaction.GlCode = model.PurchaseInvoice.GlCode;
                    stockTransaction.NetAmt = purchaseDetail.NetAmt;
                    stockTransaction.ProductCode = purchaseDetail.PCode;
                    stockTransaction.Quantity = purchaseDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(purchaseDetail.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = source;
                    stockTransaction.Unit = unitcode;
                    stockTransaction.AltUnit = item.AltUnit;
                    stockTransaction.ReferenceId = model.PurchaseInvoice.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.BatchSerialNo = batchSerialNo;
                    stockTransaction.Godown = Convert.ToString(purchaseDetail.Godown);
                    var stock =
                        _stockTransaction.Filter(x => x.ProductCode == item.PCode).ToList().LastOrDefault();
                    if (stock == null)
                    {
                        stockTransaction.StockVal = Convert.ToDecimal(purchaseDetail.BasicAmt);
                        stockTransaction.StockQty = purchaseDetail.StockQty;
                    }
                    else
                    {
                        stockTransaction.StockVal = stock.StockVal + Convert.ToDecimal(purchaseDetail.BasicAmt);
                        stockTransaction.StockQty = stock.StockQty + purchaseDetail.StockQty;
                    }
                    stockTransaction.TermAmt = purchaseDetail.TermAmt;
                    stockTransaction.TransactionType =
                        StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();

                    stockTransaction.VDate = model.PurchaseInvoice.InvoiceDate;
                    stockTransaction.VMiti = model.PurchaseInvoice.InvoiceMiti;
                    stockTransaction.VNo = model.PurchaseInvoice.InvoiceNo;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;


                    var accountTransactionDetail = new AccountTransaction();
                    accountTransactionDetail.VNo = model.PurchaseInvoice.InvoiceNo;
                    accountTransactionDetail.VDate = model.PurchaseInvoice.InvoiceDate;
                    accountTransactionDetail.VMiti = model.PurchaseInvoice.InvoiceMiti;
                    accountTransactionDetail.CrRate = Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
                    accountTransactionDetail.AgentCode = Convert.ToString(model.PurchaseInvoice.AgentCode);
                    accountTransactionDetail.CrCode = model.PurchaseInvoice.CurrCode;
                    //Purchase A/C Id need to specify in setting
                    //accountTransactionDetail.AgentCode = Convert.ToString(model.PurchaseInvoice.AgentCode);
                    accountTransactionDetail.GlCode = purchaseAc;
                    accountTransactionDetail.SlCode = model.PurchaseInvoice.SlCode;
                    accountTransactionDetail.DrAmt = Convert.ToDecimal(purchaseDetail.BasicAmt);

                    if (model.PurchaseInvoice.CurrencyId != 0 && model.PurchaseInvoice.CurrencyId != null)
                    {
                        accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(purchaseDetail.BasicAmt) * Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
                    }
                    else
                    {
                        accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(purchaseDetail.BasicAmt);
                    }
                    accountTransactionDetail.CrAmt = 0;
                    accountTransactionDetail.LocalCrAmt = 0;
                    accountTransactionDetail.Source = source;
                    accountTransactionDetail.SNo = 0;
                    accountTransactionDetail.CreatedById = userId;
                    var isCashBankdetail = _ledger.GetById(purchaseAc).IsCashBank;
                    if (isCashBankdetail)
                        accountTransactionDetail.CbCode = model.PurchaseInvoice.GlCode;
                    accountTransactionDetail.FYId = FiscalYear.Id;
                    accountTransactionDetail.ReferenceId = model.PurchaseInvoice.Id;
                    accountTransactionDetail.BranchId = CurrentBranchId;
                    accountTransactionDetail.Remarks = model.PurchaseInvoice.Remarks;
                    _accountTransaction.Add(accountTransactionDetail);
                    if (billTermList != null)
                    {
                        decimal netAmt = 0;
                        //var billWiseTerms =
                        //    billTermList.Where(x => !x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList();
                        foreach (var term in billTermList.Where(x => x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList())
                        {
                            var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                            decimal drAmt = 0;
                            decimal drLocalAmt = 0;
                            decimal crAmt = 0;
                            decimal crLocalAmt = 0;
                            decimal termAmt = 0;
                            if (term.Sign == "-")
                            {
                                crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                termAmt = System.Math.Abs(crAmt) * (-1);
                            }
                            else
                            {
                                drAmt = Convert.ToDecimal(term.Amount);
                                drLocalAmt = Convert.ToDecimal(term.Amount);
                                termAmt = drAmt;
                            }
                            var billingTermDetail = new BillingTermDetail();
                            billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                            var index = Convert.ToInt32(term.BillingTermIndex);
                            billingTermDetail.Index = index;
                            billingTermDetail.ReferenceId = model.PurchaseInvoice.Id;
                            billingTermDetail.Source = source;
                            billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                            billingTermDetail.TermAmount = termAmt;

                            billingTermDetail.DetailId = purchaseDetail.Id;
                            billingTermDetail.Type = "P";


                            billingTermDetail.IsProductWise = term.IsProductWise;
                            _billingTermDetail.Add(billingTermDetail);

                            //AccountTransaction Posting
                            var accountTransactionBillingTerm = new AccountTransaction();
                            accountTransactionBillingTerm.VNo = model.PurchaseInvoice.InvoiceNo;
                            accountTransactionBillingTerm.VDate = model.PurchaseInvoice.InvoiceDate;
                            accountTransactionBillingTerm.VMiti = model.PurchaseInvoice.InvoiceMiti;
                            accountTransactionBillingTerm.CrRate = Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
                            accountTransactionBillingTerm.AgentCode = Convert.ToString(model.PurchaseInvoice.AgentCode);
                            accountTransactionBillingTerm.CrCode = model.PurchaseInvoice.CurrCode;
                            accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                            if (model.PurchaseInvoice.CurrencyId != null && model.PurchaseInvoice.CurrencyId != 0)
                            {
                                drLocalAmt = drLocalAmt * Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
                                crLocalAmt = crLocalAmt * Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
                            }
                            accountTransactionBillingTerm.DrAmt = drAmt;
                            accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                            accountTransactionBillingTerm.CrAmt = crAmt;
                            accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;

                            accountTransactionBillingTerm.Source = source;
                            accountTransactionBillingTerm.SNo = 0;
                            accountTransactionBillingTerm.CreatedById = userId;
                            accountTransactionBillingTerm.CbCode = model.PurchaseInvoice.GlCode;
                            accountTransactionBillingTerm.FYId = FiscalYear.Id;
                            accountTransactionBillingTerm.ReferenceId = model.PurchaseInvoice.Id;
                            accountTransactionBillingTerm.BranchId = CurrentBranchId;
                            accountTransactionBillingTerm.Remarks = model.PurchaseInvoice.Remarks;
                            _accountTransaction.Add(accountTransactionBillingTerm);
                        }
                    }
                }
            }

            if (billTermList != null)
            {
                foreach (var term in billTermList.Where(x => !x.IsProductWise).ToList())
                {
                    var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                    decimal drAmt = 0;
                    decimal drLocalAmt = 0;
                    decimal crAmt = 0;
                    decimal crLocalAmt = 0;
                    decimal termAmt = 0;
                    if (term.Sign == "-")
                    {
                        crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        termAmt = System.Math.Abs(crAmt) * (-1);
                    }
                    else
                    {
                        drAmt = Convert.ToDecimal(term.Amount);
                        drLocalAmt = Convert.ToDecimal(term.Amount);
                        termAmt = drAmt;
                    }
                    var billingTermDetail = new BillingTermDetail();
                    billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    billingTermDetail.Index = index;
                    billingTermDetail.ReferenceId = model.PurchaseInvoice.Id;
                    billingTermDetail.Source = source;
                    billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                    billingTermDetail.TermAmount = termAmt;
                    billingTermDetail.Type = "B";
                    billingTermDetail.IsProductWise = term.IsProductWise;
                    _billingTermDetail.Add(billingTermDetail);

                    //AccountTransaction Posting
                    var accountTransactionBillingTerm = new AccountTransaction();
                    accountTransactionBillingTerm.VNo = model.PurchaseInvoice.InvoiceNo;
                    accountTransactionBillingTerm.VDate = model.PurchaseInvoice.InvoiceDate;
                    accountTransactionBillingTerm.VMiti = model.PurchaseInvoice.InvoiceMiti;
                    accountTransactionBillingTerm.CrRate = Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
                    accountTransactionBillingTerm.AgentCode = Convert.ToString(model.PurchaseInvoice.AgentCode);
                    accountTransactionBillingTerm.CrCode = model.PurchaseInvoice.CurrCode;
                    accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                    accountTransactionBillingTerm.SlCode = model.PurchaseInvoice.SlCode;
                    if (model.PurchaseInvoice.CurrencyId != null && model.PurchaseInvoice.CurrencyId != 0)
                    {
                        drLocalAmt = drLocalAmt * Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
                        crLocalAmt = crLocalAmt * Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
                    }
                    accountTransactionBillingTerm.DrAmt = drAmt;
                    accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                    accountTransactionBillingTerm.CrAmt = crAmt;
                    accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                    accountTransactionBillingTerm.Source = source;
                    accountTransactionBillingTerm.SNo = 0;
                    accountTransactionBillingTerm.CreatedById = userId;
                    var isCashBankbill = _ledger.GetById(billingTerm.GeneralLedgerId).IsCashBank;
                    if (isCashBankbill)
                        accountTransactionBillingTerm.CbCode = billingTerm.GeneralLedgerId;
                    accountTransactionBillingTerm.FYId = FiscalYear.Id;
                    accountTransactionBillingTerm.ReferenceId = model.PurchaseInvoice.Id;
                    accountTransactionBillingTerm.BranchId = CurrentBranchId;
                    accountTransactionBillingTerm.Remarks = model.PurchaseInvoice.Remarks;
                    _accountTransaction.Add(accountTransactionBillingTerm);
                }
            }

            var accountTransactionMaster = new AccountTransaction();
            accountTransactionMaster.VNo = model.PurchaseInvoice.InvoiceNo;
            accountTransactionMaster.VDate = model.PurchaseInvoice.InvoiceDate;
            accountTransactionMaster.VMiti = model.PurchaseInvoice.InvoiceMiti;
            accountTransactionMaster.AgentCode = Convert.ToString(model.PurchaseInvoice.AgentCode);
            accountTransactionMaster.CrRate = Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
            accountTransactionMaster.CrCode = model.PurchaseInvoice.CurrCode;
            accountTransactionMaster.GlCode = model.PurchaseInvoice.GlCode;
            accountTransactionMaster.SlCode = model.PurchaseInvoice.SlCode;
            accountTransactionMaster.DrAmt = 0;
            accountTransactionMaster.LocalDrAmt = 0;
            accountTransactionMaster.CrAmt = Convert.ToDecimal(model.PurchaseInvoice.NetAmt);
            accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(model.PurchaseInvoice.NetAmt);
            if (model.PurchaseInvoice.CurrencyId != 0 && model.PurchaseInvoice.CurrencyId != null)
            {
                accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(model.PurchaseInvoice.NetAmt) * Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
            }

            accountTransactionMaster.Source = source;
            accountTransactionMaster.SNo = 0;
            accountTransactionMaster.CreatedById = userId;
            var isCashBankMaster = _ledger.GetById(model.PurchaseInvoice.GlCode).IsCashBank;
            if (isCashBankMaster)
                accountTransactionMaster.CbCode = model.PurchaseInvoice.GlCode;
            accountTransactionMaster.FYId = FiscalYear.Id;
            accountTransactionMaster.ReferenceId = model.PurchaseInvoice.Id;
            accountTransactionMaster.Remarks = model.PurchaseInvoice.Remarks;
            accountTransactionMaster.BranchId = CurrentBranchId;
            _accountTransaction.Add(accountTransactionMaster);


            _udfData.Delete(x => x.ReferenceId == model.PurchaseInvoice.Id && x.SourceId == (int)EntryModuleEnum.PurchaseInvoice);
            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.PurchaseInvoice);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = model.PurchaseInvoice.Id,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.PurchaseInvoice
                    };

                    _udfData.Add(i);
                }
            }

            return Json(new { success = true, isEdit = true });
        }

        [CheckPermission(Permissions = "Approve", Module = "PB")]
        [HttpPost]
        public ActionResult PurchaseInvoiceApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _purchaseInvoice.GetById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _purchaseInvoice.Update(data);
            return null;
        }

        public ActionResult GetProductBatch(int productId)
        {
            var product = _product.GetById(productId);
            if (!product.IsBatch)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            var viewModel = new PurchaseProductBatchViewModel();
            viewModel.IsExpDate = product.ExpDate;
            viewModel.IsMfgDate = product.MfgDate;
            viewModel.UnitList = new SelectList(GetUnitListByProductId(productId), "Id", "Code");
            var view = base.RenderPartialViewToString("GetProductBatch", viewModel);
            return Json(new { success = true, view = view }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductBatchSales(int productId)
        {
            var product = _product.GetById(productId);
            var unit = product.Unit;
            var altUnit = product.AltUnit != null ? _unitRepository.GetById(x => x.Id == product.AltUnit).Code : string.Empty;
            if (!product.IsBatch)
            {
                return Json(new { success = false, unit = unit, altUnit = altUnit }, JsonRequestBehavior.AllowGet);
            }

            var data = _purchaseService.GetProductBatchList(productId);
            //var viewModel = new PurchaseProductBatchViewModel();
            ViewBag.IsExpDate = product.ExpDate;
            ViewBag.IsMfgDate = product.MfgDate;

            if (data.Any())
            {
                var view = base.RenderPartialViewToString("GetProductBatchSales", data);
                return Json(new { success = true, unit = unit, altUnit = altUnit, view = view }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, unit = unit, altUnit = altUnit }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetOldProductBatch(PurchaseProductBatchViewModel model)
        {
            model.UnitList = new SelectList(GetUnitListByProductId(model.ProductId), "Id", "Code");
            return PartialView("GetProductBatch", model);
        }

        [CheckPermission(Permissions = "Create", Module = "PB")]
        public ActionResult GetPurchaseDetailEntry(bool disableGodown = false)
        {
            var viewModel = new PurchaseDetailEntryViewModel();
            viewModel.DisableGodown = disableGodown;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            viewModel.EntryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            viewModel.DetailGuid = Guid.NewGuid().ToString();
            return PartialView("_PartialPurchaseDetailEntry", viewModel);
        }

        [CheckPermission(Permissions = "Edit", Module = "PB")]
        public ActionResult GetPurchaseDetailEdit(int? id, int? index)
        {
            var data = new PurchaseDetail();
            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id).ToList();
            var defaultIndex = billTerm.Max(x => x.Index);
            ViewBag.Index = defaultIndex == null ? 0 : defaultIndex + 1;
            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }

            ViewBag.BillingTermList = billTerm;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                ViewBag.AllowProductWiseBillTerm = 1;
            else
            {
                ViewBag.AllowProductWiseBillTerm = 0;
            }
            if (index != null)
                data.Index = Convert.ToInt32(index);

            data.EntryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            data.Unitlist = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");

            return PartialView("_PartialPurchaseDetailEdit", data);
        }

        public ActionResult PurchaseInvoicePrint(int invoiceId)
        {
            var model = _purchaseInvoice.GetById(invoiceId);
            var billDate = Convert.ToDateTime(model.InvoiceDate).ToString("MM/dd/yyyy");
            var invoice = new PurchaseInvoicePrintViewModel();
            invoice.InvoiceHeader = new InvoiceHeader();
            var logo = "NoImage.jpg";
            if (!string.IsNullOrEmpty(base.CompanyInfo.LogoGuid))
                logo = base.CompanyInfo.LogoGuid + base.CompanyInfo.LogoExt;
            var filePath = Server.MapPath("~/Content/Logo/" + logo);
            if (!System.IO.File.Exists(filePath))
            {
                logo = "NoImage.jpg";
            }
            invoice.InvoiceHeader.CompanyLogoUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "/Content/Logo/" + logo;
            invoice.InvoiceHeader.InvoiceDate = billDate;
            invoice.InvoiceHeader.InvoiceNo = model.InvoiceNo;
            invoice.InvoiceHeader.CompanyAddress = base.CompanyInfo.Address;
            invoice.InvoiceHeader.CompanyName = base.CompanyInfo.Name;
            invoice.InvoiceHeader.PartyName = model.Ledger.AccountName;
            invoice.BilledBy = model.User.FullName;
            int sno = 1;
            double subTotal = 0;
            invoice.InvoiceDetails = new List<InvoiceDetail>();
            foreach (var item in model.PurchaseDetails)
            {
                if (item.BasicAmt != 0)
                {
                    var product = _product.GetById(item.PCode);
                    var invoiceDetail = new InvoiceDetail();
                    invoiceDetail.Amount = Convert.ToDecimal(item.BasicAmt);
                    invoiceDetail.Particular = product.Name;
                    invoiceDetail.SNo = sno;
                    invoiceDetail.Qty = Convert.ToDecimal(item.Qty);
                    invoiceDetail.Rate = Convert.ToDecimal(item.Rate);
                    invoiceDetail.Remarks = item.Remarks;
                    sno++;
                    subTotal += Convert.ToDouble(item.BasicAmt);
                    invoice.InvoiceDetails.Add(invoiceDetail);
                }
            }

            var billingTerms = _billingTermDetail.GetMany(x => x.ReferenceId == invoiceId && x.Source == "PB").OrderBy(x => x.Index);
            invoice.InvoiceBillingTerms = new List<InvoiceBillingTerms>();
            double termAmt = 0;
            foreach (var item in billingTerms)
            {
                var invoiceBillingTerm = new InvoiceBillingTerms();
                var termName = _billingTerm.GetById(x => x.Id == item.BillingTermId && x.Type == "P").Description;
                invoiceBillingTerm.TermName = termName;
                invoiceBillingTerm.TermRate = item.Rate;
                invoiceBillingTerm.TermAmount = item.TermAmount;
                invoice.InvoiceBillingTerms.Add(invoiceBillingTerm);
                termAmt += Convert.ToDouble(item.TermAmount);
            }
            invoice.SubTotal = subTotal;
            invoice.Total = subTotal + termAmt;
            return this.ViewPdf("", "PurchaseInvoicePrint", invoice);
        }

        public JsonResult GetProductList()
        {
            var data = _product.GetMany(x => !x.IsDeleted).Select(x => new
            {
                Id = x.Id,
                Description = x.Name,
                Reference = x.Name
            }).OrderBy(x => x.Description);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductShortName(int id)
        {
            var data = _product.GetById(x => x.Id == id).ShortName;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductStockQuantity(int id)
        {
            decimal stock = 0;
            var data = _stockTransaction.Filter(x => x.ProductCode == id);
            foreach (var item in data)
            {
                if (item.TransactionType == "I")
                    stock += Convert.ToDecimal(item.Quantity);
                else
                {
                    stock -= Convert.ToDecimal(item.Quantity);
                }
            }
            return Json(stock, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLowestUnit(int id)
        {
            var code = String.Empty;
            var data = _product.GetById(x => x.Id == id);
            if (data != null)
            {
                var productcode = Convert.ToInt32(data.Unit);
                code = _unitRepository.GetById(productcode).Code;
            }
            return Json(code, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConversionDetail(int productId, decimal quantity, int Unit, string LowestUnit, string type)
        {
            var currentUnit = _unitRepository.GetById(Unit).Code;
            var convertedValue = UtilityService.UnitConversion(productId, quantity, currentUnit, LowestUnit);
            var data = _productUnitConversionRepository.GetById(x => x.Unit.ToLower() == currentUnit.ToLower() && x.ProductId == productId);
            decimal? totalrate = 0;
            if (data != null)
            {
                totalrate = type == "P" ? data.BuyPrice : data.SalePrice;
            }
            else
            {
                var rate = _product.GetById(productId);

                if (rate.SalesPrice != null)
                {
                    totalrate = type == "P" ? rate.BuyPrice : rate.SalesPrice;
                }
            }

            return Json(new { convertvalue = convertedValue, ratte = totalrate }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetConversionDetailNew(int productId, decimal quantity, string Unit, string LowestUnit, string type)
        {
            var currentUnit = Unit;
            var convertedValue = UtilityService.UnitConversion(productId, quantity, currentUnit, LowestUnit);
            var data = _productUnitConversionRepository.GetById(x => x.Unit.ToLower() == currentUnit.ToLower() && x.ProductId == productId);
            decimal? totalrate = 0;
            if (data != null)
            {
                totalrate = type == "P" ? data.BuyPrice : data.SalePrice;
            }
            else
            {
                var rate = _product.GetById(productId);
                if (rate.BuyPrice != null)
                {
                    totalrate = type == "P" ? rate.BuyPrice : rate.SalesPrice;
                }
            }
            return Json(new { convertvalue = convertedValue, ratte = totalrate }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetDropDownlist(int productId)
        {
            IEnumerable<Unit> unitlist;
            var list = _productUnitConversionRepository.GetMany(x => x.ProductId == productId).Select(x => x.Unit);
            if (list.Any())
            {
                unitlist = _unitRepository.GetMany(x => list.Contains(x.Code));
            }
            else
            {
                var unitid = _product.GetById(productId).Unit;
                var id = Convert.ToInt32(unitid);
                var unit = _unitRepository.GetById(id).Code;
                unitlist = _unitRepository.GetMany(x => x.Code.ToLower() == unit.ToLower());
            }

            var classes = unitlist.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Unit> GetUnitListByProductId(int productId)
        {
            IEnumerable<Unit> unitlist;
            var list = _productUnitConversionRepository.GetMany(x => x.ProductId == productId).Select(x => x.Unit);
            if (list.Any())
            {
                unitlist = _unitRepository.GetMany(x => list.Contains(x.Code));
            }
            else
            {
                var unitid = _product.GetById(productId).Unit;
                var id = Convert.ToInt32(unitid);
                var unit = _unitRepository.GetById(id).Code;
                unitlist = _unitRepository.GetMany(x => x.Code.ToLower() == unit.ToLower());
            }
            return unitlist;
        }

        #endregion

        #region Purchase Return
        public JsonResult CheckInvoiceNoInPurchaseReturn(string InvoiceNo, int? Id)
        {
            var feeterm = new PurchaseReturnMaster();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);

            if (Id.HasValue && Id != 0)
            {
                var data = _purchaseReturnMaster.GetById(x => x.Id == Id);
                if (data.InvoiceNo.ToLower().Trim() != InvoiceNo.ToLower().Trim())
                {
                    feeterm =
                        _purchaseReturnMaster.GetById(x => x.InvoiceNo.ToLower().Trim() == InvoiceNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _purchaseReturnMaster.GetById(x => x.InvoiceNo.ToLower().Trim() == InvoiceNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new PurchaseReturnMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }


        [CheckPermission(Permissions = "Navigate", Module = "PR")]
        public ActionResult PurchaseReturn()
        {
            var viewModel = new PurchaseReturnViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.Category == cat && !x.IsDeleted);
            var billingTermProductWise = billingTerm.Where(x => x.IsProductWise).OrderBy(x => x.DispalyOrder);
            var billingTermBillWise = billingTerm.Where(x => !x.IsProductWise).OrderBy(x => x.DispalyOrder);
            viewModel.ProductWiseBillTerms = new List<BillingTermSelectViewModel>();
            viewModel.BillWiseBillTerms = new List<BillingTermSelectViewModel>();
            billingTermProductWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.ProductWiseBillTerms.Add(billingTermSelectList);
            });
            billingTermBillWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.BillWiseBillTerms.Add(billingTermSelectList);
            });
            return View(viewModel);
        }

        [CheckPermission(Permissions = "Navigate", Module = "PR")]
        public ActionResult PurchaseReturnList()
        {
            ViewBag.UserRight = base.UserRight("PR");
            var viewModel = new PurchaseReturnViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var listPending = _purchaseReturnMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //var listAccepted = _purchaseReturnMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //  viewModel.AcceptedList = listAccepted;
            viewModel.PendingList = listPending;
            return PartialView(viewModel);
        }

        public ActionResult PurchaseReturnPendingList(int pageNo)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var list = _purchaseReturnMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView(list);
        }

        //public ActionResult PurchaseReturnAcceptedList(int pageNo)
        //{
        //    var list = _purchaseReturnMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
        //    return PartialView(list);
        //}

        public ActionResult PopulatePurchaseReturnLedgerList(int id, string date)
        {
            var vendor = _ledger.GetById(x => x.Id == id);
            //   var purchaseInvoice = new PurchaseInvoice();
            var agent = string.Empty;
            var currency = string.Empty;
            var dateDue = string.Empty;
            // var miti = string.Empty;
            if (vendor.CreditDays != null)
            {
                var dueDays = Convert.ToInt32(vendor.CreditDays);
                if (base.SystemControl.DateType == 1)
                {
                    var dueDate = Convert.ToDateTime(date);
                    dateDue = dueDate.AddDays(+dueDays).ToString("MM/dd/yyyy");
                }
                else
                {
                    var dueMiti = NepaliDateService.NepalitoEnglishDate(date).Date;
                    var data = dueMiti.AddDays(+dueDays);
                    dateDue = NepaliDateService.NepaliDate(data).Date;
                }
                // purchaseInvoice.DueDay = dueDays;
            }

            if (vendor.AgentId != null)
            {
                var agentId = Convert.ToInt32(vendor.AgentId);
                var agentData = _agent.GetById(agentId);
                if (agentData != null)
                    agent = agentData.Name;
            }
            if (vendor.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(vendor.CurrencyId);
                var currCode = _currency.GetById(currencyId);
                if (currCode != null)
                    currency = currCode.Name;
            }

            return Json(new
            {
                VendorName = vendor.AccountName,
                CreditDays = vendor.CreditDays,
                AgentName = agent,
                AgentId = vendor.AgentId,
                CurrencyId = vendor.CurrencyId,
                Currency = currency,
                DueDate = dateDue,
                DateType = base.SystemControl.DateType
                //DueMiti=miti
            });
        }

        [HttpPost]
        public ActionResult PurchaseReturnDelete(int id)
        {
            var model = _purchaseReturnMaster.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _purchaseReturnMaster.Update(model);
            _accountTransaction.Delete(x => x.Source == "PR" && x.ReferenceId == id);
            _stockTransaction.Delete(x => x.Source == "PR" && x.ReferenceId == id);
            return Json(true);
        }

        public ActionResult CheckFiscalyearDateinPurchaseReturn(string InvoiceDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(InvoiceDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(InvoiceDate);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {

                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }

        [CheckPermission(Permissions = "Create", Module = "PR")]
        public ActionResult PurchaseReturnAdd()
        {
            //var data = _purchaseReturnMaster.GetAll().LastOrDefault();
            var viewModel = base.CreateViewModel<PurchaseReturnAddViewModel>();
            // viewModel.Date = data != null ? data.InvoiceDate : DateTime.UtcNow;
            viewModel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.PurchaseInvoiceReturn).ToList();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.PurchaseReturnMaster = new PurchaseReturnMaster();
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _purchaseReturnMaster.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? data.InvoiceDate.ToShortDateString() : data.InvoiceMiti;
                }
                else
                {
                    viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }
            }
            viewModel.EntryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseReturnAdd(IEnumerable<PurchaseReturnDetailEntryViewModel> purchaseReturnDetailEntry, PurchaseReturnAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.PurchaseReturnMaster.CreatedById = userId;
            model.PurchaseReturnMaster.CreatedDate = DateTime.UtcNow;
            model.PurchaseReturnMaster.InvoiceDate = model.Date;
            model.PurchaseReturnMaster.FYId = FiscalYear.Id;
            model.PurchaseReturnMaster.BranchId = CurrentBranchId;
            var source = StringEnum.Parse(typeof(KRBAccounting.Enums.ModuleEnum), "Purchase Return").ToString();
            if (base.SystemControl.DateType == 1)
            {
                model.PurchaseReturnMaster.InvoiceDate = Convert.ToDateTime(model.InvoiceDate);
                model.PurchaseReturnMaster.InvoiceMiti = NepaliDateService.NepaliDate(model.PurchaseReturnMaster.InvoiceDate).Date;
            }
            else
            {
                model.PurchaseReturnMaster.InvoiceMiti = model.InvoiceDate;
                model.PurchaseReturnMaster.InvoiceDate = NepaliDateService.NepalitoEnglishDate(model.InvoiceDate);
            }
            if (!string.IsNullOrEmpty(model.RefDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseReturnMaster.RefBillDate = Convert.ToDateTime(model.RefDate);
                    model.PurchaseReturnMaster.RefBillMiti = NepaliDateService.NepaliDate(model.RefDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseReturnMaster.RefBillMiti = model.RefDate;
                    model.PurchaseReturnMaster.RefBillDate = NepaliDateService.NepalitoEnglishDate(model.RefDate);
                }
            }
            if (!string.IsNullOrEmpty(model.DueDate) && !string.IsNullOrEmpty(model.DueDate.Trim()))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseReturnMaster.DueDate = Convert.ToDateTime(model.DueDate);
                    model.PurchaseReturnMaster.DueMiti = NepaliDateService.NepaliDate(model.DueDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseReturnMaster.DueMiti = model.DueDate;
                    model.PurchaseReturnMaster.DueDate = NepaliDateService.NepalitoEnglishDate(model.DueDate);
                }
            }
            //if (model.PurchaseReturnMaster.CurrencyId != 0)
            if (model.PurchaseReturnMaster.CurrencyId != 0 && model.PurchaseReturnMaster.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(model.PurchaseReturnMaster.CurrencyId);
                // var Code = _currency.GetById(model.PurchaseReturnMaster.CurrencyId);
                var Code = _currency.GetById(currencyId);
                model.PurchaseReturnMaster.CurrCode = Convert.ToString(Code.Id);

            }

            _purchaseReturnMaster.Add(model.PurchaseReturnMaster);
            if (!string.IsNullOrEmpty(model.PPDDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseReturnImpTransDoc.PPDDate = Convert.ToDateTime(model.PPDDate);
                    model.PurchaseReturnImpTransDoc.PPDMiti = NepaliDateService.NepaliDate(model.PPDDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseReturnImpTransDoc.PPDMiti = model.PPDDate;
                    model.PurchaseReturnImpTransDoc.PPDDate = NepaliDateService.NepalitoEnglishDate(model.PPDDate);
                }
            }
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseReturnImpTransDoc.CnDate = Convert.ToDateTime(model.CnDate);
                    model.PurchaseReturnImpTransDoc.CnMiti = NepaliDateService.NepaliDate(model.CnDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseReturnImpTransDoc.CnMiti = model.CnDate;
                    model.PurchaseReturnImpTransDoc.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }

            var tempPurchaseReturnDetailList = new List<PurchaseReturnDetail>();
            int sno = 1;

            var accountTransactionMaster = new AccountTransaction();
            accountTransactionMaster.VNo = model.PurchaseReturnMaster.InvoiceNo;
            accountTransactionMaster.VDate = model.PurchaseReturnMaster.InvoiceDate;
            accountTransactionMaster.VMiti = model.PurchaseReturnMaster.InvoiceMiti;
            accountTransactionMaster.CrRate = Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
            accountTransactionMaster.CrCode = model.PurchaseReturnMaster.CurrCode;
            accountTransactionMaster.GlCode = model.PurchaseReturnMaster.GlCode;
            accountTransactionMaster.SlCode = model.PurchaseReturnMaster.SlCode;
            accountTransactionMaster.CrAmt = 0;
            accountTransactionMaster.LocalCrAmt = 0;
            accountTransactionMaster.DrAmt = Convert.ToDecimal(model.PurchaseReturnMaster.NetAmt);
            if (model.PurchaseReturnMaster.CurrencyId != 0 && model.PurchaseReturnMaster.CurrencyId != null)
            {
                accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(model.PurchaseReturnMaster.NetAmt) * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
            }
            else
            {
                accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(model.PurchaseReturnMaster.NetAmt);
            }
            accountTransactionMaster.Source = source;
            accountTransactionMaster.SNo = 0;
            accountTransactionMaster.CreatedById = userId;
            var isCashBank = _ledger.GetById(model.PurchaseReturnMaster.GlCode).IsCashBank;
            if (isCashBank)
                accountTransactionMaster.CbCode = model.PurchaseReturnMaster.GlCode;
            accountTransactionMaster.ReferenceId = model.PurchaseReturnMaster.Id;
            accountTransactionMaster.FYId = FiscalYear.Id;
            accountTransactionMaster.BranchId = CurrentBranchId;
            accountTransactionMaster.Remarks = model.PurchaseReturnMaster.Remarks;
            accountTransactionMaster.AgentCode = Convert.ToString(model.PurchaseReturnMaster.AgentCode);
            _accountTransaction.Add(accountTransactionMaster);
            model.PurchaseReturnImpTransDoc.PurchaseReturnId = model.PurchaseReturnMaster.Id;

            foreach (var item in purchaseReturnDetailEntry)
            {
                if (item.ProductId != null && item.ProductId != 0 && item.BasicAmt != null && item.BasicAmt != 0 && item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    sno++;
                    var unitcode = _unitRepository.GetById(item.UnitId).Code;
                    var purchaseReturnDetail = new PurchaseReturnDetail();
                    purchaseReturnDetail.SNo = sno;
                    purchaseReturnDetail.ProductCode = item.ProductId;
                    purchaseReturnDetail.AltQty = item.AltQty;
                    purchaseReturnDetail.Qty = Convert.ToDecimal(item.Qty);
                    purchaseReturnDetail.AltStockQty = item.AltQty;
                    purchaseReturnDetail.StockQty = Convert.ToDecimal(item.Qty);
                    purchaseReturnDetail.Rate = Convert.ToDecimal(item.Rate);
                    purchaseReturnDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    purchaseReturnDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    purchaseReturnDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    purchaseReturnDetail.Remarks = model.PurchaseReturnMaster.Remarks;
                    purchaseReturnDetail.PurchaseReturnId = model.PurchaseReturnMaster.Id;
                    purchaseReturnDetail.Index = item.Index;
                    purchaseReturnDetail.Unit = unitcode;
                    purchaseReturnDetail.AltUnit = item.AltUnit;
                    purchaseReturnDetail.Godown = item.GodownId;
                    purchaseReturnDetail.BatchId = item.BatchId;
                    _purchaseReturnDetail.Add(purchaseReturnDetail);
                    tempPurchaseReturnDetailList.Add(purchaseReturnDetail);

                    var accountTransactionDetail = new AccountTransaction();
                    accountTransactionDetail.VNo = model.PurchaseReturnMaster.InvoiceNo;
                    accountTransactionDetail.VDate = model.PurchaseReturnMaster.InvoiceDate;
                    accountTransactionDetail.VMiti = model.PurchaseReturnMaster.InvoiceMiti;
                    accountTransactionDetail.CrRate = Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                    accountTransactionDetail.CrCode = model.PurchaseReturnMaster.CurrCode;
                    int purchaseReturnAc;
                    var product = _product.GetById(item.ProductId);
                    purchaseReturnAc = product.PurchaseReturnId != null ? Convert.ToInt32(product.PurchaseReturnId) : base.SystemControl.PurchaseReturnAc;
                    //Purchase A/C Id need to specify in setting
                    accountTransactionDetail.GlCode = purchaseReturnAc;

                    //Purchase Return A/C Id need to specify in setting
                    //accountTransactionDetail.GlCode = base.SystemControl.PurchaseReturnAc;
                    // accountTransactionDetail.AgentCode =UtilityService.GetAgentNameById(model.PurchaseReturnMaster.AgentCode);
                    accountTransactionDetail.CrAmt = Convert.ToDecimal(item.BasicAmt);
                    if (model.PurchaseReturnMaster.CurrencyId != null && model.PurchaseReturnMaster.CurrencyId != 0)
                    {
                        accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.BasicAmt) * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                    }
                    else
                    {
                        accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.BasicAmt);
                    }
                    accountTransactionDetail.DrAmt = 0;
                    accountTransactionDetail.LocalDrAmt = 0;
                    accountTransactionDetail.Source = source;
                    accountTransactionDetail.SNo = 0;
                    accountTransactionDetail.CreatedById = userId;
                    var isCashBankReturn = _ledger.GetById(purchaseReturnAc).IsCashBank;
                    if (isCashBankReturn)
                        accountTransactionDetail.CbCode = model.PurchaseReturnMaster.GlCode;
                    accountTransactionDetail.ReferenceId = model.PurchaseReturnMaster.Id;
                    accountTransactionDetail.FYId = FiscalYear.Id;
                    accountTransactionDetail.BranchId = CurrentBranchId;
                    accountTransactionDetail.Remarks = model.PurchaseReturnMaster.Remarks;
                    accountTransactionDetail.SlCode = model.PurchaseReturnMaster.SlCode;
                    accountTransactionDetail.AgentCode = Convert.ToString(model.PurchaseReturnMaster.AgentCode);
                    _accountTransaction.Add(accountTransactionDetail);

                    var stockTransaction = new StockTransaction();
                    stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.PurchaseReturnMaster.AgentCode);
                    stockTransaction.AltQty = purchaseReturnDetail.AltQty;
                    stockTransaction.AltStockQty = purchaseReturnDetail.AltStockQty;
                    stockTransaction.AltUnit = purchaseReturnDetail.AltUnit;
                    stockTransaction.CurrCode = model.PurchaseReturnMaster.CurrCode;
                    stockTransaction.CurrRate = model.PurchaseReturnMaster.CurrRate;
                    stockTransaction.GlCode = model.PurchaseReturnMaster.GlCode;
                    stockTransaction.NetAmt = purchaseReturnDetail.NetAmt;
                    stockTransaction.ProductCode = purchaseReturnDetail.ProductCode;
                    stockTransaction.Quantity = purchaseReturnDetail.Qty;
                    stockTransaction.Unit = unitcode;
                    stockTransaction.Rate = Convert.ToDecimal(purchaseReturnDetail.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = source;
                    stockTransaction.ReferenceId = model.PurchaseReturnMaster.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.BatchSerialNo = item.BatchSerialNo;
                    stockTransaction.Godown = Convert.ToString(purchaseReturnDetail.Godown);
                    var stock = _stockTransaction.Filter(x => x.ProductCode == item.ProductId).ToList().LastOrDefault();
                    if (stock == null)
                    {
                        stockTransaction.StockVal = Convert.ToDecimal(purchaseReturnDetail.BasicAmt);
                        stockTransaction.StockQty = purchaseReturnDetail.StockQty;

                    }
                    else
                    {
                        stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(purchaseReturnDetail.BasicAmt); ;
                        stockTransaction.StockQty = stock.StockQty - purchaseReturnDetail.StockQty;
                    }
                    stockTransaction.TermAmt = purchaseReturnDetail.TermAmt;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                    stockTransaction.VDate = model.PurchaseReturnMaster.InvoiceDate;
                    stockTransaction.VMiti = model.PurchaseReturnMaster.InvoiceMiti;
                    stockTransaction.VNo = model.PurchaseReturnMaster.InvoiceNo;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);

                    if (billTermList != null)
                    {
                        decimal netAmt = 0;
                        //var billWiseTerms =
                        //    billTermList.Where(x => !x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList();
                        foreach (var term in billTermList.Where(x => x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList())
                        {
                            var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                            decimal drAmt = 0;
                            decimal drLocalAmt = 0;
                            decimal crAmt = 0;
                            decimal crLocalAmt = 0;
                            decimal termAmt = 0;
                            if (term.Sign == "+")
                            {
                                crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                termAmt = crAmt;
                            }
                            else
                            {
                                drAmt = Convert.ToDecimal(term.Amount);
                                drLocalAmt = Convert.ToDecimal(term.Amount);
                                termAmt = System.Math.Abs(drAmt) * (-1);
                            }
                            var billingTermDetail = new BillingTermDetail();
                            billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                            var index = Convert.ToInt32(term.BillingTermIndex);
                            billingTermDetail.Index = index;
                            billingTermDetail.ReferenceId = model.PurchaseReturnMaster.Id;
                            billingTermDetail.Source = source;
                            billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                            billingTermDetail.TermAmount = termAmt;
                            billingTermDetail.DetailId = purchaseReturnDetail.Id;
                            billingTermDetail.Type = "P";
                            billingTermDetail.IsProductWise = term.IsProductWise;
                            _billingTermDetail.Add(billingTermDetail);

                            //AccountTransaction Posting
                            var accountTransactionBillingTerm = new AccountTransaction();
                            accountTransactionBillingTerm.VNo = model.PurchaseReturnMaster.InvoiceNo;
                            accountTransactionBillingTerm.VDate = model.PurchaseReturnMaster.InvoiceDate;
                            accountTransactionBillingTerm.VMiti = model.PurchaseReturnMaster.InvoiceMiti;
                            accountTransactionBillingTerm.CrRate = Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                            accountTransactionBillingTerm.CrCode = model.PurchaseReturnMaster.CurrCode;
                            accountTransactionBillingTerm.AgentCode = Convert.ToString(model.PurchaseReturnMaster.AgentCode);
                            accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                            if (model.PurchaseReturnMaster.CurrencyId != null && model.PurchaseReturnMaster.CurrencyId != 0)
                            {
                                drLocalAmt = drLocalAmt * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                                crLocalAmt = crLocalAmt * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                            }
                            accountTransactionBillingTerm.DrAmt = drAmt;
                            accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                            accountTransactionBillingTerm.CrAmt = crAmt;
                            accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                            accountTransactionBillingTerm.Source = source;
                            accountTransactionBillingTerm.SNo = 0;
                            accountTransactionBillingTerm.CreatedById = userId;
                            var isCashBankbill = _ledger.GetById(model.PurchaseReturnMaster.GlCode).IsCashBank;
                            if (isCashBankbill)
                                accountTransactionBillingTerm.CbCode = model.PurchaseReturnMaster.GlCode;
                            accountTransactionBillingTerm.FYId = FiscalYear.Id;
                            accountTransactionBillingTerm.ReferenceId = model.PurchaseReturnMaster.Id;
                            accountTransactionBillingTerm.BranchId = CurrentBranchId;
                            accountTransactionBillingTerm.Remarks = model.PurchaseReturnMaster.Remarks;
                            _accountTransaction.Add(accountTransactionBillingTerm);
                        }
                    }
                }
            }

            _purchaseReturnImpTransDoc.Add(model.PurchaseReturnImpTransDoc);

            if (billTermList != null)
            {
                foreach (var term in billTermList.Where(x => !x.IsProductWise).ToList())
                {
                    var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                    decimal drAmt = 0;
                    decimal drLocalAmt = 0;
                    decimal crAmt = 0;
                    decimal crLocalAmt = 0;
                    decimal termAmt = 0;
                    if (term.Sign == "+")
                    {
                        crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        termAmt = crAmt;
                    }
                    else
                    {
                        drAmt = Convert.ToDecimal(term.Amount);
                        drLocalAmt = Convert.ToDecimal(term.Amount);
                        termAmt = System.Math.Abs(drAmt) * (-1);
                    }
                    var billingTermDetail = new BillingTermDetail();
                    billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    billingTermDetail.Index = index;
                    billingTermDetail.ReferenceId = model.PurchaseReturnMaster.Id;
                    billingTermDetail.Source = source;
                    billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                    billingTermDetail.TermAmount = termAmt;
                    billingTermDetail.Type = "B";
                    billingTermDetail.IsProductWise = term.IsProductWise;
                    _billingTermDetail.Add(billingTermDetail);

                    //AccountTransaction Posting
                    var accountTransactionBillingTerm = new AccountTransaction();
                    accountTransactionBillingTerm.VNo = model.PurchaseReturnMaster.InvoiceNo;
                    accountTransactionBillingTerm.VDate = model.PurchaseReturnMaster.InvoiceDate;
                    accountTransactionBillingTerm.VMiti = model.PurchaseReturnMaster.InvoiceMiti;
                    accountTransactionBillingTerm.CrRate = 1;
                    accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                    if (model.PurchaseReturnMaster.CurrencyId != 0 && model.PurchaseReturnMaster.CurrencyId != null)
                    {
                        drLocalAmt = drLocalAmt * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                        crLocalAmt = crLocalAmt * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                    }
                    accountTransactionBillingTerm.DrAmt = drAmt;
                    accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                    accountTransactionBillingTerm.CrAmt = crAmt;
                    accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                    accountTransactionBillingTerm.Source = source;
                    accountTransactionBillingTerm.SNo = 0;
                    accountTransactionBillingTerm.CreatedById = userId;
                    var isCashBankbill = _ledger.GetById(model.PurchaseReturnMaster.GlCode).IsCashBank;
                    if (isCashBankbill)
                        accountTransactionBillingTerm.CbCode = model.PurchaseReturnMaster.GlCode;
                    accountTransactionBillingTerm.FYId = FiscalYear.Id;
                    accountTransactionBillingTerm.ReferenceId = model.PurchaseReturnMaster.Id;
                    accountTransactionBillingTerm.BranchId = CurrentBranchId;
                    accountTransactionBillingTerm.Remarks = model.PurchaseReturnMaster.Remarks;
                    _accountTransaction.Add(accountTransactionBillingTerm);
                }
            }

            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.PurchaseInvoice);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = model.PurchaseReturnMaster.Id,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.PurchaseInvoice
                    };

                    _udfData.Add(i);
                }
            }
            return Json(new { success = true, isEdit = false });
        }

        [CheckPermission(Permissions = "Edit", Module = "PR")]
        public ActionResult PurchaseReturnEdit(int id)
        {
            var data = _purchaseReturnMaster.GetById(x => x.Id == id);
            var model = _purchaseReturnImpTransDoc.GetById(x => x.Id == id);
            var transac = _purchaseReturnImpTransDoc.GetById(x => x.PurchaseReturnId == data.Id);
            var viewmodel = base.CreateViewModel<PurchaseReturnAddViewModel>();
            viewmodel.PurchaseReturnImpTransDoc = transac;
            viewmodel.PurchaseReturnMaster = data;
            viewmodel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.PurchaseInvoiceReturn).ToList();
            ViewBag.Id = data.Id;
            ViewBag.AllowProductWiseBillTerm = 0;
            if (!string.IsNullOrEmpty(data.CurrCode))
            {
                var CurrId = Convert.ToInt32(data.CurrCode);
                //viewmodel.PurchaseReturnMaster.CurrCode = data.CurrCode;
                viewmodel.PurchaseReturnMaster.CurrCode = _currency.GetById(x => x.Id == CurrId).Code;
                viewmodel.PurchaseReturnMaster.CurrencyId = _currency.GetById(x => x.Id == CurrId).Id;
            }
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                ViewBag.AllowProductWiseBillTerm = 1;
            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id && x.Source == "PR").ToList();
            //var defaultIndex = billTerm.Max(x => x.Index);

            var defaultIndex = data.PurchaseReturnDetails.Max(x => x.Index);
            ViewBag.Index = defaultIndex == null ? 1 : defaultIndex + 1;
            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }
            if (base.SystemControl.DateType == 1)
            {
                viewmodel.InvoiceDate = data.InvoiceDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewmodel.InvoiceDate = data.InvoiceMiti;
            }

            if (data.RefBillDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewmodel.RefDate = Convert.ToDateTime(data.RefBillDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewmodel.RefDate = data.RefBillMiti;
                }
            }

            if (data.DueDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewmodel.DueDate = Convert.ToDateTime(data.DueDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewmodel.DueDate = data.DueMiti;
                }
            }

            if (transac.PPDDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewmodel.PPDDate = Convert.ToDateTime(transac.PPDDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewmodel.PPDDate = transac.PPDMiti;
                }
            }

            if (transac.CnDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewmodel.CnDate = Convert.ToDateTime(transac.CnDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewmodel.CnDate = transac.CnMiti;
                }
            }

            foreach (var term in viewmodel.PurchaseReturnMaster.PurchaseReturnDetails)
            {
                term.UnitId = _unitRepository.GetById(x => x.Code.ToLower() == term.Unit.ToLower()).Id;
            }

            ViewBag.BillingTermList = billTerm;
            viewmodel.TotalAmtRs = viewmodel.PurchaseReturnMaster.NetAmt.ToString();
            viewmodel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            var context = new DataContext();
            var PurchaseProductBatchViewModels = (from pd in context.PurchaseReturnDetails.Where(x => x.PurchaseReturnId == id)
                                                  join pb in context.PurchaseProductBatches on pd.BatchId equals pb.Id
                                                  select new ProductBatchListForSales()
                                                  {
                                                      BatchId = pb.Id,
                                                      DetailId = pd.Id,
                                                      BatchSerialNo = pb.SerialNo,
                                                      StockQty = pb.StockQuantity
                                                  }).ToList();
            ViewBag.PurchaseProductBatchViewModels = PurchaseProductBatchViewModels;
            viewmodel.EntryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            return PartialView(viewmodel);
        }

        [HttpPost]
        public ActionResult PurchaseReturnEdit(IEnumerable<PurchaseReturnDetail> purchaseReturnDetailEntry, PurchaseReturnAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.PurchaseReturnMaster.FYId = FiscalYear.Id;
            if (model.PurchaseReturnMaster.CurrencyId != 0 && model.PurchaseReturnMaster.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(model.PurchaseReturnMaster.CurrencyId);
                // var Code = _currency.GetById(model.PurchaseReturnMaster.CurrencyId);
                var Code = _currency.GetById(currencyId);
                model.PurchaseReturnMaster.CurrCode = Convert.ToString(Code.Id);
            }
            if (base.SystemControl.DateType == 1)
            {
                model.PurchaseReturnMaster.InvoiceDate = Convert.ToDateTime(model.InvoiceDate);
                model.PurchaseReturnMaster.InvoiceMiti = NepaliDateService.NepaliDate(model.PurchaseReturnMaster.InvoiceDate).Date;
            }
            else
            {
                model.PurchaseReturnMaster.InvoiceMiti = model.InvoiceDate;
                model.PurchaseReturnMaster.InvoiceDate = NepaliDateService.NepalitoEnglishDate(model.InvoiceDate);
            }
            if (!string.IsNullOrEmpty(model.RefDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseReturnMaster.RefBillDate = Convert.ToDateTime(model.RefDate);
                    model.PurchaseReturnMaster.RefBillMiti = NepaliDateService.NepaliDate(model.PurchaseReturnMaster.RefBillDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseReturnMaster.RefBillMiti = model.RefDate;
                    model.PurchaseReturnMaster.RefBillDate = NepaliDateService.NepalitoEnglishDate(model.RefDate);
                }
            }

            if (!string.IsNullOrEmpty(model.DueDate) && !string.IsNullOrEmpty(model.DueDate.Trim()))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseReturnMaster.DueDate = Convert.ToDateTime(model.DueDate);
                    model.PurchaseReturnMaster.DueMiti = NepaliDateService.NepaliDate(model.PurchaseReturnMaster.DueDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseReturnMaster.DueMiti = model.DueDate;
                    model.PurchaseReturnMaster.DueDate = NepaliDateService.NepalitoEnglishDate(model.DueDate);
                }
            }
            _purchaseReturnMaster.Update(model.PurchaseReturnMaster);
            if (!string.IsNullOrEmpty(model.PPDDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseReturnImpTransDoc.PPDDate = Convert.ToDateTime(model.PPDDate);
                    model.PurchaseReturnImpTransDoc.PPDMiti = NepaliDateService.NepaliDate(model.PurchaseReturnImpTransDoc.PPDDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseReturnImpTransDoc.PPDMiti = model.PPDDate;
                    model.PurchaseReturnImpTransDoc.PPDDate = NepaliDateService.NepalitoEnglishDate(model.PPDDate);
                }
            }
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseReturnImpTransDoc.CnDate = Convert.ToDateTime(model.CnDate);
                    model.PurchaseReturnImpTransDoc.CnMiti = NepaliDateService.NepaliDate(model.PurchaseReturnImpTransDoc.CnDate.ToDate()).Date;
                }
                else
                {
                    model.PurchaseReturnImpTransDoc.CnMiti = model.CnDate;
                    model.PurchaseReturnImpTransDoc.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }
            if (model.PurchaseReturnImpTransDoc.Id == 0)
                _purchaseReturnImpTransDoc.Add(model.PurchaseReturnImpTransDoc);
            else
                _purchaseReturnImpTransDoc.Update(model.PurchaseReturnImpTransDoc);
            var Source = StringEnum.Parse(typeof(ModuleEnum), "Purchase Return").ToString();
            _accountTransaction.Delete(x => x.ReferenceId == model.PurchaseReturnMaster.Id && x.Source == Source);
            _purchaseReturnDetail.Delete(x => x.PurchaseReturnId == model.PurchaseReturnMaster.Id);
            _stockTransaction.Delete(x => x.ReferenceId == model.PurchaseReturnMaster.Id && x.Source == Source);
            _billingTermDetail.Delete(x => x.ReferenceId == model.PurchaseReturnMaster.Id && x.Source == Source);

            var tempPurchaseReturnDetailList = new List<PurchaseReturnDetail>();

            int sno = 1;
            var accountTransactionMaster = new AccountTransaction();
            accountTransactionMaster.VNo = model.PurchaseReturnMaster.InvoiceNo;
            accountTransactionMaster.VDate = model.PurchaseReturnMaster.InvoiceDate;
            accountTransactionMaster.VMiti = model.PurchaseReturnMaster.InvoiceMiti;
            accountTransactionMaster.AgentCode = Convert.ToString(model.PurchaseReturnMaster.AgentCode);
            accountTransactionMaster.CrRate = Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
            accountTransactionMaster.CrCode = model.PurchaseReturnMaster.CurrCode;
            accountTransactionMaster.GlCode = model.PurchaseReturnMaster.GlCode;
            accountTransactionMaster.SlCode = model.PurchaseReturnMaster.SlCode;
            accountTransactionMaster.CrAmt = 0;
            accountTransactionMaster.LocalCrAmt = 0;
            accountTransactionMaster.DrAmt = Convert.ToDecimal(model.PurchaseReturnMaster.NetAmt);
            if (model.PurchaseReturnMaster.CurrencyId != null && model.PurchaseReturnMaster.CurrencyId != 0)
            {
                accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(model.PurchaseReturnMaster.NetAmt) * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
            }
            else
            {
                accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(model.PurchaseReturnMaster.NetAmt);
            }
            accountTransactionMaster.Source = Source;
            accountTransactionMaster.SNo = 0;
            accountTransactionMaster.CreatedById = userId;
            var isCashBank = _ledger.GetById(model.PurchaseReturnMaster.GlCode).IsCashBank;
            if (isCashBank)
                accountTransactionMaster.CbCode = model.PurchaseReturnMaster.GlCode;
            accountTransactionMaster.ReferenceId = model.PurchaseReturnMaster.Id;
            accountTransactionMaster.FYId = FiscalYear.Id;
            accountTransactionMaster.BranchId = CurrentBranchId;
            accountTransactionMaster.Remarks = model.PurchaseReturnMaster.Remarks;
            _accountTransaction.Add(accountTransactionMaster);
            foreach (var item in purchaseReturnDetailEntry)
            {
                if (item.ProductCode != null && item.ProductCode != 0 && item.BasicAmt != null && item.BasicAmt != 0 && item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    sno++;
                    var unitcode = _unitRepository.GetById(item.UnitId).Code;
                    var purchaseReturnDetail = new PurchaseReturnDetail();
                    purchaseReturnDetail.SNo = sno;
                    purchaseReturnDetail.ProductCode = item.ProductCode;
                    purchaseReturnDetail.AltQty = item.AltQty;
                    purchaseReturnDetail.Qty = Convert.ToDecimal(item.Qty);
                    purchaseReturnDetail.AltStockQty = item.AltQty;
                    purchaseReturnDetail.StockQty = Convert.ToDecimal(item.Qty);
                    purchaseReturnDetail.Rate = Convert.ToDecimal(item.Rate);
                    purchaseReturnDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    purchaseReturnDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    purchaseReturnDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    purchaseReturnDetail.Remarks = model.PurchaseReturnMaster.Remarks;
                    purchaseReturnDetail.PurchaseReturnId = model.PurchaseReturnMaster.Id;
                    purchaseReturnDetail.Index = item.Index;
                    purchaseReturnDetail.Unit = unitcode;
                    purchaseReturnDetail.AltUnit = item.AltUnit;
                    purchaseReturnDetail.Godown = item.Godown;
                    purchaseReturnDetail.Remarks = item.Remarks;
                    _purchaseReturnDetail.Add(purchaseReturnDetail);
                    tempPurchaseReturnDetailList.Add(purchaseReturnDetail);

                    var accountTransactionDetail = new AccountTransaction();
                    accountTransactionDetail.VNo = model.PurchaseReturnMaster.InvoiceNo;
                    accountTransactionDetail.VDate = model.PurchaseReturnMaster.InvoiceDate;
                    accountTransactionDetail.VMiti = model.PurchaseReturnMaster.InvoiceMiti;
                    accountTransactionDetail.CrRate = Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                    accountTransactionDetail.CrCode = model.PurchaseReturnMaster.CurrCode;
                    //Purchase Return A/C Id need to specify in setting
                    int purchaseReturnAc;
                    var product = _product.GetById(item.ProductCode);
                    purchaseReturnAc = product.PurchaseReturnId != null ? Convert.ToInt32(product.PurchaseReturnId) : base.SystemControl.PurchaseReturnAc;
                    //Purchase A/C Id need to specify in setting
                    // accountTransactionDetail.AgentCode =UtilityService.GetAgentNameById(model.PurchaseReturnMaster.AgentCode);
                    accountTransactionDetail.GlCode = purchaseReturnAc;
                    accountTransactionDetail.SlCode = model.PurchaseReturnMaster.SlCode;
                    accountTransactionDetail.AgentCode = Convert.ToString(model.PurchaseReturnMaster.AgentCode);
                    accountTransactionDetail.CrAmt = Convert.ToDecimal(item.BasicAmt);

                    if (model.PurchaseReturnMaster.CurrencyId != null && model.PurchaseReturnMaster.CurrencyId != 0)
                    {
                        accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.BasicAmt) * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                    }
                    else
                    {
                        accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.BasicAmt);
                    }
                    accountTransactionDetail.DrAmt = 0;
                    accountTransactionDetail.LocalDrAmt = 0;
                    accountTransactionDetail.Source = Source;
                    accountTransactionDetail.SNo = 0;
                    accountTransactionDetail.CreatedById = userId;
                    var isCashBankdetail = _ledger.GetById(purchaseReturnAc).IsCashBank;
                    if (isCashBankdetail)
                        accountTransactionDetail.CbCode = model.PurchaseReturnMaster.GlCode;
                    accountTransactionDetail.ReferenceId = model.PurchaseReturnMaster.Id;
                    accountTransactionDetail.FYId = FiscalYear.Id;
                    accountTransactionDetail.BranchId = CurrentBranchId;
                    accountTransactionDetail.Remarks = model.PurchaseReturnMaster.Remarks;
                    _accountTransaction.Add(accountTransactionDetail);

                    var stockTransaction = new StockTransaction();
                    stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.PurchaseReturnMaster.AgentCode);
                    stockTransaction.AltQty = purchaseReturnDetail.AltQty;
                    stockTransaction.AltStockQty = purchaseReturnDetail.AltStockQty;
                    stockTransaction.AltUnit = purchaseReturnDetail.AltUnit;
                    stockTransaction.CurrCode = model.PurchaseReturnMaster.CurrCode;
                    stockTransaction.CurrRate = model.PurchaseReturnMaster.CurrRate;
                    stockTransaction.GlCode = model.PurchaseReturnMaster.GlCode;
                    stockTransaction.NetAmt = purchaseReturnDetail.NetAmt;
                    stockTransaction.ProductCode = purchaseReturnDetail.ProductCode;
                    stockTransaction.Quantity = purchaseReturnDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(purchaseReturnDetail.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = Source;
                    stockTransaction.Unit = unitcode;
                    stockTransaction.ReferenceId = model.PurchaseReturnMaster.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.Godown = Convert.ToString(purchaseReturnDetail.Godown);
                    var stock = _stockTransaction.Filter(x => x.ProductCode == item.ProductCode).ToList().LastOrDefault();
                    if (stock == null)
                    {
                        stockTransaction.StockVal = Convert.ToDecimal(purchaseReturnDetail.BasicAmt);
                        stockTransaction.StockQty = purchaseReturnDetail.StockQty;

                    }
                    else
                    {
                        stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(purchaseReturnDetail.BasicAmt); ;
                        stockTransaction.StockQty = stock.StockQty - purchaseReturnDetail.StockQty;
                    }
                    stockTransaction.TermAmt = purchaseReturnDetail.TermAmt;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                    stockTransaction.VDate = model.PurchaseReturnMaster.InvoiceDate;
                    stockTransaction.VMiti = model.PurchaseReturnMaster.InvoiceMiti;
                    stockTransaction.VNo = model.PurchaseReturnMaster.InvoiceNo;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);

                    if (billTermList != null)
                    {
                        decimal netAmt = 0;
                        //var billWiseTerms =
                        //    billTermList.Where(x => !x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList();
                        foreach (var term in billTermList.Where(x => x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList())
                        {
                            var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                            decimal drAmt = 0;
                            decimal drLocalAmt = 0;
                            decimal crAmt = 0;
                            decimal crLocalAmt = 0;
                            decimal termAmt = 0;
                            if (term.Sign == "+")
                            {
                                crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                termAmt = crAmt;
                            }
                            else
                            {
                                drAmt = Convert.ToDecimal(term.Amount);
                                drLocalAmt = Convert.ToDecimal(term.Amount);
                                termAmt = System.Math.Abs(drAmt) * (-1);
                            }
                            var billingTermDetail = new BillingTermDetail();
                            billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                            var index = Convert.ToInt32(term.BillingTermIndex);
                            billingTermDetail.Index = index;
                            billingTermDetail.ReferenceId = model.PurchaseReturnMaster.Id;
                            billingTermDetail.Source = Source;
                            billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                            billingTermDetail.TermAmount = termAmt;
                            billingTermDetail.DetailId = purchaseReturnDetail.Id;
                            billingTermDetail.Type = "P";
                            billingTermDetail.IsProductWise = term.IsProductWise;
                            _billingTermDetail.Add(billingTermDetail);

                            //AccountTransaction Posting
                            var accountTransactionBillingTerm = new AccountTransaction();
                            accountTransactionBillingTerm.VNo = model.PurchaseReturnMaster.InvoiceNo;
                            accountTransactionBillingTerm.VDate = model.PurchaseReturnMaster.InvoiceDate;
                            accountTransactionBillingTerm.VMiti = model.PurchaseReturnMaster.InvoiceMiti;
                            accountTransactionBillingTerm.CrRate = Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                            accountTransactionBillingTerm.CrCode = model.PurchaseReturnMaster.CurrCode;
                            accountTransactionBillingTerm.AgentCode = Convert.ToString(model.PurchaseReturnMaster.AgentCode);
                            accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                            if (model.PurchaseReturnMaster.CurrencyId != null && model.PurchaseReturnMaster.CurrencyId != 0)
                            {
                                drLocalAmt = drLocalAmt * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                                crLocalAmt = crLocalAmt * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                            }
                            accountTransactionBillingTerm.DrAmt = drAmt;
                            accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                            accountTransactionBillingTerm.CrAmt = crAmt;
                            accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                            accountTransactionBillingTerm.Source = Source;
                            accountTransactionBillingTerm.SNo = 0;
                            accountTransactionBillingTerm.CreatedById = userId;
                            var isCashBankbill = _ledger.GetById(model.PurchaseReturnMaster.GlCode).IsCashBank;
                            if (isCashBankbill)
                                accountTransactionBillingTerm.CbCode = model.PurchaseReturnMaster.GlCode;
                            accountTransactionBillingTerm.FYId = FiscalYear.Id;
                            accountTransactionBillingTerm.ReferenceId = model.PurchaseReturnMaster.Id;
                            accountTransactionBillingTerm.BranchId = CurrentBranchId;
                            accountTransactionBillingTerm.Remarks = model.PurchaseReturnMaster.Remarks;
                            _accountTransaction.Add(accountTransactionBillingTerm);
                        }
                    }

                }
            }

            if (billTermList != null)
            {
                foreach (var term in billTermList.Where(x => !x.IsProductWise).ToList())
                {
                    var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                    decimal drAmt = 0;
                    decimal drLocalAmt = 0;
                    decimal crAmt = 0;
                    decimal crLocalAmt = 0;
                    decimal termAmt = 0;
                    if (term.Sign == "+")
                    {
                        crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        termAmt = crAmt;
                    }
                    else
                    {
                        drAmt = Convert.ToDecimal(term.Amount);
                        drLocalAmt = Convert.ToDecimal(term.Amount);
                        termAmt = System.Math.Abs(drAmt) * (-1);
                    }
                    var billingTermDetail = new BillingTermDetail();
                    billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    billingTermDetail.Index = index;
                    billingTermDetail.ReferenceId = model.PurchaseReturnMaster.Id;
                    billingTermDetail.Source = Source;
                    billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                    billingTermDetail.TermAmount = termAmt;
                    billingTermDetail.Type = "B";
                    billingTermDetail.IsProductWise = term.IsProductWise;
                    _billingTermDetail.Add(billingTermDetail);

                    //AccountTransaction Posting
                    var accountTransactionBillingTerm = new AccountTransaction();
                    accountTransactionBillingTerm.VNo = model.PurchaseReturnMaster.InvoiceNo;
                    accountTransactionBillingTerm.VDate = model.PurchaseReturnMaster.InvoiceDate;
                    accountTransactionBillingTerm.VMiti = model.PurchaseReturnMaster.InvoiceMiti;
                    accountTransactionBillingTerm.CrRate = 1;
                    accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                    if (model.PurchaseReturnMaster.CurrencyId != 0 && model.PurchaseReturnMaster.CurrencyId != null)
                    {
                        drLocalAmt = drLocalAmt * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                        crLocalAmt = crLocalAmt * Convert.ToDecimal(model.PurchaseReturnMaster.CurrRate);
                    }
                    accountTransactionBillingTerm.DrAmt = drAmt;
                    accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                    accountTransactionBillingTerm.CrAmt = crAmt;
                    accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                    accountTransactionBillingTerm.Source = Source;
                    accountTransactionBillingTerm.SNo = 0;
                    accountTransactionBillingTerm.CreatedById = userId;
                    var isCashBankbill = _ledger.GetById(model.PurchaseReturnMaster.GlCode).IsCashBank;
                    if (isCashBankbill)
                        accountTransactionBillingTerm.CbCode = model.PurchaseReturnMaster.GlCode;
                    accountTransactionBillingTerm.FYId = FiscalYear.Id;
                    accountTransactionBillingTerm.ReferenceId = model.PurchaseReturnMaster.Id;
                    accountTransactionBillingTerm.BranchId = CurrentBranchId;
                    accountTransactionBillingTerm.Remarks = model.PurchaseReturnMaster.Remarks;
                    _accountTransaction.Add(accountTransactionBillingTerm);
                }
            }

            _udfData.Delete(x => x.ReferenceId == model.PurchaseReturnMaster.Id && x.SourceId == (int)EntryModuleEnum.PurchaseInvoiceReturn);
            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.PurchaseInvoiceReturn);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = model.PurchaseReturnMaster.Id,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.PurchaseInvoiceReturn
                    };
                    _udfData.Add(i);
                }
            }
            return Json(new { success = true, isEdit = true });
        }

        [CheckPermission(Permissions = "Approve", Module = "PR")]
        [HttpPost]
        public ActionResult PurchaseReturnApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _purchaseReturnMaster.GetById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _purchaseReturnMaster.Update(data);
            return null;
        }

        [CheckPermission(Permissions = "Create", Module = "PR")]
        public ActionResult GetPurchaseReturnDetailEntry(int? index)
        {
            var viewModel = new PurchaseReturnDetailEntryViewModel();
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            if (index != 0)
                viewModel.Index = Convert.ToInt32(index);
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            viewModel.EntryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            viewModel.DetailGuid = Guid.NewGuid().ToString();
            return PartialView("_PartialPurchaseReturnDetailEntry", viewModel);
        }

        [CheckPermission(Permissions = "Edit", Module = "PR")]
        public ActionResult GetPurchaseReturnDetailEdit()
        {
            var data = new PurchaseReturnDetail();
            data.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            data.DetailGuid = Guid.NewGuid().ToString();
            return PartialView("_PartialPurchaseReturnDetailEdit", data);
        }
        #endregion

        #region Purchase Order
        public JsonResult CheckPurchaseOrderInvoiceNo(string OrderNo, int? Id)
        {
            var order = new PurchaseOrderMaster();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _purchaseOrderMaster.GetById(x => x.Id == Id);
                if (data.OrderNo.ToLower().Trim() != OrderNo.ToLower().Trim())
                {
                    order =
                        _purchaseOrderMaster.GetById(x => x.OrderNo.ToLower().Trim() == OrderNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                order = _purchaseOrderMaster.GetById(x => x.OrderNo.ToLower().Trim() == OrderNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (order == null)
            {
                order = new PurchaseOrderMaster();
            }

            return order.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Navigate", Module = "PB")]
        public ActionResult PurchaseOrder()
        {
            ViewBag.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            var viewModel = new PurchaseOrderViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.Category == cat && !x.IsDeleted);
            var billingTermProductWise = billingTerm.Where(x => x.IsProductWise).OrderBy(x => x.DispalyOrder);
            var billingTermBillWise = billingTerm.Where(x => !x.IsProductWise).OrderBy(x => x.DispalyOrder);
            viewModel.ProductWiseBillTerms = new List<BillingTermSelectViewModel>();
            viewModel.BillWiseBillTerms = new List<BillingTermSelectViewModel>();
            billingTermProductWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.ProductWiseBillTerms.Add(billingTermSelectList);
            });
            billingTermBillWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.BillWiseBillTerms.Add(billingTermSelectList);
            });
            return View(viewModel);
        }

        [CheckPermission(Permissions = "Navigate", Module = "PB")]
        public ActionResult PurchaseOrderList(int pageNo = 1)
        {
            var viewModel = new PurchaseOrderViewModel();
            var test =
                _purchaseOrderMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted).
                    AsQueryable().OrderBy(x => x.Id);
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var listPending = _purchaseOrderMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            //  var listAccepted = _purchaseOrderMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            //  viewModel.AcceptedList = listAccepted;
            viewModel.PendingList = listPending;
            return PartialView(viewModel);
        }

        public ActionResult PurchaseOrderPendingList(int pageNo)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var list = _purchaseOrderMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView(list);
        }

        //public ActionResult PurchaseOrderAcceptedList(int pageNo)
        //{
        //    var list = _purchaseOrderMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
        //    return PartialView(list);
        //}

        public ActionResult PurchaseOrderDelete(int id)
        {
            var model = _purchaseOrderMaster.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _purchaseOrderMaster.Update(model);
            _stockTransaction.Delete(x => x.Source == "PO" && x.ReferenceId == id);
            return Json(true);
        }
        public ActionResult CheckFiscalyearDate(string OrderDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(OrderDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(OrderDate);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {

                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }

        [CheckPermission(Permissions = "Create", Module = "PB")]
        public ActionResult PurchaseOrderAdd()
        {
            //var data = _purchaseInvoice.GetAll().LastOrDefault();
            var viewModel = base.CreateViewModel<PurchaseOrderAddViewModel>();
            viewModel.AllowProductWiseBillTerm = false;

            // viewModel.OrderDate = data != null ? data.InvoiceDate : DateTime.UtcNow;
            viewModel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.PurchaseInvoice).ToList();

            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.PurchaseOrder = new PurchaseOrderMaster();
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            viewModel.EntryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.OrderDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _purchaseOrderMaster.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewModel.OrderDate = base.SystemControl.DateType == 1 ? data.OrderDate.ToShortDateString() : data.OrderMiti;
                }
                else
                {
                    viewModel.OrderDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseOrderAdd(IEnumerable<PurchaseDetailEntryViewModel> purchaseDetailEntry, PurchaseOrderAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            if (model.PurchaseOrder.GlCode != 0)
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                if (model.PurchaseOrder.CurrencyId != null && model.PurchaseOrder.CurrencyId != 0 )
                {
                    var currencyId = Convert.ToInt32(model.PurchaseOrder.CurrencyId);
                   var Code = _currency.GetById(currencyId);
                    model.PurchaseOrder.CurrencyCode = Convert.ToString(Code.Id);
                }
                model.PurchaseOrder.BasicAmt = Convert.ToDecimal(model.PurchaseOrder.BasicAmt);
                model.PurchaseOrder.NetAmt = Convert.ToDecimal(model.PurchaseOrder.NetAmt);
                model.PurchaseOrder.TermAmt = Convert.ToDecimal(model.PurchaseOrder.TermAmt);
                model.PurchaseOrder.CreatedById = userId;
                model.PurchaseOrder.CreatedDate = DateTime.UtcNow;
                // model.PurchaseOrder.OrderDate = model.OrderDate;
                model.PurchaseOrder.FYId = FiscalYear.Id;
                model.PurchaseOrder.BranchId = CurrentBranchId;
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseOrder.OrderDate = model.OrderDate.ToDate();
                    model.PurchaseOrder.OrderMiti = NepaliDateService.NepaliDate(model.PurchaseOrder.OrderDate).Date;
                }
                else
                {
                    model.PurchaseOrder.OrderMiti = model.OrderDate;
                    model.PurchaseOrder.OrderDate = NepaliDateService.NepalitoEnglishDate(model.OrderDate);
                }

                _purchaseOrderMaster.Add(model.PurchaseOrder);

                if (model.PurchaseOrder.DocId != null)
                {
                    var docId = Convert.ToInt32(model.PurchaseOrder.DocId);
                    var documentnumber = _docNumering.GetById(docId);
                    documentnumber.DocCurrNo += 1;
                    _docNumering.Update(documentnumber);
                }

                var source = StringEnum.Parse(typeof(ModuleEnum), "Purchase Order").ToString();
                int sno = 1;
                decimal basicAmt = 0;
                var tempPurchaseOrderDetailList = new List<PurchaseOrderDetail>();
                foreach (var item in purchaseDetailEntry)
                {
                    if (item.ProductId != null && item.ProductId != 0 && item.BasicAmt != null && item.BasicAmt != 0 &&
                        item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                    {
                        var unitcode = _unitRepository.GetById(item.Unit).Code;
                        basicAmt += Convert.ToDecimal(item.BasicAmt);
                        var purchaseOrderDetail = new PurchaseOrderDetail();
                        purchaseOrderDetail.SNo = sno;
                        purchaseOrderDetail.ProductCode = item.ProductId;
                        purchaseOrderDetail.AltQty = item.AltQty;
                        purchaseOrderDetail.Qty = Convert.ToDecimal(item.Qty);
                        purchaseOrderDetail.AltStockQty = item.AltQty;
                        purchaseOrderDetail.StockQty = Convert.ToDecimal(item.Qty);
                        purchaseOrderDetail.Rate = Convert.ToDecimal(item.Rate);
                        purchaseOrderDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                        purchaseOrderDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                        purchaseOrderDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                        purchaseOrderDetail.Remarks = model.PurchaseOrder.Remarks;
                        purchaseOrderDetail.OrderId = model.PurchaseOrder.Id;
                        purchaseOrderDetail.Unit = unitcode;
                        purchaseOrderDetail.AltUnit = item.AltUnit;
                        _purchaseOrderDetail.Add(purchaseOrderDetail);
                        tempPurchaseOrderDetailList.Add(purchaseOrderDetail);

                        //var stockTransaction = new StockTransaction();
                        //stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.PurchaseOrder.AgentCode);
                        //stockTransaction.VMiti = model.PurchaseOrder.OrderMiti;
                        //stockTransaction.AltQty = purchaseOrderDetail.AltQty;
                        //stockTransaction.AltStockQty = purchaseOrderDetail.AltStockQty;
                        //stockTransaction.AltUnit = purchaseOrderDetail.AltUnit;
                        //stockTransaction.CurrCode = "";
                        //stockTransaction.CurrRate = 0;
                        //stockTransaction.GlCode = model.PurchaseOrder.GlCode;
                        //stockTransaction.NetAmt = Convert.ToDecimal(purchaseOrderDetail.NetAmt);
                        //stockTransaction.ProductCode = Convert.ToInt32(purchaseOrderDetail.ProductCode);
                        //stockTransaction.Quantity = purchaseOrderDetail.Qty;
                        //stockTransaction.Rate = Convert.ToDecimal(purchaseOrderDetail.Rate);
                        //stockTransaction.SNo = sno;
                        //stockTransaction.Source = source;
                        //stockTransaction.ReferenceId = model.PurchaseOrder.Id;
                        //stockTransaction.Unit = unitcode;
                        //stockTransaction.AltUnit = item.AltUnit;
                        //stockTransaction.FYId = FiscalYear.Id;
                        //var stock =
                        //    _stockTransaction.Filter(x => x.ProductCode == item.ProductId).ToList().LastOrDefault();
                        //if (stock == null)
                        //{
                        //    stockTransaction.StockVal = Convert.ToDecimal(purchaseOrderDetail.BasicAmt);
                        //    stockTransaction.StockQty = Convert.ToDecimal(purchaseOrderDetail.StockQty);

                        //}
                        //else
                        //{
                        //    stockTransaction.StockVal = stock.StockVal + Convert.ToDecimal(purchaseOrderDetail.BasicAmt);
                        //    stockTransaction.StockQty = stock.StockQty + Convert.ToDecimal(purchaseOrderDetail.StockQty);
                        //}
                        //stockTransaction.TermAmt = purchaseOrderDetail.TermAmt;
                        //stockTransaction.TransactionType =
                        //    StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();

                        //stockTransaction.VDate = model.PurchaseOrder.OrderDate;
                        //stockTransaction.VNo = model.PurchaseOrder.OrderNo;
                        //stockTransaction.BranchId = CurrentBranchId;
                        //_stockTransaction.Add(stockTransaction);
                        sno++;
                    }
                }


                if (billTermList != null)
                {
                    foreach (var term in billTermList)
                    {
                        var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                        decimal drAmt = 0;
                        decimal drLocalAmt = 0;
                        decimal crAmt = 0;
                        decimal crLocalAmt = 0;
                        decimal termAmt = 0;
                        if (term.Sign == "-")
                        {
                            crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                            termAmt = System.Math.Abs(crAmt) * (-1);
                        }
                        else
                        {
                            drAmt = Convert.ToDecimal(term.Amount);
                            termAmt = drAmt;
                        }


                        var billingTermDetail = new BillingTermDetail();
                        billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                        var index = Convert.ToInt32(term.BillingTermIndex);
                        billingTermDetail.Index = index;
                        billingTermDetail.ReferenceId = model.PurchaseOrder.Id;
                        billingTermDetail.Source = source;
                        billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                        if (term.IsProductWise)
                        {
                            billingTermDetail.DetailId = tempPurchaseOrderDetailList[index].Id;
                        }
                        billingTermDetail.TermAmount = termAmt;
                        billingTermDetail.IsProductWise = term.IsProductWise;
                        _billingTermDetail.Add(billingTermDetail);
                    }
                }

                //model.PurchaseOrderImpTransDoc.PurchaseOrderId = model.PurchaseOrder.Id;
                //if (!string.IsNullOrEmpty(model.PPDDate))
                //{
                //    if (base.SystemControl.DateType == 1)
                //    {
                //        model.PurchaseOrderImpTransDoc.PPDDate = model.PPDDate.ToDate();
                //        model.PurchaseOrderImpTransDoc.PPDmiti = NepaliDateService.NepaliDate(model.PPDDate.ToDate()).Date;
                //    }
                //    else
                //    {
                //        model.PurchaseOrderImpTransDoc.PPDmiti = model.PPDDate;
                //        model.PurchaseOrderImpTransDoc.PPDDate = NepaliDateService.NepalitoEnglishDate(model.PPDDate);
                //    }
                //}
                //if (!string.IsNullOrEmpty(model.CnDate))
                //{
                //    if (base.SystemControl.DateType == 1)
                //    {
                //        model.PurchaseOrderImpTransDoc.CnDate = model.CnDate.ToDate();
                //        model.PurchaseOrderImpTransDoc.CnMiti = NepaliDateService.NepaliDate(model.CnDate.ToDate()).Date;
                //    }
                //    else
                //    {
                //        model.PurchaseOrderImpTransDoc.CnMiti = model.CnDate;
                //        model.PurchaseOrderImpTransDoc.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                //    }
                //}

                //_purchaseOrderImpTransDoc.Add(model.PurchaseOrderImpTransDoc);
                var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.PurchaseOrder);
                if (data != null)
                {
                    foreach (var udfEntry in data)
                    {
                        var value = formCollection[udfEntry.FieldName];
                        var i = new UDFData()
                        {
                            ReferenceId = model.PurchaseOrder.Id,
                            UdfId = udfEntry.Id,
                            Value = value,
                            SourceId = (int)EntryModuleEnum.PurchaseOrder
                        };

                        _udfData.Add(i);
                    }
                }
                return Json(new { success = true, isEdit = false });
            }
            return Json(new { success = false, isEdit = false });
        }

        [CheckPermission(Permissions = "Edit", Module = "PB")]
        public ActionResult PurchaseOrderEdit(int id)
        {
            var data = _purchaseOrderMaster.GetById(id);
            //  var model = _purchaseOrderImpTransDoc.GetById(x => x.PurchaseOrderId == id);
            var viewModel = base.CreateViewModel<PurchaseOrderAddViewModel>();
            viewModel.PurchaseOrder = data;
            var date = DateTime.UtcNow.ToString("MM/dd/yyyy");
            data.CreatedDate =date.ToDate();
            viewModel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.PurchaseInvoice).ToList();
            ViewBag.Id = data.Id;
            ViewBag.AllowProductWiseBillTerm = 0;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
            {
                ViewBag.AllowProductWiseBillTerm = 1;
                viewModel.AllowProductWiseBillTerm = true;
            }
            foreach (var term in viewModel.PurchaseOrder.PurchaseOrderDetails)
            {
                term.UnitId = _unitRepository.GetById(x => x.Code.ToLower() == term.Unit.ToLower()).Id;
            }

            /* var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;*/
            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id && x.Source == "PO").ToList();

            //var defaultIndex = billTerm.Max(x => x.Index);
            //var defaultIndex = data.PurchaseOrderDetails.Max(x => x.Index);

            ViewBag.Index = 1;
            //defaultIndex == null ? 1 : defaultIndex + 1;
            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }
           
            if (base.SystemControl.DateType == 1)
            {
                viewModel.OrderDate = data.OrderDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.OrderDate = data.OrderMiti;
            }
            //if (model.PPDDate != null)
            //{
            //    if (base.SystemControl.DateType == 1)
            //    {
            //        viewModel.PPDDate = Convert.ToDateTime(model.PPDDate).ToString("MM/dd/yyyy");
            //    }
            //    else
            //    {
            //        viewModel.PPDDate = model.PPDmiti;
            //    }
            //}
            //if (model.CnDate != null)
            //{
            //    if (base.SystemControl.DateType == 1)
            //    {
            //        viewModel.CnDate = Convert.ToDateTime(model.CnDate).ToString("MM/dd/yyyy");
            //    }
            //    else
            //    {
            //        viewModel.CnDate = model.CnMiti;
            //    }
            //}
            if (!string.IsNullOrEmpty(data.CurrencyCode))
            {
               var CurrId = Convert.ToInt32(data.CurrencyCode);
                viewModel.PurchaseOrder.CurrencyCode = _currency.GetById(x => x.Id == CurrId).Code;
                viewModel.PurchaseOrder.CurrencyId = _currency.GetById(x => x.Id == CurrId).Id;
            }
            viewModel.EntryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            ViewBag.BillingTermList = billTerm;
            viewModel.TotalAmtRs = viewModel.PurchaseOrder.NetAmt.ToString();
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");

            // viewModel.PurchaseOrderImpTransDoc = _purchaseOrderImpTransDoc.GetById(x => x.PurchaseOrderId == id);
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseOrderEdit(IEnumerable<PurchaseOrderDetail> purchaseDetailEntry, PurchaseOrderAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
            //if (ModelState.IsValid)
            //{
                //var userId = _authentication.GetAuthenticatedUser().Id;
                model.PurchaseOrder.FYId = FiscalYear.Id;
                model.PurchaseOrder.BasicAmt = Convert.ToDecimal(model.PurchaseOrder.BasicAmt);
                model.PurchaseOrder.NetAmt = Convert.ToDecimal(model.PurchaseOrder.NetAmt);
                model.PurchaseOrder.TermAmt = Convert.ToDecimal(model.PurchaseOrder.TermAmt);
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseOrder.OrderDate = Convert.ToDateTime(model.OrderDate);
                    model.PurchaseOrder.OrderMiti = NepaliDateService.NepaliDate(model.PurchaseOrder.OrderDate).Date;
                }
                else
                {
                    model.PurchaseOrder.OrderMiti = model.OrderDate;
                    model.PurchaseOrder.OrderDate = NepaliDateService.NepalitoEnglishDate(model.OrderDate);
                }
                if ( model.PurchaseOrder.CurrencyId != null&&model.PurchaseOrder.CurrencyId != 0 )
                {
                    var currencyId = Convert.ToInt32(model.PurchaseOrder.CurrencyId);
                   var Code = _currency.GetById(currencyId);
                    model.PurchaseOrder.CurrencyCode = Convert.ToString(Code.Id);

                }
                _purchaseOrderMaster.Update(model.PurchaseOrder);
                //if (!string.IsNullOrEmpty(model.PPDDate))
                //{
                //    if (base.SystemControl.DateType == 1)
                //    {
                //        model.PurchaseOrderImpTransDoc.PPDDate = Convert.ToDateTime(model.PPDDate);
                //        model.PurchaseOrderImpTransDoc.PPDmiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.PurchaseOrderImpTransDoc.PPDDate)).Date;
                //    }
                //    else
                //    {
                //        model.PurchaseOrderImpTransDoc.PPDmiti = model.PPDDate;
                //        model.PurchaseOrderImpTransDoc.PPDDate = NepaliDateService.NepalitoEnglishDate(model.PPDDate);
                //    }

                //}
                //if (!string.IsNullOrEmpty(model.CnDate))
                //{
                //    if (base.SystemControl.DateType == 1)
                //    {
                //        model.PurchaseOrderImpTransDoc.CnDate = Convert.ToDateTime(model.CnDate);
                //        model.PurchaseOrderImpTransDoc.CnMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.PurchaseOrderImpTransDoc.CnDate)).Date;
                //    }
                //    else
                //    {
                //        model.PurchaseOrderImpTransDoc.CnMiti = model.CnDate;
                //        model.PurchaseOrderImpTransDoc.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                //    }
                //}

                //_purchaseOrderImpTransDoc.Update(model.PurchaseOrderImpTransDoc);
                var source = StringEnum.Parse(typeof(ModuleEnum), "Purchase Order").ToString();
                _purchaseOrderDetail.Delete(x => x.OrderId == model.PurchaseOrder.Id);
                //  _stockTransaction.Delete(x => x.ReferenceId == model.PurchaseOrder.Id && x.Source == source);
                _billingTermDetail.Delete(x => x.ReferenceId == model.PurchaseOrder.Id && x.Source == source);
                //int sno = 1;
                //decimal basicAmt = 0;


                //var source = StringEnum.Parse(typeof(ModuleEnum), "Purchase").ToString();

                int sno = 1;
                decimal basicAmt = 0;
                var tempPurchaseOrderDetailList = new List<PurchaseOrderDetail>();
                foreach (var item in purchaseDetailEntry)
                {
                    if (item.ProductCode != null && item.ProductCode != 0 && item.BasicAmt != null && item.BasicAmt != 0 &&
                        item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                    {
                        var unitcode = _unitRepository.GetById(item.UnitId).Code;
                        basicAmt += Convert.ToDecimal(item.BasicAmt);
                        var purchaseOrderDetail = new PurchaseOrderDetail();
                        purchaseOrderDetail.SNo = sno;
                        purchaseOrderDetail.ProductCode = item.ProductCode;
                        purchaseOrderDetail.AltQty = item.AltQty;
                        purchaseOrderDetail.Qty = Convert.ToDecimal(item.Qty);
                        purchaseOrderDetail.AltStockQty = item.AltQty;
                        purchaseOrderDetail.StockQty = Convert.ToDecimal(item.Qty);
                        purchaseOrderDetail.Rate = Convert.ToDecimal(item.Rate);
                        purchaseOrderDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                        purchaseOrderDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                        purchaseOrderDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                        purchaseOrderDetail.Remarks = model.PurchaseOrder.Remarks;
                        purchaseOrderDetail.OrderId = model.PurchaseOrder.Id;
                        purchaseOrderDetail.Unit = unitcode;
                        purchaseOrderDetail.AltUnit = item.AltUnit;
                        _purchaseOrderDetail.Add(purchaseOrderDetail);
                        tempPurchaseOrderDetailList.Add(purchaseOrderDetail);

                        //var stockTransaction = new StockTransaction();
                        //stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.PurchaseOrder.AgentCode);
                        //stockTransaction.AltQty = purchaseOrderDetail.AltQty;
                        //stockTransaction.VMiti = model.PurchaseOrder.OrderMiti;
                        //stockTransaction.AltStockQty = purchaseOrderDetail.AltStockQty;
                        //stockTransaction.AltUnit = purchaseOrderDetail.AltUnit;
                        //stockTransaction.CurrCode = "";
                        //stockTransaction.CurrRate = 0;
                        //stockTransaction.GlCode = model.PurchaseOrder.GlCode;
                        //stockTransaction.NetAmt = Convert.ToDecimal(purchaseOrderDetail.NetAmt);
                        //stockTransaction.ProductCode = Convert.ToInt32(purchaseOrderDetail.ProductCode);
                        //stockTransaction.Quantity = purchaseOrderDetail.Qty;
                        //stockTransaction.Rate = Convert.ToDecimal(purchaseOrderDetail.Rate);
                        //stockTransaction.SNo = sno;
                        //stockTransaction.Source = source;
                        //stockTransaction.Unit = unitcode;
                        //stockTransaction.AltUnit = item.AltUnit;
                        //stockTransaction.ReferenceId = model.PurchaseOrder.Id;
                        //stockTransaction.FYId = FiscalYear.Id;
                        //var stock =
                        //    _stockTransaction.Filter(x => x.ProductCode == item.ProductCode).ToList().LastOrDefault();
                        //if (stock == null)
                        //{
                        //    stockTransaction.StockVal = Convert.ToDecimal(purchaseOrderDetail.BasicAmt);
                        //    stockTransaction.StockQty = Convert.ToDecimal(purchaseOrderDetail.StockQty);

                        //}
                        //else
                        //{
                        //    stockTransaction.StockVal = stock.StockVal + Convert.ToDecimal(purchaseOrderDetail.BasicAmt);
                        //    stockTransaction.StockQty = stock.StockQty + Convert.ToDecimal(purchaseOrderDetail.StockQty);
                        //}
                        //stockTransaction.TermAmt = purchaseOrderDetail.TermAmt;
                        //stockTransaction.TransactionType =
                        //    StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();

                        //stockTransaction.VDate = model.PurchaseOrder.OrderDate;
                        //stockTransaction.VNo = model.PurchaseOrder.OrderNo;
                        //stockTransaction.BranchId = CurrentBranchId;
                        //_stockTransaction.Add(stockTransaction);
                        sno++;

                    }
                }


                if (billTermList != null)
                {
                    foreach (var term in billTermList)
                    {
                        var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                        decimal drAmt = 0;
                        decimal drLocalAmt = 0;
                        decimal crAmt = 0;
                        decimal crLocalAmt = 0;
                        decimal termAmt = 0;
                        if (term.Sign == "-")
                        {
                            crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                            crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                            termAmt = System.Math.Abs(crAmt) * (-1);
                        }
                        else
                        {
                            drAmt = Convert.ToDecimal(term.Amount);
                            drLocalAmt = Convert.ToDecimal(term.Amount);
                            termAmt = drAmt;
                        }
                        var billingTermDetail = new BillingTermDetail();
                        billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                        var index = Convert.ToInt32(term.BillingTermIndex);
                        billingTermDetail.Index = index;
                        billingTermDetail.ReferenceId = model.PurchaseOrder.Id;
                        billingTermDetail.Source = source;
                        billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                        if (term.IsProductWise)
                        {
                            billingTermDetail.DetailId = tempPurchaseOrderDetailList[index].Id;
                        }
                        billingTermDetail.TermAmount = termAmt;
                        billingTermDetail.IsProductWise = term.IsProductWise;
                        _billingTermDetail.Add(billingTermDetail);
                    }
                }
           // }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                if (modelState.Errors.Any())
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                    }
                }
            }

            return Json(new { success = true, isEdit = true });
        }


        [CheckPermission(Permissions = "Approve", Module = "PB")]
        [HttpPost]
        public ActionResult PurchaseOrderApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _purchaseOrderMaster.GetById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _purchaseOrderMaster.Update(data);
            return null;
        }

        [CheckPermission(Permissions = "Create", Module = "PB")]
        public ActionResult GetPurchaseOrderDetailEntry()
        {
            var viewModel = new PurchaseDetailEntryViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            return PartialView("_PartialPurchaseDetailEntry", viewModel);
        }

        [CheckPermission(Permissions = "Edit", Module = "PB")]
        public ActionResult GetPurchaseOrderDetailEdit(int? id, int? index)
        {
            var data = new PurchaseOrderDetail();
            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id).ToList();
            var defaultIndex = billTerm.Max(x => x.Index);
            ViewBag.Index = defaultIndex == null ? 0 : defaultIndex + 1;
            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }

            ViewBag.BillingTermList = billTerm;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                ViewBag.AllowProductWiseBillTerm = 1;
            else
            {
                ViewBag.AllowProductWiseBillTerm = 0;
            }
            //if (index != null)
            //    data.Index = Convert.ToInt32(index);


            data.Unitlist = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            data.EntryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            return PartialView("_PartialPurchaseOrderDetailEdit", data);
        }

        public ActionResult PurchaseOrderPrint(int orderId)
        {
            var model = _purchaseOrderMaster.GetById(orderId);
            var billDate = Convert.ToDateTime(model.OrderDate).ToString("MM/dd/yyyy");
            var invoice = new PurchaseInvoicePrintViewModel();
            invoice.InvoiceHeader = new InvoiceHeader();
            var logo = "NoImage.jpg";
            if (!string.IsNullOrEmpty(base.CompanyInfo.LogoGuid))
                logo = base.CompanyInfo.LogoGuid + base.CompanyInfo.LogoExt;
            var filePath = Server.MapPath("~/Content/Logo/" + logo);
            if (!System.IO.File.Exists(filePath))
            {
                logo = "NoImage.jpg";
            }
            invoice.InvoiceHeader.CompanyLogoUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "/Content/Logo/" + logo;
            invoice.InvoiceHeader.InvoiceDate = billDate;
            invoice.InvoiceHeader.InvoiceNo = model.OrderNo;
            invoice.InvoiceHeader.CompanyAddress = base.CompanyInfo.Address;
            invoice.InvoiceHeader.CompanyName = base.CompanyInfo.Name;
            invoice.InvoiceHeader.PartyName = model.Ledger.AccountName;
            invoice.BilledBy = model.User.FullName;
            invoice.InvoiceHeader.HeaderTitle = "Purchase Order";
            int sno = 1;
            double subTotal = 0;
            invoice.InvoiceDetails = new List<InvoiceDetail>();
            foreach (var item in model.PurchaseOrderDetails)
            {
                if (item.BasicAmt != 0)
                {
                    var product = _product.GetById(Convert.ToInt32(item.ProductCode));
                    var invoiceDetail = new InvoiceDetail();
                    invoiceDetail.Amount = Convert.ToDecimal(item.BasicAmt);
                    invoiceDetail.Particular = product.Name;
                    invoiceDetail.SNo = sno;
                    invoiceDetail.Qty = Convert.ToDecimal(item.Qty);
                    invoiceDetail.Rate = Convert.ToDecimal(item.Rate);
                    invoiceDetail.Remarks = item.Remarks;
                    sno++;
                    subTotal += Convert.ToDouble(item.BasicAmt);
                    invoice.InvoiceDetails.Add(invoiceDetail);
                }
            }

            var billingTerms = _billingTermDetail.GetMany(x => x.ReferenceId == orderId && x.Source == "PO").OrderBy(x => x.Index);
            invoice.InvoiceBillingTerms = new List<InvoiceBillingTerms>();
            double termAmt = 0;
            foreach (var item in billingTerms)
            {
                var invoiceBillingTerm = new InvoiceBillingTerms();
                var termName = _billingTerm.GetById(x => x.Id == item.BillingTermId && x.Type == "P").Description;
                invoiceBillingTerm.TermName = termName;
                invoiceBillingTerm.TermRate = item.Rate;
                invoiceBillingTerm.TermAmount = item.TermAmount;
                invoice.InvoiceBillingTerms.Add(invoiceBillingTerm);
                termAmt += Convert.ToDouble(item.TermAmount);
            }
            invoice.SubTotal = subTotal;
            invoice.Total = subTotal + termAmt;
            return this.ViewPdf("", "PurchaseInvoicePrint", invoice);
        }

        #endregion

        #region Purchase Challan
        public JsonResult CheckChallanNoInPurchaseInvoice(string ChallanNo, int? Id)
        {
            var challan = new PurchaseChallanMaster();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _purchaseChallanMaster.GetById(x => x.Id == Id);
                if (data.ChallanNo.ToLower().Trim() != ChallanNo.ToLower().Trim())
                {
                    challan =
                        _purchaseChallanMaster.GetById(x => x.ChallanNo.ToLower().Trim() == ChallanNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                challan = _purchaseChallanMaster.GetById(x => x.ChallanNo.ToLower().Trim() == ChallanNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (challan == null)
            {
                challan = new PurchaseChallanMaster();
            }

            return challan.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateChallanDetailByOrderId(int id)
        {
            var order = _purchaseOrderMaster.GetById(x => x.Id == id, "PurchaseOrderDetails");
            var vendor = _ledger.GetById(x => x.Id == order.GlCode);
            var agent = string.Empty;
            if (order.AgentCode != null)
            {
                var agentId = Convert.ToInt32(order.AgentCode);
                var agentData = _agent.GetById(agentId);
                if (agentData != null)
                    agent = agentData.Name;
            }
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var challanDetailView = string.Empty;
            var billWiseBillingTerm = string.Empty;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);

            foreach (var item in order.PurchaseOrderDetails)
            {
                var singleChallanDetail = new PurchaseDetailEntryViewModel();
                var productId = Convert.ToInt32(item.ProductCode);
                var product = _product.GetById(productId);
                singleChallanDetail.ProductName = product.Name;
                singleChallanDetail.ProductId = Convert.ToInt32(item.ProductCode);

                singleChallanDetail.Unit = item.UnitId;
                singleChallanDetail.Qty = item.Qty;
                singleChallanDetail.UnitList = unitSelectList;
                singleChallanDetail.Rate = item.Rate;
                singleChallanDetail.BasicAmt = item.BasicAmt;
                singleChallanDetail.TermAmt = item.TermAmt;
                singleChallanDetail.NetAmt = item.NetAmt;
                if (billingTerm.Count() != 0)
                    singleChallanDetail.AllowProductWiseBillTerm = true;
                challanDetailView += base.RenderPartialViewToString("_PartialPurchaseDetailEntry", singleChallanDetail);
            }

            var billingTermDetail = _billingTermDetail.GetMany(x => x.Source == "PO" && x.ReferenceId == order.Id);

            foreach (var item in billingTermDetail)
            {
                var billing = _billingTerm.GetById(x => x.Id == item.BillingTermId);
                var viewModel = new BillingTermDetailViewModel();
                viewModel.Amount = item.TermAmount;
                viewModel.Description = billing.Description;
                viewModel.Id = item.BillingTermId;
                viewModel.Rate = item.Rate;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                viewModel.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), billing.Sign));
                viewModel.BillingTermIndex = item.Index;
                viewModel.IsProductWise = item.IsProductWise;

                billWiseBillingTerm += base.RenderPartialViewToString("_PartialHdnBillingTerm", viewModel);
            }
            var CurrentBalance = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=0, @glCode=" + order.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            var TotalOutstanding = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=1, @glCode=" + order.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            return Json(new
            {
                VendorName = vendor.AccountName,
                VendorId = order.GlCode,
                AgentName = agent,
                AgentId = order.AgentCode,
                materialIssueDetailView = challanDetailView,
                billWiseBillingTerm = billWiseBillingTerm,
                BasicAmt = order.BasicAmt,
                NetAmt = order.NetAmt,
                TermAmt = order.TermAmt,
                TotalAmtRs = order.BasicAmt + order.TermAmt,
                CurrentBalance = CurrentBalance,
                TotalOutstanding = TotalOutstanding
            });
        }

        [CheckPermission(Permissions = "Navigate", Module = "PB")]
        public ActionResult PurchaseChallan()
        {
            ViewBag.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            var viewModel = new PurchaseChallanViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.Category == cat && !x.IsDeleted);
            var billingTermProductWise = billingTerm.Where(x => x.IsProductWise).OrderBy(x => x.DispalyOrder);
            var billingTermBillWise = billingTerm.Where(x => !x.IsProductWise).OrderBy(x => x.DispalyOrder);
            viewModel.ProductWiseBillTerms = new List<BillingTermSelectViewModel>();
            viewModel.BillWiseBillTerms = new List<BillingTermSelectViewModel>();
            billingTermProductWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.ProductWiseBillTerms.Add(billingTermSelectList);
            });
            billingTermBillWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.BillWiseBillTerms.Add(billingTermSelectList);
            });
            return View();
        }

        [CheckPermission(Permissions = "Navigate", Module = "PB")]
        public ActionResult PurchaseChallanList()
        {
            var viewModel = new PurchaseChallanViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var listPending = _purchaseChallanMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            // var listAccepted = _purchaseChallanMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            // viewModel.AcceptedList = listAccepted;
            viewModel.PendingList = listPending;
            return PartialView(viewModel);
        }

        public JsonResult GetPurchaseOrderForPickList()
        {
            var list = _purchaseChallanMaster.GetMany(x => !x.IsDeleted).Select(x => x.OrderId);
            var purchaselist = _purchaseInvoice.GetMany(x => !x.IsDeleted).Select(x => x.OrderId);
            var data = _purchaseOrderMaster.GetMany(x => !x.IsDeleted && x.BranchId == CurrentBranchId && !list.Contains(x.Id) && !purchaselist.Contains(x.Id)).Select(x => new
             {
                 Id = x.Id,
                 OrderNo = x.OrderNo,
                 Date = x.OrderDate.ToShortDateString(),
                 Vendor = x.Ledger.AccountName
             }).OrderByDescending(x => x.Date);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PurchaseChallanPendingList(int pageNo)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var list = _purchaseChallanMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView(list);
        }

        //public ActionResult PurchaseChallanAcceptedList(int pageNo)
        //{
        //    var list = _purchaseChallanMaster.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId && !x.IsDeleted).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
        //    return PartialView(list);
        //}

        public ActionResult PurchaseChallanDelete(int id)
        {
            var model = _purchaseChallanMaster.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _purchaseChallanMaster.Update(model);
            _stockTransaction.Delete(x => x.Source == "PC" && x.ReferenceId == id);
            return Json(true);
        }

        public JsonResult GetPurchaseChallanForPickList()
        {
            var list = _purchaseInvoice.GetMany(x => !x.IsDeleted).Select(x => x.ChallanId);
            var data = _purchaseChallanMaster.GetMany(x => !x.IsDeleted && x.BranchId == CurrentBranchId && !list.Contains(x.Id)).Select(x => new
            {
                Id = x.Id,
                ChallanNo = x.ChallanNo,
                Date = x.ChallanDate.ToShortDateString(),
                Vendor = x.Ledger.AccountName
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CheckFiscalyearDateinPurchaseChallan(string ChallanDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(ChallanDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(ChallanDate);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {

                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                }
                    
            catch (Exception e)
            {
                return Content("False");
            }

        }
        [CheckPermission(Permissions = "Create", Module = "PB")]
        public ActionResult PurchaseChallanAdd()
        {
            //var data = _purchaseInvoice.GetAll().LastOrDefault();
            var viewModel = base.CreateModel<PurchaseChallanAddViewModel>();
            viewModel.AllowProductWiseBillTerm = false;
            // viewModel.ChallanDate = data != null ? data.InvoiceDate : DateTime.UtcNow;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.PurchaseChallan = new PurchaseChallanMaster();
            viewModel.PurchaseChallan.ChallanMiti = NepaliDateService.NepaliDate(DateTime.Now).Date;
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            viewModel.PurchaseChallanImpTransDoc = new PurchaseChallanImpTransDoc();
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.ChallanDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _purchaseChallanMaster.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewModel.ChallanDate = base.SystemControl.DateType == 1 ? data.ChallanDate.ToShortDateString() : data.ChallanMiti;
                }
                else
                {
                    viewModel.ChallanDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseChallanAdd(IEnumerable<PurchaseDetailEntryViewModel> purchaseDetailEntry, PurchaseChallanAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            if (model.PurchaseChallan.GlCode != 0)
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                //model.PurchaseChallan.BasicAmt = Convert.ToDecimal(model.PurchaseChallan.BasicAmt);
                //model.PurchaseChallan.NetAmt = Convert.ToDecimal(model.PurchaseChallan.NetAmt);
                //model.PurchaseChallan.TermAmt = Convert.ToDecimal(model.PurchaseChallan.TermAmt);
                //model.PurchaseChallan.CreatedById = userId;
                //model.PurchaseChallan.CreatedDate = DateTime.UtcNow;
                ////  model.PurchaseChallan.ChallanDate = model.ChallanDate;
                //model.PurchaseChallan.FYId = FiscalYear.Id;
                //model.PurchaseChallan.BranchId = CurrentBranchId;
                //model.PurchaseChallan.IsDeleted = false;
                //model.PurchaseChallan.OrderId = model.PurchaseOrderId;
                //model.PurchaseChallan.OrderNo = model.OrderNo;
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseChallan.ChallanDate = model.ChallanDate.ToDate();
                    model.PurchaseChallan.ChallanMiti = NepaliDateService.NepaliDate(model.PurchaseChallan.ChallanDate).Date;
                }
                else
                {
                    model.PurchaseChallan.ChallanMiti = model.ChallanDate;
                    model.PurchaseChallan.ChallanDate = NepaliDateService.NepalitoEnglishDate(model.ChallanDate);
                }

                //_purchaseChallanMaster.Add(model.PurchaseChallan);
                _salesService.SavePurchaseChallan(model, userId, CurrentBranchId, FiscalYear.Id);

                if (model.PurchaseChallan.DocId != null)
                {
                    var docId = Convert.ToInt32(model.PurchaseChallan.DocId);
                    var documentnumber = _docNumering.GetById(docId);
                    documentnumber.DocCurrNo += 1;
                    _docNumering.Update(documentnumber);
                }

                var source = StringEnum.Parse(typeof(ModuleEnum), "Purchase Challan").ToString();
                int sno = 1;
                decimal basicAmt = 0;
                var tempPurchaseChallanDetailList = new List<PurchaseChallanDetail>();
                foreach (var item in purchaseDetailEntry)
                {
                    if (item.ProductId != null && item.ProductId != 0 && item.BasicAmt != null && item.BasicAmt != 0 &&
                        item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                    {
                        var unitcode = _unitRepository.GetById(item.Unit).Code;
                        basicAmt += Convert.ToDecimal(item.BasicAmt);
                        var purchaseChallanDetail = new PurchaseChallanDetail();
                        purchaseChallanDetail.SNo = sno;
                        purchaseChallanDetail.ProductCode = item.ProductId;
                        purchaseChallanDetail.AltQty = item.AltQty;
                        purchaseChallanDetail.Qty = Convert.ToDecimal(item.Qty);
                        purchaseChallanDetail.AltStockQty = item.AltQty;
                        purchaseChallanDetail.StockQty = Convert.ToDecimal(item.Qty);
                        purchaseChallanDetail.Rate = Convert.ToDecimal(item.Rate);
                        purchaseChallanDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                        purchaseChallanDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                        purchaseChallanDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                        purchaseChallanDetail.Remarks = model.PurchaseChallan.Remarks;
                        purchaseChallanDetail.ChallanId = model.PurchaseChallan.Id;
                        purchaseChallanDetail.Unit = unitcode;
                        purchaseChallanDetail.AltUnit = item.AltUnit;
                        purchaseChallanDetail.GodownCode = item.GodownId;
                        _purchaseChallanDetail.Add(purchaseChallanDetail);
                        tempPurchaseChallanDetailList.Add(purchaseChallanDetail);

                        var stockTransaction = new StockTransaction();
                        stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.PurchaseChallan.AgentCode);
                        stockTransaction.VDate = model.PurchaseChallan.ChallanDate;
                        stockTransaction.VMiti = model.PurchaseChallan.ChallanMiti;
                        stockTransaction.AltQty = purchaseChallanDetail.AltQty;
                        stockTransaction.AltStockQty = purchaseChallanDetail.AltStockQty;
                        stockTransaction.AltUnit = purchaseChallanDetail.AltUnit;
                        stockTransaction.CurrCode = "";
                        stockTransaction.CurrRate = 0;
                        stockTransaction.GlCode = model.PurchaseChallan.GlCode;
                        stockTransaction.NetAmt = purchaseChallanDetail.NetAmt;
                        stockTransaction.ProductCode = purchaseChallanDetail.ProductCode;
                        stockTransaction.Quantity = purchaseChallanDetail.Qty;
                        stockTransaction.Rate = Convert.ToDecimal(purchaseChallanDetail.Rate);
                        stockTransaction.SNo = sno;
                        stockTransaction.Source = source;
                        stockTransaction.ReferenceId = model.PurchaseChallan.Id;
                        stockTransaction.Unit = unitcode;
                        stockTransaction.AltUnit = item.AltUnit;
                        stockTransaction.FYId = FiscalYear.Id;
                        var stock =
                              _stockTransaction.Filter(x => x.ProductCode == item.ProductId).ToList().LastOrDefault();
                        if (stock == null)
                        {
                            stockTransaction.StockVal = Convert.ToDecimal(purchaseChallanDetail.BasicAmt);
                            stockTransaction.StockQty = purchaseChallanDetail.StockQty;

                        }
                        else
                        {
                            stockTransaction.StockVal = stock.StockVal + Convert.ToDecimal(purchaseChallanDetail.BasicAmt);
                            stockTransaction.StockQty = stock.StockQty + purchaseChallanDetail.StockQty;
                        }
                        stockTransaction.TermAmt = purchaseChallanDetail.TermAmt;
                        stockTransaction.TransactionType =
                            StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();

                        stockTransaction.VDate = model.PurchaseChallan.ChallanDate;
                        stockTransaction.VNo = model.PurchaseChallan.ChallanNo;
                        stockTransaction.BranchId = CurrentBranchId;
                        _stockTransaction.Add(stockTransaction);
                        sno++;
                    }
                }

                model.PurchaseChallanImpTransDoc.PurchaseChallanId = model.PurchaseChallan.Id;
                if (!string.IsNullOrEmpty(model.PPDDate))
                {
                    if (base.SystemControl.DateType == 1)
                    {
                        model.PurchaseChallanImpTransDoc.PPDDate = model.PPDDate.ToDate();
                        model.PurchaseChallanImpTransDoc.PPDMiti = NepaliDateService.NepaliDate(model.PPDDate.ToDate()).Date;
                    }
                    else
                    {
                        model.PurchaseChallanImpTransDoc.PPDMiti = model.PPDDate;
                        model.PurchaseChallanImpTransDoc.PPDDate = NepaliDateService.NepalitoEnglishDate(model.PPDDate);
                    }
                }
                if (!string.IsNullOrEmpty(model.CnDate))
                {
                    if (base.SystemControl.DateType == 1)
                    {
                        model.PurchaseChallanImpTransDoc.CnDate = model.CnDate.ToDate();
                        model.PurchaseChallanImpTransDoc.CnMiti = NepaliDateService.NepaliDate(model.CnDate.ToDate()).Date;
                    }
                    else
                    {
                        model.PurchaseChallanImpTransDoc.CnMiti = model.CnDate;
                        model.PurchaseChallanImpTransDoc.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                    }
                }
                _purchaseChallanImpTransDoc.Add(model.PurchaseChallanImpTransDoc);

                if (billTermList != null)
                {
                    foreach (var term in billTermList)
                    {
                        var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                        decimal drAmt = 0;
                        decimal drLocalAmt = 0;
                        decimal crAmt = 0;
                        decimal crLocalAmt = 0;
                        decimal termAmt = 0;
                        if (term.Sign == "-")
                        {
                            crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                            crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                            termAmt = System.Math.Abs(crAmt) * (-1);
                        }
                        else
                        {
                            drAmt = Convert.ToDecimal(term.Amount);
                            drLocalAmt = Convert.ToDecimal(term.Amount);
                            termAmt = drAmt;
                        }
                        var billingTermDetail = new BillingTermDetail();
                        billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                        var index = Convert.ToInt32(term.BillingTermIndex);
                        billingTermDetail.Index = index;
                        billingTermDetail.ReferenceId = model.PurchaseChallan.Id;
                        billingTermDetail.Source = source;
                        billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                        if (term.IsProductWise)
                        {
                            billingTermDetail.DetailId = tempPurchaseChallanDetailList[index].Id;
                        }
                        billingTermDetail.TermAmount = termAmt;
                        billingTermDetail.IsProductWise = term.IsProductWise;
                        _billingTermDetail.Add(billingTermDetail);

                    }
                }
                return Json(new { success = true, isEdit = false });
            }
            return Json(new { success = false, isEdit = false });
        }

        [CheckPermission(Permissions = "Edit", Module = "PB")]
        public ActionResult PurchaseChallanEdit(int id)
        {
            var data = _purchaseChallanMaster.GetById(id);
            var viewModel = base.CreateModel<PurchaseChallanAddViewModel>();
            viewModel.PurchaseChallanImpTransDoc = _purchaseChallanImpTransDoc.GetById(x => x.PurchaseChallanId == data.Id);
            viewModel.PurchaseChallan = data;
            if (data.OrderId != null)
            {
                viewModel.OrderNo = _purchaseOrderMaster.GetById(data.OrderId.Value).OrderNo;
            }
            ViewBag.Id = data.Id;
            ViewBag.AllowProductWiseBillTerm = 0;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
            {
                ViewBag.AllowProductWiseBillTerm = 1;
                viewModel.AllowProductWiseBillTerm = true;
            }
            if (base.SystemControl.DateType == 1)
            {
                viewModel.ChallanDate = data.ChallanDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.ChallanDate = data.ChallanMiti;
            }
            if (viewModel.PurchaseChallanImpTransDoc.PPDDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.PPDDate = Convert.ToDateTime(viewModel.PurchaseChallanImpTransDoc.PPDDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.PPDDate = viewModel.PurchaseChallanImpTransDoc.PPDMiti;
                }
            }
            if (viewModel.PurchaseChallanImpTransDoc.CnDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.CnDate = Convert.ToDateTime(viewModel.PurchaseChallanImpTransDoc.CnDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.CnDate = viewModel.PurchaseChallanImpTransDoc.CnMiti;
                }

            }

            foreach (var term in viewModel.PurchaseChallan.PurchaseChallanDetails)
            {
                term.Unit = Convert.ToString(_unitRepository.GetById(x => x.Code.ToLower() == term.Unit.ToLower()).Id);
            }

            /* var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;*/
            var billTerm =_billingTermDetail.Filter(x => x.ReferenceId == id && x.Source == "PB").ToList();

            //var defaultIndex = billTerm.Max(x => x.Index);
            //var defaultIndex = data.PurchaseChallanDetails.Max(x => x.Index);

            ViewBag.Index = 1;
            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }

            ViewBag.BillingTermList = billTerm;
            viewModel.TotalAmtRs = viewModel.PurchaseChallan.NetAmt.ToString();
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Code", "Code");
            viewModel.OrderNo = data.OrderNo;
            viewModel.PurchaseOrderId = data.OrderId;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseChallanEdit(IEnumerable<PurchaseChallanDetail> purchaseChallanDetailEntry, IEnumerable<PurchaseDetailEntryViewModel> purchaseDetailEntry, PurchaseChallanAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.PurchaseChallan.FYId = FiscalYear.Id;
            model.PurchaseChallan.BasicAmt = Convert.ToDecimal(model.PurchaseChallan.BasicAmt);
            model.PurchaseChallan.NetAmt = Convert.ToDecimal(model.PurchaseChallan.NetAmt);
            model.PurchaseChallan.TermAmt = Convert.ToDecimal(model.PurchaseChallan.TermAmt);
            model.PurchaseChallan.OrderId = model.PurchaseOrderId;
            model.PurchaseChallan.OrderNo = model.OrderNo;
            //model.PurchaseChallan.OrderNo = model.OrderNo;
            if (base.SystemControl.DateType == 1)
            {
                model.PurchaseChallan.ChallanDate = Convert.ToDateTime(model.ChallanDate);
                model.PurchaseChallan.ChallanMiti = NepaliDateService.NepaliDate(model.PurchaseChallan.ChallanDate).Date;
            }
            else
            {
                model.PurchaseChallan.ChallanMiti = model.ChallanDate;
                model.PurchaseChallan.ChallanDate = NepaliDateService.NepalitoEnglishDate(model.ChallanDate);
            }
            _purchaseChallanMaster.Update(model.PurchaseChallan);
            if (!string.IsNullOrEmpty(model.PPDDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseChallanImpTransDoc.PPDDate = Convert.ToDateTime(model.PPDDate);
                    model.PurchaseChallanImpTransDoc.PPDMiti =
                        NepaliDateService.NepaliDate(Convert.ToDateTime(model.PurchaseChallanImpTransDoc.PPDDate)).Date;
                }
                else
                {
                    model.PurchaseChallanImpTransDoc.PPDMiti = model.PPDDate;
                    model.PurchaseChallanImpTransDoc.PPDDate = NepaliDateService.NepalitoEnglishDate(model.PPDDate);
                }
            }
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.PurchaseChallanImpTransDoc.CnDate = Convert.ToDateTime(model.CnDate);
                    model.PurchaseChallanImpTransDoc.CnMiti =
                        NepaliDateService.NepaliDate(Convert.ToDateTime(model.PurchaseChallanImpTransDoc.CnDate)).Date;
                }
                else
                {
                    model.PurchaseChallanImpTransDoc.CnMiti = model.CnDate;
                    model.PurchaseChallanImpTransDoc.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }

            _purchaseChallanImpTransDoc.Update(model.PurchaseChallanImpTransDoc);
            var source = StringEnum.Parse(typeof(ModuleEnum), "Purchase Challan").ToString();
            _purchaseChallanDetail.Delete(x => x.ChallanId == model.PurchaseChallan.Id);
            _stockTransaction.Delete(x => x.ReferenceId == model.PurchaseChallan.Id && x.Source == source);
            _billingTermDetail.Delete(x => x.ReferenceId == model.PurchaseChallan.Id && x.Source == source);
            //int sno = 1;
            //decimal basicAmt = 0;


            //var source = StringEnum.Parse(typeof(ModuleEnum), "Purchase").ToString();
            int sno = 1;
            decimal basicAmt = 0;
            var tempPurchaseChallanDetailList = new List<PurchaseChallanDetail>();
            if (purchaseChallanDetailEntry != null)
            {
                foreach (var item in purchaseChallanDetailEntry)
                {
                    if (item.ProductCode != null && item.ProductCode != 0 && item.BasicAmt != null && item.BasicAmt != 0 &&
                        item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                    {
                        var unit = Convert.ToInt32(item.Unit);
                        var unitcode = _unitRepository.GetById(unit).Code;
                        basicAmt += Convert.ToDecimal(item.BasicAmt);
                        var purchaseChallanDetail = new PurchaseChallanDetail();
                        purchaseChallanDetail.SNo = sno;
                        purchaseChallanDetail.ProductCode = item.ProductCode;
                        purchaseChallanDetail.AltQty = item.AltQty;
                        purchaseChallanDetail.Qty = Convert.ToDecimal(item.Qty);
                        purchaseChallanDetail.AltStockQty = item.AltQty;
                        purchaseChallanDetail.StockQty = Convert.ToDecimal(item.Qty);
                        purchaseChallanDetail.Rate = Convert.ToDecimal(item.Rate);
                        purchaseChallanDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                        purchaseChallanDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                        purchaseChallanDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                        purchaseChallanDetail.Remarks = model.PurchaseChallan.Remarks;
                        purchaseChallanDetail.ChallanId = model.PurchaseChallan.Id;
                        purchaseChallanDetail.Unit = unitcode;
                        purchaseChallanDetail.AltUnit = item.AltUnit;

                        purchaseChallanDetail.GodownCode = item.GodownCode;
                        _purchaseChallanDetail.Add(purchaseChallanDetail);
                        tempPurchaseChallanDetailList.Add(purchaseChallanDetail);
                        var stockTransaction = new StockTransaction();
                        stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.PurchaseChallan.AgentCode);
                        stockTransaction.AltQty = purchaseChallanDetail.AltQty;
                        stockTransaction.VMiti = model.PurchaseChallan.ChallanMiti;
                        stockTransaction.AltStockQty = purchaseChallanDetail.AltStockQty;
                        stockTransaction.AltUnit = purchaseChallanDetail.AltUnit;
                        stockTransaction.CurrCode = "";
                        stockTransaction.CurrRate = 0;
                        stockTransaction.GlCode = model.PurchaseChallan.GlCode;
                        stockTransaction.NetAmt = purchaseChallanDetail.NetAmt;
                        stockTransaction.ProductCode = purchaseChallanDetail.ProductCode;
                        stockTransaction.Quantity = purchaseChallanDetail.Qty;
                        stockTransaction.Rate = Convert.ToDecimal(purchaseChallanDetail.Rate);
                        stockTransaction.SNo = sno;
                        stockTransaction.Source = source;
                        stockTransaction.Unit = unitcode;
                        stockTransaction.AltUnit = item.AltUnit;
                        stockTransaction.ReferenceId = model.PurchaseChallan.Id;
                        stockTransaction.FYId = FiscalYear.Id;
                        var stock =
                            _stockTransaction.Filter(x => x.ProductCode == item.ProductCode).ToList().LastOrDefault();
                        if (stock == null)
                        {
                            stockTransaction.StockVal = Convert.ToDecimal(purchaseChallanDetail.BasicAmt);
                            stockTransaction.StockQty = purchaseChallanDetail.StockQty;

                        }
                        else
                        {
                            stockTransaction.StockVal = stock.StockVal + Convert.ToDecimal(purchaseChallanDetail.BasicAmt);
                            stockTransaction.StockQty = stock.StockQty + purchaseChallanDetail.StockQty;
                        }
                        stockTransaction.TermAmt = purchaseChallanDetail.TermAmt;
                        stockTransaction.TransactionType =
                            StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();

                        stockTransaction.VDate = model.PurchaseChallan.ChallanDate;
                        stockTransaction.VNo = model.PurchaseChallan.ChallanNo;
                        stockTransaction.BranchId = CurrentBranchId;
                        _stockTransaction.Add(stockTransaction);
                        sno++;
                    }
                }
            }

            if (purchaseDetailEntry != null)
            {
                foreach (var item in purchaseDetailEntry)
                {
                    if (item.ProductId != null && item.ProductId != 0 && item.BasicAmt != null && item.BasicAmt != 0 &&
                        item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                    {
                        var unit = Convert.ToInt32(item.Unit);
                        var unitcode = _unitRepository.GetById(unit).Code;
                        basicAmt += Convert.ToDecimal(item.BasicAmt);
                        var purchaseChallanDetail = new PurchaseChallanDetail();
                        purchaseChallanDetail.SNo = sno;
                        purchaseChallanDetail.ProductCode = item.ProductId;
                        purchaseChallanDetail.AltQty = item.AltQty;
                        purchaseChallanDetail.Qty = Convert.ToDecimal(item.Qty);
                        purchaseChallanDetail.AltStockQty = item.AltQty;
                        purchaseChallanDetail.StockQty = Convert.ToDecimal(item.Qty);
                        purchaseChallanDetail.Rate = Convert.ToDecimal(item.Rate);
                        purchaseChallanDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                        purchaseChallanDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                        purchaseChallanDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                        purchaseChallanDetail.Remarks = model.PurchaseChallan.Remarks;
                        purchaseChallanDetail.ChallanId = model.PurchaseChallan.Id;
                        purchaseChallanDetail.Unit = unitcode;
                        purchaseChallanDetail.AltUnit = item.AltUnit;
                        _purchaseChallanDetail.Add(purchaseChallanDetail);
                        tempPurchaseChallanDetailList.Add(purchaseChallanDetail);
                        var stockTransaction = new StockTransaction();
                        stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.PurchaseChallan.AgentCode);
                        stockTransaction.AltQty = purchaseChallanDetail.AltQty;
                        stockTransaction.VMiti = model.PurchaseChallan.ChallanMiti;
                        stockTransaction.AltStockQty = purchaseChallanDetail.AltStockQty;
                        stockTransaction.AltUnit = purchaseChallanDetail.AltUnit;
                        stockTransaction.CurrCode = "";
                        stockTransaction.CurrRate = 0;
                        stockTransaction.GlCode = model.PurchaseChallan.GlCode;
                        stockTransaction.NetAmt = purchaseChallanDetail.NetAmt;
                        stockTransaction.ProductCode = purchaseChallanDetail.ProductCode;
                        stockTransaction.Quantity = purchaseChallanDetail.Qty;
                        stockTransaction.Rate = Convert.ToDecimal(purchaseChallanDetail.Rate);
                        stockTransaction.SNo = sno;
                        stockTransaction.Source = source;
                        stockTransaction.Unit = unitcode;
                        stockTransaction.AltUnit = item.AltUnit;
                        stockTransaction.ReferenceId = model.PurchaseChallan.Id;
                        stockTransaction.FYId = FiscalYear.Id;
                        var stock =
                            _stockTransaction.Filter(x => x.ProductCode == item.ProductId).ToList().LastOrDefault();
                        if (stock == null)
                        {
                            stockTransaction.StockVal = Convert.ToDecimal(purchaseChallanDetail.BasicAmt);
                            stockTransaction.StockQty = purchaseChallanDetail.StockQty;

                        }
                        else
                        {
                            stockTransaction.StockVal = stock.StockVal + Convert.ToDecimal(purchaseChallanDetail.BasicAmt);
                            stockTransaction.StockQty = stock.StockQty + purchaseChallanDetail.StockQty;
                        }
                        stockTransaction.TermAmt = purchaseChallanDetail.TermAmt;
                        stockTransaction.TransactionType =
                            StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();

                        stockTransaction.VDate = model.PurchaseChallan.ChallanDate;
                        stockTransaction.VNo = model.PurchaseChallan.ChallanNo;
                        stockTransaction.BranchId = CurrentBranchId;
                        _stockTransaction.Add(stockTransaction);
                        sno++;
                    }
                }
            }
            if (billTermList != null)
            {
                foreach (var term in billTermList)
                {
                    var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                    decimal drAmt = 0;
                    decimal drLocalAmt = 0;
                    decimal crAmt = 0;
                    decimal crLocalAmt = 0;
                    decimal termAmt = 0;
                    if (term.Sign == "-")
                    {
                        crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        termAmt = System.Math.Abs(crAmt) * (-1);
                    }
                    else
                    {
                        drAmt = Convert.ToDecimal(term.Amount);
                        drLocalAmt = Convert.ToDecimal(term.Amount);
                        termAmt = drAmt;
                    }
                    var billingTermDetail = new BillingTermDetail();
                    billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    billingTermDetail.Index = index;
                    billingTermDetail.ReferenceId = model.PurchaseChallan.Id;
                    billingTermDetail.Source = source;
                    billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                    if (term.IsProductWise)
                    {
                        billingTermDetail.DetailId = tempPurchaseChallanDetailList[index].Id;
                    }
                    billingTermDetail.TermAmount = termAmt;
                    billingTermDetail.IsProductWise = term.IsProductWise;
                    _billingTermDetail.Add(billingTermDetail);
                }
            }
            return Json(new { success = true, isEdit = true });
        }

        [CheckPermission(Permissions = "Approve", Module = "PB")]
        [HttpPost]
        public ActionResult PurchaseChallanApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _purchaseChallanMaster.GetById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _purchaseChallanMaster.Update(data);
            return null;
        }

        [CheckPermission(Permissions = "Create", Module = "PB")]
        public ActionResult GetPurchaseChallanDetailEntry()
        {
            var viewModel = new PurchaseDetailEntryViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            return PartialView("_PartialPurchaseDetailEntry", viewModel);
        }

        [CheckPermission(Permissions = "Edit", Module = "PB")]
        public ActionResult GetPurchaseChallanDetailEdit(int? id, int? index)
        {
            var data = new PurchaseDetail();
            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id).ToList();
            var defaultIndex = billTerm.Max(x => x.Index);
            ViewBag.Index = defaultIndex == null ? 0 : defaultIndex + 1;
            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }

            ViewBag.BillingTermList = billTerm;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                ViewBag.AllowProductWiseBillTerm = 1;
            else
            {
                ViewBag.AllowProductWiseBillTerm = 0;
            }
            if (index != null)
                data.Index = Convert.ToInt32(index);


            data.Unitlist = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            return PartialView("_PartialPurchaseDetailEdit", data);
        }

        public ActionResult PurchaseChallanPrint(int invoiceId)
        {
            var model = _purchaseInvoice.GetById(invoiceId);
            var billDate = Convert.ToDateTime(model.InvoiceDate).ToString("MM/dd/yyyy");
            var invoice = new PurchaseInvoicePrintViewModel();
            invoice.InvoiceHeader = new InvoiceHeader();
            var logo = "NoImage.jpg";
            if (!string.IsNullOrEmpty(base.CompanyInfo.LogoGuid))
                logo = base.CompanyInfo.LogoGuid + base.CompanyInfo.LogoExt;
            var filePath = Server.MapPath("~/Content/Logo/" + logo);
            if (!System.IO.File.Exists(filePath))
            {
                logo = "NoImage.jpg";
            }
            invoice.InvoiceHeader.CompanyLogoUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "/Content/Logo/" + logo;
            invoice.InvoiceHeader.InvoiceDate = billDate;
            invoice.InvoiceHeader.InvoiceNo = model.InvoiceNo;
            invoice.InvoiceHeader.CompanyAddress = base.CompanyInfo.Address;
            invoice.InvoiceHeader.CompanyName = base.CompanyInfo.Name;
            invoice.InvoiceHeader.PartyName = model.Ledger.AccountName;
            invoice.BilledBy = model.User.FullName;
            int sno = 1;
            double subTotal = 0;
            invoice.InvoiceDetails = new List<InvoiceDetail>();
            foreach (var item in model.PurchaseDetails)
            {
                if (item.BasicAmt != 0)
                {
                    var product = _product.GetById(item.PCode);
                    var invoiceDetail = new InvoiceDetail();
                    invoiceDetail.Amount = Convert.ToDecimal(item.BasicAmt);
                    invoiceDetail.Particular = product.Name;
                    invoiceDetail.SNo = sno;
                    invoiceDetail.Qty = Convert.ToDecimal(item.Qty);
                    invoiceDetail.Rate = Convert.ToDecimal(item.Rate);
                    invoiceDetail.Remarks = item.Remarks;
                    sno++;
                    subTotal += Convert.ToDouble(item.BasicAmt);
                    invoice.InvoiceDetails.Add(invoiceDetail);
                }
            }

            var billingTerms = _billingTermDetail.GetMany(x => x.ReferenceId == invoiceId && x.Source == "PB").OrderBy(x => x.Index);
            invoice.InvoiceBillingTerms = new List<InvoiceBillingTerms>();
            double termAmt = 0;
            foreach (var item in billingTerms)
            {
                var invoiceBillingTerm = new InvoiceBillingTerms();
                var termName = _billingTerm.GetById(x => x.Id == item.BillingTermId && x.Type == "P").Description;
                invoiceBillingTerm.TermName = termName;
                invoiceBillingTerm.TermRate = item.Rate;
                invoiceBillingTerm.TermAmount = item.TermAmount;
                invoice.InvoiceBillingTerms.Add(invoiceBillingTerm);
                termAmt += Convert.ToDouble(item.TermAmount);
            }
            invoice.SubTotal = subTotal;
            invoice.Total = subTotal + termAmt;
            return this.ViewPdf("", "PurchaseInvoicePrint", invoice);
        }

        #endregion

        #endregion

        #region Sales

        public ActionResult Sales()
        {
            return View();
        }

        #region Sales Invoice

        public JsonResult CheckInvoiceNoInSalesInvoice(string InvoiceNo, int? Id)
        {
            var feeterm = new SalesInvoice();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _salesInvoice.GetById(x => x.Id == Id);
                if (data.InvoiceNo.ToLower().Trim() != InvoiceNo.ToLower().Trim())
                {
                    feeterm =
                        _salesInvoice.GetById(x => x.InvoiceNo.ToLower().Trim() == InvoiceNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _salesInvoice.GetById(x => x.InvoiceNo.ToLower().Trim() == InvoiceNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new SalesInvoice();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Navigate", Module = "SB")]
        public ActionResult SalesInvoice()
        {

            var viewModel = new SalesInvoiceViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.Category == cat && !x.IsDeleted);
            var billingTermProductWise = billingTerm.Where(x => x.IsProductWise).OrderBy(x => x.DispalyOrder);
            var billingTermBillWise = billingTerm.Where(x => !x.IsProductWise).OrderBy(x => x.DispalyOrder);
            viewModel.ProductWiseBillTerms = new List<BillingTermSelectViewModel>();
            viewModel.BillWiseBillTerms = new List<BillingTermSelectViewModel>();
            billingTermProductWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.ProductWiseBillTerms.Add(billingTermSelectList);
            });
            billingTermBillWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.BillWiseBillTerms.Add(billingTermSelectList);
            });
            return View(viewModel);
        }

        [CheckPermission(Permissions = "Navigate", Module = "SB")]
        public ActionResult SalesInvoiceList()
        {
            ViewBag.UserRight = base.UserRight("SB");
            var viewModel = new SalesInvoiceViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var listPending = _salesInvoice.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //var listAccepted = _salesInvoice.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //viewModel.AcceptedList = listAccepted;
            viewModel.PendingList = listPending;
            return PartialView(viewModel);
        }

        public ActionResult SalesInvoicePendingList(int pageNo)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var list = _salesInvoice.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView(list);
        }

        public ActionResult PopulateSalesInvoiceLedgerList(int id, string date)
        {
            var vendor = _ledger.GetById(x => x.Id == id);
            //   var purchaseInvoice = new PurchaseInvoice();
            var agent = string.Empty;
            var currency = string.Empty;
            var dateDue = string.Empty;
            // var miti = string.Empty;
            if (vendor.CreditDays != null)
            {
                var dueDays = Convert.ToInt32(vendor.CreditDays);
                if (base.SystemControl.DateType == 1)
                {
                    var dueDate = Convert.ToDateTime(date);
                    dateDue = dueDate.AddDays(+dueDays).ToString("MM/dd/yyyy");
                }
                else
                {
                    var dueMiti = NepaliDateService.NepalitoEnglishDate(date).Date;
                    var data = dueMiti.AddDays(+dueDays);
                    dateDue = NepaliDateService.NepaliDate(data).Date;
                }
                // purchaseInvoice.DueDay = dueDays;
            }

            if (vendor.AgentId != null)
            {
                var agentId = Convert.ToInt32(vendor.AgentId);
                var agentData = _agent.GetById(agentId);
                if (agentData != null)
                    agent = agentData.Name;
            }
            if (vendor.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(vendor.CurrencyId);
                var currCode = _currency.GetById(currencyId);
                if (currCode != null)
                    currency = currCode.Name;
            }

            return Json(new
            {
                VendorName = vendor.AccountName,
                CreditDays = vendor.CreditDays,
                AgentName = agent,
                AgentId = vendor.AgentId,
                CurrencyId = vendor.CurrencyId,
                Currency = currency,
                DueDate = dateDue,
                DateType = base.SystemControl.DateType
                //DueMiti=miti
            });
        }
        //public ActionResult SalesInvoiceAcceptedList(int pageNo)
        //{
        //    var list = _salesInvoice.Filter(x => x.IsApproved && x.BranchId == CurrentBranchId).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
        //    return PartialView(list);
        //}
        [HttpPost]
        public ActionResult SalesInvoiceDelete(int id)
        {
            var model = _salesInvoice.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _salesInvoice.Update(model);
            _accountTransaction.Delete(x => x.Source == "SB" && x.ReferenceId == id);
            _stockTransaction.Delete(x => x.Source == "SB" && x.ReferenceId == id);
            return Json(true);
        }

        public ActionResult CheckFiscalyearDateinSalesInvoice(string InvoiceDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(InvoiceDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(InvoiceDate);
                    }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }
        [CheckPermission(Permissions = "Create", Module = "SB")]
        public ActionResult SalesInvoiceAdd()
        {
            //var data = _salesInvoice.GetAll().LastOrDefault();
            var viewModel = base.CreateModel<SalesInvoiceAddViewModel>();
            viewModel.CnTypeList = new SelectList(
                Enum.GetValues(typeof(CnTypeEnum)).Cast
                    <CnTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");
            //viewModel.Date = data != null ? data.InvoiceDate : DateTime.UtcNow;
            viewModel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.SalesInvoice).ToList();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.SalesInvoice = new SalesInvoice();
            viewModel.Unitlist = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _salesInvoice.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? data.InvoiceDate.ToShortDateString() : data.InvoiceMiti;
                }
                else
                {
                    viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            viewModel.EntryControl = _entryControlSales.GetAll().FirstOrDefault();
            return PartialView(viewModel);
        }

        public ActionResult PopulateSalesChallanList(int id)
        {
            var challan = _salesService.GetSalesChallanById(id);
            var vendor = _ledger.GetById(x => x.Id == challan.GlCode);
            var agent = string.Empty;
            if (challan.AgentCode != null)
            {
                var agentId = Convert.ToInt32(challan.AgentCode);
                var agentData = _agent.GetById(agentId);
                if (agentData != null)
                    agent = agentData.Name;
            }
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var challanDetailView = string.Empty;
            var billWiseBillingTerm = string.Empty;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);

            foreach (var item in challan.SalesChallanDetails)
            {
                var singleChallanDetail = new SalesDetailEntryViewModel();
                var productId = Convert.ToInt32(item.ProductCode);
                var product = _product.GetById(productId);
                singleChallanDetail.ProductName = product.Name;
                singleChallanDetail.ProductId = Convert.ToInt32(item.ProductCode);

                if (!string.IsNullOrEmpty(item.Unit))
                {
                    var unit = _unitRepository.GetById(x => x.Code == item.Unit);
                    singleChallanDetail.UnitId = unit.Id;
                }
                singleChallanDetail.Qty = item.Qty;
                singleChallanDetail.UnitList = unitSelectList;
                singleChallanDetail.Rate = item.Rate;
                singleChallanDetail.BasicAmt = item.BasicAmt;
                singleChallanDetail.TermAmt = item.TermAmt;
                singleChallanDetail.NetAmt = item.NetAmt;
                if (billingTerm.Count() != 0)
                    singleChallanDetail.AllowProductWiseBillTerm = true;
                challanDetailView += base.RenderPartialViewToString("_PartialSalesDetailEntry", singleChallanDetail);
            }

            var billingTermDetail = _billingTermDetail.GetMany(x => x.Source == "SC" && x.ReferenceId == challan.Id);

            foreach (var item in billingTermDetail)
            {
                var billing = _billingTerm.GetById(x => x.Id == item.BillingTermId);
                var viewModel = new BillingTermDetailViewModel();
                viewModel.Amount = item.TermAmount;
                viewModel.Description = billing.Description;
                viewModel.Id = item.Id;
                viewModel.Rate = item.Rate;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                viewModel.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), billing.Sign));
                viewModel.BillingTermIndex = item.Index;
                viewModel.IsProductWise = item.IsProductWise;

                billWiseBillingTerm += base.RenderPartialViewToString("_PartialHdnBillingTerm", viewModel);
            }
            var CurrentBalance = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=0, @glCode=" + challan.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            var TotalOutstanding = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=1, @glCode=" + challan.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            return Json(new
            {
                CustomerName = vendor.AccountName,
                CustomerId = challan.GlCode,
                AgentName = agent,
                AgentId = challan.AgentCode,
                challanDetailView = challanDetailView,
                billWiseBillingTerm = billWiseBillingTerm,
                BasicAmt = challan.BasicAmt,
                NetAmt = challan.NetAmt,
                TermAmt = challan.TermAmt,
                TotalAmtRs = challan.BasicAmt + challan.TermAmt,
                CurrentBalance = CurrentBalance,
                TotalOutstanding = TotalOutstanding
            });
        }

        public ActionResult PopulateSalesOrderList(int id)
        {
            var order = _salesService.GetSalesOrder(id, "SalesOrderDetails");
            var vendor = _ledger.GetById(x => x.Id == order.GlCode);
            var agent = string.Empty;
            if (order.AgentCode != null)
            {
                var agentId = Convert.ToInt32(order.AgentCode);
                var agentData = _agent.GetById(agentId);
                if (agentData != null)
                    agent = agentData.Name;
            }
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var challanDetailView = string.Empty;
            var billWiseBillingTerm = string.Empty;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);

            foreach (var item in order.SalesOrderDetails)
            {
                var singleChallanDetail = new SalesDetailEntryViewModel();
                var productId = Convert.ToInt32(item.ProductCode);
                var product = _product.GetById(productId);
                singleChallanDetail.ProductName = product.Name;
                singleChallanDetail.ProductId = productId;
                if (!string.IsNullOrEmpty(item.Unit))
                {
                    var unit = _unitRepository.GetById(x => x.Code == item.Unit);
                    singleChallanDetail.UnitId = unit.Id;
                }
                singleChallanDetail.Qty = item.Qty;
                singleChallanDetail.UnitList = unitSelectList;
                singleChallanDetail.Rate = item.Rate;
                singleChallanDetail.BasicAmt = item.BasicAmt;
                singleChallanDetail.TermAmt = item.TermAmt;
                singleChallanDetail.NetAmt = item.NetAmt;
                if (billingTerm.Count() != 0)
                    singleChallanDetail.AllowProductWiseBillTerm = true;
                challanDetailView += base.RenderPartialViewToString("_PartialSalesDetailEntry", singleChallanDetail);
            }

            var billingTermDetail = _billingTermDetail.GetMany(x => x.Source == "PO" && x.ReferenceId == order.Id);

            foreach (var item in billingTermDetail)
            {
                var billing = _billingTerm.GetById(x => x.Id == item.BillingTermId);
                var viewModel = new BillingTermDetailViewModel();
                viewModel.Amount = item.TermAmount;
                viewModel.Description = billing.Description;
                viewModel.Id = item.Id;
                viewModel.Rate = item.Rate;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                viewModel.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), billing.Sign));
                viewModel.BillingTermIndex = item.Index;
                viewModel.IsProductWise = item.IsProductWise;

                billWiseBillingTerm += base.RenderPartialViewToString("_PartialHdnBillingTerm", viewModel);
            }
            var CurrentBalance = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=0, @glCode=" + order.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            var TotalOutstanding = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=1, @glCode=" + order.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            return Json(new
            {
                VendorName = vendor.AccountName,
                VendorId = order.GlCode,
                AgentName = agent,
                AgentId = order.AgentCode,
                challanDetailView = challanDetailView,
                billWiseBillingTerm = billWiseBillingTerm,
                BasicAmt = order.BasicAmt,
                NetAmt = order.NetAmt,
                TermAmt = order.TermAmt,
                TotalAmtRs = order.BasicAmt + order.TermAmt,
                CurrentBalance = CurrentBalance,
                TotalOutstanding = TotalOutstanding
            });
        }
        [HttpPost]
        public ActionResult SalesInvoiceAdd(IEnumerable<SalesDetailEntryViewModel> salesDetailEntry, SalesInvoiceAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            //var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (base.SystemControl.DateType == 1)
            {
                model.SalesInvoice.InvoiceDate = Convert.ToDateTime(model.InvoiceDate);
                model.SalesInvoice.InvoiceMiti = NepaliDateService.NepaliDate(model.SalesInvoice.InvoiceDate).Date;
            }
            else
            {
                model.SalesInvoice.InvoiceMiti = model.InvoiceDate;
                model.SalesInvoice.InvoiceDate = NepaliDateService.NepalitoEnglishDate(model.InvoiceDate);
            }
            if (!string.IsNullOrEmpty(model.DueDate) && !string.IsNullOrEmpty(model.DueDate.Trim()))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesInvoice.DueDate = model.DueDate.ToDate();
                    model.SalesInvoice.DueMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesInvoice.DueDate)).Date;
                }
                else
                {
                    model.SalesInvoice.DueMiti = model.DueDate;
                    model.SalesInvoice.DueDate = NepaliDateService.NepalitoEnglishDate(model.DueDate);
                }
            }

            var salesInvoiceId = _salesService.SaveSalesInvoice(model, userId, CurrentBranchId, FiscalYear.Id);
            if (model.SalesInvoice.DocId != null)
            {
                var docId = Convert.ToInt32(model.SalesInvoice.DocId);
                var documentnumber = _docNumering.GetById(docId);
                documentnumber.DocCurrNo += 1;
                _docNumering.Update(documentnumber);
            }
            var tempSalesDetailList = _salesService.SaveSalesDetail(salesDetailEntry, model.SalesInvoice,
                                                                    base.SystemControl.SalesAc, userId,
                                                                    base.FiscalYear.Id, CurrentBranchId, billTermList);
            //if (billTermList != null)
            //{
            //    _salesService.SaveSalesBillingTerm(billTermList, tempSalesDetailList, model.SalesInvoice, userId, FiscalYear.Id, CurrentBranchId);
            //}
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesInvoiceOtherDetail.CnDate = model.CnDate.ToDate();
                    model.SalesInvoiceOtherDetail.CnMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesInvoiceOtherDetail.CnDate)).Date;
                }
                else
                {
                    model.SalesInvoiceOtherDetail.CnMiti = model.CnDate;
                    model.SalesInvoiceOtherDetail.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }
            if (!string.IsNullOrEmpty(model.PODate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesInvoiceOtherDetail.PODate = model.PODate.ToDate();
                    model.SalesInvoiceOtherDetail.POMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesInvoiceOtherDetail.PODate)).Date;
                }
                else
                {
                    model.SalesInvoiceOtherDetail.POMiti = model.PODate;
                    model.SalesInvoiceOtherDetail.PODate = NepaliDateService.NepalitoEnglishDate(model.PODate);
                }
            }
            if (!string.IsNullOrEmpty(model.ContractDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesInvoiceOtherDetail.ContractDate = model.ContractDate.ToDate();
                    model.SalesInvoiceOtherDetail.ContractMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesInvoiceOtherDetail.ContractDate)).Date;
                }
                else
                {
                    model.SalesInvoiceOtherDetail.ContractMiti = model.ContractDate;
                    model.SalesInvoiceOtherDetail.ContractDate = NepaliDateService.NepalitoEnglishDate(model.ContractDate);
                }
            }
            if (!string.IsNullOrEmpty(model.ContractDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesInvoiceOtherDetail.ExportInvoiceDate = model.ExportInvoiceDate.ToDate();
                    model.SalesInvoiceOtherDetail.ExportInvoiceMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesInvoiceOtherDetail.ExportInvoiceDate)).Date;
                }
                else
                {
                    model.SalesInvoiceOtherDetail.ExportInvoiceMiti = model.ExportInvoiceDate;
                    model.SalesInvoiceOtherDetail.ExportInvoiceDate = NepaliDateService.NepalitoEnglishDate(model.ExportInvoiceDate);
                }
            }
            _salesService.SaveSalesInvoiceOtherDetail(model.SalesInvoiceOtherDetail, salesInvoiceId);
            _salesService.SaveSalesInvoiceUDF(formCollection, salesInvoiceId);
            return Json(new { success = true, isEdit = false });
        }


        [CheckPermission(Permissions = "Edit", Module = "SB")]
        public ActionResult SalesInvoiceEdit(int id)
        {
            var data = _salesInvoice.GetById(x => x.Id == id);
            var otherdetail = _salesInvoiceOtherDetailRepository.GetById(x => x.SalesInvoiceId == data.Id);
            if(otherdetail == null)
            {
                otherdetail = new SalesInvoiceOtherDetail();
            }
            var viewModel = base.CreateModel<SalesInvoiceAddViewModel>();
            viewModel.SalesOrderId = data.OrderId;
            viewModel.CnTypeList = new SelectList(
                Enum.GetValues(typeof(CnTypeEnum)).Cast
                    <CnTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text", otherdetail.CnType);
            viewModel.SalesInvoice = data;
            viewModel.SalesInvoiceOtherDetail = otherdetail;
            if (!string.IsNullOrEmpty(data.CurrCode))
            {
                //viewModel.SalesInvoice.CurrCode = data.CurrCode;
                //viewModel.SalesInvoice.CurrencyId = _currency.GetById(x => x.Code == data.CurrCode).Id;
                var CurrId = Convert.ToInt32(data.CurrCode);
                viewModel.SalesInvoice.CurrCode = _currency.GetById(x => x.Id == CurrId).Code;
                viewModel.SalesInvoice.CurrencyId = _currency.GetById(x => x.Id == CurrId).Id;
            }
            viewModel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.SalesInvoice).ToList();
            if (data.OrderId != null)
            {
                viewModel.OrderNo = _salesOrderMaster.GetById(data.OrderId.Value).OrderNo;
            }

            ViewBag.Id = data.Id;
            ViewBag.AllowProductWiseBillTerm = 0;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                ViewBag.AllowProductWiseBillTerm = 1;
            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id && x.Source == "SB").ToList();

            if (base.SystemControl.DateType == 1)
            {
                viewModel.InvoiceDate = data.InvoiceDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.InvoiceDate = data.InvoiceMiti;
            }
            if (data.DueDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.DueDate = Convert.ToDateTime(data.DueDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.DueDate = data.DueMiti;
                }
            }
            if (otherdetail.CnDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.CnDate = Convert.ToDateTime(otherdetail.CnDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.CnDate = otherdetail.CnMiti;
                }
            }
            if (otherdetail.PODate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.PODate = Convert.ToDateTime(otherdetail.PODate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.PODate = otherdetail.POMiti;
                }
            }
            if (otherdetail.ContractDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.ContractDate = Convert.ToDateTime(otherdetail.ContractDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.ContractDate = otherdetail.ContractMiti;
                }
            }
            if (otherdetail.ExportInvoiceDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.ExportInvoiceDate = Convert.ToDateTime(otherdetail.ExportInvoiceDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.ExportInvoiceDate = otherdetail.ExportInvoiceMiti;
                }
            }

            //var defaultIndex = billTerm.Max(x => x.Index);
            if (data.SalesDetails != null && data.SalesDetails.Count != 0)
            {
                var defaultIndex = data.SalesDetails.Max(x => x.Index);
                ViewBag.Index = defaultIndex == null ? 1 : defaultIndex + 1;
            }
            else
            {
                ViewBag.Index = 1;
            }

            foreach (var term in viewModel.SalesInvoice.SalesDetails)
            {
                term.UnitId = _unitRepository.GetById(x => x.Code.ToLower() == term.Unit.ToLower()).Id;
            }

            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }
            if (data.ChallanId != null)
            {
                var challan = _salesService.GetSalesChallanById(data.ChallanId.Value);
                if (challan != null)
                {
                    viewModel.ChallanNo = challan.ChallanNo;
                }
            }
            ViewBag.BillingTermList = billTerm;
            viewModel.TotalAmtRs = viewModel.SalesInvoice.NetAmt.ToString();
            viewModel.Unitlist = new SelectList(_unitRepository.GetAll(), "Id", "Code");

            var context = new DataContext();
            var PurchaseProductBatchViewModels = (from pd in context.SalesDetails.Where(x => x.SalesInvoiceId == id)
                                                  join pb in context.PurchaseProductBatches on pd.BatchId equals pb.Id
                                                  select new ProductBatchListForSales()
                                                             {
                                                                 BatchId = pb.Id,
                                                                 DetailId = pd.Id,
                                                                 BatchSerialNo = pb.SerialNo,
                                                                 StockQty = pb.StockQuantity
                                                             }).ToList();
            ViewBag.PurchaseProductBatchViewModels = PurchaseProductBatchViewModels;
            viewModel.EntryControl = _entryControlSales.GetAll().FirstOrDefault();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SalesInvoiceEdit(IEnumerable<SalesDetail> salesDetailEntry, SalesInvoiceAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.SalesInvoice.OrderId = model.SalesOrderId;
            model.SalesInvoice.OrderNo = model.OrderNo;
            var source = StringEnum.Parse(typeof(ModuleEnum), "Sales").ToString();
            _accountTransaction.Delete(x => x.ReferenceId == model.SalesInvoice.Id && x.Source == source);
            _stockTransaction.Delete(x => x.ReferenceId == model.SalesInvoice.Id && x.Source == source);
            _salesDetail.Delete(x => x.SalesInvoiceId == model.SalesInvoice.Id);
            _billingTermDetail.Delete(x => x.ReferenceId == model.SalesInvoice.Id && x.Source == source);

            model.SalesInvoice.FYId = FiscalYear.Id;
            model.SalesInvoice.BasicAmt = Convert.ToDecimal(model.SalesInvoice.BasicAmt);
            model.SalesInvoice.NetAmt = Convert.ToDecimal(model.SalesInvoice.NetAmt);
            model.SalesInvoice.TermAmt = Convert.ToDecimal(model.SalesInvoice.TermAmt);
            if (model.SalesInvoice.CurrencyId != 0 && model.SalesInvoice.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(model.SalesInvoice.CurrencyId);
                // var Code = _currency.GetById(model.SalesInvoice.CurrencyId);
                var Code = _currency.GetById(currencyId);
                model.SalesInvoice.CurrCode = Convert.ToString(Code.Id);
            }
            if (base.SystemControl.DateType == 1)
            {
                model.SalesInvoice.InvoiceDate = Convert.ToDateTime(model.InvoiceDate);
                model.SalesInvoice.InvoiceMiti = NepaliDateService.NepaliDate(model.SalesInvoice.InvoiceDate).Date;
            }
            else
            {
                model.SalesInvoice.InvoiceMiti = model.InvoiceDate;
                model.SalesInvoice.InvoiceDate = NepaliDateService.NepalitoEnglishDate(model.InvoiceDate);
            }
            if (!string.IsNullOrEmpty(model.DueDate) && !string.IsNullOrEmpty(model.DueDate.Trim()))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesInvoice.DueDate = Convert.ToDateTime(model.DueDate);
                    model.SalesInvoice.DueMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesInvoice.DueDate)).Date;
                }
                else
                {
                    model.SalesInvoice.DueMiti = model.DueDate;
                    model.SalesInvoice.DueDate = NepaliDateService.NepalitoEnglishDate(model.DueDate);
                }
            }

            _salesInvoice.Update(model.SalesInvoice);
            var accountTransactionMaster = new AccountTransaction();
            accountTransactionMaster.VNo = model.SalesInvoice.InvoiceNo;
            accountTransactionMaster.VDate = model.SalesInvoice.InvoiceDate;
            accountTransactionMaster.VMiti = model.SalesInvoice.InvoiceMiti;
            accountTransactionMaster.AgentCode = Convert.ToString(model.SalesInvoice.AgentCode);
            accountTransactionMaster.CrRate = Convert.ToDecimal(model.SalesInvoice.CurrRate);
            accountTransactionMaster.CrCode = model.SalesInvoice.CurrCode;
            accountTransactionMaster.GlCode = model.SalesInvoice.GlCode;
            accountTransactionMaster.SlCode = model.SalesInvoice.SlCode;
            accountTransactionMaster.CrAmt = 0;
            accountTransactionMaster.LocalCrAmt = 0;
            accountTransactionMaster.DrAmt = Convert.ToDecimal(model.SalesInvoice.NetAmt);
            accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(model.SalesInvoice.NetAmt);
            if (model.SalesInvoice.CurrencyId != 0 && model.SalesInvoice.CurrencyId != null)
            {
                accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(model.SalesInvoice.NetAmt) * Convert.ToDecimal(model.SalesInvoice.CurrRate);
            }
            accountTransactionMaster.Source = source;
            accountTransactionMaster.SNo = 0;
            accountTransactionMaster.CreatedById = userId;
            var isCashBank = _ledger.GetById(model.SalesInvoice.GlCode).IsCashBank;
            if (isCashBank)
                accountTransactionMaster.CbCode = model.SalesInvoice.GlCode;
            accountTransactionMaster.ReferenceId = model.SalesInvoice.Id;
            accountTransactionMaster.FYId = FiscalYear.Id;
            accountTransactionMaster.BranchId = CurrentBranchId;
            accountTransactionMaster.Remarks = model.SalesInvoice.Remarks;
            _accountTransaction.Add(accountTransactionMaster);
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesInvoiceOtherDetail.CnDate = Convert.ToDateTime(model.CnDate);
                    model.SalesInvoiceOtherDetail.CnMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesInvoiceOtherDetail.CnDate)).Date;
                }
                else
                {
                    model.SalesInvoiceOtherDetail.CnMiti = model.CnDate;
                    model.SalesInvoiceOtherDetail.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }
            if (!string.IsNullOrEmpty(model.PODate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesInvoiceOtherDetail.PODate = Convert.ToDateTime(model.PODate);
                    model.SalesInvoiceOtherDetail.POMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesInvoiceOtherDetail.PODate)).Date;
                }
                else
                {
                    model.SalesInvoiceOtherDetail.POMiti = model.PODate;
                    model.SalesInvoiceOtherDetail.PODate = NepaliDateService.NepalitoEnglishDate(model.PODate);
                }
            }
            if (!string.IsNullOrEmpty(model.ContractDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesInvoiceOtherDetail.ContractDate = Convert.ToDateTime(model.ContractDate);
                    model.SalesInvoiceOtherDetail.ContractMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesInvoiceOtherDetail.ContractDate)).Date;
                }
                else
                {
                    model.SalesInvoiceOtherDetail.ContractMiti = model.ContractDate;
                    model.SalesInvoiceOtherDetail.ContractDate = NepaliDateService.NepalitoEnglishDate(model.ContractDate);
                }
            }
            if (!string.IsNullOrEmpty(model.ExportInvoiceDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesInvoiceOtherDetail.ExportInvoiceDate = Convert.ToDateTime(model.ExportInvoiceDate);
                    model.SalesInvoiceOtherDetail.ExportInvoiceMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesInvoiceOtherDetail.ExportInvoiceDate)).Date;
                }
                else
                {
                    model.SalesInvoiceOtherDetail.ExportInvoiceMiti = model.ExportInvoiceDate;
                    model.SalesInvoiceOtherDetail.ExportInvoiceDate = NepaliDateService.NepalitoEnglishDate(model.ExportInvoiceDate);
                }
            }

            _salesInvoiceOtherDetailRepository.Update(model.SalesInvoiceOtherDetail);


            int sno = 1;
            var tempSalesDetailList = new List<SalesDetail>();

            foreach (var item in salesDetailEntry)
            {
                if (item.ProductCode != null && item.ProductCode != 0 && item.BasicAmt != null && item.BasicAmt != 0 && item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    var unitcode = _unitRepository.GetById(item.UnitId).Code;
                    var salesDetail = new SalesDetail();
                    salesDetail.SNo = sno;
                    salesDetail.ProductCode = item.ProductCode;
                    salesDetail.AltQty = item.AltQty;
                    salesDetail.Qty = Convert.ToDecimal(item.Qty);
                    salesDetail.AltStockQty = item.AltQty;
                    salesDetail.StockQty = Convert.ToDecimal(item.Qty);
                    salesDetail.Rate = Convert.ToDecimal(item.Rate);
                    salesDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    salesDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    salesDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    salesDetail.Remarks = model.SalesInvoice.Remarks;
                    salesDetail.SalesInvoiceId = model.SalesInvoice.Id;
                    salesDetail.Unit = unitcode;
                    salesDetail.AltUnit = item.AltUnit;
                    salesDetail.Index = item.Index;
                    salesDetail.Godown = item.Godown;
                    salesDetail.BatchId = item.BatchId;
                    _salesDetail.Add(salesDetail);
                    tempSalesDetailList.Add(salesDetail);
                    var accountTransactionDetail = new AccountTransaction();
                    accountTransactionDetail.VNo = model.SalesInvoice.InvoiceNo;
                    accountTransactionDetail.VDate = model.SalesInvoice.InvoiceDate;
                    accountTransactionDetail.VMiti = model.SalesInvoice.InvoiceMiti;
                    accountTransactionDetail.CrRate = Convert.ToDecimal(model.SalesInvoice.CurrRate);
                    accountTransactionDetail.CrCode = model.SalesInvoice.CurrCode;

                    //Sales A/C Id need to specify in setting
                    int salesAc;
                    var product = _product.GetById(item.ProductCode);
                    salesAc = product.SalesId != null ? Convert.ToInt32(product.SalesId) : base.SystemControl.SalesAc;
                    //Purchase A/C Id need to specify in setting
                    //  accountTransactionDetail.AgentCode = UtilityService.GetAgentNameById(model.SalesInvoice.AgentCode);
                    accountTransactionDetail.AgentCode = Convert.ToString(model.SalesInvoice.AgentCode);
                    accountTransactionDetail.GlCode = salesAc;
                    accountTransactionDetail.SlCode = model.SalesInvoice.SlCode;
                    accountTransactionDetail.CrAmt = Convert.ToDecimal(item.BasicAmt);
                    accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.BasicAmt);
                    if (model.SalesInvoice.CurrencyId != 0 && model.SalesInvoice.CurrencyId != null)
                    {
                        accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(item.BasicAmt) * Convert.ToDecimal(model.SalesInvoice.CurrRate);
                    }
                    accountTransactionDetail.DrAmt = 0;
                    accountTransactionDetail.LocalDrAmt = 0;
                    accountTransactionDetail.Source = source;
                    accountTransactionDetail.SNo = 0;
                    accountTransactionDetail.CreatedById = userId;
                    var isCashBankdetail = _ledger.GetById(salesAc).IsCashBank;
                    if (isCashBankdetail)
                        accountTransactionDetail.CbCode = model.SalesInvoice.GlCode;
                    accountTransactionDetail.ReferenceId = model.SalesInvoice.Id;
                    accountTransactionDetail.FYId = FiscalYear.Id;
                    accountTransactionDetail.BranchId = CurrentBranchId;
                    accountTransactionDetail.Remarks = model.SalesInvoice.Remarks;
                    _accountTransaction.Add(accountTransactionDetail);

                    var stockTransaction = new StockTransaction();
                    stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.SalesInvoice.AgentCode);
                    stockTransaction.AltQty = salesDetail.AltQty;
                    stockTransaction.AltStockQty = salesDetail.AltStockQty;
                    stockTransaction.AltUnit = salesDetail.AltUnit;
                    stockTransaction.CurrCode = model.SalesInvoice.CurrCode;
                    stockTransaction.CurrRate = model.SalesInvoice.CurrRate;
                    stockTransaction.GlCode = model.SalesInvoice.GlCode;
                    stockTransaction.NetAmt = salesDetail.NetAmt;
                    stockTransaction.ProductCode = salesDetail.ProductCode;
                    stockTransaction.Quantity = salesDetail.Qty;
                    stockTransaction.Unit = unitcode;
                    stockTransaction.Rate = Convert.ToDecimal(salesDetail.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = source;
                    stockTransaction.ReferenceId = model.SalesInvoice.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.BatchSerialNo = item.BatchSerialNo;
                    stockTransaction.Godown = Convert.ToString(salesDetail.Godown);
                    var stock = _stockTransaction.Filter(x => x.ProductCode == item.ProductCode).ToList().LastOrDefault();
                    if (stock == null)
                    {
                        stockTransaction.StockVal = Convert.ToDecimal(salesDetail.BasicAmt);
                        stockTransaction.StockQty = salesDetail.StockQty;

                    }
                    else
                    {
                        stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(salesDetail.BasicAmt);
                        stockTransaction.StockQty = stock.StockQty - salesDetail.StockQty;
                    }
                    stockTransaction.TermAmt = salesDetail.TermAmt;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    stockTransaction.VDate = model.SalesInvoice.InvoiceDate;
                    stockTransaction.VMiti = model.SalesInvoice.InvoiceMiti;
                    stockTransaction.VNo = model.SalesInvoice.InvoiceNo;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;

                    if (billTermList != null)
                    {
                        decimal netAmt = 0;
                        //var billWiseTerms =
                        //    billTermList.Where(x => !x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList();
                        foreach (var term in billTermList.Where(x => x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList())
                        {
                            var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                            decimal drAmt = 0;
                            decimal drLocalAmt = 0;
                            decimal crAmt = 0;
                            decimal crLocalAmt = 0;
                            decimal termAmt = 0;
                            if (term.Sign == "+")
                            {
                                crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                termAmt = crAmt;
                            }
                            else
                            {
                                drAmt = Convert.ToDecimal(term.Amount);
                                drLocalAmt = Convert.ToDecimal(term.Amount);
                                termAmt = System.Math.Abs(drAmt) * (-1);
                            }
                            var billingTermDetail = new BillingTermDetail();
                            billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                            var index = Convert.ToInt32(term.BillingTermIndex);
                            billingTermDetail.Index = index;
                            billingTermDetail.ReferenceId = model.SalesInvoice.Id;
                            billingTermDetail.Source = source;
                            billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                            billingTermDetail.TermAmount = termAmt;
                            billingTermDetail.DetailId = salesDetail.Id;
                            billingTermDetail.Type = "P";
                            billingTermDetail.IsProductWise = term.IsProductWise;
                            _billingTermDetail.Add(billingTermDetail);

                            //AccountTransaction Posting
                            var accountTransactionBillingTerm = new AccountTransaction();
                            accountTransactionBillingTerm.VNo = model.SalesInvoice.InvoiceNo;
                            accountTransactionBillingTerm.VDate = model.SalesInvoice.InvoiceDate;
                            accountTransactionBillingTerm.VMiti = model.SalesInvoice.InvoiceMiti;
                            accountTransactionBillingTerm.CrRate = Convert.ToDecimal(model.SalesInvoice.CurrRate);
                            accountTransactionBillingTerm.CrCode = model.SalesInvoice.CurrCode;
                            accountTransactionBillingTerm.AgentCode = Convert.ToString(model.SalesInvoice.AgentCode);
                            accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                            accountTransactionBillingTerm.DrAmt = drAmt;
                            accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                            if (model.SalesInvoice.CurrencyId != 0 && model.SalesInvoice.CurrencyId != null)
                            {
                                accountTransactionBillingTerm.LocalDrAmt = drLocalAmt * Convert.ToDecimal(model.SalesInvoice.CurrRate);

                            }
                            accountTransactionBillingTerm.CrAmt = crAmt;
                            accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                            if (model.SalesInvoice.CurrencyId != 0 && model.SalesInvoice.CurrencyId != null)
                            {
                                accountTransactionBillingTerm.LocalCrAmt = crLocalAmt *
                                                                           Convert.ToDecimal(model.SalesInvoice.CurrRate);
                            }
                            accountTransactionBillingTerm.Source = source;
                            accountTransactionBillingTerm.SNo = 0;
                            accountTransactionBillingTerm.CreatedById = userId;
                            var isCashBankbill = _ledger.GetById(model.SalesInvoice.GlCode).IsCashBank;
                            if (isCashBankbill)
                                accountTransactionBillingTerm.CbCode = model.SalesInvoice.GlCode;
                            accountTransactionBillingTerm.FYId = FiscalYear.Id;
                            accountTransactionBillingTerm.ReferenceId = model.SalesInvoice.Id;
                            accountTransactionBillingTerm.BranchId = CurrentBranchId;
                            accountTransactionBillingTerm.Remarks = model.SalesInvoice.Remarks;
                            _accountTransaction.Add(accountTransactionBillingTerm);
                        }
                    }
                }
            }


            if (billTermList != null)
            {
                foreach (var term in billTermList.Where(x => !x.IsProductWise).ToList())
                {
                    var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                    decimal drAmt = 0;
                    decimal drLocalAmt = 0;
                    decimal crAmt = 0;
                    decimal crLocalAmt = 0;
                    decimal termAmt = 0;
                    if (term.Sign == "+")
                    {
                        crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        termAmt = crAmt;
                    }
                    else
                    {
                        drAmt = Convert.ToDecimal(term.Amount);
                        drLocalAmt = Convert.ToDecimal(term.Amount);
                        termAmt = System.Math.Abs(drAmt) * (-1);
                    }


                    var billingTermDetail = new BillingTermDetail();
                    billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                    billingTermDetail.Index = term.BillingTermIndex;
                    billingTermDetail.ReferenceId = model.SalesInvoice.Id;
                    billingTermDetail.Source = source;
                    billingTermDetail.Rate = Convert.ToDecimal(term.Rate);
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    billingTermDetail.DetailId = tempSalesDetailList[index].Id;
                    billingTermDetail.IsProductWise = term.IsProductWise;
                    billingTermDetail.TermAmount = termAmt;
                    billingTermDetail.Type = "B";
                    _billingTermDetail.Add(billingTermDetail);

                    //AccountTransaction Posting
                    var accountTransactionBillingTerm = new AccountTransaction();
                    accountTransactionBillingTerm.VNo = model.SalesInvoice.InvoiceNo;
                    accountTransactionBillingTerm.VDate = model.SalesInvoice.InvoiceDate;
                    accountTransactionBillingTerm.VMiti = model.SalesInvoice.InvoiceMiti;
                    accountTransactionBillingTerm.CrRate = Convert.ToDecimal(model.SalesInvoice.CurrRate);
                    accountTransactionBillingTerm.CrCode = model.SalesInvoice.CurrCode;
                    accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                    accountTransactionBillingTerm.SlCode = model.SalesInvoice.SlCode;
                    accountTransactionBillingTerm.DrAmt = drAmt;
                    accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                    if (model.SalesInvoice.CurrencyId != 0 && model.SalesInvoice.CurrencyId != null)
                    {
                        accountTransactionBillingTerm.LocalDrAmt = drLocalAmt * Convert.ToDecimal(model.SalesInvoice.CurrRate);
                    }

                    accountTransactionBillingTerm.CrAmt = crAmt;
                    accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                    if (model.SalesInvoice.CurrencyId != 0 && model.SalesInvoice.CurrencyId != null)
                    {
                        accountTransactionBillingTerm.LocalCrAmt = crLocalAmt * Convert.ToDecimal(model.SalesInvoice.CurrRate);
                    }

                    accountTransactionBillingTerm.Source = source;
                    accountTransactionBillingTerm.SNo = 0;
                    accountTransactionBillingTerm.CreatedById = userId;
                    var isCashBankbill = _ledger.GetById(model.SalesInvoice.GlCode).IsCashBank;
                    if (isCashBankbill)
                        accountTransactionBillingTerm.CbCode = model.SalesInvoice.GlCode;
                    accountTransactionBillingTerm.FYId = FiscalYear.Id;
                    accountTransactionBillingTerm.ReferenceId = model.SalesInvoice.Id;
                    accountTransactionBillingTerm.BranchId = CurrentBranchId;
                    accountTransactionBillingTerm.AgentCode = Convert.ToString(model.SalesInvoice.AgentCode);
                    accountTransactionBillingTerm.Remarks = model.SalesInvoice.Remarks;
                    _accountTransaction.Add(accountTransactionBillingTerm);

                }
            }
            _udfData.Delete(x => x.ReferenceId == model.SalesInvoice.Id && x.SourceId == (int)EntryModuleEnum.SalesInvoice);
            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.SalesInvoice);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = model.SalesInvoice.Id,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.SalesInvoice
                    };
                    _udfData.Add(i);
                }
            }
            return Json(new { success = true, isEdit = true });
        }

        public ActionResult SalesInvoicePrint(int invoiceId)
        {
            var model = _salesInvoice.GetById(invoiceId);
            var billDate = Convert.ToDateTime(model.InvoiceDate).ToString("MM/dd/yyyy");
            var invoice = new PurchaseInvoicePrintViewModel();
            invoice.InvoiceHeader = new InvoiceHeader();
            var logo = "NoImage.jpg";
            if (!string.IsNullOrEmpty(base.CompanyInfo.LogoGuid))
                logo = base.CompanyInfo.LogoGuid + base.CompanyInfo.LogoExt;
            var filePath = Server.MapPath("~/Content/Logo/" + logo);
            if (!System.IO.File.Exists(filePath))
            {
                logo = "NoImage.jpg";
            }
            invoice.InvoiceHeader.CompanyLogoUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "/Content/Logo/" + logo;
            invoice.InvoiceHeader.InvoiceDate = billDate;
            invoice.InvoiceHeader.InvoiceNo = model.InvoiceNo;
            invoice.InvoiceHeader.CompanyAddress = base.CompanyInfo.Address;
            invoice.InvoiceHeader.CompanyName = base.CompanyInfo.Name;
            invoice.InvoiceHeader.PartyName = model.Ledger.AccountName;
            invoice.BilledBy = model.User.FullName;
            int sno = 1;
            double subTotal = 0;
            invoice.InvoiceDetails = new List<InvoiceDetail>();
            foreach (var item in model.SalesDetails)
            {
                if (item.BasicAmt != 0)
                {
                    var product = _product.GetById(item.ProductCode);
                    var invoiceDetail = new InvoiceDetail();
                    invoiceDetail.Id = item.Id;
                    invoiceDetail.Amount = Convert.ToDecimal(item.BasicAmt);
                    invoiceDetail.Particular = product.Name;
                    invoiceDetail.SNo = sno;
                    invoiceDetail.Qty = Convert.ToDecimal(item.Qty);
                    invoiceDetail.Rate = Convert.ToDecimal(item.Rate);
                    invoiceDetail.Remarks = item.Remarks;
                    sno++;
                    subTotal += Convert.ToDouble(item.BasicAmt);
                    invoice.InvoiceDetails.Add(invoiceDetail);
                }
            }
            var billingTerms = _billingTermDetail.GetMany(x => x.ReferenceId == invoiceId && x.Source == "SB").OrderBy(x => x.Index);
            invoice.InvoiceBillingTerms = new List<InvoiceBillingTerms>();
            double termAmt = 0;
            foreach (var item in billingTerms)
            {
               
                var invoiceBillingTerm = new InvoiceBillingTerms();
                var termName = _billingTerm.GetById(x => x.Id == item.BillingTermId && x.Type == "S").Description;
                invoiceBillingTerm.TermName = termName;
                invoiceBillingTerm.TermRate = item.Rate;
                invoiceBillingTerm.Type = item.Type;
                invoiceBillingTerm.TermAmount = item.TermAmount;
                item.DetailId = item.DetailId;

                invoice.InvoiceBillingTerms.Add(invoiceBillingTerm);
                termAmt += Convert.ToDouble(item.TermAmount);
            }
            invoice.SubTotal = subTotal;
            invoice.Total = subTotal + termAmt;
           return this.ViewPdf("", "SalesInvoicePrint", invoice);
            //var view = this.RenderPartialViewToString("SalesInvoicePrint", invoice);
           //return Json(view, JsonRequestBehavior.AllowGet);
            //return View();
        }
       

        [CheckPermission(Permissions = "Approve", Module = "SB")]
        [HttpPost]
        public ActionResult SalesInvoiceApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _salesInvoice.GetById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _salesInvoice.Update(data);
            return null;
        }

        [CheckPermission(Permissions = "Create", Module = "SB")]
        public ActionResult GetSalesDetailEntry(int? index)
        {
            var viewModel = new SalesDetailEntryViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            if (index != 0)

                viewModel.Index = Convert.ToInt32(index);
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            viewModel.EntryControl = _entryControlSales.GetAll().FirstOrDefault();
            viewModel.DetailGuid = Guid.NewGuid().ToString();
            return PartialView("_PartialSalesDetailEntry", viewModel);
        }

        [CheckPermission(Permissions = "Edit", Module = "SB")]
        public ActionResult GetSalesDetailEdit(int? id, int? index)
        {
            var data = new SalesDetail();
            data.EntryControl = _entryControlSales.GetAll().FirstOrDefault();
            data.Unitlist = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");

            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id).ToList();
            var defaultIndex = billTerm.Max(x => x.Index);
            ViewBag.Index = defaultIndex == null ? 0 : defaultIndex + 1;
            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }

            ViewBag.BillingTermList = billTerm;
            var cat = Convert.ToInt32(Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                ViewBag.AllowProductWiseBillTerm = 1;
            else
            {
                ViewBag.AllowProductWiseBillTerm = 0;
            }
            if (index != null)
                data.Index = Convert.ToInt32(index);
            data.DetailGuid = Guid.NewGuid().ToString();
            return PartialView("_PartialSalesDetailEdit", data);
        }

        #endregion

        #region Sales Return

        public JsonResult CheckInvoiceNoInSalesReturnMaster(string InvoiceNo, int? Id)
        {
            var feeterm = new SalesReturnMaster();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _salesReturnMaster.GetById(x => x.Id == Id);
                if (data.InvoiceNo.ToLower().Trim() != InvoiceNo.ToLower().Trim())
                {
                    feeterm =
                        _salesReturnMaster.GetById(x => x.InvoiceNo.ToLower().Trim() == InvoiceNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
                }
            }
            else
            {
                feeterm = _salesReturnMaster.GetById(x => x.InvoiceNo.ToLower().Trim() == InvoiceNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new SalesReturnMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Navigate", Module = "SR")]
        public ActionResult SalesReturn()
        {
            var viewModel = new SalesReturnViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.Category == cat && !x.IsDeleted);
            var billingTermProductWise = billingTerm.Where(x => x.IsProductWise).OrderBy(x => x.DispalyOrder);
            var billingTermBillWise = billingTerm.Where(x => !x.IsProductWise).OrderBy(x => x.DispalyOrder);
            viewModel.ProductWiseBillTerms = new List<BillingTermSelectViewModel>();
            viewModel.BillWiseBillTerms = new List<BillingTermSelectViewModel>();
            billingTermProductWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.ProductWiseBillTerms.Add(billingTermSelectList);
            });
            billingTermBillWise.ToList().ForEach(item =>
            {
                var billingTermSelectList = new BillingTermSelectViewModel();
                billingTermSelectList.Id = item.Id;
                billingTermSelectList.Basis =
                    Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTermSelectList.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTermSelectList.Sign =
                    stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTermSelectList.Rate = item.Rate;
                billingTermSelectList.DisplayOrder = item.DispalyOrder;
                billingTermSelectList.Amount = 0;
                viewModel.BillWiseBillTerms.Add(billingTermSelectList);
            });
            return View(viewModel);
        }

        [CheckPermission(Permissions = "Navigate", Module = "SR")]
        public ActionResult SalesReturnList()
        {
            var viewModel = new SalesReturnViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var listPending = _salesReturnMaster.Filter(x => !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            // var listAccepted = _salesReturnMaster.Filter(x => x.IsApproved).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(1, base.SystemControl.PageSize);
            //  viewModel.AcceptedList = listAccepted;
            viewModel.PendingList = listPending;
            return PartialView(viewModel);
        }

        public ActionResult SalesReturnPendingList(int pageNo)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var list = _salesReturnMaster.Filter(x => !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView(list);
        }

        //public ActionResult SalesReturnAcceptedList(int pageNo)
        //{
        //    var list = _salesReturnMaster.Filter(x => x.IsApproved).AsQueryable().OrderByDescending(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
        //    return PartialView(list);
        //}
        public ActionResult PopulateSalesReturnLedgerList(int id, string date)
        {
            var vendor = _ledger.GetById(x => x.Id == id);
            //   var purchaseInvoice = new PurchaseInvoice();
            var agent = string.Empty;
            var currency = string.Empty;
            var dateDue = string.Empty;
            // var miti = string.Empty;
            if (vendor.CreditDays != null)
            {
                var dueDays = Convert.ToInt32(vendor.CreditDays);
                if (base.SystemControl.DateType == 1)
                {
                    var dueDate = Convert.ToDateTime(date);
                    dateDue = dueDate.AddDays(+dueDays).ToString("MM/dd/yyyy");
                }
                else
                {
                    var dueMiti = NepaliDateService.NepalitoEnglishDate(date).Date;
                    var data = dueMiti.AddDays(+dueDays);
                    dateDue = NepaliDateService.NepaliDate(data).Date;
                }
                // purchaseInvoice.DueDay = dueDays;
            }

            if (vendor.AgentId != null)
            {
                var agentId = Convert.ToInt32(vendor.AgentId);
                var agentData = _agent.GetById(agentId);
                if (agentData != null)
                    agent = agentData.Name;
            }
            if (vendor.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(vendor.CurrencyId);
                var currCode = _currency.GetById(currencyId);
                if (currCode != null)
                    currency = currCode.Name;
            }

            return Json(new
            {
                VendorName = vendor.AccountName,
                CreditDays = vendor.CreditDays,
                AgentName = agent,
                AgentId = vendor.AgentId,
                CurrencyId = vendor.CurrencyId,
                Currency = currency,
                DueDate = dateDue,
                DateType = base.SystemControl.DateType
                //DueMiti=miti
            });
        }

        [HttpPost]
        public ActionResult SalesReturnDelete(int id)
        {
            var model = _salesReturnMaster.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _salesReturnMaster.Update(model);
            _accountTransaction.Delete(x => x.Source == "SR" && x.ReferenceId == id);
            _stockTransaction.Delete(x => x.Source == "SR" && x.ReferenceId == id);
            return Json(true);
        }

        public ActionResult CheckFiscalyearDateinSalesReturn(string InvoiceDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(InvoiceDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(InvoiceDate);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }
        [CheckPermission(Permissions = "Create", Module = "SR")]
        public ActionResult SalesReturnAdd()
        {
            //var data = _salesReturnMaster.GetAll().LastOrDefault();
            var viewModel = base.CreateViewModel<SalesReturnAddViewModel>();
            viewModel.CnTypeList = new SelectList(
                Enum.GetValues(typeof(CnTypeEnum)).Cast
                    <CnTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");
            // viewModel.Date = data != null ? data.InvoiceDate : DateTime.UtcNow;
            viewModel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.SalesInvoiceReturn).ToList();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.SalesReturnMaster = new SalesReturnMaster();
            viewModel.Unitlist = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _salesReturnMaster.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? data.InvoiceDate.ToShortDateString() : data.InvoiceMiti;
                }
                else
                {
                    viewModel.InvoiceDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }
            }
            viewModel.EntryControl = _entryControlSales.GetAll().FirstOrDefault();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SalesReturnAdd(IEnumerable<SalesReturnDetailEntryViewModel> salesReturnDetailEntry, SalesReturnAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList, IEnumerable<PurchaseProductBatchViewModel> ProductBatchList)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.SalesReturnMaster.CreatedById = userId;
            model.SalesReturnMaster.CreatedDate = DateTime.UtcNow;
            model.SalesReturnMaster.InvoiceDate = Convert.ToDateTime(model.InvoiceDate);
            model.SalesReturnMaster.FYId = FiscalYear.Id;
            model.SalesReturnMaster.BranchId = CurrentBranchId;
            if (base.SystemControl.DateType == 1)
            {
                model.SalesReturnMaster.InvoiceDate = model.InvoiceDate.ToDate();
                model.SalesReturnMaster.InvoiceMiti = NepaliDateService.NepaliDate(model.SalesReturnMaster.InvoiceDate).Date;
            }
            else
            {
                model.SalesReturnMaster.InvoiceMiti = model.InvoiceDate;
                model.SalesReturnMaster.InvoiceDate = NepaliDateService.NepalitoEnglishDate(model.InvoiceDate);
            }
            if (!string.IsNullOrEmpty(model.RefDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesReturnMaster.RefBillDate = model.RefDate.ToDate();
                    model.SalesReturnMaster.RefBillMiti = NepaliDateService.NepaliDate(model.RefDate.ToDate()).Date;
                }
                else
                {
                    model.SalesReturnMaster.RefBillMiti = model.RefDate;
                    model.SalesReturnMaster.RefBillDate = NepaliDateService.NepalitoEnglishDate(model.RefDate);
                }
            }
            if (model.SalesReturnMaster.CurrencyId != 0 && model.SalesReturnMaster.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(model.SalesReturnMaster.CurrencyId);
                //var Code = _currency.GetById(model.SalesReturnMaster.CurrencyId);
                var Code = _currency.GetById(currencyId);
                model.SalesReturnMaster.CurrCode = Convert.ToString(Code.Id);
            }
            _salesReturnMaster.Add(model.SalesReturnMaster);
            var Source = StringEnum.Parse(typeof(ModuleEnum), "Sales Return").ToString();
            int sno = 1;
            decimal basicAmt = 0;
            var tempSalesReturnDetailList = new List<SalesReturnDetail>();

            foreach (var item in salesReturnDetailEntry)
            {
                if (item.ProductId != null && item.ProductId != 0 && item.BasicAmt != null && item.BasicAmt != 0 && item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    var unitcode = _unitRepository.GetById(item.UnitId).Code;
                    var batchSerialNo = string.Empty;
                    basicAmt += Convert.ToDecimal(item.BasicAmt);
                    var saleReturnDetail = new SalesReturnDetail();
                    saleReturnDetail.SNo = sno;
                    saleReturnDetail.ProductCode = item.ProductId;
                    saleReturnDetail.AltQty = item.AltQty;
                    saleReturnDetail.Qty = Convert.ToDecimal(item.Qty);
                    saleReturnDetail.AltStockQty = item.AltQty;
                    saleReturnDetail.StockQty = Convert.ToDecimal(item.Qty);
                    saleReturnDetail.Rate = Convert.ToDecimal(item.Rate);
                    saleReturnDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    saleReturnDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    saleReturnDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    saleReturnDetail.Remarks = model.SalesReturnMaster.Remarks;
                    saleReturnDetail.SalesReturnId = model.SalesReturnMaster.Id;
                    saleReturnDetail.Index = item.Index;
                    saleReturnDetail.Unit = unitcode;
                    saleReturnDetail.AltUnit = item.AltUnit;
                    saleReturnDetail.Godown = item.GodownId;
                    _salesReturnDetail.Add(saleReturnDetail);
                    tempSalesReturnDetailList.Add(saleReturnDetail);

                    if (ProductBatchList != null)
                    {
                        var batch = ProductBatchList.Where(x => x.ParentGuid == item.DetailGuid).FirstOrDefault();
                        if (batch != null)
                        {
                            batchSerialNo = batch.BatchSerialNo;
                            var productBatch = new PurchaseProductBatch();
                            productBatch.BranchId = base.CurrentBranch.Id;
                            productBatch.Godown = item.GodownId;
                            productBatch.ProductId = item.ProductId;
                            productBatch.DetailId = saleReturnDetail.Id;
                            productBatch.Source = "SR";
                            productBatch.Qty = Convert.ToDecimal(item.Qty);
                            productBatch.SerialNo = batch.BatchSerialNo;
                            if (batch.IsMfgDate)
                                productBatch.MFGDate = batch.MfgDate;
                            if (batch.IsExpDate)
                                productBatch.EXPDate = batch.ExpDate;
                            productBatch.BuyRate = batch.BuyRate;
                            productBatch.SalesRate = batch.SalesRate;
                            productBatch.Unit = item.UnitId;
                            productBatch.StockQuantity = Convert.ToDecimal(item.Qty);
                            _purchaseProductBatch.Add(productBatch);
                        }
                    }
                    var accountTransactionDetail = new AccountTransaction();
                    accountTransactionDetail.VNo = model.SalesReturnMaster.InvoiceNo;
                    accountTransactionDetail.VDate = model.SalesReturnMaster.InvoiceDate;
                    accountTransactionDetail.VMiti = model.SalesReturnMaster.InvoiceMiti;
                    accountTransactionDetail.CrRate = Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                    accountTransactionDetail.CrCode = model.SalesReturnMaster.CurrCode;

                    //Sales Return A/C Id need to specify in setting
                    int salesReturnAc;
                    var product = _product.GetById(item.ProductId);
                    salesReturnAc = product.SalesReturnId != null && product.SalesReturnId != 0 ? Convert.ToInt32(product.SalesReturnId) : base.SystemControl.SalesReturnAc;
                    // accountTransactionDetail.AgentCode = UtilityService.GetAgentNameById(model.SalesReturnMaster.AgentCode);
                    accountTransactionDetail.GlCode = model.SalesReturnMaster.GlCode;
                    accountTransactionDetail.SlCode = model.SalesReturnMaster.SlCode;
                    accountTransactionDetail.DrAmt = Convert.ToDecimal(item.BasicAmt);
                    accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.BasicAmt);
                    if (model.SalesReturnMaster.CurrencyId != 0 && model.SalesReturnMaster.CurrencyId != null)
                    {
                        accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.BasicAmt) * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                    }
                    accountTransactionDetail.CrAmt = 0;
                    accountTransactionDetail.LocalCrAmt = 0;
                    accountTransactionDetail.Source = Source;
                    accountTransactionDetail.SNo = 0;
                    accountTransactionDetail.CreatedById = userId;
                    var isCashBankDetail = _ledger.GetById(salesReturnAc).IsCashBank;
                    if (isCashBankDetail)
                        accountTransactionDetail.CbCode = model.SalesReturnMaster.GlCode;
                    accountTransactionDetail.ReferenceId = model.SalesReturnMaster.Id;
                    accountTransactionDetail.FYId = FiscalYear.Id;
                    accountTransactionDetail.BranchId = CurrentBranchId;
                    accountTransactionDetail.Remarks = model.SalesReturnMaster.Remarks;
                    accountTransactionDetail.AgentCode = Convert.ToString(model.SalesReturnMaster.AgentCode);
                    _accountTransaction.Add(accountTransactionDetail);

                    var stockTransaction = new StockTransaction();
                    stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.SalesReturnMaster.AgentCode);
                    stockTransaction.AltQty = saleReturnDetail.AltQty;
                    stockTransaction.AltStockQty = saleReturnDetail.AltStockQty;
                    stockTransaction.AltUnit = saleReturnDetail.AltUnit;
                    stockTransaction.CurrCode = model.SalesReturnMaster.CurrCode;
                    stockTransaction.CurrRate = model.SalesReturnMaster.CurrRate;
                    stockTransaction.GlCode = salesReturnAc;
                    stockTransaction.NetAmt = saleReturnDetail.NetAmt;
                    stockTransaction.ProductCode = saleReturnDetail.ProductCode;
                    stockTransaction.Quantity = saleReturnDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(saleReturnDetail.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = Source;
                    stockTransaction.ReferenceId = model.SalesReturnMaster.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.Unit = unitcode;
                    stockTransaction.BatchSerialNo = batchSerialNo;
                    stockTransaction.Godown = Convert.ToString(saleReturnDetail.Godown);
                    var stock = _stockTransaction.Filter(x => x.ProductCode == item.ProductId).ToList().LastOrDefault();
                    if (stock == null)
                    {
                        stockTransaction.StockVal = Convert.ToDecimal(saleReturnDetail.BasicAmt); ;
                        stockTransaction.StockQty = saleReturnDetail.StockQty;

                    }
                    else
                    {
                        stockTransaction.StockVal = stock.StockVal + Convert.ToDecimal(saleReturnDetail.BasicAmt); ;
                        stockTransaction.StockQty = stock.StockQty + saleReturnDetail.StockQty;
                    }
                    stockTransaction.TermAmt = saleReturnDetail.TermAmt;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                    stockTransaction.VDate = model.SalesReturnMaster.InvoiceDate;
                    stockTransaction.VMiti = model.SalesReturnMaster.InvoiceMiti;
                    stockTransaction.VNo = model.SalesReturnMaster.InvoiceNo;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;

                    if (billTermList != null)
                    {
                        decimal netAmt = 0;
                        //var billWiseTerms =
                        //    billTermList.Where(x => !x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList();
                        foreach (var term in billTermList.Where(x => x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList())
                        {
                            var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                            decimal drAmt = 0;
                            decimal drLocalAmt = 0;
                            decimal crAmt = 0;
                            decimal crLocalAmt = 0;
                            decimal termAmt = 0;
                            if (term.Sign == "-")
                            {
                                crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                termAmt = System.Math.Abs(crAmt) * (-1);
                            }
                            else
                            {
                                drAmt = Convert.ToDecimal(term.Amount);
                                drLocalAmt = Convert.ToDecimal(term.Amount);
                                termAmt = drAmt;
                            }
                            var billingTermDetail = new BillingTermDetail();
                            billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                            var index = Convert.ToInt32(term.BillingTermIndex);
                            billingTermDetail.Index = index;
                            billingTermDetail.ReferenceId = model.SalesReturnMaster.Id;
                            billingTermDetail.Source = Source;
                            billingTermDetail.Rate = Convert.ToDecimal(term.Rate);
                            billingTermDetail.TermAmount = termAmt;
                            billingTermDetail.DetailId = saleReturnDetail.Id;
                            billingTermDetail.Type = "P";
                            billingTermDetail.IsProductWise = term.IsProductWise;
                            _billingTermDetail.Add(billingTermDetail);

                            //AccountTransaction Posting
                            var accountTransactionBillingTerm = new AccountTransaction();
                            accountTransactionBillingTerm.VNo = model.SalesReturnMaster.InvoiceNo;
                            accountTransactionBillingTerm.VDate = model.SalesReturnMaster.InvoiceDate;
                            accountTransactionBillingTerm.VMiti = model.SalesReturnMaster.InvoiceMiti;
                            accountTransactionBillingTerm.CrRate = Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                            accountTransactionBillingTerm.AgentCode = Convert.ToString(model.SalesReturnMaster.AgentCode);
                            accountTransactionBillingTerm.CrCode = model.SalesReturnMaster.CurrCode;
                            accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                            if (model.SalesReturnMaster.CurrencyId != null && model.SalesReturnMaster.CurrencyId != 0)
                            {
                                drLocalAmt = drLocalAmt * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                                crLocalAmt = crLocalAmt * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                            }
                            accountTransactionBillingTerm.DrAmt = drAmt;
                            accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                            accountTransactionBillingTerm.CrAmt = crAmt;
                            accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;

                            accountTransactionBillingTerm.Source = Source;
                            accountTransactionBillingTerm.SNo = 0;
                            accountTransactionBillingTerm.CreatedById = userId;
                            accountTransactionBillingTerm.CbCode = model.SalesReturnMaster.GlCode;
                            accountTransactionBillingTerm.FYId = FiscalYear.Id;
                            accountTransactionBillingTerm.ReferenceId = model.SalesReturnMaster.Id;
                            accountTransactionBillingTerm.BranchId = CurrentBranchId;
                            accountTransactionBillingTerm.Remarks = model.SalesReturnMaster.Remarks;
                            _accountTransaction.Add(accountTransactionBillingTerm);
                        }
                    }
                }
            }

            if (billTermList != null)
            {
                foreach (var term in billTermList.Where(x => !x.IsProductWise).ToList())
                {
                    var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                    decimal drAmt = 0;
                    decimal drLocalAmt = 0;
                    decimal crAmt = 0;
                    decimal crLocalAmt = 0;
                    decimal termAmt = 0;
                    if (term.Sign == "-")
                    {
                        crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        termAmt = System.Math.Abs(crAmt) * (-1);
                    }
                    else
                    {
                        drAmt = Convert.ToDecimal(term.Amount);
                        drLocalAmt = Convert.ToDecimal(term.Amount);
                        termAmt = drAmt;
                    }
                    var billingTermDetail = new BillingTermDetail();
                    billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    billingTermDetail.Index = index;
                    billingTermDetail.ReferenceId = model.SalesReturnMaster.Id;
                    billingTermDetail.Source = Source;
                    billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                    billingTermDetail.TermAmount = termAmt;
                    billingTermDetail.Type = "B";
                    billingTermDetail.IsProductWise = term.IsProductWise;
                    _billingTermDetail.Add(billingTermDetail);

                    //AccountTransaction Posting
                    var accountTransactionBillingTerm = new AccountTransaction();
                    accountTransactionBillingTerm.VNo = model.SalesReturnMaster.InvoiceNo;
                    accountTransactionBillingTerm.VDate = model.SalesReturnMaster.InvoiceDate;
                    accountTransactionBillingTerm.VMiti = model.SalesReturnMaster.InvoiceMiti;
                    accountTransactionBillingTerm.CrRate = Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                    accountTransactionBillingTerm.AgentCode = Convert.ToString(model.SalesReturnMaster.AgentCode);
                    accountTransactionBillingTerm.CrCode = model.SalesReturnMaster.CurrCode;
                    accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                    accountTransactionBillingTerm.SlCode = model.SalesReturnMaster.SlCode;
                    if (model.SalesReturnMaster.CurrencyId != null && model.SalesReturnMaster.CurrencyId != 0)
                    {
                        drLocalAmt = drLocalAmt * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                        crLocalAmt = crLocalAmt * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                    }
                    accountTransactionBillingTerm.DrAmt = drAmt;
                    accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                    accountTransactionBillingTerm.CrAmt = crAmt;
                    accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                    accountTransactionBillingTerm.Source = Source;
                    accountTransactionBillingTerm.SNo = 0;
                    accountTransactionBillingTerm.CreatedById = userId;
                    var isCashBankbill = _ledger.GetById(billingTerm.GeneralLedgerId).IsCashBank;
                    if (isCashBankbill)
                        accountTransactionBillingTerm.CbCode = billingTerm.GeneralLedgerId;
                    accountTransactionBillingTerm.FYId = FiscalYear.Id;
                    accountTransactionBillingTerm.ReferenceId = model.SalesReturnMaster.Id;
                    accountTransactionBillingTerm.BranchId = CurrentBranchId;
                    accountTransactionBillingTerm.Remarks = model.SalesReturnMaster.Remarks;
                    _accountTransaction.Add(accountTransactionBillingTerm);
                }
            }

            var accountTransactionMaster = new AccountTransaction();
            accountTransactionMaster.VNo = model.SalesReturnMaster.InvoiceNo;
            accountTransactionMaster.VDate = model.SalesReturnMaster.InvoiceDate;
            accountTransactionMaster.VMiti = model.SalesReturnMaster.InvoiceMiti;
            accountTransactionMaster.AgentCode = Convert.ToString(model.SalesReturnMaster.AgentCode);
            accountTransactionMaster.CrRate = Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
            accountTransactionMaster.CrCode = model.SalesReturnMaster.CurrCode;
            accountTransactionMaster.GlCode = model.SalesReturnMaster.GlCode;
            accountTransactionMaster.SlCode = model.SalesReturnMaster.SlCode;
            accountTransactionMaster.DrAmt = 0;
            accountTransactionMaster.LocalDrAmt = 0;
            accountTransactionMaster.CrAmt = Convert.ToDecimal(model.SalesReturnMaster.NetAmt);
            accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(model.SalesReturnMaster.NetAmt);
            if (model.SalesReturnMaster.CurrencyId != 0 && model.SalesReturnMaster.CurrencyId != null)
            {
                accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(model.SalesReturnMaster.NetAmt) * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
            }
            accountTransactionMaster.Source = Source;
            accountTransactionMaster.SNo = 0;
            accountTransactionMaster.CreatedById = userId;
            var isCashBank = _ledger.GetById(model.SalesReturnMaster.GlCode).IsCashBank;
            if (isCashBank)
                accountTransactionMaster.CbCode = model.SalesReturnMaster.GlCode;
            accountTransactionMaster.ReferenceId = model.SalesReturnMaster.Id;
            accountTransactionMaster.FYId = FiscalYear.Id;
            accountTransactionMaster.BranchId = CurrentBranchId;
            accountTransactionMaster.Remarks = model.SalesReturnMaster.Remarks;
            _accountTransaction.Add(accountTransactionMaster);
            model.SalesReturnOtherDetail.SalesReturnId = model.SalesReturnMaster.Id;
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesReturnOtherDetail.CnDate = model.CnDate.ToDate();
                    model.SalesReturnOtherDetail.CnMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesReturnOtherDetail.CnDate)).Date;
                }
                else
                {
                    model.SalesReturnOtherDetail.CnMiti = model.CnDate;
                    model.SalesReturnOtherDetail.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }
            if (!string.IsNullOrEmpty(model.PODate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesReturnOtherDetail.PODate = model.PODate.ToDate();
                    model.SalesReturnOtherDetail.POMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesReturnOtherDetail.PODate)).Date;
                }
                else
                {
                    model.SalesReturnOtherDetail.POMiti = model.PODate;
                    model.SalesReturnOtherDetail.PODate = NepaliDateService.NepalitoEnglishDate(model.PODate);
                }
            }
            if (!string.IsNullOrEmpty(model.ContractDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesReturnOtherDetail.ContractDate = model.ContractDate.ToDate();
                    model.SalesReturnOtherDetail.ContractMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesReturnOtherDetail.ContractDate)).Date;
                }
                else
                {
                    model.SalesReturnOtherDetail.ContractMiti = model.ContractDate;
                    model.SalesReturnOtherDetail.ContractDate = NepaliDateService.NepalitoEnglishDate(model.ContractDate);
                }
            }
            if (!string.IsNullOrEmpty(model.ExportInvoiceDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesReturnOtherDetail.ExportInvoiceDate = model.ExportInvoiceDate.ToDate();
                    model.SalesReturnOtherDetail.ExportInvoiceMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesReturnOtherDetail.ExportInvoiceDate)).Date;
                }
                else
                {
                    model.SalesReturnOtherDetail.ExportInvoiceMiti = model.ExportInvoiceDate;
                    model.SalesReturnOtherDetail.ExportInvoiceDate = NepaliDateService.NepalitoEnglishDate(model.ExportInvoiceDate);
                }
            }

            _salesReturnOtherDetailRepository.Add(model.SalesReturnOtherDetail);


           _udfData.Delete(x => x.ReferenceId == model.SalesReturnMaster.Id && x.SourceId == (int)EntryModuleEnum.SalesInvoiceReturn);
            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.SalesInvoiceReturn);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = model.SalesReturnMaster.Id,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.SalesInvoiceReturn
                    };
                    _udfData.Add(i);
                }
            }
            return Json(new { success = true, isEdit = false });
        }

        [CheckPermission(Permissions = "Edit", Module = "SR")]
        public ActionResult SalesReturnEdit(int id)
        {
            var data = _salesReturnMaster.GetById(id);
            var otherdetial = _salesReturnOtherDetailRepository.GetById(x => x.SalesReturnId == data.Id);
            var viewModel = base.CreateViewModel<SalesReturnAddViewModel>();
            viewModel.CnTypeList = new SelectList(
                Enum.GetValues(typeof(CnTypeEnum)).Cast
                    <CnTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text", otherdetial.CnType);
            viewModel.SalesReturnMaster = data;
            viewModel.SalesReturnOtherDetail = otherdetial;
            if (!string.IsNullOrEmpty(data.CurrCode))
            {
                // viewModel.SalesReturnMaster.CurrCode = data.CurrCode;
                // viewModel.SalesReturnMaster.CurrencyId = _currency.GetById(x => x.Code == data.CurrCode).Id;
                var CurrId = Convert.ToInt32(data.CurrCode);
                viewModel.SalesReturnMaster.CurrCode = _currency.GetById(x => x.Id == CurrId).Code;
                viewModel.SalesReturnMaster.CurrencyId = _currency.GetById(x => x.Id == CurrId).Id;
            }
            viewModel.UdfEntries = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.SalesInvoiceReturn).ToList();
            ViewBag.Id = data.Id;

            ViewBag.AllowProductWiseBillTerm = 0;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                ViewBag.AllowProductWiseBillTerm = 1;
            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id && x.Source == "SR").ToList();
            if (base.SystemControl.DateType == 1)
            {
                viewModel.InvoiceDate = data.InvoiceDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.InvoiceDate = data.InvoiceMiti;
            }
            if (data.RefBillDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.RefDate = Convert.ToDateTime(data.RefBillDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.RefDate = data.RefBillMiti;
                }
            }
            if (otherdetial.CnDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.CnDate = Convert.ToDateTime(otherdetial.CnDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.CnDate = otherdetial.CnMiti;
                }
            }
            if (otherdetial.PODate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.PODate = Convert.ToDateTime(otherdetial.PODate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.PODate = otherdetial.POMiti;
                }
            }
            if (otherdetial.ContractDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.ContractDate = Convert.ToDateTime(otherdetial.ContractDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.ContractDate = otherdetial.ContractMiti;
                }
            }
            if (otherdetial.ExportInvoiceDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.ExportInvoiceDate = Convert.ToDateTime(otherdetial.ExportInvoiceDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.ExportInvoiceDate = otherdetial.ExportInvoiceMiti;
                }
            }

            //var defaultIndex = billTerm.Max(x => x.Index);
            var defaultIndex = data.SalesReturnDetails.Max(x => x.Index);

            foreach (var term in viewModel.SalesReturnMaster.SalesReturnDetails)
            {
                term.UnitId = _unitRepository.GetById(x => x.Code.ToLower() == term.Unit.ToLower()).Id;
            }

            ViewBag.Index = defaultIndex == null ? 1 : defaultIndex + 1;
            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }
            viewModel.Unitlist = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            ViewBag.BillingTermList = billTerm;
            viewModel.TotalAmtRs = viewModel.SalesReturnMaster.NetAmt.ToString();

            var context = new DataContext();
            var PurchaseProductBatchViewModels = (from pd in context.SalesReturnDetails.Where(x => x.SalesReturnId == id)
                                                  join pb in context.PurchaseProductBatches.Where(x => x.Source == "SR") on pd.Id equals pb.DetailId
                                                  join p in context.Products on pb.ProductId equals p.Id
                                                  join gd in context.Godowns on pb.Godown equals gd.Id into g
                                                  from gd in g.DefaultIfEmpty()
                                                  select new PurchaseProductBatchViewModel()
                                                  {
                                                      BatchSerialNo = pb.SerialNo,
                                                      BuyRate = pb.BuyRate,
                                                      ExpDate = pb.EXPDate,
                                                      Godown = gd == null ? string.Empty : gd.Name,
                                                      GodownId = gd == null ? 0 : gd.Id,
                                                      IsExpDate = p.ExpDate,
                                                      IsMfgDate = p.MfgDate,
                                                      MfgDate = pb.MFGDate,
                                                      ProductId = pb.ProductId,
                                                      Qty = pb.Qty,
                                                      SalesRate = pb.SalesRate,
                                                      UnitId = pb.Unit,
                                                      DetailId = pd.Id
                                                  }).ToList();
            ViewBag.PurchaseProductBatchViewModels = PurchaseProductBatchViewModels;
            viewModel.EntryControl = _entryControlSales.GetAll().FirstOrDefault();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SalesReturnEdit(IEnumerable<SalesReturnDetail> salesReturnDetailEntry, SalesReturnAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList, IEnumerable<PurchaseProductBatchViewModel> ProductBatchList)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.SalesReturnMaster.FYId = FiscalYear.Id;
            if (base.SystemControl.DateType == 1)
            {
                model.SalesReturnMaster.InvoiceDate = Convert.ToDateTime(model.InvoiceDate);
                model.SalesReturnMaster.InvoiceMiti = NepaliDateService.NepaliDate(model.SalesReturnMaster.InvoiceDate).Date;
            }
            else
            {
                model.SalesReturnMaster.InvoiceMiti = model.InvoiceDate;
                model.SalesReturnMaster.InvoiceDate = NepaliDateService.NepalitoEnglishDate(model.InvoiceDate);
            }
            if (!string.IsNullOrEmpty(model.RefDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesReturnMaster.RefBillDate = model.RefDate.ToDate();
                    model.SalesReturnMaster.RefBillMiti = NepaliDateService.NepaliDate(model.RefDate.ToDate()).Date;
                }
                else
                {
                    model.SalesReturnMaster.RefBillMiti = model.RefDate;
                    model.SalesReturnMaster.RefBillDate = NepaliDateService.NepalitoEnglishDate(model.RefDate);
                }
            }
            if (model.SalesReturnMaster.CurrencyId != 0 && model.SalesReturnMaster.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(model.SalesReturnMaster.CurrencyId);
                // var Code = _currency.GetById(model.SalesReturnMaster.CurrencyId);
                var Code = _currency.GetById(currencyId);
                model.SalesReturnMaster.CurrCode = Convert.ToString(Code.Id);
            }

            _salesReturnMaster.Update(model.SalesReturnMaster);
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesReturnOtherDetail.CnDate = Convert.ToDateTime(model.CnDate);
                    model.SalesReturnOtherDetail.CnMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesReturnOtherDetail.CnDate)).Date;
                }
                else
                {
                    model.SalesReturnOtherDetail.CnMiti = model.CnDate;
                    model.SalesReturnOtherDetail.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }
            if (!string.IsNullOrEmpty(model.PODate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesReturnOtherDetail.PODate = Convert.ToDateTime(model.PODate);
                    model.SalesReturnOtherDetail.POMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesReturnOtherDetail.PODate)).Date;
                }
                else
                {
                    model.SalesReturnOtherDetail.POMiti = model.PODate;
                    model.SalesReturnOtherDetail.PODate = NepaliDateService.NepalitoEnglishDate(model.PODate);
                }
            }
            if (!string.IsNullOrEmpty(model.ContractDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesReturnOtherDetail.ContractDate = Convert.ToDateTime(model.ContractDate);
                    model.SalesReturnOtherDetail.ContractMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesReturnOtherDetail.ContractDate)).Date;
                }
                else
                {
                    model.SalesReturnOtherDetail.ContractMiti = model.ContractDate;
                    model.SalesReturnOtherDetail.ContractDate = NepaliDateService.NepalitoEnglishDate(model.ContractDate);
                }
            }
            if (!string.IsNullOrEmpty(model.ExportInvoiceDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesReturnOtherDetail.ExportInvoiceDate = Convert.ToDateTime(model.ExportInvoiceDate);
                    model.SalesReturnOtherDetail.ExportInvoiceMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesReturnOtherDetail.ExportInvoiceDate)).Date;
                }
                else
                {
                    model.SalesReturnOtherDetail.ExportInvoiceMiti = model.ExportInvoiceDate;
                    model.SalesReturnOtherDetail.ExportInvoiceDate = NepaliDateService.NepalitoEnglishDate(model.ExportInvoiceDate);
                }
            }
            if (model.SalesReturnMaster.CurrencyId != 0 && model.SalesReturnMaster.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(model.SalesReturnMaster.CurrencyId);
                model.SalesReturnMaster.CurrCode = _currency.GetById(currencyId).Code;
            }
            _salesReturnOtherDetailRepository.Update(model.SalesReturnOtherDetail);
            var Source = StringEnum.Parse(typeof(ModuleEnum), "Sales Return").ToString();

            _accountTransaction.Delete(x => x.ReferenceId == model.SalesReturnMaster.Id && x.Source == Source);
            _stockTransaction.Delete(x => x.ReferenceId == model.SalesReturnMaster.Id && x.Source == Source);
            _salesReturnDetail.Delete(x => x.SalesReturnId == model.SalesReturnMaster.Id);
            _billingTermDetail.Delete(x => x.ReferenceId == model.SalesReturnMaster.Id && x.Source == Source);
            // _accountTransaction.Delete(x => x.ReferenceId == model.SalesReturnMaster.Id && x.Source == Source);
            int sno = 1;
            decimal basicAmt = 0;
            var tempSalesReturnDetailList = new List<SalesReturnDetail>();
            foreach (var item in salesReturnDetailEntry)
            {
                if (item.ProductCode != null && item.ProductCode != 0 && item.BasicAmt != null && item.BasicAmt != 0 && item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    var batchSerialNo = string.Empty;
                    var unitcode = _unitRepository.GetById(item.UnitId).Code;
                    basicAmt += Convert.ToDecimal(item.BasicAmt);
                    var saleReturnDetail = new SalesReturnDetail();
                    saleReturnDetail.SNo = sno;
                    saleReturnDetail.ProductCode = item.ProductCode;
                    saleReturnDetail.AltQty = item.AltQty;
                    saleReturnDetail.Qty = Convert.ToDecimal(item.Qty);
                    saleReturnDetail.AltStockQty = item.AltQty;
                    saleReturnDetail.StockQty = Convert.ToDecimal(item.Qty);
                    saleReturnDetail.Rate = Convert.ToDecimal(item.Rate);
                    saleReturnDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    saleReturnDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    saleReturnDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    saleReturnDetail.Remarks = model.SalesReturnMaster.Remarks;
                    saleReturnDetail.SalesReturnId = model.SalesReturnMaster.Id;
                    saleReturnDetail.Index = item.Index;
                    saleReturnDetail.Unit = unitcode;
                    saleReturnDetail.AltUnit = item.AltUnit;
                    saleReturnDetail.Godown = item.Godown;
                    _salesReturnDetail.Add(saleReturnDetail);
                    tempSalesReturnDetailList.Add(saleReturnDetail);

                    var accountTransactionDetail = new AccountTransaction();
                    accountTransactionDetail.VNo = model.SalesReturnMaster.InvoiceNo;
                    accountTransactionDetail.VDate = model.SalesReturnMaster.InvoiceDate;
                    accountTransactionDetail.VMiti = model.SalesReturnMaster.InvoiceMiti;
                    accountTransactionDetail.CrRate = Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                    accountTransactionDetail.CrCode = model.SalesReturnMaster.CurrCode;
                    accountTransactionDetail.SlCode = model.SalesReturnMaster.SlCode;
                    //Sales Return A/C Id need to specify in setting
                    int salesReturnAc;
                    var product = _product.GetById(item.ProductCode);
                    salesReturnAc = product.SalesReturnId != null ? Convert.ToInt32(product.PurchaseId) : base.SystemControl.SalesReturnAc;
                    accountTransactionDetail.GlCode = salesReturnAc;
                    // accountTransactionDetail.AgentCode = UtilityService.GetAgentNameById(model.SalesReturnMaster.AgentCode);
                    accountTransactionDetail.DrAmt = Convert.ToDecimal(item.BasicAmt);
                    accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.BasicAmt);
                    if (model.SalesReturnMaster.CurrencyId != 0 && model.SalesReturnMaster.CurrencyId != null)
                    {
                        accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.BasicAmt) * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                    }
                    accountTransactionDetail.CrAmt = 0;
                    accountTransactionDetail.LocalCrAmt = 0;
                    accountTransactionDetail.Source = Source;
                    accountTransactionDetail.SNo = 0;
                    accountTransactionDetail.CreatedById = userId;
                    var isCashBankdetail = _ledger.GetById(salesReturnAc).IsCashBank;
                    if (isCashBankdetail)
                        accountTransactionDetail.CbCode = model.SalesReturnMaster.GlCode;
                    accountTransactionDetail.ReferenceId = model.SalesReturnMaster.Id;
                    accountTransactionDetail.FYId = FiscalYear.Id;
                    accountTransactionDetail.BranchId = CurrentBranchId;
                    accountTransactionDetail.Remarks = model.SalesReturnMaster.Remarks;
                    accountTransactionDetail.AgentCode = Convert.ToString(model.SalesReturnMaster.AgentCode);
                    _accountTransaction.Add(accountTransactionDetail);

                    if (ProductBatchList != null)
                    {
                        var batch = ProductBatchList.Where(x => x.ParentGuid == item.DetailGuid).FirstOrDefault();
                        if (batch != null)
                        {
                            batchSerialNo = batch.BatchSerialNo;
                            var productBatch = new PurchaseProductBatch();
                            productBatch.BranchId = base.CurrentBranch.Id;
                            productBatch.Godown = item.Godown;
                            productBatch.ProductId = item.ProductCode;
                            productBatch.DetailId = saleReturnDetail.Id;
                            productBatch.Qty = Convert.ToDecimal(item.Qty);
                            productBatch.SerialNo = batch.BatchSerialNo;
                            if (batch.IsMfgDate)
                                productBatch.MFGDate = batch.MfgDate;
                            if (batch.IsExpDate)
                                productBatch.EXPDate = batch.ExpDate;
                            productBatch.BuyRate = batch.BuyRate;
                            productBatch.SalesRate = batch.SalesRate;
                            productBatch.Unit = item.UnitId;
                            productBatch.StockQuantity = Convert.ToDecimal(item.Qty);
                            _purchaseProductBatch.Add(productBatch);
                        }
                    }

                    var stockTransaction = new StockTransaction();
                    stockTransaction.AgentCode = UtilityService.GetAgentNameById(model.SalesReturnMaster.AgentCode);
                    stockTransaction.AltQty = saleReturnDetail.AltQty;
                    stockTransaction.AltStockQty = saleReturnDetail.AltStockQty;
                    stockTransaction.AltUnit = saleReturnDetail.AltUnit;
                    stockTransaction.CurrCode = model.SalesReturnMaster.CurrCode;
                    stockTransaction.CurrRate = model.SalesReturnMaster.CurrRate;
                    stockTransaction.GlCode = model.SalesReturnMaster.GlCode;
                    stockTransaction.NetAmt = saleReturnDetail.NetAmt;
                    stockTransaction.ProductCode = saleReturnDetail.ProductCode;
                    stockTransaction.Quantity = saleReturnDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(saleReturnDetail.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = Source;
                    stockTransaction.Unit = unitcode;
                    stockTransaction.ReferenceId = model.SalesReturnMaster.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.BatchSerialNo = batchSerialNo;
                    stockTransaction.Godown = Convert.ToString(saleReturnDetail.Godown);
                    var stock = _stockTransaction.Filter(x => x.ProductCode == item.ProductCode).ToList().LastOrDefault();
                    if (stock == null)
                    {
                        stockTransaction.StockVal = Convert.ToDecimal(saleReturnDetail.BasicAmt); ;
                        stockTransaction.StockQty = saleReturnDetail.StockQty;
                    }
                    else
                    {
                        stockTransaction.StockVal = stock.StockVal + Convert.ToDecimal(saleReturnDetail.BasicAmt); ;
                        stockTransaction.StockQty = stock.StockQty + saleReturnDetail.StockQty;
                    }
                    stockTransaction.TermAmt = saleReturnDetail.TermAmt;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                    stockTransaction.VDate = model.SalesReturnMaster.InvoiceDate;
                    stockTransaction.VNo = model.SalesReturnMaster.InvoiceNo;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;

                    if (billTermList != null)
                    {
                        decimal netAmt = 0;
                        //var billWiseTerms =
                        //    billTermList.Where(x => !x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList();
                        foreach (var term in billTermList.Where(x => x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList())
                        {
                            var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                            decimal drAmt = 0;
                            decimal drLocalAmt = 0;
                            decimal crAmt = 0;
                            decimal crLocalAmt = 0;
                            decimal termAmt = 0;
                            if (term.Sign == "-")
                            {
                                crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                termAmt = System.Math.Abs(crAmt) * (-1);
                            }
                            else
                            {
                                drAmt = Convert.ToDecimal(term.Amount);
                                drLocalAmt = Convert.ToDecimal(term.Amount);
                                termAmt = drAmt;
                            }
                            var billingTermDetail = new BillingTermDetail();
                            billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                            var index = Convert.ToInt32(term.BillingTermIndex);
                            billingTermDetail.Index = index;
                            billingTermDetail.ReferenceId = model.SalesReturnMaster.Id;
                            billingTermDetail.Source = Source;
                            billingTermDetail.Rate = Convert.ToDecimal(term.Rate);
                            billingTermDetail.TermAmount = termAmt;
                            billingTermDetail.DetailId = saleReturnDetail.Id;
                            billingTermDetail.Type = "P";
                            billingTermDetail.IsProductWise = term.IsProductWise;
                            _billingTermDetail.Add(billingTermDetail);

                            //AccountTransaction Posting
                            var accountTransactionBillingTerm = new AccountTransaction();
                            accountTransactionBillingTerm.VNo = model.SalesReturnMaster.InvoiceNo;
                            accountTransactionBillingTerm.VDate = model.SalesReturnMaster.InvoiceDate;
                            accountTransactionBillingTerm.VMiti = model.SalesReturnMaster.InvoiceMiti;
                            accountTransactionBillingTerm.CrRate = Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                            accountTransactionBillingTerm.AgentCode = Convert.ToString(model.SalesReturnMaster.AgentCode);
                            accountTransactionBillingTerm.CrCode = model.SalesReturnMaster.CurrCode;
                            accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                            if (model.SalesReturnMaster.CurrencyId != null && model.SalesReturnMaster.CurrencyId != 0)
                            {
                                drLocalAmt = drLocalAmt * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                                crLocalAmt = crLocalAmt * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                            }
                            accountTransactionBillingTerm.DrAmt = drAmt;
                            accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                            accountTransactionBillingTerm.CrAmt = crAmt;
                            accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;

                            accountTransactionBillingTerm.Source = Source;
                            accountTransactionBillingTerm.SNo = 0;
                            accountTransactionBillingTerm.CreatedById = userId;
                            accountTransactionBillingTerm.CbCode = model.SalesReturnMaster.GlCode;
                            accountTransactionBillingTerm.FYId = FiscalYear.Id;
                            accountTransactionBillingTerm.ReferenceId = model.SalesReturnMaster.Id;
                            accountTransactionBillingTerm.BranchId = CurrentBranchId;
                            accountTransactionBillingTerm.Remarks = model.SalesReturnMaster.Remarks;
                            _accountTransaction.Add(accountTransactionBillingTerm);
                        }
                    }
                }
            }
            if (billTermList != null)
            {
                foreach (var term in billTermList.Where(x => !x.IsProductWise).ToList())
                {
                    var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                    decimal drAmt = 0;
                    decimal drLocalAmt = 0;
                    decimal crAmt = 0;
                    decimal crLocalAmt = 0;
                    decimal termAmt = 0;
                    if (term.Sign == "-")
                    {
                        crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        termAmt = System.Math.Abs(crAmt) * (-1);
                    }
                    else
                    {
                        drAmt = Convert.ToDecimal(term.Amount);
                        drLocalAmt = Convert.ToDecimal(term.Amount);
                        termAmt = drAmt;
                    }
                    var billingTermDetail = new BillingTermDetail();
                    billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    billingTermDetail.Index = index;
                    billingTermDetail.ReferenceId = model.SalesReturnMaster.Id;
                    billingTermDetail.Source = Source;
                    billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                    billingTermDetail.TermAmount = termAmt;
                    billingTermDetail.Type = "B";
                    billingTermDetail.IsProductWise = term.IsProductWise;
                    _billingTermDetail.Add(billingTermDetail);

                    //AccountTransaction Posting
                    var accountTransactionBillingTerm = new AccountTransaction();
                    accountTransactionBillingTerm.VNo = model.SalesReturnMaster.InvoiceNo;
                    accountTransactionBillingTerm.VDate = model.SalesReturnMaster.InvoiceDate;
                    accountTransactionBillingTerm.VMiti = model.SalesReturnMaster.InvoiceMiti;
                    accountTransactionBillingTerm.CrRate = Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                    accountTransactionBillingTerm.AgentCode = Convert.ToString(model.SalesReturnMaster.AgentCode);
                    accountTransactionBillingTerm.CrCode = model.SalesReturnMaster.CurrCode;
                    accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                    accountTransactionBillingTerm.SlCode = model.SalesReturnMaster.SlCode;
                    if (model.SalesReturnMaster.CurrencyId != null && model.SalesReturnMaster.CurrencyId != 0)
                    {
                        drLocalAmt = drLocalAmt * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                        crLocalAmt = crLocalAmt * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
                    }
                    accountTransactionBillingTerm.DrAmt = drAmt;
                    accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                    accountTransactionBillingTerm.CrAmt = crAmt;
                    accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                    accountTransactionBillingTerm.Source = Source;
                    accountTransactionBillingTerm.SNo = 0;
                    accountTransactionBillingTerm.CreatedById = userId;
                    var isCashBankbill = _ledger.GetById(billingTerm.GeneralLedgerId).IsCashBank;
                    if (isCashBankbill)
                        accountTransactionBillingTerm.CbCode = billingTerm.GeneralLedgerId;
                    accountTransactionBillingTerm.FYId = FiscalYear.Id;
                    accountTransactionBillingTerm.ReferenceId = model.SalesReturnMaster.Id;
                    accountTransactionBillingTerm.BranchId = CurrentBranchId;
                    accountTransactionBillingTerm.Remarks = model.SalesReturnMaster.Remarks;
                    _accountTransaction.Add(accountTransactionBillingTerm);
                }
            }
            var accountTransactionMaster = new AccountTransaction();
            accountTransactionMaster.VNo = model.SalesReturnMaster.InvoiceNo;
            accountTransactionMaster.VDate = model.SalesReturnMaster.InvoiceDate;
            accountTransactionMaster.VMiti = model.SalesReturnMaster.InvoiceMiti;
            accountTransactionMaster.AgentCode = Convert.ToString(model.SalesReturnMaster.AgentCode);
            accountTransactionMaster.CrRate = Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
            accountTransactionMaster.CrCode = model.SalesReturnMaster.CurrCode;
            accountTransactionMaster.GlCode = model.SalesReturnMaster.GlCode;
            accountTransactionMaster.SlCode = model.SalesReturnMaster.SlCode;
            accountTransactionMaster.DrAmt = 0;
            accountTransactionMaster.LocalDrAmt = 0;
            accountTransactionMaster.CrAmt = Convert.ToDecimal(model.SalesReturnMaster.NetAmt);
            accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(model.SalesReturnMaster.NetAmt);
            if (model.SalesReturnMaster.CurrencyId != 0 && model.SalesReturnMaster.CurrencyId != null)
            {
                accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(model.SalesReturnMaster.NetAmt) * Convert.ToDecimal(model.SalesReturnMaster.CurrRate);
            }
            accountTransactionMaster.Source = Source;
            accountTransactionMaster.SNo = 0;
            accountTransactionMaster.CreatedById = userId;
            var isCashBank = _ledger.GetById(model.SalesReturnMaster.GlCode).IsCashBank;
            if (isCashBank)
                accountTransactionMaster.CbCode = model.SalesReturnMaster.GlCode;
            accountTransactionMaster.ReferenceId = model.SalesReturnMaster.Id;
            accountTransactionMaster.FYId = FiscalYear.Id;
            accountTransactionMaster.BranchId = CurrentBranchId;
            accountTransactionMaster.Remarks = model.SalesReturnMaster.Remarks;
            _accountTransaction.Add(accountTransactionMaster);
            model.SalesReturnOtherDetail.SalesReturnId = model.SalesReturnMaster.Id;
            _salesReturnOtherDetailRepository.Add(model.SalesReturnOtherDetail);

            _udfData.Delete(x => x.ReferenceId == model.SalesReturnMaster.Id && x.SourceId == (int)EntryModuleEnum.SalesInvoiceReturn);
            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.SalesInvoiceReturn);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = model.SalesReturnMaster.Id,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.SalesInvoiceReturn
                    };
                    _udfData.Add(i);
                }
            }
            return Json(new { success = true, isEdit = true });
        }

        [CheckPermission(Permissions = "Approve", Module = "SR")]
        [HttpPost]
        public ActionResult SalesReturnApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _salesReturnMaster.GetById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _salesReturnMaster.Update(data);
            return null;
        }

        [CheckPermission(Permissions = "Create", Module = "SR")]
        public ActionResult GetSalesReturnDetailEntry()
        {
            var viewModel = new SalesReturnDetailEntryViewModel();
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            viewModel.EntryControl = _entryControlSales.GetAll().FirstOrDefault();
            viewModel.DetailGuid = Guid.NewGuid().ToString();
            return PartialView("_PartialSalesReturnDetailEntry", viewModel);
        }

        [CheckPermission(Permissions = "Edit", Module = "SR")]
        public ActionResult GetSalesReturnDetailEdit(int? index)
        {
            var data = new SalesReturnDetail();
            var viewModel = new PurchaseReturnDetailEntryViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            if (index != 0)
                viewModel.Index = Convert.ToInt32(index);
            if (index != null)
                data.Index = Convert.ToInt32(index);
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            viewModel.DetailGuid = Guid.NewGuid().ToString();
            return PartialView("_PartialSalesReturnDetailEdit", data);
        }

        #endregion

        #region Sales Order

        public JsonResult CheckOrderNoInSalesOrderMaster(string OrderNo, int? Id)
        {
            var feeterm = new SalesOrderMaster();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _salesOrderMaster.GetById(x => x.Id == Id);
                if (data.OrderNo.ToLower().Trim() != OrderNo.ToLower().Trim())
                {
                    feeterm =
                        _salesOrderMaster.GetById(x => x.OrderNo.ToLower().Trim() == OrderNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _salesOrderMaster.GetById(x => x.OrderNo.ToLower().Trim() == OrderNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new SalesOrderMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        //[CheckPermission(Permissions = "Navigate", Module = "SB")]
        //public ActionResult SalesOrder()
        //{
        //    return View();
        //}

        [CheckPermission(Permissions = "Navigate", Module = "SB")]
        public ActionResult SalesOrderList()
        {
            var viewModel = new SalesOrderViewModel();
            //viewModel.AcceptedList = _salesService.GetAcceptedSalesOrder(1, base.SystemControl.PageSize, CurrentBranchId);
            viewModel.PendingList = _salesService.GetPendingSalesOrder(1, base.SystemControl.PageSize, CurrentBranchId);
            return PartialView(viewModel);
        }

        public ActionResult SalesOrderPendingList(int pageNo = 1)
        {
            var list = _salesService.GetPendingSalesOrder(pageNo, base.SystemControl.PageSize, CurrentBranchId);
            return PartialView(list);
        }

        //public ActionResult SalesOrderAcceptedList(int pageNo = 1)
        //{
        //    var list = _salesService.GetAcceptedSalesOrder(pageNo, base.SystemControl.PageSize, CurrentBranchId);
        //    return PartialView(list);
        //}
        [HttpPost]
        public ActionResult SalesOrderDelete(int id)
        {
            _salesService.DeleteSalesOrder(id);
            return Json(true);
        }

        public ActionResult CheckFiscalyearDateinSalesOrder(string OrderDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(OrderDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(OrderDate);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");

                    }
                    else
                    {
                        return
                             Content(
                                 "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                 fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }

        [CheckPermission(Permissions = "Create", Module = "SB")]
        public ActionResult SalesOrderAdd()
        {
            // var data = _salesInvoice.GetAll().LastOrDefault();
            var viewModel = base.CreateViewModel<SalesOrderAddViewModel>();
            viewModel.CnTypeList = new SelectList(
                Enum.GetValues(typeof(CnTypeEnum)).Cast
                    <CnTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");
            // viewModel.Date = data != null ? data.InvoiceDate : DateTime.UtcNow;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.SalesOrder = new SalesOrderMaster();
            viewModel.Unitlist = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.OrderDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _salesOrderMaster.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewModel.OrderDate = base.SystemControl.DateType == 1
                                              ? data.OrderDate.ToShortDateString()
                                              : data.OrderMiti;
                }
                else
                {
                    viewModel.OrderDate = base.SystemControl.DateType == 1
                                              ? DateTime.Now.ToShortDateString()
                                              : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }
            }
            return PartialView(viewModel);

        }

        [HttpPost]
        public ActionResult SalesOrderAdd(IEnumerable<SalesDetailEntryViewModel> salesDetailEntry, SalesOrderAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var source = StringEnum.Parse(typeof(ModuleEnum), "Sales Order").ToString();
            if (base.SystemControl.DateType == 1)
            {
                model.SalesOrder.OrderDate = model.OrderDate.ToDate();
                model.SalesOrder.OrderMiti = NepaliDateService.NepaliDate(model.SalesOrder.OrderDate).Date;
            }
            else
            {
                model.SalesOrder.OrderMiti = Convert.ToString(model.OrderDate);
                model.SalesOrder.OrderDate = NepaliDateService.NepalitoEnglishDate(model.OrderDate);
            }
            var order = _salesService.SaveSalesOrder(model.SalesOrder, model.SalesOrder.OrderDate, FiscalYear.Id, CurrentBranchId, source, userId);
            if (model.SalesOrder.DocId != null)
            {
                var docId = Convert.ToInt32(model.SalesOrder.DocId);
                var documentnumber = _docNumering.GetById(docId);
                documentnumber.DocCurrNo += 1;
                _docNumering.Update(documentnumber);
            }
            var tempSalesOrderDetailList = _salesService.SaveSalesOrderDetail(salesDetailEntry, order, source, userId, FiscalYear.Id, CurrentBranchId);
            if (billTermList != null)
            {
                _salesService.SaveSalesOrderBillingTerm(billTermList, order, source, tempSalesOrderDetailList, userId, FiscalYear.Id, CurrentBranchId);
            }
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesOrderOtherDetail.CnDate = Convert.ToDateTime(model.CnDate);
                    model.SalesOrderOtherDetail.CnMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesOrderOtherDetail.CnDate)).Date;
                }
                else
                {
                    model.SalesOrderOtherDetail.CnMiti = model.CnDate;
                    model.SalesOrderOtherDetail.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }
            if (!string.IsNullOrEmpty(model.PODate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesOrderOtherDetail.PODate = Convert.ToDateTime(model.PODate);
                    model.SalesOrderOtherDetail.POMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesOrderOtherDetail.PODate)).Date;
                }
                else
                {
                    model.SalesOrderOtherDetail.POMiti = model.PODate;
                    model.SalesOrderOtherDetail.PODate = NepaliDateService.NepalitoEnglishDate(model.PODate);
                }
            }
            if (!string.IsNullOrEmpty(model.ContractDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesOrderOtherDetail.ContractDate = Convert.ToDateTime(model.ContractDate);
                    model.SalesOrderOtherDetail.ContractMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesOrderOtherDetail.ContractDate)).Date;
                }
                else
                {
                    model.SalesOrderOtherDetail.ContractMiti = model.ContractDate;
                    model.SalesOrderOtherDetail.ContractDate = NepaliDateService.NepalitoEnglishDate(model.ContractDate);
                }
            }


            _salesService.SaveSalesOrderOtherDetail(model.SalesOrderOtherDetail, order.Id);
            return Json(new { success = true, isEdit = false });
        }

        public ActionResult SalesOrderEdit(int id)
        {
            var data = _salesService.GetSalesOrder(id);
            var otherdetail = _salesService.GetSalesOrderOtherDetail(data.Id);
            var viewModel = base.CreateViewModel<SalesOrderAddViewModel>();
            viewModel.CnTypeList = new SelectList(
                Enum.GetValues(typeof(CnTypeEnum)).Cast
                    <CnTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");
            viewModel.SalesOrder = data;
            viewModel.SalesOrderOtherDetail = otherdetail;
            ViewBag.Id = data.Id;
            ViewBag.AllowProductWiseBillTerm = 0;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                ViewBag.AllowProductWiseBillTerm = 1;
            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id && x.Source == "SB").ToList();
            //var defaultIndex = billTerm.Max(x => x.Index);

            if (base.SystemControl.DateType == 1)
            {
                viewModel.OrderDate = data.OrderDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.OrderDate = data.OrderMiti;
            }
            if (otherdetail.CnDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.CnDate = Convert.ToDateTime(otherdetail.CnDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.CnDate = otherdetail.CnMiti;
                }
            }

            if (otherdetail.PODate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.PODate = Convert.ToDateTime(otherdetail.PODate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.PODate = otherdetail.POMiti;
                }
            }
            if (otherdetail.ContractDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.ContractDate = Convert.ToDateTime(otherdetail.ContractDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.ContractDate = otherdetail.ContractMiti;
                }
            }

            ViewBag.Index = 1;
            foreach (var term in viewModel.SalesOrder.SalesOrderDetails)
            {
                term.UnitId = _unitRepository.GetById(x => x.Code.ToLower() == term.Unit.ToLower()).Id;
            }

            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }

            ViewBag.BillingTermList = billTerm;
            viewModel.TotalAmtRs = viewModel.SalesOrder.NetAmt.ToString();
            viewModel.Unitlist = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SalesOrderEdit(IEnumerable<SalesOrderDetail> salesDetailEntry, SalesOrderAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var source = StringEnum.Parse(typeof(ModuleEnum), "Sales Order").ToString();
            _salesService.DeleteSalesOrderRelatedDatas(model.SalesOrder.Id, source);
            if (base.SystemControl.DateType == 1)
            {
                model.SalesOrder.OrderDate = model.OrderDate.ToDate();
                model.SalesOrder.OrderMiti = NepaliDateService.NepaliDate(model.SalesOrder.OrderDate).Date;
            }
            else
            {
                model.SalesOrder.OrderMiti = Convert.ToString(model.OrderDate);
                model.SalesOrder.OrderDate = NepaliDateService.NepalitoEnglishDate(model.OrderDate);
            }
            var salesOrder = _salesService.UpdateSalesOrder(model.SalesOrder, model.SalesOrder.OrderDate, FiscalYear.Id,
                                                            CurrentBranchId, source, userId);


            //if (billTermList != null)
            //{
            //    _salesService.SaveSalesOrderBillingTerm(billTermList, order, source, tempSalesOrderDetailList, userId, FiscalYear.Id, CurrentBranchId);
            //}
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesOrderOtherDetail.CnDate = Convert.ToDateTime(model.CnDate);
                    model.SalesOrderOtherDetail.CnMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesOrderOtherDetail.CnDate)).Date;
                }
                else
                {
                    model.SalesOrderOtherDetail.CnMiti = model.CnDate;
                    model.SalesOrderOtherDetail.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }
            if (!string.IsNullOrEmpty(model.PODate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesOrderOtherDetail.PODate = Convert.ToDateTime(model.PODate);
                    model.SalesOrderOtherDetail.POMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesOrderOtherDetail.PODate)).Date;
                }
                else
                {
                    model.SalesOrderOtherDetail.POMiti = model.PODate;
                    model.SalesOrderOtherDetail.PODate = NepaliDateService.NepalitoEnglishDate(model.PODate);
                }
            }
            if (!string.IsNullOrEmpty(model.ContractDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesOrderOtherDetail.ContractDate = Convert.ToDateTime(model.ContractDate);
                    model.SalesOrderOtherDetail.ContractMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.SalesOrderOtherDetail.ContractDate)).Date;
                }
                else
                {
                    model.SalesOrderOtherDetail.ContractMiti = model.ContractDate;
                    model.SalesOrderOtherDetail.ContractDate = NepaliDateService.NepalitoEnglishDate(model.ContractDate);
                }
            }
            _salesService.SaveSalesOrderOtherDetail(model.SalesOrderOtherDetail, salesOrder.Id);
            var tempSalesOrderDetailList = _salesService.SaveSalesOrderDetail(salesDetailEntry, salesOrder, source,
                                               userId, FiscalYear.Id, CurrentBranchId);
            if (billTermList != null)
            {
                _salesService.SaveSalesOrderBillingTerm(billTermList, salesOrder, source, tempSalesOrderDetailList, userId, FiscalYear.Id, CurrentBranchId);
            }
            return Json(new { success = true, isEdit = true });
        }

        [CheckPermission(Permissions = "Approve", Module = "SB")]
        [HttpPost]
        public ActionResult SalesOrderApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _salesService.GetSalesOrder(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _salesService.ApproveSalesOrder(id);
            return null;
        }
        #endregion

        #region Sales Challan
        public JsonResult CheckChallanNoInSalesInvoice(string ChallanNo, int? Id)
        {
            var challan = _salesService.CheckChallanNo(ChallanNo, Id);
            return challan.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateSalesChallanDetailByOrderId(int id)
        {
            var order = _salesService.GetSalesOrder(id, "SalesOrderDetails");
            var vendor = _ledger.GetById(x => x.Id == order.GlCode);
            var agent = string.Empty;
            if (order.AgentCode != null)
            {
                var agentId = Convert.ToInt32(order.AgentCode);
                var agentData = _agent.GetById(agentId);
                if (agentData != null)
                    agent = agentData.Name;
            }
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var challanDetailView = string.Empty;
            var billWiseBillingTerm = string.Empty;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "S" && x.IsProductWise && x.Category == cat);

            foreach (var item in order.SalesOrderDetails)
            {
                var singleChallanDetail = new SalesDetailEntryViewModel();
                var productId = Convert.ToInt32(item.ProductCode);
                var product = _product.GetById(productId);
                singleChallanDetail.ProductName = product.Name;
                singleChallanDetail.ProductId = productId;
                if (!string.IsNullOrEmpty(item.Unit))
                {
                    var unit = _unitRepository.GetById(x => x.Code == item.Unit);
                    singleChallanDetail.UnitId = unit.Id;
                }
                singleChallanDetail.Qty = item.Qty;
                singleChallanDetail.UnitList = unitSelectList;
                singleChallanDetail.Rate = item.Rate;
                singleChallanDetail.BasicAmt = item.BasicAmt;
                singleChallanDetail.TermAmt = item.TermAmt;
                singleChallanDetail.NetAmt = item.NetAmt;
                if (billingTerm.Count() != 0)
                    singleChallanDetail.AllowProductWiseBillTerm = true;
                challanDetailView += base.RenderPartialViewToString("_PartialSalesDetailEntry", singleChallanDetail);
            }

            var billingTermDetail = _billingTermDetail.GetMany(x => x.Source == "PO" && x.ReferenceId == order.Id);

            foreach (var item in billingTermDetail)
            {
                var billing = _billingTerm.GetById(x => x.Id == item.BillingTermId);
                var viewModel = new BillingTermDetailViewModel();
                viewModel.Amount = item.TermAmount;
                viewModel.Description = billing.Description;
                viewModel.Id = item.Id;
                viewModel.Rate = item.Rate;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                viewModel.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), billing.Sign));
                viewModel.BillingTermIndex = item.Index;
                viewModel.IsProductWise = item.IsProductWise;

                billWiseBillingTerm += base.RenderPartialViewToString("_PartialHdnBillingTerm", viewModel);
            }
            var CurrentBalance = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=0, @glCode=" + order.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            var TotalOutstanding = _context.Database.SqlQuery<string>("SP_GetCurrentBalance @outputType=1, @glCode=" + order.GlCode + ",@FYId=" + base.FiscalYear.Id).FirstOrDefault();
            return Json(new
            {
                VendorName = vendor.AccountName,
                VendorId = order.GlCode,
                AgentName = agent,
                AgentId = order.AgentCode,
                challanDetailView = challanDetailView,
                billWiseBillingTerm = billWiseBillingTerm,
                BasicAmt = order.BasicAmt,
                NetAmt = order.NetAmt,
                TermAmt = order.TermAmt,
                TotalAmtRs = order.BasicAmt + order.TermAmt,
                CurrentBalance = CurrentBalance,
                TotalOutstanding = TotalOutstanding
            });
        }

        //[CheckPermission(Permissions = "Navigate", Module = "PB")]
        //public ActionResult SalesChallan()
        //{
        //    return View();
        //}

        [CheckPermission(Permissions = "Navigate", Module = "PB")]
        public ActionResult SalesChallanList()
        {
            var viewModel = new SalesChallanViewModel();
            //viewModel.AcceptedList = _salesService.GetAcceptedSalesChallan(1, base.SystemControl.PageSize, CurrentBranchId);
            viewModel.PendingList = _salesService.GetPendingSalesChallan(1, base.SystemControl.PageSize, CurrentBranchId);
            return PartialView(viewModel);
        }

        public JsonResult GetSalesOrderForPickList()
        {
            var data = _salesService.GetSalesOrderList(CurrentBranchId).Select(x => new
                 {
                     Id = x.Id,
                     OrderNo = x.OrderNo,
                     Date = x.OrderDate.ToShortDateString(),
                     Vendor = x.Ledger.AccountName
                 });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalesChallanPendingList(int pageNo = 1)
        {
            var list = _salesService.GetPendingSalesChallan(pageNo, base.SystemControl.PageSize, CurrentBranchId);
            return PartialView(list);
        }

        //public ActionResult SalesChallanAcceptedList(int pageNo = 1)
        //{
        //    var list = _salesService.GetAcceptedSalesChallan(pageNo, base.SystemControl.PageSize, CurrentBranchId);
        //    return PartialView(list);
        //}

        public ActionResult SalesChallanDelete(int id)
        {
            _salesService.DeleteSalesChallan(id);
            return Json(true);
        }

        public JsonResult GetSalesChallanForPickList()
        {

            var data = _salesService.GetSalesChallanList(CurrentBranchId).Select(x => new
            {
                Id = x.Id,
                ChallanNo = x.ChallanNo,
                Date = x.ChallanDate.ToShortDateString(),
                Vendor = x.Ledger.AccountName
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckFiscalyearDateinSalesChallan(string ChallanDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(ChallanDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(ChallanDate);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }
        [CheckPermission(Permissions = "Create", Module = "PB")]
        public ActionResult SalesChallanAdd()
        {
            var viewModel = base.CreateModel<SalesChallanAddViewModel>();
            viewModel = _salesService.PopulateSalesChallanAddViewModel(viewModel, base.SystemControl.IsCurrDate,
                                                                       base.SystemControl.DateType);
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.ChallanDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SalesChallanAdd(IEnumerable<SalesDetailEntryViewModel> salesDetailEntry, SalesChallanAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            if (model.SalesChallan.GlCode != 0)
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                var source = StringEnum.Parse(typeof(ModuleEnum), "Sales Challan").ToString();
                model.SalesChallan.OrderId = model.SalesOrderId;
                model.SalesChallan.OrderNo = model.OrderNo;
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesChallan.ChallanDate = model.ChallanDate.ToDate();
                    model.SalesChallan.ChallanMiti = NepaliDateService.NepaliDate(model.SalesChallan.ChallanDate).Date;
                }
                else
                {
                    model.SalesChallan.ChallanMiti = model.ChallanDate;
                    model.SalesChallan.ChallanDate = NepaliDateService.NepalitoEnglishDate(model.ChallanDate);
                }
                var salesChallan = _salesService.SaveSalesChallan(model.SalesChallan, model.ChallanDate, FiscalYear.Id, CurrentBranchId,
                                               source, userId, base.SystemControl.DateType);

                if (model.SalesChallan.DocId != null)
                {
                    var docId = Convert.ToInt32(model.SalesChallan.DocId);
                    var documentnumber = _docNumering.GetById(docId);
                    documentnumber.DocCurrNo += 1;
                    _docNumering.Update(documentnumber);
                }
                if (!string.IsNullOrEmpty(model.CnDate))
                {
                    if (base.SystemControl.DateType == 1)
                    {
                        model.SalesChallanImpTransDoc.CnDate = model.CnDate.ToDate();
                        model.SalesChallanImpTransDoc.CnMiti = NepaliDateService.NepaliDate(model.SalesChallanImpTransDoc.CnDate).Date;
                    }
                    else
                    {
                        model.SalesChallanImpTransDoc.CnMiti = model.CnDate;
                        model.SalesChallanImpTransDoc.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                    }
                }
                if (!string.IsNullOrEmpty(model.PPDDate))
                {
                    if (base.SystemControl.DateType == 1)
                    {
                        model.SalesChallanImpTransDoc.PPDDate = model.PPDDate.ToDate();
                        model.SalesChallanImpTransDoc.PPDMiti = NepaliDateService.NepaliDate(model.SalesChallanImpTransDoc.PPDDate).Date;
                    }
                    else
                    {
                        model.SalesChallanImpTransDoc.PPDMiti = model.PPDDate;
                        model.SalesChallanImpTransDoc.PPDDate = NepaliDateService.NepalitoEnglishDate(model.PPDDate);
                    }
                }

                var tempSalesChallanDetailList = _salesService.SaveSalesChallanDetail(salesDetailEntry, salesChallan, source, userId, FiscalYear.Id, CurrentBranchId);
                if (!string.IsNullOrEmpty(model.PPDDate))
                    _salesService.SaveSalesChallanImpTransDoc(model.SalesChallanImpTransDoc, salesChallan.Id, base.SystemControl.DateType, model.PPDDate, model.CnDate);
                if (billTermList != null)
                {
                    _salesService.SaveSalesChallanBillingTerm(billTermList, salesChallan, source, tempSalesChallanDetailList, userId, FiscalYear.Id, CurrentBranchId);
                }
                return Json(new { success = true, isEdit = false });
            }
            return Json(new { success = false, isEdit = false });
        }

        [CheckPermission(Permissions = "Edit", Module = "SC")]
        public ActionResult SalesChallanEdit(int id)
        {
            var viewModel = base.CreateModel<SalesChallanAddViewModel>();
            var allowProductWiseBillTerm = 0;
            var billTerm = new List<BillingTermDetail>();
            viewModel = _salesService.PopulateSalesChallanEditViewModel(viewModel, id, ref allowProductWiseBillTerm,
                                                                        base.SystemControl.DateType, ref billTerm);
            ViewBag.Index = 1;
            ViewBag.BillingTermList = billTerm;
            ViewBag.Id = id;
            ViewBag.AllowProductWiseBillTerm = allowProductWiseBillTerm;

            if (base.SystemControl.DateType == 1)
            {
                viewModel.ChallanDate = viewModel.SalesChallan.ChallanDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.ChallanDate = viewModel.SalesChallan.ChallanMiti;
            }
            if (viewModel.SalesChallanImpTransDoc.CnDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.CnDate = viewModel.SalesChallanImpTransDoc.CnDate.ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.CnDate = viewModel.SalesChallanImpTransDoc.CnMiti;
                }
            }
            if (viewModel.SalesChallanImpTransDoc.PPDDate != null)
            {
                if (base.SystemControl.DateType == 1)
                {
                    viewModel.PPDDate = viewModel.SalesChallanImpTransDoc.PPDDate.ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.PPDDate = viewModel.SalesChallanImpTransDoc.PPDMiti;
                }
            }

            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SalesChallanEdit(IEnumerable<SalesChallanDetail> salesDetailEntry, SalesChallanAddViewModel model, FormCollection formCollection, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.SalesChallan.OrderId = model.SalesOrderId;
            model.SalesChallan.OrderNo = model.OrderNo;
            var source = StringEnum.Parse(typeof(ModuleEnum), "Sales Challan").ToString();
            _salesService.DeleteSalesChallanRelatedDatas(model.SalesChallan.Id, source);
            if (base.SystemControl.DateType == 1)
            {
                model.SalesChallan.ChallanDate = model.ChallanDate.ToDate();
                model.SalesChallan.ChallanMiti = NepaliDateService.NepaliDate(model.SalesChallan.ChallanDate).Date;
            }
            else
            {
                model.SalesChallan.ChallanMiti = model.ChallanDate;
                model.SalesChallan.ChallanDate = NepaliDateService.NepalitoEnglishDate(model.ChallanDate);
            }
            var salesChallan = _salesService.UpdateSalesChallan(model.SalesChallan, model.ChallanDate, FiscalYear.Id, CurrentBranchId,
                                             source, userId, base.SystemControl.DateType);
            if (!string.IsNullOrEmpty(model.CnDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesChallanImpTransDoc.CnDate = model.CnDate.ToDate();
                    model.SalesChallanImpTransDoc.CnMiti = NepaliDateService.NepaliDate(model.SalesChallanImpTransDoc.CnDate).Date;
                }
                else
                {
                    model.SalesChallanImpTransDoc.CnMiti = model.CnDate;
                    model.SalesChallanImpTransDoc.CnDate = NepaliDateService.NepalitoEnglishDate(model.CnDate);
                }
            }
            if (!string.IsNullOrEmpty(model.PPDDate))
            {
                if (base.SystemControl.DateType == 1)
                {
                    model.SalesChallanImpTransDoc.PPDDate = model.PPDDate.ToDate();
                    model.SalesChallanImpTransDoc.PPDMiti = NepaliDateService.NepaliDate(model.SalesChallanImpTransDoc.PPDDate).Date;
                }
                else
                {
                    model.SalesChallanImpTransDoc.PPDMiti = model.PPDDate;
                    model.SalesChallanImpTransDoc.PPDDate = NepaliDateService.NepalitoEnglishDate(model.PPDDate);
                }
            }
            var tempSalesChallanDetailList = _salesService.SaveSalesChallanDetail(salesDetailEntry, salesChallan, source, userId, FiscalYear.Id, CurrentBranchId);
            if (!string.IsNullOrEmpty(model.PPDDate))
                _salesService.SaveSalesChallanImpTransDoc(model.SalesChallanImpTransDoc, salesChallan.Id, base.SystemControl.DateType, model.PPDDate, model.CnDate);
            if (billTermList != null)
            {
                _salesService.SaveSalesChallanBillingTerm(billTermList, salesChallan, source, tempSalesChallanDetailList, userId, FiscalYear.Id, CurrentBranchId);
            }
            return Json(new { success = true, isEdit = true });
        }

        [CheckPermission(Permissions = "Approve", Module = "SC")]
        [HttpPost]
        public ActionResult SalesChallanApproved(int id)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _salesService.GetSalesChallanById(id);
            data.ApprovedBy = user.Id;
            data.IsApproved = true;
            data.ApprovedDate = DateTime.UtcNow;
            _salesService.ApproveSalesChallan(id);
            return null;
        }

        [CheckPermission(Permissions = "Create", Module = "SC")]
        public ActionResult GetSalesChallanDetailEntry()
        {
            var viewModel = new SalesDetailEntryViewModel();
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            return PartialView("_PartialSalesDetailEntry", viewModel);
        }

        [CheckPermission(Permissions = "Edit", Module = "SC")]
        public ActionResult GetSalesChallanDetailEdit(int? id, int? index)
        {
            var data = new SalesChallanDetail();
            var billTerm = _billingTermDetail.Filter(x => x.ReferenceId == id).ToList();
            var defaultIndex = billTerm.Max(x => x.Index);
            ViewBag.Index = defaultIndex == null ? 0 : defaultIndex + 1;
            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }

            ViewBag.BillingTermList = billTerm;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                ViewBag.AllowProductWiseBillTerm = 1;
            else
            {
                ViewBag.AllowProductWiseBillTerm = 0;
            }
            data.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            return PartialView("_PartialSalesChallanDetailEdit", data);
        }

        public ActionResult SalesChallanPrint(int invoiceId)
        {
            var model = _purchaseInvoice.GetById(invoiceId);
            var billDate = Convert.ToDateTime(model.InvoiceDate).ToString("MM/dd/yyyy");
            var invoice = new PurchaseInvoicePrintViewModel();
            invoice.InvoiceHeader = new InvoiceHeader();
            var logo = "NoImage.jpg";
            if (!string.IsNullOrEmpty(base.CompanyInfo.LogoGuid))
                logo = base.CompanyInfo.LogoGuid + base.CompanyInfo.LogoExt;
            var filePath = Server.MapPath("~/Content/Logo/" + logo);
            if (!System.IO.File.Exists(filePath))
            {
                logo = "NoImage.jpg";
            }
            invoice.InvoiceHeader.CompanyLogoUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "/Content/Logo/" + logo;
            invoice.InvoiceHeader.InvoiceDate = billDate;
            invoice.InvoiceHeader.InvoiceNo = model.InvoiceNo;
            invoice.InvoiceHeader.CompanyAddress = base.CompanyInfo.Address;
            invoice.InvoiceHeader.CompanyName = base.CompanyInfo.Name;
            invoice.InvoiceHeader.PartyName = model.Ledger.AccountName;
            invoice.BilledBy = model.User.FullName;
            int sno = 1;
            double subTotal = 0;
            invoice.InvoiceDetails = new List<InvoiceDetail>();
            foreach (var item in model.PurchaseDetails)
            {
                if (item.BasicAmt != 0)
                {
                    var product = _product.GetById(item.PCode);
                    var invoiceDetail = new InvoiceDetail();
                    invoiceDetail.Amount = Convert.ToDecimal(item.BasicAmt);
                    invoiceDetail.Particular = product.Name;
                    invoiceDetail.SNo = sno;
                    invoiceDetail.Qty = Convert.ToDecimal(item.Qty);
                    invoiceDetail.Rate = Convert.ToDecimal(item.Rate);
                    invoiceDetail.Remarks = item.Remarks;
                    sno++;
                    subTotal += Convert.ToDouble(item.BasicAmt);
                    invoice.InvoiceDetails.Add(invoiceDetail);
                }
            }

            var billingTerms = _billingTermDetail.GetMany(x => x.ReferenceId == invoiceId && x.Source == "PB").OrderBy(x => x.Index);
            invoice.InvoiceBillingTerms = new List<InvoiceBillingTerms>();
            double termAmt = 0;
            foreach (var item in billingTerms)
            {
                var invoiceBillingTerm = new InvoiceBillingTerms();
                var termName = _billingTerm.GetById(x => x.Id == item.BillingTermId && x.Type == "P").Description;
                invoiceBillingTerm.TermName = termName;
                invoiceBillingTerm.TermRate = item.Rate;
                invoiceBillingTerm.TermAmount = item.TermAmount;
                invoice.InvoiceBillingTerms.Add(invoiceBillingTerm);
                termAmt += Convert.ToDouble(item.TermAmount);
            }
            invoice.SubTotal = subTotal;
            invoice.Total = subTotal + termAmt;
            return this.ViewPdf("", "PurchaseInvoicePrint", invoice);
        }
        #endregion

        #endregion

        public ActionResult GetPurchaseBillingTerm(int? productwise)
        {
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var data = new List<BillingTerm>();
            if (productwise == null)
            {
                data = _billingTerm.Filter(x => x.Type == "P" && x.Category == cat && !x.IsProductWise && !x.IsDeleted).ToList();
            }
            else
            {
                data = _billingTerm.Filter(x => x.Type == "P" && x.Category == cat && x.IsProductWise && !x.IsDeleted).ToList();
            }
            List<BillingTermSelectViewModel> models = new List<BillingTermSelectViewModel>();
            foreach (var item in data.OrderBy(x => x.DispalyOrder))
            {

                var billingTerm = new BillingTermSelectViewModel();
                billingTerm.Id = item.Id;
                billingTerm.Basis = Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTerm.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTerm.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTerm.Rate = item.Rate;
                models.Add(billingTerm);
            }
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSalesBillingTerm(int? productwise)
        {
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var data = new List<BillingTerm>();
            if (productwise == null)
            {
                data = _billingTerm.Filter(x => x.Type == "S" && x.Category == cat && !x.IsProductWise && !x.IsDeleted).ToList();
            }
            else
            {
                data = _billingTerm.Filter(x => x.Type == "S" && x.Category == cat && x.IsProductWise && !x.IsDeleted).ToList();
            }
            List<BillingTermSelectViewModel> models = new List<BillingTermSelectViewModel>();
            foreach (var item in data.OrderBy(x => x.DispalyOrder))
            {
                var billingTerm = new BillingTermSelectViewModel();
                billingTerm.Id = item.Id;
                billingTerm.Basis = Enum.GetName(typeof(BillingTermBasisEnum), item.Basis).ToString();
                billingTerm.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTerm.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTerm.Rate = item.Rate;
                models.Add(billingTerm);
            }
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertBillTerm(string desc, string basis, int id, decimal amount, decimal rate, string sign, int? index, bool isProductWise)
        {
            var viewModel = new BillingTermDetailViewModel();
            viewModel.Amount = amount;
            viewModel.Basis = basis;
            viewModel.Description = desc;
            viewModel.Id = id;
            viewModel.Rate = rate;
            viewModel.Sign = sign;
            viewModel.BillingTermIndex = index;
            viewModel.IsProductWise = isProductWise;
            return PartialView("_PartialHdnBillingTerm", viewModel);
        }

        public ActionResult InsertBillTermEdit(BillingTermDetail model)
        {
            var billingTerm = _billingTerm.GetById(x => x.Id == model.BillingTermId);
            var viewModel = new BillingTermDetailViewModel();
            viewModel.Amount = Math.Abs(model.TermAmount);
            viewModel.Basis = Enum.GetName(typeof(BillingTermBasisEnum), billingTerm.Basis).ToString();
            viewModel.Description = billingTerm.Description;
            viewModel.Id = billingTerm.Id;
            viewModel.Rate = model.Rate;
            viewModel.IsProductWise = model.IsProductWise;
            StringEnum stringEnum = new StringEnum(typeof(SignEnum));
            viewModel.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), billingTerm.Sign));
            viewModel.BillingTermIndex = model.Index;
            viewModel.ParentGuid = model.ParentGuid;
            return PartialView("_PartialHdnBillingTerm", viewModel);
        }

        public ActionResult GetDocumentNumber(string module)
        {
            var data = _docNumering.Filter(x => x.ModuleName == module).Select(x => new
            {

                Id = x.Id,
                Description = x.DocDesc,

            }).OrderBy(x => x.Description);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region Inventory Issues

        #region Bill Of Material
        public JsonResult CheckCodeInBillOfMaterial(string Code, int? Id)
        {
            var feeterm = new BillOfMaterial();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _billOfMaterial.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm =
                        _billOfMaterial.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _billOfMaterial.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new BillOfMaterial();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public ActionResult BillOfMaterialList(int pageNo = 1)
        {
            var viewModel = new BillOfMaterialViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            viewModel.BillOfMaterials = _billOfMaterial.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView("_partialBillOfMaterial", viewModel);
        }
        public ActionResult BillOfMaterial(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("BOM");
            var viewModel = new BillOfMaterialViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var data = _billOfMaterial.GetPagedList(x => !x.IsDeleted, x => x.Id, pageNo, SystemControl.PageSize);
            viewModel.BillOfMaterialAddViewModel = new BillOfMaterialAddViewModel();
            viewModel.BillOfMaterialAddViewModel.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
            viewModel.BillOfMaterials = data;
            viewModel.BillOfMaterialAddViewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            viewModel.BillOfMaterials = _billOfMaterial.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            viewModel.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
            return View(viewModel);
        }

        public ActionResult BillOfMaterialAdd()
        {
            var viewModel = new BillOfMaterialAddViewModel();
            viewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            return PartialView("BillOfMaterialAddEdit", viewModel);
        }

        [HttpPost]
        public ActionResult BillOfMaterialAdd(BillOfMaterialAddViewModel model)
        {
            var newRow = string.Empty;

            if (ModelState.IsValid)
            {
                var billOfMaterial = new BillOfMaterial();
                billOfMaterial.BranchId = CurrentBranchId;
                billOfMaterial.Code = model.Code;
                billOfMaterial.ConversionFactor = Convert.ToDecimal(model.ConversionFactor);
                billOfMaterial.CostCenterId = model.CostCenterId;

                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                billOfMaterial.CreatedById = currentUserId;
                billOfMaterial.CreatedDate = DateTime.UtcNow;
                billOfMaterial.Description = model.Description;
                billOfMaterial.FinishedGoodId = model.FinishedGoodId;
                billOfMaterial.IsActive = true;
                billOfMaterial.ModifiedById = currentUserId;
                billOfMaterial.ModifiedDate = DateTime.UtcNow;
                billOfMaterial.Qty = Convert.ToDecimal(model.Qty);
                billOfMaterial.Remarks = model.Remarks;
                billOfMaterial.UnitId = model.UnitId;
                billOfMaterial.IsDeleted = false;
                billOfMaterial.FYId = this.FiscalYear.Id;
                //if (model.BillOfMaterial.CurrencyId != 0)
                //{
                //    model.BillOfMaterial.CurrCode = _currency.GetById(model.BillOfMaterial.CurrencyId).Code;
                //}
                _billOfMaterial.Add(billOfMaterial);

                foreach (var item in model.BillOfMaterialDetailAddViewModels.Where(x => x.RawMaterialId != 0))
                {
                    var billOfMaterialDetail = new BillOfMaterialDetail();
                    billOfMaterialDetail.BillOfMaterialId = billOfMaterial.Id;
                    billOfMaterialDetail.CostCenterId = item.CostCenterId;
                    billOfMaterialDetail.GodownId = item.GodownId;
                    billOfMaterialDetail.Quantity = Convert.ToDecimal(item.Quantity);
                    billOfMaterialDetail.RawMaterialId = item.RawMaterialId;
                    billOfMaterialDetail.UnitId = item.UnitId;
                    _billOfMaterialDetail.Add(billOfMaterialDetail);
                }
                var singleBillOfMaterial = new BillOfMaterialSingleViewModel();
                singleBillOfMaterial.Id = billOfMaterial.Id;
                singleBillOfMaterial.Code = model.Code;
                singleBillOfMaterial.ConversionFactor = model.ConversionFactor;
                singleBillOfMaterial.Description = model.Description;
                singleBillOfMaterial.Qty = model.Qty;
                singleBillOfMaterial.Unit = model.Unit;
                newRow = base.RenderPartialViewToString("_PartialAddSingleBOM", singleBillOfMaterial);
            }
            return Json(new { success = true, newrow = newRow });
        }

        public ActionResult BillOfMaterialEdit(int id)
        {
            var data = _billOfMaterial.GetById(id);
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var costCenters = string.Empty;
            var godown = string.Empty;
            if (data.CostCenterId != null && data.CostCenterId != 0)
            {
                int costCenterId = Convert.ToInt32(data.CostCenterId);
                costCenters = _costCenter.GetById(costCenterId).Name;
            }

            var billOfMaterialDetailList = new List<BillOfMaterialDetailAddViewModel>();

            foreach (var item in data.BillOfMaterialDetails)
            {
                var costCenter = string.Empty;
                if (item.CostCenterId != null && item.CostCenterId != 0)
                {

                    int costCenterId = Convert.ToInt32(item.CostCenterId);
                    costCenter = _costCenter.GetById(costCenterId).Name;
                }
                if (item.GodownId != null && item.GodownId != 0)
                {
                    godown = string.Empty;
                    int godownId = Convert.ToInt32(item.GodownId);
                    godown = _godown.GetById(godownId).Name;
                }
                var viewModelBOMDetail = new BillOfMaterialDetailAddViewModel()
                                             {
                                                 CostCenter = costCenter,
                                                 BillOfMaterialId = data.Id,
                                                 CostCenterId = item.CostCenterId,
                                                 Godown = godown,
                                                 GodownId = item.GodownId,
                                                 Quantity = item.Quantity,
                                                 RawMaterial = item.RawMaterial.Name,
                                                 RawMaterialId = item.RawMaterialId,
                                                 UnitId = item.UnitId
                                             };
                viewModelBOMDetail.UnitList = unitSelectList;
                billOfMaterialDetailList.Add(viewModelBOMDetail);
                costCenter = string.Empty;
            }

            var viewModel = new BillOfMaterialAddViewModel()
                                {
                                    Code = data.Code,
                                    ConversionFactor = data.ConversionFactor,
                                    CostCenterId = data.CostCenterId,
                                    CostCenter = costCenters,
                                    Description = data.Description,
                                    BillOfMaterialDetailAddViewModels = billOfMaterialDetailList,
                                    UnitList = unitSelectList,
                                    FinishedGood = data.FinishedGood.Name,
                                    FinishedGoodId = data.FinishedGoodId,
                                    Qty = data.Qty,
                                    Remarks = data.Remarks,
                                    Unit = data.Unit.Code,
                                    UnitId = data.UnitId,
                                    Id = data.Id
                                };
            viewModel.TotalQty = data.BillOfMaterialDetails.Sum(x => x.Quantity);
            //if (!string.IsNullOrEmpty(data.CurrCode))
            //{
            //    viewModel.BillOfMaterial.CurrCode = data.CurrCode;
            //    viewModel.BillOfMaterial.CurrencyId = _currency.GetById(x => x.Code == data.CurrCode).Id;
            //}
            return PartialView("BillOfMaterialAddEdit", viewModel);
        }

        [HttpPost]
        public ActionResult BillOfMaterialEdit(BillOfMaterialAddViewModel model)
        {
            var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
            var updatedRow = string.Empty;
            int updateId = 0;
            if (ModelState.IsValid)
            {
                var billOfMaterial = _billOfMaterial.GetById(model.Id);
                billOfMaterial.BranchId = CurrentBranchId;
                billOfMaterial.Code = model.Code;
                billOfMaterial.ConversionFactor = Convert.ToDecimal(model.ConversionFactor);
                billOfMaterial.CostCenterId = model.CostCenterId;
                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                billOfMaterial.CreatedById = currentUserId;
                billOfMaterial.CreatedDate = DateTime.UtcNow;
                billOfMaterial.Description = model.Description;
                billOfMaterial.FinishedGoodId = model.FinishedGoodId;
                billOfMaterial.IsActive = true;
                billOfMaterial.ModifiedById = currentUserId;
                billOfMaterial.ModifiedDate = DateTime.UtcNow;
                billOfMaterial.Qty = Convert.ToDecimal(model.Qty);
                billOfMaterial.Remarks = model.Remarks;
                billOfMaterial.UnitId = model.UnitId;
                billOfMaterial.IsDeleted = false;
                billOfMaterial.FYId = FiscalYear.Id;
                // if (model.BillOfMaterial.CurrencyId != 0)
                //{
                //    model.BillOfMaterial.CurrCode = _currency.GetById(model.BillOfMaterial.CurrencyId).Code;
                //}

                _billOfMaterial.Update(billOfMaterial);

                _billOfMaterialDetail.Delete(x => x.BillOfMaterialId == model.Id);

                foreach (var item in model.BillOfMaterialDetailAddViewModels.Where(x => x.RawMaterialId != 0))
                {
                    var billOfMaterialDetail = new BillOfMaterialDetail();
                    billOfMaterialDetail.BillOfMaterialId = billOfMaterial.Id;
                    billOfMaterialDetail.CostCenterId = item.CostCenterId;
                    billOfMaterialDetail.GodownId = item.GodownId;
                    billOfMaterialDetail.Quantity = Convert.ToDecimal(item.Quantity);
                    billOfMaterialDetail.RawMaterialId = item.RawMaterialId;
                    billOfMaterialDetail.UnitId = item.UnitId;
                    _billOfMaterialDetail.Add(billOfMaterialDetail);
                }
                var singleBillOfMaterial = new BillOfMaterialSingleViewModel();
                updateId = billOfMaterial.Id;
                singleBillOfMaterial.Id = billOfMaterial.Id;
                singleBillOfMaterial.Code = model.Code;
                singleBillOfMaterial.ConversionFactor = model.ConversionFactor;
                singleBillOfMaterial.Description = model.Description;
                singleBillOfMaterial.Qty = model.Qty;
                singleBillOfMaterial.Unit = model.Unit;
                updatedRow = base.RenderPartialViewToString("_PartialAddSingleBOM", singleBillOfMaterial);

            }
            return Json(new { success = true, id = updateId, updatedRow = updatedRow });
        }

        [HttpPost]
        public ActionResult BillOfMaterialDelete(int id)
        {
            var data = _billOfMaterial.GetById(id);
            data.IsDeleted = true;
            _billOfMaterial.Update(data);
            return Json(true);
        }

        public JsonResult GetBillOfMaterialList()
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var data = _billOfMaterial.GetMany(x => !x.IsDeleted && x.IsActive && x.FYId == fiscalYear.Id).Select(x => new
            {
                Id = x.Id,
                Description = x.Description
            }).OrderBy(x => x.Description);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFinishedGoodsList()
        {
            var context = new DataContext();
            var data = (from b in context.BillOfMaterials
                        join p in context.Products on b.FinishedGoodId equals p.Id
                        select new
                                   {
                                       Id = p.Id,
                                       FinishedGoods = p.Name
                                   }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Material Issue
        public ActionResult MaterialIssueList(int pageNo = 1)
        {
            var viewModel = new MaterialIssueViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            viewModel.MaterialIssues = _materialIssue.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView("_PartialMaterialIssue", viewModel);
        }
        public JsonResult CheckCodeInMaterialIssue(string Code, int? Id)
        {
            var feeterm = new MaterialIssue();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _materialIssue.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm =
                        _materialIssue.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _materialIssue.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new MaterialIssue();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public ActionResult MaterialIssue(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("MI");
            var viewModel = new MaterialIssueViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            //var viewModel = new MaterialIssueAddViewModel();
            var data = _materialIssue.GetPagedList(x => !x.IsDeleted, x => x.Id, pageNo, SystemControl.PageSize);
            var model = _materialIssue.GetAll().LastOrDefault();
            viewModel.MaterialIssueAddViewModel = new MaterialIssueAddViewModel();
            viewModel.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
            viewModel.MaterialIssueAddViewModel.EntryControl = viewModel.EntryControl;
            //viewModel.MaterialIssueAddViewModel.IssueDate = DateTime.Now;
            //viewModel.MaterialIssueAddViewModel.IssueMiti = NepaliDateService.NepaliDate(DateTime.Now).Date;
            viewModel.MaterialIssues = data;
            viewModel.MaterialIssueAddViewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            viewModel.DateType = _systemControl.GetAll().FirstOrDefault().DateType;
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.MaterialIssueAddViewModel.IssueDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                if (data != null)
                {
                    viewModel.MaterialIssueAddViewModel.IssueDate = base.SystemControl.DateType == 1 ? model.IssueDate.ToShortDateString() : model.IssueMiti;
                }
                else
                {
                    viewModel.MaterialIssueAddViewModel.IssueDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            viewModel.MaterialIssues = _materialIssue.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);

            return View(viewModel);
        }

        public ActionResult CheckFiscalyearDateinMaterialIssue(string IssueDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(IssueDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(IssueDate);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
               
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }


        public ActionResult MaterialIssueAdd()
        {
            var viewModel = new MaterialIssueAddViewModel();
            viewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            //viewModel.IssueDate = DateTime.Now;
            return PartialView("MaterialIssueAddEdit", viewModel);
        }

        [HttpPost]
        public ActionResult MaterialIssueAdd(MaterialIssueAddViewModel model)
        {
            var newRow = string.Empty;
            if (ModelState.IsValid)
            {
                var materialIssue = new MaterialIssue();
                materialIssue.BranchId = CurrentBranchId;
                materialIssue.Description = model.Description;
                materialIssue.Code = model.Code;
                materialIssue.BillOfMaterialId = model.BillOfMaterialId;
                materialIssue.ConversionFactor = Convert.ToDecimal(model.ConversionFactor);
                materialIssue.CostCenterId = model.CostCenterId;
                materialIssue.Qty = Convert.ToDecimal(model.Qty);
                // materialIssue.IssueDate = model.IssueDate;
                materialIssue.IssueMiti = model.IssueMiti;
                materialIssue.Remarks = model.Remarks;
                materialIssue.UnitId = model.UnitId;
                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                materialIssue.CreatedById = currentUserId;
                materialIssue.CreatedDate = DateTime.UtcNow;
                materialIssue.FinishedGoodId = model.FinishedGoodId;
                materialIssue.ModifiedById = currentUserId;
                materialIssue.ModifiedDate = DateTime.UtcNow;
                materialIssue.IsDeleted = false;
                materialIssue.FYId = this.FiscalYear.Id;
                if (base.SystemControl.DateType == 1)
                {
                    materialIssue.IssueDate = model.IssueDate.ToDate();
                    materialIssue.IssueMiti = NepaliDateService.NepaliDate(materialIssue.IssueDate).Date;
                }
                else
                {
                    materialIssue.IssueMiti = model.IssueDate;
                    materialIssue.IssueDate = NepaliDateService.NepalitoEnglishDate(model.IssueDate);
                }
                _materialIssue.Add(materialIssue);
                int sno = 1;
                foreach (var item in model.MaterialIssueDetailAddViewModels.Where(x => x.RawMaterialId != 0))
                {
                    var materialIssueDetail = new MaterialIssueDetail();
                    materialIssueDetail.MaterialIssueId = materialIssue.Id;
                    materialIssueDetail.Quantity = Convert.ToDecimal(item.Quantity);
                    materialIssueDetail.RawMaterialId = item.RawMaterialId;
                    materialIssueDetail.UnitId = item.UnitId;
                    materialIssueDetail.GodownId = Convert.ToInt32(item.GodownId);
                    materialIssueDetail.CostCenterId = Convert.ToInt32(item.CostCenterId);
                    _materialIssueDetail.Add(materialIssueDetail);

                    //Stock Transaction
                    var stockTransaction = new StockTransaction();
                    stockTransaction.Unit = _unitRepository.GetById(x => x.Id == materialIssueDetail.UnitId).Code;
                    stockTransaction.CurrRate = 1;
                    stockTransaction.NetAmt = 0;
                    stockTransaction.ProductCode = materialIssueDetail.RawMaterialId;
                    stockTransaction.Quantity = materialIssueDetail.Quantity;
                    stockTransaction.Rate = 0;
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = "MI";
                    stockTransaction.ReferenceId = materialIssue.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    //var stock = _stockTransaction.Filter(x => x.ProductCode == materialIssueDetail.RawMaterialId).ToList().LastOrDefault();
                    //if (stock == null)
                    //{
                    stockTransaction.StockVal = 0;
                    stockTransaction.StockQty = Convert.ToDecimal(item.Quantity);

                    //}
                    //else
                    //{
                    //    stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(salesDetail.BasicAmt);
                    //    stockTransaction.StockQty = stock.StockQty - salesDetail.StockQty;
                    //}
                    stockTransaction.TermAmt = 0;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    stockTransaction.VDate = materialIssue.IssueDate;
                    stockTransaction.VNo = materialIssue.Code;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;
                }
                var singleBillOfMaterial = new MaterialIssueSingleViewModel();
                singleBillOfMaterial.Id = materialIssue.Id;
                singleBillOfMaterial.Code = model.Code;
                singleBillOfMaterial.ConversionFactor = model.ConversionFactor;
                singleBillOfMaterial.Description = model.Description;
                singleBillOfMaterial.Qty = model.Qty;
                singleBillOfMaterial.Unit = model.Unit;
                newRow = base.RenderPartialViewToString("_PartialAddSingleMI", singleBillOfMaterial);
            }
            else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    if (modelState.Errors.Any())
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                        }
                    }
                }
            }
            return Json(new { success = true, newrow = newRow });
        }

        public ActionResult MaterialIssueEdit(int id)
        {
            var data = _materialIssue.GetById(id);
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var costCenters = string.Empty;
            var godown = string.Empty;
            if (data.CostCenterId != null && data.CostCenterId != 0)
            {
                int costCenterId = Convert.ToInt32(data.CostCenterId);
                costCenters = _costCenter.GetById(costCenterId).Name;
            }

            var materialIssueDetailList = new List<MaterialIssueDetailAddViewModel>();

            foreach (var item in data.MaterialIssueDetails)
            {
                var costCenter = string.Empty;
                if (item.CostCenterId != 0 && item.CostCenterId != null)
                {

                    int costCenterId = Convert.ToInt32(item.CostCenterId);
                    costCenter = _costCenter.GetById(costCenterId).Name;
                }
                if (item.GodownId != null && item.GodownId != 0)
                {
                    godown = string.Empty;
                    int godownId = Convert.ToInt32(item.GodownId);
                    godown = _godown.GetById(godownId).Name;
                }
                var viewModelMIDetail = new MaterialIssueDetailAddViewModel()
                {
                    CostCenter = costCenter,
                    Godown = godown,
                    GodownId = item.GodownId,
                    Quantity = item.Quantity,
                    RawMaterial = item.RawMaterial.Name,
                    RawMaterialId = item.RawMaterialId,
                    UnitId = item.UnitId,
                    CostCenterId = item.CostCenterId
                };
                viewModelMIDetail.UnitList = unitSelectList;
                materialIssueDetailList.Add(viewModelMIDetail);
                costCenter = string.Empty;
            }
            var billOfMaterial = _billOfMaterial.GetById(data.BillOfMaterialId).Description;
            //var miCostCenter = _costCenter.GetById(data.CostCenterId).Name;
            var viewModel = new MaterialIssueAddViewModel()
            {

                Code = data.Code,
                ConversionFactor = data.BillOfMaterial.ConversionFactor,
                CostCenterId = data.CostCenterId,
                Description = data.BillOfMaterial.Description,
                MaterialIssueDetailAddViewModels = materialIssueDetailList,
                UnitList = unitSelectList,
                FinishedGood = data.FinishedGood.Name,
                FinishedGoodId = data.FinishedGoodId,
                Qty = data.BillOfMaterial.Qty,
                Remarks = data.BillOfMaterial.Remarks,
                Unit = data.BillOfMaterial.Unit.Code,
                UnitId = data.BillOfMaterial.UnitId,
                Id = data.Id,
                //IssueDate = data.IssueDate,
                IssueMiti = data.IssueMiti,
                BillOfMaterialId = data.BillOfMaterialId,
                BillOfMaterial = billOfMaterial,
                CostCenter = costCenters

            };
            if (base.SystemControl.DateType == 1)
            {
                viewModel.IssueDate = data.IssueDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.IssueDate = data.IssueMiti;
            }
            viewModel.TotalQty = data.MaterialIssueDetails.Sum(x => x.Quantity);
            viewModel.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
            return PartialView("MaterialIssueAddEdit", viewModel);
        }

        [HttpPost]
        public ActionResult MaterialIssueEdit(MaterialIssueAddViewModel model)
        {
            var updatedRow = string.Empty;
            int updateId = 0;
            if (ModelState.IsValid)
            {
                var materialIssue = _materialIssue.GetById(model.Id);
                materialIssue.BranchId = CurrentBranchId;
                materialIssue.Code = model.Code;
                materialIssue.ConversionFactor = Convert.ToDecimal(model.ConversionFactor);
                materialIssue.CostCenterId = model.CostCenterId;
                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                materialIssue.CreatedById = currentUserId;
                materialIssue.CreatedDate = DateTime.UtcNow;
                materialIssue.Description = model.Description;
                materialIssue.FinishedGoodId = model.FinishedGoodId;
                materialIssue.ModifiedById = currentUserId;
                materialIssue.ModifiedDate = DateTime.UtcNow;
                materialIssue.Qty = Convert.ToDecimal(model.Qty);
                materialIssue.Remarks = model.Remarks;
                materialIssue.UnitId = model.UnitId;
                materialIssue.IsDeleted = false;
                materialIssue.FYId = FiscalYear.Id;
                if (base.SystemControl.DateType == 1)
                {
                    materialIssue.IssueDate = Convert.ToDateTime(model.IssueDate);
                    materialIssue.IssueMiti = NepaliDateService.NepaliDate(materialIssue.IssueDate).Date;
                }
                else
                {
                    materialIssue.IssueMiti = model.IssueDate;
                    materialIssue.IssueDate = NepaliDateService.NepalitoEnglishDate(model.IssueDate);
                }
                _materialIssue.Update(materialIssue);

                _materialIssueDetail.Delete(x => x.MaterialIssueId == model.Id);
                int sno = 1;
                _stockTransaction.Delete(x => x.ReferenceId == materialIssue.Id && x.Source == "MI");
                foreach (var item in model.MaterialIssueDetailAddViewModels.Where(x => x.RawMaterialId != 0))
                {
                    var materialIssueDetail = new MaterialIssueDetail();
                    materialIssueDetail.MaterialIssueId = materialIssue.Id;
                    materialIssueDetail.GodownId = Convert.ToInt32(item.GodownId);
                    materialIssueDetail.Quantity = Convert.ToDecimal(item.Quantity);
                    materialIssueDetail.RawMaterialId = item.RawMaterialId;
                    materialIssueDetail.UnitId = item.UnitId;
                    materialIssueDetail.CostCenterId = Convert.ToInt32(item.CostCenterId);
                    _materialIssueDetail.Add(materialIssueDetail);

                    //Stock Transaction
                    var stockTransaction = new StockTransaction();
                    stockTransaction.Unit = _unitRepository.GetById(x => x.Id == materialIssueDetail.UnitId).Code;
                    stockTransaction.CurrRate = 1;
                    stockTransaction.NetAmt = 0;
                    stockTransaction.ProductCode = materialIssueDetail.RawMaterialId;
                    stockTransaction.Quantity = materialIssueDetail.Quantity;
                    stockTransaction.Rate = 0;
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = "MI";
                    stockTransaction.ReferenceId = materialIssue.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    //var stock = _stockTransaction.Filter(x => x.ProductCode == materialIssueDetail.RawMaterialId).ToList().LastOrDefault();
                    //if (stock == null)
                    //{
                    stockTransaction.StockVal = 0;
                    stockTransaction.StockQty = Convert.ToDecimal(item.Quantity);

                    //}
                    //else
                    //{
                    //    stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(salesDetail.BasicAmt);
                    //    stockTransaction.StockQty = stock.StockQty - salesDetail.StockQty;
                    //}
                    stockTransaction.TermAmt = 0;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    stockTransaction.VDate = materialIssue.IssueDate;
                    stockTransaction.VNo = materialIssue.Code;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;
                }
                var singleMaterialIssue = new MaterialIssueSingleViewModel();
                updateId = model.Id;
                singleMaterialIssue.Id = materialIssue.Id;
                singleMaterialIssue.Code = model.Code;
                singleMaterialIssue.ConversionFactor = model.ConversionFactor;
                singleMaterialIssue.Description = model.Description;
                singleMaterialIssue.Qty = model.Qty;
                singleMaterialIssue.Unit = model.Unit;
                updatedRow = base.RenderPartialViewToString("_PartialAddSingleMI", singleMaterialIssue);
            }
            return Json(new { success = true, id = updateId, updatedRow = updatedRow });
        }

        [HttpPost]
        public ActionResult MaterialIssueDelete(int id)
        {
            var data = _materialIssue.GetById(id);
            data.IsDeleted = true;
            _materialIssue.Update(data);
            _stockTransaction.Delete(x => x.ReferenceId == data.Id && x.Source == "MI");
            return Json(true);
        }
        public ActionResult PopulateMaterialIssueDetails(int id)
        {
            var billOfMaterial = _billOfMaterial.GetById(x => x.Id == id, "BillOfMaterialDetails");
            var finishedGood = _product.GetById(x => x.Id == billOfMaterial.FinishedGoodId);
            var bomUnit = _unitRepository.GetById(billOfMaterial.UnitId).Code;
            var bomCostCenter = string.Empty;
            if (billOfMaterial.CostCenterId != null)
            {
                var costCenterId = Convert.ToInt32(billOfMaterial.CostCenterId);
                bomCostCenter = _costCenter.GetById(costCenterId).Name;
            }
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var materialIssueDetailView = string.Empty;
            foreach (var item in billOfMaterial.BillOfMaterialDetails)
            {
                var singleMaterialIssueDetail = new MaterialIssueDetailAddViewModel();
                var product = _product.GetById(item.RawMaterialId);
                singleMaterialIssueDetail.RawMaterial = product.Name;
                singleMaterialIssueDetail.RawMaterialId = item.RawMaterialId;
                if (item.CostCenterId != null)
                {
                    var costCenterId = Convert.ToInt32(item.CostCenterId);
                    var costCenter = _costCenter.GetById(costCenterId).Name;
                    singleMaterialIssueDetail.CostCenterId = item.CostCenterId;
                    singleMaterialIssueDetail.CostCenter = costCenter;
                }
                if (item.GodownId != null)
                {
                    var godownId = Convert.ToInt32(item.GodownId);
                    var godown = _godown.GetById(godownId).Name;
                    singleMaterialIssueDetail.GodownId = item.GodownId;
                    singleMaterialIssueDetail.Godown = godown;
                }
                singleMaterialIssueDetail.UnitId = item.UnitId;
                singleMaterialIssueDetail.Quantity = item.Quantity;
                singleMaterialIssueDetail.UnitList = unitSelectList;
                singleMaterialIssueDetail.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
                materialIssueDetailView += base.RenderPartialViewToString("_PartialMIDetailEntry", singleMaterialIssueDetail);
            }
            return Json(new
            {
                FinishedGoodId = billOfMaterial.FinishedGoodId,
                FinishedGood = finishedGood.Name,
                Description = billOfMaterial.Description,
                CostCenter = bomCostCenter,
                CostCenterId = billOfMaterial.CostCenterId,
                ConversionFactor = billOfMaterial.ConversionFactor,
                Qty = billOfMaterial.Qty,
                UnitId = billOfMaterial.UnitId,
                Unit = bomUnit,
                materialIssueDetailView = materialIssueDetailView
            });
        }

        public JsonResult GetMaterialIssueList()
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var data = _materialIssue.GetMany(x => !x.IsDeleted && x.FYId == fiscalYear.Id).Select(x => new
            {
                Id = x.Id,
                IssueNo = x.Code,
                IssueDate = x.IssueDate.ToShortDateString(),
                Description = x.Description
            }).OrderBy(x => x.IssueNo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Material Return
        public JsonResult CheckCodeInMaterialReturn(string Code, int? Id)
        {
            var feeterm = new MaterialReturn();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _materialReturn.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm =
                        _materialReturn.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _materialReturn.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new MaterialReturn();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public ActionResult MaterialReturnList(int pageNo = 1)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var viewModel = new MaterialReturnViewModel();
            viewModel.MaterialReturns = _materialReturn.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView("_PartialMaterialReturn", viewModel);
        }
        public ActionResult MaterialReturn(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("MR");
            var viewModel = new MaterialReturnViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var data = _materialReturn.GetPagedList(x => !x.IsDeleted, x => x.Id, pageNo, SystemControl.PageSize);
            var model = _materialReturn.GetAll().LastOrDefault();
            viewModel.MaterialReturnAddViewModel = new MaterialReturnAddViewModel();
            viewModel.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
            viewModel.MaterialReturnAddViewModel.EntryControl = viewModel.EntryControl;
            //viewModel.MaterialReturnAddViewModel.ReturnDate = DateTime.Now;
            viewModel.MaterialReturnAddViewModel.ReturnMiti = NepaliDateService.NepaliDate(DateTime.Now).Date;
            viewModel.MaterialReturnAddViewModel.IssueDate = null;
            viewModel.MaterialReturns = data;
            viewModel.MaterialReturnAddViewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            viewModel.MaterialReturns = _materialReturn.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            viewModel.DateType = _systemControl.GetAll().FirstOrDefault().DateType;
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.MaterialReturnAddViewModel.ReturnDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {

                if (data != null)
                {
                    viewModel.MaterialReturnAddViewModel.ReturnDate = base.SystemControl.DateType == 1 ? model.ReturnDate.ToShortDateString() : model.ReturnMiti;
                }
                else
                {
                    viewModel.MaterialReturnAddViewModel.ReturnDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            return View(viewModel);
        }

        public ActionResult CheckFiscalyearDateinMaterialReturn(string ReturnDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(ReturnDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(ReturnDate);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
               
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }

        public ActionResult MaterialReturnAdd()
        {
            var viewModel = new MaterialReturnAddViewModel();
            viewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            //viewModel.IssueDate = DateTime.Now;
            return PartialView("MaterialReturnAddEdit", viewModel);
        }

        [HttpPost]
        public ActionResult MaterialReturnAdd(MaterialReturnAddViewModel model)
        {
            var newRow = string.Empty;
            if (ModelState.IsValid)
            {
                var materialReturn = new MaterialReturn();
                materialReturn.BranchId = CurrentBranchId;
                materialReturn.Code = model.Code;
                materialReturn.MaterialIssueId = model.MaterialIssueId;
                materialReturn.CostCenterId = model.CostCenterId;
                //materialReturn.IssueDate = model.IssueDate;
                materialReturn.IssueMiti = model.IssueMiti;
                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                materialReturn.CreatedById = currentUserId;
                materialReturn.CreatedDate = DateTime.UtcNow;
                materialReturn.ModifiedById = currentUserId;
                materialReturn.ModifiedDate = DateTime.UtcNow;
                materialReturn.IsDeleted = false;
                materialReturn.FYId = this.FiscalYear.Id;
                materialReturn.Remarks = model.Remarks;

                if (base.SystemControl.DateType == 1)
                {
                    materialReturn.ReturnDate = model.ReturnDate.ToDate();
                    materialReturn.ReturnMiti = NepaliDateService.NepaliDate(materialReturn.ReturnDate).Date;
                }
                else
                {
                    materialReturn.ReturnMiti = model.ReturnDate;
                    materialReturn.ReturnDate = NepaliDateService.NepalitoEnglishDate(model.ReturnDate);
                }
                if (base.SystemControl.DateType == 1)
                {
                    materialReturn.IssueDate = model.DisplayDate.ToDate();
                    materialReturn.IssueMiti = NepaliDateService.NepaliDate(materialReturn.IssueDate).Date;
                }
                else
                {
                    materialReturn.IssueMiti = model.DisplayDate.ToString("yyyy/MM/dd");
                    materialReturn.IssueDate = NepaliDateService.NepalitoEnglishDate(materialReturn.IssueMiti);
                }
                _materialReturn.Add(materialReturn);
                int sno = 1;
                foreach (var item in model.MaterialReturnDetailAddViewModels.Where(x =>x.RawMaterialId != 0))
                {
                    if (item.RawMaterialId != 0)
                    {
                        var materialIssueDetail = new MaterialReturnDetail();
                        materialIssueDetail.MaterialReturnId = materialReturn.Id;
                        materialIssueDetail.Quantity = Convert.ToDecimal(item.Quantity);
                        materialIssueDetail.RawMaterialId = item.RawMaterialId;
                        materialIssueDetail.UnitId = item.UnitId;
                        materialIssueDetail.GodownId = Convert.ToInt32(item.GodownId);
                        if (item.CostCenterId != null)
                            materialIssueDetail.CostCenterId = Convert.ToInt32(item.CostCenterId);
                        _materialReturnDetail.Add(materialIssueDetail);

                        //Stock Transaction
                        var stockTransaction = new StockTransaction();
                        stockTransaction.Unit = _unitRepository.GetById(x => x.Id == materialIssueDetail.UnitId).Code;
                        stockTransaction.CurrRate = 1;
                        stockTransaction.NetAmt = 0;
                        stockTransaction.ProductCode = materialIssueDetail.RawMaterialId;
                        stockTransaction.Quantity = materialIssueDetail.Quantity;
                        stockTransaction.Rate = 0;
                        stockTransaction.SNo = sno;
                        stockTransaction.Source = "MR";
                        stockTransaction.ReferenceId = materialReturn.Id;
                        stockTransaction.FYId = FiscalYear.Id;
                        //var stock = _stockTransaction.Filter(x => x.ProductCode == materialIssueDetail.RawMaterialId).ToList().LastOrDefault();
                        //if (stock == null)
                        //{
                        stockTransaction.StockVal = 0;
                        stockTransaction.StockQty = Convert.ToDecimal(item.Quantity);

                        //}
                        //else
                        //{
                        //    stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(salesDetail.BasicAmt);
                        //    stockTransaction.StockQty = stock.StockQty - salesDetail.StockQty;
                        //}
                        stockTransaction.TermAmt = 0;
                        stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                        stockTransaction.VDate = materialReturn.IssueDate;
                        stockTransaction.VNo = materialReturn.Code;
                        stockTransaction.BranchId = CurrentBranchId;
                        _stockTransaction.Add(stockTransaction);
                        sno++;
                    }

                }
                var singleBillOfMaterial = new MaterialReturnSingleViewModel();
                singleBillOfMaterial.Id = materialReturn.Id;
                singleBillOfMaterial.Code = model.Code;
                singleBillOfMaterial.Description = _materialIssue.GetById(x => x.Id == materialReturn.MaterialIssueId).Description;
                singleBillOfMaterial.CostCenter = _costCenter.GetById(x => x.Id == materialReturn.CostCenterId).Name;
                newRow = base.RenderPartialViewToString("_PartialAddSingleMR", singleBillOfMaterial);
            }
            return Json(new { success = true, newrow = newRow });
        }

        public ActionResult MaterialReturnEdit(int id)
        {
            var data = _materialReturn.GetById(id);
            var issue = _materialIssue.GetById(id);
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var costCenters = string.Empty;
            var godown = string.Empty;

            if (data.CostCenterId != null && data.CostCenterId != 0)
            {
                int costCenterId = Convert.ToInt32(data.CostCenterId);
                costCenters = _costCenter.GetById(costCenterId).Name;
            }

            var materialReturnDetailList = new List<MaterialReturnDetailAddViewModel>();

            foreach (var item in data.MaterialReturnDetails)
            {
                var costCenter = string.Empty;
                if (item.CostCenterId != 0 && item.CostCenterId != null)
                {
                    int costCenterId = Convert.ToInt32(data.CostCenterId);
                    costCenter = _costCenter.GetById(costCenterId).Name;
                }
                if (item.GodownId != null && item.GodownId != 0)
                {
                    godown = string.Empty;
                    int godownId = Convert.ToInt32(item.GodownId);
                    godown = _godown.GetById(godownId).Name;
                }
                var viewModelMRDetail = new MaterialReturnDetailAddViewModel()
                {
                    CostCenter = costCenter,
                    Godown = godown,
                    GodownId = item.GodownId,
                    Quantity = item.Quantity,
                    RawMaterial = item.RawMaterial.Name,
                    RawMaterialId = item.RawMaterialId,
                    UnitId = item.UnitId,
                    CostCenterId = item.CostCenterId

                };
                viewModelMRDetail.UnitList = unitSelectList;
                materialReturnDetailList.Add(viewModelMRDetail);
                costCenter = string.Empty;
            }
            var materialIssue = _materialIssue.GetById(data.MaterialIssueId).Description;
            //var miCostCenter = _costCenter.GetById(data.CostCenterId).Name;
            var viewModel = new MaterialReturnAddViewModel()
            {
                Code = data.Code,
                CostCenterId = data.CostCenterId,
                MaterialReturnDetailAddViewModels = materialReturnDetailList,
                UnitList = unitSelectList,
                Id = data.Id,
                //IssueDate = data.IssueDate,
                IssueMiti = data.IssueMiti,
                MaterialIssueId = data.MaterialIssueId,
                MaterialIssue = materialIssue,
                CostCenter = costCenters

            };
            if (base.SystemControl.DateType == 1)
            {
                viewModel.ReturnDate = data.ReturnDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.ReturnDate = data.ReturnMiti;
            }
            if (base.SystemControl.DateType == 1)
            {
                viewModel.DisplayDate = data.IssueDate;
            }
            else
            {
                viewModel.DisplayDate = data.IssueMiti.ToDate();
            }
            viewModel.TotalQty = data.MaterialReturnDetails.Sum(x => x.Quantity);
            viewModel.Remarks = data.Remarks;
            viewModel.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
            return PartialView("MaterialReturnAddEdit", viewModel);
        }

        [HttpPost]
        public ActionResult MaterialReturnEdit(MaterialReturnAddViewModel model)
        {
            var updatedRow = string.Empty;
            int updateId = 0;
            if (ModelState.IsValid)
            {
                var materialReturn = _materialReturn.GetById(model.Id);
                materialReturn.BranchId = CurrentBranchId;
                materialReturn.Code = model.Code;
                materialReturn.CostCenterId = model.CostCenterId;

                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                materialReturn.CreatedById = currentUserId;
                materialReturn.CreatedDate = DateTime.UtcNow;
                materialReturn.ModifiedById = currentUserId;
                materialReturn.ModifiedDate = DateTime.UtcNow;
                materialReturn.IsDeleted = false;
                materialReturn.FYId = FiscalYear.Id;
                materialReturn.Remarks = model.Remarks;
                if (base.SystemControl.DateType == 1)
                {
                    materialReturn.ReturnDate = Convert.ToDateTime(model.ReturnDate);
                    materialReturn.ReturnMiti = NepaliDateService.NepaliDate(materialReturn.ReturnDate).Date;
                }
                else
                {
                    materialReturn.ReturnMiti = model.ReturnDate;
                    materialReturn.ReturnDate = NepaliDateService.NepalitoEnglishDate(model.ReturnDate);
                }
                if (base.SystemControl.DateType == 1)
                {
                    materialReturn.IssueDate = Convert.ToDateTime(model.DisplayDate);
                    materialReturn.IssueMiti = NepaliDateService.NepaliDate(materialReturn.IssueDate).Date;
                }
                else
                {
                    materialReturn.IssueMiti = model.DisplayDate.ToString("yyyy/MM/dd");
                    materialReturn.IssueDate = NepaliDateService.NepalitoEnglishDate(materialReturn.IssueMiti);
                }
                _materialReturn.Update(materialReturn);

                _materialReturnDetail.Delete(x => x.MaterialReturnId == model.Id);
                int sno = 1;
                _stockTransaction.Delete(x => x.ReferenceId == materialReturn.Id && x.Source == "MI");
                foreach (var item in model.MaterialReturnDetailAddViewModels.Where(x => x.RawMaterialId != 0))
                {
                    var materialReturnDetail = new MaterialReturnDetail();
                    materialReturnDetail.MaterialReturnId = materialReturn.Id;
                    materialReturnDetail.GodownId = Convert.ToInt32(item.GodownId);
                    materialReturnDetail.CostCenterId = Convert.ToInt32(item.CostCenterId);
                    materialReturnDetail.Quantity = Convert.ToDecimal(item.Quantity);
                    materialReturnDetail.RawMaterialId = item.RawMaterialId;
                    materialReturnDetail.UnitId = item.UnitId;
                    _materialReturnDetail.Add(materialReturnDetail);

                    //Stock Transaction
                    var stockTransaction = new StockTransaction();
                    stockTransaction.Unit = _unitRepository.GetById(x => x.Id == materialReturnDetail.UnitId).Code;
                    stockTransaction.CurrRate = 1;
                    stockTransaction.NetAmt = 0;
                    stockTransaction.ProductCode = materialReturnDetail.RawMaterialId;
                    stockTransaction.Quantity = materialReturnDetail.Quantity;
                    stockTransaction.Rate = 0;
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = "MR";
                    stockTransaction.ReferenceId = materialReturn.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    //var stock = _stockTransaction.Filter(x => x.ProductCode == materialIssueDetail.RawMaterialId).ToList().LastOrDefault();
                    //if (stock == null)
                    //{
                    stockTransaction.StockVal = 0;
                    stockTransaction.StockQty = Convert.ToDecimal(item.Quantity);

                    //}
                    //else
                    //{
                    //    stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(salesDetail.BasicAmt);
                    //    stockTransaction.StockQty = stock.StockQty - salesDetail.StockQty;
                    //}
                    stockTransaction.TermAmt = 0;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                    stockTransaction.VDate = materialReturn.IssueDate;
                    stockTransaction.VNo = materialReturn.Code;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;
                }
                var singleMaterialIssue = new MaterialReturnSingleViewModel();
                updateId = materialReturn.Id;
                singleMaterialIssue.Id = materialReturn.Id;
                singleMaterialIssue.Code = model.Code;
                singleMaterialIssue.Description = _materialIssue.GetById(x => x.Id == materialReturn.MaterialIssueId).Description;
                singleMaterialIssue.CostCenter = _costCenter.GetById(x => x.Id == materialReturn.CostCenterId).Name;

                updatedRow = base.RenderPartialViewToString("_PartialAddSingleMR", singleMaterialIssue);
            }
            return Json(new { success = true, id = updateId, updatedRow = updatedRow });
        }

        [HttpPost]
        public ActionResult MaterialReturnDelete(int id)
        {
            var data = _materialReturn.GetById(id);
            data.IsDeleted = true;
            _materialReturn.Update(data);
            _stockTransaction.Delete(x => x.ReferenceId == id && x.Source == "MR");
            return Json(true);
        }

        public ActionResult PopulateMaterialReturnDetails(int id)
        {
            var materialIssue = _materialIssue.GetById(x => x.Id == id, "MaterialIssueDetails");
            var finishedGood = _product.GetById(x => x.Id == materialIssue.FinishedGoodId);
            var bomUnit = _unitRepository.GetById(materialIssue.UnitId).Code;
            var bomCostCenter = string.Empty;
            if (materialIssue.CostCenterId != null)
            {
                var costCenterId = Convert.ToInt32(materialIssue.CostCenterId);
                bomCostCenter = _costCenter.GetById(costCenterId).Name;
            }

            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var materialReturnDetailView = string.Empty;
            foreach (var item in materialIssue.MaterialIssueDetails)
            {
                var singleMaterialReturnDetail = new MaterialReturnDetailAddViewModel();
                var product = _product.GetById(item.RawMaterialId);
                singleMaterialReturnDetail.RawMaterial = product.Name;
                singleMaterialReturnDetail.RawMaterialId = item.RawMaterialId;
                if (item.CostCenterId != null && item.CostCenterId!=0)
                {
                    var costCenterId = Convert.ToInt32(item.CostCenterId);
                    var costCenter = _costCenter.GetById(costCenterId).Name;
                    singleMaterialReturnDetail.CostCenterId = item.CostCenterId;
                    singleMaterialReturnDetail.CostCenter = costCenter;
                }
                if (item.GodownId != null && item.GodownId != 0)
                {
                    var godownId = Convert.ToInt32(item.GodownId);
                    var godown = _godown.GetById(godownId).Name;
                    singleMaterialReturnDetail.GodownId = item.GodownId;
                    singleMaterialReturnDetail.Godown = godown;
                }
                if (base.SystemControl.DateType == 1)
                {
                    //materialIssue.DisplayDate = ParseDateString(materialIssue.IssueDate);
                    materialIssue.DisplayDate = materialIssue.IssueDate.ToString("MM/dd/yyyy");
                    //materialIssue.IssueMiti = NepaliDateService.NepaliDate(materialReturn.IssueDate).Date;
                }
                else
                {
                    materialIssue.DisplayDate = materialIssue.IssueMiti;
                    // materialReturn.IssueDate = NepaliDateService.NepalitoEnglishDate(model.IssueDate);
                }
                singleMaterialReturnDetail.UnitId = item.UnitId;
                singleMaterialReturnDetail.Quantity = item.Quantity;
                singleMaterialReturnDetail.UnitList = unitSelectList;
                singleMaterialReturnDetail.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
                materialReturnDetailView += base.RenderPartialViewToString("_PartialMRDetailEntry", singleMaterialReturnDetail);
            }
            return Json(new
            {
                CostCenter = bomCostCenter,
                CostCenterId = materialIssue.CostCenterId,
                IssueDate = materialIssue.IssueDate.ToString("MM/dd/yyyy"),
                IssueMiti = materialIssue.IssueMiti,
                DisplayDate = materialIssue.DisplayDate,
                materialReturnDetailView = materialReturnDetailView
            });
        }

        public string ParseDateString(DateTime date)
        {
            var dateString = date.ToShortDateString();
            var arrDate = dateString.Split('/');
            dateString = "";
            foreach (var item in arrDate)
            {
                if (!string.IsNullOrEmpty(dateString))
                    dateString += "/";

                dateString += item.PadLeft(2, '0');
            }
            return dateString;
        }
        #endregion
        #endregion

        #region Stock Transfer

        #region Godown Transfer
        public JsonResult CheckCodeInStockTransfer(string STNo, int? Id)
        {
            var feeterm = new StockTransferMaster();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _stockTransfer.GetById(x => x.Id == Id);
                if (data.STNo.ToLower().Trim() != STNo.ToLower().Trim())
                {
                    feeterm =
                        _stockTransfer.GetById(x => x.STNo.ToLower().Trim() == STNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _stockTransfer.GetById(x => x.STNo.ToLower().Trim() == STNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new StockTransferMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }


        public ActionResult StockTransferList(int pageNo = 1)
        {
            var viewModel = new StockTransferViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            viewModel.StockTransfers =
                _stockTransfer.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(
                    x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView("_PartialStockTransfer", viewModel);
        }

        public ActionResult StockTransfer(int pageNo = 1)
        {
            var viewModel = new StockTransferViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var data = _stockTransfer.GetPagedList(x => !x.IsDeleted, x => x.Id, pageNo, SystemControl.PageSize);
            var model = _stockTransfer.GetAll().LastOrDefault();
            viewModel.StockTransferAddViewModel = new StockTransferAddViewModel();
            viewModel.StockTransfers = data;
            viewModel.StockTransferAddViewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            viewModel.StockTransfers = _stockTransfer.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            viewModel.DateType = _systemControl.GetAll().FirstOrDefault().DateType;
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.StockTransferAddViewModel.STDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {

                if (data != null)
                {
                    viewModel.StockTransferAddViewModel.STDate = base.SystemControl.DateType == 1 ? model.STDate.ToShortDateString() : model.STMiti;
                }
                else
                {
                    viewModel.StockTransferAddViewModel.STDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            viewModel.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
            return View(viewModel);
        }

        public ActionResult CheckFiscalyearDateinStockTransfer(string STDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(STDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(STDate);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }


        [HttpPost]
        public ActionResult StockTransferAdd(StockTransferAddViewModel model)
        {
            var newRow = string.Empty;
            if (ModelState.IsValid)
            {
                var stockTransfer = new StockTransferMaster();
                stockTransfer.BranchId = CurrentBranchId;
                stockTransfer.STNo = model.STNo;
                //stockTransfer.STDate = model.STDate;
                stockTransfer.STMiti = model.STMiti;
                stockTransfer.GodownId = model.GodownId;
                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                stockTransfer.CreatedById = currentUserId;
                stockTransfer.CreatedDate = DateTime.UtcNow;
                stockTransfer.IsActive = true;
                stockTransfer.UpdatedById = currentUserId;
                stockTransfer.UpdatedDate = DateTime.UtcNow;
                stockTransfer.Remarks = model.Remarks;
                stockTransfer.FYId = FiscalYear.Id;
                stockTransfer.IsDeleted = false;
                stockTransfer.CurCode = model.CurCode;
                stockTransfer.CurRate = model.CurRate;
                if (base.SystemControl.DateType == 1)
                {
                    stockTransfer.STDate = model.STDate.ToDate();
                    stockTransfer.STMiti = NepaliDateService.NepaliDate(stockTransfer.STDate).Date;
                }
                else
                {
                    stockTransfer.STMiti = model.STDate;
                    stockTransfer.STDate = NepaliDateService.NepalitoEnglishDate(model.STDate);
                }
                //if (model.StockTransfer.CurrencyId != 0)
                //{
                //    stockTransfer.CurCode = _currency.GetById(model.StockTransfer.CurrencyId).Code;
                //    stockTransfer.CurRate = model.CurRate;
                //}
                _stockTransfer.Add(stockTransfer);

                var sno = 1;
                foreach (var item in model.StockTransferDetailAddViewModels.Where(x => x.GodownId != null && x.ProductId != 0 && x.Amount != 0))
                {
                    var stockTransferDetail = new StockTransferDetail();
                    stockTransferDetail.Godown = Convert.ToInt32(item.GodownId);
                    stockTransferDetail.StockTransferMasterId = stockTransfer.Id;
                    stockTransferDetail.ProductCode = item.ProductId;
                    stockTransferDetail.Qty = Convert.ToDecimal(item.Quantity);
                    stockTransferDetail.Rate = Convert.ToDecimal(item.Rate);
                    stockTransferDetail.SNo = sno;
                    stockTransferDetail.NetAmt = Convert.ToDecimal(item.Amount);
                    stockTransferDetail.UnitId = item.UnitId;
                    var unitCode = _unitRepository.GetById(item.UnitId).Code;
                    //stockTransferDetail.UnitId = Convert.ToInt32(unitCode);
                    _stockTransferDetail.Add(stockTransferDetail);


                    //Stock Transaction
                    var stockTransaction = new StockTransaction();
                    stockTransaction.Unit = unitCode;
                    stockTransaction.CurrRate = model.CurRate;
                    stockTransaction.CurrCode = model.CurCode;
                    stockTransaction.NetAmt = Convert.ToDecimal(item.Amount);
                    stockTransaction.Rate = Convert.ToDecimal(item.Rate);
                    stockTransaction.Godown = Convert.ToString(item.GodownId);
                    stockTransaction.ProductCode = stockTransferDetail.ProductCode;
                    stockTransaction.Quantity = stockTransferDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(item.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = "GT";
                    stockTransaction.VNo = model.STNo;
                    stockTransaction.ReferenceId = stockTransfer.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.StockVal = 0;
                    stockTransaction.StockQty = Convert.ToDecimal(item.Quantity);
                    stockTransaction.TermAmt = 0;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                    stockTransaction.VMiti = stockTransfer.STMiti;
                    stockTransaction.VDate = stockTransfer.STDate;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    //out transaction
                    var stocktransactionOut = _stockTransaction.GetById(x => x.Id == stockTransaction.Id);
                    stocktransactionOut.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    stocktransactionOut.Godown = Convert.ToString(model.GodownId);
                    _stockTransaction.Add(stocktransactionOut);


                    sno++;
                }
                var singleGodownTransfer = new StockTransferSingleViewModel();
                singleGodownTransfer.Id = stockTransfer.Id;
                singleGodownTransfer.STNo = stockTransfer.STNo;
                singleGodownTransfer.GodownId = stockTransfer.GodownId;
                singleGodownTransfer.STDate = stockTransfer.STDate;
                singleGodownTransfer.Remarks = stockTransfer.Remarks;
                singleGodownTransfer.Godown = _godown.GetById(singleGodownTransfer.GodownId);
                newRow = base.RenderPartialViewToString("_PartialAddSingleGT", singleGodownTransfer);
            }
            return Json(new { success = true, newrow = newRow });
        }

        public ActionResult StockTransferEdit(int id)
        {
            var data = _stockTransfer.GetById(id);
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var productName = string.Empty;
            var godownFrom = string.Empty;
            var godown = string.Empty;
            var stocktransferDetailList = new List<StockTransferDetailAddViewModel>();
            foreach (var item in data.StockTransferDetails)
            {
                productName = _product.GetById(item.ProductCode).Name;
                int godownId = Convert.ToInt32(item.Godown);
                godown = _godown.GetById(godownId).Name;
                var viewModelStDetail = new StockTransferDetailAddViewModel()
                {
                    Godown = godown,
                    GodownId = item.Godown,
                    Quantity = item.Qty,
                    StockTransferId = item.StockTransferMasterId,
                    Rate = item.Rate,
                    UnitId = item.UnitId,
                    ProductId = item.ProductCode,
                    ProductName = productName,
                    Amount = item.NetAmt,
                };
                viewModelStDetail.UnitList = unitSelectList;
                stocktransferDetailList.Add(viewModelStDetail);
            }
            godownFrom = _godown.GetById(x => x.Id == data.GodownId).Name;
            var viewModel = new StockTransferAddViewModel()
            {
                STNo = data.STNo,
                Godown = godownFrom,
                GodownId = data.GodownId,
                //STDate = data.STDate,
                StockTransferDetailAddViewModels = stocktransferDetailList,
                UnitList = unitSelectList,
                Remarks = data.Remarks,
                Id = data.Id
            };
            if (base.SystemControl.DateType == 1)
            {
                viewModel.STDate = data.STDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.STDate = data.STMiti;
            }
            if (!string.IsNullOrEmpty(data.CurCode))
            {
                viewModel.CurCode = data.CurCode;
                viewModel.CurRate = data.CurRate;
                //viewModel.StockTransfer.CurrencyId = _currency.GetById(x => x.Code == data.CurCode).Id;
            }
            viewModel.TotalQty = data.StockTransferDetails.Sum(x => x.Qty);
            return PartialView("StockTransferAddEdit", viewModel);
        }

        [HttpPost]
        public ActionResult StockTransferEdit(StockTransferAddViewModel model)
        {

            var updatedRow = string.Empty;
            int updateId = 0;
            if (ModelState.IsValid)
            {
                var stockTransfer = _stockTransfer.GetById(model.Id);
                stockTransfer.BranchId = CurrentBranchId;
                stockTransfer.STNo = model.STNo;
                // stockTransfer.STDate = model.STDate;
                stockTransfer.STMiti = model.STMiti;
                stockTransfer.GodownId = model.GodownId;
                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                stockTransfer.CreatedById = currentUserId;
                stockTransfer.CreatedDate = DateTime.UtcNow;
                stockTransfer.IsActive = true;
                stockTransfer.UpdatedById = currentUserId;
                stockTransfer.UpdatedDate = DateTime.UtcNow;
                stockTransfer.Remarks = model.Remarks;
                stockTransfer.FYId = FiscalYear.Id;
                stockTransfer.IsDeleted = false;
                stockTransfer.CurCode = model.CurCode;
                stockTransfer.CurRate = model.CurRate;
                if (base.SystemControl.DateType == 1)
                {
                    stockTransfer.STDate = Convert.ToDateTime(model.STDate);
                    stockTransfer.STMiti = NepaliDateService.NepaliDate(stockTransfer.STDate).Date;
                }
                else
                {
                    stockTransfer.STMiti = model.STDate;
                    stockTransfer.STDate = NepaliDateService.NepalitoEnglishDate(model.STDate);
                }

                _stockTransfer.Update(stockTransfer);

                var sno = 1;
                _stockTransferDetail.Delete(x => x.StockTransferMasterId == stockTransfer.Id);
                _stockTransaction.Delete(x => x.ReferenceId == model.Id);
                foreach (var item in model.StockTransferDetailAddViewModels.Where(x => x.GodownId != null && x.ProductId != 0 && x.Amount != 0))
                {
                    var stockTransferDetail = new StockTransferDetail();
                    stockTransferDetail.Godown = Convert.ToInt32(item.GodownId);
                    stockTransferDetail.StockTransferMasterId = stockTransfer.Id;
                    stockTransferDetail.ProductCode = item.ProductId;
                    stockTransferDetail.Qty = Convert.ToDecimal(item.Quantity);
                    stockTransferDetail.Rate = Convert.ToDecimal(item.Rate);
                    stockTransferDetail.SNo = sno;
                    stockTransferDetail.NetAmt = Convert.ToDecimal(item.Amount);

                    var unitCode = _unitRepository.GetById(item.UnitId).Code;
                    stockTransferDetail.UnitId = item.UnitId;
                    _stockTransferDetail.Add(stockTransferDetail);


                    //Stock Transaction
                    var stockTransaction = new StockTransaction();
                    stockTransaction.Unit = unitCode;
                    stockTransaction.CurrRate = model.CurRate;
                    stockTransaction.CurrCode = model.CurCode;
                    stockTransaction.NetAmt = Convert.ToDecimal(item.Amount);
                    stockTransaction.Rate = Convert.ToDecimal(item.Rate);
                    stockTransaction.Godown = Convert.ToString(item.GodownId);
                    stockTransaction.ProductCode = stockTransferDetail.ProductCode;
                    stockTransaction.Quantity = stockTransferDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(item.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = "GT";
                    stockTransaction.VNo = model.STNo;
                    stockTransaction.ReferenceId = stockTransfer.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.StockVal = 0;
                    stockTransaction.StockQty = Convert.ToDecimal(item.Quantity);
                    stockTransaction.TermAmt = 0;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                    stockTransaction.VMiti = stockTransfer.STMiti;
                    stockTransaction.VDate = stockTransfer.STDate;
                    stockTransaction.BranchId = CurrentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    //out transaction
                    var stocktransactionOut = _stockTransaction.GetById(x => x.Id == stockTransaction.Id);
                    stocktransactionOut.TransactionType =
                        StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    stocktransactionOut.Godown = Convert.ToString(model.GodownId);
                    _stockTransaction.Add(stocktransactionOut);


                    sno++;
                }
                var singleBillOfMaterial = new StockTransferSingleViewModel();
                singleBillOfMaterial.Id = stockTransfer.Id;
                updateId = stockTransfer.Id;
                singleBillOfMaterial.STNo = stockTransfer.STNo;
                singleBillOfMaterial.GodownId = stockTransfer.GodownId;
                singleBillOfMaterial.STDate = stockTransfer.STDate;
                singleBillOfMaterial.Remarks = stockTransfer.Remarks;
                singleBillOfMaterial.Godown = _godown.GetById(singleBillOfMaterial.GodownId);
                updatedRow = base.RenderPartialViewToString("_PartialAddSingleGT", singleBillOfMaterial);
            }
            return Json(new { success = true, id = updateId, updatedRow = updatedRow });
        }

        [HttpPost]
        public ActionResult StockTransferDelete(int id)
        {
            var stockTransfer = _stockTransfer.GetById(id);
            stockTransfer.IsDeleted = true;
            _stockTransfer.Update(stockTransfer);
            _stockTransaction.Delete(x => x.ReferenceId == id && x.Source == "GT");
            return Json(true);
        }
        #endregion

        #region Stock Adjustment
        public JsonResult CheckCodeInStockAdjust(string AdjustmentNo, int? Id)
        {
            var feeterm = new StockAdjustmentMaster();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _stockAdjustment.GetById(x => x.Id == Id);
                if (data.AdjustmentNo.ToLower().Trim() != AdjustmentNo.ToLower().Trim())
                {
                    feeterm =
                        _stockAdjustment.GetById(x => x.AdjustmentNo.ToLower().Trim() == AdjustmentNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _stockAdjustment.GetById(x => x.AdjustmentNo.ToLower().Trim() == AdjustmentNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new StockAdjustmentMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public ActionResult StockAdjustmentList(int pageNo = 1)
        {
            var viewModel = new StockAdjustmentViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            viewModel.StockAdjustment =
                _stockAdjustment.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(
                    x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView("_PartialStockAdjustment", viewModel);
        }

        public ActionResult StockAdjustment(int pageNo = 1)
        {
            var viewModel = new StockAdjustmentViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var data = _stockAdjustment.GetPagedList(x => !x.IsDeleted, x => x.Id, pageNo, SystemControl.PageSize);
            var model = _stockAdjustment.GetAll().LastOrDefault();
            viewModel.StockAdjustmentDisplay = new StockAdjustmenDisplayViewModel();
            viewModel.StockAdjustment = data;
            viewModel.StockAdjustmentDisplay.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            viewModel.StockAdjustmentDisplay.AdjustmentTypeList =
               new SelectList(
                   Enum.GetValues(typeof(KRBAccounting.Enums.TransactionTypeEnum)).Cast
                       <KRBAccounting.Enums.TransactionTypeEnum>().Select(
                           x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value", "Text");
            viewModel.StockAdjustment =
                _stockAdjustment.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(
                    x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            viewModel.DateType = _systemControl.GetAll().FirstOrDefault().DateType;
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.StockAdjustmentDisplay.AdjustmentDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            viewModel.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
            return View(viewModel);

        }

        public ActionResult CheckFiscalyearDateinStockAdjustment(string AdjustmentDate)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(AdjustmentDate).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(AdjustmentDate);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }

        public ActionResult StockAdjustmentAdd()
        {
            var viewModel = new StockAdjustmenDisplayViewModel();
            viewModel.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            return PartialView("StockAdjustmentAddEdit", viewModel);
        }
        [HttpPost]
        public ActionResult StockAdjustmentAdd(StockAdjustmenDisplayViewModel model)
        {

            var newRow = string.Empty;
            if (ModelState.IsValid)
            {
                var stockAdjustment = new StockAdjustmentMaster();
                stockAdjustment.BranchId = CurrentBranchId;
                stockAdjustment.AdjustmentNo = model.AdjustmentNo;
                // stockAdjustment.AdjustmentDate = model.AdjustmentDate;
                stockAdjustment.AdjustmentMiti = model.AdjustmentMiti;
                //stockTransfer.GodownId = model.GodownId;
                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                stockAdjustment.CreatedById = currentUserId;
                stockAdjustment.CreatedDate = DateTime.UtcNow;
                stockAdjustment.FYId = this.FiscalYear.Id;
                //stockTransfer.IsActive = true;
                //stockAdjustment.UpdatedById = currentUserId;
                //stockAdjustment.UpdatedDate = DateTime.UtcNow;
                stockAdjustment.Remarks = model.Remarks;
                // stockAdjustment.FYId = FiscalYear.Id;
                stockAdjustment.IsDeleted = false;
                stockAdjustment.CurCode = model.CurCode;
                stockAdjustment.CurRate = model.CurRate;
                if (base.SystemControl.DateType == 1)
                {
                    stockAdjustment.AdjustmentDate = model.AdjustmentDate.ToDate();
                    stockAdjustment.AdjustmentMiti = NepaliDateService.NepaliDate(stockAdjustment.AdjustmentDate).Date;
                }
                else
                {
                    stockAdjustment.AdjustmentMiti = model.AdjustmentDate;
                    stockAdjustment.AdjustmentDate = NepaliDateService.NepalitoEnglishDate(model.AdjustmentDate);
                }
                _stockAdjustment.Add(stockAdjustment);

                var sno = 1;
                foreach (var item in model.StockAdjustmentDetailAddViewModels.Where(x => x.GodownId != null && x.ProductId != 0 && x.Amount != 0))
                {
                    var stockAdjustmentDetail = new StockAdjustmentDetail();
                    stockAdjustmentDetail.GodownId = Convert.ToInt32(item.GodownId);
                    stockAdjustmentDetail.AdjustmentId = stockAdjustment.Id;
                    stockAdjustmentDetail.AdjustmentType = item.Adjustment;
                    stockAdjustmentDetail.ProductCode = item.ProductId;
                    stockAdjustmentDetail.Qty = Convert.ToDecimal(item.Quantity);
                    stockAdjustmentDetail.Rate = Convert.ToDecimal(item.Rate);
                    stockAdjustmentDetail.SNo = sno;
                    stockAdjustmentDetail.NetAmt = Convert.ToDecimal(item.Amount);
                    stockAdjustmentDetail.UnitId = item.UnitId;

                    var unitCode = _unitRepository.GetById(item.UnitId).Code;

                    _stockAdjustmentDetail.Add(stockAdjustmentDetail);


                    var stockTransaction = new StockTransaction();
                    stockTransaction.Unit = unitCode;
                    stockTransaction.CurrRate = 1;
                    stockTransaction.NetAmt = Convert.ToDecimal(item.Amount);
                    stockTransaction.Rate = Convert.ToDecimal(item.Rate);
                    stockTransaction.Godown = Convert.ToString(item.GodownId);
                    stockTransaction.ProductCode = stockAdjustmentDetail.ProductCode;
                    stockTransaction.Quantity = stockAdjustmentDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(item.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = "SA";
                    stockTransaction.VNo = model.AdjustmentNo;
                    stockTransaction.ReferenceId = stockAdjustment.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.StockVal = 0;
                    stockTransaction.StockQty = Convert.ToDecimal(item.Quantity);
                    stockTransaction.CurrCode = model.CurCode;
                    stockTransaction.CurrRate = model.CurRate;
                    stockTransaction.TermAmt = 0;

                    if (stockAdjustmentDetail.AdjustmentType == "In")
                    {
                        stockTransaction.TransactionType =
                            StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                    }
                    else
                    {
                        stockTransaction.TransactionType =
                            StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    }
                    stockTransaction.VMiti = stockAdjustment.AdjustmentMiti;
                    stockTransaction.VDate = stockAdjustment.AdjustmentDate;
                    stockTransaction.BranchId = CurrentBranchId;
                    stockTransaction.Godown = Convert.ToString(stockAdjustmentDetail.GodownId);
                    _stockTransaction.Add(stockTransaction);

                    sno++;

                }
                var singleGodownTransfer = new StockAdjustmentSingleViewModel();
                singleGodownTransfer.Id = stockAdjustment.Id;
                singleGodownTransfer.STNo = stockAdjustment.AdjustmentNo;
                //singleGodownTransfer.GodownId = stockAdjustment.GodownId;
                singleGodownTransfer.STDate = stockAdjustment.AdjustmentDate;
                singleGodownTransfer.Remarks = stockAdjustment.Remarks;
                //singleGodownTransfer.Godown = _godown.GetById(singleGodownTransfer.GodownId);
                newRow = base.RenderPartialViewToString("_PartialAddSingleSA", singleGodownTransfer);
            }
            return Json(new { success = true, newrow = newRow });
        }
        public ActionResult StockAdjustmentEdit(int id)
        {
            var data = _stockAdjustment.GetById(id);
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var adjustmentTypeList =
     new SelectList(
         Enum.GetValues(typeof(KRBAccounting.Enums.TransactionTypeEnum)).Cast
             <KRBAccounting.Enums.TransactionTypeEnum>().Select(
                 x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value", "Text");
            var productName = string.Empty;
            // var godownFrom = string.Empty;
            var godown = string.Empty;
            var stockAdjustmentDetailList = new List<StockAdjustmentDetailAddViewModel>();
            foreach (var item in data.StockAdjsutmentDetails)
            {
                productName = _product.GetById(item.ProductCode).Name;
                int godownId = Convert.ToInt32(item.GodownId);
                godown = _godown.GetById(godownId).Name;
                var viewModelStDetail = new StockAdjustmentDetailAddViewModel()
                {
                    Godown = godown,
                    GodownId = item.GodownId,
                    Quantity = item.Qty,
                    Adjustment = item.AdjustmentType,
                    StockAdjustmentId = item.AdjustmentId,
                    Rate = item.Rate,
                    UnitId = item.UnitId,
                    ProductId = item.ProductCode,
                    ProductName = productName,
                    Amount = item.NetAmt,
                };
                viewModelStDetail.UnitList = unitSelectList;
                viewModelStDetail.AdjustmentTypeList = new SelectList(
         Enum.GetValues(typeof(KRBAccounting.Enums.TransactionTypeEnum)).Cast
             <KRBAccounting.Enums.TransactionTypeEnum>().Select(
                 x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value", "Text", viewModelStDetail.Adjustment);
                stockAdjustmentDetailList.Add(viewModelStDetail);
            }
            //godownFrom = _godown.GetById(x => x.Id == data.GodownId).Name;
            var viewModel = new StockAdjustmenDisplayViewModel()
            {
                AdjustmentNo = data.AdjustmentNo,
                //Godown = godownFrom,
                // GodownId = data.GodownId,
                // AdjustmentDate = data.AdjustmentDate,
                AdjustmentMiti = data.AdjustmentMiti,
                StockAdjustmentDetailAddViewModels = stockAdjustmentDetailList,
                UnitList = unitSelectList,
                Remarks = data.Remarks,
                Id = data.Id
            };
            if (base.SystemControl.DateType == 1)
            {
                viewModel.AdjustmentDate = data.AdjustmentDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.AdjustmentDate = data.AdjustmentMiti;
            }
            if (!string.IsNullOrEmpty(data.CurCode))
            {
                viewModel.CurCode = data.CurCode;
                viewModel.CurRate = data.CurRate;

            }
            viewModel.TotalQty = data.StockAdjsutmentDetails.Sum(x => x.Qty);
            viewModel.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();
            return PartialView("StockAdjustmentAddEdit", viewModel);
        }
        [HttpPost]
        public ActionResult StockAdjustmentEdit(StockAdjustmenDisplayViewModel model)
        {
            var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
            var updatedRow = string.Empty;
            int updateId = 0;
            if (ModelState.IsValid)
            {
                var stockAdjustment = _stockAdjustment.GetById(model.Id);
                stockAdjustment.BranchId = CurrentBranchId;
                stockAdjustment.AdjustmentNo = model.AdjustmentNo;
                // stockAdjustment.AdjustmentDate = model.AdjustmentDate;
                stockAdjustment.AdjustmentMiti = model.AdjustmentMiti;
                //stockAdjustment.GodownId = model.GodownId;
                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                stockAdjustment.CreatedById = currentUserId;
                stockAdjustment.CreatedDate = DateTime.UtcNow;
                stockAdjustment.Remarks = model.Remarks;
                stockAdjustment.IsDeleted = false;
                stockAdjustment.FYId = this.FiscalYear.Id;
                stockAdjustment.CurCode = model.CurCode;
                stockAdjustment.CurRate = model.CurRate;
                if (base.SystemControl.DateType == 1)
                {
                    stockAdjustment.AdjustmentDate = Convert.ToDateTime(model.AdjustmentDate);
                    stockAdjustment.AdjustmentMiti = NepaliDateService.NepaliDate(stockAdjustment.AdjustmentDate).Date;
                }
                else
                {
                    stockAdjustment.AdjustmentMiti = model.AdjustmentDate;
                    stockAdjustment.AdjustmentDate = NepaliDateService.NepalitoEnglishDate(model.AdjustmentDate);
                }
                _stockAdjustment.Update(stockAdjustment);

                var sno = 1;
                _stockAdjustmentDetail.Delete(x => x.AdjustmentId == stockAdjustment.Id);
                _stockTransaction.Delete(x => x.ReferenceId == model.Id);

                foreach (var item in model.StockAdjustmentDetailAddViewModels.Where(x => x.GodownId != null && x.ProductId != 0 && x.Amount != 0))
                {
                    var stockAdjustmentDetail = new StockAdjustmentDetail();
                    stockAdjustmentDetail.GodownId = Convert.ToInt32(item.GodownId);
                    stockAdjustmentDetail.AdjustmentId = stockAdjustment.Id;
                    stockAdjustmentDetail.AdjustmentType = item.Adjustment;
                    stockAdjustmentDetail.ProductCode = item.ProductId;
                    stockAdjustmentDetail.Qty = Convert.ToDecimal(item.Quantity);
                    stockAdjustmentDetail.Rate = Convert.ToDecimal(item.Rate);
                    stockAdjustmentDetail.SNo = sno;
                    stockAdjustmentDetail.NetAmt = Convert.ToDecimal(item.Amount);
                    stockAdjustmentDetail.UnitId = item.UnitId;

                    var unitCode = _unitRepository.GetById(item.UnitId).Code;
                    stockAdjustmentDetail.UnitId = item.UnitId;
                    _stockAdjustmentDetail.Add(stockAdjustmentDetail);


                    //Stock Transaction
                    var stockTransaction = new StockTransaction();
                    stockTransaction.Unit = unitCode;
                    stockTransaction.CurrRate = 1;
                    stockTransaction.NetAmt = Convert.ToDecimal(item.Amount);
                    stockTransaction.Rate = Convert.ToDecimal(item.Rate);
                    stockTransaction.Godown = Convert.ToString(item.GodownId);
                    stockTransaction.ProductCode = stockAdjustmentDetail.ProductCode;
                    stockTransaction.Quantity = stockAdjustmentDetail.Qty;
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = "SA";
                    stockTransaction.VNo = model.AdjustmentNo;
                    stockTransaction.ReferenceId = stockAdjustment.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.StockVal = 0;
                    stockTransaction.StockQty = Convert.ToDecimal(item.Quantity);
                    stockTransaction.TermAmt = 0;
                    stockTransaction.CurrCode = model.CurCode;
                    stockTransaction.CurrRate = model.CurRate;
                    if (stockAdjustmentDetail.AdjustmentType == "In")
                    {
                        stockTransaction.TransactionType =
                            StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                    }
                    else
                    {
                        stockTransaction.TransactionType =
                            StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    }
                    stockTransaction.VMiti = stockAdjustment.AdjustmentMiti;
                    stockTransaction.VDate = stockAdjustment.AdjustmentDate;
                    stockTransaction.BranchId = CurrentBranchId;
                    stockTransaction.Godown = Convert.ToString(stockAdjustmentDetail.GodownId);
                    _stockTransaction.Add(stockTransaction);

                    sno++;

                }
                var singleGodownTransfer = new StockAdjustmentSingleViewModel();
                singleGodownTransfer.Id = stockAdjustment.Id;
                singleGodownTransfer.STNo = stockAdjustment.AdjustmentNo;
                //singleGodownTransfer.GodownId = stockAdjustment.GodownId;
                singleGodownTransfer.STDate = stockAdjustment.AdjustmentDate;
                singleGodownTransfer.Remarks = stockAdjustment.Remarks;
                updateId = stockAdjustment.Id;
                //singleGodownTransfer.Godown = _godown.GetById(singleGodownTransfer.GodownId);
                updatedRow = base.RenderPartialViewToString("_PartialAddSingleSA", singleGodownTransfer);
            }
            return Json(new { success = true, id = updateId, updatedRow = updatedRow });
        }
        [HttpPost]
        public ActionResult StockAdjustmentDelete(int id)
        {
            var stockAdjustment = _stockAdjustment.GetById(id);
            stockAdjustment.IsDeleted = true;
            _stockAdjustment.Update(stockAdjustment);
            _stockTransaction.Delete(x => x.ReferenceId == id && x.Source == "SA");
            return Json(true);
        }
        #endregion

        #region Expiry / Breakage

        public JsonResult CheckCodeInExpiryBreakage(string ExpBrkNo, int? Id)
        {
            var feeterm = new ExpiryBreakageMaster();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            if (Id.HasValue && Id != 0)
            {
                var data = _expiryBreakageMaster.GetById(x => x.Id == Id);
                if (data.ExpBrkNo.ToLower().Trim() != ExpBrkNo.ToLower().Trim())
                {
                    feeterm =
                        _expiryBreakageMaster.GetById(x => x.ExpBrkNo.ToLower().Trim() == ExpBrkNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                feeterm = _expiryBreakageMaster.GetById(x => x.ExpBrkNo.ToLower().Trim() == ExpBrkNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            if (feeterm == null)
            {
                feeterm = new ExpiryBreakageMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExpBrkList(int pageNo = 1)
        {
            var viewModel = new ExpBrkViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            viewModel.ExpiryBreakage =
                _expiryBreakageMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(
                    x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView("_PartialExpBrk", viewModel);
        }
        public ActionResult ExpBrk(int pageNo = 1)
        {
            var viewModel = new ExpBrkViewModel();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            var data = _expiryBreakageMaster.GetPagedList(x => !x.IsDeleted && x.FYId == fiscalYear.Id, x => x.Id, pageNo, SystemControl.PageSize);
            var model = _expiryBreakageMaster.GetAll().LastOrDefault();
            viewModel.ExpBrkDisplay = new ExpBrkDisplayViewModel();
            viewModel.ExpiryBreakage = data;
            viewModel.ExpBrkDisplay.UnitList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            viewModel.ExpBrkDisplay.TypeList =
               new SelectList(
                   Enum.GetValues(typeof(KRBAccounting.Enums.ExpBrkType)).Cast
                       <KRBAccounting.Enums.ExpBrkType>().Select(
                           x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value", "Text");
            viewModel.ExpiryBreakage =
                _expiryBreakageMaster.Filter(x => x.BranchId == CurrentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(
                    x => x.Id).ToPagedList(pageNo, base.SystemControl.PageSize);
            viewModel.DateType = _systemControl.GetAll().FirstOrDefault().DateType;
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.ExpBrkDisplay.Date = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {

                if (model != null)
                {
                    viewModel.ExpBrkDisplay.Date = base.SystemControl.DateType == 1 ? model.Date.ToShortDateString() : model.Miti;
                }
                else
                {
                    viewModel.ExpBrkDisplay.Date = base.SystemControl.DateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            viewModel.EntryControl = _entryControlInventory.GetAll().FirstOrDefault();

            return View(viewModel);

        }

        public ActionResult CheckFiscalyearDateinExpBrk(string Date)
        {
            try
            {
                var fiscalYearDate = _fiscalYear.GetById(x => x.IsDefalut);
                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    currentDate = Convert.ToDateTime(Date).Date;
                }
                else
                {
                    currentDate = NepaliDateService.NepalitoEnglishDate(Date);
                    //currentDate = NepaliDateService.NepaliDate(english).Date;
                }
                    if (currentDate >= fiscalYearDate.StartDate.Date && currentDate <= fiscalYearDate.EndDate.Date)
                    {
                        return Content("True");
                    }
                    else
                    {
                        return
                            Content(
                                "Enter Date between " + fiscalYearDate.StartDate.ToShortDateString() + " and " +
                                fiscalYearDate.EndDate.ToShortDateString());
                    }
                
            }
            catch (Exception e)
            {
                return Content("False");
            }

        }

        [HttpPost]
        public ActionResult ExpBrkAdd(ExpBrkDisplayViewModel model)
        {
            var newRow = string.Empty;
            if (ModelState.IsValid)
            {
                var expBrk = new ExpiryBreakageMaster();
                expBrk.BranchId = CurrentBranchId;
                expBrk.ExpBrkNo = model.ExpBrkNo;
                //expBrk.Date = model.Date;
                expBrk.Miti = model.DateMiti;
                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                expBrk.CreatedById = currentUserId;
                expBrk.CreatedDate = DateTime.UtcNow;
                expBrk.Type = model.Type;
                expBrk.Remarks = model.Remarks;
                // stockAdjustment.FYId = FiscalYear.Id;
                expBrk.IsDeleted = false;
                expBrk.FYId = this.FiscalYear.Id;
                expBrk.CurCode = model.CurCode;
                expBrk.CurRate = model.CurRate;
                if (base.SystemControl.DateType == 1)
                {
                    expBrk.Date = model.Date.ToDate();
                    expBrk.Miti = NepaliDateService.NepaliDate(expBrk.Date).Date;
                }
                else
                {
                    expBrk.Miti = model.Date;
                    expBrk.Date = NepaliDateService.NepalitoEnglishDate(model.Date);
                }
                _expiryBreakageMaster.Add(expBrk);

                var sno = 1;
                foreach (var item in model.ExpBrkDetailAddViewModels.Where(x => x.GodownId != null && x.ProductId != 0 && x.Amount != 0))
                {
                    var expDetail = new ExpiryBreakageDetail();
                    expDetail.GodownId = Convert.ToInt32(item.GodownId);
                    expDetail.ExpBrkId = expBrk.Id;
                    expDetail.ProductCode = item.ProductId;
                    expDetail.Qty = Convert.ToDecimal(item.Quantity);
                    expDetail.Rate = Convert.ToDecimal(item.Rate);
                    expDetail.SNo = sno;
                    expDetail.NetAmt = Convert.ToDecimal(item.Amount);
                    var unitCode = _unitRepository.GetById(item.UnitId).Code;
                    expDetail.UnitId = item.UnitId;
                    _expiryBreakageDetail.Add(expDetail);

                    var stockTransaction = new StockTransaction();
                    stockTransaction.Unit = unitCode;
                    stockTransaction.CurrRate = model.CurRate;
                    stockTransaction.CurrCode = model.CurCode;
                    stockTransaction.NetAmt = Convert.ToDecimal(item.Amount);
                    stockTransaction.Rate = Convert.ToDecimal(item.Rate);
                    stockTransaction.Godown = Convert.ToString(item.GodownId);
                    stockTransaction.ProductCode = expDetail.ProductCode;
                    stockTransaction.Quantity = expDetail.Qty;
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = "SEB";
                    stockTransaction.VMiti = expBrk.Miti;
                    stockTransaction.VDate = expBrk.Date;
                    stockTransaction.VNo = model.ExpBrkNo;
                    stockTransaction.ReferenceId = expBrk.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.StockVal = 0;
                    stockTransaction.StockQty = Convert.ToDecimal(item.Quantity);
                    stockTransaction.TermAmt = 0;

                    stockTransaction.TransactionType =
                             StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    stockTransaction.BranchId = CurrentBranchId;
                    stockTransaction.Godown = Convert.ToString(expDetail.GodownId);
                    _stockTransaction.Add(stockTransaction);
                    sno++;
                }
                var singleGodownTransfer = new ExpBrkSingleViewModel();
                singleGodownTransfer.Id = expBrk.Id;
                singleGodownTransfer.STNo = expBrk.ExpBrkNo;
                //singleGodownTransfer.GodownId = stockAdjustment.GodownId;
                singleGodownTransfer.STDate = expBrk.Date;
                singleGodownTransfer.Type = expBrk.Type;
                singleGodownTransfer.Remarks = expBrk.Remarks;
                //singleGodownTransfer.Godown = _godown.GetById(singleGodownTransfer.GodownId);
                newRow = base.RenderPartialViewToString("_PartialAddSingleSEB", singleGodownTransfer);
            }
            return Json(new { success = true, newrow = newRow });
        }
        public ActionResult ExpBrkEdit(int id)
        {
            var data = _expiryBreakageMaster.GetById(id);
            var unitSelectList = new SelectList(_unitRepository.GetMany(x => !x.IsDeleted), "Id", "Code");
            var productName = string.Empty;
            // var godownFrom = string.Empty;
            var godown = string.Empty;
            var detailList = new List<ExpBrkDetailAddViewModel>();
            foreach (var item in data.ExpiryBreakageDetails)
            {
                productName = _product.GetById(item.ProductCode).Name;
                int godownId = Convert.ToInt32(item.GodownId);
                godown = _godown.GetById(godownId).Name;
                var viewModelStDetail = new ExpBrkDetailAddViewModel()
                {
                    Godown = godown,
                    GodownId = item.GodownId,
                    Quantity = item.Qty,
                    ExpBrkId = item.ExpBrkId,
                    Rate = item.Rate,
                    UnitId = item.UnitId,
                    ProductId = item.ProductCode,
                    ProductName = productName,
                    Amount = item.NetAmt,
                };
                viewModelStDetail.UnitList = unitSelectList;
                detailList.Add(viewModelStDetail);

            }
            var typeList =
                     new SelectList(
                         Enum.GetValues(typeof(KRBAccounting.Enums.ExpBrkType)).Cast
                             <KRBAccounting.Enums.ExpBrkType>().Select(
                                 x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value", "Text", data.Type);

            var viewModel = new ExpBrkDisplayViewModel()
            {
                ExpBrkNo = data.ExpBrkNo,
                //Godown = godownFrom,
                // GodownId = data.GodownId,
                // Date = data.Date,
                DateMiti = data.Miti,
                ExpBrkDetailAddViewModels = detailList,
                UnitList = unitSelectList,
                Remarks = data.Remarks,
                Type = data.Type,
                Id = data.Id,
                TypeList = typeList

            };
            if (base.SystemControl.DateType == 1)
            {
                viewModel.Date = data.Date.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.Date = data.Miti;
            }
            if (!string.IsNullOrEmpty(data.CurCode))
            {
                viewModel.CurCode = data.CurCode;
                viewModel.CurRate = data.CurRate;

            }
            viewModel.TotalQty = data.ExpiryBreakageDetails.Sum(x => x.Qty);
            return PartialView("ExpBrkAddEdit", viewModel);
        }
        [HttpPost]
        public ActionResult ExpBrkEdit(ExpBrkDisplayViewModel model)
        {

            var updatedRow = string.Empty;
            int updateId = 0;
            if (ModelState.IsValid)
            {
                var expBrk = _expiryBreakageMaster.GetById(model.Id);
                expBrk.BranchId = CurrentBranchId;
                expBrk.ExpBrkNo = model.ExpBrkNo;
                //expBrk.Date = model.Date;
                expBrk.Miti = model.DateMiti;
                var currentUserId = _authentication.GetAuthenticatedUser().Id;
                expBrk.CreatedById = currentUserId;
                expBrk.CreatedDate = DateTime.UtcNow;
                expBrk.Type = model.Type;
                expBrk.Remarks = model.Remarks;
                // stockAdjustment.FYId = FiscalYear.Id;
                expBrk.IsDeleted = false;
                expBrk.FYId = FiscalYear.Id;
                expBrk.CurCode = model.CurCode;
                expBrk.CurRate = model.CurRate;
                if (base.SystemControl.DateType == 1)
                {
                    expBrk.Date = Convert.ToDateTime(model.Date);
                    expBrk.Miti = NepaliDateService.NepaliDate(expBrk.Date).Date;
                }
                else
                {
                    expBrk.Miti = model.Date;
                    expBrk.Date = NepaliDateService.NepalitoEnglishDate(model.Date);
                }
                _expiryBreakageMaster.Update(expBrk);


                var sno = 1;
                _expiryBreakageDetail.Delete(x => x.ExpBrkId == model.Id);
                _stockTransaction.Delete(x => x.ReferenceId == model.Id);

                foreach (var item in model.ExpBrkDetailAddViewModels.Where(x => x.GodownId != null && x.ProductId != 0 && x.Amount != 0))
                {
                    var expDetail = new ExpiryBreakageDetail();
                    expDetail.GodownId = Convert.ToInt32(item.GodownId);
                    expDetail.ExpBrkId = expBrk.Id;
                    expDetail.ProductCode = item.ProductId;
                    expDetail.Qty = Convert.ToDecimal(item.Quantity);
                    expDetail.Rate = Convert.ToDecimal(item.Rate);
                    expDetail.SNo = sno;
                    expDetail.NetAmt = Convert.ToDecimal(item.Amount);
                    var unitCode = _unitRepository.GetById(item.UnitId).Code;
                    expDetail.UnitId = item.UnitId;
                    _expiryBreakageDetail.Add(expDetail);


                    var stockTransaction = new StockTransaction();
                    stockTransaction.Unit = unitCode;
                    stockTransaction.CurrRate = model.CurRate;
                    stockTransaction.CurrCode = model.CurCode;
                    stockTransaction.NetAmt = Convert.ToDecimal(item.Amount);
                    stockTransaction.Rate = Convert.ToDecimal(item.Rate);
                    stockTransaction.Godown = Convert.ToString(item.GodownId);
                    stockTransaction.ProductCode = expDetail.ProductCode;
                    stockTransaction.Quantity = expDetail.Qty;
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = "SEB";
                    stockTransaction.VMiti = expBrk.Miti;
                    stockTransaction.VDate = expBrk.Date;
                    stockTransaction.VNo = model.ExpBrkNo;
                    stockTransaction.ReferenceId = expBrk.Id;
                    stockTransaction.FYId = FiscalYear.Id;
                    stockTransaction.StockVal = 0;
                    stockTransaction.StockQty = Convert.ToDecimal(item.Quantity);
                    stockTransaction.TermAmt = 0;

                    stockTransaction.TransactionType =
                             StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();


                    stockTransaction.BranchId = CurrentBranchId;
                    stockTransaction.Godown = Convert.ToString(expDetail.GodownId);
                    _stockTransaction.Add(stockTransaction);

                    sno++;

                }
                var singleGodownTransfer = new ExpBrkSingleViewModel();
                singleGodownTransfer.Id = expBrk.Id;
                singleGodownTransfer.STNo = expBrk.ExpBrkNo;

                singleGodownTransfer.STDate = expBrk.Date;
                singleGodownTransfer.Type = expBrk.Type;
                singleGodownTransfer.Remarks = expBrk.Remarks;
                updateId = model.Id;
                updatedRow = base.RenderPartialViewToString("_PartialAddSingleSEB", singleGodownTransfer);



            }
            return Json(new { success = true, id = updateId, updatedRow = updatedRow });
        }
        [HttpPost]
        public ActionResult ExpBrkDelete(int id)
        {
            var expBrk = _expiryBreakageMaster.GetById(id);
            expBrk.IsDeleted = true;
            _expiryBreakageMaster.Update(expBrk);
            _stockTransaction.Delete(x => x.ReferenceId == id && x.Source == "SEB");
            return Json(true);
        }
        #endregion
        #endregion

        public void DisplayModelStateErrors()
        {
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                if (modelState.Errors.Any())
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult CashBankVoucherPrint(int id)
        {
            var viewModel = this.CreateReportViewModel<CashBankVoucherPrintViewModel>("Cash Bank Voucher");
            viewModel.CashBankMaster = _cashBankMaster.GetById(id);

            return View("CashBankVoucherPrint", "_LayoutPrint", viewModel);
        }

        public ActionResult JournalVoucherPrint(int id)
        {
            var viewModel = this.CreateReportViewModel<JournalVoucherPrintViewModel>("Journal Voucher");
            viewModel.JournalVoucher = _journalVoucher.GetById(id);
            return View("JournalVoucherPrint", "_LayoutPrint", viewModel);
        }

        public ActionResult PdfCashBankVoucher(int id)
        {
            var viewModel = this.CreateReportViewModel<CashBankVoucherPrintViewModel>("Cash Bank Voucher");
            viewModel.CashBankMaster = _cashBankMaster.GetById(id);

            return this.ViewPdf("", "PdfCashBankVoucher", viewModel);

        }
        public ActionResult ExcelCashBankVoucher(int id)
        {
            var viewModel = this.CreateReportViewModel<CashBankVoucherPrintViewModel>("Cash Bank Voucher");
            viewModel.CashBankMaster = _cashBankMaster.GetById(id);

            return this.ViewExcel("", "ExcelCashBankVoucher", viewModel);

        }
        public ActionResult PdfJournalVoucher(int id)
        {
            var viewModel = this.CreateReportViewModel<JournalVoucherPrintViewModel>("Journal Voucher");
            viewModel.JournalVoucher = _journalVoucher.GetById(id);

            return this.ViewPdf("", "PdfJournalVoucher", viewModel);

        }
        public ActionResult ExcelJournalVoucher(int id)
        {
            var viewModel = this.CreateReportViewModel<JournalVoucherPrintViewModel>("Journal Voucher");
            viewModel.JournalVoucher = _journalVoucher.GetById(id);

            return this.ViewExcel("", "ExcelJournalVoucher", viewModel);

        }

    }
    public class BillingTermTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
