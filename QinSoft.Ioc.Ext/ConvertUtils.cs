using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Ext
{
    public static class ConvertUtils
    {
        /// <summary>
        /// 将字符串转为指定 Type 值
        /// </summary>
        /// <param name="Arg"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object StringConvert(this string Arg, Type type)
        {
            if (type == typeof(Int16))
            {
                return Convert.ToInt16(Arg);
            }
            else if (type == typeof(Int32))
            {
                return Convert.ToInt32(Arg);
            }
            else if (type == typeof(Int64))
            {
                return Convert.ToInt64(Arg);
            }
            else if (type == typeof(Single))
            {
                return Convert.ToSingle(Arg);
            }
            else if (type == typeof(Double))
            {
                return Convert.ToDouble(Arg);
            }
            else if (type == typeof(Guid))
            {
                return Guid.Parse(Arg);
            }
            else if (type == typeof(String))
            {
                return Arg;
            }
            else if (type.IsEnum)
            {
                return Enum.Parse(type, Arg);
            }
            else
            {
                throw new NotSupportedException(type.FullName);
            }
        }
    }
}
