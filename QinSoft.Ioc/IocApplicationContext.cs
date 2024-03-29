﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QinSoft.Ioc.Factory;
using QinSoft.Ioc.Scaner;
using QinSoft.Ioc.Container;
using System.Reflection;

namespace QinSoft.Ioc
{
    /// <summary>
    /// Ioc应用程序
    /// </summary>
    public class IocApplicationContext : IDisposable
    {
        /// <summary>
        /// 对象工厂
        /// </summary>
        public ObjectFactory ObjectFactory { get; protected set; }

        /// <summary>
        /// 依赖注入扫描
        /// </summary>
        public DependencyInjectionScaner DependencyInjectionScaner { get; protected set; }

        /// <summary>
        /// 对象容器实例
        /// </summary>
        public ObjectContainer ObjectContainer { get; protected set; }

        /// <summary>
        /// 注册对象工厂
        /// </summary>
        /// <typeparam name="T">对象工厂类型</typeparam>
        /// <param name="parameters">构造参数</param>
        /// <returns>当前应用上下文</returns>
        public virtual IocApplicationContext RegisterObjectFactory<T>(params object[] parameters) where T : ObjectFactory
        {
            this.ObjectFactory = new ObjectFactoryImp().CreateInstance<T>(parameters) as ObjectFactory;
            return this;
        }

        /// <summary>
        /// 注册依赖注入扫描
        /// </summary>
        /// <typeparam name="T">依赖扫描类型</typeparam>
        /// <param name="assemblies">程序集名称列表</param>
        /// <returns>当前应用上下文</returns>
        public virtual IocApplicationContext RegisterDependencyInjectionScaner<T>(params string[] assemblies) where T : DependencyInjectionScaner
        {
            this.DependencyInjectionScaner = new ObjectFactoryImp().CreateInstance<T>(new object[] { assemblies }) as DependencyInjectionScaner;
            return this;
        }

        /// <summary>
        /// 注册依赖注入扫描
        /// </summary>
        /// <typeparam name="T">依赖扫描类型</typeparam>
        /// <param name="assemblies">程序集列表</param>
        /// <returns>当前应用上下文</returns>
        public virtual IocApplicationContext RegisterDependencyInjectionScaner<T>(params Assembly[] assemblies) where T : DependencyInjectionScaner
        {
            this.DependencyInjectionScaner = new ObjectFactoryImp().CreateInstance<T>(new object[] { assemblies }) as DependencyInjectionScaner;
            return this;
        }

        /// <summary>
        /// 注册全局依赖注入扫描
        /// </summary>
        /// <typeparam name="T">依赖扫描类型</typeparam>
        /// <returns>当前应用上下文</returns>
        public virtual IocApplicationContext RegisterDependencyInjectionScaner<T>() where T : DependencyInjectionScaner
        {
            this.DependencyInjectionScaner = new ObjectFactoryImp().CreateInstance<T>() as DependencyInjectionScaner;
            return this;
        }

        /// <summary>
        /// 构建对象容器
        /// </summary>
        /// <typeparam name="T">对象容器类型</typeparam>
        /// <returns>对象容器实例</returns>
        public virtual IocApplicationContext BuildObjectContainer<T>() where T : ObjectContainer
        {
            if (this.ObjectFactory == null) throw new InvalidOperationException("not register object factory");
            if (this.DependencyInjectionScaner == null) throw new InvalidOperationException("not register dependency injection scaner");
            this.ObjectContainer = new ObjectFactoryImp().CreateInstance<T>(this.ObjectFactory, this.DependencyInjectionScaner) as ObjectContainer;
            return this;
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            this.ObjectContainer?.Clear();
            this.ObjectContainer = null;
            this.DependencyInjectionScaner = null;
            this.ObjectFactory = null;
        }
    }
}
