using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Attribute
{
    /// <summary>
    /// 构造函数特性
    /// 构造函数特性，如果该Type只有一个构造函数可不添加该特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public class ConstructorAttribute : System.Attribute
    {
    }
}
