using QinSoft.Ioc.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc
{
    /// <summary>
    /// Ioc对象工厂扩展类
    /// </summary>
    public static class IocObjectFactoryExtends
    {
        /// <summary>
        /// 根据参数创建实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="args">参数</param>
        /// <returns>对象实例</returns>
        public static T CreateInstance<T>(this ObjectFactory objectFactory, params object[] args) where T : class
        {
            Type type = typeof(T);
            return objectFactory.CreateInstance(type, args) as T;
        }
    }
}
