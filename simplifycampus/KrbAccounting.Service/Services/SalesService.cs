using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Pager;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Enums;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Service.Models.BillingTerm;
using KRBAccounting.Service.Models.Purchase;
using KRBAccounting.Service.Models.Sales;

namespace KRBAccounting.Service.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesOrderMasterRepository _salesOrderMaster;
        private readonly ISalesOrderDetailRepository _salesOrderDetail;
        private readonly ISalesInvoiceRepository _salesInvoice;
        private readonly IAccountTransactionRepository _accountTransaction;
        private readonly IUnitRepository _unitRepository;
        private readonly ISalesDetailRepository _salesDetail;
        private readonly IProductRepository _product;
        private readonly IStockTransactionRepository _stockTransaction;
        private readonly IBillingTermDetailRepository _billingTermDetail;
        private readonly IBillingTermRepository _billingTerm;
        private readonly ISalesInvoiceOtherDetailRepository _salesInvoiceOtherDetail;
        private readonly IUdfEntryRepository _udfEntry;
        private readonly IUDFDataRepository _udfData;
        private readonly ISalesOrderOtherDetailRepository _salesOrderOtherDetail;
        private readonly ISalesChallanMasterRepository _salesChallanMaster;
        private readonly ILedgerRepository _ledger;
        private readonly IAgentRepository _agent;
        private readonly ISalesChallanDetailRepository _salesChallanDetail;
        private readonly ISalesChallanImpTransDocRepository _salesChallanImpTransDoc;
        private readonly IPurchaseInvoiceReository _purchaseInvoice;
        private readonly IPurchaseDetailRepository _purchaseDetail;
        public readonly IPurchaseImpTransDocRepository _PurchaseImpTransDoc;
        private readonly IPurchaseChallanMasterRepository _purchaseChallanMaster;
        private readonly IPurchaseProductBatchRepository _purchaseProductBatch;
        private readonly IFiscalYearRepository _fiscalYear;
        private readonly ICurrencyRepository _currency;
        public SalesService(ISalesOrderMasterRepository salesOrderMasterRepository,
            ISalesInvoiceRepository salesInvoiceRepository,
            IAccountTransactionRepository accountTransactionRepository,
            IUnitRepository unitRepository,
            ISalesDetailRepository salesDetailRepository,
            IProductRepository productRepository,
            IStockTransactionRepository stockTransactionRepository,
            IBillingTermDetailRepository billingTermDetailRepository,
            IBillingTermRepository billingTermRepository,
            ISalesInvoiceOtherDetailRepository salesInvoiceOtherDetailRepository,
            IUdfEntryRepository udfEntryRepository,
            IUDFDataRepository udfDataRepository,
            ISalesOrderDetailRepository salesOrderDetailRepository,
            ISalesOrderOtherDetailRepository salesOrderOtherDetailRepository,
            ISalesChallanMasterRepository salesChallanMasterRepository,
            ILedgerRepository ledgerRepository,
            IAgentRepository agentRepository,
            ISalesChallanDetailRepository salesChallanDetailRepository,
            ISalesChallanImpTransDocRepository salesChallanImpTransDocRepository,
            IPurchaseInvoiceReository purchaseInvoiceReository,
            IPurchaseDetailRepository purchaseDetailRepository,
            IPurchaseImpTransDocRepository purchaseImpTransDocRepository,
            IPurchaseChallanMasterRepository purchaseChallanMasterRepository,
            IPurchaseProductBatchRepository purchaseProductBatchRepository,
            IFiscalYearRepository fiscalYearRepository,
            ICurrencyRepository currencyRepository
            )
        {
            _salesOrderMaster = salesOrderMasterRepository;
            _salesInvoice = salesInvoiceRepository;
            _accountTransaction = accountTransactionRepository;
            _unitRepository = unitRepository;
            _salesDetail = salesDetailRepository;
            _product = productRepository;
            _stockTransaction = stockTransactionRepository;
            _billingTermDetail = billingTermDetailRepository;
            _billingTerm = billingTermRepository;
            _salesInvoiceOtherDetail = salesInvoiceOtherDetailRepository;
            _udfEntry = udfEntryRepository;
            _udfData = udfDataRepository;
            _salesOrderDetail = salesOrderDetailRepository;
            _salesOrderOtherDetail = salesOrderOtherDetailRepository;
            _salesChallanMaster = salesChallanMasterRepository;
            _ledger = ledgerRepository;
            _agent = agentRepository;
            _salesChallanDetail = salesChallanDetailRepository;
            _salesChallanImpTransDoc = salesChallanImpTransDocRepository;
            _purchaseInvoice = purchaseInvoiceReository;
            _purchaseDetail = purchaseDetailRepository;
            _PurchaseImpTransDoc = purchaseImpTransDocRepository;
            _purchaseChallanMaster = purchaseChallanMasterRepository;
            _purchaseProductBatch = purchaseProductBatchRepository;
            _fiscalYear = fiscalYearRepository;
            _currency = currencyRepository;
        }

        #region Invoice

        public int SaveSalesInvoice(SalesInvoiceAddViewModel model, int userId, int currentBranchId, int fiscalYearId)
        {

            var source = StringEnum.Parse(typeof(ModuleEnum), "Sales").ToString();
            model.SalesInvoice.CreatedById = userId;
            model.SalesInvoice.CreatedDate = DateTime.UtcNow;
            model.SalesInvoice.Posting = true;
            model.SalesInvoice.Export = false;
            //model.SalesInvoice.InvoiceDate = Convert.ToDateTime(model.InvoiceDate);
            model.SalesInvoice.FYId = fiscalYearId;
            model.SalesInvoice.BranchId = currentBranchId;
            model.SalesInvoice.BasicAmt = Convert.ToDecimal(model.SalesInvoice.BasicAmt);
            model.SalesInvoice.NetAmt = Convert.ToDecimal(model.SalesInvoice.NetAmt);
            model.SalesInvoice.TermAmt = Convert.ToDecimal(model.SalesInvoice.TermAmt);
            model.SalesInvoice.OrderId = model.SalesOrderId;
            if (model.SalesInvoice.CurrencyId != 0 && model.SalesInvoice.CurrencyId != null)
            {
                var currencyId = Convert.ToInt32(model.SalesInvoice.CurrencyId);
                //var Code = _currency.GetById(model.SalesInvoice.CurrencyId);
                var Code = _currency.GetById(currencyId);
                model.SalesInvoice.CurrCode = Convert.ToString(Code.Id);

            }
            // model.OrderNo = model.OrderNo;
            _salesInvoice.Add(model.SalesInvoice);

            var accountTransactionMaster = new AccountTransaction();
            accountTransactionMaster.VNo = model.SalesInvoice.InvoiceNo;
            accountTransactionMaster.VDate = model.SalesInvoice.InvoiceDate;
            accountTransactionMaster.VMiti = model.SalesInvoice.InvoiceMiti;
            accountTransactionMaster.AgentCode = Convert.ToString(model.SalesInvoice.AgentCode);
            accountTransactionMaster.CrRate = Convert.ToDecimal(model.SalesInvoice.CurrRate);
            accountTransactionMaster.CrCode = model.SalesInvoice.CurrCode;
            accountTransactionMaster.GlCode = model.SalesInvoice.GlCode;
            accountTransactionMaster.CrAmt = 0;
            accountTransactionMaster.LocalCrAmt = 0;
            accountTransactionMaster.DrAmt = Convert.ToDecimal(model.SalesInvoice.NetAmt);

            if (model.SalesInvoice.CurrencyId != 0 && model.SalesInvoice.CurrencyId != null)
            {
                accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(model.SalesInvoice.NetAmt) * Convert.ToDecimal(model.SalesInvoice.CurrRate);
            }
            else
            {
                accountTransactionMaster.LocalDrAmt = Convert.ToDecimal(model.SalesInvoice.NetAmt);
            }
            accountTransactionMaster.Source = source;
            accountTransactionMaster.SNo = 0;
            accountTransactionMaster.CreatedById = userId;
            var isCashBank = _ledger.GetById(model.SalesInvoice.GlCode).IsCashBank;
            if (isCashBank)
                accountTransactionMaster.CbCode = model.SalesInvoice.GlCode;
            accountTransactionMaster.ReferenceId = model.SalesInvoice.Id;
            accountTransactionMaster.FYId = fiscalYearId;
            accountTransactionMaster.BranchId = currentBranchId;
            accountTransactionMaster.Remarks = model.SalesInvoice.Remarks;
            accountTransactionMaster.SlCode = model.SalesInvoice.SlCode;
            _accountTransaction.Add(accountTransactionMaster);
            return model.SalesInvoice.Id;
        }

        public List<SalesDetail> SaveSalesDetail(IEnumerable<SalesDetailEntryViewModel> salesDetailEntry, SalesInvoice invoice, int baseSalesAc, int userId, int fiscalYearId, int currentBranchId, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            var source = StringEnum.Parse(typeof(ModuleEnum), "Sales").ToString();
            int sno = 1;
            var tempSalesDetailList = new List<SalesDetail>();
            foreach (var item in salesDetailEntry)
            {
                if (item.ProductId != null && item.ProductId != 0 && item.BasicAmt != null && item.BasicAmt != 0 && item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    var unitcode = _unitRepository.GetById(item.UnitId).Code;
                    var salesDetail = new SalesDetail();
                    salesDetail.SNo = sno;
                    salesDetail.ProductCode = item.ProductId;
                    salesDetail.AltQty = item.AltQty;
                    salesDetail.Qty = Convert.ToDecimal(item.Qty);
                    salesDetail.AltStockQty = item.AltQty;
                    salesDetail.StockQty = Convert.ToDecimal(item.Qty);
                    salesDetail.Rate = Convert.ToDecimal(item.Rate);
                    salesDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    salesDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    salesDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    salesDetail.Remarks = invoice.Remarks;
                    salesDetail.SalesInvoiceId = invoice.Id;
                    salesDetail.Unit = unitcode;
                    salesDetail.AltUnit = item.AltUnit;
                    salesDetail.Index = item.Index;
                    salesDetail.Godown = item.GodownId;
                    salesDetail.BatchId = item.BatchId;
                    _salesDetail.Add(salesDetail);
                    tempSalesDetailList.Add(salesDetail);
                    var accountTransactionDetail = new AccountTransaction();
                    accountTransactionDetail.VNo = invoice.InvoiceNo;
                    accountTransactionDetail.VDate = invoice.InvoiceDate;
                    accountTransactionDetail.VMiti = invoice.InvoiceMiti;
                    accountTransactionDetail.CrRate = Convert.ToDecimal(invoice.CurrRate);
                    accountTransactionDetail.CrCode = invoice.CurrCode;

                    //Sales A/C Id need to specify in setting
                    int salesAc;
                    var product = _product.GetById(item.ProductId);
                    salesAc = product.SalesId != null && product.SalesId != 0 ? Convert.ToInt32(product.SalesId) : baseSalesAc;
                    //Purchase A/C Id need to specify in setting
                    accountTransactionDetail.AgentCode = Convert.ToString(invoice.AgentCode);
                    accountTransactionDetail.GlCode = salesAc;
                    accountTransactionDetail.SlCode = invoice.SlCode;
                    accountTransactionDetail.CrAmt = Convert.ToDecimal(item.BasicAmt);

                    if (invoice.CurrencyId != null && invoice.CurrencyId != 0)
                    {
                        accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.BasicAmt) * Convert.ToDecimal(invoice.CurrRate);
                    }
                    else
                    {
                        accountTransactionDetail.LocalCrAmt = Convert.ToDecimal(item.BasicAmt);
                    }
                    accountTransactionDetail.DrAmt = 0;
                    accountTransactionDetail.LocalDrAmt = 0;
                    accountTransactionDetail.Source = source;
                    accountTransactionDetail.SNo = 0;
                    accountTransactionDetail.CreatedById = userId;
                    var isCashBankDetail = _ledger.GetById(salesAc).IsCashBank;
                    if (isCashBankDetail)
                        accountTransactionDetail.CbCode = invoice.GlCode;
                    accountTransactionDetail.ReferenceId = invoice.Id;
                    accountTransactionDetail.FYId = fiscalYearId;
                    accountTransactionDetail.BranchId = currentBranchId;
                    accountTransactionDetail.Remarks = invoice.Remarks;
                    _accountTransaction.Add(accountTransactionDetail);

                    var stockTransaction = new StockTransaction();
                    stockTransaction.AgentCode = UtilityService.GetAgentNameById(invoice.AgentCode);
                    stockTransaction.AltQty = salesDetail.AltQty;
                    stockTransaction.AltStockQty = salesDetail.AltStockQty;
                    stockTransaction.AltUnit = salesDetail.AltUnit;
                    stockTransaction.Unit = unitcode;
                    stockTransaction.CurrCode = invoice.CurrCode;
                    stockTransaction.CurrRate = invoice.CurrRate;
                    stockTransaction.GlCode = invoice.GlCode;
                    stockTransaction.NetAmt = salesDetail.NetAmt;
                    stockTransaction.ProductCode = salesDetail.ProductCode;
                    stockTransaction.Quantity = salesDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(salesDetail.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = source;
                    stockTransaction.ReferenceId = invoice.Id;
                    stockTransaction.FYId = fiscalYearId;
                    stockTransaction.BatchSerialNo = item.BatchSerialNo;
                    stockTransaction.Godown = Convert.ToString(salesDetail.Godown);
                    var stock = _stockTransaction.Filter(x => x.ProductCode == item.ProductId).ToList().LastOrDefault();
                    if (stock == null)
                    {
                        stockTransaction.StockVal = Convert.ToDecimal(salesDetail.BasicAmt);
                        stockTransaction.StockQty = salesDetail.StockQty;

                    }
                    else
                    {
                        stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(salesDetail.BasicAmt);
                        stockTransaction.StockQty = stock.StockQty - salesDetail.StockQty;
                    }
                    stockTransaction.TermAmt = salesDetail.TermAmt;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    stockTransaction.VDate = invoice.InvoiceDate;
                    stockTransaction.VMiti = invoice.InvoiceMiti;
                    stockTransaction.VNo = invoice.InvoiceNo;
                    stockTransaction.BranchId = currentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;

                    if (billTermList != null)
                    {
                        decimal netAmt = 0;
                        //var billWiseTerms =
                        //    billTermList.Where(x => !x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList();
                        foreach (var term in billTermList.Where(x => x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList())
                        {
                            var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                            decimal drAmt = 0;
                            decimal drLocalAmt = 0;
                            decimal crAmt = 0;
                            decimal crLocalAmt = 0;
                            decimal termAmt = 0;
                            if (term.Sign == "+")
                            {
                                crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                termAmt = crAmt;
                            }
                            else
                            {
                                drAmt = Convert.ToDecimal(term.Amount);
                                drLocalAmt = Convert.ToDecimal(term.Amount);
                                termAmt = System.Math.Abs(drAmt) * (-1);
                            }
                            var billingTermDetail = new BillingTermDetail();
                            billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                            var index = Convert.ToInt32(term.BillingTermIndex);
                            billingTermDetail.Index = index;
                            billingTermDetail.ReferenceId = invoice.Id;
                            billingTermDetail.Source = source;
                            billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                            billingTermDetail.TermAmount = termAmt;
                            billingTermDetail.DetailId = salesDetail.Id;
                            billingTermDetail.Type = "P";
                            billingTermDetail.IsProductWise = term.IsProductWise;
                            _billingTermDetail.Add(billingTermDetail);

                            //AccountTransaction Posting
                            var accountTransactionBillingTerm = new AccountTransaction();
                            accountTransactionBillingTerm.VNo = invoice.InvoiceNo;
                            accountTransactionBillingTerm.VDate = invoice.InvoiceDate;
                            accountTransactionBillingTerm.VMiti = invoice.InvoiceMiti;
                            accountTransactionBillingTerm.CrRate = Convert.ToDecimal(invoice.CurrRate);
                            accountTransactionBillingTerm.CrCode = invoice.CurrCode;
                            accountTransactionBillingTerm.AgentCode = Convert.ToString(invoice.AgentCode);
                            accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                            if (invoice.CurrencyId != null && invoice.CurrencyId != 0)
                            {
                                drLocalAmt = drLocalAmt * Convert.ToDecimal(invoice.CurrRate);
                                crLocalAmt = crLocalAmt * Convert.ToDecimal(invoice.CurrRate);

                            }
                            accountTransactionBillingTerm.CrAmt = crAmt;
                            accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                            accountTransactionBillingTerm.DrAmt = drAmt;
                            accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                            accountTransactionBillingTerm.Source = source;
                            accountTransactionBillingTerm.SNo = 0;
                            accountTransactionBillingTerm.CreatedById = userId;
                            var isCashBankbill = _ledger.GetById(invoice.GlCode).IsCashBank;
                            if (isCashBankbill)
                                accountTransactionBillingTerm.CbCode = invoice.GlCode;
                            accountTransactionBillingTerm.FYId = fiscalYearId;
                            accountTransactionBillingTerm.ReferenceId = invoice.Id;
                            accountTransactionBillingTerm.BranchId = currentBranchId;
                            accountTransactionBillingTerm.Remarks = invoice.Remarks;
                            _accountTransaction.Add(accountTransactionBillingTerm);
                        }
                    }
                }
            }
            if (billTermList != null)
            {
                foreach (var term in billTermList.Where(x => !x.IsProductWise).ToList())
                {
                    var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                    decimal drAmt = 0;
                    decimal drLocalAmt = 0;
                    decimal crAmt = 0;
                    decimal crLocalAmt = 0;
                    decimal termAmt = 0;
                    if (term.Sign == "+")
                    {
                        crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        termAmt = crAmt;
                    }
                    else
                    {
                        drAmt = Convert.ToDecimal(term.Amount);
                        drLocalAmt = Convert.ToDecimal(term.Amount);
                        termAmt = System.Math.Abs(drAmt) * (-1);
                    }
                    var billingTermDetail = new BillingTermDetail();
                    billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    billingTermDetail.Index = index;
                    billingTermDetail.ReferenceId = invoice.Id;
                    billingTermDetail.Source = source;
                    billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                    billingTermDetail.TermAmount = termAmt;
                    billingTermDetail.Type = "B";
                    billingTermDetail.IsProductWise = term.IsProductWise;
                    _billingTermDetail.Add(billingTermDetail);

                    //AccountTransaction Posting
                    var accountTransactionBillingTerm = new AccountTransaction();
                    accountTransactionBillingTerm.VNo = invoice.InvoiceNo;
                    accountTransactionBillingTerm.VDate = invoice.InvoiceDate;
                    accountTransactionBillingTerm.VMiti = invoice.InvoiceMiti;
                    accountTransactionBillingTerm.CrRate = 1;
                    accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                    
                    if (invoice.CurrencyId != 0 && invoice.CurrencyId != null)
                    {
                        drLocalAmt = drLocalAmt * Convert.ToDecimal(invoice.CurrRate);
                        crLocalAmt = crLocalAmt * Convert.ToDecimal(invoice.CurrRate);
                    }
                    accountTransactionBillingTerm.DrAmt = drAmt;
                    accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                    accountTransactionBillingTerm.CrAmt = crAmt;
                    accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                    accountTransactionBillingTerm.Source = source;
                    accountTransactionBillingTerm.SNo = 0;
                    accountTransactionBillingTerm.CreatedById = userId;
                    var isCashBankbill = _ledger.GetById(invoice.GlCode).IsCashBank;
                    if (isCashBankbill)
                        accountTransactionBillingTerm.CbCode = invoice.GlCode;
                    accountTransactionBillingTerm.FYId = fiscalYearId;
                    accountTransactionBillingTerm.ReferenceId = invoice.Id;
                    accountTransactionBillingTerm.BranchId = currentBranchId;
                    accountTransactionBillingTerm.Remarks = invoice.Remarks;
                    _accountTransaction.Add(accountTransactionBillingTerm);
                }
            }
            return tempSalesDetailList;
        }

        public void SaveSalesBillingTerm(IEnumerable<BillingTermDetailViewModel> billTermList, List<SalesDetail> tempSalesDetailList, SalesInvoice invoice, int userId, int fiscalYearId, int currentBranchId)
        {
            var source = StringEnum.Parse(typeof(ModuleEnum), "Sales").ToString();
            foreach (var term in billTermList)
            {
                var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                decimal drAmt = 0;
                decimal drLocalAmt = 0;
                decimal crAmt = 0;
                decimal crLocalAmt = 0;
                decimal termAmt = 0;
                if (term.Sign == "+")
                {
                    crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                    crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                    termAmt = crAmt;
                }
                else
                {
                    drAmt = Convert.ToDecimal(term.Amount);
                    drLocalAmt = Convert.ToDecimal(term.Amount);
                    termAmt = System.Math.Abs(drAmt) * (-1);
                }
                var billingTermDetail = new BillingTermDetail();
                billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                billingTermDetail.Index = term.BillingTermIndex;
                billingTermDetail.ReferenceId = invoice.Id;
                billingTermDetail.Source = source;
                billingTermDetail.Rate = Convert.ToDecimal(term.Rate);
                var index = Convert.ToInt32(term.BillingTermIndex);
                if (term.IsProductWise)
                    billingTermDetail.DetailId = tempSalesDetailList[index].Id;
                billingTermDetail.IsProductWise = term.IsProductWise;
                billingTermDetail.TermAmount = termAmt;
                _billingTermDetail.Add(billingTermDetail);

                //AccountTransaction Posting
                var accountTransactionBillingTerm = new AccountTransaction();
                accountTransactionBillingTerm.VNo = invoice.InvoiceNo;
                accountTransactionBillingTerm.VDate = invoice.InvoiceDate;
                accountTransactionBillingTerm.VMiti = invoice.InvoiceMiti;
                accountTransactionBillingTerm.CrRate = Convert.ToDecimal(invoice.CurrRate);
                accountTransactionBillingTerm.CrCode = invoice.CurrCode;
                accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                accountTransactionBillingTerm.SlCode = invoice.SlCode;
                accountTransactionBillingTerm.DrAmt = drAmt;

                if (invoice.CurrencyId != 0)
                {
                    accountTransactionBillingTerm.LocalDrAmt = drLocalAmt * Convert.ToDecimal(invoice.CurrRate);
                }
                else
                {
                    accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                }
                accountTransactionBillingTerm.CrAmt = crAmt;

                if (invoice.CurrencyId != 0)
                {
                    accountTransactionBillingTerm.LocalCrAmt = crLocalAmt * Convert.ToDecimal(invoice.CurrRate);
                }
                else
                {
                    accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                }
                accountTransactionBillingTerm.Source = source;
                accountTransactionBillingTerm.SNo = 0;
                accountTransactionBillingTerm.CreatedById = userId;
                var isCashBankbill = _ledger.GetById(invoice.GlCode).IsCashBank;
                if (isCashBankbill)
                    accountTransactionBillingTerm.CbCode = invoice.GlCode;
                accountTransactionBillingTerm.FYId = fiscalYearId;
                accountTransactionBillingTerm.ReferenceId = invoice.Id;
                accountTransactionBillingTerm.BranchId = currentBranchId;
                accountTransactionBillingTerm.Remarks = invoice.Remarks;
                accountTransactionBillingTerm.AgentCode = Convert.ToString(invoice.AgentCode);
                _accountTransaction.Add(accountTransactionBillingTerm);
            }
        }

        public void SaveSalesInvoiceOtherDetail(SalesInvoiceOtherDetail model, int invoiceId)
        {
            model.SalesInvoiceId = invoiceId;
            _salesInvoiceOtherDetail.Add(model);
        }

        public void SaveSalesInvoiceUDF(FormCollection formCollection, int invoiceId)
        {
            var data = _udfEntry.GetMany(x => x.EntryModuleId == (int)EntryModuleEnum.SalesInvoice);
            if (data != null)
            {
                foreach (var udfEntry in data)
                {
                    var value = formCollection[udfEntry.FieldName];
                    var i = new UDFData()
                    {
                        ReferenceId = invoiceId,
                        UdfId = udfEntry.Id,
                        Value = value,
                        SourceId = (int)EntryModuleEnum.SalesInvoice
                    };

                    _udfData.Add(i);
                }
            }
        }

        #endregion

        #region Order

        public SalesOrderMaster GetSalesOrder(int id, string includeProperties = "")
        {
            return _salesOrderMaster.GetById(x => x.Id == id, includeProperties);
        }

        public PagedList<SalesOrderMaster> GetPendingSalesOrder(int pageNo, int pageSize, int currentBranchId)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            return _salesOrderMaster.Filter(x => x.BranchId == currentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, pageSize);
        }

        public PagedList<SalesOrderMaster> GetAcceptedSalesOrder(int pageNo, int pageSize, int currentBranchId)
        {
            return _salesOrderMaster.Filter(x => x.BranchId == currentBranchId && !x.IsDeleted).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, pageSize);
        }

        public SalesOrderMaster SaveSalesOrder(SalesOrderMaster model, DateTime orderDate, int fiscalYearId, int currentBranchId, string source, int userId)
        {
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            model.OrderDate = orderDate;
            model.FYId = fiscalYearId;
            model.BranchId = currentBranchId;
            model.BasicAmt = Convert.ToDecimal(model.BasicAmt);
            model.NetAmt = Convert.ToDecimal(model.NetAmt);
            model.TermAmt = Convert.ToDecimal(model.TermAmt);
            _salesOrderMaster.Add(model);
            return model;
        }

        public List<SalesOrderDetail> SaveSalesOrderDetail(IEnumerable<SalesDetailEntryViewModel> salesDetailEntry,
            SalesOrderMaster order, string source, int userId,
            int fiscalYearId, int currentBranchId)
        {
            int sno = 1;
            var tempOrderSalesDetailList = new List<SalesOrderDetail>();
            foreach (var item in salesDetailEntry)
            {
                if (item.ProductId != null && item.ProductId != 0 && item.BasicAmt != null && item.BasicAmt != 0 && item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    var unitcode = _unitRepository.GetById(item.UnitId).Code;
                    var salesDetail = new SalesOrderDetail();
                    salesDetail.SNo = sno;
                    salesDetail.ProductCode = item.ProductId;
                    salesDetail.AltQty = item.AltQty;
                    salesDetail.Qty = Convert.ToDecimal(item.Qty);
                    salesDetail.AltStockQty = item.AltQty;
                    salesDetail.StockQty = Convert.ToDecimal(item.Qty);
                    salesDetail.Rate = Convert.ToDecimal(item.Rate);
                    salesDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    salesDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    salesDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    salesDetail.Remarks = order.Remarks;
                    salesDetail.OrderId = order.Id;
                    salesDetail.Unit = unitcode;
                    salesDetail.AltUnit = item.AltUnit;
                    _salesOrderDetail.Add(salesDetail);
                    tempOrderSalesDetailList.Add(salesDetail);

                    //var stockTransaction = new StockTransaction();
                    //stockTransaction.AgentCode = UtilityService.GetAgentNameById(order.AgentCode);
                    //stockTransaction.AltQty = salesDetail.AltQty;
                    //stockTransaction.VMiti = salesDetail.SalesOrderMaster.OrderMiti;
                    //stockTransaction.AltStockQty = salesDetail.AltStockQty;
                    //stockTransaction.AltUnit = salesDetail.AltUnit;
                    //stockTransaction.Unit = unitcode;
                    //stockTransaction.CurrCode = "";
                    //stockTransaction.CurrRate = 0;
                    //stockTransaction.GlCode = order.GlCode;
                    //stockTransaction.NetAmt = salesDetail.NetAmt;
                    //stockTransaction.ProductCode = salesDetail.ProductCode;
                    //stockTransaction.Quantity = salesDetail.Qty;
                    //stockTransaction.Rate = Convert.ToDecimal(salesDetail.Rate);
                    //stockTransaction.SNo = sno;
                    //stockTransaction.Source = source;
                    //stockTransaction.ReferenceId = order.Id;
                    //stockTransaction.FYId = fiscalYearId;
                    //var stock = _stockTransaction.Filter(x => x.ProductCode == item.ProductId).ToList().LastOrDefault();
                    //if (stock == null)
                    //{
                    //    stockTransaction.StockVal = Convert.ToDecimal(salesDetail.BasicAmt);
                    //    stockTransaction.StockQty = salesDetail.StockQty;
                    //}
                    //else
                    //{
                    //    stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(salesDetail.BasicAmt);
                    //    stockTransaction.StockQty = stock.StockQty - salesDetail.StockQty;
                    //}
                    //stockTransaction.TermAmt = salesDetail.TermAmt;
                    //stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    //stockTransaction.VDate = order.OrderDate;
                    //stockTransaction.VNo = order.OrderNo;
                    //stockTransaction.BranchId = currentBranchId;
                    //_stockTransaction.Add(stockTransaction);
                    sno++;
                }
            }
            return tempOrderSalesDetailList;
        }

        public List<SalesOrderDetail> SaveSalesOrderDetail(IEnumerable<SalesOrderDetail> salesDetailEntry,
            SalesOrderMaster order, string source, int userId,
            int fiscalYearId, int currentBranchId)
        {
            int sno = 1;
            var tempOrderSalesDetailList = new List<SalesOrderDetail>();
            foreach (var item in salesDetailEntry)
            {
                if (item.ProductCode != null && item.ProductCode != 0 && item.BasicAmt != null && item.BasicAmt != 0 && item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    var unitcode = _unitRepository.GetById(item.UnitId).Code;
                    var salesDetail = new SalesOrderDetail();
                    salesDetail.SNo = sno;
                    salesDetail.ProductCode = item.ProductCode;
                    salesDetail.AltQty = item.AltQty;
                    salesDetail.Qty = Convert.ToDecimal(item.Qty);
                    salesDetail.AltStockQty = item.AltQty;
                    salesDetail.StockQty = Convert.ToDecimal(item.Qty);
                    salesDetail.Rate = Convert.ToDecimal(item.Rate);
                    salesDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    salesDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    salesDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    salesDetail.Remarks = order.Remarks;
                    salesDetail.OrderId = order.Id;
                    salesDetail.Unit = unitcode;
                    salesDetail.AltUnit = item.AltUnit;
                    _salesOrderDetail.Add(salesDetail);
                    tempOrderSalesDetailList.Add(salesDetail);
                    var accountTransactionDetail = new AccountTransaction();
                    accountTransactionDetail.VNo = order.OrderNo;
                    accountTransactionDetail.VDate = order.OrderDate;
                    accountTransactionDetail.CrRate = 1;

                    //Sales A/C Id need to specify in setting

                    //var stockTransaction = new StockTransaction();
                    //stockTransaction.AgentCode = UtilityService.GetAgentNameById(order.AgentCode);
                    //stockTransaction.AltQty = salesDetail.AltQty;
                    //stockTransaction.VMiti = salesDetail.SalesOrderMaster.OrderMiti;
                    //stockTransaction.AltStockQty = salesDetail.AltStockQty;
                    //stockTransaction.AltUnit = salesDetail.AltUnit;
                    //stockTransaction.Unit = unitcode;
                    //stockTransaction.CurrCode = "";
                    //stockTransaction.CurrRate = 0;
                    //stockTransaction.GlCode = order.GlCode;
                    //stockTransaction.NetAmt = salesDetail.NetAmt;
                    //stockTransaction.ProductCode = salesDetail.ProductCode;
                    //stockTransaction.Quantity = salesDetail.Qty;
                    //stockTransaction.Rate = Convert.ToDecimal(salesDetail.Rate);
                    //stockTransaction.SNo = sno;
                    //stockTransaction.Source = source;
                    //stockTransaction.ReferenceId = order.Id;
                    //stockTransaction.FYId = fiscalYearId;
                    //var stock = _stockTransaction.Filter(x => x.ProductCode == item.ProductCode).ToList().LastOrDefault();
                    //if (stock == null)
                    //{
                    //    stockTransaction.StockVal = Convert.ToDecimal(salesDetail.BasicAmt);
                    //    stockTransaction.StockQty = salesDetail.StockQty;
                    //}
                    //else
                    //{
                    //    stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(salesDetail.BasicAmt);
                    //    stockTransaction.StockQty = stock.StockQty - salesDetail.StockQty;
                    //}
                    //stockTransaction.TermAmt = salesDetail.TermAmt;
                    //stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    //stockTransaction.VDate = order.OrderDate;
                    //stockTransaction.VNo = order.OrderNo;
                    //stockTransaction.BranchId = currentBranchId;
                    //_stockTransaction.Add(stockTransaction);
                    sno++;
                }
            }
            return tempOrderSalesDetailList;
        }

        public void SaveSalesOrderBillingTerm(IEnumerable<BillingTermDetailViewModel> billTermList, SalesOrderMaster order,
            string source, List<SalesOrderDetail> tempSalesOrderDetailList, int userId, int fiscalYearId, int currentBranchId)
        {
            foreach (var term in billTermList)
            {
                var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                decimal drAmt = 0;
                decimal drLocalAmt = 0;
                decimal crAmt = 0;
                decimal crLocalAmt = 0;
                decimal termAmt = 0;
                if (term.Sign == "+")
                {
                    crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                    crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                    termAmt = crAmt;
                }
                else
                {
                    drAmt = Convert.ToDecimal(term.Amount);
                    drLocalAmt = Convert.ToDecimal(term.Amount);
                    termAmt = System.Math.Abs(drAmt) * (-1);
                }


                var billingTermDetail = new BillingTermDetail();
                billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                billingTermDetail.Index = term.BillingTermIndex;
                billingTermDetail.ReferenceId = order.Id;
                billingTermDetail.Source = source;
                billingTermDetail.Rate = Convert.ToDecimal(term.Rate);
                if (term.BillingTermIndex != null)
                {
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    if (term.IsProductWise)
                        billingTermDetail.DetailId = tempSalesOrderDetailList[index].Id;
                }

                billingTermDetail.IsProductWise = term.IsProductWise;
                billingTermDetail.TermAmount = termAmt;
                _billingTermDetail.Add(billingTermDetail);
            }
        }

        public void SaveSalesOrderOtherDetail(SalesOrderOtherDetail model, int orderId)
        {
            model.OrderId = orderId;
            if (model.Id == 0)
            {
                _salesOrderOtherDetail.Add(model);
            }
            else
            {
                _salesOrderOtherDetail.Update(model);
            }
        }

        public void DeleteSalesOrderRelatedDatas(int id, string source)
        {
            _stockTransaction.Delete(x => x.ReferenceId == id && x.Source == source);
            _salesOrderDetail.Delete(x => x.OrderId == id);
            _billingTermDetail.Delete(x => x.ReferenceId == id && x.Source == source);
        }

        public SalesOrderMaster UpdateSalesOrder(SalesOrderMaster model, DateTime orderDate, int fiscalYearId, int currentBranchId, string source, int userId)
        {
            model.OrderDate = orderDate;
            model.BasicAmt = Convert.ToDecimal(model.BasicAmt);
            model.NetAmt = Convert.ToDecimal(model.NetAmt);
            model.TermAmt = Convert.ToDecimal(model.TermAmt);
            _salesOrderMaster.Update(model);
            return model;
        }
        public SalesOrderMaster ApproveSalesOrder(int id)
        {
            var data = _salesOrderMaster.GetById(id);
            _salesOrderMaster.Update(data);
            return data;
        }

        public SalesOrderMaster GetSalesOrder(int id)
        {
            return _salesOrderMaster.GetById(id);
        }

        public IEnumerable<SalesOrderMaster> GetSalesOrderList(int currentBranchId)
        {
            var list = _salesChallanMaster.GetMany(x => !x.IsDeleted).Select(x => x.OrderId);
            var saleslist = _salesInvoice.GetMany(x => !x.IsDeleted).Select(x => x.OrderId);
            return _salesOrderMaster.GetMany(x => !x.IsDeleted && x.BranchId == currentBranchId && !list.Contains(x.Id) && !saleslist.Contains(x.Id));
        }

        public SalesOrderOtherDetail GetSalesOrderOtherDetail(int orderId)
        {
            return _salesOrderOtherDetail.GetById(x => x.OrderId == orderId);
        }

        public void DeleteSalesOrder(int orderId)
        {
            var model = _salesOrderMaster.GetById(x => x.Id == orderId);
            model.IsDeleted = true;
            _salesOrderMaster.Update(model);
            _stockTransaction.Delete(x => x.Source == "SC" && x.ReferenceId == orderId);
        }
        #endregion

        #region Challan

        public SalesChallanMaster CheckChallanNo(string ChallanNo, int? Id)
        {
            var challan = new SalesChallanMaster();
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);

            if (Id.HasValue && Id != 0)
            {
                var data = _salesChallanMaster.GetById(x => x.Id == Id);
                if (data.ChallanNo.ToLower().Trim() != ChallanNo.ToLower().Trim())
                {
                    challan =
                        _salesChallanMaster.GetById(x => x.ChallanNo.ToLower().Trim() == ChallanNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);

                }
            }
            else
            {
                challan = _salesChallanMaster.GetById(x => x.ChallanNo.ToLower().Trim() == ChallanNo.Trim().ToLower() && !x.IsDeleted && x.FYId == fiscalYear.Id);
            }
            return challan ?? (challan = new SalesChallanMaster());

        }

        public PagedList<SalesChallanMaster> GetPendingSalesChallan(int pageNo, int pageSize, int currentBranchId)
        {
            var fiscalYear = _fiscalYear.GetById(x => x.IsDefalut);
            return _salesChallanMaster.Filter(x => x.BranchId == currentBranchId && !x.IsDeleted && x.FYId == fiscalYear.Id).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, pageSize);
        }

        public PagedList<SalesChallanMaster> GetAcceptedSalesChallan(int pageNo, int pageSize, int currentBranchId)
        {
            return _salesChallanMaster.Filter(x => x.IsApproved && x.BranchId == currentBranchId && !x.IsDeleted).AsQueryable().OrderBy(x => x.Id).ToPagedList(pageNo, pageSize);
        }

        public SalesChallanMaster ApproveSalesChallan(int id)
        {
            var data = _salesChallanMaster.GetById(id);
            _salesChallanMaster.Update(data);
            return data;
        }

        public void DeleteSalesChallan(int challanId)
        {
            var model = _salesChallanMaster.GetById(x => x.Id == challanId);
            model.IsDeleted = true;
            _salesChallanMaster.Update(model);
            _stockTransaction.Delete(x => x.Source == "SC" && x.ReferenceId == challanId);
        }

        public SalesChallanAddViewModel PopulateSalesChallanAddViewModel(SalesChallanAddViewModel viewModel, bool isCurrDate, int dateType)
        {

            viewModel.AllowProductWiseBillTerm = false;

            // viewModel.ChallanDate = data != null ? data.InvoiceDate : DateTime.UtcNow;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
                viewModel.AllowProductWiseBillTerm = true;
            viewModel.SalesChallan = new SalesChallanMaster();
            viewModel.SalesChallan.ChallanMiti = NepaliDateService.NepaliDate(DateTime.Now).Date;
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Id", "Code");
            viewModel.SalesChallanImpTransDoc = new SalesChallanImpTransDoc();
            if (isCurrDate)
            {
                viewModel.ChallanDate = dateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            else
            {
                var data = _salesChallanMaster.GetAll().LastOrDefault();
                if (data != null)
                {
                    viewModel.ChallanDate = dateType == 1 ? data.ChallanDate.ToShortDateString() : data.ChallanMiti;
                }
                else
                {
                    viewModel.ChallanDate = dateType == 1 ? DateTime.Now.ToShortDateString() : NepaliDateService.NepaliDate(DateTime.Now).Date;
                }

            }
            return viewModel;
        }

        public SalesChallanMaster SaveSalesChallan(SalesChallanMaster model, string challanDate, int fiscalYearId, int currentBranchId, string source, int userId, int dateType)
        {
            model.BasicAmt = Convert.ToDecimal(model.BasicAmt);
            model.NetAmt = Convert.ToDecimal(model.NetAmt);
            model.TermAmt = Convert.ToDecimal(model.TermAmt);
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            //  model.PurchaseChallan.ChallanDate = model.ChallanDate;
            model.FYId = fiscalYearId;
            model.BranchId = currentBranchId;
            model.IsDeleted = false;
            model.OrderId = model.OrderId;
            model.OrderNo = model.OrderNo;
            if (dateType == 1)
            {
                model.ChallanDate = challanDate.ToDate();
                model.ChallanMiti = NepaliDateService.NepaliDate(model.ChallanDate).Date;
            }
            else
            {
                model.ChallanMiti = challanDate;
                model.ChallanDate = NepaliDateService.NepalitoEnglishDate(challanDate);
            }
            _salesChallanMaster.Add(model);
            return model;
        }

        public List<SalesChallanDetail> SaveSalesChallanDetail(IEnumerable<SalesDetailEntryViewModel> salesDetailEntry,
           SalesChallanMaster challan, string source, int userId,
           int fiscalYearId, int currentBranchId)
        {
            var tempSalesChallanDetailList = new List<SalesChallanDetail>();
            int sno = 1;
            foreach (var item in salesDetailEntry)
            {
                if (item.ProductId != null && item.ProductId != 0 && item.BasicAmt != null && item.BasicAmt != 0 &&
                    item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    var unitcode = _unitRepository.GetById(item.UnitId).Code;
                    var salesChallanDetail = new SalesChallanDetail();
                    salesChallanDetail.SNo = sno;
                    salesChallanDetail.ProductCode = item.ProductId;
                    salesChallanDetail.AltQty = item.AltQty;
                    salesChallanDetail.Qty = Convert.ToDecimal(item.Qty);
                    salesChallanDetail.AltStockQty = item.AltQty;
                    salesChallanDetail.StockQty = Convert.ToDecimal(item.Qty);
                    salesChallanDetail.Rate = Convert.ToDecimal(item.Rate);
                    salesChallanDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    salesChallanDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    salesChallanDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    salesChallanDetail.Remarks = challan.Remarks;
                    salesChallanDetail.ChallanId = challan.Id;
                    salesChallanDetail.Unit = unitcode;
                    salesChallanDetail.UnitId = unitcode.ToInt32();
                    salesChallanDetail.AltUnit = item.AltUnit;
                    _salesChallanDetail.Add(salesChallanDetail);
                    tempSalesChallanDetailList.Add(salesChallanDetail);

                    var stockTransaction = new StockTransaction();
                    var salesChallanMaster = new SalesChallanMaster();
                    stockTransaction.AgentCode = UtilityService.GetAgentNameById(challan.AgentCode);
                    stockTransaction.AltQty = salesChallanDetail.AltQty;
                    stockTransaction.AltStockQty = salesChallanDetail.AltStockQty;
                    stockTransaction.AltUnit = salesChallanDetail.AltUnit;
                    stockTransaction.Unit = unitcode;
                    stockTransaction.CurrCode = "";
                    stockTransaction.CurrRate = 0;
                    stockTransaction.GlCode = challan.GlCode;
                    stockTransaction.NetAmt = salesChallanDetail.NetAmt;
                    stockTransaction.ProductCode = salesChallanDetail.ProductCode;
                    stockTransaction.Quantity = salesChallanDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(salesChallanDetail.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = source;
                    stockTransaction.ReferenceId = challan.Id;
                    stockTransaction.FYId = fiscalYearId;
                    var stock = _stockTransaction.Filter(x => x.ProductCode == item.ProductId).ToList().LastOrDefault();
                    if (stock == null)
                    {
                        stockTransaction.StockVal = Convert.ToDecimal(salesChallanDetail.BasicAmt);
                        stockTransaction.StockQty = salesChallanDetail.StockQty;
                    }
                    else
                    {
                        stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(salesChallanDetail.BasicAmt);
                        stockTransaction.StockQty = stock.StockQty - salesChallanDetail.StockQty;
                    }
                    stockTransaction.TermAmt = salesChallanDetail.TermAmt;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    stockTransaction.VDate = challan.ChallanDate;
                    stockTransaction.VMiti =
                       NepaliDateService.NepaliDate(challan.ChallanDate).Date;
                    //stockTransaction.VMiti = salesChallanMaster.ChallanMiti;
                    stockTransaction.VNo = challan.ChallanNo;
                    stockTransaction.BranchId = currentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;
                }
            }
            return tempSalesChallanDetailList;
        }

        public List<SalesChallanDetail> SaveSalesChallanDetail(IEnumerable<SalesChallanDetail> salesDetailEntry,
          SalesChallanMaster challan, string source, int userId,
          int fiscalYearId, int currentBranchId)
        {
            var tempSalesChallanDetailList = new List<SalesChallanDetail>();
            int sno = 1;
            foreach (var item in salesDetailEntry)
            {
                if (item.ProductCode != null && item.ProductCode != 0 && item.BasicAmt != null && item.BasicAmt != 0 &&
                    item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    //var unitcode = _unitRepository.GetById(item.Unit).Code;
                    var salesChallanDetail = new SalesChallanDetail();
                    salesChallanDetail.SNo = sno;
                    salesChallanDetail.ProductCode = item.ProductCode;
                    salesChallanDetail.AltQty = item.AltQty;
                    salesChallanDetail.Qty = Convert.ToDecimal(item.Qty);
                    salesChallanDetail.AltStockQty = item.AltQty;
                    salesChallanDetail.StockQty = Convert.ToDecimal(item.Qty);
                    salesChallanDetail.Rate = Convert.ToDecimal(item.Rate);
                    salesChallanDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    salesChallanDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    salesChallanDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    salesChallanDetail.Remarks = challan.Remarks;
                    salesChallanDetail.ChallanId = challan.Id;
                    salesChallanDetail.Unit = item.Unit;
                    //salesChallanDetail.Unit = unitcode;
                    salesChallanDetail.AltUnit = item.AltUnit;
                    _salesChallanDetail.Add(salesChallanDetail);
                    tempSalesChallanDetailList.Add(salesChallanDetail);

                    var stockTransaction = new StockTransaction();
                    var salesChallanMaster = new SalesChallanMaster();
                    stockTransaction.AgentCode = UtilityService.GetAgentNameById(challan.AgentCode);
                    stockTransaction.AltQty = salesChallanDetail.AltQty;
                    stockTransaction.AltStockQty = salesChallanDetail.AltStockQty;
                    stockTransaction.AltUnit = salesChallanDetail.AltUnit;
                    stockTransaction.Unit = item.Unit;
                    // stockTransaction.Unit = unitcode;
                    stockTransaction.CurrCode = "";
                    stockTransaction.CurrRate = 0;
                    stockTransaction.GlCode = challan.GlCode;
                    stockTransaction.NetAmt = salesChallanDetail.NetAmt;
                    stockTransaction.ProductCode = salesChallanDetail.ProductCode;
                    stockTransaction.Quantity = salesChallanDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(salesChallanDetail.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = source;
                    stockTransaction.ReferenceId = challan.Id;
                    stockTransaction.FYId = fiscalYearId;
                    // stockTransaction.VDate = challan.ChallanDate;
                    //stockTransaction.VMiti = NepaliDateService.NepaliDate(challan.ChallanDate).Date;
                    var stock = _stockTransaction.Filter(x => x.ProductCode == item.ProductCode).ToList().LastOrDefault();
                    if (stock == null)
                    {
                        stockTransaction.StockVal = Convert.ToDecimal(salesChallanDetail.BasicAmt);
                        stockTransaction.StockQty = salesChallanDetail.StockQty;
                    }
                    else
                    {
                        stockTransaction.StockVal = stock.StockVal - Convert.ToDecimal(salesChallanDetail.BasicAmt);
                        stockTransaction.StockQty = stock.StockQty - salesChallanDetail.StockQty;
                    }
                    stockTransaction.TermAmt = salesChallanDetail.TermAmt;
                    stockTransaction.TransactionType = StringEnum.Parse(typeof(TransactionTypeEnum), "Out").ToString();
                    stockTransaction.VDate = challan.ChallanDate;
                    stockTransaction.VMiti = NepaliDateService.NepaliDate(challan.ChallanDate).Date;
                    stockTransaction.VNo = challan.ChallanNo;
                    stockTransaction.BranchId = currentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;
                }
            }
            return tempSalesChallanDetailList;
        }

        public void SaveSalesChallanImpTransDoc(SalesChallanImpTransDoc model, int challanId, int dateType, string PPDDate, string CnDate)
        {
            model.ChallanId = challanId;
            if (dateType == 1)
            {
                model.PPDDate = PPDDate.ToDate();
                model.PPDMiti = NepaliDateService.NepaliDate(PPDDate.ToDate()).Date;
            }
            else
            {
                model.PPDMiti = PPDDate;
                model.PPDDate = NepaliDateService.NepalitoEnglishDate(PPDDate);
            }
            if (dateType == 1)
            {
                model.CnDate = CnDate.ToDate();
                model.CnMiti = NepaliDateService.NepaliDate(CnDate.ToDate()).Date;
            }
            else
            {
                model.CnMiti = CnDate;
                model.CnDate = NepaliDateService.NepalitoEnglishDate(CnDate);
            }

            if (model.Id == 0)
                _salesChallanImpTransDoc.Add(model);
            else
                _salesChallanImpTransDoc.Update(model);
        }

        public void SaveSalesChallanBillingTerm(IEnumerable<BillingTermDetailViewModel> billTermList, SalesChallanMaster challan,
            string source, List<SalesChallanDetail> tempSalesChallanDetailList, int userId, int fiscalYearId, int currentBranchId)
        {
            foreach (var term in billTermList)
            {
                decimal drAmt = 0;
                decimal drLocalAmt = 0;
                decimal crAmt = 0;
                decimal crLocalAmt = 0;
                decimal termAmt = 0;
                if (term.Sign == "+")
                {
                    crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                    crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                    termAmt = crAmt;
                }
                else
                {
                    drAmt = Convert.ToDecimal(term.Amount);
                    drLocalAmt = Convert.ToDecimal(term.Amount);
                    termAmt = System.Math.Abs(drAmt) * (-1);
                }


                var billingTermDetail = new BillingTermDetail();
                billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                billingTermDetail.Index = term.BillingTermIndex;
                billingTermDetail.ReferenceId = challan.Id;
                billingTermDetail.Source = source;
                billingTermDetail.Rate = Convert.ToDecimal(term.Rate);
                if (term.BillingTermIndex != null)
                {
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    if (term.IsProductWise)
                        billingTermDetail.DetailId = tempSalesChallanDetailList[index].Id;
                }

                billingTermDetail.IsProductWise = term.IsProductWise;
                billingTermDetail.TermAmount = termAmt;
                _billingTermDetail.Add(billingTermDetail);
            }
        }

        public SalesChallanAddViewModel PopulateSalesChallanEditViewModel(SalesChallanAddViewModel viewModel, int challanId, ref int allowProductWiseBillTerm, int dateType, ref List<BillingTermDetail> billTerm)
        {
            var data = _salesChallanMaster.GetById(challanId);
            var purchaseChallanImpTransDoc = _salesChallanImpTransDoc.GetById(x => x.ChallanId == data.Id);
            viewModel.SalesChallanImpTransDoc = purchaseChallanImpTransDoc ?? new SalesChallanImpTransDoc();
            viewModel.SalesChallan = data;
            var cat = Convert.ToInt32(KRBAccounting.Enums.BillingTermCategoryEnum.General);
            var billingTerm = _billingTerm.Filter(x => x.Type == "P" && x.IsProductWise && x.Category == cat);
            if (billingTerm.Count() != 0)
            {
                allowProductWiseBillTerm = 1;
                viewModel.AllowProductWiseBillTerm = true;
            }
            if (dateType == 1)
            {
                viewModel.ChallanDate = data.ChallanDate.ToString("MM/dd/yyyy");
            }
            else
            {
                viewModel.ChallanDate = data.ChallanMiti;
            }
            if (viewModel.SalesChallanImpTransDoc.PPDDate != null)
            {
                if (dateType == 1)
                {
                    viewModel.PPDDate = Convert.ToDateTime(viewModel.SalesChallanImpTransDoc.PPDDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.PPDDate = viewModel.SalesChallanImpTransDoc.PPDMiti;
                }

                if (dateType == 1)
                {
                    viewModel.CnDate = Convert.ToDateTime(viewModel.SalesChallanImpTransDoc.CnDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    viewModel.CnDate = viewModel.SalesChallanImpTransDoc.CnMiti;
                }
            }
            foreach (var term in viewModel.SalesChallan.SalesChallanDetails)
            {
                term.Unit = _unitRepository.GetById(x => x.Code.ToLower() == term.Unit.ToLower()).Code;
                // var unitName= _unitRepository.GetById(x => x.Id == term.UnitId).Id;
                //  term.Unit = unitName.ToString();
            }

            billTerm = _billingTermDetail.Filter(x => x.ReferenceId == challanId && x.Source == "SC").ToList();

            if (!billTerm.Any())
            {
                billTerm = new List<BillingTermDetail>();
            }
            viewModel.TotalAmtRs = viewModel.SalesChallan.NetAmt.ToString();
            viewModel.UnitList = new SelectList(_unitRepository.GetAll(), "Code", "Code");
            viewModel.OrderNo = data.OrderNo;
            viewModel.SalesOrderId = data.OrderId;
            return viewModel;
        }

        public SalesChallanMaster UpdateSalesChallan(SalesChallanMaster model, string challanDate, int fiscalYearId, int currentBranchId, string source, int userId, int dateType)
        {
            model.FYId = fiscalYearId;
            model.BasicAmt = Convert.ToDecimal(model.BasicAmt);
            model.NetAmt = Convert.ToDecimal(model.NetAmt);
            model.TermAmt = Convert.ToDecimal(model.TermAmt);
            if (dateType == 1)
            {
                model.ChallanDate = Convert.ToDateTime(challanDate);
                model.ChallanMiti = NepaliDateService.NepaliDate(model.ChallanDate).Date;
            }
            else
            {
                model.ChallanMiti = challanDate;
                model.ChallanDate = NepaliDateService.NepalitoEnglishDate(challanDate);
            }
            _salesChallanMaster.Update(model);
            return model;
        }

        public void DeleteSalesChallanRelatedDatas(int id, string source)
        {
            _stockTransaction.Delete(x => x.ReferenceId == id && x.Source == source);
            _salesChallanDetail.Delete(x => x.ChallanId == id);
            _billingTermDetail.Delete(x => x.ReferenceId == id && x.Source == source);
        }

        public SalesChallanMaster GetSalesChallanById(int id)
        {
            return _salesChallanMaster.GetById(x => x.Id == id, "SalesChallanDetails");
        }

        public IEnumerable<SalesChallanMaster> GetSalesChallanList(int CurrentBranchId)
        {
            var list = _salesInvoice.GetMany(x => !x.IsDeleted).Select(x => x.ChallanId);
            return _salesChallanMaster.GetMany(x => !x.IsDeleted && x.BranchId == CurrentBranchId && !list.Contains(x.Id));
        }
        #endregion

        #region Purchase Invoice
        public int SavePurchaseInvoice(PurchaseInvoiceAddViewModel model, int userId, int currentBranchId, int fiscalYearId)
        {
            var source = StringEnum.Parse(typeof(ModuleEnum), "Purchase").ToString();
            model.PurchaseInvoice.BasicAmt = Convert.ToDecimal(model.PurchaseInvoice.BasicAmt);
            model.PurchaseInvoice.NetAmt = Convert.ToDecimal(model.PurchaseInvoice.NetAmt);
            model.PurchaseInvoice.TermAmt = Convert.ToDecimal(model.PurchaseInvoice.TermAmt);
            model.PurchaseInvoice.CreatedById = userId;
            model.PurchaseInvoice.CreatedDate = DateTime.UtcNow;
            // model.PurchaseInvoice.InvoiceDate = model.InvoiceDate;
            model.PurchaseInvoice.FYId = fiscalYearId;
            model.PurchaseInvoice.BranchId = currentBranchId;
            model.PurchaseInvoice.OrderId = model.PurchaseOrderId;
            // model.PurchaseInvoice.OrderNo = model.OrderNo;
            _purchaseInvoice.Add(model.PurchaseInvoice);

            var accountTransactionMaster = new AccountTransaction();
            accountTransactionMaster.VNo = model.PurchaseInvoice.InvoiceNo;
            accountTransactionMaster.VDate = model.PurchaseInvoice.InvoiceDate;
            accountTransactionMaster.VMiti = model.PurchaseInvoice.InvoiceMiti;
            accountTransactionMaster.AgentCode = Convert.ToString(model.PurchaseInvoice.AgentCode);
            accountTransactionMaster.CrCode = model.PurchaseInvoice.CurrCode;
            accountTransactionMaster.CrRate = Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
            accountTransactionMaster.GlCode = model.PurchaseInvoice.GlCode;
            accountTransactionMaster.DrAmt = 0;
            accountTransactionMaster.LocalDrAmt = 0;
            accountTransactionMaster.CrAmt = Convert.ToDecimal(model.PurchaseInvoice.NetAmt);
            if (model.PurchaseInvoice.CurrencyId != null && model.PurchaseInvoice.CurrencyId != 0)
            {
                accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(model.PurchaseInvoice.NetAmt) * Convert.ToDecimal(model.PurchaseInvoice.CurrRate);
            }
            else
            {
                accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(model.PurchaseInvoice.NetAmt);
            }
            accountTransactionMaster.Source = source;
            accountTransactionMaster.SNo = 0;
            accountTransactionMaster.CreatedById = userId;
            var isCashBank = _ledger.GetById(model.PurchaseInvoice.GlCode).IsCashBank;
            if (isCashBank)
                accountTransactionMaster.CbCode = model.PurchaseInvoice.GlCode;
            accountTransactionMaster.FYId = fiscalYearId;
            accountTransactionMaster.ReferenceId = model.PurchaseInvoice.Id;
            accountTransactionMaster.BranchId = currentBranchId;
            accountTransactionMaster.Remarks = model.PurchaseInvoice.Remarks;
            accountTransactionMaster.SlCode = model.PurchaseInvoice.SlCode;
            _accountTransaction.Add(accountTransactionMaster);
            return model.PurchaseInvoice.Id;
        }
        public List<PurchaseDetail> SavePurchaseDetail(IEnumerable<PurchaseDetailEntryViewModel> purchaseDetailEntry, PurchaseInvoice invoice, int basePurchaseAc, int userId, int fiscalYearId, int currentBranchId, IEnumerable<PurchaseProductBatchViewModel> ProductBatchList, IEnumerable<BillingTermDetailViewModel> billTermList)
        {
            var source = StringEnum.Parse(typeof(ModuleEnum), "Purchase").ToString();
            int sno = 1;
            decimal basicAmt = 0;
            var tempPurchaseDetailList = new List<PurchaseDetail>();
            foreach (var item in purchaseDetailEntry)
            {
                if (item.ProductId != null && item.ProductId != 0 && item.BasicAmt != null && item.BasicAmt != 0 &&
                    item.Qty != null && item.Qty != 0 && item.Rate != null && item.Rate != 0)
                {
                    var batchSerialNo = string.Empty;
                    var unitcode = _unitRepository.GetById(item.Unit).Code;
                    basicAmt += Convert.ToDecimal(item.BasicAmt);
                    var purchaseDetail = new PurchaseDetail();
                    purchaseDetail.SNo = sno;
                    purchaseDetail.PCode = item.ProductId;
                    purchaseDetail.AltQty = item.AltQty;
                    purchaseDetail.Qty = Convert.ToDecimal(item.Qty);
                    purchaseDetail.AltStockQty = item.AltQty;
                    purchaseDetail.StockQty = Convert.ToDecimal(item.Qty);
                    purchaseDetail.Rate = Convert.ToDecimal(item.Rate);
                    purchaseDetail.BasicAmt = Convert.ToDecimal(item.BasicAmt);
                    purchaseDetail.TermAmt = Convert.ToDecimal(item.TermAmt);
                    purchaseDetail.NetAmt = Convert.ToDecimal(item.NetAmt);
                    purchaseDetail.Remarks = invoice.Remarks;
                    purchaseDetail.PurchaseInvoiceId = invoice.Id;
                    purchaseDetail.Index = item.Index;
                    purchaseDetail.Unit = unitcode;
                    purchaseDetail.AltUnit = item.AltUnit;
                    purchaseDetail.Godown = item.GodownId;
                    _purchaseDetail.Add(purchaseDetail);
                    tempPurchaseDetailList.Add(purchaseDetail);

                    if (ProductBatchList != null)
                    {
                        var batch = ProductBatchList.Where(x => x.ParentGuid == item.DetailGuid).FirstOrDefault();
                        if (batch != null)
                        {
                            batchSerialNo = batch.BatchSerialNo;
                            var productBatch = new PurchaseProductBatch();
                            productBatch.BranchId = currentBranchId;
                            productBatch.Godown = item.GodownId;
                            productBatch.ProductId = item.ProductId;
                            productBatch.DetailId = purchaseDetail.Id;
                            productBatch.Source = "PB";
                            productBatch.Qty = Convert.ToDecimal(item.Qty);
                            productBatch.SerialNo = batch.BatchSerialNo;
                            if (batch.IsMfgDate)
                                productBatch.MFGDate = batch.MfgDate;
                            if (batch.IsExpDate)
                                productBatch.EXPDate = batch.ExpDate;
                            productBatch.BuyRate = batch.BuyRate;
                            productBatch.SalesRate = batch.SalesRate;
                            productBatch.Unit = item.Unit;
                            productBatch.StockQuantity = Convert.ToDecimal(item.Qty);
                            _purchaseProductBatch.Add(productBatch);
                        }
                    }
                    int purchaseAc;
                    var product = _product.GetById(item.ProductId);
                    purchaseAc = product.PurchaseId != null && product.PurchaseId != 0
                                     ? Convert.ToInt32(product.PurchaseId)
                                     : basePurchaseAc;
                    var stockTransaction = new StockTransaction();
                    stockTransaction.AgentCode = UtilityService.GetAgentNameById(invoice.AgentCode);
                    stockTransaction.AltQty = purchaseDetail.AltQty;
                    stockTransaction.AltStockQty = purchaseDetail.AltStockQty;
                    stockTransaction.AltUnit = purchaseDetail.AltUnit;
                    stockTransaction.CurrCode = invoice.CurrCode;
                    stockTransaction.CurrRate = invoice.CurrRate;

                    stockTransaction.GlCode = invoice.GlCode;
                    stockTransaction.NetAmt = purchaseDetail.NetAmt;
                    stockTransaction.ProductCode = purchaseDetail.PCode;
                    stockTransaction.Quantity = purchaseDetail.Qty;
                    stockTransaction.Rate = Convert.ToDecimal(purchaseDetail.Rate);
                    stockTransaction.SNo = sno;
                    stockTransaction.Source = source;
                    stockTransaction.ReferenceId = invoice.Id;
                    stockTransaction.Unit = unitcode;
                    stockTransaction.AltUnit = item.AltUnit;
                    stockTransaction.FYId = fiscalYearId;
                    stockTransaction.BatchSerialNo = batchSerialNo;
                    stockTransaction.Godown = Convert.ToString(purchaseDetail.Godown);
                    var stock =
                        _stockTransaction.Filter(x => x.ProductCode == item.ProductId).ToList().LastOrDefault();
                    if (stock == null)
                    {
                        stockTransaction.StockVal = Convert.ToDecimal(purchaseDetail.BasicAmt);
                        stockTransaction.StockQty = purchaseDetail.StockQty;

                    }
                    else
                    {
                        stockTransaction.StockVal = stock.StockVal + Convert.ToDecimal(purchaseDetail.BasicAmt);
                        stockTransaction.StockQty = stock.StockQty + purchaseDetail.StockQty;
                    }
                    stockTransaction.TermAmt = purchaseDetail.TermAmt;
                    stockTransaction.TransactionType =
                        StringEnum.Parse(typeof(TransactionTypeEnum), "In").ToString();

                    stockTransaction.VDate = invoice.InvoiceDate;
                    stockTransaction.VMiti = invoice.InvoiceMiti;
                    stockTransaction.VNo = invoice.InvoiceNo;
                    stockTransaction.BranchId = currentBranchId;
                    _stockTransaction.Add(stockTransaction);
                    sno++;


                    var accountTransactionDetail = new AccountTransaction();
                    accountTransactionDetail.VNo = invoice.InvoiceNo;
                    accountTransactionDetail.VDate = invoice.InvoiceDate;
                    accountTransactionDetail.VMiti = invoice.InvoiceMiti;
                    accountTransactionDetail.CrRate = Convert.ToDecimal(invoice.CurrRate);
                    accountTransactionDetail.AgentCode = Convert.ToString(invoice.AgentCode);
                    accountTransactionDetail.CrCode = invoice.CurrCode;

                    //Purchase A/C Id need to specify in setting
                    //accountTransactionDetail.AgentCode = UtilityService.GetAgentNameById(invoice.AgentCode);

                    accountTransactionDetail.GlCode = purchaseAc;
                    accountTransactionDetail.DrAmt = Convert.ToDecimal(item.BasicAmt);

                    if (invoice.CurrencyId != null && invoice.CurrencyId != 0)
                    {
                        accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.BasicAmt) * Convert.ToDecimal(invoice.CurrRate);
                    }
                    else
                    {
                        accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(item.BasicAmt);
                    }
                    accountTransactionDetail.CrAmt = 0;
                    accountTransactionDetail.LocalCrAmt = 0;
                    accountTransactionDetail.Source = source;
                    accountTransactionDetail.SNo = 0;
                    accountTransactionDetail.CreatedById = userId;
                    var isCashBank = _ledger.GetById(purchaseAc).IsCashBank;
                    if (isCashBank)
                        accountTransactionDetail.CbCode = purchaseAc;
                    accountTransactionDetail.FYId = fiscalYearId;
                    accountTransactionDetail.ReferenceId = invoice.Id;
                    accountTransactionDetail.BranchId = currentBranchId;
                    accountTransactionDetail.Remarks = invoice.Remarks;
                    accountTransactionDetail.SlCode = invoice.SlCode;
                    _accountTransaction.Add(accountTransactionDetail);

                    if (billTermList != null)
                    {
                        decimal netAmt = 0;
                        //var billWiseTerms =
                        //    billTermList.Where(x => !x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList();
                        foreach (var term in billTermList.Where(x => x.IsProductWise && x.ParentGuid == item.DetailGuid).ToList())
                        {
                            var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                            decimal drAmt = 0;
                            decimal drLocalAmt = 0;
                            decimal crAmt = 0;
                            decimal crLocalAmt = 0;
                            decimal termAmt = 0;
                            if (term.Sign == "-")
                            {
                                crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                                termAmt = System.Math.Abs(crAmt) * (-1);
                            }
                            else
                            {
                                drAmt = Convert.ToDecimal(term.Amount);
                                drLocalAmt = Convert.ToDecimal(term.Amount);
                                termAmt = drAmt;
                            }
                            var billingTermDetail = new BillingTermDetail();
                            billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                            var index = Convert.ToInt32(term.BillingTermIndex);
                            billingTermDetail.Index = index;
                            billingTermDetail.ReferenceId = invoice.Id;
                            billingTermDetail.Source = source;
                            billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                            billingTermDetail.TermAmount = termAmt;
                            if (term.IsProductWise)
                            {
                                billingTermDetail.DetailId = purchaseDetail.Id;
                                billingTermDetail.Type = "P";
                            }
                            else
                            {
                                billingTermDetail.Type = "B";
                            }
                            billingTermDetail.IsProductWise = term.IsProductWise;
                            _billingTermDetail.Add(billingTermDetail);

                            //AccountTransaction Posting
                            var accountTransactionBillingTerm = new AccountTransaction();
                            accountTransactionBillingTerm.VNo = invoice.InvoiceNo;
                            accountTransactionBillingTerm.VDate = invoice.InvoiceDate;
                            accountTransactionBillingTerm.VMiti = invoice.InvoiceMiti;
                            accountTransactionBillingTerm.CrRate = Convert.ToDecimal(invoice.CurrRate);
                            accountTransactionBillingTerm.CrCode = invoice.CurrCode;
                            accountTransactionBillingTerm.AgentCode = Convert.ToString(invoice.AgentCode);
                            accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;

                            if (invoice.CurrencyId != null && invoice.CurrencyId != 0)
                            {
                                drLocalAmt = drLocalAmt * Convert.ToDecimal(invoice.CurrRate);
                                crLocalAmt = crLocalAmt * Convert.ToDecimal(invoice.CurrRate);
                            }
                            accountTransactionBillingTerm.DrAmt = drAmt;
                            accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                            accountTransactionBillingTerm.CrAmt = crAmt;
                            accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                            accountTransactionBillingTerm.Source = source;
                            accountTransactionBillingTerm.SNo = 0;
                            accountTransactionBillingTerm.CreatedById = userId;
                            accountTransactionBillingTerm.CbCode = invoice.GlCode;
                            accountTransactionBillingTerm.FYId = fiscalYearId;
                            accountTransactionBillingTerm.ReferenceId = invoice.Id;
                            accountTransactionBillingTerm.BranchId = currentBranchId;
                            accountTransactionBillingTerm.Remarks = invoice.Remarks;
                            _accountTransaction.Add(accountTransactionBillingTerm);
                        }
                    }
                }
            }
            if (billTermList != null)
            {
                foreach (var term in billTermList.Where(x => !x.IsProductWise).ToList())
                {
                    var billingTerm = _billingTerm.GetById(x => x.Id == term.Id);
                    decimal drAmt = 0;
                    decimal drLocalAmt = 0;
                    decimal crAmt = 0;
                    decimal crLocalAmt = 0;
                    decimal termAmt = 0;
                    if (term.Sign == "-")
                    {
                        crAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        crLocalAmt = Math.Abs(Convert.ToDecimal(term.Amount));
                        termAmt = System.Math.Abs(crAmt) * (-1);
                    }
                    else
                    {
                        drAmt = Convert.ToDecimal(term.Amount);
                        drLocalAmt = Convert.ToDecimal(term.Amount);
                        termAmt = drAmt;
                    }
                    var billingTermDetail = new BillingTermDetail();
                    billingTermDetail.BillingTermId = Convert.ToInt32(term.Id);
                    var index = Convert.ToInt32(term.BillingTermIndex);
                    billingTermDetail.Index = index;
                    billingTermDetail.ReferenceId = invoice.Id;
                    billingTermDetail.Source = source;
                    billingTermDetail.Rate = Convert.ToDecimal(term.Rate);

                    billingTermDetail.TermAmount = termAmt;
                    billingTermDetail.Type = "B";
                    billingTermDetail.IsProductWise = term.IsProductWise;
                    _billingTermDetail.Add(billingTermDetail);

                    //AccountTransaction Posting
                    var accountTransactionBillingTerm = new AccountTransaction();
                    accountTransactionBillingTerm.VNo = invoice.InvoiceNo;
                    accountTransactionBillingTerm.VDate = invoice.InvoiceDate;
                    accountTransactionBillingTerm.VMiti = invoice.InvoiceMiti;
                    accountTransactionBillingTerm.CrRate = 1;
                    accountTransactionBillingTerm.GlCode = billingTerm.GeneralLedgerId;
                    if (invoice.CurrencyId != null && invoice.CurrencyId != 0)
                    {
                        drLocalAmt = drLocalAmt * Convert.ToDecimal(invoice.CurrRate);
                        crLocalAmt = crLocalAmt * Convert.ToDecimal(invoice.CurrRate);
                    }
                    accountTransactionBillingTerm.DrAmt = drAmt;
                    accountTransactionBillingTerm.LocalDrAmt = drLocalAmt;
                    accountTransactionBillingTerm.CrAmt = crAmt;
                    accountTransactionBillingTerm.LocalCrAmt = crLocalAmt;
                    accountTransactionBillingTerm.Source = source;
                    accountTransactionBillingTerm.SNo = 0;
                    accountTransactionBillingTerm.CreatedById = userId;
                    accountTransactionBillingTerm.CbCode = invoice.GlCode;
                    accountTransactionBillingTerm.FYId = fiscalYearId;
                    accountTransactionBillingTerm.ReferenceId = invoice.Id;
                    accountTransactionBillingTerm.BranchId = currentBranchId;
                    accountTransactionBillingTerm.Remarks = invoice.Remarks;
                    _accountTransaction.Add(accountTransactionBillingTerm);
                }
            }
            return tempPurchaseDetailList;
        }

        #endregion

        #region PurchaseChallan
        // public int SavePurchaseChallan(PurchaseChallanAddViewModel model, string challanDate, int fiscalYearId, int currentBranchId, int userId, int dateType)
        public int SavePurchaseChallan(PurchaseChallanAddViewModel model, int userId, int currentBranchId, int fiscalYearId)
        {

            model.PurchaseChallan.BasicAmt = Convert.ToDecimal(model.PurchaseChallan.BasicAmt);
            model.PurchaseChallan.NetAmt = Convert.ToDecimal(model.PurchaseChallan.NetAmt);
            model.PurchaseChallan.TermAmt = Convert.ToDecimal(model.PurchaseChallan.TermAmt);
            model.PurchaseChallan.CreatedById = userId;
            model.PurchaseChallan.CreatedDate = DateTime.UtcNow;
            //  model.PurchaseChallan.ChallanDate = model.ChallanDate;
            model.PurchaseChallan.FYId = fiscalYearId;
            model.PurchaseChallan.BranchId = currentBranchId;
            model.PurchaseChallan.IsDeleted = false;
            model.PurchaseChallan.OrderId = model.PurchaseOrderId;
            model.PurchaseChallan.OrderNo = model.OrderNo;
            _purchaseChallanMaster.Add(model.PurchaseChallan);
            return model.PurchaseChallan.Id;
        }
        #endregion
    }

    public interface ISalesService
    {
        PagedList<SalesOrderMaster> GetPendingSalesOrder(int pageNo, int pageSize, int currentBranchId);
        PagedList<SalesOrderMaster> GetAcceptedSalesOrder(int pageNo, int pageSize, int currentBranchId);
        int SaveSalesInvoice(SalesInvoiceAddViewModel model, int userId, int currentBranchId, int fiscalYearId);
        List<SalesDetail> SaveSalesDetail(IEnumerable<SalesDetailEntryViewModel> salesDetailEntry, SalesInvoice invoice,
                                          int baseSalesAc, int userId, int fiscalYearId, int currentBranchId, IEnumerable<BillingTermDetailViewModel> billTermList);
        void SaveSalesBillingTerm(IEnumerable<BillingTermDetailViewModel> billTermList,
                                  List<SalesDetail> tempSalesDetailList, SalesInvoice invoice, int userId,
                                  int fiscalYearId, int currentBranchId);
        void SaveSalesInvoiceOtherDetail(SalesInvoiceOtherDetail model, int invoiceId);
        void SaveSalesInvoiceUDF(FormCollection formCollection, int invoiceId);

        SalesOrderMaster GetSalesOrder(int id, string includeProperties = "");
        SalesOrderMaster SaveSalesOrder(SalesOrderMaster model, DateTime orderDate, int fiscalYearId, int currentBranchId,
                           string source, int userId);
        List<SalesOrderDetail> SaveSalesOrderDetail(IEnumerable<SalesDetailEntryViewModel> salesDetailEntry,
                                                           SalesOrderMaster order, string source,
                                                           int userId,
                                                           int fiscalYearId, int currentBranchId);
        List<SalesOrderDetail> SaveSalesOrderDetail(IEnumerable<SalesOrderDetail> salesDetailEntry,
                                                           SalesOrderMaster order, string source,
                                                           int userId,
                                                           int fiscalYearId, int currentBranchId);
        void SaveSalesOrderBillingTerm(IEnumerable<BillingTermDetailViewModel> billTermList, SalesOrderMaster order,
                                       string source, List<SalesOrderDetail> tempSalesOrderDetailList, int userId,
                                       int fiscalYearId, int currentBranchId);
        void SaveSalesOrderOtherDetail(SalesOrderOtherDetail model, int orderId);
        SalesOrderMaster GetSalesOrder(int id);
        SalesOrderOtherDetail GetSalesOrderOtherDetail(int orderId);
        void DeleteSalesOrderRelatedDatas(int id, string source);
        SalesOrderMaster UpdateSalesOrder(SalesOrderMaster model, DateTime orderDate, int fiscalYearId,
                                          int currentBranchId, string source, int userId);
        void DeleteSalesOrder(int orderId);
        SalesChallanMaster CheckChallanNo(string InvoiceNo, int? Id);

        PagedList<SalesChallanMaster> GetPendingSalesChallan(int pageNo, int pageSize, int currentBranchId);
        PagedList<SalesChallanMaster> GetAcceptedSalesChallan(int pageNo, int pageSize, int currentBranchId);
        IEnumerable<SalesOrderMaster> GetSalesOrderList(int currentBranchId);
        void DeleteSalesChallan(int challanId);

        SalesChallanAddViewModel PopulateSalesChallanAddViewModel(SalesChallanAddViewModel viewModel, bool isCurrDate,
                                                                  int dateType);

        SalesChallanMaster SaveSalesChallan(SalesChallanMaster model, string challanDate, int fiscalYearId,
                                            int currentBranchId, string source, int userId, int dateType);

        List<SalesChallanDetail> SaveSalesChallanDetail(IEnumerable<SalesDetailEntryViewModel> salesDetailEntry,
                                                        SalesChallanMaster challan, string source, int userId,
                                                        int fiscalYearId, int currentBranchId);

        void SaveSalesChallanImpTransDoc(SalesChallanImpTransDoc model, int challanId, int dateType, string PPDDate,
                                         string CnDate);

        void SaveSalesChallanBillingTerm(IEnumerable<BillingTermDetailViewModel> billTermList,
                                         SalesChallanMaster challan,
                                         string source, List<SalesChallanDetail> tempSalesChallanDetailList, int userId,
                                         int fiscalYearId, int currentBranchId);

        List<SalesChallanDetail> SaveSalesChallanDetail(IEnumerable<SalesChallanDetail> salesDetailEntry,
                                                        SalesChallanMaster challan, string source, int userId,
                                                        int fiscalYearId, int currentBranchId);

        SalesChallanAddViewModel PopulateSalesChallanEditViewModel(SalesChallanAddViewModel viewModel, int challanId,
                                                                   ref int allowProductWiseBillTerm, int dateType,
                                                                   ref List<BillingTermDetail> billTerm);

        SalesChallanMaster UpdateSalesChallan(SalesChallanMaster model, string challanDate, int fiscalYearId,
                                              int currentBranchId, string source, int userId, int dateType);

        void DeleteSalesChallanRelatedDatas(int id, string source);
        SalesChallanMaster GetSalesChallanById(int id);
        IEnumerable<SalesChallanMaster> GetSalesChallanList(int CurrentBranchId);
        SalesOrderMaster ApproveSalesOrder(int id);
        SalesChallanMaster ApproveSalesChallan(int id);
        int SavePurchaseInvoice(PurchaseInvoiceAddViewModel model, int userId, int currentBranchId, int fiscalYearId);

        List<PurchaseDetail> SavePurchaseDetail(IEnumerable<PurchaseDetailEntryViewModel> purchaseDetailEntry,
                                                PurchaseInvoice invoice, int basePurchaseAc, int userId,
                                                int fiscalYearId, int currentBranchId, IEnumerable<PurchaseProductBatchViewModel> ProductBatchList, IEnumerable<BillingTermDetailViewModel> billTermList);

        //void SavePurchaseBillingTerm(IEnumerable<BillingTermDetailViewModel> billTermList,
        //                             List<PurchaseDetail> tempPurchaseDetailList, PurchaseInvoice invoice, int userId,
        //                             int fiscalYearId, int currentBranchId);

        int SavePurchaseChallan(PurchaseChallanAddViewModel model, int userId, int currentBranchId, int fiscalYearId);
    }
}
