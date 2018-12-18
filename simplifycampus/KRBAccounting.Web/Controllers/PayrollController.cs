using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.ViewModels.Payroll;
using LinqKit;
using ReportManagement;
using iTextSharp.text;
using AutoMapper;
namespace KRBAccounting.Web.Controllers
{
   
    [Authorize]
    public class PayrollController : BaseController
    {
        private readonly IPyAllowanceMasterRepository _pyallowancemasterRepository;
        private readonly IPyCorporateSalaryMasterRepository _pycorporatesalarymasterRepository;
        private readonly IPyDeductionMasterRepository _pydeductionmasterRepository;
        private readonly IPyEmployeeDeductionMasterRepository _pyemployeedeductionmasterRepository;
        private readonly IPyEmployeeSalaryMasterRepository _pyemployeesalarymasterRepository;
        private readonly IPyPFEmployeeMasterRepository _pypfemployeemasterRepository;
        private readonly IPyTaxDeductionPatternRepository _pytaxdeductionpatternRepository;
        private readonly IFormsAuthenticationService _authentication;
        private readonly IScEmployeePostRepository _scemployeePostRepository;
        private readonly IPyCorporateAllowanceMappingRepository _pycorporateAllowanceMappingRepository;
        private readonly IScEmployeeInfoRepository _scEmployeeInfoRepository;
        private readonly IPyEmployeeDeductionMappingRepository _pyEmployeeDeductionMappingRepository;
        private readonly IPyEmployeeSalaryAllowanceMappingRepository _pyemployeeSalaryAllowanceMappingRepository;
        private readonly IPyPFEmployeeMappingRepository _pfEmployeeMappingRepository;
        private readonly IPyTaxDeductionEmployeeMappingRepository _pyTaxDeductionEmployeeMappingRepository;
        private readonly IScSalaryHeadRepository _scSalaryHeadRepository;
        private readonly IPyPayrollGenerationRepository _pypayrollgenerationRepository;
        private readonly IPyPayrollGenerationDetailRepository _pypayrollgenerationdetailRepository;
        private readonly IPyPaymentSlipRepository _pypaymentslipRepository;
        private readonly IDocumentNumeringSchemeRepository _documentNumeringSchemeRepository;
        private readonly IScEmployeeDepartmentRepository _employeeDepartmentRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly IEmployeeTransactionRepository _employeeTransactionRepository;

        private int fiscalyearId;
        public PayrollController(
            IPyAllowanceMasterRepository pyallowancemasterRepository,
            IPyCorporateSalaryMasterRepository pycorporatesalarymasterRepository,
            IPyDeductionMasterRepository pydeductionmasterRepository,
            IPyEmployeeDeductionMasterRepository pyemployeedeductionmasterRepository,
            IPyEmployeeSalaryMasterRepository pyemployeesalarymasterRepository,
            IPyPFEmployeeMasterRepository pypfemployeemasterRepository,
            IPyTaxDeductionPatternRepository pytaxdeductionpatternRepository,
          IScEmployeePostRepository scEmployeePostRepository,
            IPyCorporateAllowanceMappingRepository pyCorporateAllowanceMappingRepository,
            IScEmployeeInfoRepository scEmployeeInfoRepository,
            IPyEmployeeDeductionMappingRepository pyEmployeeDeductionMappingRepository,
            IPyEmployeeSalaryAllowanceMappingRepository pyEmployeeSalaryAllowanceMappingRepository,
            IPyPFEmployeeMappingRepository pyPfEmployeeMappingRepository,
            IPyTaxDeductionEmployeeMappingRepository taxDeductionEmployeeMappingRepository,
            IScSalaryHeadRepository scSalaryHeadRepository,
       IPyPayrollGenerationDetailRepository pypayrollgenerationdetailRepository,
            IPyPayrollGenerationRepository pypayrollgenerationRepository,
            IPyPaymentSlipRepository pypaymentslipRepository,
            IDocumentNumeringSchemeRepository documentNumeringSchemeRepository,
            IScEmployeeDepartmentRepository employeeDepartmentRepository,
            IAccountTransactionRepository accountTransactionRepository,
            IEmployeeTransactionRepository employeeTransactionRepository
            )
        {
            _pyallowancemasterRepository = pyallowancemasterRepository;
            _pycorporatesalarymasterRepository = pycorporatesalarymasterRepository;
            _pydeductionmasterRepository = pydeductionmasterRepository;
            _pyemployeedeductionmasterRepository = pyemployeedeductionmasterRepository;
            _pyemployeesalarymasterRepository = pyemployeesalarymasterRepository;
            _pypfemployeemasterRepository = pypfemployeemasterRepository;
            _pytaxdeductionpatternRepository = pytaxdeductionpatternRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Payroll);
            fiscalyearId = base.FiscalYear.Id;
            _scemployeePostRepository = scEmployeePostRepository;
            _pycorporateAllowanceMappingRepository = pyCorporateAllowanceMappingRepository;
            _scEmployeeInfoRepository = scEmployeeInfoRepository;
            _pyEmployeeDeductionMappingRepository = pyEmployeeDeductionMappingRepository;
            _pyemployeeSalaryAllowanceMappingRepository = pyEmployeeSalaryAllowanceMappingRepository;
            _pfEmployeeMappingRepository = pyPfEmployeeMappingRepository;
            _pyTaxDeductionEmployeeMappingRepository = taxDeductionEmployeeMappingRepository;
            _scSalaryHeadRepository = scSalaryHeadRepository;
            _pypayrollgenerationRepository = pypayrollgenerationRepository;
            _pypayrollgenerationdetailRepository = pypayrollgenerationdetailRepository;
            _pypaymentslipRepository = pypaymentslipRepository;
            _documentNumeringSchemeRepository = documentNumeringSchemeRepository;
            _employeeDepartmentRepository = employeeDepartmentRepository;
            _accountTransactionRepository = accountTransactionRepository;
            _employeeTransactionRepository = employeeTransactionRepository;
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScPY")]
        public ActionResult Index()
        {
            

            return View();
        }
      
        #region AllowanceMasters

        public JsonResult AllowanceMastersNameExists(string name, int id)
        {
            var namecheck = new PyAllowanceMaster();
            if (id != 0)
            {
                var data = _pyallowancemasterRepository.GetById(x => x.Id == id);
                if (data.Name.ToLower().Trim() != name.ToLower().Trim())
                {
                    namecheck = _pyallowancemasterRepository.GetById(x => x.Name.ToLower().Trim() == name.ToLower().Trim());

                }
            }
            else
            {
                namecheck = _pyallowancemasterRepository.GetById(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            }
            if (namecheck == null)
            {
                namecheck = new PyAllowanceMaster();
            }

            return namecheck.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "AM")]
        public ActionResult AllowanceMasters()
        {
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "AM")]
        public ActionResult AllowanceMasterAdd()
        {
            var model = new PyAllowanceMaster();
            model.AllowanceType = new SelectList(Enum.GetValues(typeof(AllowanceType)).Cast
                                                     <AllowanceType>().Select(
                                                         x =>
                                                         new SelectListItem
                                                             {
                                                                 Text = StringEnum.GetStringValue(x),
                                                                 Value = ((int)x).ToString()
                                                             }), "Value", "Text");

            model.IsFlat = true;
            return PartialView("_PartialAllowanceMasterAdd", model);
        }

        [HttpPost]
        public ActionResult AllowanceMasterAdd(PyAllowanceMaster model)
        {
            if (ModelState.IsValid)
            {
                var user = _authentication.GetAuthenticatedUser();
                model.CreatedBy = user.Id;
                model.CreatedOn = DateTime.Now;
                model.FiscalYearId = base.FiscalYear.Id;
                model.BranchId = CurrentBranch.Id;
                model.Status = true;
                _pyallowancemasterRepository.Add(model);
                return Content("True");
            }
            else
            {
                return Content("False");
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "AM")]
        public ActionResult AllowanceMasterEdit(int id)
        {
            PyAllowanceMaster objPyAllowanceMaster = _pyallowancemasterRepository.GetById(x => x.Id == id && x.BranchId == CurrentBranch.Id);
            objPyAllowanceMaster.AllowanceType = new SelectList(Enum.GetValues(typeof(AllowanceType)).Cast
                                                     <AllowanceType>().Select(
                                                         x =>
                                                         new SelectListItem
                                                         {
                                                             Text = StringEnum.GetStringValue(x),
                                                             Value = ((int)x).ToString()
                                                         }), "Value", "Text", objPyAllowanceMaster.Type);
            return PartialView("_PartialAllowanceMasterEdit", objPyAllowanceMaster);
        }

        [HttpPost]
        public ActionResult AllowanceMasterEdit(PyAllowanceMaster model)
        {

            _pyallowancemasterRepository.Update(model);
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "CSM")]
        public ActionResult AllowanceMastersList(int pageNo = 1)
        {
            
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            var lstAllowanceMasters = _pyallowancemasterRepository.GetPagedList(x => x.BranchId == CurrentBranch.Id, x => x.Id, pageNo, SystemControl.PageSize);
            return PartialView("_PartialAllowanceMastersList", lstAllowanceMasters);
        }

        public ActionResult AllowanceMasterDelete(int Id)
        {
            var data = _pyallowancemasterRepository.DeleteException(x => x.Id == Id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion AllowanceMasters
        #region CorporateSalaryMasters
           [CheckPermission(Permissions = "Navigate", Module = "CSM")]
        public ActionResult CorporateSalaryMasters()
        {
            return View();
        }
           [CheckPermission(Permissions = "Create", Module = "CSM")]
        public ActionResult CorporateSalaryMasterAdd()
        {
            var model = new PayrollCorporateSalaryMasterViewModel();
            var data = _pycorporatesalarymasterRepository.GetMany(x => x.FiscalYearId == fiscalyearId && x.BranchId == CurrentBranch.Id).Select(x => x.EmployeePostId);

            model.CheckBoxListInfos =
                new List<CheckBoxListInfo>(
                    _pyallowancemasterRepository.GetMany(x => x.Status).Select(x => new CheckBoxListInfo
                                                                                        {
                                                                                            DisplayText = x.Name,
                                                                                            Value = x.Id.ToString()
                                                                                        })).ToList();


            model.PositionList = new SelectList(_scemployeePostRepository.GetMany(x => x.Status && !data.Contains(x.Id)), "Id", "Description");
            return PartialView("_PartialCorporateSalaryMasterAdd", model);
        }

        [HttpPost]
        public ActionResult CorporateSalaryMasterAdd(PyCorporateSalaryMaster model)
        {
            try
            {
                if (model.Salary != 0)
                {
                    var user = _authentication.GetAuthenticatedUser();
                    model.CreatedBy = user.Id;
                    model.CreatedOn = DateTime.Now;
                    model.FiscalYearId = fiscalyearId;
                    model.BranchId = CurrentBranch.Id;
                    model.Status = true;
                    _pycorporatesalarymasterRepository.Add(model);
                    if (model.AllowancesId != null)
                    {
                        foreach (var data in model.AllowancesId.Select(i => new PyCorporateAllowanceMapping()
                                                                                {
                                                                                    AllowanceId = i,
                                                                                    CorporateId = model.Id
                                                                                }))
                        {
                            _pycorporateAllowanceMappingRepository.Add(data);
                        }
                    }
                    return Content("True");
                }
                else
                {
                    return Content("Amount Cannot be Zero");
                }


            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
           [CheckPermission(Permissions = "Edit", Module = "CSM")]
        public ActionResult CorporateSalaryMasterEdit(int id)
        {
            var model = new PayrollCorporateSalaryMasterViewModel();


            var data = _pycorporatesalarymasterRepository.GetMany(x => x.BranchId == CurrentBranch.Id && x.FiscalYearId == fiscalyearId).Select(x => x.EmployeePostId);
            model.CorporateSalaryMaster = _pycorporatesalarymasterRepository.GetById(x => x.Id == id && x.BranchId == CurrentBranch.Id);
            model.CheckBoxListInfos =
                new List<CheckBoxListInfo>(
                    _pyallowancemasterRepository.GetMany(x => x.Status).Select(x => new CheckBoxListInfo
                    {
                        DisplayText = x.Name,
                        Value = x.Id.ToString(),
                        IsChecked = UtilityService.CheckCorporateAllowance(id, x.Id)

                    })).ToList();
            var list = _scemployeePostRepository.GetMany(x => x.Status && !data.Contains(x.Id)).ToList();
            list.Add(model.CorporateSalaryMaster.EmployeePost);

            model.PositionList = new SelectList(list.OrderBy(x => x.Description), "Id", "Description", model.CorporateSalaryMaster.EmployeePostId);


            return PartialView("_PartialCorporateSalaryMasterEdit", model);
        }

        [HttpPost]
        public ActionResult CorporateSalaryMasterEdit(PayrollCorporateSalaryMasterViewModel model)
        {

            try
            {
                if (model.CorporateSalaryMaster.Salary != 0)
                {

                    _pycorporatesalarymasterRepository.Update(model.CorporateSalaryMaster);
                    _pycorporateAllowanceMappingRepository.DeleteException(x => x.CorporateId == model.CorporateSalaryMaster.Id);
                    if (model.AllowancesId != null)
                    {
                        foreach (var data in model.AllowancesId.Select(i => new PyCorporateAllowanceMapping()
                                                                                {
                                                                                    AllowanceId = i,
                                                                                    CorporateId =
                                                                                        model.CorporateSalaryMaster.Id
                                                                                }))
                        {
                            _pycorporateAllowanceMappingRepository.Add(data);
                        }
                    }
                    return Content("True");
                }
                else
                {
                    return Content("Amount Cannot be Zero");
                }


            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }

           [CheckPermission(Permissions = "Navigate", Module = "CSM")]
        public ActionResult CorporateSalaryMastersList(int pageNo = 1)
        {
            var lstCorporateSalaryMasters =
                _pycorporatesalarymasterRepository.GetMany(x => x.BranchId == CurrentBranch.Id && x.FiscalYearId == fiscalyearId).OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialCorporateSalaryMastersList", lstCorporateSalaryMasters.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult CorporateSalaryMasterDelete(int id)
        {
            var data = _pycorporatesalarymasterRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion CorporateSalaryMasters
        #region DeductionMasters

        public JsonResult DeductionMastersNameExists(string name, int Id)
        {
            var namecheck = new PyDeductionMaster();
            if (Id != 0)
            {
                var data = _pydeductionmasterRepository.GetById(x => x.Id == Id);
                if (data.Name.ToLower().Trim() != name.ToLower().Trim())
                {
                    namecheck = _pydeductionmasterRepository.GetById(x => x.Name.ToLower().Trim() == name.ToLower().Trim());

                }
            }
            else
            {
                namecheck = _pydeductionmasterRepository.GetById(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            }
            if (namecheck == null)
            {
                namecheck = new PyDeductionMaster();
            }

            return namecheck.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
         [CheckPermission(Permissions = "Navigate", Module = "PyDM")]
        public ActionResult DeductionMasters()
        {
            return View();
        }
         [CheckPermission(Permissions = "Create", Module = "PyDM")]
        public ActionResult DeductionMasterAdd()
        {
            var model = new PyDeductionMaster();
            model.TypeList = new SelectList(Enum.GetValues(typeof(DeductionType)).Cast
                                                   <DeductionType>().Select(
                                                       x =>
                                                       new SelectListItem
                                                       {
                                                           Text = StringEnum.GetStringValue(x),
                                                           Value = ((int)x).ToString()
                                                       }), "Value", "Text");

            model.IsFlat = true;
            return PartialView("_PartialDeductionMasterAdd", model);
        }

        [HttpPost]
        public ActionResult DeductionMasterAdd(PyDeductionMaster model)
        {
            try
            {
                if (model.Amount != 0)
                {
                    var user = _authentication.GetAuthenticatedUser();
                    model.CreatedBy = user.Id;
                    model.CreatedOn = DateTime.Now;
                    model.FiscalYearId = base.FiscalYear.Id;
                    model.BranchId = CurrentBranch.Id;
                    model.Status = true;
                    _pydeductionmasterRepository.Add(model);

                    return Content("True");
                }
                else
                {
                    return Content(model.IsFlat ? "Amount Cannot Be Zero" : "Percentage Cannot Be Zero");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
         [CheckPermission(Permissions = "Edit", Module = "PyDM")]
        public ActionResult DeductionMasterEdit(int id)
        {
            var model = _pydeductionmasterRepository.GetById(x => x.Id == id && x.BranchId == CurrentBranch.Id);



            model.TypeList = new SelectList(Enum.GetValues(typeof(DeductionType)).Cast
                                                   <DeductionType>().Select(
                                                       x =>
                                                       new SelectListItem
                                                       {
                                                           Text = StringEnum.GetStringValue(x),
                                                           Value = ((int)x).ToString()
                                                       }), "Value", "Text", model.Type);


            return PartialView("_PartialDeductionMasterEdit", model);
        }

        [HttpPost]
        public ActionResult DeductionMasterEdit(PyDeductionMaster model)
        {
            try
            {
                if (model.Amount != 0)
                {
                    _pydeductionmasterRepository.Update(model);
                    return Content("True");
                }
                else
                {
                    return Content(model.IsFlat ? "Amount Cannot Be Zero" : "Percentage Cannot Be Zero");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
         [CheckPermission(Permissions = "Navigate", Module = "PyDM")]
        public ActionResult DeductionMastersList(int pageNo = 1)
        {
            var lstDeductionMasters = _pydeductionmasterRepository.GetMany(x => x.FiscalYearId == fiscalyearId && x.BranchId == CurrentBranch.Id).OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialDeductionMastersList", lstDeductionMasters.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult DeductionMasterDelete(int id)
        {
            var data = _pydeductionmasterRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion DeductionMasters
        #region EmployeeDeductionMasters
         [CheckPermission(Permissions = "Navigate", Module = "PyEDM")]
        public ActionResult EmployeeDeductionMasters()
        {
            return View();
        }
         [CheckPermission(Permissions = "Create", Module = "PyEDM")]
        public ActionResult EmployeeDeductionMasterAdd()
        {
            var model = new PayrollEmployeeDeductionViewModel();
            var data = _pyemployeedeductionmasterRepository.GetMany(x => x.BranchId == CurrentBranch.Id && x.FiscalYearId == fiscalyearId).Select(x => x.EmployeeId);

            model.CheckBoxListInfos =
                new List<CheckBoxListInfo>(
                    _pydeductionmasterRepository.GetMany(x => x.Status).Select(x => new CheckBoxListInfo
                    {
                        DisplayText = x.Name,
                        Value = x.Id.ToString()
                    })).ToList();


            model.EmployeeList = new SelectList(_scEmployeeInfoRepository.GetMany(x => !data.Contains(x.Id)), "Id", "Description");
            return PartialView("_PartialEmployeeDeductionMasterAdd", model);
        }

        [HttpPost]
        public ActionResult EmployeeDeductionMasterAdd(PayrollEmployeeDeductionViewModel model)
        {
            try
            {

                if (model.DeductionId.Any() && model.DeductionId != null)
                {
                    var user = _authentication.GetAuthenticatedUser();
                    model.EmployeeDeductionMaster.CreatedBy = user.Id;
                    model.EmployeeDeductionMaster.CreatedOn = DateTime.Now;
                    model.EmployeeDeductionMaster.BranchId = CurrentBranch.Id;
                    model.EmployeeDeductionMaster.FiscalYearId = fiscalyearId;
                    model.EmployeeDeductionMaster.Status = true;
                    _pyemployeedeductionmasterRepository.Add(model.EmployeeDeductionMaster);

                    foreach (var data in model.DeductionId.Select(i => new PyEmployeeDeductionMapping()
                        {
                            DeductionId = i,
                            EmployeeDeductionId = model.EmployeeDeductionMaster.Id
                        }))
                    {
                        _pyEmployeeDeductionMappingRepository.Add(data);
                    }

                    return Content("True");
                }
                else
                {
                    return Content("Please Select Deduction");
                }


            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
         [CheckPermission(Permissions = "Edit", Module = "PyEDM")]
        public ActionResult EmployeeDeductionMasterEdit(int id)
        {

            var model = new PayrollEmployeeDeductionViewModel();
            var data = _pyemployeedeductionmasterRepository.GetMany(x => x.FiscalYearId == fiscalyearId && x.BranchId == CurrentBranch.Id).Select(x => x.EmployeeId);
            model.EmployeeDeductionMaster = _pyemployeedeductionmasterRepository.GetById(x => x.Id == id);
            model.CheckBoxListInfos =
                new List<CheckBoxListInfo>(
                    _pydeductionmasterRepository.GetMany(x => x.Status).Select(x => new CheckBoxListInfo
                    {
                        DisplayText = x.Name,
                        Value = x.Id.ToString(),
                        IsChecked = UtilityService.CheckEmployeeDeduction(x.Id, id)

                    })).ToList();

            var list = _scEmployeeInfoRepository.GetMany(x => !data.Contains(x.Id)).ToList();
            list.Add(model.EmployeeDeductionMaster.EmployeeInfo);
            model.EmployeeList = new SelectList(list, "Id", "Description", model.EmployeeDeductionMaster.EmployeeId);
            model.OldEmployeeDeductionId = id;
            return PartialView("_PartialEmployeeDeductionMasterEdit", model);
        }

        [HttpPost]
        public ActionResult EmployeeDeductionMasterEdit(PayrollEmployeeDeductionViewModel model)
        {
            try
            {

                if (model.DeductionId.Any() && model.DeductionId != null)
                {

                    _pyemployeedeductionmasterRepository.Update(model.EmployeeDeductionMaster);
                    _pyEmployeeDeductionMappingRepository.Delete(x => x.EmployeeDeductionId == model.OldEmployeeDeductionId);
                    foreach (var data in model.DeductionId.Select(i => new PyEmployeeDeductionMapping()
                    {
                        DeductionId = i,
                        EmployeeDeductionId = model.EmployeeDeductionMaster.Id
                    }))
                    {
                        _pyEmployeeDeductionMappingRepository.Add(data);
                    }

                    return Content("True");
                }
                else
                {
                    return Content("Please Select Deduction");
                }


            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
         [CheckPermission(Permissions = "Navigate", Module = "PyEDM")]
        public ActionResult EmployeeDeductionMastersList(int pageNo = 1)
        {
            var lstEmployeeDeductionMasters = _pyemployeedeductionmasterRepository.GetMany(x => x.BranchId == CurrentBranch.Id && x.FiscalYearId == fiscalyearId).OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialEmployeeDeductionMastersList", lstEmployeeDeductionMasters.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult EmployeeDeductionMasterDelete(int id)
        {
            var data = _pyemployeedeductionmasterRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion EmployeeDeductionMasters
        #region EmployeeSalaryMasters
                [CheckPermission(Permissions = "Navigate", Module = "PyESM")]
        public ActionResult EmployeeSalaryMasters()
        {
            return View();
        }
                [CheckPermission(Permissions = "Create", Module = "PyESM")]
        public ActionResult EmployeeSalaryMasterAdd()
        {
            var model = new PayrollEmployeeSalaryMasterViewModel();
            var data = _pyemployeesalarymasterRepository.GetMany(x => x.BranchId == CurrentBranch.Id && x.FiscalYearId == fiscalyearId).Select(x => x.EmployeeId);

            model.CheckBoxListInfos =
                new List<CheckBoxListInfo>(
                    _pyallowancemasterRepository.GetMany(x => x.Status).Select(x => new CheckBoxListInfo
                    {
                        DisplayText = x.Name,
                        Value = x.Id.ToString()
                    })).ToList();


            model.EmployeeList = new SelectList(_scEmployeeInfoRepository.GetMany(x => !data.Contains(x.Id)), "Id", "Description");
            return PartialView("_PartialEmployeeSalaryMasterAdd", model);
        }

        [HttpPost]
        public ActionResult EmployeeSalaryMasterAdd(PayrollEmployeeSalaryMasterViewModel model)
        {
            try
            {

                if (model.EmployeeSalaryMaster.BasicSalary != 0)
                {
                    var user = _authentication.GetAuthenticatedUser();
                    model.EmployeeSalaryMaster.CreatedBy = user.Id;
                    model.EmployeeSalaryMaster.CreatedOn = DateTime.Now;
                    model.EmployeeSalaryMaster.BranchId = CurrentBranch.Id;
                    model.EmployeeSalaryMaster.FiscalYearId = fiscalyearId;
                    model.EmployeeSalaryMaster.Status = true;
                    _pyemployeesalarymasterRepository.Add(model.EmployeeSalaryMaster
                        );
                    if (model.AllowanceId != null)
                    {
                        foreach (var data in model.AllowanceId.Select(i => new PyEmployeeSalaryAllowanceMapping()
                                                                               {
                                                                                   AllowanceId = i,
                                                                                   EmployeeSalaryId =
                                                                                       model.EmployeeSalaryMaster.Id
                                                                               }))
                        {
                            _pyemployeeSalaryAllowanceMappingRepository.Add(data);
                        }


                    } return Content("True");
                }
                else
                {
                    return Content("Basic Salary Cannot Be Zero");
                }


            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
                [CheckPermission(Permissions = "Edit", Module = "PyESM")]
        public ActionResult EmployeeSalaryMasterEdit(int id)
        {
            var model = new PayrollEmployeeSalaryMasterViewModel();
            var data = _pyemployeesalarymasterRepository.GetMany(x => x.FiscalYearId == fiscalyearId && x.BranchId == CurrentBranch.Id).Select(x => x.EmployeeId);
            model.EmployeeSalaryMaster = _pyemployeesalarymasterRepository.GetById(x => x.Id == id);
            model.CheckBoxListInfos =
                new List<CheckBoxListInfo>(
                    _pyallowancemasterRepository.GetMany(x => x.Status).Select(x => new CheckBoxListInfo
                    {
                        DisplayText = x.Name,
                        Value = x.Id.ToString(),
                        IsChecked = UtilityService.CheckEmployeeAllowance(x.Id, id)

                    })).ToList();

            var list = _scEmployeeInfoRepository.GetMany(x => !data.Contains(x.Id)).ToList();
            list.Add(model.EmployeeSalaryMaster.EmployeeInfo);
            model.EmployeeList = new SelectList(list, "Id", "Description", model.EmployeeSalaryMaster.EmployeeId);
            model.OldEmployeeSalaryId = id;
            return PartialView("_PartialEmployeeSalaryMasterEdit", model);
        }

        [HttpPost]
        public ActionResult EmployeeSalaryMasterEdit(PayrollEmployeeSalaryMasterViewModel model)
        {
            try
            {

                if (model.EmployeeSalaryMaster.BasicSalary != 0)
                {

                    _pyemployeesalarymasterRepository.Update(model.EmployeeSalaryMaster);
                    _pyemployeeSalaryAllowanceMappingRepository.Delete(
                        x => x.EmployeeSalaryId == model.OldEmployeeSalaryId);
                    if (model.AllowanceId != null)
                    {
                        foreach (var data in model.AllowanceId.Select(i => new PyEmployeeSalaryAllowanceMapping()
                                                                               {
                                                                                   AllowanceId = i,
                                                                                   EmployeeSalaryId =
                                                                                       model.EmployeeSalaryMaster.Id
                                                                               }))
                        {
                            _pyemployeeSalaryAllowanceMappingRepository.Add(data);
                        }
                    }
                    return Content("True");
                }
                else
                {
                    return Content("Basic Salary Cannot Be Zero");
                }


            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
                [CheckPermission(Permissions = "Navigate", Module = "PyESM")]
        public ActionResult EmployeeSalaryMastersList(int pageNo = 1)
        {
            var list = _pyemployeesalarymasterRepository.GetMany(x => x.BranchId == CurrentBranch.Id && x.FiscalYearId == fiscalyearId).OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialEmployeeSalaryMastersList", list.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult EmployeeSalaryMasterDelete(int id)
        {
            var data = _pyemployeesalarymasterRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion EmployeeSalaryMasters
        #region PFEmployeeMasters
        [CheckPermission(Permissions = "Navigate", Module = "PyPFEM")]
        public ActionResult PFEmployeeMasters()
        {
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "PyPFEM")]
        public ActionResult PFEmployeeMasterAdd()
        {
            var model = new PayrollPFEmployeeViewModel();
            var data = _pfEmployeeMappingRepository.GetMany(x => x.PyPfEmployeeMaster.BranchId == CurrentBranch.Id && x.PyPfEmployeeMaster.FiscalYearId == fiscalyearId).Select(x => x.EmployeeId);

            model.CheckBoxListInfos =
                new List<CheckBoxListInfo>(
                    _scEmployeeInfoRepository.GetMany(x => !data.Contains(x.Id)).Select(x => new CheckBoxListInfo
                    {
                        DisplayText = x.Description,
                        Value = x.Id.ToString()
                    })).ToList();


            return PartialView("_PartialPFEmployeeMasterAdd", model);
        }

        [HttpPost]
        public ActionResult PFEmployeeMasterAdd(PayrollPFEmployeeViewModel model)
        {
            try
            {

                if (model.PyPfEmployeeMaster.Value != 0)
                {
                    var user = _authentication.GetAuthenticatedUser();
                    model.PyPfEmployeeMaster.CreatedBy = user.Id;
                    model.PyPfEmployeeMaster.CreatedOn = DateTime.Now;
                    model.PyPfEmployeeMaster.BranchId = CurrentBranch.Id;
                    model.PyPfEmployeeMaster.FiscalYearId = fiscalyearId;
                    model.PyPfEmployeeMaster.Status = true;
                    _pypfemployeemasterRepository.Add(model.PyPfEmployeeMaster);
                    if (model.EmployeeId != null)
                    {
                        foreach (var data in model.EmployeeId.Select(i => new PyPFEmployeeMapping()
                        {
                            EmployeeId = i,
                            PFEmployeeId =
                                model.PyPfEmployeeMaster.Id
                        }))
                        {
                            _pfEmployeeMappingRepository.Add(data);
                        }


                    } return Content("True");
                }
                else
                {
                    if (model.PyPfEmployeeMaster.IsFlat)
                    {
                        return Content("Amount Cannot Be Zero");
                    }
                    else
                    {
                        return Content("Percentage Cannot Be Zero");
                    }
                }


            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.InnerException.Message);
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "PyPFEM")]
        public ActionResult PFEmployeeMasterEdit(int id)
        {
            var model = new PayrollPFEmployeeViewModel();
            var data =
                _pfEmployeeMappingRepository.GetMany(
                    x =>
                    x.PyPfEmployeeMaster.BranchId == CurrentBranch.Id &&
                    x.PyPfEmployeeMaster.FiscalYearId == fiscalyearId).Select(x => x.EmployeeId);
            var list = _scEmployeeInfoRepository.GetMany(x => !data.Contains(x.Id)).ToList();

            var employeelist = _pfEmployeeMappingRepository.GetMany(x => x.PFEmployeeId == id).Select(x => x.EmployeeInfo);
            list = list.Union(employeelist).OrderBy(x => x.Description).ToList();


            model.PyPfEmployeeMaster =
                _pypfemployeemasterRepository.GetById(x => x.Id == id && x.BranchId == CurrentBranch.Id);
            model.CheckBoxListInfos =
                new List<CheckBoxListInfo>(
                   list.Select(x => new CheckBoxListInfo
                    {
                        DisplayText = x.Description,
                        Value = x.Id.ToString(),
                        IsChecked = UtilityService.CheckPFEmployee(x.Id, id)
                    })).ToList();

            model.OldPFemployeeId = id;
            return PartialView("_PartialPFEmployeeMasterEdit", model);
        }

        [HttpPost]
        public ActionResult PFEmployeeMasterEdit(PayrollPFEmployeeViewModel model)
        {
            try
            {

                if (model.PyPfEmployeeMaster.Value != 0)
                {

                    _pypfemployeemasterRepository.Update(model.PyPfEmployeeMaster);
                    _pfEmployeeMappingRepository.Delete(x => x.PFEmployeeId == model.OldPFemployeeId);
                    if (model.EmployeeId != null)
                    {
                        foreach (var data in model.EmployeeId.Select(i => new PyPFEmployeeMapping()
                        {
                            EmployeeId = i,
                            PFEmployeeId =
                                model.PyPfEmployeeMaster.Id
                        }))
                        {
                            _pfEmployeeMappingRepository.Add(data);
                        }


                    } return Content("True");
                }
                else
                {
                    if (model.PyPfEmployeeMaster.IsFlat)
                    {
                        return Content("Amount Cannot Be Zero");
                    }
                    else
                    {
                        return Content("Percentage Cannot Be Zero");
                    }
                }


            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [CheckPermission(Permissions = "Navigate", Module = "PyPFEM")]
        public ActionResult PFEmployeeMastersList(int pageNo = 1)
        {
            var lstPFEmployeeMasters = _pypfemployeemasterRepository.GetMany(x => x.FiscalYearId == fiscalyearId && x.BranchId == CurrentBranch.Id).OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialPFEmployeeMastersList", lstPFEmployeeMasters.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult PFEmployeeMasterDelete(int id)
        {
            //PyPFEmployeeMaster objPyPFEmployeeMaster = _pypfemployeemasterRepository.GetById(x => x.Id == id);
            //_pypfemployeemasterRepository.DeleteException(objPyPFEmployeeMaster);
            //return Json("True");
            var data = _pypfemployeemasterRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion PFEmployeeMasters
        #region TaxDeductionPatterns
        [CheckPermission(Permissions = "Navigate", Module = "PyTDP")]
        public ActionResult TaxDeductionPatterns()
        {
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "PyTDP")]
        public ActionResult TaxDeductionPatternAdd()
        {
            var model = new PayrollTaxDeductionPatternViewModel();
            var data = _pyTaxDeductionEmployeeMappingRepository.GetMany(x => x.PyTaxDeductionPattern.BranchId == CurrentBranch.Id && x.PyTaxDeductionPattern.FiscalYearId == fiscalyearId).Select(x => x.EmployeeId);

            model.CheckBoxListInfos =
                new List<CheckBoxListInfo>(
                    _scEmployeeInfoRepository.GetMany(x => !data.Contains(x.Id)).Select(x => new CheckBoxListInfo
                    {
                        DisplayText = x.Description,
                        Value = x.Id.ToString()
                    })).ToList();


            return PartialView("_PartialTaxDeductionPatternAdd", model);
        }

        [HttpPost]
        public ActionResult TaxDeductionPatternAdd(PayrollTaxDeductionPatternViewModel model)
        {
            try
            {

                if (model.PyTaxDeductionPattern.Percentage != 0)
                {
                    var user = _authentication.GetAuthenticatedUser();
                    model.PyTaxDeductionPattern.CreatedBy = user.Id;
                    model.PyTaxDeductionPattern.CreatedOn = DateTime.Now;
                    model.PyTaxDeductionPattern.BranchId = CurrentBranch.Id;
                    model.PyTaxDeductionPattern.FiscalYearId = fiscalyearId;
                    model.PyTaxDeductionPattern.Status = true;
                    _pytaxdeductionpatternRepository.Add(model.PyTaxDeductionPattern);
                    if (model.EmployeeId != null)
                    {
                        foreach (var data in model.EmployeeId.Select(i => new PyTaxDeductionEmployeeMapping
                        {
                            EmployeeId = i,
                            TaxDeductionId =
                                model.PyTaxDeductionPattern.Id
                        }))
                        {
                            _pyTaxDeductionEmployeeMappingRepository.Add(data);
                        }


                    } return Content("True");
                }
                else
                {

                    return Content("Percentage Cannot Be Zero");

                }


            }
            catch (Exception ex)
            {
                return Content(UtilityService.ErrorMsg(ex));
            }
        }
[CheckPermission(Permissions = "Edit", Module = "PyTDP")]
        public ActionResult TaxDeductionPatternEdit(int id)
        {
            var model = new PayrollTaxDeductionPatternViewModel();
            var data =
                _pyTaxDeductionEmployeeMappingRepository.GetMany(
                    x =>
                    x.PyTaxDeductionPattern.BranchId == CurrentBranch.Id &&
                    x.PyTaxDeductionPattern.FiscalYearId == fiscalyearId).Select(x => x.EmployeeId);
            var list = _scEmployeeInfoRepository.GetMany(x => !data.Contains(x.Id)).ToList();

            var employeelist = _pyTaxDeductionEmployeeMappingRepository.GetMany(x => x.TaxDeductionId == id).Select(x => x.EmployeeInfo);
            list = list.Union(employeelist).OrderBy(x => x.Description).ToList();


            model.PyTaxDeductionPattern =
                _pytaxdeductionpatternRepository.GetById(x => x.Id == id && x.BranchId == CurrentBranch.Id);
            model.CheckBoxListInfos =
                new List<CheckBoxListInfo>(
                   list.Select(x => new CheckBoxListInfo
                   {
                       DisplayText = x.Description,
                       Value = x.Id.ToString(),
                       IsChecked = UtilityService.CheckTaxDeductionEmployee(x.Id, id)
                   })).ToList();

            model.OldtaxdeductionId = id;
            return PartialView("_PartialTaxDeductionPatternEdit", model);
        }

        [HttpPost]
        public ActionResult TaxDeductionPatternEdit(PayrollTaxDeductionPatternViewModel model)
        {
            try
            {

                if (model.PyTaxDeductionPattern.Percentage != 0)
                {
                    _pytaxdeductionpatternRepository.Update(model.PyTaxDeductionPattern);
                    _pyTaxDeductionEmployeeMappingRepository.Delete(x => x.TaxDeductionId == model.PyTaxDeductionPattern.Id);
                    if (model.EmployeeId != null)
                    {
                        foreach (var data in model.EmployeeId.Select(i => new PyTaxDeductionEmployeeMapping
                        {
                            EmployeeId = i,
                            TaxDeductionId =
                                model.PyTaxDeductionPattern.Id
                        }))
                        {
                            _pyTaxDeductionEmployeeMappingRepository.Add(data);
                        }


                    } return Content("True");
                }
                else
                {

                    return Content("Percentage Cannot Be Zero");

                }


            }
            catch (Exception ex)
            {
                return Content(UtilityService.ErrorMsg(ex));
            }
        }
        [CheckPermission(Permissions = "Navigate", Module = "PyTDP")]
        public ActionResult TaxDeductionPatternsList(int pageNo = 1)
        {
            var lstTaxDeductionPatterns = _pytaxdeductionpatternRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialTaxDeductionPatternsList", lstTaxDeductionPatterns.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult TaxDeductionPatternDelete(int id)
        {
            var data = _pytaxdeductionpatternRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion TaxDeductionPatterns
        #region SalaryHeads
        [CheckPermission(Permissions = "Navigate", Module = "PySH")]
        public ActionResult SalaryHeads()
        {
            ViewBag.UserRight = base.UserRight("PySH");

            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "PySH")]
        public ActionResult SalaryHeadAdd()
        {
            var model = new ScSalaryHead();
            model.HeadList = new SelectList(
                   Enum.GetValues(typeof(SalaryHeadType)).Cast<SalaryHeadType>().Select(
                       x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }), "Value", "Text");


            model.OperationList = new SelectList(DropDownHelper.OperatorList(), "Value", "Text");
            var lastOrDefault = _scSalaryHeadRepository.GetAll().LastOrDefault();
            if (lastOrDefault != null)
            {
                var displayOrder = lastOrDefault.DisplayOrder;
                model.DisplayOrder = displayOrder+1;
            }
         
            return PartialView("_PartialSalaryHeadAdd", model);
        }

        [HttpPost]
        public ActionResult SalaryHeadAdd(ScSalaryHead model)
        {
            try
            {
                var data = _scSalaryHeadRepository.GetById(x => x.Head == model.Head);
                if (model.LedgerId != 0 && model.LedgerId != null)
                {
                    if (data == null)
                    {
                        if (model.DisplayOrder != 0)
                        {
                            _scSalaryHeadRepository.Add(model);

                            return Content("True");
                        }
                        else
                        {
                            return Content("Display Order Cannot Be Zero");
                        }
                    }
                    else
                    {
                        var type = new StringEnum(typeof(SalaryHeadType)).GetStringValue(model.Head.ToString());

                        return Content("Already Exist For " + type);
                    }
                }
                else
                {
                    return Content("Ledger Cannot Be Null");
                }

            }
            catch (Exception ex)
            {

                var error = UtilityService.ErrorMsg(ex);
                return Content(error);
            }


        }
        [CheckPermission(Permissions = "Edit", Module = "PySH")]
        public ActionResult SalaryHeadEdit(int id)
        {

            var model = _scSalaryHeadRepository.GetById(x => x.Id == id);
            model.HeadList = new SelectList(
                   Enum.GetValues(typeof(SalaryHeadType)).Cast<SalaryHeadType>().Select(
                       x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }), "Value", "Text", model.Head);


            model.OperationList = new SelectList(DropDownHelper.OperatorList(), "Value", "Text", model.Operation);
            return PartialView("_PartialSalaryHeadEdit", model);
        }

        [HttpPost]
        public ActionResult SalaryHeadEdit(ScSalaryHead model)
        {

            try
            {
                var d = _scSalaryHeadRepository.GetById(model.Id);

                var data = _scSalaryHeadRepository.GetById(x => x.Head == model.Head);
                if (model.LedgerId != 0 && model.LedgerId != null)
                {

                    if (data == null || d.Head == model.Head)
                    {

                        if (model.DisplayOrder != 0)
                        {
                            var _dataContext = new DataContext();
                            _dataContext.Entry(model).State = EntityState.Modified;
                            _dataContext.SaveChanges();

                            return Content("True");
                        }
                        else
                        {
                            return Content("Display Order Cannot Be Zero");
                        }
                    }
                    else
                    {
                        var type = new StringEnum(typeof(SalaryHeadType)).GetStringValue(model.Head.ToString());

                        return Content("Already Exist For " + type);
                    }
                }
                else
                {
                    return Content("Ledger Cannot Be Null");
                }

            }
            catch (Exception ex)
            {

                var error = UtilityService.ErrorMsg(ex);
                return Content(error);
            }
        }
        [CheckPermission(Permissions = "Navigate", Module = "PySH")]
        public ActionResult SalaryHeadsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("PySH");
          
            var lstSalaryHeads = _scSalaryHeadRepository.GetAll().OrderBy(x => x.DisplayOrder);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialSalaryHeadList", lstSalaryHeads.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
        public ActionResult SalaryHeadsSortOrder(string id)
        {
            string[] separator = new string[2] { "tr[]=", "&" };
            string[] itemIdArray = id.Split(separator, StringSplitOptions.RemoveEmptyEntries);


            _scSalaryHeadRepository.UpdateSortOrder(itemIdArray);
            return Content("Success");
        }

        //public ActionResult SalaryHeadDelete(int id)
        //{
        //    var SalaryHead = _scsalaryheadRepository.DeleteException(x => x.Id == id);
        //    return Json(SalaryHead, JsonRequestBehavior.AllowGet);
        //}

        #endregion


        #region payroll Generation
         [CheckPermission(Permissions = "Navigate", Module = "ScPygn")]
        public ActionResult PayrollGenerations()
        {
            var viewModel = this.BuildPayrollGenerationViewModel();

            return View(viewModel);
        }
        public PayrollGenerationViewModel BuildPayrollGenerationViewModel()
        {
            var dateype = base.SystemControl.DateType;
            var viewModel = new PayrollGenerationViewModel();

            if (dateype == 1)
            {
                viewModel.MonthList = DropDownHelper.CreateMonthDate();
                viewModel.Year = DateTime.Now.Year;
                viewModel.Month = DateTime.Now.Month;

            }
            else
            {
                viewModel.MonthList = DropDownHelper.CreateMonthMiti();
                viewModel.Year = NepaliDateService.NepaliDate(DateTime.Now).Year;
                viewModel.Month = NepaliDateService.NepaliDate(DateTime.Now).Month;
            }
            viewModel.AnnualList = new List<CheckBoxListInfo>();
            var Allowance =
                UtilityService.GetExistPayrollBySalaryHeadAndYear((int)SalaryHeadType.Allowance, viewModel.Year,
                                                                  CurrentBranch.Id, dateype).Select(x => x.ReferenceId);
            var Deduction =
         UtilityService.GetExistPayrollBySalaryHeadAndYear((int)SalaryHeadType.Deduction, viewModel.Year,
                                                                  CurrentBranch.Id, dateype).Select(x => x.ReferenceId);
            var TaxDeduction =
        UtilityService.GetExistPayrollBySalaryHeadAndYear((int)SalaryHeadType.TaxDeduction, viewModel.Year,
                                                                  CurrentBranch.Id, dateype).Select(x => x.ReferenceId);
            var AllowanceYear =
                _pyallowancemasterRepository.GetMany(
                    x => x.Status && x.IsAnnual && x.BranchId == CurrentBranch.Id && x.FiscalYearId == fiscalyearId && !Allowance.Contains(x.Id)).
                    Select(x => new CheckBoxListInfo
                                    {
                                        DisplayText = x.Name,
                                        Value = "1." + x.Id.ToString()
                                    }).ToList();



            var DeductionYear = _pydeductionmasterRepository.GetMany(
                    x => x.Status && x.IsAnnual && x.BranchId == CurrentBranch.Id && x.FiscalYearId == fiscalyearId && !Deduction.Contains(x.Id)).Select(x => new CheckBoxListInfo
                    {
                        DisplayText = x.Name,
                        Value = "2." + x.Id.ToString()
                    }).ToList();

            var taxDeductionYear = _pytaxdeductionpatternRepository.GetMany(
                     x => x.Status && x.IsAnnual && x.BranchId == CurrentBranch.Id && x.FiscalYearId == fiscalyearId && !TaxDeduction.Contains(x.Id)).Select(x => new CheckBoxListInfo()
                     {
                         DisplayText = x.Name,
                         Value = "3." + x.Id.ToString()
                     }).ToList();

            viewModel.AnnualList = viewModel.AnnualList.Union(AllowanceYear).ToList();
            viewModel.AnnualList = viewModel.AnnualList.Union(DeductionYear).ToList();
            viewModel.AnnualList = viewModel.AnnualList.Union(taxDeductionYear).ToList();
            return viewModel;

        }
        [HttpPost]
        public ActionResult PayrollGeneration(PayrollGenerationViewModel model)
        {
            var employeeinfo = _scEmployeeInfoRepository.GetMany(x => x.BranchId == CurrentBranch.Id);
            var datetype = base.SystemControl.DateType;
            var year = 0;
            var yearmiti = 0;
            var month = 0;
            var monthmiti = 0;
            var msg = ""; string monthName = "";

            if (datetype == 1)
            {
                year = model.Year;
                month = model.Month;
                var date = month + "/01/"  + year;
                yearmiti = NepaliDateService.NepaliDate(Convert.ToDateTime(date)).Year;
                monthmiti = NepaliDateService.NepaliDate(Convert.ToDateTime(date)).Month;
                monthName = Convert.ToDateTime(date).ToString("MM");
            }
            else
            {
                yearmiti = model.Year;
                monthmiti = model.Month;
                var nepdate = yearmiti + "/" + monthmiti + "/01";
                DateTime date = NepaliDateService.NepalitoEnglishDate(nepdate);
                monthName = NepaliDateService.LongMonth(model.Month);
                year = date.Year;
                month = date.Month;
            }
            var user = _authentication.GetAuthenticatedUser();
            var payrollgenerationlist = new List<PayrollGenerationAddViewModel>();
            var salaryhead = _scSalaryHeadRepository.GetAll().OrderBy(x => x.DisplayOrder);

            var payroll = _pypayrollgenerationRepository.GetMany(
                   x => x.Year == year && x.Month == month && x.BranchId == CurrentBranch.Id);

            var viewModel = base.CreateReportViewModel<PayrollGenerationViewModel>("Payroll Generation For the Month Of " + monthName + " ,Year " + model.Year);
            var selectlist = new List<SelectList>();
            if (!payroll.Any())
            {


                foreach (var empinfo in employeeinfo)
                {

                   
                    decimal amount = 0;


                    // new SelectList(
                    //Enum.GetValues(typeof(SalaryHeadType)).Cast<SalaryHeadType>().Select(
                    //    x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }), "Value", "Text");
                    #region
                    foreach (var head in salaryhead)
                    {

                        switch (head.Head)
                        {
                            case 1:
                                {
                                    var e =
                                        _pyemployeesalarymasterRepository.GetById(
                                            x => x.Status && x.EmployeeId == empinfo.Id && x.BranchId == CurrentBranch.Id);

                                    var payrolldetail = new List<PayrollGenerationDetailAddViewModel>();

                                    if (e != null)
                                    {
                                        amount = e.BasicSalary;

                                    }
                                    else
                                    {
                                        var corp = _pycorporatesalarymasterRepository.GetById(
                                            x =>
                                            x.Status && x.EmployeePostId == empinfo.EmployeePostId &&
                                            x.BranchId == CurrentBranch.Id);
                                        if (corp != null)
                                        {
                                            amount = corp.Salary;

                                        }
                                    }
                                    var payrollgeneration = new PayrollGenerationAddViewModel()
                                                                {
                                                                    BranchId = CurrentBranch.Id,
                                                                    Month = month,
                                                                    MonthMiti = monthmiti,
                                                                    YearMiti = yearmiti,
                                                                    Year = year,
                                                                    FiscalYearId = fiscalyearId,
                                                                    //   EmployeeInfo = empinfo,
                                                                    EmployeeId = empinfo.Id,
                                                                    CreatedBy = user.Id,
                                                                    CreatedDate = DateTime.UtcNow,
                                                                    Amount = amount,
                                                                    PyPayrollGenerationDetails = payrolldetail,
                                                                    SalaryHeadId = Convert.ToInt32(head.Id),
                                                                    Operation = head.Operation
                                                                };


                                    payrollgenerationlist.Add(payrollgeneration);
                                    if (head.Operation == "+")
                                        amount += payrollgeneration.PyPayrollGenerationDetails.Select(x => x.Amount).Sum();
                                    else

                                        amount -= payrollgeneration.PyPayrollGenerationDetails.Select(x => x.Amount).Sum();


                                }
                                break;
                            case 2:
                                {
                                    var yearallowance = new List<string>();
                                    if (model.AnnualId != null)
                                    {
                                        yearallowance = model.AnnualId.Where(x => x.StartsWith("1")).ToList();

                                    }

                                    var e =
                                        _pyemployeesalarymasterRepository.GetById(
                                            x => x.Status && x.EmployeeId == empinfo.Id && x.BranchId == CurrentBranch.Id);

                                    var payrolldetail = new List<PayrollGenerationDetailAddViewModel>();

                                    if (e != null)
                                    {
                                        var predicateOr = PredicateBuilder.False<PyEmployeeSalaryAllowanceMapping>();
                                        predicateOr =
                                            yearallowance.Select(item => Convert.ToInt32(item.Split('.')[1])).Aggregate(
                                                predicateOr,
                                                (current,
                                                 value) =>
                                                current.Or(
                                                    x =>
                                                    x.
                                                        AllowanceId ==
                                                    value));
                                        var predicate = PredicateBuilder.True<PyEmployeeSalaryAllowanceMapping>();
                                        predicate = predicate.And(x => x.EmployeeSalaryMaster.EmployeeId == empinfo.Id);
                                        predicate = predicate.And(predicateOr.Expand());
                                        payrolldetail.AddRange(
                                            e.AllowanceSalaryAllowanceMappings.Where(x => !x.AllowanceMaster.IsAnnual).
                                                Select(item => new PayrollGenerationDetailAddViewModel
                                                                   {
                                                                       ReferenceId = item.AllowanceId,
                                                                       Amount = item.AllowanceMaster.IsFlat
                                                                                    ? item.AllowanceMaster.Value
                                                                                    : item.AllowanceMaster.Value / 100 *
                                                                                      amount
                                                                   }));
                                        var data = _pyemployeeSalaryAllowanceMappingRepository.GetExpandable(predicate);
                                        if (data.Any())
                                        {
                                            payrolldetail.AddRange(
                                                data.ToList().Select(item => new PayrollGenerationDetailAddViewModel
                                                                                 {
                                                                                     ReferenceId = item.AllowanceId,
                                                                                     Amount =
                                                                                         item.AllowanceMaster.
                                                                                             IsFlat
                                                                                             ? item.
                                                                                                   AllowanceMaster
                                                                                                   .Value
                                                                                             : item.
                                                                                                   AllowanceMaster
                                                                                                   .Value / 100 *
                                                                                               amount
                                                                                 }));
                                        }
                                    }
                                    else
                                    {
                                        var corp = _pycorporatesalarymasterRepository.GetById(
                                            x =>
                                            x.Status && x.EmployeePostId == empinfo.EmployeePostId &&
                                            x.BranchId == CurrentBranch.Id);
                                        if (corp != null)
                                        {

                                            var predicateOr = PredicateBuilder.False<PyCorporateAllowanceMapping>();
                                            predicateOr =
                                                yearallowance.Select(item => Convert.ToInt32(item.Split('.')[1])).Aggregate
                                                    (predicateOr,
                                                     (current,
                                                      value) =>
                                                     current.Or(
                                                         x =>
                                                         x.
                                                             AllowanceId ==
                                                         value));

                                            var predicate = PredicateBuilder.True<PyCorporateAllowanceMapping>();
                                            predicate =
                                                predicate.And(
                                                    x => x.CorporateSalaryMaster.EmployeePostId == empinfo.EmployeePostId);
                                            predicate = predicate.And(predicateOr.Expand());
                                            payrolldetail.AddRange(
                                                corp.CorporateAllowanceMappings.Where(x => !x.AllowanceMaster.IsAnnual).
                                                    Select(item => new PayrollGenerationDetailAddViewModel
                                                                       {
                                                                           ReferenceId = item.AllowanceId,
                                                                           Amount = item.AllowanceMaster.IsFlat
                                                                                        ? item.AllowanceMaster.Value
                                                                                        : item.AllowanceMaster.Value / 100 *
                                                                                          amount
                                                                       }));
                                            var data =
                                                _pycorporateAllowanceMappingRepository.GetExpandable(predicate);
                                            if (data.Any())
                                            {
                                                payrolldetail.AddRange(
                                                    data.ToList().Select(item => new PayrollGenerationDetailAddViewModel
                                                                                     {
                                                                                         ReferenceId =
                                                                                             item.AllowanceId,
                                                                                         Amount =
                                                                                             item.AllowanceMaster.
                                                                                                 IsFlat
                                                                                                 ? item.
                                                                                                       AllowanceMaster
                                                                                                       .Value
                                                                                                 : item.
                                                                                                       AllowanceMaster
                                                                                                       .Value /
                                                                                                   100 * amount
                                                                                     }));
                                            }
                                        }
                                    }
                                    var payrollgeneration = new PayrollGenerationAddViewModel()
                                                                {
                                                                    BranchId = CurrentBranch.Id,
                                                                    Month = month,
                                                                    MonthMiti = monthmiti,
                                                                    YearMiti = yearmiti,
                                                                    Year = year,
                                                                    FiscalYearId = fiscalyearId,
                                                                    EmployeeId = empinfo.Id,
                                                                    CreatedBy = user.Id,
                                                                    CreatedDate = DateTime.UtcNow,
                                                                    // EmployeeInfo = empinfo,
                                                                    Amount = payrolldetail.Select(x => x.Amount).Sum(),
                                                                    PyPayrollGenerationDetails = payrolldetail,
                                                                    SalaryHeadId = Convert.ToInt32(head.Id),
                                                                    Operation = head.Operation

                                                                };


                                    payrollgenerationlist.Add(payrollgeneration);
                                    if (head.Operation == "+")
                                        amount += payrollgeneration.Amount;
                                    else
                                        amount -= payrollgeneration.Amount;

                                }
                                break;
                            case 3:
                                {

                                    var yearallowance = new List<string>();
                                    if (model.AnnualId != null)
                                    {
                                        yearallowance = model.AnnualId.Where(x => x.StartsWith("2")).ToList();

                                    }

                                    var deduction =
                                        _pyemployeedeductionmasterRepository.GetById(
                                            x => x.EmployeeId == empinfo.Id && x.Status & x.BranchId == CurrentBranch.Id);
                                    var payrolldetail = new List<PayrollGenerationDetailAddViewModel>();
                                    if (deduction != null)
                                    {

                                        var predicateOr = PredicateBuilder.False<PyEmployeeDeductionMapping>();
                                        predicateOr =
                                            yearallowance.Select(item => Convert.ToInt32(item.Split('.')[1])).Aggregate(
                                                predicateOr,
                                                (current,
                                                 value) =>
                                                current.Or(
                                                    x =>
                                                    x.
                                                        DeductionId ==
                                                    value));
                                        var predicate = PredicateBuilder.True<PyEmployeeDeductionMapping>();
                                        predicate = predicate.And(x => x.EmployeeDeductionMaster.EmployeeId == empinfo.Id);
                                        predicate = predicate.And(predicateOr.Expand());
                                        payrolldetail.AddRange(
                                            deduction.EmployeeDeductionMappings.Where(x => !x.DeductionMaster.IsAnnual).
                                                Select(item => new PayrollGenerationDetailAddViewModel
                                                                   {
                                                                       ReferenceId = item.DeductionId,
                                                                       Amount = item.DeductionMaster.IsFlat
                                                                                    ? item.DeductionMaster.Amount
                                                                                    : item.DeductionMaster.Amount / 100 *
                                                                                      amount
                                                                   }));

                                        var data =
                                            _pyEmployeeDeductionMappingRepository.GetExpandable(predicate);
                                        if (data.Any())
                                        {
                                            payrolldetail.AddRange(
                                                data.ToList().Select(item => new PayrollGenerationDetailAddViewModel
                                                                                 {
                                                                                     ReferenceId =
                                                                                         item.DeductionId,
                                                                                     Amount =
                                                                                         item.DeductionMaster.
                                                                                             IsFlat
                                                                                             ? item.
                                                                                                   DeductionMaster
                                                                                                   .Amount
                                                                                             : item.
                                                                                                   DeductionMaster
                                                                                                   .Amount /
                                                                                               100 * amount
                                                                                 }));
                                        }
                                    }

                                    var payrollgeneration = new PayrollGenerationAddViewModel()
                                                                {
                                                                    BranchId = CurrentBranch.Id,
                                                                    Month = month,
                                                                    MonthMiti = monthmiti,
                                                                    YearMiti = yearmiti,
                                                                    Year = year,
                                                                    FiscalYearId = fiscalyearId,
                                                                    EmployeeId = empinfo.Id,
                                                                    CreatedBy = user.Id,
                                                                    // EmployeeInfo = empinfo,
                                                                    CreatedDate = DateTime.UtcNow,
                                                                    Amount = payrolldetail.Select(x => x.Amount).Sum(),
                                                                    PyPayrollGenerationDetails = payrolldetail,
                                                                    SalaryHeadId = Convert.ToInt32(head.Id),
                                                                    Operation = head.Operation

                                                                };


                                    payrollgenerationlist.Add(payrollgeneration);


                                    if (head.Operation == "+")
                                        amount += payrollgeneration.Amount;
                                    else
                                        amount -= payrollgeneration.Amount;

                                }
                                break;
                            case 4:
                                {
                                    var pf =
                                        _pfEmployeeMappingRepository.GetById(
                                            x =>
                                            x.EmployeeId == empinfo.Id &&
                                            x.PyPfEmployeeMaster.Status & x.PyPfEmployeeMaster.BranchId == CurrentBranch.Id);
                                    var payrolldetail = new List<PayrollGenerationDetailAddViewModel>();
                                    if (pf != null)
                                    {

                                        var item = new PayrollGenerationDetailAddViewModel
                                                       {
                                                           ReferenceId = pf.Id,
                                                           Amount = pf.PyPfEmployeeMaster.IsFlat
                                                                        ? pf.PyPfEmployeeMaster.Value
                                                                        : pf.PyPfEmployeeMaster.Value / 100 * amount
                                                       };
                                        payrolldetail.Add(item);
                                    }

                                    var payrollgeneration = new PayrollGenerationAddViewModel()
                                                                {
                                                                    BranchId = CurrentBranch.Id,
                                                                    Month = month,
                                                                    MonthMiti = monthmiti,
                                                                    YearMiti = yearmiti,
                                                                    Year = year,
                                                                    // EmployeeInfo = empinfo,
                                                                    FiscalYearId = fiscalyearId,
                                                                    EmployeeId = empinfo.Id,
                                                                    CreatedBy = user.Id,
                                                                    CreatedDate = DateTime.UtcNow,
                                                                    Amount = payrolldetail.Select(x => x.Amount).Sum(),
                                                                    PyPayrollGenerationDetails = payrolldetail,
                                                                    SalaryHeadId = Convert.ToInt32(head.Id),
                                                                    Operation = head.Operation
                                                                };


                                    payrollgenerationlist.Add(payrollgeneration);

                                    if (head.Operation == "+")
                                        amount += payrollgeneration.Amount;
                                    else
                                        amount -= payrollgeneration.Amount;
                                }
                                break;
                            case 5:
                                {
                                    var pf =
                                        _pyTaxDeductionEmployeeMappingRepository.GetById(
                                            x =>
                                            x.EmployeeId == empinfo.Id &&
                                            x.PyTaxDeductionPattern.Status &
                                            x.PyTaxDeductionPattern.BranchId == CurrentBranch.Id);
                                    var payrolldetail = new List<PayrollGenerationDetailAddViewModel>();
                                    if (pf != null)
                                    {

                                        var item = new PayrollGenerationDetailAddViewModel
                                                       {
                                                           ReferenceId = pf.Id,
                                                           Amount = pf.PyTaxDeductionPattern.Percentage / 100 * amount
                                                       };
                                        payrolldetail.Add(item);
                                    }

                                    var payrollgeneration = new PayrollGenerationAddViewModel()
                                                                {
                                                                    BranchId = CurrentBranch.Id,
                                                                    Month = month,
                                                                    MonthMiti = monthmiti,
                                                                    YearMiti = yearmiti,
                                                                    Year = year,
                                                                    //EmployeeInfo = empinfo,
                                                                    FiscalYearId = fiscalyearId,
                                                                    EmployeeId = empinfo.Id,
                                                                    CreatedBy = user.Id,
                                                                    CreatedDate = DateTime.Now,
                                                                    Amount = payrolldetail.Select(x => x.Amount).Sum(),
                                                                    PyPayrollGenerationDetails = payrolldetail,
                                                                    SalaryHeadId = Convert.ToInt32(head.Id),
                                                                    Operation = head.Operation

                                                                };


                                    payrollgenerationlist.Add(payrollgeneration);

                                    if (head.Operation == "+")
                                        amount += payrollgeneration.Amount;
                                    else
                                        amount -= payrollgeneration.Amount;
                                }
                                break;

                        }
                    }
                    #endregion



                }
            }
            else
            {
                foreach (var pay in payroll)
                {
                    var col = pay.PyPayrollGenerationDetails;
                    pay.PyPayrollGenerationDetails = null;
                    Mapper.CreateMap<PyPayrollGeneration, PayrollGenerationAddViewModel>();
                    var mo = Mapper.Map<PyPayrollGeneration, PayrollGenerationAddViewModel>(pay);
                    mo.Operation = pay.SalaryHead.Operation;
                    var d = new List<PayrollGenerationDetailAddViewModel>();
                    if (col != null)
                    {
                        foreach (var i in col)
                        {
                            Mapper.CreateMap<PyPayrollGenerationDetail, PayrollGenerationDetailAddViewModel>();
                            var de = Mapper.Map<PyPayrollGenerationDetail, PayrollGenerationDetailAddViewModel>(i);

                            d.Add(de);
                        }
                    }
                    mo.PyPayrollGenerationDetails.Union(d);

                    payrollgenerationlist.Add(mo);
                }



                payrollgenerationlist.FirstOrDefault().IsUse = true;
                msg = "Payroll For This Month Has Already Been Approved";

            }


            viewModel.PayrollGenerations = payrollgenerationlist;
            viewModel.GroupingPayrollGenerations = payrollgenerationlist.GroupBy(x => x.EmployeeId);
            viewModel.SalaryHeadList = salaryhead;

            Session["ReportModel"] = viewModel;
            var view = this.RenderPartialViewToString("_PartialPayrollGenerationDetail", viewModel);
            return Json(new { partialView = view, msg = msg }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SavePayrollGeneration(IEnumerable<PayrollGenerationAddViewModel> PayrollGenerationViewModel, List<string> amount)
        {
            var model = PayrollGenerationViewModel;
            if (model == null) return null;
            var user = _authentication.GetAuthenticatedUser();
            decimal netAmount = 0;
            var refId = 0;
            var employeeId = 0;
            var source = StringEnum.Parse(typeof(ModuleEnum), "Payroll Generation").ToString();
            var Slipno = "";



            foreach (var item in model)
            {
                var a = amount.FirstOrDefault(x => x.Split('/')[0] == item.EmployeeId.ToString());
                netAmount = Convert.ToDecimal(a.Split('/')[1]);
                var salary = _scSalaryHeadRepository.GetById(x => x.Id == item.SalaryHeadId);
                if (employeeId != item.EmployeeId)
                {
                    employeeId = 0;

                    Slipno = UtilityService.GetDocumentNumberingByModuleName(source);
                    item.VNo = Slipno;
                    
                    if (!string.IsNullOrEmpty(Slipno))
                    {
                        var documentnumber = _documentNumeringSchemeRepository.GetById(x => x.ModuleName == source);
                        documentnumber.DocCurrNo += 1;
                        _documentNumeringSchemeRepository.Update(documentnumber);
                    }
                    else
                    {
                         return Content("False");
                    }
                   
                }
                
                var detail = item.PyPayrollGenerationDetails;
                item.PyPayrollGenerationDetails = null;
                item.CreatedBy = user.Id;
                item.CreatedDate = DateTime.Now;
                item.VNo = Slipno;
                item.AcademicYearId = this.AcademicYear.Id;
                Mapper.CreateMap<PayrollGenerationAddViewModel, PyPayrollGeneration>();
                var mo = Mapper.Map<PayrollGenerationAddViewModel, PyPayrollGeneration>(item);


               
                _pypayrollgenerationRepository.Add(mo);
                refId = mo.Id;

                if (item.Amount != 0)
                {
                    var accountTransactionDetail = new AccountTransaction();

                    if (salary.Operation == "+")
                    {

                        accountTransactionDetail.DrAmt = item.Amount;
                        accountTransactionDetail.LocalDrAmt = item.Amount;
                        accountTransactionDetail.LocalCrAmt = 0;
                        accountTransactionDetail.CrAmt = 0;
                    }
                    else
                    {

                        accountTransactionDetail.DrAmt = 0;
                        accountTransactionDetail.LocalDrAmt = 0;
                        accountTransactionDetail.LocalCrAmt = item.Amount;
                        accountTransactionDetail.CrAmt = item.Amount;
                    }

                    accountTransactionDetail.VNo = Slipno;
                    accountTransactionDetail.VDate = DateTime.UtcNow.Date;
                    accountTransactionDetail.GlCode = salary.LedgerId;
                    accountTransactionDetail.SlCode = 0;
                    accountTransactionDetail.ReferenceId = refId;
                    accountTransactionDetail.Narration = "";
                    accountTransactionDetail.Source = source;
                    accountTransactionDetail.SNo = 1;
                    accountTransactionDetail.CreatedById = 1;
                    accountTransactionDetail.FYId = FiscalYear.Id;
                    accountTransactionDetail.BranchId = base.CurrentBranch.Id;
                    _accountTransactionRepository.Add(accountTransactionDetail);


                }


                if (detail != null)
                {
                    foreach (var i in detail)
                    {


                        Mapper.CreateMap<PayrollGenerationDetailAddViewModel, PyPayrollGenerationDetail>();
                        var de = Mapper.Map<PayrollGenerationDetailAddViewModel, PyPayrollGenerationDetail>(i);
                        de.PayrollGenerationId = mo.Id;
                        _pypayrollgenerationdetailRepository.Add(de);



                    }
                }
                var ii = model.FirstOrDefault(x => x.EmployeeId == item.EmployeeId);
                if (ii != null && employeeId == 0 && netAmount != 0)
                {
                    employeeId = ii.EmployeeId;
                    var employee = _scEmployeeInfoRepository.GetById(x => x.Id == ii.EmployeeId);
                    var accountTransaction = new AccountTransaction();
                    accountTransaction.VNo = Slipno;
                    accountTransaction.VDate = DateTime.UtcNow.Date;
                    accountTransaction.GlCode = employee.LedgerId;
                    accountTransaction.SlCode = 0;
                    accountTransaction.ReferenceId = refId;
                    if (netAmount < 0)
                    {
                        var namount = Math.Abs(netAmount);
                        accountTransaction.DrAmt = namount;
                        accountTransaction.LocalDrAmt = namount;
                        accountTransaction.LocalCrAmt = 0;
                        accountTransaction.CrAmt = 0;
                    }
                    else
                    {
                        accountTransaction.DrAmt = 0;
                        accountTransaction.LocalDrAmt = 0;
                        accountTransaction.LocalCrAmt = netAmount;
                        accountTransaction.CrAmt = netAmount;
                    }

                    accountTransaction.Narration = "";
                    accountTransaction.Source = source;
                    accountTransaction.SNo = 1;
                    accountTransaction.CreatedById = 1;
                    accountTransaction.FYId = FiscalYear.Id;
                    accountTransaction.BranchId = base.CurrentBranch.Id;
                    _accountTransactionRepository.Add(accountTransaction);

                    var transaction = new EmployeeTransaction()
                                          {
                                              NetAmount = netAmount,
                                              Month = item.Month,
                                              MonthMiti = item.MonthMiti,
                                              CreateById = user.Id,
                                              CreatedDate = DateTime.Now,
                                              ReferenceId = refId,
                                              Source = source,
                                              VNo = Slipno,
                                              Year = item.Year,
                                              YearMiti = item.YearMiti,
                                              AcademicYearId = this.AcademicYear.Id,
                                              BranchId = this.CurrentBranch.Id,
                                              EmployeeId = item.EmployeeId
                                          };
                    _employeeTransactionRepository.Add(transaction);

                }
                //if (employeeId != item.EmployeeId)
                //{
                //    var documentnumber = _documentNumeringSchemeRepository.GetById(x => x.ModuleName == source);
                //    documentnumber.DocCurrNo += 1;
                //    _documentNumeringSchemeRepository.Update(documentnumber);
                //}
            }


            return null;
        }
        public ActionResult PdfPayrollGenerationReport()
        {
            var view = (PayrollGenerationViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(842, 595);

            //var floatWidths = new List<FloatWidth>();
            //var floatWidthHeader = new FloatWidth();
            //floatWidthHeader.Width = FloatList("1");
            //floatWidths.Add(floatWidthHeader);
            //var floatWidthBody = new FloatWidth();
            //floatWidthBody.Width = FloatList(".15,.15,.15,.1,.075,.075,.1,.1,.1");
            //floatWidths.Add(floatWidthBody);

            return this.ViewPdf("", "PdfPayrollGeneration", view, pageSize);
        }
        public List<float> FloatList(string floatStr)
        {
            var list = floatStr.Split(',').ToList();

            return list.Select(item => (float)Convert.ToDecimal(item)).ToList();
        }

        #endregion
        #region Payment Slip
         [CheckPermission(Permissions = "Navigate", Module = "Pyps")]
        public ActionResult PaymentSlips()
        {
            ViewBag.UserRight = base.UserRight("Pyps");
            return View();
        }
         [CheckPermission(Permissions = "Navigate", Module = "Pyps")]
        public ActionResult PaymentSlipList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("Pyps");
            var list = _pypaymentslipRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
             ViewBag.DateType = this.SystemControl.DateType;
            return PartialView("_PartialPaymentSlipList", list.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
         [CheckPermission(Permissions = "Create", Module = "Pyps")]
        public ActionResult PaymentSlipAdd()
        {
            var viewModel = new PayrollPaymentSlipViewModel();
            var dateype = base.SystemControl.DateType;

            var year = 0;
            if (dateype == 1)
            {
                viewModel.Year = DateTime.Now.Year;
                viewModel.Month = DateTime.Now.Month;
                var len = viewModel.Month.ToString();
                if (viewModel.Month <= 9)
                {
                    len = "0" + viewModel.Month.ToString();
                }

                viewModel.MonthList = new SelectList(DropDownHelper.CreateMonthDate().OrderBy(x => x.Value), "Value", "Text", len);



            }
            else
            {
                viewModel.Year = DateTime.Now.Year;
                viewModel.Month = DateTime.Now.Month;
                var len = viewModel.Month.ToString();
                if (viewModel.Month <= 9)
                {
                    len = "0" + viewModel.Month.ToString();
                }

                viewModel.Year = NepaliDateService.NepaliDate(DateTime.Now).Year;
                viewModel.Month = NepaliDateService.NepaliDate(DateTime.Now).Month;
                viewModel.MonthList = new SelectList(DropDownHelper.CreateMonthMiti().OrderBy(x => x.Value), "Value", "Text", len);

            }
            var source = StringEnum.Parse(typeof(KRBAccounting.Enums.ModuleEnum), "Payroll Payment Slip Number").ToString();
            var Slipno = UtilityService.GetDocumentNumberingByModuleName(source);

            viewModel.DepartmentList = new SelectList(_employeeDepartmentRepository.GetMany(x => x.Status), "Id",
                                                      "Description").ToList();
             viewModel.DepartmentList.Insert(0,new SelectListItem{Selected = true, Text = "--- Select Department ---",Value = "0"});

            viewModel.EmployeeList = new SelectList("", "Id", "Description");

            viewModel.SlipNo = Slipno;
            if (!string.IsNullOrEmpty(Slipno))
            {
                var documentnumber = _documentNumeringSchemeRepository.GetById(x => x.ModuleName == source);
                documentnumber.DocCurrNo += 1;
                _documentNumeringSchemeRepository.Update(documentnumber);
            }
             return PartialView("_PartialPaymentSlipAdd", viewModel);
        }
        [HttpPost]
        public ActionResult PaymentSlipAdd(PayrollPaymentSlipViewModel model)
        {
            var datetype = base.SystemControl.DateType;
            var year = 0;
            var yearmiti = 0;
            var month = 0;
            var monthmiti = 0;
            var msg = ""; string monthName = "";

            if (datetype == 1)
            {
                year = model.Year;
                month = model.Month;
                var date = month + "/01/"   + year;
                var datemiti = NepaliDateService.NepaliDate(Convert.ToDateTime(date));
                yearmiti = datemiti.Year;
                monthmiti = datemiti.Month;
            }
            else
            {
                yearmiti = model.Year;
                monthmiti = model.Month;
                var nepdate = yearmiti + "/" + monthmiti + "/01";
                DateTime date = NepaliDateService.NepalitoEnglishDate(nepdate);
                year = date.Year;
                month = date.Month;
            }
            var source = StringEnum.Parse(typeof(KRBAccounting.Enums.ModuleEnum), "Payroll Payment Slip Number").ToString();
            var user = _authentication.GetAuthenticatedUser();
            var payslip = new PyPaymentSlip()
                              {
                                  CreateDate = DateTime.Now,

                                  LedgerId = model.Ledger,
                                  Month = month,
                                  MonthMiti = monthmiti,
                                  NetAmount = model.PaymentAmount,
                                  Year = year,
                                  YearMiti = yearmiti,
                                  SlipNo = model.SlipNo,
                                  CreatedById = user.Id,
                                  Remark = model.Remarks,
                                  EmployeeId = model.EmployeeId,
                                  
                              };
            _pypaymentslipRepository.Add(payslip);


            var accountTransaction = new AccountTransaction();
            accountTransaction.VNo = payslip.SlipNo;
            accountTransaction.VDate = DateTime.UtcNow.Date;
            accountTransaction.GlCode = this.SystemControl.CashBook;
            accountTransaction.SlCode = 0;
            accountTransaction.ReferenceId = payslip.Id;

            accountTransaction.DrAmt = 0;
            accountTransaction.LocalDrAmt = 0;
            accountTransaction.LocalCrAmt = model.PaymentAmount;
            accountTransaction.CrAmt = model.PaymentAmount;
            accountTransaction.Narration = "";
            accountTransaction.Source = source;
            accountTransaction.SNo = 1;
            accountTransaction.CreatedById = 1;
            accountTransaction.FYId = FiscalYear.Id;
            accountTransaction.BranchId = base.CurrentBranch.Id;
            _accountTransactionRepository.Add(accountTransaction);

            var accountTransaction1 = new AccountTransaction();
            accountTransaction1.VNo = payslip.SlipNo;
            accountTransaction1.VDate = DateTime.UtcNow.Date;
            accountTransaction1.GlCode = model.Ledger;
            accountTransaction1.SlCode = 0;
            accountTransaction1.ReferenceId = payslip.Id;

            accountTransaction1.DrAmt = model.PaymentAmount;
            accountTransaction1.LocalDrAmt = model.PaymentAmount;
            accountTransaction1.LocalCrAmt = 0;
            accountTransaction1.CrAmt = 0;
            accountTransaction1.Narration = "";
            accountTransaction1.Source = source;
            accountTransaction1.SNo = 1;
            accountTransaction1.CreatedById = 1;
            accountTransaction1.FYId = FiscalYear.Id;
            accountTransaction1.BranchId = base.CurrentBranch.Id;
            _accountTransactionRepository.Add(accountTransaction1);
            var transaction = new EmployeeTransaction()
            {
                NetAmount = model.PaymentAmount,
                Month = month,
                MonthMiti = monthmiti,
                CreateById = user.Id,
                CreatedDate = DateTime.Now,
                ReferenceId = payslip.Id,
                Source = source,
                VNo = payslip.SlipNo,
                Year = year,
                YearMiti = yearmiti,
                AcademicYearId = this.AcademicYear.Id,
                BranchId = this.CurrentBranch.Id,
                EmployeeId = model.EmployeeId
            };
            _employeeTransactionRepository.Add(transaction);


            var printfee = new PaymentSlipPrintViewModel()
            {
                SlipNo = model.SlipNo,
                Amount = model.PaymentAmount.ToStr(),
                Date = DateTime.Now.ToStr(),
                InWords = NumberToEnglish.changeCurrencyToWords(model.PaymentAmount.ToString()),
                Name = _scEmployeeInfoRepository.GetById(x=>x.Id == model.EmployeeId).Description,
                PaymentFor = "Salary",
                ReceivedBy = user.Username,
               



            };
            return Json(new { True = "True", Print = printfee }, JsonRequestBehavior.AllowGet);
       

        }
        [HttpPost]
        public ActionResult GetPayrollEmployee(int month, int year, int departmentId)
        {
            var employee = _scEmployeeInfoRepository.GetMany(x => x.EmployeeDepartmentId == departmentId).Select(x => new
                                                                                                                     {
                                                                                                                         Value = x.Id,
                                                                                                                         Text = x.Description
                                                                                                                     });


            var dateype = base.SystemControl.DateType;
            var msg = "";
            //if (dateype == 1)
            //{
            //    var data =
            //        _pypayrollgenerationRepository.GetMany(
            //            x =>
            //            x.Year == year && x.BranchId == CurrentBranch.Id && x.Month == month &&
            //            x.EmployeeInfo.EmployeeDepartmentId == departmentId);
            //    if (data != null)
            //    {
            //        var d = data.GroupBy(x => x.EmployeeId).Select(x =>
            //        {
            //            var pyPayrollGeneration = x.FirstOrDefault();
            //            return pyPayrollGeneration != null ? pyPayrollGeneration.EmployeeInfo : null;
            //        }).ToList();
            //        employee = employee.Union(d).ToList();
            //    }
            //}
            //else
            //{


            //    var data = _pypayrollgenerationRepository.GetMany(x => x.YearMiti == year && x.BranchId == CurrentBranch.Id && x.MonthMiti == month && x.EmployeeInfo.EmployeeDepartmentId == departmentId);
            //    if (data != null)
            //    {

            //        var d = data.GroupBy(x => x.EmployeeId).Select(x =>
            //        {
            //            var pyPayrollGeneration = x.FirstOrDefault();
            //            return pyPayrollGeneration != null ? pyPayrollGeneration.EmployeeInfo : null;
            //        }).ToList();
            //        employee = employee.Union(d).ToList();
            //    }
            //}
            //var view = "";


            //if (employee.Any())
            //{
            //    view = this.RenderPartialViewToString("_PartialEmployee", employee);
            //}
            //else
            //{
            //    msg = "Salary For ths Month and Department Has Not Been Generated";
            //}
            return Json(employee, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetEmployeeLedger(int employeeId)
        {
            var employee = _scEmployeeInfoRepository.GetById(x => x.Id == employeeId);
            var context = new DataContext();
            var balance = context.Database.SqlQuery<decimal?>("Select SUM(DrAmt)-SUM(CrAmt) from AccountTransaction where GlCode = " +
                                                employee.LedgerId);

            var ledger = "";
            if (employee.Ledger!=null)
            {
                ledger = employee.Ledger.AccountName;
            }
            return Json(new { ledger = ledger, LedgerId = employee.LedgerId, Balance = balance },
                        JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}