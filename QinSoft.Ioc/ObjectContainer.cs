using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc
{
    public abstract class ObjectContainer
    {
        public abstract object Get(DependencyInjection dependencyInjection);

        public abstract void Refresh();
    }
}
