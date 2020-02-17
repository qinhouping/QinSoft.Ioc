using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Container
{
    public abstract class DependencyInjectionScaner
    {
        public virtual ObjectContainer ObjectContainer { get; set; }

        public abstract DependencyInjection[] Scan();
    }
}
