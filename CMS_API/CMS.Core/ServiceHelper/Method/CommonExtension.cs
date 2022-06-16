
using CMS.Core.Attribute;
using System;
using System.Reflection;

namespace CMS.Core.ServiceHelper.ExtensionMethod
{
    public static class CommonExtension
    {

        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public static dynamic ToTimeSpanValue(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    return TimeSpan.Parse(value);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}
