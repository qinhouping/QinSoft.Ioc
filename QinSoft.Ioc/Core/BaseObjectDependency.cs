using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Ioc.Core
{
    /// <summary>
    /// 对象依赖
    /// </summary>
    public class BaseObjectDependency
    {
        /// <summary>
        /// 对象类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 对象创建方式
        /// </summary>
        public ObjectCreateType CreateType { get; set; }

        /// <summary>
        /// 引用类型 构造函数参数依赖
        /// </summary>
        public IList<BasePropertyDependency> ConstructDependency { get; set; }

        /// <summary>
        /// 引用类型 属性依赖
        /// </summary>
        public IList<BasePropertyDependency> PropertyDependency { get; set; }

        /// <summary>
        /// 值类型 值
        /// </summary>
        public System.ValueType Value { get; set; }

        /// <summary>
        /// 通过名称获取构造函数类型
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public virtual Type GetConstructPropertyType(string Name)
        {
            if (ConstructDependency == null) return null;
            foreach (BasePropertyDependency dependency in ConstructDependency)
            {
                if (dependency.Name == Name)
                {
                    return dependency.Value?.Type;
                }
            }
            return null;

        }

        /// <summary>
        /// 判断是否为值类型
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValueType()
        {
            return this.Type.IsValueType;
        }

        public override string ToString()
        {
            return Type?.FullName + "\r\n" + CreateType;
        }
    }
}
