using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class FinishedGoodReturn{        [Key]        public int Id {get;set;}        [Required(ErrorMessage =  " " )]        public string Code {get;set;}        [Required(ErrorMessage =  " " )]        public int FinishedGoodReceiveId {get;set;}        [Required(ErrorMessage =  " " )]        public DateTime ReturnDate {get;set;}        public string ReturnMiti {get;set;}        public int GoDownId {get;set;}        public int CreatedById {get;set;}        public DateTime CreatedDate {get;set;}        public int ModifiedById {get;set;}        public bool IsDeleted {get;set;}

        [ForeignKey("GoDownId")]
        public virtual Godown Godown { get; set; }    }}