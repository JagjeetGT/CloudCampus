using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Service.Models.BillingTerm
{
    public class BillingTermSelectViewModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string Basis { get; set; }
        public string Sign { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public int DisplayOrder { get; set; }
        public int Code { get; set; }
    }
}
