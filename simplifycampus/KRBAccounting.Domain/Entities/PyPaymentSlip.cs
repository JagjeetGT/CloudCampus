using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class PyPaymentSlip
{
        [Key]
        public int Id {get;set;}
        public DateTime CreateDate {get;set;}
        public string SlipNo {get;set;}
        public int EmployeeId {get;set;}
        public int Year {get;set;}
        public int Month {get;set;}
        public int YearMiti {get;set;}
        public int MonthMiti {get;set;}
        public decimal NetAmount {get;set;}
        public int LedgerId {get;set;}
        public int CreatedById { get; set; }
        public string Remark { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual ScEmployeeInfo EmployeeInfo { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
    }
}
