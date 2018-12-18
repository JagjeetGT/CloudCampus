using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScProgramMaster
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckCodeInProgramMaster", "School", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckDescriptionInProgramMaster", "School", AdditionalFields = "Id")]
        public string Description { get; set; }
        public string Incharge { get; set; }
        [Required(ErrorMessage = " ")]
        public string FacultyDescription { get; set; }
         [Required(ErrorMessage = " ")]
        public string LevelDescription { get; set; }
         [Required(ErrorMessage = " ")]
        public string UniversityDescription { get; set; }
     
        [Required(ErrorMessage = "*")]
        public int Schedule { get; set; }
        [NotMapped]
        public IEnumerable<string> FacultyList { get; set; }
        [NotMapped]
        public IEnumerable<string> LevelList { get; set; }
        [NotMapped]
        public IEnumerable<string> UniversityList { get; set; }
        [NotMapped]
        [Required(ErrorMessage = " ")]
        public int TotalSem { get; set; }
        //[NotMapped]
        //public string ClassNameCode { get; set; }

        
        public int DurationId { get; set; }
       
    }
}
