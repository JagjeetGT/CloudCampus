using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_BSAccountGroupWise
    {
        public int LedgerId { get; set; }
        public int AccountGroupId { get; set; }
        public int ASGId { get; set; }
   
        public string AccountName { get; set; }
        public string ShortName { get; set; }
        public decimal? LedgerTotal { get; set; }
        public string BSType { get; set; }
        public string AccountCategory { get; set; }
       } 
}
