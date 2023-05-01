using System;
using System.Reflection;

namespace XmlTest
{
    public class EnumAttribute : Attribute
    {
        public string Title { get; set; }

        public static string GetTitle(Type tp, string name)
        {
            MemberInfo[] mi = tp.GetMember(name);
            if (mi != null && mi.Length > 0)
            {
                EnumAttribute attr = Attribute.GetCustomAttribute(mi[0], typeof(EnumAttribute)) as EnumAttribute;
                if (attr != null)
                {
                    return attr.Title;
                }
            }
            return null;
        }
        public static string GetTitle(object enm)
        {
            if (enm != null)
            {
                MemberInfo[] mi = enm.GetType().GetMember(enm.ToString());
                if (mi != null && mi.Length > 0)
                {
                    EnumAttribute attr = Attribute.GetCustomAttribute(mi[0], typeof(EnumAttribute)) as EnumAttribute;
                    if (attr != null)
                    {
                        return attr.Title;
                    }
                }
            }
            return null;
        }
    }
}
