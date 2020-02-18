using QinSoft.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Attribute
{
    /// <summary>
    /// 组件依赖
    /// </summary>
    public class ComponentDependencyAttribute : DependencyAttribute
    {
        /// <summary>
        /// 指定类型
        /// </summary>
        public Type Type { get; protected set; }

        public ComponentDependencyAttribute(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            this.Type = type;
        }
        public override IDependency GetDependency()
        {
            return new ContainerObjectDependency(this.DependencyInjectionScaner.ObjectContainer, Type);
        }
    }
}
