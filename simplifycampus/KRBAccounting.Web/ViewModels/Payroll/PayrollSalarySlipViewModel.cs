using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PayrollPaymentSlipViewModel
    {
        public virtual PyPaymentSlip PaymentSlip { get; set; }
        public IEnumerable<ScEmployeeInfo> EmployeeInfos { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public SelectList MonthList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public int DepartmentId { get; set; }
        public int DateType { get; set; }
        public bool SelectAll { get; set; }
        public int Ledger { get; set; }
        public int CreatedBy { get; set; }
        public string SlipNo { get; set; }
        public SelectList EmployeeList { get; set; }
        public int EmployeeId { get; set; }
        public decimal NetAmount { get; set; }
        public string Remarks { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}