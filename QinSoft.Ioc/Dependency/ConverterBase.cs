using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Dependency
{
    public class ConverterBase : IConverter
    {
        public virtual object Convert(object source, IDependency dependency)
        {
            return source;
        }
    }
}
