using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class MonthlySalaryViewModel
    {
        public int Month { get; set; }
        public decimal Totalamt { get; set; }
    }
}