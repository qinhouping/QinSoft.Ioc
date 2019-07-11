using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QinSoft.Ioc.Core;
using QinSoft.Ioc.Ext;

namespace QinSoft.Ioc.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                BaseObjectDependency objectDependency = new BaseObjectDependency()
                {
                    CreateType = ObjectCreateType.Sington,
                    Type = typeof(TestClass),
                    ConstructDependencies = new BasePropertyDependency[]
                    {
                        new BasePropertyDependency()
                        {
                            Name="Action",
                            Value =new BaseObjectDependency(){
                                 Type=typeof(ActionClass)
                            }
                        },
                        new BasePropertyDependency()
                        {
                            Name="Msg",
                            Value =new BaseObjectDependency(){
                                 Type=typeof(Guid),
                                 Value=Guid.NewGuid()
                            }
                        },
                    }
                };

                IObjectCreator creator = new ObjectCreateFactory();

                TestClass c = creator.CreateObject(objectDependency) as TestClass;
                c.Greet();


                TestClass c2 = creator.CreateObject(objectDependency) as TestClass;
                c2.Greet();

                Console.WriteLine(c == c2);
            }
            {
                IConfigObjectCreateFactory createFactory = new ConfigObjectCreateFactory("Ioc");

                TestClass c = createFactory.CreateObject("Test") as TestClass;
                c.Greet();

                TestClass c2 = createFactory.CreateObject("Test") as TestClass;
                c2.Greet();

                Console.WriteLine(c == c2);
            }

            Console.ReadKey();

        }

    }

    class TestClass
    {
        private string Msg { get; set; }
        private IAction Action { get; set; }

        public TestClass(IAction Action, Guid Msg)
        {
            this.Action = Action;
            this.Msg = Msg.ToString();
        }

        public void Greet()
        {
            this.Action?.Say(Msg);
        }

    }

    public interface IAction
    {
        void Say(string Msg);
    }

    public class ActionClass : IAction
    {

        public void Say(string Msg)
        {
            Console.WriteLine(Msg);
        }
    }
}
