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

        /// <summary>
        /// 对象依赖字典 减少加载次数和实现单例模式
        /// </summary>
        protected IDictionary<string, BaseObjectDependency> ObjectDependencyDict { get; set; }


        public ConfigObjectCreateFactory(string SectionName)
        {
            IocConfig = ConfigurationManager.GetSection(SectionName) as IocSection;
            ObjectCreator = new ObjectCreateFactory();
            ObjectDependencyDict = new Dictionary<string, BaseObjectDependency>();
        }

        public ConfigObjectCreateFactory(string SectionName, IObjectCreator objectCreator)
        {
            if (objectCreator == null) throw new ArgumentNullException("ObjectCreator");
            IocConfig = ConfigurationManager.GetSection(SectionName) as IocSection;
            ObjectCreator = objectCreator;
            ObjectDependencyDict = new Dictionary<string, BaseObjectDependency>();
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
            if (ObjectDependencyDict.ContainsKey(ObjectName)) return ObjectDependencyDict[ObjectName];
            IocObjectElement objectElement = FindObjectElement(ObjectName);
            if (objectElement == null) throw new InvalidProgramException("ObjectElement does not exist");
            BaseObjectDependency objectDependency = new BaseObjectDependency()
            {
                Type = Type.GetType(objectElement.Type),
                CreateType = objectElement.CreateType
            };
            if (objectDependency.IsValueType())
            {
                objectDependency.Value = objectElement.Value.StringConvert(objectDependency.Type);
            }
            else
            {
                objectDependency.IsNull = objectElement.IsNull;
                #region 构造函数
                if (objectElement.ConstructProperties != null)
                {
                    List<BasePropertyDependency> propertyDependencies = new List<BasePropertyDependency>();
                    foreach (IocPropertyElement property in objectElement.ConstructProperties)
                    {
                        propertyDependencies.Add(new BasePropertyDependency()
                        {
                            Name = property.Name,
                            Value = FindObjectDependency(property.ObjectName)
                        });
                    }
                    objectDependency.ConstructDependencies = propertyDependencies.ToArray();
                }
                #endregion

                #region 属性
                if (objectElement.Properties != null)
                {
                    List<BasePropertyDependency> propertyDependencies = new List<BasePropertyDependency>();
                    foreach (IocPropertyElement property in objectElement.Properties)
                    {
                        propertyDependencies.Add(new BasePropertyDependency()
                        {
                            Name = property.Name,
                            Value = FindObjectDependency(property.ObjectName)
                        });
                    }
                    objectDependency.PropertyDependencies = propertyDependencies.ToArray();
                }
                #endregion 
            }
            ObjectDependencyDict.Add(ObjectName, objectDependency);
            return objectDependency;
        }

        public virtual void ClearObjectDependencyDict()
        {
            ObjectDependencyDict?.Clear();
        }
    }
}
