using System;
using System.Collections.Generic;

namespace Core
{
    public enum TestResult
    {
        Passed,
        NotRun,
        Failed
    }
    public class TestReport
    {
        public TestResult Result { get; set; }
        public string Case { get; set; }
        public Exception Exception { get; set; }

        public TestReport()
        {
            Case = "";
            Result = TestResult.NotRun;
        }
    }
}
