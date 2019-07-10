using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace QinSoft.Ioc.Ext
{
    [ConfigurationCollection(typeof(IocObjectElement), AddItemName = "Object", RemoveItemName = "RemoveObject", ClearItemsName = "ClearObjects")]
    public class IocObjectCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new IocObjectElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as IocObjectElement).Name;
        }
    }
}
