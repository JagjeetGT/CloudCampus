using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScEmployeeInfo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = " ")]
        [Remote("AcademyEmployeeInfoCodeExists", "EmployeeManagement", AdditionalFields = "Id")]
        public string Code { get; set; }

        [Required(ErrorMessage = " ")]
        public string Description { get; set; }

        public int EmployeeCategoryId { get; set; }
        public int EmployeeDepartmentId { get; set; }
        public int EmployeePostId { get; set; }

        [Required(ErrorMessage = " ")]
        public string Gender { get; set; }

        [Required(ErrorMessage = " ")]
        public string MaritalStatus { get; set; }

      
        public string Religion { get; set; }

        public int DeviceUserId { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string MitiOfBirth { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string MitiofJoin { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Weekholiday { get; set; }
        public int Status { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Country1 { get; set; }
        public string Address1 { get; set; }
        public string Phone1 { get; set; }
        public string Email1 { get; set; }
        public string Education { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public int BranchId { get; set; }
        public int LedgerId { get; set; }
        public int userId { get; set; }
        [ForeignKey("EmployeeCategoryId")]
        public virtual ScEmployeeCategory EmployeeCategory { get; set; }

        [ForeignKey("EmployeeDepartmentId")]
        public virtual ScEmployeeDepartment EmployeeDepartment { get; set; }
        [ForeignKey("EmployeePostId")]
        public virtual ScEmployeePost EmployeePost { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("userId")]
        public virtual User User { get; set; }

        [NotMapped]
        public SelectList CategoryList { get; set; }

        [NotMapped]
        public SelectList DepartmentList { get; set; }

        [NotMapped]
        public SelectList PositionList { get; set; }

        [NotMapped]
        public SelectList StatusList { get; set; }
        [NotMapped]
        public SelectList WeekholidayList { get; set; }

        [NotMapped]
        public bool Checkbox { get; set; }

        [NotMapped]
        public bool[] CheckList { get; set; }

        [NotMapped]
        public DateTime[] Date { get; set; }

        [NotMapped]
        public decimal NetAmount { get; set; }

        [NotMapped]
        public string DisplayJoinDate { get; set; }

        [NotMapped]
        public virtual SystemControl SystemControl { get; set; }

        [NotMapped]
        public string UserName { get; set; }


    }
}
