using QinSoft.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Converter
{
    /// <summary>
    /// 转换器基类
    /// </summary>
    public class ConverterBase : IConverter
    {
        /// <summary>
        /// 转换方法
        /// </summary>
        /// <param name="source">源值</param>
        /// <param name="dependency">依赖</param>
        /// <returns>转换后值</returns>
        public virtual object Convert(object source, IDependency dependency)
        {
            return source;
        }
    }
}
