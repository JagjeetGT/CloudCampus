using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.Payroll;
using KRBAccounting.Web.ViewModels.Report;
using KRBAccounting.Web.ViewModels.StudentProfile;
using KRBAccounting.Web.ViewModels.Teacher;
using LinqKit;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherController : BaseController
    {
        //
        // GET: /Teacher/
        private readonly IUserRepository _user;
        private readonly IRoleRepository _role;
        private readonly ISecurityService _securityService;
        private readonly FileHelper _filehelp = new FileHelper();
        private readonly IFormsAuthenticationService _authentication;
        private readonly ISystemControlRepository _systemControl;
        private readonly ICompanyInfoRepository _company;
        private readonly IScClassSubjectMappingRepository _scClassSubjectMapping;
        private readonly IScStudentSubjectMappingRepository _scStudentSubjectMapping;
        private readonly IScStudentRegistrationDetailRepository _scStudentRegistrationDetail;
        private readonly IScEmployeeInfoRepository _scEmployeeInfoRepository;
        private readonly IScClassScheduleRepository _scClassScheduleRepository;
        private readonly IScSubjectRepository _scSubjectRepository;
        private readonly IStudentDocumentRepository _studentDocument;
        private readonly IScTeacherScheduleRepository _teacherSchedule;
        private readonly IScClassRepository _classRepository;
        private readonly IScSectionRepository _scSectionRepository;
        private readonly IScClassTeacherMappingRepository _classTeacherMapping;
        private readonly IScStaffSubjectMappingRepository _subjectTeacherMapping;
        private readonly ITemplateRepository _TemplateRepository;
        private readonly IScMessageRepository _message;
        private readonly IScSectionRepository _section;
        private readonly IScExamMarksEntryRepository _marksEntry;
        private readonly IScExamMarkSetupRepository _markSetup;
        private readonly IScClassSubjectMappingRepository _subjectMapping;
        private readonly IScExamRepository _examRepository;
        private readonly IScCalendarEventRepository _calendarEvent;
       
        public TeacherController(
            ICompanyInfoRepository companyInfoRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ISecurityService securityService,
            ISystemControlRepository systemControlRepository,
               IScClassSubjectMappingRepository scClassSubjectMapping,
       IScStudentSubjectMappingRepository scStudentSubjectMapping,
            IScEmployeeInfoRepository scEmployeeInfoRepository,
 IScStudentRegistrationDetailRepository scStudentRegistrationDetail,
            IScClassScheduleRepository scClassScheduleRepository,
            IScSubjectRepository subjectRepository,
            IStudentDocumentRepository studentDocument,
            IScTeacherScheduleRepository teacherSchedule,
            IScClassTeacherMappingRepository classTeacherMapping,
            IScStaffSubjectMappingRepository subjectTeacherMapping,
            ITemplateRepository templateRepository,
            IScMessageRepository message,
            IScSectionRepository section,
            IScClassRepository classRepository,
            IScExamMarkSetupRepository markSetup,
            IScExamMarksEntryRepository marksEntry,
            IScClassSubjectMappingRepository subjectMapping,
            IScExamRepository examRepository,
            IScCalendarEventRepository calendarEvent
            )
        {
            _scClassSubjectMapping = scClassSubjectMapping;
            _scStudentRegistrationDetail = scStudentRegistrationDetail;
            _scStudentSubjectMapping = scStudentSubjectMapping;
            _company = companyInfoRepository;
            _role = roleRepository;
            _user = userRepository;
            _scEmployeeInfoRepository = scEmployeeInfoRepository;
            _securityService = securityService;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _systemControl = systemControlRepository;
            _scSubjectRepository = subjectRepository;
            _scClassScheduleRepository = scClassScheduleRepository;
            _teacherSchedule = teacherSchedule;
            _studentDocument = studentDocument;
            _classTeacherMapping = classTeacherMapping;
            _subjectTeacherMapping = subjectTeacherMapping;
            _TemplateRepository = templateRepository;
            _message = message;
            _section = section;
            _classRepository = classRepository;
            _marksEntry = marksEntry;
            _markSetup = markSetup;
            _subjectMapping = subjectMapping;
            _examRepository = examRepository;
            _calendarEvent = calendarEvent;

        }
        public ActionResult Index()
        {
            var viewModel = new TeacherDashboardViewModel();
            var context = new DataContext();
             var user = _authentication.GetAuthenticatedUser();
            viewModel.EmployeeInfo = UtilityService.GetTeacherbyUserId(user.Id);
            
           // viewModel.ClassTeacherMappings = _classTeacherMapping.GetMany(x => x.TeacherId == viewModel.EmployeeInfo.Id);
            viewModel.SubjectLists =
                _subjectTeacherMapping.GetMany(x => x.StaffId == viewModel.EmployeeInfo.Id).Select(
                    x => new TP_SubjectList() {SubjectId = x.SubjectId, SubjectName = x.Subject.Description});
            var classList = from cs in context.ClassSubjectMappings
                                   join ct in context.ClassTeacherMappings.Where(x => x.TeacherId == viewModel.EmployeeInfo.Id) on cs.ClassId equals ct.ClassId
                                   orderby new { cs.Class.Schedule, ct.Section.Description}
                                   select new TP_ClassList { ClassId = cs.ClassId, ClassName = cs.Class.Description, SubjectId = cs.SubjectId,SectionId = ct.SectionId
                                   ,  SectionName = ct.Section.Description};
            viewModel.ClassLists = classList.ToList();
            viewModel.TotalMessage = _message.GetMany(x => x.ReceiverId == user.Id).Count();
            
            viewModel.TeacherListItems = new SelectList(Enum.GetValues(typeof(TP_List)).Cast<TP_List>().Select(
  x => new SelectListItem() { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text").ToList();


            return View("Index","_Layout",viewModel);
        }
        #region Setting

        public ActionResult Setting()
        {

            var user = _authentication.GetAuthenticatedUser();
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("LogOn", "Account");
        }
        [HttpPost]
        public ActionResult UserInfo(User model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var user = _securityService.GetUserById(model.Id);
                if (user.Username == model.Username || _securityService.IsUserNameAvailable(model.Username))
                {

                    var imgGuid = model.ImageGuid;
                    var fileName = string.Empty;
                    var fileext = model.Ext;
                    var filenamewithoutext = string.Empty;

                    if (file != null)
                    {
                        fileext = Path.GetExtension(file.FileName);

                        if (fileext == ".jpg" || fileext == ".png" || fileext == ".jpeg")
                        {
                            imgGuid = Guid.NewGuid().ToString();
                            var coverfilename = imgGuid + fileext;
                            var path = AppDomain.CurrentDomain.BaseDirectory + "Content\\UserImages\\";
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);
                            path = Path.Combine(path, coverfilename);
                            file.SaveAs(path);
                            var thumbpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\UserImages\\";

                            _filehelp.ConvertToThumbnail(file, imgGuid + "_th", fileext, thumbpath);
                            fileName = file.FileName;
                        }
                    }

                    user.ImageGuid = imgGuid;
                    user.Ext = fileext;
                    user.FullName = model.FullName;
                    user.EmailAddress = model.EmailAddress;
                    _securityService.UpdateUser(user);
                    user.Username = model.Username;
                    _authentication.SignIn(user, false);



                }
                else
                {
                    ModelState.AddModelError("", "User name already exists. Please enter a different user name."); return View("UserInfo", "_Layout", model);
                }
                // var user1 = _authentication.GetAuthenticatedUser();


            }
            return RedirectToAction("Setting");


        }
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    var ranchId = _authentication.GetAuthenticatedUser().LastAccessedBranch;
                    var currentuser = _securityService.CheckLogin(User.Identity.Name, model.OldPassword, ranchId);
                    if (currentuser == null)
                    {
                        return Content("Invalid Password");

                    }

                    currentuser.Password = PasswordHelper.HashPassword(model.NewPassword);
                    changePasswordSucceeded = _securityService.ChangePasswrod(currentuser);

                    //MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    //changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return Content("true");
                }
                else
                {
                    return Content("The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        public ActionResult UploadImages(IEnumerable<HttpPostedFileBase> attachments)
        {
            var imgGuid = string.Empty;
            var fileName = string.Empty;
            var fileext = string.Empty;
            var filenamewithoutext = string.Empty;
            var newGUid = string.Empty;
            foreach (var file in attachments)
            {
                fileext = Path.GetExtension(file.FileName);
                filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                if (fileext != ".jpg" && fileext != ".png" && fileext != ".jpeg") continue;
                imgGuid = Guid.NewGuid().ToString();
                var coverfilename = imgGuid + fileext;
                var path = AppDomain.CurrentDomain.BaseDirectory + "Content\\UserImages\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, coverfilename);
                file.SaveAs(path);
                var thumbpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\UserImages\\";

                _filehelp.ConvertToThumbnail(file, imgGuid + "_th", fileext, thumbpath);
                fileName = file.FileName;
                //imgGuid = imgGuid + fileext;

            }
            var res = Json(new { guid = imgGuid, filename = fileName, ext = fileext, filenamewithoutext = filenamewithoutext });

            return res;
        }
        #endregion

        public ActionResult Profile(string name)
        {
            var viewModel = new TeacherProfileViewModel();
            viewModel.EmployeeInfo =
                _scEmployeeInfoRepository.GetById(
                    x => x.User.Username == name);
            ViewBag.Name = name;

            return View(viewModel);

        }

        public ActionResult TeacherSubject(int subjectId, string name)
        {

            var usr = _authentication.GetAuthenticatedUser();
            var viewModel = new TeacherSubjectViewModel();

            viewModel.Subject = _scSubjectRepository.GetById(x => x.Id == subjectId);

            viewModel.Classes =

            (from c in _teacherSchedule.GetMany(x => x.Staff.userId == usr.Id && x.ClassScheduleDetail.ClassSchedule.SubjectId == subjectId)

             group c by new
             {
                 c.ClassScheduleDetail.ClassSchedule.ClassId,
                 c.ClassScheduleDetail.ClassSchedule.SectionId
             }
                 into g
                 select g.ToList()).Select(x => new UtilityService.Classes
                            {
                                ClassId =
                                    x.FirstOrDefault().ClassScheduleDetail.
                                    ClassSchedule.ClassId,
                                ClassName =
                                    x.FirstOrDefault().ClassScheduleDetail.
                                    ClassSchedule.Class.
                                    Description,
                                SectionId =
                                    x.FirstOrDefault().ClassScheduleDetail.
                                    ClassSchedule.SectionId,
                                SectionName =
                                    (x.FirstOrDefault().ClassScheduleDetail.
                                         ClassSchedule.
                                         SectionId != 0)
                                        ? x.FirstOrDefault().
                                              ClassScheduleDetail
                                              .ClassSchedule.
                                              Section.
                                              Description
                                        : null
                            }).ToList();


            return View("TeacherSubject", "_Layout", viewModel);
        }

        public ActionResult GetStudentByClassIdAndSubjectId(int subjectid, int classid, int sectionid)
        {
            var usr = _authentication.GetAuthenticatedUser();

            var list = (from n in
                            (
                               from csm in
                                   _scClassSubjectMapping.GetMany(
                                       x => x.SubjectType == 1 && x.SubjectId == subjectid && x.ClassId == classid)
                               join st in
                                   _scStudentRegistrationDetail.GetMany(
                                       x =>
                                       x.SectionId == sectionid &&
                                       x.StudentRegistration.AcademicYearId == AcademicYear.Id)
                                   on csm.ClassId equals st.StudentRegistration.ClassId
                               select st.Studentinfo

                                ).Union
                                (

                                from ssm in
                                    _scStudentSubjectMapping.GetMany(
                                        x =>
                                        x.SubjectId == subjectid && x.ClassId == classid &&
                                        x.AcademicYearId == AcademicYear.Id)

                                select ssm.Studentinfo
                                )
                        group n by n.StudentID
                            ).ToList();



            var view = this.RenderPartialViewToString("Students", list.FirstOrDefault().ToList());

            ViewBag.classid = classid;
            ViewBag.sectionid = sectionid;
            return Json(view, JsonRequestBehavior.AllowGet);



        }
        public ActionResult ClassRoutine(int classid, int sectionid)
        {
            var viewModel = base.CreateReportViewModel<ReportTeacherScheduleReportViewModel>();


            var predicate = PredicateBuilder.True<ScClassSchedule>();
            if (classid != 0)
            {
                predicate = predicate.And(x => x.ClassId == classid);
            }
            if (sectionid != 0)
            {
                predicate = predicate.And(x => x.SectionId == sectionid);
            }

            viewModel.Schedules = _scClassScheduleRepository.GetExpandable(predicate);

            Session["TeacherSchedule"] = viewModel;

            //var partialView = this.RenderPartialViewToString("_PartialClassScheduleReport", viewModel);
            return View("ClassRoutine", "_Layout", viewModel);
        }
        public ActionResult Classes(int sectionid, int classid)
        {
            //var viewmodel = new TeacherClassViewModel();
            //viewmodel.RegistrationDetails = _scStudentRegistrationDetail.GetMany(
            //     x =>
            //     x.SectionId == sectionid &&
            //     x.StudentRegistration.AcademicYearId == AcademicYear.Id && x.StudentRegistration.ClassId == classid);
            var viewModel = base.CreateReportViewModel<ReportTeacherScheduleReportViewModel>();
            viewModel.Schedules = _scClassScheduleRepository.GetMany(x=>x.ClassId==classid && x.SectionId==sectionid);
            var list = from d in viewModel.Schedules
                       group d by new { d.ClassId, d.SectionId }
                           into g
                           select g.ToList();
            viewModel.ScheduleGroupByClassSection = list;

           // viewmodel.Class = _clas
            return View("ClassSchedule", "_Layout", viewModel);
        }
        public ActionResult TeacherDetailList(int subjectId, int classId, int sectionId, int itemValue)
        {
            var context = new DataContext();
            
            if(itemValue==1)
            {
                var student =
                    _scStudentRegistrationDetail.GetMany(
                        x => x.Studentinfo.ClassId == classId && x.SectionId == sectionId);
                return View("TP_StudentList", "_Layout", student);

            }
            if(itemValue==2)
            {
                var viewModel = base.CreateReportViewModel<ReportTeacherScheduleReportViewModel>();
                viewModel.Schedules = _scClassScheduleRepository.GetMany(x => x.ClassId == classId && x.SectionId == sectionId);
                var list = from d in viewModel.Schedules
                           group d by new { d.ClassId, d.SectionId }
                               into g
                               select g.ToList();
                viewModel.ScheduleGroupByClassSection = list;

                // viewmodel.Class = _clas
                return View("ClassSchedule", "_Layout", viewModel);
            }
            if (itemValue==3)
            {
                var viewModel = base.CreateReportViewModel<ReportExamRoutineReportViewModel>();

                var list = context.ScExamRoutine.Where(x => x.ClassId == classId && x.SubjectId == subjectId).ToList();
                var lastexamId = 0;
                var lastOrDefault = list.LastOrDefault();
                if (lastOrDefault != null)
                {
                     lastexamId = lastOrDefault.ExamId;
                }
                viewModel.ExamId = lastexamId;
                ViewBag.Subject_Id = subjectId;
                ViewBag.Class_Id = classId;
                viewModel.ExamList= new SelectList(UtilityService.GetExamist(),"Value","Text");
                viewModel.ExamRoutineGrouping = from d in list.Where(x=>x.ExamId==lastexamId)
                                                group d by new { d.ClassId, d.ExamId }
                                                    into g
                                                    select g.ToList();
                viewModel.ExamRoutines = list.Where(x => x.ExamId == lastexamId);
                viewModel.ExamRoutineFooter =
                    _TemplateRepository.GetById(x => x.TemplateType == (int)TemplateType.ExamRoutineFooter).Description;
                Session["ReportModel"] = viewModel;
                return View("TP_ExamRoutine","_Layout", viewModel);
               
            }
            if(itemValue==4)
            {
                var viewModel = base.CreateReportViewModel<ReportExamRoutineReportViewModel>("Exam Mark Setup");
               var list = context.ScExamMarkSetup.Where(x => x.ClassId == classId && x.SubjectId == subjectId).ToList();
                var lastexamId = 0;
                 var lastOrDefault = list.LastOrDefault();
                if (lastOrDefault != null)
                {
                     lastexamId = lastOrDefault.ExamId;
                }
                viewModel.ExamMarkSetupGrouping = from d in list.Where(x => x.ExamId == lastexamId)
                                                  group d by new { d.ClassId, d.ExamId }
                                                      into g
                                                      select g.ToList();
                viewModel.ExamMarkSetUps = list.Where(x => x.ExamId == lastexamId);
                ViewBag.ClassName = "Class: " + _classRepository.GetById(classId).Description + " " +
                            _section.GetById(sectionId).Description + ", Subject: " + _scSubjectRepository.GetById(subjectId).Description + ", Exam: " +
                            _examRepository.GetById(lastexamId).Description;
                Session["ReportModel"] = viewModel;
                return View("TP_ExamMarkSetup", "_Layout", viewModel);
                
            }
            if(itemValue==5)
            {
                var list = _markSetup.GetMany(x => x.ClassId == classId && x.SubjectId == subjectId).ToList();
                var lastexamId = 0;
                
                var lastOrDefault = list.LastOrDefault();
                if (lastOrDefault != null)
                {
                    lastexamId = lastOrDefault.ExamId;
                }
                if(lastexamId==0)
                {
                    return View("_PartialMarkEntryForm", "_Layout");
                }
                else
                {
                    
                    ViewBag.StatusList = new SelectList(
                   Enum.GetValues(typeof(ExamAttendanceStatus)).Cast
                       <ExamAttendanceStatus>().Select(
                           x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                   "Value", "Text");
                    ViewBag.ClassName = "Class: " + _classRepository.GetById(classId).Description + " " +
                             _section.GetById(sectionId).Description + ", Subject: " + _scSubjectRepository.GetById(subjectId).Description + ", Exam: " +
                             _examRepository.GetById(lastexamId).Description;

                    ViewBag.ClassId = classId;
                    ViewBag.SubjectId = subjectId;
                    ViewBag.ExamIdd = lastexamId;

                    var data = _marksEntry.GetById(x => x.ClassId == classId && x.ExamId == lastexamId && x.SubjectId == subjectId && x.AcademicYearId == this.AcademicYear.Id);
                    if (data == null)
                   
                    {
                        var subject = _marksEntry.GetMany(i => i.ClassId == classId && i.ExamId == lastexamId && i.SubjectId == subjectId);

                    var subj = _subjectMapping.GetById(x => x.SubjectId == subjectId && x.ClassId == classId);
                    var dataAdd = UtilityService.GetSubjectWiseMarksDetail(lastexamId, classId, subjectId, subj.SubjectType);
                        return View("_partialMarkEntryAdd", "_Layout", dataAdd);


                    }
                    else
                    {
                        data.oldExamId = lastexamId;
                        data.oldClassId = classId;
                        data.oldSubjectId = subjectId;

                        data.SubjectWiseMarksDetailses = UtilityService.GetSubjectWiseMarksDetailEdit(classId, lastexamId, subjectId);


                        return View("TP_MarkEntry", "_Layout", data);
                    }
                   
                    
                }
                
            }
            return null;

        }
        public ActionResult TeacherFriendsList(int teacherId)
        {
            var student = _scEmployeeInfoRepository.GetMany(x =>  x.Id != teacherId).OrderBy(x => x.Description);
            return View("TeacherFriendsList", "_Layout", student);
        }
        public ActionResult StudentList(int teacherId)
        {
            var context = new DataContext();
            var viewModel = new TeacherStudentListViewModel();
            viewModel.StudentRegistrationDetails= from sd in context.ScStudentRegistrationDetails
                       join s in context.ScStudentRegistrations on sd.RegMasterId equals s.Id
                       join ct in context.ClassTeacherMappings.Where(x => x.TeacherId == teacherId) on   s.ClassId equals  ct.ClassId
                       orderby  sd.Studentinfo.Class.Schedule
                       select sd;
            viewModel.GroupByList = viewModel.StudentRegistrationDetails.GroupBy(x =>x.Studentinfo.ClassId);
            return View("StudentList", "_Layout",viewModel);
        }

        public ActionResult TP_ExamRoutineListss(int examId, int classId, int subjectId)
        {
            var viewModel = base.CreateReportViewModel<ReportExamRoutineReportViewModel>();
            var context = new DataContext();

            var list = context.ScExamRoutine.Where(x => x.ClassId == classId && x.SubjectId == subjectId).ToList();

            ViewBag.Subject_Id = subjectId;
            ViewBag.Class_Id = classId;
            viewModel.ExamList = new SelectList(UtilityService.GetExamist(), "Value", "Text");
            viewModel.ExamRoutineGrouping = from d in list.Where(x => x.ExamId == examId)
                                            group d by new { d.ClassId, d.ExamId }
                                                into g
                                                select g.ToList();
            viewModel.ExamRoutines = list.Where(x => x.ExamId == examId);
            viewModel.ExamRoutineFooter =
                _TemplateRepository.GetById(x => x.TemplateType == (int)TemplateType.ExamRoutineFooter).Description;
            var partialView = this.RenderPartialViewToString("_partial_TP_ExamRoutine", viewModel);
            return Json(new { view = partialView}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentMsg(int studentId)
        {
            var model = new ScMessage();
            model.SenderId = _authentication.GetAuthenticatedUser().Id;

            var student = _scStudentRegistrationDetail.GetById(x => x.UserId == studentId);
            if (student != null)
            {
                model.MsgTitle = student.Studentinfo.StuDesc;
                model.ReceiverId = studentId;

            }
            else
            {
                var teacher = _scEmployeeInfoRepository.GetById(x => x.userId == studentId);
                model.MsgTitle = teacher.Description;
                model.ReceiverId = studentId;
            }


            return PartialView("StudentMsg", model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult StudentMsg(ScMessage model)
        {
            if (ModelState.IsValid)
            {
                var currentDate = DateTime.UtcNow;
                model.MsgDate = currentDate;
                model.MsgTime = currentDate.ToString("HH") + ":" + currentDate.ToString("mm") + " " + currentDate.ToString("tt");
                if(model.ClassId != 0 && model.SectionId !=0)
                {
                    var context = new DataContext();
                    var studentList =
                        _scStudentRegistrationDetail.GetMany(
                            x => x.Studentinfo.ClassId == model.ClassId && x.SectionId == model.SectionId).Select(
                                x => x.UserId);
                    foreach (var stu in studentList)
                    {
                        model.ReceiverId = stu;
                        _message.Add(model);
                    }

                }
                else
                {
                    _message.Add(model);
                }
               
                
                var count = _message.Count(x => x.SenderId == model.SenderId && x.ReceiverId == model.ReceiverId);
                return Json(new { userId = model.ReceiverId, content = "true", msgCount = count }, JsonRequestBehavior.AllowGet);

            }
            return null;

        }

        public ActionResult MessageInbox()
        {
            var userid = _authentication.GetAuthenticatedUser().Id;
            var data = _message.GetMany(x => x.ReceiverId == userid).OrderByDescending(x => x.MsgDate);
            return View("MessageInbox", "_Layout", data);
        }

        public ActionResult MessageInboxPartial()
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = _message.GetMany(x => x.ReceiverId == user.Id).OrderByDescending(x => x.MsgDate);
            var partialview = this.RenderPartialViewToString("_PartialMessageInbox", data);
            return Json(new { view = partialview }, JsonRequestBehavior.AllowGet);
        }
        
         public ActionResult MessageDetail(int msgId)
        {
             var user = _authentication.GetAuthenticatedUser();
            var msg = _message.GetById(msgId);
            if (!msg.IsRead)
            {
                msg.IsRead = true;
                _message.Update(msg);
            }

            var partialview = this.RenderPartialViewToString("MessageDetail", msg);
            var data = UtilityService.GetMessageCount(user.Username);
            var partialNotification = this.RenderPartialViewToString("_partialNotification", data);
            return Json(new { view = partialview, notification = partialNotification }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComposeMessage()
        {
            var context = new DataContext();
            var userid = _authentication.GetAuthenticatedUser().Id;
            var teacher = UtilityService.GetTeacherbyUserId(userid);
           
           
            var teacherdata = (from t in context.ScStaffMaster.Where(x=>x.Id != teacher.Id)
                               select new UserList() { Id = t.userId, Name = t.Description, Type = 2 }).ToList();
            var studentdata = (from ct in context.ClassTeacherMappings.Where(x=>x.TeacherId==teacher.Id)
                              join sd in context.ScStudentRegistrationDetails on ct.ClassId equals  sd.Studentinfo.ClassId
                              select new UserList() { Id = sd.UserId, Name = sd.Studentinfo.StuDesc +" ("+ sd.Studentinfo.Class.Description +")", Type = 1 }).ToList();
            


               //(from sd in context.ScStudentRegistrationDetails.Where(x => x.Studentinfo.ClassId == teacher.ClassId && x.UserId != userid)
               // select new UserList() { Id = sd.UserId, Name = sd.Studentinfo.StuDesc, Type = 1 }).ToList();

            var concat = studentdata.Concat(teacherdata);
           
            var viewModel = new ComposeMessageViewModel();
            viewModel.Messages = new ScMessage(); 
            viewModel.Messages.SenderId = userid;
            viewModel.UserLists = concat;
            viewModel.Messages.UserType = new SelectList(Enum.GetValues(typeof(SP_UserType)).Cast<SP_UserType>().Select(x =>
                new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }),
                "Value", "Text");

            var partialview = this.RenderPartialViewToString("ComposeMessage", viewModel);
            return Json(new { view = partialview }, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ComposeMessage(ComposeMessageViewModel model, List<int> ReceiverContact)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in ReceiverContact)
                {
                    var currentDate = DateTime.UtcNow;
                    model.Messages.ReceiverId = i;
                    model.Messages.MsgDate = currentDate;
                    model.Messages.MsgTime = currentDate.ToString("HH") + ":" + currentDate.ToString("mm") + " " + currentDate.ToString("tt");
                    _message.Add(model.Messages);
                }


                var context = new DataContext();
                var userid = _authentication.GetAuthenticatedUser().Id;
                var student = UtilityService.GetStudentbyUserId(userid);
             
                var studentdata =
                    (from sd in context.ScStudentRegistrationDetails.Where(x => x.Studentinfo.ClassId == student.ClassId && x.UserId != userid)
                     select new UserList() { Id = sd.UserId, Name = sd.Studentinfo.StuDesc, Type = 1 }).ToList();
                var teacherdata = (from ct in context.ClassTeacherMappings.Where(x => x.ClassId == student.ClassId)
                                   join t in context.ScStaffMaster on ct.TeacherId equals t.Id
                                   select new UserList() { Id = t.userId, Name = t.Description, Type = 2 }).ToList();

                var concat = studentdata.Concat(teacherdata);

                var viewModel = new ComposeMessageViewModel();
                viewModel.Messages = new ScMessage();
                viewModel.Messages.SenderId = userid;
                viewModel.UserLists = concat;
                viewModel.Messages.UserType = new SelectList(Enum.GetValues(typeof(SP_UserType)).Cast<SP_UserType>().Select(x =>
                    new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }),
                    "Value", "Text");

                var partialview = this.RenderPartialViewToString("ComposeMessage", viewModel);
                return Json(new { view = partialview }, JsonRequestBehavior.AllowGet);

            }
            return null;

        }

        public ActionResult SentMessage()
        {
            var userid = _authentication.GetAuthenticatedUser().Id;
            var data = _message.GetMany(x => x.SenderId == userid).OrderByDescending(x=>x.MsgDate);
            var partialview = this.RenderPartialViewToString("SentMessage", data);
             return Json(new { view = partialview }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TrashMessage()
        {
            var userid = _authentication.GetAuthenticatedUser().Id;
            var data = _message.GetMany(x => x.ReceiverId == userid && x.IsDeleted).OrderByDescending(x=>x.MsgDate);
            var partialview = this.RenderPartialViewToString("TrashMessage", data);
            return Json(new { view = partialview }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClassMessage(int classId, int sectionId)
        {
            var model = new ScMessage();
            model.SenderId = _authentication.GetAuthenticatedUser().Id;
            var classdetail = _classRepository.GetById(classId);
            var section = _section.GetById(sectionId);



            model.MsgTitle ="Class: "+ classdetail.Description + " "+ section.Description;
            model.ClassId = classId;
            model.SectionId= sectionId;
          

            return PartialView("StudentMsg", model);
        }
        [HttpPost]
        public ActionResult SubjectWiseMarksAdd(int classId, int examId, int subjectId, string Remarks, IEnumerable<Sp_SubjectWiseMarksDetail> SubjectWiseMarksDetailctEntry)
        {
            var user = _authentication.GetAuthenticatedUser();
            _marksEntry.Delete(x => x.ClassId == classId && x.ExamId == examId && x.SubjectId == subjectId);
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
                    ClassId = classId,
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedById = user.Id,
                    UpdatedDate = DateTime.UtcNow,
                    ExamId = examId,
                    Remarks = Remarks,
                    SubjectId = subjectId,
                    AcademicYearId = this.AcademicYear.Id

                };
                _marksEntry.Add(data);
            }
            var _context = new DataContext();
            _context.Database.ExecuteSqlCommand("Exec SP_UpdateMarksInfo @ClassId=" + classId + ",@ExamId=" + examId +
                                                ",@AcademicYearId=" + this.AcademicYear.Id);

            UtilityService.GenerateRank(examId, classId, this.AcademicYear.Id);
            return Content("True");


        }
        public ActionResult Notification()
        {
            var user = _authentication.GetAuthenticatedUser();
            var data = UtilityService.GetMessageCount(user.Username);

            return PartialView("_partialNotification", data);
            // var partialNotification = this.RenderPartialViewToString("_partialNotification", data);
            // return Json(partialNotification , JsonRequestBehavior.AllowGet);
        }
        public ActionResult StudentProfile(int userId)
        {


            var viewModel = new StudentProfileViewModel();
            viewModel.RegistrationDetail =
                _scStudentRegistrationDetail.GetById(
                    x => x.StudentRegistration.AcademicYearId == AcademicYear.Id && x.UserId == userId);

            ViewBag.Name = viewModel.RegistrationDetail.User.Username;
            viewModel.StudentDocument =
                _studentDocument.GetMany(x => x.StudentId == viewModel.RegistrationDetail.StudentId);
            return View("StudentProfile", "_Layout", viewModel);
        }
        public ActionResult AcademicCalendar()
        {
            return View("Calendar", "_Layout");
            //  return View("AcademicCalendar", "_Layout");
        }
        public ActionResult Assignments()
       {
           var usr = _authentication.GetAuthenticatedUser();
           var teacher = UtilityService.GetTeacherbyUserId(usr.Id);
           var data = new ScAssignment();
           var classlist =  _classTeacherMapping.GetMany(x => x.TeacherId == teacher.Id).Select(x=> new SelectListItem()
                                                                                                    {
                                                                                                        Text =  (x.SectionId != 0) ? x.Class.Description+  " / " + x.Section.Description : x.Class.Description,
                                                                                                        Value = x.Id.ToString()
                                                                                            });

           data.ClassList = new SelectList(classlist,"Value","Text");
           return View("Assignments", "_Layout", data);
       }
        [HttpPost]
        public ActionResult Assignments(ScAssignment model, HttpPostedFileBase file)
        {
            var classtecher = _classTeacherMapping.GetById(model.ClassTeacherId);
            var data = new ScAssignment()
                           {
                               ClassId = classtecher.ClassId,
                               CreateDate = DateTime.UtcNow,
                               Description = model.Description,
                               DueDate = model.DueDate,
                               FileName = model.FileName,
                               SubjectId = model.SubjectId,
                               Title = model.Title,

                           };
            return null;
        }
        [HttpPost]
        public ActionResult FileUpload(IEnumerable<HttpPostedFileBase> attachmentsroom)
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
                fileName = file.FileName;
                imgGuid = Guid.NewGuid().ToString();
                var coverfilename = imgGuid + fileext;
                var path = AppDomain.CurrentDomain.BaseDirectory + "Content\\TempFile\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, coverfilename);
                file.SaveAs(path);
                //var thumbpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\Assigment\\";
                //_fileHelper.ConvertToThumbnail(file, imgGuid + "_th", fileext, thumbpath);
                //fileName = file.FileName;
            }
            var imagepath = "/Content/TempFile/" + imgGuid + fileext;
        
            var res =
                Json(
                    new
                    {
                        filenamewithoutext = filenamewithoutext,
                        Guid=imgGuid,
                        GuidWithExt =imgGuid + fileext,
                        filename = fileName,
                        path = imagepath,
                        ext = fileext
                    });
            return res;
        }
       
        public ActionResult RemoveFile(string[] fileNames)
        {

            // The parameter of the Remove action must be called "fileNames"
          
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                   
                  

                    //var fileName = Path.GetFileName(fullName);
                    var physicalPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\TempFile\\" + fullName;
                    
                                       
                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);


                    }
                    
                }
            }

            // Return an empty string to signify success
            return null;
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
        public ActionResult Salary()
        {

            var con = new DataContext();
             var user = _authentication.GetAuthenticatedUser();

            var startDate = AcademicYear.StartDate.ToString("MM/dd/yyyy");
            var endDate = AcademicYear.EndDate.ToString("MM/dd/yyyy");
            var reportDate = "";
            
               // startDate = "01/01/2013";
               // endDate = DateTime.UtcNow.ToShortDateString();
              //  reportDate = "Report Date From " + startDate + " To " + endDate;
                //if (SystemControl.DateType == 2)
                //{
                //    endDate = NepaliDateService.NepalitoEnglishDate(endDate).ToString("MM/dd/yyyy");
                //    startDate = NepaliDateService.NepalitoEnglishDate(startDate).ToString("MM/dd/yyyy");
                //}
            var emplyoee = UtilityService.GetTeacherbyUserId(user.Id);

            var viewModel = base.CreateReportViewModel<EmployeeStatementReportViewModel>(reportDate);
            var sql = "";
           
                sql = "SP_EmployeeSalaryStatementDetails @EmpId=" + emplyoee.Id + ",@StartDate='" + startDate +
                     "',@EndDate='" + endDate + "',@AcademicYearId=" + this.AcademicYear.Id;

            
            Session["ReportModel"] = viewModel;

            viewModel.HTMLDATA = con.Database.SqlQuery<string>(sql).FirstOrDefault();
            return View("_PartialEmployeeSatementReport","_layout", viewModel);
            //var partialView = this.RenderPartialViewToString("_PartialEmployeeSatementReport", viewModel);
            //return Json(new { view = partialView },
            //            JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSubjectList(int classteacherMappingId )
        {
            var user = _authentication.GetAuthenticatedUser();

            var emplyoee = UtilityService.GetTeacherbyUserId(user.Id);

            var classTeacher = _classTeacherMapping.GetById(classteacherMappingId);

           var subjectList =_subjectTeacherMapping.GetMany(x => x.StaffId == classTeacher.TeacherId).Select(
                    x => new SelectListItem() { Value = x.SubjectId.ToString(), Text = x.Subject.Description });
            return Json(subjectList, JsonRequestBehavior.AllowGet);

        }


   

    }



    
}
