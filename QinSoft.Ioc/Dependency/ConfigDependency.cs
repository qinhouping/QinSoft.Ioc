using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace QinSoft.Ioc.Dependency
{
    public class ConfigDependency : DependencyBase
    {
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
