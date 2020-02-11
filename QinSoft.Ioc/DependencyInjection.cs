using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QinSoft.Ioc
{
    /// <summary>
    /// 依赖注入
    /// 用于构建依赖关系
    /// </summary>
    public class DependencyInjection
    {
        /// <summary>
        /// 对象类型
        /// </summary>
        public Type Type
        {
            get
            {
                return Constructor.DeclaringType;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConstructorInfo Constructor { get; set; }

        /// <summary>
        /// 构造函数依赖
        /// </summary>
        public IDictionary<ParameterInfo, DependencyInjection> ConstructInjections { get; set; }

        /// <summary>
        /// 属性依赖
        /// </summary>
        public IDictionary<PropertyInfo, DependencyInjection> PropertyInjections { get; set; }
    }
}
