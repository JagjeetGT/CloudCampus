using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
  public  class EmployeeTransaction
    {
        [Key]
        public int Id { get; set; }
        public int Month { get; set; }
        public int MonthMiti { get; set; }
        public int YearMiti { get; set; }
        public int Year { get; set; }
        public decimal NetAmount { get; set; }
        public string Source { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ReferenceId { get; set; }
        public string VNo { get; set; }
        public int CreateById { get; set; }
        public int EmployeeId { get; set; }
        public int BranchId { get; set; }
        public int AcademicYearId { get; set; }

    }
}
