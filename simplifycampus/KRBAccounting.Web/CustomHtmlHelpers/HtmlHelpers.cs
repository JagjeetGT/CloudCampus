using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    public static class HtmlHelpers
    {
        
        public static MvcHtmlString BreadcrumbLink(
        this HtmlHelper htmlHelper,
        string linkText,
        string actionName,
        string controllerName
        )
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            if (actionName == currentAction)
            {


                return htmlHelper.ActionLink(
                    linkText,
                    actionName,
                    controllerName,
                    null,
                    new
                    {
                        @class = "active"
                    });

            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName);
        }


        public static MvcHtmlString MenuRouteLink(
        this HtmlHelper htmlHelper,
        string linkText,
        string routeName
        )
        {
            var currentRoute = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            if (routeName == currentRoute || (routeName == "VideoArchivesMenu" && currentRoute == "VideoArchives"))
            {
                return htmlHelper.RouteLink(
                    linkText,
                    routeName,
                    null,
                    new
                    {
                        @class = "active"
                    });
            }
            return htmlHelper.RouteLink(linkText, routeName);
        }

        public static MvcHtmlString WellnessRouteLink(
       this HtmlHelper htmlHelper,
       string linkText,
       string routeName
       )
        {
            var currentRoute = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            if (routeName == currentRoute)
            {
                return htmlHelper.RouteLink(
                    linkText,
                    routeName,
                    null,
                    new
                    {
                        @class = "active"
                    });
            }
            return htmlHelper.RouteLink(linkText, routeName);
        }


        public static MvcHtmlString MenuRouteLinkNew(
      this HtmlHelper htmlHelper,
      string linkText,
      string routeName,
            object id
      )
        {
            var currentRoute = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            if (routeName == currentRoute)
            {
                return htmlHelper.RouteLink(
                    linkText,
                    routeName,
                    id,
                    new
                    {
                        @class = "active"
                    });
            }
            //  id = "";
            return htmlHelper.RouteLink(linkText, routeName, id);
        }
        public static bool IsHomePage(this  HtmlHelper htmlHelper)
        {
            var currentRoute = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            return currentRoute == "Index";
        }
        public static MvcHtmlString DropdownListForCustom<TModel, TProperty>(
this HtmlHelper<TModel> htmlHelper,
Expression<Func<TModel, TProperty>> expression,
IEnumerable<SelectListItem> listOfValues, object htmlAttributes = null)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            List<SelectListItem> newValues = new List<SelectListItem>();

            foreach (SelectListItem item in listOfValues)
            {

                if (metaData.Model != null && item.Value == metaData.Model.ToString())
                {
                    item.Selected = true;
                }
                newValues.Add(item);
            }
            return htmlHelper.DropDownListFor(expression, newValues, htmlAttributes);

        }
    }

}