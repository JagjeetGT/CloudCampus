using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class SmsGroupDetail{        [Key]        public int Id {get;set;}        public int GroupId {get;set;}        public int ReferenceId {get;set;}
        public int ReferenceType { get; set; }    }}