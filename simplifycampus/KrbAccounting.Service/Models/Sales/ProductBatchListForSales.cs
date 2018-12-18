using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Service.Models.Sales
{
    public class ProductBatchListForSales
    {
        public int BatchId { get; set; }
        public int DetailId { get; set; }
        public string BatchSerialNo { get; set; }
        public decimal StockQty { get; set; }
    }
}
