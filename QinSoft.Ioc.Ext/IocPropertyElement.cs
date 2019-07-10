using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace QinSoft.Ioc.Ext
{
    public class IocPropertyElement:ConfigurationElement
    {
        [ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return this["Name"] as string;
            }
            set
            {
                this["Name"] = value;
            }
        }

        [ConfigurationProperty("ObjectName", IsKey = true, IsRequired = true)]
        public string ObjectName
        {
            get
            {
                return this["ObjectName"] as string;
            }
            set
            {
                this["ObjectName"] = value;
            }
        }
    }
}
