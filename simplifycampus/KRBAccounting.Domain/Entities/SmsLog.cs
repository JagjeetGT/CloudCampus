using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class SmsLog{        [Key]        public int Id {get;set;}        public int ReferenceId {get;set;}        public int ReferenceType {get;set;}        public string SentToNumber {get;set;}        public int SentFromId {get;set;}        public string Body {get;set;}        public string Footer {get;set;}        public string Title {get;set;}        public bool IsSent {get;set;}        public DateTime SendDate {get;set;}    }}