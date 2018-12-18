using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{    public class PyAllowanceMaster
{
        [Key]
        public int Id {get;set;}
        [Required(ErrorMessage =  "*" )]
        [Remote("AllowanceMastersNameExists", "Payroll", AdditionalFields = "Id")]
        public string Name {get;set;}
        public int Type {get;set;}
        public bool IsFlat { get; set; }
        public bool IsAnnual {get;set;}
        public decimal Value {get;set;}
        public bool Status {get;set;}
        public int CreatedBy {get;set;}
        public DateTime CreatedOn {get;set;}
        public int FiscalYearId {get;set;}
        public int BranchId { get; set; }
    [NotMapped]
        public SelectList AllowanceType { get; set; }
    [ForeignKey("CreatedBy")]
    public virtual User user { get; set; }

    }
}
