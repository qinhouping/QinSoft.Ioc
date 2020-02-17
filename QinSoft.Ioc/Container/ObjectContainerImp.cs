using QinSoft.Ioc.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using QinSoft.Ioc.Dependency;

namespace QinSoft.Ioc.Container
{
    public class ObjectContainerImp : ObjectContainer
    {
        public ObjectFactory ObjectFactory { get; protected set; }

        public DependencyInjectionScaner DependencyInjectionScaner { get; protected set; }

        protected IList<object> ObjectCache { get; set; }

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

        public override void Clear()
        {
            lock (ObjectCache)
            {
                ObjectCache.Clear();
            }
        }

        public override bool Remove(Type type)
        {
            lock (ObjectCache)
            {
                return this.ObjectCache.Remove(GetInstance(type));
            }
        }

        public override bool Remove(object instance)
        {
            lock (ObjectCache)
            {
                return this.ObjectCache.Remove(instance);
            }
        }

        public override bool Exists(Type type)
        {
            lock (ObjectCache)
            {
                return this.ObjectCache.Select(u => type.Equals(u.GetType())).Count() > 0;
            }
        }

        public override bool Exists(object instance)
        {
            lock (ObjectCache)
            {
                return this.ObjectCache.Contains(instance);
            }
        }

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

        protected virtual object GetInstance(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return this.ObjectCache.FirstOrDefault(u => type.Equals(u.GetType()));
        }

        protected virtual DependencyInjection FindDependencyInjection(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return DependencyInjections.FirstOrDefault(u => type.Equals(u.Type));
        }

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
    }
}
