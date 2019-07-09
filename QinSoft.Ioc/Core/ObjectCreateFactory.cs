using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using QinSoft.Ioc.Core;

namespace QinSoft.Ioc.Core
{
    /// <summary>
    /// 对象创建工厂（支持普通和单例模式）
    /// </summary>
    public class ObjectCreateFactory : IObjectCreator
    {
        /// <summary>
        /// 对象字典映射
        /// </summary>
        protected ObjectInstanceMapper SingtonObjectDict { get; set; }

        public ObjectCreateFactory()
        {
            SingtonObjectDict = new ObjectInstanceMapper();
        }

        /// <summary>
        /// 通过对象依赖创建对象
        /// </summary>
        /// <param name="objectDependency"></param>
        /// <returns></returns>
        public virtual object CreateObject(BaseObjectDependency objectDependency)
        {
            try
            {
                if (objectDependency == null) throw new ArgumentNullException("objectDependency");
                object value = null;
                if (objectDependency.CreateType == ObjectCreateType.Normal)
                {
                    value = CreateInstance(objectDependency);
                }
                else
                {
                    if (SingtonObjectDict.Exists(objectDependency))
                    {
                        value = SingtonObjectDict.Find(objectDependency);
                    }
                    else
                    {
                        value = CreateInstance(objectDependency);
                        SingtonObjectDict.Add(objectDependency, value);
                    }
                }
                return value;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                GC.Collect();//垃圾回收
            }
        }

        /// <summary>
        /// 返回匹配构造函数（不允许多个匹配）
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        protected virtual ConstructorInfo FindConstructorInfo(BaseObjectDependency dependency)
        {
            ConstructorInfo[] constructorInfos = dependency.Type.GetConstructors();
            ConstructorInfo value = null;
            bool isOk = false;
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                isOk = true;
                foreach (ParameterInfo parameter in constructorInfo.GetParameters())
                {

                    if (!parameter.ParameterType.IsAssignableFrom(dependency.GetConstructPropertyObject(parameter.Name)?.Type))
                    {
                        isOk = false;
                        break;
                    }
                }
                if (isOk)
                {
                    if (value == null)
                        value = constructorInfo;
                    else
                        throw new InvalidProgramException(string.Format("{0}发现多个满足条件的构造函数", dependency.Type.FullName));
                }
            }
            return value;
        }

        /// <summary>
        /// 返回匹配属性
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="propertyDependency"></param>
        /// <returns></returns>
        protected virtual PropertyInfo FindPropertyInfo(BaseObjectDependency dependency, BasePropertyDependency propertyDependency)
        {
            return dependency.Type.GetProperty(propertyDependency.Name);
        }

        /// <summary>
        /// 值类型返回
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        protected virtual object GetValue(BaseObjectDependency dependency)
        {
            return dependency.Value;
        }

        /// <summary>
        /// 应用类型返回
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        protected virtual object GetObject(BaseObjectDependency dependency)
        {
            object value = null;
            #region 构造对象
            ConstructorInfo constructorInfo = FindConstructorInfo(dependency);
            if (constructorInfo == null) throw new InvalidProgramException(string.Format("{0}未发现满足条件的构造函数", dependency.Type.FullName));
            List<object> paramList = new List<object>();

            foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
            {
                paramList.Add(CreateObject(dependency.GetConstructPropertyObject(parameterInfo.Name)));
            }
            value = constructorInfo.Invoke(paramList.ToArray());
            #endregion

            #region 属性赋值
            if (dependency.PropertyDependencies != null)
            {
                foreach (BasePropertyDependency propertyDependency in dependency.PropertyDependencies)
                {
                    PropertyInfo propertyInfo = FindPropertyInfo(dependency, propertyDependency);
                    if (propertyInfo == null) throw new InvalidProgramException(string.Format("{0}未发现属性{1}", dependency.Type.FullName, propertyDependency.Name));
                    propertyInfo.SetValue(value, CreateObject(propertyDependency.Value), null);
                }
            }
            #endregion

            return value;
        }

        /// <summary>
        /// 创建对象依赖实例
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        protected virtual object CreateInstance(BaseObjectDependency dependency)
        {
            object value = null;
            if (dependency.IsValueType())
            {
                value = GetValue(dependency);
            }
            else
            {
                value = GetObject(dependency);
            }
            return value;
        }
    }
}
