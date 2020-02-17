using QinSoft.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QinSoft.Ioc.Container
{
    public class DependencyInjection
    {
        public Type Type { get; set; }

        public ConstructorInfo Constructor { get; set; }

        public IDictionary<object, IDependency> DependencyDictionary { get; set; }
    }
}
