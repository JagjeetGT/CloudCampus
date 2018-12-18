using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReceiptPrintViewModel
    {
        public string StudentName { get; set; }
        public string ReceivedBy { get; set; }
        public double Amount { get; set; }
        public string OnAccountOf { get; set; }
        public string InWords { get; set; }
        public string RegNo { get; set; }
        public ReceiptHeader Header { get; set; }
    }


    public class ReceiptHeader
    {
        public string CompanyLogo { get; set; }
        public string CompanyName { get; set; }
        public string CompanyBranchName { get; set; }
        public string CompanyAddress { get; set; }
        public string ReceiptDate { get; set; }
        public string ReceiptNo { get; set; }
        public string Phone { get; set; }
    }
}