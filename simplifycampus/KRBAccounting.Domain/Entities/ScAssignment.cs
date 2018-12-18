using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScAssignment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public int CreateById { get; set; }
     
        public int ClassId { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string TempUrl { get; set; }
     
        public int SubjectId { get; set; }

        public int SectionId { get; set; }
        
        [NotMapped]
        public SelectList ClassList { get; set; }
        [NotMapped]
        public SelectList SubjectList { get; set; }
        [NotMapped]
        public int ClassTeacherId { get; set; }
    }
}
