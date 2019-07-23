using Core;
using Xunit;

namespace Tests.TestCase
{
    public class ReportTest
    {
        private Core.TestCase testCase;

        [Fact]
        public void Returned_object_contains_information_about_passed_test_result()
        {
            //Arrange
            testCase = new Core.TestCase(() => { }, () => { });

            //Act
            testCase.Run();
            var report = testCase.GetReportObject();

            //Assert
            Assert.Equal(TestResult.Passed, report.Result);
        }

        [Fact]
        public void Returned_object_contains_information_about_not_run_test_result()
        {
            //Arrange
            testCase = new Core.TestCase(() => { }, () => { });

            //Act
            var report = testCase.GetReportObject();

            //Assert
            Assert.Equal(TestResult.NotRun, report.Result);
        }

        [Fact]
        public void Returned_object_contains_information_about_failed_test_result()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { throw new System.Exception(); },
                () => { }
                );

            //Act
            testCase.Run();
            var report = testCase.GetReportObject();

            //Assert
            Assert.Equal(TestResult.Failed, report.Result);
        }

        [Fact]
        public void Contains_empty_case_if_test_not_run()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { },
                () => {}
                );

            //Act
            var report = testCase.GetReportObject();

            //Assert
            Assert.Empty(report.Case);
        }

        [Fact]
        public void Contains_setup_fail_as_case_if_test_setup_fail()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { },
                () => { throw new System.Exception(); }
                );

            //Act
            testCase.Run();
            var report = testCase.GetReportObject();

            //Assert
            Assert.Equal("Setup failed", report.Case);
        }

        [Fact]
        public void Contains_test_run_fail_as_case_if_test_run_fail()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { throw new System.Exception(); },
                () => { }
                );

            //Act
            testCase.Run();
            var report = testCase.GetReportObject();

            //Assert
            Assert.Equal("Test run failed", report.Case);
        }

        [Fact]
        public void Contains_setup_exception_if_setup_fail()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { },
                () => { throw new System.Exception("Error message"); }
                );

            //Act
            testCase.Run();
            var report = testCase.GetReportObject();

            //Assert
            Assert.Equal(typeof(System.Exception), report.Exception.GetType());
            Assert.Equal("Error message", report.Exception.Message);
        }

        [Fact]
        public void Contains_assertion_fail_as_case_if_test_failed()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { throw new Core.AssertException(); },
                () => { }
                );

            //Act
            testCase.Run();
            var report = testCase.GetReportObject();

            //Assert
            Assert.Equal("Assertion failed", report.Case);
        }

        [Fact]
        public void Contains_assertion_exception_if_test_failed()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { throw new AssertException("Assertion message"); },
                () => { }
                );

            //Act
            testCase.Run();
            var report = testCase.GetReportObject();

            //Assert
            Assert.Equal(typeof(AssertException), report.Exception.GetType());
            Assert.Equal("Assertion message", report.Exception.Message);
        }

        private void TestMethod()
        {

        }
    }
}
