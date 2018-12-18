using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.Helpers
{
    public class Utility
    {
        public static string TrimText(string text, int length)
        {
            var trimmedText = string.Empty;
            if (text.Length <= length)
            {
                trimmedText = HttpUtility.HtmlDecode(text.ToString());
                return trimmedText;
            }
            trimmedText = HttpUtility.HtmlDecode(text.Substring(0, length));
            trimmedText = trimmedText + "...";
            return trimmedText;
        }
    }
}