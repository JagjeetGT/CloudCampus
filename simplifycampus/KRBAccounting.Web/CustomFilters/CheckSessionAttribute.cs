using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KRBAccounting.Web.CustomFilters
{
    public class CheckSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            HttpContext httpcontext = HttpContext.Current;

            //if Session(user) is null, redirect to Login page
            if (!httpcontext.Request.IsAuthenticated)
            {
                Logon(filterContext);
            }
        }

        protected void Logon(ActionExecutingContext filterContext)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary(
                new
                {
                    controller = "Account",
                    action = "LogOn"
                });
            filterContext.Result = new RedirectToRouteResult(dictionary);
        }
    }
}