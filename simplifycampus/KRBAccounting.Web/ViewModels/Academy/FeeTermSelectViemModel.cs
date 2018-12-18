using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class FeeTermSelectViemModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string Basis { get; set; }
        public string Sign { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
    }
}