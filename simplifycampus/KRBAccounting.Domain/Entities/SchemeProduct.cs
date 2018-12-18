using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class SchemeProduct
    {
        [Key]
        public int Id { get; set; }
        public int ProductSchemeId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public int FreeQty { get; set; }
        public string FreeUnit { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("ProductSchemeId")]
        public virtual Scheme ProductScheme { get; set; }

        [NotMapped]
        public SelectList Unitlist { get; set; }

        [NotMapped]
        public SelectList FreeUnitlist { get; set; }

        [NotMapped]
        public string Guid { get; set; }
        [NotMapped]
        public IEnumerable<SchemeFreeProduct> SchemeFreeProducts { get; set; }

        [NotMapped]
        public int UnitId { get; set; }


        [NotMapped]
        public int FreeUnitId { get; set; }
    }
}
