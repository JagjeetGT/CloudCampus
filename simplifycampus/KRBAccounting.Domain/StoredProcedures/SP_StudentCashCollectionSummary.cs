using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_StudentCashCollectionSummary
    {
        public int Id { get; set; }
        public string BillNo { get; set; }
        public DateTime Date { get; set; }

        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string RollNo { get; set; }
        public decimal Amount { get; set; }

    }
}
