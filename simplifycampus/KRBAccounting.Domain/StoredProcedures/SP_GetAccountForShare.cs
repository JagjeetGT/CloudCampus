using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_GetAccountForShare
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string AccountHolderName { get; set; }
    }
}
