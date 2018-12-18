using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.ViewModels.ARAP;
using KRBAccounting.Web.ViewModels.Inventory;

namespace KRBAccounting.Web.Controllers
{

    public class InventoryController : BaseController
    {
        private readonly IProductRepository _product;
        private readonly IUnitRepository _unitRepository;
        private readonly IGodownRepository _godown;
        private readonly ISystemControlRepository _systemControl;
        private readonly ICompanyInfoRepository _companyInfo;
        private readonly IFormsAuthenticationService _authentication;
        //
        // GET: /Inventory/


        public InventoryController(IProductRepository productRepository,
            IUnitRepository unitRepository, IGodownRepository godownRepository,
            ISystemControlRepository systemControlRepository,
            ICompanyInfoRepository company)
        {
            _product = productRepository;
            _unitRepository = unitRepository;
            _godown = godownRepository;
            _systemControl = systemControlRepository;
            _companyInfo = company;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Inventory);
        }
        public ActionResult DashBoard()
        {
            return View();
        }
         [CheckPermission(Permissions = "Navigate", Module = "INVSL")]
        public ActionResult StockLedger()
        {
            var viewModel = base.CreateViewModel<StockLedgerFormViewModel>();
            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code").ToList();
            viewModel.UnitList.Insert(0, (new SelectListItem { Text = "Default", Value = "0" }));
            viewModel.GodownList = _godown.GetMany(x => !x.IsDeleted);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StockLedger(string products, string StartDate, string EndDate, bool Summary, int unit, bool? Godown, string GodownId, bool? Batch, bool? DateShow, bool? ZeroBalance, bool? WithValue)
        {
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            var startDate = string.Empty;
            var endDate = string.Empty;
            string title = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                startDate = StartDate;
                endDate = EndDate;
                if (DateShow == true)
                {
                    startReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(StartDate)).Date;
                    endReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(EndDate)).Date;
                }
                else
                {
                    startReportDate = StartDate;
                    endReportDate = EndDate;
                }

            }
            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(EndDate).ToShortDateString();
                if (DateShow == true)
                {
                    startReportDate = startDate;
                    endReportDate = endDate;
                }
                else
                {
                    startReportDate = StartDate;
                    endReportDate = EndDate;
                }
            }
            var reportDate = "Report Date From " + startReportDate + " To " + endReportDate;

            if (Summary)
            {
                if (WithValue == true)
                    title = "Stock Ledger Summary - With Value";
                else
                    title = "Stock Ledger Summary";
                
                var viewModel = base.CreateReportViewModel<StockLedgerSummaryViewModel>(title, reportDate);
                if (WithValue == true)
                    viewModel.WithValue = 1;
                else
                    viewModel.WithValue = 0;

                if (string.IsNullOrEmpty(products))
                {
                    products = "0";
                }

                viewModel.StockLedgerSummaries = StoredProcedureService.GetStockLedgerSummary(startDate, endDate, products, base.FiscalYear.Id, viewModel.WithValue);
                if (unit != 0)
                {
                    foreach (var item in viewModel.StockLedgerSummaries)
                    {
                        var unitcode = _unitRepository.GetById(unit).Code;
                        var convertedUnitBalance = UtilityService.UnitConversion(item.ProductId, item.BalanceQty,
                                                                                 item.UnitShortName, unitcode);
                        var convertedUnitReceived = UtilityService.UnitConversion(item.ProductId, item.ReceivedQty,
                                                                                 item.UnitShortName, unitcode);
                        var convertedUnitDelivered = UtilityService.UnitConversion(item.ProductId, item.DeliveredQty,
                                                                            item.UnitShortName, unitcode);
                        item.BalanceQty = convertedUnitBalance;
                        item.DeliveredQty = convertedUnitDelivered;
                        item.ReceivedQty = convertedUnitReceived;
                        item.UnitShortName = UtilityService.GetPorductUnitCode(item.ProductId, item.UnitShortName,
                                                                               unitcode);
                    }
                }

                viewModel.FYId = base.FiscalYear.Id;
                Session["ReportType"] = "1";
                Session["ReportModel"] = viewModel;
                return PartialView("_PartialStockLedgerSummaryReport", viewModel);
            }
            if (Godown.HasValue && Godown == true)
            {
                int EnableBatch = 0;

                if (Batch.HasValue && Batch == true)
                {
                    title = "Stock Ledger (Godown) - Batchwise";
                    EnableBatch = 1;
                }
                else
                {
                    title = "Stock Ledger (Godown)";
                }
                var viewModel = base.CreateReportViewModel<StockLedgerViewModel>(title, reportDate);
                viewModel.Batch = Batch;
                //viewModel.Godown = Godown;
                if (WithValue == true)
                    viewModel.WithValue = 1;
                else
                    viewModel.WithValue = 0;

                viewModel.ProductCode = String.Empty;
                GodownId = GodownId == string.Empty ? "0" : GodownId;
                viewModel.Godowns = StoredProcedureService.GetStockLedgerGodowns(startDate, endDate, products, GodownId);

                var productOpenings = StoredProcedureService.GetStockProductOpening(startDate, products, base.FiscalYear.Id, EnableBatch);
                var productLedgers = StoredProcedureService.GetStockProductLedger(startDate, endDate, products, base.FiscalYear.Id, EnableBatch, viewModel.WithValue);
                viewModel.ProductOpenings = productOpenings;
                viewModel.ProcuctLedgers = productLedgers;
                viewModel.StartDate = startDate;
                viewModel.EndDate = endDate;
                viewModel.ProductIds = products;
                if (unit != 0)
                {
                    foreach (var item in viewModel.ProcuctLedgers)
                    {
                        var productUnitId = _product.GetById(item.ProductCode).Unit;
                        var productIntId = Convert.ToInt32(productUnitId);
                        var productcode = _unitRepository.GetById(productIntId).Code;
                        var unitcode = _unitRepository.GetById(unit).Code;
                        var convertedUnitReceived = UtilityService.UnitConversion(item.ProductCode, item.ReceivedQty,
                                                                            productcode, unitcode);
                        var convertedUnitDelivered = UtilityService.UnitConversion(item.ProductCode, item.DeliveredQty,
                                                                            productcode, unitcode);
                        item.DeliveredQty = convertedUnitDelivered;
                        item.ReceivedQty = convertedUnitReceived;
                        viewModel.ProductCode = unitcode;
                    }
                }
                if (DateShow == true)
                {
                    if (viewModel.Datetype == 1)
                        viewModel.Datetype = 2;
                    else
                        viewModel.Datetype = 1;
                }
                viewModel.FYId = base.FiscalYear.Id;
                Session["ReportType"] = "2";
                Session["ReportModel"] = viewModel;
                return PartialView("_PartialStockLedgerGodownReport", viewModel);
            }
            else
            {
                int EnableBatch = 0;
                if (Batch.HasValue && Batch == true)
                {
                    title = "Stock Ledger Details - Batchwise";
                    EnableBatch = 1;
                }
                else
                {
                    if (WithValue == true)
                        title = "Stock Ledger Details - With Value"; 
                    else
                        title = "Stock Ledger Details";
                }
                var viewModel = base.CreateReportViewModel<StockLedgerViewModel>(title, reportDate);
                if (WithValue == true)
                    viewModel.WithValue = 1;
                else
                    viewModel.WithValue = 0;

                viewModel.Batch = Batch;
                viewModel.ProductCode = String.Empty;
                viewModel.Products = StoredProcedureService.GetStockLedgerProduct(startDate, endDate, products, EnableBatch,"");
                var productOpenings = StoredProcedureService.GetStockProductOpening(startDate, products, base.FiscalYear.Id, EnableBatch);
                var productLedgers = StoredProcedureService.GetStockProductLedger(startDate, endDate, products, base.FiscalYear.Id, EnableBatch, viewModel.WithValue);
                viewModel.ProductOpenings = productOpenings;
                viewModel.ProcuctLedgers = productLedgers;
                if (unit != 0)
                {
                    foreach (var item in viewModel.ProcuctLedgers)
                    {
                        var productUnitId = _product.GetById(item.ProductCode).Unit;
                        var productIntId = Convert.ToInt32(productUnitId);
                        var productcode = _unitRepository.GetById(productIntId).Code;
                        var unitcode = _unitRepository.GetById(unit).Code;
                        var convertedUnitReceived = UtilityService.UnitConversion(item.ProductCode, item.ReceivedQty,
                                                                            productcode, unitcode);
                        var convertedUnitDelivered = UtilityService.UnitConversion(item.ProductCode, item.DeliveredQty,
                                                                            productcode, unitcode);
                        item.DeliveredQty = convertedUnitDelivered;
                        item.ReceivedQty = convertedUnitReceived;
                        viewModel.ProductCode = unitcode;
                    }
                }
                viewModel.Datetype = base.SystemControl.DateType;
                if (DateShow == true)
                {
                    if (viewModel.Datetype == 1)
                        viewModel.Datetype = 2;
                    else
                        viewModel.Datetype = 1;
                }
                viewModel.FYId = base.FiscalYear.Id;
                Session["ReportType"] = "2";
                Session["ReportModel"] = viewModel;
                return PartialView("_PartialStockLedgerReport", viewModel);
            }
        }

        public ActionResult PdfStockLedger()
        {
            int reportType = Convert.ToInt32(Session["ReportType"]);
            if (reportType == 1)
            {
                var view = (StockLedgerSummaryViewModel)Session["ReportModel"];
                return this.ViewPdf("", "Inventory/PdfStockLedgerSummary", view);
            }
            else
            {
                var view = (StockLedgerViewModel)Session["ReportModel"];
                if (view.Godown==null || view.Godown == false)
                {
                    return this.ViewPdf("", "Inventory/PdfStockLedgerReport", view);
                }
                return this.ViewPdf("", "Inventory/PdfStockLedgerGodownReport", view);
            }

        }
        public ActionResult ExcelStockLedger()
        {
            int reportType = Convert.ToInt32(Session["ReportType"]);
            if (reportType == 1)
            {
                var view = (StockLedgerSummaryViewModel)Session["ReportModel"];
                return this.ViewExcel("", "Inventory/ExcelStockLedgerSummary", view);
            }
            else
            {
                var view = (StockLedgerViewModel)Session["ReportModel"];
                if (view.Godown==null ||view.Godown == false)
                {
                    return this.ViewExcel("", "Inventory/ExcelStockLedgerReport", view);
                }
                return this.ViewExcel("", "Inventory/ExcelStockLedgerGodownReport", view);
            }
        }
        [CheckPermission(Permissions = "Navigate", Module = "INGod")]
        public ActionResult GodownInventory()
        {
            var viewModel = base.CreateViewModel<GodownInventoryFormViewModel>();
            //var viewModel = new GodownInventoryFormViewModel();
            viewModel.RptList = new SelectList(
               Enum.GetValues(typeof(GodownInventoryTypeEnum)).Cast
                   <GodownInventoryTypeEnum>().Select(
                       x =>
                       new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
               "Value", "Text");
            viewModel.ReportType = 1;
            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code").ToList();
            viewModel.UnitList.Insert(0, (new SelectListItem { Text = "Default", Value = "0" }));
            viewModel.GodownList = _godown.GetMany(x => !x.IsDeleted);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GodownInventory(string products, string StartDate, string EndDate, int RptType, bool Summary, int unit, bool? Godown, string GodownId, bool? Batch, bool? DateShow, bool? ZeroBalance, bool? WithValue)
        {
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            var startDate = string.Empty;
            var endDate = string.Empty;
            string title = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                startDate = StartDate;
                endDate = EndDate;
                if (DateShow == true)
                {
                    startReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(StartDate)).Date;
                    endReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(EndDate)).Date;
                }
                else
                {
                    startReportDate = StartDate;
                    endReportDate = EndDate;
                }

            }
            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(EndDate).ToShortDateString();
                if (DateShow == true)
                {
                    startReportDate = startDate;
                    endReportDate = endDate;
                }
                else
                {
                    startReportDate = StartDate;
                    endReportDate = EndDate;
                }
            }
            var reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            
            if (Summary)
            {
                    if (RptType == 1)  //for Godown/Product
                    {
                        if (WithValue == true)
                            title = "Stock Ledger Godown/Product Summary - With Value";
                        else
                            title = "Stock Ledger Godown/Product Summary";
                    }
                    else //for Product/Godown
                    {
                        if (WithValue == true)
                            title = "Stock Ledger Product/Godown Summary - With Value";
                        else
                            title = "Stock Ledger Product/Godown Summary";
                    }
                    
                    var viewModel = base.CreateReportViewModel<StockLedgerSummaryViewModel>(title, reportDate);
                   
                    if (WithValue == true)
                        viewModel.WithValue = 1;
                    else
                        viewModel.WithValue = 0;

                    if (string.IsNullOrEmpty(products))
                    {
                        products = "0";
                    }

                    viewModel.StockLedgerSummaries = StoredProcedureService.GetStockLedgerSummary(startDate, endDate,
                                                                                                  products,
                                                                                                  base.FiscalYear.Id,
                                                                                                  viewModel.WithValue);
                    if (unit != 0)
                    {
                        foreach (var item in viewModel.StockLedgerSummaries)
                        {
                            var unitcode = _unitRepository.GetById(unit).Code;
                            var convertedUnitBalance = UtilityService.UnitConversion(item.ProductId, item.BalanceQty,
                                                                                     item.UnitShortName, unitcode);
                            var convertedUnitReceived = UtilityService.UnitConversion(item.ProductId, item.ReceivedQty,
                                                                                      item.UnitShortName, unitcode);
                            var convertedUnitDelivered = UtilityService.UnitConversion(item.ProductId, item.DeliveredQty,
                                                                                       item.UnitShortName, unitcode);
                            item.BalanceQty = convertedUnitBalance;
                            item.DeliveredQty = convertedUnitDelivered;
                            item.ReceivedQty = convertedUnitReceived;
                            item.UnitShortName = UtilityService.GetPorductUnitCode(item.ProductId, item.UnitShortName,
                                                                                   unitcode);
                        }
                    }

                    viewModel.FYId = base.FiscalYear.Id;
                    Session["ReportType"] = "1";
                    Session["ReportModel"] = viewModel;
                    return PartialView("_PartialStockLedgerSummaryReport", viewModel);
                }
                if (Godown.HasValue && Godown == true)
                {
                    int EnableBatch = 0;

                    if (Batch.HasValue && Batch == true)
                    {
                        title = "Stock Ledger Godown/Product Details - Batchwise";
                        EnableBatch = 1;
                    }
                    else
                    {
                        title = "Stock Ledger Godown/Product Details";
                    }
                    var viewModel = base.CreateReportViewModel<GodownInventoryViewModel>(title, reportDate);
                    viewModel.Batch = Batch;
                    //viewModel.Godown = Godown;
                    if (WithValue == true)
                        viewModel.WithValue = 1;
                    else
                        viewModel.WithValue = 0;

                    if (RptType == 1)
                        viewModel.RptType = 1;
                    else
                        viewModel.RptType = 2;

                    viewModel.ProductCode = String.Empty;
                    GodownId = GodownId == string.Empty ? "" : GodownId;
                    viewModel.Godowns = StoredProcedureService.GetStockLedgerGodowns(startDate, endDate, products,
                                                                                     GodownId);

                    var productOpenings = StoredProcedureService.GetStockProductOpening(startDate, products,
                                                                                        base.FiscalYear.Id, EnableBatch);
                    var productLedgers = StoredProcedureService.GetStockProductLedger(startDate, endDate, products,
                                                                                      base.FiscalYear.Id, EnableBatch,
                                                                                      viewModel.WithValue);
                    viewModel.ProductOpenings = productOpenings;
                    viewModel.ProcuctLedgers = productLedgers;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.ProductIds = products;
                    if (unit != 0)
                    {
                        foreach (var item in viewModel.ProcuctLedgers)
                        {
                            var productUnitId = _product.GetById(item.ProductCode).Unit;
                            var productIntId = Convert.ToInt32(productUnitId);
                            var productcode = _unitRepository.GetById(productIntId).Code;
                            var unitcode = _unitRepository.GetById(unit).Code;
                            var convertedUnitReceived = UtilityService.UnitConversion(item.ProductCode, item.ReceivedQty,
                                                                                      productcode, unitcode);
                            var convertedUnitDelivered = UtilityService.UnitConversion(item.ProductCode,
                                                                                       item.DeliveredQty,
                                                                                       productcode, unitcode);
                            item.DeliveredQty = convertedUnitDelivered;
                            item.ReceivedQty = convertedUnitReceived;
                            viewModel.ProductCode = unitcode;
                        }
                    }
                    if (DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    viewModel.FYId = base.FiscalYear.Id;
                    Session["ReportType"] = "2";
                    Session["ReportModel"] = viewModel;
                    return PartialView("_PartialStockLedgerGodownProductReport", viewModel);
                }
                else
                {
                    int EnableBatch = 0;
                    if (Batch.HasValue && Batch == true)
                    {
                        EnableBatch = 1;
                        if (RptType == 1) //for Godown/Product
                        {
                            if (WithValue == true)
                                title = "Stock Ledger Godown/Product Details - Batchwise With Value";
                            else
                                title = "Stock Ledger Godown/Product Details";
                        }
                        else //for Product/Godown
                        {
                            if (WithValue == true)
                                title = "Stock Ledger Product/Godown Details -Batchwise With Value";
                            else
                                title = "Stock Ledger Product/Godown Details";
                        }
                    }
                    else
                    {
                        if (RptType == 1)  //for Godown/Product
                        {
                            if (WithValue == true)
                                title = "Stock Ledger Godown/Product Details - With Value";
                            else
                                title = "Stock Ledger Godown/Product Details";
                        }
                        else //for Product/Godown
                        {
                            if (WithValue == true)
                                title = "Stock Ledger Product/Godown Details - With Value";
                            else
                                title = "Stock Ledger Product/Godown Details";
                        }
                    }
                    var viewModel = base.CreateReportViewModel<GodownInventoryViewModel>(title, reportDate);
                    if (WithValue == true)
                        viewModel.WithValue = 1;
                    else
                        viewModel.WithValue = 0;
                    if (RptType == 1)
                        viewModel.RptType = 1;  
                    else
                        viewModel.RptType = 2;

                    viewModel.Batch = Batch;
                    viewModel.ProductCode = String.Empty;
                    GodownId = GodownId == string.Empty ? "0" : GodownId;
                    viewModel.Godowns = StoredProcedureService.GetStockLedgerGodowns(startDate, endDate, products,
                                                                                     GodownId);
                    viewModel.Products = StoredProcedureService.GetStockLedgerProduct(startDate, endDate, products,
                                                                                      EnableBatch,"0");
                    var productOpenings = StoredProcedureService.GetStockProductOpening(startDate, products,
                                                                                        base.FiscalYear.Id, EnableBatch);
                    var productLedgers = StoredProcedureService.GetStockProductLedger(startDate, endDate, products,
                                                                                      base.FiscalYear.Id, EnableBatch,
                                                                                      viewModel.WithValue);
                    viewModel.ProductOpenings = productOpenings;
                    viewModel.ProcuctLedgers = productLedgers;
                    if (unit != 0)
                    {
                        foreach (var item in viewModel.ProcuctLedgers)
                        {
                            var productUnitId = _product.GetById(item.ProductCode).Unit;
                            var productIntId = Convert.ToInt32(productUnitId);
                            var productcode = _unitRepository.GetById(productIntId).Code;
                            var unitcode = _unitRepository.GetById(unit).Code;
                            var convertedUnitReceived = UtilityService.UnitConversion(item.ProductCode, item.ReceivedQty,
                                                                                      productcode, unitcode);
                            var convertedUnitDelivered = UtilityService.UnitConversion(item.ProductCode,
                                                                                       item.DeliveredQty,
                                                                                       productcode, unitcode);
                            item.DeliveredQty = convertedUnitDelivered;
                            item.ReceivedQty = convertedUnitReceived;
                            viewModel.ProductCode = unitcode;
                        }
                    }
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    viewModel.FYId = base.FiscalYear.Id;
                    Session["ReportType"] = "2";
                    Session["ReportModel"] = viewModel;
                    return PartialView("_PartialStockLedgerGodownProductReport", viewModel);
                }
            
        }
        [CheckPermission(Permissions = "Navigate", Module = "INVPL")]
        public ActionResult PriceList()
        {
            var viewModel = base.CreateViewModel<PriceListFormViewModel>();
            //viewModel.AsOnDate = DateTime.UtcNow;
            viewModel.AsOnDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PriceList(string AsOnDate, string products)
        {
            //var asOnDate = Convert.ToDateTime(AsOnDate).ToString("yyyy-MM-dd");
            var asOnDate = AsOnDate;
            //var asOnReportDate = Convert.ToDateTime(AsOnDate).ToString("MM/dd/yyyy");
            var reportDate = "As On " + AsOnDate;
            var viewModel = base.CreateReportViewModel<PriceListViewModel>("Price List Summary", reportDate);
            if (string.IsNullOrEmpty(products))
            {
                products = "0";
            }
            viewModel.PriceLists = StoredProcedureService.GetPriceListReport(asOnDate, products, base.FiscalYear.Id);
            viewModel.FYId = base.FiscalYear.Id;
            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;
            return PartialView("_PartialPriceListReport", viewModel);
        }

        public ActionResult PdfPriceList()
        {
            var view = (PriceListViewModel)Session["ReportModel"];
            return this.ViewPdf("", "Inventory/PdfPriceList", view);
        }

        public ActionResult ExcelPriceList()
        {
            var view = (PriceListViewModel)Session["ReportModel"];
            return this.ViewExcel("", "Inventory/ExcelPriceList", view);
        }
        [CheckPermission(Permissions = "Navigate", Module = "INVROS")]
        public ActionResult ReOrderStatus()
        {
            var viewModel = base.CreateViewModel<ReOrderStatusFormViewModel>();
            viewModel.AsOnDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ReOrderStatus(string AsOnDate, string products)
        {
            var asOnDate = AsOnDate;
            //  var asOnReportDate = Convert.ToDateTime(AsOnDate).ToString("MM/dd/yyyy");
            var reportDate = "As On " + AsOnDate;
            var viewModel = base.CreateReportViewModel<ReOrderStatusViewModel>("ReOrder Status", reportDate);
            if (string.IsNullOrEmpty(products))
            {
                products = "0";
            }
            viewModel.ReOrderStatuses = StoredProcedureService.GetReOrderStatusReport(asOnDate, products, base.FiscalYear.Id);
            viewModel.FYId = base.FiscalYear.Id;
            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;
            return PartialView("_PartialReOrderStatusReport", viewModel);
        }

        public ActionResult PdfReOrderStatus()
        {
            var view = (ReOrderStatusViewModel)Session["ReportModel"];
            return this.ViewPdf("", "Inventory/PdfReOrderStatus", view);
        }

        public ActionResult ExcelReOrderStatus()
        {
            var view = (ReOrderStatusViewModel)Session["ReportModel"];
            return this.ViewExcel("", "Inventory/ExcelReOrderStatus", view);
        }

        public ActionResult ManufacturingInventory()
        {
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "INBOM")]
        public ActionResult BOMRegister()
        {
            var viewModel = base.CreateViewModel<BOMRegisterFormViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BOMRegister(string costcenters, string finishedgoods)
        {
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "INVPBS")]
        public ActionResult ProductBatchSummary()
        {
            var viewModel = base.CreateViewModel<ProductBatchSummaryFormViewModel>();
            viewModel.BranchList = new SelectList(_companyInfo.GetMany(x => x.ParentId != 0), "Id", "Name");
            
            
            viewModel.AllProducts = true;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ProductBatchSummary(ProductBatchSummaryFormViewModel viewModel, List<int> product)
        {
            var sys = _systemControl.GetAll().FirstOrDefault();
            var branchName = "All";
            var i = 0;
            if (viewModel.AllProducts)
            {
                viewModel.ProductId = "0";
            }
            else
            {
                foreach (var item in product)
                {
                    if(i==0)
                    {
                        viewModel.ProductId = item.ToString();
                    }
                    else
                    {
                        viewModel.ProductId += "," + item.ToString();
                    }
                    i++;
                }
            }
            if (viewModel.Mfg)
            {
                if(sys.DateType==2)
                {
                
                    var mStartDate = NepaliDateService.NepalitoEnglishDate(viewModel.MfgStartDate);
                    viewModel.MStartDate = mStartDate.ToString("yyyy-MM-dd");
                    var mEndDate = NepaliDateService.NepalitoEnglishDate(viewModel.MfgEndDate);
                    viewModel.MEndDate = mEndDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    var start = Convert.ToDateTime(viewModel.MfgStartDate);
                    viewModel.MStartDate = start.ToString("yyyy-MM-dd");
                    var end = Convert.ToDateTime(viewModel.MfgEndDate);
                    viewModel.MEndDate = end.ToString("yyyy-MM-dd");
                }
               

            }
           
            
            if (viewModel.Exp)
            {
                if (sys.DateType == 2)
                {
                   
                    var eStartDate = NepaliDateService.NepalitoEnglishDate(viewModel.ExpStartDate);
                    viewModel.EStartDate = eStartDate.ToString("yyyy-MM-dd");
                    var eEndDate = NepaliDateService.NepalitoEnglishDate(viewModel.ExpEndDate);
                    viewModel.EEndDate = eEndDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    var estart = Convert.ToDateTime(viewModel.ExpStartDate);
                    viewModel.EStartDate = estart.ToString("yyyy-MM-dd");
                    var eend = Convert.ToDateTime(viewModel.ExpEndDate);
                    viewModel.EEndDate = eend.ToString("yyyy-MM-dd");
                   
                }
               
                
            }
            viewModel.ExpiredGoods = "0";
            if (viewModel.Expgoods)
            {
                viewModel.ExpiredGoods = "1";
            }

            if(viewModel.BranchId !=0)
            {
                branchName = _companyInfo.GetById(x => x.Id == viewModel.BranchId).Name;
            }
           // var reportDate = DateTime.UtcNow.ToString("MM/dd/yyyy");

            var data = StoredProcedureService.GetProductBatchSummaryReport(viewModel.ProductId, viewModel.BranchId, viewModel.ExpiredGoods, viewModel.RemainingDays, viewModel.MStartDate, viewModel.MEndDate, viewModel.EStartDate, viewModel.EEndDate);
            var viewModel1 = base.CreateReportViewModel<ProductBatchSummaryViewModel>("Product Batch Summary - " + branchName ,"");
            viewModel1.ProductBatchSummaries = data;

            Session["ReportModel"] = viewModel1;
            return PartialView("_PartialProductBatchSummaryReport", viewModel1);
        }
        public ActionResult PdfProductBatchSummary()
        {
            var view = (ProductBatchSummaryViewModel)Session["ReportModel"];
            return this.ViewPdf("", "Inventory/PdfProductBatchSummary", view);
        }

        public ActionResult ExcelProductBatchSummary()
        {
            var view = (ProductBatchSummaryViewModel)Session["ReportModel"];
            return this.ViewExcel("", "Inventory/ExcelProductBatchSummary", view);
        }


    }
}
