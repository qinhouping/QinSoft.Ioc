using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using QinSoft.Ioc.Container;
using QinSoft.Ioc.Attribute;
using QinSoft.Ioc.Dependency;

namespace QinSoft.Ioc.Scaner
{
    /// <summary>
    /// 特性依赖注入扫描实现
    /// </summary>
    public class AttributeDependencyInjectionScanerImp : DependencyInjectionScaner
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        protected string[] namespaces { get; set; }

        /// <summary>
        /// 命名空间构造函数
        /// </summary>
        /// <param name="namespaces">命名空间列表</param>
        public AttributeDependencyInjectionScanerImp(params string[] namespaces)
        {
            if (namespaces == null) throw new ArgumentNullException("namespaces");
            this.namespaces = namespaces;
        }

        /// <summary>
        /// 扫描依赖注入实现
        /// </summary>
        /// <returns>依赖注入列表</returns>
        public override DependencyInjection[] Scan()
        {
            Type[] types = GetTypes(this.namespaces);
            return GetDependencyInjections(types);
        }

        /// <summary>
        /// 获取命名空间类型
        /// 通过ComponentAttribute指定
        /// </summary>
        /// <param name="namespaces">命名空间</param>
        /// <returns>类型列表</returns>
        protected virtual Type[] GetTypes(string[] namespaces)
        {
            if (namespaces == null) throw new ArgumentNullException("namespaces");
            IEnumerable<Type> types = new List<Type>();
            foreach (string _space in namespaces)
            {
                types = types.Union(Assembly.Load(_space).GetTypes().Where(u =>
                {
                    return System.Attribute.GetCustomAttribute(u, typeof(ComponentAttribute)) != null;
                }));
            }
            return types.ToArray();
        }

        /// <summary>
        /// 获取类型构造函数
        /// 如果一个构造函数，将使用该构造函数；如多个构造函数，请使用ConstructAttribute指定。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual ConstructorInfo GetConstructor(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            if (constructorInfos.Length != 1)
            {
                constructorInfos = constructorInfos.Where(u =>
                {
                    return System.Attribute.GetCustomAttribute(u, typeof(ConstructAttribute)) != null;
                }).ToArray();
                if (constructorInfos.Length > 1) throw new IocException(string.Format("{0} too many constructors found, check constrct attribute", type));
                if (constructorInfos.Length == 0) throw new IocException(string.Format("{0} no constructors found that meet the criteria", type));
            }
            return constructorInfos.First();
        }

        /// <summary>
        /// 创建默认依赖特性
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>依赖特性</returns>
        protected virtual DependencyAttribute CreateDependencyAttribute(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return new ComponentDependencyAttribute(type);
        }

        /// <summary>
        /// 依赖特性设置
        /// </summary>
        /// <param name="dependencyAttribute">依赖特性</param>
        protected virtual void DependencyAttributeSetting(DependencyAttribute dependencyAttribute)
        {
            if (dependencyAttribute == null) return;
            dependencyAttribute.DependencyInjectionScaner = this;
        }

        /// <summary>
        /// 从类型列表中发现指定类型的依赖类型
        /// </summary>
        /// <param name="types">类型列表</param>
        /// <param name="type">指定类型</param>
        /// <returns>依赖类型</returns>
        protected virtual Type FindDependencyType(Type[] types, Type type)
        {
            if (types == null) throw new ArgumentNullException("types");
            if (type == null) throw new ArgumentNullException("type");
            types = types.Where(u => type.IsAssignableFrom(u) && !u.IsAbstract && !u.IsInterface).ToArray();
            if (types.Length > 1) throw new IocException(string.Format("{0} find too many dependency type", type));
            if (types.Length == 0) throw new IocException(string.Format("{0} no dependency type found", type));
            return types.First();
        }

        /// <summary>
        /// 获取依赖
        /// </summary>
        /// <param name="types">类型列表</param>
        /// <param name="parameterInfo">参数信息</param>
        /// <returns>依赖</returns>
        protected virtual IDependency GetDependency(Type[] types, ParameterInfo parameterInfo)
        {
            if (types == null) throw new ArgumentNullException("types");
            if (parameterInfo == null) throw new ArgumentNullException("parameterInfo");
            DependencyAttribute dependencyAttribute = System.Attribute.GetCustomAttribute(parameterInfo, typeof(DependencyAttribute)) as DependencyAttribute ?? CreateDependencyAttribute(FindDependencyType(types, parameterInfo.ParameterType));
            DependencyAttributeSetting(dependencyAttribute);
            return dependencyAttribute.GetDependency();
        }

        /// <summary>
        /// 获取依赖
        /// </summary>
        /// <param name="types">类型列表</param>
        /// <param name="memberInfo">成员信息</param>
        /// <param name="dependency">依赖</param>
        /// <returns>是否获取成功</returns>
        protected virtual bool GetDependency(Type[] types, MemberInfo memberInfo, out IDependency dependency)
        {
            if (types == null) throw new ArgumentNullException("types");
            if (memberInfo == null) throw new ArgumentNullException("parameterInfo");
            DependencyAttribute dependencyAttribute = (System.Attribute.GetCustomAttribute(memberInfo, typeof(DependencyAttribute))) as DependencyAttribute;
            DependencyAttributeSetting(dependencyAttribute);
            dependency = dependencyAttribute?.GetDependency();
            return dependencyAttribute != null;
        }

        /// <summary>
        /// 创建指定类型的依赖注入
        /// </summary>
        /// <param name="types">类型列表</param>
        /// <param name="type">指定类型</param>
        /// <returns>依赖注入</returns>
        protected virtual DependencyInjection CreateDependencyInjection(Type[] types, Type type)
        {
            if (types == null) throw new ArgumentNullException("types");
            if (type == null) throw new ArgumentNullException("type");
            DependencyInjection dependencyInjection = new DependencyInjection();
            dependencyInjection.Type = type;
            dependencyInjection.Constructor = GetConstructor(type);
            dependencyInjection.DependencyDictionary = new Dictionary<object, IDependency>();
            foreach (ParameterInfo pinfo in dependencyInjection.Constructor.GetParameters())
            {
                dependencyInjection.DependencyDictionary[pinfo] = GetDependency(types, pinfo);
            }
            foreach (FieldInfo finfo in dependencyInjection.Type.GetFields())
            {
                if (GetDependency(types, finfo, out IDependency dependency))
                {
                    dependencyInjection.DependencyDictionary[finfo] = dependency;
                }
            }
            foreach (PropertyInfo pinfo in dependencyInjection.Type.GetProperties())
            {
                if (GetDependency(types, pinfo, out IDependency dependency))
                {
                    dependencyInjection.DependencyDictionary[pinfo] = dependency;
                }
            }
            return dependencyInjection;
        }

        /// <summary>
        /// 获取依赖注入列表
        /// </summary>
        /// <param name="types">类型列表</param>
        /// <returns>依赖注入列表</returns>
        protected virtual DependencyInjection[] GetDependencyInjections(Type[] types)
        {
            if (types == null) throw new ArgumentNullException("types");
            IList<DependencyInjection> dependencyInjections = new List<DependencyInjection>();
            foreach (Type type in types)
            {
                dependencyInjections.Add(CreateDependencyInjection(types, type));
            }
            return dependencyInjections.ToArray();
        }
    }
}
