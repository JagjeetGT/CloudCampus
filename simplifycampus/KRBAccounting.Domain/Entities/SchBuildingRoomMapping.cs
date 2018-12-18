using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{    public class SchBuildingRoomMapping{        [Key]        public int Id {get;set;}        public int BuildingId {get;set;}        public int RoomId {get;set;}        public bool IsActive {get;set;}
        public bool IsSelected { get; set; }

        [NotMapped]
        public SelectList BuildingList { get; set; }
        [NotMapped]
        public SelectList RoomList { get; set; }

        [ForeignKey("RoomId")]
        public virtual ScRoom Room { get; set; }
        [ForeignKey("BuildingId")]
        public virtual SchBuilding Building { get; set; }

        [NotMapped]
        public int OldBuildingId { get; set; }

        [NotMapped]
        public int OldRoomId { get; set; }}}