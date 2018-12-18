using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class CreditNoteDetail
    {
        [Key]
        public int Id { get; set; }
        public int SNo { get; set; }
        public int GlCode { get; set; }
        public decimal? Amount { get; set; }
        public string Narration { get; set; }
        public int CreditNoteMasterId { get; set; }
        public int? SlCode { get; set; }
        [ForeignKey("CreditNoteMasterId")]
        public virtual CreditNoteMaster CreditNoteMaster { get; set; }

        [NotMapped]
        public int Index { get; set; }
    }
}
