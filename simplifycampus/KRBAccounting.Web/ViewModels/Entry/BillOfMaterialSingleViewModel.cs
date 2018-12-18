using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class BillOfMaterialSingleViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public decimal? Qty { get; set; }
        public decimal? ConversionFactor { get; set; }
        public string Description { get; set; }
    }
}