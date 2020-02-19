using QinSoft.Ioc.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using QinSoft.Ioc.Dependency;
using QinSoft.Ioc.Scaner;

namespace QinSoft.Ioc.Container
{
    /// <summary>
    /// 对象容器实现
    /// </summary>
    public class ObjectContainerImp : ObjectContainer
    {
        /// <summary>
        /// 对象工厂
        /// </summary>
        public ObjectFactory ObjectFactory { get; protected set; }

        /// <summary>
        /// 依赖注入扫描者
        /// </summary>
        public DependencyInjectionScaner DependencyInjectionScaner { get; protected set; }

        /// <summary>
        /// 对象缓存
        /// </summary>
        protected IList<object> ObjectCache { get; set; }

        /// <summary>
        /// 依赖注入列表
        /// </summary>
        protected IList<DependencyInjection> DependencyInjections { get; set; }

        public ObjectContainerImp(ObjectFactory objectFactory, DependencyInjectionScaner dependencyInjectionScaner)
        {
            if (objectFactory == null) throw new ArgumentNullException("objectFactory");
            if (dependencyInjectionScaner == null) throw new ArgumentNullException("dependencyInjectionScaner");

            this.ObjectFactory = objectFactory;
            ObjectCache = new List<object>();

            this.DependencyInjectionScaner = dependencyInjectionScaner;
            this.DependencyInjectionScaner.ObjectContainer = this;
            DependencyInjections = this.DependencyInjectionScaner.Scan();
        }

        /// <summary>
        /// 清除对象缓存
        /// </summary>
        public override void Clear()
        {
            lock (ObjectCache)
            {
                ObjectCache.Clear();
            }
        }

        /// <summary>
        /// 移除指定类型对象
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>移除结果</returns>
        public override bool Remove(Type type)
        {
            lock (ObjectCache)
            {
                return this.ObjectCache.Remove(GetInstance(type));
            }
        }

        /// <summary>
        /// 移除实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>移除结果</returns>
        public override bool Remove(object instance)
        {
            lock (ObjectCache)
            {
                return this.ObjectCache.Remove(instance);
            }
        }

        /// <summary>
        /// 判断指定类型实例是否存在
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>是否存在</returns>
        public override bool Exists(Type type)
        {
            lock (ObjectCache)
            {
                return this.ObjectCache.Select(u => type.Equals(u.GetType())).Count() > 0;
            }
        }

        /// <summary>
        /// 判断实例是否存在
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>是否存在</returns>
        public override bool Exists(object instance)
        {
            lock (ObjectCache)
            {
                return this.ObjectCache.Contains(instance);
            }
        }

        /// <summary>
        /// 从容器中获取指定类型对象
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>对象实例</returns>
        public override object Get(Type type)
        {
            lock (ObjectCache)
            {
                if (type == null) throw new ArgumentNullException("type");
                object instance = GetInstance(type);
                if (instance == null)
                {
                    instance = CreateInstance(type);
                    this.ObjectCache.Add(instance);
                }
                return instance;
            }
        }

        /// <summary>
        /// 从容器中获取指定类型对象
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>对象实例</returns>
        protected virtual object GetInstance(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return this.ObjectCache.FirstOrDefault(u => type.Equals(u.GetType()));
        }

        /// <summary>
        /// 获取指定类型的依赖注入
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>依赖注入</returns>
        protected virtual DependencyInjection FindDependencyInjection(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return DependencyInjections.FirstOrDefault(u => type.Equals(u.Type));
        }

        /// <summary>
        /// 创建指定类型实例
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>对象实例</returns>
        protected virtual object CreateInstance(Type type)
        {
            try
            {
                DependencyInjection dependencyInjection = FindDependencyInjection(type);
                if (dependencyInjection == null) throw new InvalidOperationException("No conditional dependency found");
                List<object> args = new List<object>();
                foreach (ParameterInfo pinfo in dependencyInjection.Constructor.GetParameters())
                {
                    if (!dependencyInjection.DependencyDictionary.ContainsKey(pinfo)) throw new InvalidProgramException("Construction parameter dependency does not exist");
                    IDependency dependency = dependencyInjection.DependencyDictionary[pinfo];
                    args.Add(dependency.GetValue());
                }
                object instance = ObjectFactory.CreateInstance(dependencyInjection.Constructor, args.ToArray());

                foreach (FieldInfo finfo in type.GetFields())
                {
                    if (dependencyInjection.DependencyDictionary.ContainsKey(finfo))
                    {
                        finfo.SetValue(instance, dependencyInjection.DependencyDictionary[finfo].GetValue());
                    }
                }

                foreach (PropertyInfo pinfo in type.GetProperties())
                {
                    if (dependencyInjection.DependencyDictionary.ContainsKey(pinfo))
                    {
                        pinfo.SetValue(instance, dependencyInjection.DependencyDictionary[pinfo].GetValue(), null);
                    }
                }

                return instance;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// 刷新依赖注入
        /// </summary>
        public override void RefreshDependencyInjections()
        {
            DependencyInjections = this.DependencyInjectionScaner.Scan();
        }
    }
}
