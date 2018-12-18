using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class FinishedGoodReceiveDetail{        [Key]        public int Id {get;set;}        [Required(ErrorMessage =  " " )]        public int FinishedGoodReceiveId {get;set;}        [Required(ErrorMessage =  " " )]        public int FinishGoodId {get;set;}        [Required(ErrorMessage =  " " )]        public decimal Qty {get;set;}        [Required(ErrorMessage =  " " )]        public int UnitId {get;set;}        [Required(ErrorMessage =  " " )]        public decimal Rate {get;set;}        [Required(ErrorMessage =  " " )]        public decimal Amount {get;set;}

        [ForeignKey("FinishedGoodReceiveId")]
        public virtual FinishedGoodReceive FinishedGoodReceive { get; set; }
        [ForeignKey("FinishGoodId")]
        public virtual Product FinishGood { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }    }}