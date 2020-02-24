# QinSoft.Ioc
QinSoft框架下的依赖注入组件


### 1. 对象工厂
ObjectFactoryImp对象工厂实现

### 2. 对象容器
ObjectContainerImp对象容器实现

### 3. 依赖注入扫描
1. AttributeDependencyInjectionScanerImp特性依赖注入扫描实现
2. ComponentAttribute标识ioc依赖组件
3. ConstructorAttribute标识ioc构造入口
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
```