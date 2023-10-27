using CMS.Core.Attribute;
using System;
using System.Reflection;

namespace CMS.Core.ServiceHelper.Method
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
                return !string.IsNullOrEmpty(value) ? TimeSpan.Parse(value) : (dynamic)null;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public static long ToLongValue(this string value)
        {
            try
            {
                return long.Parse(value);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
