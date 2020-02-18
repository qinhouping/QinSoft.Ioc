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
            TestClassA testClassA = objectFactory.CreateInstance<TestClassA>("hello", 2);
            Assert.IsNotNull(testClassA);
        }

        [TestMethod]
        public void TestObjectContainerImp()
        {
            ObjectContainer objectContainer = new ObjectContainerImp(new ObjectFactoryImp(), new AttributeDependencyInjectionScanerImp("QinSoft.Ioc.UnitTest"));
            TestClassB testClassB = objectContainer.Get(typeof(TestClassB)) as TestClassB;
            TestClassB testClassB2 = objectContainer.Get(typeof(TestClassB)) as TestClassB;
            Assert.AreEqual(testClassB, testClassB2);
        }
    }

    [Component]
    public class TestClassA
    {
        public string P1 { get; set; }

        public int P2 { get; set; }

        public TestClassA(string Arg1, int Arg2)
        {
            this.P1 = Arg1;
            this.P2 = Arg2;
        }

        [Construct]
        public TestClassA([ConfigDependency("Arg2", typeof(int))] int Arg2)
        {
            this.P2 = Arg2;
        }
    }

    [Component]
    public class TestClassB
    {
        [Construct]
        public TestClassB(TestClassA classA)
        {
            this.classA = classA;
        }

        public TestClassA classA { get; set; }
    }
}
