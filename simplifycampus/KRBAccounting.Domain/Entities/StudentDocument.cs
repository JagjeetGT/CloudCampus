using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace KRBAccounting.Domain.Entities
{
    public class StudentDocument
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string DocumentGuid { get; set; }
        public string DocumentExt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public HttpPostedFileBase Attachment { get; set; }
        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }
    }
}
