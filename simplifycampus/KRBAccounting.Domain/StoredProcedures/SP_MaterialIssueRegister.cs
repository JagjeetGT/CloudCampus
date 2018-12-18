using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_MaterialIssueRegister
    {
        public DateTime IssueDate { get; set; }
        public string ShortNameFG { get; set; }
        public int FinishedGoodId { get; set; }
        public string FinishedGood { get; set; }
        public decimal QtyFG { get; set; }
        public string UnitFG { get; set; }
        public int MIID { get; set; }
        public string MICode { get; set; }
        public string Description { get; set; }
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
