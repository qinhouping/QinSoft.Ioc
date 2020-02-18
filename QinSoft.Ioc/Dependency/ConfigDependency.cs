using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using QinSoft.Ioc.Converter;

namespace QinSoft.Ioc.Dependency
{
    /// <summary>
    /// 从配置项AppSettings获取配置内容
    /// </summary>
    public class ConfigDependency : DependencyBase
    {
        /// <summary>
        /// 配置关键字
        /// </summary>
        public string Key { get; protected set; }

        public ConfigDependency(string key, IConverter converter) : base(converter)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("key");
            this.Key = key;
        }

        public ConfigDependency(string key) : base()
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("key");
            this.Key = key;
        }

        public override object GetSource()
        {
            return ConfigurationManager.AppSettings.Get(Key);
        }
    }
}
