using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_CollectionByDate
    {
        public DateTime CollectionDate { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal IndividualCollectionAmount { get; set; }
        public string Name { get; set; }
        public string LoanName { get; set; }
        public string LoanType { get; set; }
    }
}
