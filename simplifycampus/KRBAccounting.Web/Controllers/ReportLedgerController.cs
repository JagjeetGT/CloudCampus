using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.ViewModels.LedgerReport;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReportLedgerController : BaseController
    {
        private readonly ILedgerRepository _ledger;
        private readonly DataContext _context = new DataContext();
        private readonly ISystemControlRepository _systemControl;
        private readonly ICompanyInfoRepository _companyInfo;
        private readonly IFormsAuthenticationService _authentication;


        #region Accounting
        public ReportLedgerController(ILedgerRepository ledgerRepository, ISystemControlRepository systemControlRepository, ICompanyInfoRepository companyInfo)
        {
            _ledger = ledgerRepository;
            _systemControl = systemControlRepository;
            _companyInfo = companyInfo;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Accounting);
        }
        //
        // GET: /Ledger/

        public ActionResult Index()
        {
            return View();
        }

        #region Trial Balance
        [CheckPermission(Permissions = "Navigate", Module = "LR-TBR")]
        public ActionResult TrialBalance()
        {
            var viewModel = base.CreateViewModel<TrialBalanceFormViewModel>();
            viewModel.ReportTypeList = new SelectList(
                    Enum.GetValues(typeof(TrialBalanceReportTypeEnum)).Cast
                        <TrialBalanceReportTypeEnum>().Select(
                            x =>
                            new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                    "Value", "Text");
            viewModel.AsOnDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.AccountGroup = true;
            viewModel.Ledger = true;
            viewModel.ReportType = 1;
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
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TrialBalanceReport(TrialBalanceFormViewModel model)
        {
            var startDate = model.StartDate;
            var endDate = model.EndDate;
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            var asOnDate = model.AsOnDate;
            var asOnReportDate = string.Empty;
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;
            string title = string.Empty;
            string reportdate = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                asOnDate = model.AsOnDate;
                startDate = model.StartDate;
                endDate = model.EndDate;
                if (model.DateShow == true)
                {
                    asOnReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.AsOnDate)).Date;
                    startReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.StartDate)).Date;
                    endReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.EndDate)).Date;
                }
                else
                {
                    asOnReportDate = model.AsOnDate;
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }

            }
            if (base.SystemControl.DateType == 2)
            {
                asOnDate = NepaliDateService.NepalitoEnglishDate(model.AsOnDate).ToShortDateString();
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
                if (model.DateShow == true)
                {
                    asOnReportDate = model.AsOnDate;
                    startReportDate = startDate;
                    endReportDate = endDate;
                }
                else
                {
                    asOnReportDate = model.AsOnDate;
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }
            }
            reportdate = "As On " + asOnReportDate;
            var BranchName = "";
            if (branchId != 0)
            {
                title = " (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }
            var aggrp = 0;
            if (model.AccountGroup == true)
                aggrp = 1;
            var agsubgrp = 0;
            if (model.AccountSubGroup == true)
                agsubgrp = 1;
            var ledger = 0;
            if (model.Ledger == true)
                ledger = 1;
            var Subledger = 0;
            if (model.SubLedger == true)
                Subledger = 1;
            var StepFormat = 0;
            if (model.StepFormat == true)
                StepFormat = 1;
            switch (model.ReportType)
            {
                case 1:
                    var reportNormal = UtilityService.GetTrialBalanceReportNormal(asOnDate, base.FiscalYear.Id, Subledger, branchId);
                    title = "Trial Balance" + title;
                    var viewModel1 = base.CreateReportViewModel<TrialBalanceReportNormalViewModel>(title, reportdate);
                    viewModel1.ReportList = reportNormal;
                    viewModel1.SubLedger = model.SubLedger;

                    var debitList1 = from l in viewModel1.ReportList
                                     select new
                                     {
                                         Value = GetPositiveVal(l.DebitCredit)
                                     };
                    viewModel1.Total = debitList1.Sum(x => x.Value);
                    Session["TrialBalanceReportType"] = "1";
                    Session["TrialBalanceReportModel"] = viewModel1;
                    return PartialView("TrialBalanceReportNormal", viewModel1);
                    break;
                case 2:
                    title = "Trial Balance - Group Wise" + title;
                    var reportGroupWise = UtilityService.GetTrialBalanceReportGroupWise(asOnDate, base.FiscalYear.Id, agsubgrp, ledger, Subledger, branchId);
                    var viewModel2 = base.CreateReportViewModel<TrialBalanceReportGroupWiseViewModel>(title, reportdate);
                    viewModel2.ReportList = reportGroupWise;
                    viewModel2.Ledger = model.Ledger;
                    viewModel2.AccountSubGroup = model.AccountSubGroup;
                    viewModel2.SubLedger = model.SubLedger;
                    var debitList = from l in viewModel2.ReportList
                                    select new
                                    {
                                        Value = GetPositiveVal(l.DebitCredit)
                                    };
                    viewModel2.Total = debitList.Sum(x => x.Value);

                    Session["TrialBalanceReportModel"] = viewModel2;
                    if (model.StepFormat == true)
                    {
                        Session["TrialBalanceReportType"] = "4";
                        return PartialView("TrialBalanceStepFormatReportGroupWise", viewModel2);
                    }

                    else
                    {

                        Session["TrialBalanceReportType"] = "2";
                        return PartialView("TrialBalanceReportGroupWise", viewModel2);
                    }
                case 3:
                    reportdate = "Report Date From " + startReportDate + " To " + endReportDate;
                    title = "Trial Balance - Periodic" + title;
                    var reportPeriodic = UtilityService.GetTrialBalanceReportPeriodic(startDate, endDate, base.FiscalYear.Id, aggrp, agsubgrp, ledger, Subledger, branchId);
                    var viewModel3 = base.CreateReportViewModel<TrialBalanceReportPeriodicViewModel>(title, reportdate);
                    viewModel3.ReportList = reportPeriodic;
                    viewModel3.AccountGroup = model.AccountGroup;
                    viewModel3.Ledger = model.Ledger;
                    viewModel3.AccountSubGroup = model.AccountSubGroup;
                    viewModel3.SubLedger = model.SubLedger;
                    var openingList = from l in viewModel3.ReportList
                                      select new
                                      {
                                          Value = GetPositiveVal(l.OpnDebit)
                                      };
                    var periodList = from l in viewModel3.ReportList
                                     select new
                                     {
                                         Value = GetPositiveVal(l.PeriodCredit)
                                     };
                    var closingList = from l in viewModel3.ReportList
                                      select new
                                      {
                                          Value = GetPositiveVal(l.Balance)
                                      };
                    viewModel3.OpeningTotal = openingList.Sum(x => x.Value);
                    viewModel3.PeriodTotal = periodList.Sum(x => x.Value);
                    viewModel3.ClosingTotal = closingList.Sum(x => x.Value);

                    Session["TrialBalanceReportModel"] = viewModel3;
                    if (model.StepFormat == true)
                    {
                        Session["TrialBalanceReportType"] = "5";
                        return PartialView("TrialBalanceStepFormatReportPeriodic", viewModel3);
                    }

                    else
                    {
                        Session["TrialBalanceReportType"] = "3";
                        return PartialView("TrialBalanceReportPeriodic", viewModel3);
                    }

                default:
                    Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");
                    break;
            }
            return null;
        }

        public decimal GetPositiveVal(decimal val)
        {
            decimal result = 0;
            if (val > 0)
            {
                result = val;
            }
            return result;
        }
        public ActionResult PdfReportTrialBalance()
        {
            var reportype = Convert.ToInt32(Session["TrialBalanceReportType"]);
            switch (reportype)
            {
                case 1:
                    {
                        var view = (TrialBalanceReportNormalViewModel)Session["TrialBalanceReportModel"];
                        return this.ViewPdf("", "ReportLedger/PdfTrialBalanceNormalReport", view);
                        break;

                    }
                case 2:
                    {
                        var view = (TrialBalanceReportGroupWiseViewModel)Session["TrialBalanceReportModel"];
                        return this.ViewPdf("", "ReportLedger/PdfTrialBalanceGroupWiseReport", view);
                        break;

                    }
                case 3:
                    {
                        var view = (TrialBalanceReportPeriodicViewModel)Session["TrialBalanceReportModel"];
                        return this.ViewPdf("", "ReportLedger/PdfTrialBalancePeriodicReport", view);
                        break;

                    }
                case 4:
                    {
                        var view = (TrialBalanceReportGroupWiseViewModel)Session["TrialBalanceReportModel"];
                        return this.ViewPdf("", "ReportLedger/PdfTrialBalanceStepFormatReportGroupWise", view);
                        break;

                    }
                case 5:
                    {
                        var view = (TrialBalanceReportPeriodicViewModel)Session["TrialBalanceReportModel"];
                        return this.ViewPdf("", "ReportLedger/PdfTrialBalanceStepFormatReportPeriodic", view);
                        break;

                    }
            }
            return null;
        }

        public ActionResult ExcelReportTrialBalance()
        {
            var reportype = Convert.ToInt32(Session["TrialBalanceReportType"]);

            switch (reportype)
            {

                case 1:
                    {
                        var view = (TrialBalanceReportNormalViewModel)Session["TrialBalanceReportModel"];
                        return this.ViewExcel("", "ReportLedger/ExcelTrialBalanceNormalReport", view);
                        break;

                    }
                case 2:
                    {
                        var view = (TrialBalanceReportGroupWiseViewModel)Session["TrialBalanceReportModel"];
                        return this.ViewExcel("", "ReportLedger/ExcelTrialBalanceGroupWiseReport", view);
                        break;

                    }
                case 3:
                    {
                        var view = (TrialBalanceReportPeriodicViewModel)Session["TrialBalanceReportModel"];
                        return this.ViewExcel("", "ReportLedger/ExcelTrialBalancePeriodicReport", view);
                        break;

                    }
                case 4:
                    {
                        var view = (TrialBalanceReportGroupWiseViewModel)Session["TrialBalanceReportModel"];
                        return this.ViewExcel("", "ReportLedger/ExcelTrialBalanceStepFormatReportGroupWise", view);
                        break;

                    }
                case 5:
                    {
                        var view = (TrialBalanceReportPeriodicViewModel)Session["TrialBalanceReportModel"];
                        return this.ViewExcel("", "ReportLedger/ExcelTrialBalanceStepFormatReportPeriodic", view);
                        break;

                    }
            }
            return null;
        }
        #endregion
        #region BalanceSheet
        [CheckPermission(Permissions = "Navigate", Module = "LR-BSR")]
        public ActionResult BalanceSheet()
        {
            var viewModel = base.CreateViewModel<BalanceSheetFormViewModel>();
            viewModel.ReportTypeList = new SelectList(
                    Enum.GetValues(typeof(BalanceSheetReportTypeEnum)).Cast
                        <BalanceSheetReportTypeEnum>().Select(
                            x =>
                            new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                    "Value", "Text");
            viewModel.ReportFormatList = new SelectList(
                               Enum.GetValues(typeof(PLAndBSReportFormatEnum)).Cast
                                   <PLAndBSReportFormatEnum>().Select(
                                       x =>
                                       new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                               "Value", "Text");

            viewModel.ReportShowList = new SelectList(
                Enum.GetValues(typeof(BalanceSheetReportSohwEnum)).Cast
                    <BalanceSheetReportSohwEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                "Value", "Text");
            viewModel.AccountGroup = true;
            viewModel.Ledger = true;
            viewModel.AsOnDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.ReportType = 1;
            viewModel.ReportFormat = 1;
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
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BalanceSheet(BalanceSheetFormViewModel model)
        {
            ViewBag.ReportShow = model.ReportShow;
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;
            var aggrp = 0;
            if (model.AccountGroup == true)
                aggrp = 1;
            var agsubgrp = 0;
            if (model.AccountSubGroup == true)
                agsubgrp = 1;
            var ledger = 0;
            if (model.Ledger == true)
                ledger = 1;
            var Subledger = 0;
            if (model.SubLedger == true)
                Subledger = 1;
            var title = "";
            var BranchName = "";
            if (branchId != 0)
            {
                title = " (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }
            switch (model.ReportType)
            {
                case 1:
                    var asOnDate = model.AsOnDate;
                    var asOnReportDate = string.Empty;
                    if (base.SystemControl.DateType == 1)
                    {
                        asOnDate = model.AsOnDate;
                        if (model.DateShow == true)
                        {
                            asOnReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.AsOnDate)).Date;
                        }
                        else
                        {
                            asOnReportDate = model.AsOnDate;
                        }
                    }
                    if (base.SystemControl.DateType == 2)
                    {
                        asOnDate = NepaliDateService.NepalitoEnglishDate(model.AsOnDate).ToShortDateString();
                        if (model.DateShow == true)
                        {
                            asOnReportDate = model.AsOnDate;
                        }
                        else
                        {
                            asOnReportDate = model.AsOnDate;
                        }
                    }
                    var reportDate = "As On " + asOnReportDate;
                    title = "Balance Sheet" + title;
                    var viewModel = base.CreateReportViewModel<BalanceSheetViewModel>(title, reportDate);
                    Session["ReportType"] = "1";
                    viewModel.ReportShow = model.ReportShow;
                    viewModel.AccountGroup = model.AccountGroup;
                    viewModel.Ledger = model.Ledger;
                    viewModel.AccountSubGroup = model.AccountSubGroup;
                    viewModel.Subledger = model.SubLedger;
                    if (model.ReportFormat == 1)
                    {
                        Session["ReportFormat"] = false;
                    }
                    else
                    {
                        Session["ReportFormat"] = true;
                    }
                    if (model.AccountGroup)
                    {
                        viewModel.BsAccountGroupWiseList = UtilityService.GetBalanceAccountGroupWise(asOnDate, base.FiscalYear.Id, model.ReportShow, aggrp, agsubgrp, ledger, Subledger, branchId);
                        Session["ReportModel"] = viewModel;
                        Session["ReportType"] = "1";
                        Session["IsAccountGroup"] = true;
                        if (model.Ledger)
                        {
                            Session["IsLedger"] = true;
                            if (model.ReportFormat == 1)
                            {

                                return PartialView("_PartialBalanceSheetBothNormal", viewModel);
                            }

                            else
                            {

                                return PartialView("_PartialBalanceSheetTFormatNormalReport", viewModel);
                            }

                        }
                        else
                        {
                            Session["IsLedger"] = false;
                            if (model.ReportFormat == 1)
                                return PartialView("_PartialBalanceSheetAccountGroupWiseNormal", viewModel);
                            else
                                return PartialView("_PartialBalanceSheetTFormatNormalReport", viewModel);

                        }
                    }
                    viewModel.BsLedgerWiseList = UtilityService.GetBalanceLedgerWise(asOnDate, base.FiscalYear.Id, model.ReportShow, Subledger, branchId);
                    Session["ReportModel"] = viewModel;
                    Session["IsAccountGroup"] = false;
                    if (model.ReportFormat == 1)
                        return PartialView("_PartialBalaneSheetLedgerWiseNormal", viewModel);
                    else
                        return PartialView("_PartialBalanceSheetTFormatNormalReport", viewModel);

                case 2:
                    var startDate = model.StartDate;
                    var endDate = model.EndDate;
                    var startReportDate = string.Empty;
                    var endReportDate = string.Empty;
                    title = "Balance Sheet Details" + title;
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
                    var reportDate1 = "Report Date From " + startReportDate + " To " + endReportDate;

                    var viewModel1 = base.CreateReportViewModel<BalanceSheetViewModel>(title, reportDate1);
                    Session["ReportType"] = "2";
                    viewModel1.ReportShow = model.ReportShow;
                    viewModel1.AccountGroup = model.AccountGroup;
                    viewModel1.Ledger = model.Ledger;
                    viewModel1.AccountSubGroup = model.AccountSubGroup;
                    viewModel1.Subledger = model.SubLedger;
                    if (model.AccountGroup)
                    {
                        viewModel1.BsPeriodicList = UtilityService.GetBalancePeriodic(startDate, endDate, base.FiscalYear.Id, model.ReportShow, aggrp, agsubgrp, ledger, Subledger, branchId);
                        Session["ReportModel"] = viewModel1;
                        Session["IsAccountGroup"] = true;
                        if (model.Ledger)
                        {
                            Session["IsLedger"] = true;
                            if (model.ReportFormat == 1)
                                return PartialView("_PartialBalanceSheetBothPeriodic", viewModel1);
                            else
                                return PartialView("_PartialBalanceSheetTFormatPeriodicReport", viewModel1);
                        }
                        else
                        {
                            Session["IsLedger"] = false;
                            if (model.ReportFormat == 1)
                                return PartialView("_PartialBalanceSheetAccountGroupWisePeriodic", viewModel1);
                            else
                                return PartialView("_PartialBalanceSheetTFormatPeriodicReport", viewModel1);

                        }
                    }
                    Session["IsAccountGroup"] = false;
                    viewModel1.BsPeriodicList = UtilityService.GetBalancePeriodic(startDate, endDate, base.FiscalYear.Id, model.ReportShow, aggrp, agsubgrp, ledger, Subledger, branchId);
                    Session["ReportModel"] = viewModel1;
                    if (model.ReportFormat == 1)
                        return PartialView("_PartialBalaneSheetLedgerWisePeriodic", viewModel1);
                    else
                        return PartialView("_PartialBalanceSheetTFormatPeriodicReport", viewModel1);

            }
            return View();
        }
        public ActionResult PdfBalanceSheet()
        {

            var reportype = Convert.ToInt32(Session["ReportType"]);
            var isAccountGroup = Convert.ToBoolean(Session["IsAccountGroup"]);
            bool isLedger = Convert.ToBoolean(Session["IsLedger"]);
            var view = (BalanceSheetViewModel)Session["ReportModel"];
            var isReportFormat = Convert.ToBoolean(Session["ReportFormat"]);
            switch (reportype)
            {
                case 1:
                    if (isAccountGroup)
                    {
                        if (isLedger)
                        {
                            //var view = (BalanceSheetViewModel)Session["ReportModel"];
                            if (isReportFormat)
                            {
                                return this.ViewPdf("", "ReportLedger/PdfBalanceSheetTFormatNormalReport", view);
                            }
                            else
                            {
                                return this.ViewPdf("", "ReportLedger/PdfBalanceSheetBothNormal", view);
                            }

                        }
                        else
                        {

                            if (isReportFormat)
                                return this.ViewPdf("", "ReportLedger/PdfBalanceSheetTFormatNormalReport", view);
                            else return this.ViewPdf("", "ReportLedger/PdfBalanceSheetAccountGroupWiseNormal", view);


                        }
                    }
                    if (isReportFormat)
                        return this.ViewPdf("", "ReportLedger/PdfBalanceSheetTFormatNormalReport", view);
                    else
                        return this.ViewPdf("", "ReportLedger/PdfBalaneSheetLedgerWiseNormalReport", view);

                //return this.ViewPdf("", "ReportLedger/PdfBalaneSheetLedgerWiseNormalReport", view);
                case 2:
                    if (isAccountGroup)
                    {
                        if (isLedger)
                        {


                            if (isReportFormat)
                                return this.ViewPdf("", "ReportLedger/PdfBalanceSheetTFormatNormalReport", view);

                            else
                                return this.ViewPdf("", "ReportLedger/PdfBalanceSheetBothPeriodic", view);



                        }
                        else
                        {
                            if (isReportFormat)
                                return this.ViewPdf("", "ReportLedger/PdfBalanceSheetTFormatPeriodicReport", view);
                            else
                                return this.ViewPdf("", "ReportLedger/PdfBalanceSheetAccountGroupWisePeriodic", view);


                        }
                    }

                    if (isReportFormat)

                        return this.ViewPdf("", "ReportLedger/PdfBalanceSheetTFormatPeriodicReport", view);

                    else
                        return this.ViewPdf("", "ReportLedger/PdfBalaneSheetLedgerWisePeriodicReport", view);
                //return this.ViewPdf("", "ReportLedger/PdfBalaneSheetLedgerWisePeriodicReport", view);
            }
            return null;
        }

        public ActionResult ExcelBalanceSheet()
        {
            var reportype = Convert.ToInt32(Session["ReportType"]);
            var isAccountGroup = Convert.ToBoolean(Session["IsAccountGroup"]);
            bool isLedger = Convert.ToBoolean(Session["IsLedger"]);
            var view = (BalanceSheetViewModel)Session["ReportModel"];
            var isReportFormat = Convert.ToBoolean(Session["ReportFormat"]);
            switch (reportype)
            {
                case 1:
                    if (isAccountGroup)
                    {
                        if (isLedger)
                        {
                            //var view = (BalanceSheetViewModel)Session["ReportModel"];
                            if (isReportFormat)
                            {
                                return this.ViewPdf("", "ReportLedger/ExcelBalanceSheetTFormatNormalReport", view);
                            }
                            else
                            {
                                return this.ViewPdf("", "ReportLedger/ExcelBalanceSheetBothNormal", view);
                            }

                        }
                        else
                        {

                            if (isReportFormat)
                                return this.ViewPdf("", "ReportLedger/ExcelBalanceSheetTFormatNormalReport", view);
                            else return this.ViewPdf("", "ReportLedger/ExcelBalanceSheetAccountGroupWiseNormal", view);


                        }
                    }
                    if (isReportFormat)
                        return this.ViewPdf("", "ReportLedger/ExcelBalanceSheetTFormatNormalReport", view);
                    else
                        return this.ViewPdf("", "ReportLedger/ExcelBalaneSheetLedgerWiseNormalReport", view);

                //return this.ViewPdf("", "ReportLedger/PdfBalaneSheetLedgerWiseNormalReport", view);
                case 2:
                    if (isAccountGroup)
                    {
                        if (isLedger)
                        {


                            if (isReportFormat)
                                return this.ViewPdf("", "ReportLedger/ExcelBalanceSheetTFormatNormalReport", view);

                            else
                                return this.ViewPdf("", "ReportLedger/ExcelBalanceSheetBothPeriodic", view);



                        }
                        else
                        {
                            if (isReportFormat)
                                return this.ViewPdf("", "ReportLedger/ExcelBalanceSheetTFormatPeriodicReport", view);
                            else
                                return this.ViewPdf("", "ReportLedger/ExcelBalanceSheetAccountGroupWisePeriodic", view);


                        }
                    }

                    if (isReportFormat)

                        return this.ViewPdf("", "ReportLedger/ExcelBalanceSheetTFormatPeriodicReport", view);

                    else
                        return this.ViewPdf("", "ReportLedger/ExcelBalaneSheetLedgerWisePeriodicReport", view);
                //return this.ViewPdf("", "ReportLedger/PdfBalaneSheetLedgerWisePeriodicReport", view);
            }
            return null;
        }
        #endregion
        #region Profit & Loss
        [CheckPermission(Permissions = "Navigate", Module = "LR-PLR")]
        public ActionResult ProfitAndLoss()
        {
            var viewModel = base.CreateViewModel<ProfitAndLossFormViewModel>();
            viewModel.ReportTypeList = new SelectList(
                    Enum.GetValues(typeof(ProfitAndLossReportTypeEnum)).Cast
                        <ProfitAndLossReportTypeEnum>().Select(
                            x =>
                            new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                    "Value", "Text");
            viewModel.ReportFormatList = new SelectList(
                   Enum.GetValues(typeof(PLAndBSReportFormatEnum)).Cast
                       <PLAndBSReportFormatEnum>().Select(
                           x =>
                           new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                   "Value", "Text");

            viewModel.AccountGroup = true;
            viewModel.Ledger = true;
            viewModel.AsOnDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.ReportType = 1;
            viewModel.ReportFormat = 1;
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
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ProfitAndLossReport(ProfitAndLossFormViewModel model)
        {
            var startDate = model.StartDate;
            var endDate = model.EndDate;
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            var asOnDate = model.AsOnDate;
            var asOnReportDate = string.Empty;
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;
            string title = string.Empty;
            string reportdate = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                asOnDate = model.AsOnDate;
                startDate = model.StartDate;
                endDate = model.EndDate;
                if (model.DateShow == true)
                {
                    asOnReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.AsOnDate)).Date;
                    startReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.StartDate)).Date;
                    endReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(model.EndDate)).Date;
                }
                else
                {
                    asOnReportDate = model.AsOnDate;
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }
            }
            if (base.SystemControl.DateType == 2)
            {
                asOnDate = NepaliDateService.NepalitoEnglishDate(model.AsOnDate).ToShortDateString();
                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToShortDateString();
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToShortDateString();
                if (model.DateShow == true)
                {
                    asOnReportDate = model.AsOnDate;
                    startReportDate = startDate;
                    endReportDate = endDate;
                }
                else
                {
                    asOnReportDate = model.AsOnDate;
                    startReportDate = model.StartDate;
                    endReportDate = model.EndDate;
                }
            }

            var BranchName = "";
            if (branchId != 0)
            {
                title = " (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }
            title = "Profit And Loss A/C" + title;
            switch (model.ReportType)
            {
                case 1:
                    reportdate = "As On " + asOnReportDate;
                    var viewModel1 = base.CreateReportViewModel<ProfitAndLossReportNormalViewModel>(title, reportdate);
                    viewModel1.AsOnDate = asOnDate;
                    viewModel1.BranchId = branchId;
                    viewModel1.AccountGroup = model.AccountGroup;
                    viewModel1.Ledger = model.Ledger;
                    viewModel1.AccountSubGroup = model.AccountSubGroup;
                    viewModel1.Subledger = model.SubLedger;
                    Session.RemoveAll();

                    Session["ProfitAndLossReportModel"] = viewModel1;
                    if (model.ReportFormat == 1)
                    {
                        Session["ProfitAndLossReportType"] = "1";
                        return PartialView("ProfitAndLossReportNormal", viewModel1);
                    }

                    else
                    {
                        Session["ProfitAndLossReportType"] = "3";
                        return PartialView("ProfitAndLossReportTFormatNormal", viewModel1);
                    }


                case 2:
                    reportdate = "Report Date From " + startReportDate + " To " + endReportDate;
                    var viewModel2 = base.CreateReportViewModel<ProfitAndLossReportPeriodicViewModel>(title, reportdate);
                    viewModel2.StartDate = startDate;
                    viewModel2.EndDate = endDate;
                    viewModel2.BranchId = branchId;
                    viewModel2.AccountGroup = model.AccountGroup;
                    viewModel2.Ledger = model.Ledger;
                    viewModel2.AccountSubGroup = model.AccountSubGroup;
                    viewModel2.Subledger = model.SubLedger;
                    viewModel2.DisplayRemarks = viewModel2.DisplayRemarks;
                    Session.RemoveAll();

                    Session["ProfitAndLossReportModel"] = viewModel2;
                    if (model.ReportFormat == 1)
                    {
                        Session["ProfitAndLossReportType"] = "2";
                        return PartialView("ProfitAndLossReportPeriodic", viewModel2);
                    }

                    else
                    {
                        Session["ProfitAndLossReportType"] = "4";
                        return PartialView("ProfitAndLossReportTFormatPeriodic", viewModel2);
                    }



                default:
                    Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");
                    break;
            }
            return null;
        }

        public ActionResult PdfProfitAndLoss()
        {
            var reportype = Convert.ToInt32(Session["ProfitAndLossReportType"]);

            switch (reportype)
            {
                case 1:
                    {
                        var view = (ProfitAndLossReportNormalViewModel)Session["ProfitAndLossReportModel"];

                        return this.ViewPdf("", "ReportLedger/PdfProfitAndLossNormalReport", view);
                        break;
                    }
                case 2:
                    {
                        var view = (ProfitAndLossReportPeriodicViewModel)Session["ProfitAndLossReportModel"];
                        return this.ViewPdf("", "ReportLedger/PdfProfitAndLossPeriodicReport", view);
                        break;
                    }
                case 3:
                    {
                        var view = (ProfitAndLossReportNormalViewModel)Session["ProfitAndLossReportModel"];
                        return this.ViewPdf("", "ReportLedger/PdfProfitAndLossReportTFormatNormal", view);
                        break;
                    }
                case 4:
                    {
                        var view = (ProfitAndLossReportPeriodicViewModel)Session["ProfitAndLossReportModel"];
                        return this.ViewPdf("", "ReportLedger/PdfProfitAndLossReportTFormatPeriodic", view);
                        break;
                    }
            }
            return null;
        }

        public ActionResult ExcelProfitAndLoss()
        {
            var reportype = Convert.ToInt32(Session["ProfitAndLossReportType"]);

            switch (reportype)
            {
                case 1:
                    {
                        var view = (ProfitAndLossReportNormalViewModel)Session["ProfitAndLossReportModel"];
                        return this.ViewExcel("", "ReportLedger/ExcelProfitAndLossNormalReport", view);
                        break;
                    }
                case 2:
                    {
                        var view = (ProfitAndLossReportPeriodicViewModel)Session["ProfitAndLossReportModel"];
                        return this.ViewExcel("", "ReportLedger/ExcelProfitAndLossPeriodicReport", view);
                        break;
                    }
                case 3:
                    {
                        var view = (ProfitAndLossReportNormalViewModel)Session["ProfitAndLossReportModel"];
                        return this.ViewExcel("", "ReportLedger/ExcelProfitAndLossReportTFormatNormal", view);
                        break;
                    }
                case 4:
                    {
                        var view = (ProfitAndLossReportPeriodicViewModel)Session["ProfitAndLossReportModel"];
                        return this.ViewExcel("", "ReportLedger/ExcelProfitAndLossReportTFormatPeriodic", view);
                        break;
                    }
            }
            return null;
        }
        #endregion

        #region Ledger
        

        public ActionResult GetLedgerCategoryOther()
        {
            string other = StringEnum.Parse(typeof(LedgerCategoryEnum), "Other").ToString();
            var ledgers = _ledger.Filter(x => !x.IsDeleted && x.Category == other).Select(x => new
            {
                Id = x.Id,
                Description = x.AccountName,
                ShortName = x.ShortName
            }).OrderBy(x => x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLedgerListByType(int reportType)
        {
            var type = string.Empty;
            if (reportType == 2)
            {
                type = StringEnum.Parse(typeof(LedgerCategoryEnum), "Customer").ToString();
            }
            if (reportType == 3)
            {
                type = StringEnum.Parse(typeof(LedgerCategoryEnum), "Vendor").ToString();
            }
            if (reportType == 4)
            {
                type = StringEnum.Parse(typeof(LedgerCategoryEnum), "Both").ToString();
            }
            if (reportType == 5)
            {
                type = StringEnum.Parse(typeof(LedgerCategoryEnum), "Other").ToString();
            }
            var ledgers = _ledger.Filter(x => !x.IsDeleted && (type == string.Empty || x.Category == type)).Select(x => new
            {
                Id = x.Id,
                Description = x.AccountName,
                ShortName = x.ShortName
            }).OrderBy(x => x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

         [CheckPermission(Permissions = "Navigate", Module = "LR-LR")]
        public ActionResult LedgerReport()
        {
            var viewModel = base.CreateViewModel<LedgerReportFormViewModel>();
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
            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();

            viewModel.ReportTypeList = new SelectList(
                  Enum.GetValues(typeof(LedgerReportTypeEnum)).Cast
                      <LedgerReportTypeEnum>().Select(
                          x =>
                          new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                  "Value", "Text");

            viewModel.OpeningList = new SelectList(
                  Enum.GetValues(typeof(OpeningReportTypeEnum)).Cast
                      <OpeningReportTypeEnum>().Select(
                          x =>
                          new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                  "Value", "Text");

            viewModel.GroupByList = new SelectList(
                 Enum.GetValues(typeof(LedgerReportGroupByEnum)).Cast
                     <LedgerReportGroupByEnum>().Select(
                         x =>
                         new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                 "Value", "Text");

            viewModel.AllLedger = false;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult LedgerReport(LedgerReportFormViewModel model, string Groups, string ledgers, string subLedgers, bool? includeSubLedger, bool? remarks, bool? summary, int reportType, bool? productDetails, bool? zeroBalance, int groupBy, bool? docAgent)
        {
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;

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

            #region -- page -----
            var reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            var title = string.Empty;

            if (summary == true)
            {
                if (groupBy == 1)
                    title += "Ledger Summary";
                else if (groupBy == 2)
                    title += "Ledger Summary - Agent Wise";
                else if (groupBy == 3)
                    title += "Ledger Summary - Area Wise";
                else if (groupBy == 4)
                    title += "Ledger Summary - Account Group Wise";
                else if (groupBy == 5)
                    title += "Ledger Summary - Account Sub Group Wise";
            }
            else
            {
                if (groupBy == 1)
                    title += "Ledger Details";
                else if (groupBy == 2)
                    title += "Ledger Details - Agent Wise";
                else if (groupBy == 3)
                    title += "Ledger Details - Area Wise";
                else if (groupBy == 4)
                    title += "Ledger Details - Account Group Wise";
                else if (groupBy == 5)
                    title += "Ledger Details - Account Sub Group Wise";
            }
            var BranchName = "";
            if (branchId != 0)
            {
                title += " (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }
            var viewModel = base.CreateReportViewModel<LedgerReportViewModel>(title, reportDate);
            #endregion


            string catList = string.Empty;
            switch (reportType)
            {
                case 1: catList = "A"; break;
                case 2: catList = "CU"; break;
                case 3: catList = "VE"; break;
                case 4: catList = "BO"; break;
                case 5: catList = "OT"; break;
            }

            if (includeSubLedger == true)
            {
                viewModel.DisplaySubledger = Convert.ToBoolean(includeSubLedger);
                viewModel.SubLedgers = subLedgers;
            }

            bool? docAgent1 = true;
            var ledgerList = UtilityService.GetLedgers(startDate, endDate, catList, Groups, ledgers, base.FiscalYear.Id, groupBy, docAgent, branchId);
            viewModel.LedgerList = ledgerList;
            viewModel.StartDate = startDate;
            viewModel.DisplayRemarks = model.Remarks;
            viewModel.DisplayProductDetails = model.ProductDetails;
            viewModel.DisplayTermDetails = model.TermDetails;
            viewModel.DisplayZeroBalance = model.ZeroBalance;
            viewModel.EndDate = endDate;
            viewModel.GroupBy = groupBy;
            model.GroupBy = groupBy;
            viewModel.BranchId = branchId;
            if (docAgent == true)
                viewModel.DocAgent = true;
            else
                viewModel.DocAgent = false;

            //viewModel.DisplaySubledger = model.SubLedger;
            viewModel.FYId = base.FiscalYear.Id;
            viewModel.Datetype = base.SystemControl.DateType;
            if (model.DateShow == true)
            {
                if (viewModel.Datetype == 1)
                {
                    viewModel.Datetype = 2;
                }
                else
                {
                    viewModel.Datetype = 1;
                }
            }
            viewModel.Summary = summary.HasValue && summary == true;
            Session["ReportType"] = "1";
            Session["LedgerReportViewModel"] = viewModel;

            if (summary == true)
                return PartialView("_PartialLedgerSummaryReport", viewModel);
            else
                return PartialView("_PartialLedgerReport", viewModel);
        }

        public ActionResult PdfLedgerReport()
        {
            var view = (LedgerReportViewModel)Session["LedgerReportViewModel"];
            if (view.Summary)
            {
                return this.ViewPdf("", "ReportLedger/PdfLedgerSummaryReport", view);
            }
            else
            {
                return this.ViewPdf("", "ReportLedger/PdfLedgerReport", view);
            }
        }
        public ActionResult ExcelLedgerReport()
        {
            var view = (LedgerReportViewModel)Session["LedgerReportViewModel"];
            if (view.Summary)
            {
                return this.ViewExcel("", "ReportLedger/ExcelLedgerSummaryReport", view);
            }
            else
            {
                return this.ViewExcel("", "ReportLedger/ExcelLedgerReport", view);
            }
        }
        #endregion

        #region Cash Flow
        [CheckPermission(Permissions = "Navigate", Module = "LRCF")]
        public ActionResult CashFlow()
        {
            var viewModel = base.CreateViewModel<CashFlowViewModel>();
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
            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;

            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CashFlowReport(CashFlowViewModel model)
        {
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            var startDate = string.Empty;
            var endDate = string.Empty;
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;
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
            reportDate = "Report Date From " + startReportDate + " To " + endReportDate;

            var title = string.Empty;
            title = "Cash Flow - " + UtilityService.GetLedgerNameById(model.LedgerId);
            var BranchName = "";
            if (branchId != 0)
            {
                title += " (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }
            var viewModel = base.CreateReportViewModel<CashFlowReportViewModel>(title, reportDate);
            viewModel.LedgerId = model.LedgerId;
            viewModel.branchId = branchId;
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;

            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;
            return PartialView("_PartialCashFlowReport", viewModel);
        }

        public ActionResult PdfCashFlowReport()
        {
            var view = (CashFlowReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "ReportLedger/PdfCashFlowReport", view);
        }
        public ActionResult ExcelCashFlowReport()
        {
            var view = (CashFlowReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "ReportLedger/ExcelCashFlowReport", view);
        }

        #endregion

        #region Ledger List
        [CheckPermission(Permissions = "Navigate", Module = "LRLL")]
        public ActionResult LedgerList()
        {
            var viewModel = base.CreateViewModel<LedgerListViewModel>();

            viewModel.ReportTypeList = new SelectList(
                 Enum.GetValues(typeof(LedgerReportTypeEnum)).Cast
                     <LedgerReportTypeEnum>().Select(
                         x =>
                         new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                 "Value", "Text");

            viewModel.GroupByList = new SelectList(
               Enum.GetValues(typeof(LedgerReportGroupByEnum)).Cast
                   <LedgerReportGroupByEnum>().Select(
                       x =>
                       new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
               "Value", "Text");

            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult LedgerListReport(LedgerListViewModel model, string ledgersList, int GroupBy, int reportType, bool? inclideOpening)
        {
            var reportDate = "";

            var title = "Ledger List";
            if (inclideOpening == true)
                title = "Ledger Opening List";

            var viewModel = base.CreateReportViewModel<LedgerListReportViewModel>(title, reportDate);

            string LedgerCat = string.Empty;
            switch (reportType)
            {
                case 1: LedgerCat = "A"; break;
                case 2: LedgerCat = "CU"; break;
                case 3: LedgerCat = "VE"; break;
                case 4: LedgerCat = "BO"; break;
                case 5: LedgerCat = "OT"; break;
            }

            if (ledgersList == "")
                ledgersList = "0";

            var ledgerList = UtilityService.GetLedgerListByCategory(LedgerCat, ledgersList, GroupBy, base.FiscalYear.Id);
            viewModel.LedgerList = ledgerList;
            viewModel.LedgerId = model.LedgerId;
            viewModel.AllLedger = model.AllLedger;
            viewModel.ReportType = model.ReportType;
            viewModel.GroupBy = model.GroupBy;
            viewModel.InclideOpening = Convert.ToBoolean(inclideOpening);

            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;
            return PartialView("_PartialLedgerListReport", viewModel);
        }

        public ActionResult PdfLedgerListReport()
        {
            var view = (LedgerListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "ReportLedger/PdfLedgerListReport", view);
        }
        public ActionResult ExcelLedgerListReport()
        {
            var view = (LedgerListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "ReportLedger/ExcelLedgerListReport", view);
        }

        #endregion

        #region Cash/Bank Book
        [CheckPermission(Permissions = "Navigate", Module = "LR-CBR")]
        public ActionResult CashBanKBook()
        {
            var viewModel = base.CreateViewModel<CashBankBookViewModel>();
            var user = _authentication.GetAuthenticatedUser();

            //viewModel.StartDate = FiscalYear.StartDate;
            //viewModel.EndDate = FiscalYear.EndDate;
            var branchList = _companyInfo.GetAll();
            viewModel.BranchList = new SelectList(branchList, "Id", "Name");
            if (branchList.Any())
            {
                viewModel.CheckBranch = true;
                if (!user.AllBranch)
                {
                    viewModel.BranchList = new SelectList(_companyInfo.GetMany(x => x.Id == user.BranchId), "Id", "Name");
                }


            }


            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;

            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CashBankBookReport(CashBankBookViewModel model)
        {

            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            var startDate = string.Empty;
            var endDate = string.Empty;
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;


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

            var datelist = UtilityService.GetDateList(startDate, endDate, model.LedgerId, base.FiscalYear.Id, branchId);

            var title = string.Empty;
            title = "Cash/Bank - " + UtilityService.GetLedgerNameById(model.LedgerId);
            var BranchName = "";
            if (branchId != 0)
            {
                title += " (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }

            var viewModel = base.CreateReportViewModel<CashBankBookReportViewModel>(title, reportDate);
            viewModel.DisplayRemarks = model.Remarks;
            viewModel.DisplaySubLedger = model.Subledger;
            viewModel.DateList = datelist;
            viewModel.LedgerId = model.LedgerId;
            viewModel.StartDate = startDate;
            viewModel.FStartDate = base.FiscalYear.StartDate;
            viewModel.EndDate = endDate;
            viewModel.Datetype = base.SystemControl.DateType;
            if (model.DateShow == true)
            {
                if (viewModel.Datetype == 1)
                {
                    viewModel.Datetype = 2;
                }
                else
                {
                    viewModel.Datetype = 1;
                }
            }
            viewModel.BranchId = branchId;
            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;
            return PartialView("CashBankBookReport", viewModel);
        }
        public ActionResult PdfCashBankReport()
        {

            var view = (CashBankBookReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "ReportLedger/PdfCashBankBookReport", view);
        }
        public ActionResult ExcelCashBankReport()
        {
            var view = (CashBankBookReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "ReportLedger/ExcelCashBankBookReport", view);
        }

        #endregion

        #region Cash/Bank Summary
        [CheckPermission(Permissions = "Navigate", Module = "LR-CSR")]
        public ActionResult CashBanKBookSummary()
        {
            var viewModel = base.CreateViewModel<CashBankBookViewModel>();
            //viewModel.StartDate = FiscalYear.StartDate;
            //viewModel.EndDate = FiscalYear.EndDate;

            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
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

            return View("CashBankBookSummary", viewModel);
        }

        [HttpPost]
        public ActionResult CashBanKBookSummaryReport(CashBankBookViewModel model)
        {
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;
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
                {
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                }
                else
                {
                    reportDate = "Report Date From " + startDate + " To " + endDate;
                }
            }
            else
            {
                reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            }

            var summeries = UtilityService.GetCashBankSummaries(model.LedgerId, startDate, endDate, base.FiscalYear.Id, branchId);

            var title = string.Empty;
            title = "Cash/Bank Summary - " + UtilityService.GetLedgerNameById(model.LedgerId);

            var BranchName = "";
            if (branchId != 0)
            {
                title += " (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }
            var viewModel = base.CreateReportViewModel<CashBankSummaryReportViewModel>(title, reportDate);

            viewModel.SpCashBankBookSummaries = summeries;
            viewModel.Datetype = base.SystemControl.DateType;
            if (model.DateShow == true)
            {
                if (viewModel.Datetype == 1)
                {
                    viewModel.Datetype = 2;
                }
                else
                {
                    viewModel.Datetype = 1;
                }
            }
            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;
            return PartialView("CashBankSummaryReport", viewModel);
        }

        public ActionResult PdfCashBankSummaryReport()
        {

            var view = (CashBankSummaryReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "ReportLedger/PdfCashBankSummaryReport", view);
        }
        public ActionResult ExcelCashBankSummaryReport()
        {

            var view = (CashBankSummaryReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "ReportLedger/ExcelCashBankSummaryReport", view);
        }
        #endregion

        //#region Cash/Bank CheckList
        //        public ActionResult CashBanKCheckList()
        //        {
        //            var viewModel = base.CreateViewModel<CashBankBookViewModel>();
        //            viewModel.StartDate = FiscalYear.StartDate;
        //            viewModel.EndDate = FiscalYear.EndDate;
        //            return View(viewModel);
        //        }
        //        [HttpPost]
        //        public ActionResult CashBankCheckListReport(CashBankBookViewModel model)
        //        {
        //            var startReportDate = model.StartDate.Date.ToString("MM/dd/yyyy");
        //            var endReportDate = model.EndDate.Date.ToString("MM/dd/yyyy");
        //            var startDate = model.StartDate.Date.ToString("yyyy-MM-dd");
        //            var endDate = model.EndDate.Date.ToString("yyyy-MM-dd");
        //            var reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
        //            var datelist = UtilityService.GetDateList(startDate, endDate, model.LedgerId, base.FiscalYear.Id);
        //            var viewModel = base.CreateReportViewModel<CashBankBookReportViewModel>("Cash/Bank CheckList", reportDate);
        //            viewModel.DateList = datelist;
        //            viewModel.LedgerId = model.LedgerId;
        //            viewModel.StartDate = startDate;
        //            viewModel.EndDate = endDate;
        //            Session["ReportType"] = "1";
        //            Session["ReportModel"] = viewModel;
        //            return PartialView("CashBankBookReport", viewModel);
        //        }
        //#endregion

        #region Journal Voucher
        [CheckPermission(Permissions = "Navigate", Module = "LR-JVR")]
        public ActionResult JournalVoucher()
        {
            var viewModel = base.CreateViewModel<JournalVoucherFormViewModel>();
            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
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

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult JournalVoucher(JournalVoucherFormViewModel model)
        {
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            var startDate = string.Empty;
            var endDate = string.Empty;
            var reportname = "";
            reportname = "Journal Voucher";
            var BranchName = "";
            if (branchId != 0)
            {
                reportname = "Journal Voucher (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }
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

                startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToString("yyyy-MM-dd");
                endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToString("yyyy-MM-dd");
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
            var JVDates = UtilityService.GetJournalVoucherDates(startDate, endDate, base.FiscalYear.Id, branchId);
            var reportDate = "";

            if (model.DateShow == true)
            {
                if (base.SystemControl.DateType == 1)
                {
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                }
                else
                {
                    reportDate = "Report Date From " + startDate + " To " + endDate;
                }
            }
            else
            {
                reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            }
            //var viewModel = base.CreateReportViewModel<JournalVoucherViewModel>("Journal Voucher", reportDate,BranchName);
            var viewModel = base.CreateReportViewModel<JournalVoucherViewModel>(reportname, reportDate);
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.JVDateList = JVDates;
            viewModel.DisplayRemarks = model.Remarks;
            viewModel.DisplayDetails = model.Details;
            viewModel.DisplaySubLedger = model.SubLedger;
            viewModel.Datetype = base.SystemControl.DateType;
            if (model.DateShow == true)
            {
                if (viewModel.Datetype == 1)
                {
                    viewModel.Datetype = 2;
                }
                else
                {
                    viewModel.Datetype = 1;
                }
            }
            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;
            return PartialView("_PartialJournalVoucher", viewModel);
        }
        public ActionResult PdfJournalVoucherReport()
        {

            var view = (JournalVoucherViewModel)Session["ReportModel"];
            return this.ViewPdf("", "ReportLedger/PdfJournalVoucherReport", view);
        }
        public ActionResult ExcelJournalVoucherReport()
        {

            var view = (JournalVoucherViewModel)Session["ReportModel"];
            return this.ViewExcel("", "ReportLedger/ExcelJournalVoucherReport", view);
        }
        #endregion

        #region DayBook
        [CheckPermission(Permissions = "Navigate", Module = "LR-DBR")]
        public ActionResult DayBook()
        {
            var viewModel = base.CreateViewModel<DayBookFormViewModel>();
            viewModel.StartDate = base.SystemControl.DateType == 1 ? FiscalYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? FiscalYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(FiscalYear.EndDate).Date;
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            viewModel.ModuleList = KRBAccounting.Service.UtilityService.ModuleCheckboxList();
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

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DayBook(string[] modules, DayBookFormViewModel model)
        {
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;
            var s = "%";
            string sourceList = String.Join("%, %", modules);
            sourceList = s + sourceList + s;
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
                {
                    reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
                }
                else
                {
                    reportDate = "Report Date From " + startDate + " To " + endDate;
                }
            }
            else
            {
                reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            }
            ViewBag.SourceList = sourceList;
            var title = string.Empty;
            title = "Day Book DateWise";
            var BranchName = "";
            if (branchId != 0)
            {
                title += " (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }
            //Sourcelist is removed for time being. Need to add in future
            var dates = UtilityService.GetDayBookDates(startDate, endDate, base.FiscalYear.Id, sourceList, branchId);
            var viewModel = base.CreateReportViewModel<DayBookViewModel>(title, reportDate);
            viewModel.DisplayRemarks = model.Remarks;
            viewModel.DisplaySubLedger = model.SubLedger;
            viewModel.Datetype = base.SystemControl.DateType;
            viewModel.SourceList = sourceList;
            viewModel.StartDate = startDate;
            if (model.DateShow == true)
            {
                if (viewModel.Datetype == 1)
                {
                    viewModel.Datetype = 2;
                }
                else
                {
                    viewModel.Datetype = 1;
                }
            }

            viewModel.BranchId = branchId;
            viewModel.DBDateList = dates;
            Session["ReportType"] = "1";
            Session["ReportModel"] = viewModel;
            //Need to calculate Closing Balance. confusing so skipped for now
            return PartialView("_PartialDayBook", viewModel);
        }
        public ActionResult PdfDayBookReport()
        {

            var view = (DayBookViewModel)Session["ReportModel"];
            return this.ViewPdf("", "ReportLedger/PdfDayBookReport", view);
        }
        public ActionResult ExcelDayBookReport()
        {

            var view = (DayBookViewModel)Session["ReportModel"];
            return this.ViewExcel("", "ReportLedger/ExcelDayBookReport", view);
        }
        #endregion

        #endregion
        #region Opening_TrialBalance

        public ActionResult OpeningTrialBalance()
        {
            var model = new TrialBalanceOpening();
            var branchList = _companyInfo.GetAll();
            if (branchList.Any())
            {
                model.CheckBranch = true;
            }

            model.BranchList = new SelectList(branchList, "Id", "Name");


            return View(model);
        }
        [HttpPost]
        public ActionResult OpeningTrialBalance(TrialBalanceOpening model)
        {
            // var date = DateTime.UtcNow.ToShortDateString();
            var branchId = model.BranchId != null ? Convert.ToInt32(model.BranchId) : 0;
            var date = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            var BranchName = "";
            var title = "";
            title = "Opening Trial Balance";
            if (branchId != 0)
            {
                title += " (For " + _companyInfo.GetById(branchId).Name + "  Branch)";
                BranchName = "Branch Name  :    " + _companyInfo.GetById(branchId).Name;
            }
            var viewModel = base.CreateReportViewModel<TrialBalanceOpening>(title, date);
            viewModel.ReportType = model.ReportType;
            if (model.ReportType == "Normal")
            {
                var data = _context.Database.SqlQuery<SP_OpeningTrialBalance>("SP_OpeningTrialBalance @Type='L'" + ",@FYId=" + base.FiscalYear.Id + ",@BranchId=" + branchId);
                viewModel.OpeningLedgers = data;
            }
            else
            {
                viewModel.AccountGroup = true;
                var data = _context.Database.SqlQuery<SP_OpeningTrialBalance>("SP_OpeningTrialBalance @Type='G'" + ",@FYId=" + base.FiscalYear.Id + ",@BranchId=" + branchId);
                viewModel.OpeningsGroups = data;

                viewModel.OpeningLedgers = _context.Database.SqlQuery<SP_OpeningTrialBalance>("SP_OpeningTrialBalance @Type='L'" + ",@FYId=" + base.FiscalYear.Id + ",@BranchId=" + branchId);
            }
            if (model.SubLedger)
            {
                viewModel.SubLedger = true;
                var subledgers = _context.Database.SqlQuery<SP_OpeningTrialBalance>("SP_OpeningTrialBalance @Type='S'" + ",@FYId=" + base.FiscalYear.Id + ",@BranchId=" + branchId);
                viewModel.OpeningsSubLedgers = subledgers;
            }
            Session["ReportOpeningTrialBalance"] = viewModel;
            return PartialView("_PartialOpeningTrialBalanceReport", viewModel);
        }

        public ActionResult PdfOpeningTrialBalance()
        {
            var model = (TrialBalanceOpening)Session["ReportOpeningTrialBalance"];
            return this.ViewPdf("", "ReportLedger/PdfOpeningTrialBalance", model);
        }

        public ActionResult ExcelOpeningTrialBalance()
        {
            var model = (TrialBalanceOpening)Session["ReportOpeningTrialBalance"];
            return this.ViewExcel("", "ReportLedger/ExcelOpeningTrialBalance", model);
            //  return PartialView("ReportLedger/PdfOpeningTrialBalance", model);


        }
        #endregion


    }
}
