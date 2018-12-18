using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ProductUnitConversion
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal LowestQuantity { get; set; }
        public string LowestUnit { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        [NotMapped]
        public SelectList UnitList { get; set; }

        [NotMapped]
        public SelectList LowestUnitList { get; set; }

        [NotMapped]
        public int UnitId { get; set; }

        [NotMapped]
        public int LowestUnitId { get; set; }
    }

}
