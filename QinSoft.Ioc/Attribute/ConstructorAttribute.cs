using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Attribute
{
    /// <summary>
    /// 构造函数特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public class ConstructorAttribute : System.Attribute
    {
    }
}
