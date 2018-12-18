using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Service.Models.Purchase
{
    public class ProductBatchSalesViewModel
    {
        public decimal Qty { get; set; }
        public string BatchSerialNo { get; set; }
        public string ParentGuid { get; set; }
        public DateTime? MfgDate { get; set; }
        public DateTime? ExpDate { get; set; }
        public int UnitId { get; set; }
        public int? GodownId { get; set; }
        public string Godown { get; set; }
        public int ProductId { get; set; }
        public string Unit { get; set; }
        public decimal? BuyRate { get; set; }
        public decimal? SalesRate { get; set; }
        public decimal StockQty { get; set; }
        public int Id { get; set; }
        public int? ExpiredProduct { get; set; }
        public bool IsExpired { get; set; }
    }
}