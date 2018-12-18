using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class ScBusStop{        [Key]        public int Id {get;set;}        [Required(ErrorMessage =  " " )]        public string Description {get;set;}        public string Memo {get;set;}    }}