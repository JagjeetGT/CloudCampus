using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_EmployeeSalaryCreditSlipDetails
    {
        public int Id { get; set; }
        public int Employeeid { get; set; }
        public string VNo { get; set; }
        public string Source { get; set; }
        public int SalaryHeadId { get; set; }
        public string SalaryHeadDesc { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Operation { get; set; }
    }
}
