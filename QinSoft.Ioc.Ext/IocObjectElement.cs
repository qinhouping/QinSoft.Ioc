using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using QinSoft.Ioc.Core;

namespace QinSoft.Ioc.Ext
{
    public class IocObjectElement : ConfigurationElement
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

        [ConfigurationProperty("Type", IsRequired = true)]
        public string Type
        {
            get
            {
                return this["Type"] as string;
            }
            set
            {
                this["Type"] = value;
            }
        }

        [ConfigurationProperty("CreateType", DefaultValue = ObjectCreateType.Normal)]
        public ObjectCreateType CreateType
        {
            get
            {
                return (ObjectCreateType)this["CreateType"];
            }
            set
            {
                this["CreateType"] = value;
            }
        }

        [ConfigurationProperty("Value")]
        public Object Value
        {
            get
            {
                return this["Value"];
            }
            set
            {
                this["Value"] = value;
            }
        }

        [ConfigurationProperty("ConstructProperties")]
        public IocPropertyCollection ConstructProperties
        {
            get
            {
                return this["ConstructProperties"] as IocPropertyCollection;
            }
            set
            {
                this["ConstructProperties"] = value;
            }
        }

        [ConfigurationProperty("Properties")]
        public new IocPropertyCollection Properties
        {
            get
            {
                return this["Properties"] as IocPropertyCollection;
            }
            set
            {
                this["Properties"] = value;
            }
        }
    }
}
