using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QinSoft.Ioc.Dependency
{
    public abstract class DependencyBase : IDependency
    {
        public IConverter Converter { get; protected set; }

        public DependencyBase(IConverter converter)
        {
            if (converter == null) throw new ArgumentNullException("converter");
            this.Converter = converter;
        }

        public DependencyBase() : this(new ConverterBase())
        {
        }

        public abstract object GetSource();
        public override object GetValue()
        {
            return this.Converter.Convert(this.GetSource(), this);
        }
    }
}
