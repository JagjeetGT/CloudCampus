using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Models;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.ViewModels.Entry;
using KRBAccounting.Data.Repositories;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using PaperSize = CrystalDecisions.Shared.PaperSize;

namespace KRBAccounting.Web.Controllers
{
    public class PrintController : BaseController
    {
        //
        // GET: /Print/
        private readonly ISalesInvoiceRepository _salesInvoice;
        private readonly IProductRepository _product;
        private readonly IBillingTermDetailRepository _billingTermDetail;
        private readonly IBillingTermRepository _billingTerm;
        private readonly IAgentRepository _agent;
        private readonly IFormsAuthenticationService _authentication;

        public PrintController(
            ISalesInvoiceRepository salesInvoice,
            IProductRepository product,
            IBillingTermDetailRepository billingTermDetail,
            IBillingTermRepository billingTerm,
            IAgentRepository agent
            )
        {
            _salesInvoice = salesInvoice;
            _product = product;
            _billingTermDetail = billingTermDetail;
            _billingTerm = billingTerm;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _agent = agent;
        }

        public ActionResult SalesInvoicePrint(int id)
        {
                var viewModel = this.CreateReportViewModel<PurchaseInvoicePrintViewModel>("Sales Invoice");
                viewModel.SalesInvoice = _salesInvoice.GetById(id);
                if (viewModel.SalesInvoice.AgentCode.HasValue)
                {
                    viewModel.AgentName = _agent.GetById(viewModel.SalesInvoice.AgentCode.Value).Name;
                }
                double subTotal = 0;
                var billingTerms =
                _billingTermDetail.GetMany(x => x.ReferenceId == id && x.Source == "SB").OrderBy(x => x.Index);
                viewModel.InvoiceBillingTerms = new List<InvoiceBillingTerms>();
                double termAmt = 0;
                foreach (var item in billingTerms)
                {
                    var invoiceBillingTerm = new InvoiceBillingTerms();
                    var billingTerm = _billingTerm.GetById(x => x.Id == item.BillingTermId);
                    var termName = billingTerm.Description;
                    invoiceBillingTerm.DisplayOrder = billingTerm.DispalyOrder;
                    invoiceBillingTerm.TermName = termName;
                    invoiceBillingTerm.TermRate = item.Rate;
                    invoiceBillingTerm.DetailId = item.DetailId;
                    invoiceBillingTerm.Type = item.Type;
                    invoiceBillingTerm.TermAmount = item.TermAmount;
                    viewModel.InvoiceBillingTerms.Add(invoiceBillingTerm);
                    termAmt += Convert.ToDouble(item.TermAmount);
                }
                viewModel.SubTotal = subTotal;
                viewModel.Total = subTotal + termAmt;

                var view = this.RenderPartialViewToString("SalesInvoicePrint", viewModel);
                return Json(new {Body = view}, JsonRequestBehavior.AllowGet);
            }
            //var report = new ExportFormatType();
            //try
            //{
            //    bool isValid = true;

            //    var viewModel = this.CreateReportViewModel<PurchaseInvoicePrintViewModel>("Sales Invoice");
            //    viewModel.SalesInvoice = _salesInvoice.GetById(id);
            //    if (viewModel.SalesInvoice.AgentCode.HasValue)
            //    {
            //        viewModel.AgentName = _agent.GetById(viewModel.SalesInvoice.AgentCode.Value).Name;
            //    }
            //    var _context = new DataContext();
            //    var c = _context.CompanyInfos.FirstOrDefault();
            //    var reporthe = new List<ReportHeader>
            //                       {
            //                           new ReportHeader()
            //                               {
            //                                    CompanyAddress = c.Address,
            //                           CompanyName = c.Name,
            //                           ReportTitle = "Sales Invoice"
            //                               }
                                      
            //                       };
            //    this.HttpContext.Session["rptSource"] = reporthe;

            //    var rptSource = System.Web.HttpContext.Current.Session["rptSource"];
            //    double subTotal = 0;
            //    var billingTerms =
            //        _billingTermDetail.GetMany(x => x.ReferenceId == id && x.Source == "SB").OrderBy(x => x.Index);
            //    viewModel.InvoiceBillingTerms = new List<InvoiceBillingTerms>();
            //    double termAmt = 0;
            //    foreach (var item in billingTerms)
            //    {
            //        var invoiceBillingTerm = new InvoiceBillingTerms();
            //        var billingTerm = _billingTerm.GetById(x => x.Id == item.BillingTermId);
            //        var termName = billingTerm.Description;
            //        invoiceBillingTerm.DisplayOrder = billingTerm.DispalyOrder;
            //        invoiceBillingTerm.TermName = termName;
            //        invoiceBillingTerm.TermRate = item.Rate;
            //        invoiceBillingTerm.DetailId = item.DetailId;
            //        invoiceBillingTerm.Type = item.Type;
            //        invoiceBillingTerm.TermAmount = item.TermAmount;
            //        viewModel.InvoiceBillingTerms.Add(invoiceBillingTerm);
            //        termAmt += Convert.ToDouble(item.TermAmount);
            //    }
            //    viewModel.SubTotal = subTotal;
            //    viewModel.Total = subTotal + termAmt;

            //    var rd = new ReportDocument();
            //    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Rpts//CrystalReport1.rpt";
            //    rd.Load(strRptPath);
            //    //  if (rptSource != null && rptSource.GetType().ToString() != "System.String")
            //    TableLogOnInfo logOnInfo = new TableLogOnInfo();

            //    string server = "sujan-pc";
            //    string userID = "sa";
            //    string passwd = "sql";
            //    string DBNAME = "Academy_V3";

            //    int i = 0;
            //    for (i = 0; i <= rd.Database.Tables.Count - 1; i++)
            //    {
            //        logOnInfo.ConnectionInfo.ServerName = server;
            //        logOnInfo.ConnectionInfo.UserID = userID;
            //        logOnInfo.ConnectionInfo.Password = passwd;
            //        logOnInfo.ConnectionInfo.DatabaseName = DBNAME;
            //        rd.Database.Tables[i].ApplyLogOnInfo(logOnInfo);
            //    } rd.SetParameterValue("Id", id);
            //   // rd.PrintOptions.PrinterName = "Brother DCP-7055 Printer";
            //    PrinterSettings printer = new PrinterSettings();
            //    printer.PrinterName = "Brother DCP-7055 Printer";
           

            //    PageSettings Page = new PageSettings();
            //    Page.PaperSize = new System.Drawing.Printing.PaperSize()
            //                         {
            //                             Height = 210,
            //                             Width = 210
            //                         };
            //    Page.Landscape = true;



            //    rd.PrintToPrinter(printer, Page,false);

            //    //rd.ExportToHttpResponse(ExportFormatType.Text, System.Web.HttpContext.Current.Response, false,
            //                    //        "crReport");
              
                
            //    report=ExportFormatType.Text;
              
              

            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.ToString());
            //}

           // return Json(new { Body = report }, JsonRequestBehavior.AllowGet);
        
        private List<Student> GetStudents()
        {
            return new List<Student>() { 
            new Student(){StudentID=1,StudentName="Hasibul"},
            new Student(){StudentID=2,StudentName="Tst"}
            };
        }
    }
}
