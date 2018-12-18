using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class StudentSlcInfo
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }

        public string SlcSymbolNo { get; set; }
        public string SlcRegNo{ get; set; }
        public decimal Percentage { get; set; }
        public string Division { get; set; }
        public string PassYearBS { get; set; }
        public string PassYearAD { get; set; }

        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Student { get; set; }
    }
}
