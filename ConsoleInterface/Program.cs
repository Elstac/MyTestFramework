using Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            var testFramework = new TestFramework(
                new TestFixtureFactory(
                    new TestDetector()
                    ),
                new DirectoryScanner(),
                new AssemblyScanner(
                    new TestDetector()
                    )
                );

            var report = testFramework.Run(Directory.GetCurrentDirectory());
            Write(report);

            Console.ReadKey();
        }

        private static void Write(IEnumerable<TestReport> reports)
        {
            foreach (var report in reports)
            {
                Console.WriteLine(report.Name);
                foreach (var subreport in report.SubReports)
                {
                    Console.WriteLine($"Test: {subreport.Name}");
                    switch (subreport.Result)
                    {
                        case TestResult.Passed:
                            Console.WriteLine("Test passed");
                            break;
                        case TestResult.NotRun:
                            Console.WriteLine("Test not run");
                            break;
                        case TestResult.Failed:
                            Console.WriteLine("Test failed");
                            Console.WriteLine($"Case: {subreport.Case}");
                            Console.WriteLine($"Exception: {subreport.Exception.Message}");
                            break;
                        default:
                            Console.WriteLine("Test not run");
                            break;
                    }
                }
            }
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
