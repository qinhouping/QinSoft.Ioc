using System;
using System.Collections.Generic;
using System.Text;
using QinSoft.Ioc.Core;

namespace QinSoft.Ioc.Core
{
    /// <summary>
    /// 对象创建接口
    /// </summary>
    public interface IObjectCreator
    {
        object CreateObject(BaseObjectDependency objectDependency);
    }
}
