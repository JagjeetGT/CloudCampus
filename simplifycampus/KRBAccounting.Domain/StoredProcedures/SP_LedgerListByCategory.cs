using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_LedgerListByCategory
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string ShortName { get; set; }
        public string Header { get; set; }
        public decimal CreditLimit { get; set; }
        public int CreditDays { get; set; }
        public decimal RateOfInterest { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneO { get; set; }
        public decimal Amount { get; set; }
        public int AmountType { get; set; }
        public string LedgerType { get; set; }
    }
}
