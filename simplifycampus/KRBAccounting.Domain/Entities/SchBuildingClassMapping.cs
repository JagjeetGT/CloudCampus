using System.ComponentModel.DataAnnotations;

namespace KRBAccounting.Domain.Entities
{
    public class SchBuildingClassMapping
    {
        [Key]
        public int Id { get; set; }

        public int BuildingID { get; set; }
        public int ClassID { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("BuildingID")]
        public SchBuilding SchBuilding { get; set; }

        [ForeignKey("ClassID")]
        public SchClass SchClass { get; set; }
    }
}
