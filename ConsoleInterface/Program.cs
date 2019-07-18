using Core;
using System;
using System.IO;

namespace ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            var testFramework = new TestFramework(new TestDetector(), new TestFixtureFactory(new TestDetector()), new DirectoryScanner());
            var report = testFramework.Run(Directory.GetCurrentDirectory());
            Console.WriteLine(report);
            Console.ReadKey();
        }
    }

    class Test
    {
        [Test]
        public void Fail_always()
        {
            throw new NullReferenceException();
        }

        [Test]
        public void Assert_pass()
        {
            MyAssert.Equal(10, 10);
        }

        [Test]
        public void Assert_fail()
        {
            MyAssert.Contains("ffgggs","bvvc");
        }

        [Test]
        public void Assert_always_fail()
        {
            MyAssert.Fail("Always fail");
        }

        [Test]
        public void Assert_private_tester()
        {
            var test = new PrivateClass();

            test.SetValueToTen();

            var PO = new PrivateTester<PrivateClass>();
            var output = PO.GetPrivateField(test, "kal");

            MyAssert.Equal(10, output);
        }
    }
    
    class PrivateClass
    {
        private int value = 0;
        
        public void SetValueToTen()
        {
            value = 10;
        }
    }
}
