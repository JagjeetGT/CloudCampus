using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.Master;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using System.Drawing;
using System.Drawing.Imaging;

namespace KRBAccounting.Web.Controllers
{
    [CheckFirstInstallation]
    [Authorize]
    public class MasterController : BaseController
    {
        private readonly IAccountGroupRepository _accountGroup;
        private readonly IAccountSubGroupRepository _accountSubGroup;
        private readonly IAreaRepository _area;
        private readonly IAgentRepository _agent;
        private readonly ICurrencyRepository _currency;
        private readonly IFormsAuthenticationService _authentication;
        private readonly ILedgerRepository _ledger;
        private readonly ISubLedgerRepository _subLedger;
        private readonly IProductGroupRepository _productGroup;
        private readonly IProductSubGroupRepository _productSubGroup;
        private readonly IUnitRepository _unit;
        private readonly IProductRepository _product;
        private readonly IGodownRepository _godown;
        private readonly ICostCenterRepository _costCenter;
        private readonly INarrationRepository _narration;
        private readonly IBillingTermRepository _billingTerm;
        private readonly IUdfEntryRepository _udfEntry;
        private readonly IUdfEntryDetailRepository _udfEntryDetail;
        private readonly IUDFDataRepository _udfData;
        private readonly IOpeningLedgerRepository _openingLedger;
        public DataContext _dataContext = new DataContext();
        private readonly IAccountTransactionRepository _accountTransaction;
        private readonly IModuleListRepository _moduleList;
        private readonly IProductUnitConversionRepository _productUnitConversionRepository;
        private readonly IProductOpeningRepository _productopeningRepository;
        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IProductImagesRepository _productimagesRepository;
        private readonly IShortNameDetailRepository _shortName;
        private readonly ISchemeRepository _schemeRepository;
        private readonly ISchemeFreeProductRepository _schemefreeproductRepository;
        private readonly ISchemeProductRepository _schemeproductRepository;
        private readonly IBillingTermDetailRepository _billingTermDetail;
        private readonly IScBillTransactionRepository _billTransactionRepository;
            

        public readonly ISystemControlRepository _SystemControl;
        public readonly IOpeningStudentRepository _openingstudentRepository;
        public int CurrentBranchId;
        public int CurrentCompanyId;
        public bool EnableBranch;

        public MasterController(IAccountGroupRepository accountGroupRepository,
                                IProductImagesRepository productimagesRepository,
            IOpeningStudentRepository openingStudentRepository,
            IScBillTransactionRepository billTransactionRepository,
                                IUdfEntryDetailRepository udfEntryDetailRepository,
                                IUdfEntryRepository udfEntryRepository,
                                IAccountSubGroupRepository accountSubGroupRepository,
                                IAreaRepository areaRepository,
                                IAgentRepository agentRepository,
                                ICurrencyRepository currencyRepository,
                                ILedgerRepository ledgerRepository,
                                ISubLedgerRepository subLedgerRepository,
                                IProductGroupRepository productGroupRepository,
                                IProductSubGroupRepository productSubGroupRepository,
                                IUnitRepository unitRepository,
                                IProductRepository productRepository,
                                IGodownRepository godownRepository,
                                ICostCenterRepository costCenterRepository,
                                INarrationRepository narrationRepository,
                                IBillingTermRepository billingTermRepository,
                                IUDFDataRepository udfDataRepository,
                                IOpeningLedgerRepository openingLedgerRepository,
                                IAccountTransactionRepository accountTransactionRepository,
                                IModuleListRepository moduleListRepository,
                                IProductUnitConversionRepository productUnitConversionRepository,
                                IProductOpeningRepository productopeningRepository,
                                IStockTransactionRepository stockTransactionRepository,
                                IShortNameDetailRepository shortNameDetailRepository,
                                ISchemeRepository schemeRepository,
                                ISchemeFreeProductRepository schemefreeproductRepository,
                                ISchemeProductRepository schemeproductRepository,
                                ISystemControlRepository systemControlRepository,
                                IBillingTermDetailRepository billingTermDetailRepository
            )
        {
            _accountGroup = accountGroupRepository;
            _accountSubGroup = accountSubGroupRepository;
            _area = areaRepository;
            _agent = agentRepository;
            _currency = currencyRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Accounting);
            _ledger = ledgerRepository;
            _subLedger = subLedgerRepository;
            _productGroup = productGroupRepository;
            _productSubGroup = productSubGroupRepository;
            _unit = unitRepository;
            _product = productRepository;
            _godown = godownRepository;
            _costCenter = costCenterRepository;
            _narration = narrationRepository;
            _billingTerm = billingTermRepository;
            _udfEntry = udfEntryRepository;
            _udfEntryDetail = udfEntryDetailRepository;
            _udfData = udfDataRepository;
            _openingLedger = openingLedgerRepository;
            _accountTransaction = accountTransactionRepository;
            _moduleList = moduleListRepository;
            CurrentBranchId = CurrentBranch.Id;
            EnableBranch = SystemControl.EnableBranch;
            CurrentCompanyId = CompanyInfo.Id;
            _productUnitConversionRepository = productUnitConversionRepository;
            _productopeningRepository = productopeningRepository;
            _stockTransactionRepository = stockTransactionRepository;
            _productimagesRepository = productimagesRepository;
            _shortName = shortNameDetailRepository;
            _schemeRepository = schemeRepository;
            _schemefreeproductRepository = schemefreeproductRepository;
            _schemeproductRepository = schemeproductRepository;
            _SystemControl = systemControlRepository;
            _billingTermDetail = billingTermDetailRepository;
            _openingstudentRepository = openingStudentRepository;
            _billTransactionRepository = billTransactionRepository;
        }

        //
        // GET: /Master/

        public ActionResult Index()
        {
            return View();
        }

        #region Ledger

        public JsonResult CheckAccountNameInLedger(string AccountName, int? Id)
        {

            var feeterm = new Ledger();
            if (Id.HasValue && Id != 0)
            {
                var data = _ledger.GetById(x => x.Id == Id && !x.IsDeleted);
                if (data.AccountName.ToLower().Trim() != AccountName.ToLower().Trim())
                {
                    feeterm =
                        _ledger.GetById(
                            x => x.AccountName.ToLower().Trim() == AccountName.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm =
                    _ledger.GetById(x => x.AccountName.ToLower().Trim() == AccountName.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Ledger();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public JsonResult CheckShortNameInLedger(string ShortName, int? Id)
        {

            var feeterm = new Ledger();
            if (Id.HasValue && Id != 0)
            {
                var data = _ledger.GetById(x => x.Id == Id);
                if (data.ShortName.ToLower().Trim() != ShortName.ToLower().Trim())
                {
                    feeterm =
                        _ledger.GetById(x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);

                }
            }
            else
            {
                feeterm =
                    _ledger.GetById(x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Ledger();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult Ledger()
        {
            ViewBag.UserRight = base.UserRight("GL");
            return View();
        }

        public ActionResult LedgerList(int pageNo = 1)
        {

            ViewBag.UserRight = base.UserRight("GL");
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
          
            if (EnableBranch)
            {
            var    list =
                    _ledger.GetPagedList(
                        x =>
                            x.BranchId == CurrentBranchId && !x.IsDeleted ||
                            x.BranchId == CurrentCompanyId && !x.IsDeleted,
                        x => x.AccountName, pageNo, SystemControl.PageSize); return PartialView("LedgerList",
                               list);
            }
            else
            {
                var list = _ledger.GetPagedList(a => !a.IsDeleted, x => x.AccountName, pageNo, SystemControl.PageSize); return PartialView("LedgerList",
                                 list);
            }
            
            //return PartialView("LedgerList", _ledger.GetPagedList(x=>!x.IsDeleted,x=>x.Id,1,10));
            //return PartialView("LedgerList",
            //                   list);
        }

        [CheckPermission(Permissions = "Create", Module = "GL")]
        public ActionResult LedgerAdd()
        {
            var viewModel = base.CreateViewModel<LedgerAddViewModel>();
            viewModel.Category =
                new SelectList(
                    Enum.GetValues(typeof(LedgerCategoryEnum)).Cast
                        <LedgerCategoryEnum>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text");
            var group = _accountGroup.GetMany(x => !x.IsDeleted).ToList();
            viewModel.Group = new SelectList(group, "Id", "Description");
            var subGroupList = new List<AccountSubGroup>();
            if (viewModel.Group.Count() != 0)
            {
                var groupId = group.FirstOrDefault().Id;
                subGroupList = _accountSubGroup.Filter(x => x.AccountGroupId == groupId && !x.IsDeleted).ToList();
                viewModel.SubGroup = new SelectList(subGroupList, "Id", "Description");
            }
            else
            {
                viewModel.SubGroup = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            viewModel.Area = new SelectList(_area.GetAll(), "Id", "AreaName");
            viewModel.Agent = new SelectList(_agent.GetAll(), "Id", "Name");
            viewModel.Currency = new SelectList(_currency.GetAll(), "Id", "Code");
            viewModel.UdfEntries =
                _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.LedgerMaster).ToList();
            viewModel.Ledger = new Ledger();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult LedgerAdd(LedgerAddViewModel model, FormCollection formCollection)
        {
            if (_ledger.IsAccountNameAvailable(model.Ledger.AccountName))
            {
                if (_ledger.IsShortNameAvailable(model.Ledger.ShortName))
                {
                    var userId = _authentication.GetAuthenticatedUser().Id;
                    model.Ledger.CreatedDate = DateTime.UtcNow;
                    model.Ledger.UpdatedDate = DateTime.UtcNow;
                    model.Ledger.CreatedById = userId;
                    model.Ledger.UpdatedById = userId;
                    model.Ledger.BranchId = CurrentBranchId;
                    _ledger.Add(model.Ledger);

                    if (TempData["ShortNameGL"] != null)
                    {
                        ShortNameDetail shortNameDetail = (ShortNameDetail)TempData["ShortNameGL"];
                        _shortName.Add(shortNameDetail);
                    }

                    var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.LedgerMaster);
                    if (data != null)
                    {
                        foreach (var udfEntry in data)
                        {
                            var value = formCollection[udfEntry.FieldName];
                            var i = new UDFData()
                                        {
                                            ReferenceId = model.Ledger.Id,
                                            UdfId = udfEntry.Id,
                                            Value = value,
                                            SourceId = (int)EntryModuleEnum.LedgerMaster
                                        };

                            _udfData.Add(i);
                        }
                    }
                }
                else
                {
                    return Content("Short Name already exists. Please enter a different Short Name.");
                }
            }
            else
            {

                return Content("Account Name already exists. Please enter a different Account Name.");
            }
            return Content("True");
        }

        public ActionResult LedgerEdit(int id)
        {
            var data = _ledger.GetById(id);
            var viewModel = base.CreateViewModel<LedgerAddViewModel>();
            viewModel.Category =
                new SelectList(
                    Enum.GetValues(typeof(LedgerCategoryEnum)).Cast
                        <LedgerCategoryEnum>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text", data.Category);
            var group = _accountGroup.GetMany(x => !x.IsDeleted).ToList();
            viewModel.Group = new SelectList(group, "Id", "Description", data.AccountGroupId);
            var subGroupList = new List<AccountSubGroup>();
            if (viewModel.Group.Count() != 0)
            {
                var groupId = group.FirstOrDefault().Id;
                subGroupList = _accountSubGroup.Filter(x => x.AccountGroupId == groupId && !x.IsDeleted).ToList();
                viewModel.SubGroup = new SelectList(subGroupList, "Id", "Description", data.AccountSubGroupId);
            }
            else
            {
                viewModel.SubGroup = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            viewModel.Area = new SelectList(_area.GetMany(x => !x.IsDeleted), "Id", "AreaName", data.AreaId);
            viewModel.Agent = new SelectList(_agent.GetMany(x => !x.IsDeleted), "Id", "Name", data.AgentId);
            viewModel.Currency = new SelectList(_currency.GetMany(x => !x.IsDeleted), "Id", "Code", data.CurrencyId);
            viewModel.Ledger = data;
            viewModel.UdfEntries =
                _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.LedgerMaster).ToList();
            ViewBag.Id = data.Id;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult LedgerEdit(LedgerAddViewModel model, FormCollection formCollection)
        {
            var Accountname = IsAccountNameAvailable(model.Ledger.Id, model.Ledger.AccountName);
            var shortname = IsShortNameAvailable(model.Ledger.Id, model.Ledger.ShortName);
            if (Accountname.Data.ToString() == "")
            {
                if (shortname.Data.ToString() == "")
                {
                    var userId = _authentication.GetAuthenticatedUser().Id;
                    model.Ledger.UpdatedDate = DateTime.UtcNow;
                    model.Ledger.UpdatedById = userId;

                    _dataContext.Entry(model.Ledger).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                    _udfData.Delete(
                        x => x.ReferenceId == model.Ledger.Id && x.SourceId == (int)EntryModuleEnum.LedgerMaster);
                    var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.LedgerMaster);
                    if (data != null)
                    {
                        foreach (var udfEntry in data)
                        {
                            var value = formCollection[udfEntry.FieldName];
                            var i = new UDFData()
                                        {
                                            ReferenceId = model.Ledger.Id,
                                            UdfId = udfEntry.Id,
                                            Value = value,
                                            SourceId = (int)EntryModuleEnum.LedgerMaster
                                        };

                            _udfData.Add(i);
                        }
                    }
                }
                else
                {
                    return Content("Short Name already exists. Please enter a different Short Name.");
                }
            }
            else
            {

                return Content("Account Name already exists. Please enter a different Account Name.");
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult LedgerDelete(int LedgerId)
        {
            if (!CheckLedgerInSystemControl(LedgerId))
            {
                return Json(false);
            }
            var transaction = _accountTransaction.GetMany(x => x.GlCode == LedgerId).Count();
            if (transaction > 0)
            {
                return Json(false);
            }
            var model = _ledger.GetById(x => x.Id == LedgerId);
            model.IsDeleted = true;
            _ledger.Update(model);
            return Json(true);
            // return RedirectToAction("Ledger");
        }

        public bool CheckLedgerInSystemControl(int ledgerId)
        {
            var systemControl = _SystemControl.GetAll().FirstOrDefault();
            if (systemControl.CashBook == ledgerId)
            {
                return false;
            }
            if (systemControl.ProfitLossAc == ledgerId)
            {
                return false;
            }
            if (systemControl.Vat == ledgerId)
            {
                return false;
            }
            if (systemControl.SalesReturnAc == ledgerId)
            {
                return false;
            }
            if (systemControl.SalesAc == ledgerId)
            {
                return false;
            }
            if (systemControl.PurchaseReturnAc == ledgerId)
            {
                return false;
            }
            if (systemControl.PurchaseAc == ledgerId)
            {
                return false;
            }
            if (systemControl.ClosingStockPl == ledgerId)
            {
                return false;
            }
            if (systemControl.ClosingingStock == ledgerId)
            {
                return false;
            }
            return true;
        }

        public JsonResult IsAccountNameAvailable(int? id, string name)
        {
            var ledgers = string.Empty;
            if (id == null)
            {
                if (!_ledger.IsAccountNameAvailable(name))
                {
                    ledgers = "Account Name already exists. Please enter a different Account Name.";
                }
            }
            else
            {
                var data = _ledger.GetById(x => x.Id == id);
                if (data.AccountName.ToLower() != name.ToLower())
                {
                    if (!_ledger.IsAccountNameAvailable(name))
                    {
                        ledgers = "Account Name already exists. Please enter a different Account Name.";
                    }
                }
            }
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsShortNameAvailable(int? id, string name)
        {
            var ledgers = string.Empty;
            if (id == null)
            {

                if (!_ledger.IsShortNameAvailable(name))
                {
                    ledgers = "Short Name already exists. Please enter a different Short Name.";
                }
            }
            else
            {
                var data = _ledger.GetById(x => x.Id == id);
                if (data.ShortName.ToLower() != name.ToLower())
                {
                    if (!_ledger.IsShortNameAvailable(name))
                    {
                        ledgers = "Short Name already exists. Please enter a different Short Name.";
                    }
                }

            }
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLedger()
        {
            var ledgers = _ledger.GetMany(x => !x.IsDeleted).Select(x => new
                                                                             {
                                                                                 Id = x.Id,
                                                                                 Code = x.ShortName,
                                                                                 Description = x.AccountName
                                                                             }).OrderBy(x => x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCostCenterList()
        {
            var data = _costCenter.GetMany(x => !x.IsDeleted).Select(x => new
                                                                              {
                                                                                  Id = x.Id,
                                                                                  Description = x.Name,
                                                                                  ShortName = x.ShortName
                                                                              }).OrderBy(x => x.Description);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGodownList()
        {
            var data = _godown.GetMany(x => !x.IsDeleted).Select(x => new
                                                                          {
                                                                              Id = x.Id,
                                                                              Description = x.Name
                                                                          }).OrderBy(x => x.Description);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCashBankLedger()
        {
            var ledgers = _ledger.Filter(
                x => x.IsCashBank && (x.BranchId == CurrentBranchId || x.BranchId == CurrentCompanyId) && !x.IsDeleted).
                Select(x => new
                                {
                                    Reference = x.IsCashBook ? "C" : "B",
                                    Id = x.Id,
                                    Code = x.ShortName,
                                    Description = x.AccountName

                                }).OrderBy(x => x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNoCashBankLedger()
        {
            var ledgers = _ledger.Filter(x => !x.IsCashBank && !x.IsDeleted).Select(x => new
                                                                                             {
                                                                                                 Id = x.Id,
                                                                                                 Code = x.ShortName,
                                                                                                 Description =
                                                                                             x.AccountName
                                                                                             }).OrderBy(
                                                                                                 x => x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLedgerExceptCategoryOther()
        {
            string other = StringEnum.Parse(typeof(LedgerCategoryEnum), "Other").ToString();
            var ledgers = _ledger.Filter(x => x.Category != other && !x.IsDeleted).Select(x => new
                                                                                                   {
                                                                                                       Id = x.Id,
                                                                                                       Code =
                                                                                                   x.ShortName,
                                                                                                       Description =
                                                                                                   x.AccountName
                                                                                                   }).OrderBy(
                                                                                                       x =>
                                                                                                       x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLedgerCategoryOther()
        {
            string other = StringEnum.Parse(typeof(LedgerCategoryEnum), "Other").ToString();
            var ledgers = _ledger.Filter(x => x.Category == other && !x.IsDeleted).Select(x => new
                                                                                                   {
                                                                                                       Id = x.Id,
                                                                                                       Code =
                                                                                                   x.ShortName,
                                                                                                       Description =
                                                                                                   x.AccountName
                                                                                                   }).OrderBy(
                                                                                                       x =>
                                                                                                       x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SubLedger

        public JsonResult CheckDescription(string Description, int? Id)
        {

            var feeterm = new SubLedger();
            if (Id.HasValue && Id != 0)
            {
                var data = _subLedger.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm =
                        _subLedger.GetById(
                            x => x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm =
                    _subLedger.GetById(
                        x => x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new SubLedger();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public JsonResult CheckShortName(string ShortName, int? Id)
        {
            var feeterm = new SubLedger();
            if (Id.HasValue && Id != 0)
            {
                var data = _subLedger.GetById(x => x.Id == Id);
                if (data.ShortName.ToLower().Trim() != ShortName.ToLower().Trim())
                {
                    feeterm =
                        _subLedger.GetById(
                            x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);

                }
            }
            else
            {
                feeterm =
                    _subLedger.GetById(x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new SubLedger();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult SubLedger()
        {
            ViewBag.UserRight = base.UserRight("SL");
            return View();
        }

        public ActionResult SubLedgerList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("SL");
            var list = _subLedger.GetMany(x => !x.IsDeleted).OrderBy(a => a.Description);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("SubLedgerList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult SubLedgerAdd()
        {
            var model = new SubLedger();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SubLedgerAdd(SubLedger model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_subLedger.IsSubAccountNameAvailable(model.Description))
            {
                if (_subLedger.IsSubShortNameAvailable(model.ShortName))
                {
                    model.CreatedDate = DateTime.UtcNow;
                    model.UpdatedDate = DateTime.UtcNow;
                    model.CreatedById = userId;
                    model.UpdatedById = userId;
                    _subLedger.Add(model);

                    if (TempData["ShortNameSL"] != null)
                    {
                        ShortNameDetail shortNameDetail = (ShortNameDetail)TempData["ShortNameSL"];
                        _shortName.Add(shortNameDetail);
                    }
                }
                else
                {
                    return Content("Short Name already exists. Please enter a different Short Name.");
                }
            }
            else
            {

                return Content("Account Name already exists. Please enter a different Account Name.");
            }
            return Content("True");
        }

        public ActionResult SubLedgerEdit(int id)
        {
            var data = _subLedger.GetById(id);
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult SubLedgerEdit(SubLedger model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var accountName = IsSubAccountNameAvailable(model.Id, model.Description);
            var shortName = IsSubShortNameAvailable(model.Id, model.ShortName);
            if (accountName.Data.ToString() == "")
            {
                if (shortName.Data.ToString() == "")
                {
                    model.UpdatedDate = DateTime.UtcNow;
                    model.UpdatedById = userId;
                    _dataContext.Entry(model).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                }
                else
                {
                    return Content(shortName.Data.ToString());
                }
            }
            else
            {
                return Content(accountName.Data.ToString());
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult SubLedgerDelete(int id)
        {
            var transaction = _accountTransaction.GetMany(x => x.SlCode == id).Count();
            if (transaction > 0)
            {
                return Json(false);
            }
            var model = _subLedger.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _subLedger.Update(model);
            return Json(true);
        }

        public JsonResult IsSubAccountNameAvailable(int? id, string name)
        {
            var ledgers = string.Empty;
            if (id == null)
            {
                if (!_subLedger.IsSubAccountNameAvailable(name))
                {

                    ledgers = "Account Name already exists. Please enter a different Account Name.";
                }
            }
            else
            {
                var data = _subLedger.GetById(x => x.Id == id);
                if (data.Description.ToLower() != name.ToLower())
                {
                    if (!_subLedger.IsSubAccountNameAvailable(name))
                    {
                        ledgers = "Account Name already exists. Please enter a different Account Name.";
                    }
                }
            }
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsSubShortNameAvailable(int? id, string name)
        {
            var ledgers = string.Empty;
            if (id == null)
            {

                if (!_subLedger.IsSubShortNameAvailable(name))
                {
                    ledgers = "Short Name already exists. Please enter a different Short Name.";
                }
            }
            else
            {
                var data = _subLedger.GetById(x => x.Id == id);
                if (data.ShortName.ToLower() != name.ToLower())
                {
                    if (!_subLedger.IsSubShortNameAvailable(name))
                    {
                        ledgers = "Short Name already exists. Please enter a different Short Name.";
                    }
                }

            }
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubLedger()
        {
            var subLedgers = _subLedger.GetMany(x => !x.IsDeleted).Select(x => new
                                                                                   {
                                                                                       Id = x.Id,
                                                                                       Description = x.Description
                                                                                   }).OrderBy(x => x.Description);
            return Json(subLedgers, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Account Group

        public JsonResult CheckInDescription(string Description, int? Id)
        {

            var feeterm = new AccountGroup();
            if (Id.HasValue && Id != 0)
            {
                var data = _accountGroup.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm =
                        _accountGroup.GetById(
                            x => x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted);

                }
            }
            else
            {
                feeterm =
                    _accountGroup.GetById(
                        x => x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new AccountGroup();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult AccountGroup()
        {
            ViewBag.UserRight = base.UserRight("AG");
            return View();
        }

        public ActionResult AccountGroupList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("AG");
            var list = _accountGroup.GetMany(x => !x.IsDeleted).OrderBy(x => x.DisplayOrder);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("AccountGroupList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult AccountGroupAdd()
        {
            var viewModel = base.CreateViewModel<AccountGroupViewModel>();
            viewModel.Type = new SelectList(
                Enum.GetValues(typeof(AccountGroupTypeEnum)).Cast
                    <AccountGroupTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");

            viewModel.SubType = new SelectList(
                Enum.GetValues(typeof(BalanceSheetTypeEnum)).Cast
                    <BalanceSheetTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");
            viewModel.AccountGroup = new AccountGroup();
            return PartialView(viewModel);
        }

        public ActionResult AccountGroupAddModal()
        {
            var viewModel = new AccountGroupViewModel();
            viewModel.Type = new SelectList(
                Enum.GetValues(typeof(AccountGroupTypeEnum)).Cast
                    <AccountGroupTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");

            viewModel.SubType = new SelectList(
                Enum.GetValues(typeof(BalanceSheetTypeEnum)).Cast
                    <BalanceSheetTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult AccountGroupAdd(AccountGroupViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_accountGroup.IsAccountGroupAvailable(model.AccountGroup.Description))
            {
                model.AccountGroup.UpdatedById = userId;
                model.AccountGroup.CreatedById = userId;
                model.AccountGroup.CreatedDate = DateTime.UtcNow;
                model.AccountGroup.UpdatedDate = DateTime.UtcNow;
                model.AccountGroup.Code = model.AccountGroup.Description;
                _accountGroup.Add(model.AccountGroup);
            }
            else
            {
                return Content("Account Name already exists. Please enter a different Account Name.");
            }
            return Content("True");
        }

        public ActionResult AccountGroupEdit(int id)
        {
            var data = _accountGroup.GetById(id);
            var viewModel = new AccountGroupViewModel();
            viewModel.Type = new SelectList(
                Enum.GetValues(typeof(AccountGroupTypeEnum)).Cast
                    <AccountGroupTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text", data.GroupType);
            if (data.GroupType == "B")
            {
                viewModel.SubType = new SelectList(
                    Enum.GetValues(typeof(BalanceSheetTypeEnum)).Cast
                        <BalanceSheetTypeEnum>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text", data.GroupAccountType);
            }
            else
            {
                viewModel.SubType = new SelectList(
                    Enum.GetValues(typeof(PLTypeEnum)).Cast
                        <PLTypeEnum>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text", data.GroupAccountType);
            }

            viewModel.AccountGroup = data;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult AccountGroupEdit(AccountGroupViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var groupName = IsAccountGroupAvailable(model.AccountGroup.Id, model.AccountGroup.Description);
            if (groupName.Data.ToString() == "")
            {
                model.AccountGroup.UpdatedById = userId;
                model.AccountGroup.UpdatedDate = DateTime.UtcNow;
                model.AccountGroup.Code = model.AccountGroup.Description;
                _dataContext.Entry(model.AccountGroup).State = EntityState.Modified;
                _dataContext.SaveChanges();
            }
            else
            {
                return Content(groupName.Data.ToString());
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult AccountGroupDelete(int id)
        {
            var ledger = _ledger.GetMany(x => x.AccountGroupId == id && !x.IsDeleted).Count();
            if (ledger > 0)
            {
                return Json(false);
            }

            var model = _accountGroup.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _accountGroup.Update(model);
            return Json(true);
        }

        public JsonResult IsAccountGroupAvailable(int? id, string name)
        {
            var ledgers = string.Empty;
            if (id == null)
            {
                if (!_accountGroup.IsAccountGroupAvailable(name))
                {

                    ledgers = "Account Name already exists. Please enter a different Account Name.";
                }
            }
            else
            {
                var data = _accountGroup.GetById(x => x.Id == id);
                if (data.Description.ToLower() != name.ToLower())
                {
                    if (!_accountGroup.IsAccountGroupAvailable(name))
                    {
                        ledgers = "Account Name already exists. Please enter a different Account Name.";
                    }
                }
            }
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AccountGroupUpdateSortOrder(string id)
        {
            string[] separator = new string[2] { "id[]=", "&" };
            string[] itemIdArray = id.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            _accountGroup.UpdateSortOrder(itemIdArray);
            return Content("Success");
        }


        [HttpPost]
        public ActionResult GetAccountGroupType(string id)
        {
            var SubType = new SelectList("", "Value", "Text");
            if (id == "B")
            {
                SubType = new SelectList(
                    Enum.GetValues(typeof(BalanceSheetTypeEnum)).Cast
                        <BalanceSheetTypeEnum>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text");
            }
            else
            {
                SubType = new SelectList(
                    Enum.GetValues(typeof(PLTypeEnum)).Cast
                        <PLTypeEnum>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text");
            }
            return Json(SubType, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Duration = 240, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult GetAccountGroupList()
        {
            var acccountGroups = _accountGroup.GetMany(x => !x.IsDeleted).Select(x => new
                                                                                          {
                                                                                              Id = x.Id,
                                                                                              Description =
                                                                                          x.Description
                                                                                          }).OrderBy(x => x.Description);
            return Json(acccountGroups, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountSubGroupByGroupId(int id)
        {
            var subGroup = _accountSubGroup.Filter(a => a.AccountGroupId == id && !a.IsDeleted).Select(x => new
                                                                                                                {
                                                                                                                    Id =
                                                                                                                x.Id,
                                                                                                                    Description
                                                                                                                =
                                                                                                                x.
                                                                                                                Description
                                                                                                                }).
                OrderBy(x => x.Description);

            return Json(subGroup, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Account Sub Group

        public JsonResult CheckInDescriptionMaster(string Description, int? Id)
        {
            var feeterm = new AccountSubGroup();
            if (Id.HasValue && Id != 0)
            {
                var data = _accountSubGroup.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm =
                        _accountSubGroup.GetById(
                            x => x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted);

                }
            }
            else
            {
                feeterm =
                    _accountSubGroup.GetById(
                        x => x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new AccountSubGroup();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult AccountSubGroup()
        {
            return View();
        }

        public ActionResult AccountSubGroupList(int pageNo = 1)
        {
            var list = _accountSubGroup.GetMany(x => !x.IsDeleted).OrderByDescending(a => a.Description);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("AccountSubGroupList",
                               list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult AccountSubGroupAdd()
        {
            var viewModel = new AccountSubGroupViewModel();
            viewModel.GroupList = new SelectList(_accountGroup.GetMany(x => !x.IsDeleted), "Id", "Description");
            viewModel.AccountSubGroup = new AccountSubGroup();
            return PartialView(viewModel);
        }

        public ActionResult AccountSubGroupAddModal()
        {
            var viewModel = new AccountSubGroupViewModel();
            viewModel.GroupList = new SelectList(_accountGroup.GetMany(x => !x.IsDeleted), "Id", "Description");
            viewModel.AccountSubGroup = new AccountSubGroup();

            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult AccountSubGroupAdd(AccountSubGroupViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_accountSubGroup.IsAccountSubGroupAvailable(model.AccountSubGroup.Description))
            {
                model.AccountSubGroup.CreatedDate = DateTime.UtcNow;
                model.AccountSubGroup.UpdatedDate = DateTime.UtcNow;
                model.AccountSubGroup.CreatedById = userId;
                model.AccountSubGroup.UpdatedById = userId;
                _accountSubGroup.Add(model.AccountSubGroup);
            }
            else
            {
                return Content("Account Name already exists. Please enter a different Account Name.");
            }
            return Content("True");
        }

        public ActionResult AccountSubGroupEdit(int id)
        {
            var data = _accountSubGroup.GetById(id);
            var viewModel = new AccountSubGroupViewModel();
            viewModel.GroupList = new SelectList(_accountGroup.GetMany(x => !x.IsDeleted), "Id", "Description",
                                                 data.AccountGroupId);
            viewModel.AccountSubGroup = data;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult AccountSubGroupEdit(AccountSubGroupViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var groupname = IsAccountSubGroupAvailable(model.AccountSubGroup.Id, model.AccountSubGroup.Description);
            if (groupname.Data.ToString() == "")
            {
                model.AccountSubGroup.UpdatedDate = DateTime.UtcNow;
                model.AccountSubGroup.UpdatedById = userId;
                _dataContext.Entry(model.AccountSubGroup).State = EntityState.Modified;
                _dataContext.SaveChanges();
            }
            else
            {
                return Content(groupname.Data.ToString());
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult AccountSubGroupDelete(int id)
        {
            var ledger = _ledger.GetMany(x => x.AccountSubGroupId == id && !x.IsDeleted).Count();
            if (ledger > 0)
            {
                return Json(false);
            }

            var model = _accountSubGroup.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _accountSubGroup.Update(model);
            return Json(true);
        }

        public JsonResult IsAccountSubGroupAvailable(int? id, string name)
        {
            var ledgers = string.Empty;
            if (id == null)
            {
                if (!_accountSubGroup.IsAccountSubGroupAvailable(name))
                {

                    ledgers = "Account Name already exists. Please enter a different Account Name.";
                }
            }
            else
            {
                var data = _accountGroup.GetById(x => x.Id == id);
                if (data != null)
                {
                    if (data.Description.ToLower() != name.ToLower())
                    {
                        if (!_accountSubGroup.IsAccountSubGroupAvailable(name))
                        {
                            ledgers = "Account Name already exists. Please enter a different Account Name.";
                        }
                    }
                }

            }
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Product

        #region Product/Item
        
        public JsonResult CheckNameInProduct(string Name, int? Id)
        {
          
            var feeterm = new Product();
            if (Id.HasValue && Id != 0)
            {
                var data = _product.GetById(x => x.Id == Id && !x.IsDeleted);
                if (data.Name.ToLower().Trim() != Name.ToLower().Trim())
                {
                    feeterm =
                        _product.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);

                }
            }
            else
            {
                feeterm = _product.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Product();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public JsonResult CheckShortNameInProduct(string ShortName, int? Id)
        {
            var feeterm = new Product();
            if (Id.HasValue && Id != 0)
            {
                var data = _product.GetById(x => x.Id == Id && !x.IsDeleted);
                if (data.ShortName.ToLower().Trim() != ShortName.ToLower().Trim())
                {
                    feeterm =
                        _product.GetById(x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm =
                    _product.GetById(x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Product();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult UploadProductImages(IEnumerable<HttpPostedFileBase> attachmentsroom)
        {
            FileHelper _fileHelper = new FileHelper();
            var imgGuid = string.Empty;
            var fileName = string.Empty;
            var fileext = string.Empty;
            var filenamewithoutext = string.Empty;
            foreach (var file in attachmentsroom)
            {
                fileext = Path.GetExtension(file.FileName);
                filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                if (fileext != ".jpg" && fileext != ".png" && fileext != ".jpeg") continue;
                imgGuid = Guid.NewGuid().ToString();
                var coverfilename = imgGuid + fileext;
                var path = AppDomain.CurrentDomain.BaseDirectory + "Content\\ProductImage\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, coverfilename);
                file.SaveAs(path);
                var thumbpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\ProductImage\\";
                _fileHelper.ConvertToThumbnail(file, imgGuid + "_th", fileext, thumbpath);
                fileName = file.FileName;
            }
            var imagepath = "/Content/ProductImage/" + imgGuid + "_th" + fileext;
            var imagepathbig = "/Content/ProductImage/" + imgGuid + fileext;
            var res =
                Json(
                    new
                        {
                            guid = imgGuid,
                            filename = fileName,
                            ext = fileext,
                            path = imagepath,
                            bigimage = imagepathbig
                        });
            return res;
        }

        public ActionResult Product()
        {
            ViewBag.UserRight = base.UserRight("IP");
            return View();
        }

        public ActionResult ProductList( int pageNo = 1 )
        {
            ViewBag.UserRight = base.UserRight("IP");
            var list = _product.GetMany(x => !x.IsDeleted).OrderByDescending(a => a.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("ProductList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult ProductAdd()
        {
            var viewModel = new ProductAddViewModel();
            viewModel.Product = new Product();
            viewModel.Group = new SelectList(_productGroup.GetMany(x => !x.IsDeleted), "Id", "Description");
            viewModel.SubGroup = new SelectList(_productSubGroup.GetMany(x => !x.IsDeleted), "Id", "Description");
            var test = StringEnum.GetStringValue(ProductTypeEnum.Inventory);
            viewModel.Type = new SelectList(
                Enum.GetValues(typeof(ProductTypeEnum)).Cast
                    <ProductTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.ValTechList = new SelectList(
                Enum.GetValues(typeof(ValTechEnum)).Cast
                    <ValTechEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.UnitList = new SelectList(_unit.GetAll(), "Id", "Code");
            viewModel.BonusType = new SelectList(
                Enum.GetValues(typeof(BonusTypeEnum)).Cast
                    <BonusTypeEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.Vat = new SelectList(
                Enum.GetValues(typeof(YesNoEnum)).Cast
                    <YesNoEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.MediumList = new SelectList(
                Enum.GetValues(typeof(Medium)).Cast
                    <Medium>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.ExpiredProductList =
                new SelectList(Enum.GetValues(typeof(ExpiredProduct)).Cast<ExpiredProduct>().Select
                                   (x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }),
                               "Value", "Text").ToList();
            viewModel.ExpiredProductList.Insert(0, new SelectListItem()
                                                       {
                                                           Text = "None",
                                                           Value = "0"
                                                       });
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult ProductAdd(ProductAddViewModel model,
                                       IEnumerable<ProductUnitConversion> productUnitConversion,
                                       IEnumerable<string> image)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_product.IsProductItemNameAvailable(model.Product.Name))
            {
                if (_product.IsProductShortNameAvailable(model.Product.ShortName))
                {
                    model.Product.CreatedDate = DateTime.UtcNow;
                    model.Product.UpdatedDate = DateTime.UtcNow;
                    model.Product.CreatedById = userId;
                    model.Product.UpdatedById = userId;
                    _product.Add(model.Product);

                    if (TempData["ShortNameIP"] != null)
                    {
                        ShortNameDetail shortNameDetail = (ShortNameDetail)TempData["ShortNameIP"];
                        _shortName.Add(shortNameDetail);
                    }

                    if (productUnitConversion != null)
                    {
                        foreach (
                            var unitConversion in
                                productUnitConversion.Where(
                                    x => x.Quantity != 0 && x.LowestQuantity != 0 && x.SalePrice != 0))
                        {
                            var unitCode = _unit.GetById(unitConversion.UnitId).Code;
                            var unitLowestUnitCode = _unit.GetById(unitConversion.LowestUnitId).Code;
                            var unitconversion = new ProductUnitConversion()
                                                     {
                                                         ProductId = model.Product.Id,
                                                         Quantity = unitConversion.Quantity,
                                                         Unit = unitCode,
                                                         BuyPrice = unitConversion.BuyPrice,
                                                         SalePrice = unitConversion.SalePrice,
                                                         LowestQuantity = unitConversion.LowestQuantity,
                                                         LowestUnit = unitLowestUnitCode,
                                                         CreatedBy = _authentication.GetAuthenticatedUser().Id,
                                                         CreatedOn = DateTime.UtcNow
                                                     };
                            _productUnitConversionRepository.Add(unitconversion);
                        }
                    }

                    if (image != null)
                    {
                        foreach (var item in image)
                        {
                            var picModel = new ProductImages()
                                               {
                                                   ProductId = model.Product.Id,
                                                   Path = item,
                                               };
                            _productimagesRepository.Add(picModel);
                        }
                    }

                }
                else
                {
                    return Content("Product Short Name already exists. Please enter a different Short Name.");
                }
            }
            else
            {
                return Content("Produt Item Name already exists. Please enter a different Item Name.");
            }
            return Content("True");
        }

        public ActionResult ProductEdit(int id)
        {
            var data = _product.GetById(id);
            var viewModel = new ProductAddViewModel();
            viewModel.Product = data;
            viewModel.Group = new SelectList(_productGroup.GetAll(), "Id", "Description", data.ProductGroupId);
            viewModel.SubGroup = new SelectList(_productSubGroup.GetAll(), "Id", "Description", data.ProductSubGroupId);

            viewModel.Type = new SelectList(
                Enum.GetValues(typeof(ProductTypeEnum)).Cast
                    <ProductTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text", data.ProductType);
            viewModel.ValTechList = new SelectList(
                Enum.GetValues(typeof(ValTechEnum)).Cast
                    <ValTechEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text", data.ValTech);
            viewModel.UnitList = new SelectList(_unit.GetAll(), "Id", "Code", data.Unit);
            viewModel.UnitConversionList = new SelectList(_unit.GetAll(), "Id", "Code");
            viewModel.LowestUnitConversionList = new SelectList(_unit.GetAll(), "Id", "Code");

            viewModel.BonusType = new SelectList(
                Enum.GetValues(typeof(BonusTypeEnum)).Cast
                    <BonusTypeEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.Vat = new SelectList(
                Enum.GetValues(typeof(YesNoEnum)).Cast
                    <YesNoEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text",
                data.VatRate);
            viewModel.ProductUnitConversions = _productUnitConversionRepository.GetMany(x => x.ProductId == id);
            viewModel.ProductImages = _productimagesRepository.GetMany(x => x.ProductId == id);
            viewModel.MediumList = new SelectList(
                Enum.GetValues(typeof(Medium)).Cast
                    <Medium>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.ExpiredProductList =
                new SelectList(Enum.GetValues(typeof(ExpiredProduct)).Cast<ExpiredProduct>().Select
                                   (x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }),
                               "Value", "Text").ToList();
            viewModel.ExpiredProductList.Insert(0, new SelectListItem()
                                                       {
                                                           Text = "None",
                                                           Value = "0"
                                                       });
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult ProductEdit(ProductAddViewModel model,
                                        IEnumerable<ProductUnitConversion> productUnitConversion,
                                        IEnumerable<string> image)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var itemname = IsProductItemNameAvailable(model.Product.Id, model.Product.Name);
            var shortname = IsProductShortNameAvailable(model.Product.Id, model.Product.ShortName);
            if (itemname.Data.ToString() == "")
            {
                if (shortname.Data.ToString() == "")
                {
                    model.Product.UpdatedDate = DateTime.UtcNow;
                    model.Product.UpdatedById = userId;
                    _dataContext.Entry(model.Product).State = EntityState.Modified;
                    _dataContext.SaveChanges();

                    _productUnitConversionRepository.Delete(x => x.ProductId == model.Product.Id);
                    if (productUnitConversion != null)
                    {
                        foreach (
                            var unitConversion in
                                productUnitConversion.Where(
                                    x => x.Quantity != 0 && x.LowestQuantity != 0 && x.SalePrice != 0))
                        {
                            var unitCode = _unit.GetById(unitConversion.UnitId).Code;
                            var unitLowestUnitCode = _unit.GetById(unitConversion.LowestUnitId).Code;
                            var unitconversion = new ProductUnitConversion()
                                                     {
                                                         ProductId = model.Product.Id,
                                                         Quantity = unitConversion.Quantity,
                                                         Unit = unitCode,
                                                         BuyPrice = unitConversion.BuyPrice,
                                                         SalePrice = unitConversion.SalePrice,
                                                         LowestQuantity = unitConversion.LowestQuantity,
                                                         LowestUnit = unitLowestUnitCode,
                                                         CreatedBy = _authentication.GetAuthenticatedUser().Id,
                                                         CreatedOn = DateTime.UtcNow
                                                     };
                            _productUnitConversionRepository.Add(unitconversion);
                        }
                    }


                    if (image != null)
                    {
                        _productimagesRepository.Delete(x => x.ProductId == model.Product.Id);

                        foreach (var item in image)
                        {
                            var picroom = new ProductImages()
                                              {
                                                  ProductId = model.Product.Id,
                                                  Path = item,
                                              };
                            _productimagesRepository.Add(picroom);
                        }
                    }
                }
                else
                {
                    return Content(shortname.Data.ToString());
                }
            }
            else
            {
                return Content(itemname.Data.ToString());
            }
            return Content("True");
        }

        public ActionResult AddProductUnitConversion(int Id)
        {
            var model = new ProductUnitConversion();
            model.UnitList = new SelectList(_unit.GetMany(x => !x.IsDeleted), "Id", "Code");
            model.LowestUnitList = new SelectList(_unit.GetMany(x => !x.IsDeleted), "Id", "Code", Id);
            return PartialView("_PartialProductUnitConversion", model);
        }

        [HttpPost]
        public ActionResult ProductDelete(int id)
        {
            var model = _product.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _product.Update(model);
            return Json(true);
        }

        public JsonResult IsProductItemNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_product.IsProductItemNameAvailable(name))
                {
                    itemName = "Account Name already exists. Please enter a different Account Name.";
                }
            }
            else
            {
                var data = _product.GetById(x => x.Id == id);
                if (data.Name.ToLower() != name.ToLower())
                {
                    if (!_product.IsProductItemNameAvailable(name))
                    {
                        itemName = "Account Name already exists. Please enter a different Account Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsProductShortNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_product.IsProductShortNameAvailable(name))
                {
                    itemName = "Account Name already exists. Please enter a different Account Name.";
                }
            }
            else
            {
                var data = _product.GetById(x => x.Id == id);
                if (data.ShortName.ToLower() != name.ToLower())
                {
                    if (!_product.IsProductShortNameAvailable(name))
                    {
                        itemName = "Account Name already exists. Please enter a different Account Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Unit

        public JsonResult CheckCodeInUnit(string Code, int? Id)
        {
           
            var feeterm = new Unit();
            if (Id.HasValue && Id != 0)
            {
                var data = _unit.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm = _unit.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm = _unit.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Unit();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckDescriptionInUnit(string Description, int? Id)
        {
            var feeterm = new Unit();
            if (Id.HasValue && Id != 0)
            {
                var data = _unit.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm =
                        _unit.GetById(
                            x => x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm =
                    _unit.GetById(x => x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Unit();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult Unit()
        {
            ViewBag.UserRight = base.UserRight("UT");
            return View();
        }

        public ActionResult UnitList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("UT");
            var list = _unit.GetMany(x => !x.IsDeleted).OrderByDescending(a => a.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("UnitList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult UnitAdd()
        {
            var model = new Unit();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult UnitAdd(Unit model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            //if (_unit.IsProductUnitNameAvailable(model.Description))
            //{
            //    if (_unit.IsProductUnitCodeAvailable(model.Code))
            //    {
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.UpdatedById = userId;
            _unit.Add(model);
            //    }
            //    else
            //    {
            //        return Content("Produt Unit Code already exists. Please enter a different Unit Code.");
            //    }
            //}
            //else
            //{
            //    return Content("Produt Unit Name already exists. Please enter a different Unit Name.");
            //}
            return Content("True");

        }

        public ActionResult UnitEdit(int id)
        {
            var data = _unit.GetById(id);
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult UnitEdit(Unit model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            //var unitname = IsProductUnitNameAvailable(model.Id, model.Description,model.IsDeleted);
            //var unitcode = IsProductUnitCodeAvailable(model.Id, model.Code,model.IsDeleted);
            //if (unitcode.Data.ToString() == "")
            //{
            //    if (unitname.Data.ToString() == "")
            //    {
            model.UpdatedDate = DateTime.UtcNow;
            model.UpdatedById = userId;
            _dataContext.Entry(model).State = EntityState.Modified;
            _dataContext.SaveChanges();
            //    }
            //    else
            //    {
            //        return Content(unitname.Data.ToString());
            //    }
            //}
            //else
            //{
            //    return Content(unitcode.Data.ToString());
            //}
            return Content("True");
        }

        [HttpPost]
        public ActionResult UnitDelete(int id)
        {
            var product = _product.GetMany(x => x.Unit == id && !x.IsDeleted).Count();
            if (product == 0)
            {
                var model = _unit.GetById(x => x.Id == id);
                model.IsDeleted = true;
                _unit.Update(model);
                return Json(true);
            }

            return Json(false);
        }

        public JsonResult IsProductUnitNameAvailable(int? id, string name, bool isDeleted)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_unit.IsProductUnitNameAvailable(name) && isDeleted == false)
                {

                    itemName = "Produt Unit Name already exists. Please enter a different Unit Name.";
                }
            }
            else
            {
                var data = _unit.GetById(x => x.Id == id);
                if (data.Description.ToLower() != name.ToLower())
                {
                    if (!_unit.IsProductUnitNameAvailable(name) && isDeleted == false)
                    {
                        itemName = "Produt Unit Name already exists. Please enter a different Unit Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsProductUnitCodeAvailable(int? id, string name, bool isDeleted)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_unit.IsProductUnitCodeAvailable(name) && isDeleted == false)
                {

                    itemName = "Produt Unit Code already exists. Please enter a different Unit Code.";
                }
            }
            else
            {
                var data = _unit.GetById(x => x.Id == id);
                if (data.Code.ToLower() != name.ToLower())
                {
                    if (!_unit.IsProductUnitNameAvailable(name) && isDeleted == false)
                    {
                        itemName = "Produt Unit Code already exists. Please enter a different Unit Code.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Product Group

        public JsonResult CheckDescriptionInMaster(string Description, int? Id)
        {
            var feeterm = new ProductGroup();
            if (Id.HasValue && Id != 0)
            {
                var data = _productGroup.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm =
                        _productGroup.GetById(
                            x => x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm =
                    _productGroup.GetById(
                        x => x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new ProductGroup();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public JsonResult CheckShortNameInMaster(string ShortName, int? Id)
        {
            var feeterm = new ProductGroup();
            if (Id.HasValue && Id != 0)
            {
                var data = _productGroup.GetById(x => x.Id == Id);
                if (data.ShortName.ToLower().Trim() != ShortName.ToLower().Trim())
                {
                    feeterm =
                        _productGroup.GetById(
                            x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm =
                    _productGroup.GetById(
                        x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new ProductGroup();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult ProductGroup()
        {
            ViewBag.UserRight = base.UserRight("PG");
            return View();
        }

        public ActionResult ProductGroupList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("PG");

            var list = _productGroup.GetMany(x => !x.IsDeleted).OrderByDescending(a => a.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("ProductGroupList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult ProductGroupAdd()
        {
            var model = new ProductGroup();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ProductGroupAdd(ProductGroup model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;

            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.UpdatedById = userId;
            _productGroup.Add(model);
            return Content("True");
        }

        public ActionResult ProductGroupEdit(int id)
        {
            var data = _productGroup.GetById(id);
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult ProductGroupEdit(ProductGroup model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            // var groupname = IsProductGroupNameAvailable(model.Id, model.Description);
            //var groupshortname = IsProductGroupShortNameAvailable(model.Id, model.ShortName);
            //if (groupname.Data.ToString() == "")
            //{
            //    if (groupshortname.Data.ToString() == "")
            //    {
            model.UpdatedDate = DateTime.UtcNow;
            model.UpdatedById = userId;
            _dataContext.Entry(model).State = EntityState.Modified;
            _dataContext.SaveChanges();
            //    }
            //    else
            //    {
            //        return Content(groupshortname.Data.ToString());
            //    }
            //}
            //else
            //{
            //    return Content(groupname.Data.ToString());
            //}
            return Content("True");
        }

        [HttpPost]
        public ActionResult ProductGroupDelete(int id)
        {
            var product = _product.GetMany(x => x.Unit == id && !x.IsDeleted).Count();
            if (product == 0)
            {
                var model = _productGroup.GetById(x => x.Id == id);
                model.IsDeleted = true;
                _productGroup.Update(model);
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult IsProductGroupNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_productGroup.IsProductGroupNameAvailable(name))
                {

                    itemName = "Produt Group Name already exists. Please enter a different Produt Group Name.";
                }
            }
            else
            {
                var data = _productGroup.GetById(x => x.Id == id);
                if (data.Description.ToLower() != name.ToLower())
                {
                    if (!_productGroup.IsProductGroupNameAvailable(name))
                    {
                        itemName = "Produt Group Name already exists. Please enter a different Produt Group Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsProductGroupShortNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_productGroup.IsProductGroupShortNameAvailable(name))
                {

                    itemName = "Produt Group Short Name exists. Please enter a different Produt Group Short Name.";
                }
            }
            else
            {
                var data = _productGroup.GetById(x => x.Id == id);
                if (data.ShortName.ToLower() != name.ToLower())
                {
                    if (!_productGroup.IsProductGroupShortNameAvailable(name))
                    {
                        itemName =
                            "Produt Group Short Name already exists. Please enter a different Produt Group Short Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductGroup()
        {
            var productGroups = _productGroup.GetMany(x => !x.IsDeleted).Select(x => new
                                                                                         {
                                                                                             Id = x.Id,
                                                                                             Description = x.Description
                                                                                         }).OrderBy(x => x.Description);
            return Json(productGroups, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProducts()
        {
            var productGroups = _product.GetMany(x => !x.IsDeleted && x.ProductType != 2).Select(x => new
                                                                                                          {
                                                                                                              Id = x.Id,
                                                                                                              ShortName
                                                                                                          = x.ShortName,
                                                                                                              Name =
                                                                                                          x.Name,
                                                                                                              SalesPrice
                                                                                                          = x.SalesPrice,
                                                                                                          }).OrderBy(
                                                                                                              x =>
                                                                                                              x.Name);
            return Json(productGroups, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Product Sub Group

        public JsonResult CheckDescriptionInProductSubGroup(string Description, int? Id, int? ProductGroupId)
        {

            var feeterm = new ProductSubGroup();
            if (Id.HasValue && Id != 0)
            {
                var data = _productSubGroup.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm =
                        _productSubGroup.GetById(
                            x =>
                            x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted &&
                            x.ProductGroupId == ProductGroupId);

                }
            }
            else
            {
                feeterm =
                    _productSubGroup.GetById(
                        x =>
                        x.Description.ToLower().Trim() == Description.Trim().ToLower() && !x.IsDeleted &&
                        x.ProductGroupId == ProductGroupId);
            }
            if (feeterm == null)
            {
                feeterm = new ProductSubGroup();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult ProductSubGroup()
        {
            ViewBag.UserRight = base.UserRight("PSG");
            return View();
        }

        public ActionResult ProductSubGroupList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("PSG");
            var list = _productSubGroup.GetMany(x => !x.IsDeleted).OrderByDescending(a => a.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("ProductSubGroupList",
                               list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult ProductSubGroupAdd()
        {
            var viewModel = new ProductSubGroupAddViewModel();
            viewModel.ProductGroupList = new SelectList(_productGroup.GetMany(x => !x.IsDeleted), "Id", "Description");
            viewModel.ProductSubGroup = new ProductSubGroup();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult ProductSubGroupAdd(ProductSubGroupAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            //if (_productSubGroup.IsProductSubGroupNameAvailable(model.ProductSubGroup.Description))
            //{
            model.ProductSubGroup.CreatedDate = DateTime.UtcNow;
            model.ProductSubGroup.UpdatedDate = DateTime.UtcNow;
            model.ProductSubGroup.CreatedById = userId;
            model.ProductSubGroup.UpdatedById = userId;
            _productSubGroup.Add(model.ProductSubGroup);
            //}
            //else
            //{
            //    return Content("Produt SubGroup Name already exists. Please enter a different Produt SubGroup Name.");
            //}
            return Content("True");
        }

        public ActionResult ProductSubGroupEdit(int id)
        {
            var viewModel = new ProductSubGroupAddViewModel();
            var data = _productSubGroup.GetById(id);
            viewModel.ProductGroupList = new SelectList(_productGroup.GetMany(x => !x.IsDeleted), "Id", "Description",
                                                        data.ProductGroupId);
            viewModel.ProductSubGroup = data;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult ProductSubGroupEdit(ProductSubGroupAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            //var groupName = IsProductSubGroupNameAvailable(model.ProductSubGroup.Id, model.ProductSubGroup.Description);
            //if (groupName.Data.ToString() == "")
            //{
            model.ProductSubGroup.UpdatedDate = DateTime.UtcNow;
            model.ProductSubGroup.UpdatedById = userId;
            _dataContext.Entry(model.ProductSubGroup).State = EntityState.Modified;
            _dataContext.SaveChanges();
            //}
            //else
            //{
            //    return Content(groupName.Data.ToString());
            //}
            return Content("True");
        }

        [HttpPost]
        public ActionResult ProductSubGroupDelete(int id)
        {
            var product = _product.GetMany(x => x.Unit == id && !x.IsDeleted).Count();
            if (product == 0)
            {
                var model = _productSubGroup.GetById(x => x.Id == id);
                model.IsDeleted = true;
                _productSubGroup.Update(model);
                return Json(true);
            }
            return Json(false);
        }

        public ActionResult GetProductSubGroup(int id)
        {
            var productSubGroups = _productSubGroup.Filter(x => x.ProductGroupId == id && !x.IsDeleted).Select(x => new
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
                                                                                                                        Description
                                                                                                                        })
                .OrderBy(x => x.Description);
            return Json(productSubGroups, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsProductSubGroupNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_productSubGroup.IsProductSubGroupNameAvailable(name))
                {

                    itemName = "Produt SubGroup Name already exists. Please enter a different Produt SubGroup Name.";
                }
            }
            else
            {
                var data = _productSubGroup.GetById(x => x.Id == id);
                if (data.Description.ToLower() != name.ToLower())
                {
                    if (!_productSubGroup.IsProductSubGroupNameAvailable(name))
                    {
                        itemName = "Produt SubGroup Name already exists. Please enter a different Produt SubGroup Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Billing Terms

        public ActionResult BillingTerm()
        {
           
            return View();
        }

        #region Sales Billing Term

        public JsonResult CheckCodeInBillingTerm(int Code, int? Id, string Type)
        {
            var feeterm = new BillingTerm();
            if (Id != 0 && Id.HasValue)
            {
                var data = _billingTerm.GetById(x => x.Id == Id);
                if (data.Code != Code)
                {
                    feeterm =
                        _billingTerm.GetById(x => x.Code == Code && x.Type.ToLower() == Type.Trim() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm = _billingTerm.GetById(x => x.Code == Code && x.Type.ToLower() == Type.Trim() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new BillingTerm();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckDescriptionInBillingTerm(string Description, int? Id, string Type)
        {
            var feeterm = new BillingTerm();
            if (Id != 0 && Id.HasValue)
            {
                var data = _billingTerm.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm =
                        _billingTerm.GetById(
                            x =>
                            x.Description.ToLower().Trim() == Description.Trim().ToLower() &&
                            x.Type.ToLower() == Type.Trim() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm =
                    _billingTerm.GetById(
                        x =>
                        x.Description.ToLower().Trim() == Description.Trim().ToLower() &&
                        x.Type.ToLower() == Type.Trim() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new BillingTerm();
            }
            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult SalesBillingTerm()
        {
            ViewBag.UserRight = base.UserRight("SBT");
            return View();
        }

        public ActionResult SalesBillingTermList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("SBT");
            var list = _billingTerm.Filter(x => x.Type == "S" && !x.IsDeleted);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("SalesBillingTermList",
                               list.AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult SalesBillingTermAdd()
        {
            var viewModel = base.CreateViewModel<BillingTermAddViewModel>();
            viewModel.GeneralLedgerList = new SelectList(_ledger.GetMany(x => !x.IsDeleted), "Id", "AccountName");
            viewModel.BasisList = new SelectList(
                Enum.GetValues(typeof(BillingTermBasisEnum)).Cast
                    <BillingTermBasisEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.CategoryList = new SelectList(
                Enum.GetValues(typeof(BillingTermCategoryEnum)).Cast
                    <BillingTermCategoryEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.SignList = new SelectList(
                Enum.GetValues(typeof(SignEnum)).Cast
                    <SignEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.BillingTerm = new BillingTerm();
            viewModel.BillingTerm.IsActive = true;
            viewModel.BillingTerm.TermBasis = 2;
            var billTerms = _billingTerm.GetMany(x => !x.IsDeleted);
            var billTerm = 0;
            if (billTerms.Count() > 0)
            {
                billTerm = billTerms.Max(x => x.DispalyOrder);
            }
            viewModel.BillingTerm.DispalyOrder = billTerm + 1;
            viewModel.BillingTerm.Type = "S";
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SalesBillingTermAdd(BillingTermAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.BillingTerm.CreatedDate = DateTime.UtcNow;
            model.BillingTerm.UpdatedDate = DateTime.UtcNow;
            model.BillingTerm.IsActive = true;
            model.BillingTerm.TermBasis = 2;
            model.BillingTerm.CreatedById = userId;
            model.BillingTerm.UpdatedById = userId;
            model.BillingTerm.Type = "S";
            model.BillingTerm.BranchId = CurrentBranch.Id;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            model.BillingTerm.Category = cat;
            model.BillingTerm.Basis = 2;
            _billingTerm.Add(model.BillingTerm);
            return Content("True");
        }

        public ActionResult SalesBillingTermEdit(int id)
        {
            var data = _billingTerm.GetById(id);
            var viewModel = new BillingTermAddViewModel();
            viewModel.GeneralLedgerList = new SelectList(_ledger.GetMany(x => !x.IsDeleted), "Id", "AccountName",
                                                         data.GeneralLedgerId);
            viewModel.BasisList = new SelectList(
                Enum.GetValues(typeof(BillingTermBasisEnum)).Cast
                    <BillingTermBasisEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text",
                data.Basis);
            viewModel.CategoryList = new SelectList(
                Enum.GetValues(typeof(BillingTermCategoryEnum)).Cast
                    <BillingTermCategoryEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text", data.Category);
            viewModel.SignList = new SelectList(
                Enum.GetValues(typeof(SignEnum)).Cast
                    <SignEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text", data.Sign);
            viewModel.BillingTerm = data;
            data.Type = "S";
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SalesBillingTermEdit(BillingTermAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var SalesName = IsSalesBillingNameAvailable(model.BillingTerm.Id, model.BillingTerm.Description);
            var SalesCode = IsSalesBillingCodeAvailable(model.BillingTerm.Id, model.BillingTerm.Code);
            if (SalesCode.Data.ToString() == "")
            {
                if (SalesName.Data.ToString() == "")
                {
                    model.BillingTerm.UpdatedDate = DateTime.UtcNow;
                    model.BillingTerm.UpdatedById = userId;
                    model.BillingTerm.Type = "S";
                    model.BillingTerm.BranchId = CurrentBranch.Id;
                    var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
                    model.BillingTerm.Category = cat;
                    model.BillingTerm.Basis = 2;
                    _dataContext.Entry(model.BillingTerm).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                }
                else
                {
                    return Content(SalesName.Data.ToString());
                }
            }
            else
            {
                return Content(SalesCode.Data.ToString());
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult SalesBillingTermDelete(int id)
        {
            var billingTerm = _billingTermDetail.GetMany(x => x.BillingTermId == id).Count();
            if (billingTerm == 0)
            {


                var model = _billingTerm.GetById(x => x.Id == id);
                model.IsDeleted = true;
                _billingTerm.Update(model);
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult IsSalesBillingNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_billingTerm.IsSalesBillingNameAvailable(name))
                {

                    itemName = "Sales Billing Name already exists. Please enter a different Sales Billing Name.";
                }
            }
            else
            {
                var data = _billingTerm.GetById(x => x.Id == id);
                if (data.Description.ToLower() != name.ToLower())
                {
                    if (!_billingTerm.IsSalesBillingNameAvailable(name))
                    {
                        itemName = "Sales Billing Name already exists. Please enter a different Sales Billing Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsSalesBillingCodeAvailable(int? id, int code)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_billingTerm.IsSalesBillingCodeAvailable(code))
                {

                    itemName = "Sales Billing Code already exists. Please enter a different Sales Billing Code.";
                }
            }
            else
            {
                var data = _billingTerm.GetById(x => x.Id == id);
                if (data.Code != code)
                {
                    if (!_billingTerm.IsSalesBillingCodeAvailable(code))
                    {
                        itemName = "Sales Billing Code already exists. Please enter a different Sales Billing Code.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSalesTermList()
        {
            var terms = _billingTerm.GetMany(x => !x.IsDeleted && x.Type == "S").Select(x => new
                                                                                                 {
                                                                                                     Id = x.Id,
                                                                                                     Description =
                                                                                                 x.Description
                                                                                                 });
            return Json(terms, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Purchase Billing Term

        public ActionResult PurchaseBillingTerm()
        {
            ViewBag.UserRight = base.UserRight("PBT");
            return View();
        }

        public ActionResult PurchaseBillingTermList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("PBT");
            var list = _billingTerm.Filter(x => x.Type == "P" && !x.IsDeleted);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("PurchaseBillingTermList",
                               list.AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult PurchaseBillingTermAdd()
        {
            var viewModel = new BillingTermAddViewModel();
            viewModel.GeneralLedgerList = new SelectList(_ledger.GetMany(x => !x.IsDeleted), "Id", "AccountName");
            viewModel.BasisList = new SelectList(
                Enum.GetValues(typeof(BillingTermBasisEnum)).Cast
                    <BillingTermBasisEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.CategoryList = new SelectList(
                Enum.GetValues(typeof(BillingTermCategoryEnum)).Cast
                    <BillingTermCategoryEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.SignList = new SelectList(
                Enum.GetValues(typeof(SignEnum)).Cast
                    <SignEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");

            viewModel.BillingTerm = new BillingTerm();
            viewModel.BillingTerm.Type = "P";
            viewModel.BillingTerm.IsActive = true;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseBillingTermAdd(BillingTermAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            //if (_billingTerm.IsPurchaseBillingNameAvailable(model.BillingTerm.Description))
            //{
            //    if (_billingTerm.IsPurchaseBillingCodeAvailable(model.BillingTerm.Code))
            //    {
            model.BillingTerm.CreatedDate = DateTime.UtcNow;
            model.BillingTerm.UpdatedDate = DateTime.UtcNow;
            model.BillingTerm.CreatedById = userId;
            model.BillingTerm.UpdatedById = userId;
            model.BillingTerm.IsActive = true;
            model.BillingTerm.Type = "P";
            model.BillingTerm.BranchId = CurrentBranch.Id;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            model.BillingTerm.Category = cat;
            model.BillingTerm.Basis = 2;
            _billingTerm.Add(model.BillingTerm);
            //    }
            //    else
            //    {
            //        return Content("Purchase Billing Code already exists. Please enter a different Purchase Billing Code.");
            //    }
            //}
            //else
            //{
            //    return Content("Purchase Billing Name already exists. Please enter a different Purchase Billing Name.");
            //}
            return Content("True");
        }

        public ActionResult PurchaseBillingTermEdit(int id)
        {
            var data = _billingTerm.GetById(id);
            var viewModel = new BillingTermAddViewModel();
            viewModel.GeneralLedgerList = new SelectList(_ledger.GetMany(x => !x.IsDeleted), "Id", "AccountName",
                                                         data.GeneralLedgerId);
            viewModel.BasisList = new SelectList(
                Enum.GetValues(typeof(BillingTermBasisEnum)).Cast
                    <BillingTermBasisEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text",
                data.Basis);
            viewModel.CategoryList = new SelectList(
                Enum.GetValues(typeof(BillingTermCategoryEnum)).Cast
                    <BillingTermCategoryEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text", data.Category);
            viewModel.SignList = new SelectList(
                Enum.GetValues(typeof(SignEnum)).Cast
                    <SignEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text", data.Sign);
            viewModel.BillingTerm = data;
            data.Type = "P";
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult PurchaseBillingTermEdit(BillingTermAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var purchaseName = IsPurchaseBillingNameAvailable(model.BillingTerm.Id, model.BillingTerm.Description);
            var purchaseCode = IsPurchaseBillingCodeAvailable(model.BillingTerm.Id, model.BillingTerm.Code);
            if (purchaseCode.Data.ToString() == "")
            {
                if (purchaseName.Data.ToString() == "")
                {
                    model.BillingTerm.UpdatedDate = DateTime.UtcNow;
                    model.BillingTerm.UpdatedById = userId;
                    model.BillingTerm.Type = "P";
                    model.BillingTerm.BranchId = CurrentBranch.Id;
                    var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
                    model.BillingTerm.Category = cat;
                    model.BillingTerm.Basis = 2;
                    _dataContext.Entry(model.BillingTerm).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                }
                else
                {
                    return Content(purchaseName.Data.ToString());
                }
            }
            else
            {
                return Content(purchaseCode.Data.ToString());
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult PurchaseBillingTermDelete(int id)
        {
            var billingTerm = _billingTermDetail.GetMany(x => x.BillingTermId == id).Count();
            if (billingTerm == 0)
            {
                var model = _billingTerm.GetById(x => x.Id == id);
                model.IsDeleted = true;
                _billingTerm.Update(model);
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult IsPurchaseBillingCodeAvailable(int? id, int code)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_billingTerm.IsPurchaseBillingCodeAvailable(code))
                {

                    itemName = "Purchase Billing Code already exists. Please enter a different Purchase Billing Code.";
                }
            }
            else
            {
                var data = _billingTerm.GetById(x => x.Id == id);
                if (data.Code != code)
                {
                    if (!_billingTerm.IsPurchaseBillingCodeAvailable(code))
                    {
                        itemName =
                            "Purchase Billing Code already exists. Please enter a different Purchase Billing Code.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsPurchaseBillingNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_billingTerm.IsPurchaseBillingNameAvailable(name))
                {
                    itemName = "Purchase Billing Name already exists. Please enter a different Purchase Billing Name.";
                }
            }
            else
            {
                var data = _billingTerm.GetById(x => x.Id == id);
                if (data.Description.ToLower() != name.ToLower())
                {
                    if (!_billingTerm.IsPurchaseBillingNameAvailable(name))
                    {
                        itemName =
                            "Purchase Billing Name already exists. Please enter a different Purchase Billing Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPurchaseTermList()
        {
            var terms = _billingTerm.GetMany(x => !x.IsDeleted && x.Type == "P").Select(x => new
                                                                                                 {
                                                                                                     Id = x.Id,
                                                                                                     Description =
                                                                                                 x.Description
                                                                                                 }).OrderBy(
                                                                                                     x => x.Description);
            return Json(terms, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public JsonResult CheckBillingTermDisplayOrder(int DispalyOrder, int? Id)
        {
            var billingTerm = new BillingTerm();
            if (Id.HasValue && Id != 0)
            {
                var data = _billingTerm.GetById(x => x.Id == Id);
                if (data.DispalyOrder != DispalyOrder)
                {
                    billingTerm =
                        _billingTerm.GetById(x => x.DispalyOrder == DispalyOrder && !x.IsDeleted);

                }
            }
            else
            {
                billingTerm = _billingTerm.GetById(x => x.DispalyOrder == DispalyOrder && !x.IsDeleted);
            }
            if (billingTerm == null)
            {
                billingTerm = new BillingTerm();
            }

            return billingTerm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Area

        public JsonResult CheckAreaNameInArea(string AreaName, int? Id)
        {
            var feeterm = new Area();
            if (Id.HasValue && Id != 0)
            {
                var data = _area.GetById(x => x.Id == Id);
                if (data.AreaName.ToLower().Trim() != AreaName.ToLower().Trim())
                {
                    feeterm =
                        _area.GetById(x => x.AreaName.ToLower().Trim() == AreaName.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm = _area.GetById(x => x.AreaName.ToLower().Trim() == AreaName.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Area();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckShortNameInArea(string ShorName, int? Id)
        {
            var feeterm = new Area();
            if (Id.HasValue && Id != 0)
            {
                var data = _area.GetById(x => x.Id == Id);
                if (data.ShorName.ToLower().Trim() != ShorName.ToLower().Trim())
                {
                    feeterm =
                        _area.GetById(x => x.ShorName.ToLower().Trim() == ShorName.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm = _area.GetById(x => x.ShorName.ToLower().Trim() == ShorName.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Area();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult Area()
        {
            ViewBag.UserRight = base.UserRight("AR");
            return View();
        }

        public ActionResult AreaList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("AR");
            var list = _area.GetMany(x => !x.IsDeleted).OrderBy(a => a.AreaName);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("AreaList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult AreaAdd()
        {
            var model = new Area();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult AreaAdd(Area model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_area.IsAreaNameAvailable(model.AreaName))
            {
                if (_area.IsAreaShortNameAvailable(model.ShorName))
                {
                    model.CreatedDate = DateTime.UtcNow;
                    model.UpdatedDate = DateTime.UtcNow;
                    model.CreatedById = userId;
                    model.UpdatedById = userId;
                    _area.Add(model);

                    if (TempData["ShortNameAR"] != null)
                    {
                        ShortNameDetail shortNameDetail = (ShortNameDetail)TempData["ShortNameAR"];
                        _shortName.Add(shortNameDetail);
                    }
                }
                else
                {
                    return Content("Area Short Name already exists. Please enter a different Area Short Name.");
                }
            }
            else
            {

                return Content("Area Name already exists. Please enter a different Area Name.");
            }
            return Content("True");
        }

        public ActionResult AreaEdit(int id)
        {
            var data = _area.GetById(id);
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult AreaEdit(Area model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var Areaname = IsAreaNameAvailable(model.Id, model.AreaName);
            var shortname = IsAreaShortNameAvailable(model.Id, model.ShorName);
            if (Areaname.Data.ToString() == "")
            {
                if (shortname.Data.ToString() == "")
                {
                    model.UpdatedDate = DateTime.UtcNow;
                    model.UpdatedById = userId;

                    _dataContext.Entry(model).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                }
                else
                {
                    return Content(shortname.Data.ToString());
                }
            }
            else
            {
                return Content(Areaname.Data.ToString());
            }

            return Content("True");
        }

        [HttpPost]
        public ActionResult AreaDelete(int id)
        {
            var agent = _agent.GetMany(x => x.Area == id && !x.IsDeleted).Count();
            if (agent == 0)
            {
                var model = _area.GetById(x => x.Id == id);
                model.IsDeleted = true;
                _area.Update(model);
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult IsAreaNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_area.IsAreaNameAvailable(name))
                {
                    itemName = "Area Name already exists. Please enter a different Area Name.";
                }
            }
            else
            {
                var data = _area.GetById(x => x.Id == id);
                if (data.AreaName.ToLower() != name.ToLower())
                {
                    if (!_area.IsAreaNameAvailable(name))
                    {
                        itemName = "Area Name already exists. Please enter a different Area Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAreaShortNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_area.IsAreaShortNameAvailable(name))
                {
                    itemName = "Area Short Name already exists. Please enter a different Area Short Name.";
                }
            }
            else
            {
                var data = _area.GetById(x => x.Id == id);
                if (data.ShorName.ToLower() != name.ToLower())
                {
                    if (!_area.IsAreaShortNameAvailable(name))
                    {
                        itemName = "Area Short Name already exists. Please enter a different Area Short Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetArea()
        {
            var areas = _area.GetMany(x => !x.IsDeleted).Select(x => new
                                                                         {
                                                                             Id = x.Id,
                                                                             Description = x.AreaName
                                                                         });
            return Json(areas, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Godown

        public JsonResult CheckNameInGodown(string Name, int? Id)
        {

            var feeterm = new Godown();
            if (Id.HasValue && Id != 0)
            {
                var data = _godown.GetById(x => x.Id == Id);
                if (data.Name.ToLower().Trim() != Name.ToLower().Trim())
                {
                    feeterm =
                        _godown.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);

                }
            }
            else
            {
                feeterm = _godown.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Godown();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckShortNameInGodown(string ShortName, int? Id)
        {
            var feeterm = new Godown();
            if (Id.HasValue && Id != 0)
            {
                var data = _godown.GetById(x => x.Id == Id);
                if (data.ShortName.ToLower().Trim() != ShortName.ToLower().Trim())
                {
                    feeterm =
                        _godown.GetById(x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm =
                    _godown.GetById(x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Godown();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Godown()
        {
            ViewBag.UserRight = base.UserRight("GD");
            return View();
        }

        public ActionResult GodownList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("GD");
            var list = _godown.GetMany(x => !x.IsDeleted).OrderByDescending(a => a.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("GodownList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult GodownAdd()
        {
            var model = new Godown();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult GodownAdd(Godown model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_godown.IsGodownNameAvailable(model.Name))
            {
                if (_godown.IsGodownShortNameAvailable(model.ShortName))
                {
                    model.CreatedDate = DateTime.UtcNow;
                    model.UpdatedDate = DateTime.UtcNow;
                    model.CreatedById = userId;
                    model.UpdatedById = userId;
                    _godown.Add(model);

                    if (TempData["ShortNameGD"] != null)
                    {
                        ShortNameDetail shortNameDetail = (ShortNameDetail)TempData["ShortNameGD"];
                        _shortName.Add(shortNameDetail);
                    }
                }
                else
                {
                    return Content("");
                }
            }
            else
            {
                return Content("");
            }
            return Content("True");
        }

        public ActionResult GodownEdit(int id)
        {
            var data = _godown.GetById(id);
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult GodownEdit(Godown model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var name = IsGodownNameAvailable(model.Id, model.Name);
            var shortname = IsGodownShortNameAvailable(model.Id, model.ShortName);
            if (name.Data.ToString() == "")
            {
                if (shortname.Data.ToString() == "")
                {
                    model.UpdatedDate = DateTime.UtcNow;
                    model.UpdatedById = userId;
                    _dataContext.Entry(model).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                }
                else
                {
                    return Content(shortname.Data.ToString());
                }

            }
            else
            {
                return Content(name.Data.ToString());
            }

            return Content("True");
        }

        [HttpPost]
        public ActionResult GodownDelete(int id)
        {
            var model = _godown.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _godown.Update(model);
            return Json(true);
        }

        public JsonResult IsGodownNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_godown.IsGodownNameAvailable(name))
                {
                    itemName = "Godown Name already exists. Please enter a different Godown Name.";
                }
            }
            else
            {
                var data = _godown.GetById(x => x.Id == id);
                if (data.Name.ToLower() != name.ToLower())
                {
                    if (!_godown.IsGodownNameAvailable(name))
                    {
                        itemName = "Godown Name already exists. Please enter a different Godown Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsGodownShortNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_godown.IsGodownShortNameAvailable(name))
                {
                    itemName = "Godown Short Name already exists. Please enter a different Godown Short Name.";
                }
            }
            else
            {
                var data = _godown.GetById(x => x.Id == id);
                if (data.ShortName.ToLower() != name.ToLower())
                {
                    if (!_godown.IsGodownNameAvailable(name))
                    {
                        itemName = "Godown Short Name already exists. Please enter a different Godown Short Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Currency

        public JsonResult CheckCodeInCurrency(string Code, int? Id)
        {
            var feeterm = new Currency();
            if (Id.HasValue && Id != 0)
            {
                var data = _currency.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm = _currency.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm = _currency.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Currency();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public JsonResult CheckNameInCurrency(string Name, int? Id)
        {
            var feeterm = new Currency();
            if (Id.HasValue && Id != 0)
            {
                var data = _currency.GetById(x => x.Id == Id);
                if (data.Name.ToLower().Trim() != Name.ToLower().Trim())
                {
                    feeterm = _currency.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm = _currency.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Currency();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Currency()
        {
            ViewBag.UserRight = base.UserRight("CUR");
            return View();
        }

        public ActionResult CurrencyList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("CUR");
            var list = _currency.GetMany(x => !x.IsDeleted).OrderByDescending(a => a.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("CurrencyList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult CurrencyAdd()
        {
            var model = new Currency();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult CurrencyAdd(Currency model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_currency.IsCurrencyNameAvailable(model.Name))
            {
                if (_currency.IsCurrencyCodeAvailable(model.Code))
                {
                    model.CreatedDate = DateTime.UtcNow;
                    model.UpdatedDate = DateTime.UtcNow;
                    model.CreatedById = userId;
                    model.UpdatedById = userId;
                    _currency.Add(model);
                }
                else
                {
                    return Content("Currency Code already exists. Please enter a different Currency Code.");
                }
            }
            else
            {
                return Content("Currency Name already exists. Please enter a different Currency Name.");
            }
            return Content("True");
        }

        public ActionResult CurrencyEdit(int id)
        {
            var data = _currency.GetById(id);
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult CurrencyEdit(Currency model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var name = IsCurrencyNameAvailable(model.Id, model.Name);
            var code = IsCurrencyCodeAvailable(model.Id, model.Code);
            if (name.Data.ToString() == "")
            {
                if (code.Data.ToString() == "")
                {
                    model.UpdatedDate = DateTime.UtcNow;
                    model.UpdatedById = userId;

                    _dataContext.Entry(model).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                }
                else
                {
                    return Content(code.Data.ToString());
                }
            }
            else
            {
                return Content(code.Data.ToString());
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult CurrencyDelete(int id)
        {
            var model = _currency.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _currency.Update(model);
            return Json(true);
        }

        public JsonResult IsCurrencyNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_currency.IsCurrencyNameAvailable(name))
                {
                    itemName = "Currency Name already exists. Please enter a different Currency Name.";
                }
            }
            else
            {
                var data = _currency.GetById(x => x.Id == id);
                if (data.Name.ToLower() != name.ToLower())
                {
                    if (!_currency.IsCurrencyNameAvailable(name))
                    {
                        itemName = "Currency Name already exists. Please enter a different Currency Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCurrencyCodeAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_currency.IsCurrencyCodeAvailable(name))
                {
                    itemName = "Currency Code already exists. Please enter a different Currency Code.";
                }
            }
            else
            {
                var data = _currency.GetById(x => x.Id == id);
                if (data.Code.ToLower() != name.ToLower())
                {
                    if (!_currency.IsCurrencyCodeAvailable(name))
                    {
                        itemName = "Currency Code already exists. Please enter a different Currency Code.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Cost Center

        public JsonResult CheckNameInCostCentre(string Name, int? Id)
        {
            var feeterm = new CostCenter();
            if (Id.HasValue && Id != 0)
            {
                var data = _costCenter.GetById(x => x.Id == Id);
                if (data.Name.ToLower().Trim() != Name.ToLower().Trim())
                {
                    feeterm = _costCenter.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm = _costCenter.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new CostCenter();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckShortNameInCostCentre(string ShortName, int? Id)
        {
            var feeterm = new CostCenter();
            if (Id.HasValue && Id != 0)
            {
                var data = _costCenter.GetById(x => x.Id == Id);
                if (data.ShortName.ToLower().Trim() != ShortName.ToLower().Trim())
                {
                    feeterm =
                        _costCenter.GetById(
                            x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);
                }
            }
            else
            {
                feeterm =
                    _costCenter.GetById(x => x.ShortName.ToLower().Trim() == ShortName.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new CostCenter();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CostCenter()
        {
            return View();
        }

        public ActionResult CostCenterList(int pageNo = 1)
        {
            var list = _costCenter.GetMany(x => !x.IsDeleted).OrderByDescending(a => a.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("CostCenterList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult CostCenterAdd()
        {
            var model = new CostCenter();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult CostCenterAdd(CostCenter model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.UpdatedById = userId;
            _costCenter.Add(model);

            if (TempData["ShortNameCC"] != null)
            {
                ShortNameDetail shortNameDetail = (ShortNameDetail)TempData["ShortNameCC"];
                _shortName.Add(shortNameDetail);
            }
            return Content("True");
        }

        public ActionResult CostCenterEdit(int id)
        {
            var data = _costCenter.GetById(id);
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult CostCenterEdit(CostCenter model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var name = IsCostCenterNameAvailable(model.Id, model.Name);
            var shortname = IsCostCenterShortNameAvailable(model.Id, model.ShortName);
            if (name.Data.ToString() == "")
            {
                if (shortname.Data.ToString() == "")
                {
                    model.UpdatedDate = DateTime.UtcNow;
                    model.UpdatedById = userId;
                    _dataContext.Entry(model).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                }
                else
                {
                    return Content(shortname.Data.ToString());
                }
            }
            else
            {
                return Content(name.Data.ToString());
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult CostCenterDelete(int id)
        {
            var model = _costCenter.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _costCenter.Update(model);
            return Json(true);
        }

        public JsonResult IsCostCenterNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_costCenter.IsCostCenterNameAvailable(name))
                {
                    itemName = "CostCenter Name already exists. Please enter a different CostCenter Name.";
                }
            }
            else
            {
                var data = _costCenter.GetById(x => x.Id == id);
                if (data.Name.ToLower() != name.ToLower())
                {
                    if (!_costCenter.IsCostCenterNameAvailable(name))
                    {
                        itemName = "CostCenter Name already exists. Please enter a different CostCenter Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCostCenterShortNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_costCenter.IsCostCenterShorNameAvailable(name))
                {
                    itemName = "CostCenter Short Name already exists. Please enter a different CostCenter Short Name.";
                }
            }
            else
            {
                var data = _costCenter.GetById(x => x.Id == id);
                if (data.ShortName.ToLower() != name.ToLower())
                {
                    if (!_costCenter.IsCostCenterShorNameAvailable(name))
                    {
                        itemName =
                            "CostCenter Short Name already exists. Please enter a different CostCenter Short Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Narration Master

        public JsonResult CheckNameInNarration(string Name, int? Id)
        {
            var feeterm = new Narration();
            if (Id.HasValue && Id != 0)
            {
                var data = _narration.GetById(x => x.Id == Id);
                if (data.Name.ToLower().Trim() != Name.ToLower().Trim())
                {
                    feeterm =
                        _narration.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);

                }
            }
            else
            {
                feeterm = _narration.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Narration();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult GetNarrationList(int module)
        {
            var data = _narration.Filter(x => x.Type == module).Select(x => new
                                                                                {

                                                                                    Id = x.Id,
                                                                                    Description = x.Name,

                                                                                });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Narration()
        {
            return View();
        }

        public ActionResult NarrationList(int pageNo = 1)
        {
            var list = _narration.GetMany(x => !x.IsDeleted).OrderByDescending(a => a.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("NarrationList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult NarrationAdd()
        {
            var viewModel = new NarrationAddViewModel();
            viewModel.TypeList =
                new SelectList(
                    Enum.GetValues(typeof(NarrationTypeEnum)).Cast
                        <NarrationTypeEnum>().Select(
                            x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value",
                    "Text");
            viewModel.Narration = new Narration();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult NarrationAdd(NarrationAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_narration.IsNarrationNameAvailable(model.Narration.Name))
            {
                model.Narration.CreatedDate = DateTime.UtcNow;
                model.Narration.UpdatedDate = DateTime.UtcNow;
                model.Narration.CreatedById = userId;
                model.Narration.UpdatedById = userId;
                _narration.Add(model.Narration);
            }
            else
            {
                return Content("Narration Name already exists. Please enter a different Narration Name.");
            }
            return Content("True");
        }

        public ActionResult NarrationEdit(int id)
        {
            var data = _narration.GetById(id);
            var viewModel = new NarrationAddViewModel();
            viewModel.TypeList =
                new SelectList(
                    Enum.GetValues(typeof(NarrationTypeEnum)).Cast
                        <NarrationTypeEnum>().Select(
                            x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value",
                    "Text", data.Type);
            viewModel.Narration = data;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult NarrationEdit(NarrationAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var narration = IsNarrationNameAvailable(model.Narration.Id, model.Narration.Name);
            if (narration.Data.ToString() == "")
            {
                model.Narration.UpdatedDate = DateTime.UtcNow;
                model.Narration.UpdatedById = userId;

                _dataContext.Entry(model.Narration).State = EntityState.Modified;
                _dataContext.SaveChanges();
            }
            else
            {
                return Content(narration.Data.ToString());
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult NarrationDelete(int id)
        {
            var model = _narration.GetById(x => x.Id == id);
            model.IsDeleted = true;
            _narration.Update(model);
            return Json(true);
        }

        public JsonResult IsNarrationNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_narration.IsNarrationNameAvailable(name))
                {
                    itemName = "Narration Name already exists. Please enter a different Narration Name.";
                }
            }
            else
            {
                var data = _costCenter.GetById(x => x.Id == id);
                if (data != null)
                {
                    if (data.Name.ToLower() != name.ToLower())
                    {
                        if (!_narration.IsNarrationNameAvailable(name))
                        {
                            itemName = "Narration Name already exists. Please enter a different Narration Name.";
                        }
                    }
                }

            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Agent

        public JsonResult CheckNameInAgent(string Name, int? Id)
        {
            var feeterm = new Agent();
            if (Id.HasValue && Id != 0)
            {
                var data = _agent.GetById(x => x.Id == Id);
                if (data.Name.ToLower().Trim() != Name.ToLower().Trim())
                {
                    feeterm =
                        _agent.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);

                }
            }
            else
            {
                feeterm = _agent.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Agent();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public JsonResult CheckShortNameInAgent(string ShorName, int? Id)
        {
            var feeterm = new Agent();
            if (Id.HasValue && Id != 0)
            {
                var data = _agent.GetById(x => x.Id == Id);
                if (data.ShorName.ToLower().Trim() != ShorName.ToLower().Trim())
                {
                    feeterm =
                        _agent.GetById(x => x.ShorName.ToLower().Trim() == ShorName.Trim().ToLower() && !x.IsDeleted);

                }
            }
            else
            {
                feeterm = _agent.GetById(x => x.ShorName.ToLower().Trim() == ShorName.Trim().ToLower() && !x.IsDeleted);
            }
            if (feeterm == null)
            {
                feeterm = new Agent();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public ActionResult Agent()
        {
            return View();
        }

        public ActionResult AgentList(int pageNo = 1)
        {
            var list = new List<Agent>();
            if (EnableBranch)
            {
                list =
                    _agent.GetMany(x => x.BranchId == CurrentBranchId && !x.IsDeleted).OrderByDescending(x => x.Id).
                        ToList();
            }
            else
            {
                list = _agent.GetMany(x => !x.IsDeleted).OrderByDescending(x => x.Id).ToList();
            }
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView(list.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        // [CheckPermission(Permissions = "Create", Module = "CAS")]
        public ActionResult AgentAdd()
        {
            var viewModel = new AgentAddViewModel();
            viewModel.LedgerList = new SelectList(_ledger.GetMany(x => !x.IsDeleted), "Id", "AccountName");
            viewModel.SubLedgerList = new SelectList(_subLedger.GetMany(x => !x.IsDeleted), "Id", "Description");
            viewModel.Agent = new Agent();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult AgentAdd(AgentAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_agent.IsAgentNameAvailable(model.Agent.Name))
                {
                    if (_agent.IsAgentShortNameAvailable(model.Agent.ShorName))
                    {
                        var userId = _authentication.GetAuthenticatedUser().Id;
                        model.Agent.CreatedDate = DateTime.UtcNow;
                        model.Agent.UpdatedDate = DateTime.UtcNow;
                        model.Agent.CreatedById = userId;
                        model.Agent.UpdatedById = userId;
                        model.Agent.IsActive = true;
                        model.Agent.IsDeleted = false;
                        model.Agent.BranchId = CurrentBranchId;
                        _agent.Add(model.Agent);

                        if (TempData["ShortNameAT"] != null)
                        {
                            ShortNameDetail shortNameDetail = (ShortNameDetail)TempData["ShortNameAT"];
                            _shortName.Add(shortNameDetail);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Agent.ShorName", "Agent Short Name already exists!");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("Agent.Name", "Agent Name already exists!");
                    return View(model);
                }
            }
            return Content("True");
        }

        [CheckPermission(Permissions = "Edit", Module = "CAS")]
        public ActionResult AgentEdit(int id)
        {
            var data = _agent.GetById(id);
            var viewModel = new AgentAddViewModel();
            viewModel.LedgerList = new SelectList(_ledger.GetMany(x => !x.IsDeleted), "Id", "AccountName", data.LedgerId);
            viewModel.SubLedgerList = new SelectList(_subLedger.GetMany(x => !x.IsDeleted), "Id", "Description",
                                                     data.SubLedgerId);
            viewModel.Agent = data;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult AgentEdit(AgentAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            var name = IsAgentNameAvailable(model.Agent.Id, model.Agent.Name);
            var shortname = IsAgentShortNameAvailable(model.Agent.Id, model.Agent.ShorName);
            if (name.Data.ToString() == "")
            {
                if (shortname.Data.ToString() == "")
                {
                    model.Agent.UpdatedDate = DateTime.UtcNow;
                    model.Agent.UpdatedById = userId;

                    _dataContext.Entry(model.Agent).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                }
                else
                {
                    return Content(shortname.Data.ToString());
                }
            }
            else
            {
                return Content(name.Data.ToString());
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult AgentDelete(int id)
        {
            var ID = Convert.ToString(id);
            var agent = _accountTransaction.GetMany(x => x.AgentCode == ID).Count();
            if (agent == 0)
            {
                var model = _agent.GetById(x => x.Id == id);
                model.IsDeleted = true;
                _agent.Update(model);
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult IsAgentNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_agent.IsAgentNameAvailable(name))
                {
                    itemName = "Agent Name already exists. Please enter a different Agent Name.";
                }
            }
            else
            {
                var data = _agent.GetById(x => x.Id == id);
                if (data.Name.ToLower() != name.ToLower())
                {
                    if (!_agent.IsAgentNameAvailable(name))
                    {
                        itemName = "Agent Name already exists. Please enter a different Agent Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAgentShortNameAvailable(int? id, string name)
        {
            var itemName = string.Empty;
            if (id == null)
            {
                if (!_agent.IsAgentShortNameAvailable(name))
                {
                    itemName = "Agent Short Name already exists. Please enter a different Agent Short Name.";
                }
            }
            else
            {
                var data = _agent.GetById(x => x.Id == id);
                if (data.ShorName.ToLower() != name.ToLower())
                {
                    if (!_agent.IsAgentNameAvailable(name))
                    {
                        itemName = "Agent Short Name already exists. Please enter a different Agent Short Name.";
                    }
                }
            }
            return Json(itemName, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region UDF

        public ActionResult Udf()
        {
            return View();
        }

        public ActionResult UdfList()
        {
            var data = _udfEntry.GetAll().OrderByDescending(a => a.Id);
            return PartialView(data);
        }

        public ActionResult UdfAdd()
        {
            var viemodel = new UdfAddViewModel
                               {
                                   ControlType = new SelectList(Enum.GetValues(typeof(ControlTypeEnum)).Cast
                                                                    <ControlTypeEnum>().Select(
                                                                        x =>
                                                                        new SelectListItem
                                                                            {
                                                                                Text = StringEnum.GetStringValue(x),
                                                                                Value = ((int)x).ToString()
                                                                            }), "Value",
                                                                "Text"),
                                   EntryModule = new SelectList(Enum.GetValues(typeof(EntryModuleEnum)).Cast
                                                                    <EntryModuleEnum>().Select(
                                                                        x =>
                                                                        new SelectListItem
                                                                            {
                                                                                Text = StringEnum.GetStringValue(x),
                                                                                Value = ((int)x).ToString()
                                                                            }), "Value",
                                                                "Text")
                               };
            return PartialView(viemodel);


        }

        [HttpPost]
        public ActionResult UdfAdd(IEnumerable<UDFEntryDetail> UDFDetailEntry, UdfAddViewModel model)
        {
            _udfEntry.Add(model.UdfEntry);
            foreach (var udfEntryDetial in UDFDetailEntry)
            {
                if (udfEntryDetial.Value != null)
                {
                    var detail = new UDFEntryDetail()
                                     {
                                         UdfId = model.UdfEntry.Id,
                                         Value = udfEntryDetial.Value
                                     };
                    _udfEntryDetail.Add(detail);
                }
            }
            return null;
        }

        public ActionResult UdfEdit(int id)
        {
            var data = _udfEntry.GetById(id);
            var viemodel = new UdfAddViewModel
                               {
                                   ControlType = new SelectList(Enum.GetValues(typeof(ControlTypeEnum)).Cast
                                                                    <ControlTypeEnum>().Select(
                                                                        x =>
                                                                        new SelectListItem
                                                                            {
                                                                                Text = StringEnum.GetStringValue(x),
                                                                                Value = ((int)x).ToString()
                                                                            }), "Value",
                                                                "Text", data.ControlTypeId),
                                   EntryModule = new SelectList(Enum.GetValues(typeof(EntryModuleEnum)).Cast
                                                                    <EntryModuleEnum>().Select(
                                                                        x =>
                                                                        new SelectListItem
                                                                            {
                                                                                Text = StringEnum.GetStringValue(x),
                                                                                Value = ((int)x).ToString()
                                                                            }), "Value",
                                                                "Text", data.EntryModuleId),
                                   UdfEntry = data
                               };
            return PartialView(viemodel);


        }

        [HttpPost]
        public ActionResult UdfEdit(IEnumerable<UDFEntryDetail> UDFDetailEntry, UdfAddViewModel model)
        {
            _udfEntry.Update(model.UdfEntry);
            _udfEntryDetail.Delete(x => x.UdfId == model.UdfEntry.Id);
            foreach (var udfEntryDetial in UDFDetailEntry)
            {
                if (udfEntryDetial.Value != null)
                {
                    var detail = new UDFEntryDetail()
                                     {
                                         UdfId = model.UdfEntry.Id,
                                         Value = udfEntryDetial.Value
                                     };
                    _udfEntryDetail.Add(detail);
                }
            }
            return null;
        }

        public ActionResult GetUDFDetailEntry()
        {
            var data = new UDFEntryDetail();
            return PartialView("_PartialUdfDetailEntry", data);
        }

        #endregion

        #region OpeningLedger

        public ActionResult OpeningLedger()
        {
            return View();
        }

        public ActionResult OpeningLegerList(int pageNo = 1)
        {
            var list = _openingLedger.GetAll().OrderByDescending(a => a.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("OpeningLegerList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult PartialOpeningLedgerAdd()
        {
            var model = new OpeningLedger();
            model.AmountTypeList = new SelectList(
                Enum.GetValues(typeof(JVAmountTypeEnum)).Cast
                    <JVAmountTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            return PartialView(model);
        }


        public ActionResult PartialOpeningVendorAdd()
        {
            var model = new OpeningLedger();
            model.AmountTypeList = new SelectList(
                Enum.GetValues(typeof(JVAmountTypeEnum)).Cast
                    <JVAmountTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            return PartialView(model);
        }

        public ActionResult PartialOpeningBothAdd()
        {
            var model = new OpeningLedger();
            model.AmountTypeList = new SelectList(
                Enum.GetValues(typeof(JVAmountTypeEnum)).Cast
                    <JVAmountTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            return PartialView(model);
        }

        public ActionResult PartialOpeningOtherAdd()
        {
            var model = new OpeningLedger();
            model.AmountTypeList = new SelectList(
                Enum.GetValues(typeof(JVAmountTypeEnum)).Cast
                    <JVAmountTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            return PartialView(model);
        }

        public ActionResult OpeningLegerAdd()
        {
            var cat = Enum.GetName(typeof(LedgerCategoryEnum), 3).ToString();
            var viewModel = base.CreateViewModel<OpeningLedgerAddViewModel>();
            viewModel.Category =
                new SelectList(
                    Enum.GetValues(typeof(LedgerCategoryEnum)).Cast
                        <LedgerCategoryEnum>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text");

            viewModel.Ledger = new SelectList(_ledger.GetMany(x => x.Category == cat), "Id", "AccountName");
            viewModel.AmountType = new SelectList(
                Enum.GetValues(typeof(JVAmountTypeEnum)).Cast
                    <JVAmountTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.LegderId = 0;
            viewModel.CustomerOpenings = _openingLedger.GetMany(x => x.LedgerType == (int)LedgerCategoryEnum.CU);
            viewModel.VendorOpenings = _openingLedger.GetMany(x => x.LedgerType == (int)LedgerCategoryEnum.VE);
            viewModel.BothOpenings = _openingLedger.GetMany(x => x.LedgerType == (int)LedgerCategoryEnum.BO);
            viewModel.OtherOpenings = _openingLedger.GetMany(x => x.LedgerType == (int)LedgerCategoryEnum.OT);
            foreach (var item in viewModel.CustomerOpenings)
            {
                if (item.SlCode != null)
                {
                    var code = Convert.ToInt32(item.SlCode);
                    item.SubLedger = _subLedger.GetById(code);
                }

            }
            foreach (var item in viewModel.VendorOpenings)
            {
                if (item.SlCode != null)
                {
                    var code = Convert.ToInt32(item.SlCode);
                    item.SubLedger = _subLedger.GetById(code);
                }

            }

            foreach (var item in viewModel.BothOpenings)
            {
                if (item.SlCode != null)
                {
                    var code = Convert.ToInt32(item.SlCode);
                    item.SubLedger = _subLedger.GetById(code);
                }

            }

            foreach (var item in viewModel.OtherOpenings)
            {
                if (item.SlCode != null)
                {
                    var code = Convert.ToInt32(item.SlCode);
                    item.SubLedger = _subLedger.GetById(code);
                }

            }
            return PartialView(viewModel);
        }

        public ActionResult GetLedgerbyType(int id)
        {
            var catType = Enum.GetName(typeof(LedgerCategoryEnum), id).ToString();
            var ledgers = _ledger.GetMany(x => x.Category.ToLower() == catType.ToLower() && !x.IsDeleted).Select(
                x => new
                         {
                             Id = x.Id,
                             Code = x.ShortName,
                             Description = x.AccountName
                         }).OrderBy(x => x.Description);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OpeningLegerAddCustomer(OpeningLedgerAddViewModel model,
                                                    IEnumerable<OpeningLedger> openingledger)
        {
            var typeid = (int)LedgerCategoryEnum.CU;
            var openingledgers = _openingLedger.GetMany(x => x.LedgerType == typeid).Select(x => x.Id);
            _openingLedger.Delete(x => x.LedgerType == typeid);
            foreach (var i in openingledgers)
            {
                _accountTransaction.Delete(x => x.ReferenceId == i && x.Source.ToLower() == "ob");
            }
            foreach (var ledger in openingledger.Where(x => x.GlCode != 0))
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                int sno = 1;
                if (ledger.Amount != 0)
                {
                    var Opening = new OpeningLedger()
                                      {
                                          AmountType = ledger.AmountType,
                                          CreatedById = userId,
                                          GlCode = ledger.GlCode,
                                          CreatedDate = (this.FiscalYear.StartDate).AddDays(-1),
                                          Amount = ledger.Amount,
                                          FiscalYearId = FiscalYear.Id,
                                          LedgerType = (int)LedgerCategoryEnum.CU,
                                          BranchId = this.CompanyInfo.Id,
                                          SlCode = ledger.SlCode
                                      };
                    _openingLedger.Add(Opening);
                    var accountTransactionMaster = new AccountTransaction();

                    if (Opening.AmountType == (int)JVAmountTypeEnum.D)
                    {
                        accountTransactionMaster.DrAmt = Opening.Amount;
                        accountTransactionMaster.CrAmt = 0;
                        accountTransactionMaster.LocalDrAmt = Opening.Amount;
                        accountTransactionMaster.LocalCrAmt = 0;
                    }
                    else
                    {
                        accountTransactionMaster.DrAmt = 0;
                        accountTransactionMaster.CrAmt = Opening.Amount;
                        accountTransactionMaster.LocalDrAmt = 0;
                        accountTransactionMaster.LocalCrAmt = Opening.Amount;
                    }
                    accountTransactionMaster.GlCode = ledger.GlCode;
                    accountTransactionMaster.ReferenceId = Opening.Id;
                    accountTransactionMaster.VNo = "";
                    accountTransactionMaster.VDate = Opening.CreatedDate;
                    accountTransactionMaster.VMiti = NepaliDateService.NepaliDate(Opening.CreatedDate).Date;
                    accountTransactionMaster.CrRate = 1;
                    accountTransactionMaster.SNo = sno;
                    var isCashBank = _ledger.GetById(ledger.GlCode).IsCashBank;
                    if (isCashBank)
                        accountTransactionMaster.CbCode = ledger.GlCode;
                    accountTransactionMaster.Source = "OB";
                    accountTransactionMaster.CreatedById = userId;
                    accountTransactionMaster.FYId = this.FiscalYear.Id;
                    accountTransactionMaster.BranchId = this.CompanyInfo.Id;
                    accountTransactionMaster.SlCode = ledger.SlCode;

                    _accountTransaction.Add(accountTransactionMaster);
                }
            }
            return Content("True");

        }

        [HttpPost]
        public ActionResult OpeningLegerAddVendor(OpeningLedgerAddViewModel model,
                                                  IEnumerable<OpeningLedger> openingledger)
        {
            var typeid = (int)LedgerCategoryEnum.VE;
            var openingledgers = _openingLedger.GetMany(x => x.LedgerType == typeid).Select(x => x.Id);
            _openingLedger.Delete(x => x.LedgerType == typeid);
            foreach (var i in openingledgers)
            {
                _accountTransaction.Delete(x => x.ReferenceId == i && x.Source.ToLower() == "ob");
            }
            foreach (var ledger in openingledger.Where(x => x.GlCode != 0))
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                int sno = 1;
                if (ledger.Amount != 0)
                {
                    var Opening = new OpeningLedger()
                                      {
                                          AmountType = ledger.AmountType,
                                          CreatedById = userId,
                                          GlCode = ledger.GlCode,
                                          CreatedDate = (this.FiscalYear.StartDate).AddDays(-1),
                                          Amount = ledger.Amount,
                                          FiscalYearId = FiscalYear.Id,
                                          LedgerType = (int)LedgerCategoryEnum.VE,
                                          BranchId = this.CompanyInfo.Id,
                                          SlCode = ledger.SlCode
                                      };
                    _openingLedger.Add(Opening);
                    var accountTransactionMaster = new AccountTransaction();

                    if (Opening.AmountType == (int)JVAmountTypeEnum.D)
                    {
                        accountTransactionMaster.DrAmt = Opening.Amount;
                        accountTransactionMaster.CrAmt = 0;
                        accountTransactionMaster.LocalDrAmt = Opening.Amount;
                        accountTransactionMaster.LocalCrAmt = 0;
                    }
                    else
                    {
                        accountTransactionMaster.DrAmt = 0;
                        accountTransactionMaster.CrAmt = Opening.Amount;
                        accountTransactionMaster.LocalDrAmt = 0;
                        accountTransactionMaster.LocalCrAmt = Opening.Amount;
                    }
                    accountTransactionMaster.GlCode = ledger.GlCode;
                    accountTransactionMaster.ReferenceId = Opening.Id;
                    accountTransactionMaster.VNo = "";
                    accountTransactionMaster.VDate = Opening.CreatedDate;
                    accountTransactionMaster.VMiti = NepaliDateService.NepaliDate(Opening.CreatedDate).Date;
                    accountTransactionMaster.CrRate = 1;
                    accountTransactionMaster.SNo = sno;
                    var isCashBank = _ledger.GetById(ledger.GlCode).IsCashBank;
                    if (isCashBank)
                        accountTransactionMaster.CbCode = ledger.GlCode;
                    accountTransactionMaster.Source = "OB";
                    accountTransactionMaster.CreatedById = userId;
                    accountTransactionMaster.FYId = this.FiscalYear.Id;
                    accountTransactionMaster.BranchId = this.CompanyInfo.Id;
                    accountTransactionMaster.SlCode = ledger.SlCode;

                    _accountTransaction.Add(accountTransactionMaster);
                }
            }
            return Content("True");

        }

        [HttpPost]
        public ActionResult OpeningLegerAddBoth(OpeningLedgerAddViewModel model,
                                                IEnumerable<OpeningLedger> openingledger)
        {
            var typeid = (int)LedgerCategoryEnum.BO;
            var openingledgers = _openingLedger.GetMany(x => x.LedgerType == typeid).Select(x => x.Id);
            _openingLedger.Delete(x => x.LedgerType == typeid);
            foreach (var i in openingledgers)
            {
                _accountTransaction.Delete(x => x.ReferenceId == i && x.Source.ToLower() == "ob");
            }
            foreach (var ledger in openingledger.Where(x => x.GlCode != 0))
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                int sno = 1;
                if (ledger.Amount != 0)
                {
                    var Opening = new OpeningLedger()
                                      {
                                          AmountType = ledger.AmountType,
                                          CreatedById = userId,
                                          GlCode = ledger.GlCode,
                                          CreatedDate = (this.FiscalYear.StartDate).AddDays(-1),
                                          Amount = ledger.Amount,
                                          FiscalYearId = FiscalYear.Id,
                                          LedgerType = (int)LedgerCategoryEnum.BO,
                                          BranchId = this.CompanyInfo.Id,
                                          SlCode = ledger.SlCode
                                      };
                    _openingLedger.Add(Opening);
                    var accountTransactionMaster = new AccountTransaction();

                    if (Opening.AmountType == (int)JVAmountTypeEnum.D)
                    {
                        accountTransactionMaster.DrAmt = Opening.Amount;
                        accountTransactionMaster.CrAmt = 0;
                        accountTransactionMaster.LocalDrAmt = Opening.Amount;
                        accountTransactionMaster.LocalCrAmt = 0;
                    }
                    else
                    {
                        accountTransactionMaster.DrAmt = 0;
                        accountTransactionMaster.CrAmt = Opening.Amount;
                        accountTransactionMaster.LocalCrAmt = 0;
                        accountTransactionMaster.LocalCrAmt = Opening.Amount;
                    }
                    accountTransactionMaster.GlCode = ledger.GlCode;
                    accountTransactionMaster.ReferenceId = Opening.Id;
                    accountTransactionMaster.VNo = "";
                    accountTransactionMaster.VDate = Opening.CreatedDate;
                    accountTransactionMaster.VMiti = NepaliDateService.NepaliDate(Opening.CreatedDate).Date;
                    accountTransactionMaster.CrRate = 1;
                    accountTransactionMaster.SNo = sno;
                    var isCashBank = _ledger.GetById(ledger.GlCode).IsCashBank;
                    if (isCashBank)
                        accountTransactionMaster.CbCode = ledger.GlCode;
                    accountTransactionMaster.Source = "OB";
                    accountTransactionMaster.CreatedById = userId;
                    accountTransactionMaster.FYId = this.FiscalYear.Id;
                    accountTransactionMaster.BranchId = this.CompanyInfo.Id;
                    accountTransactionMaster.SlCode = ledger.SlCode;

                    _accountTransaction.Add(accountTransactionMaster);
                }
            }
            return Content("True");

        }

        [HttpPost]
        public ActionResult OpeningLegerAddOther(OpeningLedgerAddViewModel model,
                                                 IEnumerable<OpeningLedger> openingledger)
        {
            var typeid = (int)LedgerCategoryEnum.OT;
            var openingledgers = _openingLedger.GetMany(x => x.LedgerType == typeid).Select(x => x.Id);
            _openingLedger.Delete(x => x.LedgerType == typeid);
            foreach (var i in openingledgers)
            {
                _accountTransaction.Delete(x => x.ReferenceId == i && x.Source.ToLower() == "ob");
            }
            foreach (var ledger in openingledger.Where(x => x.GlCode != 0))
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                int sno = 1;
                if (ledger.Amount != 0)
                {
                    var Opening = new OpeningLedger()
                                      {
                                          AmountType = ledger.AmountType,
                                          CreatedById = userId,
                                          GlCode = ledger.GlCode,
                                          CreatedDate = (this.FiscalYear.StartDate).AddDays(-1),
                                          Amount = ledger.Amount,
                                          FiscalYearId = FiscalYear.Id,
                                          LedgerType = (int)LedgerCategoryEnum.OT,
                                          BranchId = this.CompanyInfo.Id,
                                          SlCode = ledger.SlCode
                                      };
                    _openingLedger.Add(Opening);
                    var accountTransactionMaster = new AccountTransaction();

                    if (Opening.AmountType == (int)JVAmountTypeEnum.D)
                    {
                        accountTransactionMaster.DrAmt = Opening.Amount;
                        accountTransactionMaster.CrAmt = 0;
                        accountTransactionMaster.LocalDrAmt = Opening.Amount;
                        accountTransactionMaster.LocalCrAmt = 0;
                    }
                    else
                    {
                        accountTransactionMaster.DrAmt = 0;
                        accountTransactionMaster.CrAmt = Opening.Amount;
                        accountTransactionMaster.LocalDrAmt = 0;
                        accountTransactionMaster.LocalCrAmt = Opening.Amount;
                    }
                    accountTransactionMaster.GlCode = ledger.GlCode;
                    accountTransactionMaster.ReferenceId = Opening.Id;
                    accountTransactionMaster.VNo = "";
                    accountTransactionMaster.VDate = Opening.CreatedDate;
                    accountTransactionMaster.VMiti = NepaliDateService.NepaliDate(Opening.CreatedDate).Date;
                    accountTransactionMaster.CrRate = 1;
                    accountTransactionMaster.SNo = sno;
                    var isCashBank = _ledger.GetById(ledger.GlCode).IsCashBank;
                    if (isCashBank)
                        accountTransactionMaster.CbCode = ledger.GlCode;
                    accountTransactionMaster.Source = "OB";
                    accountTransactionMaster.CreatedById = userId;
                    accountTransactionMaster.FYId = this.FiscalYear.Id;
                    accountTransactionMaster.BranchId = this.CompanyInfo.Id;
                    accountTransactionMaster.SlCode = ledger.SlCode;

                    _accountTransaction.Add(accountTransactionMaster);
                }
            }
            return Content("True");

        }

        public ActionResult OpeningLedgerEntry()
        {
            var data = new OpeningLedgerAddViewModel();
            data.AmountType = new SelectList(
                Enum.GetValues(typeof(JVAmountTypeEnum)).Cast
                    <JVAmountTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            return PartialView("_PartialOpeningLedgerEntry", data);
        }

        public ActionResult GetLedgerSupplier()
        {
            var cat = Enum.GetName(typeof(LedgerCategoryEnum), 1).ToString();
            var cat1 = Enum.GetName(typeof(LedgerCategoryEnum), 4).ToString();
            var ledgers = _ledger.Filter(x => x.Category == cat || x.Category == cat1).Select(x => new
                                                                                                       {
                                                                                                           Id = x.Id,
                                                                                                           Description =
                                                                                                       x.AccountName,
                                                                                                           ShortName =
                                                                                                       x.ShortName
                                                                                                       });
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLedgerByCategory(string cat)
        {
            var ledgers = _ledger.Filter(x => x.Category == cat || x.Category == cat).Select(x => new
                                                                                                      {
                                                                                                          Id = x.Id,
                                                                                                          AccountName =
                                                                                                      x.AccountName

                                                                                                      });
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOpeningBalanceLedger(int ledgerId)
        {
            var balance = UtilityService.GetOpeningBalance("OB", ledgerId, FiscalYear.Id);
            return Json(balance, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ProductOpening

        public ActionResult ProductOpening()
        {
            return View();
        }


        public ActionResult ProductOpeningAdd()
        {
            var model = new ProductOpening();
            model.UnitList = new SelectList(_unit.GetAll(), "Id", "Code");
            model.GodownList = new SelectList(_godown.GetAll(), "Id", "Name").ToList();
            model.GodownList.Insert(0, new SelectListItem { Text = "None", Value = "0" });
            model.OpeningDate = FiscalYear.StartDate.AddDays(-1);
            model.ProductOpenings = _productopeningRepository.GetAll();
            foreach (var item in model.ProductOpenings)
            {
                item.UnitId = _unit.GetById(x => x.Code.ToLower() == item.Unit.ToLower()).Id;
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ProductOpeningAdd(IEnumerable<ProductOpening> productOpening)
        {
            int sno = 1;
            var productopeninglist = _productopeningRepository.GetAll().Select(x => x.Id);
            foreach (var id in productopeninglist)
            {
                _productopeningRepository.Delete(x => x.Id == id);
                _stockTransactionRepository.Delete(x => x.Source.ToLower() == "pob" && x.ReferenceId == id);
            }
            foreach (var opening in productOpening.Where(x => x.Amount != 0 && x.ProductId != 0))
            {
                var fiscalYear = FiscalYear.StartDate.AddDays(-1);
                var miti = NepaliDateService.NepaliDate(fiscalYear);
                var unitcode = _unit.GetById(opening.UnitId).Code;
                var productopening = new ProductOpening()
                                         {
                                             ProductId = opening.ProductId,
                                             OpeningDate = FiscalYear.StartDate.AddDays(-1),
                                             OpeningMiti = miti.Date,
                                             Unit = unitcode,
                                             Quantity = opening.Quantity,
                                             AltUnit = opening.AltUnit,
                                             AltQuantity = opening.AltQuantity,
                                             Rate = opening.Rate,
                                             Amount = opening.Amount,
                                             GodownId = opening.GodownId
                                         };
                _productopeningRepository.Add(productopening);

                var stockTransaction = new StockTransaction();
                stockTransaction.AltQty = opening.AltQuantity;
                stockTransaction.AltUnit = opening.AltUnit;
                stockTransaction.NetAmt = opening.Amount;
                stockTransaction.ProductCode = opening.ProductId;
                stockTransaction.Quantity = opening.Quantity;
                stockTransaction.Unit = unitcode;
                stockTransaction.Rate = Convert.ToDecimal(opening.Rate);
                stockTransaction.SNo = sno;
                stockTransaction.Source = "POB";
                stockTransaction.ReferenceId = productopening.Id;
                stockTransaction.Godown = Convert.ToString(opening.GodownId);
                stockTransaction.FYId = FiscalYear.Id;
                var stock =
                    _stockTransactionRepository.Filter(x => x.ProductCode == opening.ProductId).ToList().LastOrDefault();
                if (stock == null)
                {
                    stockTransaction.StockVal = Convert.ToDecimal(opening.Amount);
                    stockTransaction.StockQty = opening.Quantity;

                }
                else
                {
                    stockTransaction.StockVal = stock.StockVal + Convert.ToDecimal(opening.Amount);
                    ;
                    stockTransaction.StockQty = stock.StockQty + opening.Quantity;
                }
                stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();
                stockTransaction.Unit = unitcode;
                stockTransaction.VDate = FiscalYear.StartDate.AddDays(-1);
                var openingMiti = FiscalYear.StartDate.AddDays(-1);
                var mitiDate = Convert.ToDateTime(openingMiti);
                stockTransaction.VMiti = Convert.ToString(NepaliDateService.NepaliDate(mitiDate).Date);
                stockTransaction.VNo = Convert.ToString(productopening.Id);
                stockTransaction.BranchId = CurrentBranchId;
                _stockTransactionRepository.Add(stockTransaction);
                sno++;
            }

            return Content("True");
        }

        public ActionResult PartialProductOpening()
        {
            var model = new ProductOpening();
            model.UnitList = new SelectList(_unit.GetAll(), "Id", "Code");
            model.OpeningDate = this.FiscalYear.StartDate.AddDays(-1);
            model.GodownList = new SelectList(_godown.GetAll(), "Id", "Name").ToList();
            model.GodownList.Insert(0, new SelectListItem { Text = "None", Value = "0" });
            return PartialView(model);
        }

        #endregion

        #region Online Data Entry

        public ActionResult OnlineGeneralLedgerAdd()
        {
            var viewModel = base.CreateViewModel<LedgerAddViewModel>();
            viewModel.Category =
                new SelectList(
                    Enum.GetValues(typeof(LedgerCategoryEnum)).Cast
                        <LedgerCategoryEnum>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text");
            var group = _accountGroup.GetAll().ToList();
            viewModel.Group = new SelectList(group, "Id", "Description");
            var subGroupList = new List<AccountSubGroup>();
            if (viewModel.Group.Count() != 0)
            {
                var groupId = group.FirstOrDefault().Id;
                subGroupList = _accountSubGroup.Filter(x => x.AccountGroupId == groupId).ToList();
                viewModel.SubGroup = new SelectList(subGroupList, "Id", "Description");
            }
            else
            {
                viewModel.SubGroup = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            viewModel.Area = new SelectList(_area.GetAll(), "Id", "AreaName");
            viewModel.Agent = new SelectList(_agent.GetAll(), "Id", "Name");
            viewModel.Currency = new SelectList(_currency.GetAll(), "Id", "Code");
            viewModel.UdfEntries =
                _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.LedgerMaster).ToList();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult OnlineGeneralLedgerAdd(LedgerAddViewModel model, FormCollection formCollection)
        {
            if (_ledger.IsAccountNameAvailable(model.Ledger.AccountName))
            {
                if (_ledger.IsShortNameAvailable(model.Ledger.ShortName))
                {
                    var userId = _authentication.GetAuthenticatedUser().Id;
                    model.Ledger.CreatedDate = DateTime.UtcNow;
                    model.Ledger.UpdatedDate = DateTime.UtcNow;
                    model.Ledger.CreatedById = userId;
                    model.Ledger.UpdatedById = userId;
                    model.Ledger.BranchId = base.CurrentBranch.Id;
                    _ledger.Add(model.Ledger);

                    var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.LedgerMaster);
                    if (data != null)
                    {
                        foreach (var udfEntry in data)
                        {
                            var value = formCollection[udfEntry.FieldName];
                            var i = new UDFData()
                                        {
                                            ReferenceId = model.Ledger.Id,
                                            UdfId = udfEntry.Id,
                                            Value = value,
                                            SourceId = (int)EntryModuleEnum.LedgerMaster
                                        };

                            _udfData.Add(i);
                        }
                    }
                }
                else
                {
                    return Content("Short Name already exists. Please enter a different Short Name.");
                }
            }
            else
            {
                return Json(new { msg = "false" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { msg = "true", id = model.Ledger.Id, title = model.Ledger.AccountName },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnlineAccountGroupAdd()
        {
            var viewModel = base.CreateViewModel<AccountGroupViewModel>();
            viewModel.Type = new SelectList(
                Enum.GetValues(typeof(AccountGroupTypeEnum)).Cast
                    <AccountGroupTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");

            viewModel.SubType = new SelectList(
                Enum.GetValues(typeof(BalanceSheetTypeEnum)).Cast
                    <BalanceSheetTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }), "Value",
                "Text");
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult OnlineAccountGroupAdd(AccountGroupViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (model.AccountGroup.Description != null)
            {
                if (_accountGroup.IsAccountGroupAvailable(model.AccountGroup.Description))
                {
                    model.AccountGroup.UpdatedById = userId;
                    model.AccountGroup.CreatedById = userId;
                    model.AccountGroup.CreatedDate = DateTime.UtcNow;
                    model.AccountGroup.UpdatedDate = DateTime.UtcNow;
                    model.AccountGroup.Code = model.AccountGroup.Description;
                    _accountGroup.Add(model.AccountGroup);

                }
                else
                {
                    return Json(new { msg = "false" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { msg = "false" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { msg = "true", id = model.AccountGroup.Id, title = model.AccountGroup.Description },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnlineCashBankLedgerAdd()
        {
            var viewModel = base.CreateViewModel<LedgerAddViewModel>();
            string category = LedgerCategoryEnum.OT.ToString();
            viewModel.Category =
                new SelectList(
                    Enum.GetValues(typeof(LedgerCategoryEnum)).Cast
                        <LedgerCategoryEnum>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text", category);
            var group = _accountGroup.GetAll().ToList();
            viewModel.Group = new SelectList(group, "Id", "Description");
            var subGroupList = new List<AccountSubGroup>();
            if (viewModel.Group.Count() != 0)
            {
                var groupId = group.FirstOrDefault().Id;
                subGroupList = _accountSubGroup.Filter(x => x.AccountGroupId == groupId).ToList();
                viewModel.SubGroup = new SelectList(subGroupList, "Id", "Description");
            }
            else
            {
                viewModel.SubGroup = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            viewModel.Area = new SelectList(_area.GetAll(), "Id", "AreaName");
            viewModel.Agent = new SelectList(_agent.GetAll(), "Id", "Name");
            viewModel.Currency = new SelectList(_currency.GetAll(), "Id", "Code");
            viewModel.UdfEntries =
                _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.LedgerMaster).ToList();
            return PartialView(viewModel);
        }

        public ActionResult OnlineAccountSubGroupAdd()
        {
            var viewModel = new AccountSubGroupViewModel();
            viewModel.GroupList = new SelectList(_accountGroup.GetAll(), "Id", "Description");
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult OnlineAccountSubGroupAdd(AccountSubGroupViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_accountSubGroup.IsAccountSubGroupAvailable(model.AccountSubGroup.Description))
            {


                model.AccountSubGroup.CreatedDate = DateTime.UtcNow;
                model.AccountSubGroup.UpdatedDate = DateTime.UtcNow;
                model.AccountSubGroup.CreatedById = userId;
                model.AccountSubGroup.UpdatedById = userId;
                _accountSubGroup.Add(model.AccountSubGroup);
            }
            else
            {
                return Json(new { msg = "false" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { msg = "true", id = model.AccountSubGroup.Id, title = model.AccountSubGroup.Description },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnlineProductGroupAdd()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult OnlineProductGroupAdd(ProductGroup model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_productGroup.IsProductGroupNameAvailable(model.Description))
            {
                if (_productGroup.IsProductGroupShortNameAvailable(model.ShortName))
                {
                    model.CreatedDate = DateTime.UtcNow;
                    model.UpdatedDate = DateTime.UtcNow;
                    model.CreatedById = userId;
                    model.UpdatedById = userId;
                    _productGroup.Add(model);
                }
                else
                {
                    return
                        Content(
                            "Produt Group Short Name already exists. Please enter a different Produt Group Short Name.");
                }

            }
            else
            {
                return Json(new { msg = "false" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { msg = "true", id = model.Id, title = model.Description }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnlineProductSubGroupAdd()
        {
            var viewModel = new ProductSubGroupAddViewModel();
            viewModel.ProductGroupList = new SelectList(_productGroup.GetAll(), "Id", "Description");
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult OnlineProductSubGroupAdd(ProductSubGroupAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_productSubGroup.IsProductSubGroupNameAvailable(model.ProductSubGroup.Description))
            {


                model.ProductSubGroup.CreatedDate = DateTime.UtcNow;
                model.ProductSubGroup.UpdatedDate = DateTime.UtcNow;
                model.ProductSubGroup.CreatedById = userId;
                model.ProductSubGroup.UpdatedById = userId;
                _productSubGroup.Add(model.ProductSubGroup);
            }
            else
            {
                return Json(new { msg = "false" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { msg = "true", id = model.ProductSubGroup.Id, title = model.ProductSubGroup.Description },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnlineProductAdd()
        {
            var viewModel = new ProductAddViewModel();
            viewModel.Product = new Product();
            viewModel.Group = new SelectList(_productGroup.GetAll(), "Id", "Description");
            viewModel.SubGroup = new SelectList(_productSubGroup.GetAll(), "Id", "Description");
            var test = StringEnum.GetStringValue(ProductTypeEnum.Inventory);
            viewModel.Type = new SelectList(
                Enum.GetValues(typeof(ProductTypeEnum)).Cast
                    <ProductTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.ValTechList = new SelectList(
                Enum.GetValues(typeof(ValTechEnum)).Cast
                    <ValTechEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.UnitList = new SelectList(_unit.GetAll(), "Id", "Code");
            viewModel.BonusType = new SelectList(
                Enum.GetValues(typeof(BonusTypeEnum)).Cast
                    <BonusTypeEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.Vat = new SelectList(
                Enum.GetValues(typeof(YesNoEnum)).Cast
                    <YesNoEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult OnlineProductAdd(ProductAddViewModel model)
        {
            var userId = _authentication.GetAuthenticatedUser().Id;
            if (_product.IsProductItemNameAvailable(model.Product.Name))
            {
                if (_product.IsProductShortNameAvailable(model.Product.ShortName))
                {
                    model.Product.CreatedDate = DateTime.UtcNow;
                    model.Product.UpdatedDate = DateTime.UtcNow;
                    model.Product.CreatedById = userId;
                    model.Product.UpdatedById = userId;
                    _product.Add(model.Product);
                }
                else
                {
                    return Content("Product Short Name already exists. Please enter a different Short Name.");
                }
            }
            else
            {
                return Json(new { msg = "false" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { msg = "true", id = model.Product.Id, title = model.Product.Name },
                        JsonRequestBehavior.AllowGet);
        }

        #endregion


        public ActionResult GetAgent()
        {
            var agents = _agent.Filter(a => a.IsActive && !a.IsDeleted).Select(x => new
                                                                                        {
                                                                                            Id = x.Id,
                                                                                            Description = x.Name
                                                                                        }).OrderBy(x => x.Description);
            return Json(agents, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModuleList()
        {
            var agents = _moduleList.GetAll().Select(x => new
                                                              {
                                                                  Id = x.Id,
                                                                  Description = x.Name
                                                              });
            return Json(agents, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUnitList()
        {
            var agents = _unit.GetMany(x => !x.IsDeleted).Select(x => new
                                                                          {
                                                                              Id = x.Id,
                                                                              Description = x.Code
                                                                          });
            return Json(agents, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetShortName(string name, string module)
        {
            var shortName = string.Empty;
            GenerateShortName(name, module, ref shortName, null);
            return Json(new { shortName = shortName }, JsonRequestBehavior.AllowGet);
        }

        public void GenerateShortName(string name, string module, ref string shortName, int? no)
        {
            var newNo = 0;
            string number = string.Empty;
            var prefix = string.Empty;
            if (name.Count() > 1)
                prefix = name.Substring(0, 2).ToUpper();
            else
            {
                prefix = name.ToUpper();
            }
            if (no != null)
                newNo = Convert.ToInt32(no);
            else
            {
                var shortNameDetail =
                    _shortName.GetMany(x => x.Prefix == prefix && x.Module == module && x.Number != null);
                if (shortNameDetail.Count() != 0)
                {
                    newNo = Convert.ToInt32(shortNameDetail.Max(x => x.Number) + 1);
                    //number = newNo.ToString().PadLeft(6, '0');
                }
                else
                {
                    newNo = 1;
                    //number = "1".PadLeft(6, '0');
                }

            }
            number = newNo.ToString().PadLeft(6, '0');
            shortName = prefix + number;
            var newShortName = shortName;
            var checkShortName = _shortName.GetById(x => x.ShortName == newShortName && x.Module == module);
            if (checkShortName != null)
            {
                newNo++;
                GenerateShortName(name, module, ref shortName, newNo);
            }
            else
            {
                var tempShortNameDetail = new ShortNameDetail()
                                              {
                                                  Module = module,
                                                  Number = newNo,
                                                  Prefix = prefix,
                                                  ShortName = newShortName
                                              };
                var tempDataName = "ShortName" + module;
                TempData[tempDataName] = tempShortNameDetail;
            }

        }

        public ActionResult GetProductList()
        {
            var list = _product.GetMany(x => !x.IsDeleted).Select(x => new { x.Id, x.Name, x.ShortName });
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        #region Schemes

        public ActionResult Scheme()
        {
            return View();
        }

        public ActionResult SchemeList()
        {
            var list = _schemeRepository.GetAll();
            return PartialView(list);
        }

        public ActionResult SchemeAdd()
        {
            var viewModel = new SchemeViewModel();
            viewModel.Scheme = new Scheme();
            viewModel.SchemeProduct = new SchemeProduct();
            viewModel.SchemeFreeProduct = new SchemeFreeProduct();
            viewModel.AmountType = new SelectList(
                Enum.GetValues(typeof(JVSalesTypeEnum)).Cast
                    <JVSalesTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.SchemeProduct.Unitlist = new SelectList(_unit.GetAll(), "Id", "Code");
            viewModel.SchemeProduct.FreeUnitlist = new SelectList(_unit.GetAll(), "Id", "Code");
            viewModel.ParentGuid = Guid.NewGuid().ToString();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SchemeAdd(SchemeViewModel model, IEnumerable<SchemeProduct> schemeProducts,
                                      IEnumerable<SchemeFreeProduct> schemeFreeProducts)
        {
            if (ModelState.IsValid)
            {
                var scheme = new Scheme()
                                 {
                                     Name = model.Scheme.Name,
                                     FromDate = model.Scheme.FromDate,
                                     ToDate = model.Scheme.ToDate,
                                     EffectOn = model.Scheme.EffectOn,
                                     CreatedOn = DateTime.UtcNow,
                                     CreatedBy = _authentication.GetAuthenticatedUser().Id,
                                     IsActive = true
                                 };
                _schemeRepository.Add(scheme);

                foreach (var item in schemeProducts.Where(x => x.ProductId != 0))
                {
                    var unitId = Convert.ToInt32(item.Unit);
                    var unitcode = _unit.GetById(unitId).Code;
                    var freeunitCode = _unit.GetById(Convert.ToInt32(item.FreeUnit)).Code;
                    var products = new SchemeProduct()
                                       {
                                           ProductId = item.ProductId,
                                           Qty = item.Qty,
                                           Unit = unitcode,
                                           FreeQty = item.FreeQty,
                                           FreeUnit = freeunitCode,
                                           ProductSchemeId = scheme.Id
                                       };
                    _schemeproductRepository.Add(products);

                    foreach (
                        var freeitem in schemeFreeProducts.Where(x => x.ParentGuid == item.Guid && x.ProductId != 0))
                    {
                        var unitcodeFree = _unit.GetById(Convert.ToInt32(freeitem.FreeUnit)).Code;
                        var freeItem = new SchemeFreeProduct()
                                           {
                                               ProductId = freeitem.ProductId,
                                               SchemeProductId = products.Id,
                                               FreeQty = freeitem.FreeQty,
                                               FreeUnit = unitcodeFree

                                           };
                        _schemefreeproductRepository.Add(freeItem);
                    }
                }
                return Content("True");
            }
            return Content("False");
        }

        public ActionResult SchemeEdit(int id)
        {
            var viewModel = new SchemeViewModel();
            viewModel.Scheme = _schemeRepository.GetById(id);
            viewModel.SchemeProduct = new SchemeProduct();

            viewModel.SchemeProducts = _schemeproductRepository.GetMany(x => x.ProductSchemeId == viewModel.Scheme.Id);

            foreach (var item in viewModel.SchemeProducts)
            {
                item.UnitId = _unit.GetById(x => x.Code.ToLower() == item.Unit.ToLower()).Id;
                item.FreeUnitId = _unit.GetById(x => x.Code.ToLower() == item.FreeUnit.ToLower()).Id;
                item.Guid = Guid.NewGuid().ToString();
                item.SchemeFreeProducts = _schemefreeproductRepository.GetMany(x => x.SchemeProductId == item.Id);
                foreach (var data2 in item.SchemeFreeProducts)
                {
                    data2.UnitId = _unit.GetById(x => x.Code.ToLower() == data2.FreeUnit.ToLower()).Id;
                    data2.ParentGuid = item.Guid;
                }
            }
            viewModel.AmountType = new SelectList(
                Enum.GetValues(typeof(JVSalesTypeEnum)).Cast
                    <JVSalesTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.SchemeProduct.Unitlist = new SelectList(_unit.GetAll(), "Id", "Code");
            viewModel.ParentGuid = Guid.NewGuid().ToString();
            return PartialView(viewModel);
        }


        [HttpPost]
        public ActionResult SchemeEdit(SchemeViewModel model, IEnumerable<SchemeProduct> schemeProducts,
                                       IEnumerable<SchemeFreeProduct> schemeFreeProducts)
        {
            if (ModelState.IsValid)
            {
                _schemeRepository.Update(model.Scheme);
                _schemefreeproductRepository.Delete(x => x.SchemeProduct.ProductSchemeId == model.Scheme.Id);
                _schemeproductRepository.Delete(x => x.ProductSchemeId == model.Scheme.Id);

                foreach (var item in schemeProducts.Where(x => x.ProductId != 0))
                {
                    var unitId = Convert.ToInt32(item.Unit);
                    var unitcode = _unit.GetById(unitId).Code;
                    var freeunitCode = _unit.GetById(Convert.ToInt32(item.FreeUnit)).Code;
                    var products = new SchemeProduct()
                                       {
                                           ProductId = item.ProductId,
                                           Qty = item.Qty,
                                           Unit = unitcode,
                                           FreeQty = item.FreeQty,
                                           FreeUnit = freeunitCode,
                                           ProductSchemeId = model.Scheme.Id
                                       };
                    _schemeproductRepository.Add(products);

                    foreach (
                        var freeitem in schemeFreeProducts.Where(x => x.ParentGuid == item.Guid && x.ProductId != 0))
                    {
                        var unitcodeFree = _unit.GetById(Convert.ToInt32(freeitem.FreeUnit)).Code;
                        var freeItem = new SchemeFreeProduct()
                                           {
                                               ProductId = freeitem.ProductId,
                                               SchemeProductId = products.Id,
                                               FreeQty = freeitem.FreeQty,
                                               FreeUnit = unitcodeFree

                                           };
                        _schemefreeproductRepository.Add(freeItem);
                    }
                }
                return Content("True");

            }
            return Content("False");
        }

        public ActionResult SchemeProduct()
        {
            var model = new SchemeProduct();
            model.Unitlist = new SelectList(_unit.GetAll(), "Id", "Code");
            model.FreeUnitlist = new SelectList(_unit.GetAll(), "Id", "Code");
            model.Guid = Guid.NewGuid().ToString();
            return PartialView("_PartialSchemeProduct", model);
        }

        public ActionResult SchemeFreeProduct(string guid)
        {
            var model = new SchemeFreeProduct();
            model.Unitlist = new SelectList(_unit.GetAll(), "Id", "Code");
            model.ParentGuid = guid;
            return PartialView("_PartialFreeProduct", model);
        }

        public ActionResult SchemeFreeProductsNew(string guid)
        {
            var model = new SchemeFreeProduct();
            model.Unitlist = new SelectList(_unit.GetAll(), "Id", "Code");
            model.ParentGuid = guid;
            return PartialView(model);
        }

        #endregion

        public string BarcodePrint(string code, HttpContext context)
        {
            if (string.IsNullOrEmpty(code))
            {
                return string.Empty;
            }
            int w = code.Length * 40;

            // Create a bitmap object of the width that we calculated and height of 100

            var oBitmap = new Bitmap(w, 100);

            // then create a Graphic object for the bitmap we just created.
            Graphics oGraphics = Graphics.FromImage(oBitmap);

            // Now create a Font object for the Barcode Font
            // (in this case the IDAutomationHC39M) of 18 point size
            Font oFont = new Font("IDAutomationHC39M", 18);

            // Let's create the Point and Brushes for the barcode
            PointF oPoint = new PointF(2f, 2f);
            SolidBrush oBrushWrite = new SolidBrush(Color.Black);
            SolidBrush oBrush = new SolidBrush(Color.White);

            // Now lets create the actual barcode image
            // with a rectangle filled with white color
            oGraphics.FillRectangle(oBrush, 0, 0, w, 100);

            // We have to put prefix and sufix of an asterisk (*),
            // in order to be a valid barcode
            oGraphics.DrawString("*" + code + "*", oFont, oBrushWrite, oPoint);

            // Then we send the Graphics with the actual barcode
            //  Response.ContentType = "image/jpeg";


            //  oBitmap.Save(Response.OutputStream, ImageFormat.Jpeg);

            // oBitmap.Save("barcode", ImageFormat.Jpeg,);

            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                oBitmap.Save(stream, ImageFormat.Jpeg);
                stream.Close();

                byteArray = stream.ToArray();
            }
            
            return "data:image/Jpeg;base64," + Convert.ToBase64String(byteArray);
            

        }

        #region opening Student


        public ActionResult OpeningStudents()
        {
            return View();
        }

        public ActionResult OpeningStudentList(int pageNo = 1)
        {
            var list = _openingstudentRepository.GetAll().OrderByDescending(a => a.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("OpeningStudentList", list.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult OpeningStudentAdd()
        {

            var viewModel = base.CreateViewModel<OpeningStudentViewModel>();
            viewModel.OpeningStudentCategory =
                new SelectList(
                    Enum.GetValues(typeof(StudentOpeningCategory)).Cast
                        <StudentOpeningCategory>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                    "Value", "Text");

            viewModel.AmountType = new SelectList(
                Enum.GetValues(typeof(JVAmountTypeEnum)).Cast
                    <JVAmountTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.OpeningStudentDue =
                _openingstudentRepository.GetMany(x => x.CategoryId == (int)StudentOpeningCategory.Due);
            viewModel.OpeningStudentDeposit =
                _openingstudentRepository.GetMany(x => x.CategoryId == (int)StudentOpeningCategory.SecurityDeposit);

            return PartialView(viewModel);
        }

        public ActionResult PartialOpeningDueAdd()
        {
            var model = new OpeningStudent();
            model.AmountTypeList = new SelectList(
            Enum.GetValues(typeof(JVAmountTypeEnum)).Cast
            <JVAmountTypeEnum>().Select(
            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }), "Value", "Text");
            return PartialView(model);
        }
        public ActionResult PartialOpeningDepositAdd()
        {
            var model = new OpeningStudent();
            model.AmountTypeList = new SelectList(
            Enum.GetValues(typeof(JVAmountTypeEnum)).Cast
            <JVAmountTypeEnum>().Select(
            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }), "Value", "Text");
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult OpeningStudentAddDue(OpeningStudentViewModel model, IEnumerable<OpeningStudent> openingdue)
        {
            var typeid = (int)StudentOpeningCategory.Due;
            var openingledgers = _openingstudentRepository.GetMany(x => x.CategoryId == typeid).Select(x => x.Id);
            _openingstudentRepository.Delete(x => x.CategoryId == typeid);
            foreach (var i in openingledgers)
            {
                _accountTransaction.Delete(x => x.ReferenceId == i && x.Source.ToLower() == "SDOB");
            }
            foreach (var ledger in openingdue.Where(x => x.StudentId != 0))
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                int sno = 1;
                if (ledger.Amount != 0)
                {
                    var Opening = new OpeningStudent()
                    {
                        AmountType = ledger.AmountType,
                        CreatedById = userId,
                        LedgerId = SystemControl.StudentFeeAc,
                        CreatedDate = DateTime.UtcNow,
                        Amount = ledger.Amount,
                        FiscalYearId = FiscalYear.Id,
                        BranchId = this.CompanyInfo.Id,
                        AcademyId = this.AcademicYear.Id,
                        CategoryId = (int)Enums.StudentOpeningCategory.Due,
                        StudentId = ledger.StudentId,

                    };
                    _openingstudentRepository.Add(Opening);
                    var branchId = this.CurrentCompanyId;
                    if (this.SystemControl.EnableBranch)
                    {
                        branchId = this.CurrentBranchId;
                    }
                    var transaction = new ScBillTransaction()
                    {
                        AcademicYearId = this.AcademicYear.Id,
                        BillAmount = ledger.Amount,
                        BMiti = NepaliDateService.NepaliDate(DateTime.Now).Date,
                        BillNo = "0",
                        BranchId = branchId,
                        ReceiptAmount = 0,
                        StudentId = ledger.StudentId,
                        ReferenceId = Opening.Id,
                        FYId = this.FiscalYear.Id,
                        Source = "SDOB",
                        Date = DateTime.Now,


                    };

                    _billTransactionRepository.Add(transaction);
                    var accountTransactionMaster = new AccountTransaction();

                    if (Opening.AmountType == (int)JVAmountTypeEnum.D)
                    {
                        accountTransactionMaster.DrAmt = Opening.Amount;
                        accountTransactionMaster.CrAmt = 0;
                    }
                    else
                    {
                        accountTransactionMaster.DrAmt = 0;
                        accountTransactionMaster.CrAmt = Opening.Amount;
                    }
                    accountTransactionMaster.GlCode = SystemControl.StudentFeeAc;
                    accountTransactionMaster.ReferenceId = Opening.Id;
                    accountTransactionMaster.VNo = "";
                    accountTransactionMaster.VDate = Opening.CreatedDate;
                    accountTransactionMaster.CrRate = 1;
                    accountTransactionMaster.SNo = sno;
                    accountTransactionMaster.CbCode = Opening.Id;
                    accountTransactionMaster.Source = "SDOB";
                    accountTransactionMaster.CreatedById = userId;
                    accountTransactionMaster.FYId = this.FiscalYear.Id;
                    accountTransactionMaster.BranchId = this.CompanyInfo.Id;
                    accountTransactionMaster.SlCode = 0;

                    _accountTransaction.Add(accountTransactionMaster);
                }
            }
            return Content("True");

        }

        [HttpPost]
        public ActionResult OpeningStudentAddDeposit(OpeningStudentViewModel model, IEnumerable<OpeningStudent> openingDeposit)
        {
            var typeid = (int)StudentOpeningCategory.SecurityDeposit;
            var openingledgers = _openingstudentRepository.GetMany(x => x.CategoryId == typeid).Select(x => x.Id);
            _openingstudentRepository.Delete(x => x.CategoryId == typeid);
            foreach (var i in openingledgers)
            {
                _accountTransaction.Delete(x => x.ReferenceId == i && x.Source.ToLower() == "SSDOB");
            }
            foreach (var ledger in openingDeposit.Where(x => x.StudentId != 0))
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                int sno = 1;
                if (ledger.Amount != 0)
                {
                    var Opening = new OpeningStudent()
                    {
                        AmountType = ledger.AmountType,
                        CreatedById = userId,
                        LedgerId = SystemControl.DepositAc,
                        CreatedDate = DateTime.UtcNow,
                        Amount = ledger.Amount,
                        FiscalYearId = FiscalYear.Id,
                        BranchId = this.CompanyInfo.Id,
                        AcademyId = this.AcademicYear.Id,
                        CategoryId = (int)Enums.StudentOpeningCategory.SecurityDeposit,
                        StudentId = ledger.StudentId,

                    };

                    _openingstudentRepository.Add(Opening);
                    var accountTransactionMaster = new AccountTransaction();

                    if (Opening.AmountType == (int)JVAmountTypeEnum.D)
                    {
                        accountTransactionMaster.DrAmt = Opening.Amount;
                        accountTransactionMaster.CrAmt = 0;
                    }
                    else
                    {
                        accountTransactionMaster.DrAmt = 0;
                        accountTransactionMaster.CrAmt = Opening.Amount;
                    }
                    accountTransactionMaster.GlCode = SystemControl.DepositAc;
                    accountTransactionMaster.ReferenceId = Opening.Id;
                    accountTransactionMaster.VNo = "";
                    accountTransactionMaster.VDate = Opening.CreatedDate;
                    accountTransactionMaster.CrRate = 1;
                    accountTransactionMaster.SNo = sno;
                    accountTransactionMaster.CbCode = Opening.Id;
                    accountTransactionMaster.Source = "SSDOB";
                    accountTransactionMaster.CreatedById = userId;
                    accountTransactionMaster.FYId = this.FiscalYear.Id;
                    accountTransactionMaster.BranchId = this.CompanyInfo.Id;
                    accountTransactionMaster.SlCode = 0;

                    _accountTransaction.Add(accountTransactionMaster);
                }
            }
            return Content("True");

        }
        #endregion
    }
}
