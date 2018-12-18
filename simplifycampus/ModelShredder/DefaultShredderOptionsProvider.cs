using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModelShredder
{
    public sealed class DefaultShredderOptionsProvider : IShredderOptionsProvider
    {
        /// <summary>
        /// Provides Shredder Options for fields first and then properties according to the order of their definition. 
        /// </summary>
        /// <param name="t">The type for which to provide ShredderOptions.</param>
        /// <returns>A <see cref="ShredderOptions"/> instance containing information that controls how the type should be shredderd.</returns>
        public ShredderOptions ProvideShredderOptions(Type t)
        {
            var props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = t.GetFields(BindingFlags.Public | BindingFlags.Instance);

            List<MemberInfo> members = new List<MemberInfo>();
            members.AddRange(fields.ToList<MemberInfo>());
            members.AddRange(props.ToList<MemberInfo>());

            return new ShredderOptions(t, members);
        }
    }
}
