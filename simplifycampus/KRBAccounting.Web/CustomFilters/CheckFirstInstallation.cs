using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Web.Helpers;

namespace KRBAccounting.Web.CustomFilters
{
    public class CheckFirstInstallation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var firstInstall = WebConfigHelper.ReadValue("FirstInstall");
            if (firstInstall != null)
            {
                if (firstInstall == "1")
                {
                    filterContext.Result = new RedirectResult("~/Install");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Install");
            }

        }
    }
}