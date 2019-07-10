using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Ext
{
    [ConfigurationCollection(typeof(IocPropertyElement), AddItemName = "Property", RemoveItemName ="RemoveProperty", ClearItemsName = "ClearProperties")]
    public class IocPropertyCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new IocPropertyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as IocPropertyElement).Name;
        }
    }
}
