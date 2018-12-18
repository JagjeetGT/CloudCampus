using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Service.Models.Sales
{
    public class PopulateSalesChallanDetailViewModel
    {
        public string VendorName { get; set; }
        public int VendorId { get; set; }
        public string AgentName { get; set; }
        public int AgentId { get; set; }
        public string materialIssueDetailView { get; set; }
        public decimal billWiseBillingTerm { get; set; }
        public string BasicAmt { get; set; }
        public string NetAmt { get; set; }
        public string TermAmt { get; set; }
        public string TotalAmtRs { get; set; }
        public string CurrentBalance { get; set; }
        public string TotalOutstanding { get; set; }
    }
}