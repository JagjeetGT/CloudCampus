using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class PyPayrollGenerationDetail{        [Key]        public int Id {get;set;}        public int PayrollGenerationId {get;set;}        public int ReferenceId {get;set;}        public decimal Amount {get;set;}

        [ForeignKey("PayrollGenerationId")]
        public virtual PyPayrollGeneration PayrollGeneration { get; set; }    }}