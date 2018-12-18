using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_StudentLedgerSummary
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string RollNo { get; set; }
        public decimal Opening { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; } 

    }
}
