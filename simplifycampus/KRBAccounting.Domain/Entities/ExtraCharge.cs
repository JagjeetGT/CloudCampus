using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{
    public class ExtraCharge
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Description { get; set; }
        public string Operator { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal Charge { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }

        public int LedgerId { get; set; }

        [ForeignKey("LedgerId")]
        public  virtual Ledger Ledger { get; set; }
    }
}
