using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class CashBankDetail
    {
        public int Id { get; set; }
        public int LedgerId { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public int AmountType { get; set; }
        public int MasterId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public int? SlCode { get; set; }
        public int? Index { get; set; }
       

        [ForeignKey("MasterId")]
        public virtual CashBankMaster CashBankMaster { get; set; }

        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }


        [NotMapped]
        public decimal? RecAmount { get; set; }
        //{
        //    get
        //    {
        //        decimal Amt = 0;
        //        if (AmountType == 1)
        //        {
        //            Amt = Amount;
        //        }
        //        return Amt;
        //    }
        //}
        [NotMapped]
        public decimal? PayAmount { get; set; }
        //{
        //    get
        //    {
        //        decimal Amt = 0;
        //        if (AmountType == 2)
        //        {
        //            Amt = Amount;
        //        }
        //        return Amt;
        //    }
        //}
        
    }
}
