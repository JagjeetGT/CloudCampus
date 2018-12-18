using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
   public class Device
    {
        public int Id { get; set; }
        public int DeviceID { get; set; }
        public string ComNo { get; set; }
        public string DeviceDescription { get; set; }
        public string PortNo { get; set; }
        public string IPAddress { get; set; }
        public string BaudRate { get; set; }
        public int ConMode { get; set; }
    }
}
