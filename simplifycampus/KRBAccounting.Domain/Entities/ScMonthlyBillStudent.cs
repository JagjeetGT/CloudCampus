using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
   public class ScMonthlyBillStudent
    {
       [Key]
       public int Id { get; set; }
       public int StudentId { get; set; }
       public decimal Amount { get; set; }
       public int ClassId { get; set; }
       [Required(ErrorMessage = " ")]
       public int Year { get; set; }
       public int Month { get; set; }
       public int YearMiti { get; set; }
       public int MonthMiti { get; set; }
       public DateTime Date { get; set; }
       public string Miti { get; set; }
       public string Remarks { get; set; }
       public int BoaderId { get; set; }
       public int ShiftId { get; set; }
       public string BillNo { get; set; }
       public int AcademicYearId { get; set; }
       public int CreatedById { get; set; }
       public DateTime CreatedDate { get; set; }
       public string CreatedMiti { get; set; }

       [NotMapped]
       public virtual string RollNo { get; set; }
       [NotMapped]
       public virtual string SectionName { get; set; }
       [NotMapped]
       public decimal EducationTax { get; set; }
       [ForeignKey("CreatedById")]
       public virtual User PreparedBy { get; set; }
       [ForeignKey("StudentId")]
       public virtual ScStudentinfo ScStudentinfo { get; set; }
       [ForeignKey("ClassId")]
       public virtual SchClass Class { get; set; }
       public virtual ICollection<ScMonthlyBill> MonthlyBills { get; set; }
       public virtual ICollection<ScFeeReceipt> FeeReceipts { get;set; }
         [NotMapped]
       public decimal DueAmount { get; set; }


    }
}
 