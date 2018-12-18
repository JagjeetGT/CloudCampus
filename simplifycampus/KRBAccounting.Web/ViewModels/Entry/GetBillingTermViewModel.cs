using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class GetBillingTermViewModel
    {
        public IEnumerable<BillingTerm> BillingTermList { get; set; }
        public BillingTerm BillingTerm { get; set; }
    }
}