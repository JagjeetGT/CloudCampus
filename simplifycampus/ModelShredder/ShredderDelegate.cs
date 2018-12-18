using System;
using System.Collections.Generic;
using System.Text;

namespace ModelShredder
{
    /// <summary>
    /// Shreds an Object.
    /// </summary>
    /// <param name="obj">The object to Shred.</param>
    /// <returns>An object Array, representing the shredded object.</returns>
    public delegate object[] ShredderDelegate(object obj);
}
