using System;

namespace Core
{
    public class TestCase:Test
    {
        public string Log { get; set; }

        private string report;
        private TestReport testReport;
        private Action testMethod;
        private Action setUp;

        public TestCase(Action testMethod, Action setUp)
        {
            Passed = true;

            Log = "";
            report = "";
            testReport = new TestReport();

            this.testMethod = testMethod;
            this.setUp = setUp ?? (() => { });
        }

        public TestReport GetReportObject()
        {
            return testReport;
        }

        public override void Run()
        {
            report = $"{testMethod.Method.Name}:\n";
            try
            {
                SetUp();
                TestMethod();
                report += "Test passed";
                testReport.Result = TestResult.Passed;
            }
            catch (Exception)
            {
                Passed = false;
                testReport.Result = TestResult.Failed;
            }

            TearDown();
        }

        public void TestMethod()
        {
            Log += "Run ";
            RunSupervised(testMethod, "Test run failed");
        }

        public void SetUp()
        {
            Log += "Setup ";
            RunSupervised(setUp, "Setup failed");
        }

        public void TearDown()
        {
            Log += "Teardown ";
        }
        
        private void RunSupervised(Action method, string errorMessage)
        {
            try
            {
                method.Invoke();
            }
            catch (Exception e)
            {
                if (e is AssertException)
                {
                    testReport.Case = "Assertion failed";
                }
                else
                {
                    testReport.Case = errorMessage;
                }

                testReport.Exception = e;
                throw e;
            }
        }

        public override string GetReport()
        {
            return report;
        }
    }
}
