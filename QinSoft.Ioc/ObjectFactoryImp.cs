using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QinSoft.Ioc
{
    /// <summary>
    /// 简单对象工厂实现
    /// </summary>
    public class ObjectFactoryImp : ObjectFactory
    {
        public override object CreateObject(DependencyInjection dependencyInjection)
        {
            object obj = CreateObjectInstance(dependencyInjection);
            InjectObjectProperty(obj, dependencyInjection);
            return obj;
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="dependencyInjection">依赖</param>
        /// <returns></returns>
        protected virtual object CreateObjectInstance(DependencyInjection dependencyInjection)
        {
            if (dependencyInjection == null || dependencyInjection.Constructor == null) return null;
            List<object> constructParams = new List<object>();
            foreach (ParameterInfo pinfo in dependencyInjection.Constructor.GetParameters())
            {
                if (dependencyInjection.ConstructInjections?.ContainsKey(pinfo) == true)
                {
                    object obj = CreateObject(dependencyInjection.ConstructInjections[pinfo]);
                    constructParams.Add(obj);
                }
                else
                {
                    throw new IocException(string.Format("creaet {0} instance failure, because construct lose parameter {0}", dependencyInjection.Type.FullName, pinfo.Name));
                }
            }
            return dependencyInjection.Constructor.Invoke(constructParams.ToArray());
        }

        /// <summary>
        /// 注入对象属性
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="dependencyInjection">依赖</param>
        protected virtual void InjectObjectProperty(object obj, DependencyInjection dependencyInjection)
        {
            if (obj == null) return;
            foreach (PropertyInfo pinfo in dependencyInjection.Type?.GetProperties())
            {
                if (dependencyInjection.PropertyInjections?.ContainsKey(pinfo) == true)
                {
                    pinfo.SetValue(obj, CreateObject(dependencyInjection.PropertyInjections[pinfo]), null);
                }
            }
        }
    }
}
