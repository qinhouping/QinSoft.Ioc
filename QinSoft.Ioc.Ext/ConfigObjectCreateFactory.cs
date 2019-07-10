using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QinSoft.Ioc.Core;
using System.Configuration;
using System.Reflection;

namespace QinSoft.Ioc.Ext
{
    public class ConfigObjectCreateFactory : IConfigObjectCreateFactory
    {
        protected IocSection IocConfig { get; set; }

        protected IObjectCreator ObjectCreator { get; set; }

        public ConfigObjectCreateFactory(string SectionName)
        {
            IocConfig = ConfigurationManager.GetSection(SectionName) as IocSection;
            ObjectCreator = new ObjectCreateFactory();
        }


        public ConfigObjectCreateFactory(string SectionName, IObjectCreator objectCreator)
        {
            if (objectCreator == null) throw new ArgumentNullException("ObjectCreator");
            IocConfig = ConfigurationManager.GetSection(SectionName) as IocSection;
            ObjectCreator = objectCreator;
        }


        public virtual object CreateObject(string ObjectName)
        {
            return ObjectCreator.CreateObject(FindObjectDependency(ObjectName));
        }

        protected virtual IocObjectElement FindObjectElement(string Name)
        {
            foreach (IocObjectElement item in IocConfig.Objects)
            {
                if (item.Name == Name)
                {
                    return item;
                }
            }
            return null;
        }

        protected virtual BaseObjectDependency FindObjectDependency(string ObjectName)
        {
            IocObjectElement objectElement = FindObjectElement(ObjectName);
            BaseObjectDependency objectDependency = new BaseObjectDependency()
            {
                Type = Type.GetType(objectElement.Type),
                CreateType = objectElement.CreateType
            };
            return objectDependency;
        }
    }
}
