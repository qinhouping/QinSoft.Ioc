using QinSoft.Ioc.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Scaner
{
    /// <summary>
    /// 依赖扫描
    /// </summary>
    public abstract class DependencyInjectionScaner
    {
        /// <summary>
        /// 对象容器
        /// </summary>
        public virtual ObjectContainer ObjectContainer { get; set; }

        /// <summary>
        /// 扫描依赖注入
        /// </summary>
        /// <returns></returns>
        public abstract DependencyInjection[] Scan();
    }
}
