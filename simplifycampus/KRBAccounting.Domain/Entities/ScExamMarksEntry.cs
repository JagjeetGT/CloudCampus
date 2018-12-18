using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Domain.Entities
{    public class ScExamMarksEntry
{
        [Key]
        public int Id {get;set;}
        public int ExamId {get;set;}
        public int ClassId {get;set;}
        public int SubjectId {get;set;}
        public string Remarks {get;set;}
        public int CreatedById {get;set;}
        public int UpdatedById {get;set;}
        public DateTime CreatedDate {get;set;}
        public DateTime UpdatedDate {get;set;}
        public decimal TotalFullMarks {get;set;}
        public decimal TotalObtainedMarks {get;set;}
        public decimal Percentage {get;set;}
        public string Result {get;set;}
        public int DivisionId {get;set;}
        public Int64 OutOf {get;set;}
        public Int64 Rank { get; set; }
        public decimal HighestMarks {get;set;}
        public decimal TheoryObtainedMarks {get;set;}
        public int TheoryStatus {get;set;}
        public decimal PracticalObtainedMarks {get;set;}
        public int PracticalStatus {get;set;}
        public string Narration {get;set;}
        public decimal Total {get;set;}
        public int StudentId {get;set;}
        public int AcademicYearId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual ScSubject Subject { get; set; }
        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("ExamId")]
        public virtual ScExam Exam { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
     [ForeignKey("DivisionId")]
        public virtual ScDivision Division { get; set; }
        [NotMapped]
        public string DivisionDescription { get; set; }


        [NotMapped]
        public IEnumerable<Sp_SubjectWiseMarksDetail> SubjectWiseMarksDetailses { get; set; }
        [NotMapped]
        public IEnumerable<SP_StudentWiseMarksDetail> StudentWiseMarksDetailses { get; set; }

    [NotMapped]
    public int oldExamId { get; set; }
    [NotMapped]
    public int oldClassId { get; set; }
    [NotMapped]
    public int oldSubjectId { get; set; }

    [NotMapped]
    public int oldStudentId { get; set; }
    }
}
