using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
   public class AttendanceLog
   {
       public int Id { get; set; }
       public int EnrollId { get; set; }
       public DateTime TDate { get; set; }
       public TimeSpan? InTime { get; set; }
       public int? Verify { get; set; }
       public TimeSpan? BreakOut { get; set; }
       public TimeSpan? BreakIn { get; set; }
       public int? InOutMode { get; set; }
       public int? WorkCode { get; set; }
       public TimeSpan? OutTIme { get; set; }
       public int? VerifyOut { get; set; }
       public int? SignInBranch { get; set; }
       public int? SignOutBranch { get; set; }
       public DateTime? OutDate { get; set; }

       

     

       [NotMapped]
       public ScEmployeeInfo EmployeeInfo { get; set; }
   }
}
