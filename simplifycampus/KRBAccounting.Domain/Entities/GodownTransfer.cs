using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class GodownTransfer{        [Key]        public int Id {get;set;}        [Required(ErrorMessage =  " " )]        public string Code {get;set;}        [Required(ErrorMessage =  " " )]        public DateTime TransferDate {get;set;}        public string TransferMiti {get;set;}        [Required(ErrorMessage =  " " )]        public int FromGodownId {get;set;}        public int CreatedById {get;set;}        public DateTime CreatedDate {get;set;}        public int ModifiedById {get;set;}        public bool IsDeleted {get;set;}

        [ForeignKey("FromGodownId")]
        public virtual Godown Godown { get; set; }    }}