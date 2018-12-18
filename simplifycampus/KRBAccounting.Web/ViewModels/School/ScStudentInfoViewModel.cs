using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.School
{
    public class ScStudentInfoViewModel : BaseViewModel
    {
        [Key]
        public int StudentID { get; set; }

        public string StdCode { get; set; }
        public ScStudentinfo Studentinfo { get; set; }
        [Required(ErrorMessage = "Student name is required")]
        public string StuDesc { get; set; }
        public int GLCode { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string BloodGrp { get; set; }
        public string MaritialSt { get; set; }
        public string Sex { get; set; }
        public DateTime? DBO { get; set; }
        public string DBOMiti { get; set; }
        public string DBODisplay { get; set; }
        public DateTime? ApplyDate { get; set; }
        public string ApplyMiti { get; set; }
        public string ApplyDateDisplay { get; set; }
        public DateTime? EntryDate { get; set; }
        public string EntryDateDisplay { get; set; }
        public string EntryMiti { get; set; }
        public string Regno { get; set; }

        [RegularExpression(
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
            ,
            ErrorMessage = "Please enter a valid e-mail adress!! E.g. abc@company.com.")]
        public string EmailId { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }

        public string PrevClsCode { get; set; }
        public int? PrevClsId { get; set; }
        public string Institue { get; set; }
        public DateTime? PassYear { get; set; }
        public string PassMiti { get; set; }
        public string PassYerDisplay { get; set; }
        public decimal MPercent { get; set; }

        [Required(ErrorMessage = "Class code is required")]
        public string CurrClsCode { get; set; }

        public string GName { get; set; }
        public string GRelation { get; set; }
        public string GProff { get; set; }
        public string GOff { get; set; }
        public string GOffAdd { get; set; }

        [RegularExpression(
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
            ,
            ErrorMessage = "Please enter a valid e-mail adress!! E.g. abc@company.com.")]
        public string GEmail { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string GMobile { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string GPhoneOff { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string GPhoneRes { get; set; }

        public string FName { get; set; }
        public string FProff { get; set; }
        public string FOff { get; set; }
        public string FOffAdd { get; set; }

        [RegularExpression(
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
            ,
            ErrorMessage = "Please enter a valid e-mail adress!! E.g. abc@company.com.")]
        public string FEmail { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string FMobile { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string FPhoneOff { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string FPhoneRes { get; set; }

        public string MName { get; set; }
        public string MProff { get; set; }
        public string MOff { get; set; }
        public string MOffAdd { get; set; }

        [RegularExpression(
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
            ,
            ErrorMessage = "Please enter a valid e-mail adress!! E.g. abc@company.com.")]
        public string MEmail { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string MMobile { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string MPhoneOff { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string MPhoneRes { get; set; }

        public string PerAdd { get; set; }
        public string PerWardNo { get; set; }
        public string PerCity { get; set; }
        public string PerDistrict { get; set; }
        public string PerState { get; set; }
        public string PerCountry { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string PerPhone { get; set; }

        public string TmpAdd { get; set; }
        public string TmpWardNo { get; set; }
        public string TmpCity { get; set; }
        public string TmpDistrict { get; set; }
        public string TmpState { get; set; }
        public string TmpCountry { get; set; }

        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid phone number.")]
        public string TmpPhone { get; set; }

        public string LedgerNo { get; set; }
        public string StHeight { get; set; }
        public string StWeight { get; set; }
        public string StuMemo { get; set; }
        //public string SecCode { get; set; }
        public int StdCategoryId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        //public string StdPhotoPath { get; set; }
        public string StdPhotoFileName { get; set; }
        public string StdPhotoExt { get; set; }
         [NotMapped]
        public string SectionName { get; set; }
        [NotMapped]
        public SelectList StudentCategories { get; set; }
        public IEnumerable<AcademicBackground> AcademicBackground { get; set; }
        public SelectList DivisionList { get; set; }


    }
}