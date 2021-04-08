using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Lista9_2 {

public class UnitTests
{
public interface Fff {};
abstract class Foo {};
class IFoo : Foo {};
class JFoo : Foo {};

class f {};


    [Fact]
    public void Test_Same_Instance_Singleton()
    {
        SimpleContainer c = new SimpleContainer();
        c.RegisterType<IFoo>(true);

        IFoo f1 = c.Resolve<IFoo>();
        IFoo f2 = c.Resolve<IFoo>();
        Assert.Equal(f1,f2);


    }
    [Fact]
    public void Test_Register_Type()
    {
        SimpleContainer c = new SimpleContainer();
        c.RegisterType<Foo,IFoo>(false);
        var f1 = c.Resolve<Foo>();
        var f2 = c.Resolve<IFoo>();
        Assert.IsType<IFoo>(f1);
    }

    [Fact]
        public void Test_same_classes_not_singleton() 
        {
            SimpleContainer container = new SimpleContainer();
            container.RegisterType<f>(false);

            var foo1 = container.Resolve<f>();
            var foo2 = container.Resolve<f>();

            Assert.NotEqual(foo1,foo2);
        }

    [Fact]
    
        public void Test_Exception_Interface() {
            SimpleContainer c = new SimpleContainer();
            Exception ex = Assert.Throws<Exception>(() => c.Resolve<Fff>());
            Assert.Equal("Unregistered interface",ex.Message);


        }


    [Fact]
        public void Test_change_registered_type()
        {
            SimpleContainer c = new SimpleContainer();
            c.RegisterType<Foo,IFoo>(false);
            var IFo = c.Resolve<Foo>();
            c.RegisterType<Foo,JFoo>(false);
            var JFo = c.Resolve<JFoo>();
            Assert.IsType<IFoo>(IFo);
            Assert.IsType<JFoo>(JFo);

        


        }
    [Fact]
    public void Test_change_registered_type_singleton() {
           
           
           SimpleContainer c = new SimpleContainer();
            c.RegisterType<Foo,IFoo>(true);
            var IFo = c.Resolve<Foo>();
            c.RegisterType<Foo,JFoo>(true);
            var JFo = c.Resolve<JFoo>();
            Assert.True(typeof(IFoo).IsInstanceOfType(IFo));
            Assert.True(typeof(JFoo).IsInstanceOfType(JFo));

        }


}



}