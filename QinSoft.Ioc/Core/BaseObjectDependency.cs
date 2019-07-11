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
        /// 引用类型 Null
        /// </summary>
        public bool IsNull { get; set; }

        /// <summary>
        /// 引用类型 构造函数参数依赖
        /// </summary>
        public IList<BasePropertyDependency> ConstructDependencies { get; set; }

        /// <summary>
        /// 引用类型 属性依赖
        /// </summary>
        public IList<BasePropertyDependency> PropertyDependencies { get; set; }


        /// <summary>
        /// 值类型 Value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 通过名称获取构造函数属性类型
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public virtual BaseObjectDependency GetConstructPropertyObject(string Name)
        {
            if (ConstructDependencies == null) return null;
            foreach (BasePropertyDependency dependency in ConstructDependencies)
            {
                if (dependency.Name == Name)
                {
                    return dependency.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 判断是否为值类型
        /// 包括String Guid Enum类型
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValueType()
        {
            if (this.Type == null) throw new InvalidOperationException("Type Is Null");
            return this.Type?.IsValueType == true || this.Type == typeof(string) || this.Type == typeof(Guid) || this.Type?.IsEnum==true;
        }

        public override string ToString()
        {
            return Type?.FullName + " " + CreateType;
        }
    }
}
