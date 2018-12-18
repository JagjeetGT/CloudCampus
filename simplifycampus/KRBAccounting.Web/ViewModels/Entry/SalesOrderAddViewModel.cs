using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class SalesOrderAddViewModel : BaseViewModel
    {
        public SalesOrderMaster SalesOrder { get; set; }

        public SalesOrderOtherDetail SalesOrderOtherDetail { get; set; }
    //[Remote("CheckFiscalyearDateinSalesOrder","Entry")]
        public string OrderDate { get; set; }
        public string DueDate { get; set; }
        public string CnDate { get; set; }
        public string PODate { get; set; }
        public string ContractDate { get; set; }
        public string ExportInvoiceDate { get; set; }
        public string ShortName { get; set; }
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
        public string Date { get; set; }
        public SelectList CnTypeList { get; set; }
        public bool AllowProductWiseBillTerm { get; set; }
        public SelectList Unitlist { get; set; }
    }
}