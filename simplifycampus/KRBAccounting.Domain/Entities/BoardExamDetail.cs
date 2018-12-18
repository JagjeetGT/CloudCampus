
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class BoardExamDetail
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int BoardExamMasterId { get; set; }
        public decimal? PassPercentage { get; set; }
        public decimal? MaskObtained_Y1 { get; set; }
        public decimal? MaskObtained_Y2 { get; set; }
        public decimal? MaskObtained_Y3{ get; set; }
        public decimal? MaskObtained_Y4 { get; set; }
        public decimal? MaskObtained_Y5 { get; set; }
        public decimal? MaskObtained_Y6 { get; set; }
        public decimal? MaskObtained_Y7 { get; set; }
        public string Result_Y1 { get; set; }
        public string Result_Y2 { get; set; }
        public string Result_Y3 { get; set; }
        public string Result_Y4 { get; set; }
        public string Result_Y5 { get; set; }
        public string Result_Y6 { get; set; }
        public string Result_Y7 { get; set; }
        public string SymbolNo_Y1 { get; set; }
        public string SymbolNo_Y2 { get; set; }
        public string SymbolNo_Y3 { get; set; }
        public string SymbolNo_Y4 { get; set; }
        public string SymbolNo_Y5 { get; set; }
        public string SymbolNo_Y6 { get; set; }
        public string SymbolNo_Y7 { get; set; }
        public string Remarks { get; set; }
        [NotMapped]
        public string ClassName { get; set; }
        [NotMapped]
        public string SubjectName { get; set; }
      
        
}
}
