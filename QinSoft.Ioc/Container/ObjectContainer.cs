using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Container
{
    /// <summary>
    /// 对象容器抽象类
    /// </summary>
    public abstract class ObjectContainer
    {
        /// <summary>
        /// 从容器中获取指定类型对象
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>对象实例</returns>
        public abstract object Get(Type type);

        /// <summary>
        /// 移除指定类型对象
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>移除结果</returns>
        public abstract bool Remove(Type type);

        /// <summary>
        /// 移除实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>移除结果</returns>
        public abstract bool Remove(object instance);

        /// <summary>
        /// 清除对象缓存
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// 判断指定类型实例是否存在
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>是否存在</returns>
        public abstract bool Exists(Type type);

        /// <summary>
        /// 判断实例是否存在
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>是否存在</returns>
        public abstract bool Exists(object instance);

        /// <summary>
        /// 刷新依赖注入
        /// </summary>
        public abstract void RefreshDependencyInjections();
    }
}
