﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QinSoft.Ioc.Container;

namespace QinSoft.Ioc.Dependency
{
    public class ContainerObjectDependency : DependencyBase
    {
        public ObjectContainer Container { get; protected set; }

        public Type Type { get; protected set; }

        public ContainerObjectDependency(ObjectContainer container, Type type, IConverter converter) : base(converter)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (type == null) throw new ArgumentNullException("type");
            this.Container = container;
            this.Type = type;
        }
        public ContainerObjectDependency(ObjectContainer container, Type type) : base()
        {
            if (container == null) throw new ArgumentNullException("container");
            if (type == null) throw new ArgumentNullException("type");
            this.Container = container;
            this.Type = type;
        }

        public override object GetSource()
        {
            return this.Container.Get(Type);
        }
    }
}