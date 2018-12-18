using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.ARAP;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.ViewModels.Entry;
using Neodynamic.SDK.Web;

namespace KRBAccounting.Web.Controllers
{
    public class ARAPController : BaseController
    {

        private readonly ILedgerRepository _ledger;
        private readonly ISystemControlRepository _systemControl;
        private readonly IFiscalYearRepository _fiscalYear;
        private readonly IGodownRepository _godown;
        private readonly ICompanyInfoRepository _companyInfo;
        private readonly IFormsAuthenticationService _authentication;


        public ARAPController(ILedgerRepository ledgerRepository, ISystemControlRepository systemControlRepository,
                              IFiscalYearRepository fiscalYearRepository, IGodownRepository godownRepository, ICompanyInfoRepository companyInfo)
        {
            _ledger = ledgerRepository;
            _systemControl = systemControlRepository;
            _fiscalYear = fiscalYearRepository;
            _godown = godownRepository;
            _companyInfo = companyInfo;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Accounting);

        }


        //
        // GET: /ARAP/

        public ActionResult Index()
        {
            return View();
        }

        #region Party Ledger

        [CheckPermission(Permissions = "Navigate", Module = "AR-PLR")]
        public ActionResult PartyLedger()
        {
            var viewModel = new PartyLedgerFormViewModel();
            var branchList = _companyInfo.GetAll();
            var user = _authentication.GetAuthenticatedUser();
            viewModel.BranchList = new SelectList(branchList, "Id", "Name");
            if (branchList.Any())
            {
                viewModel.CheckBranch = true;
                if (!user.AllBranch)
                {
                    viewModel.BranchList = new SelectList(_companyInfo.GetMany(x => x.Id == user.BranchId), "Id", "Name");
                }
            }
            viewModel.BranchList = new SelectList(branchList, "Id", "Name");

            var data = _fiscalYear.GetById(x => x.IsDefalut);
            viewModel.CategoryList = new SelectList(
                Enum.GetValues(typeof(PartyLedgerTypeEnum)).Cast
                    <PartyLedgerTypeEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");


            viewModel.GroupByList = new SelectList(
                Enum.GetValues(typeof(LedgerReportGroupByEnum)).Cast
                    <LedgerReportGroupByEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");

            viewModel.OpeningList = new SelectList(
                Enum.GetValues(typeof(OpeningReportTypeEnum)).Cast
                    <OpeningReportTypeEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");

            viewModel.StartDate = base.SystemControl.DateType == 1
                                      ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                      : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                    ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                    : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;

            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.Category = 1;
            return View(viewModel);
        }

        public ActionResult GetLedgerCategory(int category)
        {
            var cat = Enum.GetName(typeof(KRBAccounting.Enums.PartyLedgerTypeEnum), category).ToString();
            var ledgers = _ledger.Filter(x => !x.IsDeleted && (x.Category == cat || x.Category == "BO")).Select(x => new
            {
                Id
            =
            x
            .
            Id,
                Description
            =
            x
            .
            AccountName,
                ShortName
            =
            x
            .
            ShortName
            })
                .OrderBy(x => x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);

            //string other = StringEnum.Parse(typeof(LedgerCategoryEnum), "Other").ToString();
            //var ledgers = _ledger.Filter(x => !x.IsDeleted && x.Category == other).Select(x => new
            //{
            //    Id = x.Id,
            //    Description = x.AccountName,
            //    ShortName = x.ShortName
            //}).OrderBy(x => x.Description);
            //return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult PartyLedger(PartyLedgerFormViewModel model, string Groups, string ledgers, string StartDate, string EndDate,
                                        int Category,
                                        string subLedgers, bool? includeSubLedger, bool? includeRemarks,
                                        bool? includeSummary, bool? includeProductDetails, bool? includeTermDetails, int groupBy, bool? docAgent)
        {
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;

            string catList = string.Empty;
            switch (Category)
            {
                case 1:
                    catList = "BO,CU";
                    break;
                case 2:
                    catList = "BO,VE";
                    break;
            }

            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            var startDate = string.Empty;
            var endDate = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                startReportDate = StartDate;
                endReportDate = EndDate;
                startDate = StartDate;
                endDate = EndDate;
            }
            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(StartDate).ToString("yyyy-MM-dd");
                endDate = NepaliDateService.NepalitoEnglishDate(EndDate).ToString("yyyy-MM-dd");
                startReportDate = StartDate;
                endReportDate = EndDate;
            }

            #region -- Page Title -----

            var reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            var title = string.Empty;

            if (Category == 1) //for customer 
            {

                if (includeSummary == true)
                {
                    if (groupBy == 1)
                        title += "Customer Summary ";
                    else if (groupBy == 2)
                        title += "Customer Summary - Agent Wise";
                    else if (groupBy == 3)
                        title += "Customer Summary - Area Wise";
                    else if (groupBy == 4)
                        title += "Customer Summary - Account Group Wise";
                    else if (groupBy == 5)
                        title += "Customer Summary - Account-Sub Group Wise";
                }
                else
                {
                    if (groupBy == 1)
                        title += "Customer Ledger Details ";
                    else if (groupBy == 2)
                        title += "Customer Ledger Details - Agent Wise";
                    else if (groupBy == 3)
                        title += "Customer Ledger Details - Area Wise";
                    else if (groupBy == 4)
                        title += "Customer Ledger Details - Account Group Wise";
                    else if (groupBy == 5)
                        title += "Customer Ledger Details - Account-Sub Group Wise";
                }

            }
            else // for vendor
            {

                if (includeSummary == true)
                {
                    if (groupBy == 1)
                        title += "Vendor Summary";
                    else if (groupBy == 2)
                        title += "Vendor Summary - Agent Wise";
                    else if (groupBy == 3)
                        title += "Vendor Summary - Area Wise";
                    else if (groupBy == 4)
                        title += "Vendor Summary - Account Group Wise";
                    else if (groupBy == 5)
                        title += "Vendor Summary - Account Sub Group Wise";
                }
                else
                {
                    if (groupBy == 1)
                        title += "Vendor Ledger Details";
                    else if (groupBy == 2)
                        title += "Vendor Ledger Details - Agent Wise";
                    else if (groupBy == 3)
                        title += "Vendor Ledger Details - Area Wise";
                    else if (groupBy == 4)
                        title += "Vendor Ledger Details - Account Group Wise";
                    else if (groupBy == 5)
                        title += "Vendor Ledger Details - Account Sub Group Wise";
                }

            }
            var BranchName = "";
            if (branchId != 0)
            {
                title += " (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }
            #endregion

            var viewModel = base.CreateReportViewModel<PartyLedgerViewModel>(title, reportDate);

            if (includeSubLedger != null)
            {
                viewModel.DisplaySubledger = Convert.ToBoolean(includeSubLedger);
                viewModel.SubLedgers = subLedgers;
            }
            else
            {
                viewModel.DisplaySubledger = false;
            }
            if (includeRemarks != null)
            {
                viewModel.DisplayRemarks = Convert.ToBoolean(includeRemarks);
            }
            if (includeSummary != null)
            {
                viewModel.DisplaySummary = Convert.ToBoolean(includeSummary);
            }
            if (includeProductDetails != null)
            {
                viewModel.DisplayProductDetails = Convert.ToBoolean(includeProductDetails);
            }
            if (includeProductDetails != null)
            {
                viewModel.DisplayTermDetails = Convert.ToBoolean(includeTermDetails);
            }

            var ledgerList = UtilityService.GetLedgers(startDate, endDate, catList, Groups, ledgers, base.FiscalYear.Id, groupBy,
                                                       docAgent, branchId);
            viewModel.LedgerList = ledgerList;
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.FYId = base.FiscalYear.Id;
            viewModel.Datetype = base.SystemControl.DateType;
            viewModel.GroupBy = groupBy;
            viewModel.BranchId = branchId;
            viewModel.DisplaySummary = includeSummary.HasValue && includeSummary == true;
            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;


            if (includeSummary == true)
                return PartialView("_PartialPartyLedgerSummaryReport", viewModel);
            else
                if (groupBy == 1)
                    return PartialView("_PartialPartyLedgerReport", viewModel);
                else
                    return PartialView("_PartialPartyLedgerAcGrpReport", viewModel);


        }

        public ActionResult PdfPartyLegderReport()
        {
            var view = (PartyLedgerViewModel)Session["ReportModel"];

            if (view.DisplaySummary)
            {
                return this.ViewPdf("", "ARAP/PdfPartyLedgerSummaryReport", view);
            }
            else
            {
                if (view.GroupBy == 1)
                    return this.ViewPdf("", "ARAP/PdfPartyLedgerReport", view);
                else
                    return this.ViewPdf("", "ARAP/PdfPartyLedgerAcGrpReport", view);
            }

        }

        public ActionResult ExcelPartyLegderReport()
        {
            var view = (PartyLedgerViewModel)Session["ReportModel"];

            if (view.DisplaySummary)
            {
                return this.ViewExcel("", "ARAP/ExcelPartyLedgerSummaryReport", view);
            }
            else
            {
                if (view.GroupBy == 1)
                    return this.ViewExcel("", "ARAP/ExcelPartyLedgerReport", view);
                else
                    return this.ViewExcel("", "ARAP/ExcelPartyLedgerAcGrpReport", view);
            }
           
        }

        #endregion

        #region OutStanding

        #region Customer

        [CheckPermission(Permissions = "Navigate", Module = "AR-OCR")]
        public ActionResult OutStandingCustomer()
        {
            var viewModel = base.CreateViewModel<OutStandingFromViewModel>();
            viewModel.GroupByList = new SelectList(
               Enum.GetValues(typeof(LedgerReportGroupByEnum)).Cast
                   <LedgerReportGroupByEnum>().Select(
                       x =>
                       new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
               "Value", "Text");
            viewModel.AsOnDate = base.SystemControl.DateType == 1
                                     ? DateTime.Now.ToString("MM/dd/yyyy")
                                     : NepaliDateService.NepaliDate(DateTime.Now).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult OutStandingCustomer(string ledgers, string AsonDate, int groupBy, bool? docAgent)
        {
            var cat = Enum.GetName(typeof(LedgerCategoryEnum), 2).ToString();
            //var asOnDate = Convert.ToDateTime(AsonDate).ToString("yyyy-MM-dd");
            var asOnDate = AsonDate;
            var asOnReportDate = asOnDate;

            var reportDate = "As On " + asOnReportDate;
            var viewModel = base.CreateReportViewModel<OutStandingViewModel>("Customer Outstanding Report On Due Date",
                                                                             reportDate);
            var ledgerList = UtilityService.GetCustomerLedgers(asOnDate, cat, ledgers, base.FiscalYear.Id);
            viewModel.LedgerList = ledgerList;
            viewModel.AsOnDate = asOnDate;
            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;
            return PartialView("OutStandingCustomerReport", viewModel);
        }

        public ActionResult GetLedgerCustomer()
        {
            var cat = Enum.GetName(typeof(LedgerCategoryEnum), 1).ToString();
            var cat1 = Enum.GetName(typeof(LedgerCategoryEnum), 2).ToString();
            var ledgers = _ledger.Filter(x => !x.IsDeleted && (x.Category == cat || x.Category == cat1)).Select(x => new
                                                                                                                         {
                                                                                                                             Id
                                                                                                                         =
                                                                                                                         x
                                                                                                                         .
                                                                                                                         Id,
                                                                                                                             Description
                                                                                                                         =
                                                                                                                         x
                                                                                                                         .
                                                                                                                         AccountName,
                                                                                                                             ShortName
                                                                                                                         =
                                                                                                                         x
                                                                                                                         .
                                                                                                                         ShortName
                                                                                                                         })
                .OrderBy(x => x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfOutStandingCustomerReport()
        {
            var view = (OutStandingViewModel)Session["ReportModel"];
            return this.ViewPdf("", "ARAP/PdfOutStandingCustomerReport", view);
        }

        public ActionResult ExcelOutStandingCustomerReport()
        {
            var view = (OutStandingViewModel)Session["ReportModel"];
            return this.ViewExcel("", "ARAP/ExcelOutStandingCustomerReport", view);
        }

        #endregion

        #region Supplier

        [CheckPermission(Permissions = "Navigate", Module = "AR-OVR")]
        public ActionResult OutStandingSupplier()
        {
            var viewModel = base.CreateViewModel<OutStandingFromViewModel>();
            viewModel.GroupByList = new SelectList(
               Enum.GetValues(typeof(LedgerReportGroupByEnum)).Cast
                   <LedgerReportGroupByEnum>().Select(
                       x =>
                       new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
               "Value", "Text");
            viewModel.AsOnDate = base.SystemControl.DateType == 1
                                     ? DateTime.Now.ToString("MM/dd/yyyy")
                                     : NepaliDateService.NepaliDate(DateTime.Now).Date;
            // viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            // viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult OutStandingSupplier(string ledgers, string AsonDate)
        {
            var startReportDate1 = Convert.ToDateTime(AsonDate).ToString("MM/dd/yyyy");

            var cat = Enum.GetName(typeof(LedgerCategoryEnum), 4).ToString();
            var asOnDate = AsonDate;
            var asOnReportDate = asOnDate;

            var reportDate = "As On " + asOnReportDate;
            var viewModel = base.CreateReportViewModel<OutStandingViewModel>("Vendor Outstanding Report On Due Date",
                                                                             reportDate);
            var ledgerList = UtilityService.GetVenderLedgers(asOnDate, cat, ledgers, base.FiscalYear.Id);
            viewModel.LedgerList = ledgerList;
            viewModel.AsOnDate = asOnDate;
            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;
            return PartialView("OutStandingSupplierReport", viewModel);
        }

        public ActionResult GetLedgerSupplier()
        {
            var cat = Enum.GetName(typeof(LedgerCategoryEnum), 1).ToString();
            var cat1 = Enum.GetName(typeof(LedgerCategoryEnum), 4).ToString();
            var ledgers = _ledger.Filter(x => !x.IsDeleted && (x.Category == cat || x.Category == cat1)).Select(x => new
                                                                                                                         {
                                                                                                                             Id
                                                                                                                         =
                                                                                                                         x
                                                                                                                         .
                                                                                                                         Id,
                                                                                                                             Description
                                                                                                                         =
                                                                                                                         x
                                                                                                                         .
                                                                                                                         AccountName,
                                                                                                                             ShortName
                                                                                                                         =
                                                                                                                         x
                                                                                                                         .
                                                                                                                         ShortName
                                                                                                                         })
                .OrderBy(x => x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfOutStandingSupplierReport()
        {
            var view = (OutStandingViewModel)Session["ReportModel"];
            return this.ViewPdf("", "ARAP/PdfOutStandingSupplierReport", view);
        }

        public ActionResult ExcelOutStandingSupplierReport()
        {
            var view = (OutStandingViewModel)Session["ReportModel"];
            return this.ViewExcel("", "ARAP/ExcelOutStandingSupplierReport", view);
        }

        #endregion

        #endregion

            [CheckPermission(Permissions = "Navigate", Module = "ARAPS")]
        public ActionResult Sales()
        {
            return View();
        }
         [CheckPermission(Permissions = "Navigate", Module = "ARAPSS")]
        public ActionResult SalesSummary()
        {

            var viewModel = base.CreateViewModel<SalesSummaryFormViewModel>();
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
                viewModel.EndDate = base.SystemControl.DateType == 1
                                        ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                        : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            }
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.GodownList = _godown.GetMany(x => !x.IsDeleted);
            //viewModel.StartDate = FiscalYear.StartDate;
            //viewModel.EndDate = FiscalYear.EndDate;
            viewModel.StartDate = base.SystemControl.DateType == 1
                                      ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                      : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                    ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                    : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;

            viewModel.GroupByList = new SelectList(
                Enum.GetValues(typeof(SalesReportGroupByEnum)).Cast
                    <SalesReportGroupByEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SalesSummary(SalesSummaryFormViewModel model, List<string> GodownId)
        {
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            var startDate = string.Empty;
            var endDate = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                startDate = model.StartDate;
                endDate = model.EndDate;
                if (model.DateShow == true)
                {
                    startReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.StartDate)).Date;
                    endReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.EndDate)).Date;
                }
                else
                {
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }
            }
            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
                if (model.DateShow == true)
                {
                    startReportDate = startDate;
                    endReportDate = endDate;
                }
                else
                {
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }
            }

            var reportDate = "";
            if (model.DateShow == true)
            {
                if (base.SystemControl.DateType == 1)
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                else
                    reportDate = "Report Date From " + startDate + " To " + endDate;
            }
            else
                reportDate = "Report Date From " + startReportDate + " To " + endReportDate;

            if (model.Detail)
            {
                if (model.Godown)
                {
                    string godownIdString = string.Empty;
                    if (GodownId == null || GodownId.Count == 0)
                    {
                        godownIdString = "0";
                    }
                    else
                    {
                        godownIdString = String.Join(",", GodownId);
                    }
                    var salesDetailsWithGodown = StoredProcedureService.GetSalesGodownHeaders(startDate, endDate,
                                                                                              godownIdString);
                    var viewModel = base.CreateReportViewModel<SalesSummaryViewModel>("Sales Detail(Godown)", reportDate);
                    viewModel.SalesGodownHeaders = salesDetailsWithGodown;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["SalesSummaryReportModel"] = viewModel;
                    Session["SalesSummaryReportType"] = "1";
                    viewModel.Detail = model.Detail;
                    viewModel.Godown = model.Godown;

                    return PartialView("_PartialSalesDetailGodownReport", viewModel);
                }
                else
                {
                    var salesDetails = StoredProcedureService.GetSalesDetails(startDate, endDate);
                    var viewModel = base.CreateReportViewModel<SalesSummaryViewModel>("Sales Detail", reportDate);
                    viewModel.SalesDetails = salesDetails;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["SalesSummaryReportModel"] = viewModel;
                    Session["SalesSummaryReportType"] = "1";
                    viewModel.Detail = model.Detail;
                    viewModel.Godown = model.Godown;
                    return PartialView("_PartialSalesDetailReport", viewModel);
                }
            }
            else
            {
                var viewModel = base.CreateReportViewModel<SalesSummaryViewModel>("Sales Summary", reportDate);
                DataTable report = StoredProcedureService.GetSalesSummary(startDate, endDate, model.GroupBy);
                viewModel.Report = report;
                viewModel.DisplayRemarks = model.Remarks;
                viewModel.Datetype = base.SystemControl.DateType;
                if (model.DateShow == true)
                {
                    if (viewModel.Datetype == 1)
                        viewModel.Datetype = 2;
                    else
                        viewModel.Datetype = 1;
                }
                Session["SalesSummaryReportModel"] = viewModel;
                Session["SalesSummaryReportType"] = "2";
                return PartialView("_PartialSalesSummaryReport", viewModel);
            }
        }

        public ActionResult PdfSalesSummary()
        {
            var reportype = Convert.ToInt32(Session["SalesSummaryReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (SalesSummaryViewModel)Session["SalesSummaryReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewPdf("", "ARAP/PdfSalesDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewPdf("", "ARAP/", view);
                        }
                        break;
                        ;
                    }
                case 2:
                    {
                        var view = (SalesSummaryViewModel)Session["SalesSummaryReportModel"];
                        return this.ViewPdf("", "ARAP/PdfSalesSummary", view);
                        break;
                    }
            }
            return null;
        }

        public ActionResult ExcelSalesSummary()
        {
            var reportype = Convert.ToInt32(Session["SalesSummaryReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (SalesSummaryViewModel)Session["SalesSummaryReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewExcel("", "ARAP/ExcelSalesDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewExcel("", "ARAP/ExcelSalesDetails", view);
                        }


                        break;
                        ;
                    }
                case 2:
                    {
                        var view = (SalesSummaryViewModel)Session["SalesSummaryReportModel"];
                        return this.ViewExcel("", "ARAP/ExcelSalesSummary", view);
                        break;
                    }
            }
            return null;
        }
        [CheckPermission(Permissions = "Navigate", Module = "SR")]
        public ActionResult SalesReturnSummary()
        {
            var viewModel = base.CreateViewModel<SalesReturnSummaryFormViewModel>();
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
                viewModel.EndDate = base.SystemControl.DateType == 1
                                       ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                       : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            }
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                   ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                   : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.GroupByList = new SelectList(
                Enum.GetValues(typeof(SalesReportGroupByEnum)).Cast
                    <SalesReportGroupByEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.GodownList = _godown.GetMany(x => !x.IsDeleted);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SalesReturnSummary(SalesReturnSummaryFormViewModel model, List<string> GodownId)
        {
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            var startDate = string.Empty;
            var endDate = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                startDate = model.StartDate;
                endDate = model.EndDate;
                if (model.DateShow == true)
                {
                    startReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.StartDate)).Date;
                    endReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.EndDate)).Date;
                }
                else
                {
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }
            }
            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
                if (model.DateShow == true)
                {
                    startReportDate = startDate;
                    endReportDate = endDate;
                }
                else
                {
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }
            }

            var reportDate = "";
            if (model.DateShow == true)
            {
                if (base.SystemControl.DateType == 1)
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                else
                    reportDate = "Report Date From " + startDate + " To " + endDate;
            }
            else
                reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            if (model.Detail)
            {
                if (model.Godown)
                {
                    string godownIdString = string.Empty;
                    if (GodownId == null || GodownId.Count == 0)
                    {
                        godownIdString = "0";
                    }
                    else
                    {

                        godownIdString = String.Join(",", GodownId);
                    }
                    var salesDetailsWithGodown = StoredProcedureService.GetSalesReturnGodownHeaders(startDate, endDate,
                                                                                                    godownIdString);
                    var viewModel =
                        base.CreateReportViewModel<SalesReturnSummaryViewModel>("Sales Return Detail(Godown)",
                                                                                reportDate);
                    viewModel.SalesReturnGodownHeaders = salesDetailsWithGodown;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.Godown = model.Godown;
                    viewModel.Datetype = base.SystemControl.DateType;
                    Session["SalesReturnSummaryReportModel"] = viewModel;
                    Session["SalesReturnSummaryReportType"] = "1";
                    return PartialView("_PartialSalesReturnDetailGodownReport", viewModel);
                }
                else
                {
                    var salesDetails = StoredProcedureService.GetSalesReturnDetails(startDate, endDate);
                    var viewModel = base.CreateReportViewModel<SalesReturnSummaryViewModel>("Sales Return Detail",
                                                                                            reportDate);
                    viewModel.SalesReturnDetails = salesDetails;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.Datetype = base.SystemControl.DateType;
                    Session["SalesReturnSummaryReportModel"] = viewModel;
                    Session["SalesReturnSummaryReportType"] = "1";
                    return PartialView("_PartialSalesReturnDetailReport", viewModel);
                }
            }
            else
            {
                var viewModel = base.CreateReportViewModel<SalesReturnSummaryViewModel>("Sales Return Summary", reportDate);
                DataTable report = StoredProcedureService.GetSalesReturnSummary(startDate, endDate, model.GroupBy);
                viewModel.Report = report;
                viewModel.GroupBy = model.GroupBy;
                viewModel.Datetype = base.SystemControl.DateType;
                Session["SalesReturnSummaryReportModel"] = viewModel;
                Session["SalesReturnSummaryReportType"] = "2";
                
                return PartialView("_PartialSalesReturnSummaryReport", viewModel);
            }
        }

        public ActionResult PdfSalesReturnSummary()
        {
            var reportype = Convert.ToInt32(Session["SalesReturnSummaryReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (SalesReturnSummaryViewModel)Session["SalesReturnSummaryReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewPdf("", "ARAP/PdfSalesReturnDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewPdf("", "ARAP/PdfSalesReturnDetails", view);
                        }
                    }
                case 2:
                    {
                        var view = (SalesReturnSummaryViewModel)Session["SalesReturnSummaryReportModel"];
                        return this.ViewPdf("", "ARAP/PdfSalesReturnSummary", view);
                    }
            }
            return null;
          
        }

        public ActionResult ExcelSalesReturnSummary()
        {
            var reportype = Convert.ToInt32(Session["SalesReturnSummaryReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (SalesReturnSummaryViewModel)Session["SalesReturnSummaryReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewExcel("", "ARAP/ExcelSalesReturnDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewExcel("", "ARAP/ExcelSalesReturnDetails", view);
                        }
                    }
                case 2:
                    {
                        var view = (SalesReturnSummaryViewModel)Session["SalesReturnSummaryReportModel"];
                        return this.ViewExcel("", "ARAP/ExcelSalesReturnSummary", view);
                    }
            }
            return null;
        }

        [CheckPermission(Permissions = "Navigate", Module = "ARAPPS")]
        public ActionResult Purchase()
        {
            return View();
        }
        public ActionResult PurchaseOrder()
        {
            var viewModel = base.CreateViewModel<PurchaseOrderFormViewModel>();
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
                viewModel.EndDate = base.SystemControl.DateType == 1
                                        ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                        : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            }
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.GroupByList = new SelectList(
                  Enum.GetValues(typeof(PurchaseReportGroupByEnum)).Cast
                      <PurchaseReportGroupByEnum>().Select(
                          x =>
                          new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                  "Value", "Text");
            viewModel.GodownList = _godown.GetMany(x => !x.IsDeleted);
            //viewModel.GodownId = 1;
            viewModel.StartDate = base.SystemControl.DateType == 1
                                      ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                      : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                    ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                    : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult PurchaseOrder(PurchaseOrderFormViewModel model, List<string> GodownId)
        {
            var startDate = string.Empty;
            var endDate = string.Empty;
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;

            if (base.SystemControl.DateType == 1)
            {
                startReportDate = model.StartDate;
                endReportDate = model.EndDate;
                startDate = model.StartDate;
                endDate = model.EndDate;
            }
            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
                startReportDate = model.StartDate;
                endReportDate = model.EndDate;
            }
            var reportDate = "";

            if (model.DateShow == true)
            {
                if (base.SystemControl.DateType == 1)
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                else
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            }
            else
                reportDate = "Report Date From " + startReportDate + " To " + endReportDate;


            //var reportDate = "Report Date From " + model.StartDate + " To " + model.EndDate;
            if (model.Detail)
            {
                if (model.Godown)
                {
                    string godownIdString = string.Empty;
                    if (GodownId == null || GodownId.Count == 0)
                    {
                        godownIdString = "0";
                    }
                    else
                    {

                        godownIdString = String.Join(",", GodownId);
                    }
                    var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.PurchaseOrderViewModel>("Purchase Detail(Godown)",
                                                                                         reportDate);
                    var purchaseDetailsWithGodown = StoredProcedureService.GetPurchaseGodownHeaders(startDate, endDate,
                                                                                                    godownIdString);
                    viewModel.PurchaseGodownHeaders = purchaseDetailsWithGodown;

                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.Godown = model.Godown;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["ReportModel"] = viewModel;
                    Session["ReportType"] = "1";

                    return PartialView("_PartialPurchaseOrderDetailGodownReport", viewModel);
                }
                else
                {
                    var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.PurchaseOrderViewModel>("Purchase Order Detail", reportDate);
                    var purchaseDetails = StoredProcedureService.GetPurchaseOrderDetails(startDate, endDate, model.GroupBy );
                    viewModel.PurchaseDetails = purchaseDetails;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    Session["ReportModel"] = viewModel;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["ReportType"] = "1";
                    return PartialView("_PartialPurchaseOrderDetailReport", viewModel);
                }
            }
            else
            {
                var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.PurchaseOrderViewModel>("Purchase Order", reportDate);
                DataTable report = StoredProcedureService.GetPurchaseOrder(startDate, endDate, model.GroupBy);
                viewModel.Groupby = model.GroupBy;
                viewModel.Report = report;
                viewModel.DisplayRemarks = model.Remarks;
                viewModel.Datetype = base.SystemControl.DateType;
                if (model.DateShow == true)
                {
                    if (viewModel.Datetype == 1)
                        viewModel.Datetype = 2;
                    else
                        viewModel.Datetype = 1;
                }
                Session["ReportModel"] = viewModel;
                Session["ReportType"] = "2";
                return PartialView("_PartialPurchaseOrderReport", viewModel);
            }
        }

        public ActionResult PdfPurchaseOrder()
        {
            var reportype = Convert.ToInt32(Session["ReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (KRBAccounting.Web.ViewModels.ARAP.PurchaseOrderViewModel)Session["ReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewPdf("", "ARAP/PdfPurchaseDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewPdf("", "ARAP/PdfPurchaseOrderDetails", view);
                        }

                    }
                case 2:
                    {
                        var view = (KRBAccounting.Web.ViewModels.ARAP.PurchaseOrderViewModel)Session["ReportModel"];
                        return this.ViewPdf("", "ARAP/PdfPurchaseOrder", view);
                    }
            }
            return null;
        }

        public ActionResult ExcelPurchaseOrder()
        {
            var reportype = Convert.ToInt32(Session["ReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (KRBAccounting.Web.ViewModels.ARAP.PurchaseOrderViewModel)Session["ReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewExcel("", "ARAP/ExcelPurchaseDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewExcel("", "ARAP/ExcelPurchaseDetails", view);
                        }
                    }
                case 2:
                    {
                        var view = (KRBAccounting.Web.ViewModels.ARAP.PurchaseOrderViewModel)Session["ReportModel"];
                        return this.ViewExcel("", "ARAP/ExcelPurchaseOrder", view);
                    }
            }
            return null;
        }
        public ActionResult PurchaseChallan()
        {
            var viewModel = base.CreateViewModel<PurchaseChallanFormViewModel>();
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
                viewModel.EndDate = base.SystemControl.DateType == 1
                                        ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                        : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            }
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.GroupByList = new SelectList(
                  Enum.GetValues(typeof(PurchaseReportGroupByEnum)).Cast
                      <PurchaseReportGroupByEnum>().Select(
                          x =>
                          new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                  "Value", "Text");
            viewModel.GodownList = _godown.GetMany(x => !x.IsDeleted);
            //viewModel.GodownId = 1;
            viewModel.StartDate = base.SystemControl.DateType == 1
                                      ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                      : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                    ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                    : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult PurchaseChallan(PurchaseChallanFormViewModel model, List<string> GodownId)
        {
            var startDate = string.Empty;
            var endDate = string.Empty;
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;

            if (base.SystemControl.DateType == 1)
            {
                startReportDate = model.StartDate;
                endReportDate = model.EndDate;
                startDate = model.StartDate;
                endDate = model.EndDate;
            }
            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
                startReportDate = model.StartDate;
                endReportDate = model.EndDate;
            }
            var reportDate = "";

            if (model.DateShow == true)
            {
                if (base.SystemControl.DateType == 1)
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                else
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            }
            else
                reportDate = "Report Date From " + startReportDate + " To " + endReportDate;


            //var reportDate = "Report Date From " + model.StartDate + " To " + model.EndDate;
            if (model.Detail)
            {
                if (model.Godown)
                {
                    string godownIdString = string.Empty;
                    if (GodownId == null || GodownId.Count == 0)
                    {
                        godownIdString = "0";
                    }
                    else
                    {

                        godownIdString = String.Join(",", GodownId);
                    }
                    var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.PurchaseChallanViewModel>("Purchase Detail(Godown)",
                                                                                         reportDate);
                    var purchaseDetailsWithGodown = StoredProcedureService.GetPurchaseGodownHeaders(startDate, endDate,
                                                                                                    godownIdString);
                    viewModel.PurchaseGodownHeaders = purchaseDetailsWithGodown;

                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.Godown = model.Godown;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["ReportModel"] = viewModel;
                    Session["ReportType"] = "1";

                    return PartialView("_PartialPurchaseOrderDetailGodownReport", viewModel);
                }
                else
                {
                    var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.PurchaseChallanViewModel>("Purchase Challan Detail", reportDate);
                    var purchaseDetails = StoredProcedureService.GetPurchaseChallanDetails(startDate, endDate, model.GroupBy);
                    viewModel.PurchaseDetails = purchaseDetails;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    Session["ReportModel"] = viewModel;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["ReportType"] = "1";
                    return PartialView("_PartialPurchaseChallanDetailReport", viewModel);
                }
            }
            else
            {
                var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.PurchaseChallanViewModel>("Purchase Challan", reportDate);
                DataTable report = StoredProcedureService.GetPurchaseChallan(startDate, endDate, model.GroupBy);
                viewModel.Groupby = model.GroupBy;
                viewModel.Report = report;
               
                viewModel.DisplayRemarks = model.Remarks;
                viewModel.Datetype = base.SystemControl.DateType;
                if (model.DateShow == true)
                {
                    if (viewModel.Datetype == 1)
                        viewModel.Datetype = 2;
                    else
                        viewModel.Datetype = 1;
                }
                Session["ReportModel"] = viewModel;
                Session["ReportType"] = "2";
                return PartialView("_PartialPurchaseChallanReport", viewModel);
            }
        }


        public ActionResult PdfPurchaseChallan()
        {
            var reportype = Convert.ToInt32(Session["ReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (KRBAccounting.Web.ViewModels.ARAP.PurchaseChallanViewModel)Session["ReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewPdf("", "ARAP/PdfPurchaseDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewPdf("", "ARAP/PdfPurchaseOrderDetails", view);
                        }

                    }
                case 2:
                    {
                        var view = (KRBAccounting.Web.ViewModels.ARAP.PurchaseChallanViewModel)Session["ReportModel"];
                        return this.ViewPdf("", "ARAP/PdfPurchaseChallan", view);
                    }
            }
            return null;
        }

        public ActionResult ExcelPurchaseChallan()
        {
            var reportype = Convert.ToInt32(Session["ReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (KRBAccounting.Web.ViewModels.ARAP.PurchaseChallanViewModel)Session["ReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewExcel("", "ARAP/ExcelPurchaseDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewExcel("", "ARAP/ExcelPurchaseDetails", view);
                        }
                    }
                case 2:
                    {
                        var view = (KRBAccounting.Web.ViewModels.ARAP.PurchaseChallanViewModel)Session["ReportModel"];
                        return this.ViewExcel("", "ARAP/ExcelPurchaseChallan", view);
                    }
            }
            return null;
        }
        [CheckPermission(Permissions = "Navigate", Module = "PQ")]
        public ActionResult PurchaseSummary()
        {
            var viewModel = base.CreateViewModel<PurchaseSummaryFormViewModel>();
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
                viewModel.EndDate = base.SystemControl.DateType == 1
                                        ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                        : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            }
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.GroupByList = new SelectList(
                  Enum.GetValues(typeof(PurchaseReportGroupByEnum)).Cast
                      <PurchaseReportGroupByEnum>().Select(
                          x =>
                          new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                  "Value", "Text");
            viewModel.GodownList = _godown.GetMany(x => !x.IsDeleted);
            //viewModel.GodownId = 1;
            viewModel.StartDate = base.SystemControl.DateType == 1
                                      ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                      : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                    ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                    : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseSummary(PurchaseSummaryFormViewModel model, List<string> GodownId)
        {
            var startDate = string.Empty;
            var endDate = string.Empty;
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;

            if (base.SystemControl.DateType == 1)
            {
                startDate = model.StartDate;
                endDate = model.EndDate;
                if (model.DateShow == true)
                {
                    startReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.StartDate)).Date;
                    endReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.EndDate)).Date;
                }
                else
                {
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }

            }
            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
                if (model.DateShow == true)
                {
                    startReportDate = startDate;
                    endReportDate = endDate;
                }
                else
                {
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }

            }
            var reportDate = "";

            if (model.DateShow == true)
            {
                if (base.SystemControl.DateType == 1)
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                else
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            }
            else
                reportDate = "Report Date From " + startReportDate + " To " + endReportDate;


            //var reportDate = "Report Date From " + model.StartDate + " To " + model.EndDate;
            if (model.Detail)
            {
                if (model.Godown)
                {
                    string godownIdString = string.Empty;
                    if (GodownId == null || GodownId.Count == 0)
                    {
                        godownIdString = "0";
                    }
                    else
                    {

                        godownIdString = String.Join(",", GodownId);
                    }
                    var viewModel = base.CreateReportViewModel<PurchaseSummaryViewModel>("Purchase Detail(Godown)",
                                                                                         reportDate);
                    var purchaseDetailsWithGodown = StoredProcedureService.GetPurchaseGodownHeaders(startDate, endDate,
                                                                                                    godownIdString);
                    viewModel.PurchaseGodownHeaders = purchaseDetailsWithGodown;

                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.Godown = model.Godown;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["ReportModel"] = viewModel;
                    Session["ReportType"] = "1";

                    return PartialView("_PartialPurchaseDetailGodownReport", viewModel);
                }
                else
                {
                    var viewModel = base.CreateReportViewModel<PurchaseSummaryViewModel>("Purchase Detail", reportDate);
                    var purchaseDetails = StoredProcedureService.GetPurchaseDetails(startDate, endDate);
                    viewModel.PurchaseDetails = purchaseDetails;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    Session["ReportModel"] = viewModel;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["ReportType"] = "1";
                    return PartialView("_PartialPurchaseDetailReport", viewModel);
                }
            }
            else
            {
                var viewModel = base.CreateReportViewModel<PurchaseSummaryViewModel>("Purchase Summary", reportDate);
                DataTable report = StoredProcedureService.GetPurchaseSummary(startDate, endDate, model.GroupBy);
                viewModel.Groupby = model.GroupBy;
                viewModel.Report = report;
                viewModel.DisplayRemarks = model.Remarks;
                viewModel.Datetype = base.SystemControl.DateType;
                if (model.DateShow == true)
                {
                    if (viewModel.Datetype == 1)
                        viewModel.Datetype = 2;
                    else
                        viewModel.Datetype = 1;
                }
                Session["ReportModel"] = viewModel;
                Session["ReportType"] = "2";
                return PartialView("_PartialPurchaseSummaryReport", viewModel);
            }
        }

        public ActionResult PdfPurchaseSummary()
        {
            var reportype = Convert.ToInt32(Session["ReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (PurchaseSummaryViewModel)Session["ReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewPdf("", "ARAP/PdfPurchaseDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewPdf("", "ARAP/PdfPurchaseDetails", view);
                        }

                    }
                case 2:
                    {
                        var view = (PurchaseSummaryViewModel)Session["ReportModel"];
                        return this.ViewPdf("", "ARAP/PdfPurchaseSummary", view);
                    }
            }
            return null;
        }

        public ActionResult ExcelPurchaseSummary()
        {
            var reportype = Convert.ToInt32(Session["ReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (PurchaseSummaryViewModel)Session["ReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewExcel("", "ARAP/ExcelPurchaseDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewExcel("", "ARAP/ExcelPurchaseDetails", view);
                        }
                    }
                case 2:
                    {
                        var view = (PurchaseSummaryViewModel)Session["ReportModel"];
                        return this.ViewExcel("", "ARAP/ExcelPurchaseSummary", view);
                    }
            }
            return null;
        }
         [CheckPermission(Permissions = "Navigate", Module = "PR")]
        public ActionResult PurchaseReturnSummary()
        {
            var viewModel = base.CreateViewModel<PurchaseReturnSummaryFormViewModel>();
            
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
                viewModel.EndDate = base.SystemControl.DateType == 1
                                        ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                        : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            }
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.GroupByList = new SelectList(
                  Enum.GetValues(typeof(PurchaseReportGroupByEnum)).Cast
                      <PurchaseReportGroupByEnum>().Select(
                          x =>
                          new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                  "Value", "Text");
            viewModel.GodownList = _godown.GetMany(x => !x.IsDeleted);
            //viewModel.GodownId = 1;
            viewModel.StartDate = base.SystemControl.DateType == 1
                                      ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                      : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                    ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                    : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
         
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseReturnSummary(PurchaseReturnSummaryFormViewModel model, List<string> GodownId)
        {

            var startDate = string.Empty;
            var endDate = string.Empty;
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;

            if (base.SystemControl.DateType == 1)
            {
                startDate = model.StartDate;
                endDate = model.EndDate;
                if (model.DateShow == true)
                {
                    startReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.StartDate)).Date;
                    endReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.EndDate)).Date;
                }
                else
                {
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }

            }
            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
                if (model.DateShow == true)
                {
                    startReportDate = startDate;
                    endReportDate = endDate;
                }
                else
                {
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }

            }
            var reportDate = "";

            if (model.DateShow == true)
            {
                if (base.SystemControl.DateType == 1)
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                else
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            }
            else
                reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            if (model.Detail)
            {
                if (model.Godown)
                {
                    string godownIdString = string.Empty;
                    if (GodownId == null || GodownId.Count == 0)
                    {
                        godownIdString = "0";
                    }
                    else
                    {

                        godownIdString = String.Join(",", GodownId);
                    }
                    var viewModel =
                        base.CreateReportViewModel<PurchaseReturnSummaryViewModel>("Purchase Return Detail(Godown)",
                                                                                   reportDate);
                    var purchaseDetailsWithGodown = StoredProcedureService.GetPurchaseReturnGodownHeaders(startDate,
                                                                                                          endDate,
                                                                                                          godownIdString);
                    viewModel.PurchaseReturnGodownHeaders = purchaseDetailsWithGodown;
                    viewModel.Datetype = base.SystemControl.DateType;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.Godown = model.Godown;
                    Session["PurchaseReturnSummaryReportModel"] = viewModel;
                    Session["PurchaseReturnSummaryReportType"] = "1";
                    return PartialView("_PartialPurchaseReturnDetailGodownReport", viewModel);
                }
                else
                {
                    var viewModel = base.CreateReportViewModel<PurchaseReturnSummaryViewModel>(
                        "Purchase Return Detail", reportDate);
                    var purchaseDetails = StoredProcedureService.GetPurchaseReturnDetails(startDate, endDate);
                    viewModel.PurchaseReturnDetails = purchaseDetails;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.Datetype = base.SystemControl.DateType;

                    Session["PurchaseReturnSummaryReportModel"] = viewModel;
                    Session["PurchaseReturnSummaryReportType"] = "1";
                    return PartialView("_PartialPurchaseReturnDetailReport", viewModel);
                }
            }
            else
            {
                var viewModel = base.CreateReportViewModel<PurchaseReturnSummaryViewModel>("Purchase Return Summary",
                                                                                     reportDate);
                DataTable report = StoredProcedureService.GetPurchaseReturnSummary(startDate, endDate, model.GroupBy);
                viewModel.Report = report;
                viewModel.Datetype = base.SystemControl.DateType;
                viewModel.Groupby = model.GroupBy;
                Session["PurchaseReturnSummaryReportModel"] = viewModel;
                Session["PurchaseReturnSummaryReportType"] = "2";
                return PartialView("_PartialPurchaseReturnSummaryReport", viewModel);
            }
        }

        public ActionResult PdfPurchaseReturnSummary()
        {
            var reportype = Convert.ToInt32(Session["PurchaseReturnSummaryReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (PurchaseReturnSummaryViewModel)Session["PurchaseReturnSummaryReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewPdf("", "ARAP/PdfPurchaseReturnDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewPdf("", "ARAP/PdfPurchaseReturnDetail", view);
                        }
                    }
                case 2:
                    {
                        var view = (PurchaseReturnSummaryViewModel)Session["PurchaseReturnSummaryReportModel"];
                        return this.ViewPdf("", "ARAP/PdfPurchaseReturnSummary", view);
                    }
            }
            return null;
        }

        public ActionResult ExcelPurchaseReturnSummary()
        {
            var reportype = Convert.ToInt32(Session["PurchaseReturnSummaryReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (PurchaseReturnSummaryViewModel)Session["PurchaseReturnSummaryReportModel"];
                        if (view.Godown)
                        {
                            return this.ViewExcel("", "ARAP/ExcelPurchaseReturnDetailGodown", view);
                        }
                        else
                        {
                            return this.ViewExcel("", "ARAP/ExcelPurchaseReturnDetail", view);
                        }
                    }
                case 2:
                    {
                        var view = (PurchaseReturnSummaryViewModel)Session["PurchaseReturnSummaryReportModel"];
                        return this.ViewExcel("", "ARAP/ExcelPurchaseReturnSummary", view);
                    }
            }
            return null;
        }

        public ActionResult VatReport()
        {
            return View();
        }

        public ActionResult SalesRegister()
        {
            var viewModel = new SalesRegisterFormViewModel();
            viewModel.DateType = base.SystemControl.DateType;
            viewModel.StartDate = base.SystemControl.DateType == 1
                                      ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                      : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                    ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                    : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SalesRegister(SalesRegisterFormViewModel model)
        {
            var startDate = string.Empty;
            var endDate = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                startDate = model.StartDate;
                endDate = model.EndDate;
            }

            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
            }
            var reportDate = "Report Date From " + model.StartDate + " To " + model.EndDate;
            var viewModel = base.CreateReportViewModel<SalesRegisterViewModel>("Sales Vat Register", reportDate);
            viewModel.SalesRegisters = StoredProcedureService.GetSalesVatRegisterReport(startDate, endDate,
                                                                                        model.VatTerm);
            viewModel.DateType = base.SystemControl.DateType;
            Session["ReportModelSalesRegister"] = viewModel;
            return PartialView("_PartialSalesRegisterReport", viewModel);
        }

        public ActionResult PdfSalesRegister()
        {
            var model = (SalesRegisterViewModel)Session["ReportModelSalesRegister"];
            return this.ViewPdf("", "ARAP/PdfSalesRegister", model);
        }

        public ActionResult ExcelSalesRegister()
        {
            var model = (SalesRegisterViewModel)Session["ReportModelSalesRegister"];
            return this.ViewExcel("", "ARAP/ExcelSalesRegister", model);
        }

        public ActionResult PurchaseRegister()
        {
            var viewModel = new PurchaseRegisterFormViewModel();
            viewModel.DateType = base.SystemControl.DateType;
            viewModel.StartDate = base.SystemControl.DateType == 1
                                      ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                      : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                    ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                    : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseRegister(SalesRegisterFormViewModel model)
        {
            var startDate = string.Empty;
            var endDate = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                startDate = model.StartDate;
                endDate = model.EndDate;
            }

            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
            }
            var reportDate = "Report Date From " + model.StartDate + " To " + model.EndDate;
            var viewModel = base.CreateReportViewModel<PurchaseRegisterViewModel>("Purchase Vat Register", reportDate);
            viewModel.PurchaseRegisters = StoredProcedureService.GetPurchaseVatRegisterReport(startDate, endDate,
                                                                                              model.VatTerm);
            viewModel.DateType = base.SystemControl.DateType;
            Session["ReportModelPurchaseRegister"] = viewModel;
            return PartialView("_PartialPurchaseRegisterReport", viewModel);
        }

        public ActionResult PdfPurchaseRegister()
        {
            var model = (PurchaseRegisterViewModel)Session["ReportModelPurchaseRegister"];
            return this.ViewPdf("", "ARAP/PdfPurchaseRegister", model);
        }

        public ActionResult ExcelPurchaseRegister()
        {
            var model = (PurchaseRegisterViewModel)Session["ReportModelPurchaseRegister"];
            return this.ViewExcel("", "ARAP/ExcelPurchaseRegister", model);
        }

        public ActionResult PeriodicAnalysis()
        {
            var viewModel = new PeriodicAnalysisFormViewModel();
            viewModel.YearList = new SelectList(Enumerable.Range(2005, 2025));
            viewModel.MonthList = new SelectList(UtilityService.Months, "Value", "Text");
            viewModel.TypeList = new SelectList(
                Enum.GetValues(typeof(PeriodicType)).Cast
                    <PeriodicType>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.DivisorList = new SelectList(
                Enum.GetValues(typeof(Periodic_Divisor)).Cast
                    <Periodic_Divisor>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.Year = DateTime.UtcNow.Year;
            viewModel.Month = DateTime.UtcNow.Month;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PeriodicAnalysis(PeriodicAnalysisFormViewModel model)
        {
            var type = new StringEnum(typeof(PeriodicType)).GetStringValue(Convert.ToString(model.Type));
            var divisor = new StringEnum(typeof(Periodic_Divisor)).GetStringValue(Convert.ToString(model.Divisor));

            if (model.Type == 1)
            {
                var reportType = string.Empty;
                if (model.Divisor == 1)
                {
                    reportType = type + " Analysis " + " for the Month of " +
                               UtilityService.GetMonthName(model.Month, true) +
                               ", " + model.Year;
                }
                else
                {
                    reportType = type + " Analysis in " + divisor + " for the Month of " +
                                 UtilityService.GetMonthName(model.Month, true) +
                                 ", " + model.Year;
                }

                var viewModel = base.CreateReportViewModel<PeriodicAnalysisViewModel>("Periodic Analysis Monthly",
                                                                                      reportType);
                var analysisMonthly = StoredProcedureService.GetAnalysisMonthly(model.Year, model.Month, model.Divisor);
                viewModel.AnalysisMonthly = analysisMonthly;

                Session["PeriodicAnalysisReportModel"] = viewModel;
                Session["PeriodicAnalysisReportType"] = "1";
                return PartialView("_partialAnalysisMonthlyReport", viewModel);
            }
            else
            {
                var reportType = string.Empty;
                if (model.Divisor == 1)
                {
                    reportType = type + " Analysis for the Year " + model.Year;
                }
                else
                {
                    reportType = type + " Analysis in " + divisor + " for the Year " + model.Year;
                }
                // var reportType = type + " Analysis in " + divisor + " for the Year " + model.Year;
                var viewModel = base.CreateReportViewModel<PeriodicAnalysisViewModel>("Periodic Analysis Yearly",
                                                                                      reportType);
                var analysisYearly = StoredProcedureService.GetAnalysisYearly(model.Year, model.Divisor);
                viewModel.AnalysisYearly = analysisYearly;

                Session["PeriodicAnalysisReportModel"] = viewModel;
                Session["PeriodicAnalysisReportType"] = "2";
                return PartialView("_partialAnalysisYearlyReport", viewModel);
            }

        }

        public ActionResult PdfPeriodicAnalysis()
        {
            var reportype = Convert.ToInt32(Session["PeriodicAnalysisReportType"]);
            if (reportype == 1)
            {
                var model = (PeriodicAnalysisViewModel)Session["PeriodicAnalysisReportModel"];
                return this.ViewPdf("", "ARAP/PdfPeriodicAnalysismonthly", model);
            }
            else
            {
                var model = (PeriodicAnalysisViewModel)Session["PeriodicAnalysisReportModel"];
                return this.ViewPdf("", "ARAP/PdfPeriodicAnalysisYearly", model);
            }
        }

        public ActionResult ExcelPeriodicAnalysis()
        {
            var reportype = Convert.ToInt32(Session["PeriodicAnalysisReportType"]);
            if (reportype == 1)
            {
                var model = (PeriodicAnalysisViewModel)Session["PeriodicAnalysisReportModel"];
                return this.ViewExcel("", "ARAP/ExcelPeriodicAnalysismonthly", model);
            }
            else
            {
                var model = (PeriodicAnalysisViewModel)Session["PeriodicAnalysisReportModel"];
                return this.ViewExcel("", "ARAP/ExcelPeriodicAnalysisYearly", model);
            }
        }

        public ActionResult SalesOrderSummary()
        {
            var viewModel = base.CreateViewModel<SalesOrderFormViewModel>();
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
                viewModel.EndDate = base.SystemControl.DateType == 1
                                       ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                       : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            }
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                   ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                   : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.GodownList = _godown.GetMany(x => !x.IsDeleted);
            viewModel.GroupByList = new SelectList(
                    Enum.GetValues(typeof(SalesReportGroupByEnum)).Cast
                        <SalesReportGroupByEnum>().Select(
                            x =>
                            new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                    "Value", "Text");
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult SalesOrderSummary(SalesOrderFormViewModel model, List<string> GodownId )
            {
                var startReportDate = string.Empty;
                var endReportDate = string.Empty;
                var startDate = string.Empty;
                var endDate = string.Empty;
                if (base.SystemControl.DateType == 1)
                {
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                    startDate = model.StartDate;
                    endDate = model.EndDate;
                }
                if (base.SystemControl.DateType == 2)
                {
                    startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                    endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }

                var reportDate = "";
                if (model.DateShow == true)
                {
                    if (base.SystemControl.DateType == 1)
                        reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                    else
                        reportDate = "Report Date From " + startDate + " To " + endDate;
                }
                else
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;

                if (model.Detail)
                {
                    if (model.Godown)
                    {
                        string godownIdString = string.Empty;
                        if (GodownId == null || GodownId.Count == 0)
                        {
                            godownIdString = "0";
                        }
                        else
                        {

                            godownIdString = String.Join(",", GodownId);
                        }
                        var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.SalesOrderViewModel>("Sale Detail(Godown)",
                                                                                             reportDate);
                        //var purchaseDetailsWithGodown = StoredProcedureService.GetPurchaseGodownHeaders(startDate, endDate,godownIdString);
                        //viewModel.SalesGodownHeaders = purchaseDetailsWithGodown;

                        viewModel.StartDate = startDate;
                        viewModel.EndDate = endDate;
                        viewModel.Godown = model.Godown;
                        viewModel.DisplayRemarks = model.Remarks;
                        viewModel.Datetype = base.SystemControl.DateType;
                        if (model.DateShow == true)
                        {
                            if (viewModel.Datetype == 1)
                                viewModel.Datetype = 2;
                            else
                                viewModel.Datetype = 1;
                        }
                        Session["ReportModel"] = viewModel;
                        Session["ReportType"] = "1";

                        return PartialView("_PartialPurchaseOrderDetailGodownReport", viewModel);
                    }
                    else
                    {
                        var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.SalesOrderViewModel>("Sale Order Detail", reportDate);
                        var saleDetails = StoredProcedureService.GetSalesOrderDetails(startDate, endDate, model.GroupBy);
                        viewModel.SalesOrder = saleDetails;
                        viewModel.StartDate = startDate;
                        viewModel.EndDate = endDate;
                        Session["ReportModel"] = viewModel;
                        viewModel.DisplayRemarks = model.Remarks;
                        viewModel.Datetype = base.SystemControl.DateType;
                        if (model.DateShow == true)
                        {
                            if (viewModel.Datetype == 1)
                                viewModel.Datetype = 2;
                            else
                                viewModel.Datetype = 1;
                        }
                        Session["ReportType"] = "1";
                        return PartialView("_PartialSaleOrderDetailReport", viewModel);
                    }
                }
                else
                {
                    var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.SalesOrderViewModel>("Sales Order Summary", reportDate);
                    DataTable report = StoredProcedureService.GetSalesOrder(startDate, endDate, model.GroupBy);
                    viewModel.Report = report;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["SalesSummaryReportModel"] = viewModel;
                    Session["SalesSummaryReportType"] = "2";
                    return PartialView("_PartialSalesOrderReport", viewModel);
                }
        }

        public ActionResult SalesChallanSummary()
        {
            var viewModel = base.CreateViewModel<SalesChallanSummaryFormViewModel>();
            if (base.SystemControl.IsCurrDate)
            {
                viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
                viewModel.EndDate = base.SystemControl.DateType == 1
                                       ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                       : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            }
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.StartDate = base.SystemControl.DateType == 1
                                          ? FiscalYear.StartDate.ToString("MM/dd/yyyy")
                                          : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1
                                   ? FiscalYear.EndDate.ToString("MM/dd/yyyy")
                                   : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.GroupByList = new SelectList(
                Enum.GetValues(typeof (SalesReportGroupByEnum)).Cast
                    <SalesReportGroupByEnum>().Select(
                        x =>
                        new SelectListItem {Text = StringEnum.GetStringValue(x), Value = ((int) x).ToString()}),
                "Value", "Text");
                     viewModel.GodownList = _godown.GetMany(x => !x.IsDeleted);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SalesChallanSummary (SalesChallanSummaryFormViewModel model, List<string> GodownId)
        {
            var startDate = string.Empty;
            var endDate = string.Empty;
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;

            if (base.SystemControl.DateType == 1)
            {
                startReportDate = model.StartDate;
                endReportDate = model.EndDate;
                startDate = model.StartDate;
                endDate = model.EndDate;
            }
            if (base.SystemControl.DateType == 2)
            {
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
                startReportDate = model.StartDate;
                endReportDate = model.EndDate;
            }
            var reportDate = "";

            if (model.DateShow == true)
            {
                if (base.SystemControl.DateType == 1)
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                else
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            }
            else
                reportDate = "Report Date From " + startReportDate + " To " + endReportDate;


            //var reportDate = "Report Date From " + model.StartDate + " To " + model.EndDate;
            if (model.Detail)
            {
                if (model.Godown)
                {
                    string godownIdString = string.Empty;
                    if (GodownId == null || GodownId.Count == 0)
                    {
                        godownIdString = "0";
                    }
                    else
                    {

                        godownIdString = String.Join(",", GodownId);
                    }
                    var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.SalesChallanSummaryViewModel>("Sale Detail(Godown)",
                                                                                         reportDate);
                    //var purchaseDetailsWithGodown = StoredProcedureService.GetPurchaseGodownHeaders(startDate, endDate,godownIdString);
                    //viewModel.SalesGodownHeaders = purchaseDetailsWithGodown;

                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    viewModel.Godown = model.Godown;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["ReportModel"] = viewModel;
                    Session["ReportType"] = "1";

                    return PartialView("_PartialPurchaseOrderDetailGodownReport", viewModel);
                }
                else
                {
                    var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.SalesChallanSummaryViewModel>("Sale Challan Detail", reportDate);
                    var saleDetails = StoredProcedureService.GetSalesChallanDetails(startDate, endDate, model.GroupBy);
                    viewModel.SalesChallan = saleDetails;
                    viewModel.StartDate = startDate;
                    viewModel.EndDate = endDate;
                    Session["ReportModel"] = viewModel;
                    viewModel.DisplayRemarks = model.Remarks;
                    viewModel.Datetype = base.SystemControl.DateType;
                    if (model.DateShow == true)
                    {
                        if (viewModel.Datetype == 1)
                            viewModel.Datetype = 2;
                        else
                            viewModel.Datetype = 1;
                    }
                    Session["ReportType"] = "1";
                    return PartialView("_PartialSaleChallanDetailReport", viewModel);
                }
            }
            else
            {
                var viewModel = base.CreateReportViewModel<KRBAccounting.Web.ViewModels.ARAP.SalesChallanSummaryViewModel>("Sales Challan", reportDate);
                DataTable report = StoredProcedureService.GetSalesChallanSummaryTable(startDate, endDate, model.GroupBy);
                viewModel.GroupBy = model.GroupBy;
                viewModel.Report = report;
                viewModel.DisplayRemarks = model.Remarks;
                viewModel.Datetype = base.SystemControl.DateType;
                if (model.DateShow == true)
                {
                    if (viewModel.Datetype == 1)
                        viewModel.Datetype = 2;
                    else
                        viewModel.Datetype = 1;
                }
                Session["ReportModel"] = viewModel;
                Session["ReportType"] = "2";
                return PartialView("_PartialSalesChallanSummaryReport", viewModel);
            }
        }

        }
    }


