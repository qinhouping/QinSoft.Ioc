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
            TestClassA testClassA = objectFactory.CreateInstance<TestClassA>(1, "QinSoft.Ioc");
            Assert.IsNotNull(testClassA);
        }

        [TestMethod]
        public void TestObjectContainerImp()
        {
            ObjectContainer objectContainer = new ObjectContainerImp(new ObjectFactoryImp(), new AttributeDependencyInjectionScanerImp());
            TestClassB testClassB = objectContainer.Get(typeof(TestClassB)) as TestClassB;
            TestClassB testClassB2 = objectContainer.Get(typeof(TestClassB)) as TestClassB;
            Assert.AreEqual(testClassB, testClassB2);
        }

        [TestMethod]
        public void TestIocApplicationContext()
        {
            IocApplicationContext applicationContext = new IocApplicationContext();
            ObjectContainer objectContainer = applicationContext
                .RegisterObjectFactory<ObjectFactoryImp>()
                .RegisterDependencyInjectionScaner<AttributeDependencyInjectionScanerImp>("QinSoft.Ioc.UnitTest")
                .BuildObjectContainer<ObjectContainerImp>();
            TestClassB testClassB = objectContainer.Get(typeof(TestClassB)) as TestClassB;
            TestClassB testClassB2 = objectContainer.Get(typeof(TestClassB)) as TestClassB;
            Assert.AreEqual(testClassB, testClassB2);
        }
    }

    [Component]
    public class TestClassA
    {
        public int P1 { get; private set; }

        public string P2 { get; private set; }

        [Constructor]
        public TestClassA([ConfigDependency("Arg1", typeof(int))] int Arg1, [ConfigDependency("Arg2")] string Arg2 = "test")
        {
            this.P1 = Arg1;
            this.P2 = Arg2;
        }
    }

    [Component]
    public class TestClassB
    {
        public TestClassA classA { get; private set; }

        [Constructor]
        public TestClassB(TestClassA classA)
        {
            this.classA = classA;
        }
    }
}
