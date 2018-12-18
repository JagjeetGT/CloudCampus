using System;
using System.Collections.Generic;
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
using iTextSharp.text;

namespace KRBAccounting.Web.Controllers
{
    public class PayrollReportController : BaseController
    {
        private readonly IScEmployeeDepartmentRepository _employeeDepartmentRepository;
        private readonly IScEmployeeInfoRepository _employeeInfoRepository;
        private readonly IAcademicYearRepository _academicYear; private readonly IFormsAuthenticationService _authentication;
        private readonly IPyPaymentSlipRepository _paymentSlipRepository;
        public PayrollReportController(IPyPaymentSlipRepository paymentSlipRepository,
                
            IScEmployeeDepartmentRepository employeeDepartmentRepository , IScEmployeeInfoRepository employeeInfoRepository, IAcademicYearRepository academicYear)
        {
            _employeeDepartmentRepository = employeeDepartmentRepository;
            _employeeInfoRepository = employeeInfoRepository;
            _paymentSlipRepository = paymentSlipRepository;
            _academicYear = academicYear;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Payroll);
        }
             [CheckPermission(Permissions = "Navigate", Module = "RppyES")]
        public ActionResult EmployeeStatement()
        {
            var viewModel = base.CreateViewModel<EmployeeStatementViewModel>();
            viewModel.DepartmentList = new SelectList(_employeeDepartmentRepository.GetMany(x => x.Status), "Id",
                                                      "Description").ToList();
            viewModel.DepartmentList.Insert(0, new SelectListItem { Selected = true, Text = "--- Select Department ---", Value = "0" });

            viewModel.EmployeeList = new SelectList("", "Id", "Description");

            viewModel.ReportTypeList = new SelectList(
                 Enum.GetValues(typeof(BalanceSheetReportTypeEnum)).Cast
                     <BalanceSheetReportTypeEnum>().Select(
                         x =>
                         new SelectListItem { Text = StringEnum.GetStringValue(x), Value = ((int)x).ToString() }),
                 "Value", "Text");
            viewModel.AsOnDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            viewModel.StartDate = base.SystemControl.DateType == 1 ? AcademicYear.StartDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(AcademicYear.StartDate).Date;
            viewModel.EndDate = base.SystemControl.DateType == 1 ? AcademicYear.EndDate.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(AcademicYear.EndDate).Date;
            viewModel.SystemControl = this.SystemControl;
            viewModel.ReportType = 1;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EmployeeStatement(EmployeeStatementViewModel model)
        {
            var con = new DataContext();
            var startDate = AcademicYear.StartDate.ToString("MM/dd/yyyy");
            var endDate = AcademicYear.EndDate.ToString("MM/dd/yyyy");
            var reportDate = "";
            if (model.ReportType == 1)
            {
                endDate = model.AsOnDate;
                reportDate = "As On " + model.AsOnDate;
                if(SystemControl.DateType ==2)
                {
                    endDate = NepaliDateService.NepalitoEnglishDate(model.AsOnDate).ToString("MM/dd/yyyy");
                }
            }

            else
            {
                startDate = model.StartDate;
                endDate = model.EndDate;
                reportDate = "Report Date From " + model.StartDate + " To " + model.EndDate;
                if (SystemControl.DateType == 2)
                {
                    endDate = NepaliDateService.NepalitoEnglishDate(model.EndDate).ToString("MM/dd/yyyy");
                    startDate = NepaliDateService.NepalitoEnglishDate(model.StartDate).ToString("MM/dd/yyyy");
                }
            }
            
            var viewModel = base.CreateReportViewModel<EmployeeStatementReportViewModel>(reportDate);
            var sql = "";
            if(model.Type ==1)
            {
                sql = "SP_EmployeeSalaryStatement @EmpId=" + model.EmployeeId + ",@StartDate='" + startDate +
                      "',@EndDate='" + endDate + "',@AcademicYearId=" + this.AcademicYear.Id;
         
            }
            else
            {
               sql = "SP_EmployeeSalaryStatementDetails @EmpId=" + model.EmployeeId + ",@StartDate='" + startDate +
                    "',@EndDate='" + endDate + "',@AcademicYearId=" + this.AcademicYear.Id;
                
            }
            Session["ReportModel"] = viewModel;

             viewModel.HTMLDATA = con.Database.SqlQuery<string>(sql).FirstOrDefault();
            var partialView = this.RenderPartialViewToString("_PartialEmployeeSatementReport", viewModel);
            return Json(new { view = partialView },
                        JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetPayrollEmployee(int departmentId)
        {
            var employee = _employeeInfoRepository.GetMany(x => x.EmployeeDepartmentId == departmentId).Select(x => new
            {
                Value = x.Id,
                Text = x.Description
            });


          

            return Json(employee, JsonRequestBehavior.AllowGet);

        }
         [CheckPermission(Permissions = "Navigate", Module = "RppyPS")]
        public ActionResult PaymentSlips()
        {
          
            var viewModel = base.CreateViewModel<PayementSlipReportFormViewModel>();
           
            viewModel.MonthList = base.SystemControl.DateType == 1
                                      ? DropDownHelper.CreateMonthDate()
                                      : DropDownHelper.CreateMonthMiti();
            viewModel.Month = Convert.ToInt32(viewModel.MonthList.FirstOrDefault().Value);
            viewModel.DepartmentList = new SelectList(_employeeDepartmentRepository.GetMany(x => x.Status), "Id",
                                                     "Description").ToList();
            viewModel.DepartmentList.Insert(0, new SelectListItem { Selected = true, Text = "--- Select All Department  ---", Value = "0" });

            viewModel.EmployeeList = _employeeInfoRepository.GetAll();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult PaymentSlips(PayementSlipReportFormViewModel model, List<ScEmployeeInfo> EmployeeListEntry)
        {
            var month = model.Month;
            if (base.SystemControl.DateType ==2)
            {
                var date = DateTime.Now.Year + "/" + model.Month + "/1/";

                month = NepaliDateService.NepalitoEnglishDate(date).Month;

                
            }
            


            var predicate = PredicateBuilder.True<PyPaymentSlip>();
            predicate = predicate.And(x => x.Month == month);
            predicate = predicate.And(x => x.Year == DateTime.Now.Year);


            var predicateOr = PredicateBuilder.False<PyPaymentSlip>();

            predicateOr = EmployeeListEntry.Where(x=>x.Checkbox).Select(item => item.Id).Aggregate(predicateOr, (current, id) => current.Or(x => x.EmployeeId == id));

            predicate = predicate.And(predicateOr.Expand());
            var data = _paymentSlipRepository.GetExpandable(predicate);
            var viewModel = base.CreateReportViewModel<PaymentSlipReportViewModel>();
            viewModel.PaymentSlips = data;
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialPaymentSlipReport", viewModel);
            return Json(new { view = partialView },
                        JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEmployeeList(int departmentId)
        {
            var employee  = _employeeInfoRepository.GetMany(x => x.EmployeeDepartmentId == departmentId);
            if(departmentId ==0)
            {
                employee = _employeeInfoRepository.GetAll();
            }
             
            var partialView = this.RenderPartialViewToString("EmployeeList", employee);
            return Json(new { view = partialView },
                        JsonRequestBehavior.AllowGet);
        }
        public ActionResult PdfEmployeeStatement()
        {

            var view = (EmployeeStatementReportViewModel)Session["ReportModel"];
            // Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(1000));
            return this.ViewPdf("", "PayrollReport/PdfEmployeeStatement", view);

        }
        public ActionResult ExcelEmployeeStatement()
        {

            var view = (EmployeeStatementReportViewModel)Session["ReportModel"];
            Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewExcel("", "PayrollReport/ExcelEmployeeStatement", view);

        }

        public ActionResult PdfPaymentSlip()
        {

            var view = (PaymentSlipReportViewModel)Session["ReportModel"];
            // Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(1000));
            return this.ViewPdf("", "PayrollReport/PdfPaymentSlipReport", view);

        }
        public ActionResult ExcelPaymentSlip()
        {

            var view = (PaymentSlipReportViewModel)Session["ReportModel"];
           // Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewExcel("", "PayrollReport/ExcelPaymentSlipReport", view);

        }
         [CheckPermission(Permissions = "Navigate", Module = "RppySS")]
        public ActionResult SalarySlips()
        {
            var viewModel = base.CreateViewModel<PayementSlipViewModel>();
         

            viewModel.MonthList = base.SystemControl.DateType == 1
                                      ? DropDownHelper.CreateMonthDate()
                                      : DropDownHelper.CreateMonthMiti();
            viewModel.Month = Convert.ToInt32(viewModel.MonthList.FirstOrDefault().Value);
            viewModel.DepartmentList = new SelectList(_employeeDepartmentRepository.GetMany(x => x.Status), "Id",
                                                     "Description").ToList();
            viewModel.DepartmentList.Insert(0, new SelectListItem { Selected = true, Text = "--- Select All Department  ---", Value = "0" });

            viewModel.EmployeeList = _employeeInfoRepository.GetAll();

            return View(viewModel);

            
        }
        [HttpPost]
        public ActionResult SalarySips(PayementSlipReportFormViewModel model, List<ScEmployeeInfo> EmployeeListEntry)
        {
            var month = model.Month;
            if (base.SystemControl.DateType ==2)
            {
                var date = DateTime.Now.Year + "/" + model.Month + "/1/";

                month = NepaliDateService.NepalitoEnglishDate(date).Month;

                
            }
            
            var predicateOr = PredicateBuilder.False<ScEmployeeInfo>();

            predicateOr = EmployeeListEntry.Where(x => x.Checkbox).Select(item => item.Id).Aggregate(predicateOr, (current, id) => current.Or(x => x.Id == id));
           var data= _employeeInfoRepository.GetExpandable(predicateOr);
            var viewModel = base.CreateReportViewModel<EmployeeSalaryReportViewModel>();
            viewModel.MonthId = month;
            viewModel.Year = this.AcademicYear.StartDate.Year;
            viewModel.EmployeeInfos = data;
            viewModel.AYear = this.AcademicYear.Id;
            Session["ReportModel"] = viewModel;
            var partialView = this.RenderPartialViewToString("_PartialSalarySlipReport", viewModel);
            return Json(new { view = partialView },
                        JsonRequestBehavior.AllowGet);
        }
        public ActionResult PdfSalarySlip()
        {

            var view = (EmployeeSalaryReportViewModel)Session["ReportModel"];
            // Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(1000));
            return this.ViewPdf("", "PayrollReport/PdfSalarySlipReport", view);

        }
        public ActionResult ExcelSalarySlip()
        {

            var view = (EmployeeSalaryReportViewModel)Session["ReportModel"];
            // Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(842), Convert.ToInt32(595));
            return this.ViewExcel("", "PayrollReport/ExcelSalarySlipReport", view);

        }



    }
}
