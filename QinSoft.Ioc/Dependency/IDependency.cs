using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Dependency
{
    public abstract class IDependency
    {
        public abstract object GetValue();
    }
}
