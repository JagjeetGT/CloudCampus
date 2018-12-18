using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ModelShredder
{
    public interface IObjectShredder
    {
        ShredderDelegate GetShredderMethod(ShredderOptions options);
    }
}
         