using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ProductLedgerInfo
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int LedgerId { get; set; }
        public string Module { get; set; }
    }
}
