using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{    public class ScRoom
{
        [Key]
        public int Id {get;set;}
        [Required(ErrorMessage =  " " )]
        [Remote("AcademyRoomCodeExists","School",AdditionalFields = "Id")]
        public string Code {get;set;}
        public int Seats {get;set;}
        public string Memo {get;set;}
        [Required(ErrorMessage =  " " )]
        [Remote("AcademyRoomDescriptionExists","School", AdditionalFields = "Id")]
        public string Description {get;set;}
    }
}
