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
                CreateType = ObjectCreateType.Normal,
                Type = typeof(TestClass),
                ConstructDependencies = new BasePropertyDependency[]
                {
                    new BasePropertyDependency()
                    {
                        Name="Action",
                        Value =new BaseObjectDependency(){
                             Type=typeof(ClassDo)
                        }
                    }
                }
            };

            IObjectCreator creator = new ObjectCreateFactory();

            TestClass c = creator.CreateObject(objectDependency) as TestClass;
            c.Action.hello();


            TestClass c2 = creator.CreateObject(objectDependency) as TestClass;
            c2.Action.hello();

            Console.WriteLine(c == c2);

            Console.ReadKey();

        }

    }

    class TestClass
    {
        public TestClass(IDo Action)
        {
            this.Action = Action;
        }

        public IDo Action { get; set; }
    }

    public interface IDo
    {
        void hello();
    }

    public class ClassDo : IDo
    {

        public void hello()
        {
            Console.WriteLine("hello");
        }
    }
}
