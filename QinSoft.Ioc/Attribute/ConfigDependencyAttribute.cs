using QinSoft.Ioc.Dependency;
using QinSoft.Ioc.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Attribute
{
    /// <summary>
    /// 配置依赖
    /// </summary>
    public class ConfigDependencyAttribute : DependencyAttribute
    {
        /// <summary>
        /// 配置关键字
        /// </summary>
        public string Key { get; protected set; }

        /// <summary>
        /// 转换器
        /// </summary>
        public IConverter Converter { get; protected set; }

        public ConfigDependencyAttribute(string key, Type type = null) : base()
        {
            this.Key = key;
            this.Converter = type == null ? new ConverterBase() : new TypeConverter(type);
        }

        public override IDependency GetDependency()
        {
            return new ConfigDependency(Key, this.Converter);
        }
    }
}
