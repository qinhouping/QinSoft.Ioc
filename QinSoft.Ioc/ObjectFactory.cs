using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc
{
    /// <summary>
    /// 对象工厂
    /// </summary>
    public abstract class ObjectFactory
    {
        /// <summary>
        /// 通过依赖注入创建对象
        /// </summary>
        /// <param name="dependencyInjection"></param>
        /// <returns></returns>
        public abstract object CreateObject(DependencyInjection dependencyInjection);
    }
}
