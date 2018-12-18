using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PayrollGenerationAddViewModel
    {
       
        public int EmployeeId { get; set; }
        public int SalaryHeadId { get; set; }
        public decimal Amount { get; set; }
        public int FiscalYearId { get; set; }
        public int BranchId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int YearMiti { get; set; }
        public int MonthMiti { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsUse { get; set; }
        public string Operation { get; set; }
    public decimal NetAmount { get; set; }
    public int AcademicYearId { get; set; }
    public string VNo { get; set; }
    public IEnumerable<PayrollGenerationDetailAddViewModel> PyPayrollGenerationDetails { get; set; }
  
    }
}