using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
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
using KRBAccounting.Web.ViewModels.Academy;
using KRBAccounting.Web.ViewModels.School;
using KRBAccounting.Web.ViewModels.Payroll;
using KRBAccounting.Web.Helpers;


namespace KRBAccounting.Web.Controllers
{
    [CheckModulePermission("EmployeeManagement_Academy")]
    [Authorize]
    public class EmployeeManagementController : BaseController
    {
        private readonly IScEmployeePostRepository _scemployeepostRepository;
        private readonly IScSubjectRepository _subjectRepository;
        private readonly IFormsAuthenticationService _authentication;
        private readonly IScEmployeeCategoryRepository _scemployeecategoryRepository;
        private readonly IScEmployeeInfoRepository _scEmployeeInfoRepository;
        public readonly IScStudentRegistrationRepository _scStudentRegistrationRepository;
        public readonly IScStudentRegistrationDetailRepository _scStudentRegistrationDetailRepository;
        public readonly IStudentExtraActivityRepository _StudentExtraActivityRepository;
        private readonly IScSalaryHeadRepository _scsalaryheadRepository;
        private readonly IScStaffAttendanceRepository _scstaffattendanceRepository;
        private readonly IScStaffSubjectMappingRepository _scstaffsubjectmappingRepository;
        private readonly IClassScheduleDetailRepository _classscheduledetailRepository;
        private readonly IScClassScheduleRepository _scclassscheduleRepository;
        private readonly IScTeacherScheduleRepository _scteacherscheduleRepository;
        private readonly IScClassRepository _scClassRepository;
        private readonly IScSectionRepository _scSectionRepository;
        private readonly IScEmployeeDepartmentRepository _scemployeedepartmentRepository;
        private readonly ISystemControlRepository _systemControlRepository;
        private readonly IScClassTeacherMappingRepository _classTeacherMapping;
        private readonly IUserRepository _user;
        private readonly IRoleRepository _role;
        private readonly ISecurityService _securityService;
        private readonly IPyPaymentSlipRepository _pypaymentslipRepository;
        private readonly IPyPayrollGenerationRepository _pypayrollgenerationRepository;

        private int CurrentAcademyYearId;
        #region Constructor

        public EmployeeManagementController(IScEmployeeDepartmentRepository scemployeedepartmentRepository,
            IPyPayrollGenerationRepository pypayrollgenerationRepository,
            IPyPaymentSlipRepository pypaymentslipRepository,
            IScEmployeePostRepository scemployeepostRepository,
            IScStaffAttendanceRepository scstaffattendanceRepository,
            IScProgramMasterRepository scprogrammasterRepository,
            IScConsolidatedMarksSetupRepository scconsolidatedmarkssetupRepository,
            ISchBuildingRepository schBuildingRepository,
IUserRepository user,
         IRoleRepository role,
       ISecurityService securityService,

                                IScSubjectRepository subjectRepository,
                               IScEmployeeCategoryRepository scemployeecategoryRepository,
                                IScEmployeeInfoRepository scEmployeeInfoRepository,
                              IScStudentRegistrationRepository studentRegistrationRepository,
                                IScStudentRegistrationDetailRepository StudentRegistrationDetail,
                                IStudentExtraActivityRepository studentExtraActivityRepository,
            IScSalaryHeadRepository scsalaryheadRepository,
            IScStaffSubjectMappingRepository scStaffSubjectMappingRepository,
            IClassScheduleDetailRepository classScheduledetailRepository,
             IScClassScheduleRepository classScheduleRepository,
            IScTeacherScheduleRepository teacherScheduleRepository,
            IScClassRepository scClassRepository,
            IScSectionRepository sectionRepository,
            ISystemControlRepository systemControlRepository,
            IScClassTeacherMappingRepository classTeacherMapping


            )
        {
            _pypayrollgenerationRepository = pypayrollgenerationRepository;
            _pypaymentslipRepository = pypaymentslipRepository;
            _user = user;
            _securityService = securityService;
            _role = role;
            _systemControlRepository = systemControlRepository;
            _subjectRepository = subjectRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Payroll);
            _scstaffsubjectmappingRepository = scStaffSubjectMappingRepository;
            _scemployeecategoryRepository = scemployeecategoryRepository;
            _scEmployeeInfoRepository = scEmployeeInfoRepository;
            _scStudentRegistrationRepository = studentRegistrationRepository;
            _scStudentRegistrationDetailRepository = StudentRegistrationDetail;
            _StudentExtraActivityRepository = studentExtraActivityRepository;
            _scsalaryheadRepository = scsalaryheadRepository;
            CurrentAcademyYearId = AcademicYear.Id;
            _scstaffattendanceRepository = scstaffattendanceRepository;
            _classscheduledetailRepository = classScheduledetailRepository;
            _scclassscheduleRepository = classScheduleRepository;
            _scteacherscheduleRepository = teacherScheduleRepository;
            _scClassRepository = scClassRepository;
            _scSectionRepository = sectionRepository;
            _scemployeedepartmentRepository = scemployeedepartmentRepository;
            _scemployeepostRepository = scemployeepostRepository;
            _classTeacherMapping = classTeacherMapping;

        }

        #endregion
        public ActionResult DashBoard()
        {
            var viewmmodel = new DashboardViewModel();
            var context=new DataContext();
            viewmmodel.EmployeePosts = (from c in context.ScEmployeePost
                        group c by c.EmployeeCategoryId into grouping
                        select new EmployeePostViewModel {Description=grouping.FirstOrDefault().EmployeeCategory.Description,Count=grouping.Count()}).ToList();

            viewmmodel.MonthlySalaries = (from c in context.PyPaymentSlip
                        group c by c.Month into grouping
                                          select new MonthlySalaryViewModel { Month = grouping.FirstOrDefault().Month, Totalamt = grouping.Sum(x => x.NetAmount) }).ToList();
            viewmmodel.SalaryPaid = new List<decimal>();
            viewmmodel.SalaryGeneration=new List<decimal>();
            for (int i = 1; i <= 12; i++)
            {
                var paid = _pypaymentslipRepository.GetMany(x => x.Month == i).Sum(x => x.NetAmount);
                viewmmodel.SalaryPaid.Add((decimal)paid);
            }
            for (int i = 1; i <= 12; i++)
            {
                var generation = _pypayrollgenerationRepository.GetMany(x => x.Month == i && x.BranchId == CurrentBranch.Id).Sum(x=>x.Amount);
                viewmmodel.SalaryGeneration.Add((decimal)generation);
            }
                //for (int i = 1; i <= 12; i++)
                //{
                //    var paid =
                //        _employeeSalaryPaymentHistoryRepository.GetMany(x => x.Month == i && (x.ApprovedBy != null && x.ApprovedBy != 0) && x.EmployeeMaster.BranchId == CurrentBranch.Id).Sum(x => x.ESPH_SalaryReceived);

                //    viewmodel.SalaryPaid.Add((decimal)paid);
                //    var unpaid =
                //        _employeeSalaryPaymentHistoryRepository.GetMany(x => x.Month == i && (x.ApprovedBy == null || x.ApprovedBy == 0) && x.EmployeeMaster.BranchId == CurrentBranch.Id).Sum(x => x.ESPH_SalaryReceived);

                //    viewmodel.SalaryUnPaid.Add((decimal)unpaid);
                //}
                return View(viewmmodel);
        }

        #region StaffAttendances
        [CheckPermission(Permissions = "Navigate", Module = "ScHRMSA")]
        public ActionResult StaffAttendances()
        {
            

            var viewModel = new StaffAttendanceFormViewModel();
            viewModel.EmployeeDepartmentList = new SelectList(_scemployeedepartmentRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            viewModel.EmployeeDepartmentList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StaffAttendances(StaffAttendanceFormViewModel model, IEnumerable<ScEmployeeInfo> attendanceEntry)
        {

            foreach (var item in attendanceEntry)
            {
                for (var i = 0; i <= item.Date.Count() - 1; i++)
                {
                    var check = item.CheckList[i];
                    var date = item.Date[i];


                    _scstaffattendanceRepository.Delete(
                        x =>
                        x.AcademicYearId == CurrentAcademyYearId &&
                         x.StaffId == item.Id && x.Date == date);
                    var status = "A";
                    if (check)
                    {
                        status = "P";

                    }
                    var data = new ScStaffAttendance()
                    {
                        Date = date,
                        CreatedDate = DateTime.Now,
                        StaffId = item.Id,
                        Status = status,
                        AcademicYearId = CurrentAcademyYearId


                    };
                    _scstaffattendanceRepository.Add(data);
                }
            }
            return null;
        }
        public ActionResult StaffAttendancesDetail(int departmentId, int year, int month)
        {
            IEnumerable<ScEmployeeInfo> staffs = departmentId == 0
                                                     ? _scEmployeeInfoRepository.GetAll()
                                                     : _scEmployeeInfoRepository.GetMany(
                                                         x => x.EmployeeDepartmentId == departmentId);

            if (year == 0)
            {
                year = DateTime.Today.Year;
            }
            if (month == 0)
            {
                month = DateTime.Today.Month;
            }
            var MonthCount = DateTime.DaysInMonth(year, month);
            var data = new StaffAttendanceDateViewModel();
            data.ScEmployeeInfos = staffs.ToList();
            data.Month = month;
            data.MonthCount = MonthCount;
            data.Year = year;

            data.AcademicId = CurrentAcademyYearId;

            return PartialView("_PartialStaffAttendancesDetail", data);
        }


        #endregion
        #region EmployeeCategories

        public JsonResult Getemployeecategories()
        {

            List<ScEmployeeCategory> lstClasses =
                _scemployeecategoryRepository.GetAll().OrderBy(x => x.Description).ToList();

            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "EMPCa")]
        public ActionResult EmployeeCategories()
        {
            ViewBag.UserRight = base.UserRight("EMPCa");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "EMPCa")]
        public ActionResult EmployeeCategoryAdd()
        {
            var model = new ScEmployeeCategory();
            return PartialView("_PartialEmployeeCategoryAdd", model);
        }

        [HttpPost]
        public ActionResult EmployeeCategoryAdd(ScEmployeeCategory model)
        {
            var user = _authentication.GetAuthenticatedUser();
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.UtcNow;
                model.CreatedById = user.Id;
                model.UpdatedDate = DateTime.UtcNow;
                model.UpdatedById = user.Id;
                _scemployeecategoryRepository.Add(model);
                return Content("True");
            }
            else
            {
                return Content("False");
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "ScHREMPCaMSA")]
        public ActionResult EmployeeCategoryEdit(int id)
        {

            return PartialView("_PartialEmployeeCategoryEdit", _scemployeecategoryRepository.GetById(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult EmployeeCategoryEdit(ScEmployeeCategory model)
        {
            var user = _authentication.GetAuthenticatedUser();

            model.UpdatedDate = DateTime.UtcNow;
            model.UpdatedById = user.Id;


            _scemployeecategoryRepository.Update(model);
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "EMPCa")]
        public ActionResult EmployeeCategoryList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("EMPCa");
            
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart; var lstStaffGroups = _scemployeecategoryRepository.GetPagedList(x => x.Id, pageNo, SystemControl.PageSize);
            return PartialView("_PartialEmployeeCategoryList", lstStaffGroups);
        }

        public ActionResult EmployeeCategoryDelete(int id)
        {
            var data = _scemployeecategoryRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcademyEmployeeCategoryCodeExists(string Code, int Id)
        {
            var feeterm = new ScEmployeeCategory();
            if (Id != 0)
            {
                var data = _scemployeecategoryRepository.GetById(x => x.Id == Id);
                if (data.Code != Code)
                {
                    feeterm = _scemployeecategoryRepository.GetById(x => x.Code == Code);

                }
            }
            else
            {
                feeterm = _scemployeecategoryRepository.GetById(x => x.Code == Code);
            }
            if (feeterm == null)
            {
                feeterm = new ScEmployeeCategory();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyEmployeeCategoryDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScEmployeeCategory();
            if (Id != 0)
            {
                var data = _scemployeecategoryRepository.GetById(x => x.Id == Id);
                if (data.Description != Description)
                {
                    feeterm = _scemployeecategoryRepository.GetById(x => x.Description == Description.Trim());

                }
            }
            else
            {
                feeterm = _scemployeecategoryRepository.GetById(x => x.Description == Description);
            }
            if (feeterm == null)
            {
                feeterm = new ScEmployeeCategory();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        #endregion employeecategorys

        #region EmployeeInfos
        public JsonResult GetEmployeeInfo(int id, int nextid, string childrenlist)
        {
            //var data = new DataContext();
            //var lstClasses =_scEmployeeInfoRepository.GetAll().OrderBy(x => x.Description).ToList();

            var lis = new List<int>();

            // var employee = new List<ScEmployeeInfo>();

            var mapped = _classTeacherMapping.GetMany(x => x.ClassId == id && x.SectionId == nextid).Select(x => x.TeacherId);
            var employee = _scEmployeeInfoRepository.GetAll().Select(x => new 
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            }).Where(x => !mapped.Contains(x.Id)).OrderBy(x => x.Description).ToList();
            if (!string.IsNullOrEmpty(childrenlist))
            {
                var li = childrenlist.Split(',');
                foreach (var s in li)
                {
                    lis.Add(s.ToInt32());
                }

                 var employeee = employee.Where(x => !lis.Contains(x.Id)).OrderBy(x => x.Description).ToList();

                return Json(employeee, JsonRequestBehavior.AllowGet);
            }


            return Json(employee, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployeeInfoForClassTeacher(int id, int nextid)
        {

            var lstClasses =
                _classTeacherMapping.GetMany(x => x.ClassId == id && x.SectionId == nextid).Select(x => new
            {
                Id = x.Id,
                Code = (x.ScEmployeeInfo != null) ? x.ScEmployeeInfo.Code : null,
                Description = (x.ScEmployeeInfo != null) ? x.ScEmployeeInfo.Description : null
            });
            return Json(lstClasses, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "EI")]
        public ActionResult EmployeeInfos()
        {
            ViewBag.UserRight = base.UserRight("EI");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "EI")]
        public ActionResult EmployeeInfoAdd()
        {
            var model = new ScEmployeeInfo();
            if (base.SystemControl.IsCurrDate)
            {
                model.DisplayJoinDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;

            }
            model.SystemControl = _systemControlRepository.GetAll().FirstOrDefault();
            model.CategoryList = new SelectList(_scemployeecategoryRepository.GetMany(x => x.Status).OrderBy(x => x.Description).ToList(), "Id",
                                             "Description");
            model.DepartmentList = new SelectList(_scemployeedepartmentRepository.GetMany(x => x.Status).OrderBy(x => x.Description).ToList(), "Id", "Description");
            model.PositionList = new SelectList(_scemployeepostRepository.GetMany(x => x.Status).OrderBy(x => x.Description).ToList(), "Id", "Description");

            model.StatusList =
            new SelectList(
                   Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().Select(
                       x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");

            model.WeekholidayList =
           new SelectList(
                  Enum.GetValues(typeof(Weekholidayenum)).Cast<Weekholidayenum>().Select(
                      x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");



            return PartialView("_PartialEmployeeInfoAdd", model);
        }

        [HttpPost]
        public ActionResult EmployeeInfoAdd(ScEmployeeInfo model)
        {
            var user = _authentication.GetAuthenticatedUser();
            if (ModelState.IsValid)
            {
                var usrId = 0;
                if (base.SystemControl.DateType == 1)
                {
                    if (!string.IsNullOrEmpty(model.DisplayJoinDate))
                    {
                        model.DateOfJoin = Convert.ToDateTime(model.DisplayJoinDate);
                        model.MitiofJoin = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayJoinDate)).Date;
                    }



                }
                else
                {

                    if (!string.IsNullOrEmpty(model.DisplayJoinDate))
                    {
                        model.MitiofJoin = model.DisplayJoinDate;
                        model.DateOfJoin = NepaliDateService.NepalitoEnglishDate(model.MitiofJoin);
                    }

                }
                if (_securityService.IsUserNameAvailable(model.UserName))
                {
                    var usr = new User();
                    usr.IsDeleted = false;
                    usr.LastLoginDate = DateTime.UtcNow;
                    usr.LastLoginIp = string.Empty;
                    usr.Password = PasswordHelper.HashPassword(model.UserName);
                    usr.UpdatedDate = DateTime.UtcNow;
                    usr.CreatedDate = DateTime.UtcNow;
                    usr.CompanyId = this.CompanyInfo.Id;
                    usr.FullName = model.Description;
                    usr.EmailAddress = model.Email;

                    usr.Username = model.UserName;
                    usr.BranchId = !SystemControl.EnableBranch ? this.CompanyInfo.Id : this.CurrentBranch.Id;

                    usr.IsActive = true;
                    _user.Add(usr);
                    usrId = usr.Id;
                    var roleModel = _role.GetById(x => x.RoleName.ToLower() == "teacher");

                    if (roleModel != null)
                    {
                        var role = new List<int>();
                        role.Add(roleModel.Id);
                        _securityService.AssignRole(usr.Id, role);
                    }
                }
                else
                {
                    return Content("User name already exists. Please enter a different user name.");
                }



                model.CreatedDate = DateTime.UtcNow;
                model.CreatedById = user.Id;
                model.UpdatedDate = DateTime.UtcNow;
                model.UpdatedById = user.Id;
                model.BranchId = base.CurrentBranch.Id;
                model.userId = usrId;
                _scEmployeeInfoRepository.Add(model);



                return Content("True");
            }
            else
            {
              return Content(  ErrorController.Validated(ModelState.Values));
               
               // return Content("False");
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "EI")]
        public ActionResult EmployeeInfoEdit(int id)
        {
            ScEmployeeInfo objScEmployeeInfo = _scEmployeeInfoRepository.GetById(x => x.Id == id);

            objScEmployeeInfo.SystemControl = _systemControlRepository.GetAll().FirstOrDefault();
            if (base.SystemControl.DateType == 1)
            {
                if (objScEmployeeInfo.DateOfJoin != null)
                {
                    objScEmployeeInfo.DisplayJoinDate = Convert.ToDateTime(objScEmployeeInfo.DateOfJoin).ToString("MM/dd/yyyy");

                }

            }
            else
            {
                if (!string.IsNullOrEmpty(objScEmployeeInfo.MitiofJoin))
                {
                    objScEmployeeInfo.DisplayJoinDate = objScEmployeeInfo.MitiofJoin;

                }
            }

            objScEmployeeInfo.CategoryList = new SelectList(_scemployeecategoryRepository.GetMany(x => x.Status).OrderBy(x => x.Description).ToList(), "Id",
                                   "Description", objScEmployeeInfo.EmployeeCategoryId);
            objScEmployeeInfo.DepartmentList =
                new SelectList(
                    _scemployeedepartmentRepository.GetMany(x => x.Status).OrderBy(x => x.Description).ToList(), "Id",
                    "Description", objScEmployeeInfo.EmployeeDepartmentId);
            objScEmployeeInfo.PositionList =
                new SelectList(_scemployeepostRepository.GetMany(x => x.Status).OrderBy(x => x.Description).ToList(),
                               "Id", "Description", objScEmployeeInfo.EmployeePostId);
            objScEmployeeInfo.StatusList =
            new SelectList(
                   Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().Select(
                       x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text", objScEmployeeInfo.Status);

            objScEmployeeInfo.WeekholidayList =
           new SelectList(
                  Enum.GetValues(typeof(Weekholidayenum)).Cast<Weekholidayenum>().Select(
                      x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text", objScEmployeeInfo.Weekholiday);
            return PartialView("_PartialEmployeeInfoEdit", objScEmployeeInfo);
        }

        [HttpPost]
        public ActionResult EmployeeInfoEdit(ScEmployeeInfo model)
        {
            var user = _authentication.GetAuthenticatedUser();
            ScEmployeeInfo objScEmployeeInfo = _scEmployeeInfoRepository.GetById(x => x.Id == model.Id);

            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayJoinDate))
                {
                    model.DateOfJoin = Convert.ToDateTime(model.DisplayJoinDate);
                    model.MitiofJoin = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayJoinDate)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayJoinDate))
                {
                    model.MitiofJoin = model.DisplayJoinDate;
                    model.DateOfJoin = NepaliDateService.NepalitoEnglishDate(model.MitiofJoin);
                }

            }


            model.UpdatedDate = DateTime.UtcNow;
            model.UpdatedById = user.Id;
            model.BranchId = base.CurrentBranch.Id;
            model.CreatedById = objScEmployeeInfo.CreatedById;
            model.CreatedDate = objScEmployeeInfo.CreatedDate;
            model.userId = objScEmployeeInfo.userId;
            var _dataContext = new DataContext();
            _dataContext.Entry(model).State = EntityState.Modified;
            _dataContext.SaveChanges();
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "EI")]
        public ActionResult EmployeeInfosList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("EI");

            var lstEmployeeInfos = _scEmployeeInfoRepository.GetAll().OrderByDescending(x => x.Id);
            //var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            //ViewBag.SnStart = snStart;
            return PartialView("_PartialEmployeeInfoList", lstEmployeeInfos);
        }
           [CheckPermission(Permissions = "Delete", Module = "EI")]
        public ActionResult EmployeeInfoDelete(int EmployeeInfoId)
        {
            ScEmployeeInfo maaster = _scEmployeeInfoRepository.GetById(EmployeeInfoId);
            var data = _scEmployeeInfoRepository.DeleteException(maaster);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyEmployeeInfoCodeExists(string Code, int Id)
        {
            var feeterm = new ScEmployeeInfo();
            if (Id != 0)
            {
                var data = _scEmployeeInfoRepository.GetById(x => x.Id == Id);
                if (data.Code != Code)
                {
                    feeterm = _scEmployeeInfoRepository.GetById(x => x.Code == Code);

                }
            }
            else
            {
                feeterm = _scEmployeeInfoRepository.GetById(x => x.Code == Code);
            }
            if (feeterm == null)
            {
                feeterm = new ScEmployeeInfo();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeDetailInformation(int employeeId)
        {
            var employee = _scEmployeeInfoRepository.GetById(x => x.Id == employeeId);
            Session["ReportModel"] = employee;
            return View("EmployeeDetailInformation", employee);
        }


        public ActionResult PdfStaffDetailInfo()
        {

            var view = (ScEmployeeInfo)Session["ReportModel"];
            //Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "PdfStaffDetailInfo", view);

        }
        #endregion


        #region StaffSubjectMappings
        [CheckPermission(Permissions = "Navigate", Module = "SSM")]
        public ActionResult StaffSubjectMappings()
        {
            ViewBag.UserRight = base.UserRight("SSM");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "SSM")]
        public ActionResult StaffSubjectMappingAdd()
        {
            
            var subjects = _subjectRepository.GetAll();
            var model = new ScStaffSubjectMappingViewModel();
            model.StaffSubjectMapping = new ScStaffSubjectMapping();
            model.SubjectList = subjects.Select(x => new CheckBoxListInfo
            {
                DisplayText = x.Description,
                Value = x.Id.ToString()
            }).ToList();

            return PartialView("_PartialStaffSubjectMappingAdd", model);
        }

        [HttpPost]
        public ActionResult StaffSubjectMappingAdd(ScStaffSubjectMappingViewModel model, List<int> SubjectItemList)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in SubjectItemList)
                {
                    var newdata = new ScStaffSubjectMapping()
                    {
                        StaffId = model.StaffSubjectMapping.StaffId,
                        SubjectId = i,
                        IsActive = true
                    };
                    _scstaffsubjectmappingRepository.Add(newdata);
                }
                return Content("True");
            }
            else
            {
                return Content("False");
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "SSM")]
        public ActionResult StaffSubjectMappingEdit(int staffId)
        {
            var subjects = _subjectRepository.GetAll();
            var model = new ScStaffSubjectMappingViewModel();

            model.SubjectList = subjects.Select(x => new CheckBoxListInfo
            {
                DisplayText = x.Description,
                Value = x.Id.ToString(),
                IsChecked = UtilityService.IsMappedInDb(staffId, x.Id)
            }).ToList();
            model.StaffSubjectMapping = _scstaffsubjectmappingRepository.GetById(x => x.StaffId == staffId);
            model.OldStaffId = staffId;
            return PartialView("_PartialStaffSubjectMappingEdit", model);
        }

        [HttpPost]
        public ActionResult StaffSubjectMappingEdit(ScStaffSubjectMappingViewModel model, List<int> SubjectItemList)
        {
            if (ModelState.IsValid)
            {
                _scstaffsubjectmappingRepository.Delete(x => x.StaffId == model.OldStaffId);
                foreach (var i in SubjectItemList)
                {
                    var newdata = new ScStaffSubjectMapping()
                    {
                        StaffId = model.StaffSubjectMapping.StaffId,
                        SubjectId = i,
                        IsActive = true
                    };
                    _scstaffsubjectmappingRepository.Add(newdata);
                }
                return Content("True");
            }
            return Content("False");
        }
        [CheckPermission(Permissions = "Navigate", Module = "SSM")]
        public ActionResult StaffSubjectMappingsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("SSM");
            var lstStaffSubjectMappings = _scstaffsubjectmappingRepository.GetAll().OrderByDescending(x => x.Id).GroupBy(x => x.StaffId);

            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialListStaffSubjectMappings", lstStaffSubjectMappings.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
          [CheckPermission(Permissions = "Delete", Module = "SSM")]
        public ActionResult StaffSubjectMappingDelete(int staffid)
        {
            _scstaffsubjectmappingRepository.Delete(x => x.StaffId == staffid);
            return RedirectToAction("StaffSubjectMappings");
        }
        #endregion
        #region TeacherSchedule


        [CheckPermission(Permissions = "Navigate", Module = "TS")]
        public ActionResult TeacherSchedule()
        {
            ViewBag.UserRight = base.UserRight("TS");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "TS")]
        public ActionResult TeacherScheduleAdd()
        {

            var model = new ScTeacherScheduleViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult TeacherScheduleAdd(ScTeacherScheduleViewModel model, IEnumerable<ScTeacherScheduleViewModel> busRouteInfoList)
        {
            var classTeacherMapping = _classTeacherMapping.GetById(model.ThursdayTeacherId);
            if (classTeacherMapping != null)
            {
                classTeacherMapping.IsClassTeacher = true;
                //classTeacherMapping.TeacherId = model.ClassTeacherId;
                _classTeacherMapping.Update(classTeacherMapping);
            }
            //else
            //{
            //    var classData = new ScClassTeacherMapping();
            //    classData.TeacherId = model.ClassTeacherId;
            //    classData.ClassId = model.ClassId;
            //    classData.SectionId = model.SectionId;
            //    _classTeacherMapping.Add(classData);

            //}
            foreach (var item in busRouteInfoList)
            {
                AddTeacherScheduleDetail(item);
            }
            return RedirectToAction("TeacherSchedule");
        }

        public void AddTeacherScheduleDetail(ScTeacherScheduleViewModel model)
        {
            var authuser = _authentication.GetAuthenticatedUser().Id;
            var sundayscheduleId =
                _classscheduledetailRepository.GetById(x => x.ClassScheduleId == model.Sno && x.Day.ToLower() == "sunday").Id;
            var data = new ScTeacherSchedule()
            {
                StaffId = model.SundayTeacherId,
                ClassScheduleDetailId = sundayscheduleId,
                CreatedBy = authuser,
                CreatedOn = DateTime.UtcNow

            };
            _scteacherscheduleRepository.Add(data);

            var mondayscheduleId =
                   _classscheduledetailRepository.GetById(x => x.ClassScheduleId == model.Sno && x.Day.ToLower() == "monday").Id;
            var data1 = new ScTeacherSchedule()
            {
                StaffId = model.MondayTeacherId,
                ClassScheduleDetailId = mondayscheduleId,
                CreatedBy = authuser,
                CreatedOn = DateTime.UtcNow
            };
            _scteacherscheduleRepository.Add(data1);

            var tuesdayscheduleId =
                   _classscheduledetailRepository.GetById(x => x.ClassScheduleId == model.Sno && x.Day.ToLower() == "tuesday").Id;
            var data2 = new ScTeacherSchedule()
            {
                StaffId = model.TuesdayTeacherId,
                ClassScheduleDetailId = tuesdayscheduleId,
                CreatedBy = authuser,
                CreatedOn = DateTime.UtcNow
            };
            _scteacherscheduleRepository.Add(data2);

            var wednesdayscheduleId =
                       _classscheduledetailRepository.GetById(x => x.ClassScheduleId == model.Sno && x.Day.ToLower() == "wednesday").Id;
            var data3 = new ScTeacherSchedule()
            {
                StaffId = model.WednesdayTeacherId,
                ClassScheduleDetailId = wednesdayscheduleId,
                CreatedBy = authuser,
                CreatedOn = DateTime.UtcNow
            };
            _scteacherscheduleRepository.Add(data3);

            var thursdayscheduleId =
                          _classscheduledetailRepository.GetById(x => x.ClassScheduleId == model.Sno && x.Day.ToLower() == "thursday").Id;
            var data4 = new ScTeacherSchedule()
            {
                StaffId = model.ThursdayTeacherId,
                ClassScheduleDetailId = thursdayscheduleId,
                CreatedBy = authuser,
                CreatedOn = DateTime.UtcNow
            };
            _scteacherscheduleRepository.Add(data4);

            var fridayscheduleId =
                             _classscheduledetailRepository.GetById(x => x.ClassScheduleId == model.Sno && x.Day.ToLower() == "friday").Id;
            var data5 = new ScTeacherSchedule()
            {
                StaffId = model.FridayTeacherId,
                ClassScheduleDetailId = fridayscheduleId,
                CreatedBy = authuser,
                CreatedOn = DateTime.UtcNow
            };
            _scteacherscheduleRepository.Add(data5);
        }

        public ActionResult GetTimeScheduleByClassForTeacher(int classid, int sectionid, int oldClassid, int oldsectionid)
        {
            var data = new ScTeacherSchedule();
            if (oldClassid != 0 && oldsectionid != 0 && classid == oldClassid && sectionid == oldsectionid)
            {
                data =
              _scteacherscheduleRepository.GetById(
                  x =>
                  x.ClassScheduleDetail.ClassSchedule.ClassId == classid &&
                  x.ClassScheduleDetail.ClassSchedule.SectionId == sectionid && x.ClassScheduleDetail.ClassSchedule.ClassId != oldClassid &&
                  x.ClassScheduleDetail.ClassSchedule.SectionId != oldsectionid);
            }
            else
            {
                data =
              _scteacherscheduleRepository.GetById(
                  x =>
                  x.ClassScheduleDetail.ClassSchedule.ClassId == classid &&
                  x.ClassScheduleDetail.ClassSchedule.SectionId == sectionid);
            }

            if (data != null)
            {
                return Json(new { msg = "false" }, JsonRequestBehavior.AllowGet);
            }


            var model = new ScTeacherScheduleViewModel();
            model.ClassId = classid;
            model.SectionId = sectionid;

            model.ClassSchedules = _scclassscheduleRepository.GetMany(x => x.ClassId == classid && x.SectionId == sectionid);
            foreach (var item in model.ClassSchedules)
            {
                item.ScheduleDetail =
                    _classscheduledetailRepository.GetMany(x => x.ClassSchedule.ClassId == classid && x.ClassSchedule.SectionId == sectionid && x.ClassSchedule.SubjectId == item.SubjectId);
            }
            return PartialView(model);
        }
        public ActionResult GetClassTeacherName(int classid, int sectionid)
        {
            var teacherName = "";
            var classTeacherMapping = _classTeacherMapping.GetById(x => x.ClassId == classid && x.SectionId == sectionid);
            if (classTeacherMapping != null)
            {
                var teacherId = classTeacherMapping.TeacherId;
                teacherName = _scEmployeeInfoRepository.GetById(teacherId).Description;

            }
            return Json(teacherName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTimeScheduleByClassForTeacherEdit(int classid, int sectionid)
        {
            var model = new ScTeacherScheduleViewModel();
            model.ClassSchedules = _scclassscheduleRepository.GetMany(x => x.ClassId == classid && x.SectionId == sectionid);

            IEnumerable<ScTeacherScheduleViewModel> scheduleList;

            foreach (var item in model.ClassSchedules)
            {
                item.ScheduleDetail =
                    _classscheduledetailRepository.GetMany(
                        x => x.ClassSchedule.ClassId == classid && x.ClassSchedule.SectionId == sectionid && x.ClassSchedule.SubjectId == item.SubjectId);
            }
            return PartialView(model);
        }
        [CheckPermission(Permissions = "Edit", Module = "TS")]
        public ActionResult TeacherScheduleEdit(int classid, int sectionid)
        {
            var model = new ScTeacherScheduleViewModel();
            var classTeacherMapping = _classTeacherMapping.GetById(x => x.ClassId == classid && x.SectionId == sectionid);
            if (classTeacherMapping != null)
            {
                model.ClassTeacherId = classTeacherMapping.TeacherId;
                model.TeacherName = _scEmployeeInfoRepository.GetById(model.ClassTeacherId).Description;

            }
            model.ClassId = classid;
            model.Class = _scClassRepository.GetById(classid);
            model.SectionId = sectionid;
            model.Section = _scSectionRepository.GetById(sectionid);
            model.OldClassId = classid;
            model.OldSectionId = sectionid;
            return View(model);
        }

        [HttpPost]
        public ActionResult TeacherScheduleEdit(ScTeacherScheduleViewModel model, IEnumerable<ScTeacherScheduleViewModel> busRouteInfoList)
        {

            var classTeacherMapping = _classTeacherMapping.GetById(x => x.ClassId == model.ClassId && x.SectionId == model.SectionId);
            if (classTeacherMapping != null)
            {
                classTeacherMapping.TeacherId = model.ClassTeacherId;
                _classTeacherMapping.Update(classTeacherMapping);
            }
            else
            {
                var classData = new ScClassTeacherMapping();
                classData.TeacherId = model.ClassTeacherId;
                classData.ClassId = model.ClassId;
                classData.SectionId = model.SectionId;
                _classTeacherMapping.Add(classData);

            }
            foreach (var item in busRouteInfoList)
            {

                _scteacherscheduleRepository.Delete(x => x.ClassScheduleDetail.ClassScheduleId == item.Sno);
                AddTeacherScheduleDetail(item);
            }
            return RedirectToAction("TeacherSchedule");
        }
        [CheckPermission(Permissions = "Navigate", Module = "TS")]
        public ActionResult TeacherScheduleList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("TS");
            IEnumerable<List<ScTeacherSchedule>> list;
            var groupedList = from d in _scteacherscheduleRepository.GetAll()
                              group d by new { d.ClassScheduleDetail.ClassSchedule.ClassId, d.ClassScheduleDetail.ClassSchedule.SectionId }
                                  into g
                                  select g.ToList();
            list = groupedList;
            return PartialView("_PartialListTeacherSchedule", list);
        }

        public JsonResult GetClassMasterForScheduling()
        {
            var list = _scclassscheduleRepository.GetAll().Select(x => x.ClassId);
            var teachermappedlist =
                _scteacherscheduleRepository.GetAll().Select(x => x.ClassScheduleDetail.ClassSchedule.ClassId);
            var teachermappedlistsection =
                _scteacherscheduleRepository.GetAll().Select(x => x.ClassScheduleDetail.ClassSchedule.SectionId);
            List<SchClass> lstClasses =
                _scClassRepository.GetMany(x => list.Contains(x.Id)).OrderBy(x => x.Description).ToList();


            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        #endregion
        public JsonResult GetEmployeeInfoForStaffSubjectMapping(int id)
        {
            var exist = _scstaffsubjectmappingRepository.GetAll().Select(x => x.StaffId);

            List<ScEmployeeInfo> lstClasses =
                _scEmployeeInfoRepository.GetMany(x => !exist.Contains(x.Id)).OrderBy(x => x.Description).ToList();
            if (id != 0)
            {
                var currdata = _scstaffsubjectmappingRepository.GetById(x => x.StaffId == id).Staff;
                lstClasses.Add(currdata);
            }

            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }


        #region EmployeeDepartments
        public JsonResult AcademyEmployeeDepartmentCodeExists(string Code, int Id)
        {
            var feeterm = new ScEmployeeDepartment();
            if (Id != 0)
            {
                var data = _scemployeedepartmentRepository.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm = _scemployeedepartmentRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());

                }
            }
            else
            {
                feeterm = _scemployeedepartmentRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScEmployeeDepartment();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyEmployeeDepartmentDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScEmployeeDepartment();
            if (Id != 0)
            {
                var data = _scemployeedepartmentRepository.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm = _scemployeedepartmentRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());

                }
            }
            else
            {
                feeterm = _scemployeedepartmentRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScEmployeeDepartment();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Navigate", Module = "ED")]
        public ActionResult EmployeeDepartments()
        {
            ViewBag.UserRight = base.UserRight("ED");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ED")]
        public ActionResult EmployeeDepartmentAdd()
        {
           
            return PartialView("_PartialEmployeeDepartmentAdd", new ScEmployeeDepartment());
        }

        [HttpPost]
        public ActionResult EmployeeDepartmentAdd(ScEmployeeDepartment model)
        {
            if (ModelState.IsValid)
            {
                var usr = _authentication.GetAuthenticatedUser();
                model.CreatedDate = DateTime.Now;
                model.CreatedById = usr.Id;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedById = usr.Id;
                _scemployeedepartmentRepository.Add(model);
                return Content("True");
            }
            else
            {
                return Content("False");
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "ED")]
        public ActionResult EmployeeDepartmentEdit(int id)
        {
            return PartialView("_PartialEmployeeDepartmentEdit", _scemployeedepartmentRepository.GetById(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult EmployeeDepartmentEdit(ScEmployeeDepartment model)
        {
            var usr = _authentication.GetAuthenticatedUser();

            model.UpdatedDate = DateTime.Now;
            model.UpdatedById = usr.Id;
            _scemployeedepartmentRepository.Update(model);
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ED")]
        public ActionResult EmployeeDepartmentsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ED");
            var lstEmployeeDepartments = _scemployeedepartmentRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialEmployeeDepartmentList", lstEmployeeDepartments.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult EmployeeDepartmentDelete(int id)
        {
            var data = _scemployeedepartmentRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion EmployeeDepartments

        #region EmployeePosts
        [CheckPermission(Permissions = "Navigate", Module = "Employee Post")]
        public ActionResult EmployeePosts()
        {
            ViewBag.UserRight = base.UserRight("Employee Post");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "Employee Post")]
        public ActionResult EmployeePostAdd()
        {
            var category =
                new SelectList(_scemployeecategoryRepository.GetMany(x => x.Status).OrderBy(x => x.Description), "Id",
                               "Description");
            return PartialView("_PartialEmployeePostAdd", new ScEmployeePost { CategoryList = category });
        }

        [HttpPost]
        public ActionResult EmployeePostAdd(ScEmployeePost model)
        {
            if (ModelState.IsValid)
            {
                var usr = _authentication.GetAuthenticatedUser();
                model.CreatedDate = DateTime.Now;
                model.CreatedById = usr.Id;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedById = usr.Id;
                _scemployeepostRepository.Add(model);
                return Content("True");
            }
            else
            {
                return Content("False");
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "Employee Post")]
        public ActionResult EmployeePostEdit(int id)
        {
            var category =
                new SelectList(_scemployeecategoryRepository.GetMany(x => x.Status).OrderBy(x => x.Description), "Id",
                               "Description");
            var data = _scemployeepostRepository.GetById(x => x.Id == id);
            data.CategoryList = category;
            return PartialView("_PartialEmployeePostEdit", data);
        }

        [HttpPost]
        public ActionResult EmployeePostEdit(ScEmployeePost model)
        {
            var usr = _authentication.GetAuthenticatedUser();

            model.UpdatedDate = DateTime.Now;
            model.UpdatedById = usr.Id;
            _scemployeepostRepository.Update(model);
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "Employee Post")]
        public ActionResult EmployeePostList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("Employee Post");
            var lstEmployeeDepartments = _scemployeepostRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialEmployeePostList", lstEmployeeDepartments.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
         [CheckPermission(Permissions = "Delete", Module = "Employee Post")]
        public ActionResult EmployeePostDelete(int id)
        {
            var data = _scemployeepostRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion EmployeePosts

        public ActionResult GetEmployeeBySubjectId(int classId, int sectionId, int subjectId)
        {
            var data = new DataContext();


            var subject = (from d in data.ScStaffSubjectMapping.Where(x => x.IsActive && x.SubjectId == subjectId)
                           join ct in
                               data.ClassTeacherMappings.Where(x => x.ClassId == classId && x.SectionId == sectionId) on
                               d.StaffId equals ct.TeacherId

                           select d.Staff);




            var classes = subject.Select(x => new
                 {
                     Id = x.Id,
                     Code = x.Code,
                     Description = x.Description
                 });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetTeacher(int id)
        //{
        //    var subject = _scEmployeeInfoRepository.GetMany(x => x.IsActive && x.SubjectId == id).Select(x => x.Staff);
        //    var classes = subject.Select(x => new
        //    {
        //        Id = x.Id,
        //        Code = x.Code,
        //        Description = x.Description
        //    });
        //    return Json(classes, JsonRequestBehavior.AllowGet);
        //}

    }
}
