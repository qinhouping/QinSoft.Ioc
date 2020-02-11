using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc
{
    public enum ObjectContainerType
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        Singleton,
        /// <summary>
        /// 常见模式
        /// </summary>
        Normal,
        /// <summary>
        /// 自定义范围
        /// </summary>
        Scope
    }
}
