using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Service.Models.Purchase
{
    public class PurchaseProductBatchViewModel
    {
        public decimal Qty { get; set; }
        public string BatchSerialNo { get; set; }
        public string ParentGuid { get; set; }
        public bool IsMfgDate { get; set; }
        public bool IsExpDate { get; set; }
        public DateTime? MfgDate { get; set; }
        public DateTime? ExpDate { get; set; }
        public int UnitId { get; set; }
        public int GodownId { get; set; }
        public string Godown { get; set; }
        public SelectList UnitList { get; set; }
        public int ProductId { get; set; }

        public decimal? BuyRate { get; set; }
        public decimal? SalesRate { get; set; }
        public int? DetailId { get; set; }
    }
}