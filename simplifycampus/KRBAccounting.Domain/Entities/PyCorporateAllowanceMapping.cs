using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class PyCorporateAllowanceMapping
    {
        [Key]
        public int Id { get; set; }
        public int AllowanceId { get; set; }
        public int CorporateId { get; set; }
        [ForeignKey("AllowanceId")]
        public virtual PyAllowanceMaster AllowanceMaster { get; set; }
        [ForeignKey("CorporateId")]
        public virtual PyCorporateSalaryMaster CorporateSalaryMaster { get; set; }
       
    }
}
