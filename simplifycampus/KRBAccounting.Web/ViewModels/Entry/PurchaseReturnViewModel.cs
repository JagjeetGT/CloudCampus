using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Models.BillingTerm;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class PurchaseReturnViewModel
    {
        public PagedList<PurchaseReturnMaster> AcceptedList { get; set; }
        public PagedList<PurchaseReturnMaster> PendingList { get; set; }

        public List<BillingTermSelectViewModel> ProductWiseBillTerms { get; set; }
        public List<BillingTermSelectViewModel> BillWiseBillTerms { get; set; }
    }
}