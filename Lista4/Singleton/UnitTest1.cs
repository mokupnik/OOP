using System;
using System.Threading;
using System.Collections.Generic;
using Xunit;

namespace Singleton
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {   Singleton f1 = Singleton.Instance();
            Singleton f2 = Singleton.Instance();
            Assert.Equal(f1,f2);
        }
        
        [Fact]
        public void Test2()

        {  FiveSecSingleton s1 = FiveSecSingleton.Instance();
            Thread.Sleep(10000);
            FiveSecSingleton s2 = FiveSecSingleton.Instance();
            Assert.NotEqual(s1,s2);




        }


       }
 }

