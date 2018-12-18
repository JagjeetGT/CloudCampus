using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.School
{
    public static class ModelMappers
    {

        public static ScStudentinfo ScStudentInfoMappers(ScStudentInfoViewModel viewModel)
        {
            var objScStudentInfo = new ScStudentinfo();
            objScStudentInfo.ApplyDate = viewModel.ApplyDate;
            objScStudentInfo.ApplyMiti = viewModel.ApplyMiti;
            objScStudentInfo.BloodGrp = viewModel.BloodGrp;
            objScStudentInfo.ClassId = viewModel.ClassId;
            objScStudentInfo.CurrClsCode = viewModel.CurrClsCode;
            objScStudentInfo.DBO = viewModel.DBO;
            objScStudentInfo.DBOMiti = viewModel.DBOMiti;
            objScStudentInfo.EmailId = viewModel.EmailId;
            objScStudentInfo.EntryDate = viewModel.EntryDate;
            objScStudentInfo.EntryMiti = viewModel.EntryMiti;
            objScStudentInfo.FEmail = viewModel.FEmail;
            objScStudentInfo.FMobile = viewModel.FMobile;
            objScStudentInfo.FName = viewModel.FName;
            objScStudentInfo.FOff = viewModel.FOff;
            objScStudentInfo.FOffAdd = viewModel.FOffAdd;
            objScStudentInfo.FPhoneOff = viewModel.FPhoneOff;
            objScStudentInfo.FPhoneRes = viewModel.FPhoneRes;
            objScStudentInfo.FProff = viewModel.FProff;
            objScStudentInfo.GEmail = viewModel.GEmail;
            objScStudentInfo.GMobile = viewModel.GMobile;
            objScStudentInfo.GName = viewModel.GName;
            objScStudentInfo.GOff = viewModel.GOff;
            objScStudentInfo.GOffAdd = viewModel.GOffAdd;
            objScStudentInfo.GPhoneOff = viewModel.GPhoneOff;
            objScStudentInfo.GPhoneRes = viewModel.GPhoneRes;
            objScStudentInfo.GProff = viewModel.GProff;
            objScStudentInfo.GRelation = viewModel.GRelation;
            objScStudentInfo.Institue = viewModel.Institue;
            objScStudentInfo.MEmail = viewModel.MEmail;
            objScStudentInfo.MMobile = viewModel.MMobile;
            objScStudentInfo.MName = viewModel.MName;
            objScStudentInfo.MOff = viewModel.MOff;
            objScStudentInfo.MOffAdd = viewModel.MOffAdd;
            objScStudentInfo.MPercent = viewModel.MPercent;
            objScStudentInfo.MPhoneOff = viewModel.MPhoneOff;
            objScStudentInfo.MPhoneRes = viewModel.MPhoneRes;
            objScStudentInfo.MPhoneRes = viewModel.MPhoneRes;
            objScStudentInfo.MProff = viewModel.MProff;
            objScStudentInfo.MaritialSt = viewModel.MaritialSt;
            objScStudentInfo.Nationality = viewModel.Nationality;
            objScStudentInfo.PassYear = viewModel.PassYear;
            objScStudentInfo.PassMiti = viewModel.PassMiti;
            objScStudentInfo.PerAdd = viewModel.PerAdd;
            objScStudentInfo.PerCountry = viewModel.PerCountry;
            objScStudentInfo.PerCity = viewModel.PerCity;
            objScStudentInfo.PerDistrict = viewModel.PerDistrict;
            objScStudentInfo.PerPhone = viewModel.PerPhone;
            objScStudentInfo.PerWardNo = viewModel.PerWardNo;
            objScStudentInfo.PerState = viewModel.PerState;
            objScStudentInfo.Phone = viewModel.Phone;
            objScStudentInfo.PrevClsCode = viewModel.PrevClsCode;
            objScStudentInfo.PrevClassId = viewModel.PrevClsId;
            objScStudentInfo.Regno = viewModel.Regno;
            objScStudentInfo.Religion = viewModel.Religion;
            //objScStudentInfo.SectionId = viewModel.SectionId;
            objScStudentInfo.Sex = viewModel.Sex;
            objScStudentInfo.StHeight = viewModel.StHeight;
            objScStudentInfo.StWeight = viewModel.StWeight;
            objScStudentInfo.StdCategoryId = viewModel.StdCategoryId;
            objScStudentInfo.StdCode = viewModel.StdCode;
            objScStudentInfo.StdPhotoExt = viewModel.StdPhotoExt;
            objScStudentInfo.StdPhotoFileName = viewModel.StdPhotoFileName;
            objScStudentInfo.StuDesc = viewModel.StuDesc;
            objScStudentInfo.StuMemo = viewModel.StuMemo;
            objScStudentInfo.StudentID = viewModel.StudentID;
            objScStudentInfo.TmpAdd = viewModel.TmpAdd;
            objScStudentInfo.TmpCity = viewModel.TmpCity;
            objScStudentInfo.TmpCountry = viewModel.TmpCountry;
            objScStudentInfo.TmpDistrict = viewModel.TmpDistrict;
            objScStudentInfo.TmpPhone = viewModel.TmpPhone;
            objScStudentInfo.TmpState = viewModel.TmpState;
            objScStudentInfo.TmpWardNo = viewModel.TmpWardNo;
            objScStudentInfo.GLCode = viewModel.GLCode;
            return objScStudentInfo;
        }
    }
}