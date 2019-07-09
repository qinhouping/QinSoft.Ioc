using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Ioc.Core
{
    /// <summary>
    /// 对象实例映射
    /// </summary>
    public interface IObjectInstanceMapper
    {
        bool Exists(BaseObjectDependency dependency);

        object Find(BaseObjectDependency dependency);

        bool Remove(BaseObjectDependency dependency);

        bool Add(BaseObjectDependency dependency,object obj);
    }
}
