using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Ioc.Core
{
    /// <summary>
    /// 对象实例映射实现
    /// </summary>
    public class ObjectInstanceMapper : IObjectInstanceMapper
    {
        protected IDictionary<BaseObjectDependency, object> SingtonObjectDict { get; set; } = new Dictionary<BaseObjectDependency, object>();

        /// <summary>
        /// 增加映射
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool Add(BaseObjectDependency dependency, object obj)
        {
            if (Exists(dependency))
            {
                return false;
            }
            else
            {
                this.SingtonObjectDict.Add(dependency, obj);
                return true;
            }
        }

        /// <summary>
        /// 判断映射是否存在
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public virtual bool Exists(BaseObjectDependency dependency)
        {
            return this.SingtonObjectDict.ContainsKey(dependency);
        }

        /// <summary>
        /// 返回映射结果
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public virtual object Find(BaseObjectDependency dependency)
        {
            if (!Exists(dependency))
            {
                return null;
            }
            else
            {
                return this.SingtonObjectDict[dependency];
            }
        }

        /// <summary>
        /// 移除映射
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public virtual bool Remove(BaseObjectDependency dependency)
        {
            if (!Exists(dependency))
            {
                return false;
            }
            else
            {
                return this.SingtonObjectDict.Remove(dependency);
            }
        }
    }
}
