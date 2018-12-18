using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{    public class ProductOpening{        [Key]        public int Id {get;set;}        public int ProductId {get;set;}        public DateTime OpeningDate {get;set;}        public string OpeningMiti {get;set;}        public string Unit {get;set;}        public decimal Quantity {get;set;}        public string AltUnit {get;set;}        public decimal AltQuantity {get;set;}        public decimal Rate {get;set;}        public decimal Amount {get;set;}
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
        public IEnumerable<ProductOpening> ProductOpenings { get; set; }    }}