# QinSoft.Ioc
依赖注入框架

## 使用方法
----
### 1. 代码使用

#### 要求
引用 QinSoft.Ioc.dll(.net40;.netstandard2.0)
#### 具体代码
```
BaseObjectDependency objectDependency = new BaseObjectDependency()
{
    CreateType = ObjectCreateType.Sington,
    Type = typeof(TestClass),
    ConstructDependencies = new BasePropertyDependency[]
    {
        new BasePropertyDependency()
        {
            Name="Action",
            Value =new BaseObjectDependency(){
            Type=typeof(ActionClass)
            }
        },
        new BasePropertyDependency()
        {
            Name="Msg",
            Value =new BaseObjectDependency(){
            Type=typeof(Guid),
            Value=Guid.NewGuid()
            }
        },
    }
};

IObjectCreator creator = new ObjectCreateFactory();

TestClass c = creator.CreateObject(objectDependency) as TestClass;
c.Greet();


TestClass c2 = creator.CreateObject(objectDependency) as TestClass;
c2.Greet();

Console.WriteLine(c == c2);
```
----
### 2. 配置文件使用

#### 要求
引用 QinSoft.Ioc.dll.Ioc.Ext.dll(.net40)
#### 配置文件
```
    <configSections>
        <section name="Ioc" type="QinSoft.Ioc.Ext.IocSection,QinSoft.Ioc.Ext"/>
    </configSections>

    <Ioc>
        <Objects>
            <Object Name="Action" Type="QinSoft.Ioc.Test.ActionClass,QinSoft.Ioc.Test" IsNull="True"></Object>
            <Object Name="Msg" Type="System.Guid" Value="e7b14227-7635-4b66-84ad-76f8ef294232"></Object>
            <Object Name="Test" Type="QinSoft.Ioc.Test.TestClass,QinSoft.Ioc.Test" CreateType="Sington">
            <ConstructProperties>
                <Property Name="Action" ObjectName="Action"></Property>
                <Property Name="Msg" ObjectName="Msg"></Property>
            </ConstructProperties>
        </Object>
    </Objects>
  </Ioc>
```

#### 代码
````
IConfigObjectCreateFactory createFactory = new ConfigObjectCreateFactory("Ioc");

TestClass c = createFactory.CreateObject("Test") as TestClass;
c.Greet();

TestClass c2 = createFactory.CreateObject("Test") as TestClass;
c2.Greet();

Console.WriteLine(c == c2);
````
----