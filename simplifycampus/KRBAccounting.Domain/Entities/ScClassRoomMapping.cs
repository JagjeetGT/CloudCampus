using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScClassRoomMapping
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int ClassId { get; set; }
        
        public int SectionId { get; set; }
        public int RoomMappingId { get; set; }
       
        [NotMapped]
        public SelectList BuildingList { get; set; }

        [NotMapped]
        public int BuildingId { get; set; }

        [NotMapped]
        public int BuildingRoomMappingId { get; set; }

        [ForeignKey("SectionId")]
        public virtual ScSection Section { get; set; }

        [ForeignKey("RoomMappingId")]
        public virtual SchBuildingRoomMapping RoomMapping { get; set; }
        
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }

    }
}
