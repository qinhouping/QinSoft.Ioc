using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Dependency
{
    /// <summary>
    /// 依赖接口
    /// </summary>
    public abstract class IDependency
    {
        /// <summary>
        /// 获取依赖值
        /// </summary>
        /// <returns></returns>
        public abstract object GetValue();
    }
}
