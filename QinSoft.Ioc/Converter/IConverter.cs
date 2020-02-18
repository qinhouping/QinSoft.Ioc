using QinSoft.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Converter
{
    /// <summary>
    /// 转换器接口
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// 转换方法
        /// </summary>
        /// <param name="source">源值</param>
        /// <param name="dependency">依赖</param>
        /// <returns>转换后值</returns>
        object Convert(object source, IDependency dependency);
    }
}
