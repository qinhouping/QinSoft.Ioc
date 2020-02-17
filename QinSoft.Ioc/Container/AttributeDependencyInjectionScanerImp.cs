using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QinSoft.Ioc.Container
{
    public class AttributeDependencyInjectionScanerImp : DependencyInjectionScaner
    {
        public AttributeDependencyInjectionScanerImp(params string[] namespaces)
        {
        }

        public override DependencyInjection[] Scan()
        {
            throw new NotImplementedException();
        }
    }
}
