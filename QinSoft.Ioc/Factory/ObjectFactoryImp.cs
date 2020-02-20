using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QinSoft.Ioc.Factory
{
    /// <summary>
    /// 对象工厂实现
    /// </summary>
    public class ObjectFactoryImp : ObjectFactory
    {
        /// <summary>
        /// 根据构造函数创建实例
        /// </summary>
        /// <param name="constructor">构造函数</param>
        /// <param name="args">参数</param>
        /// <returns>对象实例</returns>
        public override object CreateInstance(ConstructorInfo constructor, params object[] args)
        {
            if (constructor == null) throw new ArgumentNullException("constructor");
            object instance = constructor.Invoke(args);
            return instance;
        }

        /// <summary>
        /// 判断类型是否匹配
        /// </summary>
        /// <param name="parameterTypes">构造函数参数类型</param>
        /// <param name="argTypes">参数类型</param>
        /// <returns>是否匹配</returns>
        protected virtual bool IsMatch(Type[] parameterTypes, Type[] argTypes)
        {
            if (parameterTypes == null) throw new ArgumentNullException("parameterTypes");
            if (argTypes == null) throw new ArgumentNullException("argTypes");
            if (parameterTypes.Length != argTypes.Length) return false;
            for (int i = 0; i < argTypes.Length; i++)
            {
                Type argType = argTypes[i];
                Type parameterType = parameterTypes[i];
                if (!((argType == null && !parameterType.IsValueType) || (parameterType.IsAssignableFrom(argType)))) return false;
            }
            return true;
        }

        /// <summary>
        /// 获取可用构造函数
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="argTypes">参数类型</param>
        /// <returns>构造函数</returns>
        protected virtual ConstructorInfo FindConstructor(Type type, Type[] argTypes)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (argTypes == null) throw new ArgumentNullException("argTypes");
            List<ConstructorInfo> constructorInfos = new List<ConstructorInfo>();
            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                Type[] parameterTypes = constructor.GetParameters().Select(u => u.ParameterType).ToArray();
                if (IsMatch(parameterTypes, argTypes)) constructorInfos.Add(constructor);
            }
            if (constructorInfos.Count < 1) throw new IocException("No constructors were found that exceeded the criteria");
            if (constructorInfos.Count > 1) throw new IocException("Found more than one constructor satisfying the condition");
            return constructorInfos.First();
        }

        /// <summary>
        /// 使用无参构造函数创建实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>对象实例</returns>
        public override object CreateInstance(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return CreateInstance(type, new object[] { });
        }

        /// <summary>
        /// 使用无参构造函数创建实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>对象实例</returns>
        public virtual T CreateInstance<T>() where T : class
        {
            Type type = typeof(T);
            return CreateInstance(type) as T;
        }

        /// <summary>
        /// 根据参数创建实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="args">参数</param>
        /// <returns>对象实例</returns>
        public override object CreateInstance(Type type, params object[] args)
        {
            if (type == null) throw new ArgumentNullException("type");
            Type[] argTypes = (args ?? new object[] { }).Select(u => u?.GetType()).ToArray();
            ConstructorInfo constructor = FindConstructor(type, argTypes);
            return CreateInstance(constructor, args);
        }

        /// <summary>
        /// 根据参数创建实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="args">参数</param>
        /// <returns>对象实例</returns>
        public virtual T CreateInstance<T>(params object[] args) where T : class
        {
            Type type = typeof(T);
            return CreateInstance(type, args) as T;
        }
    }
}
