using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc
{
    /// <summary>
    /// 容器对象工厂实现
    /// </summary>
    public class ContainerObjectFactoryImp : ObjectFactory
    {
        public ObjectContainer objectContainer { get; protected set; }
        public ContainerObjectFactoryImp(ObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }
        public override object CreateObject(DependencyInjection dependencyInjection)
        {
            throw new NotImplementedException();
        }
    }
}
