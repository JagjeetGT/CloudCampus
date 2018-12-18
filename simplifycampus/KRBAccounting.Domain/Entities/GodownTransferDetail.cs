using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class GodownTransferDetail{        [Key]        public int Id {get;set;}        [Required(ErrorMessage =  " " )]        public int TransferId {get;set;}        [Required(ErrorMessage =  " " )]        public int ProductId {get;set;}        [Required(ErrorMessage =  " " )]        public int UnitId {get;set;}        [Required(ErrorMessage =  " " )]        public decimal Qty {get;set;}        [Required(ErrorMessage =  " " )]        public decimal Rate {get;set;}        [Required(ErrorMessage =  " " )]        public decimal Amount {get;set;}

        [ForeignKey("TransferId")]
        public virtual GodownTransfer GodownTransfer { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }    }}