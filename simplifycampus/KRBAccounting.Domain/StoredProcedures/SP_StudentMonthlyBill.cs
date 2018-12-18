using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_StudentMonthlyBill
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Monthly { get; set; }
        public string VoucherNo { get; set; }
        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string RollNo { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }

    }
}
