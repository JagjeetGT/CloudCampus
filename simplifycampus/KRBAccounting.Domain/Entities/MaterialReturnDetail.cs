using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class MaterialReturnDetail{        [Key]        public int Id {get;set;}        [Required(ErrorMessage =  " " )]        public int MaterialReturnId {get;set;}        [Required(ErrorMessage =  " " )]        public int RawMaterialId {get;set;}        [Required(ErrorMessage =  " " )]        public int UnitId {get;set;}        [Required(ErrorMessage =  " " )]        public decimal Quantity {get;set;}

        public int? CostCenterId { get; set; }

        public int GodownId { get; set; }
        [ForeignKey("MaterialReturnId")]
        public virtual MaterialReturn MaterialReturn { get; set; }
        [ForeignKey("RawMaterialId")]
        public virtual Product RawMaterial { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }    }}