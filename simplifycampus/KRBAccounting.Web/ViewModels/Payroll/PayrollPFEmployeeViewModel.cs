using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PayrollEmployeeSalaryMasterViewModel
    {
        public PyEmployeeSalaryMaster EmployeeSalaryMaster { get; set; }
        public SelectList EmployeeList { get; set; }
        public List<CheckBoxListInfo> CheckBoxListInfos { get; set; }
        public List<int> AllowanceId { get; set; }
        public int OldEmployeeSalaryId { get; set; }
    }
}