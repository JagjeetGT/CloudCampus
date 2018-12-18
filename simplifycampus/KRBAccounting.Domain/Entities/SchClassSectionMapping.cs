using System.ComponentModel.DataAnnotations;

namespace KRBAccounting.Domain.Entities
{
    public class SchClassSectionMapping
    {
        [Key]
        public int Id { get; set; }

        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("ClassId")]
        public SchClass SchClass { get; set; }
    }
}
