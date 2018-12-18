using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.Controllers;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Enums;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Data;
using KRBAccounting.Service;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Data.CacheRepo;

namespace KRBAccounting.Web.Services
{
    public interface IFunctionService
    {
        void SaveOutStockTransaction(ScMaterialIssueDetails details, int sno, int fyId, int branchId);
        void SaveInStockTransaction(ScMaterialReturnDetails details, int sno, int fyId, int branchId);
    }
    public class FunctionService : IFunctionService
    {
        public static DataContext Context = new DataContext();
        private readonly IFormsAuthenticationService _authentication;
        private readonly IJournalVoucherDetailRepository _journalVoucherDetailRepository;
        private readonly IJournalVoucherRepository _journalVoucherRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly ILedgerRepository _ledgerRepository;
        private readonly IFiscalYearRepository _fiscalYearRepository;
        private readonly ISystemControlRepository _systemControlRepository;
        private readonly ICacheProvider _cacheProvider;
        private readonly IDocumentNumeringSchemeRepository _numeringSchemeRepository;
        private readonly IStockTransactionRepository _stockTransaction;
        private readonly IProductRepository _productRepository;
        private readonly IScEmployeeInfoRepository _staffMasterRepository;
        public IMaterialIssue_MasterRepository _materialissueMasterRepository { get; set; }
        public IScMaterialReturnDetailsRepository _MaterialReturnDetailsRepository { get; set; }
        public IScMaterialReturnMasterRepository _MaterialReturnMasterRepository { get; set; }


        public FunctionService(IJournalVoucherDetailRepository journalVoucherDetailRepository, IJournalVoucherRepository journalVoucherRepository,
             IAccountTransactionRepository accountTransactionRepository,
             ILedgerRepository ledgerRepository, IStockTransactionRepository stockTransactionRepository,
            IFiscalYearRepository fiscalYearRepository,
            ISystemControlRepository systemControlRepository,
            IScMaterialReturnDetailsRepository scMaterialReturnDetailsRepository,IScMaterialReturnMasterRepository scMaterialReturnMasterRepository,
            IDocumentNumeringSchemeRepository documentNumeringSchemeRepository, IProductRepository productRepository, IScEmployeeInfoRepository staffMasterRepository,IMaterialIssue_MasterRepository materialissueMasterRepository)
        {
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _journalVoucherDetailRepository = journalVoucherDetailRepository;
            _journalVoucherRepository = journalVoucherRepository;
            _accountTransactionRepository = accountTransactionRepository;
            _ledgerRepository = ledgerRepository;
            _fiscalYearRepository = fiscalYearRepository;
            _systemControlRepository = systemControlRepository;
            _cacheProvider = new DefaultCacheProvider();
            _numeringSchemeRepository = documentNumeringSchemeRepository;
            _stockTransaction = stockTransactionRepository;
            _productRepository = productRepository;
            _staffMasterRepository = staffMasterRepository;
            _materialissueMasterRepository = materialissueMasterRepository;
            _MaterialReturnDetailsRepository = scMaterialReturnDetailsRepository;
            _MaterialReturnMasterRepository = scMaterialReturnMasterRepository;

        }

        public void SaveOutStockTransaction(ScMaterialIssueDetails details, int sno, int fyId, int branchId)
        {
            var staff = _staffMasterRepository.GetById(details.StaffId);
            var materialissueMaster = _materialissueMasterRepository.GetById(details.MaterialIssueMasterId);
            var stockTransaction = new StockTransaction()
                                       {
                                           VNo = details.VoucherNo,
                                           VDate = materialissueMaster.VoucherDate,
                                           VMiti = materialissueMaster.VoucherMiti,
                                          // GlCode = staff.LedgerId,
                                           SNo = sno,
                                           ProductCode = details.ProductId,
                                           Quantity = details.Quantity,
                                           Rate = details.Rate,
                                           NetAmt = details.LocalAmount,
                                           TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString(),
                                           Source = "MI",
                                           ReferenceId = details.MaterialIssueMasterId,
                                           FYId = fyId,
                                           BranchId = branchId
                                       };
            _stockTransaction.Add(stockTransaction);
        }


        public void SaveInStockTransaction(ScMaterialReturnDetails details, int sno, int fyId, int branchId)
        {
            var staff = _staffMasterRepository.GetById(details.StaffId);
            var materialissueMaster = _MaterialReturnMasterRepository.GetById(details.MaterialReturnMasterId);
            var stockTransaction = new StockTransaction()
            {
                VNo = details.VoucherNo,
                VDate = materialissueMaster.VoucherDate,
                VMiti = materialissueMaster.VoucherMiti,
               // GlCode = staff.LedgerId,
                SNo = sno,
                ProductCode = details.ProductId,
                Quantity = details.Quantity,
                Rate = details.Rate,
                NetAmt = details.LocalAmount,
                TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString(),
                Source = "MR",
                ReferenceId = details.MaterialReturnMasterId,
                FYId = fyId,
                BranchId = branchId
            };
            _stockTransaction.Add(stockTransaction);
        }

    }
}