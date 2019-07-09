# QinSoft.Ioc
依赖注入框架

## 使用方法

### 1. 代码使用
```
            BaseObjectDependency objectDependency = new BaseObjectDependency()
            {
                CreateType = ObjectCreateType.Sington,
                Type = typeof(TestClass),
                PropertyDependency = new BasePropertyDependency[]
                {
                    new BasePropertyDependency()
                    {
                        Name="A",
                        Value =new BaseObjectDependency(){
                             Type=typeof(System.Int32),
                             Value=2
                        }
                    }
                }
            };

            IObjectCreator creator = new ObjectCreateFactory();

            var a = creator.CreateObject(objectDependency);
```

### 2. 配置文件使用
