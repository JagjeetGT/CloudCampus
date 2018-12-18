using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScLibraryMemberRegistration
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
       [Remote("LibraryRegNoExists","Library",AdditionalFields = "Id")]
        public string RegNo { get; set; }
      public string Narration { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreateById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdateById { get; set; }
        public int AcademyYearId { get; set; }

        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }
        [ForeignKey("StaffId")]
        public virtual ScEmployeeInfo StaffMaster { get; set; }
        [NotMapped]
        public List<SelectListItem> Typelist { get; set; }

        [NotMapped]
        public string OldType { get; set; }
          [NotMapped]
        public int OLdClassId { get; set; }
          [NotMapped]
          public string RollNo { get; set; }

          [NotMapped]
          public string Section { get; set; }
        //[NotMapped]
        //public IEnumerable<ScLibraryMemberRegistration> MemberRegistrations { get; set; }

        [NotMapped]
        public IEnumerable<ScStudentRegistrationDetail> StudentRegistrationDetails { get; set; } 

        public virtual ICollection<ScLibraryCard> LibraryCards { get; set; } 
    }
}
