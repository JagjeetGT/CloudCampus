using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class PyPFEmployeeMaster
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal Value { get; set; }
        public int FiscalYearId { get; set; }
        public bool IsAnnual { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsFlat { get; set; }
        public int BranchId { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User User { get; set; }
        public virtual ICollection<PyPFEmployeeMapping> PyPfEmployeeMappings { get; set; }
    }
}
