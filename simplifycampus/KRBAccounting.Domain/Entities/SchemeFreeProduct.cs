using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{    public class SchemeFreeProduct{        [Key]        public int Id {get;set;}

        public int ProductId { get; set; }        public int SchemeProductId {get;set;}        public int FreeQty {get;set;}        public string FreeUnit {get;set;}


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("SchemeProductId")]
        public virtual SchemeProduct SchemeProduct { get; set; }

        [NotMapped]
        public SelectList Unitlist { get; set; }

        [NotMapped]
        public string ParentGuid { get; set; }

        [NotMapped]
        public int UnitId { get; set; }
    }}