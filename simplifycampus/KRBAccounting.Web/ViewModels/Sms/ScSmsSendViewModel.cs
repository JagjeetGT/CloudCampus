using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Sms
{
    public class ScSmsSendViewModel : BaseViewModel
    {
        public SmsGroup SmsGroup { get; set; }
        public string SmsText { get; set; }
        public string MobileNumbers { get; set; }
    }
}