using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScGrade
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("SchoolGradeCodeNoExists", "School", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        public string Grade { get; set; }
        public string Memo { get; set; }
    }
}
