using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_MaterialReturnRegister
    {
        public DateTime ReturnDate { get; set; }
        public int MaterialReturnId { get; set; }
        public string MRCode { get; set; }
        public string ShortNameCC { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public int RawMaterialId { get; set; }
        public string RawMaterial { get; set; }
        public decimal QtyRaw { get; set; }
        public string UnitRaw { get; set; }
        public string CostCenterRaw { get; set; }
        public string GoDownRaw { get; set; }

    }
}
