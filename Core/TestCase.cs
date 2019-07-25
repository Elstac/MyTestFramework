using System;

namespace Core
{
    public class TestCase:Test
    {
        public string Log { get; set; }
        
        private TestReport testReport;
        private Action testMethod;
        private Action setUp;
        private Action tearDown;

        public TestCase(Action testMethod, Action setUp)
        {
            Log = "";
            testReport = new TestReport(testMethod.Method);

            this.testMethod = testMethod;
            this.setUp = setUp ?? (() => { });
            tearDown = () => { };
        }

        public TestCase(Action testMethod, Action setUp, Action tearDown)
        {
            Log = "";
            testReport = new TestReport(testMethod.Method);

            this.testMethod = testMethod;
            this.setUp = setUp ?? (() => { });
            this.tearDown = tearDown ?? (() => { });
        }

        public override TestReport GetReport()
        {
            return testReport;
        }

        public override void Run()
        {
            try
            {
                SetUp();
                TestMethod();
                TearDown();
                testReport.Result = TestResult.Passed;
            }
            catch (Exception)
            {
                testReport.Result = TestResult.Failed;
            }

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
            RunSupervised(tearDown, "Teardown failed");
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
    }
}
