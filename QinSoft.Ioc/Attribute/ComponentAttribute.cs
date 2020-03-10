using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Attribute
{
    /// <summary>
    /// 组件特性
    /// 必须增加该特性才能由Ioc容器管理
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : System.Attribute
    {
    }
}
