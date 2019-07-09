using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Ioc.Core
{
    /// <summary>
    /// 对象创建类型
    /// </summary>
    public enum ObjectCreateType
    {
        /// <summary>
        /// 普通模式 每次会重新生成对象
        /// </summary>
        Normal,
        /// <summary>
        /// 单例模式 会使用单例模式对象
        /// </summary>
        Sington
    }
}
