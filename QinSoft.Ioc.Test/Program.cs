using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QinSoft.Ioc.Core;

namespace QinSoft.Ioc.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseObjectDependency objectDependency = new BaseObjectDependency()
            {
                CreateType = ObjectCreateType.Sington,
                Type = typeof(TestClass),
                PropertyDependency = new BasePropertyDependency[]
                {
                    new BasePropertyDependency()
                    {
                        Name="A",
                        Value =new BaseObjectDependency(){
                             Type=typeof(System.Int32),
                             Value=2
                        }
                    }
                }
            };

            IObjectCreator creator = new ObjectCreateFactory();

            var a = creator.CreateObject(objectDependency);


        }

    }

    class TestClass
    {
        public int A { get; set; }
    }
}
