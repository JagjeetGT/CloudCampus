using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    public class ProductAddViewModel
    {
        public Product Product { get; set; }
        public SelectList Group { get; set; }
        public SelectList SubGroup { get; set; }
        public SelectList Type { get; set; }
        public SelectList ValTechList { get; set; }
        public SelectList UnitList { get; set; }
        public SelectList Vat { get; set; }
        public SelectList BonusType { get; set; }
        public SelectList UnitConversionList { get; set; }
        public SelectList LowestUnitConversionList { get; set; }
        public IEnumerable<ProductImages> ProductImages { get; set; }
        public IEnumerable<ProductUnitConversion> ProductUnitConversions { get; set; }
        public SelectList MediumList { get; set; }
        public List<SelectListItem> ExpiredProductList { get; set; }
    }
}