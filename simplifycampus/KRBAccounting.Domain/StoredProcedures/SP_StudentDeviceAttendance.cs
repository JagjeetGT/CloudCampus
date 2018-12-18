using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public  class SP_StudentDeviceAttendance
    {
       public int PresentDays { get; set; }
       public decimal PresentPercentage { get; set; }
       public int StudentID { get; set; }
       public string StuDesc { get; set; }
       public int ClassId { get; set; }
       public int SectionId { get; set; }
       public string ClassName { get; set; }
       public string SectionName { get; set; }
    }
}
