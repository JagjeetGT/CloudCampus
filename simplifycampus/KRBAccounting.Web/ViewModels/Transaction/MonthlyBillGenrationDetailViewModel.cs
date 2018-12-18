using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Transaction
{
    public class MonthlyBillGenerationDetailViewModel
    {
        public int sno { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string RollNo { get; set; }
        public decimal Amount { get; set; }
        public string Section { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public bool Checkbox { get; set; }
        public decimal EducationTax { get; set; }
        public List<int> MonthList { get; set; }
        public int MonthlyStudentBillId { get; set; }

    }
}