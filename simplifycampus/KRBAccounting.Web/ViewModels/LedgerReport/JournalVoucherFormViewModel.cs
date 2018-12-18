using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class JournalVoucherFormViewModel : BaseViewModel
    {
      public string StartDate { get; set; }
      public string EndDate { get; set; }

      public bool Remarks { get; set; }
      public bool Details { get; set; }
      public bool SubLedger { get; set; }
      public int Datetype { get; set; }
      public bool DateShow { get; set; }
    }
}