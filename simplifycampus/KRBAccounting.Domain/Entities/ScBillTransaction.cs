using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScBillTransaction
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public int StudentId { get; set; }
        public decimal BillAmount { get; set; }
        public decimal ReceiptAmount { get; set; }
        public string Source { get; set; }
        public int ReferenceId { get; set; }
        public int FYId { get; set; }
        public int AcademicYearId { get; set; }
        public int BranchId { get; set; }
        public string BillNo { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = " ")]
        public string BMiti { get; set; }
        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }


    }
}
