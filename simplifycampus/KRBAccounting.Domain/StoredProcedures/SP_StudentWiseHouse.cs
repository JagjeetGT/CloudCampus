using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_StudentWiseHouse
    {
        public string HouseCode { get; set; }
        public string HouseName { get; set; }
        public string ClassName { get; set; }
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
        public string RollNo { get; set; }
        public string Section { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
