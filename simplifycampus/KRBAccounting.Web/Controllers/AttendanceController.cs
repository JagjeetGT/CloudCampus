using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.ViewModels.Attendance;


namespace KRBAccounting.Web.Controllers
{
    public class AttendanceController : BaseController
    {
        //
        // GET: /Attendance/


        private readonly IScEmployeeInfoRepository _employeeInfoRepository;

        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly IScEmployeeDepartmentRepository _departmentRepository;
        private readonly ISystemControlRepository _systemControlRepository;

    private readonly IFormsAuthenticationService _authentication;
        private readonly IFiscalYearRepository _fiscalYearRepository;

        public AttendanceController(

            ISystemControlRepository systemControlRepository,
            IFiscalYearRepository fiscalYearRepository,
            IScEmployeeInfoRepository employeeInfoRepository,
            IScEmployeeDepartmentRepository employeeDepartmentRepository,
            IAttendanceLogRepository attendanceLogRepository

            )
        {
            _attendanceLogRepository = attendanceLogRepository;
            _systemControlRepository = systemControlRepository;
            _departmentRepository = employeeDepartmentRepository;
            _fiscalYearRepository = fiscalYearRepository;
            _employeeInfoRepository = employeeInfoRepository;
            _departmentRepository = employeeDepartmentRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Payroll);
        }
        public ActionResult GetEmployeeByDepartment(string depid)
        {
           
            if(string.IsNullOrEmpty(depid))
            {
                var employee = _employeeInfoRepository.GetAll().Select(x => new
                                                                                {
                                                                                    Id = x.Id,
                                                                                    Description = x.Description
                                                                                });
               return Json(employee,JsonRequestBehavior.AllowGet);

            }
            else
            {
                var department = depid.Split(',').Select(x =>Convert.ToInt32(x)).ToList();
              var  empl = _employeeInfoRepository.GetMany(x => department.Contains( x.EmployeeDepartmentId))
             .Select(x => new
                {
                    Id = x.Id,
                    Description = x.Description
                });
                return Json(empl, JsonRequestBehavior.AllowGet);
            }
        
        }
        public ActionResult GetDepartment()
        {
            var ledgers = _departmentRepository.GetAll().Select(x => new
            {
                Id
                =
                x
                .
                Id,
                Ddepartment
                =
                x
                .
                Description,

            })
            .OrderBy(x => x.Ddepartment);
            return Json(ledgers, JsonRequestBehavior.AllowGet);
        }
        #region Daily Attendance
[CheckPermission(Permissions = "Navigate", Module = "DA")]
        public ActionResult DailyAttendance()
        {
            var model = new DailyAttendanceFormViewModel();
            model.EmployeeList = new SelectList(_employeeInfoRepository.GetAll(), "Id", "Description");
            //.Select(x => new SelectListItem() { Text = x.Description , Value = x.Id.ToString()}), "Value", "Text");
            if (base.SystemControl.IsCurrDate)
            {
                model.FromDate = base.SystemControl.DateType == 1
                                     ? DateTime.Now.ToString("MM/dd/yyyy")
                                     : NepaliDateService.NepaliDate(DateTime.Now).Date;
                model.ToDate = base.SystemControl.DateType == 1
                                   ? DateTime.Now.ToString("MM/dd/yyyy")
                                   : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }

            model.DepartmentList = new SelectList(_departmentRepository.GetAll().OrderBy(x => x.Description).ToList(),
                                                  "Id", "Description");

            model.SystemControl = base.SystemControl;
            return View(model);
        }

        public ActionResult DailyAttendanceList(string employees, string FromDate, string ToDate, string departments)
        {

            FromDate = base.SystemControl.DateType == 1
                           ? Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy")

                           : NepaliDateService.NepalitoEnglishDate(FromDate).Date.ToString("MM/dd/yyyy");
            ToDate = base.SystemControl.DateType == 1
                         ? Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy")
                         : NepaliDateService.NepalitoEnglishDate(ToDate).Date.ToString("MM/dd/yyyy");


            var startReportDate = Convert.ToDateTime(FromDate).ToString("MM/dd/yyyy");
            var endReportDate = Convert.ToDateTime(ToDate).ToString("MM/dd/yyyy");
            var reportDate = "Report Date From " + startReportDate + " To " + endReportDate;
            var title = "Daily Attendance";
            if(string.IsNullOrEmpty(employees))
            {
                employees = "0";
            }if(string.IsNullOrEmpty(departments))
            {
                departments = "0";
            }
            var viewModel = base.CreateReportViewModel<DailyAttendanceReportViewModel>(title, reportDate);
            viewModel.DailyAttendances = StoredProcedureService.GetdailyAttendance(employees, FromDate, ToDate, departments);
            viewModel.DailyAttendancesGroup =
               viewModel.DailyAttendances.GroupBy(x => x.Id);
            
            return PartialView("_partialDailyAttendanceReport", viewModel);
         
        }

        
        #endregion
    }
}