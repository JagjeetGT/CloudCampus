using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class FeeAmountViewModel
    {
        public string ClassName { get; set; }
        public decimal ReceiptAmt { get; set; }
        public decimal BillAmt { get; set; }
    }
}