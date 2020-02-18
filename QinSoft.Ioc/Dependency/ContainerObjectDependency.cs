using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QinSoft.Ioc.Container;
using QinSoft.Ioc.Converter;

namespace QinSoft.Ioc.Dependency
{
    /// <summary>
    /// 从Ioc容器中获取依赖内容
    /// </summary>
    public class ContainerObjectDependency : DependencyBase
    {
        /// <summary>
        /// 对象容器
        /// </summary>
        public ObjectContainer Container { get; protected set; }

        /// <summary>
        /// 依赖类型
        /// </summary>
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
