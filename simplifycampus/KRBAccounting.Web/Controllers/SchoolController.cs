using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Models.BillingTerm;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.Academy;
using System.IO;
using KRBAccounting.Web.ViewModels.School;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using LinqKit;
using KRBAccounting.Domain.StoredProcedures;



namespace KRBAccounting.Web.Controllers
{
    [Authorize]
    //[CheckModulePermission("Academy")]
    public class SchoolController : BaseController
    {
        private readonly IScProgramMasterRepository _scprogrammasterRepository;
        private readonly ISchBuildingRepository _schBuildingRepository;
        private readonly IScClassRepository _scClassRepository;
        private readonly IScSubjectRepository _subjectRepository;
        private readonly IScShiftRepository _scshiftRepository;
        private readonly IScSectionRepository _scSectionRepository;
        private readonly IScStudentinfoRepository _scStudentinfoRepository;
        private readonly IScStudentSubjectMappingRepository _studentSubjectMappingRepository;
        private readonly IScClassSubjectMappingRepository _classSubjectMappingRepository;
        private readonly IScGradeRepository _scGradeRepository;
        private readonly IScDivisionRepository _scDivisionRepository;
        private readonly IScExtraActivityRepository _scExtraActivityRepository;
        private readonly IScExamRepository _examRepository;
        private readonly IScExamRoutineRepository _examRoutineRepository;
        private readonly IStudentExtraActivityDetailRepository _studentExtraActivityDetialRepository;
        private readonly IScFeeItemRepository _feeItemRepository;
        private readonly IScFeeTermRepository _scfeetermRepository;
        private readonly IFormsAuthenticationService _authentication;
        private readonly IScBoaderRepository _scboaderRepository;
        private readonly IScClassFeeRateRepository _scclassfeerateRepository;
        private readonly IScStudentFeeRateMasterRepository _scStudentFeeRateMasterRepository;
        private readonly IScStudentFeeRateDetailRepository _scStudentFeeRateDetailRepository;
        private readonly IScHouseGroupRepository _schousegroupRepository;
        private readonly IScHouseMappingRepository _schousemappingRepository;
        private readonly IScFineSchemeDetailsRepository _scFineSchemeDetailsRepository;
        private readonly IScFineSchemeRepository _scFineSchemeRepository;
        private readonly IScPrePaidSchemeRepository _scPrePaidSchemeRepository;
        private readonly IScPrePaidSchemeDetailsRepository _scPrePaidSchemeDetailsRepository;
        private readonly IScBoaderMappingRepository _scBoaderMappingRepository;
        public readonly IScStudentRegistrationRepository _scStudentRegistrationRepository;
        public readonly IScStudentRegistrationDetailRepository _scStudentRegistrationDetailRepository;
        public readonly IStudentExtraActivityRepository _StudentExtraActivityRepository;
        private readonly IScRoomRepository _scroomRepository;
        private readonly ISchBuildingRoomMappingRepository _schbuildingroommappingRepository;
        private readonly IScClassRoomMappingRepository _scclassroommappingRepository;
        private readonly IScClassScheduleRepository _scclassscheduleRepository;
        private readonly IScStudentFeeTermRepository _scStudentFeeTermRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly IClassScheduleDetailRepository _classscheduledetailRepository;
        private readonly IAcademicYearRepository _academicYear;
        private readonly IScExamMarksEntryRepository _marksEntryRepository;
        private readonly IStudentSlcInfoRepository _studentSlc;
        private readonly IScClassTeacherMappingRepository _classTeacherMapping;
        private readonly IScEmployeeInfoRepository _employeeInfo;
        private readonly IScCalendarEventRepository _calendarEvent;
        private readonly IScRollNumberingSchemeRepository _rollNumbering;
        private readonly IScProgramMasterRepository _programMasterRepository;

        private int CurrentAcademyYearId;

        #region Constructor

        public SchoolController(
            IScExamMarksEntryRepository examMarksEntryRepository,
            IScProgramMasterRepository scprogrammasterRepository,
            IScProgramMasterRepository programMasterRepository,
            ISchBuildingRepository schBuildingRepository,
                                IScClassRepository scClassRepository,
                                IScSectionRepository scSectionRepository,
                                IScStudentinfoRepository scStudentinfoRepository,
                                IScShiftRepository scshiftRepository,
                                IScSubjectRepository subjectRepository,
                                IScClassSubjectMappingRepository classSubjectMappingRepository,
                                IScGradeRepository scGradeRepository,
                                IScDivisionRepository scDivisionRepository,
                                IScExamRepository scExamRepository,
                                IScStudentSubjectMappingRepository studentSubjectMappingRepository,
                                IScExtraActivityRepository scExtraActivityRepository,
                                IScExamRoutineRepository scExamRoutineRepository,
                                IScFeeItemRepository feeItemRepository,
                                IScClassFeeRateRepository scclassfeerateRepository,
                                IScFeeTermRepository scfeetermRepository,
                                IScBoaderRepository scboaderRepository,
                                IScStudentFeeRateMasterRepository scStudentFeeRateMasterRepository,
                                IScStudentFeeRateDetailRepository scStudentFeeRateDetailRepository,
                                IScHouseGroupRepository schousegroupRepository,
                                IScHouseMappingRepository schousemappingRepository,
                                IScFineSchemeDetailsRepository fineSchemeDetailsRepository,
                                IScFineSchemeRepository fineSchemeRepository,
                                IScPrePaidSchemeRepository prePaidSchemeRepository,
                                IScPrePaidSchemeDetailsRepository prePaidSchemeDetailsRepository,
                                IScBoaderMappingRepository scBoaderMappingRepository,
                                IScStudentRegistrationRepository studentRegistrationRepository,
                                IScStudentRegistrationDetailRepository StudentRegistrationDetail,
                                IStudentExtraActivityRepository studentExtraActivityRepository,
                                IStudentExtraActivityDetailRepository studentExtraActivityDetialRepository,
            ISchBuildingRoomMappingRepository schbuildingroommappingRepository,
            ITemplateRepository templateRepository,
            IScRoomRepository scroomRepository,
            IAcademicYearRepository academicYearRepository,
            IClassScheduleDetailRepository classscheduledetailRepository,
            IScClassRoomMappingRepository scclassroommappingRepository,
                   IStudentSlcInfoRepository studentSlc,
            IScClassScheduleRepository scclassscheduleRepository,

            IScStudentFeeTermRepository scStudentFeeTermRepository,
            IScClassTeacherMappingRepository classTeacherMapping,
            IScEmployeeInfoRepository employeeInfo,
            IScCalendarEventRepository calendarEvent,
            IScRollNumberingSchemeRepository rollNumbering

            )
        {
            _programMasterRepository = programMasterRepository;
            _marksEntryRepository = examMarksEntryRepository;
            _scprogrammasterRepository = scprogrammasterRepository;

            _scClassRepository = scClassRepository;
            _schBuildingRepository = schBuildingRepository;
            _scshiftRepository = scshiftRepository;
            _scSectionRepository = scSectionRepository;
            _scStudentinfoRepository = scStudentinfoRepository;

            _subjectRepository = subjectRepository;
            _classSubjectMappingRepository = classSubjectMappingRepository;
            _studentSubjectMappingRepository = studentSubjectMappingRepository;

            _examRepository = scExamRepository;

            _examRoutineRepository = scExamRoutineRepository;
            _scGradeRepository = scGradeRepository;
            _scDivisionRepository = scDivisionRepository;

            _scExtraActivityRepository = scExtraActivityRepository;

            _scExtraActivityRepository = scExtraActivityRepository;

            _studentExtraActivityDetialRepository = studentExtraActivityDetialRepository;
            _feeItemRepository = feeItemRepository;
            _scfeetermRepository = scfeetermRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Academy);
            _scboaderRepository = scboaderRepository;
            _scclassfeerateRepository = scclassfeerateRepository;
            _scStudentFeeRateMasterRepository = scStudentFeeRateMasterRepository;
            _scStudentFeeRateDetailRepository = scStudentFeeRateDetailRepository;

            _schousegroupRepository = schousegroupRepository;
            _schousemappingRepository = schousemappingRepository;
            _scFineSchemeDetailsRepository = fineSchemeDetailsRepository;
            _scFineSchemeRepository = fineSchemeRepository;
            _scPrePaidSchemeDetailsRepository = prePaidSchemeDetailsRepository;
            _scPrePaidSchemeRepository = prePaidSchemeRepository;
            _scBoaderMappingRepository = scBoaderMappingRepository;
            _scStudentRegistrationRepository = studentRegistrationRepository;
            _scStudentRegistrationDetailRepository = StudentRegistrationDetail;
            _StudentExtraActivityRepository = studentExtraActivityRepository;

            _academicYear = academicYearRepository;
            _scroomRepository = scroomRepository;
            _schbuildingroommappingRepository = schbuildingroommappingRepository;
            _scclassroommappingRepository = scclassroommappingRepository;

            _scclassscheduleRepository = scclassscheduleRepository;
            _studentSlc = studentSlc;
            _scStudentFeeTermRepository = scStudentFeeTermRepository;
            CurrentAcademyYearId = AcademicYear.Id;
            _templateRepository = templateRepository;
            _classscheduledetailRepository = classscheduledetailRepository;
            _classTeacherMapping = classTeacherMapping;
            _employeeInfo = employeeInfo;
            _calendarEvent = calendarEvent;
            _rollNumbering = rollNumbering;

        }

        #endregion

        public ActionResult DashBoard()
        {

            var dataContxt = new DataContext();
            var viewModel = base.CreateViewModel<AcademyViewModel>();

            var registeredStudent = dataContxt.ScStudentRegistrationDetails.Where(x => x.StudentRegistration.AcademicYearId == AcademicYear.Id).ToList();
            viewModel.TotalStudent = registeredStudent.Count();
            ViewBag.classlist = _scClassRepository.GetAll().OrderBy(x => x.Description).ToList();
            ViewBag.classlist.Insert(0, new SchClass { Id = 0, Description = "All" });

            ViewBag.ProgramMasters = _programMasterRepository.GetAll().OrderBy(x => x.Schedule).ToList();
            if (registeredStudent.Any())
            {
                var male = registeredStudent.Count(x => x.Studentinfo.Sex == "Male");
                var female = registeredStudent.Count(x => x.Studentinfo.Sex == "Female");
                viewModel.TotalMaleStudent = (male * 100) / viewModel.TotalStudent;
                viewModel.TotalFemaleStudent = (female * 100) / viewModel.TotalStudent;

            }

            viewModel.ClassChart = (from fc in dataContxt.SchClass.OrderBy(x => x.Schedule).ToList()
                                    join fl in
                                        (from gl in
                                             (from sc in dataContxt.SchClass.OrderBy(s => s.Schedule).ToList()
                                              join rg in registeredStudent on sc.Id equals rg.StudentRegistration.ClassId
                                              select new
                                              {
                                                  ClassName = sc.Description,
                                                  FemaleCount = registeredStudent.Count(
                                                          x => x.StudentRegistration.ClassId == sc.Id && x.Studentinfo.Sex == "Female"),
                                                  MaleCount = registeredStudent.Count(
                                                          x => x.StudentRegistration.ClassId == sc.Id && x.Studentinfo.Sex == "Male"),
                                                  ClassId = sc.Id
                                              })
                                         group gl by new { gl.ClassName, gl.FemaleCount, gl.MaleCount, gl.ClassId } into gg
                                         select new
                                         {
                                             ClassName = gg.Key.ClassName,
                                             MaleCount = gg.Key.MaleCount,
                                             FemaleCount = gg.Key.FemaleCount,
                                             ClassId = gg.Key.ClassId
                                         })
                                   on fc.Id equals fl.ClassId into ff
                                    from fj in ff.DefaultIfEmpty()
                                    select new ClassChartViewModel()
                                    {
                                        ClassName = fc.Description,
                                        FemaleCount = fj == null ? 0 : fj.FemaleCount,
                                        MaleCount = fj == null ? 0 : fj.MaleCount,

                                    })
                                   .ToList();

            viewModel.CategoryChart = from c in dataContxt.ScStudentCategories.ToList()
                                      select
                                          new CategoryChartViewModel()
                                          {
                                              CatergoryName = c.Description,
                                              StudentCount =
                                                  registeredStudent.Count(x => x.Studentinfo.StdCategoryId == c.Id)
                                          };
            viewModel.AcademicYear = new List<string>();
            viewModel.AcademicStudent = new List<int>();
            var academicYear = dataContxt.AcademicYears;
            if (this.SystemControl.DateType == 1)
            {
                var list = academicYear.Select(x => x.StartDate);
                foreach (var item in list)
                {
                    viewModel.AcademicYear.Add(item.ToString("yyyy"));
                }

            }
            else
            {
                var lis = academicYear.Select(x => x.StartDateNep);
                foreach (var li in lis)
                {
                    viewModel.AcademicYear.Add(li.Split('/').FirstOrDefault());
                }

            }
            foreach (var academic in academicYear)
            {
                var student =
                    dataContxt.ScStudentRegistrationDetails.Where(
                        x => x.StudentRegistration.AcademicYearId == academic.Id).Count();
                viewModel.AcademicStudent.Add(student);
            }
            //fee amount

            viewModel.FeeAmount = (from n in dataContxt.SchClass.OrderBy(x => x.Schedule).ToList()
                                   join f in
                                       (from sbt in dataContxt.ScBillTransaction
                                        group sbt by sbt.Studentinfo.ClassId
                                            into g
                                            select new
                                            {
                                                classId = g.Key,
                                                ReceiptAmt = g.Sum(x => x.ReceiptAmount),
                                                BillAmt = g.Sum(x => x.BillAmount)
                                            }) on n.Id equals f.classId into fl
                                   from ff in fl.DefaultIfEmpty()
                                   select
                                       new FeeAmountViewModel()
                                       {
                                           ClassName = n.Description,
                                           ReceiptAmt = ff == null ? 0 : ff.ReceiptAmt,
                                           BillAmt = ff == null ? 0 : ff.BillAmt
                                       }).ToList();


            // examination
            var division = dataContxt.ScDivisions.OrderBy(x => x.Schedule).ToList();
            if (division.Any())
            {
                viewModel.DivisionName = division.Select(x => x.Description).ToList();
                var to = division.Select(x => x.PercentageTo);
                var from = division.Select(x => x.PercentageFrom);
                viewModel.DivisionValue = from.Zip(to, (n, w) => "[" + n + "," + w + "]").ToList();
            }
            return View(viewModel);
        }
        #region
        [CheckPermission(Permissions = "Navigate", Module = "ACCal")]
        public ActionResult Calendar()
        {
            if (User.IsInRole("admin"))
            {
                return View("Admincalendar");
            }
            else
            {
                return View("Calendar");
            }

        }
        public JsonResult GetEvents(double start, double end)
        {
            var events = _calendarEvent.GetAll();
            //var events = _context.Events.Where(e => e.EventDate>= startDateTime && e.EventDate<=endDateTime).ToList();

            var clientList = new List<object>();

            foreach (var e in events)
            {
                clientList.Add(
                new
                {
                    id = e.Id,
                    title = e.Title,
                    description = e.Description,
                    start = e.EventDate.ToString("s"),
                    end = e.EventDate.ToString("s"),
                    allDay = false
                });
            }
            return Json(clientList.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult EventEditDialog(int eventid)
        {
            var data = _calendarEvent.GetById(eventid);
            return PartialView(data);

        }


        public ActionResult EventDetailDialog(int id)
        {
            var data = _calendarEvent.GetById(id);
            return PartialView(data);
        }

        public ActionResult EventCreateDialog(string eventDate)
        {
            var model = new ScCalendarEvent();
            model.EventDate = Convert.ToDateTime(eventDate);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult EventAdd(ScCalendarEvent model)
        {
            model.CreatedById = _authentication.GetAuthenticatedUser().Id;
            _calendarEvent.Add(model);
            return null;
        }
        [HttpPost]
        public ActionResult EventEdit(ScCalendarEvent model)
        {
            model.CreatedById = _authentication.GetAuthenticatedUser().Id;
            _calendarEvent.Update(model);
            return null;


        }
        #endregion


        public ActionResult GetStudentCurrentStatus()
        {
            var data = new SelectList(
                Enum.GetValues(typeof(StudentCurrentStatus)).Cast<StudentCurrentStatus>().Select(
                    x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");



            var classes = data.Select(x => new
            {
                Id = x.Value,

                Description = x.Text
            });
            return Json(classes, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetStudentType()
        {
            var data = new SelectList(
                Enum.GetValues(typeof(StudentType)).Cast<StudentType>().Select(
                    x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text").Select(x => new
            {
                Id = x.Value,

                Description = x.Text

            });
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetStudentRegistrationByClassId(int id)
        {
            var academicYearId = CurrentAcademyYearId;
            var data = _scStudentRegistrationRepository.GetById(x => x.Id == id && x.AcademicYearId == academicYearId);
            var registration = _scStudentRegistrationRepository.GetMany(x => x.AcademicYearId == academicYearId).Select(x => x.ClassId);
            var clas = _scClassRepository.GetAll().ToList();
            if (id != 0)
            {
                var c = _scClassRepository.GetById(x => x.Id == data.ClassId);
                clas.Add(c);
            }
            var classes = clas.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetStudentCode(int id)
        {
            var data = _scStudentinfoRepository.GetById(x => x.StudentID == id);
            return Json(data.StdCode, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentRollno(int id)
        {
            var data = _scStudentRegistrationDetailRepository.GetById(x => x.StudentId == id);
            if (data != null)
            {
                return Json(data.RollNo, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSubjectCode(int id)
        {
            var data = _subjectRepository.GetById(x => x.Id == id);
            return Json(data.Code, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenerateStudentCode(string studentName)
        {
            StudentHelper objStudentHelper = new StudentHelper(_scStudentinfoRepository);
            string studentCode = objStudentHelper.GenerateStudentCode(studentName);
            return Json(studentCode, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStudentByClassSection(int classId, int sectionId)
        {
            //var data =
            //    _scStudentRegistrationDetailRepository.GetMany(
            //        x => x.StudentRegistration.ClassId == classId && x.SectionId == sectionId);
            // var data =_scStudentinfoRepository.GetMany(x => x.ClassId == classId && x.SectionId == sectionId).OrderBy(x => x.StuDesc);
            var predicate = PredicateBuilder.True<ScStudentRegistrationDetail>();
            predicate = predicate.And(x => x.StudentRegistration.ClassId == classId);
            if (sectionId != 0)
            {
                predicate = predicate.And(x => x.SectionId == sectionId);
            }
            predicate = predicate.And(x => x.StudentRegistration.AcademicYearId == CurrentAcademyYearId);
            var data = _scStudentRegistrationDetailRepository.GetExpandable(predicate);
            var student = new List<ScStudentinfo>();
            if (data.Any())
            {
                student = data.Select(x => x.Studentinfo).ToList();
            }
            var classes = student.Select(x => new
            {
                Id = x.StudentID,
                Code = x.StdCode,
                Description = x.StuDesc
            });
            return Json(classes, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetStudentClassIdSectionId(int id, int nextid)
        {
            Expression<Func<ScStudentRegistrationDetail, bool>> predicate = x => true;

            // var predicate = new  Expression<Func<ScStudentRegistrationDetail, bool>>();
            if (id != 0)
            {
                var Id = Convert.ToInt32(id);
                predicate = predicate.And(x => x.StudentRegistration.ClassId == Id);
            }
            if (nextid != 0)
            {
                var sectionId = Convert.ToInt32(nextid);
                predicate = predicate.And(x => x.SectionId == sectionId);
            }
            List<ScStudentRegistrationDetail> studentdetail;
            if (id == 0 && nextid == 0)
            {
                studentdetail = _scStudentRegistrationDetailRepository.GetAll().ToList();
            }
            else
            {
                studentdetail = _scStudentRegistrationDetailRepository.GetExpandable(predicate).ToList();
            }
            var detail = studentdetail.Select(x => x.Studentinfo).Select(x => new
                                                                                 {
                                                                                     Id = x.StudentID,
                                                                                     Code = x.StdCode,
                                                                                     Description = x.StuDesc
                                                                                 });

            return Json(detail, JsonRequestBehavior.AllowGet);
        }
        #region Buildings

        public JsonResult CheckCodeInBuilding(string Code, int Id)
        {

            var feeterm = new SchBuilding();
            if (Id != 0)
            {
                var data = _schBuildingRepository.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm =
                        _schBuildingRepository.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower());

                }
            }
            else
            {
                feeterm = _schBuildingRepository.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower());
            }
            if (feeterm == null)
            {
                feeterm = new SchBuilding();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckDescriptionInBuilding(string Description, int Id)
        {

            var feeterm = new SchBuilding();
            if (Id != 0)
            {
                var data = _schBuildingRepository.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm =
                        _schBuildingRepository.GetById(x => x.Description.ToLower().Trim() == Description.Trim().ToLower());

                }
            }
            else
            {
                feeterm = _schBuildingRepository.GetById(x => x.Description.ToLower().Trim() == Description.Trim().ToLower());
            }
            if (feeterm == null)
            {
                feeterm = new SchBuilding();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Navigate", Module = "ScBlD")]
        public ActionResult Buildings()
        {
            ViewBag.UserRight = base.UserRight("ScBlD");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScBlD")]
        public ActionResult AddBuilding()
        {
            var viewModel = new SchBuilding();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult AddBuilding(SchBuilding modelSchBuilding)
        {
            try
            {
                SchBuilding objSchBuilding =
                    _schBuildingRepository.Filter(x => x.Code.Equals(modelSchBuilding.Code)).FirstOrDefault();
                if (objSchBuilding != null)
                {
                    return Json(new { bcodeexists = true });
                }
                _schBuildingRepository.Add(modelSchBuilding);
                return Content("True");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
        [CheckPermission(Permissions = "Navigate", Module = "ScBlD")]
        public ActionResult ListBuildings(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScBlD");
            var lstBuildings = _schBuildingRepository.GetAll().OrderBy(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialListBuildings", lstBuildings.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));

        }
        [CheckPermission(Permissions = "Edit", Module = "ScBlD")]
        public ActionResult EditBuilding(int buildingId)
        {
            SchBuilding objSchbuilding = _schBuildingRepository.GetById(x => x.Id == buildingId);
            return PartialView("PartialEditBuilding", objSchbuilding);
        }

        [HttpPost]
        public ActionResult EditBuilding(SchBuilding modelSchBuilding)
        {
            try
            {
                SchBuilding objSchBuilding = _schBuildingRepository.GetById(x => x.Id == modelSchBuilding.Id);
                objSchBuilding.Code = modelSchBuilding.Code;
                objSchBuilding.Description = modelSchBuilding.Description;
                _schBuildingRepository.Update(objSchBuilding);
                return Content("True");
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }

        }

        #endregion

        #region Rooms

        [CheckPermission(Permissions = "Navigate", Module = "ScRoom")]
        public ActionResult Rooms()
        {
            ViewBag.UserRight = base.UserRight("ScRoom");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScRoom")]
        public ActionResult RoomAdd()
        {
            var model = new ScRoom();
            return PartialView("_PartialRoomAdd", model);
        }

        [HttpPost]
        public ActionResult RoomAdd(ScRoom model)
        {

            _scroomRepository.Add(model);
            return Content("True");


        }
        [CheckPermission(Permissions = "Edit", Module = "ScRoom")]
        public ActionResult RoomEdit(int roomId)
        {
            ScRoom objScRoom = _scroomRepository.GetById(x => x.Id == roomId);
            return PartialView("_PartialRoomEdit", objScRoom);
        }

        [HttpPost]
        public ActionResult RoomEdit(ScRoom model)
        {
            ScRoom objScRoom = _scroomRepository.GetById(x => x.Id == model.Id);
            objScRoom.Code = model.Code;
            objScRoom.Seats = model.Seats;
            objScRoom.Memo = model.Memo;
            objScRoom.Description = model.Description;
            _scroomRepository.Update(objScRoom);
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScRoom")]
        public ActionResult RoomsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScRoom");
            var lstRooms = _scroomRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialRoomsList", lstRooms.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult RoomDelete(int id)
        {
            ScRoom objScRoom = _scroomRepository.GetById(x => x.Id == id);
            var data = _scroomRepository.DeleteException(objScRoom);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AcademyRoomDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScRoom();
            if (Id != 0)
            {
                var data = _scroomRepository.GetById(x => x.Id == Id);
                if (data.Description != Description)
                {
                    feeterm = _scroomRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());

                }
            }
            else
            {
                feeterm = _scroomRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScRoom();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyRoomCodeExists(string Code, int Id)
        {
            var feeterm = new ScRoom();
            if (Id != 0)
            {
                var data = _scroomRepository.GetById(x => x.Id == Id);
                if (data.Code != Code)
                {
                    feeterm = _scroomRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());

                }
            }
            else
            {
                feeterm = _scroomRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScRoom();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region BuildingRoomMappings
        [CheckPermission(Permissions = "Navigate", Module = "ScBM")]
        public ActionResult BuildingRoomMappings()
        {
            ViewBag.UserRight = base.UserRight("ScBM");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScBM")]
        public ActionResult BuildingRoomMappingAdd()
        {

            var model = new SchBuildingRoomMapping();

            model.BuildingList = new SelectList(_schBuildingRepository.GetAll().OrderBy(x => x.Description), "Id", "Description");
            model.RoomList = new SelectList(_scroomRepository.GetAll(), "Id", "Description");

            return PartialView("_PartialBuildingRoomMappingAdd", model);
        }

        [HttpPost]
        public ActionResult BuildingRoomMappingAdd(SchBuildingRoomMapping model)
        {
            try
            {

                var data =
                    _schbuildingroommappingRepository.GetById(x => x.BuildingId == model.BuildingId && x.RoomId == model.RoomId);
                if (data == null)
                {
                    _schbuildingroommappingRepository.Add(model);
                    return Content("True");
                }
                else
                {
                    return Content(" '" + data.Room.Description + "' is already use in Building '" + data.Building.Description + "'");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Content(error);
            }




        }
        [CheckPermission(Permissions = "Edit", Module = "ScBM")]
        public ActionResult BuildingRoomMappingEdit(int buildingroommappingId)
        {
            SchBuildingRoomMapping objSchBuildingRoomMapping = _schbuildingroommappingRepository.GetById(x => x.Id == buildingroommappingId);
            objSchBuildingRoomMapping.BuildingList = new SelectList(_schBuildingRepository.GetAll().OrderBy(x => x.Description), "Id", "Description", objSchBuildingRoomMapping.BuildingId);
            objSchBuildingRoomMapping.RoomList = new SelectList(_scroomRepository.GetAll(), "Id", "Description", objSchBuildingRoomMapping.RoomId);
            objSchBuildingRoomMapping.OldRoomId = objSchBuildingRoomMapping.RoomId;
            objSchBuildingRoomMapping.OldBuildingId = objSchBuildingRoomMapping.BuildingId;
            return PartialView("_PartialBuildingRoomMappingEdit", objSchBuildingRoomMapping);
        }

        [HttpPost]
        public ActionResult BuildingRoomMappingEdit(SchBuildingRoomMapping model)
        {
            try
            {
                if (model.BuildingId == model.OldBuildingId && model.RoomId == model.OldRoomId)
                {
                    _schbuildingroommappingRepository.Update(model);
                    return Content("True");
                }
                else
                {
                    var data =
                   _schbuildingroommappingRepository.GetById(x => x.BuildingId == model.BuildingId && x.RoomId == model.RoomId);
                    if (data == null)
                    {
                        _schbuildingroommappingRepository.Update(model);
                        return Content("True");
                    }
                    else
                    {
                        return Content(" '" + data.Room.Description + "' is already use in Building '" + data.Building.Description + "'");
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Content(error);
            }
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScBM")]
        public ActionResult BuildingRoomMappingsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScBM");
            var lstBuildingRoomMappings = _schbuildingroommappingRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialBuildingRoomMappingsList", lstBuildingRoomMappings.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult BuildingRoomMappingDelete(int buildingroommappingId)
        {
            SchBuildingRoomMapping objSchBuildingRoomMapping = _schbuildingroommappingRepository.GetById(x => x.Id == buildingroommappingId);
            _schbuildingroommappingRepository.Delete(objSchBuildingRoomMapping);
            return RedirectToAction("BuildingRoomMappings");
        }
        #endregion

        #region ClassRoomMappings

        public JsonResult GetRoomsByRoomMappingId(int id)
        {
            //var context=new DataContext();
            //var list= from B in context.SchBuildingRoomMapping
            //          join C in context.ScClassRoomMapping on B.Id equals C.RoomMappingId
            //          group B by  B.RoomId into BC

            //          select new
            var lst =
                 _schbuildingroommappingRepository.GetMany(x => x.BuildingId == id && x.IsActive && !x.IsSelected).OrderBy(x => x.Room.Description).Select(x => new
                 {
                     Id = x.Id,
                     Code = x.Room.Code,
                     Description = x.Room.Description
                 });


            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScCRM")]
        public ActionResult ClassRoomMappings()
        {
            ViewBag.UserRight = base.UserRight("ScCRM");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScCRM")]


        public ActionResult ClassRoomMappingAdd()
        {
            var model = new ScClassRoomMapping();
            model.BuildingList = new SelectList(_schBuildingRepository.GetAll().OrderBy(x => x.Description), "Id", "Description");
            return PartialView("_PartialClassRoomMappingAdd", model);
        }

        [HttpPost]
        public ActionResult ClassRoomMappingAdd(ScClassRoomMapping model)
        {
            if (ModelState.IsValid && model.ClassId != 0)
            {
                try
                {
                    _scclassroommappingRepository.Add(model);
                    var data = _schbuildingroommappingRepository.GetById(x => x.Id == model.RoomMappingId);

                    data.IsSelected = true;
                    _schbuildingroommappingRepository.Update(data);
                    return Content("True");
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }

            }
            else
            {
                return Content("Enter Class");
            }

        }
        [CheckPermission(Permissions = "Edit", Module = "ScCRM")]
        public ActionResult ClassRoomMappingEdit(int id)
        {
            ScClassRoomMapping data = _scclassroommappingRepository.GetById(x => x.Id == id);
            data.BuildingRoomMappingId = data.RoomMappingId;

            data.BuildingList = new SelectList(_schBuildingRepository.GetAll(), "Id", "Description", 3);
            return PartialView("_PartialClassRoomMappingEdit", data);
        }

        [HttpPost]
        public ActionResult ClassRoomMappingEdit(ScClassRoomMapping model)
        {
            try
            {
                _scclassroommappingRepository.Update(model);
                var data = _schbuildingroommappingRepository.GetById(x => x.Id == model.RoomMappingId);
                data.IsSelected = true;
                _schbuildingroommappingRepository.Update(data);
                var data1 = _schbuildingroommappingRepository.GetById(x => x.Id == model.BuildingRoomMappingId);
                if (data1.Id == model.BuildingRoomMappingId)
                {
                    data1.IsSelected = false;
                    _schbuildingroommappingRepository.Update(data1);

                }



                return Content("True");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
        [CheckPermission(Permissions = "Navigate", Module = "ScCRM")]
        public ActionResult ClassRoomMappingsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScCRM");
            var lstClassRoomMappings = _scclassroommappingRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialClassRoomMappingList", lstClassRoomMappings.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult ClassRoomMappingDelete(int classroommappingId)
        {
            ScClassRoomMapping objScClassRoomMapping = _scclassroommappingRepository.GetById(x => x.Id == classroommappingId);
            _scclassroommappingRepository.Delete(objScClassRoomMapping);
            return RedirectToAction("ClassRoomMappings");
        }
        #endregion ClassRoomMappings


        #region Classes
        public JsonResult AcademyClassCodeExists(string Code, int Id)
        {
            var feeterm = new SchClass();
            if (Id != 0)
            {
                var data = _scClassRepository.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm = _scClassRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());
                    if (feeterm == null)
                    {

                    }
                }
            }
            else
            {
                feeterm = _scClassRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new SchClass();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyClassDescriptionExists(string Description, int Id)
        {
            var feeterm = new SchClass();
            if (Id != 0)
            {
                var data = _scClassRepository.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm = _scClassRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());

                }
            }
            else
            {
                feeterm = _scClassRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new SchClass();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetClasses()
        {

            List<SchClass> lstClasses = _scClassRepository.GetAll().OrderBy(x => x.Schedule).ToList();

            var classes = lstClasses.Select(x => new
                                                     {
                                                         Id = x.Id,
                                                         Code = x.Code,
                                                         Description = x.Description
                                                     });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }


        [CheckPermission(Permissions = "Navigate", Module = "ScCls")]
        public ActionResult Classes()
        {
            ViewBag.UserRight = base.UserRight("ScCls");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScCls")]
        public ActionResult ListClasses(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScCls");
            var lstClass = _scClassRepository.GetAll().OrderBy(x => x.Schedule);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialListClasses", lstClass.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
        [CheckPermission(Permissions = "Create", Module = "ScCls")]
        public ActionResult AddClass()
        {

            var viewModel = new SchClass();
            viewModel.Schedule = _scClassRepository.GetAll().Count() + 1;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult AddClass(SchClass modelSchClass)
        {
            if (ModelState.IsValid)
            {
                SchClass objSchClass =
                    _scClassRepository.Filter(x => x.Code.Equals(modelSchClass.Code)).FirstOrDefault();
                if (objSchClass != null)
                {
                    return Json(new { bcodeexists = true });
                }
                _scClassRepository.Add(modelSchClass);
            }
            return null;
        }
        [CheckPermission(Permissions = "Edit", Module = "ScCls")]
        public ActionResult EditClass(int classId)
        {
            SchClass objSchClass = _scClassRepository.GetById(x => x.Id == classId);

            return PartialView("PartialEditClass", objSchClass);
        }

        [HttpPost]
        public ActionResult EditClass(SchClass modelSchClass)
        {
            SchClass objSchClass = _scClassRepository.GetById(x => x.Id == modelSchClass.Id);
            objSchClass.Code = modelSchClass.Code;
            objSchClass.Description = modelSchClass.Description;
            objSchClass.Schedule = modelSchClass.Schedule;
            objSchClass.Incharge = modelSchClass.Incharge;
            _scClassRepository.Update(objSchClass);
            return null;
        }

        public ActionResult DeleteClass(int classId)
        {
            SchClass objSchClass = _scClassRepository.GetById(x => x.Id == classId);
            var mapped = _scClassRepository.DeleteException(objSchClass);
            return Json(mapped, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetStudents()
        {
            List<ScStudentinfo> lstStudent = _scStudentinfoRepository.GetAll().ToList();

            var data = lstStudent.Select(x => new
                                                  {
                                                      Id = x.StudentID,
                                                      Code = x.StdCode,
                                                      Description = x.StuDesc
                                                  });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRegistrationStudents()
        {


            List<ScStudentinfo> lstStudent = _scStudentRegistrationDetailRepository.GetAll().Select(x => x.Studentinfo).ToList();

            var data = lstStudent.Select(x => new
            {
                Id = x.StudentID,
                RegCode = x.StdCode,
                Description = x.StuDesc,
                Class = x.Class.Description,

            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRegistrationStudentsForSLC()
        {


            List<ScStudentinfo> lstStudent = _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.AcademicYearId == AcademicYear.Id).Select(x => x.Studentinfo).ToList();

            var data = lstStudent.Select(x => new
            {
                Id = x.StudentID,
                RegCode = x.StdCode,
                Description = x.StuDesc


            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRegistrationNumber(int studentId)
        {
            ScStudentinfo Student = _scStudentinfoRepository.GetById(x => x.StudentID == studentId);
            if (Student.Regno == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(Student.Regno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //Action related to section

        #region Sections
        public JsonResult AcademySectionDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScSection();
            if (Id != 0)
            {
                var data = _scSectionRepository.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm = _scSectionRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());

                }
            }
            else
            {
                feeterm = _scSectionRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScSection();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Create", Module = "ScSec")]
        public ActionResult OnlineSectionAdd()
        {
            var viewModel = new ScSection();
            return PartialView("_PartialOnlineSectionAdd", viewModel);
        }
        [HttpPost]
        public ActionResult OnlineSectionAdd(ScSection model)
        {

            if (model.Description != null)
            {
                ScSection objSchSection =
                    _scSectionRepository.GetById(
                        x => x.Description.ToLower().Trim() == model.Description.ToLower().Trim());
                if (objSchSection != null)
                {
                    return Json(new { msg = "false" }, JsonRequestBehavior.AllowGet);
                }
                _scSectionRepository.Add(model);
            }
            else
            {
                return Json(new { msg = "false" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { msg = "true", id = model.Id, title = model.Description }, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSec")]
        public ActionResult Sections()
        {
            ViewBag.UserRight = base.UserRight("ScSec");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSec")]
        public ActionResult ListSections(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScSec");
            var lstSection = _scSectionRepository.GetAll().OrderBy(x => x.Description);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialListSection",
                               lstSection.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
        [CheckPermission(Permissions = "Create", Module = "ScSec")]
        public ActionResult AddSection()
        {
            var viewModel = new ScSection();
            return PartialView("_PartialAddSection", viewModel);
        }

        [HttpPost]
        public ActionResult AddSection(ScSection modelScSection, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                ScSection objSchSection =
                    _scSectionRepository.Filter(x => x.Description.Equals(modelScSection.Description)).FirstOrDefault();
                if (objSchSection != null)
                {
                    return Json(new { bcodeexists = true });
                }
                _scSectionRepository.Add(modelScSection);
            }
            return null;
        }
        [CheckPermission(Permissions = "Edit", Module = "ScSec")]
        public ActionResult EditSection(int sectionId)
        {
            ScSection objSchSection = _scSectionRepository.GetById(x => x.Id == sectionId);
            return PartialView("_PartialEditSection", objSchSection);
        }

        [HttpPost]
        public ActionResult EditSection(ScSection modelScSection)
        {

            _scSectionRepository.Update(modelScSection);
            return null;
        }

        public ActionResult DeleteSection(int sectionId)
        {
            //ScSection objSchSection = _scSectionRepository.GetById(x => x.Id == sectionId);
            var detail = _scStudentRegistrationDetailRepository.GetById(x => x.SectionId == sectionId);
            var sectiondelete = false;
            if (detail == null)
            {
                _scSectionRepository.Delete(x => x.Id == sectionId);
                sectiondelete = true;
            }

            return Json(sectiondelete, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetSections()
        {
            var lstSections = _scSectionRepository.GetAll().Select(x => new
                                                                            {
                                                                                Id = x.Id,
                                                                                Description = x.Description,
                                                                                Memo = x.Memo
                                                                            });
            return Json(lstSections, JsonRequestBehavior.AllowGet);
        }

        #endregion


        //Section related to student category





        //Subject

        #region Subject
        public JsonResult AcademySubjectDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScSubject();
            if (Id != 0)
            {
                var data = _subjectRepository.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm = _subjectRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());

                }
            }
            else
            {
                feeterm = _subjectRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScSubject();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademySubjectCodeExists(string Code, int Id)
        {
            var feeterm = new ScSubject();
            if (Id != 0)
            {
                var data = _subjectRepository.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm = _subjectRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());

                }
            }
            else
            {
                feeterm = _subjectRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScSubject();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSubjects(string childrenlist)
        {

            List<ScSubject> lstSubject = _subjectRepository.GetAll().OrderBy(x => x.Schedule).ToList();

            var data = lstSubject.Select(x => new
                                                  {
                                                      Id = x.Id,
                                                      Code = x.Code,
                                                      Description = x.Description
                                                  });
            var lis = new List<int>();
            if (!string.IsNullOrEmpty(childrenlist))
            {
                var li = childrenlist.Split(',');
                foreach (var s in li)
                {
                    lis.Add(s.ToInt32());
                }

                data = data.Where(x => !lis.Contains(x.Id)).OrderBy(x => x.Description).ToList();

            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubjectsById(int id)
        {
            var data = _subjectRepository.GetById(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Navigate", Module = "ScSub")]
        public ActionResult Subjects()
        {
            ViewBag.UserRight = base.UserRight("ScSub");
            return View("ScSubject");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSub")]
        public ActionResult SubjectList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScSub");
            var data = _subjectRepository.GetAll().OrderBy(x => x.Schedule);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("ScSubjectList", data.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
        [CheckPermission(Permissions = "Create", Module = "ScSub")]
        public ActionResult SubjectAdd()
        {
            var data = this.BuildAcademicsSubjectAddViewModel();
            var list = _subjectRepository.GetAll();

            var lastOrDefault = list.LastOrDefault();
            if (lastOrDefault != null) data.ScSubject.Schedule = lastOrDefault.Schedule + 1;


            return PartialView("ScSubjectAdd", data);
        }

        [HttpPost]
        public ActionResult SubjectAdd(AcademicsSubjectViewModel model)
        {
            try
            {
                var data = _subjectRepository.GetById(x => x.Description == model.ScSubject.Description);
                if (data == null)
                {
                    _subjectRepository.Add(model.ScSubject);
                    return Content("True");
                }
                else
                {
                    return Content("Description Already Exists !");
                }
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }


        }

        [CheckPermission(Permissions = "Edit", Module = "ScSub")]
        public ActionResult SubjectEdit(int id)
        {
            var data = this.BuildAcademicsSubjectEditViewModel(id);
            return PartialView("ScSubjectEdit", data);
        }

        [HttpPost]
        public ActionResult SubjectEdit(AcademicsSubjectViewModel model)
        {
            try
            {
                var subject = _subjectRepository.GetById(x => x.Id == model.ScSubject.Id);

                var data =
                    _subjectRepository.GetById(
                        x => x.Description.Equals(model.ScSubject.Description) && x.Description != subject.Description);
                if (data == null)
                {
                    var _dataContext = new DataContext();
                    _dataContext.Entry(model.ScSubject).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                    //_subjectRepository.Update(model.ScSubject);
                    return Content("True");
                }
                else
                {
                    return Content("Description Already Exists !");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }


        }


        public JsonResult SubjectDelete(int id)
        {
            var content = _subjectRepository.DeleteException(x => x.Id == id);

            return Json(content, JsonRequestBehavior.AllowGet);
        }

        public AcademicsSubjectViewModel BuildAcademicsSubjectAddViewModel()
        {
            var viewModel = base.CreateViewModel<AcademicsSubjectViewModel>();
            viewModel.ClassType =
                new SelectList(
                    Enum.GetValues(typeof(ClassType)).Cast<ClassType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.EvaluationForPass =
                new SelectList(
                    Enum.GetValues(typeof(EvaluateForPass)).Cast<EvaluateForPass>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.ResultSystem =
                new SelectList(
                    Enum.GetValues(typeof(ResultSystem)).Cast<ResultSystem>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.SubjectType =
                new SelectList(
                    Enum.GetValues(typeof(SubjectType)).Cast<SubjectType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.ScSubject = new ScSubject();
            return viewModel;
        }

        public JsonResult GetEnumClassTypeList()
        {

            var list =
                new SelectList(
                    Enum.GetValues(typeof(ClassType)).Cast<ClassType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            var data = list.Select(x => new
                                            {
                                                Id = x.Value,
                                                Description = x.Text
                                            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEnumResultSystemList()
        {

            var list =
                new SelectList(
                    Enum.GetValues(typeof(ResultSystem)).Cast<ResultSystem>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            var data = list.Select(x => new
                                            {
                                                Id = x.Value,
                                                Description = x.Text
                                            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEnumEvaluateForPassList()
        {

            var list =
                new SelectList(
                    Enum.GetValues(typeof(EvaluateForPass)).Cast<EvaluateForPass>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            var data = list.Select(x => new
                                            {
                                                Id = x.Value,
                                                Description = x.Text
                                            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEnumSubjectTypeList()
        {

            var list =
                new SelectList(
                    Enum.GetValues(typeof(SubjectType)).Cast<SubjectType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            var data = list.Select(x => new
                                            {
                                                Id = x.Value,
                                                Description = x.Text
                                            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetClassType(int enumId)
        //{
        //    var data = UtilityService.GetClassType(enumId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetEvaluateForPass(int enumId)
        //{
        //    var data = UtilityService.GetEvaluateForPass(enumId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetSubjectType(int enumId)
        //{
        //    var data = UtilityService.GetSubjectType(enumId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetResultSystem(int enumId)
        //{
        //    var data = UtilityService.GetResultSystem(enumId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetSubjectChange(int resultSystemId, int evaluateforPassId, int subjectTypeId, int classTypeId)
        {
            var resultType = UtilityService.GetResultSystem(resultSystemId);
            var evaluateType = UtilityService.GetEvaluateForPass(evaluateforPassId);
            var subjectName = UtilityService.GetSubjectType(subjectTypeId);
            var classType = UtilityService.GetClassType(classTypeId);
            return
                Json(
                    new
                        {
                            resultType = resultType,
                            evaluateType = evaluateType,
                            subjectName = subjectName,
                            classType = classType
                        }, JsonRequestBehavior.AllowGet);
        }

        public AcademicsSubjectViewModel BuildAcademicsSubjectEditViewModel(int id)
        {
            var data = _subjectRepository.GetById(id);
            var viewModel = base.CreateViewModel<AcademicsSubjectViewModel>();
            viewModel.ClassType =
                new SelectList(
                    Enum.GetValues(typeof(ClassType)).Cast<ClassType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text",
                    data.ClassType);
            viewModel.EvaluationForPass =
                new SelectList(
                    Enum.GetValues(typeof(EvaluateForPass)).Cast<EvaluateForPass>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text",
                    data.EvaluateForType);
            viewModel.ResultSystem =
                new SelectList(
                    Enum.GetValues(typeof(ResultSystem)).Cast<ResultSystem>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text",
                    data.ResultSystem);
            viewModel.SubjectType =
                new SelectList(
                    Enum.GetValues(typeof(SubjectType)).Cast<SubjectType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text",
                    data.Type);
            viewModel.ScSubject = data;
            return viewModel;
        }

        #endregion

        #region Class Wise Subject

        public ActionResult ClassSubjectMappingDetailByClassId(int classId)
        {
            var data = _classSubjectMappingRepository.GetMany(x => x.ClassId == classId).ToList();
            return PartialView("_PartialClassSubjectDetails", data);
        }

        //public ActionResult ClassRatemappingByClassId(int classId)
        //{
        //    var data=_cla
        //}

        public JsonResult GetClassesSubject()
        {
            var classe = _classSubjectMappingRepository.GetAll().Select(x => x.ClassId);

            List<SchClass> lstClasses =
                _scClassRepository.GetMany(x => !classe.Contains(x.Id)).OrderBy(x => x.Schedule).ToList();

            var classes = lstClasses.Select(x => new
                                                     {
                                                         Id = x.Id,
                                                         Code = x.Code,
                                                         Description = x.Description
                                                     });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClassesCodeById(int id)
        {
            var data = _scClassRepository.GetById(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScCSM")]
        public ActionResult ClassSubjectMappings()
        {
            ViewBag.UserRight = base.UserRight("ScCSM");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScCSM")]
        public ActionResult ClassSubjectMappingList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScCSM");
            var data = _classSubjectMappingRepository.GetAll().GroupBy(x => x.ClassId).ToList();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            //return PartialView(data);
            return PartialView("ClassSubjectMappingList", data.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }
        [CheckPermission(Permissions = "Create", Module = "ScCSM")]
        public ActionResult ClassSubjectMappingAdd()
        {
            var data = new ScClassSubjectMapping();
            return PartialView(data);

        }

        public ActionResult ClassSubjectEntry()
        {
            var data = new ScClassSubjectMapping();
            return PartialView("_PartialClassSubjectEntry", data);
        }

        [HttpPost]
        public ActionResult ClassSubjectMappingAdd(ScClassSubjectMapping model,
                                                   IEnumerable<ScClassSubjectMapping> subjectEntry)
        {

            foreach (var item in subjectEntry.Where(x => x.SubjectId != 0))
            {
                var data = new ScClassSubjectMapping()
                               {
                                   ClassId = model.ClassId,
                                   SubjectId = item.SubjectId,
                                   EvaluateForType = item.EvaluateForType,
                                   ClassType = item.ClassType,
                                   ResultSystem = item.ResultSystem,
                                   SubjectType = item.SubjectType,
                                   Narration = item.Narration
                               };
                _classSubjectMappingRepository.Add(data);
            }
            return Content("True");
        }
        [CheckPermission(Permissions = "Edit", Module = "ScCSM")]
        public ActionResult ClassSubjectMappingEdit(int classId)
        {
            Session["ClassId"] = classId;

            var data = _classSubjectMappingRepository.GetMany(x => x.ClassId == classId);
            return PartialView(data);

        }

        [HttpPost]
        public ActionResult ClassSubjectMappingEdit(ScClassSubjectMapping model,
                                                    IEnumerable<ScClassSubjectMapping> subjectEntry)
        {
            var classid = Convert.ToInt32(Session["ClassId"]);
            _classSubjectMappingRepository.Delete(x => x.ClassId == classid);

            foreach (var item in subjectEntry.Where(x => x.SubjectId != 0))
            {
                var data = new ScClassSubjectMapping()
                               {
                                   ClassId = model.ClassId,
                                   SubjectId = item.SubjectId,
                                   EvaluateForType = item.EvaluateForType,
                                   ClassType = item.ClassType,
                                   ResultSystem = item.ResultSystem,
                                   SubjectType = item.SubjectType,
                                   Narration = item.Narration
                               };
                _classSubjectMappingRepository.Add(data);
            }
            return Content("True");
        }


        public ActionResult ClassSubjectMappingDelete(int classId)
        {
            _classSubjectMappingRepository.Delete(x => x.ClassId == classId);

            return RedirectToAction("ClassSubjectMappings");
        }

        #endregion

        #region Student Wise Subject
        [CheckPermission(Permissions = "Create", Module = "SCSSM")]
        public JsonResult GetStudentClass()
        {

            List<SchClass> lstClasses = _scClassRepository.GetAll().OrderBy(x => x.Schedule).ToList();

            var classes = lstClasses.Select(x => new
                                                     {
                                                         Id = x.Id,
                                                         Code = x.Code,
                                                         Description = x.Description
                                                     });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentsSubject()
        {
            var academicYearId = CurrentAcademyYearId;
            var student = _studentSubjectMappingRepository.GetMany(x => x.AcademicYearId == academicYearId).Select(x => x.StudentId);
            List<ScStudentinfo> lstStudent =
                _scStudentinfoRepository.GetMany(x => !student.Contains(x.StudentID)).OrderBy(x => x.StudentID).ToList();

            var data = lstStudent.Select(x => new
                                                  {
                                                      Id = x.StudentID,
                                                      Code = x.StdCode,
                                                      Description = x.StuDesc
                                                  });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentsSubjectByClassId(int id)
        {
            var student = _studentSubjectMappingRepository.GetMany(x => x.ClassId == id && x.AcademicYearId == CurrentAcademyYearId).Select(x => x.StudentId);
            var lstStudent =
                _scStudentRegistrationDetailRepository.GetMany(x => !student.Contains(x.StudentId) && x.StudentRegistration.ClassId == id && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).OrderBy(x => x.StudentId).ToList();

            var data = lstStudent.Select(x => new
            {
                Id = x.StudentId,
                Code = x.Studentinfo.StdCode,
                Description = x.Studentinfo.StuDesc
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentById(int id)
        {
            var data = _scStudentinfoRepository.GetById(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentDetailById(int id)
        {
            var data = _scStudentRegistrationDetailRepository.GetById(m => m.StudentId == id);
            return Json(data.Section != null ? new { StdCode = data.Studentinfo.StdCode, Regno = data.Studentinfo.Regno, SectionDes = data.Section.Description } : new { StdCode = data.Studentinfo.StdCode, Regno = data.Studentinfo.Regno, SectionDes = "-" }, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "navigate", Module = "SCSSM")]
        public ActionResult StudentSubjectMappings()
        {
            ViewBag.UserRight = base.UserRight("ScCSM");
            return View();
        }
        [CheckPermission(Permissions = "navigate", Module = "SCSSM")]
        public ActionResult StudentSubjectMappingList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScCSM");
            //  var data = _studentSubjectMappingRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId).GroupBy(x => x.StudentId).ToList();
            var data = from d in _studentSubjectMappingRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId)
                       group d by new { d.ClassId, d.StudentId }
                           into g
                           select g.ToList();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            //return PartialView(data);
            return PartialView("StudentSubjectMappingList", data.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult StudentSubjectMappingDetailByClassId(int classId, int studentId)
        {

            var data =
                _studentSubjectMappingRepository.GetMany(x => x.ClassId == classId && x.StudentId == studentId && x.AcademicYearId == CurrentAcademyYearId).ToList();
            return PartialView("_PartialStudentSubjectDetails", data);
        }

        public ActionResult StudentSubjectMappingAdd()
        {
            var data = new ScStudentSubjectMapping();
            data.AcademicYearId = CurrentAcademyYearId;
            return PartialView(data);

        }

        public ActionResult StudentSubjectEntry()
        {
            var data = new ScStudentSubjectMapping();
            return PartialView("_PartialStudentSubjectEntry", data);
        }

        [HttpPost]
        public ActionResult StudentSubjectMappingAdd(ScStudentSubjectMapping model,
                                                     IEnumerable<ScStudentSubjectMapping> subjectEntry)
        {

            foreach (var item in subjectEntry.Where(x => x.SubjectId != 0))
            {
                var data = new ScStudentSubjectMapping()
                               {
                                   ClassId = model.ClassId,
                                   StudentId = model.StudentId,
                                   SubjectId = item.SubjectId,
                                   EvaluateForType = item.EvaluateForType,
                                   ClassType = item.ClassType,
                                   ResultSystem = item.ResultSystem,
                                   SubjectType = item.SubjectType,
                                   Narration = item.Narration,
                                   AcademicYearId = model.AcademicYearId
                               };
                _studentSubjectMappingRepository.Add(data);
            }
            return Content("True");
        }
        public ActionResult GetOptionalSubjectByClassId(int id, string childrenlist)
        {
            var subjectype = Convert.ToInt32(KRBAccounting.Enums.SubjectType.Optional);
            var data =
                _classSubjectMappingRepository.GetMany(x => x.ClassId == id && x.SubjectType == subjectype).ToList();
            var list = data.Select(x => new
                                            {
                                                Id = x.SubjectId,
                                                Description = x.Subject.Description,
                                                Code = x.Subject.Code
                                            });
            var lis = new List<int>();
            if (!string.IsNullOrEmpty(childrenlist))
            {
                var li = childrenlist.Split(',');
                foreach (var s in li)
                {
                    lis.Add(s.ToInt32());
                }
                list = list.Where(x => !lis.Contains(x.Id)).OrderBy(x => x.Description).ToList();

            }


            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Edit", Module = "SCSSM")]
        public ActionResult StudentSubjectMappingEdit(int classId, int studentId)
        {
            Session["ClassId"] = classId;
            Session["studentId"] = studentId;

            var data = _studentSubjectMappingRepository.GetMany(x => x.ClassId == classId && x.StudentId == studentId && x.AcademicYearId == CurrentAcademyYearId);
            if (data.Any())
            {
                Session["AcademicYearId"] = data.First().AcademicYearId;
            }
            else
            {
                Session["AcademicYearId"] = CurrentAcademyYearId;
            }

            return PartialView(data);

        }

        [HttpPost]
        public ActionResult StudentSubjectMappingEdit(ScStudentSubjectMapping model,
                                                      IEnumerable<ScStudentSubjectMapping> subjectEntry)
        {
            var classid = Convert.ToInt32(Session["ClassId"]);
            var studentId = Convert.ToInt32(Session["studentId"]);
            var academicyearid = Convert.ToInt32(Session["AcademicYearId"]);
            _studentSubjectMappingRepository.Delete(x => x.ClassId == classid && x.StudentId == studentId && x.AcademicYearId == CurrentAcademyYearId);

            foreach (var item in subjectEntry.Where(x => x.SubjectId != 0))
            {
                var data = new ScStudentSubjectMapping()
                               {
                                   ClassId = model.ClassId,
                                   StudentId = model.StudentId,
                                   SubjectId = item.SubjectId,
                                   EvaluateForType = item.EvaluateForType,
                                   ClassType = item.ClassType,
                                   ResultSystem = item.ResultSystem,
                                   SubjectType = item.SubjectType,
                                   Narration = item.Narration,
                                   AcademicYearId = academicyearid
                               };
                _studentSubjectMappingRepository.Add(data);
            }
            return Content("True");
        }


        public ActionResult StudentSubjectMappingDelete(int classId, int studentId)
        {
            _studentSubjectMappingRepository.Delete(x => x.ClassId == classId && x.StudentId == studentId && x.AcademicYearId == CurrentAcademyYearId);

            return RedirectToAction("StudentSubjectMappings");
        }

        #endregion

        #region Subject Wise Student

        public ActionResult GetOptionalSubjectByClassIdNotmapped(int id)
        {
            var subjectype = Convert.ToInt32(KRBAccounting.Enums.SubjectType.Optional);
            var mapping = _studentSubjectMappingRepository.GetMany(x => x.ClassId == id && x.AcademicYearId == CurrentAcademyYearId).Select(x => x.SubjectId);
            var data =
                _classSubjectMappingRepository.GetMany(x => x.ClassId == id && !mapping.Contains(x.SubjectId) && x.SubjectType == subjectype).ToList();
            var list = data.Select(x => new
            {
                Id = x.SubjectId,
                Description = x.Subject.Description,
                Code = x.Subject.Code
            });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "navigate", Module = "ScSubStd")]
        public ActionResult SubjectStudentMappings()
        {
            ViewBag.UserRight = base.UserRight("ScSubStd");
            return View();
        }
        [CheckPermission(Permissions = "navigate", Module = "ScSubStd")]
        public ActionResult SubjectStudentMappingList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScSubStd");
            var data = from d in _studentSubjectMappingRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId)
                       group d by new { d.ClassId, d.SubjectId }
                           into g
                           select g.ToList();
            //return PartialView(data);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("SubjectStudentMappingList", data.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult SubjectStudentMappingDetailByClassId(int classId, int subjectId)
        {
            var data =
                _studentSubjectMappingRepository.GetMany(
                    x => x.ClassId == classId && x.SubjectId == subjectId && x.AcademicYearId == CurrentAcademyYearId).
                    Select(x => new StudentSubjectMappingViewModel
                                    {

                                        StudentSubjectMapping = x,
                                        Section = _scStudentRegistrationDetailRepository.GetById(m => m.StudentId == x.StudentId).Section != null ? _scStudentRegistrationDetailRepository.GetById(m => m.StudentId == x.StudentId).Section.Description : null

                                    }).

                    ToList();
            return PartialView("_PartialSubjectStudentDetails", data);
        }


        [CheckPermission(Permissions = "navigate", Module = "ScCTM")]
        public ActionResult ClassTeacherMapping()
        {
            ViewBag.UserRight = base.UserRight("ScCTM");
            return View();
        }
        [CheckPermission(Permissions = "create", Module = "ScCTM")]
        public ActionResult ClassTeacherMappingAdd()
        {
            var data = new ScClassTeacherMapping();
            return PartialView(data);

        }

        public ActionResult ClassTeacherMappingDetailAdd()
        {

            var modellink = new ScClassTeacherMapping();

            return PartialView("_PartialClassTeacherMapping", modellink);
        }
        [HttpPost]
        public ActionResult ClassTeacherMappingAdd(ScClassTeacherMapping model,
                                                     IEnumerable<ScClassTeacherMapping> studentEntry)
        {

            foreach (var item in studentEntry.Where(x => x.TeacherId != 0))
            {
                //var subject = _subjectRepository.GetById(x => x.Id == model.SubjectId);
                var data = new ScClassTeacherMapping()
                {
                    ClassId = model.ClassId,
                    SectionId = model.SectionId,
                    TeacherId = item.TeacherId

                };
                _classTeacherMapping.Add(data);
            }
            return Content("True");
        }
        [CheckPermission(Permissions = "edit", Module = "ScCTM")]
        public ActionResult ClassTeacherMappingEdit(int classId, int sectionId)
        {
            Session["ClassId"] = classId;
            Session["subjectId"] = sectionId;
            var data = _classTeacherMapping.GetMany(x => x.ClassId == classId && x.SectionId == sectionId);
            //return View("ClassTeacherMappingEdit", data);
            var view = this.RenderPartialViewToString("ClassTeacherMappingEdit", data);
            return Json(view, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult ClassTeacherMappingEdit(ScClassTeacherMapping model,
                                                      IEnumerable<ScClassTeacherMapping> studentEntry)
        {
            var classid = Convert.ToInt32(Session["ClassId"]);
            var subjectId = Convert.ToInt32(Session["subjectId"]);
            _classTeacherMapping.Delete(x => x.ClassId == classid && x.SectionId == subjectId);
            //var subject = _subjectRepository.GetById(x => x.Id == model.SubjectId);

            foreach (var item in studentEntry.Where(x => x.TeacherId != 0))
            {

                //var subject = _subjectRepository.GetById(x => x.Id == model.SubjectId);
                var data = new ScClassTeacherMapping()
                {
                    ClassId = model.ClassId,
                    SectionId = model.SectionId,
                    TeacherId = item.TeacherId

                };
                _classTeacherMapping.Add(data);

            }
            return Content("True");
        }

        [CheckPermission(Permissions = "navigate", Module = "ScCTM")]
        public ActionResult ClassTeacherMappingList(int pageNo = 1)
        {

            ViewBag.UserRight = base.UserRight("ScCTM");
            var data = from d in _classTeacherMapping.GetAll()
                       group d by new { d.ClassId } into g
                       select g.ToList();
            //return PartialView(data);
            // data = _classTeacherMapping.GetAll();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView(data.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }
        [CheckPermission(Permissions = "Create", Module = "ScSubStd")]
        public ActionResult SubjectStudentMappingAdd()
        {

            var data = new ScStudentSubjectMapping();
            return PartialView(data);

        }


        public ActionResult SubjectStudentEntry()
        {
            var data = new ScStudentSubjectMapping();
            return PartialView("_PartialSubjectStudentEntry", data);
        }

        [HttpPost]
        public ActionResult SubjectStudentMappingAdd(ScStudentSubjectMapping model,
                                                     IEnumerable<ScStudentSubjectMapping> studentEntry)
        {

            foreach (var item in studentEntry.Where(x => x.StudentId != 0))
            {
                var subject = _subjectRepository.GetById(x => x.Id == model.SubjectId);
                var data = new ScStudentSubjectMapping()
                {
                    ClassId = model.ClassId,
                    StudentId = item.StudentId,
                    SubjectId = model.SubjectId,
                    EvaluateForType = subject.EvaluateForType,
                    ClassType = subject.ClassType,
                    ResultSystem = subject.ResultSystem,
                    SubjectType = subject.Type,
                    Narration = item.Narration,
                    AcademicYearId = CurrentAcademyYearId
                };
                _studentSubjectMappingRepository.Add(data);
            }
            return Content("True");
        }
        [CheckPermission(Permissions = "Edit", Module = "ScSubStd")]
        public ActionResult SubjectStudentMappingEdit(int classId, int subjectid)
        {
            Session["ClassId"] = classId;
            Session["subjectId"] = subjectid;
            var data = _studentSubjectMappingRepository.GetMany(x => x.ClassId == classId && x.SubjectId == subjectid && x.AcademicYearId == CurrentAcademyYearId);
            foreach (var item in data)
            {
                var info = _scStudentRegistrationDetailRepository.GetById(x => x.StudentId == item.StudentId);
                item.StdCode = info.Studentinfo.StdCode;
                item.Regno = info.Studentinfo.Regno;
                item.Section = info.Section != null ? info.Section.Description : " - ";

            }

            return PartialView(data);

        }

        [HttpPost]
        public ActionResult SubjectStudentMappingEdit(ScStudentSubjectMapping model,
                                                      IEnumerable<ScStudentSubjectMapping> studentEntry)
        {
            var classid = Convert.ToInt32(Session["ClassId"]);
            var subjectId = Convert.ToInt32(Session["subjectId"]);
            _studentSubjectMappingRepository.Delete(x => x.ClassId == classid && x.SubjectId == subjectId && x.AcademicYearId == CurrentAcademyYearId);
            //var subject = _subjectRepository.GetById(x => x.Id == model.SubjectId);

            foreach (var item in studentEntry.Where(x => x.StudentId != 0))
            {
                var subject = _subjectRepository.GetById(x => x.Id == model.SubjectId);
                var data = new ScStudentSubjectMapping()
                {
                    ClassId = model.ClassId,
                    StudentId = item.StudentId,
                    SubjectId = model.SubjectId,
                    EvaluateForType = subject.EvaluateForType,
                    ClassType = subject.ClassType,
                    ResultSystem = subject.ResultSystem,
                    SubjectType = subject.Type,
                    Narration = item.Narration,
                    AcademicYearId = CurrentAcademyYearId
                };
                _studentSubjectMappingRepository.Add(data);
            }
            return Content("True");
        }


        public ActionResult SubjectStudentMappingDelete(int classId, int subjectId)
        {
            _studentSubjectMappingRepository.Delete(x => x.ClassId == classId && x.SubjectId == subjectId && x.AcademicYearId == CurrentAcademyYearId);

            return RedirectToAction("SubjectStudentMappings");
        }

        #endregion



        //Section related to grades

        #region Division
        public JsonResult AcademyDivisionDescriptionExists(string Description, int? Id)
        {
            var feeterm = new ScDivision();
            if (Id != 0)
            {
                var data = _scDivisionRepository.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm = _scDivisionRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());

                }
            }
            else
            {
                feeterm = _scDivisionRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScDivision();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScD")]
        public ActionResult Division()
        {
            var dataContxt = new DataContext();
            ViewBag.UserRight = base.UserRight("ScD");
            var division = _scDivisionRepository.GetAll().OrderBy(x => x.Schedule);
            if (division.Any())
            {
                ViewBag.DivisionName = division.Select(x => x.Description).ToList();
                var to = division.Select(x => x.PercentageTo);
                var from = division.Select(x => x.PercentageFrom);
                ViewBag.DivisionValue = from.Zip(to, (n, w) => "[" + n + "," + w + "]").ToList();
            }

            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScD")]
        public ActionResult DivisionList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScD");
            var lstDivisions = _scDivisionRepository.GetAll();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialDivisionList",
                               lstDivisions.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
        [CheckPermission(Permissions = "Create", Module = "ScD")]
        public ActionResult AddDivision()
        {
            int divisionCount = _scDivisionRepository.GetAll().Count() + 1;
            ScDivision objScDivision = new ScDivision();
            objScDivision.Schedule = divisionCount;
            return PartialView("_PartialDivisionAdd", objScDivision);
        }

        [HttpPost]
        public ActionResult AddDivision(ScDivision modelScDivision)
        {
            _scDivisionRepository.Add(modelScDivision);
            return null;
        }
        [CheckPermission(Permissions = "Edit", Module = "ScD")]
        public ActionResult EditDivision(int divId)
        {
            ScDivision objScDivison = _scDivisionRepository.GetById(divId);
            return PartialView("_PartialDivisionEdit", objScDivison);
        }

        [HttpPost]
        public ActionResult EditDivision(ScDivision modelScDivision)
        {
            ScDivision objScDivision = _scDivisionRepository.GetById(modelScDivision.Id);
            objScDivision.Description = modelScDivision.Description;
            objScDivision.Memo = modelScDivision.Memo;
            objScDivision.PercentageFrom = modelScDivision.PercentageFrom;
            objScDivision.PercentageTo = modelScDivision.PercentageTo;
            objScDivision.Schedule = modelScDivision.Schedule;
            _scDivisionRepository.Update(objScDivision);
            return null;
        }

        public ActionResult DeleteDivision(int divisionId)
        {
            ScDivision objSchDivision = _scDivisionRepository.GetById(x => x.Id == divisionId);
            var sectiondelete = _scDivisionRepository.DeleteException(objSchDivision);
            return Json(sectiondelete, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteExtraActivity(int extraId)
        {
            var objSchDivision = _scExtraActivityRepository.GetById(x => x.Id == extraId);
            var sectiondelete = _scExtraActivityRepository.DeleteException(objSchDivision);
            return Json(sectiondelete, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteGrading(int gradingId)
        {
            var objSchDivision = _scGradeRepository.GetById(x => x.Id == gradingId);
            var sectiondelete = _scGradeRepository.DeleteException(objSchDivision);
            return Json(sectiondelete, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteBuilding(int buildingId)
        {
            var objSchDivision = _schBuildingRepository.GetById(x => x.Id == buildingId);
            var sectiondelete = _schBuildingRepository.DeleteException(objSchDivision);
            return Json(sectiondelete, JsonRequestBehavior.AllowGet);
        }


        #endregion

        //section related to grading

        #region Grading

        public JsonResult SchoolGradeCodeNoExists(string Code, int? Id)
        {

            var feeterm = new ScGrade();
            if (Id != 0)
            {
                var data = _scGradeRepository.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm =
                        _scGradeRepository.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower());

                }
            }
            else
            {
                feeterm = _scGradeRepository.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower());
            }
            if (feeterm == null)
            {
                feeterm = new ScGrade();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }
        [CheckPermission(Permissions = "Navigate", Module = "ScG")]
        public ActionResult Grading()
        {
            ViewBag.UserRight = base.UserRight("ScG");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScG")]
        public ActionResult GradeList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScG");
            var lstGrades = _scGradeRepository.GetAll();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialGradeList",
                               lstGrades.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
        [CheckPermission(Permissions = "Create", Module = "ScG")]
        public ActionResult AddGrade()
        {
            return PartialView("_PartialGradeAdd", new ScGrade());
        }

        [HttpPost]
        public ActionResult AddGrade(ScGrade modelScGrade)
        {
            _scGradeRepository.Add(modelScGrade);
            return null;
        }
        [CheckPermission(Permissions = "Edit", Module = "ScG")]
        public ActionResult EditGrade(int gradeId)
        {
            ScGrade objScGrade = _scGradeRepository.GetById(gradeId);
            return PartialView("_PartialGradeEdit", objScGrade);
        }

        [HttpPost]
        public ActionResult EditGrade(ScGrade modelScGrade)
        {
            ScGrade objScGrade = _scGradeRepository.GetById(modelScGrade.Id);
            objScGrade.Code = modelScGrade.Code;
            objScGrade.Grade = modelScGrade.Grade;
            objScGrade.Memo = modelScGrade.Memo;
            _scGradeRepository.Update(objScGrade);
            return null;
        }

        #endregion

        //Section related to extraactivity

        #region Activity Master
        public JsonResult ExtraActivityDescriptionNoExists(string Description, int? Id)
        {

            var feeterm = new ScExtraActivity();
            if (Id != 0)
            {
                var data = _scExtraActivityRepository.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm =
                        _scExtraActivityRepository.GetById(x => x.Description.ToLower().Trim() == Description.Trim().ToLower());

                }
            }
            else
            {
                feeterm = _scExtraActivityRepository.GetById(x => x.Description.ToLower().Trim() == Description.Trim().ToLower());
            }
            if (feeterm == null)
            {
                feeterm = new ScExtraActivity();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public JsonResult ExtraActivityCodeNoExists(string Code, int? Id)
        {

            var feeterm = new ScExtraActivity();
            if (Id != 0)
            {
                var data = _scExtraActivityRepository.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm =
                        _scExtraActivityRepository.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower());

                }
            }
            else
            {
                feeterm = _scExtraActivityRepository.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower());
            }
            if (feeterm == null)
            {
                feeterm = new ScExtraActivity();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetActivity()
        {

            List<ScExtraActivity> lstClasses =
                _scExtraActivityRepository.GetAll().ToList();

            var classes = lstClasses.Select(x => new
                                                     {
                                                         Id = x.Id,
                                                         Code = x.Code,
                                                         Description = x.Description,

                                                     });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetActivityStatus()
        {

            var data = new SelectList(
                Enum.GetValues(typeof(ActivityStatus)).Cast
                    <ActivityStatus>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            var classes = data.Select(x => new
                                               {
                                                   Id = x.Value,

                                                   Description = x.Text
                                               });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "scEA")]
        public ActionResult ExtraActivity()
        {
            ViewBag.UserRight = base.UserRight("scEA");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "scEA")]
        public ActionResult ExtraActivityList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("scEA");
            var lstExtraActivity = _scExtraActivityRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialExtraActivityList",
                               lstExtraActivity.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public JsonResult GetExtraActivityList()
        {
            var lstExtraActivity = _scExtraActivityRepository.GetAll().OrderByDescending(x => x.Id);

            var extraactivities = lstExtraActivity.Select(x => new
                                                                   {
                                                                       Id = x.Id,
                                                                       Description = x.Description,
                                                                       Memo = x.Memo
                                                                   });
            return Json(extraactivities, JsonRequestBehavior.AllowGet);
        }

        [CheckPermission(Permissions = "Create", Module = "scEA")]
        public ActionResult AddExtraActivity()
        {
            ScExtraActivity objScExtraActivity = new ScExtraActivity();
            return PartialView("_PartialExtraActivityAdd", objScExtraActivity);
        }

        [HttpPost]
        public ActionResult AddExtraActivity(ScExtraActivity modelScExtraActivity)
        {
            _scExtraActivityRepository.Add(modelScExtraActivity);
            return null;
        }
        [CheckPermission(Permissions = "Edit", Module = "scEA")]
        public ActionResult EditExtraActivity(int id)
        {
            ScExtraActivity objScExtraActivity = _scExtraActivityRepository.GetById(id);
            return PartialView("_PartialExtraActivityEdit", objScExtraActivity);
        }

        [HttpPost]
        public ActionResult EditExtraActivity(ScExtraActivity modelExtraActivity)
        {
            ScExtraActivity objScExtraActivity = _scExtraActivityRepository.GetById(modelExtraActivity.Id);
            objScExtraActivity.Description = modelExtraActivity.Description;
            objScExtraActivity.Memo = modelExtraActivity.Memo;
            objScExtraActivity.Code = modelExtraActivity.Code;
            _scExtraActivityRepository.Update(objScExtraActivity);
            return null;
        }

        #endregion

        #region Student ExtraActivity
        [CheckPermission(Permissions = "Navigate", Module = "scSE")]
        public ActionResult StudentExtraActivities()
        {
            ViewBag.UserRight = base.UserRight("scSE");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "scSE")]
        public ActionResult StudentExtraActivitiesAdd()
        {

            var model = new ScStudentExtraActivity();
            return PartialView("_PartialStudentExtraActivityAdd", model);
        }

        [HttpPost]
        public ActionResult StudentExtraActivitiesAdd(IEnumerable<ScStudentExtraActivityDetails> subjectEntry,
                                                      ScStudentExtraActivity model)
        {
            var user = _authentication.GetAuthenticatedUser();
            model.CreateById = user.Id;
            model.CreateDate = DateTime.UtcNow;
            model.UpdateById = user.Id;
            model.UpdateDate = DateTime.UtcNow;
            model.AcademicYearId = CurrentAcademyYearId;
            _StudentExtraActivityRepository.Add(model);
            foreach (var data in subjectEntry.Where(x => x.StudentId != 0).Select(item => new ScStudentExtraActivityDetails()
                                                                                              {
                                                                                                  MasterId = model.Id,
                                                                                                  Narration = item.Narration,
                                                                                                  StartDate = item.StartDate,
                                                                                                  StartMiti = item.StartMiti,
                                                                                                  StudentId = item.StudentId,
                                                                                                  ActivitiesStatusId = item.ActivitiesStatusId
                                                                                              }))
            {
                _studentExtraActivityDetialRepository.Add(data);
            }
            return Content("True");
        }
        [CheckPermission(Permissions = "Edit", Module = "scSE")]
        public ActionResult StudentExtraActivitiesEdit(int id)
        {
            var model = _StudentExtraActivityRepository.GetById(x => x.Id == id && x.AcademicYearId == CurrentAcademyYearId);
            model.ActivityDetailses = _studentExtraActivityDetialRepository.GetMany(x => x.MasterId == id);


            return PartialView("_PartialStudentExtraActivityEdit", model);
        }

        [HttpPost]
        public ActionResult StudentExtraActivitiesEdit(IEnumerable<ScStudentExtraActivityDetails> subjectEntry,
                                                       ScStudentExtraActivity model)
        {
            var user = _authentication.GetAuthenticatedUser();

            model.UpdateById = user.Id;
            model.UpdateDate = DateTime.UtcNow;

            _StudentExtraActivityRepository.Update(model);
            _studentExtraActivityDetialRepository.Delete(x => x.MasterId == model.Id);
            foreach (var data in subjectEntry.Where(x => x.StudentId != 0).Select(item => new ScStudentExtraActivityDetails()
                                                                                              {
                                                                                                  MasterId = model.Id,
                                                                                                  Narration = item.Narration,
                                                                                                  StartDate = item.StartDate,
                                                                                                  StartMiti = item.StartMiti,
                                                                                                  StudentId = item.StudentId,
                                                                                                  ActivitiesStatusId = item.ActivitiesStatusId
                                                                                              }))
            {
                _studentExtraActivityDetialRepository.Add(data);
            }
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "scSE")]
        public ActionResult StudentExtraActivityList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("scSE");
            var lstExtraActivity = _StudentExtraActivityRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId).OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialStudentExtraActivityList",
                               lstExtraActivity.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult StudentExtraActivityDetail(int SectionId, int ClassId, int ActivityId, int Id)
        {
            if (Id != 0)
            {
                var i = _StudentExtraActivityRepository.GetById(x => x.Id == Id && x.AcademicYearId == CurrentAcademyYearId);
                if (i.SectionId == SectionId && i.SectionId == SectionId && i.ActivityId == ActivityId)
                {
                    return Content("ExistData");
                }
            }



            var housemapping =
                _StudentExtraActivityRepository.GetById(
                    x => x.ClassId == ClassId && x.SectionId == SectionId && x.ActivityId == ActivityId && x.AcademicYearId == CurrentAcademyYearId);
            if (housemapping == null)
            {
                var data =
                    _scStudentRegistrationDetailRepository.GetMany(
                        x => x.SectionId == SectionId && x.StudentRegistration.ClassId == ClassId && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).Select(item => new ScStudentExtraActivityDetails()
                                                   {
                                                       StudentId = item.StudentId,
                                                       Studentinfo = item.Studentinfo,
                                                       MasterId = 0,
                                                       StdCode = item.Studentinfo.StdCode,
                                                       RollNo = item.RollNo
                                                   }).ToList();

                return PartialView("_PartialStudentExtraActivityDetailEntry", data);

            }
            else
            {
                return Content("false");
            }
        }
        public ActionResult StudentExtraActivitiesDelete(int id)
        {
            _StudentExtraActivityRepository.Delete(x => x.Id == id && x.AcademicYearId == CurrentAcademyYearId);


            return RedirectToAction("StudentExtraActivities");
        }


        #endregion

        public JsonResult GetExames()
        {

            var lstExames = _examRepository.GetAll().OrderBy(x => x.Schedule).Select(x => new
              {
                  Id = x.Id,
                  Code = x.Code,
                  Description = x.Description
              });
            return Json(lstExames, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSubjectByFilter()
        {
            //var classess = _examRoutineRepository.GetAll().Select(x => x.ClassId);
            var Subject = _examRoutineRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId).Select(x => x.SubjectId);
            var lstSubject = _subjectRepository.GetMany(x => !Subject.Contains(x.Id)).OrderBy(x => x.Schedule).Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(lstSubject, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExamRoutineClasses(int id)
        {
            var data = _examRoutineRepository.GetMany(x => x.ExamId == id && x.AcademicYearId == CurrentAcademyYearId).Select(x => x.ClassId);
            var lstClasses =
                _scClassRepository.GetMany(x => !data.Contains(x.Id)).OrderBy(x => x.Schedule).Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(lstClasses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentByClassId(int id, string childrenlist)
        {

            var data = _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.ClassId == id && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).Select(x => new
            {
                Id = x.StudentId,

                Description = x.Studentinfo.StuDesc,
                Code = x.Studentinfo.StdCode,
            });
            var lis = new List<int>();
            if (!string.IsNullOrEmpty(childrenlist))
            {
                var li = childrenlist.Split(',');
                foreach (var s in li)
                {
                    lis.Add(s.ToInt32());
                }
                data = data.Where(x => !lis.Contains(x.Id)).OrderBy(x => x.Description).ToList();

            }



            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentByProgramId(int id, string childrenlist)
        {

            var context = new DataContext();
            var sql = " Select s.StudentID StudentId, s.studesc StudentName from ScStudentinfo s join SchClass c on c.Id=  s.ClassId join ScProgramMaster p on p.Id=c.ProgramId join ScStudentRegistration r on r.ClassId=c.Id where p.Id=" + id;
            var result = context.Database.SqlQuery<GetStudentByProgramId>(sql).ToList();
            var lis = new List<int>();
            if (!string.IsNullOrEmpty(childrenlist))
            {
                var li = childrenlist.Split(',');
                foreach (var s in li)
                {
                    lis.Add(s.ToInt32());
                }
                result = result.Where(x => !lis.Contains(x.StudentId)).OrderBy(x => x.StudentId).ToList();

            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetStudentByClassIdArray(int id)
        {

            var data = _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.ClassId == id && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).Select(x => new
            {
                Id = x.StudentId,

                Description = x.Studentinfo.StuDesc,
                Code = x.Studentinfo.StdCode,
            });



            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentByClassIdSection(int id, int nextid)
        {
            if (nextid != 0)
            {
                var data = _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.ClassId == id && x.SectionId == nextid && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).Select(x => new
                {
                    Id = x.StudentId,

                    Description = x.Studentinfo.StuDesc,
                    Code = x.Studentinfo.StdCode,
                });



                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.ClassId == id && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).Select(x => new
                {
                    Id = x.StudentId,

                    Description = x.Studentinfo.StuDesc,
                    Code = x.Studentinfo.StdCode,
                });



                return Json(data, JsonRequestBehavior.AllowGet);
            }



        }

        public JsonResult GetDivisionRank(decimal percent, int classId, int examId)
        {
            var division = _scDivisionRepository.GetById(x => x.PercentageFrom <= percent && x.PercentageTo >= percent);
            if (division != null)
            {
                return Json(new { divisionDescription = division.Description, id = division.Id }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Set Division First", JsonRequestBehavior.AllowGet);
            }




        }


        #region Shifts

        public JsonResult GetShifts()
        {

            List<ScShift> lstClasses =
                _scshiftRepository.GetAll().OrderBy(x => x.DispalyOrder).ToList();

            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcademyShiftCodeExists(string Code, int Id)
        {
            var feeterm = new ScShift();
            if (Id != 0)
            {
                var data = _scshiftRepository.GetById(x => x.Id == Id);
                if (data.Code != Code)
                {
                    feeterm = _scshiftRepository.GetById(x => x.Code == Code);
                    if (feeterm == null)
                    {

                    }
                }
            }
            else
            {
                feeterm = _scshiftRepository.GetById(x => x.Code == Code);
            }
            if (feeterm == null)
            {
                feeterm = new ScShift();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyShiftDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScShift();
            if (Id != 0)
            {
                var data = _scshiftRepository.GetById(x => x.Id == Id);
                if (data.Description != Description)
                {
                    feeterm = _scshiftRepository.GetById(x => x.Description == Description);
                    if (feeterm == null)
                    {

                    }
                }
            }
            else
            {
                feeterm = _scshiftRepository.GetById(x => x.Description == Description);
            }
            if (feeterm == null)
            {
                feeterm = new ScShift();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSh")]
        public ActionResult Shifts()
        {
            ViewBag.UserRight = base.UserRight("ScSh");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScSh")]
        public ActionResult ShiftAdd()
        {

            var data = _scshiftRepository.GetAll();
            var model = new ScShift();

            var lastOrDefault = data.LastOrDefault();
            if (lastOrDefault != null) model.DispalyOrder = lastOrDefault.DispalyOrder + 1;


            model.IsActive = true;
            return PartialView("_PartialShiftAdd", model);
        }

        [HttpPost]
        public ActionResult ShiftAdd(ScShift model)
        {
            var user = _authentication.GetAuthenticatedUser();
            try
            {
                model.UpdatedDate = DateTime.UtcNow;
                model.UpdatedById = user.Id;
                model.CreatedDate = DateTime.UtcNow;
                model.CreatedById = user.Id;
                _scshiftRepository.Add(model);
                return Content("True");
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }
        [CheckPermission(Permissions = "Create", Module = "ScSh")]
        public ActionResult ShiftEdit(int shiftId)
        {
            ScShift objScShift = _scshiftRepository.GetById(x => x.Id == shiftId);
            return PartialView("_PartialShiftEdit", objScShift);
        }

        [HttpPost]
        public ActionResult ShiftEdit(ScShift model)
        {
            var user = _authentication.GetAuthenticatedUser();
            ScShift objScShift = _scshiftRepository.GetById(x => x.Id == model.Id);
            objScShift.Code = model.Code;
            objScShift.Description = model.Description;
            objScShift.Memo = model.Memo;
            objScShift.UpdatedDate = DateTime.UtcNow;
            objScShift.UpdatedById = user.Id;
            objScShift.IsActive = model.IsActive;
            objScShift.DispalyOrder = model.DispalyOrder;
            _scshiftRepository.Update(objScShift);
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSh")]
        public ActionResult ShiftsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScSh");
            var lstShifts = _scshiftRepository.GetAll().OrderBy(x => x.DispalyOrder);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialShiftList", lstShifts.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult ShiftDelete(int shiftId)
        {

            var shift = _scStudentRegistrationDetailRepository.GetById(x => x.ShiftId == shiftId);
            //ScShift objScShift = _scshiftRepository.GetById(x => x.Id == shiftId);
            var deleteshift = false;
            if (shift == null)
            {
                _scshiftRepository.Delete(x => x.Id == shiftId);
                deleteshift = true;
            }

            return Json(deleteshift, JsonRequestBehavior.AllowGet);
        }
        #endregion Shifts

        #region Fee Master
        #region Fee Item
        public JsonResult AcademyFeeItemDescriptionExists(string Description, int Id)
        {



            var feeterm = new ScFeeItem();
            if (Id != 0)
            {
                var data = _feeItemRepository.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm = _feeItemRepository.GetById(x => x.Description.ToLower().Trim() == Description.Trim().ToLower());

                }
            }
            else
            {
                feeterm = _feeItemRepository.GetById(x => x.Description.ToLower().Trim() == Description.Trim().ToLower());
            }
            if (feeterm == null)
            {
                feeterm = new ScFeeItem();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }
        public JsonResult AcademyFeeItemCodeExists(string Code, int Id)
        {



            var feeterm = new ScFeeItem();
            if (Id != 0)
            {
                var data = _feeItemRepository.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm = _feeItemRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());

                }
            }
            else
            {
                feeterm = _feeItemRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScFeeItem();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }
        public JsonResult GetFeeItems(string childrenlist)
        {

            List<ScFeeItem> lstClasses =
                _feeItemRepository.GetAll().OrderBy(x => x.Schedule).ToList();

            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });

            if (!string.IsNullOrEmpty(childrenlist))
            {
                var lis = new List<int>();
                var li = childrenlist.Split(',');
                foreach (var s in li)
                {
                    lis.Add(s.ToInt32());
                }
                classes = classes.Where(x => !lis.Contains(x.Id)).OrderBy(x => x.Description).ToList();

            }


            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FeeMasters()
        {
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScFI")]
        public ActionResult FeeItems()
        {

            ViewBag.UserRight = base.UserRight("ScFI");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScFI")]
        public ActionResult FeeItemList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScFI");
            var data = _feeItemRepository.GetAll().OrderBy(x => x.Schedule);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("FeeItemList", data.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }
        [CheckPermission(Permissions = "Create", Module = "ScFI")]
        public ActionResult FeeItemAdd()
        {
            var data = this.BuildAcademicsFeeItemViewModel();
            var lastOrDefault = _feeItemRepository.GetAll().LastOrDefault();
            if (lastOrDefault != null)
                data.FeeItem.Schedule = lastOrDefault.Schedule + 1;
            return PartialView(data);
        }
        public AcademicsFeeItemViewModel BuildAcademicsFeeItemViewModel()
        {
            var viewModel = base.CreateViewModel<AcademicsFeeItemViewModel>();
            viewModel.FeeTypeList = new SelectList(
                    Enum.GetValues(typeof(FeeType)).Cast<FeeType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            viewModel.FeeItem = new ScFeeItem();
            return viewModel;
        }
        public AcademicsFeeItemViewModel BuildAcademicsFeeItemEditViewModel(int id)
        {
            var data = _feeItemRepository.GetById(x => x.Id == id);
            var viewModel = base.CreateViewModel<AcademicsFeeItemViewModel>();
            viewModel.FeeTypeList = new SelectList(
                    Enum.GetValues(typeof(FeeType)).Cast<FeeType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text", data.Type);
            viewModel.FeeItem = data;
            return viewModel;
        }
        [HttpPost]
        public ActionResult FeeItemAdd(AcademicsFeeItemViewModel model)
        {

            _feeItemRepository.Add(model.FeeItem);
            return Content("True");
        }
        [CheckPermission(Permissions = "Edit", Module = "ScFI")]
        public ActionResult FeeItemEdit(int id)
        {
            var data = this.BuildAcademicsFeeItemEditViewModel(id);
            return PartialView(data);
        }
        [HttpPost]
        public ActionResult FeeItemEdit(AcademicsFeeItemViewModel model)
        {
            _feeItemRepository.Update(model.FeeItem);
            return Content("True");
        }
        public ActionResult FeeItemDelete(int id)
        {
            var data = _feeItemRepository.DeleteException(x => x.Id == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region FeeTerms
        [CheckPermission(Permissions = "Navigate", Module = "ScFT")]
        public ActionResult FeeTerms()
        {
            ViewBag.UserRight = base.UserRight("ScFT");
            return View();
        }
        public JsonResult AcademyFeeTermCodeExists(string Code, int Id)
        {
            var feeterm = new ScFeeTerm();
            if (Id != 0)
            {
                var data = _scfeetermRepository.GetById(x => x.Id == Id);
                if (data.Code != Code)
                {
                    feeterm = _scfeetermRepository.GetById(x => x.Code == Code);
                    if (feeterm == null)
                    {

                    }
                }
            }
            else
            {
                feeterm = _scfeetermRepository.GetById(x => x.Code == Code);
            }
            if (feeterm == null)
            {
                feeterm = new ScFeeTerm();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyFeeTermDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScFeeTerm();
            if (Id != 0)
            {
                var data = _scfeetermRepository.GetById(x => x.Id == Id);
                if (data.Description != Description)
                {
                    feeterm = _scfeetermRepository.GetById(x => x.Description == Description);
                    if (feeterm == null)
                    {
                        feeterm = new ScFeeTerm();
                    }
                }
            }
            else
            {
                feeterm = _scfeetermRepository.GetById(x => x.Description == Description);
            }
            if (feeterm == null)
            {
                feeterm = new ScFeeTerm();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Create", Module = "ScFT")]
        public ActionResult FeeTermAdd()
        {
            var model = this.BuildScFeeTermsAdd();
            var lastOrDefault = _scfeetermRepository.GetAll().LastOrDefault();
            if (lastOrDefault != null)
                model.DispalyOrder = lastOrDefault.DispalyOrder + 1;
            model.IsActive = true;

            return PartialView("_PartialFeeTermAdd", model);
        }
        public ScFeeTerm BuildScFeeTermsAdd()
        {
            var model = new ScFeeTerm();
            model.Categories = new SelectList(
                    Enum.GetValues(typeof(FeeTermCategory)).Cast<FeeTermCategory>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            model.RoundedOffTypes = new SelectList(
                    Enum.GetValues(typeof(FeeTermRoundedOffType)).Cast<FeeTermRoundedOffType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            model.Signs = new SelectList(
                    Enum.GetValues(typeof(SignEnum)).Cast<SignEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            model.FeeTermTypes = new SelectList(
                    Enum.GetValues(typeof(FeeTermType)).Cast<FeeTermType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            return model;
        }
        [CheckPermission(Permissions = "Edit", Module = "ScFT")]
        public ScFeeTerm BuildScFeeTermsEdit(int id)
        {
            var model = _scfeetermRepository.GetById(id);
            model.Categories = new SelectList(
                    Enum.GetValues(typeof(FeeTermCategory)).Cast<FeeTermCategory>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text", model.Category);
            model.RoundedOffTypes = new SelectList(
                    Enum.GetValues(typeof(FeeTermRoundedOffType)).Cast<FeeTermRoundedOffType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text", model.Rounded);
            model.Signs = new SelectList(
                    Enum.GetValues(typeof(SignEnum)).Cast<SignEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text", model.Sign);
            model.FeeTermTypes = new SelectList(
                    Enum.GetValues(typeof(FeeTermType)).Cast<FeeTermType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text", model.Type);
            return model;
        }
        [HttpPost]
        public ActionResult FeeTermAdd(ScFeeTerm model)
        {
            var user = _authentication.GetAuthenticatedUser();
            try
            {
                model.CreatedById = user.Id;
                model.UpdatedById = user.Id;
                model.CreatedDate = DateTime.UtcNow;
                model.UpdatedDate = DateTime.UtcNow;
                _scfeetermRepository.Add(model);
                return Content("True");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "ScFT")]
        public ActionResult FeeTermEdit(int feetermId)
        {
            ScFeeTerm objScFeeTerm = BuildScFeeTermsEdit(feetermId);
            return PartialView("_PartialFeeTermEdit", objScFeeTerm);
        }

        [HttpPost]
        public ActionResult FeeTermEdit(ScFeeTerm model)
        {
            var user = _authentication.GetAuthenticatedUser();
            ScFeeTerm objScFeeTerm = _scfeetermRepository.GetById(x => x.Id == model.Id);
            objScFeeTerm.Code = model.Code;
            objScFeeTerm.Description = model.Description;
            objScFeeTerm.GLCode = model.GLCode;
            objScFeeTerm.Category = model.Category;
            objScFeeTerm.Rounded = model.Rounded;
            objScFeeTerm.Sign = model.Sign;
            objScFeeTerm.Type = model.Type;
            objScFeeTerm.Rate = model.Rate;
            objScFeeTerm.Formula = model.Formula;
            objScFeeTerm.SupressZero = model.SupressZero;
            objScFeeTerm.Profitablity = model.Profitablity;
            objScFeeTerm.UpdatedDate = DateTime.UtcNow;
            objScFeeTerm.UpdatedById = user.Id;
            objScFeeTerm.IsActive = model.IsActive;
            objScFeeTerm.DispalyOrder = model.DispalyOrder;
            _scfeetermRepository.Update(objScFeeTerm);
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScFT")]
        public ActionResult FeeTermsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScFT");
            var lstFeeTerms = _scfeetermRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialListFeeTerms", lstFeeTerms.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult FeeTermDelete(int id)
        {
            var data = _scfeetermRepository.DeleteException(x => x.Id == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion FeeTerms
        #endregion

        #region Boaders
        public JsonResult GetBoaders()
        {

            List<ScBoader> lstClasses =
                _scboaderRepository.GetAll().OrderBy(x => x.DispalyOrder).ToList();

            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyBoaderCodeExists(string Code, int Id)
        {
            var feeterm = new ScBoader();
            if (Id != 0)
            {
                var data = _scboaderRepository.GetById(x => x.Id == Id);
                if (data.Code != Code)
                {
                    feeterm = _scboaderRepository.GetById(x => x.Code == Code);

                }
            }
            else
            {
                feeterm = _scboaderRepository.GetById(x => x.Code == Code);
            }
            if (feeterm == null)
            {
                feeterm = new ScBoader();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyBoaderDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScBoader();
            if (Id != 0)
            {
                var data = _scboaderRepository.GetById(x => x.Id == Id);
                if (data.Description != Description)
                {
                    feeterm = _scboaderRepository.GetById(x => x.Description == Description);
                }
            }
            else
            {
                feeterm = _scboaderRepository.GetById(x => x.Description == Description);
            }
            if (feeterm == null)
            {
                feeterm = new ScBoader();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScBO")]
        public ActionResult Boaders()
        {
            ViewBag.UserRight = base.UserRight("ScBO");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScBO")]
        public ActionResult BoaderAdd()
        {
            var model = new ScBoader();
            if (_scboaderRepository.GetAll().Any())
            {
                model.DispalyOrder = _scboaderRepository.GetAll().Max(x => x.DispalyOrder) + 1;
            }
            else
            {
                model.DispalyOrder = 1;
            }
            return PartialView("_PartialBoaderAdd", model);
        }

        [HttpPost]
        public ActionResult BoaderAdd(ScBoader model)
        {
            var user = _authentication.GetAuthenticatedUser();
            try
            {
                model.CreatedDate = DateTime.UtcNow;
                model.CreatedById = user.Id;
                model.UpdatedDate = DateTime.UtcNow;
                model.UpdatedById = user.Id;
                model.IsActive = true;
                _scboaderRepository.Add(model);
                return Content("True");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "ScBO")]
        public ActionResult BoaderEdit(int boaderId)
        {
            ScBoader objScBoader = _scboaderRepository.GetById(x => x.Id == boaderId);
            return PartialView("_PartialBoaderEdit", objScBoader);
        }

        [HttpPost]
        public ActionResult BoaderEdit(ScBoader model)
        {
            var user = _authentication.GetAuthenticatedUser();
            ScBoader objScBoader = _scboaderRepository.GetById(x => x.Id == model.Id);
            objScBoader.Code = model.Code;
            objScBoader.Description = model.Description;
            objScBoader.FeeItemId = model.FeeItemId;
            objScBoader.Memo = model.Memo;

            objScBoader.UpdatedDate = DateTime.UtcNow;
            objScBoader.UpdatedById = user.Id;
            objScBoader.IsActive = model.IsActive;
            objScBoader.DispalyOrder = model.DispalyOrder;
            _scboaderRepository.Update(objScBoader);
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScBO")]
        public ActionResult BoadersList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScBO");
            var lstBoaders = _scboaderRepository.GetAll().OrderBy(x => x.DispalyOrder);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialBoaderList", lstBoaders.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult BoaderDelete(int id)
        {
            ScBoader objScBoader = _scboaderRepository.GetById(x => x.Id == id);
            var data = _scboaderRepository.DeleteException(objScBoader);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion Boaders

        #region BoaderMappings
        [CheckPermission(Permissions = "Navigate", Module = "ScBOM")]
        public ActionResult BoaderMappings()
        {
            ViewBag.UserRight = base.UserRight("ScBOM");
            var datetype = base.SystemControl.DateType;
            return View(datetype);
        }
        [CheckPermission(Permissions = "Create", Module = "ScBOM")]
        public ActionResult BoaderMappingAdd()
        {
            var model = new ScBoaderMapping();
            model.DateType = base.SystemControl.DateType;
            return PartialView("_PartialBoaderMappingAdd", model);
        }

        public ActionResult BoaderDetail(int SectionId, int ClassId, int BoaderId, int Id)
        {
            if (Id != 0)
            {
                var i = _scBoaderMappingRepository.GetById(x => x.Id == Id && x.AcademicYearId == CurrentAcademyYearId);
                if (i.SectionId == SectionId && i.SectionId == SectionId && i.BoaderId == BoaderId)
                {
                    return Content("ExistData");
                }
            }



            var housemapping =
                _scBoaderMappingRepository.GetById(
                    x => x.ClassId == ClassId && x.SectionId == SectionId && x.BoaderId == BoaderId && x.AcademicYearId == CurrentAcademyYearId);
            if (housemapping == null)
            {
                Expression<Func<ScStudentRegistrationDetail, bool>> predicate = x => true;
                predicate = predicate.And(x => x.StudentRegistration.ClassId == ClassId);
                if (SectionId != 0)
                {
                    predicate = predicate.And(x => x.SectionId == SectionId);
                }
                predicate = predicate.And(x => x.StudentRegistration.AcademicYearId == CurrentAcademyYearId);
                var data = _scStudentRegistrationDetailRepository.GetExpandable(predicate);
                var student = new List<ScStudentinfo>();
                if (data.Any())
                {
                    student = data.Select(x => x.Studentinfo).ToList();
                }

                var list = student.Select(item => new ScBoaderMapping()
                                                      {
                                                          StudentId = item.StudentID,
                                                          Studentinfo = item,
                                                          StartDate = DateTime.UtcNow.Date,
                                                          EndDate = DateTime.UtcNow.AddMonths(5).Date,
                                                          DateType = base.SystemControl.DateType,
                                                          ClassId = ClassId,
                                                          SectionId = SectionId,
                                                          StartMiti = NepaliDateService.NepaliDate(DateTime.Now.Date).Date,
                                                          EndMiti = NepaliDateService.NepaliDate(DateTime.Now.AddMonths(5).Date).Date
                                                      }).ToList();

                return PartialView("_PartialBoaderMappingDetailEntry", list);

            }
            else
            {
                return Content("false");
            }
        }

        [CheckPermission(Permissions = "Create", Module = "ScBOM")]
        public ActionResult BoaderDetailAdd(int sectionId, int classId)
        {
            var data = new ScBoaderMapping();
            data.StartDate = DateTime.UtcNow.Date;
            data.EndDate = DateTime.UtcNow.AddMonths(5).Date;
            data.DateType = base.SystemControl.DateType;
            data.ClassId = classId;
            data.SectionId = sectionId;
            data.StartMiti = NepaliDateService.NepaliDate(data.StartDate).Date;
            data.EndMiti = NepaliDateService.NepaliDate(data.EndDate).Date;
            return PartialView("_PartialBoaderMappingDetailEdit", data);
        }

        [HttpPost]
        public ActionResult BoaderMappingAdd(ScBoaderMapping model, IEnumerable<ScBoaderMapping> subjectEntry)
        {

            var user = _authentication.GetAuthenticatedUser();
            foreach (var item in subjectEntry.Where(x => x.StudentId != 0))
            {
                model.CreatedById = user.Id;
                model.CreatedDate = DateTime.UtcNow;
                model.UpdatedById = user.Id;
                model.UpdatedDate = DateTime.UtcNow;
                model.StudentId = item.StudentId;
                model.Status = item.Status;
                model.StartDate = item.StartDate;
                model.StartMiti = item.StartMiti;
                model.EndDate = item.EndDate;
                model.EndMiti = item.EndMiti;
                model.Narration = item.Narration;

                model.AcademicYearId = CurrentAcademyYearId;
                _scBoaderMappingRepository.Add(model);
            }

            return Content("True");

        }

        [CheckPermission(Permissions = "Edit", Module = "ScBOM")]
        public ActionResult BoaderMappingEdit(int classId, int sectionId, int boaderId)
        {
            var datalist = new List<ScBoaderMapping>();
            var model = new ScBoaderMapping();
            model = _scBoaderMappingRepository.GetById(x => x.ClassId == classId && x.SectionId == sectionId && x.BoaderId == boaderId && x.AcademicYearId == CurrentAcademyYearId);
            var list = _scBoaderMappingRepository.GetMany(x => x.ClassId == classId && x.SectionId == sectionId && x.BoaderId == boaderId && x.AcademicYearId == CurrentAcademyYearId);
            foreach (var item in list)
            {
                var data = new ScBoaderMapping();
                data = item;
                data.DateType = base.SystemControl.DateType;
                datalist.Add(data);
            }
            model.DateType = base.SystemControl.DateType;
            model.ScBoaderMappings = datalist;
            model.OLDCLassId = classId;
            model.OLDSectionId = sectionId;
            model.OLDBoaderId = boaderId;
            return PartialView("_PartialBoaderMappingEdit", model);
        }
        [HttpPost]
        public ActionResult BoaderMappingEdit(ScBoaderMapping model, IEnumerable<ScBoaderMapping> subjectEntry)
        {
            _scBoaderMappingRepository.Delete(x => x.ClassId == model.OLDCLassId && x.SectionId == model.OLDSectionId && x.BoaderId == model.OLDBoaderId && x.AcademicYearId == CurrentAcademyYearId);
            var user = _authentication.GetAuthenticatedUser();
            foreach (var item in subjectEntry.Where(x => x.StudentId != 0))
            {
                model.CreatedById = user.Id;
                model.CreatedDate = DateTime.UtcNow;
                model.UpdatedById = user.Id;
                model.UpdatedDate = DateTime.UtcNow;
                model.StudentId = item.StudentId;
                model.Status = item.Status;
                model.StartDate = item.StartDate;
                model.StartMiti = item.StartMiti;
                model.EndDate = item.EndDate;
                model.EndMiti = item.EndMiti;
                model.Narration = item.Narration;
                _scBoaderMappingRepository.Add(model);
            }

            return Content("True");

        }

        [CheckPermission(Permissions = "Navigate", Module = "ScBOM")]
        public ActionResult BoaderMappingsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScBOM");
            var lstHouseMappings = from d in _scBoaderMappingRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId)
                                   group d by new { d.ClassId, d.SectionId, d.BoaderId }
                                       into g
                                       select g.ToList();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialBoaderMappingList", lstHouseMappings.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult BoaderMappingDelete(int classId, int sectionId, int boaderId)
        {
            _scBoaderMappingRepository.Delete(x => x.ClassId == classId && x.SectionId == sectionId && x.BoaderId == boaderId && x.AcademicYearId == CurrentAcademyYearId);
            return RedirectToAction("BoaderMappings");
        }
        #endregion

        #region ClassFeeRates

        public ActionResult ClassWiseFeeRateDetailByClassId(int classId)
        {
            var data = _scclassfeerateRepository.GetMany(x => x.ClassId == classId).ToList();
            return PartialView("_PartialClassWiseFeeRateDetail", data);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScCFR")]
        public ActionResult ClassFeeRates()
        {
            ViewBag.UserRight = base.UserRight("ScCFR");
            return View();
        }
        public AdcademicsClassFeeRateViewModel BuildAdcademicsClassFeeRateAddViewModel()
        {

            var viewModel = base.CreateViewModel<AdcademicsClassFeeRateViewModel>();
            viewModel.ClassFeeRate = new ScClassFeeRate();


            return viewModel;
        }
        public AdcademicsClassFeeRateViewModel BuildAdcademicsClassFeeRateEditViewModel(int classId, int shiftId, int boaderId)
        {
            decimal total = 0;
            var data = _scclassfeerateRepository.GetMany(x => x.ClassId == classId && x.ShiftId == shiftId && x.BoaderId == boaderId && x.AcademyYearId == CurrentAcademyYearId);
            foreach (var item in data)
            {
                total += item.FeeRate;
            }
            var viewModel = base.CreateViewModel<AdcademicsClassFeeRateViewModel>();
            viewModel.ClassFeeRate = data.FirstOrDefault();// _scclassfeerateRepository.GetById(x => x.ClassId == classId && x.ShiftId == shiftId && x.BoarderId == boaderId);
            viewModel.ClassFeeRates = data;
            viewModel.BoaderId = boaderId;
            viewModel.ClassId = classId;
            viewModel.ShiftId = classId;
            viewModel.TotalAmount = total;
            return viewModel;
        }
        public ActionResult GetClasFeeItem(int classId, int shiftId, int boaderId)
        {
            var classfee =
                _scclassfeerateRepository.GetById(
                    x => x.ClassId == classId && x.ShiftId == shiftId && x.BoaderId == boaderId && x.AcademyYearId == CurrentAcademyYearId);
            if (classfee == null)
            {
                var feeitem = _feeItemRepository.GetAll().OrderBy(x => x.Schedule);
                var list = new List<ScClassFeeRate>();

                foreach (var item in feeitem)
                {
                    var data = new ScClassFeeRate()
                                   {
                                       FeeItemId = item.Id,
                                       ScFeeItem = item
                                   };
                    list.Add(data);
                }
                if (list.Count() == 0)
                {
                    return Content("0");
                }


                return PartialView("_PartialClassFeeDetailEntry", list);
            }
            else
            {
                return Content("false");
            }

        }
        [CheckPermission(Permissions = "Create", Module = "ScCFR")]
        public ActionResult ClassFeeRateAdd()
        {
            var model = this.BuildAdcademicsClassFeeRateAddViewModel();
            return PartialView("_PartialClassFeeRateAdd", model);
        }

        [HttpPost]
        public ActionResult ClassFeeRateAdd(AdcademicsClassFeeRateViewModel model, IEnumerable<ScClassFeeRate> subjectEntry)
        {
            var user = _authentication.GetAuthenticatedUser();

            foreach (var item in subjectEntry.Where(x => x.FeeRate != 0))
            {


                item.CreatedDate = DateTime.UtcNow;
                item.CreatedById = user.Id;
                item.UpdatedDate = DateTime.UtcNow;
                item.UpdatedById = user.Id;
                item.ClassId = model.ClassFeeRate.ClassId;
                item.BoaderId = model.ClassFeeRate.BoaderId;
                item.ShiftId = model.ClassFeeRate.ShiftId;
                item.Remarks = model.ClassFeeRate.Remarks;
                item.AcademyYearId = CurrentAcademyYearId;

                _scclassfeerateRepository.Add(item);
            }

            return Content("True");

        }
        [CheckPermission(Permissions = "Edit", Module = "ScCFR")]
        public ActionResult ClassFeeRateEdit(int classId, int boaderId, int shiftId)
        {
            var model = this.BuildAdcademicsClassFeeRateEditViewModel(classId, shiftId, boaderId);
            return PartialView("_PartialClassFeeRateEdit", model);
        }
        [HttpPost]
        public ActionResult ClassFeeRateEdit(AdcademicsClassFeeRateViewModel model, IEnumerable<ScClassFeeRate> subjectEntry)
        {
            var user = _authentication.GetAuthenticatedUser();

            _scclassfeerateRepository.Delete(
                x => x.ClassId == model.ClassId && x.BoaderId == model.BoaderId && x.ShiftId == model.ShiftId && x.AcademyYearId == CurrentAcademyYearId);

            foreach (var item in subjectEntry)
            {

                item.CreatedDate = DateTime.UtcNow;
                item.CreatedById = user.Id;
                item.UpdatedDate = DateTime.UtcNow;
                item.UpdatedById = user.Id;
                item.ClassId = model.ClassFeeRate.ClassId;
                item.BoaderId = model.ClassFeeRate.BoaderId;
                item.ShiftId = model.ClassFeeRate.ShiftId;
                item.Remarks = model.ClassFeeRate.Remarks;
                item.AcademyYearId = CurrentAcademyYearId;

                _scclassfeerateRepository.Add(item);
            }

            return Content("True");

        }

        [CheckPermission(Permissions = "Navigate", Module = "ScCFR")]
        public ActionResult ClassFeeRatesList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScCFR");

            var lstClassFeeRates = from d in _scclassfeerateRepository.GetMany(x => x.AcademyYearId == CurrentAcademyYearId)
                                   group d by new { d.ClassId, d.BoaderId, d.ShiftId }
                                       into g
                                       select g.ToList();
            //var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            //ViewBag.SnStart = snStart;
            return PartialView("_PartialClassFeeRateList", lstClassFeeRates.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult ClassFeeRateDelete(int classId, int boaderId, int shiftId)
        {

            _scclassfeerateRepository.Delete(
                x => x.ClassId == classId && x.BoaderId == boaderId && x.ShiftId == shiftId && x.AcademyYearId == CurrentAcademyYearId);
            return RedirectToAction("ClassFeeRates");
        }
        public ActionResult ClassFeeRateDetail()
        {
            var data = new ScClassFeeRate();
            return PartialView("_PartialClassFeeRateDetailEdit", data);
        }
        #endregion

        #region StudentWiseFeeRate
        public ActionResult InsertFeeTermEdit(ScStudentFeeTerm model)
        {
            var billingTerm = _scfeetermRepository.GetById(x => x.Id == model.FeeTermId);
            var viewModel = new FeeTermDetailViewModel();
            viewModel.Amount = Math.Abs(model.LocalAmount);
            //viewModel.Basis = Enum.GetName(typeof(FeeTermCategory), billingTerm.Category).ToString();
            viewModel.Description = billingTerm.Description;
            viewModel.Id = model.FeeTermId;
            viewModel.Rate = model.LocalRate;
            viewModel.IsProductWise = model.IsItemWise;
            StringEnum stringEnum = new StringEnum(typeof(SignEnum));
            viewModel.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), billingTerm.Sign));
            viewModel.FeeTermIndex = model.Index;
            return PartialView("_PartialHdnFeeTerm", viewModel);
        }
        public ActionResult InsertFeeTerm(string desc, string basis, int id, decimal amount, decimal rate, string sign, int? index, bool isProductWise)
        {
            var viewModel = new FeeTermDetailViewModel();
            viewModel.Amount = amount;
            viewModel.Basis = basis;
            viewModel.Description = desc;
            viewModel.Id = id;
            viewModel.Rate = rate;
            viewModel.Sign = sign;
            viewModel.FeeTermIndex = index;
            viewModel.IsProductWise = isProductWise;
            return PartialView("_PartialHdnFeeTerm", viewModel);
        }
        public ActionResult GetFeeTermItemWise(bool itemwise)
        {

            var data = new List<ScFeeTerm>();
            var type = 1;
            if (itemwise)
            {
                type = Convert.ToInt32(FeeTermType.FeeItemWise);
            }
            data = _scfeetermRepository.GetMany(x => x.Type == type && x.IsActive).ToList();

            List<FeeTermSelectViemModel> models = new List<FeeTermSelectViemModel>();
            foreach (var item in data)
            {

                var billingTerm = new FeeTermSelectViemModel();
                billingTerm.Id = item.Id;
                billingTerm.Basis = Enum.GetName(typeof(BillingTermBasisEnum), 2).ToString();
                billingTerm.Description = item.Description;
                StringEnum stringEnum = new StringEnum(typeof(SignEnum));
                billingTerm.Sign = stringEnum.GetStringValue(Enum.GetName(typeof(SignEnum), item.Sign));
                billingTerm.Rate = item.Rate;
                billingTerm.Amount = 0;
                models.Add(billingTerm);
            }
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentWiseFeeRateByStudentId(int studentId)
        {
            var data = _scStudentFeeRateDetailRepository.GetMany(x => x.FeeRateMaster.StudentId == studentId).ToList();
            return PartialView("_PartialStudentWiseFeeRateDetail", data);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSwF")]
        public ActionResult StudentWiseFeeRateSetups()
        {
            ViewBag.UserRight = base.UserRight("ScSwF");

            var data = _scfeetermRepository.GetMany(x => x.IsActive).Select(x => new BillingTermSelectViewModel()
                                                                     {
                                                                         Description = x.Description,
                                                                         DisplayOrder = x.DispalyOrder,
                                                                         Amount = 0,
                                                                         Rate = x.Rate,
                                                                         Sign = x.Sign == 1 ? "+" : "-",
                                                                         Id = x.Id

                                                                     });
            ViewBag.BillTerms = data;
            return View();
        }

        public ActionResult GetStudentsIdByClassId(int id)
        {
            var list = _scStudentFeeRateMasterRepository.GetMany(x => x.ClassId == id && x.AcademicYearId == CurrentAcademyYearId).Select(x => x.StudentId);
            List<ScStudentRegistrationDetail> lstStudent = _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.ClassId == id && !list.Contains(x.StudentId) && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).ToList();

            var data = lstStudent.Select(x => new
            {
                Id = x.StudentId,
                Code = x.Studentinfo.StdCode,
                Description = x.Studentinfo.StuDesc
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSwF")]
        public ActionResult StudentFeeRateList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScSwF");
            // var data = _scStudentFeeRateMasterRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId).AsQueryable().ToPagedList(pageNo, SystemControl.PageSize);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            var data = _scStudentFeeRateMasterRepository.GetPagedList(x => x.AcademicYearId == CurrentAcademyYearId, j => j.Id, pageNo, SystemControl.PageSize);
            return PartialView("_PartialStudentFeeList", data);
        }
        [CheckPermission(Permissions = "Create", Module = "ScSwF")]
        public ActionResult StudentFeeRateAdd()
        {

            var feeitemwise = Convert.ToInt32(FeeTermType.FeeItemWise);
            var billlwise = 1;//Convert.ToInt32(FeeTermType.BillWise);
            var feeitem = _scfeetermRepository.Filter(x => x.Type == feeitemwise && x.IsActive).ToList();
            var billitem = _scfeetermRepository.Filter(x => x.Type == billlwise && x.IsActive).ToList();
            var data = new ScStudentFeeRateMaster();

            if (feeitem.Count() != 0)
                ViewBag.AllowFeeWiseFeeTerm = 1;
            if (billitem.Count() != 0)
                ViewBag.AllowbillWiseFeeTerm = 1;
            return PartialView("_PartialStudentFeeRateAdd", data);
        }

        [HttpPost]
        public ActionResult StudentFeeRateAdd(IEnumerable<ScStudentFeeRateDetail> subjectEntry, ScStudentFeeRateMaster model, IEnumerable<FeeTermDetailViewModel> billTermList)
        {
            var user = _authentication.GetAuthenticatedUser();
            model.UpdatedById = user.Id;
            model.CreatedById = user.Id;
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
            model.AcademicYearId = CurrentAcademyYearId;

            _scStudentFeeRateMasterRepository.Add(model);
            var tempSalesDetailList = new List<ScStudentFeeRateDetail>();
            foreach (var item in subjectEntry.Where(x => x.FeeRate != 0))
            {
                item.MasterId = model.Id;
                _scStudentFeeRateDetailRepository.Add(item);
                tempSalesDetailList.Add(item);
                if (billTermList != null)
                {
                    foreach (var term in billTermList.Where(x => x.ParentGuid == item.DetailGuid))
                    {
                        var billingTerm = _scfeetermRepository.GetById(x => x.Id == term.Id);
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


                        var billingTermDetail = new ScStudentFeeTerm();
                        billingTermDetail.FeeTermId = Convert.ToInt32(term.Id);
                        billingTermDetail.Index = term.FeeTermIndex;
                        billingTermDetail.StudentFeeId = model.Id;
                        billingTermDetail.DetailId = item.Id;
                        // billingTermDetail.Source = source;
                        billingTermDetail.LocalRate = Convert.ToDecimal(term.Rate);
                        if (term.FeeTermIndex != null)
                        {
                            var index = Convert.ToInt32(term.FeeTermIndex);
                            if (term.IsProductWise)
                                billingTermDetail.DetailId = item.Id;
                        }
                        billingTermDetail.IsItemWise = term.IsProductWise;
                        billingTermDetail.LocalAmount = termAmt;
                        _scStudentFeeTermRepository.Add(billingTermDetail);


                    }
                }
            }


            return Content("True");
        }
        [CheckPermission(Permissions = "Edit", Module = "ScSwF")]
        public ActionResult StudentFeeRateEdit(int id)
        {
            var feeitemwise = Convert.ToInt32(FeeTermType.FeeItemWise);
            var billlwise = 1; //Convert.ToInt32(FeeTermType.BillWise);
            var feeitem = _scfeetermRepository.Filter(x => x.Type == feeitemwise && x.IsActive).ToList();
            var billitem = _scfeetermRepository.Filter(x => x.Type == billlwise && x.IsActive).ToList();
            if (feeitem.Count() != 0)
                ViewBag.AllowFeeWiseFeeTerm = 1;
            if (billitem.Count() != 0)
                ViewBag.AllowbillWiseFeeTerm = 1;

            var data = _scStudentFeeRateMasterRepository.GetById(x => x.Id == id && x.AcademicYearId == CurrentAcademyYearId);

            var viewModel = new AdcamicsStudentFeeRateViewModel();

            viewModel.StudentFeeRateMaster = data;
            viewModel.ScStudentFeeRateDetails = _scStudentFeeRateDetailRepository.GetMany(x => x.MasterId == data.Id);


            var billTerm = _scStudentFeeTermRepository.Filter(x => x.StudentFeeId == id).ToList();
            //var defaultIndex = billTerm.Max(x => x.Index);
            var defaultIndex = data.ScStudentFeeRateDetails.Max(x => x.Index);
            ViewBag.Index = defaultIndex == null ? 1 : defaultIndex + 1;
            if (!billTerm.Any())
            {
                billTerm = new List<ScStudentFeeTerm>();
            }

            ViewBag.BillingTermList = billTerm;
            return PartialView("_PartialStudentFeeRateEdit", viewModel);
        }

        [HttpPost]
        public ActionResult StudentFeeRateEdit(IEnumerable<ScStudentFeeRateDetail> subjectEntry, AdcamicsStudentFeeRateViewModel model, IEnumerable<FeeTermDetailViewModel> billTermList)
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _scStudentFeeRateMasterRepository.GetById(x => x.Id == model.StudentFeeRateMaster.Id && x.AcademicYearId == CurrentAcademyYearId);

            data.UpdatedById = user.Id;
            data.UpdatedDate = DateTime.UtcNow;
            data.ClassId = model.StudentFeeRateMaster.ClassId;
            data.StudentId = model.StudentFeeRateMaster.StudentId;
            data.BasicAmount = model.StudentFeeRateMaster.BasicAmount;
            data.NetAmount = model.StudentFeeRateMaster.NetAmount;
            data.TermAmount = model.StudentFeeRateMaster.TermAmount;

            _scStudentFeeRateMasterRepository.Update(data);
            _scStudentFeeRateDetailRepository.Delete(x => x.MasterId == data.Id);
            _scStudentFeeTermRepository.Delete(x => x.StudentFeeId == data.Id);
            var tempSalesDetailList = new List<ScStudentFeeRateDetail>();
            foreach (var item in subjectEntry.Where(x => x.FeeRate != 0))
            {
                item.MasterId = data.Id;
                _scStudentFeeRateDetailRepository.Add(item);
                tempSalesDetailList.Add(item);
                if (billTermList != null)
                {
                    foreach (var term in billTermList.Where(x => x.ParentGuid == item.DetailGuid))
                    {
                        var billingTerm = _scfeetermRepository.GetById(x => x.Id == term.Id);
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


                        var billingTermDetail = new ScStudentFeeTerm();
                        billingTermDetail.FeeTermId = Convert.ToInt32(term.Id);
                        billingTermDetail.Index = term.FeeTermIndex;
                        billingTermDetail.StudentFeeId = data.Id;
                        billingTermDetail.DetailId = item.Id;
                        // billingTermDetail.Source = source;
                        billingTermDetail.LocalRate = Convert.ToDecimal(term.Rate);
                        if (term.FeeTermIndex != null)
                        {
                            var index = Convert.ToInt32(term.FeeTermIndex);
                            if (term.IsProductWise)
                                billingTermDetail.DetailId = item.Id;
                        }
                        billingTermDetail.IsItemWise = term.IsProductWise;
                        billingTermDetail.LocalAmount = termAmt;
                        _scStudentFeeTermRepository.Add(billingTermDetail);
                    }


                }
            }
            return Content("True");
        }

        public ActionResult StudentFeeRateDelete(int id)
        {
            _scStudentFeeRateMasterRepository.Delete(x => x.Id == id);
            return RedirectToAction("StudentWiseFeeRateSetups");
        }
        [CheckPermission(Permissions = "Create", Module = "ScSwF")]
        public ActionResult StudentFeeRateDetailAdd(int AllowFeeWiseFeeTerm)
        {
            var data = new ScStudentFeeRateDetail();
            data.AllowFeeWiseFeeTerm = AllowFeeWiseFeeTerm;
            return PartialView("_PartialStudentFeeRateDetailAdd", data);
        }
        public ActionResult StudentFeeRateDetailAddForm(int classId)
        {
            var feeterm = _scclassfeerateRepository.GetMany(x => x.ClassId == classId);
            var list = new List<ScStudentFeeRateDetail>();
            foreach (var fe in feeterm)
            {
                var model = new ScStudentFeeRateDetail()
                                {
                                    FeeItemId = fe.FeeItemId,
                                    FeeRate = fe.FeeRate,
                                    FeeTermName = UtilityService.GetFeeItemName(fe.FeeItemId),
                                    DetailGuid = Guid.NewGuid().ToString(),
                                    AllowFeeWiseFeeTerm = 1

                                };
                list.Add(model);
            }
            return PartialView("StudentFeeRateDetailAddForm", list);
        }
        #endregion

        #region House Setup

        #region HouseGroups
        public JsonResult GetHouseGroups()
        {

            List<ScHouseGroup> lstClasses =
                _schousegroupRepository.GetAll().OrderBy(x => x.Description).ToList();

            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScHG")]
        public ActionResult HouseGroups()
        {
            ViewBag.UserRight = base.UserRight("ScHG");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScHG")]
        public ActionResult HouseGroupAdd()
        {
            var model = new ScHouseGroup();
            return PartialView("_PartialHouseGroupAdd", model);
        }

        [HttpPost]
        public ActionResult HouseGroupAdd(ScHouseGroup model)
        {
            var user = _authentication.GetAuthenticatedUser();
            if (ModelState.IsValid)
            {

                model.CreatedDate = DateTime.UtcNow;
                model.CreatedById = user.Id;
                model.UpdatedDate = DateTime.UtcNow;
                model.UpdatedById = user.Id;
                _schousegroupRepository.Add(model);
                return Content("True");
            }
            else
            {
                return Content("False");
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "ScHG")]
        public ActionResult HouseGroupEdit(int housegroupId)
        {
            ScHouseGroup objScHouseGroup = _schousegroupRepository.GetById(x => x.Id == housegroupId);
            return PartialView("_PartialHouseGroupEdit", objScHouseGroup);
        }

        [HttpPost]
        public ActionResult HouseGroupEdit(ScHouseGroup model)
        {

            var user = _authentication.GetAuthenticatedUser();
            ScHouseGroup objScHouseGroup = _schousegroupRepository.GetById(x => x.Id == model.Id);
            model.CreatedById = objScHouseGroup.CreatedById;
            model.CreatedDate = objScHouseGroup.CreatedDate;
            model.UpdatedDate = DateTime.UtcNow;
            model.UpdatedById = user.Id;
            var _dataContext = new DataContext();
            _dataContext.Entry(model).State = EntityState.Modified;
            _dataContext.SaveChanges();

            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScHG")]
        public ActionResult HouseGroupsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScHG");

            var lstHouseGroups = _schousegroupRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialHouseGroupList", lstHouseGroups.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult HouseGroupDelete(int id)
        {
            var data = _schousegroupRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyHouseGroupCodeExists(string Code, int Id)
        {
            var feeterm = new ScHouseGroup();
            if (Id != 0)
            {
                var data = _schousegroupRepository.GetById(x => x.Id == Id);
                if (data.Code != Code)
                {
                    feeterm = _schousegroupRepository.GetById(x => x.Code == Code);

                }
            }
            else
            {
                feeterm = _schousegroupRepository.GetById(x => x.Code == Code);
            }
            if (feeterm == null)
            {
                feeterm = new ScHouseGroup();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AcademyHouseGroupDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScHouseGroup();
            if (Id != 0)
            {
                var data = _schousegroupRepository.GetById(x => x.Id == Id);
                if (data.Description != Description)
                {
                    feeterm = _schousegroupRepository.GetById(x => x.Description == Description.Trim());

                }
            }
            else
            {
                feeterm = _schousegroupRepository.GetById(x => x.Description == Description);
            }
            if (feeterm == null)
            {
                feeterm = new ScHouseGroup();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentStatus()
        {

            var lstClasses =
                new SelectList(
                    Enum.GetValues(typeof(StudentStatus)).Cast<StudentStatus>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");

            var classes = lstClasses.Select(x => new
            {
                Id = x.Value,

                Description = x.Text
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region HouseMappings
        [CheckPermission(Permissions = "Navigate", Module = "ScHM")]
        public ActionResult HouseMappings()
        {
            ViewBag.UserRight = base.UserRight("ScHM");

            var datetype = base.SystemControl.DateType;
            return View(datetype);
        }
        [CheckPermission(Permissions = "Create", Module = "ScHM")]
        public ActionResult HouseMappingAdd()
        {
            var model = new ScHouseMapping();
            model.DateType = base.SystemControl.DateType;
            return PartialView("_PartialHouseMappingAdd", model);
        }

        public ActionResult HouseDetail(int SectionId, int ClassId, int HouseGroupId, int Id)
        {
            if (Id != 0)
            {
                var i = _schousemappingRepository.GetById(x => x.Id == Id && x.AcademicYearId == CurrentAcademyYearId);
                if (i.SectionId == SectionId && i.SectionId == SectionId && i.HouseGroupId == HouseGroupId)
                {
                    return Content("ExistData");
                }
            }



            var housemapping =
                _schousemappingRepository.GetById(
                    x => x.ClassId == ClassId && x.SectionId == SectionId && x.HouseGroupId == HouseGroupId && x.AcademicYearId == CurrentAcademyYearId);
            if (housemapping == null)
            {

                var data = _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.ClassId == ClassId && x.SectionId == SectionId);

                var list = data.Select(item => new ScHouseMapping()
                                                   {
                                                       StudentId = item.StudentId,
                                                       Studentinfo = item.Studentinfo,
                                                       StartDate = DateTime.UtcNow.Date,
                                                       EndDate = DateTime.UtcNow.AddMonths(5).Date,
                                                       DateType = base.SystemControl.DateType,
                                                       ClassId = ClassId,
                                                       SectionId = SectionId
                                                   }).ToList();

                return PartialView("_PartialHouseMappingDetailEntry", list);

            }
            else
            {
                return Content("false");
            }
        }
        [CheckPermission(Permissions = "Create", Module = "ScHM")]
        public ActionResult HouseDetailAdd(int sectionId, int classId)
        {
            var data = new ScHouseMapping();
            data.StartDate = DateTime.UtcNow.Date;
            data.EndDate = DateTime.UtcNow.AddMonths(8).Date;
            data.DateType = base.SystemControl.DateType;
            data.ClassId = classId;
            data.SectionId = sectionId;
            return PartialView("_PartialHouseMappingDetailAdd", data);
        }

        [HttpPost]
        public ActionResult HouseMappingAdd(ScHouseMapping model, IEnumerable<ScHouseMapping> subjectEntry)
        {

            var user = _authentication.GetAuthenticatedUser();
            foreach (var item in subjectEntry.Where(x => x.StudentId != 0))
            {
                model.CreatedById = user.Id;
                model.CreatedDate = DateTime.UtcNow;
                model.UpdatedById = user.Id;
                model.UpdatedDate = DateTime.UtcNow;
                model.StudentId = item.StudentId;
                model.Status = item.Status;
                model.StartDate = item.StartDate;
                model.StartMiti = item.StartMiti;
                model.EndDate = item.EndDate;
                model.EndMiti = item.EndMiti;
                model.Narration = item.Narration;
                model.AcademicYearId = CurrentAcademyYearId;
                _schousemappingRepository.Add(model);
            }

            return Content("True");

        }

        [CheckPermission(Permissions = "Edit", Module = "ScHM")]
        public ActionResult HouseMappingEdit(int classId, int sectionId, int housegroupId)
        {
            var datalist = new List<ScHouseMapping>();
            var model = new ScHouseMapping();
            model = _schousemappingRepository.GetById(x => x.ClassId == classId && x.SectionId == sectionId && x.HouseGroupId == housegroupId && x.AcademicYearId == CurrentAcademyYearId);
            var list = _schousemappingRepository.GetMany(x => x.ClassId == classId && x.SectionId == sectionId && x.HouseGroupId == housegroupId && x.AcademicYearId == CurrentAcademyYearId);
            foreach (var item in list)
            {
                var data = new ScHouseMapping();
                data = item;
                data.DateType = base.SystemControl.DateType;
                datalist.Add(data);
            }
            model.DateType = base.SystemControl.DateType;
            model.HouseMappings = datalist;
            model.OLDCLassId = classId;
            model.OLDSectionId = sectionId;
            model.OLDHouseGroupId = housegroupId;
            return PartialView("_PartialHouseMappingEdit", model);
        }
        [HttpPost]
        public ActionResult HouseMappingEdit(ScHouseMapping model, IEnumerable<ScHouseMapping> subjectEntry)
        {
            _schousemappingRepository.Delete(x => x.ClassId == model.OLDCLassId && x.SectionId == model.OLDSectionId && x.HouseGroupId == model.OLDHouseGroupId && x.AcademicYearId == CurrentAcademyYearId);
            var user = _authentication.GetAuthenticatedUser();
            foreach (var item in subjectEntry.Where(x => x.StudentId != 0))
            {
                model.CreatedById = user.Id;
                model.CreatedDate = DateTime.UtcNow;
                model.UpdatedById = user.Id;
                model.UpdatedDate = DateTime.UtcNow;
                model.StudentId = item.StudentId;
                model.Status = item.Status;
                model.StartDate = item.StartDate;
                model.StartMiti = item.StartMiti;
                model.EndDate = item.EndDate;
                model.EndMiti = item.EndMiti;
                model.Narration = item.Narration;
                _schousemappingRepository.Add(model);
            }

            return Content("True");

        }

        [CheckPermission(Permissions = "Navigate", Module = "ScHM")]
        public ActionResult HouseMappingsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScHM");
            var lstHouseMappings = from d in _schousemappingRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId)
                                   group d by new { d.ClassId, d.SectionId, d.HouseGroupId }
                                       into g
                                       select g.ToList();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialHouseMappingList", lstHouseMappings.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult HouseMappingDelete(int classId, int sectionId, int housegroupId)
        {
            _schousemappingRepository.Delete(x => x.ClassId == classId && x.SectionId == sectionId && x.HouseGroupId == housegroupId);
            return RedirectToAction("HouseMappings");
        }
        #endregion HouseMappings



        #endregion

        #region fine Schemes
        [CheckPermission(Permissions = "Navigate", Module = "ScFS")]
        public ActionResult FineSchemes()
        {
            return View();
        }
        public JsonResult AcademyFineSchemesExists(string Description, int Id)
        {
            var feeterm = new ScFineScheme();
            if (Id != 0)
            {
                var data = _scFineSchemeRepository.GetById(x => x.Id == Id);
                if (data.Description != Description)
                {
                    feeterm = _scFineSchemeRepository.GetById(x => x.Description == Description);

                }
            }
            else
            {
                feeterm = _scFineSchemeRepository.GetById(x => x.Description == Description);
            }
            if (feeterm == null)
            {
                feeterm = new ScFineScheme();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScFS")]
        public ActionResult FineSchemeList(int pageNo = 1)
        {
            var data = _scFineSchemeRepository.GetAll().AsQueryable().ToPagedList(pageNo, SystemControl.PageSize);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialFineSchemeList", data);
        }
        [CheckPermission(Permissions = "Create", Module = "ScFS")]
        public ActionResult FineSchemeAdd()
        {
            var data = new ScFineScheme();
            return PartialView("_PartialFineSchemeAdd", data);
        }
        [HttpPost]
        public ActionResult FineSchemeAdd(ScFineScheme model, IEnumerable<ScFineSchemeDetails> subjectEntry)
        {
            var usr = _authentication.GetAuthenticatedUser();
            model.CreatedDate = DateTime.UtcNow;
            model.CreatedById = usr.Id;
            model.UpdatedDate = DateTime.UtcNow;
            model.UpdatedById = usr.Id;
            _scFineSchemeRepository.Add(model);
            foreach (var item in subjectEntry.Where(x => x.Days != null))
            {
                var data = new ScFineSchemeDetails()

                               {
                                   Days = Convert.ToInt32(item.Days),
                                   Amount = item.Amount,
                                   Percentage = item.Percentage,
                                   MasterId = model.Id

                               };
                _scFineSchemeDetailsRepository.Add(data);
            }
            return Content("True");
        }
        [CheckPermission(Permissions = "Edit", Module = "ScFS")]
        public ActionResult FineSchemeEdit(int id)
        {
            var model = new ScFineScheme();
            var data = _scFineSchemeRepository.GetById(id);
            var detail = _scFineSchemeDetailsRepository.GetMany(x => x.MasterId == data.Id);
            model = data;
            model.FineSchemeDetialses = detail;
            return PartialView("_PartialFineSchemeEdit", model);
        }
        [HttpPost]
        public ActionResult FineSchemeEdit(ScFineScheme model, IEnumerable<ScFineSchemeDetails> subjectEntry)
        {
            _scFineSchemeDetailsRepository.Delete(x => x.MasterId == model.Id);
            var usr = _authentication.GetAuthenticatedUser();
            var fine = _scFineSchemeRepository.GetById(model.Id);
            fine.UpdatedDate = DateTime.UtcNow;
            fine.UpdatedById = usr.Id;
            fine.ClassId = model.ClassId;
            fine.Description = model.Description;
            _scFineSchemeRepository.Update(fine);
            foreach (var item in subjectEntry.Where(x => x.Days != null))
            {
                var data = new ScFineSchemeDetails()

                {
                    Days = Convert.ToInt32(item.Days),
                    Amount = item.Amount,
                    Percentage = item.Percentage,
                    MasterId = model.Id

                };
                _scFineSchemeDetailsRepository.Add(data);
            }
            return Content("True");
        }

        public ActionResult FineSchemeDatailAdd()
        {
            var data = new ScFineSchemeDetails();
            return PartialView("_PartialFineSchemeDetailAdd", data);
        }
        public ActionResult FineSchemeDelete(int id)
        {
            _scFineSchemeRepository.Delete(x => x.Id == id);
            return RedirectToAction("FineSchemes");
        }
        #endregion

        #region Prepaid Schemes
        [CheckPermission(Permissions = "Navigate", Module = "ScPS")]
        public ActionResult PrePaidSchemes()
        {
            return View();
        }
        public JsonResult AcademyPrePaidSchemesExists(string Description, int Id)
        {
            var feeterm = new ScPrePaidScheme();
            if (Id != 0)
            {
                var data = _scPrePaidSchemeRepository.GetById(x => x.Id == Id);
                if (data.Description != Description)
                {
                    feeterm = _scPrePaidSchemeRepository.GetById(x => x.Description == Description);

                }
            }
            else
            {
                feeterm = _scPrePaidSchemeRepository.GetById(x => x.Description == Description);
            }
            if (feeterm == null)
            {
                feeterm = new ScPrePaidScheme();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScPS")]
        public ActionResult PrePaidSchemeList(int pageNo = 1)
        {
            var data = _scPrePaidSchemeRepository.GetAll().AsQueryable().ToPagedList(pageNo, SystemControl.PageSize);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialPrePaidSchemeList", data);
        }
        [CheckPermission(Permissions = "Create", Module = "ScPS")]
        public ActionResult PrePaidSchemeAdd()
        {
            var data = new ScPrePaidScheme();
            return PartialView("_PartialPrePaidSchemeAdd", data);
        }
        [HttpPost]
        public ActionResult PrePaidSchemeAdd(ScPrePaidScheme model, IEnumerable<ScPrePaidSchemeDetails> subjectEntry)
        {
            var usr = _authentication.GetAuthenticatedUser();
            model.CreatedDate = DateTime.UtcNow;
            model.CreatedById = usr.Id;
            model.UpdatedDate = DateTime.UtcNow;
            model.UpdatedById = usr.Id;
            _scPrePaidSchemeRepository.Add(model);
            foreach (var item in subjectEntry.Where(x => x.Days != null))
            {
                var data = new ScPrePaidSchemeDetails()

                {
                    Days = Convert.ToInt32(item.Days),
                    Amount = item.Amount,
                    Percentage = item.Percentage,
                    MasterId = model.Id

                };
                _scPrePaidSchemeDetailsRepository.Add(data);
            }
            return Content("True");
        }
        [CheckPermission(Permissions = "Edit", Module = "ScPS")]
        public ActionResult PrePaidSchemeEdit(int id)
        {
            var model = new ScPrePaidScheme();
            var data = _scPrePaidSchemeRepository.GetById(id);
            var detail = _scPrePaidSchemeDetailsRepository.GetMany(x => x.MasterId == data.Id);
            model = data;
            model.PrePaidSchemeDetialses = detail;
            return PartialView("_PartialPrePaidSchemeEdit", model);
        }
        [HttpPost]
        public ActionResult PrePaidSchemeEdit(ScPrePaidScheme model, IEnumerable<ScPrePaidSchemeDetails> subjectEntry)
        {
            _scPrePaidSchemeDetailsRepository.Delete(x => x.MasterId == model.Id);
            var usr = _authentication.GetAuthenticatedUser();
            var fine = _scPrePaidSchemeRepository.GetById(model.Id);
            fine.UpdatedDate = DateTime.UtcNow;
            fine.UpdatedById = usr.Id;
            fine.ClassId = model.ClassId;
            fine.Description = model.Description;
            _scPrePaidSchemeRepository.Update(fine);
            foreach (var item in subjectEntry.Where(x => x.Days != null))
            {
                var data = new ScPrePaidSchemeDetails()

                {
                    Days = Convert.ToInt32(item.Days),
                    Amount = item.Amount,
                    Percentage = item.Percentage,
                    MasterId = model.Id

                };
                _scPrePaidSchemeDetailsRepository.Add(data);
            }
            return Content("True");
        }
        public ActionResult PrePaidSchemeDatailAdd()
        {
            var data = new ScPrePaidSchemeDetails();
            return PartialView("_PartialPrePaidSchemeDetailAdd", data);
        }
        public ActionResult PrePaidSchemeDelete(int id)
        {
            _scPrePaidSchemeRepository.Delete(x => x.Id == id);
            return RedirectToAction("PrePaidSchemes");
        }
        #endregion

        #region ClassSchedules
        [CheckPermission(Permissions = "Navigate", Module = "ScCS")]
        public ActionResult ClassSchedules()
        {
            ViewBag.UserRight = base.UserRight("ScCS");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScCS")]
        public ActionResult ClassScheduleAdd()
        {
            var model = new ScClassScheduleViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ClassScheduleAdd(ScClassScheduleViewModel model, IEnumerable<ScClassScheduleViewModel> busRouteInfoList)
        {
            foreach (var item in busRouteInfoList)
            {
                var schedule = new ScClassSchedule()
                                   {
                                       ClassId = model.ClassId,
                                       SectionId = model.SectionId,
                                       SubjectId = item.SubjectId,
                                   };
                _scclassscheduleRepository.Add(schedule);

                SaveClassScheduleDetail(schedule.Id, item);

            }
            return RedirectToAction("ClassSchedules");
        }

        public void SaveClassScheduleDetail(int scheduleId, ScClassScheduleViewModel item)
        {

            var sundayData = new ScClassScheduleDetail()
                                 {
                                     Day = "Sunday",
                                     StartTime = item.SundayStartTime,
                                     EndTime = item.SundayEndTim,
                                     ClassScheduleId = scheduleId,
                                     DisplayOrder = 1
                                 };
            _classscheduledetailRepository.Add(sundayData);

            var mondayData = new ScClassScheduleDetail()
            {
                Day = "Monday",
                StartTime = item.MondayStartTime,
                EndTime = item.MondayEndTime,
                ClassScheduleId = scheduleId,
                DisplayOrder = 2
            };
            _classscheduledetailRepository.Add(mondayData);

            var tuesdayData = new ScClassScheduleDetail()
            {
                Day = "Tuesday",
                StartTime = item.TuesdayStartTime,
                EndTime = item.TuesdayEndTime,
                ClassScheduleId = scheduleId,
                DisplayOrder = 3
            };
            _classscheduledetailRepository.Add(tuesdayData);

            var wednesdayData = new ScClassScheduleDetail()
            {
                Day = "Wednesday",
                StartTime = item.WednesdayStartTime,
                EndTime = item.WednesdayEndTime,
                ClassScheduleId = scheduleId,
                DisplayOrder = 4
            };
            _classscheduledetailRepository.Add(wednesdayData);

            var thursdayData = new ScClassScheduleDetail()
            {
                Day = "Thursday",
                StartTime = item.ThursdayStartTime,
                EndTime = item.ThursdayEndTime,
                ClassScheduleId = scheduleId,
                DisplayOrder = 5
            };
            _classscheduledetailRepository.Add(thursdayData);

            var fridayData = new ScClassScheduleDetail()
            {
                Day = "Friday",
                StartTime = item.FridayStartTime,
                EndTime = item.FridayEndTime,
                ClassScheduleId = scheduleId,
                DisplayOrder = 6
            };
            _classscheduledetailRepository.Add(fridayData);


        }

        public void EditClassScheduleDetail(ScClassScheduleViewModel model, ScClassScheduleViewModel item)
        {
            var sundayData =
                _classscheduledetailRepository.GetById(
                    x =>
                    x.ClassSchedule.ClassId == model.ClassSchedule.ClassId && x.ClassSchedule.SectionId == model.ClassSchedule.SectionId &&
                    x.ClassSchedule.SubjectId == item.SubjectId && x.Day.ToLower() == "sunday");

            sundayData.StartTime = item.SundayStartTime;
            sundayData.EndTime = item.SundayEndTim;
            _classscheduledetailRepository.Update(sundayData);


            var mondayData =
               _classscheduledetailRepository.GetById(
                   x =>
                   x.ClassSchedule.ClassId == model.ClassSchedule.ClassId && x.ClassSchedule.SectionId == model.ClassSchedule.SectionId &&
                   x.ClassSchedule.SubjectId == item.SubjectId && x.Day.ToLower() == "monday");

            mondayData.StartTime = item.MondayStartTime;
            mondayData.EndTime = item.MondayEndTime;
            _classscheduledetailRepository.Update(mondayData);

            var tuesdayData =
                _classscheduledetailRepository.GetById(
                    x =>
                    x.ClassSchedule.ClassId == model.ClassSchedule.ClassId && x.ClassSchedule.SectionId == model.ClassSchedule.SectionId &&
                    x.ClassSchedule.SubjectId == item.SubjectId && x.Day.ToLower() == "tuesday");

            tuesdayData.StartTime = item.TuesdayStartTime;
            tuesdayData.EndTime = item.TuesdayEndTime;
            _classscheduledetailRepository.Update(tuesdayData);


            var wednesdayData =
                 _classscheduledetailRepository.GetById(
                     x =>
                     x.ClassSchedule.ClassId == model.ClassSchedule.ClassId && x.ClassSchedule.SectionId == model.ClassSchedule.SectionId &&
                     x.ClassSchedule.SubjectId == item.SubjectId && x.Day.ToLower() == "wednesday");

            wednesdayData.StartTime = item.WednesdayStartTime;
            wednesdayData.EndTime = item.WednesdayEndTime;
            _classscheduledetailRepository.Update(wednesdayData);

            var thursdayData =
                 _classscheduledetailRepository.GetById(
                     x =>
                     x.ClassSchedule.ClassId == model.ClassSchedule.ClassId && x.ClassSchedule.SectionId == model.ClassSchedule.SectionId &&
                     x.ClassSchedule.SubjectId == item.SubjectId && x.Day.ToLower() == "thursday");

            thursdayData.StartTime = item.ThursdayStartTime;
            thursdayData.EndTime = item.ThursdayEndTime;
            _classscheduledetailRepository.Update(thursdayData);

            var fridayData =
                 _classscheduledetailRepository.GetById(
                     x =>
                     x.ClassSchedule.ClassId == model.ClassSchedule.ClassId && x.ClassSchedule.SectionId == model.ClassSchedule.SectionId &&
                     x.ClassSchedule.SubjectId == item.SubjectId && x.Day.ToLower() == "friday");

            fridayData.StartTime = item.FridayStartTime;
            fridayData.EndTime = item.FridayEndTime;
            _classscheduledetailRepository.Update(fridayData);

        }
        [CheckPermission(Permissions = "Edit", Module = "ScCS")]
        public ActionResult ClassScheduleEdit(int classid, int sectionid)
        {
            var model = new ScClassScheduleViewModel();
            model.ClassSchedule = sectionid != 0 ? _scclassscheduleRepository.GetById(x => x.ClassId == classid && x.SectionId == sectionid) : _scclassscheduleRepository.GetById(x => x.ClassId == classid);
            model.OldClassId = classid;
            model.OldSectionId = sectionid;
            return View(model);
        }

        [HttpPost]
        public ActionResult ClassScheduleEdit(ScClassScheduleViewModel model, IEnumerable<ScClassScheduleViewModel> busRouteInfoList)
        {
            //_classscheduledetailRepository.Delete(x => x.ClassSchedule.ClassId == model.OldClassId && x.ClassSchedule.SectionId == model.OldSectionId);
            //_scclassscheduleRepository.Delete(x => x.ClassId == model.OldClassId && x.SectionId == model.OldSectionId);


            foreach (var item in busRouteInfoList)
            {
                EditClassScheduleDetail(model, item);

            }
            return RedirectToAction("ClassSchedules");
        }

        [CheckPermission(Permissions = "Navigate", Module = "ScCS")]
        public ActionResult ClassSchedulesList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScCS");
            var lstClassSchedules = from d in _scclassscheduleRepository.GetAll().OrderByDescending(x => x.Id)
                                    group d by new { d.ClassId, d.SectionId }
                                        into g
                                        select g.ToList();
            //var lstClassSchedules = _scclassscheduleRepository.GetAll().OrderByDescending(x => x.Id).GroupBy(m => m.ClassId);
            return PartialView("_PartialListClassSchedules", lstClassSchedules.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult GetTimeScheduleByClass(int classid)
        {
            var subjectllist = _classSubjectMappingRepository.GetMany(x => x.ClassId == classid);
            return PartialView(subjectllist);
        }

        public ActionResult DeleteClassSchedule(int id)
        {
            _scclassscheduleRepository.Delete(x => x.Id == id);
            _classscheduledetailRepository.Delete(x => x.ClassScheduleId == id);
            return Json(true);
        }



        public ActionResult GetTimeScheduleByClassEdit(int classid, int sectionid)
        {
            IEnumerable<ScClassSchedule> list;
            list = sectionid != 0 ? _scclassscheduleRepository.GetMany(x => x.ClassId == classid && x.SectionId == sectionid) : _scclassscheduleRepository.GetMany(x => x.ClassId == classid);
            foreach (var item in list)
            {
                item.ScheduleDetail =
                    _classscheduledetailRepository.GetMany(
                        x => x.ClassSchedule.ClassId == classid && x.ClassSchedule.SectionId == sectionid && x.ClassSchedule.SubjectId == item.SubjectId);
            }
            return PartialView(list);
        }

        #endregion ClassSchedules

        #region ProgramMasters

        public JsonResult CheckCodeInProgramMaster(string Code, int Id)
        {

            var feeterm = new ScProgramMaster();
            if (Id != 0)
            {
                var data = _scprogrammasterRepository.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm =
                        _scprogrammasterRepository.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower());

                }
            }
            else
            {
                feeterm = _scprogrammasterRepository.GetById(x => x.Code.ToLower().Trim() == Code.Trim().ToLower());
            }
            if (feeterm == null)
            {
                feeterm = new ScProgramMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)

                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }
        public JsonResult CheckDescriptionInProgramMaster(string Description, int Id)
        {

            var feeterm = new ScProgramMaster();
            if (Id != 0)
            {
                var data = _scprogrammasterRepository.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm =
                        _scprogrammasterRepository.GetById(x => x.Description.ToLower().Trim() == Description.Trim().ToLower());

                }
            }
            else
            {
                feeterm = _scprogrammasterRepository.GetById(x => x.Description.ToLower().Trim() == Description.Trim().ToLower());
            }
            if (feeterm == null)
            {
                feeterm = new ScProgramMaster();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);


        }
        [CheckPermission(Permissions = "Navigate", Module = "ScPM")]
        public ActionResult ProgramMasters()
        {
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScPM")]
        public ActionResult ProgramMasterAdd()
        {

            var model = new ScProgramMaster();
            var list = _scprogrammasterRepository.GetAll();
            model.FacultyList = list.Select(x => x.FacultyDescription.ToUpper().Trim()).Distinct();
            model.UniversityList = list.Select(x => x.UniversityDescription.ToUpper().Trim()).Distinct();
            model.LevelList = list.Select(x => x.LevelDescription.ToUpper().Trim()).Distinct();
            var scProgramMaster = _scprogrammasterRepository.GetAll().LastOrDefault();
            if (scProgramMaster != null)
            {
                model.Schedule = scProgramMaster.Schedule;
            }
            model.DurationId = 1;
            return PartialView("_PartialProgramMasterAdd", model);
        }

        [HttpPost]
        public ActionResult ProgramMasterAdd(ScProgramMaster model, IEnumerable<AcademicSemesterAddViewModel> SemesterEntry)
        {
            if (ModelState.IsValid)
            {
                _scprogrammasterRepository.Add(model);
                var sno = _scClassRepository.GetAll().Select(s => model.Schedule).Max() + 1;
                if (SemesterEntry != null)
                {
                    foreach (var item in SemesterEntry.Where(x => x.SemesterDesc != null))
                    {
                        var c = new SchClass()

                                    {
                                        Code = item.Code,
                                        Description = item.SemesterDesc,
                                        Incharge = model.Incharge,

                                        ProgramId = model.Id,

                                        Schedule = sno
                                    };
                        _scClassRepository.Add(c);
                        sno++;
                    }

                    return Content("True");
                }
            }
            return Content("False");
        }
        [CheckPermission(Permissions = "Edit", Module = "ScPM")]
        public ActionResult ProgramMasterEdit(int id)
        {


            var viewmodel = new ScProgramClassViewModel();


            ScProgramMaster objScProgramMaster = _scprogrammasterRepository.GetById(x => x.Id == id);

            viewmodel.Class = _scClassRepository.GetMany(x => x.ProgramId == objScProgramMaster.Id).Select(x => new AcademicSemesterAddViewModel
            {
                Code = x.Code,
                SemesterDesc = x.Description,
                Sno = x.Schedule,
                classId = x.Id
            });
            viewmodel.ClassCount = viewmodel.Class.Count();
            viewmodel.ProgramMaster = objScProgramMaster;

            var list = _scprogrammasterRepository.GetAll();
            viewmodel.ProgramMaster.FacultyList = list.Select(x => x.FacultyDescription.ToUpper().Trim()).Distinct();
            viewmodel.ProgramMaster.UniversityList = list.Select(x => x.UniversityDescription.ToUpper().Trim()).Distinct();
            viewmodel.ProgramMaster.LevelList = list.Select(x => x.LevelDescription.ToUpper().Trim()).Distinct();

            return PartialView("_PartialProgramMasterEdit", viewmodel);
        }

        [HttpPost]
        public ActionResult ProgramMasterEdit(ScProgramClassViewModel model, IEnumerable<AcademicSemesterAddViewModel> SemesterEntry)
        {
            //if (ModelState.IsValid)
            //{
            var objScProgramMaster = _scprogrammasterRepository.GetById(x => x.Id == model.ProgramMaster.Id);
            objScProgramMaster.Code = model.ProgramMaster.Code;
            objScProgramMaster.Description = model.ProgramMaster.Description;
            objScProgramMaster.Incharge = model.ProgramMaster.Incharge;
            objScProgramMaster.LevelDescription = model.ProgramMaster.LevelDescription;
            objScProgramMaster.UniversityDescription = model.ProgramMaster.UniversityDescription;
            objScProgramMaster.FacultyDescription = model.ProgramMaster.FacultyDescription;
            objScProgramMaster.Schedule = model.ProgramMaster.Schedule;
            objScProgramMaster.DurationId = model.ProgramMaster.DurationId;


            _scprogrammasterRepository.Update(objScProgramMaster);
            //var sno = 1;
            //_scClassRepository.Delete(x => x.ProgramId == objScProgramMaster.Id);
            //if (SemesterEntry != null)
            //{
            //    foreach (var item in SemesterEntry.Where(x => x.SemesterDesc != null))
            //    {
            //        var c = new SchClass()

            //                    {
            //                        Code = item.Code,
            //                        Description = item.SemesterDesc,
            //                        Incharge = model.ProgramMaster.Incharge,

            //                        ProgramId = model.ProgramMaster.Id,

            //                        Schedule = sno
            //                    };
            //        sno++;
            //        _scClassRepository.Add(c);

            //    }
            //}

            //return RedirectToAction("ProgramMasterEdit", new {programmasterId = objScProgramMaster.Id});
            return Content("True");
            //}
            //return null;
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScPM")]
        public ActionResult ProgramMastersList(int pageNo = 1)
        {
            var lstProgramMasters = _scprogrammasterRepository.GetAll().OrderByDescending(x => x.Id);
            //var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            //ViewBag.SnStart = snStart;
            return PartialView("_PartialProgramMasterList", lstProgramMasters);
        }

        public ActionResult ProgramMasterDelete(int programmasterId)
        {
            ScProgramMaster objScProgramMaster = _scprogrammasterRepository.GetById(x => x.Id == programmasterId);
            _scprogrammasterRepository.Delete(objScProgramMaster);
            return RedirectToAction("ProgramMasters");
        }
        public ActionResult ProgramSemesterDetail(int length, string code, int check)
        {
            var list = new List<AcademicSemesterAddViewModel>();
            for (int i = 0; i < length; i++)
            {
                decimal sno = i + 1;

                var data = new AcademicSemesterAddViewModel();
                if (check == 1)
                {
                    data = new AcademicSemesterAddViewModel
                   {
                       Sno = Convert.ToInt32(sno),
                       Code = code + "yr" + sno,
                       SemesterDesc = code + "- Year " + sno
                   };
                    list.Add(data);
                }
                else if (check == 2)
                {




                    data = new AcademicSemesterAddViewModel
                    {
                        Sno = Convert.ToInt32(sno),
                        Code = code + "sem" + sno,
                        SemesterDesc = code + "- Semester " + sno
                    };


                    list.Add(data);
                }
                else
                {




                    data = new AcademicSemesterAddViewModel
                    {
                        Sno = Convert.ToInt32(sno),
                        Code = code + "tri" + sno,
                        SemesterDesc = code + " - Trimester " + sno

                    };
                    list.Add(data);
                }


            }
            return PartialView("_PartialProgramSemesterDetailEntry", list);
        }
        #endregion ProgramMasters

        #region CharacterCertificateTemplates
        [CheckPermission(Permissions = "Navigate", Module = "CRTMP")]
        public ActionResult TemplateIndex()
        {
            ViewBag.UserRight = base.UserRight("CRTMP");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "CRTMP")]
        public ActionResult CertificateTemplates(int templateTypeid)
        {
            var model = new Template();
            model.TemplateType = templateTypeid;
            model.TemplateTypeName = new StringEnum(typeof(TemplateType)).GetStringValue(model.TemplateType.ToString());
            return View(model);
        }
        [CheckPermission(Permissions = "Edit", Module = "CRTMP")]
        public ActionResult CertificateTemplateEditor(int typeid)
        {
            var model = _templateRepository.GetById(m => m.TemplateType == typeid);
            if (model != null)
            {
                model.TemplateTypeName = new StringEnum(typeof(TemplateType)).GetStringValue(model.TemplateType.ToString());
                return PartialView(model);
            }
            var newModel = new Template();
            newModel.TemplateType = typeid;
            newModel.TemplateTypeName = new StringEnum(typeof(TemplateType)).GetStringValue(typeid.ToString());
            return PartialView(newModel);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CertificateTemplateEditor(Template model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    model.ModifiedBy = _authentication.GetAuthenticatedUser().Id;
                    model.ModifiedOn = DateTime.UtcNow;
                    _templateRepository.Update(model);
                }
                else
                {
                    model.CreatedBy = _authentication.GetAuthenticatedUser().Id;
                    model.CreatedOn = DateTime.UtcNow;
                    model.ModifiedBy = _authentication.GetAuthenticatedUser().Id;
                    model.ModifiedOn = DateTime.UtcNow;
                    _templateRepository.Add(model);
                }

                return Content("True");
            }

            return Content("False");

        }
        public Template BuildTemplate(int certificatetype, int studentid)
        {
            var student = _scStudentRegistrationDetailRepository.GetById(x => x.StudentId == studentid);
            var date = DateTime.UtcNow.ToShortDateString();
            var session = base.AcademicYear.FiscalYear.StartDate.ToShortDateString() + "-" +
                          base.AcademicYear.FiscalYear.EndDate.ToShortDateString();
            var year = base.AcademicYear.FiscalYear.StartDate.Year.ToStr();
            var studentSLCInfo = _studentSlc.GetById(x => x.StudentId == studentid);

            if (certificatetype == (int)CertificateTypeEnum.CharacterCertificateSLC)
            {
                if (studentSLCInfo != null)
                {

                    var reportModel =
                        _templateRepository.GetById(x => x.TemplateType == (int)TemplateType.CharacterCertificateSLC);
                    if (reportModel != null)
                    {
                        var address = student.Studentinfo.PerAdd + " - " + student.Studentinfo.PerWardNo + " ," +
                                      student.Studentinfo.PerDistrict;
                        reportModel.Description =
                            reportModel.Description.Replace("&lt;date&gt;",
                                                            date).Replace(
                                                                " &lt;Name&gt;",
                                                                "<b>" + student.
                                                                            Studentinfo
                                                                            .StuDesc + "</b>").
                                Replace("&lt;FatherName&gt;", "<b>" + student.Studentinfo.FName + "</b>").
                                Replace("&lt;permanentAddress&gt;", "<b>" + address + "</b>").
                                Replace("&lt;Todaydate&gt;", "<b>" + DateTime.Now.ToShortDateString() + "</b>").Replace(
                                    "&lt;division&gt;", "<b>" + studentSLCInfo.Division + "</b>").Replace(
                                        "&lt;symbolNo&gt;", "<b>" + studentSLCInfo.SlcSymbolNo + "</b>").Replace(
                                            "&lt;passedYearBS&gt;", "<b>" + studentSLCInfo.PassYearBS + "</b>").Replace(
                                                "&lt;passedYearAD&gt;", "<b>" + studentSLCInfo.PassYearAD + "</b>").
                                Replace(
                                    "&lt;RegNo&gt;", "<b>" + studentSLCInfo.SlcRegNo + "</b>").Replace(
                                        "&lt;DOBBS&gt;",
                                        "<b>" + Convert.ToDateTime(student.Studentinfo.DBO).ToShortDateString() + "</b>")
                                .Replace(
                                    "&lt;DOBAD&gt;", "<b>" + student.Studentinfo.DBOMiti + "</b>").Replace(
                                        "&lt;passedDate&gt;", Convert.ToDateTime(
                                            student.Studentinfo.PassYear).ToShortDateString()
                                );
                        reportModel.CertificateType = (int)CertificateTypeEnum.CharacterCertificate;
                        return reportModel;
                    }
                    return null;

                }


                return null;

            }

            if (certificatetype == (int)CertificateTypeEnum.CharacterCertificate)
            {
                var reportModel =
                    _templateRepository.GetById(x => x.TemplateType == (int)TemplateType.CharacterCertificate);
                if (reportModel != null)
                {
                    var address = student.Studentinfo.PerAdd + " - " + student.Studentinfo.PerWardNo + " ," +
                                  student.Studentinfo.PerDistrict;
                    reportModel.Description =
                        reportModel.Description.Replace("&lt;date&gt;",
                                                        date).Replace(
                                                            " &lt;Name&gt;",
                                                            "<b>" + student.
                                                                        Studentinfo
                                                                        .StuDesc + "</b>").
                            Replace("&lt;FatherName&gt;", "<b>" + student.Studentinfo.FName + "</b>").
Replace("&lt;permanentAddress&gt;", "<b>" + address + "</b>").
Replace("&lt;Todaydate&gt;", "<b>" + DateTime.Now.ToShortDateString() + "</b>").Replace(
                                "&lt;RegNo&gt;", "<b>" + student.Studentinfo.Regno + "</b>").Replace(
                                "&lt;DOBBS&gt;", "<b>" + Convert.ToDateTime(student.Studentinfo.DBO).ToShortDateString() + "</b>").Replace(
                                "&lt;DOBAD&gt;", "<b>" + student.Studentinfo.DBOMiti + "</b>");
                    reportModel.CertificateType = (int)CertificateTypeEnum.CharacterCertificate;
                    return reportModel;
                }
                return null;
            }

            var reportModelTransfer =
                _templateRepository.GetById(x => x.TemplateType == (int)TemplateType.TransferCertificate);
            if (reportModelTransfer != null)
            {
                reportModelTransfer.Description = reportModelTransfer.Description.Replace("&lt;date&gt;",
                                                                                          date).Replace(
                                                                                              "&lt;StudentName&gt;",
                                                                                              student.
                                                                                                  Studentinfo
                                                                                                  .StuDesc).
                    Replace("&lt;DOB&gt;", Convert.ToDateTime(student.Studentinfo.DBO).ToShortDateString()).
                    Replace("&lt;Nationality&gt;", student.Studentinfo.Nationality).
                    Replace("&lt;Class&gt;", student.StudentRegistration.Class.Description)
                    .Replace("&lt;Year&gt;", year).Replace("&lt;LeavingDate&gt;", DateTime.UtcNow.ToShortDateString());
                reportModelTransfer.CertificateType = (int)CertificateTypeEnum.TransferCertificate;
                return reportModelTransfer;
            }
            return null;
        }
        #endregion CertificateTemplates

        #region ReRegistration
        [CheckPermission(Permissions = "Navigate", Module = "ScStdRereg")]
        public ActionResult ReRegistration()
        {
            //ViewBag.UserRight = base.UserRight("ScStdRereg");
            var viewModel = new ReRegistrationViewModel();



            viewModel.ClassList = new SelectList(_scClassRepository.GetAll(), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, new SelectListItem()
                                              {
                                                  Value = "0",
                                                  Selected = true,
                                                  Text = "None"
                                              });


            return View(viewModel);
        }
        public ActionResult GetStd(int classId)
        {

            //var _context = new DataContext();

            
            var ay = _academicYear.GetAll().Select(x => new
            {
                value = x.Id,
                text = base.SystemControl.DateType == 1 ? (x.StartDate.ToString("dd-MM-yyyy") + " To  " + x.EndDate.ToString("dd-MM-yyyy")) : (x.StartDateNep + " To  " + x.EndDateNep),
            });
            var studentDetails = _scStudentRegistrationDetailRepository.GetMany(
                                        x => x.StudentRegistration.ClassId == classId && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId);
            var examMark = from a in studentDetails
                           join r in
                               from c in
                                   (from c in
                                        _marksEntryRepository.GetMany(
                                            x =>
                                            x.ClassId == classId && x.AcademicYearId == CurrentAcademyYearId &&
                                            x.Exam.IsFinal)
                                    join sd in
                                        studentDetails on c.StudentId equals
                                        sd.StudentId
                                    orderby c.Rank
                                    select c)
                               group c by new { c.ClassId, c.StudentId }
                                   into g
                                   select g.FirstOrDefault() on a.StudentId equals r.StudentId into tt
                           from t in tt.DefaultIfEmpty()
                           select new ScExamMarksEntry()
                                      {
                                          StudentId = a.StudentId,
                                          Remarks = t == null ? "-" : t.Result,
                                          ClassId = a.StudentRegistration.ClassId,
                                          TotalFullMarks = t == null ? 0 : t.TotalFullMarks,
                                          Percentage = t == null ? 0 : t.Percentage,
                                          DivisionId = t == null ? 0 : t.DivisionId,
                                          TotalObtainedMarks = t == null ? 0 : t.TotalObtainedMarks,
                                          Result = t == null ? "Not Attend Exam" : t.Result,

                                          // studen = x.Studentinfo.StuDesc

                                      };



            ;
            //var examMark =
            //    from c in
            //        _marksEntryRepository.GetMany(x => x.ClassId == classId && x.AcademicYearId == CurrentAcademyYearId && x.Exam.IsFinal)
            //    group c by new {c.ClassId, c.StudentId}
            //    into g
            //    select (from n in g
            //            join sd in
            //                _scStudentRegistrationDetailRepository.GetMany(
            //                    x => x.StudentRegistration.ClassId == g.Key.ClassId) on n.StudentId equals sd.StudentId
            //            orderby n.Rank
            //           // where examId==0||n.ExamId==examId
            //            select n);


            var list = new List<ResultGenerationViewModel>();
            //  var rollno = 1;
            foreach (var x in examMark)
            {
                //  var x = item.FirstOrDefault();

                if (x != null)
                {
                    var data = new ResultGenerationViewModel();

                    data.Checkbox = x.Result.ToLower() == "pass";
                    data.StudentId = x.StudentId;
                    data.ClassId = x.ClassId;
                    data.TotalMark = x.TotalFullMarks;
                    data.Percentage = x.Percentage;
                    if (x.DivisionId != 0)
                        data.Division = _scDivisionRepository.GetById(x.DivisionId).Description;
                    data.ObtMark = x.TotalObtainedMarks;
                    data.Result = x.Result;
                    data.StudentName = _scStudentinfoRepository.GetById(y => y.StudentID == x.StudentId).StuDesc;
                    // data.RollNo = rollno.ToString();
                    list.Add(data);
                }


                //rollno++;
            }
            if (examMark.Any())
            {
                list.FirstOrDefault()
                    .CLassList = new SelectList(_scClassRepository.GetAll(), "Id", "Description").ToList();
                list.FirstOrDefault().CLassList.Insert(0, new SelectListItem()
                                                              {
                                                                  Value = "0",
                                                                  Selected = true,
                                                                  Text = "None"
                                                              });
                list.FirstOrDefault().AcademyList = new SelectList(ay, "value", "text");

            }
            var view = this.RenderPartialViewToString("_partialResult", list);
            return Json(view, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ReRegistration(ResultGenerationViewModel model, IEnumerable<ResultGenerationViewModel> Rereneration)
        {



            var user = _authentication.GetAuthenticatedUser();
            var regdata =
                _scStudentRegistrationRepository.GetById(
                    x => x.ClassId == model.PromoteClassId && x.AcademicYearId == model.AYId);

            var regid = 0;
            if (regdata == null)
            {
                var stuReg = new ScStudentRegistration();
                // var documentNumbering = _rollNumbering.GetById(x=>x.AcademyYearId==AcademicYear.Id && x.ClassId==model.PromoteClassId && x.SectionId==model.s)
                stuReg.CreatedDate = DateTime.UtcNow;
                stuReg.UpdatedDate = DateTime.UtcNow;
                stuReg.UpdatedById = user.Id;
                stuReg.CreatedById = user.Id;
                stuReg.ClassId = model.PromoteClassId;
                stuReg.AcademicYearId = model.AYId;
                _scStudentRegistrationRepository.Add(stuReg);
                regid = stuReg.Id;
            }
            else
            {
                regid = regdata.Id;
            }





            foreach (var item in Rereneration.Where(x => x.Checkbox))
            {


                _scStudentRegistrationDetailRepository.Delete(
                    x => x.StudentRegistration.AcademicYearId == model.AYId && x.StudentId == item.StudentId);

                var student = _scStudentinfoRepository.GetById(item.StudentId);
                student.PrevClassId = student.ClassId;
                student.ClassId = model.PromoteClassId;
                _scStudentinfoRepository.Update(student);
                var dat = new ScStudentRegistrationDetail()
                              {
                                  CurrentStatus = 0,
                                  Narration = "",
                                  RegMasterId = regid,
                                  RollNo = item.RollNo,
                                  SectionId = 0,
                                  ShiftId = 0,
                                  StudentId = item.StudentId,
                                  StudentType = 0,
                                  BoaderId = 0,


                              };
                _scStudentRegistrationDetailRepository.Add(dat);

            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult StudentSlc()
        {
            return View();
        }
        public ActionResult StudentSlcList()
        {
            var student = _studentSlc.GetAll();
            return PartialView(student);
        }
        public ActionResult StudentSlcAdd()
        {

            return PartialView();
        }
        public ActionResult GetRegisteredStudent()
        {
            var student = _scStudentRegistrationDetailRepository.GetAll().AsQueryable().Select(x => new
            {
                //Id = x.StudentRegistrationDetails.FirstOrDefault().StudentId,
                //Name =x.StudentRegistrationDetails.FirstOrDefault().Studentinfo.FName
                Id = x.StudentId,
                Name = x.Studentinfo.StuDesc,
                //Academic=x.AcademicYearId

            });

            return Json(student, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult StudentSlcAdd(StudentSlcInfo model)
        {
            if (ModelState.IsValid)
            {
                _studentSlc.Add(model);
                return Content("True");
            }
            return Content("False");
        }
        public ActionResult StudentSlcEdit(int id)
        {
            var student = _studentSlc.GetById(id);
            return PartialView(student);
        }
        [HttpPost]
        public ActionResult StudentSlcEdit(StudentSlcInfo model)
        {
            _studentSlc.Update(model);
            return Content("True");
        }
        public ActionResult StudentSlcDelete(int id)
        {
            _studentSlc.Delete(x => x.Id == id);
            return Json(true);
        }


    }
}
