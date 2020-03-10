using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QinSoft.Ioc.Dependency;
using QinSoft.Ioc.Converter;
using QinSoft.Ioc.Scaner;

namespace QinSoft.Ioc.Attribute
{
    /// <summary>
    /// 依赖特性基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public abstract class DependencyAttribute : System.Attribute
    {
        /// <summary>
        /// 依赖注入扫描者
        /// </summary>
        public DependencyInjectionScaner DependencyInjectionScaner { get; internal set; }

        /// <summary>
        /// 获取依赖
        /// </summary>
        /// <returns>依赖</returns>
        public abstract IDependency GetDependency();
    }
}
