using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace QinSoft.Ioc.Ext
{
    public class IocSection : ConfigurationSection
    {
        [ConfigurationProperty("Objects", IsDefaultCollection = true)]
        public IocObjectCollection Objects
        {
            get
            {
                return this["Objects"] as IocObjectCollection;
            }
            set
            {
                this["Objects"] = value;
            }
        }
    }
}
