using QinSoft.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Converter
{
    /// <summary>
    /// 指定类型转换器
    /// </summary>
    public class TypeConverter : ConverterBase
    {
        /// <summary>
        /// 指定转换类型
        /// </summary>
        public Type ConvertType { get; protected set; }

        public TypeConverter(Type convertType)
        {
            if (convertType == null) throw new ArgumentNullException("convertType");
            this.ConvertType = convertType;
        }

        public override object Convert(object source, IDependency dependency)
        {
            return System.Convert.ChangeType(source, ConvertType);
        }
    }
}
