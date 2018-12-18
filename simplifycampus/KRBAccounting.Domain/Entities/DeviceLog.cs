using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
   public class DeviceLog
    {
        public int Id { get; set; }
        public int EnrollID { get; set; }
        public int VerifyMode { get; set; }
        public int InOutMode { get; set; }
        public DateTime LogDate { get; set; }
        public string LogTime { get; set; }
        public int ExtractedFrom { get; set; }
    }
}
