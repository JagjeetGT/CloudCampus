using System;
using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    public static class HtmlPrefixScopeExtensions
    {
        //private const string idsToReuseKey = "__htmlPrefixScopeExtensions_IdsToReuse_";

        //public static IDisposable BeginCollectionItem(this HtmlHelper html, string collectionName)
        //{
        //    var idsToReuse = GetIdsToReuse(html.ViewContext.HttpContext, collectionName);
        //    string itemIndex = idsToReuse.Count > 0 ? idsToReuse.Dequeue() : Guid.NewGuid().ToString();

        //    // autocomplete="off" is needed to work around a very annoying Chrome behaviour whereby it reuses old values after the user clicks "Back", which causes the xyz.index and xyz[...] values to get out of sync.
        //    html.ViewContext.Writer.WriteLine(string.Format("<input type=\"hidden\" name=\"{0}.index\" autocomplete=\"off\" value=\"{1}\" />", collectionName, itemIndex)); 

        //    return BeginHtmlFieldPrefixScope(html, string.Format("{0}[{1}]", collectionName, itemIndex));
        //}

        //public static IDisposable BeginHtmlFieldPrefixScope(this HtmlHelper html, string htmlFieldPrefix)
        //{
        //    return new HtmlFieldPrefixScope(html.ViewData.TemplateInfo, htmlFieldPrefix);
        //}

        //private static Queue<string> GetIdsToReuse(HttpContextBase httpContext, string collectionName)
        //{
        //    // We need to use the same sequence of IDs following a server-side validation failure,  
        //    // otherwise the framework won't render the validation error messages next to each item.
        //    string key = idsToReuseKey + collectionName;
        //    var queue = (Queue<string>)httpContext.Items[key];
        //    if (queue == null) {
        //        httpContext.Items[key] = queue = new Queue<string>();
        //        var previouslyUsedIds = httpContext.Request[collectionName + ".index"];
        //        if (!string.IsNullOrEmpty(previouslyUsedIds))
        //            foreach (string previouslyUsedId in previouslyUsedIds.Split(','))
        //                queue.Enqueue(previouslyUsedId);
        //    }
        //    return queue;
        //}

        ////private const string JQueryTemplatingEnabledKey = "__BeginCollectionItem_jQuery";

        //public static MvcHtmlString CollectionItemJQueryTemplate<TModel, TCollectionItem>(this HtmlHelper<TModel> html,
        //                                                                                    string partialViewName,
        //                                                                                    TCollectionItem modelDefaultValues)
        //{
        //    ViewDataDictionary<TCollectionItem> viewData = new ViewDataDictionary<TCollectionItem>(modelDefaultValues);
        //    viewData.Add(idsToReuseKey, true);
        //    return html.Partial(partialViewName, modelDefaultValues, viewData);
        //}

        //private class HtmlFieldPrefixScope : IDisposable
        //{
        //    private readonly TemplateInfo templateInfo;
        //    private readonly string previousHtmlFieldPrefix;

        //    public HtmlFieldPrefixScope(TemplateInfo templateInfo, string htmlFieldPrefix)
        //    {
        //        this.templateInfo = templateInfo;

        //        previousHtmlFieldPrefix = templateInfo.HtmlFieldPrefix;
        //        templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
        //    }

        //    public void Dispose()
        //    {
        //        templateInfo.HtmlFieldPrefix = previousHtmlFieldPrefix;
        //    }
        //}



        /// <summary>
        /// Begins a collection item by inserting either a previously used .Index hidden field value for it or a new one.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="html"></param>
        /// <param name="collectionName">The name of the collection property from the Model that owns this item.</param>
        /// <returns></returns>
        public static IDisposable BeginCollectionItem<TModel>(this HtmlHelper<TModel> html, string collectionName)
        {
            if (String.IsNullOrEmpty(collectionName))
                throw new ArgumentException("collectionName is null or empty.", "collectionName");

            string collectionIndexFieldName = String.Format("{0}.Index", collectionName);

            string itemIndex = null;
            if (html.ViewData.ContainsKey(JQueryTemplatingEnabledKey))
            {
                itemIndex = "${index}";
            }
            else
            {
                itemIndex = GetCollectionItemIndex(collectionIndexFieldName);
            }

            string collectionItemName = String.Format("{0}[{1}]", collectionName, itemIndex);

            TagBuilder indexField = new TagBuilder("input");
            indexField.MergeAttributes(new Dictionary<string, string>() {
                { "name", collectionIndexFieldName },
                { "value", itemIndex },
                { "type", "hidden" },
                { "autocomplete", "off" },
                {"class","collection_index"}
            });

            html.ViewContext.Writer.WriteLine(indexField.ToString(TagRenderMode.SelfClosing));
            return new CollectionItemNamePrefixScope(html.ViewData.TemplateInfo, collectionItemName);
        }

        private const string JQueryTemplatingEnabledKey = "__BeginCollectionItem_jQuery";

        public static MvcHtmlString CollectionItemJQueryTemplate<TModel, TCollectionItem>(this HtmlHelper<TModel> html,
                                                                                            string partialViewName,
                                                                                            TCollectionItem modelDefaultValues)
        {
            ViewDataDictionary<TCollectionItem> viewData = new ViewDataDictionary<TCollectionItem>(modelDefaultValues);
            viewData.Add(JQueryTemplatingEnabledKey, true);
            var partial = html.Partial(partialViewName, modelDefaultValues, viewData).ToString();
            partial = partial.Replace("___index___", "___${index}___");
            return new MvcHtmlString(partial);
            //return html.Partial(partialViewName, modelDefaultValues, viewData);
        }

        /// <summary>
        /// Tries to reuse old .Index values from the HttpRequest in order to keep the ModelState consistent
        /// across requests. If none are left returns a new one.
        /// </summary>
        /// <param name="collectionIndexFieldName"></param>
        /// <returns>a GUID string</returns>
        private static string GetCollectionItemIndex(string collectionIndexFieldName)
        {
            Queue<string> previousIndices = (Queue<string>)HttpContext.Current.Items[collectionIndexFieldName];
            if (previousIndices == null)
            {
                HttpContext.Current.Items[collectionIndexFieldName] = previousIndices = new Queue<string>();

                string previousIndicesValues = HttpContext.Current.Request[collectionIndexFieldName];
                if (!String.IsNullOrWhiteSpace(previousIndicesValues))
                {
                    foreach (string index in previousIndicesValues.Split(','))
                        previousIndices.Enqueue(index);
                }
            }

            return previousIndices.Count > 0 ? previousIndices.Dequeue() : Guid.NewGuid().ToString();
        }

        private class CollectionItemNamePrefixScope : IDisposable
        {
            private readonly TemplateInfo _templateInfo;
            private readonly string _previousPrefix;

            public CollectionItemNamePrefixScope(TemplateInfo templateInfo, string collectionItemName)
            {
                this._templateInfo = templateInfo;

                _previousPrefix = templateInfo.HtmlFieldPrefix;
                templateInfo.HtmlFieldPrefix = collectionItemName;
            }

            public void Dispose()
            {
                _templateInfo.HtmlFieldPrefix = _previousPrefix;
            }
        }
    }
}

//namespace System.Web.Mvc
//{
//    public static class HtmlPrefixScopeExtensions
//    {
//        private const string idsToReuseKey = "__htmlPrefixScopeExtensions_IdsToReuse_";

//        public static IDisposable BeginCollectionItem(this HtmlHelper html, string collectionName)
//        {
//            var idsToReuse = GetIdsToReuse(html.ViewContext.HttpContext, collectionName);
//            string itemIndex = idsToReuse.Count > 0 ? idsToReuse.Dequeue() : Guid.NewGuid().ToString();

//            // autocomplete="off" is needed to work around a very annoying Chrome behaviour whereby it reuses old values after the user clicks "Back", which causes the xyz.index and xyz[...] values to get out of sync.
//            html.ViewContext.Writer.WriteLine(string.Format("<input type=\"hidden\" name=\"{0}.index\" autocomplete=\"off\" value=\"{1}\" />", collectionName, itemIndex)); 

//            return BeginHtmlFieldPrefixScope(html, string.Format("{0}[{1}]", collectionName, itemIndex));
//        }

//        public static IDisposable BeginHtmlFieldPrefixScope(this HtmlHelper html, string htmlFieldPrefix)
//        {
//            return new HtmlFieldPrefixScope(html.ViewData.TemplateInfo, htmlFieldPrefix);
//        }

//        private static Queue<string> GetIdsToReuse(HttpContextBase httpContext, string collectionName)
//        {
//            // We need to use the same sequence of IDs following a server-side validation failure,  
//            // otherwise the framework won't render the validation error messages next to each item.
//            string key = idsToReuseKey + collectionName;
//            var queue = (Queue<string>)httpContext.Items[key];
//            if (queue == null) {
//                httpContext.Items[key] = queue = new Queue<string>();
//                var previouslyUsedIds = httpContext.Request[collectionName + ".index"];
//                if (!string.IsNullOrEmpty(previouslyUsedIds))
//                    foreach (string previouslyUsedId in previouslyUsedIds.Split(','))
//                        queue.Enqueue(previouslyUsedId);
//            }
//            return queue;
//        }

//        private class HtmlFieldPrefixScope : IDisposable
//        {
//            private readonly TemplateInfo templateInfo;
//            private readonly string previousHtmlFieldPrefix;

//            public HtmlFieldPrefixScope(TemplateInfo templateInfo, string htmlFieldPrefix)
//            {
//                this.templateInfo = templateInfo;

//                previousHtmlFieldPrefix = templateInfo.HtmlFieldPrefix;
//                templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
//            }

//            public void Dispose()
//            {
//                templateInfo.HtmlFieldPrefix = previousHtmlFieldPrefix;
//            }
//        }
//    }
//}