using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public class Sp_DailyAttendance 
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string TDate { get; set; }
        public string InTime { get; set; }
        public string BreakOut { get; set; }
        public string BreakIn { get; set; }
        public string OutTIme { get; set; }
        public string LunchBreak { get; set; }
        public string TimeatOffice { get; set; }
        public string TotalTime { get; set; }

        public string EmployeeId { get; set; }
        public int Departmentid { get; set; }



   
    }
}
