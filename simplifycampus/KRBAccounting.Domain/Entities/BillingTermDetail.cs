using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class BillingTermDetail
    {
        [Key]
        public int Id { get; set; }
        public string Source { get; set; }
        public int ReferenceId { get; set; }
        public decimal TermAmount { get; set; }
        public int BillingTermId { get; set; }
        public int? Index { get; set; }
        public decimal Rate { get; set; }
        public int DetailId { get; set; }
        public bool IsProductWise { get; set; }
        public string Type { get; set; }
        [NotMapped]
        public string ParentGuid { get; set; }
    }
}
