using Xunit;

namespace Tests.TestCase
{
    public class ReportTest
    {
        private Core.TestCase testCase;


        [Fact]
        public void Test_passed_information_after_test_passed()
        {
            //Arrange
            testCase = new Core.TestCase(() => { }, () => { });

            //Act
            testCase.Run();

            //Assert
            Assert.Contains("Test passed", testCase.GetReport());
        }

        [Fact]
        public void Test_failed_information_after_test_setup_failed()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { },
                () => throw new System.Exception("error message")
                );

            //Act
            testCase.Run();

            //Assert
            Assert.Contains("Test failed.", testCase.GetReport());
            Assert.Contains("Startup failed: System.Exception: error message", testCase.GetReport());
        }

        [Fact]
        public void Reprt_contains_called_method_name()
        {
            //Arrange
            testCase = new Core.TestCase(
                TestMethod,
                () => { }
                );

            //Act
            testCase.Run();

            //Assert
            Assert.Contains("TestMethod:\n", testCase.GetReport());
        }
        
        [Fact]
        public void Report_after_assert_fail_does_not_contain_exception_type()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { throw new Core.AssertException(); },
                () => { }
                );

            //Act
            testCase.Run();

            //Assert
            Assert.DoesNotContain("Core.AssertException:", testCase.GetReport());
        }

        [Fact]
        public void Report_after_assert_fail_contains_assert_error_message()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { throw new Core.AssertException(); },
                () => { }
                );

            //Act
            testCase.Run();

            //Assert
            Assert.Contains("Assert failed:", testCase.GetReport());
        }

        [Fact]
        public void Report_after_assert_fail_does_not_contain_standard_error_message()
        {
            //Arrange
            testCase = new Core.TestCase(
                () => { throw new Core.AssertException(); },
                () => { }
                );

            //Act
            testCase.Run();

            //Assert
            Assert.DoesNotContain("Method run failed:", testCase.GetReport());
        }

        //[Fact]
        //public void Report_after_assert_fail_contains_assert_method_name()
        //{
        //    //Arrange
        //    var assertInfo = typeof(Core.MyAssert).GetMethod("Fail");

        //    testCase = new Core.TestCase(
        //        () => { throw new Core.AssertException(assertInfo); },
        //        () => { }
        //        );

        //    //Act
        //    testCase.Run();

        //    //Assert
        //    Assert.Contains("Assert.Fail() failure:", testCase.GetReport());
        //}

        private void TestMethod()
        {

        }
    }
}
