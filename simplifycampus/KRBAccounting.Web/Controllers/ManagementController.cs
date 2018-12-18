using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.ViewModels.Entry;
using KRBAccounting.Web.ViewModels.Management;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.Controllers
{
    [Authorize]
    public class ManagementController : BaseController
    {
        //
        // GET: /Management/
        private readonly ICompanyInfoRepository _company;
        private readonly IFormsAuthenticationService _authentication;
        //private readonly ITellerMasterRepository _tellerMasterRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILedgerRepository _ledgerRepository;
        private readonly ICashBankMasterRepository _cashBankMaster;
        private readonly IDocumentNumeringSchemeRepository _numeringSchemeRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly ICashBankDetailRepository _cashBankDetailRepository;
        private readonly ISystemControlRepository _systemControlRepository;
        private readonly IMenuItemRepository _menuitemRepository;
        private readonly DataContext _context = new DataContext();
        private readonly FileHelper _filehelp = new FileHelper();
        private readonly IScClassRepository _classRepository;
        private readonly IScRollNumberingSchemeRepository _rollNumberingSchemeRepository;
        public readonly IScSectionRepository _sectionRepository;
        public int CurrentBranchId;
        public ManagementController(ICompanyInfoRepository companyInfoRepository,
            IUserRepository userRepository,
            ILedgerRepository ledgerRepository,
            ICashBankMasterRepository cashBankMaster,
            IDocumentNumeringSchemeRepository numeringSchemeRepository,
            IAccountTransactionRepository accountTransactionRepository,
            ICashBankDetailRepository cashBankDetailRepository,
             ISystemControlRepository systemControlRepository,
            IMenuItemRepository menuitemRepository,
            IScRollNumberingSchemeRepository rollNumberingSchemeRepository,
            IScSectionRepository sectionRepository,
            IScClassRepository classRepository)
        {
            _company = companyInfoRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Setting);
            CurrentBranchId = CurrentBranch.Id;
            _userRepository = userRepository;
            _ledgerRepository = ledgerRepository;
            _cashBankMaster = cashBankMaster;
            _numeringSchemeRepository = numeringSchemeRepository;
            _accountTransactionRepository = accountTransactionRepository;
            _cashBankDetailRepository = cashBankDetailRepository;
            _systemControlRepository = systemControlRepository;
            _menuitemRepository = menuitemRepository;
            _rollNumberingSchemeRepository = rollNumberingSchemeRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
        }
          [CheckPermission(Permissions = "Navigate", Module = "MgmtCom")]
        public ActionResult Company()
        {
            var viewModel = new CompanyViewModel();
            viewModel.Company = _company.GetAll().FirstOrDefault();
            viewModel.Branches = _company.Filter(x => x.ParentId != 0);
            var sysCtrl = _systemControlRepository.GetAll().FirstOrDefault();
            if (sysCtrl != null)
            {
                // For DateType
                viewModel.SystemControl = sysCtrl;
            }
            viewModel.Company.StartDate = DateTime.Now;
            viewModel.Company.EndDate = DateTime.Now.AddYears(1);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Company(CompanyViewModel model)
        {
            if (model.Company.StartMiti == null)
            {
                model.Company.StartMiti = model.DisplayStartDate;
            }
            if (model.Company.EndMiti == null)
            {
                model.Company.EndMiti = model.DisplayEndDate;
            }
            model.Company.UpdatedDate = DateTime.UtcNow;

            _company.Update(model.Company);
            return RedirectToAction("Company");
        }

        [HttpPost]
        public ActionResult UploadImages(IEnumerable<HttpPostedFileBase> attachments)
        {
            var logoGuid = string.Empty;
            var fileName = string.Empty;
            var fileext = string.Empty;
            var filenamewithoutext = string.Empty;
            foreach (var file in attachments)
            {
                fileext = Path.GetExtension(file.FileName);
                filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                if (fileext != ".jpg" && fileext != ".png" && fileext != ".jpeg") continue;
                logoGuid = Guid.NewGuid().ToString();
                var coverfilename = logoGuid + fileext;
                var path = AppDomain.CurrentDomain.BaseDirectory + "Content\\Logo\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, coverfilename);
                file.SaveAs(path);
                var thumbpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\Logo\\";

                _filehelp.ConvertToThumbnail(file, logoGuid + "_th", fileext, thumbpath);
                fileName = file.FileName;


            }
            var res = Json(new { guid = logoGuid, filename = fileName, ext = fileext, filenamewithoutext = filenamewithoutext });

            return res;
        }

        public ActionResult Branch(int id)
        {
            var viewModel = new BranchViewModel();
            var branch = _company.GetById(x => x.Id == id);
            viewModel.Branch = branch;
            var sysCtrl = _systemControlRepository.GetAll().FirstOrDefault();
            if (sysCtrl != null)
            {
                viewModel.SystemControl = sysCtrl;
            }
            return View(viewModel);
        }

        public ActionResult BranchAdd()
        {
            var viewModel = new CompanyInfo();
            var sysCtrl = _systemControlRepository.GetAll().FirstOrDefault();
            if (sysCtrl != null)
            {
                viewModel.SystemControl = sysCtrl;
            }
            viewModel.StartDate = DateTime.Now;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult BranchAdd(CompanyInfo model)
        {
            model.ParentId = base.CompanyInfo.Id;
            model.CreatedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;
            model.StartDate = model.StartDate;
            if (model.StartMiti == null)
            {
                model.StartMiti = model.DisplayDate;
            }
            model.EndDate = base.CompanyInfo.EndDate;
            model.EndMiti = base.CompanyInfo.EndMiti;
            var user = _authentication.GetAuthenticatedUser();
           
            _company.Add(model);
            return Content("true");
        }

        [HttpPost]
        public ActionResult BranchEdit(BranchViewModel model)
        {
            model.Branch.UpdatedDate = DateTime.Now;
            //var user = _authentication.GetAuthenticatedUser();
            //model.Branch.UpdatedById = user.Id;
            _company.Update(model.Branch);
            return RedirectToAction("Company");
        }



        public ActionResult MenuSetting()
        {
            var menulist = _menuitemRepository.GetMany(x => x.ParentId == 0 || x.ParentId == null).OrderBy(x => x.Schedule);
            return View(menulist);
        }

        public ActionResult MenuItemList()
        {
            var menulist = _menuitemRepository.GetMany(x => x.ParentId == 0 || x.ParentId == null).OrderBy(x => x.Schedule);
            return PartialView(menulist);
        }
        public ActionResult ChildMenuItemList(int id)
        {
            var menulist = _menuitemRepository.GetMany(x => x.ParentId == id).OrderBy(x => x.Schedule);
            return PartialView("_PartialChildMenuItemList", menulist);
        }
        public ActionResult MenuItemAdd()
        {
            return PartialView();
        }
        public ActionResult MenuItemEdit(int id)
        {
            var data = _menuitemRepository.GetById(x => x.Id == id);

            return PartialView(data);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MenuItemEdit(MenuItem model)
        {

            _menuitemRepository.Update(model);
            return null;
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MenuItemAdd(MenuItem model)
        {

            _menuitemRepository.Add(model);
            return null;
        }

        public ActionResult GetParentMenuList()
        {
            var acccountGroups = _menuitemRepository.GetMany(x => x.ParentId == 0 || x.ParentId == null).OrderBy(x => x.Schedule).Select(x => new
            {
                Id = x.Id,
                Description = x.LinkText
            });
            return Json(acccountGroups, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ABup")]
        public ActionResult ManualBackUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManualBackUp(string savepath)
        {
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
                var builder = new SqlConnectionStringBuilder(conStr);
                string database = builder.InitialCatalog;

                var path = "exec ExeAutoBackUp @path1='" + savepath + "',@databasename1='" + database + "'";
                var data = _context.Database.SqlQuery<int>(path).FirstOrDefault();
                return Content("True");
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Content("False");
            }
        }


        #region Roll Numbering Scheme
         [CheckPermission(Permissions = "Navigate", Module = "RNS")]
        public ActionResult RollNumberingScheme()
        {
           
            return View();
        }

        public ActionResult RollNumberingSchemeList(int pageNo = 1)
         {
             ViewBag.UserRight = base.UserRight("RNS");
            var data =
                _rollNumberingSchemeRepository.GetMany(
                    x => x.AcademyYearId == this.AcademicYear.Id && x.BranchId == this.CurrentBranch.Id).
                    OrderByDescending(a => a.Id).AsQueryable().ToPagedList(pageNo, base.SystemControl.PageSize);
            return PartialView(data);
        }
        public ActionResult RollNumberingSchemeAdd()
        {


            var viewModel = new RollNumberingSchemeViewModel()
            {
                ClassList = new SelectList(_classRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description"),
                SectionList = new SelectList(_sectionRepository.GetAll().OrderBy(x => x.Description),
                "Id", "Description").ToList(),

                Mode = new SelectList(
                Enum.GetValues(typeof(DocumentNumberStyleTypeEnum)).Cast
                    <DocumentNumberStyleTypeEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                "Value", "Text"),
                //StartDate = DateTime.UtcNow,
                //EndDate = DateTime.UtcNow.AddMonths(2)

            };
            viewModel.SectionList.Insert(0, new SelectListItem { Value = "0", Text = "None" });
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult RollNumberingSchemeAdd(RollNumberingSchemeViewModel model)
        {
            var DocNum = new ScRollNumberingScheme
            {
                BodyLen = model.RollNumberingScheme.BodyLen,
                CharFill = model.RollNumberingScheme.CharFill,
                CurrNo = model.RollNumberingScheme.CurrNo,
                Description = model.RollNumberingScheme.Description,
                // EndDate = model.EndDate,
                EndNo = model.RollNumberingScheme.EndNo,
                Mode = model.RollNumberingScheme.Mode,
                NumFill = model.RollNumberingScheme.NumFill,
                Prefix = model.RollNumberingScheme.Prefix,
                // StartDate = model.StartDate,
                StartNo = model.RollNumberingScheme.StartNo,
                Suffix = model.RollNumberingScheme.Suffix,
                TotalLen = model.RollNumberingScheme.TotalLen,
                SectionId = model.RollNumberingScheme.SectionId,
                ClassId = model.RollNumberingScheme.ClassId,
                BranchId = this.CurrentBranchId,
                AcademyYearId = this.AcademicYear.Id

            };
            _rollNumberingSchemeRepository.Add(DocNum);
            return null;
        }
        public ActionResult RollNumberingSchemeEdit(int id)
        {
            var data = _rollNumberingSchemeRepository.GetById(x => x.Id == id);
            var viewModel = new RollNumberingSchemeViewModel()
            {
                ClassList = new SelectList(_classRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description", data.ClassId),
                SectionList = new SelectList(_sectionRepository.GetAll().OrderBy(x => x.Description),
                "Id", "Description", data.SectionId).ToList(),
                Mode = new SelectList(
                Enum.GetValues(typeof(DocumentNumberStyleTypeEnum)).Cast
                    <DocumentNumberStyleTypeEnum>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = x.ToString() }),
                "Value", "Text", data.Mode),
                //StartDate = data.StartDate,
                //EndDate = data.EndDate,
                RollNumberingScheme = data

            };
            viewModel.SectionList.Insert(0, new SelectListItem { Value = "0", Text = "None" });
            return PartialView(viewModel);
        }
        [HttpPost]
        public ActionResult RollNumberingSchemeEdit(RollNumberingSchemeViewModel model)
        {
            var DocNum = new ScRollNumberingScheme()
            {
                Id = model.RollNumberingScheme.Id,

                BodyLen = model.RollNumberingScheme.BodyLen,
                CharFill = model.RollNumberingScheme.CharFill,
                CurrNo = model.RollNumberingScheme.CurrNo,
                Description = model.RollNumberingScheme.Description,
                // EndDate = model.EndDate,
                EndNo = model.RollNumberingScheme.EndNo,
                Mode = model.RollNumberingScheme.Mode,
                NumFill = model.RollNumberingScheme.NumFill,
                Prefix = model.RollNumberingScheme.Prefix,
                //  StartDate = model.StartDate,
                StartNo = model.RollNumberingScheme.StartNo,
                Suffix = model.RollNumberingScheme.Suffix,
                TotalLen = model.RollNumberingScheme.TotalLen,
                SectionId = model.RollNumberingScheme.SectionId,
                ClassId = model.RollNumberingScheme.ClassId,
                BranchId = this.CurrentBranchId,
                AcademyYearId = this.AcademicYear.Id

            };
            _rollNumberingSchemeRepository.Update(DocNum);
            return null;
        }

        #endregion

    }
}
