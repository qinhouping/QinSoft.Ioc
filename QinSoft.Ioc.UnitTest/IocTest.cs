using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QinSoft.Ioc.Factory;
using QinSoft.Ioc.Container;
using QinSoft.Ioc.Dependency;

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
            ObjectFactory objectFactory = new ObjectFactoryImp();
            DependencyInjectionScaner dependencyInjectionScaner = new TestDependencyInjectionScanerImp("");
            ObjectContainer container = new ObjectContainerImp(objectFactory, dependencyInjectionScaner);
            TestClassA testClassA = container.Get(typeof(TestClassA)) as TestClassA;
            TestClassA testClassA2 = container.Get(typeof(TestClassA)) as TestClassA;
            Assert.AreEqual(testClassA, testClassA2);

            TestClassB testClassB = container.Get(typeof(TestClassB)) as TestClassB;
            TestClassB testClassB2 = container.Get(typeof(TestClassB)) as TestClassB;
            Assert.AreEqual(testClassB, testClassB2);
        }
    }

    public class TestDependencyInjectionScanerImp : DependencyInjectionScaner
    {
        public TestDependencyInjectionScanerImp(params string[] namespaces)
        {
        }

        public override DependencyInjection[] Scan()
        {
            List<DependencyInjection> dependencyInjections = new List<DependencyInjection>();

            Type type_a = typeof(TestClassA);
            ConstructorInfo constructor_a = type_a.GetConstructor(new Type[] { typeof(object) });
            ParameterInfo parameter_a = constructor_a.GetParameters()[0];
            PropertyInfo property_a = type_a.GetProperty("P2");
            DependencyInjection dependencyInjection_a = new DependencyInjection();
            dependencyInjection_a.Type = type_a;
            dependencyInjection_a.Constructor = constructor_a;
            dependencyInjection_a.DependencyDictionary = new Dictionary<object, IDependency>()
            {
                { parameter_a, new ObjectDependency("hello world")},
                { property_a, new ObjectDependency("22",new TypeConverter(typeof(Int32)))}
            };
            dependencyInjections.Add(dependencyInjection_a);

            Type type_b = typeof(TestClassB);
            ConstructorInfo constructor_b = type_b.GetConstructor(new Type[] { typeof(TestClassA) });
            ParameterInfo parameter_b = constructor_b.GetParameters()[0];
            DependencyInjection dependencyInjection_b = new DependencyInjection();
            dependencyInjection_b.Type = type_b;
            dependencyInjection_b.Constructor = constructor_b;
            dependencyInjection_b.DependencyDictionary = new Dictionary<object, IDependency>()
            {
                { parameter_b, new ContainerObjectDependency(this.ObjectContainer,typeof(TestClassA))},
            };
            dependencyInjections.Add(dependencyInjection_b);

            return dependencyInjections.ToArray();
        }
    }

    public class TestClassA
    {
        public string P1 { get; set; }

        public int P2 { get; set; }


        public TestClassA(string Arg1, int Arg2)
        {
            this.P1 = Arg1;
            this.P2 = Arg2;
        }

        public TestClassA(object Arg1)
        {
            this.P1 = Arg1?.ToString();
        }
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
