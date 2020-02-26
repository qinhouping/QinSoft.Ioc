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
        /// 判断构造函数是否满足参数要求
        /// </summary>
        /// <param name="constructor">构造函数</param>
        /// <param name="args">参数</param>
        /// <param name="parameters">返回参数</param>
        /// <returns>是否匹配</returns>
        protected virtual bool MatchConstructor(ConstructorInfo constructor, object[] args, out object[] parameters)
        {
            if (constructor == null) throw new ArgumentNullException("constructor");
            if (args == null) throw new ArgumentNullException("args");
            parameters = new object[0];
            ParameterInfo[] parameterInfos = constructor.GetParameters();
            List<object> paramterList = new List<object>();
            if (args.Length > parameterInfos.Length) return false;
            int index = 0;
            for (; index < args.Length; index++)
            {
                object arg = args[index];
                ParameterInfo parameter = parameterInfos[index];
                Type argType = arg?.GetType();
                Type parameterType = parameter.ParameterType;
                if ((argType == null && !parameterType.IsValueType) || (parameterType.IsAssignableFrom(argType)))
                {
                    paramterList.Add(arg);
                }
                else
                {
                    return false;
                }
            }
            for (; index < parameterInfos.Length; index++)
            {
                ParameterInfo parameter = parameterInfos[index];
                if (parameter.IsOptional)
                {
                    paramterList.Add(parameter.DefaultValue);
                }
                else
                {
                    return false;
                }
            }
            parameters = paramterList.ToArray();
            return true;
        }

        /// <summary>
        /// 获取可用构造函数
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="args">参数</param>
        /// <param name="parameters">返回参数</param>
        /// <returns>构造函数</returns>
        protected virtual ConstructorInfo FindConstructor(Type type, object[] args, out object[] parameters)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (args == null) throw new ArgumentNullException("args");
            List<ConstructorInfo> constructorInfos = new List<ConstructorInfo>();
            List<object[]> parametersList = new List<object[]>();
            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                if (MatchConstructor(constructor, args, out object[] tempParameters))
                {
                    constructorInfos.Add(constructor);
                    parametersList.Add(tempParameters);
                }
            }
            if (constructorInfos.Count < 1) throw new IocException("No constructors were found that exceeded the criteria");
            if (constructorInfos.Count > 1) throw new IocException("Found more than one constructor satisfying the condition");
            parameters = parametersList.First();
            return constructorInfos.First();
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
            ConstructorInfo constructor = FindConstructor(type, args, out object[] parameters);
            return CreateInstance(constructor, parameters);
        }
    }
}
