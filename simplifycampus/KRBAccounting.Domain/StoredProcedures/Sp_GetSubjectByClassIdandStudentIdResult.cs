using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.StoredProcedures
{
   public class Sp_GetSubjectByClassIdandStudentIdResult
    {
       public string SubjectName { get; set; }
       public int SubjectId { get; set; }
       public int  ClassId { get; set; }

       public int BoardExamMasterId { get; set; }
       public string ClassName { get; set; }
       public string MaskObtained_Y1 { get; set; }
       public string MaskObtained_Y2 { get; set; }
       public string MaskObtained_Y3 { get; set; }
       public string MaskObtained_Y4 { get; set; }
       public string MaskObtained_Y5 { get; set; }
       public string MaskObtained_Y6 { get; set; }
       public string MaskObtained_Y7 { get; set; }
       public string Result_Y1 { get; set; }
       public string Result_Y2 { get; set; }
       public string Result_Y3 { get; set; }
       public string Result_Y4 { get; set; }
       public string Result_Y5 { get; set; }
       public string Result_Y6 { get; set; }
       public string Result_Y7 { get; set; }
       public string SymbolNo_Y1 { get; set; }
       public string SymbolNo_Y2 { get; set; }
       public string SymbolNo_Y3 { get; set; }
       public string SymbolNo_Y4 { get; set; }
       public string SymbolNo_Y5 { get; set; }
       public string SymbolNo_Y6 { get; set; }
       public string SymbolNo_Y7 { get; set; }

       public string Remarks { get; set; }
       public decimal? PassPercentage { get; set; }

       public int? GraduationYear { get; set; }
       public decimal? Percentage { get; set; }

       public string Division { get; set; }

       public DateTime? CharacterCertificateIssueDate { get; set; }
       public string CharacterCertificateIssueMiti { get; set; }
       public int? CharacterCertificateNumber { get; set; }
       public DateTime? TranscriptIssueDate { get; set; }
       public string TranscriptIssueMiti { get; set; }
       public int? TranscriptNumber { get; set; }

       public List<SelectListItem> StudentList { get; set; }
       public List<SelectListItem> ProgramList { get; set; }
       public SelectList DivisionList { get; set; }

       public string CharacterDisplayDate { get; set; }
       public string TranscriptDisplayDate { get; set; }

       public SystemControl SystemControl { get; set; }

    public int StudenId { get; set; }

      public int ProgramId { get; set; }

      public int DivisionId { get; set; }

       public string ProgramName { get; set; }
       public string Level { get; set; }
       public DateTime AcademicYear { get; set; }

       public string RegId { get; set; }
       public string UniversityRegistrationNo { get; set; }
       public string ProgramCode { get; set; }
    }
}
