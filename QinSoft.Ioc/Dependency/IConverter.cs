using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Dependency
{
    public interface IConverter
    {
        object Convert(object source, IDependency dependency);
    }
}
