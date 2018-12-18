using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{    public class OpeningStudent{        [Key]        public int Id {get;set;}        public int LedgerId {get;set;}        public int StudentId {get;set;}        public decimal Amount {get;set;}        public int AmountType {get;set;}        public int CreatedById {get;set;}        public DateTime CreatedDate {get;set;}        public int BranchId {get;set;}        public int FiscalYearId {get;set;}        public int AcademyId {get;set;}
        public int CategoryId { get; set; }
        [NotMapped]
        public SelectList AmountTypeList { get; set; }
        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User User { get; set; }    }}