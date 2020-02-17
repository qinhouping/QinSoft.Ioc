using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Dependency
{
    public class TypeConverter : ConverterBase
    {
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
