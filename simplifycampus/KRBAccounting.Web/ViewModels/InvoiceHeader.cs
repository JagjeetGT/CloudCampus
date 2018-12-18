using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels
{
    public class InvoiceHeader
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public string PartyName { get; set; }
        public string BilledBy { get; set; }
        public string CompanyLogoUrl { get; set; }
        public string HeaderTitle { get; set; }
    }
}