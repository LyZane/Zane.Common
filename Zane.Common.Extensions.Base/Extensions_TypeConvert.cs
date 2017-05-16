using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zane.Common.Extensions.Base
{
    public static class Extensions_TypeConvert
    {
        public static long ToInt64(this object obj)
        {
            return Convert.ToInt64(obj);
        }

        public static int ToInt32(this object obj)
        {
            return Convert.ToInt32(obj);
        }

        public static double ToDouble(this object obj)
        {
            return Convert.ToDouble(obj);
        }

        public static char ToChar(this object obj)
        {
            return Convert.ToChar(obj);
        }
        internal static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        public static object ChangeType(this object obj, Type t)
        {
            var value = obj;
            if (value is DBNull)
            {
                value = null;
            }
            if (t.IsNullableType() && value ==null)
            {
                return null;
            }
            //int? ==> int
            var temp =  Nullable.GetUnderlyingType(t);
            t = temp == null ? t : temp;

            if (t == typeof(DateTime))//DateTime务必在ValueType前面
            {
                return Convert.ToDateTime(value);
            }
            else if (t.IsEnum)//enum务必在ValueType前面
            {
                if (value == null)
                {
                    value = 0;
                }
                return Enum.Parse(t, value.ToString());
            }
            else if (t.IsValueType)
            {
                if (value == null)
                {
                    value = 0;
                }
                return Convert.ChangeType(value, t);
            }
            else if (t == typeof(string))
            {
                if (value == null)
                {
                    return null;
                }
                return value.ToString();
            }
            else if (t == typeof(Uri))
            {
                if (value==null|| value.ToString()=="")
                {
                    return null;
                }
                return new Uri(value.ToString());
            }
            return null;
        }
    }
}
