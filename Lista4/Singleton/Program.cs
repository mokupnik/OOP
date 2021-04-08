using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Singleton
{

    public class Singleton
    {
    private static Singleton instancja ;
    private static readonly object locked = new object();
    private int m_nCounter = 0;
 
    public static Singleton Instance()
   {
            if (instancja == null) {
                lock (locked) {
                    if (instancja == null) {
                        instancja = new Singleton();
                    }
                }
            }
            return instancja;
 
   }
    }

public class SingletonThread 
{

    [ThreadStatic] private static SingletonThread instancja;
    [ThreadStatic] private static readonly object locked = new object();
    public static SingletonThread Instance()
    {
      
                if (instancja == null)
                {
                lock(locked) {
                    instancja = new SingletonThread();
                }
                }
                return instancja;
            
        
    }
 

}



public class FiveSecSingleton {
    private static FiveSecSingleton instancja;
    private static DateTime czas;


    public static FiveSecSingleton Instance()
    {
       
                if (instancja == null || czas < DateTime.Now)
                {
                    instancja = new FiveSecSingleton();
                    czas = DateTime.Now.AddSeconds(5);
                }
                return instancja;
        
    }


}





    public class Program
{
    static void Main(string[] args)
    {
        

}
}
}