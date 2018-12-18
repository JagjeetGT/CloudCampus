using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_ReportCourseWise
    {
        public decimal ReceiptAmount { get; set; }
        public decimal BillAmount { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Balance { get; set; }
    }
}
