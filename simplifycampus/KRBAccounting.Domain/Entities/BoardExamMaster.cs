using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Win32;

namespace KRBAccounting.Domain.Entities
{
   public class BoardExamMaster
    {
       public int Id { get; set; }
       public int StudentId { get; set; }
       public int ProgramId { get; set; }
    
       public int? GraduationYear { get; set; }
       public decimal? Percentage { get; set; }
       public int? DivisionId { get; set; }
       public DateTime? CharacterCertificateIssueDate{get; set; }
       public string CharacterCertificateIssueMiti { get; set; }
       public DateTime? TranscriptIssueDate { get; set; }
       public string TranscriptIssueMiti { get; set; }
       public int? TranscriptNumber { get; set; }
       public int? CharacterCertificateNumber { get; set; }
       public string Remarks { get; set; }

       public string UniversityRegistrationNo { get; set; }       

       [ForeignKey("StudentId")]
       public virtual ScStudentinfo Studentinfo { get; set; }

       [ForeignKey("ProgramId")]
       public virtual ScProgramMaster Program { get; set; }

       [ForeignKey("DivisionId")]
       public virtual ScDivision Division { get; set; }

      [NotMapped]
       public SelectList StudentList { get; set; }

    }
}
