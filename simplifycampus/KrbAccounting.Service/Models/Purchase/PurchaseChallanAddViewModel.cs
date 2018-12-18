using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Service.Models.Purchase
{
    public class PurchaseChallanAddViewModel : BaseModel
    {
        public PurchaseChallanImpTransDoc PurchaseChallanImpTransDoc { get; set; }
       public string ChallanDate { get; set; }
        public string PPDDate { get; set; }
        public string CnDate { get; set; }
        public string ShortName { get; set; }
        public PurchaseChallanMaster PurchaseChallan { get; set; }
        public string StockQty { get; set; }
        public string AltStockQty { get; set; }
        public string TotalQty { get; set; }
        public string TotalAltQty { get; set; }
        public string TotalAmt { get; set; }
        public string TotalAmtRs { get; set; }

        public string CreditLimit { get; set; }
        public string CurrentBalance { get; set; }
        public string OutstandingChallan { get; set; }
        public string TotalOutstanding { get; set; }

        public string DisplayDate { get; set; }

        public bool AllowProductWiseBillTerm { get; set; }

        public int? PurchaseOrderId { get; set; }
        public SelectList UnitList { get; set; }

        public string OrderNo { get; set; }

       //public string PickDate { get; set; }
    }
}
