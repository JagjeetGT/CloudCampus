using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PayrollTaxDeductionPatternViewModel
    {
        public PyTaxDeductionPattern PyTaxDeductionPattern { get; set; }

        public List<CheckBoxListInfo> CheckBoxListInfos { get; set; }
        public List<int> EmployeeId { get; set; }
        public int OldtaxdeductionId { get; set; }
    }
}