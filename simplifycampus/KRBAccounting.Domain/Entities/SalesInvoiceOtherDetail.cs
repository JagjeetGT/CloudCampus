using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace KRBAccounting.Domain.Entities
{
    public class SalesInvoiceOtherDetail
    {
        [Key]
        public int Id { get; set; }
        public string Transport { get; set; }
        public string VehicleNo { get; set; }
        public string CnNo { get; set; }
        public DateTime? CnDate { get; set; }
        public string CnMiti { get; set; }
        public string CnType { get; set; }
        public decimal? Package { get; set; }
        public string DocBank { get; set; }
        public string DocThru { get; set; }
        public string PONo { get; set; }
        public DateTime? PODate { get; set; }
        public string POMiti { get; set; }
        public decimal? CnFreight { get; set; }
        public decimal? CnAdvance { get; set; }
        public decimal? CnBalance { get; set; }
        public string DriverName { get; set; }
        public string ContractNo { get; set; }
        public DateTime? ContractDate { get; set; }
        public string ContractMiti { get; set; }
        public string ExportInvoiceNo { get; set; }
        public DateTime? ExportInvoiceDate { get; set; }
        public string ExportInvoiceMiti { get; set; }
        public string LCNumber { get; set; }
        public string CustomName { get; set; }
        public string COFDDesc { get; set; }
        public string LicenseName { get; set; }
        public int SalesInvoiceId { get; set; }

        [ForeignKey("SalesInvoiceId")]
        public SalesInvoice SalesInvoice { get; set; }
    }
}
