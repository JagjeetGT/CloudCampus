using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Xml.Linq;
using KRBAccounting.Service.Helpers;
using Newtonsoft.Json;

namespace System.Web.Mvc
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString AjaxAction(this HtmlHelper htmlHelper,string action)
        {

            var script = "<script>$.ajax({url: '" + action + "'," +
                           "success: function(data) {" +
                           "$('." + action + "').html(data);} });</script>";
            var sb = "<div class=" + action + "><img title=\"w_loader_gr.gif\" src=\"/Content/themes/img/loaders/w_loader_gr.gif\"  /></div>" + script;
            return MvcHtmlString.Create(sb);
        }
        public static MvcHtmlString AjaxAction(this HtmlHelper htmlHelper, string name, string action)
        {

            var script = "<script>$.ajax({url: '" + action + "'," +
                           "success: function(data) {" +
                           "$('." + name + "').html(data);} });</script>";
            var sb = "<div class=" + name + "><img title=\"w_loader_gr.gif\" src=\"/Content/themes/img/loaders/w_loader_gr.gif\"  /></div>" + script;
            return MvcHtmlString.Create(sb);
        }
        public static MvcHtmlString AjaxAction(this HtmlHelper htmlHelper, string name, string action,string controller)
        {
            controller = "/" + controller + '/';
            var script = "<script>$.ajax({url: '" + controller + action + "'," +
                           "success: function(data) {" +
                           "$('." + name + "').html(data);} });</script>";
           var sb = "<div class=" + name + "><img title=\"w_loader_gr.gif\" src=\"/Content/themes/img/loaders/w_loader_gr.gif\"  /></div>" + script;
            return MvcHtmlString.Create(sb);



        }
        public static MvcHtmlString RadioButtonForSelectList<TModel, TProperty>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TProperty>> expression,
           IEnumerable<SelectListItem> listOfValues)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sb = new StringBuilder();

            if (listOfValues != null)
            {
                // Create a radio button for each item in the list
                foreach (SelectListItem item in listOfValues)
                {
                    // Generate an id to be given to the radio button field
                    var id = string.Format("{0}_{1}", metaData.PropertyName, item.Value);

                    // Create and populate a radio button using the existing html helpers
                    var label = htmlHelper.Label(id, HttpUtility.HtmlEncode(item.Text));
                    var radio = htmlHelper.RadioButtonFor(expression, item.Value, new { id = id }).ToHtmlString();

                    // Create the html string that will be returned to the client
                    // e.g. <input data-val="true" data-val-required="You must select an option" id="TestRadio_1" name="TestRadio" type="radio" value="1" /><label for="TestRadio_1">Line1</label>
                    sb.AppendFormat("<div class=\"RadioButton\">{0}{1}</div>", radio, label);
                }
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        #region Checkboxlist

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, List<CheckBoxListInfo> listInfo)
        {
            return htmlHelper.CheckBoxList(name, listInfo,
                ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, List<CheckBoxListInfo> listInfo,
            object htmlAttributes)
        {
            return htmlHelper.CheckBoxList(name, listInfo,
                ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, List<CheckBoxListInfo> listInfo,
           IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("The argument must have a value", "name");
            if (listInfo == null)
                throw new ArgumentNullException("listInfo");
            if (listInfo.Count < 1)
                //throw new ArgumentException("The list must contain at least one value", "listInfo");
                return new MvcHtmlString("");
            StringBuilder sb = new StringBuilder();

            foreach (CheckBoxListInfo info in listInfo)
            {
                TagBuilder b = new TagBuilder("label");
                TagBuilder builder = new TagBuilder("input");
                if (info.IsChecked) builder.MergeAttribute("checked", "checked");
                builder.MergeAttributes<string, object>(htmlAttributes);
                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", info.Value);
                builder.MergeAttribute("name", name);
                builder.InnerHtml = info.DisplayText;
                b.InnerHtml = builder.ToString(TagRenderMode.Normal);
                sb.Append(b.ToString(TagRenderMode.Normal));

                sb.Append("<div class='clearfix'></div>");
            }

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        public static MvcHtmlString DateTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var mvcHtmlString = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, htmlAttributes ?? new { @class = "text-box single-line date-picker" });
            var xDoc = XDocument.Parse(mvcHtmlString.ToHtmlString());
            var xElement = xDoc.Element("input");
            if (xElement != null)
            {
                var valueAttribute = xElement.Attribute("value");
                if (!string.IsNullOrEmpty(valueAttribute.Value))
                {
                    valueAttribute.Value = DateTime.Parse(valueAttribute.Value).ToString("MM/dd/yyyy");
                    if (valueAttribute.Value == "01/01/0001")
                        valueAttribute.Value = string.Empty;
                }
            }
            return new MvcHtmlString(xDoc.ToString());
        }
        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var mvcHtmlString = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, htmlAttributes ?? new { @class = "text-box single-line date-picker" });
            var xDoc = XDocument.Parse(mvcHtmlString.ToHtmlString());
            var xElement = xDoc.Element("input");
            if (xElement != null)
            {
                var valueAttribute = xElement.Attribute("type");
                valueAttribute.Value = "file";
            }
            return new MvcHtmlString(xDoc.ToString());
        }

        public static MvcHtmlString TimeTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var mvcHtmlString = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, htmlAttributes ?? new { @class = "text-box single-line date-picker" });
            var xDoc = XDocument.Parse(mvcHtmlString.ToHtmlString());
            var xElement = xDoc.Element("input");
            if (xElement != null)
            {
                var valueAttribute = xElement.Attribute("value");
                if (valueAttribute != null)
                {
                    valueAttribute.Value = DateTime.Parse(valueAttribute.Value).ToShortTimeString();
                    if (valueAttribute.Value == "01/01/0001")
                        valueAttribute.Value = string.Empty;
                }
            }
            return new MvcHtmlString(xDoc.ToString());
        }
    }
    public static class JsonHtmlExtensions
    {
        public static MvcHtmlString Json<TModel, TObject>(this HtmlHelper<TModel> html, TObject obj)
        {
            return MvcHtmlString.Create(JsonConvert.SerializeObject(obj));
        }
    }

}