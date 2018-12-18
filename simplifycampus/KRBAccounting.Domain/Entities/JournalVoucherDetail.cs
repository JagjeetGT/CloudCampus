using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class JournalVoucherDetail
    {
        [Key]
        public int Id { get; set; }

        public string JVNumber { get; set; }

        public int GlCode { get; set; }

        public decimal Amount { get; set; }

        public string Narration { get; set; }

        public int JVMasterId { get; set; }

        public int JVAmountType { get; set; }

        public int? SlCode { get; set; }
        [NotMapped]
        public int Index { get; set; }
        [ForeignKey("JVMasterId")]
        public virtual JournalVoucher JournalVoucher { get; set; }
        [NotMapped]
        public decimal? DrAmount { get; set; }
        [NotMapped]
        public decimal? CrAmount { get; set; }
        [ForeignKey("GlCode")]
        public virtual Ledger Ledger { get; set; }
    }
}
