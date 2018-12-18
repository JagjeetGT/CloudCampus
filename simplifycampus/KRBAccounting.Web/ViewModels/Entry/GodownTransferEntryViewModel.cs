using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class GodownTransferEntryViewModel
    {
        public int ProductId { get; set; }
        public decimal? AltQty { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int Index { get; set; }
        public int GodownId { get; set; }
    }
}