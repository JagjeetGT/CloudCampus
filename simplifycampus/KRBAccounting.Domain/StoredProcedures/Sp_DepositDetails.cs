using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class Sp_DepositDetails
    {
        public int Id { get; set; }
        public DateTime DepositedOn { get; set; }
        public string DepositedMiti { get; set; }
        public string DepositSlipNumber { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public string ChequeNumber { get; set; }
        public string DepositedBy { get; set; }
        public decimal DepositedAmount { get; set; }
    }
}
