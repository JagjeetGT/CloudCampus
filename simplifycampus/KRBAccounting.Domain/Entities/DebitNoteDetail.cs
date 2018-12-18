using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class DebitNoteDetail
    {
        [Key]
        public int Id { get; set; }
        public int SNo { get; set; }
        public int GlCode { get; set; }
        public decimal? Amount { get; set; }
        public string Narration { get; set; }
        public int DebitNoteMasterId { get; set; }
        public int? SlCode { get; set; }
        [ForeignKey("DebitNoteMasterId")]
        public virtual DebitNoteMaster DebitNoteMaster { get; set; }

        [NotMapped]
        public int Index { get; set; }
    }
}
