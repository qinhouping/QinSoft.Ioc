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

### 5. 扩展类
1. IocObjectContainerExtends
2. IocObjectFactoryExtends
3. TypeExtends

### 5. 测试（使用）案例
```
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
```