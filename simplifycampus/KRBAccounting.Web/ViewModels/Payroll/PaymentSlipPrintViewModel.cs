using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PaymentSlipPrintViewModel
    {
        public string Date { get; set; }
        public string SlipNo { get; set; }
        public string Name { get; set; }
        public string PaymentFor { get; set; }
        public string InWords { get; set; }
        public string Amount { get; set; }
        public string ReceivedBy { get; set; }
    }
}
