using QinSoft.Ioc.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Dependency
{
    /// <summary>
    /// 简单对象依赖
    /// </summary>
    public class ObjectDependency : DependencyBase
    {
        /// <summary>
        /// 对象参数
        /// </summary>
        public object Arg { get; protected set; }
        public ObjectDependency(object arg, IConverter converter) : base(converter)
        {
            this.Arg = arg;
        }

        public ObjectDependency(object arg) : base()
        {
            this.Arg = arg;
        }

        public override object GetSource()
        {
            return Arg;
        }
    }
}
