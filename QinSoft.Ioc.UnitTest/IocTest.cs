using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QinSoft.Ioc.Factory;
using QinSoft.Ioc.Container;
using QinSoft.Ioc.Dependency;
using QinSoft.Ioc.Scaner;
using QinSoft.Ioc.Converter;
using QinSoft.Ioc.Attribute;

namespace QinSoft.Ioc.UnitTest
{
    [TestClass]
    public class IocTest
    {
        [TestMethod]
        public void TestObjectFactoryImp()
        {
            ObjectFactoryImp objectFactory = new ObjectFactoryImp();
            TestClassA testClassA = objectFactory.CreateInstance<TestClassA>();
            Assert.IsNotNull(testClassA);
        }

        [TestMethod]
        public void TestObjectContainerImp()
        {
            ObjectContainer objectContainer = new ObjectContainerImp(new ObjectFactoryImp(), new AttributeDependencyInjectionScanerImp());
            TestClassB testClassB = objectContainer.Get(typeof(TestClassBB)) as TestClassBB;
            TestClassB testClassB2 = objectContainer.Get(typeof(TestClassBB)) as TestClassBB;
            Assert.AreEqual(testClassB, testClassB2);
        }

        [TestMethod]
        public void TestIocApplicationContext()
        {
            IocApplicationContext applicationContext = new IocApplicationContext();
            ObjectContainer objectContainer = applicationContext
                .RegisterObjectFactory<ObjectFactoryImp>()
                .RegisterDependencyInjectionScaner<AttributeDependencyInjectionScanerImp>()
                .BuildObjectContainer<ObjectContainerImp>();
            TestClassB testClassB = objectContainer.Get<TestClassBB>();
            TestClassB testClassB2 = objectContainer.Get<TestClassBB>();
            Assert.AreEqual(testClassB, testClassB2);
        }
    }

    [Component]
    public class TestClassA
    {
        [ConfigDependency("Arg1")]
        public int P1 { get; private set; }

        [ConfigDependency("Arg2")]
        public string P2 { get; private set; }
    }

    public abstract class TestClassB
    {
        [ComponentDependency]
        private TestClassA classA { get; set; }

        [ComponentDependency]
        private TestClassA classAA;

        public abstract string DoSomething();
    }

    [Component]
    public class TestClassBB : TestClassB
    {
        public override string DoSomething()
        {
            return "test";
        }
    }
}
