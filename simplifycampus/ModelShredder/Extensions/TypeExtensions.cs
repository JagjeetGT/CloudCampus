using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelShredder.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNullable(this Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

    }
}
