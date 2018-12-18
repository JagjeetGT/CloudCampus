using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ModelShredder;

namespace KRBAccounting.Web.Extensions
{
    public static class IEnumerableExtensions
    {
        public static DataTable ToDataTable(this IEnumerable source)
        {
            Shredder ms = new Shredder(new DefaultShredderOptionsProvider(), new InjectionObjectShredder(), new DefaultSchemaBuilder());

            return ms.Shred(source);
        }
    }
}
