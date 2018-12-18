using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace KRBAccounting.Domain.Entities
{
    public class PyPayrollGeneration
    {
        [Key]
        public int Id { get; set; }
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
        public int AcademicYearId { get; set; }
        public string VNo { get; set; }
        //[ScriptIgnore]
        [ForeignKey("EmployeeId")]
        public virtual ScEmployeeInfo EmployeeInfo { get; set; }
        [ForeignKey("SalaryHeadId")]
        public virtual ScSalaryHead SalaryHead { get; set; }
        [NotMapped]
        public decimal NetAmount { get; set; }
        public virtual ICollection<PyPayrollGenerationDetail> PyPayrollGenerationDetails { get; set; }
    }
}
