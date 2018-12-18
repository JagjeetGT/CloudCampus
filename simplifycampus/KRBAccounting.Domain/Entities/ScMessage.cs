using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
   public class ScMessage
    {
       [Key]
       public int Id { get; set; }

       public DateTime MsgDate { get; set; }
       [Required(ErrorMessage = "*")]
       public string MsgBody { get; set; }
       public string MsgTime { get; set; }
       public int SenderId { get; set; }
       public int ReceiverId { get; set; }
       public bool IsRead { get; set; }
         [Required(ErrorMessage = "*")]
       public string MsgSubject { get; set; }

         public bool IsDeleted { get; set; }
       [NotMapped]
       public string MsgTitle { get; set; }
       [NotMapped]
       public string StudentName { get; set; }
        [NotMapped]
       public SelectList UserType { get; set; }

        [NotMapped]
        public int ClassId { get; set; }

        [NotMapped]
        public int SectionId { get; set; }
  
        public virtual User Receiver { get; set; }
    }
   
}
