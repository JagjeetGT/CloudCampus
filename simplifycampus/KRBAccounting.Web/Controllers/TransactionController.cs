using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.Transaction;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.Services;

using LinqKit;

namespace KRBAccounting.Web.Controllers
{
    [Authorize]
    [CheckModulePermission("Academy")]
    public class TransactionController : BaseController
    {
        //
        // GET: /Transaction/
        public IScMonthlyBillRepository _scMonthlyBillRepository { get; set; }
        public IScFeeItemRepository _scFeeItemRepository { get; set; }
        public IScStudentinfoRepository _scStudentinfo { get; set; }
        public IScStudentRegistrationDetailRepository _scStudentRegistrationDetail { get; set; }
        public IScMonthlyBillStudentRepository _monthlyBillStudentRepository { get; set; }
        public IMaterialIssue_MasterRepository _materialissueMasterRepository { get; set; }
        private readonly ISystemControlRepository _systemControl;
        public IMaterialIssue_DetailsRepository _materialissueDetailsRepository { get; set; }
        public IAccountTransactionRepository _accountTransaction { get; set; }
        private readonly IDocumentNumeringSchemeRepository _documentNumeringSchemeRepository;
        private readonly IFormsAuthenticationService _authentication;
        private readonly IFunctionService _functionService;
        private readonly IStockTransactionRepository _stockTransaction;
        private readonly IProductRepository _productRepository;
        private readonly IScEmployeeInfoRepository _staffMasterRepository;
        private readonly IScMaterialReturnDetailsRepository _scmaterialreturndetailsRepository;
        private readonly IScMaterialReturnMasterRepository _scmaterialreturnmasterRepository;
        private readonly IScBillTransactionRepository _scbilltransactionRepository;
        private readonly IScFeeReceiptRepository _scfeereceiptRepository;
        private readonly IScStudentFeeRateMasterRepository _scStudentFeeRateMasterRepository;
        private readonly IScStudentFeeRateDetailRepository _scStudentFeeRateDetailRepository;
        private readonly IScStudentFeeTermRepository _scStudentFeeTermRepository;
        private int CurrentAcademyYearId;

        public TransactionController(
            IScMonthlyBillRepository scMonthlyBillRepository,
            IScFeeItemRepository scFeeItemRepository,
            IScStudentRegistrationDetailRepository scStudentRegistrationDetailRepository,
            IScStudentinfoRepository scStudentinfoRepository,
            IScMonthlyBillStudentRepository monthlyBillStudentRepository,
            IMaterialIssue_MasterRepository materialissueMasterRepository,
            IScStudentFeeRateMasterRepository scStudentFeeRateMasterRepository,
            ISystemControlRepository systemControlRepository,
            IMaterialIssue_DetailsRepository materialissueDetailsRepository,
            IAccountTransactionRepository accountTransactionRepository,
            IScStudentFeeRateDetailRepository scStudentFeeRateDetailRepository,
         IStockTransactionRepository stockTransactionRepository,
          IFunctionService functionService,
            IProductRepository productRepository,
            IScEmployeeInfoRepository staffMasterRepository,
            IDocumentNumeringSchemeRepository documentNumeringSchemeRepository,
            IScMaterialReturnDetailsRepository materialReturnDetailsRepository,
            IScMaterialReturnMasterRepository materialreturnmasterRepository,
            IScBillTransactionRepository scbilltransactionRepository,
            IScFeeReceiptRepository scfeereceiptRepository,
            IScStudentFeeTermRepository scStudentFeeTermRepository
            )
        {
            _scMonthlyBillRepository = scMonthlyBillRepository;
            _scFeeItemRepository = scFeeItemRepository;
            _scStudentRegistrationDetail = scStudentRegistrationDetailRepository;
            _scStudentinfo = scStudentinfoRepository;
            _monthlyBillStudentRepository = monthlyBillStudentRepository;
            _materialissueMasterRepository = materialissueMasterRepository;
            _systemControl = systemControlRepository;
            _materialissueDetailsRepository = materialissueDetailsRepository;
            _accountTransaction = accountTransactionRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Academy);
            _functionService = functionService;
            _stockTransaction = stockTransactionRepository;
            _productRepository = productRepository;
            _staffMasterRepository = staffMasterRepository;
            _documentNumeringSchemeRepository = documentNumeringSchemeRepository;
            _scmaterialreturndetailsRepository = materialReturnDetailsRepository;
            _scmaterialreturnmasterRepository = materialreturnmasterRepository;
            _scbilltransactionRepository = scbilltransactionRepository;
            _scfeereceiptRepository = scfeereceiptRepository;
            _scStudentFeeTermRepository = scStudentFeeTermRepository;
            _scStudentFeeRateDetailRepository = scStudentFeeRateDetailRepository;
            _scStudentFeeRateMasterRepository = scStudentFeeRateMasterRepository;
            CurrentAcademyYearId = AcademicYear.Id;

        }



        

   
         [CheckPermission(Permissions = "Navigate", Module = "ScMbGE")]
        public ActionResult MonthlyBillGenerationEdit()
         {
            
            var model = this.BuildMonthlyBillGenerationAddViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetStudentByClassIdEdit(MonthlyBillGenerationViewModel model)
        {
            var nepdate = NepaliDateService.NepaliDate(DateTime.Now).Date;
            var date = "";
            var engdate = new DateTime();
         
           var  scbillStudent =
               _monthlyBillStudentRepository.GetMany(x => x.ClassId == model.MonthlyBill.ClassId && x.AcademicYearId == CurrentAcademyYearId && x.MonthMiti == model.Month);
            if (base.SystemControl.DateType == 1)
            {
                date = model.Month + "/1/" + model.Year;
                engdate = Convert.ToDateTime(date);
                nepdate = NepaliDateService.NepaliDate(engdate).Date;
                 scbillStudent =_monthlyBillStudentRepository.GetMany(
                    x => x.ClassId == model.MonthlyBill.ClassId && x.AcademicYearId == CurrentAcademyYearId && x.Month == model.Month);

            }
            else
            {
                nepdate = model.Year + "/" + model.Month + "/1";

                engdate = NepaliDateService.NepalitoEnglishDate(nepdate);
            }

            //var d =
            //    _monthlyBillStudentRepository.GetMany(
            //        x =>

            //        x.Year == engdate.Year && x.AcademicYearId == CurrentAcademyYearId && x.ClassId==model.MonthlyBill.ClassId).Select(x => x.StudentId);
            var studentId = new List<int>();
            
            if (scbillStudent != null)
                studentId = new List<int>(scbillStudent.Select(x => x.StudentId));
            var data =
                  _scStudentRegistrationDetail.GetMany(
                      x =>
                      x.StudentRegistration.ClassId == model.MonthlyBill.ClassId
                           && studentId.Contains(x.StudentId) 
                      &&
                      x.StudentRegistration.AcademicYearId == CurrentAcademyYearId && x.ShiftId == model.MonthlyBill.ShiftId &&
                      x.BoaderId == model.MonthlyBill.BoaderId);



            var list = new List<MonthlyBillGenerationDetailViewModel>();
            var sno = 1;
            foreach (var item in data.Distinct())
            {
                var sec = "";
                if (item.Section != null)
                    sec = item.Section.Description;
                decimal amount = 0;
                decimal educationTax = 0;
                var  studentMonthlyBillId = 0;
                var studentBill = _monthlyBillStudentRepository.GetById(
                        x =>x.StudentId == item.StudentId && x.ClassId == model.MonthlyBill.ClassId && x.MonthMiti==model.Month);
                if(base.SystemControl.DateType==1)
                {
                    studentBill = _monthlyBillStudentRepository.GetById(
                        x => x.StudentId == item.StudentId && x.ClassId == model.MonthlyBill.ClassId && x.Month == model.Month);
                   
                }
                if(studentBill != null)
                {
                    
                    var studentMonthlyBill =
                        _scMonthlyBillRepository.GetMany(x => x.MonthlyBillStudentId == studentBill.Id);
                    amount = studentMonthlyBill.Sum(x => x.FeeAmount);
                    educationTax = studentMonthlyBill.Sum(x => x.EducationTaxAmount);
                    studentMonthlyBillId = studentBill.Id;
                }
                
                        
                var a = new MonthlyBillGenerationDetailViewModel
                {
                    Section = sec,
                    StudentId = item.StudentId,
                    RollNo = item.RollNo,
                    Code = item.Studentinfo.StdCode,
                    Status = Enum.GetName(typeof(StudentStatus), item.CurrentStatus),
                    sno = sno,
                    StudentName = item.Studentinfo.StuDesc,
                    Amount = amount,
                    EducationTax = educationTax,
                    MonthlyStudentBillId = studentMonthlyBillId
                };

                list.Add(a);
                sno++;
            }

            var partialView = this.RenderPartialViewToString("_PartialMonthlyBillGenerationDetailEntry", list.GroupBy(x => x.StudentId));


            return Json(new { view = partialView, value = list.Count() }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetStudentMonthlyFeeTermDetails(int studentMonthlyBillId)
        {
            var data = _scMonthlyBillRepository.GetMany(x => x.MonthlyBillStudentId == studentMonthlyBillId);
            return PartialView(data);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScMbG")]
        public ActionResult MonthlyBillGenerationAdd()
        {

            var model = this.BuildMonthlyBillGenerationAddViewModel();
            return View(model);
        }
        //public ActionResult MonthlyBillGenerationList()
        //{
        //    var data = _monthlyBillStudentRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId);
        //    return PartialView("_PartialMonthlyBillGerationList", data);
        //}
        public MonthlyBillGenerationViewModel BuildMonthlyBillGenerationAddViewModel()
        {
            var AdmissionFee = _scFeeItemRepository.GetMany(x => x.Type == (int)FeeType.Admission);
            var Monthly = _scFeeItemRepository.GetMany(x => x.Type == (int)FeeType.Monthly);
            var Exam = _scFeeItemRepository.GetMany(x => x.Type == (int)FeeType.Exam);
            var Others = _scFeeItemRepository.GetMany(x => x.Type == (int)FeeType.Others);
            var viewModel = base.CreateViewModel<MonthlyBillGenerationViewModel>();
            viewModel.AdmissionFeeList = AdmissionFee.Select(x => new CheckBoxListInfo
                                                                  {
                                                                      DisplayText = x.Description,
                                                                      Value = x.Id.ToString()
                                                                  }).ToList();




            viewModel.MonthlyFeeList = Monthly.Select(x => new CheckBoxListInfo
            {
                DisplayText = x.Description,
                Value = x.Id.ToString()
            }).ToList();
            viewModel.ExamFeeList = Exam.Select(x => new CheckBoxListInfo
            {
                DisplayText = x.Description,
                Value = x.Id.ToString()
            }).ToList();
            viewModel.OtherList = Others.Select(x => new CheckBoxListInfo
            {
                DisplayText = x.Description,
                Value = x.Id.ToString()
            }).ToList();


            if (base.SystemControl.DateType == 1)
            {
                 viewModel.MonthLists=DropDownHelper.CreateMonthDate();
                 viewModel.MonthList = DropDownHelper.CreateMonthDate().Select(x => new CheckBoxListInfo
                 {
                     DisplayText = x.Text,
                     Value = x.Value as string
                 }).ToList();
            }
               
            else{ 
                viewModel.MonthList = DropDownHelper.CreateMonthMiti().Select(x => new CheckBoxListInfo
            {
                DisplayText = x.Text,
                Value = x.Value as string
            }).ToList();
                viewModel.MonthLists = DropDownHelper.CreateMonthMiti();
            }

           
            var nepyear = NepaliDateService.NepaliDate(DateTime.Now).Year;
            viewModel.MonthlyBill = new ScMonthlyBillStudent();
            viewModel.Month = Convert.ToInt32(viewModel.MonthList.FirstOrDefault().Value);
            viewModel.Year = base.SystemControl.DateType == 1 ? DateTime.Now.Year : nepyear;
            return viewModel;
        }

        [HttpPost]
        public ActionResult MonthlyBillGenerationAdd(MonthlyBillGenerationViewModel model, IEnumerable<MonthlyBillGenerationDetailViewModel> subjectEntry, List<int> FeeItems)
        {
            //  var 
            //     nepdate = NepaliDateService.NepaliDate(DateTime.Now).Date;
            //  var date = "";
            //  var endate = new DateTime();
            //if(base.SystemControl.DateType == 1)
            //{
            //    date = model.Month + "/1/" + model.Year;
            //    endate = Convert.ToDateTime(date);
            //    nepdate = NepaliDateService.NepaliDate(endate).Date;
            //}
            //else
            //{
            //    nepdate = model.Year+"/"+ model.Month + "/1" ;

            //  endate = NepaliDateService.NepalitoEnglishDate(nepdate);
            //}
            //var MonthMiti = NepaliDateService.NepaliDate(endate).Month;
            //  var YearMiti = NepaliDateService.NepaliDate(endate).Year;
            var Source = StringEnum.Parse(typeof(ModuleEnum), "Monthly Bill Generation").ToString();
            //  List<string>  amount = studentAmount.Where(x=>x.ToString() !="0").ToList();
            var usr = _authentication.GetAuthenticatedUser();

            var sno = 1;
            foreach (var item in subjectEntry.Where(x => x.Checkbox && x.Amount != 0))
            {
                if (item.MonthList != null)
                {
                    foreach (var month in item.MonthList.Distinct())
                    {


                        var
                            nepdate = NepaliDateService.NepaliDate(DateTime.Now).Date;
                        var date = "";
                        var endate = new DateTime();
                        if (base.SystemControl.DateType == 1)
                        {
                            date = month + "/1/" + model.Year;
                            endate = Convert.ToDateTime(date);
                            nepdate = NepaliDateService.NepaliDate(endate).Date;
                        }
                        else
                        {
                            nepdate = model.Year + "/" + month + "/1";

                            endate = NepaliDateService.NepalitoEnglishDate(nepdate);
                        }
                        var MonthMiti = NepaliDateService.NepaliDate(endate).Month;
                        var YearMiti = NepaliDateService.NepaliDate(endate).Year;
                        var vNo = UtilityService.GetDocumentNumberingByModuleName(Source);
                        var createdate = DateTime.UtcNow;
                        var createmiti = NepaliDateService.NepaliDate(Convert.ToDateTime(createdate)).Date;
                        var billdetail = new ScMonthlyBillStudent
                        {
                            Amount = item.Amount + item.EducationTax,
                            ClassId = model.MonthlyBill.ClassId,
                            Month = endate.Month,
                            Remarks = model.MonthlyBill.Remarks,
                            Year = endate.Year,
                            Date = endate,
                            StudentId = item.StudentId,
                            BoaderId = model.MonthlyBill.BoaderId,
                            ShiftId = model.MonthlyBill.ShiftId,
                            BillNo = vNo,
                            AcademicYearId = CurrentAcademyYearId,
                            Miti = nepdate,
                            MonthMiti = MonthMiti,
                            YearMiti = YearMiti,
                            CreatedById = usr.Id,
                            CreatedDate = createdate,
                            CreatedMiti = createmiti
                        };
                        _monthlyBillStudentRepository.Add(billdetail);



                          
                      
                        var documentnumber =
                            _documentNumeringSchemeRepository.GetById(
                                x => x.ModuleName.ToLower().Trim() == Source.ToLower().Trim());
                        if (documentnumber != null)
                        {
                            documentnumber.DocCurrNo += 1;
                            _documentNumeringSchemeRepository.Update(documentnumber);
                        }
                        var student = _scStudentinfo.GetById(x => x.StudentID == item.StudentId);
                        var accountTransactionDetail = new AccountTransaction();
                        accountTransactionDetail.VNo = billdetail.BillNo;
                        accountTransactionDetail.VDate = DateTime.UtcNow.Date;
                        if (student != null) accountTransactionDetail.GlCode = base.SystemControl.StudentFeeAc;
                        accountTransactionDetail.SlCode = 0;
                        accountTransactionDetail.ReferenceId = billdetail.Id;
                        accountTransactionDetail.DrAmt = Convert.ToDecimal(item.Amount + item.EducationTax);
                        accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.Amount + item.EducationTax);
                        accountTransactionDetail.Narration = model.MonthlyBill.Remarks;
                        accountTransactionDetail.Source = Source;
                        accountTransactionDetail.SNo = sno;
                        accountTransactionDetail.CreatedById = usr.Id;
                        accountTransactionDetail.FYId = FiscalYear.Id;
                        accountTransactionDetail.BranchId = base.CurrentBranch.Id;
                        _accountTransaction.Add(accountTransactionDetail);
                        var billtransaction = new ScBillTransaction()
                                                  {
                                                      BillAmount = item.Amount + item.EducationTax,
                                                      ReceiptAmount = 0,
                                                      ReferenceId = billdetail.Id,
                                                      StudentId = billdetail.StudentId,
                                                      BranchId = base.CurrentBranch.Id,
                                                      FYId = base.FiscalYear.Id,
                                                      Source = Source,
                                                      BillNo = vNo,
                                                      Date = billdetail.Date,
                                                      BMiti = billdetail.Miti,
                                                      AcademicYearId = CurrentAcademyYearId

                                                  };
                        _scbilltransactionRepository.Add(billtransaction);


                        sno++;
                        var fesno = 1;
                        foreach (var feeItem in FeeItems)
                        {
                            SP_MonthlyBillFeeWise mon_fee =
                                UtilityService.GetMonthlyBillGenerateFeeWise(model.MonthlyBill.ClassId,
                                                                             model.MonthlyBill.BoaderId,
                                                                             model.MonthlyBill.ShiftId,
                                                                             item.StudentId.ToString(),
                                                                             feeItem.ToString()).FirstOrDefault();

                            if (mon_fee != null)
                            {
                                decimal amount = 0;
                                amount = mon_fee.NetAmount != 0 ? mon_fee.NetAmount : mon_fee.Amount;
                                var EducationTax = UtilityService.CheckEducationTax(feeItem);
                                decimal taxamount = 0;
                                if (EducationTax)
                                {
                                    taxamount = amount * Convert.ToDecimal(0.01);
                                }
                                var data = new ScMonthlyBill()
                                               {

                                                   FeeItemId = feeItem,
                                                   FeeAmount = amount,
                                                   MonthlyBillStudentId = billdetail.Id,
                                                   EducationTaxAmount = taxamount

                                               };
                                _scMonthlyBillRepository.Add(data);

                                var fee = _scFeeItemRepository.GetById(x => x.Id == feeItem);

                                var accountTransactionMaster = new AccountTransaction();
                                accountTransactionMaster.VNo = billdetail.BillNo;
                                accountTransactionMaster.VDate = DateTime.UtcNow.Date;
                                if (fee != null) accountTransactionMaster.GlCode = fee.GLCode;
                                accountTransactionMaster.SlCode = 0;
                                accountTransactionMaster.ReferenceId = billdetail.Id;
                                accountTransactionMaster.CrAmt = Convert.ToDecimal(mon_fee.Amount);
                                accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(mon_fee.Amount);
                                accountTransactionMaster.Narration = model.MonthlyBill.Remarks;
                                accountTransactionMaster.Source = Source;
                                accountTransactionMaster.SNo = fesno;
                                accountTransactionMaster.CreatedById = usr.Id;
                                accountTransactionMaster.FYId = FiscalYear.Id;
                                accountTransactionMaster.BranchId = base.CurrentBranch.Id;

                                _accountTransaction.Add(accountTransactionMaster);
                                if (EducationTax && taxamount != 0)
                                {

                                    var accountTransaction = new AccountTransaction();
                                    accountTransaction.VNo = billdetail.BillNo;
                                    accountTransaction.VDate = DateTime.UtcNow.Date;
                                    if (fee != null)
                                    {
                                        accountTransaction.GlCode = SystemControl.EducationTaxAc != 0
                                                                        ? SystemControl.EducationTaxAc
                                                                        : fee.GLCode;
                                    }

                                    accountTransaction.SlCode = 0;
                                    accountTransaction.ReferenceId = billdetail.Id;
                                    accountTransaction.CrAmt = Convert.ToDecimal(taxamount);
                                    accountTransaction.LocalCrAmt = Convert.ToDecimal(taxamount);
                                    accountTransaction.Narration = "Education Tax have been Received From" +
                                                                   item.StudentName;
                                    accountTransaction.Source = Source;
                                    accountTransaction.SNo = fesno;
                                    accountTransaction.CreatedById = usr.Id;
                                    accountTransaction.FYId = FiscalYear.Id;
                                    accountTransaction.BranchId = base.CurrentBranch.Id;

                                    _accountTransaction.Add(accountTransaction);
                                }

                                fesno++;
                            }

                            var studentfee = UtilityService.GetStudentFeeTerm(item.StudentId, feeItem,
                                                                              CurrentAcademyYearId);
                            //_scStudentFeeTermRepository.GetMany(
                            //    x =>
                            //    x.StudentFeeRateDetail.FeeRateMaster.StudentId == item.StudentId &&
                            //    x.StudentFeeRateDetail.FeeItemId == feeItem);
                            foreach (var billingTerm in studentfee)
                            {


                                decimal drAmt = 0;
                                decimal drLocalAmt = 0;
                                decimal crAmt = 0;
                                decimal crLocalAmt = 0;
                                decimal termAmt = 0;
                                decimal crrate = 0;
                                decimal drrate = 0;
                                if (billingTerm.FeeTerm.Sign == 1)
                                {
                                    crAmt = Math.Abs(Convert.ToDecimal(billingTerm.LocalAmount));
                                    crLocalAmt = Math.Abs(Convert.ToDecimal(billingTerm.LocalAmount));

                                    termAmt = crAmt;
                                }
                                else
                                {
                                    drAmt = Convert.ToDecimal(billingTerm.LocalAmount);
                                    drLocalAmt = Convert.ToDecimal(billingTerm.LocalAmount);

                                    termAmt = System.Math.Abs(drAmt) * (-1);
                                }

                                //AccountTransaction Posting
                                var accountTransactionBillingTerm = new AccountTransaction();
                                accountTransactionBillingTerm.VNo = vNo;
                                accountTransactionBillingTerm.VDate = DateTime.UtcNow.Date;
                                accountTransactionBillingTerm.CrRate = billingTerm.LocalRate;

                                accountTransactionBillingTerm.GlCode = billingTerm.FeeTerm.GLCode;
                                accountTransactionBillingTerm.DrAmt = drAmt;
                                accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                                accountTransactionBillingTerm.CrAmt = crAmt;
                                accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                                accountTransactionBillingTerm.Source = Source;
                                accountTransactionBillingTerm.SNo = 0;
                                accountTransactionBillingTerm.CreatedById = usr.Id;
                                accountTransactionBillingTerm.CbCode = billingTerm.FeeTerm.GLCode;
                                accountTransactionBillingTerm.FYId = FiscalYear.Id;
                                accountTransactionBillingTerm.ReferenceId = billdetail.Id;
                                accountTransactionBillingTerm.BranchId = CurrentBranch.Id;
                                _accountTransaction.Add(accountTransactionBillingTerm);
                            }
                        }
                    }
                }
            }

            return Content("True");


        }

        //public class Stuentf
        //{
        //    public int StudentId { get; set; }
        //    public int ClassId { get; set; }
        //    public decimal FeeRate { get; set; }
        //    public int FeeItemId { get; set; }
        //    public string FeeItem { get; set; }
        //}
        //public ActionResult GetStudentFeeRate(int studentid)
        //{
        //    var context = new DataContext();
        //    var data =
        //        (from b in
        //             (from c in _scStudentFeeRateDetailRepository.GetMany(x => x.FeeRateMaster.StudentId == studentid)
        //            select new 
        //            {
        //                StudentId = c.FeeRateMaster.StudentId,
        //                ClassId = c.FeeRateMaster.Studentinfo.ClassId,
        //                FeeItemId = c.FeeItemId,
        //                FeeRate = c.FeeRate,
        //                FeeItem = c.FeeItem.Description

        //            }).Union(
        //                from a in
        //                    context.ScClassFeeRate.Where(
        //                        x =>
        //                            x.ClassId == 1 &&
        //                            !_scStudentFeeRateDetailRepository.GetMany(j => j.FeeRateMaster.StudentId == studentid)
        //                                .Select(j => j.FeeItemId)
        //                                .Contains(x.FeeItemId))



        //                select new 
        //                {
        //                    StudentId = studentid,
        //                    ClassId = a.ClassId,
        //                    FeeItemId = a.FeeItemId,
        //                    FeeRate = a.FeeRate,
        //                    FeeItem = a.ScFeeItem.Description

        //                }

        //            )
        //        select b).ToList();






        //}

        [HttpPost]
        public ActionResult MonthlyBillGenerationEdit(MonthlyBillGenerationViewModel model, IEnumerable<MonthlyBillGenerationDetailViewModel> subjectEntry, List<int> FeeItems)
        {
            var
               nepdate = NepaliDateService.NepaliDate(DateTime.Now).Date;
            var date = "";
            var endate = new DateTime();
         
            if (base.SystemControl.DateType == 1)
            {
                date = model.Month + "/1/" + model.Year;
                endate = Convert.ToDateTime(date);
                nepdate = NepaliDateService.NepaliDate(endate).Date;
            }
            else
            {
                nepdate = model.Year + "/" + model.Month + "/1";

                endate = NepaliDateService.NepalitoEnglishDate(nepdate);
            }
            var MonthMiti = NepaliDateService.NepaliDate(endate).Month;
            var YearMiti = NepaliDateService.NepaliDate(endate).Year;
            var Source = StringEnum.Parse(typeof(ModuleEnum), "Monthly Bill Generation").ToString();
            //  List<string>  amount = studentAmount.Where(x=>x.ToString() !="0").ToList();
            var usr = _authentication.GetAuthenticatedUser();

            var sno = 1;
            foreach (var item in subjectEntry.Where(x => x.Checkbox && x.Amount != 0))
            {



                var vNo = UtilityService.GetDocumentNumberingByModuleName(Source);
                        
                           var  billdetail = _monthlyBillStudentRepository.GetById(item.MonthlyStudentBillId);


                            billdetail.Amount = item.Amount + item.EducationTax;
                            billdetail.ClassId = model.MonthlyBill.ClassId;
                            billdetail.Month = endate.Month;
                            billdetail.Remarks = model.MonthlyBill.Remarks;
                            billdetail.Year = endate.Year;
                            billdetail.Date = endate;
                            billdetail.StudentId = item.StudentId;
                            billdetail.BoaderId = model.MonthlyBill.BoaderId;
                            billdetail.ShiftId = model.MonthlyBill.ShiftId;
                            billdetail.BillNo = vNo;
                            billdetail.AcademicYearId = CurrentAcademyYearId;
                            billdetail.MonthMiti = MonthMiti;
                            billdetail.YearMiti = YearMiti;
                            billdetail.CreatedById = usr.Id;
                          
                            _monthlyBillStudentRepository.Update(billdetail);
                            _accountTransaction.Delete(x => x.ReferenceId == billdetail.Id);
                            _scbilltransactionRepository.Delete(x => x.ReferenceId == billdetail.Id);
                            _scMonthlyBillRepository.Delete(x => x.MonthlyBillStudentId == billdetail.Id);
                        
                        
                        var documentnumber =
                            _documentNumeringSchemeRepository.GetById(
                                x => x.ModuleName.ToLower().Trim() == Source.ToLower().Trim());
                        if (documentnumber != null)
                        {
                            documentnumber.DocCurrNo += 1;
                            _documentNumeringSchemeRepository.Update(documentnumber);
                        }
                        var student = _scStudentinfo.GetById(x => x.StudentID == item.StudentId);
                        var accountTransactionDetail = new AccountTransaction();
                        accountTransactionDetail.VNo = billdetail.BillNo;
                        accountTransactionDetail.VDate = DateTime.UtcNow.Date;
                        if (student != null) accountTransactionDetail.GlCode = base.SystemControl.StudentFeeAc;
                        accountTransactionDetail.SlCode = 0;
                        accountTransactionDetail.ReferenceId = billdetail.Id;
                        accountTransactionDetail.DrAmt = Convert.ToDecimal(item.Amount + item.EducationTax);
                        accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.Amount + item.EducationTax);
                        accountTransactionDetail.Narration = model.MonthlyBill.Remarks;
                        accountTransactionDetail.Source = Source;
                        accountTransactionDetail.SNo = sno;
                        accountTransactionDetail.CreatedById = usr.Id;
                        accountTransactionDetail.FYId = FiscalYear.Id;
                        accountTransactionDetail.BranchId = base.CurrentBranch.Id;
                        _accountTransaction.Add(accountTransactionDetail);
                        var billtransaction = new ScBillTransaction()
                        {
                            BillAmount = item.Amount + item.EducationTax,
                            ReceiptAmount = 0,
                            ReferenceId = billdetail.Id,
                            StudentId = billdetail.StudentId,
                            BranchId = base.CurrentBranch.Id,
                            FYId = base.FiscalYear.Id,
                            Source = Source,
                            BillNo = vNo,
                            Date = billdetail.Date,
                            BMiti = billdetail.Miti,
                            AcademicYearId = CurrentAcademyYearId

                        };
                        _scbilltransactionRepository.Add(billtransaction);


                        sno++;
                        var fesno = 1;
                        foreach (var feeItem in FeeItems)
                        {
                            SP_MonthlyBillFeeWise mon_fee =
                                UtilityService.GetMonthlyBillGenerateFeeWise(model.MonthlyBill.ClassId,
                                                                             model.MonthlyBill.BoaderId,
                                                                             model.MonthlyBill.ShiftId,
                                                                             item.StudentId.ToString(),
                                                                             feeItem.ToString()).FirstOrDefault();

                            if (mon_fee != null)
                            {
                                decimal amount = 0;
                                amount = mon_fee.NetAmount != 0 ? mon_fee.NetAmount : mon_fee.Amount;
                                var EducationTax = UtilityService.CheckEducationTax(feeItem);
                                decimal taxamount = 0;
                                if (EducationTax)
                                {
                                    taxamount = amount * Convert.ToDecimal(0.01);
                                }
                                var data = new ScMonthlyBill()
                                {

                                    FeeItemId = feeItem,
                                    FeeAmount = amount,
                                    MonthlyBillStudentId = billdetail.Id,
                                    EducationTaxAmount = taxamount

                                };
                                _scMonthlyBillRepository.Add(data);

                                var fee = _scFeeItemRepository.GetById(x => x.Id == feeItem);

                                var accountTransactionMaster = new AccountTransaction();
                                accountTransactionMaster.VNo = billdetail.BillNo;
                                accountTransactionMaster.VDate = DateTime.UtcNow.Date;
                                if (fee != null) accountTransactionMaster.GlCode = fee.GLCode;
                                accountTransactionMaster.SlCode = 0;
                                accountTransactionMaster.ReferenceId = billdetail.Id;
                                accountTransactionMaster.CrAmt = Convert.ToDecimal(mon_fee.Amount);
                                accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(mon_fee.Amount);
                                accountTransactionMaster.Narration = model.MonthlyBill.Remarks;
                                accountTransactionMaster.Source = Source;
                                accountTransactionMaster.SNo = fesno;
                                accountTransactionMaster.CreatedById = usr.Id;
                                accountTransactionMaster.FYId = FiscalYear.Id;
                                accountTransactionMaster.BranchId = base.CurrentBranch.Id;

                                _accountTransaction.Add(accountTransactionMaster);
                                if (EducationTax && taxamount != 0)
                                {

                                    var accountTransaction = new AccountTransaction();
                                    accountTransaction.VNo = billdetail.BillNo;
                                    accountTransaction.VDate = DateTime.UtcNow.Date;
                                    if (fee != null)
                                    {
                                        accountTransaction.GlCode = SystemControl.EducationTaxAc != 0
                                                                        ? SystemControl.EducationTaxAc
                                                                        : fee.GLCode;
                                    }

                                    accountTransaction.SlCode = 0;
                                    accountTransaction.ReferenceId = billdetail.Id;
                                    accountTransaction.CrAmt = Convert.ToDecimal(taxamount);
                                    accountTransaction.LocalCrAmt = Convert.ToDecimal(taxamount);
                                    accountTransaction.Narration = "Education Tax have been Received From" +
                                                                   item.StudentName;
                                    accountTransaction.Source = Source;
                                    accountTransaction.SNo = fesno;
                                    accountTransaction.CreatedById = usr.Id;
                                    accountTransaction.FYId = FiscalYear.Id;
                                    accountTransaction.BranchId = base.CurrentBranch.Id;

                                    _accountTransaction.Add(accountTransaction);
                                }

                                fesno++;
                            }

                            var studentfee = UtilityService.GetStudentFeeTerm(item.StudentId, feeItem,
                                                                              CurrentAcademyYearId);
                            //_scStudentFeeTermRepository.GetMany(
                            //    x =>
                            //    x.StudentFeeRateDetail.FeeRateMaster.StudentId == item.StudentId &&
                            //    x.StudentFeeRateDetail.FeeItemId == feeItem);
                            foreach (var billingTerm in studentfee)
                            {


                                decimal drAmt = 0;
                                decimal drLocalAmt = 0;
                                decimal crAmt = 0;
                                decimal crLocalAmt = 0;
                                decimal termAmt = 0;
                                decimal crrate = 0;
                                decimal drrate = 0;
                                if (billingTerm.FeeTerm.Sign == 1)
                                {
                                    crAmt = Math.Abs(Convert.ToDecimal(billingTerm.LocalAmount));
                                    crLocalAmt = Math.Abs(Convert.ToDecimal(billingTerm.LocalAmount));

                                    termAmt = crAmt;
                                }
                                else
                                {
                                    drAmt = Convert.ToDecimal(billingTerm.LocalAmount);
                                    drLocalAmt = Convert.ToDecimal(billingTerm.LocalAmount);

                                    termAmt = System.Math.Abs(drAmt) * (-1);
                                }

                                //AccountTransaction Posting
                                var accountTransactionBillingTerm = new AccountTransaction();
                                accountTransactionBillingTerm.VNo = vNo;
                                accountTransactionBillingTerm.VDate = DateTime.UtcNow.Date;
                                accountTransactionBillingTerm.CrRate = billingTerm.LocalRate;

                                accountTransactionBillingTerm.GlCode = billingTerm.FeeTerm.GLCode;
                                accountTransactionBillingTerm.DrAmt = drAmt;
                                accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                                accountTransactionBillingTerm.CrAmt = crAmt;
                                accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                                accountTransactionBillingTerm.Source = Source;
                                accountTransactionBillingTerm.SNo = 0;
                                accountTransactionBillingTerm.CreatedById = usr.Id;
                                accountTransactionBillingTerm.CbCode = billingTerm.FeeTerm.GLCode;
                                accountTransactionBillingTerm.FYId = FiscalYear.Id;
                                accountTransactionBillingTerm.ReferenceId = billdetail.Id;
                                accountTransactionBillingTerm.BranchId = CurrentBranch.Id;
                                _accountTransaction.Add(accountTransactionBillingTerm);
                            }
                       
                }
            }

            return Content("True");


        }

        [HttpPost]
        public ActionResult GetStudentByClassId(MonthlyBillGenerationViewModel model)
        {
            //var
            //   nepdate = NepaliDateService.NepaliDate(DateTime.Now).Date;
            //var date = "";
            //var engdate = new DateTime();
            //if (base.SystemControl.DateType == 1)
            //{
            //    date = model.Month + "/1/" + model.Year;
            //    engdate = Convert.ToDateTime(date);
            //    nepdate = NepaliDateService.NepaliDate(engdate).Date;


            //}
            //else
            //{
            //    nepdate = model.Year + "/" + model.Month + "/1";

            //    engdate = NepaliDateService.NepalitoEnglishDate(nepdate);
            //}

            //var d =
            //    _monthlyBillStudentRepository.GetMany(
            //        x =>

            //        x.Year == engdate.Year && x.AcademicYearId == CurrentAcademyYearId && x.ClassId==model.MonthlyBill.ClassId).Select(x => x.StudentId);
            var data =
                  _scStudentRegistrationDetail.GetMany(
                      x =>
                      x.StudentRegistration.ClassId == model.MonthlyBill.ClassId
                          // && !d.Contains(x.StudentId) 
                      &&
                      x.StudentRegistration.AcademicYearId == CurrentAcademyYearId && x.ShiftId == model.MonthlyBill.ShiftId &&
                      x.BoaderId == model.MonthlyBill.BoaderId);



            var list = new List<MonthlyBillGenerationDetailViewModel>();
            var sno = 1;
            foreach (var item in data.Distinct())
            {
                var sec = "";
                if (item.Section != null)
                    sec = item.Section.Description;

                var a = new MonthlyBillGenerationDetailViewModel
                            {
                                Section = sec,
                                StudentId = item.StudentId,
                                RollNo = item.RollNo,
                                Code = item.Studentinfo.StdCode,
                                Status = Enum.GetName(typeof(StudentStatus), item.CurrentStatus),
                                sno = sno,
                                StudentName = item.Studentinfo.StuDesc,
                            };

                list.Add(a);
                sno++;
            }

            var partialView = this.RenderPartialViewToString("_PartialMonthlyBillGenerationDetailEntry", list.GroupBy(x => x.StudentId));


            return Json(new { view = partialView, value = list.Count() }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult BillGenerateByStudentIdWithFeeItemId(MonthlyBillGenerationViewModel model, List<int> MonthId, IEnumerable<MonthlyBillGenerationDetailViewModel> subjectEntry, List<int> FeeItems)
        {
            var item = "0";
            var studentId = "0";
            if (FeeItems == null)
            {
                FeeItems = new List<int>();

            }

            foreach (var feeItem in FeeItems)
            {
                if (item != "0")
                {
                    item += "," + feeItem.ToString();
                }
                else
                {
                    item = feeItem.ToString();
                }
            }
            foreach (var student in subjectEntry.Where(x => x.Checkbox))
            {
                if (studentId != "0")
                {
                    studentId += "," + student.StudentId;
                }
                else
                {
                    studentId = student.StudentId.ToString();
                }
               
            }
            object list = null;
           
           if(subjectEntry.Any())
           {
               if (subjectEntry.FirstOrDefault().MonthlyStudentBillId == 0)
               {

                   list = UtilityService.GetMonthlyBillGenerate(model.MonthlyBill.ClassId, model.MonthlyBill.BoaderId, model.MonthlyBill.ShiftId, studentId, item, CurrentAcademyYearId).Select(x => new
                   {
                       StudentId = x.StudentId,
                       Amount = GetMonthCount(x.StudentId, MonthId).Count == 0 ? 0 : x.Amount,
                       EducationTax = x.EducationTax,
                       MonthList = GetMonthCount(x.StudentId, MonthId)

                   }).ToList();
         
               }
               else
               {
                   list = UtilityService.GetMonthlyBillGenerate(model.MonthlyBill.ClassId, model.MonthlyBill.BoaderId, model.MonthlyBill.ShiftId, studentId, item, CurrentAcademyYearId).Select(x => new
                   {
                       StudentId = x.StudentId,
                       Amount = x.Amount,
                       EducationTax = x.EducationTax
                   });
               }
           }


            return Json(new { List = list }, JsonRequestBehavior.AllowGet);





        }
        public List<int> GetMonthCount(int studentId, List<int> MonthList)
        {
            var cuurentYear = DateTime.UtcNow.Year;
            var student = new List<int>();
            if (base.SystemControl.DateType == 2)
            {
                student =
               _monthlyBillStudentRepository.GetMany(
                   x => x.StudentId == studentId && x.AcademicYearId == this.AcademicYear.Id ).Select(x => x.MonthMiti).ToList();
            }
            else
            {


                student =
                    _monthlyBillStudentRepository.GetMany(
                        x =>
                        x.StudentId == studentId && x.AcademicYearId == this.AcademicYear.Id ).
                        Select(x => x.Month).ToList();
            }
            var list = new List<int>();
            foreach (var i in MonthList)
            {
                if (!student.Contains(i))
                {
                    list.Add(i);
                }
            }
            //var list = MonthList.Intersect(student).Count();
            return list;
        }

        public JsonResult GetBillByStudentId(int id)
        {

            List<ScMonthlyBillStudent> lstClasses = _monthlyBillStudentRepository.GetMany(x => x.StudentId == id && x.AcademicYearId == CurrentAcademyYearId).OrderBy(x => x.Id).ToList();

            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Code = x.BillNo,
                Description = x.Date
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        #region Fee Receipt
        [CheckPermission(Permissions = "Navigate", Module = "ScFR")]
        public ActionResult FeeReceipts()
        {
            ViewBag.UserRight = base.UserRight("ScFR");
            return View();
        }
        public JsonResult GetBalanceByStudentId(int studentId)
        {
            decimal BillAmount = (from b in _scbilltransactionRepository.GetMany(x => x.StudentId == studentId && x.AcademicYearId == CurrentAcademyYearId)

                                  select new
                                  {
                                      Number = b.BillAmount,

                                  }).Sum(x => x.Number);
            decimal RecAmount = (from b in _scbilltransactionRepository.GetMany(x => x.StudentId == studentId && x.AcademicYearId == CurrentAcademyYearId)

                                 select new
                                 {
                                     Number = b.ReceiptAmount,

                                 }).Sum(x => x.Number);
            decimal bal = BillAmount - RecAmount;
            var Balance = bal + " Dr";
            if (bal < 0)
            {
                Balance = Math.Abs(bal) + " Cr";
            }
            if (bal == 0)
            {
                Balance = "0";
            }
            // var data = _scbilltransactionRepository.GetById(x => x.ReferenceId == BillId && x.Source == "MBG" && x.AcademicYearId == CurrentAcademyYearId);
            return Json(new { Balance = Balance, }, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetBalanceByStudentId(int studentId, int BillId)
        //{
        //    decimal BillAmount = (from b in _scbilltransactionRepository.GetMany(x => x.StudentId == studentId && x.AcademicYearId == CurrentAcademyYearId)

        //                          select new
        //                                     {
        //                                         Number = b.BillAmount,

        //                                     }).Sum(x => x.Number);
        //    decimal RecAmount = (from b in _scbilltransactionRepository.GetMany(x => x.StudentId == studentId && x.AcademicYearId == CurrentAcademyYearId)

        //                         select new
        //                         {
        //                             Number = b.ReceiptAmount,

        //                         }).Sum(x => x.Number);
        //    decimal bal = BillAmount - RecAmount;
        //    var Balance = bal + " Dr";
        //    if (bal < 0)
        //    {
        //        Balance = Math.Abs(bal) + " Cr";
        //    }
        //    if (bal == 0)
        //    {
        //        Balance = "0";
        //    }
        //    var data = _scbilltransactionRepository.GetById(x => x.ReferenceId == BillId && x.Source == "MBG" && x.AcademicYearId == CurrentAcademyYearId);
        //    return Json(new { Balance = Balance, Value = data.BillAmount }, JsonRequestBehavior.AllowGet);
        //}
        [CheckPermission(Permissions = "Navigate", Module = "ScFR")]
        public ActionResult FeeReceiptList(int pageNo = 1)
        {

            ViewBag.DateType = base.SystemControl.DateType;
            var lstMaterialIssue_Masters = _scfeereceiptRepository.GetAll().OrderByDescending(x => x.Id);
            return PartialView("_PartialFeeReceiptList", lstMaterialIssue_Masters.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
        public ActionResult FeeReceiptPrint(int id)
        {
            var fee = _scfeereceiptRepository.GetById(x => x.Id == id);
            var user = _authentication.GetAuthenticatedUser();
            var student = UtilityService.GetStudentDetial(fee.StudentId, this.CurrentAcademyYearId);
            var section = "";
            var rollno = "";
            if (student != null)
            {
                if (student.Section != null) section = student.Section.Description;
                rollno = student.RollNo.ToString();
            }
            var list = new List<FeePrintViewModel>();
            var company = this.CurrentBranch;
            var logo = company.LogoGuid + "_th" + company.LogoExt;
            var filePath = Server.MapPath("~/Content/Logo/" + logo);
            if (!System.IO.File.Exists(filePath))
            {
                logo = "NoImage.jpg";
            }
            decimal Balance = 0;

            var billtrans =
                _scbilltransactionRepository.GetMany(
                    x => x.StudentId == fee.StudentId && x.AcademicYearId == this.AcademicYear.Id);

            decimal BillAmount = billtrans.Sum(x => x.BillAmount);
       decimal RecAmount = billtrans.Sum(x => x.ReceiptAmount);
            decimal bal = BillAmount - RecAmount;
            var prebal = bal + fee.ReceiptAmount;
            if (bal > 0)
            {
                Balance = Math.Abs(bal);
            }
           
            if (prebal < 0)
            {
                prebal = Math.Abs(bal);
            }

            for (int i = 1; i <= this.SystemControl.NoOfFeeReceiptPrint; i++)
            {
                var printfee = new FeePrintViewModel()
                                   {
                                       ReceiptNo = fee.ReceiptNo,
                                       ClassName = fee.Class.Description,
                                       Amount = fee.ReceiptAmount.ToString(),
                                       Date = fee.ReceiptDate.ToString("MM/dd/yyyy"),
                                       InWords =
                                           NumberToEnglish.changeCurrencyToWords(fee.ReceiptAmount.ToString()),
                                       Name = fee.Studentinfo.StuDesc,
                                       PaymentFor = "Monthly Fee",
                                       ReceivedBy = user.Username,
                                       RollNo = rollno,
                                       SectionName = section,
                                       BalanceDue = Balance.ToString(),
                                       PreBalance = (prebal).ToString(),
                                       ReportHeader = new ReportHeader()
                                       {

                                           CompanyAddress =
                                               company.Address + ", " + company.City + ", " + company.State + ", " +
                                               company.Country,
                                           CompanyName = company.Name,

                                           PanNo = company.PanNo,
                                           Phone = company.PhoneNo,
                                           Email = company.Email,
                                           Logo = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "Content/Logo/" + logo

                                       }


                                   };
                list.Add(printfee);
            }
            return Json(new { printfee = list, PrintData = this.SystemControl.PrintDataOnly }, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Create", Module = "ScFR")]
        public ActionResult FeeReceiptAdd()
        {
            var usr = _authentication.GetAuthenticatedUser();
            var Source = StringEnum.Parse(typeof(ModuleEnum), "Fee Receipt").ToString();
            var documentnumber = _documentNumeringSchemeRepository.GetById(x => x.ModuleName.ToLower().Trim() == Source.ToLower().Trim());

            if (documentnumber == null)
            {
                return PartialView("_PartialDocumentNumberingError");
            }
            documentnumber.DocCurrNo += 1;
            _documentNumeringSchemeRepository.Update(documentnumber);
            var vNo = UtilityService.GetDocumentNumberingByModuleName(Source);
            var data = new ScFeeReceipt();
            data.ReceiptNo = vNo;
            data.ReceiptDate = DateTime.UtcNow.Date;
            data.CreatedById = usr.Id;
            data.MonthList = base.SystemControl.DateType == 1 ? DropDownHelper.CreateMonthDate() : DropDownHelper.CreateMonthMiti();
            data.Month = Convert.ToInt32(data.MonthList.FirstOrDefault().Value);
            data.GLCode = base.SystemControl.CashBook;
            return PartialView("_PartialFeeReceiptAdd", data);
        }
        [HttpPost]
        public ActionResult FeeReceiptAdd(ScFeeReceipt model)
        {
            model.BillNo = "0";
            var
               nepdate = NepaliDateService.NepaliDate(DateTime.Now).Date;
            var date = "";
            var engdate = new DateTime();
            if (base.SystemControl.DateType == 1)
            {
                date = model.Month + "/1/" + DateTime.Now.Year;
                engdate = Convert.ToDateTime(date);
                nepdate = NepaliDateService.NepaliDate(engdate).Date;


            }
            else
            {
                var year = NepaliDateService.NepaliDate(DateTime.Now).Year;
                nepdate = year + "/" + model.Month + "/1";

                engdate = NepaliDateService.NepalitoEnglishDate(nepdate);
            }
        

            var datemiti = NepaliDateService.NepaliDate(engdate);
            model.PaidUpYear = engdate.Year;
            model.PaidUpMonth = engdate.Month;
            model.PaidUpMonthMiti = datemiti.Month;
            model.PaidUpYearMiti = datemiti.Year;
            var usr = _authentication.GetAuthenticatedUser();
            var Source = StringEnum.Parse(typeof(ModuleEnum), "Fee Receipt").ToString();
            model.ReceiptTimi = DateTime.UtcNow.ToUniversalTime();
            model.AcademicYearId = CurrentAcademyYearId;
            model.GLCode = base.SystemControl.CashBook;
            model.CreatedById = usr.Id;
        
           


            _scfeereceiptRepository.Add(model);

            var billtransaction = new ScBillTransaction()
            {
                // BillAmount = model.ReceiptAmount,
                ReceiptAmount = model.ReceiptAmount,
                ReferenceId = model.Id,
                StudentId = model.StudentId,
                BranchId = base.CurrentBranch.Id,
                FYId = base.FiscalYear.Id,
                Source = Source,
                BillNo = model.ReceiptNo,
                Date = model.ReceiptDate,
                AcademicYearId = CurrentAcademyYearId,
                BMiti = model.ReceiptMiti,

            };
            _scbilltransactionRepository.Add(billtransaction);
            var accountTransactionDetail = new AccountTransaction();
            accountTransactionDetail.VNo = model.ReceiptNo;
            accountTransactionDetail.VDate = model.ReceiptDate;
            accountTransactionDetail.GlCode = model.GLCode;
            accountTransactionDetail.SlCode = 0;
            accountTransactionDetail.ReferenceId = model.Id;
            accountTransactionDetail.DrAmt = Convert.ToDecimal(model.ReceiptAmount);
            accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(model.ReceiptAmount);
            accountTransactionDetail.Narration = model.Remarks;
            accountTransactionDetail.Source = Source;
            accountTransactionDetail.SNo = 1;
            accountTransactionDetail.CreatedById = usr.Id;
            accountTransactionDetail.FYId = FiscalYear.Id;
            accountTransactionDetail.BranchId = base.CurrentBranch.Id;
            _accountTransaction.Add(accountTransactionDetail);

            var accountTransactionMaster = new AccountTransaction();
            accountTransactionMaster.VNo = model.ReceiptNo;
            accountTransactionMaster.VDate = model.ReceiptDate;
            accountTransactionMaster.GlCode = base.SystemControl.StudentFeeAc;
            accountTransactionMaster.SlCode = 0;
            accountTransactionMaster.ReferenceId = model.Id;
            accountTransactionMaster.CrAmt = Convert.ToDecimal(model.ReceiptAmount);
            accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(model.ReceiptAmount);
            accountTransactionMaster.Narration = model.Remarks;
            accountTransactionMaster.Source = Source;
            accountTransactionMaster.SNo = 1;
            accountTransactionMaster.CreatedById = usr.Id;
            accountTransactionMaster.FYId = FiscalYear.Id;
            accountTransactionMaster.BranchId = base.CurrentBranch.Id;
            _accountTransaction.Add(accountTransactionMaster);

            //var fee = _scfeereceiptRepository.GetById(x => x.Id == model.Id);

            var user = _authentication.GetAuthenticatedUser();
            var student = UtilityService.GetStudentDetial(model.StudentId, this.CurrentAcademyYearId);
            var section = "";
            var rollno = "";
            if (student != null)
            {
                if (student.Section != null) section = student.Section.Description;
                rollno = student.RollNo.ToString();
            }
            var list = new List<FeePrintViewModel>();

            var company = this.CurrentBranch;
            var logo = company.LogoGuid + "_th" + company.LogoExt;
            var filePath = Server.MapPath("~/Content/Logo/" + logo);
            if (!System.IO.File.Exists(filePath))
            {
                logo = "NoImage.jpg";
            }
            decimal Balance = 0;

            var billtrans =
                _scbilltransactionRepository.GetMany(
                    x => x.StudentId == model.StudentId && x.AcademicYearId == this.AcademicYear.Id);

            decimal BillAmount = billtrans.Sum(x => x.BillAmount);

            decimal RecAmount = billtrans.Sum(x => x.ReceiptAmount);

            decimal bal = BillAmount - RecAmount;
            var prebal = bal - model.ReceiptAmount;
            if (bal > 0)
            {
                Balance = Math.Abs(bal);
            }
            
            if (prebal < 0)
            {
                prebal = Math.Abs(bal);
            }
            for (int i = 1; i <= this.SystemControl.NoOfFeeReceiptPrint; i++)
            {

                var printfee = new FeePrintViewModel()
                                   {
                                       ReceiptNo = model.ReceiptNo,
                                       ClassName =
                                           @UtilityService.GetClassNameByStudent(model.StudentId, CurrentAcademyYearId),
                                       Amount = model.ReceiptAmount.ToString(),
                                       Date = model.ReceiptDate.ToString("MM/dd/yyyy"),
                                       InWords =
                                           NumberToEnglish.changeCurrencyToWords(model.ReceiptAmount.ToString()),
                                       Name = student.Studentinfo.StuDesc,
                                       PaymentFor = "Monthly Fee",
                                       ReceivedBy = user.Username,
                                       RollNo = rollno,
                                       SectionName = section,
                                       PreBalance = prebal.ToString(),
                                       BalanceDue = Balance.ToString(),
                                       ReportHeader = new ReportHeader()
                                      {

                                          CompanyAddress =
                                              company.Address + ", " + company.City + ", " + company.State + ", " +
                                              company.Country,
                                          CompanyName = company.Name,

                                          PanNo = company.PanNo,
                                          Phone = company.PhoneNo,
                                          Email = company.Email,
                                          Logo = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "Content/Logo/" + logo

                                      }


                                   };
                list.Add(printfee);
            }









            return Json(new { True = "True", Print = list, PrintData = this.SystemControl.PrintDataOnly }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region MaterialIssue_Masters
        public ActionResult MaterialIssue_Masters()
        {
            return View();
        }

        public ActionResult MaterialIssue_MasterAdd()
        {
            var model = new ScMaterialIssueMaster();
            var sysCtrl = _systemControl.GetAll().FirstOrDefault();
            if (sysCtrl != null)
            {
                model.SystemControl = sysCtrl;
            }
            var Source = StringEnum.Parse(typeof(ModuleEnum), "Material Issue").ToString();
            var documentnumber = _documentNumeringSchemeRepository.GetById(x => x.ModuleName.ToLower().Trim() == Source.ToLower().Trim());
            if (documentnumber == null)
            {
                return PartialView("_PartialDocumentNumberingError");
            }
            documentnumber.DocCurrNo += 1;
            _documentNumeringSchemeRepository.Update(documentnumber);
            var vNo = UtilityService.GetDocumentNumberingByModuleName(Source);
            model.VoucherNo = vNo;
            model.VoucherDate = DateTime.UtcNow;
            return PartialView("_PartialMaterialIssue_MasterAdd", model);
        }

        [HttpPost]
        public ActionResult MaterialIssue_MasterAdd(ScMaterialIssueMaster model, IEnumerable<ScMaterialIssueDetails> MaterialIssueList)
        {
            model.CreatedOn = DateTime.UtcNow;
            model.ModifiedOn = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                model.CreatedBy = userId;
                model.ModifiedBy = userId;
                _materialissueMasterRepository.Add(model);

                int Sno = 1;
                foreach (ScMaterialIssueDetails item in MaterialIssueList)
                {

                    if (item.ProductId != 0 && item.StaffId != 0)
                    {
                        var details = new ScMaterialIssueDetails()

                                      {
                                          VoucherNo = model.VoucherNo,
                                          MaterialIssueMasterId = model.Id,
                                          ProductId = item.ProductId,
                                          StaffId = item.StaffId,
                                          Quantity = item.Quantity,
                                          Rate = item.Rate,
                                          LocalAmount = item.LocalAmount,


                                      };
                        _materialissueDetailsRepository.Add(details);

                        _functionService.SaveOutStockTransaction(details, Sno, base.FiscalYear.Id, base.CurrentBranch.Id);
                        Sno++;
                    }



                }

                return Content("True");
            }
            return Content("False");
        }

        public ActionResult MaterialIssue_MasterEdit(int materialissue_masterId)
        {
            ScMaterialIssueMaster objMaterialIssue_Master = _materialissueMasterRepository.GetById(x => x.Id == materialissue_masterId);
            objMaterialIssue_Master.MaterialIssueDetailses =
                _materialissueDetailsRepository.GetMany(x => x.MaterialIssueMasterId == materialissue_masterId);
            return PartialView("_PartialMaterialIssue_MasterEdit", objMaterialIssue_Master);
        }

        [HttpPost]
        public ActionResult MaterialIssue_MasterEdit(ScMaterialIssueMaster model, IEnumerable<ScMaterialIssueDetails> MaterialIssueList)
        {
            _materialissueMasterRepository.Update(model);
            var detailList = _materialissueDetailsRepository.GetMany(m => m.MaterialIssueMasterId == model.Id);
            foreach (var data in detailList)
            {
                _materialissueDetailsRepository.Delete(data);
            }

            var stocklist = _stockTransaction.GetMany(x => x.ReferenceId == model.Id);
            foreach (var stock in stocklist)
            {
                _stockTransaction.Delete(stock);
            }
            var Sno = 1;
            foreach (var item in MaterialIssueList)
            {
                if (item.ProductId != 0 && item.StaffId != 0)
                {
                    var details = new ScMaterialIssueDetails()
                    {

                        VoucherNo = model.VoucherNo,
                        MaterialIssueMasterId = model.Id,
                        ProductId = item.ProductId,
                        StaffId = item.StaffId,
                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        LocalAmount = item.LocalAmount
                    };
                    _materialissueDetailsRepository.Add(details);
                    _functionService.SaveOutStockTransaction(details, Sno, base.FiscalYear.Id, base.CurrentBranch.Id);
                    Sno++;
                }
            }
            return Content("True");
        }

        public ActionResult MaterialIssue_MastersList(int pageNo = 1)
        {
            var lstMaterialIssue_Masters = _materialissueMasterRepository.GetAll().OrderByDescending(x => x.Id);
            return PartialView("_PartialListMaterialIssue_Masters", lstMaterialIssue_Masters.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult MaterialIssue_MasterDelete(int materialissue_masterId)
        {
            _materialissueMasterRepository.Delete(x => x.Id == materialissue_masterId);

            return RedirectToAction("MaterialIssue_Masters");
        }

        public ActionResult AddMaterialIssueDetail()
        {
            var model = new ScMaterialIssueDetails();
            return PartialView(model);
        }

        public JsonResult GetProductRate(int productid)
        {
            decimal? productrate;
            if (productid != 0)
            {
                productrate = _productRepository.GetById(productid).BuyPrice;
            }
            else
            {
                productrate = 0;
            }

            return Json(productrate, JsonRequestBehavior.AllowGet);
        }


        #endregion MaterialIssue_Masters

        #region MaterialReturn_Masters
        public ActionResult MaterialReturn_Masters()
        {
            return View();
        }

        public ActionResult MaterialReturn_MasterAdd()
        {
            var model = new ScMaterialReturnMaster();
            var sysCtrl = _systemControl.GetAll().FirstOrDefault();
            if (sysCtrl != null)
            {
                model.SystemControl = sysCtrl;
            }
            var Source = StringEnum.Parse(typeof(ModuleEnum), "Material Return").ToString();
            var documentnumber = _documentNumeringSchemeRepository.GetById(x => x.ModuleName.ToLower().Trim() == Source.ToLower().Trim());
            if (documentnumber == null)
            {
                return PartialView("_PartialDocumentNumberingError");
            }
            documentnumber.DocCurrNo += 1;
            _documentNumeringSchemeRepository.Update(documentnumber);
            var vNo = UtilityService.GetDocumentNumberingByModuleName(Source);
            model.VoucherNo = vNo;
            model.VoucherDate = DateTime.UtcNow;
            return PartialView("_PartialMaterialReturn_MasterAdd", model);
        }

        [HttpPost]
        public ActionResult MaterialReturn_MasterAdd(ScMaterialReturnMaster model, IEnumerable<ScMaterialReturnDetails> MaterialReturnList)
        {
            model.CreatedOn = DateTime.UtcNow;
            model.ModifiedOn = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                var userId = _authentication.GetAuthenticatedUser().Id;
                model.CreatedBy = userId;
                model.ModifiedBy = userId;
                _scmaterialreturnmasterRepository.Add(model);

                int Sno = 1;
                foreach (ScMaterialReturnDetails item in MaterialReturnList)
                {
                    if (item.ProductId != 0 && item.StaffId != 0)
                    {
                        var details = new ScMaterialReturnDetails()
                        {

                            VoucherNo = model.VoucherNo,
                            MaterialReturnMasterId = model.Id,
                            ProductId = item.ProductId,
                            StaffId = item.StaffId,
                            Quantity = item.Quantity,
                            Rate = item.Rate,
                            LocalAmount = item.LocalAmount
                        };
                        _scmaterialreturndetailsRepository.Add(details);
                        _functionService.SaveInStockTransaction(details, Sno, base.FiscalYear.Id, base.CurrentBranch.Id);
                        Sno++;
                    }


                }

                return Content("True");
            }
            return Content("False");
        }

        public ActionResult MaterialReturn_MasterEdit(int materialreturn_masterId)
        {
            ScMaterialReturnMaster objMaterialIssue_Master = _scmaterialreturnmasterRepository.GetById(x => x.Id == materialreturn_masterId);
            objMaterialIssue_Master.MaterialReturnDetailses =
                _scmaterialreturndetailsRepository.GetMany(x => x.MaterialReturnMasterId == materialreturn_masterId);
            return PartialView("_PartialMaterialReturn_MasterEdit", objMaterialIssue_Master);
        }

        [HttpPost]
        public ActionResult MaterialReturn_MasterEdit(ScMaterialReturnMaster model, IEnumerable<ScMaterialReturnDetails> MaterialReturnList)
        {
            _scmaterialreturnmasterRepository.Update(model);
            var detailList = _scmaterialreturndetailsRepository.GetMany(m => m.MaterialReturnMasterId == model.Id);
            foreach (var data in detailList)
            {
                _scmaterialreturndetailsRepository.Delete(data);
            }

            var stocklist = _stockTransaction.GetMany(x => x.ReferenceId == model.Id);
            foreach (var stock in stocklist)
            {
                _stockTransaction.Delete(stock);
            }
            var Sno = 1;
            foreach (var item in MaterialReturnList)
            {
                if (item.ProductId != 0 && item.StaffId != 0)
                {
                    var details = new ScMaterialReturnDetails()
                    {

                        VoucherNo = model.VoucherNo,
                        MaterialReturnMasterId = model.Id,
                        ProductId = item.ProductId,
                        StaffId = item.StaffId,
                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        LocalAmount = item.LocalAmount
                    };
                    _scmaterialreturndetailsRepository.Add(details);
                    _functionService.SaveInStockTransaction(details, Sno, base.FiscalYear.Id, base.CurrentBranch.Id);
                    Sno++;
                }
            }
            return Content("True");
        }

        public ActionResult MaterialReturn_MastersList(int pageNo = 1)
        {
            var lstMaterialIssue_Masters = _scmaterialreturnmasterRepository.GetAll().OrderByDescending(x => x.Id);
            return PartialView("_PartialListMaterialReturn_Masters", lstMaterialIssue_Masters.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }

        public ActionResult MaterialReturn_MasterDelete(int materialissue_masterId)
        {

            _materialissueMasterRepository.Delete(x => x.Id == materialissue_masterId);

            return RedirectToAction("MaterialIssue_Masters");
        }

        public ActionResult AddMaterialReturnDetail()
        {
            var model = new ScMaterialReturnDetails();
            return PartialView(model);
        }


        #endregion MaterialReturn_Masters

    }
}
