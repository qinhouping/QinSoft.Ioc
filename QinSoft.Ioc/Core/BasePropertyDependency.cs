using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Ioc.Core
{
    /// <summary>
    /// 属性依赖
    /// </summary>
    public class BasePropertyDependency
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public BaseObjectDependency Value { get; set; }

        public override string ToString()
        {
            return Name + " " + (Value?.ToString());
        }
    }
}
