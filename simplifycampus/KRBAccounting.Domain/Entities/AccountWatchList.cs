using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class AccountWatchList
    {
        [Key]
        public int Id { get; set; }
        public int LedgerId { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }
    }
}
