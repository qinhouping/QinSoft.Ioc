using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QinSoft.Ioc.UnitTest
{
    [TestClass]
    public class IocBaseTest
    {
        [TestMethod]
        public void TestObjectFactoryImp()
        {
            Type typea = typeof(TestClassA);
            Type typeb = typeof(TestClassB);
            DependencyInjection dependencyInjectiona = new DependencyInjection()
            {
                Constructor = typea.GetConstructor(new Type[0]),
                ConstructInjections = null,
                PropertyInjections = null
            };

            DependencyInjection dependencyInjectionb = new DependencyInjection()
            {
                Constructor = typeb.GetConstructor(new Type[] { typea }),
                ConstructInjections = new Dictionary<ParameterInfo, DependencyInjection>() { { typeb.GetConstructor(new Type[] { typea }).GetParameters()[0], dependencyInjectiona } }
            };

            ObjectFactory objectFactory = new ObjectFactoryImp();
            TestClassA classA = objectFactory.CreateObject(dependencyInjectiona) as TestClassA;
            TestClassB classB = objectFactory.CreateObject(dependencyInjectionb) as TestClassB;
        }
    }

    public class TestClassA
    {
        public string P1 { get; set; }
    }

    public class TestClassB
    {
        public TestClassB(TestClassA classA)
        {
            this.classA = classA;
        }
        public TestClassA classA { get; set; }
    }
}
