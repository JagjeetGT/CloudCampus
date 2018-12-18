using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class StockAdjustmentDetail{        [Key]        [Required(ErrorMessage =  "*" )]        public int Id {get;set;}        [Required(ErrorMessage =  "*" )]        public int AdjustmentId {get;set;}        public int SNo {get;set;}        public int ProductCode {get;set;}        [Required(ErrorMessage =  "*" )]        public int GodownId {get;set;}        public string AdjustmentType {get;set;}        public decimal AltQty {get;set;}        public decimal AltUnit {get;set;}        public decimal Qty {get;set;}        public decimal Unit {get;set;}        public decimal AltStockQty {get;set;}        public decimal StockQty {get;set;}        public decimal Rate {get;set;}        public decimal NetAmt {get;set;}        public string Description {get;set;}        public int ClassCode {get;set;}        public string PhysicalStockNo {get;set;}        public int PhysicalStockSNo {get;set;}
        public int UnitId { get; set; }

        [ForeignKey("AdjustmentId")]
        public virtual StockAdjustmentMaster StockAdjustmentMaster { get; set; }    }  }