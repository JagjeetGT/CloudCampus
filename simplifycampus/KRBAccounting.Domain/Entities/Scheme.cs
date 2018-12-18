using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class Scheme{        [Key]        public int Id {get;set;}        [Required(ErrorMessage =  "*" )]        public string Name {get;set;}        public DateTime FromDate {get;set;}        public DateTime ToDate {get;set;}        public int EffectOn {get;set;}        public DateTime CreatedOn {get;set;}        public int CreatedBy {get;set;}        public bool IsDeleted {get;set;}        public bool IsActive {get;set;}

        [NotMapped]
        public string TrId { get; set; }    }}