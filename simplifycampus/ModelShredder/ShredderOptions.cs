using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ModelShredder
{
    public class ShredderOptions
    {
        public Type Type { get; private set; }
        public ReadOnlyCollection<MemberInfo> Members { get; private set; }


        public ShredderOptions(Type type, IList<MemberInfo> members)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (members == null) throw new ArgumentNullException("members");
            
            foreach (MemberInfo member in members)
            {
                if (member.MemberType != MemberTypes.Property && member.MemberType != MemberTypes.Field)
                    throw new ArgumentException("May only contan Field and Property Infos.", "members");
            }

            Type = type;
            Members = new ReadOnlyCollection<MemberInfo>(members);
        }
    }
}