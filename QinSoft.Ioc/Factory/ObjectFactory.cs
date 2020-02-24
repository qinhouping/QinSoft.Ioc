using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QinSoft.Ioc.Factory
{
    /// <summary>
    /// 对象工厂
    /// </summary>
    public abstract class ObjectFactory
    {
        /// <summary>
        /// 根据构造函数创建实例
        /// </summary>
        /// <param name="constructor">构造函数</param>
        /// <param name="args">参数</param>
        /// <returns>对象实例</returns>
        public abstract object CreateInstance(ConstructorInfo constructor, params object[] args);

        /// <summary>
        /// 根据参数创建实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="args">参数</param>
        /// <returns>对象实例</returns>
        public abstract object CreateInstance(Type type, params object[] args);
    }
}
