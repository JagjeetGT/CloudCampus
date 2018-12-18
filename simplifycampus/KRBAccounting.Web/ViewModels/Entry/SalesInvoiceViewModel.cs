using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Models.BillingTerm;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class SalesInvoiceViewModel 
    {
        public PagedList<SalesInvoice> AcceptedList { get; set; }
        public PagedList<SalesInvoice> PendingList { get; set; }

        public List<BillingTermSelectViewModel> ProductWiseBillTerms { get; set; }
        public List<BillingTermSelectViewModel> BillWiseBillTerms { get; set; }
    }
}