using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Service.Models.Sales
{
    public class SalesInvoiceAddViewModel : BaseModel
    {
        public SalesInvoice SalesInvoice { get; set; }

        public SalesInvoiceOtherDetail SalesInvoiceOtherDetail { get; set; }
        public IEnumerable<UDFEntry> UdfEntries { get; set; }
        public string InvoiceDate { get; set; }
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
        public DateTime Date { get; set; }
        public SelectList CnTypeList { get; set; }
        public bool AllowProductWiseBillTerm { get; set; }
        public SelectList Unitlist { get; set; }

        public string ChallanNo { get; set; }
        public string OrderNo { get; set; }
        public int? SalesOrderId { get; set; }
        public EntryControlSales EntryControl { get; set; }
    }
}
