using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PriceList
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? LastBuyPrice { get; set; }
        public decimal? LastSalePrice { get; set; }
        public decimal? CurrentStock { get; set; }
    }
}
