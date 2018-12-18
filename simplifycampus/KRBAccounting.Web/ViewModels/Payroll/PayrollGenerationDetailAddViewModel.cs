using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PayrollGenerationDetailAddViewModel
    {
        public int PayrollGenerationId { get; set; }
        public int ReferenceId { get; set; }
        public decimal Amount { get; set; }
    }
}