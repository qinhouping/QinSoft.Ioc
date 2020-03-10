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
        public string Key { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type Type { get; set; }

        public ConfigDependencyAttribute(string key, Type type = null) : base()
        {
            this.Key = key;
            this.Type = type;
        }

        public override IDependency GetDependency()
        {
            return new ConfigDependency(Key, this.Type == null ? new ConverterBase() : new TypeConverter(Type));
        }
    }
}
