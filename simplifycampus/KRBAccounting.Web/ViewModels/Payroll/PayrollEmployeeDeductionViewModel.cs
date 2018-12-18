using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PayrollEmployeeDeductionViewModel
    {
        public PyEmployeeDeductionMaster EmployeeDeductionMaster { get; set; }
        public SelectList EmployeeList { get; set; }
        public List<CheckBoxListInfo> CheckBoxListInfos { get; set; }
        public List<int> DeductionId { get; set; }
        public int OldEmployeeDeductionId { get; set; }
    }
}