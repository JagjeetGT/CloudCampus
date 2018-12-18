using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScFineSchemeDetails
    {
        [Key]
        public int Id { get; set; }
        public int MasterId { get; set; }
       
        public int? Days { get; set; }
        public decimal Percentage { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("MasterId")]
        public ScFineScheme FineScheme { get; set; }

    }
}
