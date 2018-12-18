using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScFeeReceipt
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public string ReceiptNo { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime ReceiptDate { get; set; }
        [Required(ErrorMessage = " ")]
        public string ReceiptMiti { get; set; }
        public DateTime ReceiptTimi { get; set; }
        [Required(ErrorMessage = " ")]
        public int GLCode { get; set; }
        [Required(ErrorMessage = " ")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = " ")]
        public int StudentId { get; set; }
       // [Required(ErrorMessage = " ")]
        public string BillNo { get; set; }
        public string Remarks { get; set; }
        public int PaidUpYear { get; set; }
        public int PaidUpMonth { get; set; }
        public int PaidUpMonthMiti { get; set; }
        public int PaidUpYearMiti { get; set; }
        public decimal FineAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ReceiptAmount { get; set; }
        public int MonthlyBillStudentId { get; set; }
        public int AcademicYearId { get; set; }
        public int CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User PreparedBy { get; set; }
        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }

        [ForeignKey("MonthlyBillStudentId")]
        public virtual ScMonthlyBillStudent MonthlyBillStudents { get; set; }

        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [NotMapped]
        public SelectList MonthList { get; set; }
        [NotMapped]
        public int Month { get; set; }
    }
}
