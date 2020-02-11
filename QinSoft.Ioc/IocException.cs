using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace QinSoft.Ioc
{
    /// <summary>
    /// Ioc异常
    /// </summary>
    public class IocException : Exception
    {
        public IocException() : base() { }
        public IocException(string message) : base(message) { }
        public IocException(string message, Exception innerException) : base(message, innerException) { }
        protected IocException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
