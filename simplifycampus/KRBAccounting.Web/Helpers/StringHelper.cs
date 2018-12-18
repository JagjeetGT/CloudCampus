using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace KRBAccounting.Web.Helpers
{
    public static class StringHelper
    {
        public static string Compact(this string str)
        {
            StringBuilder result = new StringBuilder();
            string[] stringSplit = str.Split(' ');

            foreach (var s in stringSplit)
            {
                result.Append(s.Trim());
            }

            return result.ToString();
        }
    }
}