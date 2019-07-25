using System;
using System.Collections.Generic;
using System.Reflection;

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
        public List<TestReport> SubReports { get; set; }
        public string Name { get; set; }

        public TestReport()
        {
            Case = "";
            Result = TestResult.NotRun;
            SubReports = new List<TestReport>();
        }
        public TestReport(MethodInfo methodInfo)
        {
            Case = "";
            Result = TestResult.NotRun;
            SubReports = new List<TestReport>();
            Name = methodInfo.Name;
        }
    }
}
