using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Sms
{
    public class ScSmsGroupViewModel : BaseViewModel
    {
        public int SmsGroupId { get; set; }
        public string SmsText { get; set; }
        public int TemplateId { get; set; }
        public SelectList TemplateList { get; set; }
    }
}