using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class SchBuilding
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required Field Missing!")]
        [RegularExpression(@"^[\w ]+$", ErrorMessage = "Please Insert AlphaNumeric Only.")]
        [Remote("CheckCodeInBuilding", "School", AdditionalFields = "Id")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Required Field Missing!")]
        [Remote("CheckDescriptionInBuilding", "School", AdditionalFields = "Id")]
        public string Description { get; set; }
    }
}
