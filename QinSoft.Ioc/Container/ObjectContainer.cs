using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Container
{
    public abstract class ObjectContainer
    {
        public abstract object Get(Type type);

        public abstract bool Remove(Type type);

        public abstract bool Remove(object instance);

        public abstract void Clear();

        public abstract bool Exists(Type type);

        public abstract bool Exists(object instance);
    }
}
