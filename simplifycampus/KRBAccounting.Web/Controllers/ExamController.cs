using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.Academy;

namespace KRBAccounting.Web.Controllers
{
    [Authorize]
    [CheckModulePermission("Academy")]
    public class ExamController : BaseController
    {
        private readonly IScClassRepository _scClassRepository;
        private readonly IScClassSubjectMappingRepository _classSubjectMappingRepository;
        private readonly IScExamRepository _examRepository;
        private readonly IScExamRoutineRepository _examRoutineRepository;
        private readonly IScExamMarkSetupRepository _examMarkSetupRepository;
        private readonly IFormsAuthenticationService _authentication;
        private readonly IScStudentRegistrationDetailRepository _scStudentRegistrationDetailRepository;
        private readonly IScExamAttendanceMasterRepository _scexamattendancemasterRepository;
        private readonly IScExamAttendanceDetailRepository _scexamattendancedetailRepository;
        private readonly IScExamMarksEntryRepository _scexammarksentryRepository;
        private readonly IScConsolidatedMarksSetupRepository _scconsolidatedmarkssetupRepository;
        private readonly IScDivisionRepository _divisionRepository;
        private readonly ISystemControlRepository _systemControl;
        private readonly IScProgramMasterRepository _programMasterRepository;
        private readonly IBoardExamMasterRepository _boardExamMasterRepository;
        private readonly IBoardExamDetailRepository _boardExamDetailRepository;
        private readonly IScDivisionRepository _scDivisionRepository;
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IScStudentinfoRepository _scStudentinfoRepository;
        //private readonly IClassIdPercentageRepository _classidPercentageRepository;
        
        

        

        private int CurrentAcademyYearId;
        #region Constructor

        public ExamController(
            IScStudentinfoRepository scStudentinfoRepository,
            //IClassIdPercentageRepository classidPercentageRepository,
            IScDivisionRepository scDivisionRepository,
            IAcademicYearRepository academicYearRepository,
            IScProgramMasterRepository programMasterRepository,
            IScConsolidatedMarksSetupRepository scconsolidatedmarkssetupRepository,
                                IScClassRepository scClassRepository,
                                IScClassSubjectMappingRepository classSubjectMappingRepository,
                                IScExamRepository scExamRepository,
                                IScExamRoutineRepository scExamRoutineRepository,
                                IScExamMarksEntryRepository scexammarksentryRepository,
                                IScExamMarkSetupRepository scExamMarkSetupRepository,
                                IScStudentRegistrationDetailRepository StudentRegistrationDetail,
                                IScExamAttendanceMasterRepository scexamattendancemasterRepository,
                                IScExamAttendanceDetailRepository scexamattendancedetailRepository,
                IScDivisionRepository divisionRepository,
            ISystemControlRepository systemControl,
            IBoardExamMasterRepository boardExamMasterRepository,
            IBoardExamDetailRepository boardExamDetailRepository



            )
        {
            //_classidPercentageRepository = classidPercentageRepository;
            _academicYearRepository = academicYearRepository;
            _boardExamMasterRepository = boardExamMasterRepository;
            _boardExamDetailRepository = boardExamDetailRepository;
            _scconsolidatedmarkssetupRepository = scconsolidatedmarkssetupRepository;
            _scClassRepository = scClassRepository;
            _scStudentinfoRepository = scStudentinfoRepository;
            _classSubjectMappingRepository = classSubjectMappingRepository;
          
            _examRepository = scExamRepository;

            _examRoutineRepository = scExamRoutineRepository;

            _examMarkSetupRepository = scExamMarkSetupRepository;
            _divisionRepository = divisionRepository;

            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Academy);

            _scStudentRegistrationDetailRepository = StudentRegistrationDetail;
            _scexamattendancemasterRepository = scexamattendancemasterRepository;
            _scexamattendancedetailRepository = scexamattendancedetailRepository;
            _scexammarksentryRepository = scexammarksentryRepository;
            _systemControl = systemControl;
            _programMasterRepository = programMasterRepository;
            CurrentAcademyYearId = AcademicYear.Id;
            _scDivisionRepository = scDivisionRepository;


        }

        #endregion
        #region Exam
        public JsonResult AcademyExamDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScExam();
            if (Id != 0)
            {
                var data = _examRepository.GetById(x => x.Id == Id);
                if (data.Description.ToLower().Trim() != Description.ToLower().Trim())
                {
                    feeterm = _examRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());
                    if (feeterm == null)
                    {

                    }
                }
            }
            else
            {
                feeterm = _examRepository.GetById(x => x.Description.ToLower().Trim() == Description.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScExam();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcademyExamCodeExists(string Code, int Id)
        {
            var feeterm = new ScExam();
            if (Id != 0)
            {
                var data = _examRepository.GetById(x => x.Id == Id);
                if (data.Code.ToLower().Trim() != Code.ToLower().Trim())
                {
                    feeterm = _examRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());
                    if (feeterm == null)
                    {

                    }
                }
            }
            else
            {
                feeterm = _examRepository.GetById(x => x.Code.ToLower().Trim() == Code.ToLower().Trim());
            }
            if (feeterm == null)
            {
                feeterm = new ScExam();
            }

            return feeterm.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        public ActionResult OnlineExamAdd()
        {
            var viewModel = this.BuildAcademicsExamAddViewModel();
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult OnlineExamAdd(AcademicsExamAddViewModel model)
        {
            model.ScExam.StartDate = model.StartDate;
            model.ScExam.EndDate = model.EndDate;
            _examRepository.Add(model.ScExam);

            return Json(new { msg = "true", id = model.ScExam.Id, title = model.ScExam.Description },
                        JsonRequestBehavior.AllowGet);
        }


        [CheckPermission(Permissions = "Navigate", Module = "Exam")]
        public ActionResult Exams()
        {
            ViewBag.UserRight = base.UserRight("Exam");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "Exam")]
        public ActionResult ExamList(int pageNo = 1)
        {
            ViewBag.DateType = base.SystemControl.DateType;
            ViewBag.UserRight = base.UserRight("Exam");
            var viewModel = base.CreateViewModel<AcademicsExamViewModel>();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            viewModel.ScExam = _examRepository.GetPagedList(x=>x.Id,pageNo, base.SystemControl.PageSize);

            return PartialView(viewModel.ScExam);
        }
        [CheckPermission(Permissions = "Create", Module = "Exam")]
        public ActionResult ExamAdd()
        {
            var viewModel = this.BuildAcademicsExamAddViewModel();

            if (base.SystemControl.IsCurrDate)
            {
                viewModel.DisplayStartDate = base.SystemControl.DateType == 1
                                                 ? DateTime.Now.ToString("MM/dd/yyyy")
                                                 : NepaliDateService.NepaliDate(DateTime.Now).Date;
                viewModel.DisplayEndDate = base.SystemControl.DateType == 1
                                               ? DateTime.Now.ToString("MM/dd/yyyy")
                                               : NepaliDateService.NepaliDate(DateTime.Now).Date;


           



            }
            var data = _examRepository.GetAll();
            var lastOrDefault = data.LastOrDefault();
            if (lastOrDefault != null) viewModel.ScExam.Schedule = lastOrDefault.Schedule + 1;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult ExamAdd(AcademicsExamAddViewModel model)
        {


            var _con = new DataContext();
            _con.Database.SqlQuery<string>("Update ScExam set IsFinal = '0'");

            model.SystemControl = _systemControl.GetAll().FirstOrDefault();
            //if (model.SystemControl.DateType == 1)
            //{
            //    model.ScExam.StartDate = NepaliDateService.NepalitoEnglishDate(model.DisplayStartDate);
            //    model.ScExam.EndDate = NepaliDateService.NepalitoEnglishDate(model.DisplayEndDate);

            //}
            //else
            //{
            //    model.ScExam.StartMiti = model.DisplayStartDate;
            //    model.ScExam.EndMiti = model.DisplayEndDate;
            //}
            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayStartDate))
                {
                    model.ScExam.StartDate = Convert.ToDateTime(model.DisplayStartDate);
                    model.ScExam.StartMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayStartDate)).Date;
                }
                if (!string.IsNullOrEmpty(model.DisplayEndDate))
                {
                    model.ScExam.EndDate = Convert.ToDateTime(model.DisplayEndDate);
                    model.ScExam.EndMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayEndDate)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayStartDate))
                {
                    model.ScExam.StartMiti = model.DisplayStartDate;
                    model.ScExam.StartDate = NepaliDateService.NepalitoEnglishDate(model.ScExam.StartMiti);
                }
                if (!string.IsNullOrEmpty(model.DisplayEndDate))
                {
                    model.ScExam.EndMiti = model.DisplayEndDate;
                    model.ScExam.EndDate = NepaliDateService.NepalitoEnglishDate(model.ScExam.EndMiti);

                }


            }




            _examRepository.Add(model.ScExam);

            return Content("True");

        }

        public AcademicsExamAddViewModel BuildAcademicsExamAddViewModel()
        {
            var viewModel = base.CreateViewModel<AcademicsExamAddViewModel>();
            viewModel.ExamType = new SelectList(
                Enum.GetValues(typeof(ExamType)).Cast
                    <ExamType>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            viewModel.StartDate = DateTime.UtcNow;
            viewModel.EndDate = DateTime.UtcNow;
            viewModel.ScExam = new ScExam();
            return viewModel;


        }

        public AcademicsExamAddViewModel BuildAcademicsExamAddEditViewModel(int id)
        {
            var data = _examRepository.GetById(id);
            var viewModel = base.CreateViewModel<AcademicsExamAddViewModel>();
            viewModel.ExamType = new SelectList(
                Enum.GetValues(typeof(ExamType)).Cast
                    <ExamType>().Select(
                        x =>
                        new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text", data.Type);
            // viewModel.StartDate = data.StartDate;
            // viewModel.EndDate = data.EndDate;
            viewModel.ScExam = data;

            //viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            //if (viewModel.SystemControl.DateType == 1)
            //{
            //    viewModel.DisplayStartDate = viewModel.StartDate.ToShortDateString();
            //    viewModel.DisplayEndDate = viewModel.EndDate.ToShortDateString();
            //}
            //else
            //{
            //    viewModel.DisplayStartDate = viewModel.ScExam.StartMiti;
            //    viewModel.DisplayEndDate = viewModel.ScExam.EndMiti;
            //}
            return viewModel;


        }
        [CheckPermission(Permissions = "Edit", Module = "Exam")]
        public ActionResult ExamEdit(int id)
        {

            var viewModel = this.BuildAcademicsExamAddEditViewModel(id);

            if (base.SystemControl.DateType == 1)
            {
                if (viewModel.StartDate != null)
                {
                    viewModel.DisplayStartDate = Convert.ToDateTime(viewModel.ScExam.StartDate).ToString("MM/dd/yyyy");

                }
                if (viewModel.EndDate != null)
                {
                    viewModel.DisplayEndDate = Convert.ToDateTime(viewModel.ScExam.EndDate).ToString("MM/dd/yyyy");

                }

            }
            else
            {
                if (!string.IsNullOrEmpty(viewModel.ScExam.StartMiti))
                {
                    viewModel.DisplayStartDate = viewModel.ScExam.StartMiti;

                }
                if (!string.IsNullOrEmpty(viewModel.ScExam.EndMiti))
                {
                    viewModel.DisplayEndDate = viewModel.ScExam.EndMiti;


                }

            }
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult ExamEdit(AcademicsExamAddViewModel model)
        {
            var _con = new DataContext();
            _con.Database.SqlQuery<string>("Update ScExam set IsFinal = '0'");
            model.SystemControl = _systemControl.GetAll().FirstOrDefault();
            //if (model.SystemControl.DateType == 1)
            //{
            //    model.DisplayStartDate = model.StartDate.ToShortDateString();
            //    model.DisplayEndDate = model.EndDate.ToShortDateString();
            //}
            //else
            //{
            //    model.DisplayStartDate = model.ScExam.StartMiti;
            //    model.DisplayEndDate = model.ScExam.EndMiti;
            //}

            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayStartDate))
                {
                   model.ScExam.StartDate = Convert.ToDateTime(model.DisplayStartDate);
                   model.ScExam.StartMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayStartDate)).Date;
                }
                if (!string.IsNullOrEmpty(model.DisplayEndDate))
                {
                    model.ScExam.EndDate = Convert.ToDateTime(model.DisplayEndDate);
                   model.ScExam.EndMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayEndDate)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayStartDate))
                {
                    model.ScExam.StartMiti = model.DisplayStartDate;
                   model.ScExam.StartDate= NepaliDateService.NepalitoEnglishDate(model.ScExam.StartMiti);
                }
                if (!string.IsNullOrEmpty(model.DisplayEndDate))
                {
                    model.ScExam.EndMiti = model.DisplayEndDate;
                   model.ScExam.EndDate= NepaliDateService.NepalitoEnglishDate(model.ScExam.EndMiti);

                }


            }
            _examRepository.Update(model.ScExam);

            return Content("True");

        }
         [CheckPermission(Permissions = "Delete", Module = "Exam")]
        public ActionResult ExamDelete(int id)
        {
            var data = _examRepository.DeleteException(x => x.Id == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ExamRoutine


        [CheckPermission(Permissions = "Navigate", Module = "ER")]
        public ActionResult ExamRoutines()
        {
            ViewBag.UserRight = base.UserRight("ER");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ER")]
        public ActionResult ExamRoutineList()
        {
            ViewBag.UserRight = base.UserRight("ER");
            var viewModel = this.BuildAcademicsExamRoutineListViewModel();
            return PartialView(viewModel);
        }

        public AcademicsExamRoutineViewModel BuildAcademicsExamRoutineListViewModel()
        {
            var viewModel = base.CreateViewModel<AcademicsExamRoutineViewModel>();
            viewModel.ExamRoutineGrouping = from d in _examRoutineRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId)
                                            group d by new { d.ClassId, d.ExamId }
                                                into g
                                                select g.ToList();

            return viewModel;

        }

        public AcademicsExamRoutineViewModel BuildAcademicsExamRoutineEditViewModel(int ExamId, int ClassId)
        {
            var data =
                _examRoutineRepository.GetMany(
                    x => x.ClassId == ClassId && x.ExamId == ExamId && x.AcademicYearId == CurrentAcademyYearId);

            var viewModel = base.CreateViewModel<AcademicsExamRoutineViewModel>();
            viewModel.ExamRoutine =
                _examRoutineRepository.GetMany(x => x.ClassId == ClassId && x.ExamId == ExamId && x.AcademicYearId == CurrentAcademyYearId).FirstOrDefault();
            viewModel.ExamRoutines = data;
            viewModel.ExamId = ExamId;
            viewModel.ClassId = ClassId;
            return viewModel;

        }

        public AcademicsExamRoutineViewModel BuildAcademicsExamRoutineAddViewModel()
        {
            var viewModel = base.CreateViewModel<AcademicsExamRoutineViewModel>();
            viewModel.ExamRoutine = new ScExamRoutine();
            return viewModel;
        }
        [CheckPermission(Permissions = "Create", Module = "ER")]
        public ActionResult ExamRoutineAdd()
        {
            var date = new ScExamRoutine();
            date.ExamDate = null;

            var data = this.BuildAcademicsExamRoutineAddViewModel();

            //ViewBag.ExamList = new SelectList(_examRepository.GetAll(), "Id", "Description");
            //ViewBag.ClassList = new SelectList(_scClassRepository.GetAll(), "Id", "Description");
            ViewBag.DateType = base.SystemControl.DateType;
            return PartialView(data);
        }
        [CheckPermission(Permissions = "Edit", Module = "ER")]
        public ActionResult ExamRoutineEdit(int ExamId, int ClassId)
        {

            var data = this.BuildAcademicsExamRoutineEditViewModel(ExamId, ClassId);
            ViewBag.DateType = base.SystemControl.DateType;
            return PartialView(data);
        }

        public ActionResult ExamRoutineDetialAdd()
        {
            var data = new ScExamRoutine();
            ViewBag.DateType = base.SystemControl.DateType;
            data.ExamDate = DateTime.UtcNow.Date;
            return PartialView("_PartialExamRoutineEntry", data);
        }

        public ActionResult ClassWithSubjectByClassId(int classId, int examid)
        {
            ViewBag.DateType = base.SystemControl.DateType;
            var data = _classSubjectMappingRepository.GetMany(x => x.ClassId == classId);
            var examDate = _examRepository.GetById(examid).StartDate;
            List<ScExamRoutine> listroutine = new List<ScExamRoutine>();
            foreach (var item in data)
            {
                var routine = new ScExamRoutine()
                                  {
                                      SubjectId = item.SubjectId,
                                      Subject = item.Subject,
                                      EvaluateForType = item.EvaluateForType,
                                      ResultSystem = item.ResultSystem,
                                      ExamType = item.ClassType,
                                      ExamDate = examDate
                                  };
                listroutine.Add(routine);
            }
            //var newdata = new ScExamRoutine();
            //newdata.ExamDate = DateTime.UtcNow.Date;
            //listroutine.Add(newdata);

            return PartialView("_PartialExamRoutineDetailEntry", listroutine);
        }

        [HttpPost]
        public ActionResult ExamRoutineAdd(IEnumerable<ScExamRoutine> subjectEntry, AcademicsExamRoutineViewModel model)
        {

            foreach (var item in subjectEntry.Where(x => x.SubjectId != 0))
            {
                var examRoutine = new ScExamRoutine()
                                      {
                                          ClassId = model.ExamRoutine.ClassId,
                                          ExamId = model.ExamRoutine.ExamId,
                                          EvaluateForType = item.EvaluateForType,
                                          ResultSystem = item.ResultSystem,
                                          SubjectId = item.SubjectId,
                                          ExamType = item.ExamType,
                                          StartTime = item.StartTime,
                                         
                                          Remarks = item.Remarks,
                                          ExamHour = item.ExamHour,
                                          EndTime = item.EndTime,
                                          ExamDate = item.ExamDate, 
                                          ExamMiti = item.ExamMiti,
                                          AcademicYearId = CurrentAcademyYearId

                                      };
                _examRoutineRepository.Add(examRoutine);
            }
            return Content("True");
        }

        [HttpPost]
        public ActionResult ExamRoutineEdit(IEnumerable<ScExamRoutine> subjectEntry, AcademicsExamRoutineViewModel model)
        {
            _examRoutineRepository.Delete(x => x.ExamId == model.ExamId && x.ClassId == model.ClassId && x.AcademicYearId == CurrentAcademyYearId);



            foreach (var item in subjectEntry.Where(x => x.SubjectId != 0))
            {
                var examRoutine = new ScExamRoutine()
                                      {
                                          ClassId = model.ExamRoutine.ClassId,
                                          ExamId = model.ExamRoutine.ExamId,
                                          EvaluateForType = item.EvaluateForType,
                                          ResultSystem = item.ResultSystem,
                                          SubjectId = item.SubjectId,
                                          ExamType = item.ExamType,
                                          StartTime = item.StartTime,
                                          ExamMiti = item.ExamMiti,
                                          Remarks = item.Remarks,
                                          ExamHour = item.ExamHour,
                                          EndTime = item.EndTime,
                                          ExamDate = item.ExamDate,
                                          AcademicYearId = model.ExamRoutine.AcademicYearId

                                      };
                _examRoutineRepository.Add(examRoutine);
            }
            return Content("True");
        }

        public ActionResult ExamRoutineDelete(int examId, int classId)
        {
            _examRoutineRepository.Delete(x => x.ExamId == examId && x.ClassId == classId && x.AcademicYearId == CurrentAcademyYearId);

            return RedirectToAction("ExamRoutines");
        }

        #endregion

        #region ExamMarkSetup

        public JsonResult GetExamMarkSetupClasses(int id)
        {
            var data = _examMarkSetupRepository.GetMany(x => x.ExamId == id).Select(x => x.ClassId);
            List<SchClass> lstClasses =
                _scClassRepository.GetAll().OrderBy(x => x.Schedule).ToList();

            var classes = lstClasses.Select(x => new
                                                     {
                                                         Id = x.Id,
                                                         Code = x.Code,
                                                         Description = x.Description
                                                     });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "EMS")]
        public ActionResult ExamMarkSetupList()
        {
            ViewBag.UserRight = base.UserRight("EMS");
            var viewModel = this.BuildAcademicsExamMarkSetupListViewModel();
            return PartialView(viewModel);
        }

        public AcademicsExamMarkSetupViewModel BuildAcademicsExamMarkSetupListViewModel()
        {
            var viewModel = base.CreateViewModel<AcademicsExamMarkSetupViewModel>();
            var lstHouseMappings = from d in _examMarkSetupRepository.GetAll()
                                   group d by new { d.ClassId, d.ExamId }
                                       into g
                                       select g.ToList();
            viewModel.ExamMarkSetupGrouping = lstHouseMappings;
            return viewModel;

        }

        public AcademicsExamMarkSetupViewModel BuildAcademicsExamMarkSetupEditViewModel(int ExamId, int ClassId)
        {
            var data = _examMarkSetupRepository.GetMany(x => x.ClassId == ClassId && x.ExamId == ExamId);
            var exam = _examMarkSetupRepository.GetMany(x => x.ClassId == ClassId && x.ExamId == ExamId).Select(x=>x.SubjectId);
            var list = new List<ScExamMarkSetup>();
            decimal totaltheory = 0;
            decimal totalpractical = 0;
            foreach (var item in data)
            {
                var classes =
                    _classSubjectMappingRepository.GetById(x => x.ClassId == ClassId && x.SubjectId == item.SubjectId);
                var datalist = item;
                if (classes != null) datalist.SubjectType = classes.SubjectType;
                datalist.Total = item.PracticalFullMark + item.TheoryFullMark;
                totaltheory += item.TheoryFullMark;
                totalpractical += item.PracticalFullMark;

                datalist.ClassType = classes.ClassType;
                list.Add(datalist);
            }
         var   classSub = _classSubjectMappingRepository.GetMany(x => x.ClassId == ClassId && !exam.Contains(x.SubjectId));

         


           
            foreach (var item in classSub)
            {
                var routine = new ScExamMarkSetup()
                {
                    SubjectId = item.SubjectId,
                    Subject = item.Subject,
                    EvaluateForType = item.EvaluateForType,
                    ResultSystem = item.ResultSystem,
                    SubjectType = item.SubjectType,
                    ClassType = item.ClassType
                };
                list.Add(routine);
            }
            var viewModel = base.CreateViewModel<AcademicsExamMarkSetupViewModel>();
            viewModel.ExamMarkSetup =
                _examMarkSetupRepository.GetMany(x => x.ClassId == ClassId && x.ExamId == ExamId).FirstOrDefault();
            viewModel.ExamMarkSetups = list;
            viewModel.ExamId = ExamId;
            viewModel.ClassId = ClassId;
            viewModel.TotalTheoryMark = totaltheory;
            viewModel.TotalPracticalMark = totalpractical;
            return viewModel;

        }

        public AcademicsExamMarkSetupViewModel BuildAcademicsExamMarkSetupAddViewModel()
        {
            var viewModel = base.CreateViewModel<AcademicsExamMarkSetupViewModel>();
            viewModel.ExamMarkSetup = new ScExamMarkSetup();
            return viewModel;
        }
        [CheckPermission(Permissions = "Navigate", Module = "EMS")]
        public ActionResult ExamMarkSetups()
        {
            ViewBag.UserRight = base.UserRight("EMS");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "EMS")]
        public ActionResult ExamMarkSetupAdd()
        {
            ViewBag.UserRight = base.UserRight("EMS");
            var data = this.BuildAcademicsExamMarkSetupAddViewModel();
            ViewBag.DateType = base.SystemControl.DateType;
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult ExamMarkSetupAdd(IEnumerable<ScExamMarkSetup> subjectEntry,
                                             AcademicsExamMarkSetupViewModel model)
        {

            foreach (var item in subjectEntry.Where(x => x.SubjectId != 0))
            {
                var examMarkSetup = new ScExamMarkSetup()
                                        {
                                            ClassId = model.ExamMarkSetup.ClassId,
                                            ExamId = model.ExamMarkSetup.ExamId,
                                            EvaluateForType = item.EvaluateForType,
                                            ResultSystem = item.ResultSystem,
                                            SubjectId = item.SubjectId,
                                            Narration = item.Narration,
                                            PracticalFullMark = item.PracticalFullMark,
                                            PracticalPassMark = item.PracticalPassMark,
                                            TheoryFullMark = item.TheoryFullMark,
                                            TheoryPassMark = item.TheoryPassMark,
                                            SubjectType = item.SubjectType,
                                            ClassType = item.ClassType

                                        };
                _examMarkSetupRepository.Add(examMarkSetup);
            }
            return Content("True");
        }

        public ActionResult ExamMarkSetupSubjectByClassId(int classId, int examId)
        {
            IEnumerable<ScClassSubjectMapping> data;
            ViewBag.DateType = base.SystemControl.DateType;
            var exam = _examMarkSetupRepository.GetMany(x => x.ClassId == classId && x.ExamId==examId).Select(x=>x.SubjectId).Distinct();
            data = _classSubjectMappingRepository.GetMany(x => x.ClassId == classId && !exam.Contains(x.SubjectId));
            
            //if(exam.Count() != 0)
            //{
            //     data = _classSubjectMappingRepository.GetMany(x => x.ClassId == classId && ! exam.Contains(x.SubjectId));
            //}
            //else
            //{
            //     data = _classSubjectMappingRepository.GetMany(x => x.ClassId == classId );
            //}
           

            var listroutine = new List<ScExamMarkSetup>();
            foreach (var item in data)
            {
                var routine = new ScExamMarkSetup()
                                  {
                                      SubjectId = item.SubjectId,
                                      Subject = item.Subject,
                                      EvaluateForType = item.EvaluateForType,
                                      ResultSystem = item.ResultSystem,
                                      SubjectType = item.SubjectType,
                                      ClassType = item.ClassType
                                  };
                listroutine.Add(routine);
            }

            return PartialView("_PartialExamMarkSetupDetailEntry", listroutine);
        }

        public ActionResult ExamMarkSetupSubjectByClassIdEdit(int classId)
        {
            ViewBag.DateType = base.SystemControl.DateType;
            var data = _classSubjectMappingRepository.GetMany(x => x.ClassId == classId);
            List<ScExamMarkSetup> listroutine = new List<ScExamMarkSetup>();
            foreach (var item in data)
            {
                var routine = new ScExamMarkSetup()
                                  {
                                      SubjectId = item.SubjectId,
                                      Subject = item.Subject,
                                      EvaluateForType = item.EvaluateForType,
                                      ResultSystem = item.ResultSystem,
                                      SubjectType = item.SubjectType,
                                      ClassType = item.ClassType
                                  };
                listroutine.Add(routine);
            }

            return PartialView("_PartialExamMarkSetupDetailEntry", listroutine);
        }


        [CheckPermission(Permissions = "Edit", Module = "EMS")]
        public ActionResult ExamMarkSetupEdit(int ExamId, int ClassId)
        {

            var data = this.BuildAcademicsExamMarkSetupEditViewModel(ExamId, ClassId);
            ViewBag.DateType = base.SystemControl.DateType;
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult ExamMarkSetupEdit(IEnumerable<ScExamMarkSetup> subjectEntry,
                                              AcademicsExamMarkSetupViewModel model)
        {
            _examMarkSetupRepository.Delete(x => x.ExamId == model.ExamId && x.ClassId == model.ClassId);

            foreach (var item in subjectEntry.Where(x => x.SubjectId != 0))
            {
                var examMarkSetup = new ScExamMarkSetup()
                                        {
                                            ClassId = model.ExamMarkSetup.ClassId,
                                            ExamId = model.ExamMarkSetup.ExamId,
                                            EvaluateForType = item.EvaluateForType,
                                            ResultSystem = item.ResultSystem,
                                            SubjectId = item.SubjectId,
                                            Narration = item.Narration,
                                            PracticalFullMark = item.PracticalFullMark,
                                            PracticalPassMark = item.PracticalPassMark,
                                            TheoryFullMark = item.TheoryFullMark,
                                            TheoryPassMark = item.TheoryPassMark,
                                            SubjectType = item.SubjectType,
                                            ClassType = item.ClassType
                                        };
                _examMarkSetupRepository.Add(examMarkSetup);
            }
            return Content("True");
        }

        #endregion

        #region SubjectWiseMarkss

        public JsonResult GetExamAttendanceStatus()
        {
            var data = new SelectList(
                Enum.GetValues(typeof(ExamAttendanceStatus)).Cast
                    <ExamAttendanceStatus>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            var classes = data.Select(x => new
                                               {
                                                   Id = x.Value,

                                                   Description = x.Text
                                               });
            return Json(classes, JsonRequestBehavior.AllowGet);

        }



        public ActionResult GetSubjectWiseMarkss(int classId, int examId, int subjectId)
        {

            var subject =_scexammarksentryRepository.GetMany(i => i.ClassId == classId && i.ExamId == examId && i.SubjectId == subjectId && i.AcademicYearId == CurrentAcademyYearId);

                var subj = _classSubjectMappingRepository.GetById(x => x.SubjectId == subjectId && x.ClassId == classId);
                var data = UtilityService.GetSubjectWiseMarksDetail(examId, classId, subjectId, subj.SubjectType);
            
                return PartialView("_PartialSubjectWiseMarksDetailEntry", data);
            
          


        }

        public JsonResult GetSubjectByExamClasId(int id, int nextid)
        {
            var data = _scexammarksentryRepository.GetMany(x => x.ExamId == id && x.ClassId == nextid && x.AcademicYearId == CurrentAcademyYearId).Select(x => x.SubjectId);
            var lstClasses =
                 _classSubjectMappingRepository.GetMany(x => !data.Contains(x.Id) && x.ClassId == nextid).OrderBy(x => x.Schedule).ToList();

            var classes = lstClasses.Select(x => new
             {
                 Id = x.Subject.Id,
                 Code = x.Subject.Code,
                 Description = x.Subject.Description
             });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSWS")]
        public ActionResult SubjectWiseMarks()
        {
            ViewBag.UserRight = base.UserRight("ScSWS");
            if (!_divisionRepository.GetAll().Any())
            {
                //hasError = true;
                ViewBag.ErrorMsg = "You havent added Division Master yet. In order to add subject wise marks, you need to add Division master first.";
                ViewBag.Link = " <a href='/School/Division' target='_blank'>Click Here To Add Division Master</a>";
                return View("_Error");
            }
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSWS")]
        public ActionResult SubjectWiseMarksList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScSWS");
            var lstHouseMappings = from d in _scexammarksentryRepository.GetMany(i => i.AcademicYearId == CurrentAcademyYearId)
                                   group d by new { d.ClassId, d.ExamId, d.SubjectId }
                                       into g
                                       select g.ToList();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialSubjectWiseMarksList", lstHouseMappings.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
        [CheckPermission(Permissions = "Create", Module = "ScSWS")]
        public ActionResult SubjectWiseMarksAdd()
        {
            var model = new ScExamMarksEntry();
            return PartialView("_PartialSubjectWiseMarksAdd", model);
        }

        [HttpPost]
        public ActionResult SubjectWiseMarksAdd(ScExamMarksEntry model, IEnumerable<Sp_SubjectWiseMarksDetail> SubjectWiseMarksDetailctEntry)
        {
            var user = _authentication.GetAuthenticatedUser();
          //  _scexammarksentryRepository.Delete(x => x.ClassId == model.ClassId && x.ExamId == model.ExamId && x.SubjectId == model.SubjectId && x.AcademicYearId == CurrentAcademyYearId);
            foreach (var item in SubjectWiseMarksDetailctEntry.Where(x => x.StudentID != 0))
            {
                var data = new ScExamMarksEntry()
                               {

                                   Narration = item.Narration,
                                   PracticalObtainedMarks = item.PracticalObtainedMarks,
                                   PracticalStatus = item.PracticalStatus,
                                   TheoryObtainedMarks = item.TheoryObtainedMarks,
                                   TheoryStatus = item.TheoryStatus,
                                   StudentId = item.StudentID,
                                   Total = item.Total,
                                   ClassId = model.ClassId,
                                   CreatedById = user.Id,
                                   CreatedDate = DateTime.UtcNow,
                                   UpdatedById = user.Id,
                                   UpdatedDate = DateTime.UtcNow,
                                   ExamId = model.ExamId,
                                   Remarks = model.Remarks,
                                   SubjectId = model.SubjectId,
                                   AcademicYearId = CurrentAcademyYearId

                               };
                _scexammarksentryRepository.Add(data);
            }
            var _context = new DataContext();
            _context.Database.ExecuteSqlCommand("Exec SP_UpdateMarksInfo @ClassId=" + model.ClassId + ",@ExamId=" + model.ExamId +
                                                ",@AcademicYearId=" + this.AcademicYear.Id);

            UtilityService.GenerateRank(model.ExamId, model.ClassId, CurrentAcademyYearId);
            return Content("True");


        }
        [CheckPermission(Permissions = "Edit", Module = "ScSWS")]
        public ActionResult SubjectWiseMarksEdit(int classId, int examId, int subjectId)
        {

            var data = _scexammarksentryRepository.GetById(x => x.ClassId == classId && x.ExamId == examId && x.SubjectId == subjectId && x.AcademicYearId == CurrentAcademyYearId);
            data.oldExamId = examId;
            data.oldClassId = classId;
            data.oldSubjectId = subjectId;
           
            data.SubjectWiseMarksDetailses = UtilityService.GetSubjectWiseMarksDetailEdit(classId, examId, subjectId);
           


            return PartialView("_PartialSubjectWiseMarksEdit", data);
        }

        [HttpPost]
        public ActionResult SubjectWiseMarksEdit(ScExamMarksEntry model, IEnumerable<Sp_SubjectWiseMarksDetail> SubjectWiseMarksDetailctEntry)
        {
            var user = _authentication.GetAuthenticatedUser();
            _scexammarksentryRepository.Delete(x => x.ClassId == model.oldClassId && x.ExamId == model.oldExamId && x.SubjectId == model.oldSubjectId && x.AcademicYearId == CurrentAcademyYearId);
            foreach (var item in SubjectWiseMarksDetailctEntry.Where(x => x.StudentID != 0))
            {
                var data = new ScExamMarksEntry()
                {
                    Narration = item.Narration,
                    PracticalObtainedMarks = item.PracticalObtainedMarks,
                    PracticalStatus = item.PracticalStatus,
                    TheoryObtainedMarks = item.TheoryObtainedMarks,
                    TheoryStatus = item.TheoryStatus,
                    StudentId = item.StudentID,
                    Total = item.Total,
                    ClassId = model.ClassId,
                    CreatedById = model.CreatedById,
                    CreatedDate = model.CreatedDate,
                    UpdatedById = user.Id,
                    UpdatedDate = DateTime.UtcNow,
                    ExamId = model.ExamId,
                    Remarks = model.Remarks,
                    SubjectId = model.SubjectId,
                    AcademicYearId = CurrentAcademyYearId
                };
                _scexammarksentryRepository.Add(data);
            }
            var _context = new DataContext();
            _context.Database.ExecuteSqlCommand("Exec SP_UpdateMarksInfo @ClassId=" + model.ClassId + ",@ExamId=" + model.ExamId +
                                     ",@AcademicYearId=" + this.AcademicYear.Id);

            UtilityService.GenerateRank(model.ExamId, model.ClassId, CurrentAcademyYearId);
            return Content("True");

        }




        #endregion

        #region student Wise Marks Setup

       

        public ActionResult GetStudentWiseMarkss(int classId, int examId, int studentId)
        {

            var subject =
                _scexammarksentryRepository.GetById(
                    i => i.ClassId == classId && i.ExamId == examId && i.StudentId == studentId && i.AcademicYearId == CurrentAcademyYearId);

            if (subject == null)
            {
                var data = UtilityService.GetStudentWiseMarksDetail(examId, classId, studentId);
                return PartialView("_PartialStudentWiseMarksDetailEntry", data);
            }

            else
            {
                return Content("ExistData");
            }



        }

        public JsonResult GetStudentByClasId(int id, int nextid)
        {
            var list = _scexammarksentryRepository.GetMany(x => x.ClassId == id && x.ExamId == nextid && x.AcademicYearId == CurrentAcademyYearId).Select(x => x.StudentId);
            var data = _scStudentRegistrationDetailRepository.GetMany(x => !list.Contains(x.StudentId) && x.StudentRegistration.ClassId == id).Select(x => new
                                                                                             {
                                                                                                 Id = x.StudentId,
                                                                                                 Description = x.Studentinfo.StuDesc,
                                                                                                 Code = x.Studentinfo.StdCode,
                                                                                             });



            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [CheckPermission(Permissions = "Navigate", Module = "ScSWSs")]
        public ActionResult StudentWiseMarks()
        {
            ViewBag.UserRight = base.UserRight("ScSWSs");
            if (!_divisionRepository.GetAll().Any())
            {
                //hasError = true;
                ViewBag.ErrorMsg = "You havent added Division Master yet. In order to add subject wise marks, you need to add Division master first.";
                ViewBag.Link = " <a href='/School/Division' target='_blank'>Click Here To Add Division Master</a>";
                return View("_Error");
            }

            return View();
        }

        [CheckPermission(Permissions = "Navigate", Module = "ScSWSs")]
        public ActionResult StudentWiseMarksList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScSWSs");
            var lstHouseMappings = from d in _scexammarksentryRepository.GetMany(i => i.AcademicYearId == CurrentAcademyYearId)
                                   group d by new { d.ClassId, d.ExamId, d.StudentId }
                                       into g
                                       select g.ToList();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialStudentWiseMarksList", lstHouseMappings.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        [CheckPermission(Permissions = "Create", Module = "ScSWSs")]
        public ActionResult StudentWiseMarksAdd()
        {
            var model = new ScExamMarksEntry();
            return PartialView("_PartialStudentWiseMarksAdd", model);
        }
        [HttpPost]
        public ActionResult StudentWiseMarksAdd(ScExamMarksEntry model, IEnumerable<SP_StudentWiseMarksDetail> SubjectWiseMarksDetailctEntry)
        {
            var user = _authentication.GetAuthenticatedUser();

            _scexammarksentryRepository.Delete(x => x.StudentId == model.StudentId && x.ClassId == model.ClassId && x.ExamId == model.ExamId && x.AcademicYearId == CurrentAcademyYearId);
            foreach (var item in SubjectWiseMarksDetailctEntry.Where(x => x.SubjectId != 0))
            {
                var data = new ScExamMarksEntry()
                {

                    Narration = item.Narration,
                    PracticalObtainedMarks = item.PracticalObtainedMarks,
                    PracticalStatus = item.PracticalStatus,
                    TheoryObtainedMarks = item.TheoryObtainedMarks,
                    TheoryStatus = item.TheoryStatus,
                    StudentId = model.StudentId,
                    Total = item.Total,
                    ClassId = model.ClassId,
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedById = user.Id,
                    UpdatedDate = DateTime.UtcNow,
                    ExamId = model.ExamId,
                    Remarks = model.Remarks,
                    SubjectId = item.SubjectId,
                    DivisionId = model.DivisionId,
                    Percentage = model.Percentage,
                    Result = model.Result,
                    TotalFullMarks = model.TotalFullMarks,
                    TotalObtainedMarks = model.TotalObtainedMarks,
                    AcademicYearId = CurrentAcademyYearId


                };
                _scexammarksentryRepository.Add(data);

            }

            UtilityService.GenerateRank(model.ExamId, model.ClassId, CurrentAcademyYearId);
            return Content("True");

        }

        [CheckPermission(Permissions = "Edit", Module = "ScSWSs")]
        public ActionResult StudentWiseMarksEdit(int classId, int examId, int studentId)
        {

            var data = _scexammarksentryRepository.GetById(x => x.ClassId == classId && x.ExamId == examId && x.StudentId == studentId && x.AcademicYearId == CurrentAcademyYearId);
            data.oldExamId = examId;
            data.oldClassId = classId;
            data.oldStudentId = studentId;
            data.StudentWiseMarksDetailses = UtilityService.GetStudentWiseMarksDetailEdit(examId, classId, studentId);

            return PartialView("_PartialStudentWiseMarksEdit", data);
        }
        [HttpPost]
        public ActionResult StudentWiseMarksEdit(ScExamMarksEntry model, IEnumerable<SP_StudentWiseMarksDetail> SubjectWiseMarksDetailctEntry)
        {
            var user = _authentication.GetAuthenticatedUser();
            _scexammarksentryRepository.Delete(x => x.StudentId == model.oldStudentId && x.ExamId == model.oldExamId && x.ClassId == model.oldClassId && x.AcademicYearId == CurrentAcademyYearId);
            foreach (var item in SubjectWiseMarksDetailctEntry.Where(x => x.SubjectId != 0))
            {
                var data = new ScExamMarksEntry()
                {

                    Narration = item.Narration,
                    PracticalObtainedMarks = item.PracticalObtainedMarks,
                    PracticalStatus = item.PracticalStatus,
                    TheoryObtainedMarks = item.TheoryObtainedMarks,
                    TheoryStatus = item.TheoryStatus,
                    StudentId = model.StudentId,
                    Total = item.Total,
                    ClassId = model.ClassId,
                    CreatedById = model.CreatedById,
                    CreatedDate = model.CreatedDate,
                    UpdatedById = user.Id,
                    UpdatedDate = DateTime.UtcNow,
                    ExamId = model.ExamId,
                    Remarks = model.Remarks,
                    SubjectId = item.SubjectId,
                    DivisionId = model.DivisionId,
                    Percentage = model.Percentage,
                    Result = model.Result,
                    TotalFullMarks = model.TotalFullMarks,
                    TotalObtainedMarks = model.TotalObtainedMarks,
                    AcademicYearId = CurrentAcademyYearId


                };
                _scexammarksentryRepository.Add(data);


            }
            //var _context = new DataContext();
            //_context.Database.SqlQuery<string>("Sp_UpateRank @ExamId=" + model.ExamId + ",@ClassId=" + model.ClassId + ",@AcademicYearId=" + CurrentAcademyYearId).;

            UtilityService.GenerateRank(model.ExamId, model.ClassId, CurrentAcademyYearId);
            return Content("True");

        }


        #endregion






        #region Exam Attendance
        [CheckPermission(Permissions = "Navigate", Module = "Sc EA")]
        public ActionResult ExamAttendances()
        {
            ViewBag.UserRight = base.UserRight("Sc EA");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "Sc EA")]
        public ActionResult ExamAttendanceList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("Sc EA");
            var data = _scexamattendancemasterRepository.GetPagedList(x => x.AcademicYearId , pageNo, this.SystemControl.PageSize);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialExamAttendanceList", data);
        }
        [CheckPermission(Permissions = "Create", Module = "Sc EA")]
        public ActionResult ExamAttendanceAdd()
        {
            var data = new ScExamAttendanceMaster();
            // data.date = DateTime.UtcNow.ToString("MM/dd/yyyy");
            data.EntryDate = DateTime.UtcNow;

            return PartialView("_PartialExamAttendanceAdd", data);
        }
        [HttpPost]
        public ActionResult ExamAttendanceAdd(IEnumerable<ScExamAttendanceDetail> subjectEntry, ScExamAttendanceMaster model)
        {
            var user = _authentication.GetAuthenticatedUser();
            model.CreatedById = user.Id;
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedById = user.Id;
            model.UpdatedDate = DateTime.UtcNow;
            model.AcademicYearId = CurrentAcademyYearId;
            _scexamattendancemasterRepository.Add(model);

            foreach (var item in subjectEntry.Where(x => x.StudentId != 0))
            {
                var data = new ScExamAttendanceDetail()
                {
                    MasterId = model.Id,
                    Narration = item.Narration,
                    PresentDays = item.PresentDays,
                    StudentId = item.StudentId,
                    AbsentDays = item.AbsentDays
                 

                };
                _scexamattendancedetailRepository.Add(data);
            }
            return Content("True");
        }
        [CheckPermission(Permissions = "Edit", Module = "Sc EA")]
        public ActionResult ExamAttendanceEdit(int id)
        {
            var data = _scexamattendancemasterRepository.GetById(x => x.Id == id && x.AcademicYearId == CurrentAcademyYearId);
            data.AttendanceDetails = _scexamattendancedetailRepository.GetMany(x => x.MasterId == id);
            return PartialView("_PartialExamAttendanceEdit", data);
        }
        [HttpPost]
        public ActionResult ExamAttendanceEdit(IEnumerable<ScExamAttendanceDetail> subjectEntry, ScExamAttendanceMaster model)
        {
            var user = _authentication.GetAuthenticatedUser();
            model.UpdatedById = user.Id;
            model.UpdatedDate = DateTime.UtcNow;
            _scexamattendancemasterRepository.Update(model);
            _scexamattendancedetailRepository.Delete(x => x.MasterId == model.Id);
            foreach (var item in subjectEntry.Where(x => x.StudentId != 0))
            {
                var data = new ScExamAttendanceDetail()
                {
                    MasterId = model.Id,
                    Narration = item.Narration,
                    PresentDays = item.PresentDays,
                    StudentId = item.StudentId,
                    AbsentDays = item.AbsentDays
                };
                _scexamattendancedetailRepository.Add(data);
            }
            return Content("True");
        }
        public ActionResult ExamAttendanceDetail(int SectionId, int ClassId, int ExamId, int Id)
        {
            if (Id != 0)
            {
                var i = _scexamattendancemasterRepository.GetById(x => x.Id == Id && x.AcademicYearId == CurrentAcademyYearId);
                if (i.SectionId == SectionId && i.SectionId == SectionId && i.ExamId == ExamId)
                {
                    return Content("ExistData");
                }
            }



            var housemapping =
                _scexamattendancemasterRepository.GetById(
                    x => x.ClassId == ClassId && x.SectionId == SectionId && x.ExamId == ExamId && x.AcademicYearId == CurrentAcademyYearId);
            if (housemapping == null)
            {
                var data =
                    _scStudentRegistrationDetailRepository.GetMany(
                        x => x.SectionId == SectionId && x.StudentRegistration.ClassId == ClassId);

                var list = new List<ScExamAttendanceDetail>();
                foreach (var item in data)
                {
                    if (item.Studentinfo != null)
                    {
                        var model = new ScExamAttendanceDetail()
                        {
                            StudentId = item.StudentId,
                            Studentinfo = item.Studentinfo,
                            MasterId = 0,
                            StdCode = item.Studentinfo.StdCode,
                            StdRollNo = item.RollNo
                        };
                        list.Add(model);
                    }
                }

                return PartialView("_PartialExamAttendanceDetailEntry", list);

            }
            else
            {
                return Content("false");
            }
        }

        #endregion
        #region ConsolidatedMarksSetups
        [CheckPermission(Permissions = "Navigate", Module = "ScCMS")]
        public ActionResult ConsolidatedMarksSetups()
        {
            ViewBag.UserRight = base.UserRight("ScCMS");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScCMS")]
        public ActionResult ConsolidatedMarksSetupAdd()
        {
            var data = new AcademicsConsolidatedMarksSetupsViewModel();
            data.ConsolidatedMarksSetup = new ScConsolidatedMarksSetup();
            return PartialView("_PartialConsolidatedMarksSetupAdd", data);
        }

        [HttpPost]
        public ActionResult ConsolidatedMarksSetupAdd(AcademicsConsolidatedMarksSetupsViewModel model, IEnumerable<ScConsolidatedMarksSetup> ConsolidatedEntry)
        {
            foreach (var item in ConsolidatedEntry.Where(x => x.ExamGrdId != 0))
            {
                var data = new ScConsolidatedMarksSetup
                {
                    ClassId = model.ConsolidatedMarksSetup.ClassId,
                    ExamId = model.ConsolidatedMarksSetup.ExamId,
                    Code = model.ConsolidatedMarksSetup.Code,
                    Description = model.ConsolidatedMarksSetup.Description,
                    ExamGrdId = item.ExamGrdId,
                    ExamOrder = item.ExamOrder,
                    Percentage = item.Percentage,
                    Remarks = model.ConsolidatedMarksSetup.Remarks

                };
                _scconsolidatedmarkssetupRepository.Add(data);

            } return Content("True");
        }
        [CheckPermission(Permissions = "Edit", Module = "ScCMS")]
        public ActionResult ConsolidatedMarksSetupEdit(int classId, int examId)
        {
            var data = _scconsolidatedmarkssetupRepository.GetMany(x => x.ClassId == classId && x.ExamId == examId);
            var viewModel = new AcademicsConsolidatedMarksSetupsViewModel();
            viewModel.Classid = classId;
            viewModel.ExamId = examId;
            viewModel.ConsolidatedMarksSetup = data.FirstOrDefault();
            viewModel.ConsolidatedMarksSetups = data.OrderBy(x => x.ExamOrder);
            return PartialView("_PartialConsolidatedMarksSetupEdit", viewModel);
        }

        [HttpPost]
        public ActionResult ConsolidatedMarksSetupEdit(AcademicsConsolidatedMarksSetupsViewModel model, IEnumerable<ScConsolidatedMarksSetup> ConsolidatedEntry)
        {
            _scconsolidatedmarkssetupRepository.Delete(
                x => x.ClassId == model.Classid && x.ExamId == model.ExamId);
            foreach (var item in ConsolidatedEntry.Where(x => x.ExamGrdId != 0))
            {
                var data = new ScConsolidatedMarksSetup
                {
                    ClassId = model.ConsolidatedMarksSetup.ClassId,
                    ExamId = model.ConsolidatedMarksSetup.ExamId,
                    Code = model.ConsolidatedMarksSetup.Code,
                    Description = model.ConsolidatedMarksSetup.Description,
                    ExamGrdId = item.ExamGrdId,
                    ExamOrder = item.ExamOrder,
                    Percentage = item.Percentage,
                    Remarks = model.ConsolidatedMarksSetup.Remarks

                };
                _scconsolidatedmarkssetupRepository.Add(data);

            } return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScCMS")]
        public ActionResult ConsolidatedMarksSetupsList()
        {
            ViewBag.UserRight = base.UserRight("ScCMS");
            var data = new AcademicsConsolidatedMarksSetupsViewModel();
            var lstHouseMappings = from d in _scconsolidatedmarkssetupRepository.GetAll()
                                   group d by new { d.ClassId, d.ExamId }
                                       into g
                                       select g.ToList();
            data.ConsolidatedMarksSetupGrouping = lstHouseMappings;
            return PartialView("_PartialConsolidatedMarksSetupList", data.ConsolidatedMarksSetupGrouping);
        }
        [CheckPermission(Permissions = "Delete", Module = "ScCMS")]
        public ActionResult ConsolidatedMarksSetupDelete(int consolidatedmarkssetupId)
        {
            ScConsolidatedMarksSetup objScConsolidatedMarksSetup = _scconsolidatedmarkssetupRepository.GetById(x => x.Id == consolidatedmarkssetupId);
            _scconsolidatedmarkssetupRepository.Delete(objScConsolidatedMarksSetup);
            return RedirectToAction("ConsolidatedMarksSetups");
        }
        public ActionResult ConsolidatedMarksSetupDetail()
        {
            var data = new ScConsolidatedMarksSetup();
            return PartialView("_PartialConsolidatedMarksSetupDetailEdit", data);
        }
        #endregion

        #region BoardExamMaster

        public ActionResult BoardExamMaster()
        {
            return View();
        }

        public ActionResult BoardExamMasterList(int pageNo=1)
        {
            var list = _boardExamMasterRepository.GetAll();
            return PartialView("_PartialBoardExamMasterList",list);
        }

        public ActionResult BoardExamMasterAdd()
        {
            List<ScStudentinfo> lstStudent = _scStudentRegistrationDetailRepository.GetAll().Select(x => x.Studentinfo).ToList();

            var data = lstStudent.Select(x => new
            {
                Id = x.StudentID,
                RegCode = x.StdCode,
                Description = x.StuDesc,
                Class = x.Class.Description,

            });
            var model = new BoardExamViewModel();
            model.StudentList = new SelectList(data,"Id","Description").ToList();
            model.StudentList.Insert(0, new SelectListItem { Value = "0", Text = "None" });

            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.CharacterDisplayDate))
                {
                    model.BoardExamMaster.CharacterCertificateIssueDate = Convert.ToDateTime(model.CharacterDisplayDate);
                    model.BoardExamMaster.CharacterCertificateIssueMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.CharacterDisplayDate)).Date;
                }
                if (!string.IsNullOrEmpty(model.TranscriptDisplayDate))
                {
                    model.BoardExamMaster.CharacterCertificateIssueDate = Convert.ToDateTime(model.TranscriptDisplayDate);
                    model.BoardExamMaster.TranscriptIssueMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.TranscriptDisplayDate)).Date;
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(model.CharacterDisplayDate))
                {
                    model.BoardExamMaster.CharacterCertificateIssueMiti = model.CharacterDisplayDate;
                    model.BoardExamMaster.CharacterCertificateIssueDate =
                        NepaliDateService.NepalitoEnglishDate(model.BoardExamMaster.CharacterCertificateIssueMiti);
                }
                if (!string.IsNullOrEmpty(model.TranscriptDisplayDate))
                {
                    model.BoardExamMaster.TranscriptIssueMiti = model.TranscriptDisplayDate;
                    model.BoardExamMaster.TranscriptIssueDate =
                        NepaliDateService.NepalitoEnglishDate(model.BoardExamMaster.TranscriptIssueMiti);
                }
            }
            model.SystemControl = _systemControl.GetAll().FirstOrDefault();
            model.ProgramList=new SelectList(_programMasterRepository.GetAll(),"Id","Description").ToList();
            model.ProgramList.Insert(0, new SelectListItem { Value = "0", Text = "None" });
            model.DivisionList = new SelectList(_scDivisionRepository.GetAll(), "Id", "Description").ToList();
            model.DivisionList.Insert(0, new SelectListItem { Value = "0", Text = "Select Division" });
           
            return PartialView("_PartialBoardExamMasterAdd",model);
        }

        [HttpPost]
        public ActionResult BoardExamMasterAdd(BoardExamViewModel viewmodel, IEnumerable<BoardExamDetail> BoardExamDetailList, IEnumerable<ClassPercentageViewModel> ClassIdPercentageList)
        {
           
           
                      if (base.SystemControl.DateType == 1)
                {
                    if (!string.IsNullOrEmpty(viewmodel.CharacterDisplayDate))
                    {
                        viewmodel.BoardExamMaster.CharacterCertificateIssueDate = Convert.ToDateTime(viewmodel.CharacterDisplayDate);
                        viewmodel.BoardExamMaster.CharacterCertificateIssueMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(viewmodel.CharacterDisplayDate)).Date;
                    }
                    if (!string.IsNullOrEmpty(viewmodel.TranscriptDisplayDate))
                    {
                         viewmodel.BoardExamMaster.TranscriptIssueDate= Convert.ToDateTime(viewmodel.TranscriptDisplayDate);
                         viewmodel.BoardExamMaster.TranscriptIssueMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(viewmodel.TranscriptDisplayDate)).Date;
                    }
                 
                }
                else
                {
                    if (!string.IsNullOrEmpty(viewmodel.CharacterDisplayDate))
                    {
                         viewmodel.BoardExamMaster.CharacterCertificateIssueMiti = viewmodel.CharacterDisplayDate;
                         viewmodel.BoardExamMaster.CharacterCertificateIssueDate =
                            NepaliDateService.NepalitoEnglishDate( viewmodel.BoardExamMaster.CharacterCertificateIssueMiti);
                    }
                            if (!string.IsNullOrEmpty(viewmodel.TranscriptDisplayDate))
                    {
                         viewmodel.BoardExamMaster.TranscriptIssueMiti = viewmodel.TranscriptDisplayDate;
                         viewmodel.BoardExamMaster.TranscriptIssueDate =
                            NepaliDateService.NepalitoEnglishDate( viewmodel.BoardExamMaster.TranscriptIssueMiti);
                    }
                }

           _boardExamMasterRepository.Add(viewmodel.BoardExamMaster);
   


            foreach (var item in ClassIdPercentageList)
            {
              
                foreach (var boardExamDetail in BoardExamDetailList)
                {
                    if (item.ClassId == boardExamDetail.ClassId)
                    {
                           var detail = new BoardExamDetail()
                    {
                        BoardExamMasterId = viewmodel.BoardExamMaster.Id,
                        SubjectId = boardExamDetail.SubjectId,
                        ClassId = boardExamDetail.ClassId,

                        MaskObtained_Y1 = boardExamDetail.MaskObtained_Y1,
                        MaskObtained_Y2 = boardExamDetail.MaskObtained_Y2,
                        MaskObtained_Y3 = boardExamDetail.MaskObtained_Y3,
                        MaskObtained_Y4 = boardExamDetail.MaskObtained_Y4,
                        MaskObtained_Y5 = boardExamDetail.MaskObtained_Y5,
                        MaskObtained_Y6 = boardExamDetail.MaskObtained_Y6,
                        MaskObtained_Y7 = boardExamDetail.MaskObtained_Y7,
                        Result_Y1 = boardExamDetail.Result_Y1,
                        Result_Y2 = boardExamDetail.Result_Y2,
                        Result_Y3 = boardExamDetail.Result_Y3,
                        Result_Y4 = boardExamDetail.Result_Y4,
                        Result_Y5 = boardExamDetail.Result_Y5,
                        Result_Y6 = boardExamDetail.Result_Y6,
                        Result_Y7 = boardExamDetail.Result_Y7,
                        SymbolNo_Y1 = boardExamDetail.SymbolNo_Y1,
                        SymbolNo_Y2 = boardExamDetail.SymbolNo_Y2,
                        SymbolNo_Y3 = boardExamDetail.SymbolNo_Y3,
                        SymbolNo_Y4 = boardExamDetail.SymbolNo_Y4,
                        SymbolNo_Y5 = boardExamDetail.SymbolNo_Y5,
                        SymbolNo_Y6 = boardExamDetail.SymbolNo_Y6,
                        SymbolNo_Y7 = boardExamDetail.SymbolNo_Y7,
                        PassPercentage = item.Percentage,
                        Remarks = boardExamDetail.Remarks,

                    };
                      
                        _boardExamDetailRepository.Add(detail);
                    }
                 
            }
         
                
            }
            return Content("True");
       
        }
        public ActionResult GetSubjectByStudentIdAndClassId(int studentid,int programid)
        {
            var studentprogramcheck =
                _scClassRepository.GetMany(x => x.ProgramId == programid).Select(x => new ScStudentinfo
                {
                    ClassId = x.Id
                });
            DataContext context = new DataContext();
            var data = _scClassRepository.GetMany(x => x.ProgramId == programid);
            var classid = "";
            foreach (var item in data)
            {
                if (classid == "")
                {
                    classid = item.Id.ToString();
                }
                else
                {
                    classid += "," + item.Id;
                }
            }

         
            var program = _programMasterRepository.GetById(x => x.Id == programid);
            var viewModel = new BoardExamViewModel();
            viewModel.Level = program.LevelDescription;
            viewModel.ProgramName = program.Description;
            viewModel.Durationtype = Enum.GetName(typeof (DurationType), program.DurationId);
            viewModel.AcademicYear = _academicYearRepository.GetById(x => x.IsDefalut == true).StartDate;
            viewModel.RegId=_scStudentinfoRepository.GetById(x=>x.StudentID==studentid).Regno;
            viewModel.BoardExamDetail = context.Database.SqlQuery<GetSubjectByStudentIdandClassId>("EXEC Sp_GetSubjectByClassIdandStudentId @studentId=" + studentid + ",@classId='" + classid + "'").Select(x => new BoardExamDetail
            {
                ClassId = x.ClassId,
                ClassName = x.ClassName,
                SubjectId = x.SubjectId,
                SubjectName = x.SubjectName
            });
            var view = RenderPartialViewToString("_PartialBoardExamMasterDetial", viewModel);
            return Json(view,JsonRequestBehavior.AllowGet);
        }

        public ActionResult BoardExamMasterEdit(int boardexammasterid)
        {
            var viewmodel = new BoardExamViewModel();
           viewmodel.BoardExamMaster = _boardExamMasterRepository.GetById(x => x.Id == boardexammasterid);
           viewmodel.BoardExamDetail = _boardExamDetailRepository.GetMany(x => x.BoardExamMasterId == boardexammasterid);
            
            List<ScStudentinfo> lstStudent = _scStudentRegistrationDetailRepository.GetAll().Select(x => x.Studentinfo).ToList();

            var data = lstStudent.Select(x => new
            {
                Id = x.StudentID,
                RegCode = x.StdCode,
                Description = x.StuDesc,
                Class = x.Class.Description,

            });

            viewmodel.StudentList = new SelectList(data, "Id", "Description").ToList();
            viewmodel.StudentList.Insert(0, new SelectListItem { Value = "0", Text = "None" });
            viewmodel.ProgramList = new SelectList(_programMasterRepository.GetAll(), "Id", "Description").ToList();
            viewmodel.ProgramList.Insert(0, new SelectListItem { Value = "0", Text = "None" });
            viewmodel.DivisionList = new SelectList(_scDivisionRepository.GetAll(), "Id", "Description").ToList();
            viewmodel.DivisionList.Insert(0, new SelectListItem { Value = "0", Text = "Select Division" });
            if (base.SystemControl.DateType == 1)
            {

                if (viewmodel.BoardExamMaster.CharacterCertificateIssueDate != null)
                {
                    viewmodel.CharacterDisplayDate = Convert.ToDateTime(viewmodel.BoardExamMaster.CharacterCertificateIssueDate).ToString("MM/dd/yyyy");

                }
                if (viewmodel.BoardExamMaster.TranscriptIssueDate != null)
                {
                    viewmodel.TranscriptDisplayDate = Convert.ToDateTime(viewmodel.BoardExamMaster.TranscriptIssueDate).ToString("MM/dd/yyyy");

                }

               
            }
            else
            {
                if (!string.IsNullOrEmpty(viewmodel.BoardExamMaster.CharacterCertificateIssueMiti))
                {
                    viewmodel.CharacterDisplayDate = viewmodel.BoardExamMaster.CharacterCertificateIssueMiti;
                }

                if (!string.IsNullOrEmpty(viewmodel.BoardExamMaster.TranscriptIssueMiti))
                {
                    viewmodel.TranscriptDisplayDate = viewmodel.BoardExamMaster.TranscriptIssueMiti;
                }
             
            }
            viewmodel.SystemControl = _systemControl.GetAll().FirstOrDefault();
            DataContext context = new DataContext();
          


            var program = _programMasterRepository.GetById(x => x.Id == viewmodel.BoardExamMaster.ProgramId);

            //var data = _scClassRepository.GetMany(x => x.ProgramId == program.Id);
            //var classid = "";
            //foreach (var item in data)
            //{
            //    if (classid == "")
            //    {
            //        classid = item.Id.ToString();
            //    }
            //    else
            //    {
            //        classid += "," + item.Id;
            //    }
            //}
           
            viewmodel.Level = program.LevelDescription;
            viewmodel.ProgramName = program.Description;
            viewmodel.Durationtype = Enum.GetName(typeof(DurationType), program.DurationId);
            viewmodel.AcademicYear = _academicYearRepository.GetById(x => x.IsDefalut == true).StartDate;
            viewmodel.RegId = _scStudentinfoRepository.GetById(x => x.StudentID == viewmodel.BoardExamMaster.StudentId).Regno;

            viewmodel.BoardExamDetail = context.Database.SqlQuery<Sp_GetSubjectByClassIdandStudentIdResult>("Sp_GetSubjectByClassIdandStudentIdResult @ProgramId=" + viewmodel.BoardExamMaster.ProgramId + ",@studentId=" + viewmodel.BoardExamMaster.StudentId).Select(x => new BoardExamDetail
                 {
                     ClassId = x.ClassId,
                     ClassName = x.ClassName,
                     SubjectId = x.SubjectId,
                     SubjectName = x.SubjectName,
                     MaskObtained_Y1 = Convert.ToDecimal(x.MaskObtained_Y1),
                     MaskObtained_Y2 = Convert.ToDecimal(x.MaskObtained_Y2),
                     MaskObtained_Y3 = Convert.ToDecimal(x.MaskObtained_Y3),
                     MaskObtained_Y4 = Convert.ToDecimal(x.MaskObtained_Y4),
                     MaskObtained_Y5 = Convert.ToDecimal(x.MaskObtained_Y5),
                     MaskObtained_Y6 = Convert.ToDecimal(x.MaskObtained_Y6),
                     MaskObtained_Y7 = Convert.ToDecimal(x.MaskObtained_Y7),
                     SymbolNo_Y1=x.SymbolNo_Y1,
                     SymbolNo_Y2=x.SymbolNo_Y2,
                     SymbolNo_Y3=x.SymbolNo_Y3,
                     SymbolNo_Y4=x.SymbolNo_Y4,
                     SymbolNo_Y5=x.SymbolNo_Y5,
                     SymbolNo_Y6=x.SymbolNo_Y6,
                     SymbolNo_Y7=x.SymbolNo_Y7,
                     Result_Y1=x.Result_Y1,
                     Result_Y2=x.Result_Y2,
                     Result_Y3=x.Result_Y3,
                     Result_Y4=x.Result_Y4,
                     Result_Y5=x.Result_Y5,
                     Result_Y6=x.Result_Y6,
                     Result_Y7=x.Result_Y7,
                    
                     PassPercentage=x.PassPercentage


                 });
            viewmodel.ClassPercentageViewModel=viewmodel.BoardExamDetail.Select(x=>new ClassPercentageViewModel{
            ClassId=x.ClassId,
            Percentage=x.PassPercentage
            
            });
        
            return PartialView("_PartialBoardExamMasterEdit", viewmodel);
        }

        [HttpPost]
        public ActionResult BoardExamMasterEdit(BoardExamViewModel viewmodel,
            IEnumerable<BoardExamDetail> BoardExamDetailList, IEnumerable<ClassPercentageViewModel> ClassIdPercentageList)
        {
            if (base.SystemControl.DateType == 1)
                {
                    if (!string.IsNullOrEmpty(viewmodel.CharacterDisplayDate))
                    {
                        viewmodel.BoardExamMaster.CharacterCertificateIssueDate = Convert.ToDateTime(viewmodel.CharacterDisplayDate);
                        viewmodel.BoardExamMaster.CharacterCertificateIssueMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(viewmodel.CharacterDisplayDate)).Date;
                    }
                    if (!string.IsNullOrEmpty(viewmodel.TranscriptDisplayDate))
                    {
                         viewmodel.BoardExamMaster.TranscriptIssueDate= Convert.ToDateTime(viewmodel.TranscriptDisplayDate);
                         viewmodel.BoardExamMaster.TranscriptIssueMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(viewmodel.TranscriptDisplayDate)).Date;
                    }
                 
                }
                else
                {
                    if (!string.IsNullOrEmpty(viewmodel.CharacterDisplayDate))
                    {
                         viewmodel.BoardExamMaster.CharacterCertificateIssueMiti = viewmodel.CharacterDisplayDate;
                         viewmodel.BoardExamMaster.CharacterCertificateIssueDate =
                            NepaliDateService.NepalitoEnglishDate( viewmodel.BoardExamMaster.CharacterCertificateIssueMiti);
                    }
                            if (!string.IsNullOrEmpty(viewmodel.TranscriptDisplayDate))
                    {
                         viewmodel.BoardExamMaster.TranscriptIssueMiti = viewmodel.TranscriptDisplayDate;
                         viewmodel.BoardExamMaster.TranscriptIssueDate =
                            NepaliDateService.NepalitoEnglishDate( viewmodel.BoardExamMaster.TranscriptIssueMiti);
                    }
                }

           _boardExamMasterRepository.Update(viewmodel.BoardExamMaster);
            _boardExamDetailRepository.Delete(x=>x.BoardExamMasterId==viewmodel.BoardExamMaster.Id);
            foreach (var item in ClassIdPercentageList)
            {

                foreach (var boardExamDetail in BoardExamDetailList)
                {
                    if (item.ClassId == boardExamDetail.ClassId)
                    {
                        var detail = new BoardExamDetail()
                        {
                            BoardExamMasterId = viewmodel.BoardExamMaster.Id,
                            SubjectId = boardExamDetail.SubjectId,
                            ClassId = boardExamDetail.ClassId,

                            MaskObtained_Y1 = boardExamDetail.MaskObtained_Y1,
                            MaskObtained_Y2 = boardExamDetail.MaskObtained_Y2,
                            MaskObtained_Y3 = boardExamDetail.MaskObtained_Y3,
                            MaskObtained_Y4 = boardExamDetail.MaskObtained_Y4,
                            MaskObtained_Y5 = boardExamDetail.MaskObtained_Y5,
                            MaskObtained_Y6 = boardExamDetail.MaskObtained_Y6,
                            MaskObtained_Y7 = boardExamDetail.MaskObtained_Y7,
                            Result_Y1 = boardExamDetail.Result_Y1,
                            Result_Y2 = boardExamDetail.Result_Y2,
                            Result_Y3 = boardExamDetail.Result_Y3,
                            Result_Y4 = boardExamDetail.Result_Y4,
                            Result_Y5 = boardExamDetail.Result_Y5,
                            Result_Y6 = boardExamDetail.Result_Y6,
                            Result_Y7 = boardExamDetail.Result_Y7,
                            SymbolNo_Y1 = boardExamDetail.SymbolNo_Y1,
                            SymbolNo_Y2 = boardExamDetail.SymbolNo_Y2,
                            SymbolNo_Y3 = boardExamDetail.SymbolNo_Y3,
                            SymbolNo_Y4 = boardExamDetail.SymbolNo_Y4,
                            SymbolNo_Y5 = boardExamDetail.SymbolNo_Y5,
                            SymbolNo_Y6 = boardExamDetail.SymbolNo_Y6,
                            SymbolNo_Y7 = boardExamDetail.SymbolNo_Y7,
                            PassPercentage = item.Percentage,
                            Remarks = boardExamDetail.Remarks,

                        };
                        _boardExamDetailRepository.Add(detail);
                    }

                }


            }
            return RedirectToAction("BoardExamMaster");

        }
        public ActionResult BoardExamMasterDelete(int Id)
        {
          
            _boardExamMasterRepository.Delete(x=>x.Id==Id);
            _boardExamDetailRepository.Delete(x=>x.BoardExamMasterId==Id);
            return RedirectToAction("BoardExamMaster");
        }
        #endregion
        public JsonResult GetExames()
        {

            List<ScExam> lstExames = _examRepository.GetAll().OrderBy(x => x.Schedule).ToList();

            var classes = lstExames.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetExamRoutineClasses(int id)
        {
            var data = _examRoutineRepository.GetMany(x => x.ExamId == id && x.AcademicYearId == CurrentAcademyYearId).Select(x => x.ClassId);
            List<SchClass> lstClasses =
                _scClassRepository.GetMany(x => !data.Contains(x.Id)).OrderBy(x => x.Schedule).ToList();

            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
    }
}
