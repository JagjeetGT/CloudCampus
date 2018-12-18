using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ExtraTerm
    {
        [Key]
        public int Id { get; set; }
        public string Source { get; set; }
        public int SourceId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


    }
    public class ExtraTermDetail
    {
        [Key]
        public int Id { get; set; }
        public int ExtraTermId { get; set; }
        public int ExtraChargeId { get; set; }
        public decimal Value { get; set; }

        [ForeignKey("ExtraTermId")]
        public ExtraTerm ExtraTerm { get; set; }

        [ForeignKey("ExtraChargeId")]
        public ExtraCharge ExtraCharge { get; set; }

    }
}
