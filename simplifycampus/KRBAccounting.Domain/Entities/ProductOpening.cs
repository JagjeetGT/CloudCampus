using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{    public class ProductOpening
        public int? GodownId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [NotMapped]
        public int UnitId { get; set; }

        [NotMapped]
        public SelectList UnitList { get; set; }

        [NotMapped]
        public List<SelectListItem> GodownList { get; set; }

        [NotMapped]
        public IEnumerable<ProductOpening> ProductOpenings { get; set; }