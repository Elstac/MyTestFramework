using System;

namespace Core
{
    public class TestCase:Test
    {
        public string Log { get; set; }
        public string Error { get; set; }

        private string report;
        private Action testMethod;
        private Action setUp;

        public TestCase(Action testMethod, Action setUp)
        {
            Passed = true;
            Log = "";
            report = "";
            this.testMethod = testMethod;
            this.setUp = setUp != null?setUp:()=> { };
        }
        public override void Run()
        {
            report = $"{testMethod.Method.Name}:\n";
            try
            {
                SetUp();
                TestMethod();
                report += "Test passed";
            }
            catch (Exception)
            {
                Passed = false;
                report += "Test failed. " + Error;
            }
            
            TearDown();
        }

        public void TestMethod()
        {
            Log += "Run ";
            RunSupervised(testMethod, "Method run failed");
        }

        public void SetUp()
        {
            Log += "Setup ";
            RunSupervised(setUp, "Startup failed");
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
                Error = $"{errorMessage}: { e.GetType()}: {e.Message}";
                throw e;
            }
        }

        public override string GetReport()
        {
            return report;
        }
    }
}
