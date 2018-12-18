using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public class SP_StudentInfoHeader
    {
        public string Student { get; set; }
        public string StdCode { get; set; }
        public string Regno { get; set; }
        public string Rollno { get; set; }
        public string Section { get; set; }
        public string Class { get; set; }
        public string TmpAdd { get; set; }
        public string TmpCity { get; set; }
        public string TmpDistrict { get; set; }
        public string TmpCountry { get; set; }
    }
}
