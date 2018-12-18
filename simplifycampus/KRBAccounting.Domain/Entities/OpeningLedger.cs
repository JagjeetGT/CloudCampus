using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
   public class OpeningLedger 
    {
       [Key]
       public int Id { get; set; }
       public int GlCode { get; set; }
       public int? SlCode { get; set; }
       public decimal Amount { get; set; }
       public int AmountType { get; set; }
       public int CreatedById { get; set; }
       public int LedgerType { get; set; }
       public DateTime CreatedDate { get; set; }
       [ForeignKey("GlCode")]
       public virtual Ledger Ledger { get; set; }

       [ForeignKey("SlCode")]
       public virtual SubLedger SubLedger { get; set; }


       [ForeignKey("CreatedById")]
       public virtual User User { get; set; }
       public int BranchId { get; set; }
       public int FiscalYearId { get; set; }
       [ForeignKey("BranchId")]
       public CompanyInfo Branch { get; set; }

       [NotMapped]
       public SelectList AmountTypeList { get; set; }
    }
}
