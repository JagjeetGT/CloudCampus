using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScConsolidatedMarksSetup
    {
        [Key]
        [Required(ErrorMessage = " ")]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        public string Description { get; set; }
        public int ExamOrder { get; set; }
        public decimal Percentage { get; set; }
        public string Remarks { get; set; }
        public int ClassId { get; set; }
        public int ExamId { get; set; }
        public int ExamGrdId { get; set; }

        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("ExamId")]
        public virtual ScExam Exam { get; set; }
        [ForeignKey("ExamGrdId")]
        public virtual ScExam ExamGrd { get; set; }

    }
}
