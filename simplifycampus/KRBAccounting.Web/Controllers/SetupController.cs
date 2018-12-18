using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.Setup;
using KRBAccounting.Enums;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.Controllers
{
    [Authorize]
    public class SetupController : BaseController
    {
        private readonly IDocumentNumeringSchemeRepository _documentNumeringSchemeRepository;
        private readonly IStudentRegistrationNumberingRepository _studentRegistrationNumberingRepository;
        private readonly IDesignRepository _designRepository;
        private readonly ISystemControlRepository _systemControlRepository;
        private readonly ILedgerRepository _ledgerRepository;
        private readonly ISecurityService _securityService;
        private readonly IFiscalYearRepository _fiscalYear;
        private readonly IFormsAuthenticationService _authentication;
        private readonly IEntryControlPLRepository _entryControlPL;
        private readonly IEntryControlSalesRepository _entryControlSales;
        private readonly IEntryControlPurchaseRepository _entryControlPurchase;
        private readonly IEntryControlInventoryRepository _entryControlInventory;
        private readonly IAccountGroupRepository _accountGroupRepository;
        private readonly IBillingTermRepository _billingTermRepository;
        private readonly IAcademicYearRepository _academicYearRepository;
        public SetupController(
            IDesignRepository designRepository,
            IDocumentNumeringSchemeRepository documentNumeringSchemeRepository,
            ISystemControlRepository systemControlRepository,
            IStudentRegistrationNumberingRepository studentRegistrationNumberingRepository,
            ILedgerRepository ledgerRepository,
            ISecurityService securityService,
            IFiscalYearRepository fiscalYearRepository,
            IEntryControlPLRepository entryControlRepositoryPL,
            IEntryControlSalesRepository entryControlSalesRepository,
            IEntryControlPurchaseRepository entryControlPurchaseRepository,
            IEntryControlInventoryRepository entryControlInventoryRepository,
            IAccountGroupRepository accountGroupRepository,
             IBillingTermRepository billingTermRepository, IAcademicYearRepository academicYearRepository
            )
        {
            _studentRegistrationNumberingRepository = studentRegistrationNumberingRepository;
            _documentNumeringSchemeRepository = documentNumeringSchemeRepository;
            _systemControlRepository = systemControlRepository;
            _ledgerRepository = ledgerRepository;
            _securityService = securityService;
            _fiscalYear = fiscalYearRepository;
            _designRepository = designRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Setting);
            _entryControlPL = entryControlRepositoryPL;
            _entryControlInventory = entryControlInventoryRepository;
            _entryControlPurchase = entryControlPurchaseRepository;
            _entryControlSales = entryControlSalesRepository;
            _accountGroupRepository = accountGroupRepository;
            _billingTermRepository = billingTermRepository;
            _academicYearRepository = academicYearRepository;

        }

        public ActionResult GetLedger()
        {
            var ledgers = _ledgerRepository.GetAll().Select(x => new
            {
                Id = x.Id,
                Description = x.AccountName
            });
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DashBoard()
        {
            return View();
        }
        #region Document Numbering Scheme
         [CheckPermission(Permissions = "Navigate", Module = "DNS")]
        public ActionResult DocumentNumberingScheme()
         {
           ViewBag.UserRight = base.UserRight("DNS");
            return View();
        }
         [CheckPermission(Permissions = "Navigate", Module = "DNS")]
        public ActionResult DocumentNumberingSchemeList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("DNS");
           // var data = _documentNumeringSchemeRepository.GetAll().OrderByDescending(a => a.Id).ToList().AsQueryable().ToPagedList(pageNo, base.SystemControl.PageSize);

            var data =  _documentNumeringSchemeRepository.GetPagedList(x => x.Id, pageNo, base.SystemControl.PageSize);
             return PartialView(data);
        }
         [CheckPermission(Permissions = "Create", Module = "DNS")]
        public ActionResult DocumentNumberingSchemeAdd()
        {


            var viewModel = new DocumentNumberingSchemeViewModel()
                                {
                                    Module = new SelectList(Enum.GetValues(typeof(ModuleEnum)).Cast
                                              <ModuleEnum>().Select(
                                                  x =>
                                                  new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value", "Text"),
                                    Type = new SelectList(
                                    Enum.GetValues(typeof(DocumentCashBankBookTypeEnum)).Cast
                                        <DocumentCashBankBookTypeEnum>().Select(
                                            x =>
                                            new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                                    "Value", "Text"),
                                    Mode = new SelectList(
                                    Enum.GetValues(typeof(DocumentNumberStyleTypeEnum)).Cast
                                        <DocumentNumberStyleTypeEnum>().Select(
                                            x =>
                                            new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                                    "Value", "Text"),
                                    StartDate = DateTime.UtcNow,
                                    EndDate = DateTime.UtcNow.AddMonths(2)

                                };

            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult DocumentNumberingSchemeAdd(DocumentNumberingSchemeViewModel model)
        {
            var DocNum = new DocumentNumberingScheme
                             {
                                 DocBodyLen = model.DocumentNumberingScheme.DocBodyLen,
                                 DocCharFill = model.DocumentNumberingScheme.DocCharFill,
                                 DocCurrNo = model.DocumentNumberingScheme.DocCurrNo,
                                 DocDesc = model.DocumentNumberingScheme.DocDesc,
                                 DocEndDate = model.EndDate,
                                 DocEndNo = model.DocumentNumberingScheme.DocEndNo,
                                 DocMode = model.DocumentNumberingScheme.DocMode,
                                 DocNumFill = model.DocumentNumberingScheme.DocNumFill,
                                 DocPrefix = model.DocumentNumberingScheme.DocPrefix,
                                 DocStartDate = model.StartDate,
                                 DocStartNo = model.DocumentNumberingScheme.DocStartNo,
                                 DocSuffix = model.DocumentNumberingScheme.DocSuffix,
                                 DocTotalLen = model.DocumentNumberingScheme.DocTotalLen,
                                 DocType = model.DocumentNumberingScheme.DocType,
                                 ModuleName = model.DocumentNumberingScheme.ModuleName

                             };
            _documentNumeringSchemeRepository.Add(DocNum);
            return null;
        }
          [CheckPermission(Permissions = "Edit", Module = "DNS")]
        public ActionResult DocumentNumberingSchemeEdit(int id)
        {
            var data = _documentNumeringSchemeRepository.GetById(x => x.Id == id);
            var viewModel = new DocumentNumberingSchemeViewModel()
            {
                Module = new SelectList(
                    Enum.GetValues(typeof(ModuleEnum)).Cast
                        <ModuleEnum>().Select(
                            x =>
                            new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text", data.ModuleName),
                Type = new SelectList(
                Enum.GetValues(typeof(DocumentCashBankBookTypeEnum)).Cast
                    <DocumentCashBankBookTypeEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                "Value", "Text", data.DocType),
                Mode = new SelectList(
                Enum.GetValues(typeof(DocumentNumberStyleTypeEnum)).Cast
                    <DocumentNumberStyleTypeEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                "Value", "Text", data.DocMode),
                StartDate = data.DocStartDate,
                EndDate = data.DocEndDate,
                DocumentNumberingScheme = data

            };
            return PartialView(viewModel);
        }
        [HttpPost]
        public ActionResult DocumentNumberingSchemeEdit(DocumentNumberingSchemeViewModel model)
        {
            var DocNum = new DocumentNumberingScheme
            {
                Id = model.DocumentNumberingScheme.Id,

                DocBodyLen = model.DocumentNumberingScheme.DocBodyLen,
                DocCharFill = model.DocumentNumberingScheme.DocCharFill,
                DocCurrNo = model.DocumentNumberingScheme.DocCurrNo,
                DocDesc = model.DocumentNumberingScheme.DocDesc,
                DocEndDate = model.EndDate,
                DocEndNo = model.DocumentNumberingScheme.DocEndNo,
                DocMode = model.DocumentNumberingScheme.DocMode,
                DocNumFill = model.DocumentNumberingScheme.DocNumFill,
                DocPrefix = model.DocumentNumberingScheme.DocPrefix,
                DocStartDate = model.StartDate,
                DocStartNo = model.DocumentNumberingScheme.DocStartNo,
                DocSuffix = model.DocumentNumberingScheme.DocSuffix,
                DocTotalLen = model.DocumentNumberingScheme.DocTotalLen,
                DocType = model.DocumentNumberingScheme.DocType,
                ModuleName = model.DocumentNumberingScheme.ModuleName

            };
            _documentNumeringSchemeRepository.Update(DocNum);
            return null;
        }

        #endregion

        #region System Control
           [CheckPermission(Permissions = "Navigate", Module = "SysCon")]
        public ActionResult SystemControl()
        {
            var numList = new List<SelectListItem>
                              {
                                  new SelectListItem {Value = "1", Text = "1"},
                                  new SelectListItem {Value = "2", Text = "2"},
                                  new SelectListItem {Value = "3", Text = "3"},
                                  new SelectListItem {Value = "4", Text = "4"},
                                  new SelectListItem {Value = "5", Text = "5"},
                              };
            var data = _systemControlRepository.GetById(x => x.CompanyId == this.CompanyInfo.Id);
            var viewmodel = new SystemControlViewModel();
            //if (data == null)
            //{

            viewmodel.DateFormat = new SelectList(
                Enum.GetValues(typeof(DateFormatEnum)).Cast
                    <DateFormatEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewmodel.AuditTrial = new SelectList(
                Enum.GetValues(typeof(YesNoEnum)).Cast
                    <YesNoEnum>().Select(
                        x =>
                        new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewmodel.UDF = new SelectList(
                    Enum.GetValues(typeof(YesNoEnum)).Cast
                        <YesNoEnum>().Select(
                            x =>
                            new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }),
                    "Value", "Text");
            viewmodel.NoOfFeeReceiptPrintList = numList;
            viewmodel.NoOfBillPrintList = numList;
            if (data != null)
            {
                viewmodel.SystemControl = data;
            }
            else
            {
                viewmodel.SystemControl = new SystemControl();
            }
            viewmodel.ExpiredProductList = new SelectList(
Enum.GetValues(typeof(ExpiredProduct)).Cast
<ExpiredProduct>().Select(
x =>
new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }),
"Value", "Text");
            viewmodel.NegativeStockList = new SelectList(
            Enum.GetValues(typeof(ExpiredProduct)).Cast
            <ExpiredProduct>().Select(
            x =>
            new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }),
            "Value", "Text");



            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult SystemControl(SystemControlViewModel model)
        {
            var systemcontrol = new SystemControl()
                                    {
                                        AuditTrial = model.SystemControl.AuditTrial,
                                        CashBook = model.SystemControl.CashBook,
                                        IsAutoPopup = model.SystemControl.IsAutoPopup,
                                        IsConfirmCancel = model.SystemControl.IsConfirmCancel,
                                        IsConfirmSaving = model.SystemControl.IsConfirmSaving,
                                        IsCurrDate = model.SystemControl.IsCurrDate,
                                        OpeningStockPl = model.SystemControl.OpeningStockPl,
                                        ProfitLossAc = model.SystemControl.ProfitLossAc,
                                        PurchaseAc = model.SystemControl.PurchaseAc,
                                        PurchaseReturnAc = model.SystemControl.PurchaseReturnAc,
                                        SalesAc = model.SystemControl.SalesAc,
                                        SalesReturnAc = model.SystemControl.SalesReturnAc,
                                        DateType = model.SystemControl.DateType,
                                        Vat = model.SystemControl.Vat,
                                        CurrUnit = model.SystemControl.CurrUnit,
                                        CurrDesc = model.SystemControl.CurrDesc,
                                        ClosingingStock = model.SystemControl.ClosingingStock,
                                        ClosingStockPl = model.SystemControl.ClosingStockPl,
                                        CurrCode = model.SystemControl.CurrCode,
                                        UDF = model.SystemControl.UDF,
                                        CompanyId = this.CompanyInfo.Id,
                                        StudentFeeAc = model.SystemControl.StudentFeeAc,
                                        PageSize = model.SystemControl.PageSize,
                                        EnableBranch = model.SystemControl.EnableBranch,
                                        EducationTaxAc = model.SystemControl.EducationTaxAc,
                                        NoOfFeeReceiptPrint = model.SystemControl.NoOfFeeReceiptPrint,
                                        PrintDataOnly = model.SystemControl.PrintDataOnly,
                                        NoOfBillPrint = model.SystemControl.NoOfBillPrint,
                                        LibraryLateFine = model.SystemControl.LibraryLateFine,
                                        NegativeStock = model.SystemControl.NegativeStock



                                    };
            if (model.SystemControl.Id == 0)
            {
                _systemControlRepository.Add(systemcontrol);
            }
            else
            {
                systemcontrol.Id = model.SystemControl.Id;
                _systemControlRepository.Update(systemcontrol);
            }
            return RedirectToAction("SystemControl");
        }


        public ActionResult GetSalesBillingTerms()
        {
            var data = _billingTermRepository.GetMany(x => x.Type == "S").Select(x => new
                                                                                          {
                                                                                              Id = x.Id,
                                                                                              Description = x.Description
                                                                                          });
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSalesBillingTermById(int id)
        {
            var name = string.Empty;
            var data = _billingTermRepository.GetById(id);
            if (data != null)
            {
                name = data.Description;
            }
            return Json(name, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetLedgerById(string id)
        {
            var accountName = "";
            int convertId = int.Parse(id);
            var data = _ledgerRepository.GetById(x => x.Id == convertId);
            if (data != null)
            {
                accountName = data.AccountName;
            }

            return Json(accountName, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Fiscal Year
           [CheckPermission(Permissions = "Navigate", Module = "FiscalYear")]
        public ActionResult FiscalYear()
        {
            ViewBag.UserRight = base.UserRight("FiscalYear");
            return View();
        }
         [CheckPermission(Permissions = "Navigate", Module = "FiscalYear")]
        public ActionResult FiscalyearList()
           {
               ViewBag.UserRight = base.UserRight("FiscalYear");

            var data = _fiscalYear.GetMany(x => x.CompanyId == this.CompanyInfo.Id).ToList().OrderByDescending(a => a.IsDefalut);
            return PartialView(data);
        }
         [CheckPermission(Permissions = "Create", Module = "FiscalYear")]
        public ActionResult FiscalYearAdd()
        {
            var data = base.CreateViewModel<FiscalYearViewModel>();

            return PartialView(data);
        }
        [HttpPost]
        public ActionResult FiscalYearAdd(FiscalYearViewModel model)
        {
            var fiscalYear = new FiscalYear();
            fiscalYear.CompanyId = this.CompanyInfo.Id;
            fiscalYear.IsDefalut = false;
            fiscalYear.CreatedById = _authentication.GetAuthenticatedUser().Id;
            fiscalYear.UpdatedById = _authentication.GetAuthenticatedUser().Id;
            if (base.SystemControl.DateType == 1)
            {
                fiscalYear.StartDate = Convert.ToDateTime(model.DisplayStartDate);
                fiscalYear.StartDateNep = NepaliDateService.NepaliDate(fiscalYear.StartDate).Date;
            }
            else
            {
                fiscalYear.StartDateNep = model.DisplayStartDate;
                fiscalYear.StartDate = NepaliDateService.NepalitoEnglishDate(model.DisplayStartDate);
            }
            if (base.SystemControl.DateType == 1)
            {
                fiscalYear.EndDate = Convert.ToDateTime(model.DisplayEndDate);
                fiscalYear.EndDateNep = NepaliDateService.NepaliDate(fiscalYear.EndDate).Date;
            }
            else
            {
                fiscalYear.EndDateNep = model.DisplayEndDate;
                fiscalYear.EndDate = NepaliDateService.NepalitoEnglishDate(model.DisplayEndDate);
            }
            _fiscalYear.Add(fiscalYear);
            return null;
        }
          [CheckPermission(Permissions = "Edit", Module = "FiscalYear")]
        public ActionResult FiscalyearEdit(int id)
        {
            var data = _fiscalYear.GetById(x => x.Id == id);

            var viewmodel = base.CreateViewModel<FiscalYearViewModel>();
            if (base.SystemControl.DateType == 1)
            {
                viewmodel.DisplayStartDate = data.StartDate.ToString("MM/dd/yyyy");
                viewmodel.DisplayEndDate = data.EndDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewmodel.DisplayStartDate = data.StartDateNep;
                viewmodel.DisplayEndDate = data.EndDateNep;
            }
            viewmodel.FiscalYear = data;

            return PartialView(viewmodel);
        }
        [HttpPost]
        public ActionResult FiscalyearEdit(FiscalYearViewModel model)
        {
            if (base.SystemControl.DateType == 1)
            {
                model.FiscalYear.StartDate = Convert.ToDateTime(model.DisplayStartDate);
                model.FiscalYear.StartDateNep = NepaliDateService.NepaliDate(model.FiscalYear.StartDate).Date;
            }
            else
            {
                model.FiscalYear.StartDateNep = model.DisplayStartDate;
                model.FiscalYear.StartDate = NepaliDateService.NepalitoEnglishDate(model.DisplayStartDate);
            }
            if (base.SystemControl.DateType == 1)
            {
                model.FiscalYear.EndDate = Convert.ToDateTime(model.DisplayEndDate);
                model.FiscalYear.EndDateNep = NepaliDateService.NepaliDate(model.FiscalYear.EndDate).Date;
            }
            else
            {
                model.FiscalYear.EndDateNep = model.DisplayEndDate;
                model.FiscalYear.EndDate = NepaliDateService.NepalitoEnglishDate(model.DisplayEndDate);
            }
            model.FiscalYear.UpdatedById = _authentication.GetAuthenticatedUser().Id;
            model.FiscalYear.CompanyId = this.CompanyInfo.Id;
            _fiscalYear.Update(model.FiscalYear);
            return null;
        }
        public ActionResult SetDefautFiscalYear(int id)
        {
            var companyfiscalyear = _fiscalYear.GetMany(x => x.CompanyId == this.CompanyInfo.Id && x.IsDefalut);
            foreach (var fiscalYear in companyfiscalyear.ToList())
            {
                fiscalYear.IsDefalut = false;
                _fiscalYear.Update(fiscalYear);
            }

            FiscalYear data = _fiscalYear.GetById(x => x.Id == id);
            data.IsDefalut = true;
            data.UpdatedById = _authentication.GetAuthenticatedUser().Id;
            _fiscalYear.Update(data);
            //var context = new DataContext();
            //context.Entry(data).State = EntityState.Modified;
            //context.SaveChanges();
            return null;
        }
        #endregion


        //#region Fiscal Year

        //public ActionResult FiscalYear()
        //{
        //    return View();
        //}
        //public ActionResult FiscalyearList()
        //{

        //    var data = _fiscalYear.GetMany(x => x.CompanyId == this.CompanyInfo.Id).ToList().OrderByDescending(a => a.IsDefalut);
        //    return PartialView(data);
        //}
        //public ActionResult FiscalYearAdd()
        //{
        //    var data = base.CreateViewModel<FiscalYearViewModel>();
        //    return PartialView(data);
        //}
        //[HttpPost]
        //public ActionResult FiscalYearAdd(FiscalYearViewModel model)
        //{
        //    model.FiscalYear.CompanyId = this.CompanyInfo.Id;
        //    model.FiscalYear.IsDefalut = false;
        //    model.FiscalYear.CreatedById = _authentication.GetAuthenticatedUser().Id;
        //    model.FiscalYear.UpdatedById = _authentication.GetAuthenticatedUser().Id;
        //    _fiscalYear.Add(model.FiscalYear);
        //    return null;
        //}
        //public ActionResult FiscalyearEdit(int id)
        //{
        //    var data = _fiscalYear.GetById(x => x.Id == id);
        //    var viewmodel = base.CreateViewModel<FiscalYearViewModel>();
        //    viewmodel.FiscalYear = data;

        //    return PartialView(viewmodel);
        //}
        //[HttpPost]
        //public ActionResult FiscalyearEdit(FiscalYearViewModel model)
        //{
        //    model.FiscalYear.UpdatedById = _authentication.GetAuthenticatedUser().Id;
        //    model.FiscalYear.CompanyId = this.CompanyInfo.Id;//    _fiscalYear.Update(model.FiscalYear);
        //    return null;
        //}
        //public ActionResult SetDefautFiscalYear(int id)
        //{
        //    var companyfiscalyear = _fiscalYear.GetMany(x => x.CompanyId == this.CompanyInfo.Id && x.IsDefalut);
        //    foreach (var fiscalYear in companyfiscalyear)
        //    {
        //        fiscalYear.IsDefalut = false;
        //        _fiscalYear.Update(fiscalYear);
        //    }

        //    FiscalYear data = _fiscalYear.GetById(x => x.Id == id);
        //    data.IsDefalut = true;
        //    data.UpdatedById = _authentication.GetAuthenticatedUser().Id;
        //    _fiscalYear.Update(data);
        //    //var context = new DataContext();
        //    //context.Entry(data).State = EntityState.Modified;
        //    //context.SaveChanges();
        //    return null;
        //}
        //#endregion

        #region Academic Year
        //SCAy
        [CheckPermission(Permissions = "Navigate", Module = "SCAy")]
        public ActionResult AcademicYear()
        {
            ViewBag.UserRight = base.UserRight("SCAy");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "SCAy")]
        public ActionResult AcademicYearList()
        {
            ViewBag.UserRight = base.UserRight("SCAy");

            var data = _academicYearRepository.GetMany(x => x.CompanyId == this.CompanyInfo.Id).ToList().OrderByDescending(a => a.IsDefalut);
            return PartialView(data);
        }
        [CheckPermission(Permissions = "Create", Module = "SCAy")]
        public ActionResult AcademicYearAdd()
        {
            var data = base.CreateViewModel<AcademicYearViewModel>();

            return PartialView(data);
        }
        [HttpPost]
        public ActionResult AcademicYearAdd(AcademicYearViewModel model)
        {
            model.AcademicYear = new AcademicYear();
            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayStartDate) && !string.IsNullOrEmpty(model.DisplayEndDate))
                {
                    model.AcademyYear.StartDate = Convert.ToDateTime(model.DisplayStartDate);
                    model.AcademyYear.StartDateNep = NepaliDateService.NepaliDate(model.AcademyYear.StartDate).Date;
                    model.AcademyYear.EndDate = Convert.ToDateTime(model.DisplayEndDate);
                    model.AcademyYear.EndDateNep = NepaliDateService.NepaliDate(model.AcademyYear.EndDate).Date;

                }

            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayStartDate) && !string.IsNullOrEmpty(model.DisplayEndDate))
                {
                    model.AcademyYear.StartDateNep = model.DisplayStartDate;
                    model.AcademyYear.StartDate = NepaliDateService.NepalitoEnglishDate(model.AcademyYear.StartDateNep).Date;
                    model.AcademyYear.EndDateNep = model.DisplayEndDate;
                    model.AcademyYear.EndDate = NepaliDateService.NepalitoEnglishDate(model.AcademyYear.EndDateNep).Date;

                }
            }
            model.AcademyYear.CompanyId = this.CompanyInfo.Id;
            model.AcademyYear.IsDefalut = false;
            model.AcademyYear.CreatedById = _authentication.GetAuthenticatedUser().Id;
            model.AcademyYear.ModifiedById = _authentication.GetAuthenticatedUser().Id;
            _academicYearRepository.Add(model.AcademyYear);
            return Content("True");
        }
         [CheckPermission(Permissions = "Edit", Module = "SCAy")]
        public ActionResult AcademicYearEdit(int id)
        {
            var data = _academicYearRepository.GetById(x => x.Id == id);

            var viewmodel = base.CreateViewModel<AcademicYearViewModel>();
            if (base.SystemControl.DateType == 1)
            {
                viewmodel.DisplayStartDate = data.StartDate.ToString("MM/dd/yyy");
                viewmodel.DisplayEndDate = data.EndDate.ToString("MM/dd/yyy");
            }
            else
            {
                viewmodel.DisplayStartDate = data.StartDateNep;
                viewmodel.DisplayEndDate = data.EndDateNep;
            }
            viewmodel.AcademyYear = data;

            return PartialView(viewmodel);
        }
        [HttpPost]
        public ActionResult AcademicYearEdit(AcademicYearViewModel model)
        {

            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayStartDate) && !string.IsNullOrEmpty(model.DisplayEndDate))
                {
                    model.AcademyYear.StartDate = Convert.ToDateTime(model.DisplayStartDate);
                    model.AcademyYear.StartDateNep = NepaliDateService.NepaliDate(model.AcademyYear.StartDate).Date;
                    model.AcademyYear.EndDate = Convert.ToDateTime(model.DisplayEndDate);
                    model.AcademyYear.EndDateNep = NepaliDateService.NepaliDate(model.AcademyYear.EndDate).Date;

                }

            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayStartDate) && !string.IsNullOrEmpty(model.DisplayEndDate))
                {
                    model.AcademyYear.StartDateNep = model.DisplayStartDate;
                    model.AcademyYear.StartDate = NepaliDateService.NepalitoEnglishDate(model.AcademyYear.StartDateNep).Date;
                    model.AcademyYear.EndDateNep = model.DisplayEndDate;
                    model.AcademyYear.EndDate = NepaliDateService.NepalitoEnglishDate(model.AcademyYear.EndDateNep).Date;

                }
            }
            model.AcademyYear.ModifiedById = _authentication.GetAuthenticatedUser().Id;

            model.AcademyYear.CompanyId = this.CompanyInfo.Id;
            _academicYearRepository.Update(model.AcademyYear);

            return Content("True");
        }
        public ActionResult SetDefautAcademicYear(int id)
        {
            var companyfiscalyear = _academicYearRepository.GetMany(x => x.CompanyId == this.CompanyInfo.Id && x.IsDefalut);
            foreach (var fiscalYear in companyfiscalyear.ToList())
            {
                fiscalYear.IsDefalut = false;
                _academicYearRepository.Update(fiscalYear);
            }

            AcademicYear data = _academicYearRepository.GetById(x => x.Id == id);
            data.IsDefalut = true;
            data.ModifiedById = _authentication.GetAuthenticatedUser().Id;
            _academicYearRepository.Update(data);
            //var context = new DataContext();
            //context.Entry(data).State = EntityState.Modified;
            //context.SaveChanges();
            return null;
        }

        public JsonResult GetFiscalYear()
        {

            List<FiscalYear> lstClasses =
                _fiscalYear.GetAll().OrderBy(x => x.Id).ToList();

            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Description = x.StartDate.ToShortDateString() + " - " + x.EndDate.ToShortDateString()
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult EntryControlPL()
        {
            var entryControl = _entryControlPL.GetAll().FirstOrDefault();
            if (entryControl == null)
            {
                return PartialView("EntryControlPLCreate");
            }
            return PartialView("EntryControlPLEdit", entryControl);
        }

        [HttpPost]
        public ActionResult EntryControlPL(EntryControlPL model)
        {
            if (model.Id == 0)
            {
                _entryControlPL.Add(model);
            }
            else
            {
                _entryControlPL.Update(model);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EntryControlPurchase()
        {
            var entryControl = _entryControlPurchase.GetAll().FirstOrDefault();
            if (entryControl == null)
            {
                return PartialView("EntryControlPurchaseCreate");
            }
            return PartialView("EntryControlPurchaseEdit", entryControl);
        }

        [HttpPost]
        public ActionResult EntryControlPurchase(EntryControlPurchase model)
        {
            if (model.Id == 0)
            {
                _entryControlPurchase.Add(model);
            }
            else
            {
                _entryControlPurchase.Update(model);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EntryControlSales()
        {
            var entryControl = _entryControlSales.GetAll().FirstOrDefault();
            if (entryControl == null)
            {
                return PartialView("EntryControlSalesCreate");
            }
            return PartialView("EntryControlSalesEdit", entryControl);
        }

        [HttpPost]
        public ActionResult EntryControlSales(EntryControlSales model)
        {
            if (model.Id == 0)
            {
                _entryControlSales.Add(model);
            }
            else
            {
                _entryControlSales.Update(model);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EntryControlInventory()
        {
            var entryControl = _entryControlInventory.GetAll().FirstOrDefault();
            if (entryControl == null)
            {
                return PartialView("EntryControlInventoryCreate");
            }
            return PartialView("EntryControlInventoryEdit", entryControl);
        }

        [HttpPost]
        public ActionResult EntryControlInventory(EntryControlInventory model)
        {
            if (model.Id == 0)
            {
                _entryControlInventory.Add(model);
            }
            else
            {
                _entryControlInventory.Update(model);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #region StudentRegistrationNumbering
         [CheckPermission(Permissions = "Navigate", Module = "StdRN")]

        public ActionResult StudentRegistrationNumbering()
        {
           // ViewBag.UserRight = base.UserRight("StdRN");
            var AYID = base.AcademicYear.Id;
            var data = _studentRegistrationNumberingRepository.GetById(x => x.AcademyYearId == AYID && x.BranchId == CurrentBranch.Id);

            var viewModel = new StudentRegistrationNumberingViewModel();
            if (data == null)
            {


                //viewModel.Mode = new SelectList(
                //    Enum.GetValues(typeof(DocumentNumberStyleTypeEnum)).Cast
                //        <DocumentNumberStyleTypeEnum>().Select(
                //            x =>
                //            new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                //    "Value", "Text");
                viewModel.StudentRegistrationNumbering = new ScStudentRegistrationNumbering();
                viewModel.StudentRegistrationNumbering.DocSuffix = "-"+base.AcademicYear.StartDate.Year.ToString();

            }
            else
            {


                //viewModel.Mode = new SelectList(
                //    Enum.GetValues(typeof(DocumentNumberStyleTypeEnum)).Cast
                //        <DocumentNumberStyleTypeEnum>().Select(
                //            x =>
                //            new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                //    "Value", "Text", data.DocMode);

                viewModel.StudentRegistrationNumbering = data;


            }
            return View(viewModel);
        }
         [CheckPermission(Permissions = "Navigate", Module = "StdRN")]
        public ActionResult StudentRegistrationNumberingList(int pageNo = 1)
        {
            var data = _studentRegistrationNumberingRepository.GetAll().OrderByDescending(a => a.Id).AsQueryable().ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView(data);
        }


        [HttpPost]
        public ActionResult StudentRegistrationNumberingAdd(StudentRegistrationNumberingViewModel model)
        {
          
            if (model.StudentRegistrationNumbering.Id != 0)
            {
                var data=_studentRegistrationNumberingRepository.GetById(model.StudentRegistrationNumbering.Id);
                data.DocBodyLen = model.StudentRegistrationNumbering.DocBodyLen;
                data.DocCharFill = model.StudentRegistrationNumbering.DocCharFill;
                data.DocCurrNo = model.StudentRegistrationNumbering.DocCurrNo;

                data.DocEndNo = model.StudentRegistrationNumbering.DocEndNo;
                data.DocMode = model.StudentRegistrationNumbering.DocMode;
                data.DocNumFill = model.StudentRegistrationNumbering.DocNumFill;
                data.DocPrefix = model.StudentRegistrationNumbering.DocPrefix;

                data.DocStartNo = model.StudentRegistrationNumbering.DocStartNo;
                data.DocSuffix = model.StudentRegistrationNumbering.DocSuffix;
                data.DocBodyLen = model.StudentRegistrationNumbering.DocTotalLen;
                data.DocTotalLen = base.AcademicYear.Id;
                data.BranchId = CurrentBranch.Id;
                _studentRegistrationNumberingRepository.Update(data);

            }
            else
            {
                  var DocNum = new ScStudentRegistrationNumbering
            {
                DocBodyLen = model.StudentRegistrationNumbering.DocBodyLen,
                DocCharFill = model.StudentRegistrationNumbering.DocCharFill,
                DocCurrNo = model.StudentRegistrationNumbering.DocCurrNo,

                DocEndNo = model.StudentRegistrationNumbering.DocEndNo,
                DocMode = model.StudentRegistrationNumbering.DocMode,
                DocNumFill = model.StudentRegistrationNumbering.DocNumFill,
                DocPrefix = model.StudentRegistrationNumbering.DocPrefix,

                DocStartNo = model.StudentRegistrationNumbering.DocStartNo,
                DocSuffix = model.StudentRegistrationNumbering.DocSuffix,
                DocTotalLen = model.StudentRegistrationNumbering.DocTotalLen,
                AcademyYearId = base.AcademicYear.Id,
                BranchId = CurrentBranch.Id

            };

                _studentRegistrationNumberingRepository.Add(DocNum);
                
            }

           
            return RedirectToAction("StudentRegistrationNumbering");
        }

        //[HttpPost]
        //public ActionResult StudentRegistrationNumberingEdit(DocumentNumberingSchemeViewModel model)
        //{
        //    var DocNum = new DocumentNumberingScheme
        //    {
        //        Id = model.DocumentNumberingScheme.Id,

        //        DocBodyLen = model.DocumentNumberingScheme.DocBodyLen,
        //        DocCharFill = model.DocumentNumberingScheme.DocCharFill,
        //        DocCurrNo = model.DocumentNumberingScheme.DocCurrNo,
        //        DocDesc = model.DocumentNumberingScheme.DocDesc,
        //        DocEndDate = model.EndDate,
        //        DocEndNo = model.DocumentNumberingScheme.DocEndNo,
        //        DocMode = model.DocumentNumberingScheme.DocMode,
        //        DocNumFill = model.DocumentNumberingScheme.DocNumFill,
        //        DocPrefix = model.DocumentNumberingScheme.DocPrefix,
        //        DocStartDate = model.StartDate,
        //        DocStartNo = model.DocumentNumberingScheme.DocStartNo,
        //        DocSuffix = model.DocumentNumberingScheme.DocSuffix,
        //        DocTotalLen = model.DocumentNumberingScheme.DocTotalLen,
        //        DocType = model.DocumentNumberingScheme.DocType,
        //        ModuleName = model.DocumentNumberingScheme.ModuleName

        //    };
        //    _documentNumeringSchemeRepository.Update(DocNum);
        //    return null;
        //}

        #endregion

        public ActionResult Designs()
        {
            
            var data = _designRepository.GetAll();
            return View();
        }
        public ActionResult DesignList()
        {
            var data = _designRepository.GetAll();
            return PartialView(data);
        }
        public ActionResult DesignTemplate()
        {
            var design = new ReportHeader();
            var type = design.GetType();
           // var header = type.GetProperties().ToList().Where(x=>x.Name!="Id" || x.Name="").Select(item => item.Name).ToList();
            var data = new Design();
            return View(data);

        }
        

    }
}
