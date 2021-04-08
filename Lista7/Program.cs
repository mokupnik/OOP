using System;
using System.Collections.Generic;
using System.Reflection;



namespace Lista9_2
{
    public class SimpleContainer
    {

    public interface ICreator {
        object Create();


    }
    private Dictionary<Type,ICreator> KnownTypes;

    public SimpleContainer ()
    {

    this.KnownTypes = new Dictionary<Type,ICreator>();


    }
    
public class ObjectResolver : ICreator {
        ConstructorInfo creator;
        public ObjectResolver (ConstructorInfo creator)
        {
            this.creator = creator;
        }

        public object Create()
        {

            return creator.Invoke(new object[] {});



        }

}
    


    public class SingletonResolver : ICreator {
        private object _instance;
        ConstructorInfo creator;
        public SingletonResolver(ConstructorInfo creator)
        {

            // this.creator = TType.GetConstructor(new Type[] {});
                this.creator = creator;
        }

        public object Create()
        {
            if(this._instance == null)
            {
                this._instance = creator.Invoke(new object[] {});

            }

        return this._instance;

        }



    }

   
   
    public void RegisterType<T>(bool Singleton ) where T : class
     {
        ConstructorInfo construct = typeof(T).GetConstructor(new Type[] {});
       var tt = typeof(T);
            if(Singleton)
            {
                this.KnownTypes[tt] = new SingletonResolver(construct);

            }
            else 
            {

                this.KnownTypes[tt] = new ObjectResolver(construct);
            }


    }
    public void RegisterType<From, To>( bool Singleton ) where To : From
    {
       ConstructorInfo construct = typeof(To).GetConstructor(new Type[] {});

        if(Singleton)
        {



            this.KnownTypes[typeof(From)] = new SingletonResolver(construct);
        }
        else
        {


            this.KnownTypes[typeof(From)] = new ObjectResolver(construct);

        }
    }

    public object Helper (Type TT)
    {


        ConstructorInfo constructor = TT.GetConstructor(new Type[] {});
        return constructor.Invoke(new object[] {});

    }
    public T Resolve<T>()
    {
        ICreator creatorr;
        if(this.KnownTypes.ContainsKey(typeof(T)))
        {
            creatorr = this.KnownTypes[typeof(T)];
            return (T)creatorr.Create();
        }
        if(typeof(T).IsInterface)
        {

            throw new Exception("Unregistered interface");

        }   
        else 
        {
            return (T) Helper(typeof(T));

        }
    

    



    }

    }
    abstract class Foo {};
        class IFoo : Foo {};

    class Program
    {
        static void Main(string[] args)
        {
          

            var container = new SimpleContainer();

            container.RegisterType<Foo,IFoo>(false);
            var bar1= container.Resolve<Foo>();
            Console.WriteLine(bar1);
            




        }
    }
}