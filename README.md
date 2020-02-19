# QinSoft.Ioc
QinSoft框架下的依赖注入组件


### 1. 对象工厂
ObjectFactoryImp对象工厂实现

### 2. 对象容器
ObjectContainerImp对象容器实现

### 3. 依赖注入扫描
1. AttributeDependencyInjectionScanerImp特性依赖注入扫描实现
2. ComponentAttribute标识ioc依赖组件
3. ConstructAttribute标识ioc构造入口
4. DependencyAttribute标识依赖注入点

### 4. Ioc应用程序
IocApplicationContext

### 5. 测试（使用）案例
```
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
```