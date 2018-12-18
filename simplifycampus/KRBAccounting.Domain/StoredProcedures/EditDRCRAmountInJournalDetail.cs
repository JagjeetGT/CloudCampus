using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class EditDRCRAmountInJournalDetail
    {
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
    }
}
