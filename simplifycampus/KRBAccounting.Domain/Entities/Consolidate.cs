using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class Consolidate
    {

        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Section { get; set; }
        public string RollNo { get; set; }
        public int ClassId { get; set; }
        public int ExamId { get; set; }
        public int AcademicYearId { get; set; }
        public string ConsolidateCode { get; set; }
        public string Division { get; set; }
        public decimal? Percent { get; set; }
        public decimal? TotalFullMarks { get; set; }
        public decimal? TotalObtained { get; set; }
        public string Rank { get; set; }
        public string Result { get; set; }
        public string OutOff { get; set; }
      


        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }
        [ForeignKey("ExamId")]
        public virtual ScExam Exam { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
    }
}
