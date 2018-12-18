using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{
    public class AcademicYear 
    {
        [Key]
        public int Id { get; set; }
        public string StartDateNep { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public string EndDateNep { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int CompanyId { get; set; }
        public int FiscalYearId { get; set; }
        public bool IsDefalut { get; set; }

        public int CreatedById { get; set; }
        public int ModifiedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreateUser { get; set; }
        [ForeignKey("ModifiedById")]
        public virtual User UpdateUser { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual FiscalYear FiscalYear { get; set; }
        
    }
}
