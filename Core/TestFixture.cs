using System.Collections.Generic;

namespace Core
{
    public class TestFixture:Test
    {
        List<ITest> tests;
        private string name;
        private TestReport report;

        public TestFixture()
        {
            name = "TestFixture";
            report = new TestReport();
            tests = new List<ITest>();
        }

        public TestFixture(string name)
        {
            this.name = name;
            tests = new List<ITest>();
        }

        public ITest Get(int index)
        {
            return tests.Count>index?tests[index]:null;
        }

        public void Add(ITest newTest)
        {
            tests.Add(newTest);
        }
        
        public override void Run()
        {
            foreach (var test in tests)
            {
                test.Run();

                var subReport = test.GetReport();

                if (subReport.Result == TestResult.Failed)
                    report.Result = TestResult.Failed;

                report.SubReports.Add(subReport);
            }
            if (report.Result != TestResult.Failed)
                report.Result = TestResult.Passed;
        }

        public override TestReport GetReport()
        {
            return report;
        }
    }
}
