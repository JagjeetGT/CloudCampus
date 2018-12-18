using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class SMSSettings{        [Key]        public int Id {get;set;}        public string UserName {get;set;}        public string Password {get;set;}        public bool AttendanceSMS {get;set;}        public bool EventsSMS {get;set;}        public bool BirthdaySMS {get;set;}        public bool ExamResultScheduleSMS {get;set;}    }}