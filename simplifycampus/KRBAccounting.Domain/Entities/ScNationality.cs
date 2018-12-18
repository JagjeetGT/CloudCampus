using System.ComponentModel.DataAnnotations;

namespace KRBAccounting.Domain.Entities
{
    public class ScNationality
    {
        [Key]
        public int Id { get; set; }
        public string Nationality { get; set; }
    }
}
