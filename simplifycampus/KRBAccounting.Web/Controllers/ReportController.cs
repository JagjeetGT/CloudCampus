using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.Academy;
using KRBAccounting.Web.ViewModels.LedgerReport;
using KRBAccounting.Web.ViewModels.Master;
using KRBAccounting.Web.ViewModels.Report;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using LinqKit;
using iTextSharp.text;

namespace KRBAccounting.Web.Controllers
{
    [Authorize]
    public class ReportController : BaseController
    {
        public DataContext _dataContext = new DataContext();
        //
        // GET: /Report/

        #region repsitoryDeclaration

        private readonly IScEmployeeCategoryRepository _scstaffgroupRepository;
        private readonly IScEmployeeInfoRepository _scstaffmasterRepository;
        private readonly IScMonthlyBillStudentRepository _scMonthlyBillStudent;
        private readonly IScClassRepository _scClassRepository;
        private readonly IScSectionRepository _scSectionRepository;
        private readonly IScStudentinfoRepository _scStudentinfoRepository;
        private readonly IScStudentRegistrationDetailRepository _scStudentRegistrationDetailRepository;
        private readonly IScStudentRegistrationRepository _scStudentRegistrationRepository;
        private readonly IScSubjectRepository _ScSubjectRepository;
        private readonly IScExamRepository _ScExamRepository;
        private readonly IScFeeItemRepository _ScFeeItemRepository;
        private readonly IScBusRepository _ScBusRepository;
        private readonly IScFeeReceiptRepository _ScFeeReceiptRepository;
        private readonly IScBoaderRepository _ScBoaderRepository;
        private readonly IScFeeTermRepository _ScFeeTermRepository;
        private readonly IScBusStopRepository _ScBusStopRepository;
        private readonly IScBusRouteDetailsRepository _ScBusRouteDetailsRepository;
        private readonly IScStudentCategoryRepository _StudentCategoryRepository;
        private readonly ISchBuildingRepository _SchBuildingRepository;
        private readonly IScClassRoomMappingRepository _ScClassRoomMappingRepository;
        private readonly IScBillTransactionRepository _scBillTransactionRepository;
        private readonly IScExamAttendanceDetailRepository _ScExamAttendanceDetailRepository;
        private readonly IScExamRoutineRepository _ScExamRoutineRepository;
        private readonly IScExamMarkSetupRepository _ScExamMarkSetupRepository;
        private readonly IScHouseMappingRepository _ScHouseMappingRepository;
        private readonly IScClassFeeRateRepository _ScClassFeeRateRepository;
        private readonly IScStudentFeeRateMasterRepository _ScStudentFeeRateMasterRepository;
        private readonly IScShiftRepository _ScShiftRepository;
        private readonly ITemplateRepository _TemplateRepository;
        private readonly IScExamMarksEntryRepository _ScExamMarksEntryRepository;
        private readonly IScStudentAttendanceRepository _ScStudentAttendanceRepository;
        private readonly IScTeacherScheduleRepository _ScTeacherScheduleRepository;
        private readonly IScClassScheduleRepository _ScClassScheduleRepository;
        private readonly IScConsolidatedMarksSetupRepository _consolidatedMarksSetupRepository;
        private readonly IScClassSubjectMappingRepository _classSubjectMappingRepository;
        private readonly IScProgramMasterRepository _scProgramMasterRepository;
        private readonly IConsolidateRepository _consolidateRepository;
        private readonly IScStudentCategoryRepository _studentCategoryRepository;
        private readonly IConsolidateDetailRepository _consolidateDetailRepository;
        private readonly IScEmployeeInfoRepository _scEmployeeInfoRepository;
        private readonly IScStudentCategoryRepository _categoryRepository;
        private readonly IStudentSlcInfoRepository _studentSlcInfoRepository;
        private readonly IScProgramMasterRepository _programMasterRepository;
        private readonly IScDivisionRepository _divisionRepository;
        private readonly IScRoomRepository _roomRepository;
        private readonly ISchBuildingRoomMappingRepository _schBuildingRoomMappingRepository;
        private readonly IScClassRoomMappingRepository _scClassRoomMappingRepository;
        private readonly IBoardExamMasterRepository _boardExamMasterRepository;
        private readonly IBoardExamDetailRepository _boardExamDetailRepository;
        private readonly IAcademicBackgroundRepository _academicBackgroundRepository;

        private readonly ISystemControlRepository _systemcontrolRepository;
        private readonly IFormsAuthenticationService _authentication;

        public int AcademicYearId;


        #endregion

        #region ConstructorDeclaration

        public ReportController(IScEmployeeCategoryRepository scStaffGroupRepository,
                                IScMonthlyBillStudentRepository scMonthlyBillStudentRepository,
                                IScEmployeeInfoRepository scStaffMasterRepository,
                                IScClassRepository scClassRepository,
                                IScSectionRepository sectionRepository,
                                IScStudentinfoRepository studentinfoRepository,
                                IScStudentRegistrationDetailRepository registrationDetailRepository,
                                IScStudentRegistrationRepository scStudentRegistrationRepository,
                                IScSubjectRepository scSubjectRepository,
                                IScExamRepository scExamRepository,
                                IScFeeItemRepository scFeeItemRepository,
                                IScBusRepository scBusRepository,
                                IScFeeReceiptRepository scFeeReceiptRepository,
                                IScBoaderRepository scBoaderRepository,
                                IScFeeTermRepository scFeeTermRepository,
                                IScBusStopRepository scBusStopRepository,
                                IScBusRouteDetailsRepository scBusRouteDetailsRepository,
                                IScStudentCategoryRepository scStudentCategoryRepository,
                                ISchBuildingRepository schBuildingRepository,
                                IScClassRoomMappingRepository scClassRoomMappingRepository,
                                IScBillTransactionRepository billTransactionRepository,
                                IScExamAttendanceDetailRepository examAttendanceDetailRepository,
                                IScExamRoutineRepository examRoutineRepository,
                                IScExamMarksEntryRepository scExamMarksEntryRepository,
                                IScStudentFeeRateMasterRepository studentFeeRateMasterRepository,
                                IScShiftRepository scShiftRepository,
                                IScTeacherScheduleRepository teacherScheduleRepository,
                                IScStudentAttendanceRepository scStudentAttendanceRepository,
                                IScConsolidatedMarksSetupRepository scConsolidatedMarksSetupRepository,
                                ITemplateRepository Template, IScClassScheduleRepository classScheduleRepository,
                                IScClassSubjectMappingRepository scClassSubjectMappingRepository,
                                IScProgramMasterRepository scProgramMasterRepository,
                                IScExamMarkSetupRepository examMarkSetupRepository,
                                IScHouseMappingRepository houseMappingRepository,
                                IConsolidateRepository consolidateRepository,
                                IScClassFeeRateRepository scClassFeeRateRepository,
            IConsolidateDetailRepository consolidateDetailRepository,
            IScProgramMasterRepository programMasterRepository,
                                IScStudentCategoryRepository categoryRepository,
            IScDivisionRepository divisionRepository,
             IScEmployeeInfoRepository scEmployeeInfoRepository,
            IStudentSlcInfoRepository studentSlcInfoRepository,
            ISystemControlRepository systemControlRepository,
            IScStudentCategoryRepository studentCategoryRepository,
            IScRoomRepository scRoomRepository,
            ISchBuildingRoomMappingRepository schBuildingRoomMappingRepository,
            IScClassRoomMappingRepository classRoomMappingRepository,
            IBoardExamMasterRepository boardExamMasterRepository,
            IBoardExamDetailRepository boardExamDetailRepository,
            IAcademicBackgroundRepository academicBackgroundRepository

            )
        {
            _academicBackgroundRepository = academicBackgroundRepository;
            _boardExamMasterRepository = boardExamMasterRepository;
            _boardExamDetailRepository = boardExamDetailRepository;
            _systemcontrolRepository = systemControlRepository;
            _ScStudentFeeRateMasterRepository = studentFeeRateMasterRepository;
            _scstaffgroupRepository = scStaffGroupRepository;
            _scstaffmasterRepository = scStaffMasterRepository;
            _ScSubjectRepository = scSubjectRepository;
            _scClassRepository = scClassRepository;
            _scSectionRepository = sectionRepository;
            _scProgramMasterRepository = scProgramMasterRepository;
            _scStudentRegistrationDetailRepository = registrationDetailRepository;
            _scStudentRegistrationRepository = scStudentRegistrationRepository;
            _scStudentinfoRepository = studentinfoRepository;
            _ScExamRepository = scExamRepository;
            _ScFeeItemRepository = scFeeItemRepository;
            _ScBusRepository = scBusRepository;
            _ScFeeReceiptRepository = scFeeReceiptRepository;
            _ScBoaderRepository = scBoaderRepository;
            _ScFeeTermRepository = scFeeTermRepository;
            _ScBusStopRepository = scBusStopRepository;
            _ScBusRouteDetailsRepository = scBusRouteDetailsRepository;
            _StudentCategoryRepository = scStudentCategoryRepository;
            _scstaffgroupRepository = scStaffGroupRepository;
            _SchBuildingRepository = schBuildingRepository;
            _ScClassRoomMappingRepository = scClassRoomMappingRepository;
            _scBillTransactionRepository = billTransactionRepository;
            AcademicYearId = AcademicYear.Id;
            _scMonthlyBillStudent = scMonthlyBillStudentRepository;
            _ScExamAttendanceDetailRepository = examAttendanceDetailRepository;
            _ScExamRoutineRepository = examRoutineRepository;
            _ScExamMarkSetupRepository = examMarkSetupRepository;
            _ScHouseMappingRepository = houseMappingRepository;
            _ScClassFeeRateRepository = scClassFeeRateRepository;
            _ScShiftRepository = scShiftRepository;
            _TemplateRepository = Template;
            _ScExamMarksEntryRepository = scExamMarksEntryRepository;
            _ScStudentAttendanceRepository = scStudentAttendanceRepository;
            _ScTeacherScheduleRepository = teacherScheduleRepository;
            _ScClassScheduleRepository = classScheduleRepository;
            _consolidatedMarksSetupRepository = scConsolidatedMarksSetupRepository;
            _classSubjectMappingRepository = scClassSubjectMappingRepository;
            _consolidateRepository = consolidateRepository;
            _categoryRepository = categoryRepository;
            _scEmployeeInfoRepository = scEmployeeInfoRepository;
            _studentSlcInfoRepository = studentSlcInfoRepository;
            _divisionRepository = divisionRepository;
            _consolidateDetailRepository = consolidateDetailRepository;
            _studentCategoryRepository = studentCategoryRepository;
            _programMasterRepository = programMasterRepository;
            _roomRepository = scRoomRepository;
            _schBuildingRoomMappingRepository = schBuildingRoomMappingRepository;
            _scClassRoomMappingRepository = classRoomMappingRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Academy);
        }


        #endregion

        #region Staff List
        [CheckPermission(Permissions = "Navigate", Module = "ScSl")]
        public ActionResult StaffList()
        {

            if (!_scEmployeeInfoRepository.GetAll().Any())
            {
                ViewBag.ErrorMsg = "You haven't added any Staff yet. In order to add a Generate Staff list, you need to add Staf first.";
                ViewBag.Link = " <a href='/EmployeeManagement/EmployeeInfos' target='_blank'>Click Here To Add Staff</a>";
                return View("_Error");
            }
            var viewModel = base.CreateViewModel<ReportStaffListFromViewModel>();
            viewModel.GroupList =
                new SelectList(_scstaffgroupRepository.GetAll().OrderBy(x => x.Description).ToList(), "Id",
                               "Description").ToList();

            viewModel.StatusList =
                new SelectList(
                    Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text").
                    ToList();
            var reglin = _scstaffmasterRepository.GetAll().GroupBy(x => x.Religion);
            viewModel.GenderList = DropDownHelper.CreateGenderDropDown();
            viewModel.ReligionList =
                new SelectList(reglin.FirstOrDefault().OrderBy(x => x.Religion), "Religion", "Religion").ToList();
            ;
            viewModel.ReligionList.Insert(0, (new SelectListItem { Text = "All", Value = "All" }));
            viewModel.StatusList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.GroupList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult StaffListReport(ReportStaffListFromViewModel model)
        {


            Expression<Func<ScEmployeeInfo, bool>> predicate = x => true;
            if (model.Gender.ToLower().Trim() != "both")
            {
                predicate = predicate.And(x => x.Gender.ToLower().Trim() == model.Gender.ToLower().Trim());
            }
            if (model.Religion.ToLower().Trim() != "all")
            {
                predicate = predicate.And(x => x.Religion.ToLower().Trim().Contains(model.Religion.ToLower().Trim()));
            }
            if (model.Status != 0)
            {
                predicate = predicate.And(x => x.Status == model.Status);
            }
            if (model.Group != 0)
            {
                predicate = predicate.And(x => x.EmployeeCategoryId == model.Group);
            }
            var viewModel = base.CreateReportViewModel<ReportStaffListReportViewModel>("Staff List");
            if (model.Gender.ToLower().Trim() == "both" && model.Religion.ToLower().Trim() == "all" && model.Status == 0 &&
                model.Group == 0)
            {
                viewModel.StaffList = _scstaffmasterRepository.GetAll();
            }
            else
            {
                viewModel.StaffList = _scstaffmasterRepository.GetExpandable(predicate);
            }
            Session["ReportModel"] = viewModel;

            var partialView = this.RenderPartialViewToString("_PartialStaffListReport", viewModel);


            return Json(new { view = partialView, value = viewModel.StaffList.Count() }, JsonRequestBehavior.AllowGet);

            // return PartialView("_PartialStaffListReport", viewModel);
        }

        public ActionResult PdfStaffListReport()
        {
            var view = (ReportStaffListReportViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "AcademyReport/PdfReportStaffListReport", view, pageSize);
        }

        public ActionResult ExcelStaffListReport()
        {
            var view = (ReportStaffListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelStaffListReport", view);
        }

        #endregion

        #region Master Reports
        [CheckPermission(Permissions = "Navigate", Module = "ScLOM")]
        public ActionResult Masters()
        {
            return View();
        }

        #region Subject

        public ActionResult SubjectList()
        {
            return View();
        }

        public ActionResult SubjectListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportSubjectListReportViewModel>("Subject List");
            viewModel.SubjectList = _ScSubjectRepository.GetAll();
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialSubjectListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.SubjectList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfSubjectListReport()
        {
            var view = (ReportSubjectListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfSubjectListReport", view);
        }

        public ActionResult ExcelSubjectListReport()
        {
            var view = (ReportSubjectListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelSubjectListReport", view);
        }

        #endregion

        #region Class

        public ActionResult ClassList()
        {
            return View();
        }

        public ActionResult ClassListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportClassListReportViewModel>("Class List");
            viewModel.ClassList = _scClassRepository.GetAll();
            Session["ClassReport"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialClassListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.ClassList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfClassListReport()
        {
            var view = (ReportClassListReportViewModel)Session["ClassReport"];
            return this.ViewPdf("", "AcademyReport/PdfClassListReport", view);
        }


        public ActionResult ExcelClassListReport()
        {
            var view = (ReportClassListReportViewModel)Session["ClassReport"];
            return this.ViewExcel("", "AcademyReport/ExcelClassListReport", view);
        }

        #endregion

        #region Section

        public ActionResult SectionList()
        {
            return View();
        }

        public ActionResult SectionListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportSectionListReportViewModel>("Section List");
            viewModel.SectionList = _scSectionRepository.GetAll();
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialSectionListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.SectionList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfSectionListReport()
        {
            var view = (ReportSectionListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfSectionList", view);
        }

        public ActionResult ExcelSectionListReport()
        {
            var view = (ReportSectionListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelSectionListReport", view);
        }

        #endregion

        #region Exam

        public ActionResult ExamList()
        {
            return View();
        }

        public ActionResult ExamListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportExaminationListReportViewModel>("Exam List");
            viewModel.ExamList = _ScExamRepository.GetAll();
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialExamListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.ExamList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfExamListReport()
        {
            var view = (ReportExaminationListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfExamList", view);
        }

        public ActionResult ExcelExamListReport()
        {
            var view = (ReportExaminationListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelExamListReport", view);
        }

        #endregion

        #region Fee

        public ActionResult Feelist()
        {
            return View();
        }

        public ActionResult FeeListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportFeeListReportViewModel>("Fee List");
            viewModel.FeeList = _ScFeeItemRepository.GetAll();
            Session["ReportModel"] = viewModel;

            var partialView = this.RenderPartialViewToString("_PartialFeeListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.FeeList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfFeeListReport()
        {
            var view = (ReportFeeListReportViewModel)Session["ReportModel"];
         
            return this.ViewPdf("", "AcademyReport/PdfFeeListReport", view);
        }

        public ActionResult ExcelFeeListReport()
        {
            var view = (ReportFeeListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelFeeListReport", view);
        }

        #endregion

        #region Bus

        public ActionResult Buslist()
        {
            return View();
        }

        public ActionResult BusListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportBusListReportViewModel>("Bus List");
            viewModel.BusList = _ScBusRepository.GetAll();
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialBusListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.BusList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfBusListReport()
        {
            var view = (ReportBusListReportViewModel)Session["ReportModel"];
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "AcademyReport/PdfBusList", view, pageSize);
        }

        public ActionResult ExcelBusListReport()
        {
            var view = (ReportBusListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelBusListReport", view);
        }

        #endregion

        #region Border

        public ActionResult Borderlist()
        {
            return View();
        }

        public ActionResult BorderListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportBoaderListReportViewModel>("Boader List");
            viewModel.BoaderList = _ScBoaderRepository.GetAll();
            Session["ReportModel"] = viewModel;

            var partialView = this.RenderPartialViewToString("_PartialBoaderListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.BoaderList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfBorderListReport()
        {
            var view = (ReportBoaderListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfBorderList", view);
        }

        public ActionResult ExcelBorderListReport()
        {
            var view = (ReportBoaderListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelBorderListReport", view);
        }

        #endregion

        #region Fee Term

        public ActionResult FeeTermlist()
        {
            return View();
        }

        public ActionResult FeeTermListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportFeeTermListReportViewModel>("Fee Term List");
            viewModel.FeetermList = _ScFeeTermRepository.GetAll();
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialFeeTermListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.FeetermList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfFeeTermListReport()
        {
            var view = (ReportFeeTermListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfFeeTermList", view);
        }

        public ActionResult ExcelFeeTermListReport()
        {
            var view = (ReportFeeTermListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelFeeTermListReport", view);
        }

        #endregion

        #region Bus Stop

        public ActionResult BusStoplist()
        {
            return View();
        }

        public ActionResult BusStopListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportBusStopListReportViewModel>("Bus Stop List");
            viewModel.BusStopsList = _ScBusStopRepository.GetAll();
            Session["ReportModel"] = viewModel;

            var partialView = this.RenderPartialViewToString("_PartialBusStopListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.BusStopsList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfBusStopListReport()
        {
            var view = (ReportBusStopListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfBusStopList", view);
        }

        public ActionResult ExcelBusStopListReport()
        {
            var view = (ReportBusStopListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelBusStopListReport", view);
        }

        #endregion

        #region BusRoute

        public ActionResult BusRoutelist()
        {
            return View();
        }

        public ActionResult BusRouteListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportBusStopRouteListReportViewModel>("Bus Stop Route List");
            viewModel.BusStopRouteList = _ScBusRouteDetailsRepository.GetAll();
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialBusRouteDetailReport", viewModel);
            return Json(new { view = partialView, value = viewModel.BusStopRouteList.Count() },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfBusRouteListReport()
        {
            var view = (ReportBusStopRouteListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfBusRouteList", view);
        }

        public ActionResult ExcelBusRouteListReport()
        {
            var view = (ReportBusStopRouteListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelBusRouteListReport", view);
        }

        #endregion

        #region StudentCategory

        public ActionResult StudentCategorylist()
        {
            return View();
        }

        public ActionResult StudentCategoryListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportStudentCategoryListReportViewModel>("Student Category List");
            viewModel.StudentCategoryList = _StudentCategoryRepository.GetAll();
            Session["ReportModel"] = viewModel;

            var partialView = this.RenderPartialViewToString("_PartialStudentCategoryReport", viewModel);
            return Json(new { view = partialView, value = viewModel.StudentCategoryList.Count() },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfStudentCategorylist()
        {
            var view = (ReportStudentCategoryListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfStudentCategorylist", view);
        }

        public ActionResult ExcelStudentCategorylistReport()
        {
            var view = (ReportStudentCategoryListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelStudentCategorylistReport", view);
        }

        #endregion

        #region StaffGroup

        public ActionResult StaffGrouplist()
        {
            return View();
        }

        public ActionResult StaffGroupListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportStaffgroupListReportViewModel>("Staff Group List");
            viewModel.StaffGroupList = _scstaffgroupRepository.GetAll();
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialStaffGroupReport", viewModel);
            return Json(new { view = partialView, value = viewModel.StaffGroupList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfStaffGroupListReport()
        {
            var view = (ReportStaffgroupListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfStaffGroupList", view);
        }

        public ActionResult ExcelStaffGroupListReport()
        {
            var view = (ReportStaffgroupListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelStaffGroupListReport", view);
        }

        #endregion

        #region Bulding

        public ActionResult Buildinglist()
        {
            return View();
        }

        public ActionResult BuildingListReport()
        {
            var viewModel = base.CreateReportViewModel<ReportBuildingListReportViewModel>("Building List");
            viewModel.BuildingGroupList = _SchBuildingRepository.GetAll();
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialBuildingReport", viewModel);
            return Json(new { view = partialView, value = viewModel.BuildingGroupList.Count() },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfBuildingListReport()
        {
            var view = (ReportBuildingListReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfBuildingList", view);
        }

        public ActionResult ExcelBuildingListReport()
        {
            var view = (ReportBuildingListReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelBuildingListReport", view);
        }

        #endregion

        //#region ClassRoom
        //public ActionResult ClassRoomlist()
        //{
        //    return View();
        //}

        //public ActionResult ClassRoomListReport()
        //{
        //    var viewModel = base.CreateReportViewModel<ReportClassRoomReportViewModel>("Building List");
        //    viewModel.ClassRoomlist = _ScClassRoomMappingRepository.GetAll();

        //    var partialView = this.RenderPartialViewToString("_PartialClassRoomReport", viewModel);
        //    return Json(new { view = partialView, value = viewModel.ClassRoomlist.Count() }, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        #endregion

        #region Student Ledger
        [CheckPermission(Permissions = "Navigate", Module = "ScSld")]
        public ActionResult StudentLedger()
        {
            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();

            var viewModel = base.CreateViewModel<ReportStudentLedgerFromViewModel>();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            ;
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.SectionList =
                new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            ;
            viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.DateFrom = base.AcademicYear.StartDate;
            viewModel.DateTo = base.AcademicYear.EndDate;
            viewModel.StudentinfoList = studentlist.GroupBy(x => x.StudentId).ToList();
            if (base.SystemControl.DateType == 1)
            {
                viewModel.DisplayDateFrom = base.AcademicYear.StartDate.ToString("MM/dd/yyyy");
                viewModel.DisplayDateTo = base.AcademicYear.EndDate.ToString("MM/dd/yyyy");

            }
            else
            {
                viewModel.DisplayDateFrom = base.AcademicYear.StartDateNep;
                viewModel.DisplayDateTo = base.AcademicYear.EndDateNep;
            }

            viewModel.SystemControl = _systemcontrolRepository.GetAll().FirstOrDefault();
            return View(viewModel);
        }

        public ActionResult GetStudentOther(int classId, int sectionId)
        {
            var studentlist = new List<ScStudentRegistrationDetail>();
            Expression<Func<ScStudentRegistrationDetail, bool>> predicate = x => true;
            predicate = predicate.And(x => x.StudentRegistration.AcademicYearId == AcademicYearId);
            if (classId != 0)
            {
                predicate = predicate.And(x => x.StudentRegistration.ClassId == classId);
            }
            if (sectionId != 0)
            {
                predicate = predicate.And(x => x.SectionId == sectionId);
            }
            if (classId == 0 && sectionId == 0)
            {


                studentlist =
                    _scStudentRegistrationDetailRepository.GetMany(
                        x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc)
                        .ToList();

            }
            else
            {
                studentlist =
                    _scStudentRegistrationDetailRepository.GetExpandable(predicate).OrderBy(x => x.Studentinfo.StuDesc).
                        ToList();
            }
            var data = studentlist.GroupBy(x => x.StudentId).ToList();
            var partialView = this.RenderPartialViewToString("_PartialStudentLedgerDetailEntry", data);
            return Json(new { view = partialView, value = data.Count() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StudentLedgerReport(ReportStudentLedgerFromViewModel model,
                                                IEnumerable<ScStudentRegistrationDetail> StudentListEntry)
        {

            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.DateFrom = Convert.ToDateTime(model.DisplayDateFrom);
                    model.MitiFrom = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateFrom)).Date;
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.DateTo = Convert.ToDateTime(model.DisplayDateTo);
                    model.MitiTo = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateTo)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.MitiFrom = model.DisplayDateFrom;
                    model.DateFrom = NepaliDateService.NepalitoEnglishDate(model.MitiFrom);
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.MitiTo = model.DisplayDateTo;
                    model.DateTo = NepaliDateService.NepalitoEnglishDate(model.MitiTo);

                }


            }

            var startDate = model.DateFrom.ToString("yyyy-MM-dd");
            var startReportDate = model.DateFrom.ToString("dd/MM/yyyy");
            var endReportDate = model.DateTo.ToString("dd/MM/yyyy");
            var endDate = model.DateTo.ToString("yyyy-MM-dd");
            var reportDate = startReportDate + "  To  " + endReportDate;
            var viewModel = base.CreateReportViewModel<ReportStudentLedgerReportViewModel>("Student Ledger", reportDate);
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.FYId = base.FiscalYear.Id;
            var studentId = "";
            var partialView = "";
            int count = 0;
            foreach (var item in StudentListEntry.Where(x => x.Checkbox))
            {
                if (studentId == "")
                {
                    studentId = item.StudentId.ToString();
                }
                else
                {
                    studentId += "," + item.StudentId.ToString();
                }
            }
            var ledgerList = UtilityService.GetStudentLedgerSumary(startDate, endDate, model.ClassId,
                                                                   model.SectionId, studentId, AcademicYearId);

        
            
            count = ledgerList.Count();
            viewModel.StudentLedgerList = ledgerList;
            viewModel.Summary = model.Summary;
            viewModel.FromDate = model.DateFrom;
            viewModel.ToDate = model.DateTo;
            Session["StudentLedgerReport"] = viewModel;


            partialView =
                this.RenderPartialViewToString(
                    model.Summary ? "_PartialStudentLedgerSummaryReport" : "_PartialStudentLedgerBillDetailReport",
                    viewModel);


            return Json(new { view = partialView, value = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfStudentLedgerReport()
        {
            var view = (ReportStudentLedgerReportViewModel)Session["StudentLedgerReport"];
           
                Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
                if (view.Summary)
                {
                    return this.ViewPdf("", "AcademyReport/PdfStudentLedgerSummaryReport", view, pageSize);

                }

                return this.ViewPdf("", "AcademyReport/PdfStudentLedgerBillDetailReport", view, pageSize);
            


        }

        public ActionResult ExcelStudentLedgerReport()
        {
            var view = (ReportStudentLedgerReportViewModel)Session["StudentLedgerReport"];
            if (view.Summary)
            {
                return this.ViewExcel("", "AcademyReport/ExcelStudentLedgerSummaryReport", view);

            }

            return this.ViewExcel("", "AcademyReport/ExcelStudentLedgerBillDetailReport", view);


        }

        #endregion

        #region  Student Cash Collection
        [CheckPermission(Permissions = "Navigate", Module = "ScSCC")]
        public ActionResult StudentCashCollection()
        {
            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();

            var viewModel = base.CreateViewModel<ReportStudentCashCollectionFromViewModel>();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            ;
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            //viewModel.SectionList = new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList(); ;
            //viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            //viewModel.DateFrom = base.AcademicYear.StartDate;
            //viewModel.DateTo = base.AcademicYear.EndDate;
            if (base.SystemControl.DateType == 1)
            {
                viewModel.DisplayDateFrom = base.AcademicYear.StartDate.ToString("MM/dd/yyyy");
                viewModel.DisplayDateTo = base.AcademicYear.EndDate.ToString("MM/dd/yyyy");

            }
            else
            {
                viewModel.DisplayDateFrom = base.AcademicYear.StartDateNep;
                viewModel.DisplayDateTo = base.AcademicYear.EndDateNep;
            }

            viewModel.StudentinfoList = studentlist.GroupBy(x => x.StudentId).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StudentCashCollectionReport(ReportStudentCashCollectionFromViewModel model,
                                                        IEnumerable<ScStudentRegistrationDetail> StudentListEntry)
        {
            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.DateFrom = Convert.ToDateTime(model.DisplayDateFrom);
                    model.MitiFrom = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateFrom)).Date;
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.DateTo = Convert.ToDateTime(model.DisplayDateTo);
                    model.MitiTo = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateTo)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.MitiFrom = model.DisplayDateFrom;
                    model.DateFrom = NepaliDateService.NepalitoEnglishDate(model.MitiFrom);
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.MitiTo = model.DisplayDateTo;
                    model.DateTo = NepaliDateService.NepalitoEnglishDate(model.MitiTo);

                }


            }


            var startDate = model.DateFrom.ToString("yyyy-MM-dd");
            var startReportDate = model.DateFrom.ToString("dd/MM/yyyy");
            var endReportDate = model.DateTo.ToString("dd/MM/yyyy");
            var endDate = model.DateTo.ToString("yyyy-MM-dd");
            var reportDate = startReportDate + " To " + endReportDate;
            var viewModel = base.CreateReportViewModel<ReportStudentCashCollectionReportViewModel>("Student Cash Collection",
                                                                                                   reportDate);
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.FYId = base.FiscalYear.Id;
            var partialView = "";
            var count = 0;
            var predicate = PredicateBuilder.True<ScBillTransaction>();
            predicate = predicate.And(x => x.Source.ToLower().Trim() == "rec");
            predicate = predicate.And(x => x.AcademicYearId == AcademicYearId);
            if (model.DateFrom.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                predicate = predicate.And(x => x.Date >= model.DateFrom);
            }
            if (model.DateTo.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                predicate = predicate.And(x => x.Date <= model.DateTo);
            }
            var predicateOr = PredicateBuilder.False<ScBillTransaction>();
            foreach (var item in StudentListEntry.Where(x => x.Checkbox))
            {
                var value = item.StudentId;
                predicateOr = predicateOr.Or(x => x.StudentId == value);

            }
            predicate = predicate.And(predicateOr.Expand());
            viewModel.StudentCashCollectionList = _scBillTransactionRepository.GetExpandable(predicate);
            Session["studentcashcollection"] = viewModel;
            partialView = this.RenderPartialViewToString("_PartialStudentCashCollectionReport", viewModel);
            return Json(new { view = partialView, value = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfStudentCashCollection()
        {
            var view = (ReportStudentCashCollectionReportViewModel)Session["studentcashcollection"];
            return this.ViewPdf("", "AcademyReport/PdfStudentCashCollection", view);
        }

        public ActionResult ExcelStudentCashCollection()
        {
            var view = (ReportStudentCashCollectionReportViewModel)Session["studentcashcollection"];
            return this.ViewExcel("", "AcademyReport/ExcelStudentCashCollection", view);
        }

        #endregion

        #region Student MonthlyBill
        [CheckPermission(Permissions = "Navigate", Module = "ScRSMB")]
        public ActionResult StudentMonthlyBill()
        {
            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();

            var viewModel = base.CreateViewModel<StudentMonthlyBillFromViewModel>();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            ;
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.SectionList =
                new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            ;
            viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            //viewModel.DateFrom = base.AcademicYear.StartDate;
            //viewModel.DateTo = base.AcademicYear.EndDate;
            if (base.SystemControl.DateType == 1)
            {
                viewModel.DisplayDateFrom = base.AcademicYear.StartDate.ToString("MM/dd/yyyy");
                viewModel.DisplayDateTo = base.AcademicYear.EndDate.ToString("MM/dd/yyyy");

            }
            else
            {
                viewModel.DisplayDateFrom = base.AcademicYear.StartDateNep;
                viewModel.DisplayDateTo = base.AcademicYear.EndDateNep;
            }


            viewModel.StudentinfoList = studentlist.GroupBy(x => x.StudentId).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StudentMonthlyBillReport(StudentMonthlyBillFromViewModel model,
                                                     IEnumerable<ScStudentRegistrationDetail> StudentListEntry)
        {

            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.DateFrom = Convert.ToDateTime(model.DisplayDateFrom);
                    model.MitiFrom = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateFrom)).Date;
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.DateTo = Convert.ToDateTime(model.DisplayDateTo);
                    model.MitiTo = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateTo)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.MitiFrom = model.DisplayDateFrom;
                    model.DateFrom = NepaliDateService.NepalitoEnglishDate(model.MitiFrom);
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.MitiTo = model.DisplayDateTo;
                    model.DateTo = NepaliDateService.NepalitoEnglishDate(model.MitiTo);

                }


            }


            var startDate = model.DateFrom.ToString("yyyy-MM-dd");
            var startReportDate = model.DateFrom.ToString("dd/MM/yyyy");
            var endReportDate = model.DateTo.ToString("dd/MM/yyyy");
            var endDate = model.DateTo.ToString("yyyy-MM-dd");
            var reportDate = startReportDate + " To " + endReportDate;
            var viewModel = base.CreateReportViewModel<ReportStudentMonthlyBillReportViewModel>("Student Monthly Bill",
                                                                                                reportDate);
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.FYId = base.FiscalYear.Id;
            var studentId = "";
            var partialView = "";
            int count = 0;


            var predicateOr = PredicateBuilder.False<ScMonthlyBillStudent>();

            predicateOr = StudentListEntry.Where(x => x.Checkbox).Select(item => item.StudentId).Aggregate(predicateOr,
                                                                                                           (current,
                                                                                                            value) =>
                                                                                                           current.Or(
                                                                                                               x =>
                                                                                                               x.
                                                                                                                   StudentId ==
                                                                                                               value));
            //foreach (var item in StudentListEntry.Where(x => x.Checkbox))
            //{

            //    predicateOr = predicateOr.Or(x => x.StudentId == item.StudentId);
            //}

            Expression<Func<ScMonthlyBillStudent, bool>> predicate = x => x.AcademicYearId == AcademicYearId;

            if (model.DateFrom.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                //    int year = model.DateFrom.Year;
                //    int month = model.DateFrom.Month;
                //    predicate = predicate.And(x => x.Year >= year && x.Month >= month);
                predicate = predicate.And(x => x.Date >= model.DateFrom);
            }
            if (model.DateTo.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                predicate = predicate.And(x => x.Date <= model.DateTo);

                //int year = model.DateTo.Year;
                //int month = model.DateTo.Month;
                //predicate = predicate.And(x => x.Year <= year && x.Month <= month);
            }

            predicate = predicate.And(predicateOr.Expand());

            viewModel.BillStudents = _scMonthlyBillStudent.GetExpandable(predicate);

            count = viewModel.BillStudents.Count();
            viewModel.Details = model.Detials;
            Session["StudentMonthlyBill"] = viewModel;
            partialView =
                this.RenderPartialViewToString(
                    model.Detials ? "_PartialStudentMonthlyBillDetailReport" : "_PartialStudentMonthlyBillSummaryReport",
                    viewModel);
            return Json(new { view = partialView, value = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfStudentMonthlyBillReport()
        {
            var view = (ReportStudentMonthlyBillReportViewModel)Session["StudentMonthlyBill"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            if (view.Details)
            {
                return this.ViewPdf("", "AcademyReport/PdfStudentMonthlyBillDetails", view, pageSize);

            }
            return this.ViewPdf("", "AcademyReport/PdfStudentMonthlyBillSummary", view, pageSize);
        }

        public ActionResult ExcelStudentMonthlyBillReport()
        {
            var view = (ReportStudentMonthlyBillReportViewModel)Session["StudentMonthlyBill"];
            if (view.Details)
            {
                return this.ViewExcel("", "AcademyReport/ExcelStudentMonthlyBillDetails", view);

            }
            return this.ViewExcel("", "AcademyReport/ExcelStudentMonthlyBillSummary", view);
        }

        #endregion

        #region Student Report
        // ScReStd
        [CheckPermission(Permissions = "Navigate", Module = "ScReStd")]
        public ActionResult Students()
        {
            return View();
        }
        public ActionResult AgeWiseReport()
        {
            var viewModel = new AgeWiseReportFormViewModel();
            viewModel.CategoryList = new SelectList(_categoryRepository.GetAll(), "Id", "Description");
            viewModel.ClassList = _scClassRepository.GetAll().OrderBy(x => x.Schedule);
            viewModel.ProgramMasters = _programMasterRepository.GetAll().OrderBy(x => x.Schedule);
            return View("StudentAgeWiseReport", viewModel);
        }
        [HttpPost]
        public ActionResult AgeWiseReport(AgeWiseReportFormViewModel model)
        {
            var con = new DataContext();
            var classid = "";
            foreach (var item in model.ClassId)
            {
                if (classid == "")
                {
                    classid = item.ToString();
                }
                else
                {
                    classid += "," + item.ToString();
                }
            }
            var categoryId = "";

            foreach (var item in model.CategoryId)
            {
                if (categoryId == "")
                {
                    categoryId = item.ToString();
                }
                else
                {
                    categoryId += "," + item.ToString();
                }

            }

            var viewModel = base.CreateReportViewModel<AgeWiseReportViewModel>();



            var sql = "SP_CategoryWise_StudentTotal @AgeList='', @CategoryList='" + categoryId +
            "'  , @ACYId=" + this.AcademicYearId + ", @ClassList='" +
            classid + "'";
            viewModel.HTMLDATA = con.Database.SqlQuery<string>(sql).FirstOrDefault();

            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialStudentAgeWiseReport", viewModel);
            return Json(new { view = partialView },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfAgeWiseReport()
        {

            var view = (AgeWiseReportViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "AcademyReport/PdfAgeWiseReport", view, pageSize);

        }
        public ActionResult ExcelAgeWiseReport()
        {

            var view = (AgeWiseReportViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewExcel("", "AcademyReport/ExcelAgeWiseReport", view);

        }


        #region Apply Student

        public ActionResult ApplyStudents()
        {
            var viewModel = new ReportApplyStudentListFormViewModel();

            viewModel.CategoryList = new SelectList(_StudentCategoryRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            viewModel.CategoryList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.GenderList = DropDownHelper.CreateGenderDropDown();


            viewModel.SystemControl = _systemcontrolRepository.GetAll().FirstOrDefault();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ApplyStudentsReport(ReportApplyStudentListFormViewModel model)
        {

            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.DateFrom = Convert.ToDateTime(model.DisplayDateFrom);
                    model.MitiFrom = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateFrom)).Date;
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.DateTo = Convert.ToDateTime(model.DisplayDateTo);
                    model.MitiTo = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateTo)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.MitiFrom = model.DisplayDateFrom;
                    model.DateFrom = NepaliDateService.NepalitoEnglishDate(model.MitiFrom);
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.MitiTo = model.DisplayDateTo;
                    model.DateTo = NepaliDateService.NepalitoEnglishDate(model.MitiTo);

                }


            }

            var predicate = PredicateBuilder.True<ScStudentinfo>();
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.ClassId == model.ClassId);
            }
            if (model.CategoryId != 0)
            {
                predicate = predicate.And(x => x.StdCategoryId == model.CategoryId);
            }
            if (model.Gender.ToLower().Trim() != "both")
            {
                predicate = predicate.And(x => x.Sex.ToLower().Trim() == model.Gender.ToLower().Trim());
            }
            if (model.DateFrom.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                predicate = predicate.And(x => x.ApplyDate >= model.DateFrom);
            }
            if (model.DateTo.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                predicate = predicate.And(x => x.ApplyDate <= model.DateTo);
            }
            var startReportDate = Convert.ToDateTime(model.DateFrom).ToString("MM/dd/yyyy");
            var endReportDate = Convert.ToDateTime(model.DateTo).ToString("MM/dd/yyyy");
            var reportDate = "";
            if (model.DateTo.ToString("MM/dd/yyyy") != "01/01/0001" && model.DateFrom.ToString("MM/dd/yyyy") == "01/01/0001")
            {
                reportDate = "Report Date To " + endReportDate;
            } if (model.DateTo.ToString("MM/dd/yyyy") == "01/01/0001" && model.DateFrom.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                reportDate = "Report Date From " + startReportDate;
            }
            if (model.DateTo.ToString("MM/dd/yyyy") != "01/01/0001" && model.DateFrom.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                reportDate = "Report Date From " + startReportDate + "To " + endReportDate;
            }
            var viewModel = base.CreateReportViewModel<ReportApplyStudentListViewModel>("Apply Student List", reportDate);
            viewModel.StudentList = _scStudentinfoRepository.GetExpandable(predicate);
            Session["ApplyStudent"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialApplyStudentListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.StudentList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfApplyStudents()
        {

            var view = (ReportApplyStudentListViewModel)Session["ApplyStudent"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "AcademyReport/PdfApplyStudentList", view, pageSize);

        }

        public ActionResult ExcelApplyStudents()
        {
            var view = (ReportApplyStudentListViewModel)Session["ApplyStudent"];

            return this.ViewExcel("", "AcademyReport/ExcelApplyStudentList", view);
        }

        #endregion

        #region Registration Student

        public ActionResult RegistrationStudents()
        {
            var viewModel = new ReportRegistrationStudentListFormViewModel();
            viewModel.CategoryList = new SelectList(_StudentCategoryRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            viewModel.CategoryList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.GenderList = DropDownHelper.CreateGenderDropDown();

            viewModel.SystemControl = _systemcontrolRepository.GetAll().FirstOrDefault();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult RegistrationStudentsReport(ReportRegistrationStudentListFormViewModel model)
        {


            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.DateFrom = Convert.ToDateTime(model.DisplayDateFrom);
                    model.MitiFrom = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateFrom)).Date;
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.DateTo = Convert.ToDateTime(model.DisplayDateTo);
                    model.MitiTo = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateTo)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.MitiFrom = model.DisplayDateFrom;
                    model.DateFrom = NepaliDateService.NepalitoEnglishDate(model.MitiFrom);
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.MitiTo = model.DisplayDateTo;
                    model.DateTo = NepaliDateService.NepalitoEnglishDate(model.MitiTo);

                }


            }
            var viewModel = base.CreateReportViewModel<ReportRegistrationStudentListViewModel>("Registered Student List");
            var predicate = PredicateBuilder.True<ScStudentRegistrationDetail>();
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.StudentRegistration.ClassId == model.ClassId);
            }
            if (model.CategoryId != 0)
            {
                predicate = predicate.And(x => x.Studentinfo.StdCategoryId == model.CategoryId);
            }
            if (model.Gender.ToLower().Trim() != "both")
            {
                predicate = predicate.And(x => x.Studentinfo.Sex.ToLower().Trim() == model.Gender.ToLower().Trim());
            }
            if (model.DateFrom.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                predicate = predicate.And(x => x.Studentinfo.ApplyDate >= model.DateFrom);
            }
            if (model.DateTo.ToString("MM/dd/yyyy") != "01/01/0001")
            {
                predicate = predicate.And(x => x.Studentinfo.ApplyDate <= model.DateTo);
            }
            viewModel.StudentList = _scStudentRegistrationDetailRepository.GetExpandable(predicate);
            Session["RegistrationStudent"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialRegistrationStudentListReport", viewModel);
            return Json(new { view = partialView, value = viewModel.StudentList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfRegistrationStudents()
        {

            var view = (ReportRegistrationStudentListViewModel)Session["RegistrationStudent"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "AcademyReport/PdfRegistrationStudent", view, pageSize);

        }

        public ActionResult ExcelRegistrationStudents()
        {
            var view = (ReportRegistrationStudentListViewModel)Session["RegistrationStudent"];

            return this.ViewExcel("", "AcademyReport/ExcelRegistrationStudent", view);
        }


        #endregion

        #region Exam Attendance

        public ActionResult ExamAttendances()
        {
            var viewModel = new ReportExamAttendanceFormViewModel();
            viewModel.SectionList = new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ExamList = new SelectList(_ScExamRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ExamAttendanceReport(ReportExamAttendanceFormViewModel model)
        {
            var viewModel = base.CreateReportViewModel<ReportExamAttendanceReportViewModel>("Exam Wise Attendances");
            var predicate = PredicateBuilder.True<ScExamAttendanceDetail>();
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.ExamAttendanceMaster.ClassId == model.ClassId);
            }
            if (model.SectionId != 0)
            {
                predicate = predicate.And(x => x.ExamAttendanceMaster.SectionId == model.SectionId);
            }
            if (model.ExamId != 0)
            {
                predicate = predicate.And(x => x.ExamAttendanceMaster.ExamId == model.ExamId);
            }
            viewModel.AttendanceDetails = _ScExamAttendanceDetailRepository.GetExpandable(predicate);
            Session["ExaminationAttendance"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialExamAttendanceReport", viewModel);
            return Json(new { view = partialView, value = viewModel.AttendanceDetails.Count() },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfExaminationAttendance()
        {

            var view = (ReportExamAttendanceReportViewModel)Session["ExaminationAttendance"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "AcademyReport/PdfExaminationAttendance", view, pageSize);

        }

        public ActionResult ExcelExaminationAttendance()
        {
            var view = (ReportExamAttendanceReportViewModel)Session["ExaminationAttendance"];

            return this.ViewExcel("", "AcademyReport/ExcelExaminationAttendance", view);
        }

        #endregion

        #region Student Wise House

        public ActionResult StudentWiseHouse()
        {
            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();

            var viewModel = base.CreateViewModel<ReportStudentLedgerFromViewModel>();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));


            if (base.SystemControl.DateType == 1)
            {
                viewModel.DisplayDateFrom = base.AcademicYear.StartDate.ToShortDateString();
                viewModel.DisplayDateTo = base.AcademicYear.EndDate.ToShortDateString();

            }
            else
            {
                viewModel.DisplayDateFrom = base.AcademicYear.StartDateNep;
                viewModel.DisplayDateTo = base.AcademicYear.EndDateNep;
            }
            viewModel.StudentinfoList = studentlist.GroupBy(x => x.StudentId).ToList();


            //if (base.SystemControl.IsCurrDate)
            //{
            //    viewModel.DateFrom = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            //    viewModel.DateTo = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;


            //}
            //viewModel.SystemControl = _systemcontrolRepository.GetAll().FirstOrDefault();


            return View(viewModel);
        }


        [HttpPost]
        public ActionResult StudentWiseHouse(ReportStudentLedgerFromViewModel model,
                                             IEnumerable<ScStudentRegistrationDetail> StudentListEntry)
        {
            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.DateFrom = Convert.ToDateTime(model.DisplayDateFrom);
                    model.MitiFrom = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateFrom)).Date;
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.DateTo = Convert.ToDateTime(model.DisplayDateTo);
                    model.MitiTo = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDateTo)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayDateFrom))
                {
                    model.MitiFrom = model.DisplayDateFrom;
                    model.DateFrom = NepaliDateService.NepalitoEnglishDate(model.MitiFrom);
                }
                if (!string.IsNullOrEmpty(model.DisplayDateTo))
                {
                    model.MitiTo = model.DisplayDateTo;
                    model.DateTo = NepaliDateService.NepalitoEnglishDate(model.MitiTo);

                }


            }


            var startDate = model.DateFrom.ToString("yyyy-MM-dd");
            var startReportDate = model.DateFrom.ToString("dd/MM/yyyy");
            var endReportDate = model.DateTo.ToString("dd/MM/yyyy");
            var endDate = model.DateTo.ToString("yyyy-MM-dd");
            var reportDate = startReportDate + " To " + endReportDate;
            var viewModel = base.CreateReportViewModel<ReportStudentHouseReportViewModel>("Student House", reportDate);
            viewModel.StartDate = startDate;
            viewModel.EndDate = endDate;
            viewModel.FYId = base.FiscalYear.Id;
            var studentId = "";
            var partialView = "";
            int count = 0;
            foreach (var item in StudentListEntry.Where(x => x.Checkbox))
            {
                if (studentId == "")
                {
                    studentId = item.StudentId.ToString();
                }
                else
                {
                    studentId += "," + item.StudentId.ToString();
                }
            }

            var ledgerList = UtilityService.GetStudentHouseMapping(startDate, endDate, model.ClassId,
                                                                   model.HouseId, studentId);


            viewModel.HouseMappingsGrouping = ledgerList.GroupBy(x => x.HouseCode);
            Session["ReportModel"] = viewModel;
            partialView = this.RenderPartialViewToString("_PartialStudentHouseMappingReport", viewModel);


            return Json(new { view = partialView, value = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfStudentHouseWise()
        {
            var view = (ReportStudentHouseReportViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "AcademyReport/PdfStudentHouseWise", view, pageSize);
        }

        public ActionResult ExcelStudentHouseWise()
        {
            var view = (ReportStudentHouseReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelStudentHouseWise", view);
        }

        #endregion

        #region Student BioData

        public ActionResult StudentBioData()
        {
            var viewModel = base.CreateViewModel<ReportStudentBioDataViewModel>();
            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.SectionList =
                new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            viewModel.StudentList =
                new SelectList(studentlist.OrderBy(x => x.StudentRegistration.ClassId).ThenBy(x => x.Studentinfo.StuDesc), "StudentId",
                               "Studentinfo.StuDesc").ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StudentBioData(ReportStudentBioDataViewModel model)
        {
            var partialView = "";
            int count = 0;
            var reportDate = DateTime.UtcNow.ToShortDateString();
            var viewModel = base.CreateReportViewModel<ReportStudentBioDataReportViewModel>("Student Bio Data",
                                                                                            reportDate);
            viewModel.StudentInfo = _scStudentinfoRepository.GetById(x => x.StudentID == model.StudentId);

            Session["ReportModel"] = viewModel;
            partialView = this.RenderPartialViewToString("_PartialStudentBioDataReport", viewModel);


            return Json(new { view = partialView, value = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfStudentBioData()
        {
            var view = (ReportStudentBioDataReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfStudentBioData", view);
        }

        public ActionResult ExcelStudentBioData()
        {
            var view = (ReportStudentBioDataReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelStudentBioData", view);
        }

        #endregion

        #region StudentIdCard

        public ActionResult StudentIdCard()
        {
            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();

            var viewModel = base.CreateViewModel<ReportStudentLedgerFromViewModel>();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.StudentinfoList = studentlist.OrderBy(x => x.Studentinfo.Class.Description).ThenBy(x => x.Studentinfo.StuDesc).GroupBy(x => x.StudentId).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StudentIdCard(ReportStudentLedgerFromViewModel model,
                                          IEnumerable<ScStudentRegistrationDetail> StudentListEntry)
        {
            var reportDate = DateTime.UtcNow.ToShortDateString();
            var clas = "All";
            if (model.ClassId != 0)
            {
                clas = _scClassRepository.GetById(model.ClassId).Description;
            }

            var reportHeader = "Student Identity Cards - For " + clas;
            var viewModel = base.CreateReportViewModel<ReportStudentIdCardaReportViewModel>(reportHeader, reportDate);
            var partialView = "";

            var predicateAnd = PredicateBuilder.True<ScStudentRegistrationDetail>();
            if (model.ClassId != 0)
            {
                predicateAnd = predicateAnd.And(x => x.StudentRegistration.ClassId == model.ClassId);
            }
            var predicateOr = PredicateBuilder.False<ScStudentRegistrationDetail>();
            foreach (var item in StudentListEntry.Where(x => x.Checkbox))
            {
                var studentid = item.StudentId;
                predicateOr = predicateOr.Or(x => x.StudentId == studentid);
            }
            predicateAnd = predicateAnd.And(predicateOr.Expand());
            var list = _scStudentRegistrationDetailRepository.GetExpandable(predicateAnd);
            viewModel.StudentRegistrationDetailList = list;
            Session["ReportModel"] = viewModel;

            partialView = this.RenderPartialViewToString("_PartialStudentIdCardReport", viewModel);
            return Json(new { view = partialView, value = viewModel.StudentRegistrationDetailList.Count() },
                        JsonRequestBehavior.AllowGet);

        }

        public ActionResult PdfStudentIdCard()
        {
            var view = (ReportStudentIdCardaReportViewModel)Session["ReportModel"];

            double width = 3.5;
            double height = 2.5;

            width = width * 72;
            height = height * 72;
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(width), Convert.ToInt32(height));

            return this.ViewPdf("", "AcademyReport/PdfStudentIdCard", view, pageSize);




        }

        public ActionResult ExcelStudentIdCard()
        {
            var view = (ReportStudentIdCardaReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelStudentIdCard", view);
        }

        #endregion
        
        #region Student Attendance

        public ActionResult StudentAttendance()
        {
            var viewModel = new ReportStudentAttendanceFormViewModel();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.SectionList =
                new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));

            if (base.SystemControl.DateType == 1)
            {
                viewModel.DateFrom = AcademicYear.StartDate.ToString("MM/dd/yyyy");
                viewModel.DateTo = AcademicYear.EndDate.ToString("MM/dd/yyyy");

            }
            else
            {

                viewModel.DateFrom =AcademicYear.StartDateNep;
                viewModel.DateTo = AcademicYear.EndDateNep;
            }
            viewModel.SystemControl = _systemcontrolRepository.GetAll().FirstOrDefault();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StudentAttendanceReport(ReportStudentAttendanceFormViewModel model)
        {
            //DateTime datefrom = AcademicYear.StartDate.Date;
            //DateTime dateTo = AcademicYear.EndDate.Date;
           
            //if (base.SystemControl.DateType == 1)
            //{
            //    datefrom = Convert.ToDateTime(model.DateFrom).Date;
            //    dateTo = Convert.ToDateTime(model.DateTo).Date;

            //}
            //else
            //{

            //    datefrom = NepaliDateService.NepalitoEnglishDate(model.DateFrom);
            //    dateTo = NepaliDateService.NepalitoEnglishDate(model.DateTo);
            //}

            DateTime DateFrom = AcademicYear.StartDate.Date;
            DateTime DateTo = AcademicYear.EndDate.Date;
            string MitiFrom = string.Empty;
            string MitiTo = string.Empty;
            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DateFrom))
                {
                    DateFrom = Convert.ToDateTime(model.DateFrom);
                    MitiFrom = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DateFrom)).Date;
                }
                if (!string.IsNullOrEmpty(model.DateTo))
                {
                    DateTo = Convert.ToDateTime(model.DateTo);
                    MitiTo = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DateTo)).Date;
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(model.DateFrom))
                {
                   MitiFrom = model.DateFrom;
                    DateFrom = NepaliDateService.NepalitoEnglishDate(model.DateFrom);
                }
                if (!string.IsNullOrEmpty(model.DateTo))
                {
                    MitiTo = model.DateTo;
                    DateTo = NepaliDateService.NepalitoEnglishDate(model.DateTo);

                }


            }


           
            var startReportDate = model.DateFrom;
            var endReportDate = model.DateTo;
          
            var reportDate = startReportDate + " To " + endReportDate;



            //var predicate = PredicateBuilder.True<ScStudentAttendance>();
            //if (model.ClassId != 0)
            //{
            //    predicate = predicate.And(x => x.ClassId == model.ClassId);
            //}
            //if (model.SectionId != 0)
            //{
            //    predicate = predicate.And(x => x.SectionId == model.ClassId);
            //}
            //if (!String.IsNullOrEmpty(model.DateFrom))
            //{
            //    predicate = predicate.And(x => x.Date >= DateFrom);
               
            //}
            //if (!String.IsNullOrEmpty(model.DateTo))
            //{
            //    predicate = predicate.And(x => x.Date <= DateTo);
                
            //}


            ////var reportDate = datefrom.ToShortDateString() + " To " + dateTo.ToShortDateString();
            //var stdAttendance = _ScStudentAttendanceRepository.GetExpandable(predicate).ToList();
            var viewModel = base.CreateReportViewModel<ReportStudentAttendanceReportViewModel>("Attendance Report",
                                                                                               reportDate);
            viewModel.WorkingDays = (int)(DateTo.Date - DateFrom.Date).TotalDays;

            viewModel.StudentAttendances = StoredProcedureService.GetStudentAttendance(model.ClassId, DateFrom.ToString("yyyy/MM/dd"),
                DateTo.ToString("yyyy/MM/dd"), model.SectionId, AcademicYearId, viewModel.WorkingDays,0);
            viewModel.StduentAttendacegrouping = from d in viewModel.StudentAttendances
                group d by new {d.ClassId, d.SectionId}
                into g
                select g.ToList();
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialStudentAttendanceReport", viewModel);
            return Json(new { view = partialView }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PdfStudentAttendanceReport()
        {
            var view = (ReportStudentAttendanceReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfStudentAttendanceReport", view);
        }

        public ActionResult ExcelStudentAttendanceReport()
        {
            var view = (ReportStudentAttendanceReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelStudentAttendanceReport", view);
        }

        #endregion

        #region StudentAdmitCard

        public ActionResult StudentAdmitCard()
        {
            var viewModel = new StudentAdmitCardForm();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "None", Value = "0" }));
            viewModel.SectionList =
                new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ExamList =
                new SelectList(_ScExamRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ExamList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StudentAdmitCard(StudentAdmitCardForm model)
        {
            var viewModel = base.CreateReportViewModel<ReportStudentAdmitCardViewModel>("Student Admit Card");

            var list =
                _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.ClassId == model.ClassId);
            var examinfo = _ScExamRepository.GetById(model.ExamId);
            viewModel.ExamDescription = examinfo.Description;

            viewModel.StudentRegistrationDetailList = list;
            Session["ExaminationAttendance"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialStudentAdmitCard", viewModel);
            return Json(new { view = partialView, value = viewModel.StudentRegistrationDetailList.Count() },
                        JsonRequestBehavior.AllowGet);

        }

        public ActionResult PdfStudentAdmitCard()
        {
            var view = (ReportStudentAdmitCardViewModel)Session["ExaminationAttendance"];

            double width = 3.5;
            double height = 2.5;

            width = width * 72;
            height = height * 72;
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(width), Convert.ToInt32(height));

            return this.ViewPdf("", "AcademyReport/PdfStudentAdmitCard", view, pageSize);




        }

        #endregion

        #endregion

        #region Exams
        [CheckPermission(Permissions = "Navigate", Module = "ScReE")]
        public ActionResult Exams()
        {
            return View();
        }

        #region Exam Routine

        public ActionResult ExamRoutine()
        {
            var viewModel = new ReportExamRoutineFormViewModel();
            viewModel.SubjectList = new SelectList(_ScSubjectRepository.GetAll(), "Id", "Description").ToList();
            viewModel.SubjectList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ExamList =
                new SelectList(_ScExamRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ExamList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ExamRoutineReport(ReportExamRoutineFormViewModel model)
        {
            var viewModel = base.CreateReportViewModel<ReportExamRoutineReportViewModel>();
            var predicate = PredicateBuilder.True<ScExamRoutine>();
            predicate = predicate.And(x => x.AcademicYearId == AcademicYearId);
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.ClassId == model.ClassId);
            }
            if (model.SubjectId != 0)
            {
                predicate = predicate.And(x => x.SubjectId == model.SubjectId);
            }
            if (model.ExamId != 0)
            {
                predicate = predicate.And(x => x.ExamId == model.ExamId);
            }
            viewModel.ExamRoutineGrouping = from d in _ScExamRoutineRepository.GetExpandable(predicate).ToList()
                                            group d by new { d.ClassId, d.ExamId }
                                                into g
                                                select g.ToList();
            viewModel.ExamRoutines = _ScExamRoutineRepository.GetExpandable(predicate);
            viewModel.ExamRoutineFooter =
                _TemplateRepository.GetById(x => x.TemplateType == (int)TemplateType.ExamRoutineFooter).Description;
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialExamRoutineReport", viewModel);
            return Json(new { view = partialView, value = viewModel.ExamRoutines.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfExamRoutineReport()
        {
            var view = (ReportExamRoutineReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfExamRoutineReport", view);
        }

        public ActionResult ExcelExamRoutineReport()
        {
            var view = (ReportExamRoutineReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelExamRoutineReport", view);
        }

        #endregion

        #region Exam Mark Setup

        public ActionResult ExamMarkSetup()
        {
            var viewModel = new ReportExamMarkSetupFormViewModel();
            viewModel.SubjectList = new SelectList(_ScSubjectRepository.GetAll(), "Id", "Description").ToList();
            viewModel.SubjectList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ExamList =
                new SelectList(_ScExamRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ExamList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ExamMarkSetupReport(ReportExamMarkSetupFormViewModel model)
        {
            var viewModel = base.CreateReportViewModel<ReportExamRoutineReportViewModel>("Exam Mark Setup");
            var predicate = PredicateBuilder.True<ScExamMarkSetup>();
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.ClassId == model.ClassId);
            }
            if (model.SubjectId != 0)
            {
                predicate = predicate.And(x => x.SubjectId == model.SubjectId);
            }
            if (model.ExamId != 0)
            {
                predicate = predicate.And(x => x.ExamId == model.ExamId);
            }
            viewModel.ExamMarkSetupGrouping = from d in _ScExamMarkSetupRepository.GetExpandable(predicate).ToList()
                                              group d by new { d.ClassId, d.ExamId }
                                                  into g
                                                  select g.ToList();
            viewModel.ExamMarkSetUps = _ScExamMarkSetupRepository.GetExpandable(predicate);
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialExamMarkSetupReport", viewModel);
            return Json(new { view = partialView, value = viewModel.ExamMarkSetUps.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfExamMarkSetupReport()
        {
            var view = (ReportExamRoutineReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfExamMarkSetupReport", view);
        }

        public ActionResult ExcelExamMarkSetupReport()
        {
            var view = (ReportExamRoutineReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelExamMarkSetupReport", view);
        }

        #endregion

        #endregion

        #region Certificates

        public ActionResult Certificates()
        {
            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();

            var viewModel = base.CreateViewModel<ReportStudentCertificateViewModel>();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.StudentList =
                new SelectList(studentlist.OrderBy(x => x.Studentinfo.Class.Schedule), "StudentId",
                               "Studentinfo.StuDesc").ToList();
            viewModel.CertificateTypeList = new SelectList(
                Enum.GetValues(typeof(CertificateTypeEnum)).Cast
                    <CertificateTypeEnum>().Select(
                        x => new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                "Value", "Text");
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Certificates(ReportStudentCertificateViewModel model)
        {
         
   var partialView = "";
            int count = 0;
            var reportDate = DateTime.Now.ToShortDateString();
            var viewModel = base.CreateReportViewModel<ReportStudentCertificateReportViewModel>("Student Certificate",
                                                                                                reportDate);
            viewModel.Template = BuildTemplate(model.Certificatetype, model.StudentId);
            if (viewModel.Template == null)
            {
                return Json(new { view = "null" }, JsonRequestBehavior.AllowGet);
            }
            Session["ReportModel"] = viewModel;
            partialView = this.RenderPartialViewToString("_PartialStudentCertificateReport", viewModel);
            return Json(new { view = partialView, value = count }, JsonRequestBehavior.AllowGet);
       
                  
         
        
        }

         

        public ActionResult PdfCertificates()
        {
            var view = (ReportStudentCertificateReportViewModel)Session["ReportModel"];

            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));

            return this.ViewPdf("", "AcademyReport/PdfCertificates", view, pageSize);
        }

        public ActionResult ExcelCertificates()
        {
            var view = (ReportStudentCertificateReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelCertificates", view);
        }

        public Template BuildTemplate(int certificatetype, int studentid)
        {
            
                var student = _scStudentRegistrationDetailRepository.GetById(x => x.StudentId == studentid);
                var date = DateTime.UtcNow.ToShortDateString();
                var session = base.AcademicYear.StartDate.ToShortDateString() + "-" +
                            base.AcademicYear.EndDate.ToShortDateString();
                var year = base.AcademicYear.StartDate.Year.ToStr();
                var studentSLCInfo = _studentSlcInfoRepository.GetById(x => x.StudentId == student.StudentId);

                if (certificatetype == (int)CertificateTypeEnum.CharacterCertificateSLC)
                {
                    if (studentSLCInfo != null)
                    {

                        var reportModel =
                            _TemplateRepository.GetById(x => x.TemplateType == (int)TemplateType.CharacterCertificateSLC);
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
                        _TemplateRepository.GetById(x => x.TemplateType == (int)TemplateType.CharacterCertificate);
                    if (studentSLCInfo != null)
                    {

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
                                        "&lt;DOBAD&gt;", "<b>" + student.Studentinfo.DBOMiti + "</b>")
                                        .Replace("&lt;symbolNo&gt;", "<b>" + studentSLCInfo.SlcSymbolNo + "</b>").Replace(
                                        "&lt;passedYearBS&gt;", "<b>" + studentSLCInfo.PassYearBS + "</b>").Replace(
                                                        "&lt;passedYearAD&gt;", "<b>" + studentSLCInfo.PassYearAD + "</b>").Replace(
                                            "&lt;division&gt;", "<b>" + studentSLCInfo.Division + "</b>");
                            reportModel.CertificateType = (int)CertificateTypeEnum.CharacterCertificate;
                            return reportModel;
                        }
                    }
                    return null;
                }

                var reportModelTransfer =
                    _TemplateRepository.GetById(x => x.TemplateType == (int)TemplateType.TransferCertificate);
                if (studentSLCInfo != null)
                {
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
                            .Replace("&lt;Year&gt;", year).Replace("&lt;LeavingDate&gt;", DateTime.UtcNow.ToShortDateString()).Replace(
                                        "&lt;passedYearBS&gt;", "<b>" + studentSLCInfo.PassYearBS + "</b>").Replace(
                                                        "&lt;passedYearAD&gt;", "<b>" + studentSLCInfo.PassYearAD + "</b>").Replace(
                                            "&lt;Division&gt;", "<b>" + studentSLCInfo.Division + "</b>");
                        reportModelTransfer.CertificateType = (int)CertificateTypeEnum.TransferCertificate;
                        return reportModelTransfer;
                    }
                }
            return null;
        }

        #endregion

        #region Result

        public ActionResult StudentCategoryConsolidateLedgerSheet()
        {
            var viewModel = new StudentCategoryConsolidateLedgerSheetViewModel();
            viewModel.ExamList =
               new SelectList(_ScExamRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ExamList.Insert(0, (new SelectListItem { Text = "[None]", Value = "0" }));
            viewModel.CategoryList = new SelectList(_studentCategoryRepository.GetAll(), "Id", "Description").ToList();
            viewModel.CategoryList.Insert(0, (new SelectListItem { Text = "[None]", Value = "0" }));
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult StudentCategoryConsolidateLedgerSheet(StudentCategoryConsolidateLedgerSheetViewModel model)
        {
            var con = new DataContext();
            var categroy = _studentCategoryRepository.GetById(x => x.Id == model.CategoryId);
            var exam = _ScExamRepository.GetById(x => x.Id == model.ExamId);
            var reportheader = "Report For " + categroy.Description + "," + exam.Description;
            var viewModel = base.CreateReportViewModel<StudentCategoryConsolidateLedgerSheetReportViewModel>(reportheader);



            var sql = "SP_Category_AggregateConsolidateMarkSheet @CategoryId=" + model.CategoryId +
            "  , @AcademicYearId=" + this.AcademicYearId + ", @ExamId=" +
            model.ExamId;
            viewModel.HTMLDATA = con.Database.SqlQuery<string>(sql).FirstOrDefault();

            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialStudentCategoryConsolidateLedgerSheetReport", viewModel);
            return Json(new { view = partialView },
                        JsonRequestBehavior.AllowGet);
        }
        public ActionResult PdfStudentCategoryConsolidateLedgerSheet()
        {

            var view = (StudentCategoryConsolidateLedgerSheetReportViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "AcademyReport/PdfStudentCategoryConsolidateLedgerSheet", view, pageSize);

        }
        public ActionResult ExcelStudentCategoryConsolidateLedgerSheet()
        {

            var view = (StudentCategoryConsolidateLedgerSheetReportViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewExcel("", "AcademyReport/ExcelStudentCategoryConsolidateLedgerSheet", view);

        }
        public ActionResult StudentCategoryLedgerSheet()
        {
            var viewModel = new StudentCategoryLedgerSheetViewModel();
            viewModel.ExamList =
               new SelectList(_ScExamRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ExamList.Insert(0, (new SelectListItem { Text = "[None]", Value = "0" }));
            viewModel.CategoryList = new SelectList(_studentCategoryRepository.GetAll(), "Id", "Description").ToList();
            viewModel.CategoryList.Insert(0, (new SelectListItem { Text = "[None]", Value = "0" }));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StudentCategoryLedgerSheet(StudentCategoryLedgerSheetViewModel model)
        {
            var con = new DataContext();
            var categroy = _studentCategoryRepository.GetById(x => x.Id == model.CategoryId);
            var exam = _ScExamRepository.GetById(x => x.Id == model.ExamId);
            var reportheader = "Report For " + categroy.Description + "," + exam.Description;
            var viewModel = base.CreateReportViewModel<StudentCategoryLedgerSheetReportViewModel>(reportheader);



            var sql = "SP_Category_AggregateMarkSheet @CategoryId=" + model.CategoryId +
            "  , @AcademicYearId=" + this.AcademicYearId + ", @ExamId=" +
            model.ExamId;
            viewModel.HTMLDATA = con.Database.SqlQuery<string>(sql).FirstOrDefault();

            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialStudentCategoryLedgerSheetReport", viewModel);
            return Json(new { view = partialView },
                        JsonRequestBehavior.AllowGet);
        }
        public ActionResult PdfStudentCategoryLedgerSheet()
        {

            var view = (StudentCategoryLedgerSheetReportViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(1000));
            return this.ViewPdf("", "AcademyReport/PdfStudentCategoryLedgerSheet", view, pageSize);

        }
        public ActionResult ExcelStudentCategoryLedgerSheet()
        {

            var view = (StudentCategoryLedgerSheetReportViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewExcel("", "AcademyReport/ExcelStudentCategoryLedgerSheet", view);

        }
        [CheckPermission(Permissions = "Navigate", Module = "ScResult")]
        public ActionResult Result()
        {

            return View();
        }



        #region MarkSheet

        public ActionResult MarkSheet()
        {
            var viewModel = new ReportMarkSheetFormViewModel();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));



            viewModel.ExamList =
                new SelectList(_ScExamRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ExamList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();
            viewModel.StudentinfoList = studentlist.GroupBy(x => x.StudentId).ToList();
            return View(viewModel);
        }



        [HttpPost]
        public ActionResult MarkSheetReport(ReportMarkSheetFormViewModel model,
                                            IEnumerable<ScStudentRegistrationDetail> StudentListEntry)
        {
            var viewModel = base.CreateReportViewModel<ReportMarkSheetReportViewModel>();
            var predicateOr = PredicateBuilder.False<ScExamMarksEntry>();

            foreach (var item in StudentListEntry.Where(x => x.Checkbox))
            {
                var value = item.StudentId;
                predicateOr = predicateOr.Or(x => x.StudentId == value);

            }
            var predicate = PredicateBuilder.True<ScExamMarksEntry>();
            predicate = predicate.And(x => x.AcademicYearId == AcademicYearId);
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.ClassId == model.ClassId);
            }
            if (model.ExamId != 0)
            {
                predicate = predicate.And(x => x.ExamId == model.ExamId);
            }

            predicate = predicate.And(predicateOr.Expand());

            viewModel.ExamMarksGrouping = from d in _ScExamMarksEntryRepository.GetExpandable(predicate).ToList()
                                          group d by new { d.ClassId, d.ExamId, d.StudentId }
                                              into g
                                              select g.ToList();
            viewModel.ExamMarksEntries = _ScExamMarksEntryRepository.GetExpandable(predicate);
           
            viewModel.Division = _divisionRepository.GetAll();
            
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialMarkSheetReport", viewModel);
            return Json(new { view = partialView, value = viewModel.ExamMarksEntries.Count() },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfMarkSheetReport()
        {
            var view = (ReportMarkSheetReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfMarkSheetReport", view);
        }

        public ActionResult ExcelMarkSheetReport()
        {
            var view = (ReportMarkSheetReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/ExcelMarkSheetReport", view);
        }

        #endregion

        #region MarkSheetLedger

        public ActionResult MarkSheetLedger()
        {
            var viewModel = new ReportMarkSheetLedgerFormViewModel();
            //viewModel.ClassList = new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            //viewModel.ClassList.Insert(0, (new SelectListItem { Text = "None", Value = "0" }));
            viewModel.ClassList = _scClassRepository.GetAll().OrderBy(x => x.Description).ToList();
            viewModel.ClassList.Insert(0, new SchClass { Id = 0, Description = "None" });
            viewModel.ProgramMasters = _scProgramMasterRepository.GetAll().OrderBy(x => x.Schedule);
            viewModel.ExamList =
                new SelectList(_ScExamRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult MarkSheetLedgerReport(ReportMarkSheetLedgerFormViewModel model)
        {
            var clas =
                _scClassRepository.GetById(model.ClassId);
            var exam = _ScExamRepository.GetById(model.ExamId);
            var reportheader = "Ledger Report For " + clas.Description + "," + exam.Description;
            var viewModel = base.CreateReportViewModel<ReportMarkSheetLedgerReportViewModel>(reportheader);
            viewModel.HTMLDATA = UtilityService.GetMarkSheetLedger(model.ClassId, model.ExamId, AcademicYearId);
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialMarkSheetLedgerReport", viewModel);
            return Json(new { view = partialView }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfMarkSheetLedgerReport()
        {
            var view = (ReportMarkSheetLedgerReportViewModel)Session["ReportModel"];
            return this.ViewPdf("", "AcademyReport/PdfMarkSheetLedger", view);
        }

        public ActionResult ExcelMarkSheetLedgerReport()
        {
            var view = (ReportMarkSheetLedgerReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelMarkSheetLedgerReport", view);
        }

        #endregion

        #endregion

        #region Fee
        [CheckPermission(Permissions = "Navigate", Module = "SCFee")]
        public ActionResult Fee()
        {
            return View();
        }

        #region ClassFeeRate

        public ActionResult ClassFeeRate()
        {
            var viewModel = new ReportClassFeeRateFromViewModel();
            viewModel.BoaderList = new SelectList(_ScBoaderRepository.GetAll(), "Id", "Description").ToList();
            viewModel.BoaderList.Insert(0, (new SelectListItem { Text = "None", Value = "0" }));
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ShiftList =
                new SelectList(_ScShiftRepository.GetAll().OrderBy(x => x.DispalyOrder), "Id", "Description").ToList();
            viewModel.ShiftList.Insert(0, (new SelectListItem { Text = "None", Value = "0" }));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ClassFeeRateReport(ReportClassFeeRateFromViewModel model)
        {
            var viewModel = base.CreateReportViewModel<ReportClassFeeRateReportViewModel>("Class Fee Rate");
            var predicate = PredicateBuilder.True<ScClassFeeRate>();
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.ClassId == model.ClassId);
            }
            if (model.BoaderId != 0)
            {
                predicate = predicate.And(x => x.BoaderId == model.BoaderId);
            }
            if (model.ShiftId != 0)
            {
                predicate = predicate.And(x => x.ShiftId == model.ShiftId);
            }
            viewModel.ClassFeeRateGrouping = from d in _ScClassFeeRateRepository.GetExpandable(predicate).ToList()
                                             group d by new { d.ClassId, d.BoaderId, d.ShiftId }
                                                 into g
                                                 select g.ToList();
            viewModel.ClassFeeRates = _ScClassFeeRateRepository.GetExpandable(predicate);
            Session["ClassFeeRate"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialClassFeeRateReport", viewModel);
            return Json(new { view = partialView, value = viewModel.ClassFeeRates.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfClassFeeRateReport()
        {
            var view = (ReportClassFeeRateReportViewModel)Session["ClassFeeRate"];
            return this.ViewPdf("", "AcademyReport/PdfClassFeeRateReport", view);
        }

        public ActionResult ExcelClassFeeRateReport()
        {
            var view = (ReportClassFeeRateReportViewModel)Session["ClassFeeRate"];
            return this.ViewExcel("", "AcademyReport/ExcelClassFeeRateReport", view);
        }

        #endregion

        public ActionResult StudentFeeRate()
        {

            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();

            var viewModel = base.CreateViewModel<ReportStudentFeeRateFormViewModel>();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            ;
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.StudentinfoList = studentlist.GroupBy(x => x.StudentId).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StudentFeeRateReport(ReportStudentFeeRateFormViewModel model,
                                                 IEnumerable<ScStudentRegistrationDetail> StudentListEntry)
        {
            var viewModel = base.CreateReportViewModel<ReportStudentFeeRateReportViewModel>("Student Fee Rate");
            var predicateOr = PredicateBuilder.False<ScStudentFeeRateMaster>();

            foreach (var item in StudentListEntry.Where(x => x.Checkbox))
            {
                var value = item.StudentId;
                predicateOr = predicateOr.Or(x => x.StudentId == value);

            }
            var predicate = PredicateBuilder.True<ScStudentFeeRateMaster>();
            predicate = predicate.And(x => x.AcademicYearId == AcademicYearId);
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.ClassId == model.ClassId);
            }
            predicate = predicate.And(predicateOr.Expand());

            viewModel.StudentFeeRateGrouping =
                from d in _ScStudentFeeRateMasterRepository.GetExpandable(predicate).ToList()
                group d by new { d.ClassId }
                    into g
                    select g.ToList();
            viewModel.StudentFeeRates = _ScStudentFeeRateMasterRepository.GetExpandable(predicate);
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialStudentFeeRateReport", viewModel);
            return Json(new { view = partialView, value = viewModel.StudentFeeRates.Count() },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfStudentFeeRateReport()
        {
            var view = (ReportStudentFeeRateReportViewModel)Session["ReportModel"];

            double width = 10;
            double height = 10;

            width = width * 72;
            height = height * 72;
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(width), Convert.ToInt32(height));

            return this.ViewPdf("", "AcademyReport/PdfStudentFeeRate", view);
        }

        public ActionResult ExcelStudentFeeRateReport()
        {
            var view = (ReportStudentFeeRateReportViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelStudentFeeRateReport", view);
        }

        #endregion

        #region Printing
        [CheckPermission(Permissions = "Navigate", Module = "ScPrint")]
        public ActionResult Printing()
        {
            return View();
        }

        #region Monthly Bill

        public ActionResult PrintingMonthlyBill()
        {
            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();

            var viewModel = base.CreateViewModel<PrintingMonthlyBillFromViewModel>();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            ;
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.SectionList =
                new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            ;
            viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.MonthList = base.SystemControl.DateType == 1
                                      ? DropDownHelper.CreateMonthDate()
                                      : DropDownHelper.CreateMonthMiti();
            viewModel.Month = Convert.ToInt32(viewModel.MonthList.FirstOrDefault().Value);
            viewModel.StudentinfoList = studentlist.GroupBy(x => x.StudentId).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PrintingMonthlyBillReport(PrintingMonthlyBillFromViewModel model,
                                                      IEnumerable<ScStudentRegistrationDetail> StudentListEntry)
        {

            var
                nepdate = NepaliDateService.NepaliDate(DateTime.Now).Date;
            var date = "";
            var endate = new DateTime();
            if (base.SystemControl.DateType == 1)
            {
                date = model.Month + "/1/" + DateTime.Now.Year;
                endate = Convert.ToDateTime(date);
                nepdate = NepaliDateService.NepaliDate(endate).Date;
            }
            else
            {
                var year = NepaliDateService.NepaliDate(DateTime.Now.Date).Year;
                nepdate = year + "/" + model.Month + "/1";

                endate = NepaliDateService.NepalitoEnglishDate(nepdate);
            }
            var viewModel = base.CreateReportViewModel<PrintMonthlyBillReportViewModel>("Monthly Bill");
            viewModel.FYId = base.FiscalYear.Id;
            var partialView = "";
            int count = 0;
            PredicateBuilder.True<ScMonthlyBillStudent>();
            var predicateOr = PredicateBuilder.False<ScMonthlyBillStudent>();
            predicateOr = StudentListEntry.Where(x => x.Checkbox).Select(item => item.StudentId).Aggregate(predicateOr,
                                                                                                           (current,
                                                                                                            value) =>
                                                                                                           current.Or(
                                                                                                               x =>
                                                                                                               x.
                                                                                                                   StudentId ==
                                                                                                               value));

            Expression<Func<ScMonthlyBillStudent, bool>> predicate = x => x.AcademicYearId == AcademicYearId;

            predicate = predicate.And(x => x.Year == endate.Year);
            predicate = predicate.And(x => x.Month == endate.Month);
            predicate = predicate.And(predicateOr.Expand());
            viewModel.BillStudents = _scMonthlyBillStudent.GetExpandable(predicate);
            count = viewModel.BillStudents.Count();
            var lst = new List<BillParent>();
            var preDue = new List<PreviousDue>();
            var company = this.CurrentBranch;
            var logo = company.LogoGuid + "_th" + company.LogoExt;
            var filePath = Server.MapPath("~/Content/Logo/" + logo);
            if (!System.IO.File.Exists(filePath))
            {
                logo = "NoImage.jpg";
            }
            var ReportHeader = new ReportHeader()
                                   {

                                       CompanyAddress =
                                           company.Address + ", " + company.City + ", " + company.State + ", " +
                                           company.Country,
                                       CompanyName = company.Name,

                                       PanNo = company.PanNo,
                                       Phone = company.PhoneNo,
                                       Email = company.Email,
                                       Logo =
                                           string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority,
                                                         Url.Content("~")) + "Content/Logo/" + logo

                                   };
            foreach (var item in viewModel.BillStudents)
            {
                decimal Balance = 0;
                decimal BillAmount = (from b in
                                          _scBillTransactionRepository.GetMany(
                                              x => x.Date < item.Date && x.StudentId == item.StudentId && x.AcademicYearId == this.AcademicYear.Id)

                                      select new
                                                 {
                                                     Number = b.BillAmount,

                                                 }).Sum(x => x.Number);
                decimal RecAmount = (from b in
                                         _scBillTransactionRepository.GetMany(
                                             x => x.Date < item.Date &&
                                             x.StudentId == item.StudentId && x.AcademicYearId == this.AcademicYear.Id)

                                     select new
                                                {
                                                    Number = b.ReceiptAmount,

                                                }).Sum(x => x.Number);
                decimal bal = BillAmount - RecAmount;

                if (bal > 0)
                {
                    Balance = Math.Abs(bal);
                }
                var student = UtilityService.GetStudentDetial(item.StudentId, AcademicYearId);
                var section = "";
                if (student.Section != null)
                {
                    section = student.Section.Description;
                }
                item.SectionName = section;
                item.RollNo = student.RollNo.ToStr();
                var bill = new BillParent();
                for (var i = 1; i <= this.SystemControl.NoOfBillPrint; i++)
                {
                    bill = new BillParent()
                               {
                                   StudentName = item.ScStudentinfo.StuDesc,
                                   Amount = item.Amount + Balance,
                                   AmountInWord =
                                       NumberToEnglish.changeCurrencyToWords((item.Amount + Balance).ToString()),
                                   BillDate = item.Miti,
                                   BillFor = NepaliDateService.LongMonth(item.MonthMiti) + "  " + item.YearMiti,
                                   BillNo = item.BillNo,
                                   ClassName = item.Class.Description,
                                   RollNo = student.RollNo,
                                   PreparedBy = item.PreparedBy.Username,
                                   SectionName = section,
                                   Total = item.MonthlyBills.Sum(x => x.FeeAmount),
                                   //Children =
                                   //    item.MonthlyBills.Where(x => x.FeeAmount != 0).Select(x => new BillChild()
                                   //                                                                   {
                                   //                                                                       Amount
                                   //                                                                           =
                                   //                                                                           x.
                                   //                                                                           FeeAmount,
                                   //                                                                       ItemName
                                   //                                                                           =
                                   //                                                                           x.
                                   //                                                                           FeeItem
                                   //                                                                           .
                                   //                                                                           Description
                                   //                                                                   }).ToList(),
                                   Children =
                                       item.MonthlyBills.Where(x => x.FeeAmount != 0).Select(x => new BillChild()
                                                                                                      {
                                                                                                          TermAmount
                                                                                                              = UtilityService.GetStudentFeeTerm(item.StudentId, x.FeeItemId, x.ScMonthlyBillStudent.AcademicYearId).Sum(k => k.LocalAmount),
                                                                                                          //x.
                                                                                                          //FeeAmount,
                                                                                                          ItemName
                                                                                                              =
                                                                                                              x.
                                                                                                              FeeItem
                                                                                                              .
                                                                                                              Description,
                                                                                                          Amount = x.FeeAmount - UtilityService.GetStudentFeeTerm(item.StudentId, x.FeeItemId, x.ScMonthlyBillStudent.AcademicYearId).Sum(k => k.LocalAmount),
                                                                                                          NetAmount = x.FeeAmount,
                                                                                                          TaxAmount = x.EducationTaxAmount,
                                                                                                          Total = x.FeeAmount + x.EducationTaxAmount
                                                                                                      }).ToList(),
                                   EducationTax = item.MonthlyBills.Sum(x => x.EducationTaxAmount),
                                   Header = ReportHeader


                               };
                    if (Balance != 0)
                    {
                        //var child = new BillChild()
                        //                {
                        //                    Amount = Balance,
                        //                    ItemName = "Due"

                        //                };
                        //bill.Children.Add(child);
                        bill.DueAmount = Balance;
                      
                    }
                }
                if (Balance != 0)
                {
                    //item.MonthlyBills.Add(new ScMonthlyBill()
                    //                          {

                    //                              FeeAmount = Balance ,
                    //                              FeeItem = new ScFeeItem()
                    //                                            {
                    //                                                Description = "Due"
                    //                                            },

                    //                          });
                    item.DueAmount = Balance;
                }
                lst.Add(bill);


            }



            Session["ReportModel"] = viewModel;
            partialView = this.RenderPartialViewToString("_PartialPrintMonthlyBillDetailReport", viewModel);
            // var parView = this.RenderPartialViewToString("_PartialPrintMonthlyBillReport", viewModel);
            var parView = this.RenderPartialViewToString("_PartialPrintMonthlyBillDonboscoReport", viewModel);
            return
                Json(
                    new
                        {
                            list = lst,
                            view = partialView,
                            parView = parView,
                            value = count,
                            dataprint = this.SystemControl.PrintDataOnly
                        }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region  Fee Receipt

        public ActionResult PrintingFeeReceipt()
        {
            var studentlist =
                _scStudentRegistrationDetailRepository.GetMany(
                    x => x.StudentRegistration.AcademicYearId == AcademicYearId).OrderBy(x => x.Studentinfo.StuDesc).
                    ToList();

            var viewModel = base.CreateViewModel<PrintingMonthlyBillFromViewModel>();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();

            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.SectionList =
                new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            ;
            viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.MonthList = base.SystemControl.DateType == 1
                                      ? DropDownHelper.CreateMonthDate()
                                      : DropDownHelper.CreateMonthMiti();
            viewModel.Month = Convert.ToInt32(viewModel.MonthList.FirstOrDefault().Value);
            viewModel.StudentinfoList = studentlist.GroupBy(x => x.StudentId).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PrintingFeeReceiptReport(PrintingMonthlyBillFromViewModel model,
                                                     IEnumerable<ScStudentRegistrationDetail> StudentListEntry)
        {

            var
           nepdate = NepaliDateService.NepaliDate(DateTime.Now).Date;
            var date = "";
            var endate = new DateTime();
            if (base.SystemControl.DateType == 1)
            {
                date = model.Month + "/1/" + DateTime.Now.Year;
                endate = Convert.ToDateTime(date);
                nepdate = NepaliDateService.NepaliDate(endate).Date;
            }
            else
            {
                var year = NepaliDateService.NepaliDate(DateTime.Now.Date).Year;
                nepdate = year + "/" + model.Month + "/1";

                endate = NepaliDateService.NepalitoEnglishDate(nepdate);
            }
            var viewModel = new PrintingFeeReceiptReportViewModel();

            var partialView = "";
            var count = 0;
            var predicate = PredicateBuilder.True<ScFeeReceipt>();

            predicate = predicate.And(x => x.AcademicYearId == AcademicYearId);

            var predicateOr = PredicateBuilder.False<ScFeeReceipt>();
            predicateOr = StudentListEntry.Where(x => x.Checkbox).Select(item => item.StudentId).Aggregate(predicateOr,
                                                                                                           (current,
                                                                                                            value) =>
                                                                                                           current.Or(
                                                                                                               x =>
                                                                                                               x.
                                                                                                                   StudentId ==
                                                                                                               value));


            predicate = predicate.And(x => x.PaidUpYear == endate.Year);
            predicate = predicate.And(x => x.PaidUpMonth == endate.Month);
            predicate = predicate.And(predicateOr.Expand());
            viewModel.ScFeeReceipts = _ScFeeReceiptRepository.GetExpandable(predicate);
            var company = this.CurrentBranch;
            var logo = company.LogoGuid + "_th" + company.LogoExt;
            var filePath = Server.MapPath("~/Content/Logo/" + logo);
            if (!System.IO.File.Exists(filePath))
            {
                logo = "NoImage.jpg";
            }
            viewModel.Header = new ReportHeader()
                                   {
                                       CompanyAddress =
                                           company.Address + ", " + company.City + ", " + company.State + ", " +
                                           company.Country,
                                       CompanyName = company.Name,

                                       PanNo = company.PanNo,
                                       Phone = company.PhoneNo,
                                       Email = company.Email,
                                       Logo =
                                           string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority,
                                                         Url.Content("~")) + "Content/Logo/" + logo

                                   };

            Session["ReportModel"] = viewModel;
            partialView = this.RenderPartialViewToString("_PartialPrintingFeeReceiptReport", viewModel);
            return Json(new { view = partialView, value = count }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region TeacherSchedule
        [CheckPermission(Permissions = "Navigate", Module = "RpTSchl")]
        public ActionResult TeacherSchedule()
        {
            var viewModel = new ReportTeacherScheduleFormViewModel();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.SectionList =
                new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Id), "Id", "Description").ToList();
            viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TeacherSchedule(ReportTeacherScheduleFormViewModel model)
        {
            var viewModel = base.CreateReportViewModel<ReportTeacherScheduleReportViewModel>();
            var predicate = PredicateBuilder.True<ScClassSchedule>();
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.ClassId == model.ClassId);
            }
            if (model.SectionId != 0)
            {
                predicate = predicate.And(x => x.SectionId == model.SectionId);
            }

            viewModel.Schedules = _ScClassScheduleRepository.GetExpandable(predicate);
            var list = from d in viewModel.Schedules
                       group d by new { d.ClassId, d.SectionId }
                           into g
                           select g.ToList();
            viewModel.ScheduleGroupByClassSection = list;
            Session["TeacherSchedule"] = viewModel;

            var partialView = this.RenderPartialViewToString("_PartialTeacherScheduleReport", viewModel);
            return Json(new { view = partialView }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfTeacherSchedule()
        {

            var view = (ReportTeacherScheduleReportViewModel)Session["TeacherSchedule"];

            return this.ViewPdf("", "AcademyReport/PdfTeacherSchedule", view);

        }

        public ActionResult ExcelTeacherSchedule()
        {
            var view = (ReportTeacherScheduleReportViewModel)Session["TeacherSchedule"];

            return this.ViewExcel("", "AcademyReport/ExcelTeacherSchedule", view);
        }

        #endregion

        #region ClassSchedule
        [CheckPermission(Permissions = "Navigate", Module = "RpCSchl")]
        public ActionResult ClassSchedule()
        {
            var viewModel = new ReportTeacherScheduleFormViewModel();
            viewModel.ClassList =
                new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.SectionList =
                new SelectList(_scSectionRepository.GetAll().OrderBy(x => x.Id), "Id", "Description").ToList();
            viewModel.SectionList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ClassSchedule(ReportTeacherScheduleFormViewModel model)
        {
            var viewModel = base.CreateReportViewModel<ReportTeacherScheduleReportViewModel>();
            var predicate = PredicateBuilder.True<ScClassSchedule>();
            if (model.ClassId != 0)
            {
                predicate = predicate.And(x => x.ClassId == model.ClassId);
            }
            if (model.SectionId != 0)
            {
                predicate = predicate.And(x => x.SectionId == model.SectionId);
            }

            viewModel.Schedules = _ScClassScheduleRepository.GetExpandable(predicate);
            var list = from d in viewModel.Schedules
                       group d by new { d.ClassId, d.SectionId }
                           into g
                           select g.ToList();
            viewModel.ScheduleGroupByClassSection = list;
            Session["TeacherSchedule"] = viewModel;

            var partialView = this.RenderPartialViewToString("_PartialClassScheduleReport", viewModel);
            return Json(new { view = partialView }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region BuildingRoomStudent

        public ActionResult BuildingStudent()
        {
            var objBuildingStudent = new BuildingStudentViewModel();

            var buildingList = _SchBuildingRepository.GetAll().ToList();

            var rooomList = _roomRepository.GetAll().ToList();

            objBuildingStudent.BuildingList = new SelectList(buildingList, "Id", "Description").ToList();
            objBuildingStudent.BuildingList.Insert(0, new SelectListItem { Text = "All", Value = "0" });


            objBuildingStudent.RoomList = new SelectList(rooomList, "Id", "Description").ToList();

            objBuildingStudent.RoomList.Insert(0, new SelectListItem { Text = "All", Value = "0" });



            return View(objBuildingStudent);
        }


        public ActionResult NewRoomList(int id)
        {


            var objBuildingStudent = new BuildingStudentViewModel();

            var newRoomList = _schBuildingRoomMappingRepository.GetMany(x => x.BuildingId == id).Select(x => new
                                                                                                               {

                                                                                                                   Id = x.RoomId,
                                                                                                                   Description = x.Room.Description

                                                                                                               });

            return Json(newRoomList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult BuildingStudentList(int roomList, int buildingList)
        {
            //BuildingStudentViewModel model = new BuildingStudentViewModel();
            var model = base.CreateReportViewModel<BuildingStudentViewModel>("Building Student List");

            DataContext context = new DataContext();


            model.StatisticsStudentBuildingCounts = context.Database.SqlQuery<StudentBuildingCount>("EXEC StudentBuildingCount @BuildingId='" + buildingList + "',@RoomId='" + roomList + "'").ToList();
            Session["BuildingStudentCount"] = model;

            var partialView = this.RenderPartialViewToString("BuildingStudentList", model);
            return Json(new { view = partialView }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PdfBuildingStudentCountReport()
        {
            var view = (BuildingStudentViewModel)Session["BuildingStudentCount"];
            return this.ViewPdf("", "AcademyReport/PdfBuildingStudentCountReport", view);
        }

        public ActionResult ExcelBuildingStudentCountReport()
        {
            var view = (BuildingStudentViewModel)Session["BuildingStudentCount"];
            return this.ViewExcel("", "AcademyReport/ExcelBuildingStudentCountReport", view);
        }
        #endregion

        #region UniversityMarkLedger

        public ActionResult UniversityMarkLedger()
        {
              List<ScStudentinfo> lstStudent = _scStudentRegistrationDetailRepository.GetAll().Select(x => x.Studentinfo).ToList();

            var data = lstStudent.Select(x => new
            {
                Id = x.StudentID,
                RegCode = x.StdCode,
                Description = x.StuDesc,
                Class = x.Class.Description,

            });
            var viewmodel = new UniversityMarkLedgerViewModel();
            viewmodel.StudentList = new SelectList(data, "Id", "Description").ToList();
            viewmodel.StudentList.Insert(0, new SelectListItem { Value = "0", Text = "None" });
            viewmodel.ProgramList = new SelectList(_programMasterRepository.GetAll(), "Id", "Description").ToList();
            viewmodel.ProgramList.Insert(0, new SelectListItem { Value = "0", Text = "None" });
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult UniversityMarkLedgerList(int studentid,int programid)
        {
           //var model = base.CreateReportViewModel<UniversityMarkLedgerViewModel>("University Mark Ledger");
            try
            {
  DataContext context = new DataContext();
         var model=new UniversityMarkLedgerViewModel();
        
            var durationid = _programMasterRepository.GetById(x => x.Id == programid).DurationId;
            model.Durationtype = Enum.GetName(typeof(DurationType), durationid);
            model.Studentinfo = _scStudentinfoRepository.GetById(x => x.StudentID == studentid);
            var sql = "EXEC Sp_GetSubjectByClassIdandStudentIdResult @ProgramId=" + programid + ",@studentId=" + studentid;
            model.SpGetSubjectByClassIdandStudentIdResult = context.Database.SqlQuery<Sp_GetSubjectByClassIdandStudentIdResult>(sql).ToList();
            model.AcademicBackgrounds = _academicBackgroundRepository.GetMany(x => x.StudentId == studentid);
           
            Session["UniversityMarkLedger"] = model;
            var partialView = this.RenderPartialViewToString("_PartialUniversityMarkLedgerList", model);
            return Json(new { view = partialView }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Please Generate University Ledger Mark for this Student";
                var partialView = this.RenderPartialViewToString("_PartialErrorMessage");
                return Json(new { view = partialView }, JsonRequestBehavior.AllowGet);
           
            }
          

           
        }

       
        public ActionResult PdfUniversityMarkLedger()
        {
            var view = (UniversityMarkLedgerViewModel)Session["UniversityMarkLedger"];
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewPdf("", "AcademyReport/PdfUniversityMarkLedger", view, pageSize, null, 35, 25);
      
        }

        public ActionResult ExcelUniversityMarkLedger()
        {
            var view = (UniversityMarkLedgerViewModel)Session["UniversityMarkLedger"];
            return this.ViewExcel("", "AcademyReport/ExcelUniversityMarkLedger", view);
        }
        #endregion

        public ActionResult PdfFeeReceipt()
        {

            var view = (PrintingFeeReceiptReportViewModel)Session["ReportModel"];


            return this.ViewPdf("", "AcademyReport/PdfPrintingFeeReceipt", view);

        }


        public ActionResult PdfMonthlyBill()
        {

            var view = (PrintMonthlyBillReportViewModel)Session["ReportModel"];

            return this.ViewPdf("", "AcademyReport/PdfMonthlyBill", view);

        }

        public ActionResult ExcelMonthlyBill()
        {

            var view = (PrintMonthlyBillReportViewModel)Session["ReportModel"];

            return this.ViewPdf("", "AcademyReport/ExcelMonthlyBill", view);

        }


        public ActionResult ExcelFeeReceipt()
        {
            var view = (PrintingFeeReceiptReportViewModel)Session["ReportModel"];

            return this.ViewExcel("", "AcademyReport/ExcelPrintingFeeReceipt", view);
        }

        public ActionResult ConsolidatedLedgerSheet()
        {
            var viewModel = new ConsolidatedLedgerSheetViewModel();
            //viewModel.ClassList = new SelectList(_scClassRepository.GetAll().OrderBy(x => x.Description), "Id", "Description").ToList();
            //viewModel.ClassList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ClassList = _scClassRepository.GetAll().OrderBy(x => x.Description).ToList();
            viewModel.ClassList.Insert(0, new SchClass { Id = 0, Description = "None" });
            viewModel.ProgramMasters = _scProgramMasterRepository.GetAll().OrderBy(x => x.Schedule);
            viewModel.ExamList =
                new SelectList(_ScExamRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ExamList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            viewModel.ConsolidatedList = new SelectList("");
            return View(viewModel);
        }

        public ActionResult GetConsolidated(int classId, int examId)
        {
            var con =
                _consolidatedMarksSetupRepository.GetMany(x => x.ClassId == classId && x.ExamId == examId).GroupBy(
                    x => x.Code).Select(x => new
                                                 {
                                                     value = x.FirstOrDefault().Code,
                                                     text = x.FirstOrDefault().Description,
                                                 });
            return Json(con, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ConsolidatedLedgerSheet(ConsolidatedLedgerSheetViewModel model)
        {

            var clas =
                _scClassRepository.GetById(model.ClassId);
            var exam = _ScExamRepository.GetById(model.ExamId);
            var reportheader = "Ledger Report For " + clas.Description + "," + exam.Description;
            var viewModel = base.CreateReportViewModel<ReportConsolidatedLedgerSheetViewModel>(reportheader);
            var _context = new DataContext();
            viewModel.HTMLDATA =
                _context.Database.SqlQuery<string>("SP_ConsolidateLedger @ClassId=" + model.ClassId + ",@ExamId=" +
                                                   model.ExamId + ",@ConsolidateCode='" + model.ConsolidatedCode +
                                                   "', @AcademicYearId=" + this.AcademicYearId).FirstOrDefault();


            var studentRank =
                  _consolidateRepository.GetMany(
                      x =>
                      x.AcademicYearId == this.AcademicYearId && x.ClassId == model.ClassId &&
                      x.ExamId == model.ExamId).Select(x => new StudentRank
                                                               {
                                                                   Rank = x.Rank,
                                                                   StudentId = x.StudentId
                                                               });
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialConsolidatedLedgerReport", viewModel);
            return Json(new { partialView = partialView, studentrank = studentRank }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfConsolidatedLedgerReport()
        {
            var view = (ReportConsolidatedLedgerSheetViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(3369.6), Convert.ToInt32(2383.2));
            return this.ViewPdf("", "AcademyReport/PdfConsolidatedLedgerReport", view, pageSize);
        }

        public ActionResult ExcelConsolidatedLedgerReport()
        {
            var view = (ReportConsolidatedLedgerSheetViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelConsolidatedLedgerReport", view);
        }

        public ActionResult GetConsolidateMarkSheet(int classId, int examId)
        {
            var data =
                _consolidateRepository.GetMany(
                    x => x.ClassId == classId && x.ExamId == examId && x.AcademicYearId == this.AcademicYearId).Select(
                        x => new
                                 {
                                     value
                                 =
                                 x
                                 .
                                 StudentId,
                                     text
                                 =
                                 x
                                 .
                                 Studentinfo
                                 .
                                 StuDesc
                                 });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsolidateMarkSheet()
        {
            var viewModel = new ConsolidateMarkSheetViewModel();

            viewModel.ClassList = _scClassRepository.GetAll().OrderBy(x => x.Description).ToList();
            viewModel.ClassList.Insert(0, new SchClass { Id = 0, Description = "None" });
            viewModel.ProgramMasters = _scProgramMasterRepository.GetAll().OrderBy(x => x.Schedule);
            viewModel.ExamList =
                new SelectList(_ScExamRepository.GetAll().OrderBy(x => x.Schedule), "Id", "Description").ToList();
            viewModel.ExamList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));

            viewModel.Studentinfos = new SelectList("");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ConsolidateMarkSheet(ConsolidateMarkSheetViewModel model)
        {


            var viewModel = base.CreateReportViewModel<ReportConsolidateMarkSheetViewModel>();



            var _context = new DataContext();
            //var str = "SP_ConsolidateMarksheet @StudentId=" + model.StudentId +", @ClassId=" + model.ClassId + ",@ExamId=" +model.ExamId + ", @AcademicYearId=" + this.AcademicYearId;
            viewModel.HTMLDATA =
                _context.Database.SqlQuery<string>("SP_ConsolidateMarksheet @StudentId=" + model.StudentId +
                                                   ", @ClassId=" + model.ClassId + ",@ExamId=" +
                                                   model.ExamId + ", @AcademicYearId=" + this.AcademicYearId).
                    FirstOrDefault();

            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialConsolidatedMarkSheetReport", viewModel);
            return Json(partialView, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PdfConsolidatedMarkSheetReport()
        {
            var view = (ReportConsolidateMarkSheetViewModel)Session["ReportModel"];

            return this.ViewPdf("", "AcademyReport/PdfConsolidatedMarkSheetReport", view);
        }

        public ActionResult ExcelConsolidatedMarkSheetReport()
        {
            var view = (ReportConsolidateMarkSheetViewModel)Session["ReportModel"];
            return this.ViewExcel("", "AcademyReport/ExcelConsolidatedMarkSheetReport", view);
        }

        //public ActionResult StudentCollectionCount()
        //{
        //    var viewModel = new ReportStudentCollectionViewModel();
        //    viewModel.CountList =
        //        new SelectList(
        //            Enum.GetValues(typeof(ReportCountList)).Cast
        //                <ReportCountList>().Select(
        //                    x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value",
        //            "Text");

        //    return View(viewModel);

        //}

        //[HttpPost]
        //public ActionResult StudentCollectionCount(ReportStudentCollectionViewModel model)
        //{
        //    var reportDate = DateTime.UtcNow.ToString("MM/dd/yyyy");
        //    var viewModel = base.CreateReportViewModel<ReportStuduentCountForView>("Student Count", reportDate);


        //    //Category

        //    if (model.Count == 1)
        //    {
        //        viewModel = base.CreateReportViewModel<ReportStuduentCountForView>("Student Count (Category)",
        //                                                                           reportDate);
        //        //viewModel.StudentCategories = _categoryRepository.GetAll();
        //        ////var countlist= new List<int>();
        //        //foreach (var item in viewModel.StudentCategories)
        //        //{

        //        //   var categoryId = item.Id;
        //        //   var studentlist = _scStudentinfoRepository.GetMany(x => x.StdCategoryId == categoryId).Count();
        //        // //countlist.Add(studentlist);
        //        //   //viewModel.Count = studentlist;
        //        //    Session["ReportModel"] = viewModel;
        //        //    Session["ReportType"] = 1;
        //        //}

        //        //var data = (from a in _dataContext.InterestPostingDetails
        //        //            join b in _dataContext.InterestPostings on a.InterestPostingId equals b.Id
        //        //            join c in _dataContext.Account on a.AccountId equals c.Id
        //        //            join d in _dataContext.AccountHolders on c.AccountHolderId equals d.AccountId
        //        //            select new InterestPostingDataViewModel()
        //        //            {
        //        //                AccountId = a.AccountId,
        //        //                AccountHolderDtl = c.AccNo + " - " + d.FullName,
        //        //                PostingDate = b.PostingDate,
        //        //                PostingMiti = b.PostingMiti,
        //        //                InterestAmount = a.InterestAmount,
        //        //                TDSAmount = a.TDSAmount,
        //        //                IsAuto = b.IsAuto //,
        //        //                //PostToAccountId = (c.InterestDepositToAccount == 0 ? a.AccountId : c.InterestDepositToAccount)
        //        //            }).AsQueryable().OrderByDescending(x => x.AccountId).ToPagedList(pageNo,
        //        //                                                                                   SystemControl.
        //        //                                                                                       PageSize);

        //        var data = _dataContext.Database.SqlQuery<SP_GetStudentCountByCategoryId>(
        //            "Exec SP_GetStudentCountByCategoryId").ToList();
        //        viewModel.StudentCategories = data;
        //        return PartialView("ReportStCountByCategory", viewModel);
        //    }

        //    //Address 
        //    if (model.Count == 2)
        //    {
        //       var listing = from studentinfo in _dataContext.ScStudentinfos
        //                             where studentinfo.PerAdd != null
        //                             join studDetail in _dataContext.ScStudentRegistrationDetails on
        //                                 studentinfo.StudentID equals studDetail.StudentId

        //                             select new
        //                                        {
        //                                            studentinfo.StudentID,
        //                                            studentinfo.PerAdd,
        //                                        };
        //        viewModel.Studentinfo = (IEnumerable<ScStudentinfo>) listing;
        //        //            var model = _db.table1
        //        //.Where(r => r.Valid > 0)
        //        //.GroupBy(r => r.GROUP) // First, group only on GROUP
        //        //.Select(r => new
        //        //{
        //        //    Group = r.Key,
        //        //    // Second, group on unique ProjectId's
        //        //    GroupCount = r.GroupBy(g => g.Project).Count()
        //        //});
        //        //            var test = new ReportStuduentCountForView();
        //        //            test = studentAddress.Where(r=>r.StudentID > 0).GroupBy(r=>r.PerAdd).Select( r=> new{ perA});
        //        return PartialView("ReportStCountByCategory", viewModel);

        //    }
        //    return null;







        //}

    }
}
