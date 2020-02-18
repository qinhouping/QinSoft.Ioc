using QinSoft.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QinSoft.Ioc.Container
{
    /// <summary>
    /// 依赖注入关系
    /// </summary>
    public class DependencyInjection
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConstructorInfo Constructor { get; set; }

        /// <summary>
        /// 依赖字典
        /// </summary>
        public IDictionary<object, IDependency> DependencyDictionary { get; set; }

        //public override bool Equals(object obj)
        //{
        //    if (obj is DependencyInjection)
        //    {
        //        return this.Type?.Equals((obj as DependencyInjection)?.Type) == true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public override int GetHashCode()
        //{
        //    return Type?.GetHashCode() ?? 0;
        //}

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Type, Constructor, DependencyDictionary);
        }
    }
}
