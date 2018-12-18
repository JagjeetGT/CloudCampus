using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Service.Models.BillingTerm
{
    public class BillingTermDetailViewModel : BillingTermSelectViewModel
    {
        public int? BillingTermIndex { get; set; }
        public bool IsProductWise { get; set; }
        public string ParentGuid { get; set; }
    }
}
