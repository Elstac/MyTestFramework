using Core;
using Moq;
using System;
using Xunit;

namespace Tests.TestFixtureT
{
    public class ReportTests
    {
        private Core.TestFixture testFixture;
        
        [Fact]
        public void Result_not_run_if_fixture_not_run()
        {
            //Arrange
            testFixture = new Core.TestFixture();

            //Act 
            var report = testFixture.GetReport();

            //Assert
            Assert.Equal(TestResult.NotRun, report.Result);
        }

        [Fact]
        public void Whole_fixture_fails_after_at_least_one_test_fails()
        {
            //Arrange
            testFixture = new Core.TestFixture();
            var failedTest = new Core.TestCase(() => { }, () => throw new Exception());
            var succsessfulTest = new Core.TestCase(() => { }, () => { });

            testFixture.Add(succsessfulTest);
            testFixture.Add(failedTest);

            //Act
            testFixture.Run();
            var report = testFixture.GetReport();

            //Assert
            Assert.Equal(TestResult.Failed, report.Result);
        }

        [Fact]
        public void Whole_fixture_pass_after_all_tests_passed()
        {
            //Arrange
            testFixture = new Core.TestFixture();
            var failedTest = new Core.TestCase(() => { }, () => { });
            var succsessfulTest = new Core.TestCase(() => { }, () => { });

            testFixture.Add(succsessfulTest);
            testFixture.Add(failedTest);

            //Act
            testFixture.Run();
            var report = testFixture.GetReport();

            //Assert
            Assert.Equal(TestResult.Passed, report.Result);
        }

        [Fact]
        public void Report_contains_all_tests_reports()
        {
            //Arange
            testFixture = new Core.TestFixture();

            var mockReport = new TestReport();
            var passedTest = new Mock<ITest>();
            passedTest.Setup(test => test.GetReport()).Returns(mockReport);

            var mockReport2 = new TestReport();
            var failedTest = new Mock<ITest>();
            failedTest.Setup(test => test.GetReport()).Returns(mockReport2);

            testFixture.Add(failedTest.Object);
            testFixture.Add(passedTest.Object);

            //Act
            testFixture.Run();
            var report = testFixture.GetReport();

            //Assert
            Assert.Contains(mockReport, report.SubReports);
            Assert.Contains(mockReport2, report.SubReports);
        }

        private class TestMock
        {
            public static void Test()
            {

            }
        }
    }
}
