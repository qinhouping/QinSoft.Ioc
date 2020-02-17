using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QinSoft.Ioc.Dependency;

namespace QinSoft.Ioc.Container
{
    public abstract class DependencyAttribute : Attribute
    {
        public abstract IDependency GetDependency();
    }
}
