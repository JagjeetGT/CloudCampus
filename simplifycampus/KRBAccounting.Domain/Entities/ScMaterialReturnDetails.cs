using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{    public class ScMaterialReturnDetails{        [Key]        public int Id {get;set;}        public string VoucherNo {get;set;}        public int MaterialReturnMasterId {get;set;}        public int ProductId {get;set;}        public int StaffId {get;set;}        public decimal Quantity {get;set;}        public decimal Rate {get;set;}        public decimal LocalAmount {get;set;}

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("StaffId")]
        public virtual ScEmployeeInfo Staff { get; set; }

        [ForeignKey("MaterialReturnMasterId")]
        public virtual ScMaterialReturnMaster MaterialReturnMaster { get; set; }    }}