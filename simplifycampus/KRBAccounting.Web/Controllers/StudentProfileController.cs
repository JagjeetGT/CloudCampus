using System;
using System.Collections.Generic;
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

using KRBAccounting.Web.ViewModels.Report;
using KRBAccounting.Web.ViewModels.Teacher;
using LinqKit;

using KRBAccounting.Web.ViewModels.StudentProfile;


namespace KRBAccounting.Web.Controllers
{
    [Authorize(Roles = "student")]
    public class StudentProfileController : BaseController
    {
        //
        // GET: /StudentFront/
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
        private readonly IScClassScheduleRepository _scClassScheduleRepository;
        private readonly IScStudentinfoRepository _scStudentinfo;
        private readonly IStudentDocumentRepository _studentDocument;
        private readonly IScClassTeacherMappingRepository _classTeacherMapping;
        private readonly IScEmployeeInfoRepository _employeeInfo;
        private readonly IScLibraryCardRepository _libraryCard;
        private readonly IScLibraryMemberRegistrationRepository _libraryMember;
        private readonly IScBillTransactionRepository _billTransaction;
        private readonly IScMonthlyBillStudentRepository _monthlyBillStudent;
        private readonly IScExamRoutineRepository _examRoutine;
        private readonly IScExamMarksEntryRepository _examMarksEntry;
        private readonly IScDivisionRepository _divisionRepository;
        private readonly IScBookReceivedDetailsRepository _bookReceivedDetails;
        private readonly IScMonthlyBillRepository _monthlyBillDetail;
        private readonly IScMessageRepository _message;
        private readonly IScCalendarEventRepository _calendarEvent;


        public StudentProfileController(
            ICompanyInfoRepository companyInfoRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ISecurityService securityService,
            ISystemControlRepository systemControlRepository,
               IScClassSubjectMappingRepository scClassSubjectMapping,
       IScStudentSubjectMappingRepository scStudentSubjectMapping,
            IScClassTeacherMappingRepository classTeacherMapping,
            IScEmployeeInfoRepository employeeInfo,
           IScStudentRegistrationDetailRepository scStudentRegistrationDetail,
            IScClassScheduleRepository scClassScheduleRepository,
            IScStudentinfoRepository studentinfo,
            IStudentDocumentRepository studentDocument,
            IScLibraryCardRepository libraryCard,
            IScLibraryMemberRegistrationRepository libraryMember,
            IScBillTransactionRepository billTransaction,
            IScMonthlyBillStudentRepository monthlyBillStudent,
            IScExamRoutineRepository examRoutine,
            IScExamMarksEntryRepository examMarksEntry,
            IScDivisionRepository divisionRepository,
            IScBookReceivedDetailsRepository bookReceivedDetails,
            IScMonthlyBillRepository monthlyBill,
            IScMessageRepository message,
            IScCalendarEventRepository calendarEvent

            )
        {
            _scClassSubjectMapping = scClassSubjectMapping;
            _scStudentRegistrationDetail = scStudentRegistrationDetail;
            _scStudentSubjectMapping = scStudentSubjectMapping;
            _company = companyInfoRepository;
            _role = roleRepository;
            _user = userRepository;
            _securityService = securityService;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _systemControl = systemControlRepository;
            _scStudentinfo = studentinfo;
            _scClassScheduleRepository = scClassScheduleRepository;
            _classTeacherMapping = classTeacherMapping;
            _studentDocument = studentDocument;
            _employeeInfo = employeeInfo;
            _libraryCard = libraryCard;
            _libraryMember = libraryMember;
            _billTransaction = billTransaction;
            _monthlyBillStudent = monthlyBillStudent;
            _examRoutine = examRoutine;
            _examMarksEntry = examMarksEntry;
            _divisionRepository = divisionRepository;
            _bookReceivedDetails = bookReceivedDetails;
            _monthlyBillDetail = monthlyBill;
            _message = message;
            _calendarEvent = calendarEvent;
        }
        public ActionResult Index()
        {
            var _context = new DataContext();

            var user = _authentication.GetAuthenticatedUser();
            var viewModel = new StudentProfileDashboard();
            viewModel.StudentRegistrationDetail = _scStudentRegistrationDetail.GetById(x => x.UserId == user.Id);
            var teacher = _classTeacherMapping.GetById(
                    x => x.ClassId == viewModel.StudentRegistrationDetail.Studentinfo.ClassId &&
                    x.SectionId == viewModel.StudentRegistrationDetail.SectionId);
            if (teacher != null)
            {
                var teacherinfo = _employeeInfo.GetById(teacher.TeacherId);
                viewModel.ClassTeacher = teacherinfo.Description;
                viewModel.TeacherUserName = teacherinfo.User.Username;
            }
            viewModel.SystemControl = _systemControl.GetAll().FirstOrDefault();

            viewModel.CardName = from n in _context.ScLibraryCards
                                 join t in _context.ScLibrarymemberRegistration.Where(x => x.StudentId == viewModel.StudentRegistrationDetail.StudentId) on n.LibraryRegistrationId equals t.Id
                                 select n.CardNo;
            var libraymember = _libraryMember.GetById(x => x.StudentId == viewModel.StudentRegistrationDetail.StudentId);
            var regId = (libraymember != null) ? libraymember.Id : 0;
            viewModel.LibraryCardUsed = _libraryCard.Count(x => x.IsUse && x.LibraryRegistrationId == regId);
            viewModel.CardDetails = _context.ScBookIssued.Where(x => x.LibraryCard.IsUse &&
                x.LibraryCard.LibraryMemberRegistration.StudentId == viewModel.StudentRegistrationDetail.StudentId)
              .ToList().Select(x => new StCardDetails
                                                                       {
                                                                           CardNo = x.LibraryCard.CardNo,
                                                                           Date = x.Date,
                                                                           DueDate = x.BookDueDate,
                                                                           BookName = x.BookReceivedDetails.BookReceived.Book.Name,
                                                                           Edition = x.BookReceivedDetails.BookReceived.Book.Edition,
                                                                           Publisher = x.BookReceivedDetails.BookReceived.Book.Publisher,
                                                                           Fine = UtilityService.GetLibraryFine(x.BookDueDate)
                                                                       });

            var list = (from bd in _context.ScBookReceivedDetails.Where(x => x.Status == 1)
                        join br in _context.ScBookReceived on bd.MasterId equals br.Id
                        join b in _context.ScBook on br.BookId equals b.Id
                        select b).ToList();


            viewModel.BooKlist = new SelectList(list, "Id", "Name");


            //////////////////             Billl  ///////////////////////////////
            var Bill =
                _billTransaction.GetMany(
                    x => x.StudentId == viewModel.StudentRegistrationDetail.StudentId && x.Source == "MBG").Sum(
                        x => x.BillAmount);
            var receipt =
               _billTransaction.GetMany(
                   x => x.StudentId == viewModel.StudentRegistrationDetail.StudentId && x.Source == "REC").Sum(
                       x => x.ReceiptAmount);
            viewModel.TotalDueAmount = Bill - receipt;
            var monthlybill = _monthlyBillStudent.GetMany(x => x.StudentId == viewModel.StudentRegistrationDetail.StudentId).OrderByDescending(x => x.Month).Select(x => new { x.Month, x.Amount, x.MonthMiti });
            var billList = new List<BillInfo>();
            var amount = viewModel.TotalDueAmount;
            foreach (var item in monthlybill)
            {
                if (amount > 0)
                {

                    // var monthAmt = monthlybill.Amount;
                    var monthId = item.Month;
                    if (base.SystemControl.DateType == 2)
                        monthId = item.MonthMiti;
                    var value = amount;
                    amount = amount - item.Amount;

                    if (amount >= 0)
                    {
                        var bill = new BillInfo()
                                       {
                                           Amount = item.Amount,
                                           MonthId = monthId
                                       };
                        billList.Add(bill);

                    }
                    else
                    {
                        var bill = new BillInfo()
                        {
                            Amount = value,
                            MonthId = monthId
                        };
                        billList.Add(bill);

                    }
                }
            }
            viewModel.BillInfos = billList.OrderBy(x => x.MonthId);
            viewModel.TotalMessage = _message.GetMany(x => x.ReceiverId == user.Id).Count();



            return View(viewModel);
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


            var viewModel = new StudentProfileViewModel();
            viewModel.RegistrationDetail =
                _scStudentRegistrationDetail.GetById(
                    x => x.StudentRegistration.AcademicYearId == AcademicYear.Id && x.User.Username == name);
            ViewBag.Name = name;
            viewModel.StudentDocument =
                _studentDocument.GetMany(x => x.StudentId == viewModel.RegistrationDetail.StudentId);
            return View(viewModel);
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
            return View("Profile","_Layout",viewModel);
        }

        public ActionResult ClassRoutine()
        {
            var viewModel = base.CreateReportViewModel<ReportTeacherScheduleReportViewModel>();

            var student = _scStudentRegistrationDetail.GetById(x => x.User.Username == User.Identity.Name && x.StudentRegistration.AcademicYearId == AcademicYear.Id);
            var predicate = PredicateBuilder.True<ScClassSchedule>();
            if (student.StudentRegistration.ClassId != 0)
            {
                predicate = predicate.And(x => x.ClassId == student.StudentRegistration.ClassId);
            }
            if (student.SectionId != 0)
            {
                predicate = predicate.And(x => x.SectionId == student.SectionId);
            }

            viewModel.Schedules = _scClassScheduleRepository.GetExpandable(predicate);

            Session["TeacherSchedule"] = viewModel;

            //var partialView = this.RenderPartialViewToString("_PartialClassScheduleReport", viewModel);
            return View("ClassRoutine", "_Layout", viewModel);
        }
        public ActionResult ExamRoutine(int classId, string examId)
        {
            var exam = Convert.ToInt32(examId);
            var list = _examRoutine.GetMany(x => x.ClassId == classId && x.ExamId == exam);
            return View("ExamRoutine", "_Layout", list);

        }
        public ActionResult ExamResult(int studentId, int classId, string examId)
        {
            var exam = Convert.ToInt32(examId);
            //var list = _examMarksEntry.GetMany(x => x.ClassId == classId && x.ExamId == exam && x.StudentId==studentId);
            //return View("ExamResult", "_Layout", list);

            var viewModel = base.CreateReportViewModel<ReportMarkSheetReportViewModel>();
            //var predicateOr = PredicateBuilder.False<ScExamMarksEntry>();



            var predicate = PredicateBuilder.True<ScExamMarksEntry>();
            //predicate = predicate.And(x => x.AcademicYearId == AcademicYearId);
            predicate = predicate.And(x => x.StudentId == studentId);

            predicate = predicate.And(x => x.ClassId == classId);

            predicate = predicate.And(x => x.ExamId == exam);

            viewModel.ExamMarksGrouping = from d in _examMarksEntry.GetExpandable(predicate).ToList()
                                          group d by new { d.ClassId, d.ExamId, d.StudentId }
                                              into g
                                              select g.ToList();
            viewModel.ExamMarksEntries = _examMarksEntry.GetExpandable(predicate);
            viewModel.Division = _divisionRepository.GetAll();
            return View("ExamResult", "_Layout", viewModel);

        }
        public ActionResult LibraryBookList(string book_name)
        {
            var book = _bookReceivedDetails.GetMany(x => x.Status == 1 && x.BookReceived.Book.Name.StartsWith(book_name)).GroupBy(x => x.BookReceived.Book.Id).ToList();
            return View("LibraryBookList", "_Layout", book);
        }
        public ActionResult StudentLibraryHistory(int studentId)
        {
            var context = new DataContext();
            var history = (from i in context.ScBookIssued.Where(x => x.LibraryCard.LibraryMemberRegistration.StudentId == studentId)
                           join ir in context.ScBookIssueReturn on i.Id equals ir.IssueId into tt
                           from rt in tt.DefaultIfEmpty()
                           select new StudentLibraryHistory
                                      {
                                          IssueId = i.Id,
                                          IssueDate = i.Date,
                                          BookDetailId = i.BookReceivedDetailId,
                                          CardName = i.LibraryCard.CardNo,
                                          IsReturn = i.IsReturn,
                                          ReturnDate = (DateTime?)rt.ReturnDate,
                                          Fine = rt == null ? 0 : rt.FineAmount,
                                          BookName = i.BookReceivedDetails.BookReceived.Book.Name,
                                          Publisher = i.BookReceivedDetails.BookReceived.Book.Publisher,
                                          Edition = i.BookReceivedDetails.BookReceived.Book.Edition


                                      }).ToList();
            return View("StudentLibraryHistory", "_Layout", history);
        }

        public ActionResult TeacherProfile(string teacherUsername)
        {
            var viewModel = new TeacherProfileViewModel();
            viewModel.EmployeeInfo =
                _employeeInfo.GetById(
                    x => x.User.Username == teacherUsername);
            ViewBag.Name = teacherUsername;

            return View("TeacherProfile", "_Layout", viewModel);


        }
        public ActionResult Friends(int classId, int studentId)
        {
            var student = _scStudentRegistrationDetail.GetMany(x => x.Studentinfo.ClassId == classId && x.StudentId != studentId).OrderBy(x => x.Studentinfo.StuDesc);
            ViewBag.SudentMsgList = _message.GetAll();
            ViewBag.CurrentUserId = _authentication.GetAuthenticatedUser().Id;
            return View("Friends", "_Layout", student);
        }

        public ActionResult Library()
        {
            var book = _bookReceivedDetails.GetMany(x => x.Status == 1).GroupBy(x => x.BookReceived.Book.Id).ToList();
            return View("LibraryBookList", "_Layout", book);
        }
        public ActionResult TeacherList(int classId)
        {
            var context = new DataContext();
            var list = (from n in context.ClassSubjectMappings.Where(x => x.ClassId == classId)
                        join nn in context.ScStaffSubjectMapping on n.SubjectId equals nn.SubjectId

                        select new TeacherListViewModel
                        {
                            SubjectType = n.SubjectType,
                            SubjectId = n.SubjectId,
                            TeacherUserName = nn.Staff.User.Username,
                            SubName = nn.Subject.Description,
                            TeacherName = nn.Staff.Description,
                            ClassName = n.Class.Description,
                            UserId = nn.Staff.userId
                           
                        }).OrderBy(x => x.TeacherName).ToList();
            return View("TeacherList", "_Layout", list);

        }

        public ActionResult AcademicCalendar()
        {
            return View("Calendar","_Layout");
          //  return View("AcademicCalendar", "_Layout");
        }

        public ActionResult FeeHistory(int studentId)
        {
            var viewModel = new StudentFeeListViewModel();
            viewModel.BillTransactions = _billTransaction.GetMany(x => x.StudentId == studentId);
            viewModel.MonthlyBills = _monthlyBillDetail.GetAll().AsEnumerable();
            ViewBag.DateType = _systemControl.GetAll().FirstOrDefault().DateType;
            return View("FeeHistory", "_Layout", viewModel);
        }

        public ActionResult StudentMsg(int studentId)
        {
            var model = new ScMessage();
            model.SenderId = _authentication.GetAuthenticatedUser().Id;
          
           var   student = _scStudentRegistrationDetail.GetById(x => x.UserId == studentId);
            if (student != null)
            {
                model.MsgTitle = student.Studentinfo.StuDesc;
                model.ReceiverId = studentId;
                
            }
            else
            {
                var teacher = _employeeInfo.GetById(x => x.userId == studentId);
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
                _message.Add(model);
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
            return Json(new { view = partialview
                , notification = partialNotification 
            }, JsonRequestBehavior.AllowGet);
        }
    
     public ActionResult Notification()
        {
            var user = _authentication.GetAuthenticatedUser();
         var data = UtilityService.GetMessageCount(user.Username);

         return PartialView("_partialNotification", data);
         // var partialNotification = this.RenderPartialViewToString("_partialNotification", data);
         // return Json(partialNotification , JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComposeMessage()
        {
            var context = new DataContext();
            var userid = _authentication.GetAuthenticatedUser().Id;
            var student = UtilityService.GetStudentbyUserId(userid);
           
            var studentdata =
                (from sd in context.ScStudentRegistrationDetails.Where(x => x.Studentinfo.ClassId == student.ClassId && x.UserId!=userid)
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
    }
}
