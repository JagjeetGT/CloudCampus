using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PLAcGroupHeader
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ASGId { get; set; }
        public string ASGName { get; set; }
        public string GroupAccountType { get; set; }
        public decimal Total { get; set; }
    }
}
