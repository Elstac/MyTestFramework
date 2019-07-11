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
        [Setup]
        public void Setup()
        {
            throw new InvalidCastException("message");
        }

        [Test]
        public void Fail_always()
        {
            throw new NullReferenceException();
        }

        [Test]
        public void Sucess_always()
        {

        }
    }
    
}
