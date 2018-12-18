using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class GodownTransferAddViewModel : BaseViewModel
    {
        public virtual StockTransferMaster StockTransfer { get; set; }
        //public GodownTransferEntryViewModel GodownTransferEntryViewModel { get; set; }
        public string ShortName { get; set; }
        public string FromStockQty { get; set; }
        public string FromAltStockQty { get; set; }

        public string ToStockQty { get; set; }
        public string ToAltStockQty { get; set; }
        public string TotalQty { get; set; }
        public string TotalAltQty { get; set; }
        public string TotalAmt { get; set; }
        public string DisplayDate { get; set; }
        public DateTime Date { get; set; }

    }
}