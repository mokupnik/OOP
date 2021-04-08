using System;

namespace L3Grasp
{
    public class Rama {
    public int dlugosc;
    public Rama(int nowaDlugosc){

         dlugosc = nowaDlugosc;

    }}

    public class Kolo
    {
    public int wymiary;
       public  Kolo(int noweWymiary) 
        {   
        
        wymiary = noweWymiary;
        }

    }



    public class Rower {
        public Rama rama;
        public Kolo kolo;
        public Rower(int cale, int dlugosc)
        
        {

               rama = new Rama(dlugosc);
               kolo = new Kolo(cale);
            


        }
    }
 // THE CREATOR - Tworzenie ramy i kola(Klasy B) dzieje sie w klasie Rower (Klasa A) poniewaz zawiera on obiekt B i używa go.
 // Więc spełnia conajmniej jeden warunek The Creatora

 // THE INFORMATION EXPERT - Rower tworzy instancje kola i ramy wiec ma wszystkie informacje potrzebne do tego, jest 'ekspertem' w tym temacie
    public abstract class  Artysta{
        private string profesja;
        private string Imie;
        private string Nazwisko;

        public  Artysta(string nowaProfesja, string noweImie, string noweNazwisko)
        {
            profesja = nowaProfesja;
            Imie = noweImie;
            Nazwisko = noweNazwisko; 


        }

        public abstract void wypisz();


    }

    public class Muzyk : Artysta{
        private string Imie;
        private string Nazwisko;
        public Muzyk(string noweImie, string noweNazwisko) : base ("Muzyk", noweImie, noweNazwisko)
        {

        }

        public override void wypisz()
        {
            Console.WriteLine("Rock n roll");

        }


    }
  // Low Coupling  - Zmniejszamy zależność między Muzykiem a Artystą(klasa abstract) mimo że Muzyk dziedziczy po klasie Artysty, 
  //nie będzie problemu żeby wykorzystać klase Artysta jeszcze wiele razy w innych przypadkach
  // Polimorfizm - dodajac jakies inne klasy nasz Artysta bedzie wiedział co wypisać - czy to zdefiniowane u muzyka, czy cos w innej klasie
  // High Cohension - duża spójność, łatwe do zrozumienia klasy i obiekty
    class Program
    {
        static void Main(string[] args)
        {
            Rower BMX = new Rower(15,150);
            Artysta IanCurtis = new Muzyk("Ian", "Curtis");
            IanCurtis.wypisz();
            

        }
    }
}
