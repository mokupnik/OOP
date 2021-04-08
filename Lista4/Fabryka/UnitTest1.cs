using System;
using Xunit;

namespace Factory
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            Factory factory = new Factory();
            factory.NowyPracownik(new KwadratFactoryWorker());
            Ksztalt square = factory.Wybierz("Square", 5);
            

            Assert.NotNull(factory.pracownicy);

        }
    }

}
