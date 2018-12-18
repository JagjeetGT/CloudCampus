using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_BOMRegister
    {
        public string ShortNameFG { get; set; }
        public int FinishedGoodId { get; set; }
        public string FinishedGood { get; set; }
        public decimal QtyFG { get; set; }
        public string UnitFG { get; set; }
        public int BOMID { get; set; }
        public string BomCode { get; set; }
        public string BOM { get; set; }
        public string ShortNameCC { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        //  End Of Master
        public int RawMaterialId { get; set; }
        public string RawMaterial { get; set; }
        public decimal QtyRaw { get; set; }
        public string UnitRaw { get; set; }
        public string CostCenterRaw { get; set; }
        public string GoDownRaw { get; set; }


    }
}
