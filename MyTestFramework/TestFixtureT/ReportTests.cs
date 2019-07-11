using Core;
using Moq;
using Xunit;

namespace Tests.TestFixtureT
{
    public class ReportTests
    {
        private Core.TestFixture testFixture;


        [Fact]
        public void Report_1_if_only_one_test_exists_in_fixture()
        {
            //Arange
            testFixture = new Core.TestFixture();
            testFixture.Add(
                new Core.TestCase(
                    () => { },
                    () => { }
                ));

            //Act
            testFixture.Run();
            var report = testFixture.GetReport();

            //Assert
            Assert.Contains("Test passed: 1.", report);
        }


        [Fact]
        public void Report_contains_number_of_passed_tests()
        {
            //Arange
            testFixture = new Core.TestFixture();
            testFixture.Add(
                new Core.TestCase(
                    () => { },
                    () => { }
                ));

            testFixture.Add(
                new Core.TestCase(
                    () => { },
                    () => { }
                ));

            //Act
            testFixture.Run();
            var report = testFixture.GetReport();

            //Assert
            Assert.Contains("Test passed: 2.", report);
        }

        [Fact]
        public void Report_contains_number_of_failed_tests_whatever_causes_fail()
        {
            //Arange
            testFixture = new Core.TestFixture();
            testFixture.Add(
                new Core.TestCase(
                    () => throw new System.Exception(),
                    () => { }
                ));

            testFixture.Add(
                new Core.TestCase(
                    () => { },
                    () => throw new System.Exception()
                ));

            //Act
            testFixture.Run();
            var report = testFixture.GetReport();

            //Assert
            Assert.Contains("Test failed: 2.", report);
        }

        [Fact]
        public void Report_contains_all_tests_reports()
        {
            //Arange
            testFixture = new Core.TestFixture();

            var passedTest = new Mock<ITest>();
            passedTest.Setup(test => test.GetReport()).Returns("cxc");

            var failedTest = new Mock<ITest>();
            failedTest.Setup(test => test.GetReport()).Returns("xcx");

            testFixture.Add(failedTest.Object);
            testFixture.Add(passedTest.Object);

            //Act
            testFixture.Run();
            var report = testFixture.GetReport();

            //Assert
            Assert.Contains("cxc", report);
            Assert.Contains("xcx", report);
        }

        [Fact]
        public void Report_contains_test_class_name()
        {
            //Arrange
            testFixture = new Core.TestFixture("fixture");

            //Act
            testFixture.Run();
            var report = testFixture.GetReport();

            //Assert
            Assert.Contains("fixture:", report);
        }

        [Fact]
        public void Default_name_is_TestFixture()
        {
            //Arrange
            testFixture = new Core.TestFixture();

            //Act
            testFixture.Run();
            var report = testFixture.GetReport();

            //Assert
            Assert.Contains("TestFixture:", report);
        }

        private class TestMock
        {
            public static void Test()
            {

            }
        }
    }
}
