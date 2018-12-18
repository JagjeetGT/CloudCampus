using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Transaction
{
    public class FeePrintViewModel
    {
        public string Date { get; set; }
        public string ReceiptNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string RollNo { get; set; }
        public string PaymentFor { get; set; }
        public string InWords { get; set; }
        public string Amount { get; set; }
        public string ReceivedBy { get; set; }
        public ReportHeader ReportHeader { get; set; }
        public string PreBalance { get; set; }
        public string BalanceDue { get; set; }

    }
}