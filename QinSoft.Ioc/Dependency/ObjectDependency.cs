using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Dependency
{
    public class ObjectDependency : DependencyBase
    {
        public object Arg { get; protected set; }
        public ObjectDependency(object arg, IConverter converter) : base(converter)
        {
            this.Arg = arg;
        }

        public ObjectDependency(object arg) : base()
        {
            this.Arg = arg;
        }

        public override object GetSource()
        {
            return Arg;
        }
    }
}
