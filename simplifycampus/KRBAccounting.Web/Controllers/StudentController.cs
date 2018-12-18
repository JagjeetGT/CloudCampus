using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.Academy;
using KRBAccounting.Web.ViewModels.School;
using LinqKit;
using KRBAccounting.Web.ViewModels.Student;
using KRBAccounting.Web.ViewModels.Report;
using System.Data;
using iTextSharp.text;

namespace KRBAccounting.Web.Controllers
{
    [Authorize]
    
    public class StudentController : BaseController
    {
        private readonly IDocumentNumeringSchemeRepository _documentNumeringSchemeRepository;
        private readonly IScClassRepository _scClassRepository;
        private readonly IScBillTransactionRepository _scBillTransactionRepository;
        private readonly IScSectionRepository _scSectionRepository;
        private readonly IScStudentinfoRepository _scStudentinfoRepository;
        private readonly IScReligionRepository _scReligionRepository;
        private readonly IScNationalityRepository _scNationalityRepository;
        private readonly IScStudentCategoryRepository _scStudentCategoryRepository;
        private readonly IScProgramMasterRepository _programMasterRepository;
        private readonly IFormsAuthenticationService _authentication;
        private readonly ISystemControlRepository _systemControl;
        private readonly IScClassFeeRateRepository _scclassfeerateRepository;
        private readonly IUserRepository _user;
        private readonly IRoleRepository _roleRepository;
        private readonly ISecurityService _securityService;
        private readonly IScStudentRegistrationRepository _scStudentRegistrationRepository;
        private readonly IScStudentRegistrationDetailRepository _scStudentRegistrationDetailRepository;
        private readonly IStudentExtraActivityRepository _StudentExtraActivityRepository;

        private readonly IScAbsentApplicationRepository _scabsentapplicationRepository;
        private readonly IStudentSlcInfoRepository _studentSlc;
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly IScStudentAttendanceRepository _scstudentattendanceRepository;
        private readonly IStudentDocumentRepository _studentDocument;
        private readonly IScDivisionRepository _scDivisionRepository;
        private readonly IAcademicBackgroundRepository _academicBackgroundRepository;
        private readonly IScBookReceivedRepository _scbookreceivedRepository;
        private readonly IScBookReceivedDetailsRepository _scbookreceiveddetailsRepository;
        private readonly IScLibraryMemberRegistrationRepository _sclibrarymemberregistrationRepository;
        private readonly IScLibraryCardRepository _scLibraryCardRepository;
        private readonly IScBookIssuedRepository _scbookissuedRepository;
        private readonly IScBookIssueReturnRepository _scbookissuereturnRepository;
        private readonly IScExamMarksEntryRepository _scexammarksentryRepository;
        private readonly IStudentExtraActivityDetailRepository _studentExtraActivityDetailRepository;
        private readonly IAcademicYearRepository _academicyearRepository;
        private readonly IScMonthlyBillStudentRepository _scmonthlybillstudentrepository;
        private readonly IScBillTransactionRepository _scbilltransactionrepository;
        private readonly ITemplateRepository _templaterepository;
        private readonly IStudentSlcInfoRepository _studentSlcInfoRepository;
        private int CurrentAcademyYearId;
        
        #region Constructor

        public StudentController(IUserRepository userRepository,
            IStudentSlcInfoRepository studentSlcInfoRepository,
            ITemplateRepository templaterepository,
            IScBillTransactionRepository scbilltransactionrepository,
            IScMonthlyBillStudentRepository scmonthlybillstudentrepository,
            IAcademicYearRepository academicyearRepository,
            IStudentExtraActivityDetailRepository studentExtraActivityDetailRepository,
            IStudentExtraActivityRepository studentExtraActivityRepository,
            IScExamMarksEntryRepository scExamMarksEntryRepository,
            IScLibraryCardRepository scLibraryCardRepository,
            IScBookIssuedRepository scBookIssuedRepository,
            IScBookIssueReturnRepository scBookIssueReturnRepository,
            IScBookReceivedRepository scBookReceivedRepository,
            IScBookReceivedDetailsRepository scBookReceivedDetailsRepository,
            IScLibraryMemberRegistrationRepository scLibraryMemberRegistrationRepository,
            ISecurityService securityService,
            IRoleRepository roleRepository,
            IAttendanceLogRepository attendanceLogRepository,
            IAcademicBackgroundRepository academicBackgroundRepository,
                                IScClassRepository scClassRepository,
                                IScSectionRepository scSectionRepository,
                                IScStudentinfoRepository scStudentinfoRepository,
                                IScReligionRepository scReligionRepository,
                                IScNationalityRepository scNationalityRepository,
                                 IScBillTransactionRepository billTransactionRepository,
                                IScStudentCategoryRepository scStudentCategoryRepository,
            IDocumentNumeringSchemeRepository documentNumeringSchemeRepository,
                                IScClassFeeRateRepository scclassfeerateRepository,
                               ISystemControlRepository systemControl,
                                IScStudentRegistrationRepository studentRegistrationRepository,
                                IScStudentRegistrationDetailRepository StudentRegistrationDetail,
                              
                               IScProgramMasterRepository programMasterRepository,
         IScStudentAttendanceRepository scstudentattendanceRepository,
            IScAbsentApplicationRepository scabsentapplicationRepository,
            IStudentSlcInfoRepository studentSlc,
            IStudentDocumentRepository studentDocument,
            IScDivisionRepository scDivisionRepository



            )
        {
            _studentSlcInfoRepository = studentSlcInfoRepository;
            _templaterepository = templaterepository;
            _scbilltransactionrepository = scbilltransactionrepository;
            _scmonthlybillstudentrepository = scmonthlybillstudentrepository;
            _academicyearRepository = academicyearRepository;
            _studentExtraActivityDetailRepository = studentExtraActivityDetailRepository;
            _StudentExtraActivityRepository = studentExtraActivityRepository;
            _scexammarksentryRepository=scExamMarksEntryRepository;
            _scLibraryCardRepository = scLibraryCardRepository;
            _scbookissuereturnRepository = scBookIssueReturnRepository;
            _scbookissuedRepository = _scbookissuedRepository;
            _sclibrarymemberregistrationRepository = scLibraryMemberRegistrationRepository;
            _scbookreceiveddetailsRepository = scBookReceivedDetailsRepository;
            _scbookreceivedRepository = scBookReceivedRepository;
            _scDivisionRepository = scDivisionRepository;
            _scClassRepository = scClassRepository;
            _user = userRepository;
            _securityService = securityService;
            _scSectionRepository = scSectionRepository;
            _scStudentinfoRepository = scStudentinfoRepository;
            _scReligionRepository = scReligionRepository;
            _scNationalityRepository = scNationalityRepository;
            _scStudentCategoryRepository = scStudentCategoryRepository;
            _programMasterRepository = programMasterRepository;
            _systemControl = systemControl;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Academy);
            _documentNumeringSchemeRepository = documentNumeringSchemeRepository;
            _scclassfeerateRepository = scclassfeerateRepository;
            _roleRepository = roleRepository;
            _scStudentRegistrationRepository = studentRegistrationRepository;
            _scStudentRegistrationDetailRepository = StudentRegistrationDetail;
            _StudentExtraActivityRepository = studentExtraActivityRepository;
            _scBillTransactionRepository = billTransactionRepository;
            _scabsentapplicationRepository = scabsentapplicationRepository;

            CurrentAcademyYearId = AcademicYear.Id;

            _scstudentattendanceRepository = scstudentattendanceRepository;
            _studentSlc = studentSlc;
            _studentDocument = studentDocument;
            _attendanceLogRepository = attendanceLogRepository;
            _academicBackgroundRepository = academicBackgroundRepository;
        }
        #endregion

        #region StudentCategory

        public ActionResult GetStudentCategories()
        {
            var lstStudentCategories = _scStudentCategoryRepository.GetAll().AsQueryable().Select(x => new
               {
                   Id = x.Id,
                   Description
               =
               x.Description,
                   Memo =
               x.Memo
               });

            return Json(lstStudentCategories, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Create", Module = "scsc")]
        public ActionResult AddStudentCategories(bool? fromPicklist)
        {
            if (fromPicklist != null)
            {
                ViewBag.SaveFromPickList = true;
            }
            return PartialView("_PartialAddStudentCategory", new ScStudentCategory());
        }

        [HttpPost]
        public ActionResult AddStudentCategories(ScStudentCategory modelStudentCategories)
        {
            if (Request.Form["SaveFromPickList"] != null)
            {
                _scStudentCategoryRepository.Add(modelStudentCategories);
                return
                    Json(new { msg = "true", id = modelStudentCategories.Id, title = modelStudentCategories.Description },
                         JsonRequestBehavior.AllowGet);
            }
            _scStudentCategoryRepository.Add(modelStudentCategories);
            return null;
        }

        public JsonResult AcademyStudentCategoryCodeExists(string Code, int Id)
        {
            var feeterm = new ScStudentCategory();
            if (Id != 0)
            {
                var data = _scStudentCategoryRepository.GetById(x => x.Id == Id);
                if (data.Code != Code)
                {
                    feeterm = _scStudentCategoryRepository.GetById(x => x.Code == Code.Trim());

                }
            }
            else
            {
                feeterm = _scStudentCategoryRepository.GetById(x => x.Code == Code);
            }
            if (feeterm == null)
            {
                feeterm = new ScStudentCategory();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcademyStudentCategoryDescriptionExists(string Description, int Id)
        {
            var feeterm = new ScStudentCategory();
            if (Id != 0)
            {
                var data = _scStudentCategoryRepository.GetById(x => x.Id == Id);
                if (data.Description != Description)
                {
                    feeterm = _scStudentCategoryRepository.GetById(x => x.Description == Description.Trim());

                }
            }
            else
            {
                feeterm = _scStudentCategoryRepository.GetById(x => x.Description == Description);
            }
            if (feeterm == null)
            {
                feeterm = new ScStudentCategory();
            }

            return feeterm.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSC")]
        public ActionResult StudentCategories()
        {
            ViewBag.UserRight = base.UserRight("ScSC");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScSC")]
        public ActionResult StudentCategoryAdd()
        {
            var model = new ScStudentCategory();
            return PartialView("_PartialStudentCategoryAdd", model);
        }

        [HttpPost]
        public ActionResult StudentCategoryAdd(ScStudentCategory model)
        {
            if (ModelState.IsValid)
            {
                var user = _authentication.GetAuthenticatedUser();
                model.CreatedDate = DateTime.UtcNow;
                model.UpdatedDate = DateTime.UtcNow;
                model.CreatedById = user.Id;
                model.UpdatedById = user.Id;
                _scStudentCategoryRepository.Add(model);
                return Content("True");
            }
            else
            {
                return Content("False");
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "ScSC")]
        public ActionResult StudentCategoryEdit(int id)
        {
            ScStudentCategory objScStudentCategory = _scStudentCategoryRepository.GetById(x => x.Id == id);
            return PartialView("_PartialStudentCategoryEdit", objScStudentCategory);
        }

        [HttpPost]
        public ActionResult StudentCategoryEdit(ScStudentCategory model)
        {
            ScStudentCategory objScStudentCategory = _scStudentCategoryRepository.GetById(x => x.Id == model.Id);
            objScStudentCategory.Description = model.Description;
            objScStudentCategory.Code = model.Code;
            objScStudentCategory.Memo = model.Memo;
            var user = _authentication.GetAuthenticatedUser();

            model.UpdatedDate = DateTime.UtcNow;

            model.UpdatedById = user.Id;
            _scStudentCategoryRepository.Update(objScStudentCategory);
            return Content("True");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScSC")]
        public ActionResult StudentCategorysList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScSC");

            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            var lstStudentCategorys = _scStudentCategoryRepository.GetPagedList(x => x.Id, pageNo, SystemControl.PageSize);
            return PartialView("_PartialStudentCategoryList",
                               lstStudentCategorys);
        }
        [CheckPermission(Permissions = "Delete", Module = "ScSC")]
        public ActionResult StudentCategoryDelete(int id)
        {

            var data = _scStudentCategoryRepository.DeleteException(x => x.Id == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Students Information
        [CheckPermission(Permissions = "Navigate", Module = "ScStinfo")]
        public ActionResult Students()
        {
            ViewBag.UserRight = base.UserRight("ScStinfo");

            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScStinfo")]
        public ActionResult StudentList()
        {
            ViewBag.UserRight = base.UserRight("ScStinfo");

            var data = _scStudentinfoRepository.GetAll().OrderBy(x => x.Class.Description).ThenBy(x => x.StuDesc);
            //var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            //ViewBag.SnStart = snStart;.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
            return PartialView("_PartialStudentList", data);
        }

        private void FillDataForAddStudentView()
        {
            var lstNationality = _scNationalityRepository.GetAll().AsQueryable().Select(x => new
            {
                Id = x.Id,
                Nationality
            =
            x.Nationality
            }).ToList();
            ViewBag.NationalityDropDown = lstNationality;
            var lstReligion = _scReligionRepository.GetAll().AsQueryable().Select(x => new
            {
                Id = x.Id,
                Religion =
            x.Religion
            }).ToList();
            ViewBag.ReligionDropDown = lstReligion;
            ViewBag.SexDropDown = DropDownHelper.CreateSexDropDown();
            ViewBag.MartialStatusDropDowm = DropDownHelper.CreateMartialStatusDropDown();
            ViewBag.BloodGroup = new SelectList(
                    Enum.GetValues(typeof(BloodGroupDropDown)).Cast
                        <BloodGroupDropDown>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = StringEnum.GetStringValue(x) }), "Value", "Text");
            ViewBag.RelationDropDown = DropDownHelper.CreateRelationDropDown();

        }
        [CheckPermission(Permissions = "Create", Module = "ScStinfo")]
        public ActionResult AddStudent()
        {
            this.FillDataForAddStudentView();
            var model = base.CreateViewModel<ScStudentInfoViewModel>();
            model.ApplyDate = null;
            model.DBO = null;
            model.EntryDate = null;
            model.PassYear = null;
            model.StudentCategories = new SelectList(_scStudentCategoryRepository.GetAll(), "Id", "Description");
            StudentHelper objStudentHelper = new StudentHelper(_scStudentinfoRepository);
            //model.Regno = objStudentHelper.GenerateStudentRegistrationNum();
            DataContext _datacontext = new DataContext();
            var documentNumbering = _datacontext.RegistrationNumberings.FirstOrDefault();
            if (documentNumbering != null)
            {
                model.Regno = UtilityService.GetStudentRegistrationNumbering();

                if (base.SystemControl.IsCurrDate)
                {
                    model.ApplyDateDisplay = base.SystemControl.DateType == 1
                                                 ? DateTime.Now.ToString("MM/dd/yyyy")
                                                 : NepaliDateService.NepaliDate(DateTime.Now).Date;
                    model.EntryDateDisplay = base.SystemControl.DateType == 1
                                                 ? DateTime.Now.ToString("MM/dd/yyyy")
                                                 : NepaliDateService.NepaliDate(DateTime.Now).Date;


                }
                model.SystemControl = _systemControl.GetAll().FirstOrDefault();
                model.DivisionList = new SelectList(_scDivisionRepository.GetAll(), "Id", "Description");

                return View("_PartialAddStudent", model);
            }
            ViewBag.ErrorMsg = "Please Enter Student Registration Numbering";
            ViewBag.Link = "<a href='/Setup/StudentRegistrationNumbering'>click here to Add</a>";
            return View("_Error");
        }
        [CheckPermission(Permissions = "Edit", Module = "ScStinfo")]
        public ActionResult StudentEdit(int id)
        {
            this.FillDataForAddStudentView();
            var model = base.CreateViewModel<ScStudentInfoViewModel>();


            var data = _scStudentinfoRepository.GetById(id);

            Mapper.CreateMap<ScStudentinfo, ScStudentInfoViewModel>();
            model = Mapper.Map<ScStudentinfo, ScStudentInfoViewModel>(data);
            model.Studentinfo = data;
            model.SystemControl = base.SystemControl;
            model.StudentCategories = new SelectList(_scStudentCategoryRepository.GetAll(), "Id", "Description",
                                                     data.StdCategoryId);
            model.AcademicBackground = _academicBackgroundRepository.GetMany(x => x.StudentId == id);
            if (base.SystemControl.DateType == 1)
            {
                if (model.DBO != null)
                {
                    model.DBODisplay = Convert.ToDateTime(data.DBO).ToString("MM/dd/yyyy");

                }
                if (model.ApplyDate != null)
                {
                    model.ApplyDateDisplay = Convert.ToDateTime(data.ApplyDate).ToString("MM/dd/yyyy");

                }


                if (model.EntryDate != null)
                {
                    model.EntryDateDisplay = Convert.ToDateTime(model.EntryDate).ToString("MM/dd/yyyy");

                }
                if (model.PassYear != null)
                {
                    model.PassYerDisplay = Convert.ToDateTime(model.PassYear).Date.Year.ToString();

                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DBOMiti))
                {
                    model.DBODisplay = data.DBOMiti;

                }
                if (!string.IsNullOrEmpty(model.ApplyMiti))
                {
                    model.ApplyDateDisplay = data.ApplyMiti;


                }


                if (!string.IsNullOrEmpty(model.EntryMiti))
                {
                    model.EntryDateDisplay = data.EntryMiti;


                }
                if (!string.IsNullOrEmpty(model.PassMiti))
                {
                    model.PassYerDisplay = data.PassMiti;

                }


            }



            var section = "";
            ViewBag.SexDropDown = new SelectList(DropDownHelper.CreateSexDropDown(), "Text", "Value", data.Sex);
            ViewBag.BloodGroup = new SelectList(
                    Enum.GetValues(typeof(BloodGroupDropDown)).Cast
                        <BloodGroupDropDown>().Select(
                            x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = StringEnum.GetStringValue(x) }), "Value", "Text", data.BloodGrp);
            ViewBag.MartialStatusDropDowm = new SelectList(DropDownHelper.CreateMartialStatusDropDown(), "Text", "Value", data.MaritialSt);


            //if (data.Section != null)
            //{
            //    section = data.Section.Description;
            //}


            return View("_PartialEditStudent", model);
        }



        public JsonResult GenerateStudentRegistrationNumber()
        {
            StudentHelper objStudentHelper = new StudentHelper(_scStudentinfoRepository);
            var data = objStudentHelper.GenerateStudentRegistrationNum();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AcademyBackGroundEntry()
        {
            var data = new AcademicBackground();
            return PartialView("_PartialAcademicBackground", data);
        }
        private ScStudentInfoViewModel CollectStudentInformation(ScStudentInfoViewModel modelScStudentInfo,
                                                                 FormCollection fc)
        {

            modelScStudentInfo.Nationality = Request.Form["Nationality"];
            modelScStudentInfo.CurrClsCode = Request.Form["CurrClsCode"];
            modelScStudentInfo.Religion = Request.Form["Religion"];
            modelScStudentInfo.StdCategoryId = modelScStudentInfo.StdCategoryId;
            //modelScStudentInfo.MaritialSt = Request.Form["MartialStatus"];
            modelScStudentInfo.MaritialSt = Request.Form["MaritialSt"];

            //modelScStudentInfo.Sex = Request.Form["SexDropDown"];
            modelScStudentInfo.Sex = Request.Form["Sex"];
            //modelScStudentInfo.BloodGrp = Request.Form["BloodGroup"];
            modelScStudentInfo.BloodGrp = Request.Form["BloodGrp"];
            modelScStudentInfo.StdPhotoFileName = Request.Form["ImageGuid"];
            modelScStudentInfo.StdPhotoExt = Request.Form["Ext"];
            modelScStudentInfo.GRelation = Request.Form["GurdainRelation"];
        modelScStudentInfo.Regno=UtilityService.GetStudentRegistrationNumbering();

            return modelScStudentInfo;
        }

        public ActionResult StudentDetailInformation(int Id)
        {
            var data = _scStudentinfoRepository.GetById(x => x.StudentID == Id);
            //var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            //ViewBag.SnStart = snStart;.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
            return View("StudentDetailInformation", data);
        }


        [HttpPost]
        public JsonResult AddStudent(ScStudentInfoViewModel modelScStudentInfo, FormCollection fc, IEnumerable<StudentDocument> studentDocumentList, IEnumerable<AcademicBackground> qualificatonDetailList)
        {
             DataContext _datacontext = new DataContext();
            var documentNumbering = _datacontext.RegistrationNumberings.FirstOrDefault();
            documentNumbering.DocCurrNo = documentNumbering.DocCurrNo + 1;
            _datacontext.Entry(documentNumbering).State = EntityState.Modified;
            _datacontext.SaveChanges();
            modelScStudentInfo = CollectStudentInformation(modelScStudentInfo, fc);
            ScStudentinfo objScStudentInfo = ModelMappers.ScStudentInfoMappers(modelScStudentInfo);
            objScStudentInfo.AcademicYearId = base.AcademicYear.Id;
           
           
            //modelScStudentInfo.Studentinfo= new ScStudentinfo();
            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(modelScStudentInfo.DBODisplay))
                {
                    objScStudentInfo.DBO = Convert.ToDateTime(modelScStudentInfo.DBODisplay);
                    objScStudentInfo.DBOMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(objScStudentInfo.DBO)).Date;
                }
                if (!string.IsNullOrEmpty(modelScStudentInfo.ApplyDateDisplay))
                {
                    objScStudentInfo.ApplyDate = Convert.ToDateTime(modelScStudentInfo.ApplyDateDisplay);
                    objScStudentInfo.ApplyMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(objScStudentInfo.ApplyDate)).Date;
                }


                if (!string.IsNullOrEmpty(modelScStudentInfo.EntryDateDisplay))
                {
                    objScStudentInfo.EntryDate = Convert.ToDateTime(modelScStudentInfo.EntryDateDisplay);
                    objScStudentInfo.EntryMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(objScStudentInfo.EntryDate)).Date;
                }
                if (!string.IsNullOrEmpty(modelScStudentInfo.PassYerDisplay))
                {
                    var miti = "01/01/" + modelScStudentInfo.PassYerDisplay;
                    objScStudentInfo.PassYear = Convert.ToDateTime(miti);


                    objScStudentInfo.PassMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(objScStudentInfo.PassYear)).Year.ToString();
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(modelScStudentInfo.DBODisplay))
                {
                    objScStudentInfo.DBOMiti = modelScStudentInfo.DBODisplay;
                    objScStudentInfo.DBO = NepaliDateService.NepalitoEnglishDate(objScStudentInfo.DBOMiti);
                }
                if (!string.IsNullOrEmpty(modelScStudentInfo.ApplyDateDisplay))
                {
                    objScStudentInfo.ApplyMiti = modelScStudentInfo.ApplyDateDisplay;
                    objScStudentInfo.ApplyDate = NepaliDateService.NepalitoEnglishDate(objScStudentInfo.ApplyMiti);

                }


                if (!string.IsNullOrEmpty(modelScStudentInfo.EntryDateDisplay))
                {
                    objScStudentInfo.EntryMiti = modelScStudentInfo.EntryDateDisplay;
                    objScStudentInfo.EntryDate = NepaliDateService.NepalitoEnglishDate(objScStudentInfo.EntryMiti);

                }
                if (!string.IsNullOrEmpty(modelScStudentInfo.PassYerDisplay))
                {
                    objScStudentInfo.PassMiti = modelScStudentInfo.PassYerDisplay;

                    var date = objScStudentInfo.PassMiti + "/01/01";
                    objScStudentInfo.PassYear = NepaliDateService.NepalitoEnglishDate(date);

                }


            }
            _scStudentinfoRepository.Add(objScStudentInfo);
            foreach (var doc in studentDocumentList)
            {
                if (!string.IsNullOrEmpty(doc.DocumentGuid) && !string.IsNullOrEmpty(doc.DocumentExt))
                {
                    doc.StudentId = objScStudentInfo.StudentID;
                    _studentDocument.Add(doc);
                }

            }
            
            foreach (var item in qualificatonDetailList)
            {
                
                if (item.DivisionId != null)
                {
                    item.StudentId = objScStudentInfo.StudentID;
 _academicBackgroundRepository.Add(item);
                }
               
            }
           
            //var viewmodel=new ScStudentinfo();
            //DataContext _datacontext = new DataContext();
            //var documentNumbering = _datacontext.RegistrationNumberings.FirstOrDefault();
            //documentNumbering.DocCurrNo = documentNumbering.DocCurrNo + 1;
            //if (documentNumbering != null)
            //{
            //    viewmodel.Regno= = UtilityService.GetStudentRegistrationNumbering();




            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StudentEdit(ScStudentInfoViewModel modelScStudentInfo, FormCollection fc, IEnumerable<StudentDocument> studentDocumentList, IEnumerable<AcademicBackground> qualificatonDetailList)
        {
            modelScStudentInfo = CollectStudentInformation(modelScStudentInfo, fc);
            ScStudentinfo objScStudentInfo = ModelMappers.ScStudentInfoMappers(modelScStudentInfo);
            objScStudentInfo.AcademicYearId = base.AcademicYear.Id;
            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(modelScStudentInfo.DBODisplay))
                {
                    objScStudentInfo.DBO = Convert.ToDateTime(modelScStudentInfo.DBODisplay);
                    objScStudentInfo.DBOMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(objScStudentInfo.DBO)).Date;
                }
                if (!string.IsNullOrEmpty(modelScStudentInfo.ApplyDateDisplay))
                {
                    objScStudentInfo.ApplyDate = Convert.ToDateTime(modelScStudentInfo.ApplyDateDisplay);
                    objScStudentInfo.ApplyMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(objScStudentInfo.ApplyDate)).Date;
                }


                if (!string.IsNullOrEmpty(modelScStudentInfo.EntryDateDisplay))
                {
                    objScStudentInfo.EntryDate = Convert.ToDateTime(modelScStudentInfo.EntryDateDisplay);
                    objScStudentInfo.EntryMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(objScStudentInfo.EntryDate)).Date;
                }
                if (!string.IsNullOrEmpty(modelScStudentInfo.PassYerDisplay))
                {
                    var miti = "01/01/" + modelScStudentInfo.PassYerDisplay;

                    objScStudentInfo.PassYear = Convert.ToDateTime(miti);

                    objScStudentInfo.PassMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(objScStudentInfo.PassYear)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(modelScStudentInfo.DBODisplay))
                {
                    objScStudentInfo.DBOMiti = modelScStudentInfo.DBODisplay;
                    objScStudentInfo.DBO = NepaliDateService.NepalitoEnglishDate(objScStudentInfo.DBOMiti);
                }
                if (!string.IsNullOrEmpty(modelScStudentInfo.ApplyDateDisplay))
                {
                    objScStudentInfo.ApplyMiti = modelScStudentInfo.ApplyDateDisplay;
                    objScStudentInfo.ApplyDate = NepaliDateService.NepalitoEnglishDate(objScStudentInfo.ApplyMiti);

                }


                if (!string.IsNullOrEmpty(modelScStudentInfo.EntryDateDisplay))
                {
                    objScStudentInfo.EntryMiti = modelScStudentInfo.EntryDateDisplay;
                    objScStudentInfo.EntryDate = NepaliDateService.NepalitoEnglishDate(objScStudentInfo.EntryMiti);

                }
                if (!string.IsNullOrEmpty(modelScStudentInfo.PassYerDisplay))
                {
                    objScStudentInfo.PassMiti = modelScStudentInfo.PassYerDisplay;
                    var date = objScStudentInfo.PassMiti + "/01/01";
                    objScStudentInfo.PassYear = NepaliDateService.NepalitoEnglishDate(date);



                }


            }
            _scStudentinfoRepository.Update(objScStudentInfo);
            _studentDocument.Delete(x => x.StudentId == objScStudentInfo.StudentID);
            foreach (var doc in studentDocumentList)
            {
                if (!string.IsNullOrEmpty(doc.DocumentGuid) && !string.IsNullOrEmpty(doc.DocumentExt))
                {
                    doc.StudentId = objScStudentInfo.StudentID;
                    _studentDocument.Add(doc);
                }

            }
            _academicBackgroundRepository.Delete(x=>x.StudentId==objScStudentInfo.StudentID);
            foreach (var item in qualificatonDetailList)
            {

                if (item.DivisionId != null)
                {
                    item.StudentId = objScStudentInfo.StudentID;
                    _academicBackgroundRepository.Add(item);
                }

            }
//            foreach(var item in qualificatonDetailList ){
             
//              item.StudentId=objScStudentInfo.StudentID;
//              if (item.DivisionId != 0 || item.DivisionId!=null)
//              {
//_academicBackgroundRepository.Add(item);
//              }
              
//            }
            return RedirectToAction("Students");
        }


        public ActionResult UploadStudentPic(IEnumerable<HttpPostedFileBase> attachments)
        {
            FileHelper _fileHelper = new FileHelper();
            var imgGuid = string.Empty;
            var fileName = string.Empty;
            var fileext = string.Empty;
            var filenamewithoutext = string.Empty;
            foreach (var file in attachments)
            {
                fileext = Path.GetExtension(file.FileName);
                filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                if (fileext != ".jpg" && fileext != ".png" && fileext != ".jpeg") continue;
                imgGuid = Guid.NewGuid().ToString();
                var coverfilename = imgGuid + fileext;
                var path = AppDomain.CurrentDomain.BaseDirectory + "Content\\academicsimage\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, coverfilename);
                file.SaveAs(path);
                var thumbpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\academicsimage\\";
                _fileHelper.ConvertToThumbnail(file, imgGuid + "_th", fileext, thumbpath);
                fileName = file.FileName;
            }
            var imagepath = "/Content/academicsimage/" + imgGuid + "_th" + fileext;
            var res =
                Json(
                    new
                    {
                        guid = imgGuid,
                        filename = fileName,
                        ext = fileext,
                        filenamewithoutext = filenamewithoutext,
                        path = imagepath
                    });
            return res;
        }

        [HttpPost]
        public ActionResult UploadDocument()
        {
            HttpPostedFileBase file = Request.Files[0];
            var imgGuid = string.Empty;
            var fileName = string.Empty;
            var fileext = string.Empty;
            var filenamewithoutext = string.Empty;
            var thumbpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\AcademicStudentDocs\\";
            fileext = Path.GetExtension(file.FileName);
            filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
            //if (fileext != ".jpg" && fileext != ".png" && fileext != ".jpeg") continue;
            imgGuid = Guid.NewGuid().ToString();
            var coverfilename = imgGuid + fileext;
            var path = AppDomain.CurrentDomain.BaseDirectory + "Content\\AcademicStudentDocs\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, coverfilename);
            file.SaveAs(path);

            //_filehelp.ConvertToThumbnail(file, imgGuid + "_th", fileext, thumbpath);
            fileName = file.FileName;
            thumbpath += imgGuid + "_th" + fileext;
            //var imagepath = "/Content/StudentDocs/" + imgGuid + "_th" + fileext;
            var res = Json(new { guid = imgGuid, filename = fileName, ext = fileext, filenamewithoutext = filenamewithoutext });
            return res;
        }

        public ActionResult StudentInformationDelete(int id)
        {

            var mapped = _scStudentinfoRepository.DeleteException(x => x.StudentID == id);


            return Json(mapped, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetDivision()
        {
            var division = _scDivisionRepository.GetAll().Select(x => new
             {
                 Id = x.Id,
                 Description = x.Description,

             });
            return Json(division, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Student Addmission / Registration

        public ActionResult ClassWiseStudentlistByClassId(int classId)
        {
            var data = _scclassfeerateRepository.GetMany(x => x.ClassId == classId).ToList();
            return PartialView("_PartialClassWiseFeeRateDetail", data);
        }

        [CheckPermission(Permissions = "Navigate", Module = "scStdReg")]
        public ActionResult StudentRegistrations()
        {
            ViewBag.UserRight = base.UserRight("scStdReg");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "scStdReg")]
        public ActionResult StudentRegistrationList()
        {
            ViewBag.UserRight = base.UserRight("scStdReg");
            // var data = _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).OrderBy(x => x.StudentRegistration.Class.Description);
            var data =
                _scStudentRegistrationRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId).OrderBy(
                    x => x.Class.Description).GroupBy(x => x.ClassId);
            // return PartialView("_PartialStudentRegistrationList",data);
            return PartialView("_PartialStudentRegistrationList", data);
        }

        public ActionResult StudentRegistrationDetail(int ClassId, int Id)
        {
            var list = new List<ScStudentRegistrationDetail>();
            if (Id != 0)
            {
                var i = _scStudentRegistrationRepository.GetById(x => x.Id == Id && x.AcademicYearId == CurrentAcademyYearId);
                if (i.ClassId == ClassId)
                {
                    return Content("false");

                }

            }

            var housemapping =
                _scStudentRegistrationRepository.GetById(x => x.ClassId == ClassId && x.AcademicYearId == CurrentAcademyYearId);


            if (housemapping == null)
            {

                var data = _scStudentinfoRepository.GetMany(x => x.ClassId == ClassId).OrderBy(x => x.StuDesc);



                foreach (var item in data)
                {
                    var sec = new ScSection();
                    var shift = new ScShift();
                    if (Id != 0)
                    {
                        var reg =
                            _scStudentRegistrationDetailRepository.GetById(
                                x => x.RegMasterId == Id && x.StudentId == item.StudentID);
                        if (reg != null && reg.Shift != null)
                            shift = reg.Shift;
                    }
                    var model = new ScStudentRegistrationDetail()
                    {
                        StudentId = item.StudentID,
                        Studentinfo = item,
                        SectionId = sec.Id,
                        RegMasterId = Id,
                        Section = sec,
                        Shift = shift,
                        RollNo = "" //UtilityService.GetRollNumber(ClassId, sec.Id, this.CurrentBranch.Id, this.CurrentAcademyYearId)

                    };
                    list.Add(model);

                }

                return PartialView("_PartialStudentRegistrationDetailEntry", list);

            }

            else
            {
                var stu =
                _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.ClassId == ClassId && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).Select(x => x.StudentId);
                var studentcount =
                    _scStudentRegistrationDetailRepository.GetMany(
                        x =>
                        x.StudentRegistration.ClassId == ClassId &&
                        x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).Count();
                //int sectionId=0;
                //var sectionlist = _scSectionRepository.GetAll();
                //if(studentcount != 0)
                //{
                //    var maxstu = _scClassRepository.GetById(ClassId).MaxStudent;
                //    if (studentcount < maxstu)
                //    {
                //        if (sectionlist != null) sectionId = sectionlist.ElementAt(0).Id;
                //    }
                //    if (studentcount > maxstu && studentcount < 2*studentcount)
                //    {
                //        if (sectionlist != null) sectionId = sectionlist.ElementAt(1).Id;
                //    }
                //    if (studentcount > 2* maxstu && studentcount < 3 * studentcount)
                //    {
                //        sectionId = sectionlist.ElementAt(2).Id;
                //    }



                //}

                var data = _scStudentinfoRepository.GetMany(x => x.ClassId == ClassId && !stu.Contains(x.StudentID));

                foreach (var item in data)
                {
                    var sec = new ScSection();
                    var shift = new ScShift();

                    if (Id != 0)
                    {
                        var reg =
                            _scStudentRegistrationDetailRepository.GetById(
                                x => x.RegMasterId == Id && x.StudentId == item.StudentID);
                        if (reg != null && reg.Shift != null)
                            shift = reg.Shift;
                    }
                    // var sec = _scSectionRepository.GetById(sectionId);
                    var model = new ScStudentRegistrationDetail()
                    {
                        StudentId = item.StudentID,
                        Studentinfo = item,
                        SectionId = sec.Id,
                        RegMasterId = Id,
                        Section = sec,
                        Shift = shift,
                        RollNo = ""//UtilityService.GetRollNumber(ClassId, 0, this.CurrentBranch.Id, this.CurrentAcademyYearId)

                    };
                    list.Add(model);
                }

                return PartialView("_PartialStudentRegistrationDetailEntry", list);
            }
            return Content("True");
        }
        public JsonResult GenerateStudentCode(string studentName)
        {
            StudentHelper objStudentHelper = new StudentHelper(_scStudentinfoRepository);
            string studentCode = objStudentHelper.GenerateStudentCode(studentName);
            return Json(studentCode, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StudentRegistrationDetailAdd()
        {



            var sec = new ScSection();
            var shift = new ScShift();
            var student = new ScStudentinfo();
            var model = new ScStudentRegistrationDetail()
            {
                StudentId = student.StudentID,
                Studentinfo = student,
                SectionId = 0,
                RegMasterId = 0,
                Section = sec,
                Shift = shift,


            };



            return PartialView("_PartialStudentRegistrationDetailEdit", model);
        }
        [CheckPermission(Permissions = "Create", Module = "scStdReg")]
        public ActionResult StudentRegistrationAdd()
        {
            var data = new ScStudentRegistration();
            data.AcademicYearId = CurrentAcademyYearId;
            return PartialView("_PartialStudentRegistrationAdd", data);

        }

        [HttpPost]
        public ActionResult StudentRegistrationAdd(IEnumerable<ScStudentRegistrationDetail> subjectEntry,
                                                   ScStudentRegistration model)
        {
            var data =
                _scStudentRegistrationRepository.GetById(
                    x => x.ClassId == model.ClassId && x.AcademicYearId == CurrentAcademyYearId);
            if (data == null)
            {
                var user = _authentication.GetAuthenticatedUser();
                model.CreatedDate = DateTime.UtcNow;
                model.UpdatedDate = DateTime.UtcNow;
                model.UpdatedById = user.Id;
                model.CreatedById = user.Id;
                model.AcademicYearId = CurrentAcademyYearId;
                _scStudentRegistrationRepository.Add(model);
            }
            else
            {
                model.Id = data.Id;
            }
            var cls =
            _scClassRepository.GetById(x => x.Id == model.ClassId);

            var errorMsg = "Sorry ! Class :" + cls.Description + ", Section :";

            var erorrlist = new List<string>();
            foreach (var item in subjectEntry.Where(x => x.StudentId != 0))
            {
                var roll = UtilityService.GetRollNumber(model.ClassId, item.SectionId, this.CurrentBranch.Id,
                                                        this.CurrentAcademyYearId);
                if (!string.IsNullOrEmpty(roll))
                {



                    //byte[] buffer = Guid.NewGuid().ToByteArray();
                    //var number = BitConverter.ToUInt32(buffer, 8).ToString();

                    var student = _scStudentinfoRepository.GetById(item.StudentId);
                    var number = student.Regno;
                    var user = new User();
                    user.IsDeleted = false;
                    user.LastLoginDate = DateTime.UtcNow;
                    user.LastLoginIp = string.Empty;
                    user.Password = PasswordHelper.HashPassword(number);
                    user.UpdatedDate = DateTime.UtcNow;
                    user.CreatedDate = DateTime.UtcNow;
                    user.CompanyId = this.CompanyInfo.Id;
                    user.FullName = student.StuDesc;
                    user.EmailAddress = student.EmailId;

                    user.Username = student.Regno;
                    user.BranchId = !SystemControl.EnableBranch ? this.CompanyInfo.Id : this.CurrentBranch.Id;

                    user.IsActive = true;
                    _user.Add(user);
                    var roleModel = _roleRepository.GetById(x => x.RoleName.ToLower() == "student");

                    if (roleModel != null)
                    {
                        var role = new List<int>();
                        role.Add(roleModel.Id);
                        _securityService.AssignRole(user.Id, role);
                    }
                    var dat = new ScStudentRegistrationDetail()
                                 {
                                     CurrentStatus = item.CurrentStatus,
                                     Narration = item.Narration,
                                     RegMasterId = model.Id,
                                     RollNo = roll,
                                     SectionId = item.SectionId,
                                     ShiftId = item.ShiftId,
                                     StudentId = item.StudentId,
                                     StudentType = item.StudentType,
                                     BoaderId = item.BoaderId,
                                     UserId = user.Id

                                 };
                    _scStudentRegistrationDetailRepository.Add(dat);
                }
                else
                {
                    var sectionName = "";
                    if (item.SectionId != 0)
                    {
                        var section = _scSectionRepository.GetById(x => x.Id == item.SectionId);
                        if (section != null)
                        {
                            sectionName = section.Description;
                        }
                    }

                    erorrlist.Add(sectionName);
                }

            }
            if (erorrlist.Any())
            {
                var error = erorrlist.GroupBy(x => x);
                errorMsg = error.Aggregate(errorMsg, (current, item) => current + (" ," + item.FirstOrDefault()));
                errorMsg += "  doesn't registered, Please, Generate the Roll No. of respective section !!";
                return Content(errorMsg);
            }
            return Content("True");
        }
        public class ErrorClass
        {
            public int Id { get; set; }
            public string Message { get; set; }
        }

        [CheckPermission(Permissions = "Edit", Module = "scStdReg")]
        public ActionResult
            StudentRegistrationEdit(int id)
        {

            var data = _scStudentRegistrationRepository.GetById(x => x.Id == id && x.AcademicYearId == CurrentAcademyYearId);

            return PartialView("_PartialStudentRegistrationEdit", data);

        }

        [HttpPost]
        public ActionResult StudentRegistrationEdit(IEnumerable<ScStudentRegistrationDetail> subjectEntry,
                                                    ScStudentRegistration model)
        {
            var user = _authentication.GetAuthenticatedUser();

            model.UpdatedDate = DateTime.UtcNow;
            model.UpdatedById = user.Id;
            _scStudentRegistrationRepository.Update(model);

            _scStudentRegistrationDetailRepository.Delete(x => x.RegMasterId == model.Id);
            foreach (var item in subjectEntry.Where(x => x.StudentId != 0))
            {
                var roll = item.RollNo;
                if (string.IsNullOrEmpty(roll))
                {
                    roll = UtilityService.GetRollNumber(model.ClassId, item.SectionId, this.CurrentBranch.Id,
                                                           this.CurrentAcademyYearId);

                }

                var dat = new ScStudentRegistrationDetail()
                {
                    CurrentStatus = item.CurrentStatus,
                    Narration = item.Narration,
                    RegMasterId = model.Id,
                    RollNo = roll,
                    SectionId = item.SectionId,
                    ShiftId = item.ShiftId,
                    StudentId = item.StudentId,
                    StudentType = item.StudentType,
                    BoaderId = item.BoaderId,
                    UserId = item.UserId

                };
                _scStudentRegistrationDetailRepository.Add(dat);
            }
            return Content("True");
        }

        public ActionResult StudentRegistrationDelete(int id)
        {
            _scStudentRegistrationRepository.Delete(x => x.Id == id && x.AcademicYearId == CurrentAcademyYearId);


            return RedirectToAction("StudentRegistrations");
        }

        public ActionResult StudentRegDetailById(int studentRegId, int pageNo = 1)
        {
            //   var data = _scexamattendancemasterRepository.GetPagedList(x => x.AcademicYearId, pageNo, this.SystemControl.PageSize);
            
            //var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            //ViewBag.SnStart = snStart;
            //return PartialView("_PartialExamAttendanceList", data);
            var student = _scStudentRegistrationDetailRepository.GetMany(x => x.RegMasterId == studentRegId).ToList();
            return PartialView(student.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
        public ActionResult GetStudentsIdForRegistrationByClassId(int id)
        {
            var academyId = base.AcademicYear.Id;
            var student =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.ClassId == id && x.StudentRegistration.AcademicYearId == academyId).
                    Select(x => x.StudentId);
            List<ScStudentinfo> lstStudent = _scStudentinfoRepository.GetMany(x => x.ClassId == id && x.AcademicYearId == academyId && !student.Contains(x.StudentID)).ToList();

            var data = lstStudent.Select(x => new
            {
                Id = x.StudentID,
                Code = x.StdCode,
                Description = x.StuDesc
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region StudentAttendances
        [CheckPermission(Permissions = "Navigate", Module = "scSA")]
        public ActionResult StudentAttendances()
        {
            var viewModel = new StudentAttendanceFormViewModel();
            viewModel.ClassList = new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "None", Value = "0" }));
            viewModel.SectionList = new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StudentAttendances(StudentAttendanceFormViewModel model, IEnumerable<ScStudentRegistrationDetail> attendanceEntry)
        {

            foreach (var item in attendanceEntry)
            {
                for (int i = 0; i <= item.Date.Count() - 1; i++)
                {
                    var check = item.CheckList[i];
                    var date = item.Date[i];


                    _attendanceLogRepository.Delete(
                        x =>

                        x.EnrollId == item.DeviceUserId && EntityFunctions.TruncateTime(x.TDate) == date);

                    if (check)
                    {
                        var data = new AttendanceLog()
                        {
                            EnrollId = item.DeviceUserId,
                            TDate = DateTime.UtcNow.Date,
                            InTime = DateTime.UtcNow.TimeOfDay


                        };
                        _attendanceLogRepository.Add(data);
                    }


                }
            }
            return null;
        }
        public ActionResult StudentAttendancesDetail(int classId, int sectionId, int year, int month)
        {

            var predicate = PredicateBuilder.True<ScStudentRegistrationDetail>();
            predicate = predicate.And(x => x.StudentRegistration.AcademicYearId == CurrentAcademyYearId);
            predicate = predicate.And(x => x.Studentinfo.DeviceUserId != 0);
            if (classId != 0)
            {
                predicate = predicate.And(x => x.StudentRegistration.ClassId == classId);
            }
            if (sectionId != 0)
            {
                predicate = predicate.And(x => x.SectionId == sectionId);
            }
            var student = _scStudentRegistrationDetailRepository.GetExpandable(predicate);
            if (year == 0)
            {
                year = DateTime.Today.Year;
            }
            if (month == 0)
            {
                month = DateTime.Today.Month;
            }
            var MonthCount = DateTime.DaysInMonth(year, month);
            var data = new StudentAttendanceDateViewModel();
            data.StudentRegistrationDetails = student.ToList();
            data.Month = month;
            data.MonthCount = MonthCount;
            data.Year = year;
            data.ClassId = classId;
            data.SectionId = sectionId;
            data.AcademicId = CurrentAcademyYearId;

            return PartialView("_PartialStudentAttendancesDetail", data);
        }


        #endregion

        #region AbsentApplications
        [CheckPermission(Permissions = "Navigate", Module = "ScAb")]
        public ActionResult AbsentApplications()
        {
            ViewBag.UserRight = base.UserRight("ScAb");
            return View();
        }
  
        public ActionResult AbsentApplicationSearch()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Yes",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "No",
                Value = "No"

            });

            List<SelectListItem> item = new List<SelectListItem>();
            item.Add(new SelectListItem
            {
                Text = "HalfDay",
                Value = "HalfDay",
                Selected = true
            });
            item.Add(new SelectListItem
            {
                Text = "FullDay",
                Value = "FullDay"

            });

            var model = new ScAbsentApplication();
            model.YesNoList = items;
            model.HalfFull = item;


            model.SectionList = new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            model.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            model.ClassList = new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            model.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));




            model.StudentList = new SelectList(_scStudentRegistrationDetailRepository.GetAll().OrderBy(x => x.Studentinfo.Class.Schedule), "StudentId", "Studentinfo.StuDesc");


            return PartialView("_PartialAbsentApplicationSearch", model);

        }


        [HttpPost]
        public ActionResult AbsentApplicationSearch(ScAbsentApplication model, int pageNo = 1)
        {

            Expression<Func<ScAbsentApplication, bool>> predicate = x => true;
            predicate = predicate.And(x => x.IsConfirm == model.IsConfirm);
            predicate = predicate.And(x => !x.IsDeleted);
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.ClassId == model.ClassId);
            }
            if (model.SectionId != 0)
            {
                predicate = predicate.And(x => x.SectionId == model.SectionId);
            }
            if (model.DateFrom.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                predicate = predicate.And(x => x.DateFrom >= model.DateFrom);
            }
            if (model.DateTo.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                predicate = predicate.And(x => x.DateTo <= model.DateTo);
            }
            var lstAbsentApplications = _scabsentapplicationRepository.GetExpandable(predicate).OrderByDescending(x => x.Id);

            // return PartialView("_PartialAbsentApplicationList", lstAbsentApplications.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
            var partialView = this.RenderPartialViewToString("_PartialAbsentApplicationList", lstAbsentApplications.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));

            var list = lstAbsentApplications.Count();
            ViewBag.SnStart = 1;

            return Json(new { view = partialView, value = list }, JsonRequestBehavior.AllowGet);


        }


        [CheckPermission(Permissions = "Create", Module = "ScAb")]
        public ActionResult AbsentApplicationAdd()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Yes",

            });
            items.Add(new SelectListItem
            {
                Text = "No",
                Value = "No",
                Selected = true
            });

            List<SelectListItem> item = new List<SelectListItem>();
            item.Add(new SelectListItem
            {
                Text = "HalfDay",
                Value = "HalfDay",
                Selected = true
            });
            item.Add(new SelectListItem
            {
                Text = "FullDay",
                Value = "FullDay"

            });

            var model = new ScAbsentApplication();
            model.YesNoList = items;
            model.HalfFull = item;
            model.ReasionList = _scabsentapplicationRepository.GetAll().Select(x => x.Reason);


            model.SectionList = new SelectList(_scSectionRepository.GetAll(), "Id", "Description").ToList();
            model.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            model.ClassList = new SelectList(_scClassRepository.GetAll(), "Id", "Description").ToList();
            model.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));

            model.StudentList = new SelectList(_scStudentRegistrationDetailRepository.GetAll(), "StudentId", "Studentinfo.StuDesc");


            List<string> reason = _scabsentapplicationRepository.GetAll().Select(x => x.Reason).Distinct().ToList();
            ViewBag.ReasonList = reason.Distinct().ToList();

            return PartialView("_PartialAbsentApplicationAdd", model);
        }


        [HttpPost]
        public ActionResult AbsentApplicationAdd(ScAbsentApplication model)
        {
            if (ModelState.IsValid)
            {
                _scabsentapplicationRepository.Add(model);
                return Content("True");
            }
            else
            {
                return Content("False");
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "ScAb")]
        public ActionResult AbsentApplicationEdit(int absentapplicationId)
        {


            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Yes",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "No",
                Value = "No"

            });

            List<SelectListItem> item = new List<SelectListItem>();
            item.Add(new SelectListItem
            {
                Text = "HalfDay",
                Value = "HalfDay",
                Selected = true
            });
            item.Add(new SelectListItem
            {
                Text = "FullDay",
                Value = "FullDay"

            });
            ScAbsentApplication objScAbsentApplication = _scabsentapplicationRepository.GetById(x => x.Id == absentapplicationId);

            objScAbsentApplication.YesNoList = items;
            objScAbsentApplication.HalfFull = item;



            objScAbsentApplication.SectionList = new SelectList(_scSectionRepository.GetAll(), "Id", "Description", objScAbsentApplication.SectionId).ToList();
            objScAbsentApplication.ClassList = new SelectList(_scClassRepository.GetAll(), "Id", "Description", objScAbsentApplication.ClassId).ToList();
            objScAbsentApplication.StudentList = new SelectList(_scStudentRegistrationDetailRepository.GetAll(), "StudentId", "Studentinfo.StuDesc", objScAbsentApplication.StudentId);

            return PartialView("_PartialAbsentApplicationEdit", objScAbsentApplication);
        }

        [HttpPost]
        public ActionResult AbsentApplicationEdit(ScAbsentApplication model)
        {
            ScAbsentApplication objScAbsentApplication = _scabsentapplicationRepository.GetById(x => x.Id == model.Id);
            objScAbsentApplication.ClassId = model.ClassId;
            objScAbsentApplication.SectionId = model.SectionId;
            objScAbsentApplication.StudentId = model.StudentId;
            objScAbsentApplication.DateFrom = model.DateFrom;
            objScAbsentApplication.MitiFrom = model.MitiFrom;
            objScAbsentApplication.DateTo = model.DateTo;
            objScAbsentApplication.MitiTo = model.MitiTo;
            objScAbsentApplication.IsConfirm = model.IsConfirm;
            objScAbsentApplication.Reason = model.Reason;
            objScAbsentApplication.Status = model.Status;
            objScAbsentApplication.IsApplication = model.IsApplication;
            objScAbsentApplication.Remarks = model.Remarks;
            _scabsentapplicationRepository.Update(objScAbsentApplication);
            return Content("True");
            //return RedirectToAction("AbsentApplicationEdit", new { absentapplicationId = objScAbsentApplication.Id });
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScAb")]
        public ActionResult AbsentApplicationList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScAb");
            var lstAbsentApplications = _scabsentapplicationRepository.GetMany(x => !x.IsDeleted).OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialAbsentApplicationList", lstAbsentApplications.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult AbsentApplicationDelete(int absentapplicationId)
        {
            //_scabsentapplicationRepository.Delete(x => x.Id == absentapplicationId);
            var data = _scabsentapplicationRepository.GetById(absentapplicationId);
            data.IsDeleted = true;
            _scabsentapplicationRepository.Update(data);
            //return JsonRequestBehavior.AllowGet

            return RedirectToAction("AbsentApplications");
        }
        #endregion

        #region Student Search

        [CheckPermission(Permissions = "Edit", Module = "STDSM")]
        public ActionResult StudentSearchMaster()
        {
            var viewModel = new StudentSearchViewModel();

            //viewModel.SrClassList = new SelectList(_scClassRepository.GetAll(), "Id", "Description").ToList();
            //viewModel.SrClassList.Insert(0, (new SelectListItem { Value = "0",Text = "All",Selected = true }));
            viewModel.SrClassList = _scClassRepository.GetAll().OrderBy(x => x.Description).ToList();
            viewModel.SrClassList.Insert(0, new SchClass { Id = 0, Description = "All" });
            viewModel.ProgramMasters = _programMasterRepository.GetAll().OrderBy(x => x.Schedule).ToList();

            return PartialView("_PartialStudentSearchMaster", viewModel);
        }
        [HttpPost]
        public ActionResult StudentFeeReceipt(int studentId, int classId, decimal dueAmount)
        {
            var usr = _authentication.GetAuthenticatedUser();
            var Source = StringEnum.Parse(typeof(ModuleEnum), "Fee Receipt").ToString();
            var documentnumber = _documentNumeringSchemeRepository.GetById(x => x.ModuleName.ToLower().Trim() == Source.ToLower().Trim());
            if (documentnumber == null)
            {
                return PartialView("_PartialDocumentNumberingError");
            }
            documentnumber.DocCurrNo += 1;
            _documentNumeringSchemeRepository.Update(documentnumber);
            var vNo = UtilityService.GetDocumentNumberingByModuleName(Source);
            var data = new ScFeeReceipt();
            data.ReceiptNo = vNo;
            data.ReceiptDate = DateTime.Now.Date;
            data.ReceiptMiti = NepaliDateService.NepaliDate(DateTime.Now).ToString();
            data.CreatedById = usr.Id;
            data.MonthList = base.SystemControl.DateType == 1 ? DropDownHelper.CreateMonthDate() : DropDownHelper.CreateMonthMiti();
            data.Month = base.SystemControl.DateType == 1 ? DateTime.Now.Month : NepaliDateService.NepaliDate(DateTime.Now).Month;
            data.GLCode = base.SystemControl.CashBook;
            data.ClassId = classId;
            data.StudentId = studentId;
            data.Class = _scClassRepository.GetById(classId);
            data.Studentinfo = _scStudentinfoRepository.GetById(studentId);
            data.ReceiptAmount = dueAmount;

            return PartialView("_PartialStudentFeeReceiptAdd", data);
        }

        public ActionResult StudentSearch()
        {

            var list = (List<StudentSearchReportViewModel>)Session["StudetSearch"] ??
                        new List<StudentSearchReportViewModel>();
           


            return View("StudentSearch", list);
        }
        [HttpPost]
        public ActionResult StudentSearch(StudentSearchViewModel viewModel)
        {

            Expression<Func<ScStudentRegistrationDetail, bool>> predicate = x => true;
            int studentId;
            predicate = predicate.And(x => x.StudentRegistration.AcademicYearId == CurrentAcademyYearId);
            if (!string.IsNullOrEmpty(viewModel.SrRegNo))
            {
                predicate = predicate.And(x => x.Studentinfo.Regno.ToLower().Trim().Contains(viewModel.SrRegNo.ToLower().Trim()));
            }
            if (!string.IsNullOrEmpty(viewModel.SrStudentName))
            {
                predicate = predicate.And(x => x.Studentinfo.StuDesc.ToLower().Trim().Contains(viewModel.SrStudentName.ToLower().Trim()));
            }
            if (viewModel.SrClassId != 0)
            {
                predicate = predicate.And(x => x.StudentRegistration.ClassId == viewModel.SrClassId);
            }
            if (!string.IsNullOrEmpty(viewModel.SrRollNo))
            {
                predicate = predicate.And(x => x.RollNo.ToLower().Trim().Contains(viewModel.SrRollNo));
            }

            var list = new List<StudentSearchReportViewModel>();

            var student = _scStudentRegistrationDetailRepository.GetExpandable(predicate);
            foreach (var item in student)
            {


                decimal Balance = 0;
                decimal BillAmount = (from b in _scBillTransactionRepository.GetMany(x => x.StudentId == item.StudentId && x.AcademicYearId == this.AcademicYear.Id)

                                      select new
                                      {
                                          Number = b.BillAmount,

                                      }).Sum(x => x.Number);
                decimal RecAmount = (from b in _scBillTransactionRepository.GetMany(x => x.StudentId == item.StudentId && x.AcademicYearId == this.AcademicYear.Id)

                                     select new
                                     {
                                         Number = b.ReceiptAmount,

                                     }).Sum(x => x.Number);
                decimal bal = BillAmount - RecAmount;

                if (bal > 0)
                {
                    Balance = Math.Abs(bal);
                }
                ViewBag.Balance = Balance;
                var data = new StudentSearchReportViewModel()
                {
                    ScStudentRegistrationDetail = item,
                    DueAmount = Balance
                };
                list.Add(data);
            }



            Session["StudentSearch"] = list;
            return View("StudentSearch", list);
        }

        public ActionResult StudentMoreDetail(int studentid)
        {
            var studentinfo = new ScStudentinfo();
            studentinfo = _scStudentinfoRepository.GetById(x => x.StudentID == studentid);
            studentinfo.AcademicBackGrounds = _academicBackgroundRepository.GetMany(x => x.StudentId == studentid);
            ViewBag.studentid = studentid;
            ViewBag.classid = studentinfo.ClassId;


            var student = _scStudentRegistrationDetailRepository.GetMany(x => x.StudentId == studentid);
            var list = new List<StudentSearchReportViewModel>();
            foreach (var item in student)
            {

                decimal Balance = 0;
                decimal BillAmount = (from b in _scBillTransactionRepository.GetMany(x => x.StudentId == item.StudentId && x.AcademicYearId == this.AcademicYear.Id)

                                      select new
                                      {
                                          Number = b.BillAmount,

                                      }).Sum(x => x.Number);
                decimal RecAmount = (from b in _scBillTransactionRepository.GetMany(x => x.StudentId == item.StudentId && x.AcademicYearId == this.AcademicYear.Id)

                                     select new
                                     {
                                         Number = b.ReceiptAmount,

                                     }).Sum(x => x.Number);
                decimal bal = BillAmount - RecAmount;

                if (bal > 0)
                {
                    Balance = Math.Abs(bal);
                }
                ViewBag.Balance = Balance;
                var data = new StudentSearchReportViewModel()
                {
                    ScStudentRegistrationDetail = item,
                    DueAmount = Balance
                };
                list.Add(data);
            }
            Session["ReportModel"] = studentinfo;
            return View("StudentMoreDetail", studentinfo);


        }

      
        public ActionResult UniversityLedgerMark(int studentid, int classid)
        {
            try
            {
var programid = _scClassRepository.GetById(x => x.Id == classid).ProgramId;
            DataContext context = new DataContext();
            var model = base.CreateReportViewModel<UniversityMarkLedgerViewModel>("University Ledger Mark", null);
                
               
            var durationid = _programMasterRepository.GetById(x => x.Id == programid).DurationId;
            model.Durationtype = Enum.GetName(typeof(DurationType), durationid);
            model.Studentinfo = _scStudentinfoRepository.GetById(x => x.StudentID == studentid);
            var sql = "Sp_GetSubjectByClassIdandStudentIdResult @ProgramId=" + programid + ",@studentId=" + studentid;
            model.SpGetSubjectByClassIdandStudentIdResult = context.Database.SqlQuery<Sp_GetSubjectByClassIdandStudentIdResult>(sql).ToList();

            model.AcademicBackgrounds = _academicBackgroundRepository.GetMany(x => x.StudentId == studentid).ToList();

            Session["UniversityMarkLedger"] = model;
            return View("_PartialUniversityMarkLedgerList", model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = "Please Generate University Ledger Mark For this Student";
                return View("_PartialErrorMessage");
            }
            
            //var partialView = View("_PartialUniversityMarkLedgerList", model);
            //return Json(new { view = partialView }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PdfUniversityLedgerMark()
        {
            var view = (UniversityMarkLedgerViewModel)Session["UniversityMarkLedger"];
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "Student/PdfUniversityLedgerMark", view, pageSize,null,35,25);
        }
        public ActionResult ExcelUniversityLedgerMark()
        {
            var view = (UniversityMarkLedgerViewModel)Session["UniversityMarkLedger"];
            //iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewExcel("", "Student/ExcelUniversityLedgerMark", view);
        }

        public ActionResult LibraryInformation(int studentid)
        {
            try
            {
  DataContext context = new DataContext();

            var viewmodel = base.CreateReportViewModel<LibraryInformationViewModel>("Library Clearance", null);
            var libraryregistrationid = _sclibrarymemberregistrationRepository.GetById(x => x.StudentId == studentid);
            viewmodel.LibraryCard = _scLibraryCardRepository.GetMany(x => x.LibraryRegistrationId == libraryregistrationid.Id);
            var sql = "SP_LibraryInformation @student=" + studentid;
            viewmodel.LibraryInformations = context.Database.SqlQuery<SP_LibraryInformation>(sql);
            var sql1 = "SP_StudentInfoHeader @StudentId=" + studentid;
            viewmodel.SP_StudentInfoHeader=context.Database.SqlQuery<SP_StudentInfoHeader>(sql1);
            viewmodel.LibraryRegistrationNumber = libraryregistrationid.RegNo;
            var sql2 = "SP_LibraryDueAmount @studentid=" + studentid;
            viewmodel.SP_LibraryDueAmount = context.Database.SqlQuery<SP_LibraryDueAmount>(sql2);


            Session["ReportModel"] = viewmodel;
            return View("_PartialLibraryInformation", viewmodel);
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Library Information";
                ViewBag.ErrMsg = "Please Generate Library Membership for this student";
                return View("_PartialErrorMessage");
            }
          
        }
        public ActionResult PdfLibraryInformation()
        {
            var view = (LibraryInformationViewModel)Session["ReportModel"];

            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            //iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));


            return this.ViewPdf("", "Student/PdfLibraryInformation", view, pageSize, null, 35, 25);
        }
        public ActionResult ExcelLibraryInformation()
        {
            var view = (LibraryInformationViewModel)Session["ReportModel"];


            //iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));


            return this.ViewExcel("", "Student/ExcelLibraryInformation", view);
        }
        public ActionResult StudentWiseMarksList(int studentid)
        {
            var viewModel = base.CreateReportViewModel<StudentGeneralInformationViewModel>(null,null);
           
            viewModel.ExamMarksGrouping = from d in _scexammarksentryRepository.GetMany(x => x.StudentId == studentid)
                                          group d by new { d.ClassId, d.ExamId, d.StudentId }
                                              into g
                                              select g.ToList();
            viewModel.ExamMarksEntries = _scexammarksentryRepository.GetMany(x => x.StudentId == studentid);
            viewModel.Division = _scDivisionRepository.GetAll();
            Session["ReportModel"] = viewModel;

            return View("_PartialStudentWiseMarksList", viewModel);
        }
        public ActionResult PdfStudentWiseMarksList()
        {
            var view = (StudentGeneralInformationViewModel)Session["ReportModel"];
           

            //iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));


            return this.ViewPdf("", "Student/PdfStudentWiseeMarksList", view);

        }
        public ActionResult ExcelStudentWiseMarksList()
        {
            var view = (StudentGeneralInformationViewModel)Session["ReportModel"];
            return this.ViewPdf("", "Student/ExcelStudentWiseMarksList", view);
        }

        public ActionResult StudentApplication(int studentid)
        {
            var context = new DataContext();
            var viewModel = base.CreateReportViewModel<StudentGeneralInformationViewModel>("Student Application",null);
            viewModel.ScAbsentApplications = _scabsentapplicationRepository.GetMany(x => x.StudentId == studentid).OrderByDescending(x => x.Id);
            viewModel.ScStudentRegistrationDetail = _scStudentRegistrationDetailRepository.GetById(x => x.StudentId == studentid);
            viewModel.Studentinfo = _scStudentinfoRepository.GetById(s => s.StudentID == studentid);
            viewModel.Section = _scSectionRepository.GetById(x => x.Id == viewModel.ScStudentRegistrationDetail.SectionId).Description;
            var sql1 = "SP_StudentInfoHeader @studentId=" + studentid;
            viewModel.SpStudentInfoHeader = context.Database.SqlQuery<SP_StudentInfoHeader>(sql1);
            Session["ReportModel"] = viewModel;
            return View("_PartialStudentApplicationList", viewModel);
        }
        public ActionResult PdfStudentApplication()
        {
            var view = (StudentGeneralInformationViewModel)Session["ReportModel"];


            //iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));


            return this.ViewPdf("", "Student/PdfStudentApplicationReport", view);

        }
        public ActionResult ExcelStudentApplication()
        {
            var view = (StudentGeneralInformationViewModel)Session["ReportModel"];
            return this.ViewExcel("", "Student/ExcelStudentApplicationReport", view);
        }
        public ActionResult StudentExtraActivity(int studentid)
        {
            var context = new DataContext();
            var viewModel = base.CreateReportViewModel<StudentGeneralInformationViewModel>("Student Extra Activity",null);
            viewModel.Studentinfo = _scStudentinfoRepository.GetById(x => x.StudentID == studentid);
          
            try
            {

                var sql = "select A.Description,S.Remarks from ScStudentExtraActivityDetails	E join ScStudentExtraActivity S on S.Id=E.MasterId join ScExtraActivity A on A.Id=S.ActivityId where E.studentid=" + studentid;
                viewModel.ExtraActivityInformations = context.Database.SqlQuery<ExtraActivityInformation>(sql).ToList();
             
                var sql1 = "SP_StudentInfoHeader @studentId=" + studentid;
                viewModel.SpStudentInfoHeader = context.Database.SqlQuery<SP_StudentInfoHeader>(sql1);
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Student Extra Activity";
               ViewBag.ErrMsg= "There is no any inforamtion related to this";
                return View("_PartialErrorMessage");
            }
            Session["ReportModel"] = viewModel;
           return View("_PartialStudentExtraActivityList", viewModel);
        }

        public ActionResult PdfStudentExtraActivity()
        {
            try
            {
 var view = (StudentGeneralInformationViewModel)Session["ReportModel"];
 return this.ViewPdf("", "Student/PdfStudentExtraActivity", view);
            }

            catch (Exception ex)
            {
                return null;
            }
         
}
        public ActionResult ExcelStudentExtraActivity()
        {
            try
            {
                var view = (StudentGeneralInformationViewModel)Session["ReportModel"];
                return this.ViewExcel("", "Student/ExcelStudentExtraActivity", view);
            }

            catch (Exception ex)
            {
                return null;
            }
            
        }
        public ActionResult StudentFeeRate(int studentid)
        {
            var viewModel = base.CreateReportViewModel<StudentGeneralInformationViewModel>("Student Fee Rate",null);
            var classid = _scStudentinfoRepository.GetById(x => x.StudentID == studentid).ClassId;
            var context = new DataContext();
            var sql = "GetStudentFeeRate @studentId=" + studentid + ",@classId=" + classid;
            viewModel.GetStudentFeeRate = context.Database.SqlQuery<GetStudentFeeRate>(sql);
            var sql1 = "SP_StudentInfoHeader @StudentId=" + studentid;
            viewModel.SpStudentInfoHeader = context.Database.SqlQuery<SP_StudentInfoHeader>(sql1);
            Session["StudentLedgerReport"] = viewModel;
            return View("_PartialStudentFeeRate", viewModel);
        }
        public ActionResult PdfStudentFeeRate()
        {
            var view = (StudentGeneralInformationViewModel)Session["StudentLedgerReport"];
            return this.ViewPdf("", "Student/PdfStudentFeeRate", view);
        }

        public ActionResult ExcelStudentFeeRate()
        {
            var view = (StudentGeneralInformationViewModel)Session["StudentLedgerReport"];
            return this.ViewExcel("", "Student/ExcelStudentFeeRate", view);
        }


        public ActionResult StudentAttendanceLog(int studentid)
        {
            DateTime DateFrom = AcademicYear.StartDate.Date;
            DateTime DateTo = AcademicYear.StartDate.AddYears(1);
         
            var startReportDate = string.Empty;
            var endReportDate = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                startReportDate = DateFrom.ToString();
                endReportDate = DateTo.ToString();
            }
            else
            {
                startReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(DateFrom)).Date;
                endReportDate = NepaliDateService.NepaliDate(Convert.ToDateTime(DateTo)).Date;
            }
            


          
            
            var viewModel = base.CreateReportViewModel<StudentGeneralInformationViewModel>("Attendance Report",
                                                                                               null);
            viewModel.Studentinfo = _scStudentinfoRepository.GetById(x => x.StudentID == studentid);
            viewModel.ScStudentRegistrationDetail= _scStudentRegistrationDetailRepository.GetById(x => x.StudentId == studentid);
            viewModel.WorkingDays = (int)(DateTo.Date - DateFrom.Date).TotalDays;
            
            var academicid = _academicyearRepository.GetById(x=>x.IsDefalut==true).Id;
            viewModel.StudentAttendances = StoredProcedureService.GetStudentAttendance(viewModel.Studentinfo.ClassId, DateFrom.ToString("yyyy/MM/dd"),
                DateTo.ToString("yyyy/MM/dd"), viewModel.ScStudentRegistrationDetail.SectionId, academicid, viewModel.WorkingDays,studentid);
            viewModel.StduentAttendacegrouping = from d in viewModel.StudentAttendances
                                                 group d by new { d.ClassId, d.SectionId }
                                                     into g
                                                     select g.ToList();
            Session["ReportModel"] = viewModel;
            return View("_PartialStudentAttendance", viewModel);
        }

        public ActionResult PdfStudentAttendanceLog()
        {
            var view = (StudentGeneralInformationViewModel)Session["ReportModel"];
            return this.ViewPdf("", "Student/PdfStudentAttendanceLog", view);
            }

        public ActionResult ExcelStudentAttendanceLog()
        {
            var view = (StudentGeneralInformationViewModel)Session["ReportModel"];
            return this.ViewExcel("", "Student/ExcelStudentAttendanceLog", view);
        }
   
        public ActionResult StudentLedger(int studentid)
        {
            var context = new DataContext();
            int count = 0;
            DateTime DateFrom = AcademicYear.StartDate.Date;
            DateTime DateTo = AcademicYear.StartDate.AddYears(1);

            
            var academicid = _academicyearRepository.GetById(x => x.IsDefalut == true).Id;
            var student = _scStudentinfoRepository.GetById(x => x.StudentID == studentid);
            var section = _scStudentRegistrationDetailRepository.GetById(x => x.StudentId == studentid).SectionId;

            var reportDate = DateFrom.ToShortDateString() + "  To  " + DateTo.ToShortDateString();
            var viewModel = base.CreateReportViewModel<ReportStudentLedgerReportViewModel>("Student Ledger", reportDate);
          
            viewModel.FYId = base.FiscalYear.Id;


            var ledgerList = UtilityService.GetStudentLedgerSumary(DateFrom.ToShortDateString(), DateTo.ToShortDateString(), student.ClassId,
                                                                   section, studentid.ToString(), academicid);
            ViewBag.fromdate = DateFrom;
            ViewBag.todate = DateTo;
            viewModel.FromDate = DateFrom;
            viewModel.ToDate=DateTo;
            count = ledgerList.Count();
            viewModel.StudentLedgerList = ledgerList;
            var sql1 = "SP_StudentInfoHeader @studentId=" + studentid;
            viewModel.SpStudentInfoHeader = context.Database.SqlQuery<SP_StudentInfoHeader>(sql1);
           // viewModel.Summary = model.Summary;
            Session["StudentLedgerReport"] = viewModel;
            return View("_PartialStudentLedgerBillDetailReport", viewModel);
        }

        public ActionResult PdfStudentLedgerReport()
        {
            
            
            var view = (ReportStudentLedgerReportViewModel)Session["StudentLedgerReport"];

            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
          

            return this.ViewPdf("", "Student/PdfStudentLedgerBillDetailReport", view, pageSize);



        }

        public ActionResult ExcelStudentLedgerReport()
        {
            var view = (ReportStudentLedgerReportViewModel)Session["StudentLedgerReport"];
          

            return this.ViewExcel("", "AcademyReport/ExcelStudentLedgerBillDetailReport", view);


        }
        public ActionResult MonthlyBill(int studentid)
        {
            var context = new DataContext();
            var startDate = _academicyearRepository.GetById(x => x.IsDefalut == true).StartDate;

            var endDate = startDate.AddYears(1);
            var reportDate = startDate.ToShortDateString() + " To " + endDate.ToShortDateString();
            var viewModel = base.CreateReportViewModel<ReportStudentMonthlyBillReportViewModel>("Student Monthly Bill",
                                                                                                reportDate);
           
            viewModel.FYId = base.FiscalYear.Id;
            viewModel.BillStudents = _scmonthlybillstudentrepository.GetMany(x => x.StudentId == studentid);
            viewModel.StudentInfo = _scStudentinfoRepository.GetById(x => x.StudentID == studentid);
            var sql1 = "SP_StudentInfoHeader @studentId=" + studentid;
            viewModel.SpStudentInfoHeader = context.Database.SqlQuery<SP_StudentInfoHeader>(sql1);
            Session["StudentLedgerReport"] = viewModel;
            return View("_PartialStudentMonthlyBillDetailReport", viewModel);
        }

        public ActionResult PdfMonthlyBill()
        {
            var view = (ReportStudentMonthlyBillReportViewModel)Session["StudentLedgerReport"];
            return this.ViewPdf("", "Student/PdfMonthlyBill", view);
        }

        public ActionResult ExcelMonthlyBill()
        {
            var view = (ReportStudentMonthlyBillReportViewModel)Session["StudentLedgerReport"];


            return this.ViewExcel("", "Student/ExcelMonthlyBill", view);


        }
        public ActionResult CashCollection(int studentid)
        {

            var context = new DataContext();
            var startReportDate = _academicyearRepository.GetById(x => x.IsDefalut == true).StartDate;
            var endReportDate = startReportDate.AddYears(1);
           
            var reportDate = startReportDate.ToShortDateString() + " To " + endReportDate.ToShortDateString();
            var viewModel = base.CreateReportViewModel<ReportStudentCashCollectionReportViewModel>("Student Cash Collection",
                                                                                                   reportDate);
           
            viewModel.FYId = base.FiscalYear.Id;
        
           
            viewModel.StudentCashCollectionList = _scbilltransactionrepository.GetMany(x=>x.StudentId==studentid);
            viewModel.StudentInfo = _scStudentinfoRepository.GetById(x => x.StudentID == studentid);
            var sql1 = "SP_StudentInfoHeader @studentId=" + studentid;
            viewModel.SP_StudentInfoHeader = context.Database.SqlQuery<SP_StudentInfoHeader>(sql1);
            Session["StudentLedgerReport"] = viewModel;
            return View("_PartialStudentCashCollectionReport", viewModel);
        }

        public ActionResult PdfCashCollection()
        {
            var view = (ReportStudentCashCollectionReportViewModel)Session["StudentLedgerReport"];
            return this.ViewPdf("", "Student/PdfCashCollection", view);
        }

        public ActionResult ExcelCashCollection()
        {
            var view = (ReportStudentCashCollectionReportViewModel)Session["StudentLedgerReport"];


            return this.ViewExcel("", "Student/ExcelCashCollection", view);


        }
        public ActionResult Certificates(int studentid)
        {
            try
            {
 var reportDate = DateTime.Now.ToShortDateString();
            var viewModel = base.CreateReportViewModel<ReportStudentCertificateReportViewModel>("Student Certificate",
                                                                                                reportDate);
         
            var temp = new  List<Template>();
            foreach (var item in _templaterepository.GetAll())
            {
                if (item.TemplateType != 3 && item.TemplateType != 4)
                {
temp.Add(BuildTemplate(item.TemplateType, studentid));
                } 

               }
            viewModel.Templates = temp;
           // ViewBag.List = templatelist;
            if (viewModel.Templates == null)
            {
                return Json(new { view = "null" }, JsonRequestBehavior.AllowGet);
            }
            Session["ReportModel"] = viewModel;
            return View("_PartialStudentCertificateReport", viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = "You have not created SLC Student Info.";
                ViewBag.Link = " <a href='/Setup/DocumentNumberingScheme' target='_blank'>Click Here To Create SLC Student Info </a>";
                return View("_PartialErrorMessage");
            }
        }
        public ActionResult PdfCertificate()
        {
           
var view = (ReportStudentCertificateReportViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "Student/PdfCertificate", view,pageSize);
            
            //catch (Exception ex)
            //{
            //    ViewBag.ErrorMsg = "You have not created SLC Student Info.";
            //    ViewBag.Link = " <a href='/Setup/DocumentNumberingScheme' target='_blank'>Click Here To Create SLC Student Info </a>";
            //    return View("_PartialErrorMessage");
            //}
        }

        public ActionResult ExcelCertificate()
        {
            var view = (ReportStudentCertificateReportViewModel)Session["ReportModel"];
           // Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));

            return this.ViewExcel("", "Student/ExcelCertificate", view);


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
                        _templaterepository.GetById(x => x.TemplateType == (int)TemplateType.CharacterCertificateSLC);
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
                    _templaterepository.GetById(x => x.TemplateType == (int)TemplateType.CharacterCertificate);
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
                _templaterepository.GetById(x => x.TemplateType == (int)TemplateType.TransferCertificate);
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
        public ActionResult PdfStudentMoreDetail()
        {
            var view = (ScStudentinfo)Session["ReportModel"];
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "Student/PdfStudentProfile1", view, pageSize, null, 35, 25);
        }

        public ActionResult CheckDOB(string date)
        {
            DateTime dob;
            try
            {


                var currentDate = DateTime.UtcNow;
                if (base.SystemControl.DateType == 1)
                {
                    dob = Convert.ToDateTime(date);
                }
                else
                {
                    dob = NepaliDateService.NepalitoEnglishDate(date);
                }
                if (dob > currentDate)
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                return Json("false", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

//        foreach (DataRow drItem in invoice.GetInvoiceRows().Rows)
//{
//   writeText(cb, "blah", left_margin, top_margin, f_cn, 10);
//   top_margin -= 12;
//   // Simpleage break function, check position
//   if(top_margin <= lastwriteposition)
//   {
//      // Okay, we need to switch page, end writing text
//      cb.EndText();
//      // Make the page break
//      document.NewPage();
//      // Start writing again on the new page
//      cb.BeginText();
//      // Assign the new write location on page two!
//      // Here you might want to implement a new row header creation
//      top_margin = 780;
//   }
//}



    }
}
