using QinSoft.Ioc.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc
{
    /// <summary>
    /// Ioc对象容器拓展类
    /// </summary>
    public static class IocObjectContainerExtends
    {
        /// <summary>
        /// 从对象容器中获取指定类型实例
        /// </summary>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <param name="objectContainer">对象容器</param>
        /// <returns>对象实例</returns>
        public static T Get<T>(this ObjectContainer objectContainer) where T : class
        {
            if (objectContainer == null) throw new ArgumentNullException("objectContainer");
            return objectContainer.Get(typeof(T)) as T;
        }

        /// <summary>
        /// 从对象容器中移除指定类型实例
        /// </summary>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <param name="objectContainer">对象容器</param>
        /// <returns>对象实例</returns>
        public static bool Remove<T>(this ObjectContainer objectContainer) where T : class
        {
            if (objectContainer == null) throw new ArgumentNullException("objectContainer");
            return objectContainer.Remove(typeof(T));
        }

        /// <summary>
        /// 判断指定类型实例是否在容器中存在
        /// </summary>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <param name="objectContainer">对象容器</param>
        /// <returns>是否存在</returns>
        public static bool Exists<T>(this ObjectContainer objectContainer) where T : class
        {
            if (objectContainer == null) throw new ArgumentNullException("objectContainer");
            return objectContainer.Exists(typeof(T));
        }
    }
}
