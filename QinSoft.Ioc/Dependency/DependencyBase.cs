using QinSoft.Ioc.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Dependency
{
    /// <summary>
    /// 依赖基类
    /// </summary>
    public abstract class DependencyBase : IDependency
    {
        /// <summary>
        /// 转换器
        /// </summary>
        public IConverter Converter { get; protected set; }

        public DependencyBase(IConverter converter)
        {
            if (converter == null) throw new ArgumentNullException("converter");
            this.Converter = converter;
        }

        public DependencyBase() : this(new ConverterBase())
        {
        }

        /// <summary>
        /// 获取原值
        /// </summary>
        /// <returns></returns>
        public abstract object GetSource();

        /// <summary>
        /// 获取转换后的值
        /// </summary>
        /// <returns></returns>
        public override object GetValue()
        {
            return this.Converter.Convert(this.GetSource(), this);
        }
    }
}
