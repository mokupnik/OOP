using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Factory
{

    public abstract class Ksztalt{

    }

    public class Kwadrat : Ksztalt{
        int x;
        public Kwadrat (int x)
        {
            this.x = x;
        }

    }

    public class Prostokat : Ksztalt {
        int x,y;
        public Prostokat (int x, int y)
        {
            this.x=x;
            this.y=y;
        }

    }

    public interface KsztaltFactoryWorker {
        
        
         bool Akceptuje( string args);
        Ksztalt Stworz(params object[] args);

    }

    public class KwadratFactoryWorker : KsztaltFactoryWorker {
        public bool Akceptuje(string args){
            return args == "Square";

        }
        public Ksztalt Stworz(params object[] args) {
            if(args.Length != 1) {
                Console.WriteLine("Cos poszlo nie tak"); // new
            }
            int x = (int)args[0];
            return new Kwadrat(x);


        }
    }

    public class ProstokatFactoryWorker : KsztaltFactoryWorker {
        public bool Akceptuje(string args) {
            return args == "Rectangle";


        }

        public Ksztalt Stworz(params object[] args) {
            if(args.Length !=2) {
                Console.WriteLine("Cos poszlo nie tak");

            }
        int x = (int)args[0];
        int y = (int)args[1];
        return new Prostokat(x,y);

        }

    }


    public class Factory{
    public List<KsztaltFactoryWorker> pracownicy = new List<KsztaltFactoryWorker>();
    public void NowyPracownik (KsztaltFactoryWorker pracownik){
        pracownicy.Add(pracownik);

    } 
    
    public Ksztalt Wybierz(string ksztalt, params object[] args)
    {
        foreach (var pracownik in pracownicy) {

            if(pracownik.Akceptuje(ksztalt)) {

                return pracownik.Stworz(args);

            }
            

        }
        throw new Exception();

    }

    }
    class Program {
        static void Main(string[] args) {
        }
    }



}
